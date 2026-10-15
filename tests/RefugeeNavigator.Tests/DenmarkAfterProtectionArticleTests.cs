using System.Globalization;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Components;
using RefugeeNavigator.Web.Components.Pages;

namespace RefugeeNavigator.Tests;

public sealed class DenmarkAfterProtectionArticleTests
{
    private static readonly Guid DenmarkId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly Guid TemporaryProtectionId = Guid.Parse("20000000-0000-0000-0000-000000000001");
    private static readonly Guid ArticleId = Guid.Parse("2d106fd3-e981-4ef9-9733-568b654dfca5");
    private static readonly Guid ExistingEmploymentEntryId = Guid.Parse("c2356631-3fa2-4553-8fda-50a4419262c0");
    private static readonly string[] RequiredSources =
    [
        "https://www.retsinformation.dk/eli/lta/2026/828",
        "https://nyidanmark.dk/en-GB/Words-and-concepts/F%C3%A6lles/Information-to-Ukrainian-citizens",
        "https://nyidanmark.dk/en-GB/You-want-to-apply/Work/Labout-Market-Attachment",
        "https://nyidanmark.dk/en-GB/You-want-to-apply/Study/Educational-attachment-and-change-of-education/Educational-Attachment",
        "https://nyidanmark.dk/en-GB/You-want-to-apply/Work/Pay-limit-scheme",
        "https://nyidanmark.dk/en-GB/You-want-to-apply/Study/Higher-Education"
    ];

    [Fact]
    public async Task CatalogContainsVerifiedArticleAndPreservesExistingEmploymentEntry()
    {
        var catalog = await LoadProductionCatalogAsync();
        var article = Assert.Single(catalog.InformationEntries, entry => entry.Id == ArticleId);
        Assert.Equal(DenmarkId, article.CountryId);
        Assert.Equal(TemporaryProtectionId, article.CategoryId);

        var translations = catalog.InformationEntryTranslations
            .Where(translation => translation.InformationEntryId == ArticleId)
            .ToArray();
        Assert.Equal(new[] { "en", "ru", "uk" }, translations.Select(translation => translation.LanguageCode).OrderBy(code => code));
        Assert.All(translations, translation =>
        {
            Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
            Assert.Equal(new DateOnly(2026, 10, 15), DateOnly.FromDateTime(translation.LastUpdatedAt.UtcDateTime));
        });

        Assert.Contains(catalog.InformationEntries, entry =>
            entry.Id == ExistingEmploymentEntryId && entry.CountryId == DenmarkId &&
            entry.CategoryId == Guid.Parse("20000000-0000-0000-0000-000000000002"));

        var sources = catalog.OfficialSources.Where(source => source.InformationEntryId == ArticleId).ToArray();
        Assert.Equal(RequiredSources.OrderBy(url => url), sources.Select(source => source.Url.AbsoluteUri).OrderBy(url => url));
        Assert.All(sources, source =>
            Assert.Equal(new DateOnly(2026, 10, 15), DateOnly.FromDateTime(source.LastCheckedAt.UtcDateTime)));
    }

    [Theory]
    [InlineData("uk", "Які є варіанти проживання після тимчасового захисту в Данії?", "BEK nr. 828/2026", "17 березня 2028 року", "17 березня 2027 року", "Перевірено", "Джерело востаннє перевірено")]
    [InlineData("en", "Residence options after temporary protection in Denmark", "BEK no. 828/2026", "17 March 2028", "17 March 2027", "Reviewed", "Source last checked")]
    [InlineData("ru", "Какие есть варианты проживания после временной защиты в Дании?", "BEK № 828/2026", "17 марта 2028 года", "17 марта 2027 года", "Проверено", "Источник проверен")]
    public async Task LocalizedCategoryRendersArticleStatusDatesAndOfficialSources(
        string language,
        string expectedTitle,
        string expectedRegulation,
        string expectedExtensionDate,
        string expectedAgencyDate,
        string expectedStatus,
        string expectedSourceLabel)
    {
        var catalog = await LoadProductionCatalogAsync();
        var html = await RenderAsync<Category>(catalog, language,
            "/countries/denmark/categories/temporary-protection",
            new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = "denmark",
                [nameof(Category.CategorySlug)] = "temporary-protection"
            });
        var visibleHtml = WebUtility.HtmlDecode(html);

        Assert.Contains(expectedTitle, visibleHtml);
        Assert.Contains(expectedRegulation, visibleHtml);
        Assert.Contains(expectedExtensionDate, visibleHtml);
        Assert.Contains(expectedAgencyDate, visibleHtml);
        Assert.Contains(expectedStatus, visibleHtml);
        Assert.Equal(RequiredSources.Length, RequiredSources.Count(url => html.Contains($"href=\"{url}\"", StringComparison.Ordinal)));
        Assert.Contains(expectedSourceLabel, visibleHtml);
        Assert.DoesNotContain("2d106fd3-e981-4ef9-9733-568b654dfca5", visibleHtml);
    }

    [Fact]
    public async Task SearchAndComparisonExposeTheArticleThroughExistingRoutes()
    {
        var catalog = await LoadProductionCatalogAsync();
        var searchHtml = await RenderAsync<Search>(catalog, "en",
            "/search?q=Residence%20options%20after%20temporary%20protection&country=denmark&category=temporary-protection");
        Assert.Contains("href=\"/countries/denmark/categories/temporary-protection\"", searchHtml);
        Assert.Contains("Residence options after temporary protection in Denmark", WebUtility.HtmlDecode(searchHtml));

        var compareHtml = await RenderAsync<Compare>(catalog, "en",
            "/compare?country1=denmark&country2=germany&category=temporary-protection");
        Assert.Contains("Residence options after temporary protection in Denmark", WebUtility.HtmlDecode(compareHtml));
        Assert.Contains("href=\"/countries/denmark/categories/temporary-protection\"", compareHtml);
    }

    private static Task<CatalogData> LoadProductionCatalogAsync() =>
        new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();

    private static async Task<string> RenderAsync<TComponent>(
        CatalogData catalog,
        string language,
        string currentPath,
        IDictionary<string, object?>? parameters = null)
        where TComponent : IComponent
    {
        var previousCulture = CultureInfo.CurrentCulture;
        var previousUiCulture = CultureInfo.CurrentUICulture;
        var culture = CultureInfo.GetCultureInfo(language);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        try
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton(catalog);
            services.AddSingleton<NavigationManager>(new TestNavigationManager(currentPath));
            using var provider = services.BuildServiceProvider();
            await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());

            return await renderer.Dispatcher.InvokeAsync(async () =>
            {
                var parameterView = parameters is null ? ParameterView.Empty : ParameterView.FromDictionary(parameters);
                var component = await renderer.RenderComponentAsync<TComponent>(parameterView);
                return component.ToHtmlString();
            });
        }
        finally
        {
            CultureInfo.CurrentCulture = previousCulture;
            CultureInfo.CurrentUICulture = previousUiCulture;
        }
    }

    private sealed class TestNavigationManager : NavigationManager
    {
        public TestNavigationManager(string currentPath) => Initialize("http://localhost/", $"http://localhost{currentPath}");
        protected override void NavigateToCore(string uri, bool forceLoad) { }
    }
}
