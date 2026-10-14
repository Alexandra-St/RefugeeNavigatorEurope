using System.Globalization;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Search;

namespace RefugeeNavigator.Tests;

public sealed class ArticleSearchTests
{
    private static readonly Guid DenmarkId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly Guid GermanyId = Guid.Parse("10000000-0000-0000-0000-000000000002");
    private static readonly Guid EmploymentId = Guid.Parse("20000000-0000-0000-0000-000000000001");
    private static readonly Guid HealthcareId = Guid.Parse("20000000-0000-0000-0000-000000000002");
    private static readonly Guid FirstEntryId = Guid.Parse("30000000-0000-0000-0000-000000000001");
    private static readonly Guid SecondEntryId = Guid.Parse("30000000-0000-0000-0000-000000000002");
    private static readonly DateTimeOffset UpdatedAt = new(2026, 10, 13, 12, 0, 0, TimeSpan.Zero);

    [Theory]
    [InlineData("en", "employment", "job", "EMPLOYERS", "employ")]
    [InlineData("uk", "працевлаштування", "робота", "РОБОТОДАВЦІ", "ваканс")]
    [InlineData("ru", "трудоустройство", "работа", "РАБОТОДАТЕЛИ", "ваканс")]
    public void SearchesOnlyTheCurrentLanguageTitleSummaryAndContent(
        string language,
        string titleQuery,
        string summaryQuery,
        string contentQuery,
        string partialQuery)
    {
        var catalog = CreateCatalog();

        Assert.Single(ArticleSearch.Search(catalog, titleQuery.ToUpperInvariant(), null, null, null, language));
        Assert.Single(ArticleSearch.Search(catalog, summaryQuery.ToUpperInvariant(), null, null, null, language));
        Assert.Single(ArticleSearch.Search(catalog, contentQuery.ToUpperInvariant(), null, null, null, language));
        Assert.Single(ArticleSearch.Search(catalog, partialQuery, null, null, null, language));

        var otherLanguageQuery = language switch
        {
            "en" => "ваканс",
            "uk" => "vacanc",
            _ => "vacanc"
        };
        Assert.Empty(ArticleSearch.Search(catalog, otherLanguageQuery, null, null, null, language));
        Assert.Equal(2, ArticleSearch.Search(catalog, "  ", null, null, null, language).Count);
    }

    [Fact]
    public void AppliesCountryCategoryAndTranslationStatusTogetherAndKeepsCatalogOrder()
    {
        var catalog = CreateCatalog();

        var all = ArticleSearch.Search(catalog, null, null, null, null, "en");
        Assert.Equal(new[] { FirstEntryId, SecondEntryId }, all.Select(result => result.Entry.Id));

        var filtered = ArticleSearch.Search(catalog, "job", "DENMARK", "employment", "verified", "en");
        var result = Assert.Single(filtered);
        Assert.Equal(FirstEntryId, result.Entry.Id);
        Assert.Equal("denmark", result.Country.Slug);
        Assert.Equal("employment", result.Category.Slug);
        Assert.Equal(VerificationStatus.Verified, result.Translation.VerificationStatus);

        Assert.Empty(ArticleSearch.Search(catalog, null, "unknown", null, null, "en"));
        Assert.Empty(ArticleSearch.Search(catalog, null, null, null, "unknown", "en"));
    }

    [Fact]
    public async Task SearchPageRendersEscapedResultsPreservedQueryFiltersResetAndExistingRoute()
    {
        var html = await RenderAsync(CreateCatalog(), "en", "?q=%20job%20&country=denmark&category=employment&status=Verified");

        Assert.Contains("value=\" job \"", html);
        Assert.Contains("selected", html);
        Assert.Contains("href=\"/search\"", html);
        Assert.Contains("href=\"/countries/denmark/categories/employment\"", html);
        Assert.Contains("Job &lt;guide&gt;", html);
        Assert.Contains("&amp; practical", html);
        Assert.DoesNotContain("<guide>", html);
        Assert.DoesNotContain("Another article", WebUtility.HtmlDecode(html));
        Assert.Contains("Verified", WebUtility.HtmlDecode(html));
    }

    [Fact]
    public async Task EmptyCatalogAndNoMatchesHaveDistinctLocalizedStates()
    {
        var emptyHtml = WebUtility.HtmlDecode(await RenderAsync(CreateCatalog(includeEntries: false), "en"));
        Assert.Contains("No articles are currently available.", emptyHtml);

        var noMatchesHtml = WebUtility.HtmlDecode(await RenderAsync(
            CreateCatalog(),
            "en",
            "?q=nothing-matches"));
        Assert.Contains("No articles match these search criteria.", noMatchesHtml);
    }

    private static CatalogData CreateCatalog(bool includeEntries = true)
    {
        var countries = new[]
        {
            new Country(DenmarkId, "Denmark", "denmark"),
            new Country(GermanyId, "Germany", "germany")
        };
        var categories = new[]
        {
            new InformationCategory(EmploymentId, "Employment", "employment"),
            new InformationCategory(HealthcareId, "Healthcare", "healthcare")
        };
        var entries = includeEntries
            ? new[]
            {
                new InformationEntry(FirstEntryId, DenmarkId, EmploymentId),
                new InformationEntry(SecondEntryId, GermanyId, HealthcareId)
            }
            : Array.Empty<InformationEntry>();
        var translations = includeEntries
            ? new[]
            {
                Translation(FirstEntryId, "en", "Employment guide", "Job <guide> & practical", "Employers share job opportunities.", VerificationStatus.Verified),
                Translation(FirstEntryId, "uk", "Працевлаштування", "Робота", "Роботодавці публікують вакансії.", VerificationStatus.Verified),
                Translation(FirstEntryId, "ru", "Трудоустройство", "Работа", "Работодатели публикуют вакансии.", VerificationStatus.Verified),
                Translation(SecondEntryId, "en", "Another article", "Medical help", "Find a clinic.", VerificationStatus.NeedsReview),
                Translation(SecondEntryId, "uk", "Інша стаття", "Медична допомога", "Знайдіть клініку.", VerificationStatus.NeedsReview),
                Translation(SecondEntryId, "ru", "Другая статья", "Медицинская помощь", "Найдите клинику.", VerificationStatus.NeedsReview)
            }
            : Array.Empty<InformationEntryTranslation>();

        return (CatalogData)Activator.CreateInstance(
            typeof(CatalogData),
            BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            args: [countries, categories, entries, translations, Array.Empty<OfficialSource>()],
            culture: CultureInfo.InvariantCulture)!;
    }

    private static InformationEntryTranslation Translation(
        Guid entryId,
        string language,
        string title,
        string summary,
        string content,
        VerificationStatus status) =>
        new(entryId, language, title, summary, content, status, UpdatedAt);

    private static async Task<string> RenderAsync(
        CatalogData catalog,
        string language,
        string queryString = "")
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
            services.AddSingleton(catalog);
            services.AddSingleton<NavigationManager>(new TestNavigationManager($"http://localhost/search{queryString}"));
            using var provider = services.BuildServiceProvider();
            await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());

            return await renderer.Dispatcher.InvokeAsync(async () =>
            {
                var component = await renderer.RenderComponentAsync<Search>(ParameterView.Empty);
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
        public TestNavigationManager(string uri)
        {
            Initialize("http://localhost/", uri);
        }

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
        }

    }
}
