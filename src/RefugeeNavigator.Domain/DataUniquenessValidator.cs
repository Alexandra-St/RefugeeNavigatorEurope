namespace RefugeeNavigator.Domain;

public static class DataUniquenessValidator
{
    public static IReadOnlyList<string> Validate(IEnumerable<Country> countries)
    {
        ArgumentNullException.ThrowIfNull(countries);

        var errors = new List<string>();
        var ids = new HashSet<Guid>();
        var slugs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var country in countries)
        {
            if (!ids.Add(country.Id))
            {
                errors.Add("Country: duplicate Id.");
            }

            if (!slugs.Add(country.Slug))
            {
                errors.Add("Country: duplicate Slug.");
            }
        }

        return errors.AsReadOnly();
    }

    public static IReadOnlyList<string> Validate(IEnumerable<InformationCategory> categories)
    {
        ArgumentNullException.ThrowIfNull(categories);

        var errors = new List<string>();
        var ids = new HashSet<Guid>();
        var slugs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var category in categories)
        {
            if (!ids.Add(category.Id))
            {
                errors.Add("InformationCategory: duplicate Id.");
            }

            if (!slugs.Add(category.Slug))
            {
                errors.Add("InformationCategory: duplicate Slug.");
            }
        }

        return errors.AsReadOnly();
    }

    public static IReadOnlyList<string> Validate(IEnumerable<InformationEntry> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);

        var errors = new List<string>();
        var ids = new HashSet<Guid>();

        foreach (var entry in entries)
        {
            if (!ids.Add(entry.Id))
            {
                errors.Add("InformationEntry: duplicate Id.");
            }
        }

        return errors.AsReadOnly();
    }

    public static IReadOnlyList<string> Validate(IEnumerable<OfficialSource> sources)
    {
        ArgumentNullException.ThrowIfNull(sources);

        var errors = new List<string>();
        var ids = new HashSet<Guid>();

        foreach (var source in sources)
        {
            if (!ids.Add(source.Id))
            {
                errors.Add("OfficialSource: duplicate Id.");
            }
        }

        return errors.AsReadOnly();
    }
}
