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

public sealed class BelgiumContentIntegrationTests
{
    private static readonly Guid CountryId = Guid.Parse("10000000-0000-0000-0000-000000000017");
    private static readonly IReadOnlyDictionary<string, (Guid Id, string Category)> Articles = new Dictionary<string, (Guid, string)>
    {
        ["BE-TP-01"] = (Guid.Parse("aef6ec65-f8ed-5054-bafb-c465e790dd57"), "temporary-protection"),
        ["BE-TP-02"] = (Guid.Parse("54c6f95f-589e-5901-beae-990bed994160"), "temporary-protection"),
        ["BE-EMP-01"] = (Guid.Parse("f2c1526a-a218-5bf0-ab2a-9167dbec0c26"), "employment"),
        ["BE-EMP-02"] = (Guid.Parse("55fa79e5-89a2-518b-90b2-85a2735f171b"), "employment"),
        ["BE-HEALTH-01"] = (Guid.Parse("673316a9-6ee9-50d6-9f66-e19659aeff0e"), "healthcare"),
        ["BE-HOUSING-01"] = (Guid.Parse("3646ae19-215c-5f0e-9075-85ef5bae88f7"), "housing"),
        ["BE-EDU-01"] = (Guid.Parse("ac80dcbd-4289-5c77-8340-f9e4226a47f6"), "education"),
        ["BE-SOC-01"] = (Guid.Parse("20eef3c3-fcc7-5b5b-b672-695f29faad61"), "social-assistance")
    };
    private static readonly IReadOnlyDictionary<string, int> ExpectedSourceCounts = new Dictionary<string, int>
    {
        ["BE-TP-01"] = 10,
        ["BE-TP-02"] = 16,
        ["BE-EMP-01"] = 17,
        ["BE-EMP-02"] = 22,
        ["BE-HEALTH-01"] = 13,
        ["BE-HOUSING-01"] = 23,
        ["BE-EDU-01"] = 25,
        ["BE-SOC-01"] = 20
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
    public async Task BelgiumHasEightDevelopmentOnlyArticlesAndExpectedCatalogCounts()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "belgium");
        Assert.Equal(CountryId, country.Id);
        Assert.Equal("Belgium", country.Name);
        Assert.Equal(19, catalog.Countries.Count);
        Assert.Equal(147, catalog.InformationEntries.Count);
        Assert.Equal(441, catalog.InformationEntryTranslations.Count);
        Assert.Equal(357, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(84, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
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
            Assert.All(translations, item =>
            {
                Assert.Equal(VerificationStatus.NeedsReview, item.VerificationStatus);
                Assert.Equal(new DateTimeOffset(2026, 11, 11, 0, 0, 0, TimeSpan.Zero), item.LastUpdatedAt);
            });
        }

        Assert.Equal(14, CountryComparisonTopicMap.SupportedCountrySlugs.Count);
        Assert.DoesNotContain("ireland", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.DoesNotContain("france", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.All(CountryComparisonTopicMap.All, topic => Assert.Equal(14, topic.EntryIds.Count));
        var allFacts = (await Facts.Value).Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).ToArray();
        Assert.Equal(336, allFacts.Length);
        Assert.DoesNotContain(allFacts, fact => fact.CountrySlug == "ireland");
        Assert.DoesNotContain(allFacts, fact => fact.CountrySlug == "france");
        Assert.DoesNotContain(allFacts, fact => fact.CountrySlug == "belgium");
    }

    [Fact]
    public async Task BelgiumTranslationsAndOneHundredFortySixArticleOwnedSourcesMatchDraftsExactly()
    {
        var catalog = await Catalog.Value;
        Assert.Equal(1400, catalog.OfficialSources.Count);
        Assert.Equal(catalog.OfficialSources.Count, catalog.OfficialSources.Select(item => item.Id).Distinct().Count());
        var repositoryRoot = FindRepositoryRoot();
        var draftsRoot = Path.Combine(repositoryRoot, "docs", "content-drafts", "belgium-v1");
        var articleIds = Articles.Values.Select(item => item.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(item => articleIds.Contains(item.InformationEntryId)).ToArray();
        Assert.Equal(146, sources.Length);
        Assert.Equal(146, sources.Select(item => item.Id).Distinct().Count());
        Assert.All(sources, source =>
        {
            Assert.Contains(source.InformationEntryId, articleIds);
            Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme);
            Assert.Equal(new DateTimeOffset(2026, 11, 11, 0, 0, 0, TimeSpan.Zero), source.LastCheckedAt);
        });
        Assert.All(catalog.OfficialSources
            .GroupBy(item => (item.InformationEntryId, item.Title, item.Url.AbsoluteUri)), group => Assert.Single(group));

        var sourceById = catalog.OfficialSources.ToDictionary(item => item.Id);
        foreach (var (articleCode, expected) in Articles)
        {
            var draft = await File.ReadAllTextAsync(Path.Combine(draftsRoot, articleCode + ".md"));
            Assert.Contains($"Article ID: `{expected.Id}`", draft);
            var sourceMatches = Regex.Matches(draft,
                @"(?m)^- `(?<key>BE-[^`]+)` → `(?<id>[^`]+)` — (?<title>.*?) \(research source .*?; \[official source\]\((?<url>https://.*)\)\)$");
            Assert.Equal(ExpectedSourceCounts[articleCode], sourceMatches.Count);
            var ownedSources = sources.Where(item => item.InformationEntryId == expected.Id).ToDictionary(item => item.Id);
            Assert.Equal(ExpectedSourceCounts[articleCode], ownedSources.Count);
            foreach (Match sourceMatch in sourceMatches)
            {
                var sourceId = Guid.Parse(sourceMatch.Groups["id"].Value);
                Assert.True(ownedSources.TryGetValue(sourceId, out var source));
                Assert.Equal(expected.Id, source!.InformationEntryId);
                var expectedTitle = sourceMatch.Groups["title"].Value;
                Assert.Equal(expectedTitle, source.Title);
                Assert.Equal(sourceMatch.Groups["url"].Value, source.Url.AbsoluteUri);
            }

            foreach (var (language, section) in new[] { ("en", "EN"), ("uk", "UK"), ("ru", "RU") })
            {
                var match = Regex.Match(draft,
                    $@"(?ms)^## {section}\s+Title: (?<title>.*?)\s+Summary: (?<summary>.*?)\s+Content:\s+(?<content>.*?)(?=^### Claim coverage and editorial exclusions$)");
                Assert.True(match.Success, $"Missing {section} in {articleCode}.");
                var translation = Assert.Single(catalog.InformationEntryTranslations,
                    item => item.InformationEntryId == expected.Id && item.LanguageCode == language);
                Assert.Equal(match.Groups["title"].Value.Trim(), translation.Title);
                Assert.Equal(match.Groups["summary"].Value.Trim(), translation.Summary);
                Assert.Equal(match.Groups["content"].Value.Trim(), translation.Content);
                var tokens = Regex.Matches(translation.Content, @"\[\[source:([^|\]]+)\|([^\]]+)\]\]");
                Assert.NotEmpty(tokens);
                foreach (Match token in tokens)
                {
                    var sourceId = Guid.Parse(token.Groups[1].Value);
                    Assert.True(ownedSources.ContainsKey(sourceId), $"{articleCode} {language} cites non-owned source {sourceId}.");
                    Assert.Equal(expected.Id, sourceById[sourceId].InformationEntryId);
                }
            }
        }
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task BelgiumIsVisibleInDevelopmentButHiddenFromProductionAndComparison(string language)
    {
        var catalog = await Catalog.Value;
        var productionHome = await RenderAsync<Home>("Production", language);
        Assert.Equal(15, Regex.Matches(productionHome.Html, "class=\"country-card\"").Count);
        Assert.DoesNotContain("href=\"/countries/belgium\"", productionHome.Html);
        var productionSearch = await RenderAsync<Search>("Production", language, "/search");
        Assert.DoesNotContain("belgium", SelectValues(productionSearch.Html, "country"));
        var productionOverview = await RenderAsync<CountryOverview>("Production", language, "/countries/belgium",
            new() { [nameof(CountryOverview.CountrySlug)] = "belgium" });
        Assert.True(productionOverview.NotFound);
        var productionCategory = await RenderAsync<Category>("Production", language,
            "/countries/belgium/categories/employment", new()
            {
                [nameof(Category.CountrySlug)] = "belgium",
                [nameof(Category.CategorySlug)] = "employment"
            });
        Assert.True(productionCategory.NotFound);
        foreach (var categorySlug in new[] { "temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance" })
        {
            var result = await RenderAsync<Category>("Production", language,
                $"/countries/belgium/categories/{categorySlug}", new()
                {
                    [nameof(Category.CountrySlug)] = "belgium",
                    [nameof(Category.CategorySlug)] = categorySlug
                });
            Assert.True(result.NotFound);
        }
        var productionResults = await RenderAsync<Search>("Production", language, "/search?country=belgium");
        Assert.Empty(Regex.Matches(productionResults.Html, "class=\"result-card\"").Cast<Match>());
        var productionCompare = await RenderAsync<Compare>("Production", language, "/compare");
        foreach (var hiddenSlug in new[] { "belgium", "finland", "france", "ireland"})
        {
            Assert.DoesNotContain(hiddenSlug, SelectValues(productionCompare.Html, "country1"));
            Assert.DoesNotContain(hiddenSlug, SelectValues(productionCompare.Html, "country2"));
        }

        var developmentHome = await RenderAsync<Home>("Development", language);
        Assert.Equal(19, Regex.Matches(developmentHome.Html, "class=\"country-card\"").Count);
        Assert.Contains("href=\"/countries/belgium\"", developmentHome.Html);
        var developmentOverview = await RenderAsync<CountryOverview>("Development", language, "/countries/belgium",
            new() { [nameof(CountryOverview.CountrySlug)] = "belgium" });
        Assert.False(developmentOverview.NotFound);
        var developmentSearch = await RenderAsync<Search>("Development", language, "/search");
        Assert.Contains("belgium", SelectValues(developmentSearch.Html, "country"));
        Assert.Equal(catalog.Countries.Select(country => country.Slug), SelectValues(developmentSearch.Html, "country"));
        var belgiumSearch = await RenderAsync<Search>("Development", language, "/search?country=belgium");
        Assert.Equal(8, Regex.Matches(belgiumSearch.Html, "class=\"result-card\"").Count);
        Assert.Equal(8, ArticleSearch.Search(catalog, null, "belgium", null, null, language).Count);

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
                $"/countries/belgium/categories/{categorySlug}", new()
                {
                    [nameof(Category.CountrySlug)] = "belgium",
                    [nameof(Category.CategorySlug)] = categorySlug
                });
            Assert.False(category.NotFound);
            Assert.Equal(expectedCount, Regex.Matches(category.Html, "class=\"information-entry\"").Count);
            if (categorySlug == "education")
            {
                Assert.DoesNotContain("[BE-EDU-01-S21]", category.Html);
                Assert.DoesNotContain("[BE-EDU-01-S28]", category.Html);
                var visibleText = Regex.Replace(category.Html, "<[^>]+>", " ");
                visibleText = Regex.Replace(visibleText, @"\s+", " ");
                Assert.Contains("Study in Flanders / Flemish Government, “Tuition fees” — general tuition guidance", visibleText);
                Assert.Contains("Study in Flanders / Flemish Government, “Tuition fees” — temporary-protection fee category", visibleText);
            }
        }

        var developmentCompare = await RenderAsync<Compare>("Development", language, "/compare");
        foreach (var hiddenSlug in new[] { "belgium", "finland", "france", "ireland"})
        {
            Assert.DoesNotContain(hiddenSlug, SelectValues(developmentCompare.Html, "country1"));
            Assert.DoesNotContain(hiddenSlug, SelectValues(developmentCompare.Html, "country2"));
        }
        Assert.DoesNotContain("ireland", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.DoesNotContain("france", CountryComparisonTopicMap.SupportedCountrySlugs);
    }

    [Fact]
    public async Task DevelopmentBelgiumSearchExcludesOutdatedTranslation()
    {
        const string language = "en";
        var catalog = await Catalog.Value;
        var outdatedTranslation = Assert.Single(catalog.InformationEntryTranslations,
            item => item.InformationEntryId == Articles["BE-EMP-01"].Id && item.LanguageCode == language);
        var previewCatalog = WithBelgiumTranslationStatus(catalog, outdatedTranslation.InformationEntryId,
            language, VerificationStatus.Outdated);

        var search = await RenderAsync<Search>("Development", language, "/search?country=belgium",
            catalogOverride: previewCatalog);

        Assert.Equal(7, Regex.Matches(search.Html, "class=\"result-card\"").Count);
        Assert.DoesNotContain(outdatedTranslation.Title, search.Html);
        Assert.All(Articles.Values.Select(item => item.Id).Where(id => id != outdatedTranslation.InformationEntryId), id =>
        {
            Assert.Contains(catalog.InformationEntryTranslations.Single(item =>
                item.InformationEntryId == id && item.LanguageCode == language).Title, search.Html);
        });
    }

    [Fact]
    public void BelgiumCountryLocalizationIsPresentInAllLanguages()
    {
        Assert.Equal("Belgium", Resources.GetString("Country.Belgium", CultureInfo.GetCultureInfo("en")));
        Assert.Equal("Belgium", Resources.GetString("Country.Belgium.In", CultureInfo.GetCultureInfo("en")));
        Assert.Equal("Бельгія", Resources.GetString("Country.Belgium", CultureInfo.GetCultureInfo("uk")));
        Assert.Equal("у Бельгії", Resources.GetString("Country.Belgium.In", CultureInfo.GetCultureInfo("uk")));
        Assert.Equal("Бельгия", Resources.GetString("Country.Belgium", CultureInfo.GetCultureInfo("ru")));
        Assert.Equal("в Бельгии", Resources.GetString("Country.Belgium.In", CultureInfo.GetCultureInfo("ru")));
    }

    private static string[] SelectValues(string html, string name)
    {
        var select = Regex.Match(html, $"<select\\b[^>]*name=\"{name}\"[^>]*>(.*?)</select>", RegexOptions.Singleline);
        Assert.True(select.Success);
        return Regex.Matches(select.Groups[1].Value, "<option\\b[^>]*value=\"([^\"]+)\"")
            .Select(match => match.Groups[1].Value).ToArray();
    }

    private static CatalogData WithBelgiumTranslationStatus(CatalogData catalog, Guid informationEntryId,
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
