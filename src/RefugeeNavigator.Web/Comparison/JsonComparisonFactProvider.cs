using System.Text.Json;
using System.Text.Json.Serialization;
using RefugeeNavigator.Data;

namespace RefugeeNavigator.Web.Comparison;

public sealed class JsonComparisonFactProvider(string path)
{
    private static readonly string[] Languages = ["uk", "en", "ru"];
    private static readonly string[] Countries = ["denmark", "germany", "poland"];
    private static readonly IReadOnlyDictionary<string, string> MissingText = new Dictionary<string, string>(StringComparer.Ordinal)
    {
        ["uk"] = "Не встановлено за перевіреними джерелами.",
        ["en"] = "Not established in the reviewed sources.",
        ["ru"] = "Не установлено по проверенным источникам."
    };
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public async Task<ComparisonFactCatalog> LoadAsync(
        CatalogData catalog,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(catalog);
        await using var stream = File.OpenRead(path);
        var result = await JsonSerializer.DeserializeAsync<ComparisonFactCatalog>(stream, JsonOptions, cancellationToken)
            ?? throw new InvalidDataException("Comparison fact catalog is empty.");
        Validate(result, catalog);
        return result;
    }

    public static void Validate(ComparisonFactCatalog facts, CatalogData catalog)
    {
        ArgumentNullException.ThrowIfNull(facts);
        ArgumentNullException.ThrowIfNull(catalog);
        var errors = new List<string>();
        var sourceById = catalog.OfficialSources.ToDictionary(source => source.Id);
        var entryById = catalog.InformationEntries.ToDictionary(entry => entry.Id);
        var expectedTopics = CountryComparisonTopicMap.All.ToDictionary(topic => topic.ResourceKey, StringComparer.Ordinal);

        if (facts.Topics.Count != expectedTopics.Count || facts.Topics.Select(topic => topic.TopicResourceKey).Distinct(StringComparer.Ordinal).Count() != facts.Topics.Count)
        {
            errors.Add("The comparison fact catalog must map each of the eight comparison topics exactly once.");
        }

        var aspectIds = new HashSet<string>(StringComparer.Ordinal);
        foreach (var topic in facts.Topics)
        {
            if (!expectedTopics.TryGetValue(topic.TopicResourceKey, out var mappedTopic))
            {
                errors.Add($"Unknown comparison topic '{topic.TopicResourceKey}'.");
                continue;
            }

            if (!string.Equals(topic.CategorySlug, mappedTopic.CategorySlug, StringComparison.Ordinal))
            {
                errors.Add($"Topic '{topic.TopicResourceKey}' has an incorrect category relationship.");
            }

            if (topic.Aspects.Count == 0)
            {
                errors.Add($"Topic '{topic.TopicResourceKey}' must contain at least one comparison aspect.");
            }

            foreach (var aspect in topic.Aspects)
            {
                if (string.IsNullOrWhiteSpace(aspect.Id) || !aspectIds.Add(aspect.Id))
                {
                    errors.Add($"Topic '{topic.TopicResourceKey}' has a missing or duplicate aspect ID.");
                }

                if (string.IsNullOrWhiteSpace(aspect.QuestionResourceKey))
                {
                    errors.Add($"Aspect '{aspect.Id}' is missing its localization key.");
                }

                if (aspect.Facts.Select(fact => fact.CountrySlug).Distinct(StringComparer.OrdinalIgnoreCase).Count() != Countries.Length ||
                    !Countries.All(country => aspect.Facts.Any(fact => string.Equals(fact.CountrySlug, country, StringComparison.OrdinalIgnoreCase))))
                {
                    errors.Add($"Aspect '{aspect.Id}' must contain one answer for Denmark, Germany, and Poland.");
                }

                foreach (var fact in aspect.Facts)
                {
                    if (!mappedTopic.EntryIds.TryGetValue(fact.CountrySlug, out var expectedEntryId) || expectedEntryId != fact.InformationEntryId)
                    {
                        errors.Add($"Fact '{aspect.Id}/{fact.CountrySlug}' refers to the wrong comparison article.");
                    }

                    if (!entryById.TryGetValue(fact.InformationEntryId, out var entry) || entry.CategoryId != catalog.Categories.SingleOrDefault(category => category.Slug == topic.CategorySlug)?.Id)
                    {
                        errors.Add($"Fact '{aspect.Id}/{fact.CountrySlug}' has an invalid article/category relationship.");
                    }

                    foreach (var sourceId in fact.OfficialSourceIds)
                    {
                        if (!sourceById.TryGetValue(sourceId, out var source) || source.InformationEntryId != fact.InformationEntryId)
                        {
                            errors.Add($"Fact '{aspect.Id}/{fact.CountrySlug}' refers to an unrelated or missing official source.");
                        }
                    }

                    foreach (var language in Languages)
                    {
                        if (!fact.Values.TryGetValue(language, out var value) || string.IsNullOrWhiteSpace(value))
                        {
                            errors.Add($"Fact '{aspect.Id}/{fact.CountrySlug}' is missing its '{language}' text.");
                        }
                    }

                    if (fact.Values.Count != Languages.Length)
                    {
                        errors.Add($"Fact '{aspect.Id}/{fact.CountrySlug}' must contain exactly the supported languages.");
                    }

                    if (!fact.IsAvailable)
                    {
                        if (fact.OfficialSourceIds.Count > 0)
                        {
                            errors.Add($"Unavailable fact '{aspect.Id}/{fact.CountrySlug}' must not imply a source supports an absence claim.");
                        }

                        foreach (var (language, expectedText) in MissingText)
                        {
                            if (!string.Equals(fact.Values.GetValueOrDefault(language), expectedText, StringComparison.Ordinal))
                            {
                                errors.Add($"Unavailable fact '{aspect.Id}/{fact.CountrySlug}' must use the localized reviewed-source fallback.");
                            }
                        }
                    }

                    if (fact.IsAvailable && fact.OfficialSourceIds.Count == 0)
                    {
                        errors.Add($"Available fact '{aspect.Id}/{fact.CountrySlug}' must cite at least one official source.");
                    }
                }
            }
        }

        foreach (var topicKey in expectedTopics.Keys.Except(facts.Topics.Select(topic => topic.TopicResourceKey), StringComparer.Ordinal))
        {
            errors.Add($"Comparison fact catalog is missing topic '{topicKey}'.");
        }

        if (errors.Count > 0)
        {
            throw new InvalidDataException($"Comparison fact catalog is invalid: {string.Join(" ", errors)}");
        }
    }
}
