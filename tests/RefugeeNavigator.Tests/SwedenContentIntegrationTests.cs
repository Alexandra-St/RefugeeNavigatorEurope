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

public sealed class SwedenContentIntegrationTests
{
    private static readonly Guid SwedenId = Guid.Parse("10000000-0000-0000-0000-000000000006");
    private static readonly IReadOnlyDictionary<string, Guid> CategoryIds = new Dictionary<string, Guid>
    {
        ["temporary-protection"] = Guid.Parse("20000000-0000-0000-0000-000000000001"),
        ["employment"] = Guid.Parse("20000000-0000-0000-0000-000000000002"),
        ["healthcare"] = Guid.Parse("20000000-0000-0000-0000-000000000003"),
        ["housing"] = Guid.Parse("20000000-0000-0000-0000-000000000004"),
        ["education"] = Guid.Parse("20000000-0000-0000-0000-000000000005"),
        ["social-assistance"] = Guid.Parse("20000000-0000-0000-0000-000000000006"),
    };
    private static readonly IReadOnlyDictionary<string, Guid> StableArticleIds = new Dictionary<string, Guid>
    {
        ["SE-TP-01"] = Guid.Parse("d2ab273d-21b7-44e4-b408-f4269a8753d8"),
        ["SE-TP-02"] = Guid.Parse("befa7777-af70-4269-883c-8918961554dc"),
        ["SE-EMP-01"] = Guid.Parse("a218013b-3a77-4ba2-829a-d724689837f2"),
        ["SE-EMP-02"] = Guid.Parse("55949af5-b4fe-404e-bcb7-6ebb37131bab"),
        ["SE-HEALTH-01"] = Guid.Parse("2234b91f-4021-40b2-b9d5-a029a6b533f2"),
        ["SE-HOUSING-01"] = Guid.Parse("e0f307e9-7991-4350-89f4-2f84a9047393"),
        ["SE-EDU-01"] = Guid.Parse("cac58c0c-ce69-41ac-877e-4fd124105b89"),
        ["SE-SOC-01"] = Guid.Parse("8b5cc53a-9933-4366-8421-d795966267ec"),
    };
    private static readonly IReadOnlyDictionary<string, int> ExpectedSourceCounts = new Dictionary<string, int>
    {
        ["SE-TP-01"] = 11,
        ["SE-TP-02"] = 10,
        ["SE-EMP-01"] = 7,
        ["SE-EMP-02"] = 9,
        ["SE-HEALTH-01"] = 6,
        ["SE-HOUSING-01"] = 6,
        ["SE-EDU-01"] = 10,
        ["SE-SOC-01"] = 9,
    };
    private static readonly IReadOnlyDictionary<string, string> DraftFolders = new Dictionary<string, string>
    {
        ["SE-TP-01"] = "temporary-protection-application",
        ["SE-TP-02"] = "after-temporary-protection",
        ["SE-EMP-01"] = "finding-a-job",
        ["SE-EMP-02"] = "employment-rights",
        ["SE-HEALTH-01"] = "healthcare",
        ["SE-HOUSING-01"] = "housing",
        ["SE-EDU-01"] = "education",
        ["SE-SOC-01"] = "social-assistance",
    };
    private static readonly string[] Languages = ["en", "uk", "ru"];
    private static readonly string[] Categories =
    [
        "temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance"
    ];
    private static readonly DateTimeOffset ArticleTimestamp = new(2026, 10, 20, 14, 1, 38, TimeSpan.Zero);
    private static readonly DateTimeOffset SourceCheckTimestamp = new(2026, 10, 20, 14, 4, 15, TimeSpan.Zero);
    private static readonly Lazy<Task<CatalogData>> Catalog = new(() =>
        new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());
    private static readonly ResourceManager Resources = new(
        "RefugeeNavigator.Web.Resources.Localization.SharedResource", typeof(SharedResource).Assembly);

