using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Data;

public sealed class JsonCatalogProvider
{
    private readonly JsonCountryProvider _countryProvider;
    private readonly JsonInformationCategoryProvider _categoryProvider;

    public JsonCatalogProvider(string contentRootPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(contentRootPath);

        var dataDirectory = Path.Combine(contentRootPath, "Data");
        _countryProvider = new JsonCountryProvider(Path.Combine(dataDirectory, "countries.json"));
        _categoryProvider = new JsonInformationCategoryProvider(Path.Combine(dataDirectory, "categories.json"));
    }

    public async Task<CatalogData> LoadAsync(CancellationToken cancellationToken = default)
    {
        var countries = await _countryProvider.LoadAsync(cancellationToken);
        var categories = await _categoryProvider.LoadAsync(cancellationToken);

        ValidateCountries(countries);
        ValidateCategories(categories);

        return new CatalogData(countries, categories);
    }

    private static void ValidateCountries(IReadOnlyList<Country> countries)
    {
        var errors = new List<string>();

        if (countries.Count == 0)
        {
            errors.Add("The catalog must contain at least one country.");
        }

        if (countries.Any(country => country is null))
        {
            errors.Add("The catalog must not contain null countries.");
        }
        else
        {
            foreach (var country in countries)
            {
                errors.AddRange(DomainModelValidator.Validate(country)
                    .Select(error => $"Country '{country.Slug}': {error}"));
            }

            errors.AddRange(DataUniquenessValidator.Validate(countries));
        }

        ThrowIfInvalid("Country", errors);
    }

    private static void ValidateCategories(IReadOnlyList<InformationCategory> categories)
    {
        var errors = new List<string>();

        if (categories.Count == 0)
        {
            errors.Add("The catalog must contain at least one information category.");
        }

        if (categories.Any(category => category is null))
        {
            errors.Add("The catalog must not contain null information categories.");
        }
        else
        {
            foreach (var category in categories)
            {
                errors.AddRange(DomainModelValidator.Validate(category)
                    .Select(error => $"Information category '{category.Slug}': {error}"));
            }

            errors.AddRange(DataUniquenessValidator.Validate(categories));
        }

        ThrowIfInvalid("Information category", errors);
    }

    private static void ThrowIfInvalid(string catalogName, IReadOnlyCollection<string> errors)
    {
        if (errors.Count > 0)
        {
            throw new InvalidDataException($"{catalogName} catalog is invalid: {string.Join(" ", errors)}");
        }
    }
}
