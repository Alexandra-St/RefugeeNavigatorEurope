using System.Globalization;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using RefugeeNavigator.Web.Components;
using RefugeeNavigator.Web.Security;
using RefugeeNavigator.Web.Seo;

namespace RefugeeNavigator.Tests;

public sealed class SeoStructuredDataTests
{
    private const string Origin = "https://canonical.example";

    [Theory]
    [InlineData("en", "Article & </script><script>alert(1)</script>", "Home")]
    [InlineData("uk", "Стаття", "Головна")]
    [InlineData("ru", "Статья", "Главная")]
    public async Task Article_head_embeds_one_safe_localized_graph(string language, string title, string home)
    {
        var urls = new SeoMetadataGenerator(Origin);
        var path = $"/{language}/articles/germany/housing/123";
        var metadata = urls.Create(title, "Description < &", path, []);
        var date = new DateTimeOffset(2026, 10, 20, 13, 30, 0, TimeSpan.FromHours(2));
        var parents = new SeoBreadcrumb[]
        {
            new(home, "/"), new("Country", "/countries/germany"),
            new("Category", "/countries/germany/categories/housing")
        };
        var oldCulture = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(language);
        try
        {
            var head = await RenderHead(metadata, urls, StructuredPageKind.Article, parents, date, "public");
            var json = Assert.Single(ReadJsonLdScripts(head));
            Assert.DoesNotContain('<', json);
            Assert.DoesNotContain("&quot;", json);
            using var document = JsonDocument.Parse(json);
            Assert.Equal("https://schema.org", document.RootElement.GetProperty("@context").GetString());
            var graph = document.RootElement.GetProperty("@graph").EnumerateArray()
                .ToDictionary(node => node.GetProperty("@type").GetString()!);
            Assert.Equal(3, graph.Count);
            var page = graph["WebPage"];
            var article = graph["Article"];
            var breadcrumb = graph["BreadcrumbList"];
            var canonical = Origin + path;
            Assert.Equal(canonical + "#webpage", page.GetProperty("@id").GetString());
            Assert.Equal(canonical + "#article", article.GetProperty("@id").GetString());
            Assert.Equal(canonical + "#breadcrumb", breadcrumb.GetProperty("@id").GetString());
            Assert.Equal(canonical + "#webpage", article.GetProperty("mainEntityOfPage").GetProperty("@id").GetString());
            Assert.Equal(canonical, article.GetProperty("url").GetString());
            Assert.Equal(language, article.GetProperty("inLanguage").GetString());
            Assert.Equal(title, article.GetProperty("headline").GetString());
            Assert.Equal(metadata.Description, article.GetProperty("description").GetString());
            Assert.Equal(date.ToString("O", CultureInfo.InvariantCulture), article.GetProperty("dateModified").GetString());
            foreach (var unsupported in new[] { "datePublished", "author", "publisher", "image" })
                Assert.False(article.TryGetProperty(unsupported, out _));

            var items = breadcrumb.GetProperty("itemListElement").EnumerateArray().ToArray();
            Assert.Equal(new[] { 1, 2, 3, 4 }, items.Select(item => item.GetProperty("position").GetInt32()));
            Assert.Equal(new[] { home, "Country", "Category", title }, items.Select(item => item.GetProperty("name").GetString()));
            Assert.Equal(new[]
            {
                $"{Origin}/{language}/", $"{Origin}/{language}/countries/germany",
                $"{Origin}/{language}/countries/germany/categories/housing", canonical
            }, items.Select(item => item.GetProperty("item").GetString()));
        }
        finally
        {
            CultureInfo.CurrentUICulture = oldCulture;
        }
    }

    [Theory]
    [InlineData(StructuredPageKind.Home, "WebPage", "WebSite")]
    [InlineData(StructuredPageKind.CollectionPage, "CollectionPage", null)]
    [InlineData(StructuredPageKind.WebPage, "WebPage", null)]
    [InlineData(StructuredPageKind.AboutPage, "AboutPage", null)]
    [InlineData(StructuredPageKind.ContactPage, "ContactPage", null)]
    public void Uses_only_supported_entities(StructuredPageKind kind, string mainType, string? extraType)
    {
        var urls = new SeoMetadataGenerator(Origin);
        var metadata = urls.Create("Title", "Description", "/en/", []);
        using var document = JsonDocument.Parse(SeoStructuredData.Serialize(metadata, urls, kind, "en"));
        var graph = document.RootElement.GetProperty("@graph").EnumerateArray().ToArray();
        Assert.Equal(extraType is null ? new[] { mainType } : new[] { mainType, extraType },
            graph.Select(item => item.GetProperty("@type").GetString()));
        Assert.Equal(graph.Length, graph.Select(item => item.GetProperty("@id").GetString()).Distinct().Count());
    }

    [Fact]
    public async Task Private_mode_and_missing_metadata_do_not_embed_public_entities()
    {
        var urls = new SeoMetadataGenerator(Origin);
        var metadata = urls.Create("Title", "Description", "/en/about", []);
        Assert.Empty(ReadJsonLdScripts(await RenderHead(metadata, urls, StructuredPageKind.AboutPage, [], null, "private")));
        Assert.Empty(ReadJsonLdScripts(await RenderHead(null, urls, StructuredPageKind.AboutPage, [], null, "public")));
    }

    private static IEnumerable<string> ReadJsonLdScripts(string head) =>
        Regex.Matches(head,
                """<script\b(?=[^>]*\stype="(?<type>[^"]+)")[^>]*>(?<json>.*?)</script>""",
                RegexOptions.Singleline)
            .Cast<Match>()
            .Where(script => WebUtility.HtmlDecode(script.Groups["type"].Value) == "application/ld+json")
            .Select(script => script.Groups["json"].Value);

    private static async Task<string> RenderHead(SeoMetadata? metadata, SeoMetadataGenerator urls,
        StructuredPageKind kind, SeoBreadcrumb[] parents, DateTimeOffset? date, string mode)
    {
        var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["PRIVATE_ALPHA_ACCESS_MODE"] = mode,
            ["PRIVATE_ALPHA_USERNAME"] = "test-user",
            ["PRIVATE_ALPHA_PASSWORD"] = "Strong-Test-Password-123!"
        }).Build();
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton(urls);
        services.AddSingleton(PrivateAlphaAccessOptions.FromConfiguration(config, false));
        services.AddSingleton<IJSRuntime, StubJsRuntime>();
        using var provider = services.BuildServiceProvider();
        await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());
        return await renderer.Dispatcher.InvokeAsync(async () =>
        {
            await renderer.RenderComponentAsync<SharedSeoMetadata>(ParameterView.FromDictionary(
                new Dictionary<string, object?>
                {
                    ["Metadata"] = metadata, ["Kind"] = kind, ["Parents"] = parents, ["DateModified"] = date
                }));
            return (await renderer.RenderComponentAsync<HeadOutlet>(ParameterView.Empty)).ToHtmlString();
        });
    }

    private sealed class StubJsRuntime : IJSRuntime
    {
        public ValueTask<T> InvokeAsync<T>(string identifier, object?[]? args) => ValueTask.FromResult(default(T)!);
        public ValueTask<T> InvokeAsync<T>(string identifier, CancellationToken token, object?[]? args) =>
            ValueTask.FromResult(default(T)!);
    }
}