    [Fact]
    public async Task SwedenCatalogHasExpectedStableIdentityAndNeedsReviewTranslations()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "sweden");
        Assert.Equal(SwedenId, country.Id);
        Assert.Equal("Sweden", country.Name);
        Assert.Equal(7, catalog.Countries.Count);
        Assert.Equal(56, catalog.InformationEntries.Count);
        Assert.Equal(168, catalog.InformationEntryTranslations.Count);
        Assert.Equal(126, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(42, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Outdated));

        var entries = catalog.InformationEntries.Where(item => item.CountryId == SwedenId).ToArray();
        Assert.Equal(8, entries.Length);
        Assert.Equal(StableArticleIds.Values.Order().ToArray(), entries.Select(item => item.Id).Order().ToArray());
        Assert.All(entries, entry =>
        {
            var translations = catalog.InformationEntryTranslations.Where(item => item.InformationEntryId == entry.Id).ToArray();
            Assert.Equal(3, translations.Length);
            Assert.Equal(Languages.Order(StringComparer.Ordinal).ToArray(), translations.Select(item => item.LanguageCode).Order(StringComparer.Ordinal).ToArray());
            Assert.All(translations, translation =>
            {
                Assert.Equal(VerificationStatus.NeedsReview, translation.VerificationStatus);
                Assert.False(string.IsNullOrWhiteSpace(translation.Title));
                Assert.False(string.IsNullOrWhiteSpace(translation.Summary));
                Assert.False(string.IsNullOrWhiteSpace(translation.Content));
                Assert.Equal(ArticleTimestamp, translation.LastUpdatedAt);
            });
        });
        Assert.Equal(new[] { 2, 2, 1, 1, 1, 1 }, Categories.Select(category =>
            entries.Count(entry => entry.CategoryId == CategoryIds[category])).ToArray());
    }

    [Fact]
    public async Task IntegratedTranslationsMatchAllSwedenDraftsExactly()
    {
        var catalog = await Catalog.Value;
        var repositoryRoot = FindRepositoryRoot();
        var entries = catalog.InformationEntries.Where(item => item.CountryId == SwedenId).ToArray();
        var languageMap = new Dictionary<string, string> { ["en"] = "EN", ["uk"] = "UK", ["ru"] = "RU" };
        var draftFiles = Directory.GetFiles(Path.Combine(repositoryRoot, "docs", "content-drafts", "sweden-v1"), "SE-*.md");
        Assert.Equal(8, draftFiles.Length);

        foreach (var (key, _) in DraftFolders)
        {
            var draft = await File.ReadAllTextAsync(Path.Combine(repositoryRoot, "docs", "content-drafts", "sweden-v1", key + ".md"));
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
                Assert.DoesNotContain("Editorial exclusions", actual.Content, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("Editorial source set", actual.Content, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("research ID", actual.Content, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotMatch(@"\bSE-(?:TP|EMP|HEALTH|HOUSING|EDU|SOC)-\d{2}-\d{2}\b", actual.Content);
                Assert.DoesNotContain("https://", actual.Content, StringComparison.OrdinalIgnoreCase);
            }
        }
    }

    [Fact]
    public async Task SwedenSourcesAreUniqueOwnedValidAndFreshlyChecked()
    {
        var catalog = await Catalog.Value;
        var repositoryRoot = FindRepositoryRoot();
        var entries = catalog.InformationEntries.Where(item => item.CountryId == SwedenId).ToArray();
        var entryIds = entries.Select(item => item.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(item => entryIds.Contains(item.InformationEntryId)).ToArray();
        Assert.Equal(68, sources.Length);
        Assert.Equal(catalog.OfficialSources.Count, catalog.OfficialSources.Select(item => item.Id).Distinct().Count());
        Assert.Equal(60, sources.Select(item => item.Url.AbsoluteUri).Distinct(StringComparer.OrdinalIgnoreCase).Count());
        Assert.All(sources, source =>
        {
            Assert.Contains(source.InformationEntryId, entryIds);
            Assert.True(source.Url.IsAbsoluteUri);
            Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme);
            Assert.False(string.IsNullOrWhiteSpace(source.Title));
            Assert.Equal(SourceCheckTimestamp, source.LastCheckedAt);
        });

        foreach (var (key, expectedCount) in ExpectedSourceCounts)
        {
            var entryId = StableArticleIds[key];
            var ownedSources = sources.Where(item => item.InformationEntryId == entryId).ToArray();
            Assert.Equal(expectedCount, ownedSources.Length);
            var draft = await File.ReadAllTextAsync(Path.Combine(repositoryRoot, "docs", "content-drafts", "sweden-v1", key + ".md"));
            var allowedUrls = ReadEditorialUrls(repositoryRoot, key, draft);
            Assert.Equal(expectedCount, allowedUrls.Count);
            Assert.All(ownedSources, source => Assert.Contains(source.Url.AbsoluteUri, allowedUrls));
        }
    }

    [Theory]
    [InlineData("en", "Sweden", "Sweden")]
    [InlineData("uk", "Швеція", "у Швеції")]
    [InlineData("ru", "Швеция", "в Швеции")]
    public async Task SwedenLocalizationAndHomeCardRenderNaturally(string language, string name, string locationName)
    {
        var culture = CultureInfo.GetCultureInfo(language);
        Assert.Equal(name, Resources.GetString("Country.Sweden", culture));
        Assert.Equal(locationName, Resources.GetString("Country.Sweden.In", culture));
        var html = WebUtility.HtmlDecode(await RenderAsync<Home>(language));
        Assert.Contains(name, html);
        Assert.Contains(locationName, html);
        Assert.Contains("🇸🇪", html);
        Assert.Contains("href=\"/countries/sweden\"", html);
        Assert.DoesNotContain("Country.Sweden", html, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("en", "Sweden")]
    [InlineData("uk", "Швеція")]
    [InlineData("ru", "Швеция")]
    public async Task SwedenOverviewHasSixCategoriesAndNoGuide(string language, string countryName)
    {
        var html = WebUtility.HtmlDecode(await RenderAsync<CountryOverview>(language,
            new Dictionary<string, object?> { [nameof(CountryOverview.CountrySlug)] = "sweden" },
            "http://localhost/countries/sweden"));
        Assert.Contains(countryName, html);
        Assert.Equal(6, html.Split("href=\"/countries/sweden/categories/", StringSplitOptions.None).Length - 1);
        Assert.DoesNotContain("country-guide-callout", html, StringComparison.Ordinal);
        Assert.DoesNotContain("Country.Sweden", html, StringComparison.Ordinal);
        foreach (var category in Categories) Assert.Contains($"/countries/sweden/categories/{category}", html);
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task AllSixSwedenCategoryPagesRenderTheirNeedsReviewArticles(string language)
    {
        var catalog = await Catalog.Value;
        var entries = catalog.InformationEntries.Where(item => item.CountryId == SwedenId).ToArray();
        foreach (var category in Categories)
        {
            var html = WebUtility.HtmlDecode(await RenderAsync<Category>(language,
                new Dictionary<string, object?>
                {
                    [nameof(Category.CountrySlug)] = "sweden",
                    [nameof(Category.CategorySlug)] = category
                }, $"http://localhost/countries/sweden/categories/{category}"));
            var categoryId = CategoryIds[category];
            var matching = entries.Where(entry => entry.CategoryId == categoryId).ToArray();
            Assert.Equal(category == "temporary-protection" || category == "employment" ? 2 : 1, matching.Length);
            foreach (var entry in matching)
            {
                var translation = Assert.Single(catalog.InformationEntryTranslations,
                    item => item.InformationEntryId == entry.Id && item.LanguageCode == language);
                Assert.Equal(VerificationStatus.NeedsReview, translation.VerificationStatus);
                Assert.Contains(translation.Title, html);
                Assert.Contains(translation.Summary, html);
            }
            Assert.Contains(Resources.GetString($"Category.{ToCategoryKey(category)}", CultureInfo.GetCultureInfo(language))!, html);
            Assert.DoesNotContain("Country.Sweden", html, StringComparison.Ordinal);
            Assert.DoesNotContain("Category.", html, StringComparison.Ordinal);
        }
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task SearchOffersSwedenFilterButDoesNotExposeNeedsReviewArticles(string language)
    {
        var catalog = await Catalog.Value;
        var entries = catalog.InformationEntries.Where(item => item.CountryId == SwedenId).ToArray();
        var noResults = Resources.GetString("Search.NoResults", CultureInfo.GetCultureInfo(language))!;
        var html = WebUtility.HtmlDecode(await RenderAsync<Search>(language, uri: "http://localhost/search?country=sweden"));
        Assert.Contains("value=\"sweden\"", html);
        Assert.Contains(noResults, html);
        foreach (var entry in entries)
        {
            var translation = Assert.Single(catalog.InformationEntryTranslations,
                item => item.InformationEntryId == entry.Id && item.LanguageCode == language);
            Assert.DoesNotContain(translation.Title, html);
        }
    }

    [Fact]
    public async Task CountryComparisonMapsSwedenToAllEightNeedsReviewArticles()
    {
        var catalog = await Catalog.Value;
        var facts = await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json"))
            .LoadAsync(catalog);
        CountryComparisonTopicMap.Validate(catalog);
        Assert.Contains("sweden", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Equal(7, facts.Topics.SelectMany(topic => topic.Aspects)
            .SelectMany(aspect => aspect.Facts).Select(fact => fact.CountrySlug).Distinct(StringComparer.Ordinal).Count());

        foreach (var topic in CountryComparisonTopicMap.All)
        {
            var entryId = CountryComparisonTopicMap.EntryIdFor(topic, "sweden");
            Assert.Contains(StableArticleIds.Values, expected => expected == entryId);
            var entry = Assert.Single(catalog.InformationEntries, item => item.Id == entryId);
            Assert.Equal(SwedenId, entry.CountryId);
            var translations = catalog.InformationEntryTranslations.Where(item => item.InformationEntryId == entryId).ToArray();
            Assert.Equal(3, translations.Length);
            Assert.All(translations, translation => Assert.Equal(VerificationStatus.NeedsReview, translation.VerificationStatus));
        }

        foreach (var (language, name) in new[] { ("en", "Sweden"), ("uk", "Швеція"), ("ru", "Швеция") })
        {
            var html = WebUtility.HtmlDecode(await RenderAsync<Compare>(language, uri: "http://localhost/compare"));
            Assert.Contains("value=\"sweden\"", html);
            Assert.Contains(name, html);
            Assert.DoesNotContain("Country.Sweden", html, StringComparison.Ordinal);
        }
    }

    private static string ToCategoryKey(string slug) => slug switch
    {
        "temporary-protection" => "TemporaryProtection",
        "employment" => "Employment",
        "healthcare" => "Healthcare",
        "housing" => "Housing",
        "education" => "Education",
        "social-assistance" => "SocialAssistance",
        _ => throw new InvalidOperationException($"Unknown category {slug}.")
    };

    private static HashSet<string> ReadEditorialUrls(string root, string articleKey, string draft)
    {
        var folder = DraftFolders[articleKey];
        var matrixPath = Path.Combine(root, "docs", "content-research", "sweden-batch-2026-10-20", folder, "SOURCES_AND_CLAIMS.md");
        var matrix = File.ReadAllText(matrixPath).Split("## Official source inventory", 2, StringSplitOptions.None)[1];
        var editorialIds = Regex.Matches(draft.Split("## Editorial source set", 2, StringSplitOptions.None)[1], @"\b[A-Z]+-S\d+\b")
            .Cast<Match>().Select(match => match.Value).ToHashSet(StringComparer.Ordinal);
        var urls = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var id in editorialIds)
        {
            var match = Regex.Match(matrix,
                @"(?ms)^###\s+" + Regex.Escape(id) + @"\b.*?(?=^###\s+|\z)");
            Assert.True(match.Success, $"Editorial source {id} is missing from {matrixPath}.");
            var url = Regex.Match(match.Value, @"^- URL:\s*(\S+)", RegexOptions.Multiline);
            Assert.True(url.Success, $"Editorial source {id} has no URL.");
            urls.Add(new Uri(url.Groups[1].Value.Trim()).AbsoluteUri);
        }
        return urls;
    }

    private static string FindRepositoryRoot()
    {
        for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "docs", "content-drafts", "sweden-v1", "SE-TP-01.md"))) return directory.FullName;
        }
        throw new DirectoryNotFoundException("Could not locate the Sweden editorial drafts.");
    }

    private static (string Title, string Summary, string Content) ParseDraft(string markdown, string language)
    {
        var match = Regex.Match(markdown,
            $"^## {language}\\s*\\n(.*?)(?=^## (?:EN|UK|RU)\\s*$|\\z)", RegexOptions.Multiline | RegexOptions.Singleline);
        Assert.True(match.Success, $"Draft section {language} is missing.");
        var section = match.Groups[1].Value.Split("### Claim coverage", 2, StringSplitOptions.None)[0];
        string Field(string name) => Regex.Match(section, $"^{name}:\\s*\\n([^\\r\\n]+)", RegexOptions.Multiline).Groups[1].Value.Trim();
        var content = Regex.Match(section, "^Content:\\s*\\n(.*)$", RegexOptions.Multiline | RegexOptions.Singleline);
        Assert.True(content.Success, $"Content field for {language} is missing.");
        return (Field("Title"), Field("Summary"), content.Groups[1].Value.Trim());
    }

    private static string NormalizeLines(string value) => value.Replace("\r\n", "\n").Trim();

    private static async Task<string> RenderAsync<TComponent>(string language,
        IDictionary<string, object?>? parameters = null, string uri = "http://localhost/") where TComponent : IComponent
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
