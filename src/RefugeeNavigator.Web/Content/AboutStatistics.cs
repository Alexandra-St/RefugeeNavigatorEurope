using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Comparison;

namespace RefugeeNavigator.Web.Content;

public sealed record AboutStatistics(int Countries, int PublishedArticles, int OfficialSources, int SupportedLanguages);

public static class AboutStatisticsProvider
{
    public static AboutStatistics Create(CatalogData catalog, ComparisonFactCatalog comparisonFacts, IReadOnlyCollection<string> supportedLanguages)
    {
        var countries = CountryVisibility.GetVisibleCountries(catalog, isDevelopment: false);
        var publishedEntryIds = catalog.InformationEntryTranslations
            .Where(item => item.VerificationStatus == VerificationStatus.Verified)
            .Select(item => item.InformationEntryId)
            .Where(entryId => countries.Any(country => catalog.InformationEntries.Any(entry =>
                entry.Id == entryId && entry.CountryId == country.Id)))
            .ToHashSet();

        var sourceIds = catalog.OfficialSources
            .Where(source => publishedEntryIds.Contains(source.InformationEntryId))
            .Select(source => source.Id)
            .ToHashSet();

        foreach (var fact in comparisonFacts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts))
        {
            if (supportedLanguages.Any(language => ComparisonFactVisibility.CanRender(fact, catalog, language, isDevelopment: false)))
                sourceIds.UnionWith(fact.OfficialSourceIds.Where(sourceId => catalog.OfficialSources.Any(source =>
                    source.Id == sourceId && source.InformationEntryId == fact.InformationEntryId)));
        }

        return new AboutStatistics(countries.Count, publishedEntryIds.Count, sourceIds.Count, supportedLanguages.Count);
    }
}
