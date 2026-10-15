using System.Globalization;
using System.Net;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Components;
using RefugeeNavigator.Web.Components.Pages;

namespace RefugeeNavigator.Tests;

public sealed class DenmarkHousingArticleTests
{
    private static readonly Guid DenmarkId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly Guid HousingCategoryId = Guid.Parse("20000000-0000-0000-0000-000000000004");
    private static readonly Guid ArticleId = Guid.Parse("19afdd19-e2cc-4da6-bde3-6dc3d64b7503");
    private static readonly string[] ExistingDenmarkArticleIds =
    [
        "c2356631-3fa2-4553-8fda-50a4419262c0",
        "2d106fd3-e981-4ef9-9733-568b654dfca5",
        "b1e4aed1-bbaf-4f09-9468-74000bf96430",
        "fee9dcf0-447d-4402-92a2-8b1bec125944",
        "1a19c1f9-1111-4d60-8490-da548f33083f"
    ];
    private static readonly string[] RequiredSources =
    [
        "https://www.nyidanmark.dk/en-GB/You-are-waiting-for-an-answer/Fordreven-fra-Ukraine",
        "https://www.nyidanmark.dk/en-GB/You-have-received-an-answer/Ukraine/Ja-Ukraine",
        "https://www.retsinformation.dk/eli/lta/2026/339",
        "https://integrationogjob.dk/nyankomne/faq-ukraine/",
        "https://lifeindenmark.borger.dk/housing-and-moving/rental-property/renting-a-home",
        "https://lifeindenmark.borger.dk/housing-and-moving/housing-benefits",
        "https://www.retsinformation.dk/eli/lta/2025/995/pdf",
        "https://lifeindenmark.borger.dk/money-and-tax/social-benefits/financial-support-from-the-municipality",
        "https://lifeindenmark.borger.dk/housing-and-moving/rental-property/tenancy-deposit-loans",
        "https://lifeindenmark.borger.dk/housing-and-moving/moving/change-address?selfserviceId=72929690-5c60-4079-8740-bdb114d81279",
        "https://www.borger.dk/bolig-og-flytning/lejebolig/beboerklagenaevn-og-huslejenaevn"
    ];

    [Fact]
    public async Task CatalogContainsVerifiedArticleWithAllSourcesAndPreservesExistingDenmarkArticles()
    {
        var catalog = await LoadProductionCatalogAsync();
        var article = Assert.Single(catalog.InformationEntries, entry => entry.Id == ArticleId);
        Assert.Equal(DenmarkId, article.CountryId);
        Assert.Equal(HousingCategoryId, article.CategoryId);

        var translations = catalog.InformationEntryTranslations
            .Where(translation => translation.InformationEntryId == ArticleId)
            .ToArray();
        Assert.Equal(new[] { "en", "ru", "uk" }, translations.Select(translation => translation.LanguageCode).OrderBy(code => code));
        Assert.All(translations, translation =>
        {
            Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
            Assert.False(string.IsNullOrWhiteSpace(translation.Title));
            Assert.False(string.IsNullOrWhiteSpace(translation.Summary));
            Assert.False(string.IsNullOrWhiteSpace(translation.Content));
            Assert.Equal(new DateOnly(2026, 10, 15), DateOnly.FromDateTime(translation.LastUpdatedAt.UtcDateTime));
        });
        var unsupportedEmphasisMarkers = new[]
        {
            "*boligstøtte*",
            "*Borgerservice*",
            "*Huslejenævn*",
            "*Beboerklagenævn*"
        };
        Assert.All(translations, translation =>
            Assert.DoesNotContain(unsupportedEmphasisMarkers, marker => translation.Content.Contains(marker, StringComparison.Ordinal)));

        var sources = catalog.OfficialSources.Where(source => source.InformationEntryId == ArticleId).ToArray();
        Assert.Equal(RequiredSources.OrderBy(url => url), sources.Select(source => source.Url.AbsoluteUri).OrderBy(url => url));
        Assert.All(sources, source =>
        {
            Assert.Equal(ArticleId, source.InformationEntryId);
            Assert.Equal(new DateOnly(2026, 10, 15), DateOnly.FromDateTime(source.LastCheckedAt.UtcDateTime));
        });

        Assert.All(ExistingDenmarkArticleIds, id =>
            Assert.Contains(catalog.InformationEntries, entry => entry.Id == Guid.Parse(id) && entry.CountryId == DenmarkId));
        Assert.Equal(7, catalog.InformationEntries.Count(entry => entry.CountryId == DenmarkId));
    }

