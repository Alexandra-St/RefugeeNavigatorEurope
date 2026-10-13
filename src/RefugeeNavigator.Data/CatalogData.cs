using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Data;

public sealed class CatalogData
{
    internal CatalogData(IEnumerable<Country> countries, IEnumerable<InformationCategory> categories)
    {
        Countries = Array.AsReadOnly(countries.ToArray());
        Categories = Array.AsReadOnly(categories.ToArray());
    }

    public IReadOnlyList<Country> Countries { get; }

    public IReadOnlyList<InformationCategory> Categories { get; }
}
