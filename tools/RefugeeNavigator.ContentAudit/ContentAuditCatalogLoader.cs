using System.Text.Json;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.ContentAudit;

public sealed class ContentAuditCatalogLoader
{
    public async Task<ContentAuditData> LoadAsync(string dataDirectory, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(dataDirectory);
        var countries = await new JsonCountryProvider(Path.Combine(dataDirectory, "countries.json")).LoadAsync(cancellationToken);
        var categories = await new JsonInformationCategoryProvider(Path.Combine(dataDirectory, "categories.json")).LoadAsync(cancellationToken);
        var (articles, translations) = await new JsonInformationEntryProvider(Path.Combine(dataDirectory, "information-entries.json")).LoadAsync(cancellationToken);
        var sources = await new JsonOfficialSourceProvider(Path.Combine(dataDirectory, "official-sources.json")).LoadAsync(cancellationToken);
        var facts = await LoadComparisonFactsAsync(Path.Combine(dataDirectory, "comparison-facts.json"), cancellationToken);
        return new ContentAuditData(countries, categories, articles, translations, sources, facts);
    }

    private static async Task<IReadOnlyList<ComparisonFactReference>> LoadComparisonFactsAsync(string path, CancellationToken cancellationToken)
    {
        await using var stream = File.OpenRead(path);
        using var document = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);
        var references = new List<ComparisonFactReference>();
        foreach (var topic in document.RootElement.GetProperty("Topics").EnumerateArray())
        {
            var topicKey = topic.GetProperty("TopicResourceKey").GetString() ?? string.Empty;
            var categorySlug = topic.GetProperty("CategorySlug").GetString() ?? string.Empty;
            foreach (var aspect in topic.GetProperty("Aspects").EnumerateArray())
            {
                var aspectId = aspect.GetProperty("Id").GetString() ?? string.Empty;
                foreach (var fact in aspect.GetProperty("Facts").EnumerateArray())
                {
                    references.Add(new ComparisonFactReference(
                        topicKey,
                        categorySlug,
                        aspectId,
                        fact.GetProperty("CountrySlug").GetString() ?? string.Empty,
                        fact.GetProperty("InformationEntryId").GetGuid(),
                        fact.GetProperty("OfficialSourceIds").EnumerateArray().Select(item => item.GetGuid()).ToArray()));
                }
            }
        }

        return references.AsReadOnly();
    }
}
