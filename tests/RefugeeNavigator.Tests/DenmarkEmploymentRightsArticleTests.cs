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

public sealed class DenmarkEmploymentRightsArticleTests
{
    private static readonly Guid DenmarkId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly Guid EmploymentCategoryId = Guid.Parse("20000000-0000-0000-0000-000000000002");
    private static readonly Guid ArticleId = Guid.Parse("35da68f6-8880-40b2-9b3f-eb2b809b1caf");
    private static readonly Guid ExistingJobSearchArticleId = Guid.Parse("c2356631-3fa2-4553-8fda-50a4419262c0");

    private static readonly string[] RequiredSources =
    [
        "https://www.nyidanmark.dk/en-GB/You-are-waiting-for-an-answer/Fordreven-fra-Ukraine",
        "https://www.nyidanmark.dk/en-GB/You-want-to-apply/Ukraine/Displaced-from-Ukraine",
        "https://www.nyidanmark.dk/en-GB/You-want-to-apply/Ukraine/Family-to-displaced-from-ukraine",
        "https://www.nyidanmark.dk/en-GB/EmployersAndEducation/Employers/Exemption",
        "https://lifeindenmark.borger.dk/working/work-rights/working-conditions/the-danish-labour-market-model",
        "https://lifeindenmark.borger.dk/working/work-rights/working-conditions/pay",
        "https://lifeindenmark.borger.dk/working/work-rights/working-conditions/working-hours",
        "https://lifeindenmark.borger.dk/working/holiday-allowance-ny/holiday-allowance",
        "https://lifeindenmark.borger.dk/working/work-rights/leave-of-absence/sick-leave-and-sickness-benefit",
        "https://lifeindenmark.borger.dk/working/work-rights/leave-of-absence/sickness-benefits/sickness-benefits-for-a-salary-earner",
        "https://lifeindenmark.borger.dk/working/equality-in-the-workspace/non-discrimination-and-equal-treatment",
        "https://workplacedenmark.dk/health-and-safety/when-working-in-dk",
        "https://lifeindenmark.borger.dk/working/work-rights/complain-about-violations-of-the-working-environment-act",
        "https://workplacedenmark.dk/health-and-safety/industrial-injuries-employees",
        "https://skat.dk/en-us/businesses/employees-and-pay/non-danish-labour/get-a-tax-card-as-a-non-danish-employee",
        "https://skat.dk/en-us/individuals/cross-border-tax-matters/your-guide-to-working-in-denmark",
        "https://star.dk/digital-service/borger-it/find-job-og-joblog",
        "https://lifeindenmark.borger.dk/working/work-rights/unemployment-benefits",
        "https://lifeindenmark.borger.dk/working/the-danish-tax-system/deductions-and-allowances",
        "https://www.retsinformation.dk/eli/lta/2023/501/pdf",
        "https://at.dk/kend-din-rolle/arbejdsgiver/arbejdsgivere-har-pligt-til-at-anmelde-ulykker-naar-de-er-anmeldepligtige/",
        "https://www.aes.dk/nye-regler-anmeldelse-af-arbejdsulykker",
        "https://www.retsinformation.dk/eli/lta/2026/667/pdf",
        "https://www.workindenmark.dk/working-in-denmark/terms-of-employment/holiday-allowance"
    ];

    private static readonly string[] ExcludedSources =
    [
        "https://lifeindenmark.borger.dk/working/work-rights/working-conditions/employment-contracts",
        "https://workplacedenmark.dk/en/health-and-safety/report-a-work-accident/",
        "https://workplacedenmark.dk/health-and-safety/work-accidents-and-insurance",
        "https://skat.dk/en-us/help/forms/04-tax-tax-return-and-preliminary-income-individuals/04063-endk-apply-for-tax-card-and-danish-personal-tax-number",
        "https://www.ft.dk/samling/20261/lovforslag/l43/index.htm",
        "https://www.retsinformation.dk/eli/ft/202612L00043",
        "https://lifeindenmark.borger.dk/working/finding-a-job/finding-and-taking-a-job"
    ];

