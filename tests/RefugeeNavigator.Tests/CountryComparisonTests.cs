using System.Globalization;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Tests;

public sealed class CountryComparisonTests
{
    private static readonly Guid DenmarkId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly Guid GermanyId = Guid.Parse("10000000-0000-0000-0000-000000000002");
    private static readonly Guid PolandId = Guid.Parse("10000000-0000-0000-0000-000000000003");
    private static readonly Guid EmploymentId = Guid.Parse("20000000-0000-0000-0000-000000000001");
    private static readonly Guid HealthcareId = Guid.Parse("20000000-0000-0000-0000-000000000002");
    private static readonly Guid FirstEntryId = Guid.Parse("30000000-0000-0000-0000-000000000001");
    private static readonly Guid SecondEntryId = Guid.Parse("30000000-0000-0000-0000-000000000002");
    private static readonly Guid ThirdEntryId = Guid.Parse("30000000-0000-0000-0000-000000000003");

    private static readonly ResourceManager Resources = new(
        "RefugeeNavigator.Web.Resources.Localization.SharedResource",
        typeof(SharedResource).Assembly);

    [Theory]
    [InlineData("uk", "Данія", "Німеччина", "Працевлаштування", "Перший <посібник> & деталі", "Другий матеріал", "Матеріал Німеччини", "Перевірено", "Потребує перевірки", "Може бути застарілою")]
    [InlineData("en", "Denmark", "Germany", "Employment", "First <Guide> & details", "Second article", "Germany article", "Reviewed", "Needs review", "May be outdated")]
    [InlineData("ru", "Дания", "Германия", "Трудоустройство", "Первая <инструкция> & детали", "Вторая статья", "Статья Германии", "Проверено", "Требует проверки", "Возможно, устарело")]
    public async Task SelectedComparisonRendersLocalizedPanelsAndPublishedArticlesInCatalogOrder(
        string language,
        string denmark,
        string germany,
        string employment,
        string firstTitle,
        string secondTitle,
        string germanyTitle,
        string verified,
        string needsReview,
        string outdated)
    {
        var html = await RenderAsync(language, "?country1=denmark&country2=germany&category=employment");
        var visibleHtml = WebUtility.HtmlDecode(html);

        Assert.Equal(2, Regex.Matches(html, "class=\"country-panel").Count);
        Assert.Contains(denmark, visibleHtml);
        Assert.Contains(germany, visibleHtml);
        Assert.Contains(employment, visibleHtml);
        Assert.Contains(firstTitle, visibleHtml);
        Assert.Contains(secondTitle, visibleHtml);
        Assert.Contains(germanyTitle, visibleHtml);
        Assert.Contains($"href=\"/countries/denmark/categories/employment\"", html);
        Assert.Contains($"href=\"/countries/germany/categories/employment\"", html);
        Assert.Contains(verified, visibleHtml);
        Assert.Contains(needsReview, visibleHtml);
        Assert.Contains(outdated, visibleHtml);
        Assert.Contains(GetResource("Compare.PreviewNotice", language), visibleHtml);
        Assert.Contains("name=\"country1\"", html);
        Assert.Contains("name=\"country2\"", html);
        Assert.Contains("name=\"category\"", html);
        Assert.Contains("selected", html);
        Assert.Single(Regex.Matches(html, "<h1\\b").Cast<Match>());

        var titlePositions = new[] { firstTitle, secondTitle, germanyTitle }
            .Select(title => visibleHtml.IndexOf(title, StringComparison.Ordinal))
            .ToArray();
        Assert.True(titlePositions[0] < titlePositions[1] && titlePositions[1] < titlePositions[2]);

        if (language == "en")
        {
            Assert.Contains("First &lt;Guide&gt; &amp; details", html);
            Assert.DoesNotContain("First <Guide>", html);
        }
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task EmptyAndPartialStateShowsInstructionsAndReset(string language)
    {
        var initial = WebUtility.HtmlDecode(await RenderAsync(language));
        Assert.Contains(GetResource("Compare.SelectionInstructions", language), initial);
        Assert.Contains(GetResource("Common.Preview", language), initial);
        Assert.Contains("name=\"country1\" required", initial);
        Assert.Contains("name=\"country2\" required", initial);
        Assert.Contains("name=\"category\" required", initial);
        Assert.DoesNotContain("class=\"country-panel", initial);
        Assert.Contains("href=\"/compare\"", initial);

        var partial = await RenderAsync(language, "?country1=denmark");
        Assert.Contains("selected", partial);
        Assert.DoesNotContain("class=\"country-panel", partial);
    }

    [Theory]
    [InlineData("?country1=denmark&country2=denmark&category=employment", "Compare.SameCountry")]
    [InlineData("?country1=unknown&country2=germany&category=employment", "Compare.InvalidCountry")]
    [InlineData("?country1=denmark&country2=germany&category=unknown", "Compare.InvalidCategory")]
    [InlineData("?country1=denmark&category=employment", "Compare.SelectionInstructions")]
    public async Task InvalidAndIncompleteQueryParametersRenderLocalizedMessages(string query, string resourceKey)
    {
        var html = WebUtility.HtmlDecode(await RenderAsync("en", query));

        Assert.Contains(GetResource(resourceKey, "en"), html);
        Assert.DoesNotContain("class=\"country-panel", html);
        Assert.DoesNotContain("HTTP 500", html);
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task CategoryWithNoEntriesShowsBothEmptyStatesAndCorrectLinks(string language)
    {
        var html = WebUtility.HtmlDecode(await RenderAsync(language, "?country1=germany&country2=poland&category=healthcare"));
        var expectedEmptyState = GetResource("Compare.EmptyPanel", language);

        Assert.Equal(2, Regex.Matches(html, "class=\"empty-state\"").Count);
        Assert.Equal(2, Regex.Matches(html, Regex.Escape(expectedEmptyState)).Count);
        Assert.Contains("href=\"/countries/germany/categories/healthcare\"", html);
        Assert.Contains("href=\"/countries/poland/categories/healthcare\"", html);
        Assert.DoesNotContain("Germany article", html);
        Assert.DoesNotContain("First &lt;Guide&gt;", html);
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task PreviewExplainsThatPanelsDoNotMakeFactualComparisons(string language)
    {
        var html = WebUtility.HtmlDecode(await RenderAsync(language, "?country1=denmark&country2=germany&category=employment"));
        var notice = GetResource("Compare.PreviewNotice", language);

        Assert.Contains(notice, html);
        Assert.DoesNotContain("better conditions", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("legal advantage", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("difference found", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Compare.", html);
        Assert.Contains(GetResource("Common.Disclaimer", language), html);
    }

    private static string GetResource(string key, string language) =>
        Resources.GetString(key, CultureInfo.GetCultureInfo(language)) ?? string.Empty;

    private static async Task<string> RenderAsync(string language, string query = "")
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
            services.AddSingleton<NavigationManager>(new TestNavigationManager($"http://localhost/compare{query}"));
            using var provider = services.BuildServiceProvider();
            await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());

            return await renderer.Dispatcher.InvokeAsync(async () =>
            {
                var component = await renderer.RenderComponentAsync<Compare>(ParameterView.Empty);
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
            new Country(DenmarkId, "Denmark", "denmark"),
            new Country(GermanyId, "Germany", "germany"),
            new Country(PolandId, "Poland", "poland")
        };
        var categories = new[]
        {
            new InformationCategory(EmploymentId, "Employment", "employment"),
            new InformationCategory(HealthcareId, "Healthcare", "healthcare")
        };
        var entries = new[]
        {
            new InformationEntry(FirstEntryId, DenmarkId, EmploymentId),
            new InformationEntry(SecondEntryId, DenmarkId, EmploymentId),
            new InformationEntry(ThirdEntryId, GermanyId, EmploymentId)
        };
        var translations = new[]
        {
            Translation(FirstEntryId, "uk", "Перший <посібник> & деталі", VerificationStatus.Verified),
            Translation(FirstEntryId, "en", "First <Guide> & details", VerificationStatus.Verified),
            Translation(FirstEntryId, "ru", "Первая <инструкция> & детали", VerificationStatus.Verified),
            Translation(SecondEntryId, "uk", "Другий матеріал", VerificationStatus.NeedsReview),
            Translation(SecondEntryId, "en", "Second article", VerificationStatus.NeedsReview),
            Translation(SecondEntryId, "ru", "Вторая статья", VerificationStatus.NeedsReview),
            Translation(ThirdEntryId, "uk", "Матеріал Німеччини", VerificationStatus.Outdated),
            Translation(ThirdEntryId, "en", "Germany article", VerificationStatus.Outdated),
            Translation(ThirdEntryId, "ru", "Статья Германии", VerificationStatus.Outdated)
        };

        return (CatalogData)Activator.CreateInstance(
            typeof(CatalogData),
            BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            args: [countries, categories, entries, translations, Array.Empty<OfficialSource>()],
            culture: CultureInfo.InvariantCulture)!;
    }

    private static InformationEntryTranslation Translation(Guid id, string language, string title, VerificationStatus status) =>
        new(id, language, title, "Synthetic summary", "Synthetic content", status, new DateTimeOffset(2026, 10, 13, 12, 0, 0, TimeSpan.Zero));

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
