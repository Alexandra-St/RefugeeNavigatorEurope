using System.Globalization;
using System.Net;
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

public sealed class DenmarkSocialAssistanceArticleTests
{
    private static readonly Guid DenmarkId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly Guid SocialAssistanceCategoryId = Guid.Parse("20000000-0000-0000-0000-000000000006");
    private static readonly Guid ArticleId = Guid.Parse("1a19c1f9-1111-4d60-8490-da548f33083f");
    private static readonly string[] ExistingDenmarkArticleIds =
    [
        "c2356631-3fa2-4553-8fda-50a4419262c0",
        "2d106fd3-e981-4ef9-9733-568b654dfca5",
        "b1e4aed1-bbaf-4f09-9468-74000bf96430",
        "19afdd19-e2cc-4da6-bde3-6dc3d64b7503",
        "fee9dcf0-447d-4402-92a2-8b1bec125944",
        "35da68f6-8880-40b2-9b3f-eb2b809b1caf",
    ];
    private static readonly string[] RequiredSources =
    [
        "https://www.nyidanmark.dk/en-GB/You-are-waiting-for-an-answer/Fordreven-fra-Ukraine",
        "https://integrationogjob.dk/nyankomne/faq-ukraine/",
        "https://www.borger.dk/arbejde-dagpenge-ferie/Dagpenge-kontanthjaelp-og-sygedagpenge/Kontanthjaelp/kontanthjaelp",
        "https://www.retsinformation.dk/eli/accn/A20260079329",
        "https://www.retsinformation.dk/eli/lta/2024/1655",
        "https://lifeindenmark.borger.dk/money-and-tax/social-benefits/financial-support-from-the-municipality",
        "https://lifeindenmark.borger.dk/family-and-children/family-benefits/child-and-youth-benefits",
        "https://lifeindenmark.borger.dk/family-and-children/childcare/income-based-daycare-subsidy--sibling-subsidy-and-other-subsidies",
        "https://integrationogjob.dk/beskaeftigelse/faq-arbejdspligt/",
        "https://www.retsinformation.dk/eli/lta/2025/804"
    ];

