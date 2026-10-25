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

public sealed class DenmarkEducationArticleTests
{
    private static readonly Guid DenmarkId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly Guid EducationCategoryId = Guid.Parse("20000000-0000-0000-0000-000000000005");
    private static readonly Guid ArticleId = Guid.Parse("fee9dcf0-447d-4402-92a2-8b1bec125944");
    private static readonly string[] ExistingDenmarkArticleIds =
    [
        "c2356631-3fa2-4553-8fda-50a4419262c0",
        "2d106fd3-e981-4ef9-9733-568b654dfca5",
        "b1e4aed1-bbaf-4f09-9468-74000bf96430",
        "19afdd19-e2cc-4da6-bde3-6dc3d64b7503",
        "1a19c1f9-1111-4d60-8490-da548f33083f",
        "35da68f6-8880-40b2-9b3f-eb2b809b1caf",
    ];
    private static readonly string[] RequiredSources =
    [
        "https://uvm.dk/aktuelt/i-fokus/ukraine/vejledning/grundskolen/adgang-til-undervisning",
        "https://uvm.dk/aktuelt/i-fokus/ukraine/vejledning/grundskolen/indskrivning-af-elever-fordrevet-fra-ukraine/",
        "https://uvm.dk/aktuelt/i-fokus/ukraine/vejledning/grundskolen/sprog-og-tolkebistand/",
        "https://uvm.dk/aktuelt/i-fokus/ukraine/vejledning/grundskolen/modtagelsesklasser-og-saerlige-hold/",
        "https://uvm.dk/grundskole/folkeskolen/om-folkeskolen/skolestart/",
        "https://lifeindenmark.borger.dk/school-and-education/school/enrolment-to-start-school",
        "https://lifeindenmark.borger.dk/family-and-children/childcare/family-daycare--nurseries-and-kindergartens",
        "https://uvm.dk/aktuelt/i-fokus/ukraine/vejledning/dagtilbud/pasningsgaranti",
        "https://lifeindenmark.borger.dk/family-and-children/childcare/income-based-daycare-subsidy--sibling-subsidy-and-other-subsidies",
        "https://lifeindenmark.borger.dk/school-and-education/upper-secondary-education/the-higher-general-education-programme---stx",
        "https://lifeindenmark.borger.dk/school-and-education/upper-secondary-education/vocational-education-and-training---vet---courses",
        "https://lifeindenmark.borger.dk/leisure-and-networking/danish-language-training",
        "https://danskogproever.dk/sprogcenter/om-danskuddannelserne/danskuddannelserne/",
        "https://danskogproever.dk/kommune/danskuddannelse-til-udlaendinge/henvisning-og-visitation-til-danskuddannelse/",
        "https://lifeindenmark.borger.dk/school-and-education/education-for-adults/general-adult-education",
        "https://lifeindenmark.borger.dk/school-and-education/higher-education/admission-to-higher-education-in-denmark",
        "https://lifeindenmark.borger.dk/working/finding-a-job/recognition-of-foreign-qualifications",
        "https://www.ku.dk/studies/admission/application-deposit-fee-and-tuition-fees",
        "https://www.su.dk/english/refugees-from-ukraine",
        "https://lifeindenmark.borger.dk/school-and-education/student-finance/su---state-educational-grant"
    ];

