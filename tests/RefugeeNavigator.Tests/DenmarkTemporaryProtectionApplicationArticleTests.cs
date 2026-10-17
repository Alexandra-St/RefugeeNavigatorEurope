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

public sealed class DenmarkTemporaryProtectionApplicationArticleTests
{
    private static readonly Guid ArticleId = Guid.Parse("afc14824-4035-4ca7-9662-6d5352ba63cb");
    private static readonly Guid DenmarkId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly Guid CategoryId = Guid.Parse("20000000-0000-0000-0000-000000000001");
    private static readonly Guid ExistingAfterProtectionId = Guid.Parse("2d106fd3-e981-4ef9-9733-568b654dfca5");
    private static readonly string[] ExistingDenmarkArticleIds =
    [
        "c2356631-3fa2-4553-8fda-50a4419262c0",
        "2d106fd3-e981-4ef9-9733-568b654dfca5",
        "b1e4aed1-bbaf-4f09-9468-74000bf96430",
        "19afdd19-e2cc-4da6-bde3-6dc3d64b7503",
        "1a19c1f9-1111-4d60-8490-da548f33083f",
        "fee9dcf0-447d-4402-92a2-8b1bec125944",
        "35da68f6-8880-40b2-9b3f-eb2b809b1caf",
    ];
    private static readonly string[] ExcludedSourceUrls =
    [
        "https://www.nyidanmark.dk/en-GB/News-Front-Page/2025/10/Extension-of-residence-permit-under-the-Ukraine-Special-Act",
        "https://www.ft.dk/samling/20261/lovforslag/l43/index.htm",
        "https://www.retsinformation.dk/eli/ft/202612L00043"
    ];

