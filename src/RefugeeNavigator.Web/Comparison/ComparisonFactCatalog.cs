namespace RefugeeNavigator.Web.Comparison;

public enum ComparisonFactStatus
{
    Draft,
    Approved
}

public sealed record ComparisonFactCatalog(IReadOnlyList<ComparisonTopicFacts> Topics);

public sealed record ComparisonTopicFacts(
    string TopicResourceKey,
    string CategorySlug,
    IReadOnlyList<ComparisonAspect> Aspects);

public sealed record ComparisonAspect(
    string Id,
    string QuestionResourceKey,
    IReadOnlyList<CountryComparisonFact> Facts);

public sealed record CountryComparisonFact(
    string CountrySlug,
    Guid InformationEntryId,
    ComparisonFactStatus Status,
    bool IsAvailable,
    IReadOnlyList<Guid> OfficialSourceIds,
    IReadOnlyDictionary<string, string> Values);
