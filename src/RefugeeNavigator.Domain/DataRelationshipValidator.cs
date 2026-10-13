namespace RefugeeNavigator.Domain;

public static class DataRelationshipValidator
{
    public static IReadOnlyList<string> Validate(
        IEnumerable<Country> countries,
        IEnumerable<InformationCategory> categories,
        IEnumerable<InformationEntry> entries,
        IEnumerable<OfficialSource> sources)
    {
        return Validate(countries, categories, entries, Array.Empty<InformationEntryTranslation>(), sources);
    }

    public static IReadOnlyList<string> Validate(
        IEnumerable<Country> countries,
        IEnumerable<InformationCategory> categories,
        IEnumerable<InformationEntry> entries,
        IEnumerable<InformationEntryTranslation> translations,
        IEnumerable<OfficialSource> sources)
    {
        ArgumentNullException.ThrowIfNull(countries);
        ArgumentNullException.ThrowIfNull(categories);
        ArgumentNullException.ThrowIfNull(entries);
        ArgumentNullException.ThrowIfNull(translations);
        ArgumentNullException.ThrowIfNull(sources);

        var countryIds = new HashSet<Guid>();
        foreach (var country in countries)
        {
            countryIds.Add(country.Id);
        }

        var categoryIds = new HashSet<Guid>();
        foreach (var category in categories)
        {
            categoryIds.Add(category.Id);
        }

        var errors = new List<string>();
        var entryIds = new HashSet<Guid>();
        foreach (var entry in entries)
        {
            entryIds.Add(entry.Id);

            if (!countryIds.Contains(entry.CountryId))
            {
                errors.Add("InformationEntry: CountryId references a missing Country.");
            }

            if (!categoryIds.Contains(entry.CategoryId))
            {
                errors.Add("InformationEntry: CategoryId references a missing InformationCategory.");
            }
        }

        foreach (var translation in translations)
        {
            if (!entryIds.Contains(translation.InformationEntryId))
            {
                errors.Add("InformationEntryTranslation: InformationEntryId references a missing InformationEntry.");
            }
        }

        foreach (var source in sources)
        {
            if (!entryIds.Contains(source.InformationEntryId))
            {
                errors.Add("OfficialSource: InformationEntryId references a missing InformationEntry.");
            }
        }

        return errors.AsReadOnly();
    }
}