    [Fact]
    public async Task CatalogHasAcceptedArticleWithThreeTranslationsAndReviewedSourceSet()
    {
        var catalog = await LoadProductionCatalogAsync();
        var article = Assert.Single(catalog.InformationEntries, entry => entry.Id == ArticleId);
        Assert.Equal(DenmarkId, article.CountryId);
        Assert.Equal(EmploymentCategoryId, article.CategoryId);

        var translations = catalog.InformationEntryTranslations
            .Where(translation => translation.InformationEntryId == ArticleId)
            .ToArray();
        Assert.Equal(new[] { "en", "ru", "uk" }, translations.Select(item => item.LanguageCode).OrderBy(code => code));
        Assert.All(translations, translation =>
        {
            Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
            Assert.False(string.IsNullOrWhiteSpace(translation.Title));
            Assert.False(string.IsNullOrWhiteSpace(translation.Summary));
            Assert.False(string.IsNullOrWhiteSpace(translation.Content));
            Assert.Equal(8, Regex.Matches(translation.Content, "(?m)^## ").Count);
            Assert.DoesNotContain("http://", translation.Content, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("https://", translation.Content, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("**", translation.Content, StringComparison.Ordinal);
            Assert.Equal(new DateOnly(2026, 10, 25), DateOnly.FromDateTime(translation.LastUpdatedAt.UtcDateTime));
        });

        var sources = catalog.OfficialSources.Where(source => source.InformationEntryId == ArticleId).ToArray();
        Assert.Equal(24, sources.Length);
        Assert.Equal(24, sources.Select(source => source.Id).Distinct().Count());
        Assert.Equal(24, sources.Select(source => source.Url.AbsoluteUri).Distinct(StringComparer.Ordinal).Count());
        Assert.Equal(RequiredSources.OrderBy(url => url), sources.Select(source => source.Url.AbsoluteUri).OrderBy(url => url));
        Assert.All(sources, source =>
        {
            Assert.Equal(ArticleId, source.InformationEntryId);
            Assert.Equal(new DateOnly(2026, 10, 15), DateOnly.FromDateTime(source.LastCheckedAt.UtcDateTime));
        });
        Assert.DoesNotContain(sources, source => ExcludedSources.Contains(source.Url.AbsoluteUri, StringComparer.Ordinal));

        var previous = Assert.Single(catalog.InformationEntries, entry => entry.Id == ExistingJobSearchArticleId);
        Assert.Equal(DenmarkId, previous.CountryId);
        Assert.Equal(EmploymentCategoryId, previous.CategoryId);
        Assert.Equal(5, catalog.OfficialSources.Count(source => source.InformationEntryId == ExistingJobSearchArticleId));
        Assert.Contains(catalog.InformationEntryTranslations, item =>
            item.InformationEntryId == ExistingJobSearchArticleId && item.LanguageCode == "en" &&
            item.Title == "Find jobs and job-search guidance in Denmark" &&
            item.VerificationStatus == VerificationStatus.Verified);
        Assert.Equal(8, catalog.InformationEntries.Count(entry => entry.CountryId == DenmarkId));
    }

    [Theory]
    [InlineData("uk", "Робота в Данії: дозвіл на працю та права працівників", "Хто може працювати в Данії|Трудовий договір|Зарплата та робочий час|Відпустка, хвороба та умови праці|Податки та реєстрація|Права на роботі та куди звертатися|Якщо робота завершується або ви змінюєте роботодавця|Де отримати допомогу")]
    [InlineData("en", "Working in Denmark: work authorization and employee rights", "Who can work in Denmark|Employment contracts|Salary and working hours|Holidays, sickness and working conditions|Taxes and registration|Workplace rights and where to raise a concern|If a job ends or you change employers|Where to get help")]
    [InlineData("ru", "Работа в Дании: разрешение на работу и права работников", "Кто может работать в Дании|Трудовой договор|Зарплата и рабочее время|Отпуск, болезнь и условия труда|Налоги и регистрация|Права на работе и куда обратиться|Если работа заканчивается или вы меняете работодателя|Где получить помощь")]
    public async Task LocalizedArticleRendersEightSectionsAndSources(string language, string title, string expectedHeadings)
    {
        var catalog = await LoadCatalogWithTargetStatusAsync(language, VerificationStatus.Verified);
        var html = await RenderAsync<Category>(catalog, language,
            "/countries/denmark/categories/employment",
            new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = "denmark",
                [nameof(Category.CategorySlug)] = "employment"
            });
        var articleHtml = ExtractArticle(html, ArticleId);
        var visibleHtml = WebUtility.HtmlDecode(articleHtml);
        AssertEmploymentRightsInlineLinks(catalog, language, articleHtml);

        Assert.Contains(title, visibleHtml);
        var headings = Regex.Matches(articleHtml, "<h3\\b[^>]*class=\"entry-section-heading\"[^>]*>(.*?)</h3>", RegexOptions.Singleline)
            .Cast<Match>()
            .Select(match => WebUtility.HtmlDecode(match.Groups[1].Value))
            .ToArray();
        Assert.Equal(expectedHeadings.Split('|'), headings);
        Assert.Equal(28, Regex.Matches(articleHtml, "<a\\b[^>]*href=\"https://").Count);
        Assert.Contains("entry-metadata", articleHtml);
        Assert.Contains("entry-sources", articleHtml);

        var compareHtml = await RenderAsync<Compare>(catalog, language,
            "/compare?country1=denmark&country2=germany&category=employment&tab=full");
        AssertEmploymentRightsInlineLinks(catalog, language, compareHtml);
        Assert.Contains("href=\"/compare?country1=denmark&amp;country2=germany&amp;category=employment&amp;tab=full#article-section-", compareHtml);
    }

    [Theory]
    [InlineData("uk", "перевищує 3 години на тиждень протягом 4 послідовних тижнів", "гарантованої кількості оплачуваних годин", "протягом 7 календарних днів", "протягом одного місяця", "Кваліфікована колективна угода", "після того, як вона зареєструвала ваші відбитки пальців і фото обличчя", "Сама подана заява не дає безумовного права працювати", "немає встановленої законом загальнонаціональної мінімальної зарплати", "п’ять тижнів", "інші працівники можуть мати оплату за договором або колективною угодою", "Номер CPR", "безпечне та здорове робоче середовище", "працівник також може повідомити про травму самостійно", "не гарантує компенсації", "Членство у профспілці є окремим і також добровільним")]
    [InlineData("en", "more than 3 hours a week over 4 consecutive weeks", "no guaranteed amount of paid hours", "within 7 calendar days", "within one month", "A qualifying collective agreement", "after it has recorded your fingerprints and facial image", "An application by itself is not an unconditional right to work", "no statutory nationwide minimum wage", "five weeks", "or may need to meet separate sickness-benefit conditions", "A CPR number", "safe and healthy working environment", "Under applicable conditions, an injured worker can also report the injury directly", "Reporting does not guarantee compensation", "Membership of a trade union is separate and also voluntary")]
    [InlineData("ru", "превышает 3 часа в неделю за 4 последовательные недели", "гарантированное количество оплачиваемых часов", "в течение 7 календарных дней", "в течение одного месяца", "Квалифицируемое коллективное соглашение", "после того, как у неё зарегистрированы ваши отпечатки пальцев и изображение лица", "Одно лишь заявление не даёт безусловного права на работу", "нет установленной законом общенациональной минимальной зарплаты", "пять недель", "либо должны соответствовать отдельным условиям для пособия по болезни", "Номер CPR", "безопасную и здоровую рабочую среду", "При соблюдении установленных условий работник также может сообщить о травме самостоятельно", "сообщение не гарантирует компенсацию", "Членство в профсоюзе отдельное и также добровольное")]
    public async Task ClaimsKeepMaterialConditionsInAllLanguages(
        string language, string threshold, string noGuaranteedHours, string sevenDays, string oneMonth,
        string collectiveAgreement, string pendingWorkCondition, string noAutomaticPermission,
        string noMinimumWage, string holiday, string sickPayQualification, string cpr,
        string safety, string injurySelfReport, string noCompensationGuarantee, string separateMembership)
    {
        var catalog = await LoadProductionCatalogAsync();
        var translation = Assert.Single(catalog.InformationEntryTranslations,
            item => item.InformationEntryId == ArticleId && item.LanguageCode == language);
        var text = translation.Title + "\n" + translation.Summary + "\n" + translation.Content;

        foreach (var phrase in new[]
        {
            threshold, noGuaranteedHours, sevenDays, oneMonth, collectiveAgreement, pendingWorkCondition,
            noAutomaticPermission, noMinimumWage, holiday, sickPayQualification, cpr, safety,
            injurySelfReport, noCompensationGuarantee, separateMembership
        })
        {
            Assert.Contains(phrase, text, StringComparison.OrdinalIgnoreCase);
        }
        Assert.DoesNotContain("L 43", text, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("L43", text, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task VerifiedInMemoryArticleAppearsInSearchAndCountryComparison()
    {
        var catalog = await LoadCatalogWithTargetStatusAsync("en", VerificationStatus.Verified);
        var searchHtml = await RenderAsync<Search>(catalog, "en",
            "/search?q=work%20authorization%20and%20employee%20rights&country=denmark&category=employment");
        Assert.Contains("href=\"/countries/denmark/categories/employment\"", searchHtml);
        Assert.Contains("Working in Denmark: work authorization and employee rights", WebUtility.HtmlDecode(searchHtml));

        var compareHtml = await RenderAsync<Compare>(catalog, "en",
            "/compare?country1=denmark&country2=germany&category=employment");
        Assert.Contains("Working in Denmark: work authorization and employee rights", WebUtility.HtmlDecode(compareHtml));
        Assert.Contains($"href=\"/countries/denmark/categories/employment#entry-{ArticleId:N}\"", compareHtml);
        AssertEmploymentRightsInlineLinks(catalog, "en", compareHtml);
    }

    [Fact]
    public async Task AcceptedArticleAppearsInSearchAndCountryComparison()
    {
        var catalog = await LoadProductionCatalogAsync();
        var searchHtml = await RenderAsync<Search>(catalog, "en",
            "/search?q=work%20authorization%20and%20employee%20rights&country=denmark&category=employment");
        Assert.Contains("href=\"/countries/denmark/categories/employment\"", searchHtml);
        Assert.Contains("Working in Denmark: work authorization and employee rights", WebUtility.HtmlDecode(searchHtml));
        var compareHtml = await RenderAsync<Compare>(catalog, "en",
            "/compare?country1=denmark&country2=germany&category=employment");
        Assert.Contains("Working in Denmark: work authorization and employee rights", WebUtility.HtmlDecode(compareHtml));
        Assert.Contains($"href=\"/countries/denmark/categories/employment#entry-{ArticleId:N}\"", compareHtml);
        AssertEmploymentRightsInlineLinks(catalog, "en", compareHtml);
    }

    [Fact]
    public async Task ArticleContentIsSafelyHtmlEncoded()
    {
        var temporaryRoot = Path.Combine(Path.GetTempPath(), "RefugeeNavigatorEmploymentEncoding", Guid.NewGuid().ToString("N"));
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
            var html = await RenderAsync<Category>(syntheticCatalog, "en",
                "/countries/denmark/categories/employment",
                new Dictionary<string, object?>
                {
                    [nameof(Category.CountrySlug)] = "denmark",
                    [nameof(Category.CategorySlug)] = "employment"
                });
            Assert.Contains("&lt;script&gt;alert(&#x27;x&#x27;)&lt;/script&gt;", html);
            Assert.DoesNotContain("<script>", html);
        }
        finally
        {
            Directory.Delete(temporaryRoot, recursive: true);
        }
    }

    private static async Task<CatalogData> LoadCatalogWithTargetStatusAsync(string language, VerificationStatus status)
    {
        var temporaryRoot = Path.Combine(Path.GetTempPath(), "RefugeeNavigatorEmploymentStatus", Guid.NewGuid().ToString("N"));
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
            var translation = article!["Translations"]!.AsArray()
                .Single(item => item!["LanguageCode"]!.GetValue<string>() == language);
            translation!["VerificationStatus"] = (int)status;
            await File.WriteAllTextAsync(entriesPath, entries.ToJsonString());
            return await new JsonCatalogProvider(temporaryRoot).LoadAsync();
        }
        finally
        {
            Directory.Delete(temporaryRoot, recursive: true);
        }
    }

    private static void AssertEmploymentRightsInlineLinks(CatalogData catalog, string language, string html)
    {
        var expected = language switch
        {
            "en" => new[]
            {
                (Guid.Parse("b0ceabf9-2608-4748-86f7-e5e826eb6641"), "the Danish Immigration Service"),
                (Guid.Parse("2acf95dd-1fea-45bc-9478-fda84be4496d"), "written-information rules"),
                (Guid.Parse("dd5b7430-e9ae-4f48-9e14-3a511ae5fc96"), "a tax card"),
                (Guid.Parse("25cfdf97-36ca-4f82-8b61-85089d022079"), "anonymous complaints about suspected violations")
            },
            "uk" => new[]
            {
                (Guid.Parse("b0ceabf9-2608-4748-86f7-e5e826eb6641"), "Імміграційна служба Данії"),
                (Guid.Parse("2acf95dd-1fea-45bc-9478-fda84be4496d"), "Вимоги щодо письмового інформування"),
                (Guid.Parse("dd5b7430-e9ae-4f48-9e14-3a511ae5fc96"), "податкової картки"),
                (Guid.Parse("25cfdf97-36ca-4f82-8b61-85089d022079"), "анонімні скарги про ймовірні порушення")
            },
            "ru" => new[]
            {
                (Guid.Parse("b0ceabf9-2608-4748-86f7-e5e826eb6641"), "Иммиграционная служба Дании"),
                (Guid.Parse("2acf95dd-1fea-45bc-9478-fda84be4496d"), "Требования о письменном информировании"),
                (Guid.Parse("dd5b7430-e9ae-4f48-9e14-3a511ae5fc96"), "налоговой карты"),
                (Guid.Parse("25cfdf97-36ca-4f82-8b61-85089d022079"), "анонимные жалобы о предполагаемых нарушениях")
            },
            _ => throw new ArgumentOutOfRangeException(nameof(language))
        };
        var translation = Assert.Single(catalog.InformationEntryTranslations,
            item => item.InformationEntryId == ArticleId && item.LanguageCode == language);
        var tokenIds = Regex.Matches(translation.Content, @"\[\[source:([0-9a-f-]+)\|")
            .Cast<Match>().Select(match => Guid.Parse(match.Groups[1].Value)).Order().ToArray();
        Assert.Equal(expected.Select(item => item.Item1).Order().ToArray(), tokenIds);
        Assert.Equal(4, tokenIds.Length);
        var decodedHtml = WebUtility.HtmlDecode(html);
        foreach (var (sourceId, label) in expected)
        {
            var source = Assert.Single(catalog.OfficialSources,
                item => item.Id == sourceId && item.InformationEntryId == ArticleId);
            Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme);
            var linkPattern = $"<a\\b(?=[^>]*class=\\\"article-source-link\\\")(?=[^>]*href=\\\"{Regex.Escape(source.Url.AbsoluteUri)}\\\")[^>]*>{Regex.Escape(label)}(?:<span\\b|</a>)";
            Assert.Matches(linkPattern, decodedHtml);
            Assert.Contains(label, decodedHtml);
        }
    }

    private static Task<CatalogData> LoadProductionCatalogAsync() =>
        new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();

    private static string ExtractArticle(string html, Guid id)
    {
        var match = Regex.Match(html,
            $"<article class=\"information-entry\" aria-labelledby=\"entry-heading-{id:N}\"[^>]*>(.*?)</article>",
            RegexOptions.Singleline);
        Assert.True(match.Success, $"Article {id} was not rendered.");
        return match.Groups[1].Value;
    }

    private static async Task<string> RenderAsync<TComponent>(
        CatalogData catalog, string language, string currentPath, IDictionary<string, object?>? parameters = null)
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
            await ComparisonTestServices.RegisterAsync(services);
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
