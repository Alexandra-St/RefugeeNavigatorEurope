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
    [InlineData("uk", "Данія", "Німеччина", "Польща", "Докладніше про країну «{0}»")]
    [InlineData("en", "Denmark", "Germany", "Poland", "Explore {0}")]
    [InlineData("ru", "Дания", "Германия", "Польша", "Информация о стране: {0}")]
    public async Task CountryLinksUseVisibleLocalizedNamesAndKeepTheirRoutes(
        string language,
        string denmark,
        string germany,
        string poland,
        string linkLabelTemplate)
    {
        var html = await RenderAsync<Home>(language);
        var visibleHtml = WebUtility.HtmlDecode(html);
        var countries = new[]
        {
            (Slug: "denmark", Name: denmark),
            (Slug: "germany", Name: germany),
            (Slug: "poland", Name: poland)
        };

        var renderedHeadings = Regex.Matches(visibleHtml, @"<h3\b[^>]*>(?<name>.*?)</h3>", RegexOptions.Singleline)
            .Select(match => match.Groups["name"].Value.Trim())
            .TakeLast(3)
            .ToArray();
        Assert.True(countries.Select(country => country.Name).SequenceEqual(renderedHeadings), visibleHtml);

        foreach (var country in countries)
        {
            var link = Regex.Match(
                html,
                $@"<a(?<attributes>[^>]*href=""/countries/{Regex.Escape(country.Slug)}""[^>]*)>(?<content>.*?)</a>",
                RegexOptions.Singleline);

            Assert.True(link.Success, $"Expected a link to {country.Slug}.");
            Assert.DoesNotContain("aria-label=", link.Groups["attributes"].Value, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("aria-hidden=\"true\"", link.Groups["content"].Value);

            var linkText = Regex.Replace(link.Groups["content"].Value, "<span[^>]*>.*?</span>", string.Empty, RegexOptions.Singleline);
            Assert.Equal(string.Format(CultureInfo.GetCultureInfo(language), linkLabelTemplate, country.Name), WebUtility.HtmlDecode(linkText).Trim());
        }
    }

    [Theory]
    [InlineData("uk", "Пошук інформації", "Порівняння країн", "опубліковані статті")]
    [InlineData("en", "Search information", "Compare countries", "currently published")]
    [InlineData("ru", "Поиск информации", "Сравнение стран", "опубликованные статьи")]
    public async Task SearchIsFunctionalAndCompareHasInteractiveSelectionPreview(
        string language,
        string searchHeading,
        string compareHeading,
        string searchScope)
    {
        var searchHtml = await RenderAsync<Search>(language);
        var compareHtml = await RenderAsync<Compare>(language);

        var visibleSearchHtml = WebUtility.HtmlDecode(searchHtml);
        Assert.Contains("<main id=\"main-content\"", visibleSearchHtml);
        Assert.Matches($"<h1[^>]*>{Regex.Escape(searchHeading)}</h1>", visibleSearchHtml);
        Assert.Single(Regex.Matches(searchHtml, "<h1\\b").Cast<Match>());
        Assert.Contains("method=\"get\"", searchHtml);
        Assert.Contains("action=\"/search\"", searchHtml);
        Assert.Contains("name=\"q\"", searchHtml);
        Assert.Contains(searchScope, visibleSearchHtml);
        Assert.DoesNotContain("Search.", visibleSearchHtml);
        Assert.Contains("href=\"/\"", searchHtml);
        Assert.Contains(GetResource("Common.Disclaimer", language), visibleSearchHtml);

        var visibleCompareHtml = WebUtility.HtmlDecode(compareHtml);
        Assert.Contains("<main id=\"main-content\"", visibleCompareHtml);
        Assert.Matches($"<h1[^>]*>{Regex.Escape(compareHeading)}</h1>", visibleCompareHtml);
        Assert.Single(Regex.Matches(compareHtml, "<h1\\b").Cast<Match>());
        Assert.Contains("method=\"get\"", compareHtml);
        Assert.Contains("action=\"/compare\"", compareHtml);
        Assert.Contains("name=\"country1\"", compareHtml);
        Assert.Contains("name=\"country2\"", compareHtml);
        Assert.Contains("name=\"category\"", compareHtml);
        Assert.Contains(GetResource("Compare.SelectionInstructions", language), visibleCompareHtml);
        Assert.Contains(GetResource("Common.Preview", language), visibleCompareHtml);
        Assert.Contains(GetResource("Common.Disclaimer", language), visibleCompareHtml);
        Assert.DoesNotContain("Compare.", visibleCompareHtml);
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