    [Theory]
    [InlineData("uk", "Як знайти житло в Данії", "З чого почати|Житло через муніципалітет|Самостійна оренда житла|Фінансова підтримка на житло|Переїзд і реєстрація адреси|Де отримати допомогу", "він має визначити тимчасове місце проживання", "може натомість призначити постійне житло", "15 робочих днів", "протягом перших трьох років", "за чотири тижні до переїзду", "не пізніше ніж через п’ять днів", "подали заяву на дозвіл за Спеціальним законом або подадуть її протягом п’яти робочих днів", "очевидно не перебувають поза цільовою групою Спеціального закону", "може подати заяву, якщо має постійне місце проживання")]
    [InlineData("en", "How to find housing in Denmark", "Where to start|Accommodation through municipalities|Renting housing independently|Housing-related financial support|Moving and address registration|Where to get help", "must designate a temporary place to stay", "may instead designate permanent housing", "15 working days", "within the first three years", "four weeks before moving", "five days after the move", "applied for a permit under the Special Act (or apply within five working days)", "not obviously outside the Special Act’s target group", "may apply if they have fixed residence in Denmark")]
    [InlineData("ru", "Как найти жильё в Дании", "С чего начать|Жильё через муниципалитет|Самостоятельная аренда жилья|Финансовая поддержка на жильё|Переезд и регистрация адреса|Где получить помощь", "обязан определить временное место проживания", "может вместо этого назначить постоянное жильё", "15 рабочих дней", "в течение первых трёх лет", "за четыре недели до переезда", "не позднее пяти дней после него", "подали заявление на разрешение по Специальному закону или подадут его в течение пяти рабочих дней", "не находятся явно вне целевой группы Специального закона", "может подать заявление, если имеет постоянное место жительства")]
    public async Task LocalizedArticleRendersPlacementConditionsSourcesAndSixSections(
        string language,
        string expectedTitle,
        string expectedHeadings,
        string temporaryPlacement,
        string permanentAlternative,
        string responsibilityPeriod,
        string depositLoanCondition,
        string addressAdvanceNotice,
        string addressDeadline,
        string applicantCondition,
        string applicantTargetGroupCondition,
        string housingBenefitCondition)
    {
        var catalog = await LoadProductionCatalogAsync();
        var html = await RenderAsync<Category>(catalog, language,
            "/countries/denmark/categories/housing",
            new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = "denmark",
                [nameof(Category.CategorySlug)] = "housing"
            });
        var visibleHtml = WebUtility.HtmlDecode(html);

        Assert.Contains(expectedTitle, visibleHtml);
        var headings = Regex.Matches(html, "<h3\\b[^>]*class=\"entry-section-heading\"[^>]*>(.*?)</h3>")
            .Cast<Match>()
            .Select(match => WebUtility.HtmlDecode(match.Groups[1].Value))
            .ToArray();
        Assert.Equal(expectedHeadings.Split('|'), headings);
        Assert.Contains(temporaryPlacement, visibleHtml);
        Assert.Contains(permanentAlternative, visibleHtml);
        Assert.Contains(responsibilityPeriod, visibleHtml);
        Assert.Contains(depositLoanCondition, visibleHtml);
        Assert.Contains(addressAdvanceNotice, visibleHtml);
        Assert.Contains(addressDeadline, visibleHtml);
        Assert.Contains(applicantCondition, visibleHtml);
        Assert.Contains(applicantTargetGroupCondition, visibleHtml);
        Assert.Contains(housingBenefitCondition, visibleHtml);
        Assert.Equal(RequiredSources.Length, RequiredSources.Count(url => html.Contains($"href=\"{url}\"", StringComparison.Ordinal)));
        Assert.Equal(VerificationStatus.Verified, Assert.Single(catalog.InformationEntryTranslations,
            translation => translation.InformationEntryId == ArticleId && translation.LanguageCode == language).VerificationStatus);
    }

    [Fact]
    public async Task HousingArticleAppearsInSearchAndCountryComparison()
    {
        var catalog = await LoadProductionCatalogAsync();
        var searchHtml = await RenderAsync<Search>(catalog, "en",
            "/search?q=How%20to%20find%20housing%20in%20Denmark&country=denmark&category=housing");
        Assert.Contains("href=\"/countries/denmark/categories/housing\"", searchHtml);
        Assert.Contains("How to find housing in Denmark", WebUtility.HtmlDecode(searchHtml));

        var compareHtml = await RenderAsync<Compare>(catalog, "en",
            "/compare?country1=denmark&country2=germany&category=housing");
        Assert.Contains("How to find housing in Denmark", WebUtility.HtmlDecode(compareHtml));
        Assert.Contains("href=\"/countries/denmark/categories/housing\"", compareHtml);
    }

    [Fact]
    public async Task HousingArticleRendersContentAsEncodedText()
    {
        var temporaryRoot = Path.Combine(Path.GetTempPath(), "RefugeeNavigatorHousingEncoding", Guid.NewGuid().ToString("N"));
        var dataDirectory = Path.Combine(temporaryRoot, "Data");
        Directory.CreateDirectory(dataDirectory);
        try
        {
            foreach (var fileName in new[] { "countries.json", "categories.json", "information-entries.json", "official-sources.json" })
            {
                File.Copy(Path.Combine(AppContext.BaseDirectory, "Data", fileName), Path.Combine(dataDirectory, fileName));
            }

            var entriesPath = Path.Combine(dataDirectory, "information-entries.json");
            var entries = JsonNode.Parse(await File.ReadAllTextAsync(entriesPath))!.AsArray();
            var article = entries.Single(entry => entry!["Id"]!.GetValue<string>() == ArticleId.ToString());
            var english = article!["Translations"]!.AsArray()
                .Single(translation => translation!["LanguageCode"]!.GetValue<string>() == "en");
            english!["Content"] = "<script>alert('x')</script>";
            await File.WriteAllTextAsync(entriesPath, entries.ToJsonString());

            var syntheticCatalog = await new JsonCatalogProvider(temporaryRoot).LoadAsync();
            var html = await RenderAsync<Category>(syntheticCatalog, "en",
                "/countries/denmark/categories/housing",
                new Dictionary<string, object?>
                {
                    [nameof(Category.CountrySlug)] = "denmark",
                    [nameof(Category.CategorySlug)] = "housing"
                });

            Assert.Contains("&lt;script&gt;alert(&#x27;x&#x27;)&lt;/script&gt;", html);
            Assert.DoesNotContain("<script>", html);
        }
        finally
        {
            Directory.Delete(temporaryRoot, recursive: true);
        }
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
