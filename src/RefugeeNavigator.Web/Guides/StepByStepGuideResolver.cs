using RefugeeNavigator.Data;

namespace RefugeeNavigator.Web.Guides;

public static class StepByStepGuideResolver
{
    public static StepByStepGuide? Resolve(
        StepByStepGuideCatalog guides,
        CatalogData catalog,
        string countrySlug,
        string guideSlug)
    {
        ArgumentNullException.ThrowIfNull(guides);
        ArgumentNullException.ThrowIfNull(catalog);

        var country = catalog.Countries.SingleOrDefault(item =>
            string.Equals(item.Slug, countrySlug, StringComparison.OrdinalIgnoreCase));
        return country is null
            ? null
            : guides.Guides.SingleOrDefault(item =>
                item.CountryId == country.Id &&
                string.Equals(item.Slug, guideSlug, StringComparison.OrdinalIgnoreCase));
    }
}