    [Fact]
    public async Task CatalogContainsVerifiedArticleWithTwentySourcesAndPreservesExistingDenmarkArticles()
    {
        var catalog = await LoadProductionCatalogAsync();
        var article = Assert.Single(catalog.InformationEntries, entry => entry.Id == ArticleId);
        Assert.Equal(DenmarkId, article.CountryId);
        Assert.Equal(EducationCategoryId, article.CategoryId);

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
            Assert.Contains("- ", translation.Content);
            Assert.Equal(new DateOnly(2026, 10, 25), DateOnly.FromDateTime(translation.LastUpdatedAt.UtcDateTime));
        });

        var sources = catalog.OfficialSources.Where(source => source.InformationEntryId == ArticleId).ToArray();
        Assert.Equal(RequiredSources.OrderBy(url => url), sources.Select(source => source.Url.AbsoluteUri).OrderBy(url => url));
        Assert.All(sources, source =>
        {
            Assert.Equal(ArticleId, source.InformationEntryId);
            Assert.Equal(new DateOnly(2026, 10, 15), DateOnly.FromDateTime(source.LastCheckedAt.UtcDateTime));
        });
        Assert.DoesNotContain(catalog.OfficialSources, source => source.InformationEntryId == ArticleId && source.Url.AbsoluteUri.Contains("202612L00043", StringComparison.Ordinal));

        Assert.All(ExistingDenmarkArticleIds, id =>
            Assert.Contains(catalog.InformationEntries, entry => entry.Id == Guid.Parse(id) && entry.CountryId == DenmarkId));
        Assert.Equal(8, catalog.InformationEntries.Count(entry => entry.CountryId == DenmarkId));
    }

    [Theory]
    [InlineData("uk", "Освіта в Данії: школи, дитсадки та навчання для дорослих", "Варіанти навчання в Данії|Школи для дітей|Ясла та дитячі садки|Навчання для підлітків|Данська мова та навчання для дорослих|Вища освіта та кваліфікації|Витрати, фінансова підтримка та куди звертатися", "17 березня 2026 року", "не надається автоматично", "Копенгагенський університет", "може подати заяву", "рішення про вступ ухвалює заклад")]
    [InlineData("en", "Education in Denmark: schools, childcare and adult learning", "Education options in Denmark|Schools for children|Nurseries and kindergartens|Education for teenagers|Danish language and adult learning|Higher education and qualifications|Costs, financial support and where to apply", "17 March 2026", "is not automatic", "University of Copenhagen", "may apply for equal status", "the institution decides admission")]
    [InlineData("ru", "Образование в Дании: школы, детские сады и обучение взрослых", "Варианты обучения в Дании|Школы для детей|Ясли и детские сады|Обучение для подростков|Датский язык и обучение взрослых|Высшее образование и квалификации|Расходы, финансовая поддержка и куда обращаться", "17 марта 2026 года", "не предоставляется автоматически", "Копенгагенский университет", "может подать заявление", "решение о зачислении принимает учебное заведение")]
    public async Task LocalizedEducationArticleRendersSevenSectionsAndQualifiedGuidance(
        string language, string expectedTitle, string expectedHeadings, string schoolFlexExpiration,
        string nonAutomaticSu, string ucphScope, string suApplication, string institutionAdmission)
    {
        var catalog = await LoadProductionCatalogAsync();
        var html = await RenderAsync<Category>(catalog, language,
            "/countries/denmark/categories/education",
            new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = "denmark",
                [nameof(Category.CategorySlug)] = "education"
            });
        var visibleHtml = WebUtility.HtmlDecode(html);
        Assert.Contains(expectedTitle, visibleHtml);
        Assert.Contains(schoolFlexExpiration, visibleHtml);
        Assert.Contains(nonAutomaticSu, visibleHtml);
        Assert.Contains(ucphScope, visibleHtml);
        Assert.Contains(suApplication, visibleHtml);
        Assert.Contains(institutionAdmission, visibleHtml);
        Assert.Contains("Optagelse.dk", visibleHtml);
        var qualificationPhrases = language switch
        {
            "uk" => new[]
            {
                "муніципалітет індивідуально визначає",
                "гарантія не означає негайне зарахування",
                "не автоматичний вибір для кожної сім’ї",
                "Якщо заява ще розглядається",
                "Не припускайте, що інший заклад застосовує те саме правило",
                "сама по собі не гарантує вступу"
            },
            "en" => new[]
            {
                "municipality decides in an individual case",
                "does not promise a place at a particular facility or immediate admission",
                "not automatically for every family",
                "If an application is pending",
                "Do not assume another institution applies the same rule",
                "does not itself guarantee admission"
            },
            "ru" => new[]
            {
                "муниципалитет в индивидуальном порядке определяет",
                "гарантия не означает немедленного зачисления",
                "это не автоматический выбор для каждой семьи",
                "Если заявление ещё рассматривается",
                "Не предполагайте, что другое учреждение применяет то же правило",
                "сама по себе не гарантирует поступления"
            },
            _ => throw new ArgumentOutOfRangeException(nameof(language))
        };
        Assert.All(qualificationPhrases, phrase => Assert.Contains(phrase, visibleHtml));

        var headings = Regex.Matches(html, "<h3\\b[^>]*class=\"entry-section-heading\"[^>]*>(.*?)</h3>")
            .Cast<Match>().Select(match => WebUtility.HtmlDecode(match.Groups[1].Value)).ToArray();
        Assert.Equal(expectedHeadings.Split('|'), headings);
        Assert.Equal(RequiredSources.Length, RequiredSources.Count(url => html.Contains($"href=\"{url}\"", StringComparison.Ordinal)));
        Assert.Contains("<ul class=\"entry-list\"", html);
        AssertEducationInlineLinks(catalog, language, html);
        Assert.Contains("href=\"/countries/denmark/categories/education#article-section-", html);

        var comparisonCatalog = await LoadCatalogWithEducationTranslationStatusAsync(language, VerificationStatus.Verified);
        var comparisonHtml = await RenderAsync<Compare>(comparisonCatalog, language,
            "/compare?country1=denmark&country2=germany&category=education&tab=full");
        AssertEducationInlineLinks(comparisonCatalog, language, comparisonHtml);
        Assert.Contains("href=\"/compare?country1=denmark&amp;country2=germany&amp;category=education&amp;tab=full#article-section-", comparisonHtml);
    }

    [Fact]
    public async Task VerifiedEducationArticleIsVisibleInSearchAndCountryComparison()
    {
        var catalog = await LoadProductionCatalogAsync();
        var searchHtml = await RenderAsync<Search>(catalog, "en",
            "/search?q=Education%20in%20Denmark&country=denmark&category=education");
        Assert.Contains("Education in Denmark: schools, childcare and adult learning", WebUtility.HtmlDecode(searchHtml));

        var compareHtml = await RenderAsync<Compare>(catalog, "en",
            "/compare?country1=denmark&country2=germany&category=education");
        Assert.Contains("Education in Denmark: schools, childcare and adult learning", WebUtility.HtmlDecode(compareHtml));
    }

    [Fact]
    public async Task EducationArticleContentIsHtmlEncoded()
    {
        var temporaryRoot = Path.Combine(Path.GetTempPath(), "RefugeeNavigatorEducationEncoding", Guid.NewGuid().ToString("N"));
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
                "/countries/denmark/categories/education",
                new Dictionary<string, object?>
                {
                    [nameof(Category.CountrySlug)] = "denmark",
                    [nameof(Category.CategorySlug)] = "education"
                });
            Assert.Contains("&lt;script&gt;alert(&#x27;x&#x27;)&lt;/script&gt;", html);
            Assert.DoesNotContain("<script>", html);
        }
        finally
        {
            Directory.Delete(temporaryRoot, recursive: true);
        }
    }

    private static void AssertEducationInlineLinks(CatalogData catalog, string language, string html)
    {
        var expected = language switch
        {
            "uk" => new[]
            {
                (Guid.Parse("fcb08b16-c39a-4e68-9a7e-25ce55571450"), "дітей, переміщених з України"),
                (Guid.Parse("55c3e79d-1c9f-43fe-b01f-b0c0787571a4"), "гарантія місця"),
                (Guid.Parse("12b2a6e7-a5c4-45e3-aa7b-31771977b602"), "направляють дорослих, які відповідають умовам, на курси данської мови"),
                (Guid.Parse("186153a2-5805-43d8-b615-85eb0c64a256"), "може подати заяву на прирівняний статус за § 2(2) Закону про інтеграцію")
            },
            "ru" => new[]
            {
                (Guid.Parse("fcb08b16-c39a-4e68-9a7e-25ce55571450"), "детей, перемещённых из Украины"),
                (Guid.Parse("55c3e79d-1c9f-43fe-b01f-b0c0787571a4"), "гарантию места"),
                (Guid.Parse("12b2a6e7-a5c4-45e3-aa7b-31771977b602"), "направляют взрослых, соответствующих условиям, на курсы датского языка"),
                (Guid.Parse("186153a2-5805-43d8-b615-85eb0c64a256"), "может подать заявление на равный статус согласно § 2(2) Закона об интеграции")
            },
            "en" => new[]
            {
                (Guid.Parse("fcb08b16-c39a-4e68-9a7e-25ce55571450"), "children displaced from Ukraine"),
                (Guid.Parse("55c3e79d-1c9f-43fe-b01f-b0c0787571a4"), "the guarantee"),
                (Guid.Parse("12b2a6e7-a5c4-45e3-aa7b-31771977b602"), "refer eligible adults to Danish language education"),
                (Guid.Parse("186153a2-5805-43d8-b615-85eb0c64a256"), "may apply for equal status under section 2(2) of the Integration Act")
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
            Assert.Contains($"href=\"{source.Url.AbsoluteUri}\"", html);
            Assert.Contains(label, decodedHtml);
        }
    }

    private static async Task<CatalogData> LoadCatalogWithEducationTranslationStatusAsync(string language, VerificationStatus status)
    {
        var temporaryRoot = Path.Combine(Path.GetTempPath(), "RefugeeNavigatorEducationComparison", Guid.NewGuid().ToString("N"));
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
