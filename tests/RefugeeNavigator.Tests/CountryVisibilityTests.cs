using System.Globalization;
using System.Net;
using System.Reflection;
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

namespace RefugeeNavigator.Tests;

public sealed class CountryVisibilityTests
{
    private static readonly string[] AcceptedCountries =
        ["denmark", "germany", "poland", "czechia", "spain", "sweden", "netherlands"];
    private static readonly Lazy<Task<CatalogData>> Catalog = new(
        () => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());
    private static readonly Lazy<Task<ComparisonFactCatalog>> Facts = new(async () =>
        await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json"))
            .LoadAsync(await Catalog.Value));

    [Fact]
    public async Task OneVerifiedTranslationMakesACountryVisibleWithoutChangingTheCountryList()
    {
        var catalog = await Catalog.Value;
        Assert.Equal(AcceptedCountries, CountryVisibility.GetVisibleCountries(catalog, false).Select(country => country.Slug));
        Assert.Equal(catalog.Countries, CountryVisibility.GetVisibleCountries(catalog, true));

        var sweden = catalog.Countries.Single(country => country.Slug == "sweden");
        var swedenEntryIds = catalog.InformationEntries.Where(item => item.CountryId == sweden.Id)
            .Select(item => item.Id).ToHashSet();
        var hiddenCatalog = WithSwedenTranslations(catalog, swedenEntryIds, VerificationStatus.NeedsReview);
        Assert.DoesNotContain(CountryVisibility.GetVisibleCountries(hiddenCatalog, false), country => country.Slug == "sweden");

        var visibleCatalog = WithSwedenTranslations(catalog, swedenEntryIds, VerificationStatus.NeedsReview,
            catalog.InformationEntryTranslations.First(item => swedenEntryIds.Contains(item.InformationEntryId)).InformationEntryId);
        Assert.Contains(CountryVisibility.GetVisibleCountries(visibleCatalog, false), country => country.Slug == "sweden");
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task ProductionHomeAndFiltersExposeExactlyTheSevenAcceptedCountries(string language)
    {
        var home = await RenderAsync<Home>("Production", language);
        Assert.Equal(7, Regex.Matches(home.Html, "class=\"country-card\"").Count);
        foreach (var slug in AcceptedCountries) Assert.Contains($"href=\"/countries/{slug}\"", home.Html);

        var search = await RenderAsync<Search>("Production", language, "/search");
        Assert.Equal(AcceptedCountries, SelectValues(search.Html, "country"));
        var compare = await RenderAsync<Compare>("Production", language, "/compare");
        Assert.Equal(AcceptedCountries, SelectValues(compare.Html, "country1"));
        Assert.Equal(AcceptedCountries, SelectValues(compare.Html, "country2"));
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task ProductionDirectCountryAndCategoryRoutesExposeAllAcceptedCountries(string language)
    {
        var catalog = await Catalog.Value;
        foreach (var slug in AcceptedCountries)
        {
            var overview = await RenderAsync<CountryOverview>("Production", language, $"/countries/{slug}",
                new() { [nameof(CountryOverview.CountrySlug)] = slug });
            Assert.False(overview.NotFound);
            Assert.Equal(6, Regex.Matches(overview.Html, "class=\"category-card\"").Count);
            Assert.Contains("class=\"country-page\"", overview.Html);

            foreach (var category in catalog.Categories)
            {
                var result = await RenderAsync<Category>("Production", language,
                    $"/countries/{slug}/categories/{category.Slug}", new()
                    {
                        [nameof(Category.CountrySlug)] = slug,
                        [nameof(Category.CategorySlug)] = category.Slug
                    });
                Assert.False(result.NotFound);
                Assert.Contains("class=\"category-page\"", result.Html);
            }
        }
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task ProductionSearchPreservesVerifiedResultsAndHeldArticleSuppression(string language)
    {
        var catalog = await Catalog.Value;
        foreach (var slug in new[] { "czechia", "spain", "netherlands", "sweden" })
        {
            var result = await RenderAsync<Search>("Production", language, $"/search?country={slug}");
            Assert.Equal(slug == "netherlands" ? 7 : 8,
                Regex.Matches(result.Html, "class=\"result-card\"").Count);
            var countryId = catalog.Countries.Single(country => country.Slug == slug).Id;
            foreach (var entry in catalog.InformationEntries.Where(item => item.CountryId == countryId))
            {
                var translation = catalog.InformationEntryTranslations.Single(item => item.InformationEntryId == entry.Id && item.LanguageCode == language);
                Assert.Equal(translation.VerificationStatus == VerificationStatus.Verified,
                    result.Html.Contains(translation.Title, StringComparison.Ordinal));
            }
        }
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task ProductionCompareAcceptsSwedenAndOtherAcceptedCountries(string language)
    {
        foreach (var slug in new[] { "czechia", "spain", "netherlands", "sweden" })
        {
            var result = await RenderAsync<Compare>("Production", language,
                $"/compare?country1={slug}&country2=denmark&category=employment");
            Assert.DoesNotContain("class=\"selection-message\"", result.Html);
            Assert.Contains("class=\"structured-country\"", result.Html);
            Assert.Contains($"value=\"{slug}\" selected", result.Html);
        }
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task DevelopmentKeepsSwedenNavigationAndComparisonPreview(string language)
    {
        var catalog = await Catalog.Value;
        var home = await RenderAsync<Home>("Development", language);
        Assert.Equal(7, Regex.Matches(home.Html, "class=\"country-card\"").Count);
        Assert.Contains("href=\"/countries/sweden\"", home.Html);
        var overview = await RenderAsync<CountryOverview>("Development", language, "/countries/sweden",
            new() { [nameof(CountryOverview.CountrySlug)] = "sweden" });
        Assert.False(overview.NotFound);
        Assert.Equal(6, Regex.Matches(overview.Html, "class=\"category-card\"").Count);
        var category = await RenderAsync<Category>("Development", language, "/countries/sweden/categories/employment",
            new() { [nameof(Category.CountrySlug)] = "sweden", [nameof(Category.CategorySlug)] = "employment" });
        Assert.False(category.NotFound);
        Assert.Equal(2, Regex.Matches(category.Html, "class=\"information-entry\"").Count);
        var search = await RenderAsync<Search>("Development", language, "/search");
        Assert.Equal(catalog.Countries.Select(country => country.Slug), SelectValues(search.Html, "country"));
        var compare = await RenderAsync<Compare>("Development", language,
            "/compare?country1=sweden&country2=denmark&category=all");
        Assert.Equal(catalog.Countries.Select(country => country.Slug), SelectValues(compare.Html, "country1"));
        Assert.Equal(catalog.Countries.Select(country => country.Slug), SelectValues(compare.Html, "country2"));
        Assert.DoesNotContain("class=\"selection-message\"", compare.Html);
        Assert.Contains("class=\"draft-preview-notice\"", compare.Html);
        Assert.Contains("class=\"draft-label\"", compare.Html);
    }

    private static string[] SelectValues(string html, string name)
    {
        var select = Regex.Match(html, $"<select\\b[^>]*name=\"{name}\"[^>]*>(.*?)</select>", RegexOptions.Singleline);
        Assert.True(select.Success);
        return Regex.Matches(select.Groups[1].Value, "<option\\b[^>]*value=\"([^\"]+)\"")
            .Select(match => match.Groups[1].Value).ToArray();
    }

    private static CatalogData WithSwedenTranslations(CatalogData catalog, HashSet<Guid> swedenEntryIds,
        VerificationStatus defaultStatus, Guid? verifiedEntryId = null)
    {
        var translations = catalog.InformationEntryTranslations.Select(item =>
        {
            if (!swedenEntryIds.Contains(item.InformationEntryId)) return item;
            var status = verifiedEntryId == item.InformationEntryId && item.LanguageCode == "uk"
                ? VerificationStatus.Verified : defaultStatus;
            return new InformationEntryTranslation(item.InformationEntryId, item.LanguageCode, item.Title,
                item.Summary, item.Content, status, item.LastUpdatedAt);
        });
        return (CatalogData)Activator.CreateInstance(typeof(CatalogData),
            BindingFlags.Instance | BindingFlags.NonPublic, binder: null,
            args: [catalog.Countries, catalog.Categories, catalog.InformationEntries, translations, catalog.OfficialSources],
            culture: CultureInfo.InvariantCulture)!;
    }

    private static async Task<(string Html, bool NotFound)> RenderAsync<TComponent>(string environment,
        string language, string path = "/", Dictionary<string, object?>? parameters = null) where TComponent : IComponent
    {
        var oldCulture = CultureInfo.CurrentCulture;
        var oldUiCulture = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(language);
        try
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton(await Catalog.Value);
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
