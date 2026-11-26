using RefugeeNavigator.Web.Seo;
using RefugeeNavigator.Web.Security;

namespace RefugeeNavigator.Tests;

public sealed class SeoMetadataGeneratorTests
{
    [Theory]
    [InlineData("//foreign.example/en")]
    [InlineData("/\\foreign.example/en")]
    public void Site_relative_paths_cannot_override_canonical_origin(string path)
    {
        var generator = new SeoMetadataGenerator("https://canonical.example");
        Assert.Throws<ArgumentException>(() => generator.BuildAbsoluteUrl(path));
    }

    [Theory]
    [InlineData(PrivateAlphaAccessMode.Private, false)]
    [InlineData(PrivateAlphaAccessMode.Development, false)]
    [InlineData(PrivateAlphaAccessMode.Public, true)]
    public void Robots_advertises_only_the_public_sitemap(PrivateAlphaAccessMode mode, bool isPublic)
    {
        var robots = SeoRoutePolicy.Robots(mode, new SeoMetadataGenerator("https://canonical.example"));
        Assert.Equal(isPublic, SeoRoutePolicy.ExposesSitemap(mode));
        if (isPublic)
        {
            Assert.Contains("Sitemap: https://canonical.example/sitemap.xml", robots);
            Assert.DoesNotContain("/search", robots);
            Assert.DoesNotContain("/compare", robots);
        }
        else
        {
            Assert.Equal("User-agent: *\nDisallow: /\n", robots);
        }
    }

    [Fact]
    public void Generates_localized_self_canonical_and_alternates_without_query_parameters()
    {
        var generator = new SeoMetadataGenerator("https://example.test");
        var metadata = generator.Create(
            "About",
            "Localized description",
            "/en/about?utm_source=test#section",
            [("en", "/en/about"), ("uk", "/uk/about"), ("ru", "/ru/about")]);

        Assert.Equal("https://example.test/en/about", metadata.CanonicalUrl);
        Assert.Equal("https://example.test/en/about", metadata.OpenGraphUrl);
        Assert.Equal(new[] { "en", "uk", "ru" }, metadata.Alternates.Select(item => item.Language));
        Assert.Equal("https://example.test/ru/about", metadata.Alternates[2].Url);
    }

    [Fact]
    public void Uses_production_origin_when_not_configured()
    {
        var generator = new SeoMetadataGenerator(null);
        var metadata = generator.Create("Title", "Description", "/en/about", []);
        Assert.StartsWith(SeoMetadataGenerator.DefaultCanonicalOrigin, metadata.CanonicalUrl);
    }

    [Theory]
    [InlineData("http://example.test")]
    [InlineData("https://user@example.test")]
    [InlineData("https://example.test/path")]
    [InlineData("https://example.test?query=1")]
    [InlineData("https://example.test#fragment")]
    public void Rejects_non_origin_configuration(string origin)
        => Assert.Throws<ArgumentException>(() => new SeoMetadataGenerator(origin));

    [Fact]
    public void Rejects_relative_canonical_paths()
    {
        var generator = new SeoMetadataGenerator("https://example.test");
        Assert.Throws<ArgumentException>(() => generator.Create("Title", "Description", "about", []));
    }

    [Fact]
    public void Static_page_alternates_are_absolute_and_reciprocal()
    {
        var generator = new SeoMetadataGenerator("https://example.test");
        foreach (var lang in new[] { "en", "uk", "ru" })
        {
            var meta = generator.Create("Title", "Description", $"/{lang}/about",
                new[] { ("en", "/en/about"), ("uk", "/uk/about"), ("ru", "/ru/about") });
            Assert.Equal(3, meta.Alternates.Count);
            Assert.Contains(meta.Alternates, item => item.Language == lang && item.Url == meta.CanonicalUrl);
        }
    }

    [Fact]
    public void Fragment_article_destinations_are_excluded_from_alternates()
    {
        var generator = new SeoMetadataGenerator("https://example.test");
        var metadata = generator.Create("Title", "Description", "/en/countries/de/categories/tp",
            [("en", "/en/countries/de/categories/tp#entry-1"), ("uk", "/uk/countries/de/categories/tp")]);
        Assert.Equal(new[] { "uk" }, metadata.Alternates.Select(item => item.Language));
    }
}
