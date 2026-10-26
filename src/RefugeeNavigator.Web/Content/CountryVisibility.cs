using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Web.Content;

public static class CountryVisibility
{
    public static IReadOnlyList<Country> GetVisibleCountries(CatalogData catalog, bool isDevelopment)
    {
        if (isDevelopment)
        {
            return catalog.Countries;
        }

        var verifiedEntryIds = catalog.InformationEntryTranslations
            .Where(translation => translation.VerificationStatus == VerificationStatus.Verified)
            .Select(translation => translation.InformationEntryId)
            .ToHashSet();
        var acceptedCountryIds = catalog.InformationEntries
            .Where(entry => verifiedEntryIds.Contains(entry.Id))
            .Select(entry => entry.CountryId)
            .ToHashSet();

        return catalog.Countries.Where(country => acceptedCountryIds.Contains(country.Id)).ToArray();
    }
}
