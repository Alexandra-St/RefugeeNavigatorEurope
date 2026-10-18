using System.Globalization;
using System.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RefugeeNavigator.Data;
using RefugeeNavigator.Web.Guides;
using RefugeeNavigator.Web.Localization;
using GuideModel = RefugeeNavigator.Web.Guides.StepByStepGuide;
using GuideView = RefugeeNavigator.Web.Components.StepByStepGuideView;

namespace RefugeeNavigator.Tests;

public sealed class StepByStepGuideTests
{
    private static readonly IReadOnlyDictionary<string, string[]> ExpectedClaimIds = new Dictionary<string, string[]>(StringComparer.Ordinal)
    {
        ["arrival-check-local-process"] = ["TP03"],
        ["arrival-follow-application-process"] = ["TP02", "TP03"],
        ["arrival-check-online-route"] = ["TP03"],
        ["arrival-keep-pending-records"] = ["TP04"],
        ["arrival-check-90-day-exemption"] = ["TP03", "TP07"],
        ["pending-keep-records"] = ["TP04"],
        ["pending-check-document-effect"] = ["TP04"],
        ["pending-ask-about-work-entry"] = ["TP04", "TP05"],
        ["permit-read-individual-date"] = ["TP05"],
        ["permit-check-german-continuation-rule"] = ["TP05"],
        ["permit-separate-eu-and-german-dates"] = ["TP05", "TP06"],
        ["permit-request-written-clarification"] = ["TP05"],
        ["prior-protection-record-timeline"] = ["TP06"],
        ["prior-protection-clarify-eu-discrepancy"] = ["TP08"],
        ["prior-protection-separate-german-exemption"] = ["TP03", "TP07"],
        ["prior-protection-follow-local-advice"] = ["TP03", "TP08"]
    };

    private static readonly IReadOnlyDictionary<string, string[]> ExpectedDenmarkClaimIds = new Dictionary<string, string[]>(StringComparer.Ordinal)
    {
        ["section-one-check-framework"] = ["C01", "C02", "C03", "C19"],
        ["section-one-prepare-case-records"] = ["C05", "C07"],
        ["section-one-follow-sl1-route"] = ["C06", "C07"],
        ["section-one-follow-identity-instructions"] = ["C08"],
        ["family-check-section-two-route"] = ["C03", "C04", "C19"],
        ["family-follow-sl2-instructions"] = ["C04", "C06", "C07"],
        ["family-check-case-specific-evidence"] = ["C03", "C05", "C07", "C19"],
        ["family-follow-identity-instructions"] = ["C08"],
        ["pending-keep-submission-records"] = ["C08", "C09"],
        ["pending-clarify-work-conditions"] = ["C11"],
        ["pending-check-stay-travel-health"] = ["C10", "C12"],
        ["pending-respond-to-case-messages"] = ["C09", "C15", "C19"],
        ["permit-read-individual-decision"] = ["C13", "C19"],
        ["permit-separate-municipality-cpr-housing"] = ["C13", "C14", "C20"],
        ["permit-check-extension-communication"] = ["C05", "C16", "C17"],
        ["permit-check-family-and-further-questions"] = ["C04", "C19"]
    };

