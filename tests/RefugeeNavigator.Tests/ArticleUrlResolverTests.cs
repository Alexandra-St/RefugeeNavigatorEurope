using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Seo;

namespace RefugeeNavigator.Tests;

public sealed class ArticleUrlResolverTests
{
    private static readonly Lazy<Task<CatalogData>> Catalog = new(() => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());

    [Fact]
    public async Task PublishedArticleUrlResolvesBackToSameEntryAndTranslation()
    {
        var catalog = await Catalog.Value;
        var resolver = new ArticleUrlResolver(catalog);
        var entry = catalog.InformationEntries.First(e => catalog.InformationEntryTranslations.Any(t =>
            t.InformationEntryId == e.Id && t.LanguageCode == "en" && t.VerificationStatus == VerificationStatus.Verified));
        var url = resolver.Generate(entry, "en");
        Assert.NotNull(url);
        var parts = url!.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var result = resolver.Resolve(parts[0], parts[2], parts[3], parts[4]);
        Assert.NotNull(result);
        Assert.Equal(entry.Id, result.Entry.Id);
        Assert.Equal("en", result.Translation.LanguageCode);
    }

    [Fact]
    public async Task EveryProductionLocalizedUrlIsUniqueAndMatchesExpectedCoverage()
    {
        var audit = ArticleUrlCatalogAuditRunner.Run(await Catalog.Value);
        Assert.Equal(152, audit.ProductionArticles);
        Assert.Equal(456, audit.LocalizedArticleUrls);
        Assert.Empty(audit.Collisions);
    }

    [Fact]
    public async Task ArticleIdentityAndKeyRemainStableAcrossLanguages()
    {
        var catalog = await Catalog.Value;
        var resolver = new ArticleUrlResolver(catalog);
        var entry = catalog.InformationEntries.First(e => catalog.InformationEntryTranslations.Any(t => t.InformationEntryId == e.Id && t.LanguageCode == "uk" && t.VerificationStatus == VerificationStatus.Verified));
        var paths = new[] { "uk", "en", "ru" }.Select(language => resolver.Generate(entry, language)).ToArray();
        Assert.All(paths, path => Assert.NotNull(path));
        Assert.All(paths, path => Assert.EndsWith(entry.Id.ToString("D"), path, StringComparison.Ordinal));
        Assert.Equal(new[] { "uk", "en", "ru" }, paths.Select(path => path!.Split('/')[1]));
    }

    [Fact]
    public async Task UnverifiedOrUnavailableTranslationIsExcluded()
    {
        var catalog = await Catalog.Value;
        var resolver = new ArticleUrlResolver(catalog);
        var unavailable = catalog.InformationEntryTranslations.FirstOrDefault(t => t.VerificationStatus != VerificationStatus.Verified);
        if (unavailable is not null)
        {
            var entry = catalog.InformationEntries.Single(e => e.Id == unavailable.InformationEntryId);
            Assert.Null(resolver.Generate(entry, unavailable.LanguageCode));
        }
        var anyEntry = catalog.InformationEntries[0];
        Assert.Null(resolver.Generate(anyEntry, "de"));
    }

    [Fact]
    public async Task MissingTranslationProducesNoUrlAndAuditReportsIt()
    {
        var catalog = await Catalog.Value;
        var resolver = new ArticleUrlResolver(catalog);
        Assert.Null(resolver.Generate(catalog.InformationEntries[0], "fr"));
        Assert.Empty(ArticleUrlCatalogAuditRunner.Run(catalog).MissingTranslations);
    }

    [Fact]
    public async Task InvalidKeysLanguagesAndMismatchedTaxonomyAreRejected()
    {
        var catalog = await Catalog.Value;
        var resolver = new ArticleUrlResolver(catalog);
        var entry = catalog.InformationEntries.First(e => catalog.InformationEntryTranslations.Any(t => t.InformationEntryId == e.Id && t.LanguageCode == "en" && t.VerificationStatus == VerificationStatus.Verified));
        var country = catalog.Countries.Single(c => c.Id == entry.CountryId);
        var category = catalog.Categories.Single(c => c.Id == entry.CategoryId);
        Assert.Null(resolver.Resolve("EN", country.Slug, category.Slug, entry.Id.ToString("D")));
        Assert.Null(resolver.Resolve("en", country.Slug, category.Slug, "not-a-guid"));
        Assert.Null(resolver.Resolve("en", country.Slug, category.Slug, entry.Id.ToString("N")));
        Assert.Null(resolver.Resolve("en", country.Slug, category.Slug, Guid.NewGuid().ToString("D")));
        var otherCountry = catalog.Countries.First(c => c.Id != country.Id);
        Assert.Null(resolver.Resolve("en", otherCountry.Slug, category.Slug, entry.Id.ToString("D")));
    }

    [Fact]
    public async Task ProductionVisibilityExcludesUnverifiedOnlyArticles()
    {
        var catalog = await Catalog.Value;
        var resolver = new ArticleUrlResolver(catalog);
        var hidden = catalog.InformationEntries.FirstOrDefault(e => !catalog.InformationEntryTranslations.Any(t => t.InformationEntryId == e.Id && t.VerificationStatus == VerificationStatus.Verified));
        if (hidden is not null)
            Assert.All(new[] { "uk", "en", "ru" }, language => Assert.Null(resolver.Generate(hidden, language)));
    }
}
