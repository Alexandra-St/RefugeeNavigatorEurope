using System.Globalization;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Components;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Tests;

public sealed class AlphaPreviewPageTests
{
    private static readonly ResourceManager Resources = new(
        "RefugeeNavigator.Web.Resources.Localization.SharedResource",
        typeof(SharedResource).Assembly);

    [Theory]
    [InlineData("uk", "Пошук інформації", "Порівняння країн", "Попередній перегляд")]
    [InlineData("en", "Search information", "Compare countries", "Preview")]
    [InlineData("ru", "Поиск информации", "Сравнение стран", "Предварительный просмотр")]
    public async Task HomepageLinksToBothPreviewsAndKeepsCountryLinks(string language, string searchTitle, string compareTitle, string previewLabel)
    {
        var html = await RenderAsync<Home>(language);
        var visibleHtml = WebUtility.HtmlDecode(html);

        Assert.Contains("href=\"/search\"", visibleHtml);
        Assert.Contains("href=\"/compare\"", visibleHtml);
        Assert.Contains(searchTitle, visibleHtml);
        Assert.Contains(compareTitle, visibleHtml);

        var expectedCountryRoutes = new[] { "/countries/denmark", "/countries/germany", "/countries/poland" };
        var routePositions = expectedCountryRoutes.Select(route => visibleHtml.IndexOf($"href=\"{route}\"", StringComparison.Ordinal)).ToArray();
        Assert.All(routePositions, position => Assert.True(position >= 0));
        Assert.True(routePositions[0] < routePositions[1] && routePositions[1] < routePositions[2]);
        Assert.Contains(previewLabel, visibleHtml);
    }

    [Theory]
    [InlineData("uk", "Пошук інформації", "Порівняння країн", "Функція ще розробляється", "Функція ще розробляється")]
    [InlineData("en", "Search information", "Compare countries", "This feature is in development", "This feature is in development")]
    [InlineData("ru", "Поиск информации", "Сравнение стран", "Функция находится в разработке", "Функция находится в разработке")]
    public async Task PreviewPagesAreLocalizedAccessibleAndExplicitlyNotFunctional(
        string language,
        string searchHeading,
        string compareHeading,
        string searchNotice,
        string compareNotice)
    {
        var searchHtml = await RenderAsync<Search>(language);
        var compareHtml = await RenderAsync<Compare>(language);

        AssertPreviewPage(searchHtml, searchHeading, searchNotice, GetResource("Search.PageTitle", language), language);
        AssertPreviewPage(compareHtml, compareHeading, compareNotice, GetResource("Compare.PageTitle", language), language);
        Assert.Contains(language switch
        {
            "uk" => "які можна зіставити між країнами",
            "en" => "can be compared across countries",
            _ => "которые можно сопоставить между странами"
        }, WebUtility.HtmlDecode(compareHtml));
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task CountryOverviewKeepsExistingCategoryLinks(string language)
    {
        var parameters = new Dictionary<string, object?>
        {
            [nameof(CountryOverview.CountrySlug)] = "denmark"
        };
        var html = await RenderAsync<CountryOverview>(language, parameters);
        var visibleHtml = WebUtility.HtmlDecode(html);

        foreach (var slug in new[]
        {
            "temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance"
        })
        {
            Assert.Contains($"href=\"/countries/denmark/categories/{slug}\"", visibleHtml);
        }
    }

    [Theory]
    [InlineData(typeof(Search), "/search")]
    [InlineData(typeof(Compare), "/compare")]
    public void PreviewPagesUsePermanentRoutes(Type componentType, string expectedRoute)
    {
        var route = componentType.GetCustomAttribute<RouteAttribute>();

        Assert.NotNull(route);
        Assert.Equal(expectedRoute, route!.Template);
    }

    private static void AssertPreviewPage(string html, string heading, string notice, string pageTitle, string language)
    {
        var visibleHtml = WebUtility.HtmlDecode(html);
        Assert.Contains("<main id=\"main-content\"", visibleHtml);
        Assert.Matches($"<h1[^>]*>{Regex.Escape(heading)}</h1>", visibleHtml);
        Assert.Single(Regex.Matches(html, "<h1\\b").Cast<Match>());
        Assert.Contains(notice, visibleHtml);
        Assert.Contains("href=\"/\"", visibleHtml);
        Assert.Contains(GetResource("Common.Disclaimer", language), visibleHtml);
        Assert.DoesNotContain("Search.", visibleHtml);
        Assert.DoesNotContain("Compare.", visibleHtml);
        Assert.False(string.IsNullOrWhiteSpace(pageTitle));
        Assert.DoesNotContain("PageTitle", pageTitle);
    }

    private static string GetResource(string key, string language) =>
        Resources.GetString(key, CultureInfo.GetCultureInfo(language)) ?? string.Empty;

    private static async Task<string> RenderAsync<TComponent>(
        string language,
        IDictionary<string, object?>? parameters = null)
        where TComponent : IComponent
    {
        var originalCulture = CultureInfo.CurrentCulture;
        var originalUiCulture = CultureInfo.CurrentUICulture;
        var culture = CultureInfo.GetCultureInfo(language);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        try
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton(CreateCatalog());
            services.AddSingleton<NavigationManager, TestNavigationManager>();
            using var provider = services.BuildServiceProvider();
            await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());

            return await renderer.Dispatcher.InvokeAsync(async () =>
            {
                var view = parameters is null
                    ? ParameterView.Empty
                    : ParameterView.FromDictionary(parameters);
                var component = await renderer.RenderComponentAsync<TComponent>(view);
                return component.ToHtmlString();
            });
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
            CultureInfo.CurrentUICulture = originalUiCulture;
        }
    }

    private static CatalogData CreateCatalog()
    {
        var countries = new[]
        {
            new Country(Guid.Parse("10000000-0000-0000-0000-000000000001"), "Denmark", "denmark"),
            new Country(Guid.Parse("10000000-0000-0000-0000-000000000002"), "Germany", "germany"),
            new Country(Guid.Parse("10000000-0000-0000-0000-000000000003"), "Poland", "poland")
        };
        var categorySlugs = new[]
        {
            "temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance"
        };
        var categories = categorySlugs.Select((slug, index) =>
            new InformationCategory(Guid.Parse($"20000000-0000-0000-0000-{index + 1:000000000000}"), slug, slug)).ToArray();

        return (CatalogData)Activator.CreateInstance(
            typeof(CatalogData),
            BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            args: [countries, categories, Array.Empty<InformationEntry>(), Array.Empty<InformationEntryTranslation>(), Array.Empty<OfficialSource>()],
            culture: CultureInfo.InvariantCulture)!;
    }

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
}
