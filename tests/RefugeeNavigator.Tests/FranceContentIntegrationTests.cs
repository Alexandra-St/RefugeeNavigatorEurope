using System.Globalization;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Comparison;
using RefugeeNavigator.Web.Components;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Content;
using RefugeeNavigator.Web.Guides;
using RefugeeNavigator.Web.Localization;
using RefugeeNavigator.Web.Search;

namespace RefugeeNavigator.Tests;

public sealed class FranceContentIntegrationTests
{
    private static readonly Guid CountryId = Guid.Parse("10000000-0000-0000-0000-000000000015");
    private static readonly IReadOnlyDictionary<string, (Guid Id, string Category)> Articles = new Dictionary<string, (Guid, string)>
    {
        ["FR-TP-01"] = (Guid.Parse("d16ad009-3391-5e3b-80b2-5b0d348503d3"), "temporary-protection"),
        ["FR-EMP-01"] = (Guid.Parse("82179b12-c3bd-5bf1-be0a-374502fc0386"), "employment"),
        ["FR-EMP-02"] = (Guid.Parse("084c5497-7b96-59a2-9d9a-4b0be9b5a2ff"), "employment"),
        ["FR-HEALTH-01"] = (Guid.Parse("ffc21bf8-901c-5ec3-9f54-fce1dc797974"), "healthcare"),
        ["FR-EDU-01"] = (Guid.Parse("0a1c276b-9a49-5bf5-8be8-104974dd0e5e"), "education"),
        ["FR-SOC-01"] = (Guid.Parse("8d7dd894-f0c5-53f4-831c-4c936f27fdff"), "social-assistance"),
        ["FR-TP-02"] = (Guid.Parse("78be24cd-82f5-5f29-ab54-0013aa15ab93"), "temporary-protection"),
        ["FR-HOUSING-01"] = (Guid.Parse("a308b75d-4b39-56d0-9ce9-effb4418400c"), "housing")
    };
    private static readonly IReadOnlyDictionary<string, int> ExpectedSourceCounts = new Dictionary<string, int>
    {
        ["FR-TP-01"] = 9,
        ["FR-EMP-01"] = 14,
        ["FR-EMP-02"] = 15,
        ["FR-HEALTH-01"] = 14,
        ["FR-EDU-01"] = 18,
        ["FR-SOC-01"] = 21,
        ["FR-TP-02"] = 12,
        ["FR-HOUSING-01"] = 14
    };
    private static readonly ResourceManager Resources = new(
        "RefugeeNavigator.Web.Resources.Localization.SharedResource",
        typeof(SharedResource).Assembly);
    private static readonly Lazy<Task<CatalogData>> Catalog = new(
        () => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());
    private static readonly Lazy<Task<ComparisonFactCatalog>> Facts = new(async () =>
        await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json"))
            .LoadAsync(await Catalog.Value));

