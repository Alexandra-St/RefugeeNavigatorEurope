using System.Globalization;
using System.Net;
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
using RefugeeNavigator.Web.Components;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Comparison;
using RefugeeNavigator.Web.Guides;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Tests;

public sealed class SpainContentIntegrationTests
{
    private static readonly Guid SpainId = Guid.Parse("10000000-0000-0000-0000-000000000005");
    private static readonly IReadOnlyDictionary<string, Guid> CategoryIds = new Dictionary<string, Guid>
    {
        ["temporary-protection"] = Guid.Parse("20000000-0000-0000-0000-000000000001"),
        ["employment"] = Guid.Parse("20000000-0000-0000-0000-000000000002"),
        ["healthcare"] = Guid.Parse("20000000-0000-0000-0000-000000000003"),
        ["housing"] = Guid.Parse("20000000-0000-0000-0000-000000000004"),
        ["education"] = Guid.Parse("20000000-0000-0000-0000-000000000005"),
        ["social-assistance"] = Guid.Parse("20000000-0000-0000-0000-000000000006")
    };
    private static readonly IReadOnlyDictionary<string, int> ExpectedSourceCounts = new Dictionary<string, int>
    {
        ["ES-TP-01"] = 7,
        ["ES-TP-02"] = 4,
        ["ES-EMP-01"] = 7,
        ["ES-EMP-02"] = 9,
        ["ES-HEALTH-01"] = 3,
        ["ES-HOUSING-01"] = 6,
        ["ES-EDU-01"] = 6,
        ["ES-SOC-01"] = 11
    };
    private static readonly IReadOnlyDictionary<string, Guid> StableArticleIds = new Dictionary<string, Guid>
    {
        ["ES-TP-01"] = Guid.Parse("b278c1d6-1add-4e65-8af1-e16431eaffb3"),
        ["ES-TP-02"] = Guid.Parse("06ce2f10-d309-45b2-aced-e9dd8367ffed"),
        ["ES-EMP-01"] = Guid.Parse("a5107970-958a-4be6-9c98-ae484fc24a15"),
        ["ES-EMP-02"] = Guid.Parse("b76b631c-92de-4379-8136-c9e4e08e2fd2"),
        ["ES-HEALTH-01"] = Guid.Parse("50829c0b-7ee0-4ee4-be7b-47dfd0dc408d"),
        ["ES-HOUSING-01"] = Guid.Parse("bd08644f-dd27-412c-9bfd-888de1fe304d"),
        ["ES-EDU-01"] = Guid.Parse("084c06c7-592b-4eed-bb20-6cd1913f14a2"),
        ["ES-SOC-01"] = Guid.Parse("d33628da-cb49-44b5-a7aa-ae6c6ac8f389")
    };
    private static readonly string[] Languages = ["en", "uk", "ru"];
    private static readonly string[] Categories =
    [
        "temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance"
    ];
    private static readonly IReadOnlyDictionary<string, string> DraftFolders = new Dictionary<string, string>
    {
        ["ES-TP-01"] = "temporary-protection-application",
        ["ES-TP-02"] = "after-temporary-protection",
        ["ES-EMP-01"] = "finding-a-job",
        ["ES-EMP-02"] = "employment-rights",
        ["ES-HEALTH-01"] = "healthcare",
        ["ES-HOUSING-01"] = "housing",
        ["ES-EDU-01"] = "education",
        ["ES-SOC-01"] = "social-assistance"
    };
    private static readonly Lazy<Task<CatalogData>> Catalog = new(() =>
        new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());
    private static readonly ResourceManager Resources = new(
        "RefugeeNavigator.Web.Resources.Localization.SharedResource", typeof(SharedResource).Assembly);

