using System.Text.Json;
using RefugeeNavigator.ContentAudit;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Tests;

public sealed class ContentMaintenanceAuditTests
{
    private static readonly DateOnly AsOf = new(2026, 10, 17);

    [Fact]
    public void SourceAgeUsesFixedUtcCalendarDatesAndDefaultThresholds()
    {
        var data = CreateData(sourceDates: [AsOf.AddDays(-30), AsOf.AddDays(-31), AsOf.AddDays(-90), AsOf.AddDays(-91)]);

        var report = new ContentMaintenanceAuditor().CreateReport(data, AsOf);

        Assert.Equal(1, report.RecentlyCheckedCount);
        Assert.Equal(2, report.ReviewSuggestedCount);
        Assert.Equal(1, report.ReviewPriorityCount);
        Assert.Equal(new[] { 30, 31, 90, 91 }, report.Sources.OrderBy(source => source.DaysSinceLastCheck).Select(source => source.DaysSinceLastCheck));
        Assert.Equal(SourceReviewPriority.ReviewPriority, report.Sources.Single(source => source.DaysSinceLastCheck == 91).Priority);
    }

    [Fact]
    public void CustomThresholdsAndExplicitAsOfDateAreAppliedDeterministically()
    {
        var command = ContentAuditCommandLine.Parse(
            ["--as-of", "2026-10-17", "--recent-days", "2", "--review-days", "4"],
            new DateOnly(2026, 12, 31));
        var data = CreateData(sourceDates: [AsOf.AddDays(-2), AsOf.AddDays(-3), AsOf.AddDays(-4), AsOf.AddDays(-5)]);

        var report = new ContentMaintenanceAuditor().CreateReport(data, command.AsOf, command.Thresholds);

        Assert.Equal(AsOf, report.AsOf);
        Assert.Equal(new AuditThresholds(2, 4), report.Thresholds);
        Assert.Equal(1, report.RecentlyCheckedCount);
        Assert.Equal(2, report.ReviewSuggestedCount);
        Assert.Equal(1, report.ReviewPriorityCount);
    }

    [Fact]
    public void CommandLineDefaultsToInjectedUtcDateAndRejectsInvalidThresholds()
    {
        var today = new DateOnly(2026, 10, 17);
        var command = ContentAuditCommandLine.Parse([], today);

        Assert.Equal(today, command.AsOf);
        Assert.Equal(new AuditThresholds(30, 90), command.Thresholds);
        Assert.Throws<ArgumentException>(() => ContentAuditCommandLine.Parse(["--as-of", "17-10-2026"], today));
        Assert.Throws<ArgumentOutOfRangeException>(() => ContentAuditCommandLine.Parse(["--recent-days", "91", "--review-days", "90"], today));
    }

    [Fact]
    public void SourceDependenciesUseIdsForArticlesCategoriesCountriesAndComparisonFacts()
    {
        var data = CreateData(sourceDates: [AsOf.AddDays(-45)]);

        var report = new ContentMaintenanceAuditor().CreateReport(data, AsOf);

        var source = Assert.Single(report.Sources);
        var article = Assert.Single(source.Articles);
        Assert.Equal("Germany", article.Country);
        Assert.Equal("Employment", article.Category);
        Assert.Equal("Work in Germany", article.Title);
        var comparison = Assert.Single(source.ComparisonFacts);
        Assert.Equal("Compare.Topic.Employment", comparison.TopicResourceKey);
        Assert.Equal("work-rights", comparison.AspectId);
        Assert.Equal("Germany", comparison.Country);
        Assert.Single(report.AffectedArticles);
        Assert.Single(report.AffectedComparisonFacts);
        Assert.Empty(report.RelationshipIssues);
    }

