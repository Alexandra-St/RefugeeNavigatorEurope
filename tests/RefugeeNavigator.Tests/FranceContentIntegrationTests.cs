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
        ["FR-SOC-01"] = (Guid.Parse("8d7dd894-f0c5-53f4-831c-4c936f27fdff"), "social-assistance")
    };
    private static readonly IReadOnlyDictionary<string, int> ExpectedSourceCounts = new Dictionary<string, int>
    {
        ["FR-TP-01"] = 7,
        ["FR-EMP-01"] = 12,
        ["FR-EMP-02"] = 12,
        ["FR-HEALTH-01"] = 12,
        ["FR-EDU-01"] = 17,
        ["FR-SOC-01"] = 20
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
    public async Task FranceHasSixDevelopmentOnlyArticlesAndExpectedCatalogCounts()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "france");
        Assert.Equal(CountryId, country.Id);
        Assert.Equal("France", country.Name);
        Assert.Equal(18, catalog.Countries.Count);
        Assert.Equal(138, catalog.InformationEntries.Count);
        Assert.Equal(414, catalog.InformationEntryTranslations.Count);
        Assert.Equal(309, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(105, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Outdated));

        var entries = catalog.InformationEntries.Where(item => item.CountryId == CountryId).ToArray();
        Assert.Equal(6, entries.Length);
        Assert.Equal(18, catalog.InformationEntryTranslations.Count(item => entries.Any(entry => entry.Id == item.InformationEntryId)));
        Assert.Equal(Articles.Values.Select(item => item.Id).Order().ToArray(), entries.Select(item => item.Id).Order().ToArray());
        var categoryCounts = entries
            .GroupBy(item => catalog.Categories.Single(category => category.Id == item.CategoryId).Slug)
            .ToDictionary(group => group.Key, group => group.Count());
        Assert.Equal(2, categoryCounts["employment"]);
        Assert.Equal(1, categoryCounts["healthcare"]);
        Assert.Equal(1, categoryCounts["education"]);
        Assert.Equal(1, categoryCounts["social-assistance"]);
        Assert.Equal(1, categoryCounts["temporary-protection"]);
        Assert.DoesNotContain("housing", categoryCounts.Keys);

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

        Assert.Equal(13, CountryComparisonTopicMap.SupportedCountrySlugs.Count);
        Assert.DoesNotContain("france", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.DoesNotContain("belgium", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.DoesNotContain("ireland", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.All(CountryComparisonTopicMap.All, topic => Assert.Equal(13, topic.EntryIds.Count));
        var allFacts = (await Facts.Value).Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).ToArray();
        Assert.Equal(312, allFacts.Length);
        Assert.DoesNotContain(allFacts, fact => fact.CountrySlug == "france");
        Assert.DoesNotContain(allFacts, fact => fact.CountrySlug == "belgium");
        Assert.DoesNotContain(allFacts, fact => fact.CountrySlug == "ireland");
    }

    [Fact]
    public async Task FranceTranslationsAndEightyArticleOwnedSourcesMatchDraftsExactly()
    {
        var catalog = await Catalog.Value;
        Assert.Equal(1357, catalog.OfficialSources.Count);
        Assert.Equal(catalog.OfficialSources.Count, catalog.OfficialSources.Select(item => item.Id).Distinct().Count());
        var repositoryRoot = FindRepositoryRoot();
        var draftsRoot = Path.Combine(repositoryRoot, "docs", "content-drafts", "france-v1");
        var articleIds = Articles.Values.Select(item => item.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(item => articleIds.Contains(item.InformationEntryId)).ToArray();
        Assert.Equal(80, sources.Length);
        Assert.Equal(80, sources.Select(item => item.Id).Distinct().Count());
        Assert.All(sources, source =>
        {
            Assert.Contains(source.InformationEntryId, articleIds);
            Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme);
            Assert.Equal(new DateTimeOffset(2026, 11, 11, 0, 0, 0, TimeSpan.Zero), source.LastCheckedAt);
        });
        Assert.All(catalog.OfficialSources.GroupBy(item => (item.InformationEntryId, item.Title, item.Url.AbsoluteUri)), group => Assert.Single(group));

        var sourceById = catalog.OfficialSources.ToDictionary(item => item.Id);
        foreach (var (articleCode, expected) in Articles)
        {
            var draft = await File.ReadAllTextAsync(Path.Combine(draftsRoot, articleCode + ".md"));
            Assert.Contains($"Article ID: `{expected.Id}`", draft);
            var sourceMatches = Regex.Matches(draft,
                @"(?m)^- `(?<key>FR-[^`]+)` → `(?<id>[^`]+)` — (?<title>.*?) \(research source .*?; \[official source\]\((?<url>https://[^)]+)\)\)$");
            Assert.Equal(ExpectedSourceCounts[articleCode], sourceMatches.Count);
            var ownedSources = sources.Where(item => item.InformationEntryId == expected.Id).ToDictionary(item => item.Id);
            Assert.Equal(ExpectedSourceCounts[articleCode], ownedSources.Count);
            foreach (Match sourceMatch in sourceMatches)
            {
                var sourceId = Guid.Parse(sourceMatch.Groups["id"].Value);
                Assert.True(ownedSources.TryGetValue(sourceId, out var source));
                Assert.Equal(expected.Id, source!.InformationEntryId);
                Assert.Equal(sourceMatch.Groups["title"].Value, source.Title);
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
    public async Task FranceIsVisibleInDevelopmentButHiddenFromProductionAndComparison(string language)
    {
        var catalog = await Catalog.Value;
        var productionHome = await RenderAsync<Home>("Production", language);
        Assert.Equal(13, Regex.Matches(productionHome.Html, "class=\"country-card\"").Count);
        Assert.DoesNotContain("href=\"/countries/france\"", productionHome.Html);
        var productionSearch = await RenderAsync<Search>("Production", language, "/search");
        Assert.DoesNotContain("france", SelectValues(productionSearch.Html, "country"));
        var productionOverview = await RenderAsync<CountryOverview>("Production", language, "/countries/france",
            new() { [nameof(CountryOverview.CountrySlug)] = "france" });
        Assert.True(productionOverview.NotFound);
        var productionCategory = await RenderAsync<Category>("Production", language,
            "/countries/france/categories/employment", new()
            {
                [nameof(Category.CountrySlug)] = "france",
                [nameof(Category.CategorySlug)] = "employment"
            });
        Assert.True(productionCategory.NotFound);
        foreach (var categorySlug in new[] { "temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance" })
        {
            var result = await RenderAsync<Category>("Production", language,
                $"/countries/france/categories/{categorySlug}", new()
                {
                    [nameof(Category.CountrySlug)] = "france",
                    [nameof(Category.CategorySlug)] = categorySlug
                });
            Assert.True(result.NotFound);
        }
        var productionResults = await RenderAsync<Search>("Production", language, "/search?country=france");
        Assert.Empty(Regex.Matches(productionResults.Html, "class=\"result-card\"").Cast<Match>());
        var productionCompare = await RenderAsync<Compare>("Production", language, "/compare");
        Assert.DoesNotContain("france", SelectValues(productionCompare.Html, "country1"));
        Assert.DoesNotContain("belgium", SelectValues(productionCompare.Html, "country1"));
        Assert.DoesNotContain("france", SelectValues(productionCompare.Html, "country2"));
        Assert.DoesNotContain("belgium", SelectValues(productionCompare.Html, "country2"));

        var developmentHome = await RenderAsync<Home>("Development", language);
        Assert.Equal(18, Regex.Matches(developmentHome.Html, "class=\"country-card\"").Count);
        Assert.Contains("href=\"/countries/france\"", developmentHome.Html);
        var developmentOverview = await RenderAsync<CountryOverview>("Development", language, "/countries/france",
            new() { [nameof(CountryOverview.CountrySlug)] = "france" });
        Assert.False(developmentOverview.NotFound);
        var developmentSearch = await RenderAsync<Search>("Development", language, "/search");
        Assert.Contains("france", SelectValues(developmentSearch.Html, "country"));
        var franceSearch = await RenderAsync<Search>("Development", language, "/search?country=france");
        Assert.Equal(6, Regex.Matches(franceSearch.Html, "class=\"result-card\"").Count);
        Assert.Equal(6, ArticleSearch.Search(catalog, null, "france", null, null, language).Count);

        var categoryCounts = new Dictionary<string, int>
        {
            ["employment"] = 2,
            ["temporary-protection"] = 1,
            ["healthcare"] = 1,
            ["education"] = 1,
            ["social-assistance"] = 1,
            ["housing"] = 0
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
        Assert.DoesNotContain("france", SelectValues(developmentCompare.Html, "country1"));
        Assert.DoesNotContain("belgium", SelectValues(developmentCompare.Html, "country1"));
        Assert.DoesNotContain("france", SelectValues(developmentCompare.Html, "country2"));
        Assert.DoesNotContain("belgium", SelectValues(developmentCompare.Html, "country2"));
        Assert.DoesNotContain("france", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.DoesNotContain("ireland", CountryComparisonTopicMap.SupportedCountrySlugs);
    }

    [Fact]
    public async Task DevelopmentFranceSearchExcludesOutdatedTranslation()
    {
        const string language = "en";
        var catalog = await Catalog.Value;
        var outdatedTranslation = Assert.Single(catalog.InformationEntryTranslations,
            item => item.InformationEntryId == Articles["FR-EMP-01"].Id && item.LanguageCode == language);
        var previewCatalog = WithFranceTranslationStatus(catalog, outdatedTranslation.InformationEntryId,
            language, VerificationStatus.Outdated);

        var search = await RenderAsync<Search>("Development", language, "/search?country=france",
            catalogOverride: previewCatalog);

        Assert.Equal(5, Regex.Matches(search.Html, "class=\"result-card\"").Count);
        Assert.DoesNotContain(outdatedTranslation.Title, search.Html);
        Assert.Contains(catalog.InformationEntryTranslations.Single(item =>
            item.InformationEntryId == Articles["FR-EMP-02"].Id && item.LanguageCode == language).Title, search.Html);
    }

    [Fact]
    public void FranceCountryLocalizationIsPresentInAllLanguages()
    {
        Assert.Equal("France", Resources.GetString("Country.France", CultureInfo.GetCultureInfo("en")));
        Assert.Equal("in France", Resources.GetString("Country.France.In", CultureInfo.GetCultureInfo("en")));
        Assert.Equal("Франція", Resources.GetString("Country.France", CultureInfo.GetCultureInfo("uk")));
        Assert.Equal("у Франції", Resources.GetString("Country.France.In", CultureInfo.GetCultureInfo("uk")));
        Assert.Equal("Франция", Resources.GetString("Country.France", CultureInfo.GetCultureInfo("ru")));
        Assert.Equal("во Франции", Resources.GetString("Country.France.In", CultureInfo.GetCultureInfo("ru")));
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
