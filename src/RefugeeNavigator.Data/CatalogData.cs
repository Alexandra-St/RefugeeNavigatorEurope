using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Data;

public sealed class CatalogData
{
    internal CatalogData(
        IEnumerable<Country> countries,
        IEnumerable<InformationCategory> categories,
        IEnumerable<InformationEntry> informationEntries,
        IEnumerable<InformationEntryTranslation> informationEntryTranslations,
        IEnumerable<OfficialSource> officialSources)
    {
        Countries = Array.AsReadOnly(countries.ToArray());
        Categories = Array.AsReadOnly(categories.ToArray());
        InformationEntries = Array.AsReadOnly(informationEntries.ToArray());
        InformationEntryTranslations = Array.AsReadOnly(informationEntryTranslations.ToArray());
        OfficialSources = Array.AsReadOnly(officialSources.ToArray());
    }

    public IReadOnlyList<Country> Countries { get; }

    public IReadOnlyList<InformationCategory> Categories { get; }

    public IReadOnlyList<InformationEntry> InformationEntries { get; }

    public IReadOnlyList<InformationEntryTranslation> InformationEntryTranslations { get; }

    public IReadOnlyList<OfficialSource> OfficialSources { get; }
}