    private static readonly IReadOnlyDictionary<string, string[]> ExpectedDenmarkSources = new Dictionary<string, string[]>(StringComparer.Ordinal)
    {
        ["section-one-check-framework"] = ["486ee6fc-fc67-4d04-891b-acb967e5477b", "a17d9700-bcc4-4978-b9f8-0687c42ef038", "441b7bd9-54cf-47a8-80a1-e89a0e410952", "6677f201-1246-417c-8d8f-56dc99c93b0f"],
        ["section-one-prepare-case-records"] = ["486ee6fc-fc67-4d04-891b-acb967e5477b", "9d53a568-07c9-4713-a5e8-1c4db5826a98", "814a05cf-08c2-4f0d-b516-4ff3655f5b3c", "abf16498-621a-447c-9d72-9ec8efe9d99c"],
        ["section-one-follow-sl1-route"] = ["486ee6fc-fc67-4d04-891b-acb967e5477b", "9d53a568-07c9-4713-a5e8-1c4db5826a98"],
        ["section-one-follow-identity-instructions"] = ["486ee6fc-fc67-4d04-891b-acb967e5477b", "a17d9700-bcc4-4978-b9f8-0687c42ef038"],
        ["family-check-section-two-route"] = ["486ee6fc-fc67-4d04-891b-acb967e5477b", "a17d9700-bcc4-4978-b9f8-0687c42ef038", "441b7bd9-54cf-47a8-80a1-e89a0e410952"],
        ["family-follow-sl2-instructions"] = ["486ee6fc-fc67-4d04-891b-acb967e5477b", "a17d9700-bcc4-4978-b9f8-0687c42ef038"],
        ["family-check-case-specific-evidence"] = ["486ee6fc-fc67-4d04-891b-acb967e5477b", "a17d9700-bcc4-4978-b9f8-0687c42ef038", "814a05cf-08c2-4f0d-b516-4ff3655f5b3c", "abf16498-621a-447c-9d72-9ec8efe9d99c"],
        ["family-follow-identity-instructions"] = ["486ee6fc-fc67-4d04-891b-acb967e5477b", "a17d9700-bcc4-4978-b9f8-0687c42ef038"],
        ["pending-keep-submission-records"] = ["486ee6fc-fc67-4d04-891b-acb967e5477b", "a17d9700-bcc4-4978-b9f8-0687c42ef038", "141d8418-08af-4315-b5c2-ff34900b3369"],
        ["pending-clarify-work-conditions"] = ["141d8418-08af-4315-b5c2-ff34900b3369"],
        ["pending-check-stay-travel-health"] = ["486ee6fc-fc67-4d04-891b-acb967e5477b", "141d8418-08af-4315-b5c2-ff34900b3369"],
        ["pending-respond-to-case-messages"] = ["486ee6fc-fc67-4d04-891b-acb967e5477b", "141d8418-08af-4315-b5c2-ff34900b3369", "8a754763-dabb-4aee-ae13-415f5025e6ba", "f360d680-d215-4fdf-b419-d75a63ad68a8"],
        ["permit-read-individual-decision"] = ["486ee6fc-fc67-4d04-891b-acb967e5477b", "65822ddc-ad39-4a32-89ee-140dfca11496"],
        ["permit-separate-municipality-cpr-housing"] = ["486ee6fc-fc67-4d04-891b-acb967e5477b", "65822ddc-ad39-4a32-89ee-140dfca11496", "654c059c-be25-4b0a-bc47-cd995a2b91ea", "8ad828b3-b58d-4716-aa57-cd16798bb31c"],
        ["permit-check-extension-communication"] = ["486ee6fc-fc67-4d04-891b-acb967e5477b", "a17d9700-bcc4-4978-b9f8-0687c42ef038", "814a05cf-08c2-4f0d-b516-4ff3655f5b3c", "886dfd95-7713-4c69-9b3f-a2d3be8ad323", "9f908ed5-7fef-4075-a1e3-a1943669e3e6", "abf16498-621a-447c-9d72-9ec8efe9d99c"],
        ["permit-check-family-and-further-questions"] = ["486ee6fc-fc67-4d04-891b-acb967e5477b", "a17d9700-bcc4-4978-b9f8-0687c42ef038", "65822ddc-ad39-4a32-89ee-140dfca11496"]
    };

    private static readonly ResourceManager Resources = new(
        "RefugeeNavigator.Web.Resources.Localization.SharedResource",
        typeof(SharedResource).Assembly);

    private static readonly Lazy<Task<CatalogData>> Catalog = new(
        () => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());

    private static readonly Lazy<Task<StepByStepGuideCatalog>> GuideCatalog = new(async () =>
    {
        var catalog = await Catalog.Value;
        return await new JsonStepByStepGuideProvider(Path.Combine(AppContext.BaseDirectory, "Data", "step-by-step-guides.json")).LoadAsync(catalog);
    });

