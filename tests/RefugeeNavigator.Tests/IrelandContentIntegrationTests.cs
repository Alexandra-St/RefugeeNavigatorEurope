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

public sealed class IrelandContentIntegrationTests
{
    private static readonly Guid CountryId = Guid.Parse("10000000-0000-0000-0000-000000000014");
    private static readonly IReadOnlyDictionary<string, (Guid Id, string Category)> Articles = new Dictionary<string, (Guid, string)>
    {
        ["IE-EMP-01"] = (Guid.Parse("db600af0-bbf7-517b-ac82-3a7781105b8d"), "employment"),
        ["IE-EMP-02"] = (Guid.Parse("e6b45fb5-4180-5e57-8e48-30c25ddad0f8"), "employment"),
        ["IE-HEALTH-01"] = (Guid.Parse("864a1ff8-dc63-56bf-ae78-860a452a8f04"), "healthcare"),
        ["IE-EDU-01"] = (Guid.Parse("8dc791b3-5002-59ab-b8fb-2c7ab1dbf43b"), "education"),
        ["IE-SOC-01"] = (Guid.Parse("682561e1-29db-5332-974b-cf38327a859d"), "social-assistance")
    };
    private static readonly IReadOnlyDictionary<string, int> ExpectedSourceCounts = new Dictionary<string, int>
    {
        ["IE-EMP-01"] = 9,
        ["IE-EMP-02"] = 20,
        ["IE-HEALTH-01"] = 10,
        ["IE-EDU-01"] = 15,
        ["IE-SOC-01"] = 16
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
    public async Task IrelandHasFiveDevelopmentOnlyArticlesAndExpectedCatalogCounts()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "ireland");
        Assert.Equal(CountryId, country.Id);
        Assert.Equal("Ireland", country.Name);
        Assert.Equal(19, catalog.Countries.Count);
        Assert.Equal(147, catalog.InformationEntries.Count);
        Assert.Equal(441, catalog.InformationEntryTranslations.Count);
        Assert.Equal(357, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(84, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Outdated));