    [Fact]
    public void ComparisonFactWithExistingButDifferentArticleCategoryIsReported()
    {
        var country = new Country(Guid.NewGuid(), "Germany", "germany");
        var employment = new InformationCategory(Guid.NewGuid(), "Employment", "employment");
        var housing = new InformationCategory(Guid.NewGuid(), "Housing", "housing");
        var housingArticle = new InformationEntry(Guid.NewGuid(), country.Id, housing.Id);
        var fact = new ComparisonFactReference(
            "Compare.Topic.Employment",
            employment.Slug,
            "work-rights",
            country.Slug,
            housingArticle.Id,
            []);
        var data = new ContentAuditData([country], [employment, housing], [housingArticle], [], [], [fact]);

        var report = new ContentMaintenanceAuditor().CreateReport(data, AsOf);

        Assert.Contains(report.RelationshipIssues, issue => issue.Contains("category/article mismatch", StringComparison.Ordinal));
        Assert.DoesNotContain(report.RelationshipIssues, issue => issue.Contains("missing category slug", StringComparison.Ordinal));
        Assert.DoesNotContain(report.RelationshipIssues, issue => issue.Contains("missing article ID", StringComparison.Ordinal));
    }

    [Fact]
    public void DuplicateUrlsAreGroupedWithoutMergingSourceIdsOrDates()
    {
        var data = CreateData(sourceDates: [AsOf.AddDays(-10), AsOf.AddDays(-100)], duplicateUrl: true);

        var report = new ContentMaintenanceAuditor().CreateReport(data, AsOf);

        var duplicate = Assert.Single(report.DuplicateUrls);
        Assert.Equal(2, duplicate.SourceIds.Count);
        Assert.Equal(2, report.Sources.Count);
        Assert.Equal(new[] { 10, 100 }, report.Sources.Select(source => source.DaysSinceLastCheck).Order().ToArray());
        Assert.Equal(2, report.Sources.Select(source => source.LastCheckedAt).Distinct().Count());
    }