    [Fact]
    public async Task GuideHasFourOrderedScenariosAndSixteenStableDraftStepsWithCompleteTranslations()
    {
        var guideCatalog = await GuideCatalog.Value;
        Assert.Equal(2, guideCatalog.Guides.Count);
        var guide = guideCatalog.Guides.Single(item => item.CountryId == new Guid("10000000-0000-0000-0000-000000000002"));

        Assert.Equal("germany-temporary-protection-v1", guide.Id);
        Assert.Equal("temporary-protection", guide.Slug);
        Assert.Equal(new Guid("10000000-0000-0000-0000-000000000002"), guide.CountryId);
        Assert.Equal(GuideEditorialStatus.Approved, guide.EditorialStatus);
        Assert.Equal(4, guide.Scenarios.Count);
        Assert.Equal(16, guide.Scenarios.Sum(scenario => scenario.Steps.Count));
        Assert.Equal(["new-arrival", "application-pending", "existing-section-24-permit", "previous-protection-other-member-state"],
            guide.Scenarios.OrderBy(scenario => scenario.Order).Select(scenario => scenario.Id));
        Assert.Equal(Enumerable.Range(1, 4), guide.Scenarios.OrderBy(scenario => scenario.Order).Select(scenario => scenario.Order));

        var expectedStepIds = new[]
        {
            "arrival-check-local-process", "arrival-check-90-day-exemption", "arrival-follow-application-process", "arrival-check-online-route", "arrival-keep-pending-records",
            "pending-keep-records", "pending-check-document-effect", "pending-ask-about-work-entry",
            "permit-read-individual-date", "permit-check-german-continuation-rule", "permit-separate-eu-and-german-dates", "permit-request-written-clarification",
            "prior-protection-record-timeline", "prior-protection-clarify-eu-discrepancy", "prior-protection-separate-german-exemption", "prior-protection-follow-local-advice"
        };
        Assert.Equal(expectedStepIds, guide.Scenarios.OrderBy(scenario => scenario.Order).SelectMany(scenario => scenario.Steps.OrderBy(step => step.Order).Select(step => step.Id)));
        Assert.Equal(ExpectedClaimIds.Keys.Order(StringComparer.Ordinal), expectedStepIds.Order(StringComparer.Ordinal));

        var catalog = await Catalog.Value;
        foreach (var translation in guide.Translations.Values)
            Assert.False(string.IsNullOrWhiteSpace(translation.Title + translation.Description));
        foreach (var scenario in guide.Scenarios)
        {
            Assert.Equal(new[] { "en", "ru", "uk" }, scenario.Translations.Keys.Order(StringComparer.Ordinal));
            Assert.Equal(Enumerable.Range(1, scenario.Steps.Count), scenario.Steps.OrderBy(step => step.Order).Select(step => step.Order));
            foreach (var step in scenario.Steps)
            {
                var article = Assert.Single(catalog.InformationEntries, entry => entry.Id == step.ArticleId);
                Assert.Equal(guide.CountryId, article.CountryId);
                Assert.NotEmpty(step.ClaimIds);
                Assert.Equal(ExpectedClaimIds[step.Id], step.ClaimIds);
                Assert.All(step.OfficialSourceIds, sourceId => Assert.Contains(catalog.OfficialSources,
                    source => source.Id == sourceId && source.InformationEntryId == step.ArticleId));
                Assert.Equal(new[] { "en", "ru", "uk" }, step.Translations.Keys.Order(StringComparer.Ordinal));
                foreach (var language in new[] { "uk", "en", "ru" })
                {
                    var text = step.Translations[language];
                    Assert.False(string.IsNullOrWhiteSpace(text.Title));
                    Assert.False(string.IsNullOrWhiteSpace(text.Action));
                    Assert.False(string.IsNullOrWhiteSpace(text.Explanation));
                    Assert.False(string.IsNullOrWhiteSpace(text.Authority));
                    Assert.False(string.IsNullOrWhiteSpace(text.Warning));
                }
            }
        }

        Assert.All(guide.Scenarios.SelectMany(scenario => scenario.Steps), step => Assert.Equal(GuideActionKind.Recommendation, step.Kind));
    }

