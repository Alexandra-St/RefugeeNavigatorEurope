using System.Globalization;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Comparison;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Tests;

public sealed class CountryComparisonTests
{
    private static readonly ResourceManager Resources = new(
        "RefugeeNavigator.Web.Resources.Localization.SharedResource",
        typeof(SharedResource).Assembly);

    private static readonly Lazy<Task<CatalogData>> ProductionCatalog = new(
        () => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());

    private static readonly Lazy<Task<ComparisonFactCatalog>> FactCatalog = new(async () =>
    {
        var catalog = await LoadCatalogAsync();
        return await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(catalog);
    });

    [Fact]
    public async Task StructuredCatalogHasEightTopicsTwentyFourAspectsAndFourCountryFacts()
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        JsonComparisonFactProvider.Validate(facts, catalog);

        Assert.Equal(8, facts.Topics.Count);
        Assert.Equal(24, facts.Topics.Sum(topic => topic.Aspects.Count));
        Assert.Equal(24, facts.Topics.SelectMany(topic => topic.Aspects).Select(aspect => aspect.QuestionResourceKey).Distinct(StringComparer.Ordinal).Count());
        Assert.Equal(96, facts.Topics.SelectMany(topic => topic.Aspects).Sum(aspect => aspect.Facts.Count));
        Assert.Equal(94, facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).Count(fact => fact.IsAvailable));
        Assert.Equal(2, facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).Count(fact => !fact.IsAvailable));
        Assert.Equal(96, facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).Count(fact => fact.Status == ComparisonFactStatus.Approved));
        Assert.Equal(0, facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).Count(fact => fact.Status == ComparisonFactStatus.Draft));
        Assert.All(facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).Where(fact => fact.CountrySlug != "czechia"), fact => Assert.Equal(ComparisonFactStatus.Approved, fact.Status));
        Assert.Equal(190, facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).Sum(fact => fact.OfficialSourceIds.Count));
        Assert.Equal(32, catalog.InformationEntries.Count);
        Assert.Equal(96, catalog.InformationEntryTranslations.Count);
        Assert.Equal(96, catalog.InformationEntryTranslations.Count(translation => translation.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(translation => translation.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(translation => translation.VerificationStatus == VerificationStatus.Outdated));
        var czechiaId = Guid.Parse("10000000-0000-0000-0000-000000000004");
        var czechiaEntryIds = catalog.InformationEntries.Where(entry => entry.CountryId == czechiaId).Select(entry => entry.Id).ToHashSet();
        Assert.All(catalog.InformationEntryTranslations.Where(translation => czechiaEntryIds.Contains(translation.InformationEntryId)),
            translation => Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus));
        Assert.All(catalog.InformationEntryTranslations.Where(translation => !czechiaEntryIds.Contains(translation.InformationEntryId)),
            translation => Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus));
        var unavailableFacts = facts.Topics.SelectMany(topic => topic.Aspects)
            .SelectMany(aspect => aspect.Facts.Where(fact => !fact.IsAvailable)
                .Select(fact => (aspect.Id, fact.CountrySlug, fact))).ToArray();
        Assert.Equal(new[]
        {
            ("education-pupil-language", "germany"),
            ("employment-assistance-conditions", "denmark")
        }, unavailableFacts.Select(item => (item.Id, item.CountrySlug)).OrderBy(item => item.Id).ToArray());
        foreach (var (_, _, fact) in unavailableFacts)
        foreach (var language in new[] { "uk", "en", "ru" })
        {
            Assert.Equal(GetResource("Compare.NotEstablishedInReviewedSources", language), fact.Values[language]);
        }

        var employerDutiesAspect = Assert.Single(facts.Topics.SelectMany(topic => topic.Aspects), aspect => aspect.Id == "work-employer-duties");
        var germanEmployerDuties = Assert.Single(employerDutiesAspect.Facts, fact => fact.CountrySlug == "germany");
        Assert.True(germanEmployerDuties.IsAvailable);
        Assert.Equal(ComparisonFactStatus.Approved, germanEmployerDuties.Status);
        Assert.Equal(new Guid("cac1773b-211a-5b77-9f96-8056401b5902"), Assert.Single(germanEmployerDuties.OfficialSourceIds));
        Assert.Contains("§4a(4)", germanEmployerDuties.Values["uk"]);
        Assert.Contains("§4a(4)", germanEmployerDuties.Values["en"]);
        Assert.Contains("§4a(4)", germanEmployerDuties.Values["ru"]);
        Assert.Contains("тривалих платних послуг", germanEmployerDuties.Values["uk"]);
        Assert.Contains("sustained paid services", germanEmployerDuties.Values["en"]);
        Assert.Contains("длительных платных услуг", germanEmployerDuties.Values["ru"]);
        Assert.Contains("Протягом зайнятості роботодавець зберігає копію", germanEmployerDuties.Values["uk"]);
        Assert.Contains("For the duration of employment", germanEmployerDuties.Values["en"]);
        Assert.Contains("В течение всего периода занятости", germanEmployerDuties.Values["ru"]);
        Assert.Contains("главою 2, розділом 4", germanEmployerDuties.Values["uk"]);
        Assert.Contains("Chapter 2, Section 4", germanEmployerDuties.Values["en"]);
        Assert.Contains("главы 2, раздела 4", germanEmployerDuties.Values["ru"]);

        foreach (var topic in facts.Topics)
        {
            Assert.Equal(4, topic.Aspects.SelectMany(aspect => aspect.Facts).Select(fact => fact.CountrySlug).Distinct().Count());
            Assert.Equal(topic.CategorySlug, Assert.Single(CountryComparisonTopicMap.All, mapped => mapped.ResourceKey == topic.TopicResourceKey).CategorySlug);
            foreach (var aspect in topic.Aspects)
            foreach (var fact in aspect.Facts)
            {
                Assert.Equal(ComparisonFactStatus.Approved, fact.Status);
                Assert.All(new[] { "uk", "en", "ru" }, language => Assert.False(string.IsNullOrWhiteSpace(fact.Values.GetValueOrDefault(language))));
                if (fact.IsAvailable)
                {
                    Assert.NotEmpty(fact.OfficialSourceIds);
                    Assert.All(fact.OfficialSourceIds, sourceId => Assert.Contains(catalog.OfficialSources, source => source.Id == sourceId && source.InformationEntryId == fact.InformationEntryId));
                }
                else
                {
                    Assert.Empty(fact.OfficialSourceIds);
                    Assert.Equal(GetResource("Compare.NotEstablishedInReviewedSources", "en"), fact.Values["en"]);
                }
            }
        }
    }

    [Fact]
    public async Task CzechiaHasTwentyFourApprovedFactsMappedToItsEightArticlesAndOwnedSources()
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var expectedArticleIds = new Dictionary<string, Guid>(StringComparer.Ordinal)
        {
            ["protection-scheme"] = Guid.Parse("49d3c355-af96-54f7-92d5-68062647d464"),
            ["protection-application"] = Guid.Parse("49d3c355-af96-54f7-92d5-68062647d464"),
            ["protection-dates"] = Guid.Parse("49d3c355-af96-54f7-92d5-68062647d464"),
            ["residence-routes"] = Guid.Parse("2d35d95d-3524-5f3c-b7bd-ff61bd67ccd5"),
            ["residence-conditions"] = Guid.Parse("2d35d95d-3524-5f3c-b7bd-ff61bd67ccd5"),
            ["residence-filing"] = Guid.Parse("2d35d95d-3524-5f3c-b7bd-ff61bd67ccd5"),
            ["employment-vacancies"] = Guid.Parse("e8a5e6d5-a989-51de-ae14-b58e724604b8"),
            ["employment-institutions"] = Guid.Parse("e8a5e6d5-a989-51de-ae14-b58e724604b8"),
            ["employment-assistance-conditions"] = Guid.Parse("e8a5e6d5-a989-51de-ae14-b58e724604b8"),
            ["work-permission"] = Guid.Parse("829b9986-d949-5dd8-80c4-b9b4b7cf4d44"),
            ["work-employer-duties"] = Guid.Parse("829b9986-d949-5dd8-80c4-b9b4b7cf4d44"),
            ["work-minimum-pay"] = Guid.Parse("829b9986-d949-5dd8-80c4-b9b4b7cf4d44"),
            ["healthcare-access"] = Guid.Parse("d252adbc-48c0-5546-bbe1-2b08e2293f27"),
            ["healthcare-proof"] = Guid.Parse("d252adbc-48c0-5546-bbe1-2b08e2293f27"),
            ["healthcare-urgent"] = Guid.Parse("d252adbc-48c0-5546-bbe1-2b08e2293f27"),
            ["housing-initial"] = Guid.Parse("957ae315-47a4-5a73-806e-ca1d6cf258d1"),
            ["housing-private-rental"] = Guid.Parse("957ae315-47a4-5a73-806e-ca1d6cf258d1"),
            ["housing-financial-support"] = Guid.Parse("957ae315-47a4-5a73-806e-ca1d6cf258d1"),
            ["education-school-enrolment"] = Guid.Parse("954761f7-bc2b-5725-aeba-c8f1d2192588"),
            ["education-pupil-language"] = Guid.Parse("954761f7-bc2b-5725-aeba-c8f1d2192588"),
            ["education-higher-admission"] = Guid.Parse("954761f7-bc2b-5725-aeba-c8f1d2192588"),
            ["social-subsistence"] = Guid.Parse("f408e721-31a3-572a-a702-69ab600023a3"),
            ["social-application"] = Guid.Parse("f408e721-31a3-572a-a702-69ab600023a3"),
            ["social-child-benefits"] = Guid.Parse("f408e721-31a3-572a-a702-69ab600023a3")
        };
        var czechFacts = facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect =>
            aspect.Facts.Where(fact => fact.CountrySlug == "czechia").Select(fact => (aspect.Id, fact))).ToArray();

        Assert.Equal(24, czechFacts.Length);
        foreach (var (aspectId, fact) in czechFacts)
        {
            Assert.Equal(ComparisonFactStatus.Approved, fact.Status);
            Assert.True(fact.IsAvailable);
            Assert.Equal(expectedArticleIds[aspectId], fact.InformationEntryId);
            Assert.Equal(new[] { "en", "ru", "uk" }, fact.Values.Keys.Order(StringComparer.Ordinal).ToArray());
            Assert.NotEmpty(fact.OfficialSourceIds);
            Assert.All(fact.OfficialSourceIds, sourceId => Assert.Contains(catalog.OfficialSources,
                source => source.Id == sourceId && source.InformationEntryId == fact.InformationEntryId));
        }
    }

    [Fact]
    public async Task CzechiaHighRiskComparisonAnswersPreserveDatesCohortsAndConditionsAcrossLanguages()
    {
        var facts = await FactCatalog.Value;
        var expectedPhrases = new Dictionary<(string Aspect, string Language), string[]>
        {
            [("protection-application", "en")] = ["5 August 2026", "18–22", "Reserv+", "paper eVOD", "23–60", "family-reunification", "not ordinary extensions"],
            [("protection-application", "uk")] = ["5 серпня 2026", "18–22", "Reserv+", "паперовий eVOD", "23–60", "возз’єднання сім’ї", "не звичайного продовження"],
            [("protection-application", "ru")] = ["5 августа 2026", "18–22", "Reserv+", "бумажный eVOD", "23–60", "воссоединения семьи", "не обычного продления"],
            [("protection-dates", "en")] = ["31 March 2027", "31 March 2028", "4 March 2028", "has not been announced"],
            [("protection-dates", "uk")] = ["31 березня 2027", "31 березня 2028", "4 березня 2028", "ще не оголошено"],
            [("protection-dates", "ru")] = ["31 марта 2027", "31 марта 2028", "4 марта 2028", "ещё не объявили"],
            [("residence-conditions", "en")] = ["1 April 2026", "2025", "CZK 440,000", "CZK 110,000", "not an individual eligibility decision"],
            [("residence-conditions", "uk")] = ["1 квітня 2026", "2025", "440 000 CZK", "110 000 CZK", "не індивідуальне рішення"],
            [("residence-conditions", "ru")] = ["1 апреля 2026", "2025", "440 000 CZK", "110 000 CZK", "не индивидуальное решение"],
            [("work-minimum-pay", "en")] = ["2026 only", "CZK 22,400", "CZK 134.40", "not a 2027 rate"],
            [("work-minimum-pay", "uk")] = ["Лише на 2026 рік", "22 400 CZK", "134,40 CZK", "не ставка на 2027 рік"],
            [("work-minimum-pay", "ru")] = ["Только на 2026 год", "22 400 CZK", "134,40 CZK", "не ставка на 2027 год"],
            [("healthcare-access", "en")] = ["10 February 2025", "150 days", "11 February 2025", "90 days", "repeat TP does not restart", "not everyone must self-pay"],
            [("healthcare-access", "uk")] = ["10 лютого 2025", "150 днів", "11 лютого 2025", "90 днів", "повторне надання захисту його не поновлює", "самостійна сплата потрібна не всім"],
            [("healthcare-access", "ru")] = ["10 февраля 2025", "150 дней", "11 февраля 2025", "90 дней", "повторное предоставление защиты его не возобновляет", "самостоятельно платить должны не все"],
            [("housing-initial", "en")] = ["90 days", "repeat grant does not restart", "vulnerable people", "cannot choose the region or facility"],
            [("housing-initial", "uk")] = ["90 днів", "повторне надання не запускає строк заново", "вразливих осіб", "обрати регіон чи заклад не можна"],
            [("housing-initial", "ru")] = ["90 дней", "повторное предоставление не запускает срок заново", "уязвимых лиц", "выбрать регион или учреждение нельзя"],
            [("social-subsistence", "en")] = ["CZK 4,860", "CZK 3,490", "CZK 4,188", "not guaranteed payments", "150-day", "CZK 3,130"],
            [("social-subsistence", "uk")] = ["4 860 CZK", "3 490 CZK", "4 188 CZK", "не гарантовані виплати", "150-денного", "3 130 CZK"],
            [("social-subsistence", "ru")] = ["4 860 CZK", "3 490 CZK", "4 188 CZK", "не гарантированные выплаты", "150-дневного", "3 130 CZK"]
        };

        foreach (var ((aspectId, language), phrases) in expectedPhrases)
        {
            var fact = Assert.Single(facts.Topics.SelectMany(topic => topic.Aspects)
                .Where(aspect => aspect.Id == aspectId)
                .SelectMany(aspect => aspect.Facts), item => item.CountrySlug == "czechia");
            Assert.Equal(ComparisonFactStatus.Approved, fact.Status);
            foreach (var phrase in phrases)
            {
                Assert.Contains(phrase, fact.Values[language]);
            }
        }
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task ApprovedCzechiaFactsRenderInProductionForAllAndIndividualCategories(string language)
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var czechFacts = facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts)
            .Where(fact => fact.CountrySlug == "czechia").ToArray();
        var allQuery = "?country1=czechia&country2=germany&category=all";
        var developmentHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, language, allQuery));
        var productionHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, language, allQuery, Environments.Production));

        foreach (var html in new[] { developmentHtml, productionHtml })
        {
            Assert.Contains("class=\"structured-comparison\"", html);
            Assert.DoesNotContain("class=\"draft-label\"", html);
            Assert.DoesNotContain(GetResource("Compare.DraftPreviewNotice", language), html);
            Assert.DoesNotContain(GetResource("Compare.DraftStatus", language), html);
        }

        foreach (var fact in czechFacts)
        {
            Assert.Equal(ComparisonFactStatus.Approved, fact.Status);
            Assert.Contains(fact.Values[language], productionHtml);
            var sources = catalog.OfficialSources.Where(item =>
                fact.OfficialSourceIds.Contains(item.Id) && item.InformationEntryId == fact.InformationEntryId).ToArray();
            Assert.NotEmpty(sources);
            foreach (var source in sources)
            {
                Assert.Contains($"href=\"{source.Url.AbsoluteUri}\"", productionHtml);
            }
            var topic = Assert.Single(facts.Topics, item => item.Aspects.Any(aspect => aspect.Facts.Contains(fact)));
            Assert.Contains($"/countries/czechia/categories/{topic.CategorySlug}#entry-{fact.InformationEntryId:N}", productionHtml);
        }

        foreach (var category in new[] { "temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance" })
        {
            var categoryQuery = $"?country1=czechia&country2=germany&category={category}";
            var categoryHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, language, categoryQuery, Environments.Production));
            var categoryFacts = czechFacts.Where(fact => CountryComparisonTopicMap.ForCategory(category)
                .Any(topic => topic.EntryIds.Values.Contains(fact.InformationEntryId))).ToArray();
            Assert.Contains("class=\"structured-comparison\"", categoryHtml);
            Assert.DoesNotContain("class=\"draft-label\"", categoryHtml);
            Assert.DoesNotContain(GetResource("Compare.DraftPreviewNotice", language), categoryHtml);
            Assert.NotEmpty(categoryFacts);
            foreach (var fact in categoryFacts)
            {
                Assert.Contains(fact.Values[language], categoryHtml);
            }
        }
    }

    [Fact]
    public async Task IncorrectArticleAndSourceRelationshipsFailStructuredCatalogValidation()
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var firstTopic = facts.Topics[0];
        var firstAspect = firstTopic.Aspects[0];
        var firstFact = firstAspect.Facts[0];

        var wrongArticle = ReplaceFact(facts, firstTopic, firstAspect, firstFact with { InformationEntryId = Guid.NewGuid() });
        Assert.Throws<InvalidDataException>(() => JsonComparisonFactProvider.Validate(wrongArticle, catalog));

        var wrongSource = ReplaceFact(facts, firstTopic, firstAspect, firstFact with { OfficialSourceIds = [Guid.NewGuid()] });
        Assert.Throws<InvalidDataException>(() => JsonComparisonFactProvider.Validate(wrongSource, catalog));
    }

    [Fact]
    public async Task ApprovedFactsRenderInProductionAndFullArticleComparisonIsCollapsed()
    {
        var catalog = await LoadCatalogAsync();
        const string query = "?country1=germany&country2=poland&category=employment";
        var localHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, "en", query));
        var productionHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, "en", query, Environments.Production));

        Assert.Contains("structured-comparison", localHtml);
        Assert.Contains("structured-comparison", productionHtml);
        Assert.DoesNotContain("class=\"draft-label\"", productionHtml);
        Assert.Contains("Structured comparison", productionHtml);
        Assert.DoesNotContain(GetResource("Compare.DraftPreviewNotice", "en"), localHtml);
        Assert.DoesNotContain(GetResource("Compare.DraftPreviewNotice", "en"), productionHtml);
        Assert.DoesNotContain(GetResource("Compare.DraftStatus", "en"), productionHtml);
        Assert.Contains("class=\"comparison-topic\"", productionHtml);
        Assert.Contains(GetResource("Compare.ReadDetails", "en"), productionHtml);
        Assert.Contains("class=\"source-details\"", productionHtml);
        Assert.Contains("href=\"/countries/germany/categories/employment#entry-", productionHtml);
        Assert.Matches("<details\\b[^>]*class=\"full-article-comparison\"[^>]*>", productionHtml);
        Assert.DoesNotMatch("<details\\b[^>]*class=\"full-article-comparison\"[^>]*\\bopen(?:\\s|>)", productionHtml);
        Assert.Contains("class=\"comparison-topic\"", localHtml);
        Assert.Contains(GetResource("Compare.ReadFullArticles", "en"), localHtml);

        var draftFacts = WithStatus(await FactCatalog.Value, ComparisonFactStatus.Draft);
        var developmentDraftHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, "en", query, comparisonFacts: draftFacts));
        var productionDraftHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, "en", query, Environments.Production, draftFacts));
        Assert.Contains(GetResource("Compare.DraftPreviewNotice", "en"), developmentDraftHtml);
        Assert.Contains(GetResource("Compare.DraftStatus", "en"), developmentDraftHtml);
        Assert.Contains("class=\"draft-label\"", developmentDraftHtml);
        Assert.DoesNotContain("structured-comparison", productionDraftHtml);
        Assert.Contains("class=\"comparison-topic\"", productionDraftHtml);
        Assert.DoesNotContain("class=\"full-article-comparison\"", productionDraftHtml);
    }

    [Fact]
    public async Task StructuredFactAndMissingInformationFallbackAreHtmlEncodedAndLocalized()
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var topic = facts.Topics.Single(item => item.CategorySlug == "healthcare");
        var aspect = topic.Aspects[0];
        var denmarkFact = aspect.Facts.Single(fact => fact.CountrySlug == "denmark");
        var injected = WithFact(facts, topic, aspect, denmarkFact with
        {
            Values = new Dictionary<string, string>(denmarkFact.Values) { ["en"] = "<script>alert(1)</script>" }
        });
        var html = await RenderAsync(catalog, "en", "?country1=denmark&country2=germany&category=healthcare", comparisonFacts: injected);
        Assert.Contains("&lt;script&gt;alert(1)&lt;/script&gt;", html);
        Assert.DoesNotContain("<script>alert(1)</script>", html);

        foreach (var (language, fallback) in new[]
        {
            ("uk", "Не встановлено за перевіреними джерелами."),
            ("en", "Not established in the reviewed sources."),
            ("ru", "Не установлено по проверенным источникам.")
        })
        {
            var unavailable = WithFact(facts, topic, aspect, denmarkFact with
            {
                IsAvailable = false,
                OfficialSourceIds = [],
                Values = new Dictionary<string, string>
                {
                    ["uk"] = "Не встановлено за перевіреними джерелами.",
                    ["en"] = "Not established in the reviewed sources.",
                    ["ru"] = "Не установлено по проверенным источникам."
                }
            });
            var unavailableHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, language,
                "?country1=denmark&country2=germany&category=healthcare", comparisonFacts: unavailable));
            Assert.Contains(fallback, unavailableHtml);
            JsonComparisonFactProvider.Validate(unavailable, catalog);
        }
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task EveryStructuredAspectAndPreviewLabelHasCompleteLocalization(string language)
    {
        var facts = await FactCatalog.Value;
        var aspectKeys = facts.Topics.SelectMany(topic => topic.Aspects).Select(aspect => aspect.QuestionResourceKey)
            .Concat(new[]
            {
                "Compare.StructuredHeading", "Compare.DraftPreviewHeading", "Compare.DraftPreviewNotice", "Compare.DraftStatus", "Compare.FactSources",
                "Compare.ApprovedEditorialStatus", "Compare.NotEstablishedInReviewedSources", "Compare.ReadFullArticles", "Compare.AllCategories"
            }).Distinct(StringComparer.Ordinal);
        foreach (var aspectKey in aspectKeys)
        {
            var value = GetResource(aspectKey, language);
            Assert.False(string.IsNullOrWhiteSpace(value), $"Missing {language} localization for {aspectKey}.");
            Assert.NotEqual(aspectKey, value);
        }
    }

    [Theory]
    [InlineData("uk", "Усі категорії")]
    [InlineData("en", "All categories")]
    [InlineData("ru", "Все категории")]
    public async Task AllCategoriesSelectorOptionIsLocalizedAndUsesStableQueryValue(string language, string label)
    {
        var html = WebUtility.HtmlDecode(await RenderAsync(await LoadCatalogAsync(), language));
        var option = Regex.Match(html, "<option\\b(?=[^>]*value=\"all\")(?<attributes>[^>]*)>(?<label>.*?)</option>", RegexOptions.Singleline);

        Assert.True(option.Success);
        Assert.Equal(label, option.Groups["label"].Value.Trim());
        Assert.Contains("name=\"category\" required", html);
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task AllCategoriesRendersStructuredFactsAndGroupedCollapsedArticlesInCatalogOrder(string language)
    {
        var catalog = await LoadCatalogAsync();
        var factCatalog = await FactCatalog.Value;
        var html = await RenderAsync(catalog, language,
            "?country1=germany&country2=poland&category=all");
        var visibleHtml = WebUtility.HtmlDecode(html);
        var renderedCategoryHeadings = Regex.Matches(
                visibleHtml,
                "<section\\b[^>]*class=\"structured-category\"[^>]*>\\s*<h3\\b[^>]*>(?<heading>.*?)</h3>",
                RegexOptions.Singleline)
            .Cast<Match>()
            .Select(match => match.Groups["heading"].Value.Trim())
            .ToArray();

        Assert.Equal(catalog.Categories.Select(category => GetResource(
            $"Category.{string.Concat(category.Slug.Split('-').Select(part => char.ToUpperInvariant(part[0]) + part[1..]))}", language)),
            renderedCategoryHeadings);
        Assert.Equal(factCatalog.Topics.Count, Regex.Matches(html, "class=\"structured-topic\"").Count);
        Assert.Equal(factCatalog.Topics.Sum(topic => topic.Aspects.Count), Regex.Matches(html, "class=\"structured-aspect\"").Count);
        Assert.Equal(factCatalog.Topics.Sum(topic => topic.Aspects.Count) * 2, Regex.Matches(html, "class=\"structured-country\"").Count);
        Assert.Contains(GetResource("Country.Germany", language), visibleHtml);
        Assert.Contains(GetResource("Country.Poland", language), visibleHtml);
        Assert.Contains("class=\"fact-sources\"", html);
        Assert.Contains("target=\"_blank\" rel=\"noopener noreferrer\"", html);
        Assert.DoesNotContain(GetResource("Compare.InvalidCategory", language), visibleHtml);
        Assert.Contains("name=\"category\" required", html);
        Assert.Matches("<option\\b[^>]*value=\"all\"[^>]*selected", html);
        var elementIds = Regex.Matches(html, "\\bid=\"([^\"]+)\"")
            .Cast<Match>()
            .Select(match => match.Groups[1].Value)
            .ToArray();
        Assert.Equal(elementIds.Length, elementIds.Distinct(StringComparer.Ordinal).Count());

        var categoryDetails = Regex.Matches(html, "<details\\b[^>]*class=\"full-article-category\"[^>]*>", RegexOptions.Singleline);
        Assert.Equal(catalog.Categories.Count, categoryDetails.Count);
        var fullArticleCategoryHeadings = Regex.Matches(
                visibleHtml,
                "<details\\b[^>]*class=\"full-article-category\"[^>]*>\\s*<summary\\b[^>]*>(?<heading>.*?)</summary>",
                RegexOptions.Singleline)
            .Cast<Match>()
            .Select(match => match.Groups["heading"].Value.Trim())
            .ToArray();
        Assert.Equal(renderedCategoryHeadings, fullArticleCategoryHeadings);
        Assert.Equal(CountryComparisonTopicMap.All.Count, Regex.Matches(html, "class=\"comparison-topic\"").Count);
        Assert.Matches("<details\\b[^>]*class=\"full-article-comparison\"[^>]*>", html);
        Assert.DoesNotMatch("<details\\b[^>]*class=\"full-article-comparison\"[^>]*\\bopen(?:\\s|>)", html);
        Assert.DoesNotContain("class=\"full-article-category\" open", html);
        Assert.DoesNotMatch("<details\\b[^>]*class=\"article-details\"[^>]*\\bopen(?:\\s|>)", html);

        foreach (var category in catalog.Categories)
        {
            foreach (var topic in CountryComparisonTopicMap.ForCategory(category.Slug))
            {
                foreach (var countrySlug in new[] { "germany", "poland" })
                {
                    var articleId = CountryComparisonTopicMap.EntryIdFor(topic, countrySlug);
                    var translation = Assert.Single(catalog.InformationEntryTranslations, item =>
                        item.InformationEntryId == articleId && item.LanguageCode == language);
                    Assert.Contains(translation.Title, visibleHtml);
                    Assert.Contains($"href=\"/countries/{countrySlug}/categories/{category.Slug}#entry-{articleId:N}\"", html);
                }

                var structuredTopic = Assert.Single(factCatalog.Topics, item => item.TopicResourceKey == topic.ResourceKey);
                foreach (var aspect in structuredTopic.Aspects)
                foreach (var countrySlug in new[] { "germany", "poland" })
                {
                    var fact = Assert.Single(aspect.Facts, item => item.CountrySlug == countrySlug);
                    Assert.Equal(ComparisonFactStatus.Approved, fact.Status);
                    Assert.Contains(GetResource(aspect.QuestionResourceKey, language), visibleHtml);
                    if (fact.IsAvailable)
                    {
                        Assert.Contains(fact.Values[language], visibleHtml);
                        Assert.NotEmpty(fact.OfficialSourceIds);
                        foreach (var sourceId in fact.OfficialSourceIds)
                        {
                            var source = Assert.Single(catalog.OfficialSources, item => item.Id == sourceId);
                            Assert.Equal(fact.InformationEntryId, source.InformationEntryId);
                            Assert.Contains(source.Title, ReadRenderedText(html));
                            Assert.Contains($"href=\"{WebUtility.HtmlEncode(source.Url.AbsoluteUri)}\" target=\"_blank\" rel=\"noopener noreferrer\"", html);
                        }
                    }
                    else
                    {
                        Assert.Contains(GetResource("Compare.NotEstablishedInReviewedSources", language), visibleHtml);
                    }
                }
            }
        }
    }

    [Fact]
    public async Task MissingComparisonLocalizationFailsCoverageValidation()
    {
        var facts = await FactCatalog.Value;
        Assert.Throws<InvalidDataException>(() => ComparisonFactLocalizationValidator.Validate(
            facts,
            (key, culture) => key == "Compare.DraftPreviewNotice" && culture.Name == "ru"
                ? null
                : Resources.GetString(key, culture)));
    }

    [Fact]
    public async Task ComparisonMapContainsEightOrderedTopicsAndMapsRealArticlesToExpectedRelationships()
    {
        var catalog = await LoadCatalogAsync();
        CountryComparisonTopicMap.Validate(catalog);

        Assert.Equal(8, CountryComparisonTopicMap.All.Count);
        Assert.Equal(
            ["temporary-protection", "temporary-protection", "employment", "employment", "healthcare", "housing", "education", "social-assistance"],
            CountryComparisonTopicMap.All.Select(topic => topic.CategorySlug));
        var expectedCountrySlugs = new[] { "czechia", "denmark", "germany", "poland" };
        Assert.Equal(expectedCountrySlugs, CountryComparisonTopicMap.SupportedCountrySlugs.Order(StringComparer.Ordinal).ToArray());
        Assert.Equal(32, CountryComparisonTopicMap.All.SelectMany(topic => topic.EntryIds.Values).Distinct().Count());

        foreach (var topic in CountryComparisonTopicMap.All)
        {
            Assert.Equal(4, topic.EntryIds.Count);
            Assert.Equal(expectedCountrySlugs, topic.EntryIds.Keys.Order(StringComparer.Ordinal).ToArray());
            var category = Assert.Single(catalog.Categories, item => item.Slug == topic.CategorySlug);
            foreach (var (countrySlug, entryId) in topic.EntryIds)
            {
                var country = Assert.Single(catalog.Countries, item => item.Slug == countrySlug);
                var entry = Assert.Single(catalog.InformationEntries, item => item.Id == entryId);
                Assert.Equal(country.Id, entry.CountryId);
                Assert.Equal(category.Id, entry.CategoryId);
            }
        }
    }

    [Fact]
    public async Task AllCountryPairsCategoriesAndLanguagesRenderAlignedReviewedCatalogContent()
    {
        var catalog = await LoadCatalogAsync();
        var factCatalog = await FactCatalog.Value;
        var languages = new[] { "uk", "en", "ru" };
        var categories = new[]
        {
            "temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance"
        };
        var countryPairs = new[]
        {
            ("denmark", "germany"), ("germany", "poland"), ("poland", "denmark")
        };

        foreach (var language in languages)
        foreach (var categorySlug in categories)
        foreach (var (countryOneSlug, countryTwoSlug) in countryPairs)
        {
            var html = await RenderAsync(catalog, language,
                $"?country1={countryOneSlug}&country2={countryTwoSlug}&category={categorySlug}");
            var visibleHtml = WebUtility.HtmlDecode(html);
            var topics = CountryComparisonTopicMap.ForCategory(categorySlug);

            Assert.Equal(topics.Count, Regex.Matches(html, "class=\"comparison-topic\"").Count);
            Assert.Equal(topics.Count * 2, Regex.Matches(html, "class=\"topic-country\"").Count);
            Assert.Contains("name=\"country1\"", html);
            Assert.Contains("name=\"country2\"", html);
            Assert.Contains("name=\"category\"", html);
            Assert.Contains("selected", html);
            Assert.Contains(GetResource("Compare.ScopeNotice", language), visibleHtml);
            Assert.Contains(GetResource("Common.Disclaimer", language), visibleHtml);
            Assert.Contains(GetResource("Compare.StructuredHeading", language), visibleHtml);
            Assert.DoesNotContain(GetResource("Compare.DraftPreviewNotice", language), visibleHtml);
            Assert.DoesNotContain(GetResource("Compare.DraftStatus", language), visibleHtml);
            Assert.DoesNotContain("class=\"draft-label\"", html);
            Assert.Contains(GetResource("Compare.ReadFullArticles", language), visibleHtml);
            Assert.Matches("<details\\b[^>]*class=\"full-article-comparison\"[^>]*>", html);
            Assert.DoesNotMatch("<details\\b[^>]*class=\"full-article-comparison\"[^>]*\\bopen(?:\\s|>)", html);
            Assert.DoesNotContain("Compare.", visibleHtml);
            Assert.DoesNotContain("preview layout", visibleHtml, StringComparison.OrdinalIgnoreCase);

            foreach (var countrySlug in new[] { countryOneSlug, countryTwoSlug })
            foreach (var topic in topics)
            {
                var entryId = CountryComparisonTopicMap.EntryIdFor(topic, countrySlug);
                var entry = Assert.Single(catalog.InformationEntries, item => item.Id == entryId);
                var translation = Assert.Single(catalog.InformationEntryTranslations, item =>
                    item.InformationEntryId == entryId && item.LanguageCode == language);
                Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
                Assert.Contains(translation.Title, visibleHtml);
                Assert.Contains(translation.Summary, visibleHtml);
                AssertFullContentIsRendered(translation.Content, visibleHtml);
                Assert.Contains($"href=\"/countries/{countrySlug}/categories/{categorySlug}#entry-{entryId:N}\"", html);

                var sources = catalog.OfficialSources.Where(source => source.InformationEntryId == entryId).ToArray();
                Assert.NotEmpty(sources);
                foreach (var source in sources)
                {
                    Assert.Contains(source.Title, ReadRenderedText(html));
                    Assert.Contains($"href=\"{WebUtility.HtmlEncode(source.Url.AbsoluteUri)}\" target=\"_blank\" rel=\"noopener noreferrer\"", html);
                    AssertExternalSourceLink(html, source, language);
                    Assert.Contains(source.LastCheckedAt.ToString("d MMM yyyy", CultureInfo.GetCultureInfo(language)), visibleHtml);
                }
            }

            foreach (var topic in topics)
            {
                var structuredTopic = Assert.Single(factCatalog.Topics, item => item.TopicResourceKey == topic.ResourceKey);
                foreach (var aspect in structuredTopic.Aspects)
                foreach (var countrySlug in new[] { countryOneSlug, countryTwoSlug })
                {
                    var fact = Assert.Single(aspect.Facts, item => item.CountrySlug == countrySlug);
                    Assert.Equal(ComparisonFactStatus.Approved, fact.Status);
                    Assert.DoesNotContain("class=\"draft-label\"", html);
                    Assert.DoesNotContain(GetResource("Compare.DraftStatus", language), visibleHtml);
                    Assert.DoesNotContain(GetResource("Compare.DraftPreviewNotice", language), visibleHtml);
                    if (fact.IsAvailable)
                    {
                        Assert.Contains(fact.Values[language], visibleHtml);
                        foreach (var sourceId in fact.OfficialSourceIds)
                        {
                            var source = Assert.Single(catalog.OfficialSources, item => item.Id == sourceId);
                            Assert.Contains(source.Title, ReadRenderedText(html));
                            Assert.Contains($"href=\"{WebUtility.HtmlEncode(source.Url.AbsoluteUri)}\" target=\"_blank\" rel=\"noopener noreferrer\"", html);
                            AssertExternalSourceLink(html, source, language);
                        }
                    }
                    else
                    {
                        Assert.Contains(GetResource("Compare.NotEstablishedInReviewedSources", language), visibleHtml);
                    }
                    Assert.Contains(GetResource(aspect.QuestionResourceKey, language), visibleHtml);
                }
            }

            Assert.Contains("<details class=\"article-details\"", html);
            Assert.DoesNotContain("article-details\" open", html);
            Assert.Contains("<details class=\"source-details\"", html);
            Assert.Contains(GetResource("Compare.ReadDetails", language), visibleHtml);
            Assert.Contains(GetResource("Compare.OfficialSources", language), visibleHtml);
            Assert.Matches("<h5\\b", html);
            Assert.Matches("<ul\\b", html);
            Assert.DoesNotContain("<p>## ", html);
            Assert.DoesNotContain("<p>- ", html);
            AssertAllArticleMetadataShowsOnlyLastUpdated(html, language);
        }
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public Task EveryComparisonTopicHasLocalizedHeading(string language)
    {
        foreach (var topic in CountryComparisonTopicMap.All)
        {
            var value = GetResource(topic.ResourceKey, language);
            Assert.False(string.IsNullOrWhiteSpace(value), $"Missing {language} translation for {topic.ResourceKey}.");
            Assert.DoesNotContain(topic.ResourceKey, value);
        }

        return Task.CompletedTask;
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task NeedsReviewAndOutdatedTranslationsUseGenericUnavailableState(string language)
    {
        var catalog = await LoadCatalogAsync();
        var topic = CountryComparisonTopicMap.ForCategory("employment")[0];
        var entryId = CountryComparisonTopicMap.EntryIdFor(topic, "denmark");
        var original = Assert.Single(catalog.InformationEntryTranslations, item =>
            item.InformationEntryId == entryId && item.LanguageCode == language);

        foreach (var status in new[] { VerificationStatus.NeedsReview, VerificationStatus.Outdated })
        {
            var changedCatalog = WithTranslations(catalog, entryId, language, status);
            var html = await RenderAsync(changedCatalog, language,
                "?country1=denmark&country2=germany&category=employment");

            Assert.Contains(GetResource("Compare.InformationUnavailable", language), WebUtility.HtmlDecode(html));
            Assert.DoesNotContain("class=\"article-status\"", html);
            Assert.DoesNotContain(GetResource(StatusResourceKey(status), language), WebUtility.HtmlDecode(html));
            Assert.DoesNotContain(GetResource("Category.Status.Verified", language), WebUtility.HtmlDecode(html));
            Assert.DoesNotContain(WebUtility.HtmlEncode(original.Title), html);
            Assert.DoesNotContain(WebUtility.HtmlEncode(original.Summary), html);
            Assert.DoesNotContain(WebUtility.HtmlEncode(original.Content), html);
            Assert.Contains(catalog.OfficialSources.First(source => source.InformationEntryId == entryId).Title, ReadRenderedText(html));
        }
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task MissingMappedArticleUsesUnavailableFallback(string language)
    {
        var catalog = await LoadCatalogAsync();
        var entryId = CountryComparisonTopicMap.EntryIdFor(CountryComparisonTopicMap.ForCategory("healthcare")[0], "denmark");
        var withoutEntry = CreateCatalog(
            catalog.Countries,
            catalog.Categories,
            catalog.InformationEntries.Where(entry => entry.Id != entryId),
            catalog.InformationEntryTranslations.Where(translation => translation.InformationEntryId != entryId),
            catalog.OfficialSources.Where(source => source.InformationEntryId != entryId));

        var html = await RenderAsync(withoutEntry, language, "?country1=denmark&country2=germany&category=healthcare");
        var visibleHtml = WebUtility.HtmlDecode(html);

        Assert.Contains(GetResource("Compare.InformationUnavailable", language), visibleHtml);
        Assert.Contains("class=\"topic-country\"", html);
        Assert.Contains("href=\"/countries/germany/categories/healthcare#entry-", html);
    }

    [Fact]
    public async Task ArticleDetailsHtmlEncodesUntrustedMarkupAndKeepsFormattingSafe()
    {
        var catalog = await LoadCatalogAsync();
        var entryId = CountryComparisonTopicMap.EntryIdFor(CountryComparisonTopicMap.ForCategory("healthcare")[0], "denmark");
        var injected = WithContent(catalog, entryId, "en", "<script>alert(1)</script>\n\n## Safe heading\n\n- Safe list item");
        var html = await RenderAsync(injected, "en", "?country1=denmark&country2=germany&category=healthcare");
        var visibleHtml = WebUtility.HtmlDecode(html);

        Assert.Contains("&lt;script&gt;alert(1)&lt;/script&gt;", html);
        Assert.DoesNotContain("<script>alert(1)</script>", html);
        Assert.Matches("<h5\\b[^>]*>Safe heading</h5>", html);
        Assert.Matches("<li\\b[^>]*>Safe list item</li>", html);
        Assert.DoesNotContain("## Safe heading", visibleHtml);
        Assert.DoesNotContain("- Safe list item", visibleHtml);
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task EmptyAndPartialStateShowsInstructionsAndReset(string language)
    {
        var initial = WebUtility.HtmlDecode(await RenderAsync(await LoadCatalogAsync(), language));
        Assert.Contains(GetResource("Compare.SelectionInstructions", language), initial);
        Assert.Contains("name=\"country1\" required", initial);
        Assert.Contains("name=\"country2\" required", initial);
        Assert.Contains("name=\"category\" required", initial);
        Assert.DoesNotContain("class=\"comparison-topic\"", initial);
        Assert.Contains("href=\"/compare\"", initial);

        var partial = await RenderAsync(await LoadCatalogAsync(), language, "?country1=denmark");
        Assert.Contains("selected", partial);
        Assert.DoesNotContain("class=\"comparison-topic\"", partial);
    }

    [Fact]
    public async Task AllCategoriesSelectionDoesNotChangeSameCountryValidation()
    {
        var html = WebUtility.HtmlDecode(await RenderAsync(await LoadCatalogAsync(), "en",
            "?country1=germany&country2=germany&category=all"));

        Assert.Contains(GetResource("Compare.SameCountry", "en"), html);
        Assert.DoesNotContain("class=\"structured-comparison\"", html);
    }

    [Theory]
    [InlineData("?country1=denmark&country2=denmark&category=employment", "Compare.SameCountry")]
    [InlineData("?country1=unknown&country2=germany&category=employment", "Compare.InvalidCountry")]
    [InlineData("?country1=denmark&country2=germany&category=unknown", "Compare.InvalidCategory")]
    [InlineData("?country1=denmark&category=employment", "Compare.SelectionInstructions")]
    public async Task InvalidAndIncompleteQueryParametersRenderLocalizedMessages(string query, string resourceKey)
    {
        var html = WebUtility.HtmlDecode(await RenderAsync(await LoadCatalogAsync(), "en", query));

        Assert.Contains(GetResource(resourceKey, "en"), html);
        Assert.DoesNotContain("class=\"comparison-topic\"", html);
        Assert.DoesNotContain("HTTP 500", html);
    }

    private static string StatusResourceKey(VerificationStatus status) => status switch
    {
        VerificationStatus.NeedsReview => "Category.Status.NeedsReview",
        VerificationStatus.Outdated => "Category.Status.Outdated",
        _ => throw new ArgumentOutOfRangeException(nameof(status))
    };

    private static void AssertAllArticleMetadataShowsOnlyLastUpdated(string html, string language)
    {
        var blocks = Regex.Matches(html, "<dl\\b[^>]*class=\"article-metadata\"[^>]*>(?<body>.*?)</dl>", RegexOptions.Singleline);
        Assert.NotEmpty(blocks.Cast<Match>());
        foreach (Match block in blocks)
        {
            var content = ReadRenderedText(block.Groups["body"].Value);
            Assert.Contains(GetResource("Compare.LastUpdated", language), content);
            foreach (var status in Enum.GetValues<VerificationStatus>())
            {
                var label = GetResource($"Category.Status.{status}", language);
                Assert.DoesNotContain(label, content);
            }
        }
    }

    private static void AssertFullContentIsRendered(string content, string renderedHtml)
    {
        var blocks = Regex.Split(content, @"\r?\n\s*\r?\n+")
            .Select(block => block.Trim())
            .Where(block => block.Length > 0);

        foreach (var block in blocks)
        {
            if (block.StartsWith("## ", StringComparison.Ordinal))
            {
                Assert.Matches($"<h5\\b[^>]*>{Regex.Escape(block[3..].Trim())}</h5>", renderedHtml);
            }
            else if (block.Split('\n').All(line => line.TrimEnd('\r').StartsWith("- ", StringComparison.Ordinal)))
            {
                foreach (var item in block.Split('\n').Select(line => line.TrimEnd('\r')[2..].Trim()))
                {
                    Assert.Matches($"<li\\b[^>]*>{Regex.Escape(item)}</li>", renderedHtml);
                }
            }
            else
            {
                Assert.Contains(block, renderedHtml);
            }
        }
    }

    private static string GetResource(string key, string language) =>
        Resources.GetString(key, CultureInfo.GetCultureInfo(language)) ?? string.Empty;

    private static void AssertExternalSourceLink(string html, OfficialSource source, string language)
    {
        var encodedUrl = WebUtility.HtmlEncode(source.Url.AbsoluteUri);
        var anchor = Regex.Matches(html, "<a\\b(?<attributes>[^>]*)>(?<content>.*?)</a>", RegexOptions.Singleline)
            .Cast<Match>()
            .FirstOrDefault(match =>
            {
                var attributes = match.Groups["attributes"].Value;
                var content = Regex.Replace(match.Groups["content"].Value, "<!--.*?-->", string.Empty, RegexOptions.Singleline);
                return attributes.Contains($"href=\"{encodedUrl}\"", StringComparison.Ordinal) &&
                    attributes.Contains("target=\"_blank\"", StringComparison.Ordinal) &&
                    attributes.Contains("rel=\"noopener noreferrer\"", StringComparison.Ordinal) &&
                    ReadRenderedText(content).Contains(source.Title, StringComparison.Ordinal);
            });

        Assert.NotNull(anchor);
        var content = Regex.Replace(anchor.Groups["content"].Value, "<!--.*?-->", string.Empty, RegexOptions.Singleline);
        var visibleContent = WebUtility.HtmlDecode(Regex.Replace(content, "<[^>]+>", string.Empty));
        Assert.Matches("<span\\b(?=[^>]*aria-hidden=\\\"true\\\")(?=[^>]*class=\\\"external-link-icon\\\")[^>]*>↗</span>", content);
        Assert.Contains($"({GetResource("Common.OpensInNewTab", language)})", visibleContent);
        Assert.DoesNotContain("Common.OpensInNewTab", html);
    }

    private static string ReadRenderedText(string html) => WebUtility.HtmlDecode(
        Regex.Replace(Regex.Replace(html, "<!--.*?-->", string.Empty, RegexOptions.Singleline), "<[^>]+>", string.Empty));

    private static Task<CatalogData> LoadCatalogAsync() => ProductionCatalog.Value;

    private static CatalogData WithTranslations(CatalogData catalog, Guid entryId, string language, VerificationStatus status)
    {
        var translations = catalog.InformationEntryTranslations.Select(translation =>
            translation.InformationEntryId == entryId && translation.LanguageCode == language
                ? new InformationEntryTranslation(
                    translation.InformationEntryId,
                    translation.LanguageCode,
                    translation.Title,
                    translation.Summary,
                    translation.Content,
                    status,
                    translation.LastUpdatedAt)
                : translation).ToArray();
        return CreateCatalog(catalog.Countries, catalog.Categories, catalog.InformationEntries, translations, catalog.OfficialSources);
    }

    private static CatalogData WithContent(CatalogData catalog, Guid entryId, string language, string content)
    {
        var translations = catalog.InformationEntryTranslations.Select(translation =>
            translation.InformationEntryId == entryId && translation.LanguageCode == language
                ? new InformationEntryTranslation(
                    translation.InformationEntryId,
                    translation.LanguageCode,
                    translation.Title,
                    translation.Summary,
                    content,
                    translation.VerificationStatus,
                    translation.LastUpdatedAt)
                : translation).ToArray();
        return CreateCatalog(catalog.Countries, catalog.Categories, catalog.InformationEntries, translations, catalog.OfficialSources);
    }

    private static CatalogData CreateCatalog(
        IEnumerable<Country> countries,
        IEnumerable<InformationCategory> categories,
        IEnumerable<InformationEntry> entries,
        IEnumerable<InformationEntryTranslation> translations,
        IEnumerable<OfficialSource> sources) =>
        (CatalogData)Activator.CreateInstance(
            typeof(CatalogData),
            BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            args: [countries, categories, entries, translations, sources],
            culture: CultureInfo.InvariantCulture)!;

    private static async Task<string> RenderAsync(
        CatalogData catalog,
        string language,
        string query = "",
        string environmentName = "Development",
        ComparisonFactCatalog? comparisonFacts = null)
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
            services.AddSingleton(catalog);
            services.AddSingleton(comparisonFacts ?? await FactCatalog.Value);
            services.AddSingleton<IWebHostEnvironment>(new TestWebHostEnvironment(environmentName));
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

    private static ComparisonFactCatalog ReplaceFact(
        ComparisonFactCatalog catalog,
        ComparisonTopicFacts topic,
        ComparisonAspect aspect,
        CountryComparisonFact replacement) => WithFact(catalog, topic, aspect, replacement);

    private static ComparisonFactCatalog WithFact(
        ComparisonFactCatalog catalog,
        ComparisonTopicFacts targetTopic,
        ComparisonAspect targetAspect,
        CountryComparisonFact replacement) => catalog with
    {
        Topics = catalog.Topics.Select(topic => topic != targetTopic ? topic : topic with
        {
            Aspects = topic.Aspects.Select(aspect => aspect != targetAspect ? aspect : aspect with
            {
                Facts = aspect.Facts.Select(fact => fact.CountrySlug == replacement.CountrySlug ? replacement : fact).ToArray()
            }).ToArray()
        }).ToArray()
    };

    private static ComparisonFactCatalog WithStatus(ComparisonFactCatalog catalog, ComparisonFactStatus status) => catalog with
    {
        Topics = catalog.Topics.Select(topic => topic with
        {
            Aspects = topic.Aspects.Select(aspect => aspect with
            {
                Facts = aspect.Facts.Select(fact => fact with { Status = status }).ToArray()
            }).ToArray()
        }).ToArray()
    };

    private sealed class TestWebHostEnvironment(string environmentName) : IWebHostEnvironment
    {
        public string ApplicationName { get; set; } = "RefugeeNavigator.Tests";
        public string EnvironmentName { get; set; } = environmentName;
        public string WebRootPath { get; set; } = AppContext.BaseDirectory;
        public string ContentRootPath { get; set; } = AppContext.BaseDirectory;
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }
}