        var entries = catalog.InformationEntries.Where(item => item.CountryId == CountryId).ToArray();
        Assert.Equal(5, entries.Length);
        Assert.Equal(15, catalog.InformationEntryTranslations.Count(item => entries.Any(entry => entry.Id == item.InformationEntryId)));
        Assert.Equal(Articles.Values.Select(item => item.Id).Order().ToArray(), entries.Select(item => item.Id).Order().ToArray());
        var categoryCounts = entries
            .GroupBy(item => catalog.Categories.Single(category => category.Id == item.CategoryId).Slug)
            .ToDictionary(group => group.Key, group => group.Count());
        Assert.Equal(2, categoryCounts["employment"]);
        Assert.Equal(1, categoryCounts["healthcare"]);
        Assert.Equal(1, categoryCounts["education"]);
        Assert.Equal(1, categoryCounts["social-assistance"]);
        Assert.DoesNotContain("temporary-protection", categoryCounts.Keys);
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
                Assert.Equal(new DateTimeOffset(2026, 11, 10, 0, 0, 0, TimeSpan.Zero), item.LastUpdatedAt);
            });
        }

        Assert.Equal(14, CountryComparisonTopicMap.SupportedCountrySlugs.Count);
        Assert.DoesNotContain("ireland", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.DoesNotContain("belgium", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.All(CountryComparisonTopicMap.All, topic => Assert.Equal(14, topic.EntryIds.Count));
        var allFacts = (await Facts.Value).Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).ToArray();
        Assert.Equal(336, allFacts.Length);
        Assert.DoesNotContain(allFacts, fact => fact.CountrySlug == "ireland");
        Assert.DoesNotContain(allFacts, fact => fact.CountrySlug == "belgium");
    }

    [Fact]
    public async Task IrelandTranslationsAndSeventyArticleOwnedSourcesMatchDraftsExactly()
    {
        var catalog = await Catalog.Value;
        Assert.Equal(1400, catalog.OfficialSources.Count);
        Assert.Equal(catalog.OfficialSources.Count, catalog.OfficialSources.Select(item => item.Id).Distinct().Count());
        var repositoryRoot = FindRepositoryRoot();
        var draftsRoot = Path.Combine(repositoryRoot, "docs", "content-drafts", "ireland-v1");
        var articleIds = Articles.Values.Select(item => item.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(item => articleIds.Contains(item.InformationEntryId)).ToArray();
        Assert.Equal(70, sources.Length);
        Assert.Equal(70, sources.Select(item => item.Id).Distinct().Count());
        Assert.All(sources, source =>
        {
            Assert.Contains(source.InformationEntryId, articleIds);
            Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme);
            Assert.Equal(new DateTimeOffset(2026, 11, 10, 0, 0, 0, TimeSpan.Zero), source.LastCheckedAt);
        });
        Assert.All(catalog.OfficialSources.GroupBy(item => (item.InformationEntryId, item.Title, item.Url.AbsoluteUri)), group => Assert.Single(group));

        var sourceById = catalog.OfficialSources.ToDictionary(item => item.Id);
        foreach (var (articleCode, expected) in Articles)
        {
            var draft = await File.ReadAllTextAsync(Path.Combine(draftsRoot, articleCode + ".md"));
            Assert.Contains($"Article ID: `{expected.Id}`", draft);
            var sourceMatches = Regex.Matches(draft,
                @"(?m)^- `(?<key>IE-[^`]+)` → `(?<id>[^`]+)` — (?<title>.*?) \(research source .*?; \[official source\]\((?<url>https://[^)]+)\)\)$");
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
    public async Task IrelandIsVisibleInDevelopmentButHiddenFromProductionAndComparison(string language)
    {
        var catalog = await Catalog.Value;
        var productionHome = await RenderAsync<Home>("Production", language);
        Assert.Equal(15, Regex.Matches(productionHome.Html, "class=\"country-card\"").Count);
        Assert.DoesNotContain("href=\"/countries/ireland\"", productionHome.Html);
        var productionSearch = await RenderAsync<Search>("Production", language, "/search");
        Assert.DoesNotContain("ireland", SelectValues(productionSearch.Html, "country"));
        var productionOverview = await RenderAsync<CountryOverview>("Production", language, "/countries/ireland",
            new() { [nameof(CountryOverview.CountrySlug)] = "ireland" });
        Assert.True(productionOverview.NotFound);
        var productionCategory = await RenderAsync<Category>("Production", language,
            "/countries/ireland/categories/employment", new()
            {
                [nameof(Category.CountrySlug)] = "ireland",
                [nameof(Category.CategorySlug)] = "employment"
            });
        Assert.True(productionCategory.NotFound);
        var productionResults = await RenderAsync<Search>("Production", language, "/search?country=ireland");
        Assert.Empty(Regex.Matches(productionResults.Html, "class=\"result-card\"").Cast<Match>());
        var productionCompare = await RenderAsync<Compare>("Production", language, "/compare");
        Assert.DoesNotContain("ireland", SelectValues(productionCompare.Html, "country1"));
        Assert.DoesNotContain("belgium", SelectValues(productionCompare.Html, "country1"));

        var developmentHome = await RenderAsync<Home>("Development", language);
        Assert.Equal(19, Regex.Matches(developmentHome.Html, "class=\"country-card\"").Count);
        Assert.Contains("href=\"/countries/ireland\"", developmentHome.Html);
        var developmentOverview = await RenderAsync<CountryOverview>("Development", language, "/countries/ireland",
            new() { [nameof(CountryOverview.CountrySlug)] = "ireland" });
        Assert.False(developmentOverview.NotFound);
        var developmentSearch = await RenderAsync<Search>("Development", language, "/search");
        Assert.Contains("ireland", SelectValues(developmentSearch.Html, "country"));
        var irelandSearch = await RenderAsync<Search>("Development", language, "/search?country=ireland");
        Assert.Equal(5, Regex.Matches(irelandSearch.Html, "class=\"result-card\"").Count);
        Assert.Equal(5, ArticleSearch.Search(catalog, null, "ireland", null, null, language).Count);

        var categoryCounts = new Dictionary<string, int>
        {
            ["employment"] = 2,
            ["healthcare"] = 1,
            ["education"] = 1,
            ["social-assistance"] = 1,
            ["temporary-protection"] = 0,
            ["housing"] = 0
        };
        foreach (var (categorySlug, expectedCount) in categoryCounts)
        {
            var category = await RenderAsync<Category>("Development", language,
                $"/countries/ireland/categories/{categorySlug}", new()
                {
                    [nameof(Category.CountrySlug)] = "ireland",
                    [nameof(Category.CategorySlug)] = categorySlug
                });
            Assert.False(category.NotFound);
            Assert.Equal(expectedCount, Regex.Matches(category.Html, "class=\"information-entry\"").Count);
        }

        var developmentCompare = await RenderAsync<Compare>("Development", language, "/compare");
        Assert.DoesNotContain("ireland", SelectValues(developmentCompare.Html, "country1"));
        Assert.DoesNotContain("belgium", SelectValues(developmentCompare.Html, "country1"));
        Assert.DoesNotContain("ireland", SelectValues(developmentCompare.Html, "country2"));
        Assert.DoesNotContain("belgium", SelectValues(developmentCompare.Html, "country2"));
        Assert.DoesNotContain("ireland", CountryComparisonTopicMap.SupportedCountrySlugs);
    }

    [Fact]
    public async Task DevelopmentIrelandSearchExcludesOutdatedTranslation()
    {
        const string language = "en";
        var catalog = await Catalog.Value;
        var outdatedTranslation = Assert.Single(catalog.InformationEntryTranslations,
            item => item.InformationEntryId == Articles["IE-EMP-01"].Id && item.LanguageCode == language);
        var previewCatalog = WithIrelandTranslationStatus(catalog, outdatedTranslation.InformationEntryId,
            language, VerificationStatus.Outdated);

        var search = await RenderAsync<Search>("Development", language, "/search?country=ireland",
            catalogOverride: previewCatalog);

        Assert.Equal(4, Regex.Matches(search.Html, "class=\"result-card\"").Count);
        Assert.DoesNotContain(outdatedTranslation.Title, search.Html);
        Assert.Contains(catalog.InformationEntryTranslations.Single(item =>
            item.InformationEntryId == Articles["IE-EMP-02"].Id && item.LanguageCode == language).Title, search.Html);
    }

    [Fact]
    public void IrelandCountryLocalizationIsPresentInAllLanguages()
    {
        Assert.Equal("Ireland", Resources.GetString("Country.Ireland", CultureInfo.GetCultureInfo("en")));
        Assert.Equal("Ireland", Resources.GetString("Country.Ireland.In", CultureInfo.GetCultureInfo("en")));
        Assert.Equal("Ірландія", Resources.GetString("Country.Ireland", CultureInfo.GetCultureInfo("uk")));
        Assert.Equal("в Ірландії", Resources.GetString("Country.Ireland.In", CultureInfo.GetCultureInfo("uk")));
        Assert.Equal("Ирландия", Resources.GetString("Country.Ireland", CultureInfo.GetCultureInfo("ru")));
        Assert.Equal("в Ирландии", Resources.GetString("Country.Ireland.In", CultureInfo.GetCultureInfo("ru")));
    }

    private static string[] SelectValues(string html, string name)
    {
        var select = Regex.Match(html, $"<select\\b[^>]*name=\"{name}\"[^>]*>(.*?)</select>", RegexOptions.Singleline);
        Assert.True(select.Success);
        return Regex.Matches(select.Groups[1].Value, "<option\\b[^>]*value=\"([^\"]+)\"")
            .Select(match => match.Groups[1].Value).ToArray();
    }

    private static CatalogData WithIrelandTranslationStatus(CatalogData catalog, Guid informationEntryId,
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
