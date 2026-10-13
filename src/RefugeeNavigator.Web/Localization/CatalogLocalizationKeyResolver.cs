using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Web.Localization;

public static class CatalogLocalizationKeyResolver
{
    public static string CountryName(Country country) => $"Country.{ToResourceSuffix(country.Slug)}";

    public static string CategoryName(InformationCategory category) => $"Category.{ToResourceSuffix(category.Slug)}";

    public static string CategoryDescription(InformationCategory category) =>
        $"{CategoryName(category)}Description";

    private static string ToResourceSuffix(string slug)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(slug);

        return string.Concat(slug.Split('-', StringSplitOptions.RemoveEmptyEntries)
            .Select(part => char.ToUpperInvariant(part[0]) + part[1..]));
    }
}
