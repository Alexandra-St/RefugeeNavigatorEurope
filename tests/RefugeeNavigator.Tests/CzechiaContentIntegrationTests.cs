using System.Globalization;
using System.Net;
using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Components;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Guides;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Tests;

public sealed class CzechiaContentIntegrationTests
{
    private static readonly Guid CzechiaId = Guid.Parse("10000000-0000-0000-0000-000000000004");
    private static readonly IReadOnlyDictionary<string, Guid> CategoryIds = new Dictionary<string, Guid>
    {
        ["temporary-protection"] = Guid.Parse("20000000-0000-0000-0000-000000000001"),
        ["employment"] = Guid.Parse("20000000-0000-0000-0000-000000000002"),
        ["healthcare"] = Guid.Parse("20000000-0000-0000-0000-000000000003"),
        ["housing"] = Guid.Parse("20000000-0000-0000-0000-000000000004"),
        ["education"] = Guid.Parse("20000000-0000-0000-0000-000000000005"),
        ["social-assistance"] = Guid.Parse("20000000-0000-0000-0000-000000000006")
    };
    private static readonly IReadOnlyDictionary<string, int> ExpectedArticleCounts = new Dictionary<string, int>
    {
        ["temporary-protection"] = 2,
        ["employment"] = 2,
        ["healthcare"] = 1,
        ["housing"] = 1,
        ["education"] = 1,
        ["social-assistance"] = 1
    };
    private static readonly IReadOnlyDictionary<string, int> ExpectedSourceCounts = new Dictionary<string, int>
    {
        ["CZ-TP-01"] = 5,
        ["CZ-TP-02"] = 10,
        ["CZ-EMP-01"] = 7,
        ["CZ-EMP-02"] = 9,
        ["CZ-HEALTH-01"] = 10,
        ["CZ-HOUSING-01"] = 6,
        ["CZ-EDU-01"] = 4,
        ["CZ-SOC-01"] = 6
    };
    private static readonly string[] Languages = ["uk", "en", "ru"];
    private static readonly string[] Categories =
    [
        "temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance"
    ];
    private static readonly Lazy<Task<CatalogData>> Catalog = new(() =>
        new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());
    private static readonly ResourceManager Resources = new(
        "RefugeeNavigator.Web.Resources.Localization.SharedResource", typeof(SharedResource).Assembly);

