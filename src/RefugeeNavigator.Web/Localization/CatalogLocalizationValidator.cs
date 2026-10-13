using System.Globalization;
using RefugeeNavigator.Data;

namespace RefugeeNavigator.Web.Localization;

public static class CatalogLocalizationValidator
{
    private static readonly string[] SupportedCultures = ["uk", "en", "ru"];

    public static void Validate(
        CatalogData catalog,
        Func<string, CultureInfo, string?> getResource)
    {
        ArgumentNullException.ThrowIfNull(catalog);
        ArgumentNullException.ThrowIfNull(getResource);

        var errors = new List<string>();
        foreach (var cultureName in SupportedCultures)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);

            foreach (var country in catalog.Countries)
            {
                ValidateResource(CatalogLocalizationKeyResolver.CountryName(country), culture, getResource, errors);
            }

            foreach (var category in catalog.Categories)
            {
                ValidateResource(CatalogLocalizationKeyResolver.CategoryName(category), culture, getResource, errors);
                ValidateResource(CatalogLocalizationKeyResolver.CategoryDescription(category), culture, getResource, errors);
            }
        }

        if (errors.Count > 0)
        {
            throw new InvalidDataException(
                $"Catalog localization validation failed:{Environment.NewLine}{string.Join(Environment.NewLine, errors)}");
        }
    }

    private static void ValidateResource(
        string key,
        CultureInfo culture,
        Func<string, CultureInfo, string?> getResource,
        ICollection<string> errors)
    {
        if (string.IsNullOrWhiteSpace(getResource(key, culture)))
        {
            errors.Add($"Missing or empty localization value '{key}' for culture '{culture.Name}'.");
        }
    }
}