    [Fact]
    public async Task FranceHasEightProductionPublishedArticlesAndExpectedCatalogCounts()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "france");
        Assert.Equal(CountryId, country.Id);
        Assert.Equal("France", country.Name);
        Assert.Equal(19, catalog.Countries.Count);
        Assert.Equal(152, catalog.InformationEntries.Count);
        Assert.Equal(456, catalog.InformationEntryTranslations.Count);
        Assert.Equal(456, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Outdated));

        var entries = catalog.InformationEntries.Where(item => item.CountryId == CountryId).ToArray();
        Assert.Equal(8, entries.Length);
        Assert.Equal(24, catalog.InformationEntryTranslations.Count(item => entries.Any(entry => entry.Id == item.InformationEntryId)));
        Assert.Equal(Articles.Values.Select(item => item.Id).Order().ToArray(), entries.Select(item => item.Id).Order().ToArray());
        var categoryCounts = entries
            .GroupBy(item => catalog.Categories.Single(category => category.Id == item.CategoryId).Slug)
            .ToDictionary(group => group.Key, group => group.Count());
        Assert.Equal(2, categoryCounts["employment"]);
        Assert.Equal(1, categoryCounts["healthcare"]);
        Assert.Equal(1, categoryCounts["education"]);
        Assert.Equal(1, categoryCounts["social-assistance"]);
        Assert.Equal(2, categoryCounts["temporary-protection"]);
        Assert.Equal(1, categoryCounts["housing"]);

        foreach (var (articleCode, expected) in Articles)
        {
            var entry = Assert.Single(entries, item => item.Id == expected.Id);
            Assert.Equal(expected.Category, catalog.Categories.Single(item => item.Id == entry.CategoryId).Slug);
            var translations = catalog.InformationEntryTranslations.Where(item => item.InformationEntryId == expected.Id).ToArray();
            Assert.Equal(new[] { "en", "ru", "uk" }, translations.Select(item => item.LanguageCode).Order(StringComparer.Ordinal).ToArray());
            var baselineDate = new DateTimeOffset(2026, 11, 19, 0, 0, 0, TimeSpan.Zero);
            Assert.All(translations, item =>
            {
                Assert.Equal(VerificationStatus.Verified, item.VerificationStatus);
                var expectedDate = item.InformationEntryId == Guid.Parse("084c5497-7b96-59a2-9d9a-4b0be9b5a2ff") || item.InformationEntryId == Guid.Parse("78be24cd-82f5-5f29-ab54-0013aa15ab93") || (item.InformationEntryId == Guid.Parse("8d7dd894-f0c5-53f4-831c-4c936f27fdff") && item.LanguageCode == "ru")
                    ? new DateTimeOffset(2026, 11, 22, 0, 0, 0, TimeSpan.Zero)
                    : baselineDate;
                Assert.Equal(expectedDate, item.LastUpdatedAt);
            });
        }

        Assert.Equal(19, CountryComparisonTopicMap.SupportedCountrySlugs.Count);
        Assert.Contains("france", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Contains("ireland", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.All(CountryComparisonTopicMap.All, topic => Assert.Equal(19, topic.EntryIds.Count));
        var allFacts = (await Facts.Value).Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).ToArray();
        Assert.Equal(456, allFacts.Length);
        Assert.Equal(24, allFacts.Count(fact => fact.CountrySlug == "ireland"));
        Assert.Equal(24, allFacts.Count(fact => fact.CountrySlug == "france"));
    }

    [Fact]
    public async Task FranceTranslationsAndOneHundredSeventeenArticleOwnedSourcesMatchCertificationExactly()
    {
        var catalog = await Catalog.Value;
        Assert.Equal(1562, catalog.OfficialSources.Count);
        Assert.Equal(catalog.OfficialSources.Count, catalog.OfficialSources.Select(item => item.Id).Distinct().Count());
        var repositoryRoot = FindRepositoryRoot();
        var refreshRoot = Path.Combine(repositoryRoot, "docs", "content-refresh", "2026-11", "france");
        var articleIds = Articles.Values.Select(item => item.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(item => articleIds.Contains(item.InformationEntryId)).ToArray();
        Assert.Equal(117, sources.Length);
        Assert.Equal(sources.Length, sources.Select(item => item.Id).Distinct().Count());
        Assert.All(sources, source =>
        {
            Assert.Contains(source.InformationEntryId, articleIds);
            Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme);
        });
        Assert.All(catalog.OfficialSources.GroupBy(item => (item.InformationEntryId, item.Title, item.Url.AbsoluteUri)), group => Assert.Single(group));

        var sourceById = catalog.OfficialSources.ToDictionary(item => item.Id);
        foreach (var (articleCode, expected) in Articles)
        {
            var draft = await File.ReadAllTextAsync(Path.Combine(refreshRoot, articleCode + "_DRAFT_01.md"));
            Assert.Contains($"ArticleId: `{expected.Id}`", draft);
            Assert.Equal(ExpectedSourceCounts[articleCode], sources.Count(item => item.InformationEntryId == expected.Id));
            foreach (var (language, section) in new[] { ("en", "EN"), ("uk", "UK"), ("ru", "RU") })
            {
                var match = Regex.Match(draft,
                    $@"(?ms)^## {section}\s+(?<content>.*?)(?=^## (?:EN|UK|RU|EN claim handles)$)");
                Assert.True(match.Success, $"Missing {section} in {articleCode}.");
                var translation = Assert.Single(catalog.InformationEntryTranslations,
                    item => item.InformationEntryId == expected.Id && item.LanguageCode == language);
                Assert.Equal(match.Groups["content"].Value.Trim(), translation.Content);
                var tokens = Regex.Matches(translation.Content, @"\[\[source:([^|\]]+)\|([^\]]+)\]\]");
                foreach (Match token in tokens)
                {
                    var sourceId = Guid.Parse(token.Groups[1].Value);
                    Assert.True(sourceById.TryGetValue(sourceId, out var source), $"Unknown source {sourceId}.");
                    Assert.Equal(expected.Id, source!.InformationEntryId);
                }
            }
        }

        Assert.Contains(sources, item => item.Id == Guid.Parse("fecb425a-243b-5233-871d-2ce26b742a66")
            && item.InformationEntryId == Articles["FR-TP-02"].Id);
        Assert.DoesNotContain(sources, item => item.Id == Guid.Parse("d0f6fb72-2f27-487d-8cc2-0cfdd32506f9"));
        Assert.DoesNotContain(sources, item => item.Id == Guid.Parse("2e094655-76bf-4800-91a5-7b6e62823d20"));
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task FranceIsVisibleInProductionDevelopmentAndCountryComparison(string language)
    {
        var catalog = await Catalog.Value;
        var productionHome = await RenderAsync<Home>("Production", language);
        Assert.Equal(19, Regex.Matches(productionHome.Html, "class=\"country-card\"").Count);
        Assert.Contains("href=\"/countries/france\"", productionHome.Html);
        var productionSearch = await RenderAsync<Search>("Production", language, "/search");
        Assert.Contains("france", SelectValues(productionSearch.Html, "country"));
        var productionOverview = await RenderAsync<CountryOverview>("Production", language, "/countries/france",
            new() { [nameof(CountryOverview.CountrySlug)] = "france" });
        Assert.False(productionOverview.NotFound);
        var productionCategoryCounts = new Dictionary<string, int>
        {
            ["temporary-protection"] = 2,
            ["employment"] = 2,
            ["healthcare"] = 1,
            ["housing"] = 1,
            ["education"] = 1,
            ["social-assistance"] = 1
        };
        foreach (var (categorySlug, expectedCount) in productionCategoryCounts)
        {
            var category = await RenderAsync<Category>("Production", language,
                $"/countries/france/categories/{categorySlug}", new()
                {
                    [nameof(Category.CountrySlug)] = "france",
                    [nameof(Category.CategorySlug)] = categorySlug
                });
            Assert.False(category.NotFound);
            Assert.Equal(expectedCount, Regex.Matches(category.Html, "class=\"information-entry\"").Count);
        }
        var productionResults = await RenderAsync<Search>("Production", language, "/search?country=france");
        Assert.Equal(8, Regex.Matches(productionResults.Html, "class=\"result-card\"").Count);
        var productionCompare = await RenderAsync<Compare>("Production", language, "/compare");
        Assert.Contains("france", SelectValues(productionCompare.Html, "country1"));
        Assert.Contains("belgium", SelectValues(productionCompare.Html, "country1"));
        Assert.Contains("france", SelectValues(productionCompare.Html, "country2"));
        var productionFranceComparison = await RenderAsync<Compare>("Production", language,
            "/compare?country1=france&country2=norway&category=all");
        await AssertRenderedFranceComparison(productionFranceComparison.Html, language);

        var developmentHome = await RenderAsync<Home>("Development", language);
        Assert.Equal(19, Regex.Matches(developmentHome.Html, "class=\"country-card\"").Count);
        Assert.Contains("href=\"/countries/france\"", developmentHome.Html);
        var developmentOverview = await RenderAsync<CountryOverview>("Development", language, "/countries/france",
            new() { [nameof(CountryOverview.CountrySlug)] = "france" });
        Assert.False(developmentOverview.NotFound);
        var developmentSearch = await RenderAsync<Search>("Development", language, "/search");
        Assert.Contains("france", SelectValues(developmentSearch.Html, "country"));
        var franceSearch = await RenderAsync<Search>("Development", language, "/search?country=france");
        Assert.Equal(8, Regex.Matches(franceSearch.Html, "class=\"result-card\"").Count);
        Assert.Equal(8, ArticleSearch.Search(catalog, null, "france", null, null, language).Count);

        var categoryCounts = new Dictionary<string, int>
        {
            ["employment"] = 2,
            ["temporary-protection"] = 2,
            ["healthcare"] = 1,
            ["education"] = 1,
            ["social-assistance"] = 1,
            ["housing"] = 1
        };
        foreach (var (categorySlug, expectedCount) in categoryCounts)
        {
            var category = await RenderAsync<Category>("Development", language,
                $"/countries/france/categories/{categorySlug}", new()
                {
                    [nameof(Category.CountrySlug)] = "france",
                    [nameof(Category.CategorySlug)] = categorySlug
                });
            Assert.False(category.NotFound);
            Assert.Equal(expectedCount, Regex.Matches(category.Html, "class=\"information-entry\"").Count);
        }

        var developmentCompare = await RenderAsync<Compare>("Development", language, "/compare");
        Assert.Contains("france", SelectValues(developmentCompare.Html, "country1"));
        Assert.Contains("france", SelectValues(developmentCompare.Html, "country2"));
        var developmentFranceComparison = await RenderAsync<Compare>("Development", language,
            "/compare?country1=france&country2=norway&category=all");
        await AssertRenderedFranceComparison(developmentFranceComparison.Html, language);
        Assert.Contains("france", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Contains("ireland", CountryComparisonTopicMap.SupportedCountrySlugs);
    }

    [Fact]
    public async Task FranceCompareFactsMatchReviewedDraftAndOwnedProductionSourcesExactly()
    {
        var catalog = await Catalog.Value;
        var facts = await Facts.Value;
        CountryComparisonTopicMap.Validate(catalog);
        Assert.Equal(8, facts.Topics.Count);
        Assert.Equal(24, facts.Topics.Sum(topic => topic.Aspects.Count));
        var repositoryRoot = FindRepositoryRoot();
        var draft = await File.ReadAllTextAsync(Path.Combine(repositoryRoot, "docs", "content-refresh", "2026-11", "france", "COMPARE_DRAFT_01.md"));
        var factBlocks = Regex.Split(draft, @"(?m)(?=^## [a-z0-9-]+\s*$)")
            .Where(block => Regex.IsMatch(block, @"(?m)^## [a-z0-9-]+\s*$"))
            .ToArray();
        Assert.Equal(24, factBlocks.Length);

        var franceFacts = facts.Topics.SelectMany(topic => topic.Aspects
                .SelectMany(aspect => aspect.Facts.Where(fact => fact.CountrySlug == "france")
                    .Select(fact => (topic, aspect, fact))))
            .ToArray();
        Assert.Equal(24, franceFacts.Length);
        Assert.Equal(24, franceFacts.Select(item => item.aspect.Id).Distinct(StringComparer.Ordinal).Count());
        Assert.Equal(72, franceFacts.Length * 3);

        foreach (var block in factBlocks)
        {
            var aspectId = Regex.Match(block, @"(?m)^## (?<id>[a-z0-9-]+)\s*$").Groups["id"].Value;
            var actual = Assert.Single(franceFacts, item => item.aspect.Id == aspectId);
            var mappedEntryId = CountryComparisonTopicMap.EntryIdFor(
                Assert.Single(CountryComparisonTopicMap.All, topic => topic.ResourceKey == actual.topic.TopicResourceKey), "france");
            Assert.Equal(mappedEntryId, actual.fact.InformationEntryId);
            Assert.Equal(ComparisonFactStatus.Approved, actual.fact.Status);
            Assert.True(actual.fact.IsAvailable);
            Assert.Equal(new[] { "en", "ru", "uk" }, actual.fact.Values.Keys.Order(StringComparer.Ordinal).ToArray());

            foreach (var language in new[] { "en", "uk", "ru" })
            {
                var value = Regex.Match(block, $@"(?m)^- {language.ToUpperInvariant()}: (?<value>.+)$");
                Assert.True(value.Success, $"Missing {language} draft value for {aspectId}.");
                Assert.Equal(value.Groups["value"].Value, actual.fact.Values[language]);
            }

            var expectedSourceIds = Regex.Matches(
                    Regex.Match(block, @"(?m)^- Candidate SourceIds: (?<ids>.+)$").Groups["ids"].Value,
                    @"(?<id>[0-9a-f-]{36})")
                .Select(match => Guid.Parse(match.Groups["id"].Value))
                .ToArray();
            Assert.Equal(expectedSourceIds, actual.fact.OfficialSourceIds);
            Assert.NotEmpty(actual.fact.OfficialSourceIds);
            Assert.All(actual.fact.OfficialSourceIds, sourceId =>
            {
                var source = Assert.Single(catalog.OfficialSources, item => item.Id == sourceId);
                Assert.Equal(actual.fact.InformationEntryId, source.InformationEntryId);
                Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme);
            });
        }
    }

    [Fact]
    public async Task OutdatedFranceTranslationIsExcludedFromProductionVisibleSearches()
    {
        const string language = "en";
        var catalog = await Catalog.Value;
        var outdatedTranslation = Assert.Single(catalog.InformationEntryTranslations,
            item => item.InformationEntryId == Articles["FR-EMP-01"].Id && item.LanguageCode == language);
        var previewCatalog = WithFranceTranslationStatus(catalog, outdatedTranslation.InformationEntryId,
            language, VerificationStatus.Outdated);

        var productionSearch = await RenderAsync<Search>("Production", language, "/search?country=france",
            catalogOverride: previewCatalog);
        Assert.Equal(7, Regex.Matches(productionSearch.Html, "class=\"result-card\"").Count);
        Assert.DoesNotContain(outdatedTranslation.Title, productionSearch.Html);
        Assert.Contains(catalog.InformationEntryTranslations.Single(item =>
            item.InformationEntryId == Articles["FR-EMP-02"].Id && item.LanguageCode == language).Title, productionSearch.Html);

        var developmentSearch = await RenderAsync<Search>("Development", language, "/search?country=france",
            catalogOverride: previewCatalog);

        Assert.Equal(7, Regex.Matches(developmentSearch.Html, "class=\"result-card\"").Count);
        Assert.DoesNotContain(outdatedTranslation.Title, developmentSearch.Html);
        Assert.Contains(catalog.InformationEntryTranslations.Single(item =>
            item.InformationEntryId == Articles["FR-EMP-02"].Id && item.LanguageCode == language).Title, developmentSearch.Html);
    }

    [Fact]
    public void FranceCountryLocalizationIsPresentInAllLanguages()
    {
        Assert.Equal("France", Resources.GetString("Country.France", CultureInfo.GetCultureInfo("en")));
        Assert.Equal("France", Resources.GetString("Country.France.In", CultureInfo.GetCultureInfo("en")));
        Assert.Equal("Франція", Resources.GetString("Country.France", CultureInfo.GetCultureInfo("uk")));
        Assert.Equal("у Франції", Resources.GetString("Country.France.In", CultureInfo.GetCultureInfo("uk")));
        Assert.Equal("Франция", Resources.GetString("Country.France", CultureInfo.GetCultureInfo("ru")));
        Assert.Equal("во Франции", Resources.GetString("Country.France.In", CultureInfo.GetCultureInfo("ru")));
    }

    private static async Task AssertRenderedFranceComparison(string html, string language)
    {
        var visibleHtml = WebUtility.HtmlDecode(html);
        Assert.Contains("value=\"france\" selected", html);
        Assert.Contains("value=\"norway\" selected", html);
        Assert.DoesNotContain("class=\"selection-message\"", html);
        Assert.Equal(8, Regex.Matches(html, "class=\"structured-topic\"").Count);
        Assert.Equal(24, Regex.Matches(html, "class=\"structured-aspect\"").Count);
        Assert.Equal(48, Regex.Matches(html, "class=\"structured-country\"").Count);

        var catalog = await Catalog.Value;
        var facts = await Facts.Value;
        var franceFacts = facts.Topics.SelectMany(topic => topic.Aspects)
            .SelectMany(aspect => aspect.Facts.Where(fact => fact.CountrySlug == "france"))
            .ToArray();
        Assert.Equal(24, franceFacts.Length);
        foreach (var fact in franceFacts)
        {
            Assert.Equal(ComparisonFactStatus.Approved, fact.Status);
            Assert.True(fact.IsAvailable);
            foreach (var sourceId in fact.OfficialSourceIds)
            {
                var source = Assert.Single(catalog.OfficialSources, item => item.Id == sourceId);
                Assert.Contains(source.Url.AbsoluteUri, visibleHtml);
            }
        }
    }

    private static string[] SelectValues(string html, string name)
    {
        var select = Regex.Match(html, $"<select\\b[^>]*name=\"{name}\"[^>]*>(.*?)</select>", RegexOptions.Singleline);
        Assert.True(select.Success);
        return Regex.Matches(select.Groups[1].Value, "<option\\b[^>]*value=\"([^\"]+)\"")
            .Select(match => match.Groups[1].Value).ToArray();
    }

    private static CatalogData WithFranceTranslationStatus(CatalogData catalog, Guid informationEntryId,
        string languageCode, VerificationStatus status)
    {
        var translations = catalog.InformationEntryTranslations.Select(item =>
            item.InformationEntryId == informationEntryId && item.LanguageCode == languageCode
                ? new InformationEntryTranslation(item.InformationEntryId, item.LanguageCode, item.Title,
                    item.Summary, item.Content, status, item.LastUpdatedAt)
                : item);
        return (CatalogData)Activator.CreateInstance(typeof(CatalogData),
            BindingFlags.Instance | BindingFlags.NonPublic, binder: null,
            args: [catalog.Countries, catalog.Categories, catalog.InformationEntries, translations, catalog.OfficialSources],
            culture: CultureInfo.InvariantCulture)!;
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !Directory.Exists(Path.Combine(directory.FullName, ".git")))
            directory = directory.Parent;
        return directory?.FullName ?? throw new DirectoryNotFoundException("Could not locate repository root.");
    }

    private static async Task<(string Html, bool NotFound)> RenderAsync<TComponent>(string environment,
        string language, string path = "/", Dictionary<string, object?>? parameters = null,
        CatalogData? catalogOverride = null) where TComponent : IComponent
    {
        var oldCulture = CultureInfo.CurrentCulture;
        var oldUiCulture = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(language);
        try
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton(catalogOverride ?? await Catalog.Value);
            services.AddSingleton(await Facts.Value);
            services.AddSingleton(new StepByStepGuideCatalog([]));
            services.AddSingleton<IWebHostEnvironment>(new TestEnvironment(environment));
            var navigation = new TestNavigationManager(path);
            var notFound = false;
            navigation.OnNotFound += (_, _) => notFound = true;
            services.AddSingleton<NavigationManager>(navigation);
            using var provider = services.BuildServiceProvider();
            await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());
            var html = await renderer.Dispatcher.InvokeAsync(async () =>
            {
                var component = await renderer.RenderComponentAsync<TComponent>(parameters is null
                    ? ParameterView.Empty : ParameterView.FromDictionary(parameters));
                return component.ToHtmlString();
            });
            return (WebUtility.HtmlDecode(html), notFound);
        }
        finally
        {
            CultureInfo.CurrentCulture = oldCulture;
            CultureInfo.CurrentUICulture = oldUiCulture;
        }
    }

    private sealed class TestNavigationManager : NavigationManager
    {
        public TestNavigationManager(string path) => Initialize("http://localhost/", $"http://localhost{path}");
        protected override void NavigateToCore(string uri, bool forceLoad) { }
    }

    private sealed class TestEnvironment(string environmentName) : IWebHostEnvironment
    {
        public string EnvironmentName { get; set; } = environmentName;
        public string ApplicationName { get; set; } = "RefugeeNavigator.Tests";
        public string ContentRootPath { get; set; } = AppContext.BaseDirectory;
        public string WebRootPath { get; set; } = AppContext.BaseDirectory;
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
    }
}
