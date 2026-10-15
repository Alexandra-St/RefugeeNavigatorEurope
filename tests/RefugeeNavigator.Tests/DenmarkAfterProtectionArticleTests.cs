using System.Globalization;
using System.Net;
using System.Reflection;
using System.Text.Json.Nodes;
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
    private static readonly Guid HealthcareCategoryId = Guid.Parse("20000000-0000-0000-0000-000000000003");
    private static readonly Guid HealthcareArticleId = Guid.Parse("b1e4aed1-bbaf-4f09-9468-74000bf96430");
    private static readonly string[] RequiredSources =
    [
        "https://www.retsinformation.dk/eli/lta/2026/828",
        "https://nyidanmark.dk/en-GB/Words-and-concepts/F%C3%A6lles/Information-to-Ukrainian-citizens",
        "https://nyidanmark.dk/en-GB/You-want-to-apply/Work/Labout-Market-Attachment",
        "https://nyidanmark.dk/en-GB/You-want-to-apply/Study/Educational-attachment-and-change-of-education/Educational-Attachment",
        "https://nyidanmark.dk/en-GB/You-want-to-apply/Work/Pay-limit-scheme",
        "https://nyidanmark.dk/en-GB/You-want-to-apply/Study/Higher-Education"
    ];
    private static readonly string[] RequiredHealthcareSources =
    [
        "https://www.ism.dk/nyheder/2022/april/sundhedshjaelp-til-ukrainske-flygtninge",
        "https://www.borger.dk/udlaendinge-i-danmark/flytte-til-danmark/sygesikring-for-udlaendinge-i-danmark",
        "https://lifeindenmark.borger.dk/theme/when-you-arrive",
        "https://lifeindenmark.borger.dk/settle-in-denmark/ics-international-citizen-service/ics-east-in-copenhagen",
        "https://www.borger.dk/sundhed-og-sygdom/sundhedskort--laegevalg-og-tandlaege/nyt-sundhedskort",
        "https://www.borger.dk/sundhed-og-sygdom/sundhedskort--laegevalg-og-tandlaege/valg-af-laege",
        "https://lifeindenmark.borger.dk/healthcare/Emergencies",
        "https://www.borger.dk/sundhed-og-sygdom/dine-rettigheder-som-patient/Tolkning-for-patienter",
        "https://www.borger.dk/sundhed-og-sygdom/sundhedskort--laegevalg-og-tandlaege/sygesikring-og-sikringsgrupper"
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
    [InlineData("uk", "Які є варіанти проживання після тимчасового захисту в Данії?", "BEK nr. 828/2026", "17 березня 2028 року", "17 березня 2027 року", "Перевірено", "Джерело востаннє перевірено", "Тимчасове проживання та строки продовження|Варіанти проживання, пов’язані з роботою|Варіанти проживання, пов’язані з навчанням|Інші дозволи на проживання|Постійне проживання|Де знайти офіційну інформацію")]
    [InlineData("en", "Residence options after temporary protection in Denmark", "BEK no. 828/2026", "17 March 2028", "17 March 2027", "Reviewed", "Source last checked", "Temporary residence and extension dates|Employment-related residence options|Education-related residence options|Other residence permits|Permanent residence|Where to find official information")]
    [InlineData("ru", "Какие есть варианты проживания после временной защиты в Дании?", "BEK № 828/2026", "17 марта 2028 года", "17 марта 2027 года", "Проверено", "Источник проверен", "Временное проживание и сроки продления|Варианты проживания, связанные с работой|Варианты проживания, связанные с обучением|Другие разрешения на проживание|Постоянное проживание|Где найти официальную информацию")]
    public async Task LocalizedCategoryRendersArticleStatusDatesAndOfficialSources(
        string language,
        string expectedTitle,
        string expectedRegulation,
        string expectedExtensionDate,
        string expectedAgencyDate,
        string expectedStatus,
        string expectedSourceLabel,
        string expectedHeadings)
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
        Assert.Single(System.Text.RegularExpressions.Regex.Matches(html, "<h1\\b").Cast<System.Text.RegularExpressions.Match>());
        Assert.Single(System.Text.RegularExpressions.Regex.Matches(html, "<h2\\b").Cast<System.Text.RegularExpressions.Match>());
        Assert.True(System.Text.RegularExpressions.Regex.Matches(html, "<h3\\b").Count == 7);
        Assert.True(System.Text.RegularExpressions.Regex.Matches(html, "<ul\\b").Count == 3);
        Assert.True(System.Text.RegularExpressions.Regex.Matches(html, "<li\\b").Count == 12);
        Assert.All(expectedHeadings.Split('|'), heading =>
            Assert.Matches($"<h3\\b[^>]*class=\"entry-section-heading\"[^>]*>{System.Text.RegularExpressions.Regex.Escape(heading)}</h3>", visibleHtml));
    }

    [Fact]
    public async Task ExistingEmploymentArticleRemainsVerifiedAndPlainText()
    {
        var catalog = await LoadProductionCatalogAsync();
        var employmentTranslation = Assert.Single(catalog.InformationEntryTranslations,
            translation => translation.InformationEntryId == ExistingEmploymentEntryId && translation.LanguageCode == "en");
        Assert.Equal(VerificationStatus.Verified, employmentTranslation.VerificationStatus);
        Assert.Contains("Workindenmark provides job listings in English", employmentTranslation.Content);

        var html = await RenderAsync<Category>(catalog, "en", "/countries/denmark/categories/employment",
            new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = "denmark",
                [nameof(Category.CategorySlug)] = "employment"
            });
        Assert.Contains("Workindenmark provides job listings in English", WebUtility.HtmlDecode(html));
        Assert.DoesNotContain("entry-section-heading", html);
        Assert.DoesNotContain("class=\"entry-list\"", html);
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

    [Fact]
    public async Task HealthcareArticleHasThreeCompleteVerifiedTranslationsAndNineSources()
    {
        var catalog = await LoadProductionCatalogAsync();
        var article = Assert.Single(catalog.InformationEntries, entry => entry.Id == HealthcareArticleId);
        Assert.Equal(DenmarkId, article.CountryId);
        Assert.Equal(HealthcareCategoryId, article.CategoryId);

        var translations = catalog.InformationEntryTranslations
            .Where(translation => translation.InformationEntryId == HealthcareArticleId)
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

        var sources = catalog.OfficialSources.Where(source => source.InformationEntryId == HealthcareArticleId).ToArray();
        Assert.Equal(RequiredHealthcareSources.OrderBy(url => url), sources.Select(source => source.Url.AbsoluteUri).OrderBy(url => url));
        Assert.All(sources, source =>
        {
            Assert.Equal(HealthcareArticleId, source.InformationEntryId);
            Assert.Equal(new DateOnly(2026, 10, 15), DateOnly.FromDateTime(source.LastCheckedAt.UtcDateTime));
        });
    }

    [Theory]
    [InlineData("uk", "Картка підтверджує право", "цифровий застосунок картки", "картку замовляють і надсилають на вашу адресу в Данії", "Зазвичай доставка триває кілька тижнів", "у своєму місцевому Borgerservice")]
    [InlineData("en", "The card documents your access", "A digital card app is also available", "the card is ordered and sent to your Danish address", "Delivery generally takes several weeks", "check with your local Borgerservice")]
    [InlineData("ru", "Карточка подтверждает право", "доступно цифровое приложение карточки", "карточку заказывают и отправляют на ваш адрес в Дании", "Обычно доставка занимает несколько недель", "уточните это в местном Borgerservice")]
    public async Task YellowCardInformationAppearsOncePerTranslation(
        string language,
        string expectedPurpose,
        string expectedDigitalCard,
        string expectedDelivery,
        string expectedTiming,
        string expectedBorgerserviceAdvice)
    {
        var catalog = await LoadProductionCatalogAsync();
        var translation = Assert.Single(catalog.InformationEntryTranslations,
            item => item.InformationEntryId == HealthcareArticleId && item.LanguageCode == language);

        Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
        Assert.Contains(expectedPurpose, translation.Content);
        Assert.Contains(expectedDigitalCard, translation.Content);
        Assert.Contains(expectedTiming, translation.Content);
        Assert.Contains(expectedBorgerserviceAdvice, translation.Content);
        Assert.Equal(1, translation.Content.Split(expectedDelivery, StringSplitOptions.None).Length - 1);
    }

    [Theory]
    [InlineData("uk", "Як отримати медичну допомогу в Данії", "Почніть із реєстрації в CPR і медичної картки|Жовта медична картка (sundhedskort)|Як знайти сімейного лікаря та звернутися до нього|Невідкладна та екстрена допомога|Усний переклад і мовна допомога|Витрати та покриття", "Стан або травма, що загрожує життю: телефонуйте 112", "Номери й порядок звернення відрізняються залежно від регіону", "Перевірено")]
    [InlineData("en", "How to access healthcare in Denmark", "Start with CPR registration and your healthcare card|The yellow health card (sundhedskort)|Find and contact your GP|Urgent and emergency care|Interpretation and language support|Costs and coverage", "Life-threatening illness or injury: call 112", "out-of-hours medical service for your region", "Reviewed")]
    [InlineData("ru", "Как получить медицинскую помощь в Дании", "Начните с регистрации в CPR и медицинской карточки|Жёлтая медицинская карточка (sundhedskort)|Как найти семейного врача и обратиться к нему|Срочная и экстренная помощь|Устный перевод и языковая помощь|Расходы и покрытие", "Состояние или травма, угрожающие жизни: звоните 112", "службу медицинской помощи в нерабочее время вашего региона", "Проверено")]
    public async Task HealthcareArticleRendersLocalizedSectionsEmergencyGuidanceAndSources(
        string language,
        string expectedTitle,
        string expectedHeadings,
        string expected112,
        string expectedRegionalGuidance,
        string expectedStatus)
    {
        var catalog = await LoadProductionCatalogAsync();
        var html = await RenderAsync<Category>(catalog, language,
            "/countries/denmark/categories/healthcare",
            new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = "denmark",
                [nameof(Category.CategorySlug)] = "healthcare"
            });
        var visibleHtml = WebUtility.HtmlDecode(html);

        Assert.Contains(expectedTitle, visibleHtml);
        Assert.Contains(expectedStatus, visibleHtml);
        Assert.Contains(expected112, visibleHtml);
        Assert.Contains(expectedRegionalGuidance, visibleHtml);
        var headings = System.Text.RegularExpressions.Regex.Matches(html, "<h3\\b[^>]*class=\"entry-section-heading\"[^>]*>(.*?)</h3>")
            .Cast<System.Text.RegularExpressions.Match>()
            .Select(match => WebUtility.HtmlDecode(match.Groups[1].Value))
            .ToArray();
        Assert.Equal(expectedHeadings.Split('|'), headings);
        Assert.Single(System.Text.RegularExpressions.Regex.Matches(html, "<ul\\b[^>]*class=\"entry-list\"").Cast<System.Text.RegularExpressions.Match>());
        var entryList = System.Text.RegularExpressions.Regex.Match(html, "<ul\\b[^>]*class=\"entry-list\"[^>]*>(.*?)</ul>", System.Text.RegularExpressions.RegexOptions.Singleline);
        Assert.True(entryList.Success);
        var listItems = System.Text.RegularExpressions.Regex.Matches(entryList.Groups[1].Value, "<li\\b");
        Assert.Collection(listItems, _ => { }, _ => { }, _ => { }, _ => { });
        Assert.Equal(RequiredHealthcareSources.Length, RequiredHealthcareSources.Count(url => html.Contains($"href=\"{url}\"", StringComparison.Ordinal)));
        Assert.DoesNotContain("<script>", html);
        Assert.DoesNotContain("<img", html);
    }

    [Fact]
    public async Task HealthcareArticleAppearsInSearchAndCountryComparison()
    {
        var catalog = await LoadProductionCatalogAsync();
        var searchHtml = await RenderAsync<Search>(catalog, "en",
            "/search?q=How%20to%20access%20healthcare%20in%20Denmark&country=denmark&category=healthcare");
        Assert.Contains("href=\"/countries/denmark/categories/healthcare\"", searchHtml);
        Assert.Contains("How to access healthcare in Denmark", WebUtility.HtmlDecode(searchHtml));

        var compareHtml = await RenderAsync<Compare>(catalog, "en",
            "/compare?country1=denmark&country2=germany&category=healthcare");
        Assert.Contains("How to access healthcare in Denmark", WebUtility.HtmlDecode(compareHtml));
        Assert.Contains("href=\"/countries/denmark/categories/healthcare\"", compareHtml);
    }

    [Fact]
    public async Task HealthcareArticleRendersUntrustedContentAsEncodedText()
    {
        var temporaryRoot = Path.Combine(Path.GetTempPath(), "RefugeeNavigatorHealthcareEncoding", Guid.NewGuid().ToString("N"));
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
            var article = entries.Single(entry => entry!["Id"]!.GetValue<string>() == HealthcareArticleId.ToString());
            var english = article!["Translations"]!.AsArray()
                .Single(translation => translation!["LanguageCode"]!.GetValue<string>() == "en");
            english!["Content"] = "<script>alert('x')</script>";
            await File.WriteAllTextAsync(entriesPath, entries.ToJsonString());

            var syntheticCatalog = await new JsonCatalogProvider(temporaryRoot).LoadAsync();
            var html = await RenderAsync<Category>(syntheticCatalog, "en",
                "/countries/denmark/categories/healthcare",
                new Dictionary<string, object?>
                {
                    [nameof(Category.CountrySlug)] = "denmark",
                    [nameof(Category.CategorySlug)] = "healthcare"
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
