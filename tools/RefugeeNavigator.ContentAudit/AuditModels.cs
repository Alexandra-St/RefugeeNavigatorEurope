using RefugeeNavigator.Domain;

namespace RefugeeNavigator.ContentAudit;

public sealed record AuditThresholds(int RecentlyCheckedThroughDays = 30, int ReviewSuggestedThroughDays = 90)
{
    public void Validate()
    {
        if (RecentlyCheckedThroughDays < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(RecentlyCheckedThroughDays));
        }

        if (ReviewSuggestedThroughDays < RecentlyCheckedThroughDays)
        {
            throw new ArgumentOutOfRangeException(nameof(ReviewSuggestedThroughDays));
        }
    }
}

public enum SourceReviewPriority
{
    RecentlyChecked,
    ReviewSuggested,
    ReviewPriority
}

public sealed record ComparisonFactReference(
    string TopicResourceKey,
    string CategorySlug,
    string AspectId,
    string CountrySlug,
    Guid InformationEntryId,
    IReadOnlyList<Guid> OfficialSourceIds);

public sealed record ContentAuditData(
    IReadOnlyList<Country> Countries,
    IReadOnlyList<InformationCategory> Categories,
    IReadOnlyList<InformationEntry> Articles,
    IReadOnlyList<InformationEntryTranslation> Translations,
    IReadOnlyList<OfficialSource> Sources,
    IReadOnlyList<ComparisonFactReference> ComparisonFacts);

public sealed record ArticleDependency(
    Guid InformationEntryId,
    string Country,
    string Category,
    string Title);

public sealed record ComparisonDependency(
    string TopicResourceKey,
    string AspectId,
    string Country);

public sealed record AuditedSource(
    Guid Id,
    string Title,
    Uri Url,
    DateTimeOffset LastCheckedAt,
    int DaysSinceLastCheck,
    SourceReviewPriority Priority,
    IReadOnlyList<ArticleDependency> Articles,
    IReadOnlyList<ComparisonDependency> ComparisonFacts);

public sealed record DuplicateSourceUrl(Uri Url, IReadOnlyList<Guid> SourceIds);

public sealed record ContentAuditReport(
    DateOnly AsOf,
    AuditThresholds Thresholds,
    int ArticleCount,
    int TranslationCount,
    int SourceCount,
    int ComparisonFactCount,
    int RecentlyCheckedCount,
    int ReviewSuggestedCount,
    int ReviewPriorityCount,
    IReadOnlyList<AuditedSource> Sources,
    IReadOnlyList<DuplicateSourceUrl> DuplicateUrls,
    IReadOnlyList<string> RelationshipIssues)
{
    public IReadOnlyList<AuditedSource> ReviewCandidates =>
        Sources.Where(source => source.Priority != SourceReviewPriority.RecentlyChecked).ToArray();

    public IReadOnlyList<ArticleDependency> AffectedArticles => ReviewCandidates
        .SelectMany(source => source.Articles)
        .DistinctBy(article => article.InformationEntryId)
        .ToArray();

    public IReadOnlyList<ComparisonDependency> AffectedComparisonFacts => ReviewCandidates
        .SelectMany(source => source.ComparisonFacts)
        .DistinctBy(fact => (fact.TopicResourceKey, fact.AspectId, fact.Country))
        .ToArray();
}
