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

    private static readonly HashSet<Guid> NetherlandsPromotedArticleIds = new()
    {
        Guid.Parse("6a077f79-89c2-55d6-81f3-9ba7279979c9"),
        Guid.Parse("d5c4d5fa-3b0f-5cc2-865f-841bd40911d1"),
        Guid.Parse("19dde97c-cb42-5a90-97f7-e344f2139d2e"),
        Guid.Parse("8d466e7d-93f7-596e-a44f-188e04396aa0")
    };

    private static readonly HashSet<string> NetherlandsApprovedAspects = new(StringComparer.Ordinal)
    {
        "work-permission", "work-employer-duties", "work-minimum-pay",
        "healthcare-access", "healthcare-proof", "healthcare-urgent",
        "housing-initial", "housing-private-rental", "housing-financial-support",
        "social-subsistence", "social-application", "social-child-benefits"
    };

    private static readonly Lazy<Task<CatalogData>> ProductionCatalog = new(
        () => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());

    private static readonly Lazy<Task<ComparisonFactCatalog>> FactCatalog = new(async () =>
    {
        var catalog = await LoadCatalogAsync();
        return await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(catalog);
    });

    [Fact]
    public async Task StructuredCatalogHasEightTopicsTwentyFourAspectsAndSevenCountryFacts()
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        JsonComparisonFactProvider.Validate(facts, catalog);

        Assert.Equal(8, facts.Topics.Count);
        Assert.Equal(24, facts.Topics.Sum(topic => topic.Aspects.Count));
        Assert.Equal(24, facts.Topics.SelectMany(topic => topic.Aspects).Select(aspect => aspect.QuestionResourceKey).Distinct(StringComparer.Ordinal).Count());
        Assert.Equal(168, facts.Topics.SelectMany(topic => topic.Aspects).Sum(aspect => aspect.Facts.Count));
        Assert.Equal(161, facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).Count(fact => fact.IsAvailable));
        Assert.Equal(7, facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).Count(fact => !fact.IsAvailable));
        Assert.Equal(132, facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).Count(fact => fact.Status == ComparisonFactStatus.Approved));
        Assert.Equal(36, facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).Count(fact => fact.Status == ComparisonFactStatus.Draft));
        Assert.All(facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).Where(fact => fact.CountrySlug is not ("sweden" or "netherlands")), fact => Assert.Equal(ComparisonFactStatus.Approved, fact.Status));
        Assert.Equal(324, facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).Sum(fact => fact.OfficialSourceIds.Count));
        Assert.Equal(56, catalog.InformationEntries.Count);
        Assert.Equal(168, catalog.InformationEntryTranslations.Count);
        Assert.Equal(132, catalog.InformationEntryTranslations.Count(translation => translation.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(36, catalog.InformationEntryTranslations.Count(translation => translation.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(translation => translation.VerificationStatus == VerificationStatus.Outdated));
        var czechiaId = Guid.Parse("10000000-0000-0000-0000-000000000004");
        var czechiaEntryIds = catalog.InformationEntries.Where(entry => entry.CountryId == czechiaId).Select(entry => entry.Id).ToHashSet();
        var spainId = Guid.Parse("10000000-0000-0000-0000-000000000005");
        var spainEntryIds = catalog.InformationEntries.Where(entry => entry.CountryId == spainId).Select(entry => entry.Id).ToHashSet();
        var swedenId = Guid.Parse("10000000-0000-0000-0000-000000000006");
        var swedenEntryIds = catalog.InformationEntries.Where(entry => entry.CountryId == swedenId).Select(entry => entry.Id).ToHashSet();
        var netherlandsId = Guid.Parse("10000000-0000-0000-0000-000000000007");
        var netherlandsEntryIds = catalog.InformationEntries.Where(entry => entry.CountryId == netherlandsId).Select(entry => entry.Id).ToHashSet();
        Assert.All(catalog.InformationEntryTranslations.Where(translation => czechiaEntryIds.Contains(translation.InformationEntryId)),
            translation => Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus));
        Assert.All(catalog.InformationEntryTranslations.Where(translation => spainEntryIds.Contains(translation.InformationEntryId)),
            translation => Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus));
        Assert.All(catalog.InformationEntryTranslations.Where(translation => swedenEntryIds.Contains(translation.InformationEntryId)),
            translation => Assert.Equal(VerificationStatus.NeedsReview, translation.VerificationStatus));
        Assert.All(catalog.InformationEntryTranslations.Where(translation => netherlandsEntryIds.Contains(translation.InformationEntryId)),
            translation => Assert.Equal(NetherlandsPromotedArticleIds.Contains(translation.InformationEntryId)
                ? VerificationStatus.Verified : VerificationStatus.NeedsReview, translation.VerificationStatus));
        Assert.All(catalog.InformationEntryTranslations.Where(translation => !czechiaEntryIds.Contains(translation.InformationEntryId)
                                                                            && !spainEntryIds.Contains(translation.InformationEntryId)
                                                                            && !swedenEntryIds.Contains(translation.InformationEntryId)
                                                                            && !netherlandsEntryIds.Contains(translation.InformationEntryId)
                                                                            && translation.InformationEntryId != Guid.Parse("afc14824-4035-4ca7-9662-6d5352ba63cb")),
            translation => Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus));
        var unavailableFacts = facts.Topics.SelectMany(topic => topic.Aspects)
            .SelectMany(aspect => aspect.Facts.Where(fact => !fact.IsAvailable)
                .Select(fact => (aspect.Id, fact.CountrySlug, fact))).ToArray();
        Assert.Equal(new[]
        {
            ("education-pupil-language", "germany"),
            ("employment-assistance-conditions", "denmark"),
            ("housing-financial-support", "spain"),
            ("protection-dates", "spain"),
            ("residence-filing", "netherlands"),
            ("social-child-benefits", "spain"),
            ("social-child-benefits", "sweden")
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
            Assert.Equal(7, topic.Aspects.SelectMany(aspect => aspect.Facts).Select(fact => fact.CountrySlug).Distinct().Count());
            Assert.Equal(topic.CategorySlug, Assert.Single(CountryComparisonTopicMap.All, mapped => mapped.ResourceKey == topic.TopicResourceKey).CategorySlug);
            foreach (var aspect in topic.Aspects)
            foreach (var fact in aspect.Facts)
            {
                var expectedStatus = fact.CountrySlug == "sweden" ||
                    fact.CountrySlug == "netherlands" && !NetherlandsApprovedAspects.Contains(aspect.Id)
                    ? ComparisonFactStatus.Draft
                    : ComparisonFactStatus.Approved;
                Assert.Equal(expectedStatus, fact.Status);
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
    public async Task SwedenHasTwentyFourDraftFactsWithOwnedSourcesAndOneUnavailableAspect()
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var swedenEntryIds = new Dictionary<string, Guid>(StringComparer.Ordinal)
        {
            ["protection-scheme"] = Guid.Parse("d2ab273d-21b7-44e4-b408-f4269a8753d8"),
            ["protection-application"] = Guid.Parse("d2ab273d-21b7-44e4-b408-f4269a8753d8"),
            ["protection-dates"] = Guid.Parse("d2ab273d-21b7-44e4-b408-f4269a8753d8"),
            ["residence-routes"] = Guid.Parse("befa7777-af70-4269-883c-8918961554dc"),
            ["residence-conditions"] = Guid.Parse("befa7777-af70-4269-883c-8918961554dc"),
            ["residence-filing"] = Guid.Parse("befa7777-af70-4269-883c-8918961554dc"),
            ["employment-vacancies"] = Guid.Parse("a218013b-3a77-4ba2-829a-d724689837f2"),
            ["employment-institutions"] = Guid.Parse("a218013b-3a77-4ba2-829a-d724689837f2"),
            ["employment-assistance-conditions"] = Guid.Parse("a218013b-3a77-4ba2-829a-d724689837f2"),
            ["work-permission"] = Guid.Parse("55949af5-b4fe-404e-bcb7-6ebb37131bab"),
            ["work-employer-duties"] = Guid.Parse("55949af5-b4fe-404e-bcb7-6ebb37131bab"),
            ["work-minimum-pay"] = Guid.Parse("55949af5-b4fe-404e-bcb7-6ebb37131bab"),
            ["healthcare-access"] = Guid.Parse("2234b91f-4021-40b2-b9d5-a029a6b533f2"),
            ["healthcare-proof"] = Guid.Parse("2234b91f-4021-40b2-b9d5-a029a6b533f2"),
            ["healthcare-urgent"] = Guid.Parse("2234b91f-4021-40b2-b9d5-a029a6b533f2"),
            ["housing-initial"] = Guid.Parse("e0f307e9-7991-4350-89f4-2f84a9047393"),
            ["housing-private-rental"] = Guid.Parse("e0f307e9-7991-4350-89f4-2f84a9047393"),
            ["housing-financial-support"] = Guid.Parse("e0f307e9-7991-4350-89f4-2f84a9047393"),
            ["education-school-enrolment"] = Guid.Parse("cac58c0c-ce69-41ac-877e-4fd124105b89"),
            ["education-pupil-language"] = Guid.Parse("cac58c0c-ce69-41ac-877e-4fd124105b89"),
            ["education-higher-admission"] = Guid.Parse("cac58c0c-ce69-41ac-877e-4fd124105b89"),
            ["social-subsistence"] = Guid.Parse("8b5cc53a-9933-4366-8421-d795966267ec"),
            ["social-application"] = Guid.Parse("8b5cc53a-9933-4366-8421-d795966267ec"),
            ["social-child-benefits"] = Guid.Parse("8b5cc53a-9933-4366-8421-d795966267ec")
        };
        var swedenFacts = facts.Topics.SelectMany(topic => topic.Aspects)
            .SelectMany(aspect => aspect.Facts.Where(fact => fact.CountrySlug == "sweden")
                .Select(fact => (aspect.Id, fact))).ToArray();
        var ownedEntryIds = swedenEntryIds.Values.ToHashSet();
        var swedenSourceIds = catalog.OfficialSources.Where(source => ownedEntryIds.Contains(source.InformationEntryId))
            .Select(source => source.Id).ToHashSet();

        Assert.Equal(24, swedenFacts.Length);
        Assert.Equal(23, swedenFacts.Count(item => item.fact.IsAvailable));
        Assert.Equal(1, swedenFacts.Count(item => !item.fact.IsAvailable));
        Assert.Equal(68, swedenSourceIds.Count);
        Assert.All(swedenFacts, item =>
        {
            var fact = item.fact;
            Assert.Equal(swedenEntryIds[item.Id], fact.InformationEntryId);
            Assert.Equal(ComparisonFactStatus.Draft, fact.Status);
            Assert.Equal(new[] { "en", "ru", "uk" }, fact.Values.Keys.Order(StringComparer.Ordinal).ToArray());
            Assert.All(fact.Values.Values, value =>
            {
                Assert.False(string.IsNullOrWhiteSpace(value));
                Assert.DoesNotContain("https://", value, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("SE-", value, StringComparison.Ordinal);
                Assert.DoesNotMatch(@"\b(?:TP|RES|JOB|WR|HEALTH|HOUSE|EDU|SOC)-S\d+\b", value);
            });
            if (fact.IsAvailable)
            {
                Assert.InRange(fact.OfficialSourceIds.Count, 1, 3);
                Assert.All(fact.OfficialSourceIds, sourceId =>
                {
                    Assert.Contains(sourceId, swedenSourceIds);
                    var source = Assert.Single(catalog.OfficialSources, itemSource => itemSource.Id == sourceId);
                    Assert.Equal(fact.InformationEntryId, source.InformationEntryId);
                });
            }
            else
            {
                Assert.Equal("social-child-benefits", item.Id);
                Assert.Empty(fact.OfficialSourceIds);
                Assert.Equal(GetResource("Compare.NotEstablishedInReviewedSources", "en"), fact.Values["en"]);
                Assert.Equal(GetResource("Compare.NotEstablishedInReviewedSources", "uk"), fact.Values["uk"]);
                Assert.Equal(GetResource("Compare.NotEstablishedInReviewedSources", "ru"), fact.Values["ru"]);
            }
        });
    }

    [Theory]
    [InlineData("en", "Sweden")]
    [InlineData("uk", "Швеція")]
    [InlineData("ru", "Швеция")]
    public async Task SwedenDraftComparisonIsPreviewOnlyAndSelectorIsLocalized(string language, string countryName)
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var swedenFacts = facts.Topics.SelectMany(topic => topic.Aspects)
            .SelectMany(aspect => aspect.Facts.Where(fact => fact.CountrySlug == "sweden" && fact.IsAvailable)).ToArray();
        var query = "?country1=sweden&country2=germany&category=all";
        var developmentHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, language, query));
        Assert.Contains("value=\"sweden\"", developmentHtml);
        Assert.Contains(countryName, developmentHtml);
        Assert.Contains(GetResource("Compare.DraftPreviewNotice", language), developmentHtml);
        Assert.Contains(GetResource("Compare.DraftStatus", language), developmentHtml);
        Assert.DoesNotContain("Country.Sweden", developmentHtml, StringComparison.Ordinal);
        Assert.All(swedenFacts, fact => Assert.Contains(fact.Values[language], developmentHtml));

        var productionHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, language, query, Environments.Production));
        Assert.DoesNotContain(GetResource("Compare.DraftPreviewNotice", language), productionHtml);
        Assert.DoesNotContain(GetResource("Compare.DraftStatus", language), productionHtml);
        Assert.DoesNotContain("class=\"draft-label\"", productionHtml);
        Assert.All(swedenFacts, fact => Assert.DoesNotContain(fact.Values[language], productionHtml));
    }

    [Fact]
    public async Task NetherlandsHasTwentyFourFactsMappedToOwnedSourcesAndSafeUnavailableAnswer()
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var expectedArticleIds = new Dictionary<string, Guid>(StringComparer.Ordinal)
        {
            ["protection-scheme"] = Guid.Parse("244b1055-4d8c-55b2-820e-60ff018c3b1f"),
            ["protection-application"] = Guid.Parse("244b1055-4d8c-55b2-820e-60ff018c3b1f"),
            ["protection-dates"] = Guid.Parse("244b1055-4d8c-55b2-820e-60ff018c3b1f"),
            ["residence-routes"] = Guid.Parse("c5f5f833-0ab7-5e1a-9325-87a92b21d2ad"),
            ["residence-conditions"] = Guid.Parse("c5f5f833-0ab7-5e1a-9325-87a92b21d2ad"),
            ["residence-filing"] = Guid.Parse("c5f5f833-0ab7-5e1a-9325-87a92b21d2ad"),
            ["employment-vacancies"] = Guid.Parse("757c1db6-b66b-54b9-ac14-d8a3da958ef5"),
            ["employment-institutions"] = Guid.Parse("757c1db6-b66b-54b9-ac14-d8a3da958ef5"),
            ["employment-assistance-conditions"] = Guid.Parse("757c1db6-b66b-54b9-ac14-d8a3da958ef5"),
            ["work-permission"] = Guid.Parse("6a077f79-89c2-55d6-81f3-9ba7279979c9"),
            ["work-employer-duties"] = Guid.Parse("6a077f79-89c2-55d6-81f3-9ba7279979c9"),
            ["work-minimum-pay"] = Guid.Parse("6a077f79-89c2-55d6-81f3-9ba7279979c9"),
            ["healthcare-access"] = Guid.Parse("d5c4d5fa-3b0f-5cc2-865f-841bd40911d1"),
            ["healthcare-proof"] = Guid.Parse("d5c4d5fa-3b0f-5cc2-865f-841bd40911d1"),
            ["healthcare-urgent"] = Guid.Parse("d5c4d5fa-3b0f-5cc2-865f-841bd40911d1"),
            ["housing-initial"] = Guid.Parse("19dde97c-cb42-5a90-97f7-e344f2139d2e"),
            ["housing-private-rental"] = Guid.Parse("19dde97c-cb42-5a90-97f7-e344f2139d2e"),
            ["housing-financial-support"] = Guid.Parse("19dde97c-cb42-5a90-97f7-e344f2139d2e"),
            ["education-school-enrolment"] = Guid.Parse("7633a085-4555-5a8f-a9a8-747a0f6e0545"),
            ["education-pupil-language"] = Guid.Parse("7633a085-4555-5a8f-a9a8-747a0f6e0545"),
            ["education-higher-admission"] = Guid.Parse("7633a085-4555-5a8f-a9a8-747a0f6e0545"),
            ["social-subsistence"] = Guid.Parse("8d466e7d-93f7-596e-a44f-188e04396aa0"),
            ["social-application"] = Guid.Parse("8d466e7d-93f7-596e-a44f-188e04396aa0"),
            ["social-child-benefits"] = Guid.Parse("8d466e7d-93f7-596e-a44f-188e04396aa0")
        };
        var netherlandsFacts = facts.Topics.SelectMany(topic => topic.Aspects)
            .SelectMany(aspect => aspect.Facts.Where(fact => fact.CountrySlug == "netherlands")
                .Select(fact => (aspect.Id, fact))).ToArray();

        Assert.Equal(24, netherlandsFacts.Length);
        Assert.Equal(23, netherlandsFacts.Count(item => item.fact.IsAvailable));
        Assert.Equal(1, netherlandsFacts.Count(item => !item.fact.IsAvailable));
        Assert.Equal(
            new[]
            {
                Guid.Parse("979a64e2-7a45-5c2b-8151-9ad7f57d61d8"),
                Guid.Parse("fda6d123-58d8-5858-9e91-311f5266f5ee"),
                Guid.Parse("249d9618-2212-5251-8f6e-8de0766e0cf3")
            },
            netherlandsFacts.Single(item => item.Id == "residence-routes").fact.OfficialSourceIds);
        Assert.Equal(144, facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts)
            .Count(fact => fact.CountrySlug != "netherlands"));
        Assert.All(netherlandsFacts, item =>
        {
            var fact = item.fact;
            Assert.Equal(expectedArticleIds[item.Id], fact.InformationEntryId);
            Assert.Equal(NetherlandsApprovedAspects.Contains(item.Id) ? ComparisonFactStatus.Approved : ComparisonFactStatus.Draft,
                fact.Status);
            Assert.Equal(new[] { "en", "ru", "uk" }, fact.Values.Keys.Order(StringComparer.Ordinal).ToArray());
            Assert.All(fact.Values.Values, value =>
            {
                Assert.False(string.IsNullOrWhiteSpace(value));
                Assert.DoesNotContain("https://", value, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotMatch(@"\b(?:NL-TP|NL-EMP|NL-HEALTH|NL-HOUSING|NL-EDU|NL-SOC)", value);
            });
            if (fact.IsAvailable)
            {
                var maxSources = item.Id == "healthcare-access" && fact.CountrySlug == "netherlands" ? 4 : 3;
                Assert.InRange(fact.OfficialSourceIds.Count, 1, maxSources);
                Assert.All(fact.OfficialSourceIds, sourceId =>
                {
                    var source = Assert.Single(catalog.OfficialSources, itemSource => itemSource.Id == sourceId);
                    Assert.Equal(fact.InformationEntryId, source.InformationEntryId);
                });
            }
            else
            {
                Assert.Equal("residence-filing", item.Id);
                Assert.Empty(fact.OfficialSourceIds);
                Assert.Equal(GetResource("Compare.NotEstablishedInReviewedSources", "en"), fact.Values["en"]);
                Assert.Equal(GetResource("Compare.NotEstablishedInReviewedSources", "uk"), fact.Values["uk"]);
                Assert.Equal(GetResource("Compare.NotEstablishedInReviewedSources", "ru"), fact.Values["ru"]);
            }
        });

        var employmentSources = catalog.OfficialSources.Where(source => source.InformationEntryId == expectedArticleIds["employment-institutions"]).ToArray();
        var uwvSource = Assert.Single(employmentSources, source => source.Id == Guid.Parse("1690f9d6-0721-5005-b9de-fda56864ed70"));
        Assert.Equal(new Uri("https://www.uwv.nl/en/individuals/working-in-the-netherlands/ukrainian-refugee"), uwvSource.Url);
        Assert.Contains(netherlandsFacts.Single(item => item.Id == "employment-institutions").fact.OfficialSourceIds, sourceId => sourceId == uwvSource.Id);

        var healthcare = netherlandsFacts.Single(item => item.Id == "healthcare-access").fact;
        Assert.True(healthcare.IsAvailable);
        Assert.Contains("Official pages conflict", healthcare.Values["en"]);
        Assert.Contains("does not resolve", healthcare.Values["en"]);
        Assert.Contains("суперечать", healthcare.Values["uk"]);
        Assert.Contains("не определяет", healthcare.Values["ru"]);
    }

    [Fact]
    public async Task NetherlandsDuoCourseFeeConditionRequiresNationalityReasonAndAllOtherCriteriaInAllLanguages()
    {
        var fact = (await FactCatalog.Value).Topics.SelectMany(topic => topic.Aspects)
            .Single(aspect => aspect.Id == "education-higher-admission").Facts
            .Single(item => item.CountrySlug == "netherlands");

        Assert.Equal(ComparisonFactStatus.Draft, fact.Status);
        Assert.Contains("no eligibility for student finance or a secondary-education student grant because the nationality criteria are not met", fact.Values["en"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("відсутності права на студентське фінансування чи грант для учнів закладів середньої освіти через невідповідність критеріям громадянства", fact.Values["uk"], StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("та права на студентське фінансування чи грант", fact.Values["uk"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("отсутствие права на студенческое финансирование или грант для учащихся среднего образования из-за несоответствия критериям гражданства", fact.Values["ru"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("not free education or guaranteed admission", fact.Values["en"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("не означає безоплатної освіти чи гарантованого вступу", fact.Values["uk"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("не означает бесплатного образования или гарантированного поступления", fact.Values["ru"], StringComparison.OrdinalIgnoreCase);

        var nationalityReasons = new Dictionary<string, string>
        {
            ["en"] = "because the nationality criteria are not met",
            ["uk"] = "через невідповідність критеріям громадянства",
            ["ru"] = "из-за несоответствия критериям гражданства"
        };
        var articleNationalityReasons = new Dictionary<string, string>
        {
            ["en"] = "because you do not meet the nationality criteria",
            ["uk"] = "через невідповідність критеріям громадянства",
            ["ru"] = "из-за несоответствия критериям гражданства"
        };
        var catalog = await LoadCatalogAsync();
        var articleTranslations = catalog.InformationEntryTranslations
            .Where(item => item.InformationEntryId == Guid.Parse("7633a085-4555-5a8f-a9a8-747a0f6e0545"))
            .ToDictionary(item => item.LanguageCode);
        var preservedConditions = new Dictionary<string, string[]>
        {
            ["en"] = ["only if all criteria are met", "person under temporary protection", "age 18 to under 31 on 1 August", "inability to pay and no sponsor", "31 July 2027", "2026–27"],
            ["uk"] = ["лише за виконання всіх критеріїв", "людина під тимчасовим захистом", "віку від 18 до 31 року (не включно) на 1 серпня", "неможливості сплатити й відсутності спонсора", "31 липня 2027 року", "2026–2027"],
            ["ru"] = ["только при выполнении всех критериев", "человек под временной защитой", "возраст от 18 до 31 года (не включая) на 1 августа", "невозможность оплатить обучение и отсутствие спонсора", "31 июля 2027 года", "2026–2027"]
        };
        foreach (var (language, reason) in nationalityReasons)
        {
            Assert.Contains(reason, fact.Values[language], StringComparison.OrdinalIgnoreCase);
            Assert.Contains(articleNationalityReasons[language], articleTranslations[language].Content, StringComparison.OrdinalIgnoreCase);
            foreach (var condition in preservedConditions[language])
                Assert.Contains(condition, fact.Values[language], StringComparison.OrdinalIgnoreCase);
        }
    }

    [Theory]
    [InlineData("en", "Netherlands")]
    [InlineData("uk", "Нідерланди")]
    [InlineData("ru", "Нидерланды")]
    public async Task NetherlandsApprovedFactsRenderAndDraftFactsRemainGatedInProduction(string language, string countryName)
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var netherlandsFacts = facts.Topics.SelectMany(topic => topic.Aspects)
            .SelectMany(aspect => aspect.Facts.Where(fact => fact.CountrySlug == "netherlands")
                .Select(fact => (aspect.Id, fact))).ToArray();
        var query = "?country1=netherlands&country2=germany&category=all";

        var developmentHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, language, query));
        Assert.Contains("value=\"netherlands\"", developmentHtml);
        Assert.Contains(countryName, developmentHtml);
        Assert.Contains(GetResource("Compare.DraftPreviewNotice", language), developmentHtml);
        Assert.Contains(GetResource("Compare.DraftStatus", language), developmentHtml);
        Assert.All(netherlandsFacts.Where(item => item.fact.IsAvailable),
            item => Assert.Contains(item.fact.Values[language], developmentHtml));

        var productionHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, language, query, Environments.Production));
        Assert.Contains("value=\"netherlands\"", productionHtml);
        Assert.DoesNotContain(GetResource("Compare.DraftPreviewNotice", language), productionHtml);
        Assert.DoesNotContain(GetResource("Compare.DraftStatus", language), productionHtml);
        Assert.DoesNotContain("class=\"draft-label\"", productionHtml);
        foreach (var (aspectId, fact) in netherlandsFacts)
        {
            Assert.Equal(NetherlandsApprovedAspects.Contains(aspectId) ? ComparisonFactStatus.Approved : ComparisonFactStatus.Draft,
                fact.Status);
            var panel = StructuredCountryPanel(productionHtml, aspectId, GetResource("Country.Netherlands", language));
            if (NetherlandsApprovedAspects.Contains(aspectId))
            {
                Assert.True(fact.IsAvailable);
                Assert.Contains(fact.Values[language], panel);
                Assert.Contains("class=\"fact-sources\"", panel);
                Assert.All(fact.OfficialSourceIds, sourceId =>
                {
                    var source = Assert.Single(catalog.OfficialSources, item =>
                        item.Id == sourceId && item.InformationEntryId == fact.InformationEntryId);
                    Assert.Contains($"href=\"{source.Url.AbsoluteUri}\"", panel);
                });
            }
            else
            {
                Assert.Contains(GetResource("Compare.NotEstablishedInReviewedSources", language), panel);
                if (fact.IsAvailable)
                    Assert.DoesNotContain(fact.Values[language], panel);
                else
                    Assert.Empty(fact.OfficialSourceIds);
                Assert.DoesNotContain("class=\"fact-sources\"", panel);
            }
        }

        var unavailable = netherlandsFacts.Single(item => item.Id == "residence-filing").fact;
        Assert.False(unavailable.IsAvailable);
        Assert.Empty(unavailable.OfficialSourceIds);
        Assert.Equal(ComparisonFactStatus.Draft, unavailable.Status);
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task SwedenComparisonDescriptionIsNeutralWhileDraftPreviewRemainsClear(string language)
    {
        var html = WebUtility.HtmlDecode(await RenderAsync(
            await LoadCatalogAsync(),
            language,
            "?country1=sweden&country2=denmark&category=all"));

        Assert.Contains(GetResource("Compare.Description", language), html);
        Assert.Contains(GetResource("Compare.DraftPreviewNotice", language), html);
        Assert.Contains(GetResource("Compare.DraftStatus", language), html);
        Assert.DoesNotContain("reviewed information", GetResource("Compare.Description", language), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("перевірену", GetResource("Compare.Description", language), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("проверенную", GetResource("Compare.Description", language), StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task SwedenComparisonPolishUsesNaturalTermsAndPreservesConditions()
    {
        var facts = await FactCatalog.Value;
        var aspects = facts.Topics.SelectMany(topic => topic.Aspects).ToDictionary(aspect => aspect.Id, StringComparer.Ordinal);
        var education = Assert.Single(aspects["education-school-enrolment"].Facts, fact => fact.CountrySlug == "sweden");
        var healthcare = Assert.Single(aspects["healthcare-access"].Facts, fact => fact.CountrySlug == "sweden");

        Assert.Equal(ComparisonFactStatus.Draft, education.Status);
        Assert.True(education.IsAvailable);
        Assert.Contains("Муніципалітет, відповідальний за місце проживання дитини", education.Values["uk"]);
        Assert.Contains("Муниципалитет, отвечающий за место жительства ребёнка", education.Values["ru"]);
        Assert.DoesNotContain("Домашній муніципалітет", education.Values["uk"]);
        Assert.DoesNotContain("Домашний муниципалитет", education.Values["ru"]);
        Assert.Contains("does not guarantee a place", education.Values["en"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("не гарантує місця", education.Values["uk"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("не гарантирует место", education.Values["ru"], StringComparison.OrdinalIgnoreCase);

        Assert.Equal(ComparisonFactStatus.Draft, healthcare.Status);
        Assert.True(healthcare.IsAvailable);
        Assert.Contains("Человек с разрешением на временное проживание, на которое распространяется это правило", healthcare.Values["ru"]);
        Assert.Contains("если он зарегистрирован в реестре населения", healthcare.Values["ru"]);
        Assert.Contains("медицинскую и стоматологическую помощь", healthcare.Values["ru"]);
        Assert.Contains("незарегистрированного ребёнка младше 18 лет", healthcare.Values["ru"]);
        Assert.Contains("пациентские сборы и региональные правила", healthcare.Values["ru"]);
    }

    [Fact]
    public async Task SpainHasTwentyFourApprovedFactsMappedToItsEightArticlesAndOwnedSources()
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var entryByAspect = new Dictionary<string, Guid>(StringComparer.Ordinal)
        {
            ["protection-scheme"] = Guid.Parse("b278c1d6-1add-4e65-8af1-e16431eaffb3"),
            ["protection-application"] = Guid.Parse("b278c1d6-1add-4e65-8af1-e16431eaffb3"),
            ["protection-dates"] = Guid.Parse("b278c1d6-1add-4e65-8af1-e16431eaffb3"),
            ["residence-routes"] = Guid.Parse("06ce2f10-d309-45b2-aced-e9dd8367ffed"),
            ["residence-conditions"] = Guid.Parse("06ce2f10-d309-45b2-aced-e9dd8367ffed"),
            ["residence-filing"] = Guid.Parse("06ce2f10-d309-45b2-aced-e9dd8367ffed"),
            ["employment-vacancies"] = Guid.Parse("a5107970-958a-4be6-9c98-ae484fc24a15"),
            ["employment-institutions"] = Guid.Parse("a5107970-958a-4be6-9c98-ae484fc24a15"),
            ["employment-assistance-conditions"] = Guid.Parse("a5107970-958a-4be6-9c98-ae484fc24a15"),
            ["work-permission"] = Guid.Parse("b76b631c-92de-4379-8136-c9e4e08e2fd2"),
            ["work-employer-duties"] = Guid.Parse("b76b631c-92de-4379-8136-c9e4e08e2fd2"),
            ["work-minimum-pay"] = Guid.Parse("b76b631c-92de-4379-8136-c9e4e08e2fd2"),
            ["healthcare-access"] = Guid.Parse("50829c0b-7ee0-4ee4-be7b-47dfd0dc408d"),
            ["healthcare-proof"] = Guid.Parse("50829c0b-7ee0-4ee4-be7b-47dfd0dc408d"),
            ["healthcare-urgent"] = Guid.Parse("50829c0b-7ee0-4ee4-be7b-47dfd0dc408d"),
            ["housing-initial"] = Guid.Parse("bd08644f-dd27-412c-9bfd-888de1fe304d"),
            ["housing-private-rental"] = Guid.Parse("bd08644f-dd27-412c-9bfd-888de1fe304d"),
            ["housing-financial-support"] = Guid.Parse("bd08644f-dd27-412c-9bfd-888de1fe304d"),
            ["education-school-enrolment"] = Guid.Parse("084c06c7-592b-4eed-bb20-6cd1913f14a2"),
            ["education-pupil-language"] = Guid.Parse("084c06c7-592b-4eed-bb20-6cd1913f14a2"),
            ["education-higher-admission"] = Guid.Parse("084c06c7-592b-4eed-bb20-6cd1913f14a2"),
            ["social-subsistence"] = Guid.Parse("d33628da-cb49-44b5-a7aa-ae6c6ac8f389"),
            ["social-application"] = Guid.Parse("d33628da-cb49-44b5-a7aa-ae6c6ac8f389"),
            ["social-child-benefits"] = Guid.Parse("d33628da-cb49-44b5-a7aa-ae6c6ac8f389")
        };
        var spainFacts = facts.Topics.SelectMany(topic => topic.Aspects)
            .SelectMany(aspect => aspect.Facts.Where(fact => fact.CountrySlug == "spain")
                .Select(fact => (aspect.Id, fact))).ToArray();

        Assert.Equal(24, spainFacts.Length);
        Assert.All(spainFacts, item =>
        {
            Assert.Equal(ComparisonFactStatus.Approved, item.fact.Status);
            Assert.Equal(entryByAspect[item.Id], item.fact.InformationEntryId);
            Assert.Equal(new[] { "en", "ru", "uk" }, item.fact.Values.Keys.Order(StringComparer.Ordinal).ToArray());
            Assert.All(item.fact.Values.Values, value => Assert.False(string.IsNullOrWhiteSpace(value)));
            if (item.fact.IsAvailable)
            {
                Assert.NotEmpty(item.fact.OfficialSourceIds);
                Assert.All(item.fact.OfficialSourceIds, sourceId => Assert.Contains(catalog.OfficialSources,
                    source => source.Id == sourceId && source.InformationEntryId == item.fact.InformationEntryId));
                Assert.DoesNotContain("Reserv+", string.Join(' ', item.fact.Values.Values), StringComparison.Ordinal);
                Assert.DoesNotContain("eVOD", string.Join(' ', item.fact.Values.Values), StringComparison.Ordinal);
            }
            else
            {
                Assert.Empty(item.fact.OfficialSourceIds);
                Assert.Equal("Not established in the reviewed sources.", item.fact.Values["en"]);
                Assert.Equal("Не встановлено за перевіреними джерелами.", item.fact.Values["uk"]);
                Assert.Equal("Не установлено по проверенным источникам.", item.fact.Values["ru"]);
            }
        });
        Assert.Equal(21, spainFacts.Count(item => item.fact.IsAvailable));
        Assert.Equal(3, spainFacts.Count(item => !item.fact.IsAvailable));
        Assert.Equal(new[] { "housing-financial-support", "protection-dates", "social-child-benefits" },
            spainFacts.Where(item => !item.fact.IsAvailable).Select(item => item.Id).Order(StringComparer.Ordinal).ToArray());

        var healthcareUrgent = Assert.Single(spainFacts, item => item.Id == "healthcare-urgent").fact;
        Assert.All(new[] { "en", "uk", "ru" }, language => Assert.Contains("112", healthcareUrgent.Values[language]));

        var minimumPay = Assert.Single(spainFacts, item => item.Id == "work-minimum-pay").fact;
        foreach (var (language, amounts) in new[]
        {
            ("en", new[] { "2026", "1,221", "40.70", "17,094" }),
            ("uk", new[] { "2026", "1 221", "40,70", "17 094" }),
            ("ru", new[] { "2026", "1 221", "40,70", "17 094" })
        })
        {
            Assert.All(amounts, amount => Assert.Contains(amount, minimumPay.Values[language]));
        }
        Assert.False(Assert.Single(spainFacts, item => item.Id == "protection-dates").fact.IsAvailable);
        Assert.False(Assert.Single(spainFacts, item => item.Id == "housing-financial-support").fact.IsAvailable);
        Assert.False(Assert.Single(spainFacts, item => item.Id == "social-child-benefits").fact.IsAvailable);
        Assert.Equal(24, catalog.InformationEntryTranslations.Count(translation =>
            translation.InformationEntryId == Guid.Parse("b278c1d6-1add-4e65-8af1-e16431eaffb3")
            || translation.InformationEntryId == Guid.Parse("06ce2f10-d309-45b2-aced-e9dd8367ffed")
            || translation.InformationEntryId == Guid.Parse("a5107970-958a-4be6-9c98-ae484fc24a15")
            || translation.InformationEntryId == Guid.Parse("b76b631c-92de-4379-8136-c9e4e08e2fd2")
            || translation.InformationEntryId == Guid.Parse("50829c0b-7ee0-4ee4-be7b-47dfd0dc408d")
            || translation.InformationEntryId == Guid.Parse("bd08644f-dd27-412c-9bfd-888de1fe304d")
            || translation.InformationEntryId == Guid.Parse("084c06c7-592b-4eed-bb20-6cd1913f14a2")
            || translation.InformationEntryId == Guid.Parse("d33628da-cb49-44b5-a7aa-ae6c6ac8f389")));
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

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task ProductionRendersApprovedFactsIndependentlyFromDraftFacts(string language)
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var topic = Assert.Single(facts.Topics, item => item.TopicResourceKey == "Compare.Topic.WorkAuthorizationRights");
        var aspect = Assert.Single(topic.Aspects, item => item.Id == "work-permission");
        var germanyFact = Assert.Single(aspect.Facts, fact => fact.CountrySlug == "germany");
        var privateDraftAnswer = $"DRAFT-ONLY-ANSWER-{language}";
        var draftFact = germanyFact with
        {
            Status = ComparisonFactStatus.Draft,
            Values = new Dictionary<string, string>(StringComparer.Ordinal)
            {
                ["en"] = "DRAFT-ONLY-ANSWER-en",
                ["uk"] = "DRAFT-ONLY-ANSWER-uk",
                ["ru"] = "DRAFT-ONLY-ANSWER-ru"
            }
        };
        var mixedFacts = WithFact(facts, topic, aspect, draftFact);
        const string query = "?country1=germany&country2=poland&category=employment";

        var productionHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, language, query, Environments.Production, mixedFacts));
        var draftPanel = StructuredCountryPanel(productionHtml, aspect.Id, GetResource("Country.Germany", language));
        var developmentHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, language, query, comparisonFacts: mixedFacts));
        var developmentDraftPanel = StructuredCountryPanel(developmentHtml, aspect.Id, GetResource("Country.Germany", language));
        var polandFact = Assert.Single(aspect.Facts, fact => fact.CountrySlug == "poland");

        Assert.Contains("class=\"structured-comparison\"", productionHtml);
        Assert.DoesNotContain(privateDraftAnswer, productionHtml);
        Assert.Contains(GetResource("Compare.NotEstablishedInReviewedSources", language), draftPanel);
        Assert.DoesNotContain("class=\"draft-label\"", draftPanel);
        Assert.DoesNotContain("class=\"fact-sources\"", draftPanel);
        Assert.Contains(polandFact.Values[language], productionHtml);
        Assert.Contains(privateDraftAnswer, developmentDraftPanel);
        Assert.Contains(GetResource("Compare.DraftPreviewNotice", language), developmentHtml);
        Assert.Contains(GetResource("Compare.DraftStatus", language), developmentDraftPanel);
        Assert.Contains("class=\"fact-sources\"", developmentDraftPanel);
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task ProductionApprovedFactRequiresValidOwnedSources(string language)
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var topic = Assert.Single(facts.Topics, item => item.TopicResourceKey == "Compare.Topic.WorkAuthorizationRights");
        var aspect = Assert.Single(topic.Aspects, item => item.Id == "work-permission");
        var germanyFact = Assert.Single(aspect.Facts, fact => fact.CountrySlug == "germany");
        var replacement = germanyFact with
        {
            OfficialSourceIds = [Guid.NewGuid()],
            Values = new Dictionary<string, string>(StringComparer.Ordinal)
            {
                ["en"] = "INVALID-SOURCE-ANSWER-en",
                ["uk"] = "INVALID-SOURCE-ANSWER-uk",
                ["ru"] = "INVALID-SOURCE-ANSWER-ru"
            }
        };
        var changedFacts = WithFact(facts, topic, aspect, replacement);
        var html = WebUtility.HtmlDecode(await RenderAsync(catalog, language,
            "?country1=germany&country2=poland&category=employment", Environments.Production, changedFacts));
        var panel = StructuredCountryPanel(html, aspect.Id, GetResource("Country.Germany", language));

        Assert.DoesNotContain(replacement.Values[language], html);
        Assert.Contains(GetResource("Compare.NotEstablishedInReviewedSources", language), panel);
        Assert.DoesNotContain("class=\"fact-sources\"", panel);
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task ProductionApprovedFactRequiresVerifiedTranslationInTheSelectedLanguage(string language)
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var topic = Assert.Single(facts.Topics, item => item.TopicResourceKey == "Compare.Topic.FindingEmployment");
        var aspect = Assert.Single(topic.Aspects, item => item.Id == "employment-vacancies");
        var denmarkFact = Assert.Single(aspect.Facts, fact => fact.CountrySlug == "denmark");
        var entryId = denmarkFact.InformationEntryId;
        var original = Assert.Single(catalog.InformationEntryTranslations, item => item.InformationEntryId == entryId && item.LanguageCode == language);
        var baselineHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, language,
            "?country1=denmark&country2=germany&category=employment", Environments.Production, facts));
        var baselineSourceDetails = Regex.Matches(baselineHtml, "class=\"source-details\"").Count;
        Assert.True(baselineSourceDetails > 0);

        foreach (var status in new[] { VerificationStatus.NeedsReview, VerificationStatus.Outdated })
        {
            var changedCatalog = WithTranslations(catalog, entryId, language, status);
            var html = WebUtility.HtmlDecode(await RenderAsync(changedCatalog, language,
                "?country1=denmark&country2=germany&category=employment", Environments.Production, facts));
            var denmarkPanel = StructuredCountryPanel(html, aspect.Id, GetResource("Country.Denmark", language));

            Assert.DoesNotContain(denmarkFact.Values[language], html);
            Assert.Contains(GetResource("Compare.NotEstablishedInReviewedSources", language), denmarkPanel);
            Assert.DoesNotContain("class=\"fact-sources\"", denmarkPanel);
            Assert.DoesNotContain(original.Title, html);
            Assert.DoesNotContain(original.Summary, html);
            Assert.Equal(baselineSourceDetails - 1, Regex.Matches(html, "class=\"source-details\"").Count);
        }
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task NetherlandsApprovedFactsAreGatedPerLinkedArticleAndUnavailableFactDoesNotBlock(string language)
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var tp01Id = Guid.Parse("244b1055-4d8c-55b2-820e-60ff018c3b1f");
        var tp02Id = Guid.Parse("c5f5f833-0ab7-5e1a-9325-87a92b21d2ad");
        var tp02VerifiedCatalog = WithTranslations(catalog, tp02Id, language, VerificationStatus.Verified);
        var tp01NeedsReviewCatalog = WithTranslations(tp02VerifiedCatalog, tp01Id, language, VerificationStatus.NeedsReview);
        var tp01OutdatedCatalog = WithTranslations(tp02VerifiedCatalog, tp01Id, language, VerificationStatus.Outdated);
        var tp01VerifiedCatalog = WithTranslations(tp02VerifiedCatalog, tp01Id, language, VerificationStatus.Verified);
        var tp01Translation = Assert.Single(catalog.InformationEntryTranslations,
            item => item.InformationEntryId == tp01Id && item.LanguageCode == language);

        var gatedAspects = new[] { "protection-scheme", "protection-application", "protection-dates" };
        foreach (var aspectId in gatedAspects)
        {
            var (topic, aspect, fact) = FindFact(facts, aspectId, "netherlands");
            var secretValues = new Dictionary<string, string>(StringComparer.Ordinal)
            {
                ["en"] = $"NL-TP01-SECRET-{aspectId}-en",
                ["uk"] = $"NL-TP01-SECRET-{aspectId}-uk",
                ["ru"] = $"NL-TP01-SECRET-{aspectId}-ru"
            };
            facts = WithFact(facts, topic, aspect, fact with { Status = ComparisonFactStatus.Approved, Values = secretValues });
        }

        var (routesTopic, routesAspect, routesFact) = FindFact(facts, "residence-routes", "netherlands");
        facts = WithFact(facts, routesTopic, routesAspect, routesFact with { Status = ComparisonFactStatus.Approved });
        var filing = FindFact(facts, "residence-filing", "netherlands").Fact;
        var query = "?country1=netherlands&country2=germany&category=temporary-protection";
        var tp01VerifiedHtml = WebUtility.HtmlDecode(await RenderAsync(tp01VerifiedCatalog, language, query, Environments.Production, facts));
        var tp01VerifiedSourceDetails = Regex.Matches(tp01VerifiedHtml, "class=\"source-details\"").Count;
        Assert.True(tp01VerifiedSourceDetails > 0);

        foreach (var changedCatalog in new[] { tp01NeedsReviewCatalog, tp01OutdatedCatalog })
        {
            var html = WebUtility.HtmlDecode(await RenderAsync(changedCatalog, language, query, Environments.Production, facts));
            var tp01Panel = StructuredCountryPanel(html, "protection-scheme", GetResource("Country.Netherlands", language));
            var tp01ArticlePanel = FullArticleCountryPanel(html, "Compare.Topic.TemporaryProtectionApplication", GetResource("Country.Netherlands", language));

            Assert.Contains("class=\"structured-comparison\"", html);
            Assert.Contains(routesFact.Values[language], html);
            Assert.Contains(GetResource("Compare.NotEstablishedInReviewedSources", language), tp01Panel);
            Assert.DoesNotContain("class=\"fact-sources\"", tp01Panel);
            Assert.DoesNotContain(tp01Translation.Title, html);
            Assert.DoesNotContain(tp01Translation.Summary, html);
            Assert.DoesNotContain("class=\"article-body\"", tp01ArticlePanel);
            foreach (var aspectId in gatedAspects)
                Assert.DoesNotContain($"NL-TP01-SECRET-{aspectId}-{language}", html);
            Assert.False(filing.IsAvailable);
            Assert.Equal(ComparisonFactStatus.Draft, filing.Status);
            Assert.Empty(filing.OfficialSourceIds);
            Assert.Equal(GetResource("Compare.NotEstablishedInReviewedSources", language), filing.Values[language]);
            var filingPanel = StructuredCountryPanel(html, "residence-filing", GetResource("Country.Netherlands", language));
            Assert.Contains(GetResource("Compare.NotEstablishedInReviewedSources", language), filingPanel);
            Assert.DoesNotContain("class=\"fact-sources\"", filingPanel);
            Assert.Equal(tp01VerifiedSourceDetails - 1, Regex.Matches(html, "class=\"source-details\"").Count);
        }
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task ApprovedSpainFactsRenderInProductionForAllAndIndividualCategories(string language)
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var spainFacts = facts.Topics.SelectMany(topic => topic.Aspects)
            .SelectMany(aspect => aspect.Facts.Where(fact => fact.CountrySlug == "spain"))
            .ToArray();
        Assert.Equal(24, spainFacts.Length);
        Assert.All(spainFacts, fact => Assert.Equal(ComparisonFactStatus.Approved, fact.Status));

        var allHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, language,
            "?country1=spain&country2=germany&category=all", Environments.Production));
        Assert.Contains("class=\"structured-comparison\"", allHtml);
        Assert.DoesNotContain(GetResource("Compare.DraftPreviewNotice", language), allHtml);
        Assert.DoesNotContain(GetResource("Compare.DraftStatus", language), allHtml);
        Assert.DoesNotContain("class=\"draft-label\"", allHtml);

        foreach (var topic in facts.Topics)
        foreach (var aspect in topic.Aspects)
        {
            var spainFact = Assert.Single(aspect.Facts, fact => fact.CountrySlug == "spain");
            var germanyFact = Assert.Single(aspect.Facts, fact => fact.CountrySlug == "germany");
            Assert.Equal(ComparisonFactStatus.Approved, spainFact.Status);
            Assert.Equal(ComparisonFactStatus.Approved, germanyFact.Status);
            Assert.Contains(spainFact.IsAvailable
                ? spainFact.Values[language]
                : GetResource("Compare.NotEstablishedInReviewedSources", language), allHtml);
            Assert.Contains(germanyFact.IsAvailable
                ? germanyFact.Values[language]
                : GetResource("Compare.NotEstablishedInReviewedSources", language), allHtml);
            Assert.Contains($"/countries/spain/categories/{topic.CategorySlug}#entry-{spainFact.InformationEntryId:N}", allHtml);

            if (spainFact.IsAvailable)
            {
                Assert.NotEmpty(spainFact.OfficialSourceIds);
                foreach (var sourceId in spainFact.OfficialSourceIds)
                {
                    var source = Assert.Single(catalog.OfficialSources, item =>
                        item.Id == sourceId && item.InformationEntryId == spainFact.InformationEntryId);
                    Assert.Contains(source.Url.AbsoluteUri, allHtml);
                }
            }
            else
            {
                Assert.Empty(spainFact.OfficialSourceIds);
            }
        }

        var categories = CountryComparisonTopicMap.All.Select(topic => topic.CategorySlug)
            .Distinct(StringComparer.Ordinal).ToArray();
        Assert.Equal(6, categories.Length);
        foreach (var category in categories)
        {
            var categoryHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, language,
                $"?country1=spain&country2=germany&category={category}", Environments.Production));
            Assert.Contains("class=\"structured-comparison\"", categoryHtml);
            Assert.DoesNotContain(GetResource("Compare.DraftPreviewNotice", language), categoryHtml);
            Assert.DoesNotContain(GetResource("Compare.DraftStatus", language), categoryHtml);
            Assert.DoesNotContain("class=\"draft-label\"", categoryHtml);

            foreach (var topic in facts.Topics.Where(topic => topic.CategorySlug == category))
            foreach (var aspect in topic.Aspects)
            foreach (var countrySlug in new[] { "spain", "germany" })
            {
                var fact = Assert.Single(aspect.Facts, item => item.CountrySlug == countrySlug);
                Assert.Contains(fact.IsAvailable
                    ? fact.Values[language]
                    : GetResource("Compare.NotEstablishedInReviewedSources", language), categoryHtml);
            }
        }
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
        var expectedCountrySlugs = new[] { "czechia", "denmark", "germany", "netherlands", "poland", "spain", "sweden" };
        Assert.Equal(expectedCountrySlugs, CountryComparisonTopicMap.SupportedCountrySlugs.Order(StringComparer.Ordinal).ToArray());
        Assert.Equal(56, CountryComparisonTopicMap.All.SelectMany(topic => topic.EntryIds.Values).Distinct().Count());

        foreach (var topic in CountryComparisonTopicMap.All)
        {
            Assert.Equal(7, topic.EntryIds.Count);
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
                if (translation.VerificationStatus == VerificationStatus.NeedsReview)
                {
                    Assert.DoesNotContain(translation.Title, visibleHtml);
                    Assert.DoesNotContain(translation.Summary, visibleHtml);
                    Assert.DoesNotContain($"href=\"/countries/{countrySlug}/categories/{categorySlug}#entry-{entryId:N}\"", html);
                    continue;
                }
                Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
                Assert.Contains(translation.Title, visibleHtml);
                Assert.Contains(translation.Summary, visibleHtml);
                var sources = catalog.OfficialSources.Where(source => source.InformationEntryId == entryId).ToArray();
                Assert.NotEmpty(sources);
                AssertFullContentIsRendered(translation.Content, visibleHtml, sources);
                Assert.Contains($"href=\"/countries/{countrySlug}/categories/{categorySlug}#entry-{entryId:N}\"", html);

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
                    if (fact.InformationEntryId is { } entryId
                        && catalog.InformationEntryTranslations.Any(item => item.InformationEntryId == entryId
                            && item.LanguageCode == language && item.VerificationStatus == VerificationStatus.NeedsReview))
                    {
                        Assert.DoesNotContain(fact.Values[language], visibleHtml);
                        continue;
                    }
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

    [Theory]
    [InlineData("en", "official authority")]
    [InlineData("uk", "офіційний орган")]
    [InlineData("ru", "официальный орган")]
    public async Task FullArticleComparisonRendersOwnedInlineSourceLinks(string language, string localizedLabel)
    {
        var catalog = await LoadCatalogAsync();
        var topic = CountryComparisonTopicMap.ForCategory("housing")[0];
        var entryId = CountryComparisonTopicMap.EntryIdFor(topic, "denmark");
        var source = catalog.OfficialSources.First(item => item.InformationEntryId == entryId);
        Assert.Equal(VerificationStatus.Verified, catalog.InformationEntryTranslations.Single(item =>
            item.InformationEntryId == entryId && item.LanguageCode == language).VerificationStatus);
        var content = $"Contact [[source:{source.Id:D}|{localizedLabel}]] for current details.";
        var withInlineSource = WithContent(catalog, entryId, language, content);

        var html = await RenderAsync(
            withInlineSource,
            language,
            "?country1=denmark&country2=germany&category=housing");

        var inlineLink = Regex.Match(html,
            "<a class=\"article-source-link\"(?<attributes>[^>]*)>(?<content>.*?)</a>",
            RegexOptions.Singleline);
        Assert.True(inlineLink.Success, "No inline source link was rendered in the full-article comparison.");
        var href = Regex.Match(inlineLink.Groups["attributes"].Value, "href=\"(?<url>[^\"]+)\"");
        Assert.Equal(source.Url.AbsoluteUri, WebUtility.HtmlDecode(href.Groups["url"].Value));
        Assert.Contains("target=\"_blank\"", inlineLink.Groups["attributes"].Value);
        Assert.Contains("rel=\"noopener noreferrer\"", inlineLink.Groups["attributes"].Value);
        Assert.StartsWith(localizedLabel, ReadRenderedText(inlineLink.Groups["content"].Value), StringComparison.Ordinal);
        Assert.Contains("Contact ", WebUtility.HtmlDecode(html));
        Assert.Contains(" for current details.", WebUtility.HtmlDecode(html));
        Assert.Contains("class=\"source-details\"", html);
        Assert.Contains(source.Title, ReadRenderedText(html));
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

    private static void AssertFullContentIsRendered(
        string content,
        string renderedHtml,
        IReadOnlyCollection<OfficialSource> sources)
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
                    if (item.Contains("[[source:", StringComparison.Ordinal))
                    {
                        AssertInlineSourceContentIsRendered(item, renderedHtml, sources);
                    }
                    else
                    {
                        Assert.Matches($"<li\\b[^>]*>{Regex.Escape(item)}</li>", renderedHtml);
                    }
                }
            }
            else if (block.Contains("[[source:", StringComparison.Ordinal))
            {
                AssertInlineSourceContentIsRendered(block, renderedHtml, sources);
            }
            else
            {
                Assert.Contains(block, renderedHtml);
            }
        }
    }

    private static void AssertInlineSourceContentIsRendered(
        string content,
        string renderedHtml,
        IReadOnlyCollection<OfficialSource> sources)
    {
        var tokenPattern = new Regex(@"\[\[source:(?<id>[0-9a-fA-F-]{36})\|(?<label>[^\]]+)\]\]");
        var matches = tokenPattern.Matches(content);
        Assert.NotEmpty(matches);
        var cursor = 0;

        foreach (Match match in matches)
        {
            var text = content[cursor..match.Index].Trim();
            if (text.Length > 0)
            {
                Assert.Contains(text, renderedHtml);
            }

            Assert.True(Guid.TryParse(match.Groups["id"].Value, out var sourceId));
            var source = Assert.Single(sources, item => item.Id == sourceId);
            Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme);
            var label = match.Groups["label"].Value;
            var linkPattern = $"<a\\b(?=[^>]*class=\\\"article-source-link\\\")(?=[^>]*href=\\\"{Regex.Escape(WebUtility.HtmlEncode(source.Url.AbsoluteUri))}\\\")[^>]*>{Regex.Escape(label)}(?:<span\\b|</a>)";
            Assert.Matches(linkPattern, renderedHtml);
            cursor = match.Index + match.Length;
        }

        var trailingText = content[cursor..].Trim();
        if (trailingText.Length > 0)
        {
            Assert.Contains(trailingText, renderedHtml);
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

    private static (ComparisonTopicFacts Topic, ComparisonAspect Aspect, CountryComparisonFact Fact) FindFact(
        ComparisonFactCatalog catalog,
        string aspectId,
        string countrySlug)
    {
        var topic = Assert.Single(catalog.Topics, item => item.Aspects.Any(aspect => aspect.Id == aspectId));
        var aspect = Assert.Single(topic.Aspects, item => item.Id == aspectId);
        var fact = Assert.Single(aspect.Facts, item => item.CountrySlug == countrySlug);
        return (topic, aspect, fact);
    }

    private static string StructuredCountryPanel(string html, string aspectId, string countryName)
    {
        const string sectionStartTag = "<section class=\"structured-aspect\"";
        var startIndex = html.IndexOf(sectionStartTag, StringComparison.Ordinal);
        while (startIndex >= 0)
        {
            var tagEnd = html.IndexOf('>', startIndex);
            if (tagEnd >= 0 && html[startIndex..(tagEnd + 1)].Contains(aspectId, StringComparison.Ordinal))
                break;
            startIndex = html.IndexOf(sectionStartTag, tagEnd < 0 ? startIndex + sectionStartTag.Length : tagEnd + 1, StringComparison.Ordinal);
        }

        Assert.True(startIndex >= 0, $"Structured aspect '{aspectId}' was not rendered.");
        var nextStart = html.IndexOf(sectionStartTag, startIndex + sectionStartTag.Length, StringComparison.Ordinal);
        var aspectMarkup = nextStart < 0 ? html[startIndex..] : html[startIndex..nextStart];
        var countryPanels = Regex.Matches(aspectMarkup,
                "<article class=\"structured-country\"[^>]*>.*?</article>",
                RegexOptions.Singleline)
            .Cast<Match>()
            .Select(match => match.Value)
            .ToArray();
        return Assert.Single(countryPanels, panel => panel.Contains($">{countryName}</h", StringComparison.Ordinal));
    }

    private static string FullArticleCountryPanel(string html, string topicResourceKey, string countryName)
    {
        var headingId = $"topic-{topicResourceKey.Replace('.', '-')}";
        var topicStart = html.IndexOf($"<section class=\"comparison-topic\" aria-labelledby=\"{headingId}\"", StringComparison.Ordinal);
        Assert.True(topicStart >= 0, $"Full-article topic '{topicResourceKey}' was not rendered.");
        var nextTopic = html.IndexOf("<section class=\"comparison-topic\"", topicStart + 1, StringComparison.Ordinal);
        var topicMarkup = nextTopic < 0 ? html[topicStart..] : html[topicStart..nextTopic];
        var countryPanels = Regex.Matches(topicMarkup,
                "<article class=\"topic-country\"[^>]*>.*?</article>",
                RegexOptions.Singleline)
            .Cast<Match>()
            .Select(match => match.Value)
            .ToArray();
        return Assert.Single(countryPanels, panel => panel.Contains(countryName, StringComparison.Ordinal));
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