    [Fact]
    public async Task CatalogContainsVerifiedArticleWithThreeTranslationsAndPreservesExistingDenmarkArticles()
    {
        var catalog = await LoadProductionCatalogAsync();
        var article = Assert.Single(catalog.InformationEntries, entry => entry.Id == ArticleId);
        Assert.Equal(DenmarkId, article.CountryId);
        Assert.Equal(SocialAssistanceCategoryId, article.CategoryId);
        Assert.Equal(new[] { "en", "ru", "uk" }, catalog.InformationEntryTranslations
            .Where(translation => translation.InformationEntryId == ArticleId)
            .Select(translation => translation.LanguageCode).OrderBy(language => language));

        var translations = catalog.InformationEntryTranslations.Where(translation => translation.InformationEntryId == ArticleId).ToArray();
        Assert.Equal(3, translations.Length);
        Assert.All(translations, translation =>
        {
            Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
            Assert.False(string.IsNullOrWhiteSpace(translation.Title));
            Assert.False(string.IsNullOrWhiteSpace(translation.Summary));
            Assert.False(string.IsNullOrWhiteSpace(translation.Content));
            Assert.Equal(7, Regex.Matches(translation.Content, "(?m)^## ").Count);
            Assert.DoesNotContain("L 43", translation.Content, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("**", translation.Content, StringComparison.Ordinal);
            Assert.Equal(new DateOnly(2026, 10, 15), DateOnly.FromDateTime(translation.LastUpdatedAt.UtcDateTime));
        });
        Assert.Equal(8, catalog.InformationEntries.Count(entry => entry.CountryId == DenmarkId));
        Assert.All(ExistingDenmarkArticleIds, id =>
            Assert.Contains(catalog.InformationEntries, entry => entry.Id == Guid.Parse(id) && entry.CountryId == DenmarkId));
    }

    [Fact]
    public async Task ArticleHasTenUniqueSupportingSourcesAndNoLegislativeWatchSources()
    {
        var catalog = await LoadProductionCatalogAsync();
        var sources = catalog.OfficialSources.Where(source => source.InformationEntryId == ArticleId).ToArray();
        Assert.Equal(10, sources.Length);
        Assert.Equal(RequiredSources.OrderBy(url => url), sources.Select(source => source.Url.AbsoluteUri).OrderBy(url => url));
        Assert.Equal(10, sources.Select(source => source.Url.AbsoluteUri).Distinct(StringComparer.Ordinal).Count());
        Assert.DoesNotContain(sources, source => source.Url.AbsoluteUri.Contains("202612L00043", StringComparison.Ordinal));
        Assert.All(sources, source =>
        {
            Assert.Equal(ArticleId, source.InformationEntryId);
            Assert.False(string.IsNullOrWhiteSpace(source.Title));
            Assert.Equal(new DateOnly(2026, 10, 15), DateOnly.FromDateTime(source.LastCheckedAt.UtcDateTime));
        });
    }

    [Theory]
    [InlineData("uk", "Соціальна допомога в Данії: виплати та порядок звернення", "Хто може звернутися по допомогу|Грошова допомога на необхідні витрати|Виплати для сімей із дітьми|Допомога на окремі необхідні витрати|Робота, участь у заходах і повідомлення про зміни|Як подати заяву|Де отримати допомогу", "спільних неповнолітніх дітей у Данії", "майно обох із подружжя може й надалі враховуватися", "не охоплені окремою системою arbejdspligt", "не скасовує інших вимог інтеграційної програми", "протягом чотирьох тижнів", "муніципалітет", "якщо не може забезпечити себе та виконує інші умови", "субсидію на оплату догляду за дитиною")]
    [InlineData("en", "Social assistance in Denmark: benefits and how to apply", "Who can ask for assistance|Cash assistance for necessary living costs|Benefits for families with children|Help with certain necessary expenses|Work, activities and reporting changes|How to apply|Where to get help", "shared minor children in Denmark", "assets of both spouses may still be considered", "are not covered by the separate arbejdspligt regime", "does not remove other integration-programme requirements", "within four weeks", "municipality", "if they cannot support themselves and meet the other conditions", "income-based subsidy towards childcare costs")]
    [InlineData("ru", "Социальная помощь в Дании: выплаты и порядок обращения", "Кто может обратиться за помощью|Денежная помощь на необходимые расходы|Выплаты семьям с детьми|Помощь на отдельные необходимые расходы|Работа, мероприятия и сообщение об изменениях|Как подать заявление|Где получить помощь", "общих несовершеннолетних детей в Дании", "имущество обоих супругов может по-прежнему учитываться", "не подпадают под отдельный режим arbejdspligt", "не отменяет другие требования интеграционной программы", "в течение четырёх недель", "муниципалитет", "если не может обеспечить себя и выполняет остальные условия", "субсидию на оплату ухода за ребёнком")]
    public async Task ArticleRendersSevenSectionsWithQualifiedLocalizedGuidance(
        string language, string expectedTitle, string expectedHeadings, string childrenCondition,
        string spouseAssets, string workObligation, string otherObligations, string appealPeriod, string municipality,
        string eligibilityLimit, string childcarePhrase)
    {
        var catalog = await LoadProductionCatalogAsync();
        var html = await RenderAsync<Category>(catalog, language, "/countries/denmark/categories/social-assistance",
            new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = "denmark",
                [nameof(Category.CategorySlug)] = "social-assistance"
            });
        var visibleHtml = WebUtility.HtmlDecode(html);
        Assert.Contains(expectedTitle, visibleHtml);
        Assert.Contains(childrenCondition, visibleHtml);
        var spouseUkraineCondition = language switch
        {
            "uk" => "залишився/-лася в Україні й не може повністю чи частково допомагати",
            "en" => "spouse remains in Ukraine and cannot contribute, fully or partly",
            "ru" => "остался(-ась) в Украине и не может полностью или частично участвовать",
            _ => throw new ArgumentOutOfRangeException(nameof(language))
        };
        var separationIsNotEnough = language switch
        {
            "uk" => "не відбувається автоматично лише через проживання подружжя окремо",
            "en" => "does not apply automatically just because spouses live apart",
            "ru" => "не применяется автоматически только потому, что супруги живут раздельно",
            _ => throw new ArgumentOutOfRangeException(nameof(language))
        };
        Assert.Contains(spouseUkraineCondition, visibleHtml);
        Assert.Contains(separationIsNotEnough, visibleHtml);
        Assert.Contains(spouseAssets, visibleHtml);
        Assert.Contains(workObligation, visibleHtml);
        Assert.Contains(otherObligations, visibleHtml, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(appealPeriod, visibleHtml);
        Assert.Contains("Udbetaling Danmark", visibleHtml);
        Assert.Contains("kontanthjælp", visibleHtml);
        Assert.Contains(eligibilityLimit, visibleHtml);
        Assert.Contains(childcarePhrase, visibleHtml);
        Assert.Contains("børne- og ungeydelse", visibleHtml);
        Assert.Contains("børnetilskud", visibleHtml);
        Assert.DoesNotContain("L 43", visibleHtml, StringComparison.OrdinalIgnoreCase);

        var headings = Regex.Matches(html, "<h3\\b[^>]*class=\"entry-section-heading\"[^>]*>(.*?)</h3>")
            .Cast<Match>().Select(match => WebUtility.HtmlDecode(match.Groups[1].Value)).ToArray();
        Assert.Equal(expectedHeadings.Split('|'), headings);
        Assert.Equal(RequiredSources.Length, RequiredSources.Count(url => html.Contains($"href=\"{url}\"", StringComparison.Ordinal)));
        Assert.Contains("<ul class=\"entry-list\"", html);
        Assert.Contains(municipality, visibleHtml, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ArticleAppearsInSearchAndCountryComparison()
    {
        var catalog = await LoadProductionCatalogAsync();
        var searchHtml = await RenderAsync<Search>(catalog, "en",
            "/search?q=Social%20assistance%20in%20Denmark&country=denmark&category=social-assistance");
        Assert.Contains("href=\"/countries/denmark/categories/social-assistance\"", searchHtml);
        Assert.Contains("Social assistance in Denmark: benefits and how to apply", WebUtility.HtmlDecode(searchHtml));

        var compareHtml = await RenderAsync<Compare>(catalog, "en",
            "/compare?country1=denmark&country2=germany&category=social-assistance");
        Assert.Contains("Social assistance in Denmark: benefits and how to apply", WebUtility.HtmlDecode(compareHtml));
        Assert.Contains($"href=\"/countries/denmark/categories/social-assistance#entry-{ArticleId:N}\"", compareHtml);
    }

    [Fact]
    public async Task ArticleContentIsSafelyHtmlEncoded()
    {
        var temporaryRoot = Path.Combine(Path.GetTempPath(), "RefugeeNavigatorSocialAssistanceEncoding", Guid.NewGuid().ToString("N"));
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
            english!["Content"] = "## Safe rendering\n\n<script>alert('x')</script>";
            await File.WriteAllTextAsync(entriesPath, entries.ToJsonString());

            var syntheticCatalog = await new JsonCatalogProvider(temporaryRoot).LoadAsync();
            var html = await RenderAsync<Category>(syntheticCatalog, "en", "/countries/denmark/categories/social-assistance",
                new Dictionary<string, object?>
                {
                    [nameof(Category.CountrySlug)] = "denmark",
                    [nameof(Category.CategorySlug)] = "social-assistance"
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

    private static async Task<string> RenderAsync<TComponent>(CatalogData catalog, string language, string currentPath,
        IDictionary<string, object?>? parameters = null) where TComponent : IComponent
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
        public TestNavigationManager(string currentPath)
        {
            Initialize("http://localhost/", $"http://localhost{currentPath}");
        }

        protected override void NavigateToCore(string uri, bool forceLoad) => Uri = ToAbsoluteUri(uri).ToString();
    }
}
