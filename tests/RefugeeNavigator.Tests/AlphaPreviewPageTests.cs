using System.Globalization;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Components;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Guides;
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
    [InlineData("uk", "Основна навігація", "На головну", "Пошук", "Порівняння країн")]
    [InlineData("en", "Primary navigation", "Home", "Search", "Compare countries")]
    [InlineData("ru", "Основная навигация", "На главную", "Поиск", "Сравнение стран")]
    public async Task GlobalNavigationHasLocalizedNamedLinksAndPreservesLanguageReturnUrl(
        string language,
        string navigationName,
        string homeLabel,
        string searchLabel,
        string compareLabel)
    {
        var html = WebUtility.HtmlDecode(await RenderAsync<SiteHeader>(language, currentPath: "/search?q=jobs"));

        Assert.Contains($"aria-label=\"{navigationName}\"", html);
        Assert.Collection(Regex.Matches(html, "<nav\\b").Cast<Match>(), _ => { }, _ => { });
        Assert.Contains($"aria-label=\"{GetResource("Common.LanguageSelection", language)}\"", html);
        AssertNavigationLink(html, "/", homeLabel, isCurrent: false);
        AssertNavigationLink(html, "/search", searchLabel, isCurrent: true);
        AssertNavigationLink(html, "/compare", compareLabel, isCurrent: false);
        Assert.Contains("href=\"/set-language?culture=uk&amp;returnUrl=%2Fsearch%3Fq%3Djobs\"", await RenderAsync<SiteHeader>(language, currentPath: "/search?q=jobs"));
        Assert.DoesNotContain("Navigation.", html);
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task LanguageSwitchLinksDisableEnhancedNavigationAndPreserveReturnUrl(string language)
    {
        var rendered = await RenderAsync<SiteHeader>(language, currentPath: "/guides/germany/temporary-protection?step=3");
        var html = WebUtility.HtmlDecode(rendered);
        var languageLinks = Regex.Matches(html, "<a\\b(?<attributes>[^>]*)>(?<content>.*?)</a>", RegexOptions.Singleline)
            .Cast<Match>()
            .Where(match => match.Groups["attributes"].Value.Contains("/set-language?culture=", StringComparison.Ordinal))
            .ToArray();

        Assert.Equal(3, languageLinks.Length);
        Assert.All(languageLinks, link => Assert.Contains("data-enhance-nav=\"false\"", link.Groups["attributes"].Value));
        Assert.All(languageLinks, link => Assert.Contains("returnUrl=%2Fguides%2Fgermany%2Ftemporary-protection%3Fstep%3D3", link.Groups["attributes"].Value));
    }

    [Theory]
    [InlineData("/", "/")]
    [InlineData("/search?q=jobs", "/search")]
    [InlineData("/compare?country1=denmark", "/compare")]
    public async Task GlobalNavigationMarksTheCurrentSection(string currentPath, string activeRoute)
    {
        var html = WebUtility.HtmlDecode(await RenderAsync<SiteHeader>("en", currentPath: currentPath));
        var activeLinks = Regex.Matches(html, "<a\\b(?<attributes>[^>]*)>(?<content>.*?)</a>", RegexOptions.Singleline)
            .Cast<Match>()
            .Where(match => Regex.IsMatch(match.Groups["attributes"].Value, "aria-current=\"page\""))
            .Select(match => Regex.Match(match.Groups["attributes"].Value, "href=\"(?<route>[^\"]+)\"").Groups["route"].Value)
            .ToArray();

        Assert.Equal([activeRoute], activeLinks);
    }

    [Theory]
    [InlineData("uk", "На головну")]
    [InlineData("en", "Home")]
    [InlineData("ru", "На главную")]
    public async Task InternalPagesHaveTopAndBottomHomeLinksButHomepageDoesNot(
        string language,
        string homeLabel)
    {
        var countryParameters = new Dictionary<string, object?>
        {
            [nameof(CountryOverview.CountrySlug)] = "denmark"
        };
        var categoryParameters = new Dictionary<string, object?>
        {
            [nameof(Category.CountrySlug)] = "denmark",
            [nameof(Category.CategorySlug)] = "employment"
        };
        var pages = new[]
        {
            await RenderAsync<Search>(language, currentPath: "/search"),
            await RenderAsync<Compare>(language, currentPath: "/compare"),
            await RenderAsync<CountryOverview>(language, countryParameters, "/countries/denmark"),
            await RenderAsync<Category>(language, categoryParameters, "/countries/denmark/categories/employment"),
            await RenderAsync<NotFound>(language, currentPath: "/not-found")
        };

        foreach (var page in pages)
        {
            AssertHomeLinksAtTopAndBottom(page, homeLabel);
            Assert.Single(Regex.Matches(page, "<main\\b"));
        }

        var homepage = await RenderAsync<Home>(language);
        Assert.DoesNotContain("home-return-link", homepage);
    }

    [Theory]
    [InlineData("uk", "Данія", "Німеччина", "Польща", "Докладніше про країну: {0}")]
    [InlineData("en", "Denmark", "Germany", "Poland", "Explore {0}")]
    [InlineData("ru", "Дания", "Германия", "Польша", "Подробнее о стране: {0}")]
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
    [InlineData("uk", "Пошук інформації", "Порівняння країн", "статті, доступні в застосунку")]
    [InlineData("en", "Search information", "Compare countries", "currently available in the app")]
    [InlineData("ru", "Поиск информации", "Сравнение стран", "статьи, доступные в приложении")]
    public async Task SearchIsFunctionalAndCompareExplainsItsReviewScope(
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
        Assert.Contains(GetResource("Common.Disclaimer", language), visibleCompareHtml);
        Assert.DoesNotContain("Compare.", visibleCompareHtml);

        var selectedCompareHtml = WebUtility.HtmlDecode(await RenderAsync<Compare>(language,
            currentPath: "/compare?country1=denmark&country2=germany&category=employment"));
        Assert.Contains(GetResource("Compare.ScopeNotice", language), selectedCompareHtml);
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
    [InlineData("denmark", "10000000-0000-0000-0000-000000000001")]
    [InlineData("germany", "10000000-0000-0000-0000-000000000002")]
    [InlineData("poland", "10000000-0000-0000-0000-000000000003")]
    public async Task ApprovedCountryGuideCalloutAppearsBeforeCategoriesInProduction(string slug, string countryId)
    {
        var parameters = new Dictionary<string, object?>
        {
            [nameof(CountryOverview.CountrySlug)] = slug
        };
        var html = WebUtility.HtmlDecode(await RenderAsync<CountryOverview>(
            "en",
            parameters,
            $"/countries/{slug}",
            CreateGuideCatalog(Guid.Parse(countryId), GuideEditorialStatus.Approved),
            "Production"));

        Assert.Contains("class=\"country-guide-callout\"", html);
        Assert.Contains($"href=\"/guides/{slug}/temporary-protection\"", html);
        Assert.Single(Regex.Matches(html, "class=\"country-guide-callout\""));
        Assert.True(html.IndexOf("class=\"country-guide-callout\"", StringComparison.Ordinal) <
                    html.IndexOf("class=\"categories\"", StringComparison.Ordinal));
        Assert.Contains("class=\"categories\"", html);
    }

    [Fact]
    public async Task GuideForDifferentCountryDoesNotAppearOnCountryOverview()
    {
        var parameters = new Dictionary<string, object?>
        {
            [nameof(CountryOverview.CountrySlug)] = "poland"
        };
        var denmarkGuide = CreateGuideCatalog(Guid.Parse("10000000-0000-0000-0000-000000000001"), GuideEditorialStatus.Approved);
        var html = await RenderAsync<CountryOverview>("en", parameters, "/countries/poland", denmarkGuide, "Production");

        Assert.DoesNotContain("country-guide-callout", html);
    }

    [Theory]
    [InlineData("uk", "Не знаєте, з чого почати?", "Оберіть ситуацію, яка найбільше схожа на вашу, і перегляньте інформацію крок за кроком.", "Відкрити покроковий помічник")]
    [InlineData("en", "Not sure where to start?", "Choose the situation closest to yours and follow the information step by step.", "Open step-by-step guide")]
    [InlineData("ru", "Не знаете, с чего начать?", "Выберите ситуацию, которая ближе всего к вашей, и пройдите информацию шаг за шагом.", "Открыть пошаговый помощник")]
    public async Task CountryGuideCalloutUsesDedicatedLocalizedText(string language, string title, string description, string linkText)
    {
        var parameters = new Dictionary<string, object?>
        {
            [nameof(CountryOverview.CountrySlug)] = "germany"
        };
        var html = WebUtility.HtmlDecode(await RenderAsync<CountryOverview>(
            language,
            parameters,
            "/countries/germany",
            CreateGuideCatalog(Guid.Parse("10000000-0000-0000-0000-000000000002"), GuideEditorialStatus.Approved),
            "Production"));

        Assert.Contains(title, html);
        Assert.Contains(description, html);
        Assert.Contains(linkText, html);
        Assert.DoesNotContain("A step-by-step guide is available for this topic.", html);
    }

    [Theory]
    [InlineData("Development")]
    [InlineData("Production")]
    public async Task ApprovedCountryGuideIsVisibleInBothEnvironments(string environmentName)
    {
        var parameters = new Dictionary<string, object?>
        {
            [nameof(CountryOverview.CountrySlug)] = "germany"
        };
        var html = await RenderAsync<CountryOverview>(
            "en",
            parameters,
            "/countries/germany",
            CreateGuideCatalog(Guid.Parse("10000000-0000-0000-0000-000000000002"), GuideEditorialStatus.Approved),
            environmentName);

        Assert.Contains("class=\"country-guide-callout\"", html);
    }

    [Fact]
    public async Task DraftCountryGuideIsVisibleOnlyInDevelopment()
    {
        var parameters = new Dictionary<string, object?>
        {
            [nameof(CountryOverview.CountrySlug)] = "germany"
        };
        var draft = CreateGuideCatalog(Guid.Parse("10000000-0000-0000-0000-000000000002"), GuideEditorialStatus.Draft);
        var production = await RenderAsync<CountryOverview>("en", parameters, "/countries/germany", draft, "Production");
        var development = await RenderAsync<CountryOverview>("en", parameters, "/countries/germany", draft, "Development");

        Assert.DoesNotContain("country-guide-callout", production);
        Assert.Contains("class=\"country-guide-callout\"", development);
    }

    [Theory]
    [InlineData("uk", "denmark", "Данія", "у Данії", "Життя у Данії")]
    [InlineData("uk", "germany", "Німеччина", "у Німеччині", "Життя у Німеччині")]
    [InlineData("uk", "poland", "Польща", "у Польщі", "Життя у Польщі")]
    [InlineData("en", "denmark", "Denmark", "Denmark", "Living in Denmark")]
    [InlineData("en", "germany", "Germany", "Germany", "Living in Germany")]
    [InlineData("en", "poland", "Poland", "Poland", "Living in Poland")]
    [InlineData("ru", "denmark", "Дания", "в Дании", "Жизнь в Дании")]
    [InlineData("ru", "germany", "Германия", "в Германии", "Жизнь в Германии")]
    [InlineData("ru", "poland", "Польша", "в Польше", "Жизнь в Польше")]
    public async Task CountryCardsAndOverviewsUseNaturalLocalizedNames(
        string language,
        string slug,
        string nominativeName,
        string locationPhrase,
        string livingHeading)
    {
        var homeHtml = WebUtility.HtmlDecode(await RenderAsync<Home>(language));
        var countryParameters = new Dictionary<string, object?>
        {
            [nameof(CountryOverview.CountrySlug)] = slug
        };
        var overviewHtml = WebUtility.HtmlDecode(await RenderAsync<CountryOverview>(language, countryParameters, $"/countries/{slug}"));

        var countryHeadings = Regex.Matches(homeHtml, @"<h3\b[^>]*>(?<name>.*?)</h3>", RegexOptions.Singleline)
            .Cast<Match>()
            .Select(match => match.Groups["name"].Value.Trim())
            .TakeLast(3)
            .ToArray();
        Assert.Contains(nominativeName, countryHeadings);
        Assert.Contains(string.Format(CultureInfo.GetCultureInfo(language), GetResource("Home.CountryDescription", language), locationPhrase), homeHtml);
        Assert.Contains($"href=\"/countries/{slug}\"", homeHtml);
        Assert.Matches($"<h1\\b[^>]*>{Regex.Escape(nominativeName)}</h1>", overviewHtml);
        Assert.Contains(livingHeading, overviewHtml);
        Assert.Contains(locationPhrase, overviewHtml);
        Assert.DoesNotContain("«", homeHtml + overviewHtml);
        Assert.DoesNotContain("»", homeHtml + overviewHtml);
        Assert.DoesNotContain("в в ", homeHtml + overviewHtml);
        Assert.DoesNotContain("у у ", homeHtml + overviewHtml);
        Assert.Contains($"href=\"/countries/{slug}/categories/", overviewHtml);
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
        IDictionary<string, object?>? parameters = null,
        string currentPath = "/",
        StepByStepGuideCatalog? guides = null,
        string environmentName = "Development")
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
            await ComparisonTestServices.RegisterAsync(services);
            services.AddSingleton(guides ?? new StepByStepGuideCatalog([]));
            services.AddSingleton<IWebHostEnvironment>(new TestWebHostEnvironment(environmentName));
            services.AddSingleton<NavigationManager>(new TestNavigationManager(currentPath));
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

    private static StepByStepGuideCatalog CreateGuideCatalog(Guid countryId, GuideEditorialStatus status)
    {
        var guide = new RefugeeNavigator.Web.Guides.StepByStepGuide(
            $"{countryId}-guide",
            "temporary-protection",
            countryId,
            status,
            new Dictionary<string, LocalizedGuideText>(),
            []);
        return new StepByStepGuideCatalog([guide]);
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

    private sealed class TestNavigationManager : NavigationManager
    {
        public TestNavigationManager(string currentPath)
        {
            Initialize("http://localhost/", $"http://localhost{currentPath}");
        }

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
        }
    }

    private static void AssertHomeLinksAtTopAndBottom(string html, string expectedLabel)
    {
        var links = Regex.Matches(html, "<a\\b(?<attributes>[^>]*)>(?<content>.*?)</a>", RegexOptions.Singleline)
            .Cast<Match>()
            .Where(link => Regex.IsMatch(link.Groups["attributes"].Value, "class=\"home-return-link\"") &&
                           Regex.IsMatch(link.Groups["attributes"].Value, "href=\"/\""))
            .ToArray();

        Assert.Equal(2, links.Length);
        Assert.All(links, link => Assert.Contains(expectedLabel, WebUtility.HtmlDecode(link.Groups["content"].Value)));
        Assert.All(links, link => Assert.Contains("aria-hidden=\"true\"", link.Groups["content"].Value));

        var mainStart = html.IndexOf("<main", StringComparison.Ordinal);
        var mainEnd = html.LastIndexOf("</main>", StringComparison.Ordinal);
        Assert.True(links[0].Index < mainStart, "The first homepage link should appear before main content.");
        Assert.True(links[1].Index > mainStart && links[1].Index < mainEnd, "The second homepage link should appear near the end of main content.");
    }

    private static void AssertNavigationLink(string html, string route, string label, bool isCurrent)
    {
        var link = Regex.Matches(html, "<a\\b(?<attributes>[^>]*)>(?<content>.*?)</a>", RegexOptions.Singleline)
            .Cast<Match>()
            .SingleOrDefault(match => Regex.IsMatch(
                match.Groups["attributes"].Value,
                $"href=\"{Regex.Escape(route)}\""));

        Assert.NotNull(link);
        Assert.Equal(label, WebUtility.HtmlDecode(Regex.Replace(link!.Groups["content"].Value, "<[^>]+>", string.Empty)).Trim());
        Assert.Equal(isCurrent, Regex.IsMatch(link.Groups["attributes"].Value, "aria-current=\"page\""));
    }
}
