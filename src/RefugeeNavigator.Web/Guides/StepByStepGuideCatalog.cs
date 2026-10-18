namespace RefugeeNavigator.Web.Guides;

public enum GuideEditorialStatus
{
    Draft,
    Approved
}

public enum GuideActionKind
{
    Required,
    Recommendation,
    Optional
}

public sealed record StepByStepGuideCatalog(IReadOnlyList<StepByStepGuide> Guides);

public sealed record StepByStepGuide(
    string Id,
    string Slug,
    Guid CountryId,
    GuideEditorialStatus EditorialStatus,
    IReadOnlyDictionary<string, LocalizedGuideText> Translations,
    IReadOnlyList<GuideScenario> Scenarios);

public sealed record LocalizedGuideText(string Title, string Description);

public sealed record GuideScenario(
    string Id,
    int Order,
    IReadOnlyDictionary<string, LocalizedScenarioText> Translations,
    IReadOnlyList<GuideStep> Steps);

public sealed record LocalizedScenarioText(string Title, string Description);

public sealed record GuideStep(
    string Id,
    int Order,
    GuideActionKind Kind,
    Guid ArticleId,
    IReadOnlyList<Guid> OfficialSourceIds,
    IReadOnlyList<string> ClaimIds,
    IReadOnlyDictionary<string, LocalizedGuideStepText> Translations);

public sealed record LocalizedGuideStepText(
    string Title,
    string Action,
    string Explanation,
    IReadOnlyList<string> Documents,
    string Authority,
    string Warning);