    [Fact]
    public void MissingAndMismatchedIdsAreReportedInsteadOfInferred()
    {
        var unknownId = Guid.NewGuid();
        var countries = new[] { new Country(Guid.NewGuid(), "Germany", "germany") };
        var categories = new[] { new InformationCategory(Guid.NewGuid(), "Employment", "employment") };
        var article = new InformationEntry(Guid.NewGuid(), unknownId, unknownId);
        var translation = new InformationEntryTranslation(Guid.NewGuid(), "en", "Orphan", "Summary", "Text", VerificationStatus.NeedsReview, AsOf.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc));
        var source = new OfficialSource(Guid.NewGuid(), Guid.NewGuid(), "Orphan source", new Uri("https://example.test/source"), AsOf.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc));
        var fact = new ComparisonFactReference("Compare.Topic.Unknown", "unknown-category", "unknown-aspect", "unknown-country", Guid.NewGuid(), [Guid.NewGuid()]);
        var mismatchedFact = new ComparisonFactReference("Compare.Topic.Unknown", "employment", "mismatched-source", "germany", article.Id, [source.Id]);
        var data = new ContentAuditData(countries, categories, [article], [translation], [source], [fact, mismatchedFact]);

        var report = new ContentMaintenanceAuditor().CreateReport(data, AsOf);

        Assert.Contains(report.RelationshipIssues, issue => issue.Contains("missing country ID", StringComparison.Ordinal));
        Assert.Contains(report.RelationshipIssues, issue => issue.Contains("missing category ID", StringComparison.Ordinal));
        Assert.Contains(report.RelationshipIssues, issue => issue.Contains("missing article ID", StringComparison.Ordinal));
        Assert.Contains(report.RelationshipIssues, issue => issue.Contains("missing official source ID", StringComparison.Ordinal));
        Assert.Contains(report.RelationshipIssues, issue => issue.Contains("linked to a different article", StringComparison.Ordinal));
        Assert.Contains(report.RelationshipIssues, issue => issue.Contains("missing country slug", StringComparison.Ordinal));
        Assert.Contains(report.RelationshipIssues, issue => issue.Contains("missing category slug", StringComparison.Ordinal));
        Assert.Contains(report.RelationshipIssues, issue => issue.Contains("references missing article ID", StringComparison.Ordinal));
    }

    [Fact]
    public void EmptyEntityIdsAreReported()
    {
        var data = new ContentAuditData(
            [new Country(Guid.Empty, "Germany", "germany")],
            [new InformationCategory(Guid.Empty, "Employment", "employment")],
            [new InformationEntry(Guid.Empty, Guid.Empty, Guid.Empty)],
            [],
            [],
            []);

        var report = new ContentMaintenanceAuditor().CreateReport(data, AsOf);

        Assert.Contains(report.RelationshipIssues, issue => issue == "Country catalog contains an empty ID.");
        Assert.Contains(report.RelationshipIssues, issue => issue == "Category catalog contains an empty ID.");
        Assert.Contains(report.RelationshipIssues, issue => issue == "Article catalog contains an empty ID.");
    }

    [Fact]
    public async Task EmptyCatalogsProduceAnEmptyReadOnlyReport()
    {
        var directory = Path.Combine(Path.GetTempPath(), $"content-audit-{Guid.NewGuid():N}");
        Directory.CreateDirectory(directory);
        try
        {
            var files = new Dictionary<string, string>
            {
                ["countries.json"] = "[]",
                ["categories.json"] = "[]",
                ["information-entries.json"] = "[]",
                ["official-sources.json"] = "[]",
                ["comparison-facts.json"] = "{\"Topics\":[]}"
            };
            foreach (var (name, content) in files)
            {
                await File.WriteAllTextAsync(Path.Combine(directory, name), content);
            }

            var before = files.Keys.ToDictionary(name => name, name => File.ReadAllBytes(Path.Combine(directory, name)));
            var data = await new ContentAuditCatalogLoader().LoadAsync(directory);
            var report = new ContentMaintenanceAuditor().CreateReport(data, AsOf);
            var after = files.Keys.ToDictionary(name => name, name => File.ReadAllBytes(Path.Combine(directory, name)));

            Assert.Equal(0, report.ArticleCount);
            Assert.Equal(0, report.TranslationCount);
            Assert.Equal(0, report.SourceCount);
            Assert.Equal(0, report.ComparisonFactCount);
            Assert.Empty(report.Sources);
            Assert.Empty(report.RelationshipIssues);
            Assert.Equal(before.Keys.Order(), after.Keys.Order());
            foreach (var name in files.Keys)
            {
                Assert.Equal(before[name], after[name]);
            }
        }
        finally
        {
            Directory.Delete(directory, recursive: true);
        }
    }

    [Fact]
    public void AuditDoesNotChangeVerificationOrSourceTimestamps()
    {
        var data = CreateData(sourceDates: [AsOf.AddDays(-100)]);
        var translation = Assert.Single(data.Translations);
        var source = Assert.Single(data.Sources);
        var originalStatus = translation.VerificationStatus;
        var originalUpdatedAt = translation.LastUpdatedAt;
        var originalCheckedAt = source.LastCheckedAt;

        var report = new ContentMaintenanceAuditor().CreateReport(data, AsOf);

        Assert.Equal(SourceReviewPriority.ReviewPriority, Assert.Single(report.Sources).Priority);
        Assert.Equal(originalStatus, translation.VerificationStatus);
        Assert.Equal(originalUpdatedAt, translation.LastUpdatedAt);
        Assert.Equal(originalCheckedAt, source.LastCheckedAt);
    }

    private static ContentAuditData CreateData(IReadOnlyList<DateOnly> sourceDates, bool duplicateUrl = false)
    {
        var country = new Country(Guid.Parse("10000000-0000-0000-0000-000000000001"), "Germany", "germany");
        var category = new InformationCategory(Guid.Parse("20000000-0000-0000-0000-000000000002"), "Employment", "employment");
        var article = new InformationEntry(Guid.Parse("30000000-0000-0000-0000-000000000001"), country.Id, category.Id);
        var titleTime = AsOf.AddDays(-2).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        var translations = new[]
        {
            new InformationEntryTranslation(article.Id, "en", "Work in Germany", "Summary", "Article body", VerificationStatus.NeedsReview, titleTime)
        };
        var sources = sourceDates.Select((date, index) => new OfficialSource(
            Guid.Parse($"40000000-0000-0000-0000-{index + 1:000000000000}"),
            article.Id,
            $"Official source {index + 1}",
            new Uri(duplicateUrl ? "https://example.test/shared" : $"https://example.test/source-{index + 1}"),
            date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc))).ToArray();
        var fact = new ComparisonFactReference(
            "Compare.Topic.Employment",
            category.Slug,
            "work-rights",
            country.Slug,
            article.Id,
            [sources[0].Id]);
        return new ContentAuditData([country], [category], [article], translations, sources, [fact]);
    }
}
