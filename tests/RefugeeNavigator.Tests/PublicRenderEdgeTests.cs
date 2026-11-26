using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace RefugeeNavigator.Tests;

public sealed class PublicRenderEdgeTests(PublicRenderEdgeFixture fixture) : IClassFixture<PublicRenderEdgeFixture>
{
    [Fact]
    public async Task Public_edge_preserves_https_and_public_seo_without_credentials()
    {
        using var client = new HttpClient(new HttpClientHandler { AllowAutoRedirect = false })
        {
            BaseAddress = fixture.Origin
        };
        client.DefaultRequestHeaders.Add("X-Forwarded-Proto", "http");

        using var home = await client.GetAsync("/en/");
        Assert.Equal(HttpStatusCode.OK, home.StatusCode);
        Assert.Empty(home.Headers.WwwAuthenticate);
        Assert.False(home.Headers.Contains("X-Robots-Tag"));
        var html = await home.Content.ReadAsStringAsync();
        Assert.Contains("rel=\"canonical\"", html);
        var scripts = Regex.Matches(html.Split("</head>", 2)[0],
            """<script\b(?=[^>]*\stype="(?<type>[^"]+)")[^>]*>(?<json>.*?)</script>""",
            RegexOptions.Singleline);
        var jsonLd = Assert.Single(scripts.Cast<Match>(), script =>
            WebUtility.HtmlDecode(script.Groups["type"].Value) == "application/ld+json");
        using var document = JsonDocument.Parse(jsonLd.Groups["json"].Value);
        Assert.Equal("https://schema.org", document.RootElement.GetProperty("@context").GetString());
        var graph = document.RootElement.GetProperty("@graph").EnumerateArray().ToArray();
        var page = Assert.Single(graph, entity => entity.GetProperty("@type").GetString() == "WebPage");
        var site = Assert.Single(graph, entity => entity.GetProperty("@type").GetString() == "WebSite");
        Assert.Equal("en", page.GetProperty("inLanguage").GetString());
        Assert.Equal("en", site.GetProperty("inLanguage").GetString());
        Assert.Equal(site.GetProperty("@id").GetString(),
            page.GetProperty("isPartOf").GetProperty("@id").GetString());

        using var language = await client.GetAsync("/set-language?culture=uk&returnUrl=%2Fuk%2F");
        Assert.Equal(HttpStatusCode.Redirect, language.StatusCode);
        Assert.Equal("/uk/", language.Headers.Location?.OriginalString);
        var cookie = Assert.Single(language.Headers.GetValues("Set-Cookie"));
        Assert.Contains("; secure", cookie, StringComparison.OrdinalIgnoreCase);

        using var robots = await client.GetAsync("/robots.txt");
        Assert.Equal(HttpStatusCode.OK, robots.StatusCode);
        Assert.Contains("Sitemap: https://", await robots.Content.ReadAsStringAsync());

        using var sitemap = await client.GetAsync("/sitemap.xml");
        Assert.Equal(HttpStatusCode.OK, sitemap.StatusCode);
        Assert.Equal("application/xml", sitemap.Content.Headers.ContentType?.MediaType);
    }
}

public sealed class PublicRenderEdgeFixture : SeoHttpFixture
{
    protected override bool UsePublicRenderEdge => true;
}