    [Fact]
    public async Task DenmarkGuideHasSixteenStableApprovedStepsWithExactClaimsSourcesAndCompleteTranslations()
    {
        var guides = await GuideCatalog.Value;
        var catalog = await Catalog.Value;
        var guide = Assert.Single(guides.Guides, item => item.CountryId == new Guid("10000000-0000-0000-0000-000000000001"));
        Assert.Equal("denmark-temporary-protection-v1", guide.Id);
        Assert.Equal("temporary-protection", guide.Slug);
        Assert.Equal(GuideEditorialStatus.Approved, guide.EditorialStatus);
        Assert.Equal(4, guide.Scenarios.Count);
        Assert.Equal(16, guide.Scenarios.Sum(item => item.Steps.Count));
        Assert.Equal(["first-section-one-application", "family-section-two-application", "application-pending", "existing-special-act-permit"],
            guide.Scenarios.OrderBy(item => item.Order).Select(item => item.Id));

        var ids = new[]
        {
            "section-one-check-framework", "section-one-prepare-case-records", "section-one-follow-sl1-route", "section-one-follow-identity-instructions",
            "family-check-section-two-route", "family-follow-sl2-instructions", "family-check-case-specific-evidence", "family-follow-identity-instructions",
            "pending-keep-submission-records", "pending-clarify-work-conditions", "pending-check-stay-travel-health", "pending-respond-to-case-messages",
            "permit-read-individual-decision", "permit-separate-municipality-cpr-housing", "permit-check-extension-communication", "permit-check-family-and-further-questions"
        };
        var steps = guide.Scenarios.OrderBy(item => item.Order).SelectMany(item => item.Steps.OrderBy(step => step.Order)).ToArray();
        Assert.Equal(ids, steps.Select(item => item.Id));
        Assert.All(steps, step =>
        {
            Assert.Equal(new Guid("afc14824-4035-4ca7-9662-6d5352ba63cb"), step.ArticleId);
            Assert.Equal(GuideActionKind.Recommendation, step.Kind);
            Assert.NotEmpty(step.ClaimIds);
            Assert.Equal(ExpectedDenmarkClaimIds[step.Id], step.ClaimIds);
            Assert.Equal(ExpectedDenmarkSources[step.Id], step.OfficialSourceIds.Select(id => id.ToString()));
            Assert.NotEmpty(step.OfficialSourceIds);
            Assert.All(step.OfficialSourceIds, sourceId => Assert.Contains(catalog.OfficialSources,
                source => source.Id == sourceId && source.InformationEntryId == step.ArticleId));
            Assert.Equal(new[] { "en", "ru", "uk" }, step.Translations.Keys.Order(StringComparer.Ordinal));
            foreach (var language in new[] { "uk", "en", "ru" })
            {
                var translation = step.Translations[language];
                Assert.All(new[] { translation.Title, translation.Action, translation.Explanation, translation.Authority, translation.Warning },
                    value => Assert.False(string.IsNullOrWhiteSpace(value)));
                Assert.All(translation.Documents, value => Assert.False(string.IsNullOrWhiteSpace(value)));
            }
        });
        Assert.True(StepByStepGuideVisibility.CanRender(guide.EditorialStatus, isDevelopment: true));
        Assert.True(StepByStepGuideVisibility.CanRender(guide.EditorialStatus, isDevelopment: false));
        Assert.Equal(GuideEditorialStatus.Approved,
            guides.Guides.Single(item => item.CountryId == new Guid("10000000-0000-0000-0000-000000000002")).EditorialStatus);
        Assert.True(StepByStepGuideVisibility.CanRender(GuideEditorialStatus.Approved, isDevelopment: false));

        foreach (var language in new[] { "uk", "en", "ru" })
        {
            var productionHtml = System.Net.WebUtility.HtmlDecode(await RenderAsync(language, Environments.Production, guideOverride: guide));
            var developmentHtml = System.Net.WebUtility.HtmlDecode(await RenderAsync(language, Environments.Development, guideOverride: guide));
            var draftNotice = Resources.GetString("Guide.DraftPreviewNotice", CultureInfo.GetCultureInfo(language))!;
            Assert.Contains(guide.Translations[language].Title, productionHtml);
            Assert.Contains(guide.Translations[language].Title, developmentHtml);
            foreach (var scenario in guide.Scenarios)
            {
                Assert.Contains(scenario.Translations[language].Title, productionHtml);
                Assert.Contains(scenario.Translations[language].Title, developmentHtml);
            }
            Assert.DoesNotContain(draftNotice, productionHtml);
            Assert.DoesNotContain(draftNotice, developmentHtml);
            Assert.DoesNotContain("Guide.", productionHtml);
            Assert.DoesNotContain("Guide.", developmentHtml);
        }
    }