    [Fact]
    public async Task CzechiaCatalogHasExpectedCountryEntriesAndVerifiedTranslations()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, country => country.Slug == "czechia");

        Assert.Equal(CzechiaId, country.Id);
        Assert.Equal("Czechia", country.Name);
        Assert.Equal(7, catalog.Countries.Count);
        Assert.Equal(56, catalog.InformationEntries.Count);
        Assert.Equal(168, catalog.InformationEntryTranslations.Count);
        Assert.Equal(120, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(48, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Outdated));

        var entries = catalog.InformationEntries.Where(entry => entry.CountryId == CzechiaId).ToArray();
        Assert.Equal(8, entries.Length);
        foreach (var (slug, expectedCount) in ExpectedArticleCounts)
        {
            Assert.Equal(expectedCount, entries.Count(entry => entry.CategoryId == CategoryIds[slug]));
        }

        var entryIds = entries.Select(entry => entry.Id).ToHashSet();
        var translations = catalog.InformationEntryTranslations
            .Where(translation => entryIds.Contains(translation.InformationEntryId)).ToArray();
        Assert.Equal(24, translations.Length);
        Assert.All(entries, entry =>
        {
            var languages = translations.Where(item => item.InformationEntryId == entry.Id)
                .Select(item => item.LanguageCode).Order(StringComparer.Ordinal).ToArray();
            Assert.Equal(new[] { "en", "ru", "uk" }, languages);
        });
        Assert.All(translations, translation =>
        {
            Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
            Assert.False(string.IsNullOrWhiteSpace(translation.Title));
            Assert.False(string.IsNullOrWhiteSpace(translation.Summary));
            Assert.False(string.IsNullOrWhiteSpace(translation.Content));
            Assert.DoesNotContain("Claim coverage", translation.Content, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("Editorial source set", translation.Content, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("CZ-TP01-", translation.Content, StringComparison.Ordinal);
            Assert.DoesNotContain("https://", translation.Content, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("](", translation.Content, StringComparison.Ordinal);
        });
    }

    [Fact]
    public async Task IntegratedArticleTranslationsMatchTheThreeEditorialDraftsExactly()
    {
        var catalog = await Catalog.Value;
        var repositoryRoot = FindRepositoryRoot();
        var entries = catalog.InformationEntries.Where(entry => entry.CountryId == CzechiaId).ToArray();
        var languageMap = new Dictionary<string, string> { ["en"] = "EN", ["uk"] = "UK", ["ru"] = "RU" };

        foreach (var draftPath in Directory.GetFiles(Path.Combine(repositoryRoot, "docs", "content-drafts", "czechia-v1"), "CZ-*.md"))
        {
            var key = Path.GetFileNameWithoutExtension(draftPath);
            var draft = await File.ReadAllTextAsync(draftPath);
            var entryId = Guid.Parse(StableArticleIds[key]);
            var entry = Assert.Single(entries, item => item.Id == entryId);
            Assert.Equal(CzechiaId, entry.CountryId);
            Assert.Equal(CategoryIds[entry.CategorySlug(catalog)], entry.CategoryId);

            foreach (var (languageCode, section) in languageMap)
            {
                var expected = ParseDraft(draft, section);
                var actual = Assert.Single(entry.Translations(catalog), item => item.LanguageCode == languageCode);
                Assert.Equal(expected.Title, actual.Title);
                Assert.Equal(expected.Summary, actual.Summary);
                Assert.Equal(NormalizeLines(expected.Content), NormalizeLines(actual.Content));
            }
        }

        Assert.Equal(8, entries.Length);
    }

    [Fact]
    public async Task CzechiaSourcesAreUniqueOwnedValidAndFreshlyChecked()
    {
        var catalog = await Catalog.Value;
        var entries = catalog.InformationEntries.Where(entry => entry.CountryId == CzechiaId).ToArray();
        var entryIds = entries.Select(entry => entry.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(source => entryIds.Contains(source.InformationEntryId)).ToArray();

        Assert.Equal(57, sources.Length);
        Assert.Equal(sources.Length, sources.Select(source => source.Id).Distinct().Count());
        Assert.All(sources, source =>
        {
            Assert.Contains(source.InformationEntryId, entryIds);
            Assert.True(source.Url.IsAbsoluteUri);
            Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme);
            Assert.False(string.IsNullOrWhiteSpace(source.Title));
            Assert.NotEqual(default, source.LastCheckedAt);
            Assert.True(source.LastCheckedAt <= DateTimeOffset.UtcNow.AddMinutes(2));
        });

        foreach (var (key, expectedCount) in ExpectedSourceCounts)
        {
            var entry = Assert.Single(entries, item => item.Id == Guid.Parse(StableArticleIds[key]));
            Assert.Equal(expectedCount, sources.Count(source => source.InformationEntryId == entry.Id));
        }
    }

    [Theory]
    [InlineData("en", "Czechia", "Czechia")]
    [InlineData("uk", "Чехія", "у Чехії")]
    [InlineData("ru", "Чехия", "в Чехии")]
    public async Task CountryLocalizationAndHomeCardRenderNaturally(string language, string name, string locationName)
    {
        var culture = CultureInfo.GetCultureInfo(language);
        Assert.Equal(name, Resources.GetString("Country.Czechia", culture));
        Assert.Equal(locationName, Resources.GetString("Country.Czechia.In", culture));

        var html = WebUtility.HtmlDecode(await RenderAsync<Home>(language));
        Assert.Contains(name, html);
        Assert.Contains(locationName, html);
        Assert.Contains("🇨🇿", html);
        Assert.Contains("href=\"/countries/czechia\"", html);
    }

    [Theory]
    [InlineData("uk", "Чехія")]
    [InlineData("en", "Czechia")]
    [InlineData("ru", "Чехия")]
    public async Task CountryOverviewShowsSixCategoryLinksAndNoGuideForCzechia(string language, string countryName)
    {
        var html = WebUtility.HtmlDecode(await RenderAsync<CountryOverview>(language,
            new Dictionary<string, object?> { [nameof(CountryOverview.CountrySlug)] = "czechia" },
            "http://localhost/countries/czechia"));
        Assert.Contains(countryName, html);
        Assert.Equal(6, html.Split("href=\"/countries/czechia/categories/", StringSplitOptions.None).Length - 1);
        Assert.DoesNotContain("country-guide-callout", html, StringComparison.Ordinal);
        foreach (var category in Categories)
        {
            Assert.Contains($"/countries/czechia/categories/{category}", html);
        }
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task AllSixCzechiaCategoryPagesRenderTheirVerifiedArticles(string language)
    {
        var catalog = await Catalog.Value;
        var entries = catalog.InformationEntries.Where(entry => entry.CountryId == CzechiaId).ToArray();
        foreach (var category in Categories)
        {
            var html = WebUtility.HtmlDecode(await RenderAsync<Category>(language,
                new Dictionary<string, object?>
                {
                    [nameof(Category.CountrySlug)] = "czechia",
                    [nameof(Category.CategorySlug)] = category
                },
                $"http://localhost/countries/czechia/categories/{category}"));
            var categoryId = CategoryIds[category];
            foreach (var entry in entries.Where(entry => entry.CategoryId == categoryId))
            {
                var translation = Assert.Single(entry.Translations(catalog), item => item.LanguageCode == language);
                Assert.Contains(translation.Title, html);
                Assert.Contains(translation.Summary, html);
            }
            Assert.DoesNotContain("country-guide-callout", html, StringComparison.Ordinal);
            Assert.DoesNotContain("Country.Czechia", html, StringComparison.Ordinal);
            Assert.DoesNotContain("Category.", html, StringComparison.Ordinal);
        }
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task PublicSearchReturnsVerifiedCzechiaArticlesAndHonorsCategoryFilter(string language)
    {
        var catalog = await Catalog.Value;
        var entries = catalog.InformationEntries.Where(entry => entry.CountryId == CzechiaId).ToArray();
        var noResults = Resources.GetString("Search.NoResults", CultureInfo.GetCultureInfo(language))!;
        var allHtml = WebUtility.HtmlDecode(await RenderAsync<Search>(language, uri: "http://localhost/search?country=czechia"));

        Assert.Contains("value=\"czechia\"", allHtml);
        Assert.DoesNotContain(noResults, allHtml);
        foreach (var entry in entries)
        {
            var translation = Assert.Single(catalog.InformationEntryTranslations, item =>
                item.InformationEntryId == entry.Id && item.LanguageCode == language);
            Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
            Assert.Contains(translation.Title, allHtml);
        }

        foreach (var category in Categories)
        {
            var filteredHtml = WebUtility.HtmlDecode(await RenderAsync<Search>(language,
                uri: $"http://localhost/search?country=czechia&category={category}"));
            var matchingEntries = entries.Where(entry => entry.CategoryId == CategoryIds[category]).ToArray();
            Assert.NotEmpty(matchingEntries);
            Assert.DoesNotContain(noResults, filteredHtml);
            foreach (var entry in matchingEntries)
            {
                var translation = Assert.Single(catalog.InformationEntryTranslations, item =>
                    item.InformationEntryId == entry.Id && item.LanguageCode == language);
                Assert.Contains(translation.Title, filteredHtml);
            }
            foreach (var entry in entries.Except(matchingEntries))
            {
                var translation = Assert.Single(catalog.InformationEntryTranslations, item =>
                    item.InformationEntryId == entry.Id && item.LanguageCode == language);
                Assert.DoesNotContain(translation.Title, filteredHtml);
            }
        }
    }

    [Fact]
    public async Task CountryComparisonOffersCzechia()
    {
        var html = WebUtility.HtmlDecode(await RenderAsync<Compare>("en", uri: "http://localhost/compare"));
        Assert.Contains("value=\"czechia\"", html, StringComparison.Ordinal);
        Assert.Contains("value=\"denmark\"", html, StringComparison.Ordinal);
        Assert.Contains("value=\"germany\"", html, StringComparison.Ordinal);
        Assert.Contains("value=\"poland\"", html, StringComparison.Ordinal);
    }

    private static readonly IReadOnlyDictionary<string, string> StableArticleIds = new Dictionary<string, string>
    {
        ["CZ-TP-01"] = "49d3c355-af96-54f7-92d5-68062647d464",
        ["CZ-TP-02"] = "2d35d95d-3524-5f3c-b7bd-ff61bd67ccd5",
        ["CZ-EMP-01"] = "e8a5e6d5-a989-51de-ae14-b58e724604b8",
        ["CZ-EMP-02"] = "829b9986-d949-5dd8-80c4-b9b4b7cf4d44",
        ["CZ-HEALTH-01"] = "d252adbc-48c0-5546-bbe1-2b08e2293f27",
        ["CZ-HOUSING-01"] = "957ae315-47a4-5a73-806e-ca1d6cf258d1",
        ["CZ-EDU-01"] = "954761f7-bc2b-5725-aeba-c8f1d2192588",
        ["CZ-SOC-01"] = "f408e721-31a3-572a-a702-69ab600023a3"
    };

    private static string FindRepositoryRoot()
    {
        for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "docs", "content-drafts", "czechia-v1", "CZ-TP-01.md")))
            {
                return directory.FullName;
            }
        }
        throw new DirectoryNotFoundException("Could not locate the checked-out repository draft sources.");
    }

    private static (string Title, string Summary, string Content) ParseDraft(string markdown, string language)
    {
        var match = Regex.Match(markdown, $"^## {language}\\s*\\n(.*?)(?=^## (?:EN|UK|RU)\\s*$|\\z)", RegexOptions.Multiline | RegexOptions.Singleline);
        Assert.True(match.Success, $"Draft section {language} is missing.");
        var section = match.Groups[1].Value.Split("### Claim coverage", 2, StringSplitOptions.None)[0];
        string Field(string name) => Regex.Match(section, $"^{name}:\\s*\\n([^\\r\\n]+)", RegexOptions.Multiline).Groups[1].Value.Trim();
        var content = Regex.Match(section, "^Content:\\s*\\n(.*)$", RegexOptions.Multiline | RegexOptions.Singleline).Groups[1].Value.Trim();
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
            services.AddSingleton<IWebHostEnvironment>(new TestEnvironment());
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

internal static class CzechiaTestCatalogExtensions
{
    public static string CategorySlug(this InformationEntry entry, CatalogData catalog) =>
        catalog.Categories.Single(category => category.Id == entry.CategoryId).Slug;

    public static IReadOnlyList<InformationEntryTranslation> Translations(this InformationEntry entry, CatalogData catalog) =>
        catalog.InformationEntryTranslations.Where(translation => translation.InformationEntryId == entry.Id).ToArray();
}
