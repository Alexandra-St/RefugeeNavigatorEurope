using System.Globalization;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Guides;

namespace RefugeeNavigator.Tests;

public sealed class CategoryRenderingTests
{
    private static readonly ResourceManager Resources = new(
        "RefugeeNavigator.Web.Resources.Localization.SharedResource",
        typeof(RefugeeNavigator.Web.Localization.SharedResource).Assembly);
    private static readonly Guid CountryId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly Guid OtherCountryId = Guid.Parse("10000000-0000-0000-0000-000000000002");
    private static readonly Guid CategoryId = Guid.Parse("20000000-0000-0000-0000-000000000001");
    private static readonly Guid OtherCategoryId = Guid.Parse("20000000-0000-0000-0000-000000000002");
    private static readonly DateTimeOffset UpdatedAt = new(2024, 2, 3, 4, 5, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset CheckedAt = new(2024, 4, 5, 6, 7, 0, TimeSpan.Zero);
    private static readonly Lazy<Task<CatalogData>> ProductionCatalog = new(
        () => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());
    private static readonly Lazy<Task<StepByStepGuideCatalog>> ProductionGuides = new(async () =>
    {
        var catalog = await ProductionCatalog.Value;
        return await new JsonStepByStepGuideProvider(Path.Combine(AppContext.BaseDirectory, "Data", "step-by-step-guides.json")).LoadAsync(catalog);
    });

    public static IEnumerable<object[]> ContentParagraphCases() =>
    [
        ["Single paragraph.", new[] { "Single paragraph." }],
        ["First line.\nSecond line remains the same paragraph.", new[] { "First line.\nSecond line remains the same paragraph." }],
        ["First paragraph.\n\nSecond paragraph.", new[] { "First paragraph.", "Second paragraph." }],
        ["First paragraph.\r\n\r\nSecond paragraph.", new[] { "First paragraph.", "Second paragraph." }],
        ["First paragraph.\n\n\n\nSecond paragraph.", new[] { "First paragraph.", "Second paragraph." }],
        ["First paragraph.\n \t \nSecond paragraph.", new[] { "First paragraph.", "Second paragraph." }],
        ["\n\nFirst paragraph.\n\nSecond paragraph.\n\n", new[] { "First paragraph.", "Second paragraph." }],
        ["\r\n \t\r\nFirst paragraph.\r\n\r\nSecond paragraph.\r\n  \r\n", new[] { "First paragraph.", "Second paragraph." }]
    ];

    [Fact]
    public async Task EmptyCategoryRendersExistingPlaceholder()
    {
        var catalog = await CreateCatalogAsync(Array.Empty<EntryFixture>());
        var html = await RenderAsync(catalog, "en");
        var visibleHtml = WebUtility.HtmlDecode(html);

        Assert.Contains("Information coming soon", visibleHtml);
        Assert.Contains("Detailed information for this category will be added after verification against official sources.", visibleHtml);
        Assert.DoesNotContain("class=\"information-entries\"", html);
        Assert.DoesNotContain("class=\"guide-callout\"", html);
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task ApprovedGermanyGuideCalloutIsLocalizedOnTemporaryProtectionCategory(string language)
    {
        var catalog = await ProductionCatalog.Value;
        var guides = await ProductionGuides.Value;
        var html = WebUtility.HtmlDecode(await RenderAsync(catalog, language, "germany", "temporary-protection", guides, "Production"));
        var culture = CultureInfo.GetCultureInfo(language);

        Assert.Contains(Resources.GetString("Category.GuideCalloutTitle", culture)!, html);
        Assert.Contains(Resources.GetString("Category.GuideCalloutDescription", culture)!, html);
        Assert.Contains(Resources.GetString("Category.GuideCalloutLink", culture)!, html);
        Assert.Contains("href=\"/guides/germany/temporary-protection\"", html);
        Assert.Contains("class=\"guide-callout\"", html);
    }

    [Fact]
    public async Task GuideCalloutOnlyAppearsForMatchingPublishableGuide()
    {
        var catalog = await ProductionCatalog.Value;
        var guides = await ProductionGuides.Value;
        var polandGuide = guides.Guides.Single(guide => guide.Id == "poland-temporary-protection-v1");
        var draftPolandGuides = guides with
        {
            Guides = guides.Guides.Select(guide => guide.Id == polandGuide.Id
                ? guide with { EditorialStatus = GuideEditorialStatus.Draft }
                : guide).ToArray()
        };

        var denmark = await RenderAsync(catalog, "en", "denmark", "temporary-protection", guides, "Production");
        var denmarkDevelopment = WebUtility.HtmlDecode(await RenderAsync(catalog, "en", "denmark", "temporary-protection", guides, "Development"));
        var poland = WebUtility.HtmlDecode(await RenderAsync(catalog, "en", "poland", "temporary-protection", guides, "Production"));
        var polandDevelopment = WebUtility.HtmlDecode(await RenderAsync(catalog, "en", "poland", "temporary-protection", guides, "Development"));
        var polandDraftProduction = await RenderAsync(catalog, "en", "poland", "temporary-protection", draftPolandGuides, "Production");
        var polandDraftDevelopment = WebUtility.HtmlDecode(await RenderAsync(catalog, "en", "poland", "temporary-protection", draftPolandGuides, "Development"));
        var germanyHealthcare = await RenderAsync(catalog, "en", "germany", "healthcare", guides, "Production");
        var denmarkHealthcare = await RenderAsync(catalog, "en", "denmark", "healthcare", guides, "Production");
        var polandHealthcare = await RenderAsync(catalog, "en", "poland", "healthcare", guides, "Production");
        var germanyEmployment = await RenderAsync(catalog, "en", "germany", "employment", guides, "Production");

        Assert.Contains("class=\"guide-callout\"", denmark);
        Assert.Contains("href=\"/guides/denmark/temporary-protection\"", denmark);
        Assert.Contains("class=\"guide-callout\"", denmarkDevelopment);
        Assert.Contains("href=\"/guides/denmark/temporary-protection\"", denmarkDevelopment);
        Assert.Contains("class=\"guide-callout\"", poland);
        Assert.Contains("href=\"/guides/poland/temporary-protection\"", poland);
        Assert.Contains("class=\"guide-callout\"", polandDevelopment);
        Assert.DoesNotContain("class=\"guide-callout\"", germanyHealthcare);
        Assert.DoesNotContain("class=\"guide-callout\"", denmarkHealthcare);
        Assert.DoesNotContain("class=\"guide-callout\"", polandHealthcare);
        Assert.DoesNotContain("class=\"guide-callout\"", germanyEmployment);
        Assert.DoesNotContain("class=\"guide-callout\"", polandDraftProduction);
        Assert.Contains("class=\"guide-callout\"", polandDraftDevelopment);
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task PolandApprovedGuideCalloutIsLocalizedInProductionAndDevelopment(string language)
    {
        var catalog = await ProductionCatalog.Value;
        var guides = await ProductionGuides.Value;
        var production = WebUtility.HtmlDecode(await RenderAsync(catalog, language, "poland", "temporary-protection", guides, "Production"));
        var development = WebUtility.HtmlDecode(await RenderAsync(catalog, language, "poland", "temporary-protection", guides, "Development"));
        var culture = CultureInfo.GetCultureInfo(language);

        Assert.Contains("class=\"guide-callout\"", production);
        Assert.Contains("class=\"guide-callout\"", development);
        Assert.Contains("href=\"/guides/poland/temporary-protection\"", production);
        Assert.Contains("href=\"/guides/poland/temporary-protection\"", development);
        Assert.Contains(Resources.GetString("Category.GuideCalloutTitle", culture)!, production);
        Assert.Contains(Resources.GetString("Category.GuideCalloutDescription", culture)!, production);
        Assert.Contains(Resources.GetString("Category.GuideCalloutLink", culture)!, production);
        Assert.Contains(Resources.GetString("Category.GuideCalloutTitle", culture)!, development);
        Assert.Contains(Resources.GetString("Category.GuideCalloutDescription", culture)!, development);
        Assert.Contains(Resources.GetString("Category.GuideCalloutLink", culture)!, development);
    }

    [Theory]
    [InlineData("uk", "Title uk", "Потребує перевірки")]
    [InlineData("en", "Title en", "Needs review")]
    [InlineData("ru", "Title ru", "Требует проверки")]
    public async Task RendersTranslationAndStatusForCurrentUiCulture(string language, string title, string status)
    {
        var entry = FixtureEntry(Guid.NewGuid());
        var catalog = await CreateCatalogAsync(new[] { entry });

        var html = await RenderAsync(catalog, language);
        var visibleHtml = WebUtility.HtmlDecode(html);

        Assert.Contains(title, visibleHtml);
        Assert.Contains(status, visibleHtml);
        Assert.Contains($"Summary {language}", visibleHtml);
        Assert.Contains($"Body {language}", visibleHtml);
    }

    [Fact]
    public async Task RendersOnlyMatchingCountryAndCategoryEntriesInCatalogOrder()
    {
        var first = FixtureEntry(Guid.Parse("30000000-0000-0000-0000-000000000001"), title: "First item");
        var otherCountry = FixtureEntry(Guid.Parse("30000000-0000-0000-0000-000000000002"), countryId: OtherCountryId, title: "Other country item");
        var otherCategory = FixtureEntry(Guid.Parse("30000000-0000-0000-0000-000000000003"), categoryId: OtherCategoryId, title: "Other category item");
        var second = FixtureEntry(Guid.Parse("30000000-0000-0000-0000-000000000004"), title: "Second item");
        var catalog = await CreateCatalogAsync(new[] { first, otherCountry, otherCategory, second });

        var html = await RenderAsync(catalog, "en");
        var visibleHtml = WebUtility.HtmlDecode(html);

        Assert.True(visibleHtml.IndexOf("First item en", StringComparison.Ordinal) < visibleHtml.IndexOf("Second item en", StringComparison.Ordinal));
        Assert.DoesNotContain("Other country item", visibleHtml);
        Assert.DoesNotContain("Other category item", visibleHtml);
    }

    [Theory]
    [InlineData(VerificationStatus.NeedsReview, "en", "Needs review")]
    [InlineData(VerificationStatus.NeedsReview, "uk", "Потребує перевірки")]
    [InlineData(VerificationStatus.NeedsReview, "ru", "Требует проверки")]
    [InlineData(VerificationStatus.Verified, "en", "Reviewed")]
    [InlineData(VerificationStatus.Verified, "uk", "Перевірено")]
    [InlineData(VerificationStatus.Verified, "ru", "Проверено")]
    [InlineData(VerificationStatus.Outdated, "en", "May be outdated")]
    [InlineData(VerificationStatus.Outdated, "uk", "Може бути застарілою")]
    [InlineData(VerificationStatus.Outdated, "ru", "Возможно, устарело")]
    public async Task RendersEveryVerificationStatusInEveryLanguage(
        VerificationStatus status,
        string language,
        string expected)
    {
        var catalog = await CreateCatalogAsync(new[] { FixtureEntry(Guid.NewGuid(), status: status) });

        var html = await RenderAsync(catalog, language);

        Assert.Contains(expected, WebUtility.HtmlDecode(html));
    }

    [Fact]
    public async Task RendersTranslationAndSourceDatesSeparatelyForMultipleSources()
    {
        var entry = FixtureEntry(Guid.NewGuid(), content: "First paragraph.\n\nSecond paragraph.");
        var sources = new[]
        {
            FixtureSource(Guid.NewGuid(), entry.Entry.Id, "First official page", "https://example.gov/first"),
            FixtureSource(Guid.NewGuid(), entry.Entry.Id, "Second official page", "https://example.gov/second")
        };
        var catalog = await CreateCatalogAsync(new[] { entry }, sources);

        var html = await RenderAsync(catalog, "en");
        var visibleHtml = WebUtility.HtmlDecode(html);

        Assert.Contains("Translation last updated", visibleHtml);
        Assert.Contains("Source last checked", visibleHtml);
        Assert.Contains("datetime=\"2024-02-03T04:05:00", visibleHtml);
        Assert.Contains("datetime=\"2024-04-05T06:07:00", visibleHtml);
        Assert.Contains("First official page", visibleHtml);
        Assert.Contains("Second official page", visibleHtml);
        Assert.Contains("target=\"_blank\" rel=\"noopener noreferrer\"", visibleHtml);
        Assert.Equal(new[] { "First paragraph.", "Second paragraph." }, ReadContentParagraphs(html));
    }

    [Fact]
    public async Task EntryWithoutSourcesDoesNotRenderEmptySourcesSection()
    {
        var catalog = await CreateCatalogAsync(new[] { FixtureEntry(Guid.NewGuid()) });

        var html = await RenderAsync(catalog, "en");
        var visibleHtml = WebUtility.HtmlDecode(html);

        Assert.DoesNotContain("Supporting sources", visibleHtml);
        Assert.DoesNotContain("Source last checked", visibleHtml);
    }

    [Fact]
    public async Task RendersEntryContentAsEncodedPlainText()
    {
        var entry = FixtureEntry(Guid.NewGuid(), content: "<script>alert('x')</script>\n\n<img src=x onerror='alert(1)'>");
        var catalog = await CreateCatalogAsync(new[] { entry });

        var html = await RenderAsync(catalog, "en");
        var paragraphs = Regex.Matches(html, """<p class="entry-content"[^>]*>(.*?)</p>""", RegexOptions.Singleline)
            .Cast<Match>()
            .Select(match => match.Groups[1].Value)
            .ToArray();

        Assert.Equal(2, paragraphs.Length);
        Assert.Contains("&lt;script&gt;alert(&#x27;x&#x27;)&lt;/script&gt;", paragraphs[0]);
        Assert.Contains("&lt;img src=x onerror=&#x27;alert(1)&#x27;&gt;", paragraphs[1]);
        Assert.DoesNotContain("<script>", html);
        Assert.DoesNotContain("<img", html);
    }

    [Theory]
    [MemberData(nameof(ContentParagraphCases))]
    public async Task RendersPlainTextContentAsNonemptyParagraphsInOrder(string content, string[] expectedParagraphs)
    {
        var catalog = await CreateCatalogAsync(new[] { FixtureEntry(Guid.NewGuid(), content: content) });

        var html = await RenderAsync(catalog, "en");
        var paragraphs = ReadContentParagraphs(html);

        Assert.Equal(expectedParagraphs, paragraphs);
        Assert.All(paragraphs, paragraph => Assert.False(string.IsNullOrWhiteSpace(paragraph)));
    }

    [Fact]
    public async Task RendersStructuredBlocksInOrderWithSemanticHeadingAndListElements()
    {
        const string content = "Opening paragraph.\n\n## Section heading\n\nFirst body paragraph.\nContinued line.\n\n- First item\n- Second item\n\nClosing paragraph.";
        var catalog = await CreateCatalogAsync(new[] { FixtureEntry(Guid.NewGuid(), content: content) });

        var html = await RenderAsync(catalog, "en");
        var heading = Regex.Match(html, "<h3\\b[^>]*class=\"entry-section-heading\"[^>]*>Section heading</h3>");
        var list = Regex.Match(html, "<ul\\b[^>]*class=\"entry-list\"");

        Assert.Single(Regex.Matches(html, "<h1\\b").Cast<Match>());
        Assert.Single(Regex.Matches(html, "<h2\\b").Cast<Match>());
        Assert.Single(Regex.Matches(html, "<h3\\b").Cast<Match>());
        Assert.True(heading.Success);
        Assert.True(list.Success);
        Assert.True(html.IndexOf("Opening paragraph.", StringComparison.Ordinal) < heading.Index);
        Assert.True(heading.Index < html.IndexOf("First body paragraph.", StringComparison.Ordinal));
        Assert.True(html.IndexOf("Continued line.", StringComparison.Ordinal) < list.Index);
        Assert.True(list.Index < html.IndexOf("First item", StringComparison.Ordinal));
        Assert.True(html.IndexOf("Second item", StringComparison.Ordinal) < html.IndexOf("Closing paragraph.", StringComparison.Ordinal));
        Assert.Matches("<li\\b[^>]*>First item</li>", html);
        Assert.Matches("<li\\b[^>]*>Second item</li>", html);
        Assert.Equal(new[] { "Opening paragraph.", "First body paragraph.\nContinued line.", "Closing paragraph." }, ReadContentParagraphs(html));
    }

    [Fact]
    public async Task EncodesTextInHeadingsListsAndParagraphs()
    {
        const string content = "## <script>alert('heading')</script>\n\n- <img src=x onerror='list'>\n\n<p>paragraph</p>";
        var catalog = await CreateCatalogAsync(new[] { FixtureEntry(Guid.NewGuid(), content: content) });

        var html = await RenderAsync(catalog, "en");

        Assert.Contains("&lt;script&gt;alert(&#x27;heading&#x27;)&lt;/script&gt;", html);
        Assert.Contains("&lt;img src=x onerror=&#x27;list&#x27;&gt;", html);
        Assert.Contains("&lt;p&gt;paragraph&lt;/p&gt;", html);
        Assert.DoesNotContain("<script>", html);
        Assert.DoesNotContain("<img", html);
        Assert.DoesNotContain("<p>paragraph</p>", html);
    }

    [Fact]
    public async Task MalformedAndUnsupportedFormattingRemainsVisibleAsSafeParagraphText()
    {
        const string content = "### Unsupported heading\n\n## \n\n- \n\n- First item\nordinary text";
        var catalog = await CreateCatalogAsync(new[] { FixtureEntry(Guid.NewGuid(), content: content) });

        var html = await RenderAsync(catalog, "en");

        Assert.Equal(new[] { "### Unsupported heading", "##", "-", "- First item\nordinary text" }, ReadContentParagraphs(html));
        Assert.DoesNotContain("<ul class=\"entry-list\">", html);
        Assert.DoesNotContain("<h3 class=\"entry-section-heading\">", html);
    }

    [Fact]
    public async Task MissingCurrentTranslationFailsInsteadOfFallingBack()
    {
        var validCatalog = await CreateCatalogAsync(new[] { FixtureEntry(Guid.NewGuid()) });
        var incompleteCatalog = CreateCatalogWithoutLanguage(validCatalog, "en");

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => RenderAsync(incompleteCatalog, "en"));

        Assert.Contains("has no 'en' translation", exception.Message);
    }

    private static EntryFixture FixtureEntry(
        Guid id,
        Guid countryId = default,
        Guid categoryId = default,
        string title = "Title",
        string content = "Body",
        VerificationStatus status = VerificationStatus.NeedsReview) =>
        new(
            new InformationEntry(id, countryId == default ? CountryId : countryId, categoryId == default ? CategoryId : categoryId),
            title,
            content,
            status);

    private static OfficialSource FixtureSource(Guid id, Guid entryId, string title, string url) =>
        new(id, entryId, title, new Uri(url), CheckedAt);

    private static string[] ReadContentParagraphs(string html) => Regex.Matches(
            html,
            """<p class="entry-content"[^>]*>(.*?)</p>""",
            RegexOptions.Singleline)
        .Cast<Match>()
        .Select(match => WebUtility.HtmlDecode(match.Groups[1].Value))
        .ToArray();

    private static Task<CatalogData> CreateCatalogAsync(
        IReadOnlyList<EntryFixture> entries,
        IReadOnlyList<OfficialSource>? sources = null)
    {
        var documents = entries.Select(fixture => new
        {
            fixture.Entry.Id,
            fixture.Entry.CountryId,
            fixture.Entry.CategoryId,
            Translations = new[] { "uk", "en", "ru" }.Select(language =>
                new InformationEntryTranslation(
                    fixture.Entry.Id,
                    language,
                    $"{fixture.Title} {language}",
                    $"Summary {language}",
                    fixture.Content == "Body" ? $"Body {language}" : fixture.Content,
                    fixture.Status,
                    UpdatedAt)).ToArray()
        }).ToArray();

        return WithCatalogFilesAsync(
            JsonSerializer.Serialize(new[]
            {
                new Country(CountryId, "Denmark", "denmark"),
                new Country(OtherCountryId, "Germany", "germany")
            }),
            JsonSerializer.Serialize(new[]
            {
                new InformationCategory(CategoryId, "Employment", "employment"),
                new InformationCategory(OtherCategoryId, "Healthcare", "healthcare")
            }),
            JsonSerializer.Serialize(documents),
            JsonSerializer.Serialize(sources ?? Array.Empty<OfficialSource>()));
    }

    private static async Task<CatalogData> WithCatalogFilesAsync(
        string countriesJson,
        string categoriesJson,
        string entriesJson,
        string sourcesJson)
    {
        var contentRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        var dataPath = Path.Combine(contentRoot, "Data");
        Directory.CreateDirectory(dataPath);

        try
        {
            await File.WriteAllTextAsync(Path.Combine(dataPath, "countries.json"), countriesJson);
            await File.WriteAllTextAsync(Path.Combine(dataPath, "categories.json"), categoriesJson);
            await File.WriteAllTextAsync(Path.Combine(dataPath, "information-entries.json"), entriesJson);
            await File.WriteAllTextAsync(Path.Combine(dataPath, "official-sources.json"), sourcesJson);
            return await new JsonCatalogProvider(contentRoot).LoadAsync();
        }
        finally
        {
            Directory.Delete(contentRoot, recursive: true);
        }
    }

    private static CatalogData CreateCatalogWithoutLanguage(CatalogData catalog, string languageCode)
    {
        var translations = catalog.InformationEntryTranslations
            .Where(translation => translation.LanguageCode != languageCode)
            .ToArray();
        return (CatalogData)Activator.CreateInstance(
            typeof(CatalogData),
            BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            args:
            [
                catalog.Countries,
                catalog.Categories,
                catalog.InformationEntries,
                translations,
                catalog.OfficialSources
            ],
            culture: CultureInfo.InvariantCulture)!;
    }

    private static async Task<string> RenderAsync(
        CatalogData catalog,
        string language,
        string countrySlug = "denmark",
        string categorySlug = "employment",
        StepByStepGuideCatalog? guides = null,
        string environmentName = "Development")
    {
        var priorCulture = CultureInfo.CurrentCulture;
        var priorUiCulture = CultureInfo.CurrentUICulture;
        var culture = CultureInfo.GetCultureInfo(language);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        try
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton(catalog);
            services.AddSingleton(guides ?? new StepByStepGuideCatalog([]));
            services.AddSingleton<IWebHostEnvironment>(new TestWebHostEnvironment(environmentName));
            services.AddSingleton<NavigationManager, TestNavigationManager>();
            using var serviceProvider = services.BuildServiceProvider();
            await using var renderer = new HtmlRenderer(serviceProvider, serviceProvider.GetRequiredService<ILoggerFactory>());

            return await renderer.Dispatcher.InvokeAsync(async () =>
            {
                var parameters = ParameterView.FromDictionary(new Dictionary<string, object?>
                {
                    [nameof(Category.CountrySlug)] = countrySlug,
                    [nameof(Category.CategorySlug)] = categorySlug
                });
                var component = await renderer.RenderComponentAsync<Category>(parameters);
                return component.ToHtmlString();
            });
        }
        finally
        {
            CultureInfo.CurrentCulture = priorCulture;
            CultureInfo.CurrentUICulture = priorUiCulture;
        }
    }

    private sealed record EntryFixture(
        InformationEntry Entry,
        string Title,
        string Content,
        VerificationStatus Status);

    private sealed class TestNavigationManager : NavigationManager
    {
        public TestNavigationManager()
        {
            Initialize("http://localhost/", "http://localhost/");
        }

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
        }
    }

    private sealed class TestWebHostEnvironment(string environmentName) : IWebHostEnvironment
    {
        public string ApplicationName { get; set; } = "RefugeeNavigator.Tests";
        public string EnvironmentName { get; set; } = environmentName;
        public string WebRootPath { get; set; } = AppContext.BaseDirectory;
        public string ContentRootPath { get; set; } = AppContext.BaseDirectory;
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }
}