    [Fact]
    public async Task DenmarkGuideRouteResolvesByCountryAndGuideSlugsIndependentOfCulture()
    {
        var guides = await GuideCatalog.Value;
        var catalog = await Catalog.Value;
        var previous = CultureInfo.CurrentUICulture;
        try
        {
            foreach (var culture in new[] { "uk", "en", "ru" })
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
                Assert.Equal("denmark-temporary-protection-v1", StepByStepGuideResolver.Resolve(guides, catalog, "denmark", "temporary-protection")?.Id);
                Assert.Equal("germany-temporary-protection-v1", StepByStepGuideResolver.Resolve(guides, catalog, "GERMANY", "TEMPORARY-PROTECTION")?.Id);
            }
            Assert.Null(StepByStepGuideResolver.Resolve(guides, catalog, "unknown", "temporary-protection"));
            Assert.Null(StepByStepGuideResolver.Resolve(guides, catalog, "poland", "temporary-protection"));
            Assert.Null(StepByStepGuideResolver.Resolve(guides, catalog, "denmark", "unknown"));
        }
        finally { CultureInfo.CurrentUICulture = previous; }
    }

    [Theory]
    [InlineData("uk", "виїзду з України")]
    [InlineData("en", "left Ukraine")]
    [InlineData("ru", "выезда из Украины")]
    public async Task DenmarkA2RecordPreparationDoesNotAskForDepartureDate(string language, string departureDateWording)
    {
        var guide = (await GuideCatalog.Value).Guides.Single(item => item.CountryId == new Guid("10000000-0000-0000-0000-000000000001"));
        var step = guide.Scenarios.SelectMany(scenario => scenario.Steps).Single(item => item.Id == "section-one-prepare-case-records");
        var translation = step.Translations[language];
        var nonMilitaryContent = string.Join(" ", translation.Title, translation.Action, translation.Explanation,
            translation.Documents, translation.Authority);

        Assert.Equal(["C05", "C07"], step.ClaimIds);
        Assert.DoesNotContain(departureDateWording, nonMilitaryContent, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(language switch
        {
            "uk" => "25 червня 2026 року",
            "en" => "25 June 2026",
            _ => "25 июня 2026 года"
        }, translation.Warning);
        Assert.Contains(language switch
        {
            "uk" => "3 вересня 2026 року",
            "en" => "3 September 2026",
            _ => "3 сентября 2026 года"
        }, translation.Warning);
    }

    [Theory]
    [InlineData("uk", "17 березня 2027", "17 березня 2028", "25 червня 2026 року", "3 вересня 2026 року")]
    [InlineData("en", "17 March 2027", "17 March 2028", "25 June 2026", "3 September 2026")]
    [InlineData("ru", "17 марта 2027", "17 марта 2028", "25 июня 2026 года", "3 сентября 2026 года")]
    public async Task DenmarkExtensionConflictAndMilitaryScopeRemainLocalized(string language, string date2027, string date2028, string applicationDate, string changeDate)
    {
        var guide = (await GuideCatalog.Value).Guides.Single(item => item.CountryId == new Guid("10000000-0000-0000-0000-000000000001"));
        var steps = guide.Scenarios.SelectMany(scenario => scenario.Steps).ToDictionary(step => step.Id);
        var extensionText = string.Join(" ", steps["permit-check-extension-communication"].Translations[language].Explanation,
            steps["permit-check-extension-communication"].Translations[language].Warning);
        var militaryText = string.Join(" ", steps["section-one-prepare-case-records"].Translations[language].Warning,
            steps["family-check-case-specific-evidence"].Translations[language].Warning);
        var pendingWorkText = string.Join(" ", steps["pending-clarify-work-conditions"].Translations[language].Explanation,
            steps["pending-clarify-work-conditions"].Translations[language].Warning);
        var processingText = string.Join(" ", steps["pending-keep-submission-records"].Translations[language].Explanation,
            steps["pending-keep-submission-records"].Translations[language].Warning);
        Assert.Contains(date2027, extensionText);
        Assert.Contains(date2028, extensionText);
        Assert.Contains("17", extensionText);
        Assert.Contains(applicationDate, militaryText);
        Assert.Contains(changeDate, militaryText);
        Assert.Contains("23–59", militaryText);
        Assert.Contains(language switch { "uk" => "Жодна загальна дата", "en" => "Neither general date", _ => "Ни одна общая дата" }, extensionText, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(language switch { "uk" => "біометричні дані мають бути записані", "en" => "biometrics must be recorded", _ => "биометрические данные должны быть зарегистрированы" }, pendingWorkText, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("35", processingText);
        Assert.Contains(language switch { "uk" => "не гарантія", "en" => "not a guarantee", _ => "не гарантия" }, processingText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("L 43", extensionText);
    }

    [Fact]
    public void GuidePageUsesCountryAndGuideSlugRoute()
    {
        var route = typeof(RefugeeNavigator.Web.Components.Pages.StepByStepGuide)
            .GetCustomAttributes(typeof(RouteAttribute), inherit: false).Cast<RouteAttribute>().Single();
        Assert.Equal("/guides/{CountrySlug}/{GuideSlug}", route.Template);
    }

    [Fact]
    public async Task IncorrectArticleOrSourceRelationshipFailsGuideValidation()
    {
        var catalog = await Catalog.Value;
        var guides = await GuideCatalog.Value;
        var guide = guides.Guides[0];
        var scenario = guide.Scenarios[0];
        var step = scenario.Steps[0];

        var wrongArticle = ReplaceStep(guides, guide, scenario, step with { ArticleId = Guid.NewGuid() });
        Assert.Throws<InvalidDataException>(() => JsonStepByStepGuideProvider.Validate(wrongArticle, catalog));

        var wrongSource = ReplaceStep(guides, guide, scenario, step with { OfficialSourceIds = [Guid.NewGuid()] });
        Assert.Throws<InvalidDataException>(() => JsonStepByStepGuideProvider.Validate(wrongSource, catalog));

        var approved = guides with { Guides = [guide with { EditorialStatus = GuideEditorialStatus.Approved }] };
        JsonStepByStepGuideProvider.Validate(approved, catalog);

        var unknownStatus = guides with { Guides = [guide with { EditorialStatus = (GuideEditorialStatus)int.MaxValue }] };
        Assert.Throws<InvalidDataException>(() => JsonStepByStepGuideProvider.Validate(unknownStatus, catalog));
    }

    [Fact]
    public async Task NewArrivalRecordPreparationStepMatchesApprovedProposalInAllLanguages()
    {
        var step = (await GuideCatalog.Value).Guides.SelectMany(guide => guide.Scenarios).SelectMany(scenario => scenario.Steps).Single(step => step.Id == "arrival-follow-application-process");
        Assert.Equal(["TP02", "TP03"], step.ClaimIds);
        Assert.Equal(
            ["e4bb8ae0-6e0a-573b-8ab9-c6ea8fffb30a", "1d973c40-5d66-5b6a-ae13-78f667576a0a", "a71ea73c-f2db-5545-81c8-1a4c5016c361"],
            step.OfficialSourceIds.Select(id => id.ToString()));

        var expectedTitles = new Dictionary<string, string>
        {
            ["uk"] = "Підготуйте документи, які можуть знадобитися",
            ["en"] = "Gather potentially relevant records",
            ["ru"] = "Подготовьте документы, которые могут понадобиться"
        };
        var expectedTerms = new Dictionary<string, string[]>
        {
            ["uk"] = ["першого в’їзду", "документи про родинні зв’язки", "інші докази", "універсального списку", "не обов’язковий"],
            ["en"] = ["first date of entry", "Family-link records", "may request other evidence", "universal checklist", "not a mandatory"],
            ["ru"] = ["первого въезда", "родственных связях", "может запросить другие доказательства", "универсальный список", "не обязательный"]
        };

        foreach (var language in new[] { "uk", "en", "ru" })
        {
            var translation = step.Translations[language];
            Assert.Equal(expectedTitles[language], translation.Title);
            var allText = string.Join(" ", new[] { translation.Action, translation.Explanation, translation.Authority, translation.Warning }.Concat(translation.Documents));
            foreach (var term in expectedTerms[language])
                Assert.Contains(term, allText, StringComparison.OrdinalIgnoreCase);
        }
    }

    [Fact]
    public async Task PendingCaseNextStepMatchesApprovedProposalWithoutGivingWorkAdvice()
    {
        var step = (await GuideCatalog.Value).Guides.SelectMany(guide => guide.Scenarios).SelectMany(scenario => scenario.Steps).Single(step => step.Id == "pending-ask-about-work-entry");
        Assert.Equal(["TP04", "TP05"], step.ClaimIds);
        Assert.Equal(
            ["1d973c40-5d66-5b6a-ae13-78f667576a0a", "fd4673e5-b187-5e24-9d17-c4dae465491a"],
            step.OfficialSourceIds.Select(id => id.ToString()));
        var expectedTitles = new Dictionary<string, string>
        {
            ["uk"] = "Попросіть уточнити наступний крок у вашій справі",
            ["en"] = "Ask the competent authority to clarify your next step",
            ["ru"] = "Попросите разъяснить следующий шаг по вашему делу"
        };
        var expectedTerms = new Dictionary<string, string[]>
        {
            ["uk"] = ["компетентного Ausländerbehörde", "номер справи", "документ", "дат", "право на роботу", "результат розгляду"],
            ["en"] = ["competent Ausländerbehörde", "case reference", "document", "date", "work authorization", "outcome of the pending application"],
            ["ru"] = ["компетентное Ausländerbehörde", "номер дела", "документ", "дат", "право на работу", "исход рассмотрения заявления"]
        };

        foreach (var language in new[] { "uk", "en", "ru" })
        {
            var translation = step.Translations[language];
            Assert.Equal(expectedTitles[language], translation.Title);
            var allText = string.Join(" ", new[] { translation.Action, translation.Explanation, translation.Authority, translation.Warning }.Concat(translation.Documents));
            foreach (var term in expectedTerms[language])
                Assert.Contains(term, allText, StringComparison.OrdinalIgnoreCase);
        }

        Assert.DoesNotContain("work entry", string.Join(" ", step.Translations["en"].Title, step.Translations["en"].Action, step.Translations["en"].Explanation), StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("uk", "Тимчасовий захист у Німеччині", "Я щойно приїхав/приїхала й шукаю тимчасовий захист")]
    [InlineData("en", "Temporary protection in Germany", "I have just arrived and am seeking temporary protection")]
    [InlineData("ru", "Временная защита в Германии", "Я недавно приехал/приехала и ищу временную защиту")]
    public async Task DevelopmentPreviewShowsLocalizedDraftNoticeAndAllFourScenarioChoices(string language, string title, string firstScenario)
    {
        var approved = (await GuideCatalog.Value).Guides[0];
        var draft = approved with { EditorialStatus = GuideEditorialStatus.Draft };
        var html = await RenderAsync(language, Environments.Development, guideOverride: draft);
        var visibleHtml = System.Net.WebUtility.HtmlDecode(html);

        Assert.Contains(title, visibleHtml);
        Assert.Contains(firstScenario, visibleHtml);
        Assert.Contains(Resources.GetString("Guide.DraftPreviewNotice", CultureInfo.GetCultureInfo(language))!, visibleHtml);
        Assert.Equal(1, CountOccurrences(visibleHtml, Resources.GetString("Guide.SelectSituation", CultureInfo.GetCultureInfo(language))!));
        Assert.DoesNotContain("class=\"eyebrow\"", html);
        Assert.Contains("role=\"note\"", html);
        Assert.Equal(4, System.Text.RegularExpressions.Regex.Matches(html, "class=\"scenario-choice\"").Count);
        Assert.DoesNotContain("Guide.", visibleHtml);
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task SelectedStepShowsLocalizedContentArticleAndOfficialSources(string language)
    {
        var catalog = await Catalog.Value;
        var guide = (await GuideCatalog.Value).Guides[0];
        var scenario = guide.Scenarios.OrderBy(item => item.Order).First();
        var step = scenario.Steps.OrderBy(item => item.Order).First();
        var html = System.Net.WebUtility.HtmlDecode(await RenderAsync(language, Environments.Development, scenario, 0));
        var text = step.Translations[language];
        var source = Assert.Single(catalog.OfficialSources, item => item.Id == step.OfficialSourceIds[0]);

        Assert.Contains(text.Title, html);
        Assert.Contains(text.Action, html);
        Assert.Contains(text.Explanation, html);
        Assert.Contains(text.Authority, html);
        Assert.Contains(text.Warning, html);
        Assert.Contains(source.Url.AbsoluteUri, html);
        Assert.Contains($"/countries/germany/categories/temporary-protection#entry-{step.ArticleId:N}", html);
        Assert.Contains(Resources.GetString("Guide.ChangeSituation", CultureInfo.GetCultureInfo(language))!, html);
        Assert.Contains(Resources.GetString("Guide.Next", CultureInfo.GetCultureInfo(language))!, html);
        Assert.Contains("<progress", html);
        var culture = CultureInfo.GetCultureInfo(language);
        var progressLabel = string.Format(culture, Resources.GetString("Guide.Progress", culture)!, 1, scenario.Steps.Count);
        Assert.Contains(progressLabel, html);
        Assert.Contains(Resources.GetString("Guide.ProgressNotice", culture)!, html);
        Assert.Contains("class=\"progress-notice\"", html);
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task DenmarkStepLinksToItsCatalogCountryAndCategoryArticle(string language)
    {
        var guide = (await GuideCatalog.Value).Guides.Single(item => item.CountryId == new Guid("10000000-0000-0000-0000-000000000001"));
        var scenario = guide.Scenarios.OrderBy(item => item.Order).First();
        var step = scenario.Steps.OrderBy(item => item.Order).First();
        var html = await RenderAsync(language, Environments.Development, scenario, 0, guide);
        Assert.Contains($"/countries/denmark/categories/temporary-protection#entry-{step.ArticleId:N}", html);
        Assert.DoesNotContain("/countries/germany/categories", html);
        Assert.DoesNotContain(Resources.GetString("Guide.DraftPreviewNotice", CultureInfo.GetCultureInfo(language))!, System.Net.WebUtility.HtmlDecode(html));
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task ApprovedGuideRendersInProductionAndDevelopmentWithoutDraftNotice(string language)
    {
        var productionHtml = System.Net.WebUtility.HtmlDecode(await RenderAsync(language, Environments.Production));
        var developmentHtml = System.Net.WebUtility.HtmlDecode(await RenderAsync(language, Environments.Development));
        var title = (await GuideCatalog.Value).Guides[0].Translations[language].Title;

        Assert.Contains(title, productionHtml);
        Assert.Contains(title, developmentHtml);
        Assert.DoesNotContain(Resources.GetString("Guide.DraftPreviewNotice", CultureInfo.GetCultureInfo(language))!, productionHtml);
        Assert.DoesNotContain(Resources.GetString("Guide.DraftPreviewNotice", CultureInfo.GetCultureInfo(language))!, developmentHtml);
        Assert.DoesNotContain(Resources.GetString("Guide.Unavailable", CultureInfo.GetCultureInfo(language))!, productionHtml);
        Assert.False(StepByStepGuideVisibility.CanRender(GuideEditorialStatus.Draft, isDevelopment: false));
        Assert.True(StepByStepGuideVisibility.CanRender(GuideEditorialStatus.Draft, isDevelopment: true));
        Assert.True(StepByStepGuideVisibility.CanRender(GuideEditorialStatus.Approved, isDevelopment: false));
    }

    [Fact]
    public async Task SyntheticDraftGuideRemainsDevelopmentOnlyAndKeepsPreviewNotice()
    {
        var approved = (await GuideCatalog.Value).Guides[0];
        var draft = approved with { EditorialStatus = GuideEditorialStatus.Draft };
        var developmentHtml = System.Net.WebUtility.HtmlDecode(await RenderAsync("en", Environments.Development, guideOverride: draft));
        var productionHtml = System.Net.WebUtility.HtmlDecode(await RenderAsync("en", Environments.Production, guideOverride: draft));

        Assert.Contains(Resources.GetString("Guide.DraftPreviewNotice", CultureInfo.GetCultureInfo("en"))!, developmentHtml);
        Assert.Contains(draft.Translations["en"].Title, developmentHtml);
        Assert.DoesNotContain(draft.Translations["en"].Title, productionHtml);
        Assert.Contains(Resources.GetString("Guide.Unavailable", CultureInfo.GetCultureInfo("en"))!, productionHtml);
        Assert.False(StepByStepGuideVisibility.CanRender(draft.EditorialStatus, isDevelopment: false));
        Assert.True(StepByStepGuideVisibility.CanRender(draft.EditorialStatus, isDevelopment: true));
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task GuideInterfaceLocalizationIsComplete(string language)
    {
        var catalog = await GuideCatalog.Value;
        StepByStepGuideLocalizationValidator.Validate(catalog, Resources.GetString);
        foreach (var key in new[]
        {
            "Guide.DraftPreviewNotice", "Guide.SelectSituation", "Guide.Back", "Guide.Next", "Guide.Previous",
            "Guide.ChangeSituation", "Guide.ReadArticle", "Guide.Progress", "Guide.ProgressNotice", "Guide.Documents", "Guide.Authority",
            "Guide.Warning", "Guide.Required", "Guide.Recommendation", "Guide.Optional", "Guide.Unavailable", "Guide.Step"
        })
        {
            Assert.False(string.IsNullOrWhiteSpace(Resources.GetString(key, CultureInfo.GetCultureInfo(language))), $"Missing {language}: {key}");
        }
    }

    private static StepByStepGuideCatalog ReplaceStep(StepByStepGuideCatalog catalog, GuideModel guide, GuideScenario scenario, GuideStep replacement) => catalog with
    {
        Guides = catalog.Guides.Select(item => item != guide ? item : item with
        {
            Scenarios = item.Scenarios.Select(candidate => candidate != scenario ? candidate : candidate with
            {
                Steps = candidate.Steps.Select(step => step.Id == replacement.Id ? replacement : step).ToArray()
            }).ToArray()
        }).ToArray()
    };

    private static int CountOccurrences(string value, string search) =>
        System.Text.RegularExpressions.Regex.Matches(value, System.Text.RegularExpressions.Regex.Escape(search)).Count;

    private static async Task<string> RenderAsync(string language, string environmentName, GuideScenario? selectedScenario = null, int stepIndex = 0, GuideModel? guideOverride = null)
    {
        var originalCulture = CultureInfo.CurrentCulture;
        var originalUiCulture = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(language);
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(language);
        try
        {
            var catalog = await Catalog.Value;
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton(catalog);
            services.AddSingleton(await GuideCatalog.Value);
            using var provider = services.BuildServiceProvider();
            await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());
            var guide = guideOverride ?? (await GuideCatalog.Value).Guides[0];
            var isAvailable = StepByStepGuideVisibility.CanRender(guide.EditorialStatus, environmentName == Environments.Development);
            var parameters = new Dictionary<string, object?>
            {
                [nameof(GuideView.Guide)] = isAvailable ? guide : null,
                [nameof(GuideView.Catalog)] = catalog,
                [nameof(GuideView.IsAvailable)] = isAvailable,
                [nameof(GuideView.SelectedScenario)] = selectedScenario,
                [nameof(GuideView.StepIndex)] = stepIndex
            };
            return await renderer.Dispatcher.InvokeAsync(async () =>
            {
                var output = await renderer.RenderComponentAsync<GuideView>(ParameterView.FromDictionary(parameters));
                return output.ToHtmlString();
            });
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
            CultureInfo.CurrentUICulture = originalUiCulture;
        }
    }

}