    [Fact]
    public async Task CatalogContainsVerifiedCandidateWithFifteenSourcesAndPreservesExistingArticles()
    {
        var catalog = await LoadProductionCatalogAsync();
        var article = Assert.Single(catalog.InformationEntries, entry => entry.Id == ArticleId);
        Assert.Equal(DenmarkId, article.CountryId);
        Assert.Equal(CategoryId, article.CategoryId);

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
            Assert.Equal(7, Regex.Matches(translation.Content, "(?m)^## ").Count);
            Assert.Equal(new DateOnly(2026, 10, 15), DateOnly.FromDateTime(translation.LastUpdatedAt.UtcDateTime));
            Assert.DoesNotContain("L 43", translation.Title + translation.Summary + translation.Content);
        });

        var sources = catalog.OfficialSources.Where(source => source.InformationEntryId == ArticleId).ToArray();
        Assert.Equal(15, sources.Length);
        Assert.Equal(15, sources.Select(source => source.Id).Distinct().Count());
        Assert.Equal(15, sources.Select(source => source.Url.AbsoluteUri).Distinct(StringComparer.Ordinal).Count());
        Assert.Contains(sources, source => source.Url.AbsoluteUri == "https://www.retsinformation.dk/api/pdf/229946");
        Assert.Contains(sources, source => source.Url.AbsoluteUri == "https://www.retsinformation.dk/eli/lta/2026/453/pdf");
        Assert.All(sources, source =>
        {
            Assert.Equal(ArticleId, source.InformationEntryId);
            Assert.Equal(new DateOnly(2026, 10, 15), DateOnly.FromDateTime(source.LastCheckedAt.UtcDateTime));
            Assert.DoesNotContain(source.Url.AbsoluteUri, ExcludedSourceUrls);
        });

        Assert.All(ExistingDenmarkArticleIds, id =>
            Assert.Contains(catalog.InformationEntries, entry => entry.Id == Guid.Parse(id) && entry.CountryId == DenmarkId));
        Assert.Equal(8, catalog.InformationEntries.Count(entry => entry.CountryId == DenmarkId));
        var previous = Assert.Single(catalog.InformationEntries, entry => entry.Id == ExistingAfterProtectionId);
        Assert.Equal(CategoryId, previous.CategoryId);
        Assert.Contains(catalog.InformationEntryTranslations, item =>
            item.InformationEntryId == ExistingAfterProtectionId && item.LanguageCode == "en" &&
            item.Title == "Residence options after temporary protection in Denmark" &&
            item.VerificationStatus == VerificationStatus.Verified);
    }

    [Theory]
    [InlineData("uk", "Хто може подати заяву|Як подати заяву|Поки триває розгляд|Що відбувається після схвалення|Продовження та строк дії дозволу|Члени сім’ї та особливі обставини|Де отримати допомогу", "Перевірено")]
    [InlineData("en", "Who can apply|How to submit an application|While waiting for a decision|What happens after approval|Extension and permit validity|Family members and special circumstances|Where to get help", "Reviewed")]
    [InlineData("ru", "Кто может подать заявление|Как подать заявление|Пока заявление рассматривается|Что происходит после одобрения|Продление и срок действия разрешения|Члены семьи и особые обстоятельства|Где получить помощь", "Проверено")]
    public async Task LocalizedArticleRendersSevenSectionsAndBothAttributedExtensionDates(
        string language, string expectedHeadings, string expectedStatus)
    {
        var catalog = await LoadProductionCatalogAsync();
        var translation = Assert.Single(catalog.InformationEntryTranslations,
            item => item.InformationEntryId == ArticleId && item.LanguageCode == language);
        var html = await RenderAsync<Category>(catalog, language,
            "/countries/denmark/categories/temporary-protection",
            new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = "denmark",
                [nameof(Category.CategorySlug)] = "temporary-protection"
            });
        var articleMatch = Regex.Match(
            html,
            $"<article class=\"information-entry\" aria-labelledby=\"entry-heading-{ArticleId:N}\"[^>]*>(.*?)</article>",
            RegexOptions.Singleline);
        Assert.True(articleMatch.Success);
        var articleHtml = articleMatch.Groups[1].Value;
        var visibleHtml = WebUtility.HtmlDecode(articleHtml);

        Assert.Contains(translation.Title, visibleHtml);
        Assert.Contains(expectedStatus, visibleHtml);
        Assert.Contains("2028", translation.Content);
        Assert.Contains("2027", translation.Content);
        var extensionParagraphs = translation.Content.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
        Assert.Contains(extensionParagraphs, paragraph => paragraph.Contains("2028", StringComparison.Ordinal) && paragraph.Contains("BEK", StringComparison.Ordinal));
        var extensionGuarantee = language switch
        {
            "uk" => "Це можлива дата продовження за регламентом, а не гарантія",
            "en" => "This is a possible extension date under the regulation, not a guarantee",
            "ru" => "Это возможная дата продления по регламенту, а не гарантия",
            _ => throw new ArgumentOutOfRangeException(nameof(language))
        };
        Assert.Contains(extensionGuarantee, translation.Content, StringComparison.OrdinalIgnoreCase);
        var noAutomaticExtension = language switch
        {
            "uk" => "Це можлива дата продовження за регламентом, а не гарантія, що саме такою буде особиста дата завершення кожного дозволу",
            "en" => "This is a possible extension date under the regulation, not a guarantee that every permit holder has that personal expiry date",
            "ru" => "Это возможная дата продления по регламенту, а не гарантия, что именно такова личная дата окончания каждого разрешения",
            _ => throw new ArgumentOutOfRangeException(nameof(language))
        };
        Assert.Contains(noAutomaticExtension, translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("2027", translation.Content, StringComparison.Ordinal);
        var localizedAgencyName = language switch
        {
            "uk" => "Імміграційної служби",
            "en" => "Immigration Service",
            "ru" => "Иммиграционной службы",
            _ => throw new ArgumentOutOfRangeException(nameof(language))
        };
        Assert.Contains(extensionParagraphs, paragraph => paragraph.Contains("2027", StringComparison.Ordinal) && paragraph.Contains(localizedAgencyName, StringComparison.OrdinalIgnoreCase));
        Assert.Contains("BEK", translation.Content);
        var headings = Regex.Matches(articleHtml, "<h3\\b[^>]*class=\"entry-section-heading\"[^>]*>(.*?)</h3>")
            .Cast<Match>().Select(match => WebUtility.HtmlDecode(match.Groups[1].Value)).ToArray();
        Assert.Equal(expectedHeadings.Split('|'), headings);
        Assert.DoesNotContain("L 43", visibleHtml);
        Assert.Equal(15, catalog.OfficialSources.Where(source => source.InformationEntryId == ArticleId)
            .Count(source => articleHtml.Contains($"href=\"{source.Url.AbsoluteUri}\"", StringComparison.Ordinal)));
        Assert.DoesNotContain("<script", html, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("uk", "SL1", "SL2", "§ 1", "§ 2", "23–59", "25 червня 2026", "Право працювати під час розгляду має умови")]
    [InlineData("en", "SL1", "SL2", "section 1", "section 2", "23–59", "25 June 2026", "The right to work while waiting is conditional")]
    [InlineData("ru", "SL1", "SL2", "§ 1", "§ 2", "23–59", "25 июня 2026", "Право работать во время рассмотрения заявления имеет условия")]
    public async Task EligibilityRoutesWorkAndMilitaryConditionsRemainQualified(
        string language, string sectionOneForm, string sectionTwoForm, string sectionOneReference, string sectionTwoReference, string ageBand,
        string applicationDate, string conditionalWork)
    {
        var catalog = await LoadProductionCatalogAsync();
        var translation = Assert.Single(catalog.InformationEntryTranslations,
            item => item.InformationEntryId == ArticleId && item.LanguageCode == language);
        Assert.Contains(sectionOneForm, translation.Content);
        Assert.Contains(sectionTwoForm, translation.Content);
        Assert.Contains(ageBand, translation.Content);
        Assert.Contains(applicationDate, translation.Content);
        Assert.Contains(conditionalWork, translation.Content);
        Assert.Contains(sectionOneReference, translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(sectionTwoReference, translation.Content, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("uk", "негайно прочитайте рішення", "інструкцій щодо оскарження", "не гарантує права залишатися")]
    [InlineData("en", "read the decision immediately", "follow the specific appeal instructions in it", "does not by itself guarantee permission to remain")]
    [InlineData("ru", "немедленно прочитайте решение", "следуйте приведённым в нём конкретным инструкциям", "не гарантирует право оставаться")]
    public async Task AppealAdviceHasNoUnverifiedNumericDeadlinesOrPromiseToRemain(
        string language, string readPrompt, string instructionsPrompt, string noGuarantee)
    {
        var catalog = await LoadProductionCatalogAsync();
        var translation = Assert.Single(catalog.InformationEntryTranslations,
            item => item.InformationEntryId == ArticleId && item.LanguageCode == language);
        Assert.Contains(readPrompt, translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(instructionsPrompt, translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(noGuarantee, translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("8 weeks", translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("7 days", translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("вісім тижнів", translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("сім днів", translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("восемь недель", translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("семь дней", translation.Content, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("uk", "порядок оскарження залежить від виду рішення", "сам факт подання скарги не гарантує права залишатися", "чинність строків і наслідків", "не вдалося незалежно підтвердити")]
    [InlineData("en", "appeal procedures depend on the type of decision", "filing an appeal does not by itself guarantee permission to remain", "current validity of the deadlines and effects", "could not be independently confirmed")]
    [InlineData("ru", "порядок обжалования зависит от вида решения", "сам факт подачи апелляции не гарантирует право оставаться", "актуальность указанных сроков и последствий", "подтвердить актуальность")]
    public async Task AppealCopyUsesDecisionSpecificGuidanceWithoutTheOutdatedDisclaimer(
        string language, string decisionSpecificGuidance, string noAutomaticRightToRemain,
        string obsoleteDisclaimerFragmentOne, string obsoleteDisclaimerFragmentTwo)
    {
        var catalog = await LoadProductionCatalogAsync();
        var translation = Assert.Single(catalog.InformationEntryTranslations,
            item => item.InformationEntryId == ArticleId && item.LanguageCode == language);
        Assert.Contains(decisionSpecificGuidance, translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(noAutomaticRightToRemain, translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(obsoleteDisclaimerFragmentOne, translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(obsoleteDisclaimerFragmentTwo, translation.Content, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("uk", "За § 57(5) Закону про інтеграцію", "дозволу за § 2(1)", "дуже особливих особистих обставин", "не тотожне праву на житло", "окремому матеріалі про житло в Данії", "Бажаний муніципалітет або конкретне помешкання не гарантуються", "звичайне розміщення за § 12 не застосовується")]
    [InlineData("en", "Under section 57(5) of the Integration Act", "permit under section 2(1)", "very special personal circumstances", "separate from entitlement to accommodation", "separate Denmark Housing guide", "A preferred municipality or particular dwelling is not guaranteed", "ordinary section 12 accommodation placement does not apply")]
    [InlineData("ru", "Согласно § 57(5) Закона об интеграции", "разрешению по § 2(1)", "очень особых личных обстоятельств", "не означает право на жильё", "отдельной статье о жилье в Дании", "Предпочтительный муниципалитет или конкретное жильё не гарантируются", "обычное размещение по § 12 не применяется")]
    public async Task ApprovedRouteHousingGuidanceIsScopedAndRenderedForEachLanguage(
        string language, string sectionReference, string permitBasis, string exception,
        string assignmentQualification, string housingGuideReference, string noPlacementGuarantee,
        string sectionTwelveExclusion)
    {
        var catalog = await LoadProductionCatalogAsync();
        var translation = Assert.Single(catalog.InformationEntryTranslations,
            item => item.InformationEntryId == ArticleId && item.LanguageCode == language);
        Assert.Contains(sectionReference, translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(permitBasis, translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(exception, translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(assignmentQualification, translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(housingGuideReference, translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(noPlacementGuarantee, translation.Content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(sectionTwelveExclusion, translation.Content, StringComparison.OrdinalIgnoreCase);

        var html = await RenderAsync<Category>(catalog, language,
            "/countries/denmark/categories/temporary-protection",
            new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = "denmark",
                [nameof(Category.CategorySlug)] = "temporary-protection"
            });
        var visibleHtml = WebUtility.HtmlDecode(html);
        Assert.Contains(permitBasis, visibleHtml, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<script", html, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ArticleAppearsInSearchAndCountryComparison()
    {
        var catalog = await LoadProductionCatalogAsync();
        var searchHtml = await RenderAsync<Search>(catalog, "en",
            "/search?q=Temporary%20protection%20in%20Denmark%3A%20how%20to%20apply&country=denmark&category=temporary-protection");
        Assert.Contains("/countries/denmark/categories/temporary-protection", searchHtml);
        Assert.Contains("Temporary protection in Denmark: how to apply and extend your permit", WebUtility.HtmlDecode(searchHtml));

        var compareHtml = await RenderAsync<Compare>(catalog, "en",
            "/compare?country1=denmark&country2=germany&category=temporary-protection");
        Assert.Contains("Temporary protection in Denmark: how to apply and extend your permit", WebUtility.HtmlDecode(compareHtml));
        Assert.Contains($"/countries/denmark/categories/temporary-protection#entry-{ArticleId:N}", compareHtml);
    }

    [Fact]
    public async Task ArticleContentIsSafelyHtmlEncoded()
    {
        var temporaryRoot = Path.Combine(Path.GetTempPath(), "RefugeeNavigatorTemporaryProtectionEncoding", Guid.NewGuid().ToString("N"));
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
                "/countries/denmark/categories/temporary-protection",
                new Dictionary<string, object?>
                {
                    [nameof(Category.CountrySlug)] = "denmark",
                    [nameof(Category.CategorySlug)] = "temporary-protection"
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
