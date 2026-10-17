using System.Globalization;

namespace RefugeeNavigator.ContentAudit;

public static class ContentAuditReportFormatter
{
    public static string ToConsoleSummary(ContentAuditReport report) =>
        $"Content maintenance audit as of {report.AsOf:yyyy-MM-dd} (UTC date)\n" +
        $"Articles: {report.ArticleCount}; translations: {report.TranslationCount}; sources: {report.SourceCount}; comparison facts: {report.ComparisonFactCount}\n" +
        $"Source age: {report.RecentlyCheckedCount} recently checked; {report.ReviewSuggestedCount} review suggested; {report.ReviewPriorityCount} review priority\n" +
        $"Review candidates: {report.ReviewCandidates.Count} sources; {report.AffectedArticles.Count} articles; {report.AffectedComparisonFacts.Count} comparison facts\n" +
        $"Duplicate URLs: {report.DuplicateUrls.Count}; relationship issues: {report.RelationshipIssues.Count}\n" +
        "Age is an editorial scheduling signal only. This report does not check official websites or determine legal validity.";

    public static string ToMarkdown(ContentAuditReport report)
    {
        var lines = new List<string>
        {
            "# Content freshness and dependency audit",
            string.Empty,
            $"As of **{report.AsOf:yyyy-MM-dd} (UTC date)**. These thresholds schedule editorial review; age does not prove that a source or content is outdated. This report reads local catalog dates only and performs no HTTP checks.",
            string.Empty,
            "## Catalog summary",
            string.Empty,
            $"- Articles: {report.ArticleCount}",
            $"- Translations: {report.TranslationCount}",
            $"- Official source records: {report.SourceCount}",
            $"- Structured comparison facts: {report.ComparisonFactCount}",
            $"- Review thresholds: recently checked through {report.Thresholds.RecentlyCheckedThroughDays} days; review suggested through {report.Thresholds.ReviewSuggestedThroughDays} days; review priority above {report.Thresholds.ReviewSuggestedThroughDays} days",
            $"- Source age: {report.RecentlyCheckedCount} recently checked, {report.ReviewSuggestedCount} review suggested, {report.ReviewPriorityCount} review priority",
            $"- Duplicate URLs: {report.DuplicateUrls.Count}",
            $"- Invalid or missing relationships: {report.RelationshipIssues.Count}",
            string.Empty,
            "## Sources and dependencies",
            string.Empty,
            "| Source ID | Official source | URL | Country | Article | Category | LastCheckedAt | Days since check | Review priority | Related comparison facts |",
            "|---|---|---|---|---|---|---|---:|---|---|"
        };

        foreach (var source in report.Sources)
        {
            var article = source.Articles.FirstOrDefault();
            var country = article?.Country ?? "—";
            var articleName = article is null ? "—" : $"{article.Title} ({article.InformationEntryId})";
            var category = article?.Category ?? "—";
            var comparisons = source.ComparisonFacts.Count == 0
                ? "—"
                : string.Join("<br>", source.ComparisonFacts.Select(fact => $"{fact.TopicResourceKey} / {fact.AspectId} / {fact.Country}"));
            lines.Add($"| {source.Id} | {Escape(source.Title)} | [{Escape(source.Url.AbsoluteUri)}]({source.Url.AbsoluteUri}) | {Escape(country)} | {Escape(articleName)} | {Escape(category)} | {source.LastCheckedAt.ToString("O", CultureInfo.InvariantCulture)} | {source.DaysSinceLastCheck} | {PriorityLabel(source.Priority)} | {Escape(comparisons)} |");
        }

        lines.AddRange([string.Empty, "## Sources suggested for review", string.Empty]);
        if (report.ReviewCandidates.Count == 0)
        {
            lines.Add("No sources are beyond the recently checked threshold.");
        }
        else
        {
            foreach (var source in report.ReviewCandidates)
            {
                var articleTitles = source.Articles.Count == 0 ? "No linked article" : string.Join(", ", source.Articles.Select(article => article.Title));
                var comparisonCount = source.ComparisonFacts.Count;
                lines.Add($"- **{PriorityLabel(source.Priority)}** — {Escape(source.Title)} (`{source.Id}`), {source.DaysSinceLastCheck} days since recorded check; affected article(s): {Escape(articleTitles)}; comparison facts: {comparisonCount}.");
            }
        }

        lines.AddRange([string.Empty, "## Affected articles", string.Empty]);
        if (report.AffectedArticles.Count == 0)
        {
            lines.Add("None.");
        }
        else
        {
            foreach (var article in report.AffectedArticles)
            {
                lines.Add($"- {Escape(article.Country)} / {Escape(article.Category)} — {Escape(article.Title)} (`{article.InformationEntryId}`).");
            }
        }

        lines.AddRange([string.Empty, "## Affected comparison facts", string.Empty]);
        if (report.AffectedComparisonFacts.Count == 0)
        {
            lines.Add("None.");
        }
        else
        {
            foreach (var fact in report.AffectedComparisonFacts)
            {
                lines.Add($"- {Escape(fact.TopicResourceKey)} / `{Escape(fact.AspectId)}` / {Escape(fact.Country)}.");
            }
        }

        lines.AddRange([string.Empty, "## Duplicate source URLs", string.Empty]);
        if (report.DuplicateUrls.Count == 0)
        {
            lines.Add("None found. Duplicate records are not merged by this tool.");
        }
        else
        {
            foreach (var duplicate in report.DuplicateUrls)
            {
                lines.Add($"- [{Escape(duplicate.Url.AbsoluteUri)}]({duplicate.Url.AbsoluteUri}) — source records: {string.Join(", ", duplicate.SourceIds.Select(id => $"`{id}`"))}. Records and check dates remain separate.");
            }
        }

        lines.AddRange([string.Empty, "## Invalid or missing relationships", string.Empty]);
        if (report.RelationshipIssues.Count == 0)
        {
            lines.Add("None found.");
        }
        else
        {
            lines.AddRange(report.RelationshipIssues.Select(issue => $"- {Escape(issue)}"));
        }

        lines.AddRange([string.Empty, "## Interpretation and next step", string.Empty,
            "A listed date means only that a catalog record contains that `LastCheckedAt` value; this tool has not checked the source page. Review a source directly, assess its effect on linked articles and comparison facts, record any genuine content edits through the normal editorial workflow, and update review metadata only after human verification."]);
        return string.Join(Environment.NewLine, lines);
    }

    private static string PriorityLabel(SourceReviewPriority priority) => priority switch
    {
        SourceReviewPriority.RecentlyChecked => "Recently checked",
        SourceReviewPriority.ReviewSuggested => "Review suggested",
        SourceReviewPriority.ReviewPriority => "Review priority",
        _ => priority.ToString()
    };

    private static string Escape(string value) => value.Replace("|", "\\|", StringComparison.Ordinal).Replace("\r", " ", StringComparison.Ordinal).Replace("\n", " ", StringComparison.Ordinal);
}
