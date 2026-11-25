using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Content;

namespace RefugeeNavigator.Web.Comparison;

public static class ComparisonFactVisibility
{
    public static bool CanRender(CountryComparisonFact fact, CatalogData catalog, string languageCode, bool isDevelopment)
    {
        var country = CountryVisibility.GetVisibleCountries(catalog, isDevelopment)
            .FirstOrDefault(item => string.Equals(item.Slug, fact.CountrySlug, StringComparison.OrdinalIgnoreCase));
        if (!fact.IsAvailable || country is null ||
            !CountryComparisonTopicMap.SupportedCountrySlugs.Contains(fact.CountrySlug) ||
            !catalog.InformationEntries.Any(entry => entry.Id == fact.InformationEntryId && entry.CountryId == country.Id))
            return false;

        var translation = catalog.InformationEntryTranslations.SingleOrDefault(item =>
            item.InformationEntryId == fact.InformationEntryId && item.LanguageCode == languageCode);
        var statusAllowsRendering = fact.Status == ComparisonFactStatus.Approved ||
            (isDevelopment && fact.Status == ComparisonFactStatus.Draft);
        var translationAllowsRendering = translation?.VerificationStatus == VerificationStatus.Verified ||
            (isDevelopment && fact.Status == ComparisonFactStatus.Draft &&
             translation?.VerificationStatus == VerificationStatus.NeedsReview);
        var hasValidOwnedSources = fact.OfficialSourceIds.Count > 0 && fact.OfficialSourceIds.All(sourceId =>
            catalog.OfficialSources.Any(source => source.Id == sourceId && source.InformationEntryId == fact.InformationEntryId));

        return statusAllowsRendering && translationAllowsRendering && hasValidOwnedSources;
    }
}
