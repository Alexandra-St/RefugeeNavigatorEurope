using System.Globalization;
using System.Reflection;
using System.Resources;
using Microsoft.AspNetCore.Components;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Comparison;
using RefugeeNavigator.Web.Content;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Tests;

public sealed class AboutStatisticsTests
{
    private static readonly Lazy<Task<CatalogData>> Catalog = new(() => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());
    private static readonly Lazy<Task<ComparisonFactCatalog>> Facts = new(async () =>
        await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(await Catalog.Value));
    private static readonly string[] Languages = ["uk", "en", "ru"];

    [Fact]
    public async Task CountriesUseProductionVisibilityEvenWhenRunningInDevelopment()
    {
        var catalog = await Catalog.Value;
        var sweden = catalog.Countries.Single(country => country.Slug == "sweden");
        var swedenEntryIds = catalog.InformationEntries.Where(entry => entry.CountryId == sweden.Id).Select(entry => entry.Id).ToHashSet();
        var hiddenCatalog = WithStatuses(catalog, swedenEntryIds, VerificationStatus.NeedsReview);
        Assert.Equal(19, CountryVisibility.GetVisibleCountries(hiddenCatalog, isDevelopment: true).Count);
        Assert.Equal(18, CountryVisibility.GetVisibleCountries(hiddenCatalog, isDevelopment: false).Count);
        var stats = AboutStatisticsProvider.Create(hiddenCatalog, await Facts.Value, Languages);
        Assert.Equal(18, stats.Countries);
        var visibleCountryIds = CountryVisibility.GetVisibleCountries(hiddenCatalog, false).Select(country => country.Id).ToHashSet();
        var expectedPublished = hiddenCatalog.InformationEntries.Where(entry => visibleCountryIds.Contains(entry.CountryId) &&
            hiddenCatalog.InformationEntryTranslations.Any(translation => translation.InformationEntryId == entry.Id &&
                translation.VerificationStatus == VerificationStatus.Verified)).Select(entry => entry.Id).Distinct().Count();
        Assert.Equal(expectedPublished, stats.PublishedArticles);
    }

    [Fact]
    public async Task PublishedArticleCountExcludesDraftAndHiddenTranslations()
    {
        var catalog = await Catalog.Value;
        var visibleCountryIds = CountryVisibility.GetVisibleCountries(catalog, false).Select(country => country.Id).ToHashSet();
        var expected = catalog.InformationEntries.Where(entry => visibleCountryIds.Contains(entry.CountryId) &&
            catalog.InformationEntryTranslations.Any(translation => translation.InformationEntryId == entry.Id &&
                translation.VerificationStatus == VerificationStatus.Verified)).Select(entry => entry.Id).Distinct().Count();
        var stats = AboutStatisticsProvider.Create(catalog, await Facts.Value, Languages);
        Assert.Equal(expected, stats.PublishedArticles);
    }

    [Fact]
    public async Task CompareDraftAndUnavailableFactsAreNotPubliclyVisible()
    {
        var catalog = await Catalog.Value;
        var draftFacts = (await Facts.Value).Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts)
            .Where(fact => fact.Status == ComparisonFactStatus.Draft || !fact.IsAvailable);
        Assert.NotEmpty(draftFacts);
        Assert.All(draftFacts, fact => Assert.All(Languages, language =>
            Assert.False(ComparisonFactVisibility.CanRender(fact, catalog, language, isDevelopment: false))));
    }

    [Fact]
    public async Task SourceCounterCountsDistinctArticleAndCompareReferences()
    {
        var catalog = await Catalog.Value;
        var facts = await Facts.Value;
        var visibleIds = CountryVisibility.GetVisibleCountries(catalog, false).Select(country => country.Id).ToHashSet();
        var publishedIds = catalog.InformationEntries.Where(entry => visibleIds.Contains(entry.CountryId) &&
            catalog.InformationEntryTranslations.Any(translation => translation.InformationEntryId == entry.Id &&
                translation.VerificationStatus == VerificationStatus.Verified)).Select(entry => entry.Id).ToHashSet();
        var expected = catalog.OfficialSources.Where(source => publishedIds.Contains(source.InformationEntryId)).Select(source => source.Id).ToHashSet();
        foreach (var fact in facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts))
            if (Languages.Any(language => ComparisonFactVisibility.CanRender(fact, catalog, language, false)))
                expected.UnionWith(fact.OfficialSourceIds);

        var stats = AboutStatisticsProvider.Create(catalog, facts, Languages);
        Assert.Equal(expected.Count, stats.OfficialSources);
        Assert.Equal(stats.OfficialSources, expected.Distinct().Count());
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public void AboutLocalizationContainsEveryPageResource(string language)
    {
        var manager = new ResourceManager("RefugeeNavigator.Web.Resources.Localization.SharedResource", typeof(SharedResource).Assembly);
        var culture = CultureInfo.GetCultureInfo(language);
        foreach (var key in new[] { "About.PageTitle", "About.MetaDescription", "About.Heading", "About.MissionTitle", "About.Mission", "About.CoverageTitle", "About.Coverage", "About.StatisticsTitle", "About.Countries", "About.Articles", "About.Sources", "About.Languages", "About.ResearchTitle", "About.Research", "About.MaintenanceTitle", "About.Maintenance", "About.IndependenceTitle", "About.Independence", "About.DisclaimerTitle", "About.Disclaimer", "About.SupportTitle", "About.Support", "About.SupportVoluntary", "About.SupportCta" })
            Assert.False(string.IsNullOrWhiteSpace(manager.GetString(key, culture)), $"Missing {key} for {language}");
    }

    [Fact]
    public void AboutPageUsesExpectedRoute()
    {
        LocalizedRouteAssertions.HasRouteSet(typeof(About), "/about");
    }

    private static CatalogData WithStatuses(CatalogData catalog, HashSet<Guid> entryIds, VerificationStatus status)
    {
        var translations = catalog.InformationEntryTranslations.Select(item => !entryIds.Contains(item.InformationEntryId)
            ? item
            : new InformationEntryTranslation(item.InformationEntryId, item.LanguageCode, item.Title, item.Summary,
                item.Content, status, item.LastUpdatedAt));
        return (CatalogData)Activator.CreateInstance(typeof(CatalogData), BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null, args: [catalog.Countries, catalog.Categories, catalog.InformationEntries, translations, catalog.OfficialSources],
            culture: CultureInfo.InvariantCulture)!;
    }
}
