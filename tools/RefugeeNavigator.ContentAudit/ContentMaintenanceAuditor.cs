using RefugeeNavigator.Domain;

namespace RefugeeNavigator.ContentAudit;

public sealed class ContentMaintenanceAuditor
{
    public ContentAuditReport CreateReport(ContentAuditData data, DateOnly asOf, AuditThresholds? thresholds = null)
    {
        ArgumentNullException.ThrowIfNull(data);
        thresholds ??= new AuditThresholds();
        thresholds.Validate();

        var countriesById = data.Countries.GroupBy(country => country.Id).ToDictionary(group => group.Key, group => group.First());
        var countriesBySlug = data.Countries.GroupBy(country => country.Slug, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(group => group.Key, group => group.First(), StringComparer.OrdinalIgnoreCase);
        var categoriesById = data.Categories.GroupBy(category => category.Id).ToDictionary(group => group.Key, group => group.First());
        var categoriesBySlug = data.Categories.GroupBy(category => category.Slug, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(group => group.Key, group => group.First(), StringComparer.OrdinalIgnoreCase);
        var articlesById = data.Articles.GroupBy(article => article.Id).ToDictionary(group => group.Key, group => group.First());
        var sourcesById = data.Sources.GroupBy(source => source.Id).ToDictionary(group => group.Key, group => group.First());
        var englishTitles = data.Translations
            .Where(translation => translation.LanguageCode == "en")
            .GroupBy(translation => translation.InformationEntryId)
            .ToDictionary(group => group.Key, group => group.First().Title);
        var issues = new List<string>();

        AddEmptyAndDuplicateIdIssues("Country", data.Countries.Select(country => country.Id), issues);
        AddEmptyAndDuplicateIdIssues("Category", data.Categories.Select(category => category.Id), issues);
        AddEmptyAndDuplicateIdIssues("Article", data.Articles.Select(article => article.Id), issues);
        AddEmptyAndDuplicateIdIssues("Official source", data.Sources.Select(source => source.Id), issues);

        foreach (var article in data.Articles)
        {
            if (!countriesById.ContainsKey(article.CountryId))
            {
                issues.Add($"Article '{article.Id}' references missing country ID '{article.CountryId}'.");
            }

            if (!categoriesById.ContainsKey(article.CategoryId))
            {
                issues.Add($"Article '{article.Id}' references missing category ID '{article.CategoryId}'.");
            }
        }

        foreach (var translation in data.Translations)
        {
            if (!articlesById.ContainsKey(translation.InformationEntryId))
            {
                issues.Add($"Translation '{translation.InformationEntryId}/{translation.LanguageCode}' references a missing article ID.");
            }
        }

        var factsBySourceId = new Dictionary<Guid, List<ComparisonDependency>>();
        foreach (var fact in data.ComparisonFacts)
        {
            var hasArticle = articlesById.TryGetValue(fact.InformationEntryId, out var article);
            if (!hasArticle)
            {
                issues.Add($"Comparison fact '{fact.TopicResourceKey}/{fact.AspectId}/{fact.CountrySlug}' references missing article ID '{fact.InformationEntryId}'.");
            }

            var hasCategory = categoriesBySlug.TryGetValue(fact.CategorySlug, out var topicCategory);
            if (!hasCategory)
            {
                issues.Add($"Comparison topic '{fact.TopicResourceKey}' references missing category slug '{fact.CategorySlug}'.");
            }
            else if (hasArticle && article is not null && article.CategoryId != topicCategory!.Id)
            {
                issues.Add($"Comparison fact '{fact.TopicResourceKey}/{fact.AspectId}/{fact.CountrySlug}' has a category/article mismatch.");
            }

            var hasCountry = countriesBySlug.TryGetValue(fact.CountrySlug, out var country);
            if (!hasCountry)
            {
                issues.Add($"Comparison fact '{fact.TopicResourceKey}/{fact.AspectId}' references missing country slug '{fact.CountrySlug}'.");
            }
            else if (hasArticle && article is not null && countriesById.TryGetValue(article.CountryId, out var articleCountry) &&
                     articleCountry.Id != country!.Id)
            {
                issues.Add($"Comparison fact '{fact.TopicResourceKey}/{fact.AspectId}/{fact.CountrySlug}' has a country/article mismatch.");
            }

            foreach (var sourceId in fact.OfficialSourceIds)
            {
                if (!sourcesById.TryGetValue(sourceId, out var source))
                {
                    issues.Add($"Comparison fact '{fact.TopicResourceKey}/{fact.AspectId}/{fact.CountrySlug}' references missing official source ID '{sourceId}'.");
                    continue;
                }

                if (source.InformationEntryId != fact.InformationEntryId)
                {
                    issues.Add($"Comparison fact '{fact.TopicResourceKey}/{fact.AspectId}/{fact.CountrySlug}' references source ID '{sourceId}' linked to a different article.");
                }

                if (!factsBySourceId.TryGetValue(sourceId, out var dependencies))
                {
                    dependencies = [];
                    factsBySourceId.Add(sourceId, dependencies);
                }

                dependencies.Add(new ComparisonDependency(fact.TopicResourceKey, fact.AspectId, country?.Name ?? fact.CountrySlug));
            }
        }

        var articleDependenciesBySourceId = new Dictionary<Guid, IReadOnlyList<ArticleDependency>>();
        foreach (var source in data.Sources)
        {
            if (!articlesById.TryGetValue(source.InformationEntryId, out var article))
            {
                issues.Add($"Official source '{source.Id}' references missing article ID '{source.InformationEntryId}'.");
                articleDependenciesBySourceId[source.Id] = [];
                continue;
            }

            var country = countriesById.GetValueOrDefault(article.CountryId)?.Name ?? article.CountryId.ToString();
            var category = categoriesById.GetValueOrDefault(article.CategoryId)?.Name ?? article.CategoryId.ToString();
            articleDependenciesBySourceId[source.Id] =
            [new ArticleDependency(article.Id, country, category, englishTitles.GetValueOrDefault(article.Id, article.Id.ToString()))];
        }

        var auditedSources = data.Sources.Select(source =>
        {
            var daysSinceCheck = asOf.DayNumber - DateOnly.FromDateTime(source.LastCheckedAt.UtcDateTime).DayNumber;
            if (daysSinceCheck < 0)
            {
                issues.Add($"Official source '{source.Id}' has a LastCheckedAt date after the report date.");
            }

            var priority = GetPriority(daysSinceCheck, thresholds);
            return new AuditedSource(
                source.Id,
                source.Title,
                source.Url,
                source.LastCheckedAt,
                daysSinceCheck,
                priority,
                articleDependenciesBySourceId.GetValueOrDefault(source.Id, []),
                factsBySourceId.GetValueOrDefault(source.Id, []));
        }).ToArray();

        var duplicates = data.Sources
            .GroupBy(source => source.Url.AbsoluteUri, StringComparer.Ordinal)
            .Where(group => group.Count() > 1)
            .Select(group => new DuplicateSourceUrl(group.First().Url, group.Select(source => source.Id).ToArray()))
            .ToArray();

        return new ContentAuditReport(
            asOf,
            thresholds,
            data.Articles.Count,
            data.Translations.Count,
            data.Sources.Count,
            data.ComparisonFacts.Count,
            auditedSources.Count(source => source.Priority == SourceReviewPriority.RecentlyChecked),
            auditedSources.Count(source => source.Priority == SourceReviewPriority.ReviewSuggested),
            auditedSources.Count(source => source.Priority == SourceReviewPriority.ReviewPriority),
            auditedSources.OrderBy(source => source.Priority).ThenByDescending(source => source.DaysSinceLastCheck).ToArray(),
            duplicates,
            issues.Distinct(StringComparer.Ordinal).ToArray());
    }

    private static SourceReviewPriority GetPriority(int ageInDays, AuditThresholds thresholds) =>
        ageInDays <= thresholds.RecentlyCheckedThroughDays
            ? SourceReviewPriority.RecentlyChecked
            : ageInDays <= thresholds.ReviewSuggestedThroughDays
                ? SourceReviewPriority.ReviewSuggested
                : SourceReviewPriority.ReviewPriority;

    private static void AddEmptyAndDuplicateIdIssues(string label, IEnumerable<Guid> ids, ICollection<string> issues)
    {
        var values = ids.ToArray();
        if (values.Contains(Guid.Empty))
        {
            issues.Add($"{label} catalog contains an empty ID.");
        }

        foreach (var duplicate in values.GroupBy(id => id).Where(group => group.Count() > 1))
        {
            issues.Add($"{label} catalog contains duplicate ID '{duplicate.Key}'.");
        }
    }
}
