using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Content;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Web.Seo;

public sealed record ResolvedArticleUrl(InformationEntry Entry, InformationEntryTranslation Translation,
    Country Country, InformationCategory Category, string Language, string Path);

/// <summary>Builds and resolves stable, language-specific URLs for production articles.</summary>
public sealed class ArticleUrlResolver(CatalogData catalog)
{
    private readonly CatalogData _catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
    private static readonly HashSet<string> Languages = SupportedCultures.All
        .Select(culture => culture.TwoLetterISOLanguageName).ToHashSet(StringComparer.Ordinal);

    public string? Generate(InformationEntry entry, string language)
    {
        ArgumentNullException.ThrowIfNull(entry);
        var catalogEntry = _catalog.InformationEntries.FirstOrDefault(candidate => candidate.Id == entry.Id);
        if (catalogEntry is null) return null;
        entry = catalogEntry;
        if (!Languages.Contains(language)) return null;
        var published = FindPublished(entry.Id, language);
        if (published is null) return null;
        var country = CountryVisibility.GetVisibleCountries(_catalog, false).FirstOrDefault(c => c.Id == entry.CountryId);
        var category = _catalog.Categories.FirstOrDefault(c => c.Id == entry.CategoryId);
        return country is null || category is null ? null : BuildPath(language, country.Slug, category.Slug, entry.Id);
    }

    public ResolvedArticleUrl? Resolve(string? language, string? countrySlug, string? categorySlug, string? articleKey)
    {
        if (language is null || !Languages.Contains(language) || countrySlug is null || categorySlug is null ||
            !Guid.TryParseExact(articleKey, "D", out var id) || !string.Equals(id.ToString("D"), articleKey, StringComparison.Ordinal)) return null;

        var country = CountryVisibility.GetVisibleCountries(_catalog, false)
            .FirstOrDefault(c => string.Equals(c.Slug, countrySlug, StringComparison.Ordinal));
        var category = _catalog.Categories.FirstOrDefault(c => string.Equals(c.Slug, categorySlug, StringComparison.Ordinal));
        var entry = _catalog.InformationEntries.FirstOrDefault(e => e.Id == id);
        var translation = entry is null ? null : FindPublished(id, language);
        if (country is null || category is null || entry is null || translation is null ||
            entry.CountryId != country.Id || entry.CategoryId != category.Id) return null;

        return new ResolvedArticleUrl(entry, translation, country, category, language,
            BuildPath(language, country.Slug, category.Slug, entry.Id));
    }

    public static string BuildPath(string language, string countrySlug, string categorySlug, Guid articleId) =>
        $"/{language}/articles/{countrySlug}/{categorySlug}/{articleId:D}";

    private InformationEntryTranslation? FindPublished(Guid id, string language) =>
        _catalog.InformationEntryTranslations.FirstOrDefault(t => t.InformationEntryId == id && t.LanguageCode == language &&
            t.VerificationStatus == VerificationStatus.Verified && !string.IsNullOrWhiteSpace(t.Title) &&
            !string.IsNullOrWhiteSpace(t.Summary) && !string.IsNullOrWhiteSpace(t.Content));
}
