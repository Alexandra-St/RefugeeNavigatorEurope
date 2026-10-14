using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Web.Search;

public sealed record ArticleSearchResult(
    InformationEntry Entry,
    InformationEntryTranslation Translation,
    Country Country,
    InformationCategory Category);

public static class ArticleSearch
{
    public static IReadOnlyList<ArticleSearchResult> Search(
        CatalogData catalog,
        string? query,
        string? countrySlug,
        string? categorySlug,
        string? statusValue,
        string languageCode)
    {
        ArgumentNullException.ThrowIfNull(catalog);
        ArgumentException.ThrowIfNullOrWhiteSpace(languageCode);

        var country = string.IsNullOrWhiteSpace(countrySlug)
            ? null
            : catalog.Countries.FirstOrDefault(item => string.Equals(item.Slug, countrySlug, StringComparison.OrdinalIgnoreCase));
        var category = string.IsNullOrWhiteSpace(categorySlug)
            ? null
            : catalog.Categories.FirstOrDefault(item => string.Equals(item.Slug, categorySlug, StringComparison.OrdinalIgnoreCase));
        var hasCountryFilter = !string.IsNullOrWhiteSpace(countrySlug);
        var hasCategoryFilter = !string.IsNullOrWhiteSpace(categorySlug);

        if ((hasCountryFilter && country is null) || (hasCategoryFilter && category is null))
        {
            return Array.Empty<ArticleSearchResult>();
        }

        VerificationStatus? status = null;
        if (!string.IsNullOrWhiteSpace(statusValue))
        {
            if (!Enum.TryParse<VerificationStatus>(statusValue, ignoreCase: true, out var parsedStatus) ||
                !Enum.IsDefined(parsedStatus))
            {
                return Array.Empty<ArticleSearchResult>();
            }

            status = parsedStatus;
        }

        var normalizedQuery = query?.Trim();
        var translationsByEntry = catalog.InformationEntryTranslations
            .Where(translation => string.Equals(translation.LanguageCode, languageCode, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(translation => translation.InformationEntryId);
        var countriesById = catalog.Countries.ToDictionary(item => item.Id);
        var categoriesById = catalog.Categories.ToDictionary(item => item.Id);
        var results = new List<ArticleSearchResult>();

        foreach (var entry in catalog.InformationEntries)
        {
            if (!translationsByEntry.TryGetValue(entry.Id, out var translation) ||
                !countriesById.TryGetValue(entry.CountryId, out var entryCountry) ||
                !categoriesById.TryGetValue(entry.CategoryId, out var entryCategory))
            {
                continue;
            }

            if ((country is not null && entry.CountryId != country.Id) ||
                (category is not null && entry.CategoryId != category.Id) ||
                (status.HasValue && translation.VerificationStatus != status.Value))
            {
                continue;
            }

            if (!string.IsNullOrEmpty(normalizedQuery) &&
                !Contains(translation.Title, normalizedQuery) &&
                !Contains(translation.Summary, normalizedQuery) &&
                !Contains(translation.Content, normalizedQuery))
            {
                continue;
            }

            results.Add(new ArticleSearchResult(entry, translation, entryCountry, entryCategory));
        }

        return results;
    }

    private static bool Contains(string value, string query) =>
        value.Contains(query, StringComparison.OrdinalIgnoreCase);
}