    [Fact]
    public async Task SpainCatalogHasExpectedStableIdentityAndVerifiedTranslations()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "spain");

        Assert.Equal(SpainId, country.Id);
        Assert.Equal("Spain", country.Name);
        Assert.Equal(19, catalog.Countries.Count);
        Assert.Equal(152, catalog.InformationEntries.Count);
        Assert.Equal(456, catalog.InformationEntryTranslations.Count);
        Assert.Equal(456, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Outdated));

        var entries = catalog.InformationEntries.Where(item => item.CountryId == SpainId).ToArray();
        Assert.Equal(8, entries.Length);
        Assert.Equal(StableArticleIds.Values.Order().ToArray(), entries.Select(item => item.Id).Order().ToArray());

        foreach (var (key, expectedId) in StableArticleIds)
        {
            var entry = Assert.Single(entries, item => item.Id == expectedId);
            Assert.Equal(SpainId, entry.CountryId);
            Assert.Equal(CategoryIds[ExpectedCategory(key)], entry.CategoryId);

            var translations = catalog.InformationEntryTranslations
                .Where(item => item.InformationEntryId == entry.Id).ToArray();
            Assert.Equal(3, translations.Length);
            Assert.Equal(Languages.Order(StringComparer.Ordinal).ToArray(),
                translations.Select(item => item.LanguageCode).Order(StringComparer.Ordinal).ToArray());
            Assert.All(translations, item =>
            {
                Assert.Equal(VerificationStatus.Verified, item.VerificationStatus);
                Assert.Equal(DateTimeOffset.Parse("2026-11-18T00:00:00+00:00"), item.LastUpdatedAt);
                Assert.False(string.IsNullOrWhiteSpace(item.Title));
                Assert.False(string.IsNullOrWhiteSpace(item.Summary));
                Assert.False(string.IsNullOrWhiteSpace(item.Content));
            });
        }

        Assert.Equal(24, catalog.InformationEntryTranslations.Count(item =>
            entries.Select(entry => entry.Id).Contains(item.InformationEntryId)));
    }

    [Fact]
    public async Task IntegratedTranslationsMatchAllSpainDraftsExactly()
    {
        var catalog = await Catalog.Value;
        var repositoryRoot = FindRepositoryRoot();
        var entries = catalog.InformationEntries.Where(item => item.CountryId == SpainId).ToArray();
        var languageMap = new Dictionary<string, string> { ["en"] = "EN", ["uk"] = "UK", ["ru"] = "RU" };

        Assert.Equal(8, Directory.GetFiles(Path.Combine(repositoryRoot, "docs", "content-drafts", "spain-v1"), "ES-*.md").Length);
        foreach (var (key, folder) in DraftFolders)
        {
            var draft = await File.ReadAllTextAsync(Path.Combine(repositoryRoot, "docs", "content-drafts", "spain-v1", key + ".md"));
            var entry = Assert.Single(entries, item => item.Id == StableArticleIds[key]);

            foreach (var (languageCode, section) in languageMap)
            {
                var expected = ParseDraft(draft, section);
                var actual = Assert.Single(catalog.InformationEntryTranslations,
                    item => item.InformationEntryId == entry.Id && item.LanguageCode == languageCode);
                Assert.Equal(expected.Title, actual.Title);
                Assert.Equal(expected.Summary, actual.Summary);
                Assert.Equal(NormalizeLines(expected.Content), NormalizeLines(actual.Content));
                Assert.DoesNotContain("Claim coverage", actual.Content, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("Editorial source set", actual.Content, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("https://", actual.Content, StringComparison.OrdinalIgnoreCase);
            }
        }
    }

    [Fact]
    public async Task SpainSourcesAreUniqueOwnedValidAndFreshlyChecked()
    {
        var catalog = await Catalog.Value;
        var entryIds = catalog.InformationEntries.Where(item => item.CountryId == SpainId)
            .Select(item => item.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(item => entryIds.Contains(item.InformationEntryId)).ToArray();

        Assert.Equal(53, sources.Length);
        Assert.Equal(sources.Length, sources.Select(item => item.Id).Distinct().Count());
        Assert.Equal(catalog.OfficialSources.Count, catalog.OfficialSources.Select(item => item.Id).Distinct().Count());
        Assert.All(sources, source =>
        {
            Assert.Contains(source.InformationEntryId, entryIds);
            Assert.True(source.Url.IsAbsoluteUri);
            Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme);
            Assert.False(string.IsNullOrWhiteSpace(source.Title));
            Assert.NotEqual(default, source.LastCheckedAt);
            Assert.True(source.LastCheckedAt <= DateTimeOffset.UtcNow);
        });

        foreach (var (key, expectedCount) in ExpectedSourceCounts)
        {
            Assert.Equal(expectedCount, sources.Count(item => item.InformationEntryId == StableArticleIds[key]));
        }
        Assert.Contains(sources, item =>
            item.InformationEntryId == StableArticleIds["ES-TP-02"]
            && item.Url == new Uri("https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX:02003L0109-20260612"));
        var added = Assert.Single(sources, item => item.Id == Guid.Parse("55dfb9f1-bee9-54a7-867a-0c2379dca38e"));
        Assert.Equal(StableArticleIds["ES-EMP-02"], added.InformationEntryId);
        Assert.Equal(new Uri("https://www.lamoncloa.gob.es/consejoministros/referencias/Paginas/2026/20261117-referencia-rueda-de-consejo-ministros.aspx"), added.Url);
        Assert.Equal(new Uri("https://eures.europa.eu/index_es"), Assert.Single(sources, item => item.Id == Guid.Parse("ba96278e-9e7b-40cd-a678-f917e392e861")).Url);
        Assert.Equal(new Uri("https://www.seg-social.es/wps/portal/wss/internet/Trabajadores/PrestacionesPensionesTrabajadores/10967/27924/desc27924?changeLanguage=es"), Assert.Single(sources, item => item.Id == Guid.Parse("f2bb0f3b-2674-54ea-8d97-5b0244e958cc")).Url);
    }

    [Fact]
    public async Task SpainRefreshPreservesReviewedSafetyDistinctionsAndCompareBinding()
    {
        var catalog = await Catalog.Value;
        var translations = catalog.InformationEntryTranslations.Where(item =>
            StableArticleIds.Values.Contains(item.InformationEntryId) && item.LanguageCode == "en")
            .ToDictionary(item => StableArticleIds.Single(pair => pair.Value == item.InformationEntryId).Key, item => item.Content);

        Assert.Contains("through 4 March 2028", translations["ES-TP-01"]);
        Assert.Contains("from 5 August 2026", translations["ES-TP-01"]);
        Assert.Contains("on 4 August 2026", translations["ES-TP-01"]);
        Assert.Contains("compliance with Ukrainian military obligations or an exemption", translations["ES-TP-01"]);
        Assert.Contains("does not specify age groups or the accepted evidence format", translations["ES-TP-01"]);
        Assert.Contains("through 4 March 2027", translations["ES-TP-01"]);
        Assert.Contains("does not establish that every TIE is valid through 2028", translations["ES-TP-01"]);
        Assert.DoesNotContain("30 July 2026", translations["ES-TP-01"]);
        Assert.DoesNotContain("24 hours", translations["ES-TP-01"], StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("guaranteed service time", translations["ES-TP-01"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Ukrainian nationality alone does not establish", translations["ES-TP-01"]);
        Assert.Contains("A receipt or NIE is not the same as a grant decision or a TIE card", translations["ES-TP-01"]);
        Assert.Contains("A decision granting temporary protection", translations["ES-EMP-02"]);
        Assert.Contains("employees or self-employed", translations["ES-EMP-02"]);
        Assert.Contains("€1,221 per month", translations["ES-EMP-02"]);
        Assert.Contains("€40.70 per day", translations["ES-EMP-02"]);
        Assert.Contains("€17,094", translations["ES-EMP-02"]);
        Assert.Contains("€18,004 per year from 1 January 2027", translations["ES-EMP-02"]);
        Assert.Contains("No hourly rate is inferred", translations["ES-EMP-02"]);
        Assert.Contains("route still apply", translations["ES-TP-02"]);
        Assert.Contains("not one new permit automatically granted", translations["ES-TP-02"]);
        Assert.Contains("does not by itself establish that a person qualifies for or will receive long-term residence", translations["ES-TP-02"]);
        Assert.Contains("must be granted", translations["ES-TP-02"]);
        Assert.Contains("A TIE, NIE, temporary-protection decision, application receipt and individual health card are different documents", translations["ES-HEALTH-01"]);
        Assert.Contains("not itself a health card", translations["ES-HEALTH-01"]);
        Assert.Contains("Autonomous Community", translations["ES-HEALTH-01"]);
        Assert.DoesNotContain("free for all Ukrainian applicants", translations["ES-HEALTH-01"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("is not guaranteed", translations["ES-HOUSING-01"]);
        Assert.Contains("temporary protection alone does not establish admission", translations["ES-HOUSING-01"]);
        Assert.Contains("publish calls and administer applications", translations["ES-HOUSING-01"]);
        Assert.Contains("Padrón records where a person actually lives", translations["ES-HOUSING-01"]);
        Assert.Contains("determine immigration status", translations["ES-HOUSING-01"]);
        Assert.Contains("means-tested Social Security benefit", translations["ES-SOC-01"]);
        Assert.Contains("do not assume the same cash assistance exists everywhere", translations["ES-SOC-01"]);
        Assert.Contains("ages 6 through 16", translations["ES-EDU-01"]);
        Assert.Contains("competent education authority determines placement", translations["ES-EDU-01"]);

        var comparison = await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json"))
            .LoadAsync(catalog);
        var wage = Assert.Single(comparison.Topics.SelectMany(topic => topic.Aspects)
            .Single(aspect => aspect.Id == "work-minimum-pay").Facts, fact => fact.CountrySlug == "spain");
        Assert.Equal(new[] { Guid.Parse("2b15b8cb-2568-4b24-b5eb-4afd6b3682a7"), Guid.Parse("55dfb9f1-bee9-54a7-867a-0c2379dca38e") }, wage.OfficialSourceIds);
        Assert.Contains("€1,221", wage.Values["en"]);
        Assert.Contains("€18,004 per year from 1 January 2027", wage.Values["en"]);
        Assert.DoesNotContain("€1,286", wage.Values["en"]);
        Assert.DoesNotContain("€42.87", wage.Values["en"]);
    }

    [Theory]
    [InlineData("en", "Spain", "Spain")]
    [InlineData("uk", "Іспанія", "в Іспанії")]
    [InlineData("ru", "Испания", "в Испании")]
    public async Task SpainLocalizationAndHomeCardRenderNaturally(string language, string name, string locationName)
    {
        var culture = CultureInfo.GetCultureInfo(language);
        Assert.Equal(name, Resources.GetString("Country.Spain", culture));
        Assert.Equal(locationName, Resources.GetString("Country.Spain.In", culture));

        var html = WebUtility.HtmlDecode(await RenderAsync<Home>(language));
        Assert.Contains(name, html);
        Assert.Contains(locationName, html);
        Assert.Contains("🇪🇸", html);
        Assert.Contains("href=\"/countries/spain\"", html);
    }

    [Theory]
    [InlineData("en", "Spain")]
    [InlineData("uk", "Іспанія")]
    [InlineData("ru", "Испания")]
    public async Task SpainOverviewHasSixCategoriesAndNoGuide(string language, string countryName)
    {
        var html = WebUtility.HtmlDecode(await RenderAsync<CountryOverview>(language,
            new Dictionary<string, object?> { [nameof(CountryOverview.CountrySlug)] = "spain" },
            "http://localhost/countries/spain"));

        Assert.Contains(countryName, html);
        Assert.Equal(6, html.Split("href=\"/countries/spain/categories/", StringSplitOptions.None).Length - 1);
        Assert.DoesNotContain("country-guide-callout", html, StringComparison.Ordinal);
        Assert.DoesNotContain("Country.Spain", html, StringComparison.Ordinal);
        foreach (var category in Categories)
        {
            Assert.Contains($"/countries/spain/categories/{category}", html);
        }
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task AllSpainCategoryPagesRenderTheirVerifiedArticles(string language)
    {
        var catalog = await Catalog.Value;
        var entries = catalog.InformationEntries.Where(item => item.CountryId == SpainId).ToArray();

        foreach (var category in Categories)
        {
            var html = WebUtility.HtmlDecode(await RenderAsync<Category>(language,
                new Dictionary<string, object?>
                {
                    [nameof(Category.CountrySlug)] = "spain",
                    [nameof(Category.CategorySlug)] = category
                },
                $"http://localhost/countries/spain/categories/{category}"));

            var categoryId = CategoryIds[category];
            var matchingEntries = entries.Where(item => item.CategoryId == categoryId).ToArray();
            Assert.Equal(category == "temporary-protection" || category == "employment" ? 2 : 1, matchingEntries.Length);
            foreach (var entry in matchingEntries)
            {
                var translation = Assert.Single(catalog.InformationEntryTranslations,
                    item => item.InformationEntryId == entry.Id && item.LanguageCode == language);
                Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
                Assert.Contains(translation.Title, html);
                Assert.Contains(translation.Summary, html);
            }
            Assert.DoesNotContain("country-guide-callout", html, StringComparison.Ordinal);
            Assert.DoesNotContain("Country.Spain", html, StringComparison.Ordinal);
            Assert.DoesNotContain("Category.", html, StringComparison.Ordinal);
        }
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task SpainVerifiedArticlesAppearInSearchAndHonorCategoryFilter(string language)
    {
        var catalog = await Catalog.Value;
        var entries = catalog.InformationEntries.Where(item => item.CountryId == SpainId).ToArray();
        var noResults = Resources.GetString("Search.NoResults", CultureInfo.GetCultureInfo(language))!;
        var html = WebUtility.HtmlDecode(await RenderAsync<Search>(language, uri: "http://localhost/search?country=spain"));

        Assert.Contains("value=\"spain\"", html);
        Assert.DoesNotContain(noResults, html);
        foreach (var entry in entries)
        {
            var translation = Assert.Single(catalog.InformationEntryTranslations,
                item => item.InformationEntryId == entry.Id && item.LanguageCode == language);
            Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
            Assert.Contains(translation.Title, html);
        }

        foreach (var category in Categories)
        {
            var filteredHtml = WebUtility.HtmlDecode(await RenderAsync<Search>(language,
                uri: $"http://localhost/search?country=spain&category={category}"));
            var matchingEntries = entries.Where(entry => entry.CategoryId == CategoryIds[category]).ToArray();
            Assert.NotEmpty(matchingEntries);
            Assert.DoesNotContain(noResults, filteredHtml);
            foreach (var entry in matchingEntries)
            {
                var translation = Assert.Single(catalog.InformationEntryTranslations,
                    item => item.InformationEntryId == entry.Id && item.LanguageCode == language);
                Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
                Assert.Contains(translation.Title, filteredHtml);
            }
            foreach (var entry in entries.Except(matchingEntries))
            {
                var translation = Assert.Single(catalog.InformationEntryTranslations,
                    item => item.InformationEntryId == entry.Id && item.LanguageCode == language);
                Assert.DoesNotContain(translation.Title, filteredHtml);
            }
        }
    }

    [Fact]
    public async Task CountryComparisonOffersSpainAlongsideTheFourExistingCountries()
    {
        var html = WebUtility.HtmlDecode(await RenderAsync<Compare>("en", uri: "http://localhost/compare"));
        Assert.Contains("value=\"spain\"", html, StringComparison.Ordinal);
        Assert.Contains("value=\"denmark\"", html, StringComparison.Ordinal);
        Assert.Contains("value=\"germany\"", html, StringComparison.Ordinal);
        Assert.Contains("value=\"poland\"", html, StringComparison.Ordinal);
        Assert.Contains("value=\"czechia\"", html, StringComparison.Ordinal);
    }

    private static string ExpectedCategory(string key) => key switch
    {
        "ES-TP-01" or "ES-TP-02" => "temporary-protection",
        "ES-EMP-01" or "ES-EMP-02" => "employment",
        "ES-HEALTH-01" => "healthcare",
        "ES-HOUSING-01" => "housing",
        "ES-EDU-01" => "education",
        "ES-SOC-01" => "social-assistance",
        _ => throw new InvalidOperationException($"Unknown article key {key}.")
    };

    private static string FindRepositoryRoot()
    {
        for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "docs", "content-drafts", "spain-v1", "ES-TP-01.md")))
            {
                return directory.FullName;
            }
        }
        throw new DirectoryNotFoundException("Could not locate the Spain editorial drafts.");
    }

    private static (string Title, string Summary, string Content) ParseDraft(string markdown, string language)
    {
        var match = Regex.Match(markdown,
            $"^## {language}\\s*\\n(.*?)(?=^## (?:EN|UK|RU)\\s*$|\\z)",
            RegexOptions.Multiline | RegexOptions.Singleline);
        Assert.True(match.Success, $"Draft section {language} is missing.");
        var section = match.Groups[1].Value.Split("### Claim coverage", 2, StringSplitOptions.None)[0];
        string Field(string name) => Regex.Match(section,
            $"^{name}:\\s*\\n([^\\r\\n]+)", RegexOptions.Multiline).Groups[1].Value.Trim();
        var content = Regex.Match(section, "^Content:\\s*\\n(.*)$",
            RegexOptions.Multiline | RegexOptions.Singleline).Groups[1].Value.Trim();
        return (Field("Title"), Field("Summary"), content);
    }

    private static string NormalizeLines(string text) => text.Replace("\r\n", "\n").Trim();

    private static async Task<string> RenderAsync<TComponent>(
        string language,
        IDictionary<string, object?>? parameters = null,
        string uri = "http://localhost/") where TComponent : IComponent
    {
        var oldCulture = CultureInfo.CurrentCulture;
        var oldUiCulture = CultureInfo.CurrentUICulture;
        var culture = CultureInfo.GetCultureInfo(language);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        try
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton(await Catalog.Value);
            services.AddSingleton(new StepByStepGuideCatalog([]));
            services.AddSingleton<IWebHostEnvironment>(new TestEnvironment());
            services.AddSingleton<NavigationManager>(_ => new TestNavigationManager(uri));
            await ComparisonTestServices.RegisterAsync(services);

            using var provider = services.BuildServiceProvider();
            await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());
            var parameterView = parameters is null ? ParameterView.Empty : ParameterView.FromDictionary(parameters);
            return await renderer.Dispatcher.InvokeAsync(async () =>
            {
                var component = await renderer.RenderComponentAsync<TComponent>(parameterView);
                return component.ToHtmlString();
            });
        }
        finally
        {
            CultureInfo.CurrentCulture = oldCulture;
            CultureInfo.CurrentUICulture = oldUiCulture;
        }
    }

    private sealed class TestNavigationManager : NavigationManager
    {
        public TestNavigationManager(string uri) => Initialize("http://localhost/", uri);

        protected override void NavigateToCore(string uri, bool forceLoad) => Uri = ToAbsoluteUri(uri).AbsoluteUri;
    }

    private sealed class TestEnvironment : IWebHostEnvironment
    {
        public string ApplicationName { get; set; } = "RefugeeNavigator.Tests";
        public string EnvironmentName { get; set; } = "Development";
        public string WebRootPath { get; set; } = AppContext.BaseDirectory;
        public string ContentRootPath { get; set; } = AppContext.BaseDirectory;
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }
}
