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
        var guide = Assert.Single(guideCatalog.Guides);

        Assert.Equal("germany-temporary-protection-v1", guide.Id);
        Assert.Equal("temporary-protection", guide.Slug);
        Assert.Equal(new Guid("10000000-0000-0000-0000-000000000002"), guide.CountryId);
        Assert.Equal(GuideEditorialStatus.Draft, guide.EditorialStatus);
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
        var html = await RenderAsync(language, Environments.Development);
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
    public async Task ProductionDoesNotRenderDraftGuideContent(string language)
    {
        var html = System.Net.WebUtility.HtmlDecode(await RenderAsync(language, Environments.Production));
        Assert.Contains(Resources.GetString("Guide.Unavailable", CultureInfo.GetCultureInfo(language))!, html);
        Assert.DoesNotContain("Temporary protection in Germany", html);
        Assert.DoesNotContain("Applying for Temporary Protection", html);
        Assert.DoesNotContain("Guide.DraftPreviewNotice", html);
        Assert.False(StepByStepGuideVisibility.CanRender(GuideEditorialStatus.Draft, isDevelopment: false));
        Assert.True(StepByStepGuideVisibility.CanRender(GuideEditorialStatus.Draft, isDevelopment: true));
        Assert.True(StepByStepGuideVisibility.CanRender(GuideEditorialStatus.Approved, isDevelopment: false));
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

    private static async Task<string> RenderAsync(string language, string environmentName, GuideScenario? selectedScenario = null, int stepIndex = 0)
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
            var guide = (await GuideCatalog.Value).Guides[0];
            var isAvailable = environmentName == Environments.Development;
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
