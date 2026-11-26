using System.Globalization;
using System.Net;
using System.Resources;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Tests;

public sealed class SeoHttpPolicyTests(SeoHttpFixture fixture) : IClassFixture<SeoHttpFixture>
{
    [Theory]
    [InlineData("/en/invalid-seo-route")]
    [InlineData("/uk/invalid-seo-route")]
    [InlineData("/ru/invalid-seo-route")]
    [InlineData("/uk/countries/invalid-country")]
    [InlineData("/ru/countries/germany/categories/invalid-category")]
    [InlineData("/en/guides/germany/invalid-guide")]
    [InlineData("/en/not-found")]
    [InlineData("/uk/not-found")]
    [InlineData("/ru/not-found")]
    [InlineData("/en/articles/germany/temporary-protection/00000000-0000-0000-0000-000000000000")]
    public async Task Invalid_destinations_are_real_not_found_responses(string path)
    {
        using var client = Client(authenticate: true);
        using var response = await client.GetAsync(path);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Null(response.Headers.Location);
        var html = await response.Content.ReadAsStringAsync();
        var head = html.Split("</head>", 2)[0];
        Assert.DoesNotContain("rel=\"canonical\"", head);
        Assert.DoesNotContain("property=\"og:url\"", head);
        Assert.DoesNotContain("hreflang=", head);
        Assert.DoesNotContain("application/ld+json", html);
        if (path.EndsWith("/not-found", StringComparison.Ordinal)
            || path.EndsWith("/invalid-seo-route", StringComparison.Ordinal))
        {
            Assert.Equal("text/html", response.Content.Headers.ContentType?.MediaType);
            Assert.False(string.IsNullOrWhiteSpace(html));
            var language = path.Split('/')[1];
            Assert.Contains($"<html lang=\"{language}\">", html);
            var culture = CultureInfo.GetCultureInfo(language);
            var resources = new ResourceManager("RefugeeNavigator.Web.Resources.Localization.SharedResource",
                typeof(SharedResource).Assembly);
            Assert.Equal(resources.GetString("NotFound.Heading", culture),
                ReadLocalizedText(html, "h1", "id", "not-found-heading"));
            Assert.Equal(resources.GetString("NotFound.Description", culture),
                ReadLocalizedText(html, "p", "class", "description"));
            Assert.Contains("name=\"robots\" content=\"noindex, follow\"", head);
        }
    }

    private static string ReadLocalizedText(string html, string element, string attribute, string value)
    {
        // Razor CSS isolation adds attributes to opening tags. Match the required attribute
        // independently of its position, then compare the full decoded localized text.
        var matches = Regex.Matches(html,
            $"""<{element}\b(?=[^>]*\s{attribute}="{Regex.Escape(value)}")[^>]*>(.*?)</{element}>""",
            RegexOptions.Singleline);
        return WebUtility.HtmlDecode(Assert.Single(matches).Groups[1].Value);
    }

    [Fact]
    public async Task Legacy_language_redirect_keeps_the_query()
    {
        using var client = Client(authenticate: true);
        using var response = await client.GetAsync("/search?q=permit&utm_source=test");
        Assert.Equal(HttpStatusCode.PermanentRedirect, response.StatusCode);
        Assert.Equal("/uk/search?q=permit&utm_source=test", response.Headers.Location?.OriginalString);
    }

    [Theory]
    [InlineData("/robots.txt")]
    [InlineData("/sitemap.xml")]
    public async Task Private_crawling_endpoints_still_require_authentication(string path)
    {
        using var client = Client(authenticate: false);
        using var response = await client.GetAsync(path);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.Contains("noindex, nofollow", response.Headers.GetValues("X-Robots-Tag"));
        Assert.Empty(await response.Content.ReadAsStringAsync());
    }

    private HttpClient Client(bool authenticate)
    {
        var client = new HttpClient(new HttpClientHandler { AllowAutoRedirect = false, UseProxy = false })
        {
            BaseAddress = fixture.Origin
        };
        if (authenticate)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes("rne-smoke:" + fixture.Password)));
        }
        return client;
    }
}
