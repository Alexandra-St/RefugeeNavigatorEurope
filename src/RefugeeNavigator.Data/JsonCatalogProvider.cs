using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Data;

public sealed class JsonCatalogProvider
{
    private static readonly string[] RequiredTranslationLanguages = ["uk", "en", "ru"];

    private readonly JsonCountryProvider _countryProvider;
    private readonly JsonInformationCategoryProvider _categoryProvider;
    private readonly JsonInformationEntryProvider _informationEntryProvider;
    private readonly JsonOfficialSourceProvider _officialSourceProvider;

    public JsonCatalogProvider(string contentRootPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(contentRootPath);

        var dataDirectory = Path.Combine(contentRootPath, "Data");
        _countryProvider = new JsonCountryProvider(Path.Combine(dataDirectory, "countries.json"));
        _categoryProvider = new JsonInformationCategoryProvider(Path.Combine(dataDirectory, "categories.json"));
        _informationEntryProvider = new JsonInformationEntryProvider(Path.Combine(dataDirectory, "information-entries.json"));
        _officialSourceProvider = new JsonOfficialSourceProvider(Path.Combine(dataDirectory, "official-sources.json"));
    }

    public async Task<CatalogData> LoadAsync(CancellationToken cancellationToken = default)
    {
        var countries = await _countryProvider.LoadAsync(cancellationToken);
        var categories = await _categoryProvider.LoadAsync(cancellationToken);
        var content = await _informationEntryProvider.LoadAsync(cancellationToken);
        var officialSources = await _officialSourceProvider.LoadAsync(cancellationToken);

        ValidateCountries(countries);
        ValidateCategories(categories);
        ValidateInformationContent(countries, categories, content.Entries, content.Translations, officialSources);

        return new CatalogData(countries, categories, content.Entries, content.Translations, officialSources);
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

    private static void ValidateInformationContent(
        IReadOnlyList<Country> countries,
        IReadOnlyList<InformationCategory> categories,
        IReadOnlyList<InformationEntry> entries,
        IReadOnlyList<InformationEntryTranslation> translations,
        IReadOnlyList<OfficialSource> sources)
    {
        var errors = new List<string>();

        foreach (var entry in entries)
        {
            errors.AddRange(DomainModelValidator.Validate(entry)
                .Select(error => $"InformationEntry '{entry.Id}': {error}"));
        }

        foreach (var translation in translations)
        {
            errors.AddRange(DomainModelValidator.Validate(translation)
                .Select(error => $"InformationEntryTranslation '{translation.InformationEntryId}/{translation.LanguageCode}': {error}"));
        }

        foreach (var source in sources)
        {
            errors.AddRange(DomainModelValidator.Validate(source)
                .Select(error => $"OfficialSource '{source.Id}': {error}"));
        }

        errors.AddRange(DataUniquenessValidator.Validate(entries));
        errors.AddRange(DataUniquenessValidator.Validate(translations));
        errors.AddRange(DataUniquenessValidator.Validate(sources));
        errors.AddRange(DataRelationshipValidator.Validate(countries, categories, entries, translations, sources));
        ValidateTranslationCoverage(entries, translations, errors);

        ThrowIfInvalid("Information content", errors);
    }

    private static void ValidateTranslationCoverage(
        IReadOnlyList<InformationEntry> entries,
        IReadOnlyList<InformationEntryTranslation> translations,
        ICollection<string> errors)
    {
        foreach (var entry in entries)
        {
            var entryTranslations = translations
                .Where(translation => translation.InformationEntryId == entry.Id)
                .ToArray();

            if (entryTranslations.Length == 0)
            {
                errors.Add($"InformationEntry '{entry.Id}' must contain at least one translation.");
            }

            foreach (var languageCode in RequiredTranslationLanguages)
            {
                var languageCount = entryTranslations.Count(translation => translation.LanguageCode == languageCode);
                if (languageCount == 0)
                {
                    errors.Add($"InformationEntry '{entry.Id}' is missing translation for language '{languageCode}'.");
                }
                else if (languageCount > 1)
                {
                    errors.Add($"InformationEntry '{entry.Id}' has multiple '{languageCode}' translations.");
                }
            }
        }
    }

    private static void ThrowIfInvalid(string catalogName, IReadOnlyCollection<string> errors)
    {
        if (errors.Count > 0)
        {
            throw new InvalidDataException($"{catalogName} catalog is invalid: {string.Join(" ", errors)}");
        }
    }
}
