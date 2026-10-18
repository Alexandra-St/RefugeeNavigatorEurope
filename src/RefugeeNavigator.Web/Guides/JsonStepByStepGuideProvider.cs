using System.Text.Json;
using System.Text.Json.Serialization;
using RefugeeNavigator.Data;

namespace RefugeeNavigator.Web.Guides;

public sealed class JsonStepByStepGuideProvider(string path)
{
    private static readonly string[] Languages = ["uk", "en", "ru"];
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public async Task<StepByStepGuideCatalog> LoadAsync(
        CatalogData catalog,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(catalog);
        await using var stream = File.OpenRead(path);
        var result = await JsonSerializer.DeserializeAsync<StepByStepGuideCatalog>(stream, JsonOptions, cancellationToken)
            ?? throw new InvalidDataException("Step-by-step guide catalog is empty.");
        Validate(result, catalog);
        return result;
    }

    public static void Validate(StepByStepGuideCatalog guideCatalog, CatalogData catalog)
    {
        ArgumentNullException.ThrowIfNull(guideCatalog);
        ArgumentNullException.ThrowIfNull(catalog);
        var errors = new List<string>();
        var countryIds = catalog.Countries.Select(country => country.Id).ToHashSet();
        var entries = catalog.InformationEntries.ToDictionary(entry => entry.Id);
        var sources = catalog.OfficialSources.ToDictionary(source => source.Id);

        if (guideCatalog.Guides.Count == 0)
            errors.Add("At least one guide is required.");

        if (guideCatalog.Guides.Select(guide => guide.Id).Distinct(StringComparer.Ordinal).Count() != guideCatalog.Guides.Count)
            errors.Add("Guide IDs must be unique.");

        if (guideCatalog.Guides.Select(guide => (guide.CountryId, guide.Slug)).Distinct().Count() != guideCatalog.Guides.Count)
            errors.Add("Guide slugs must be unique within each country.");

        foreach (var guide in guideCatalog.Guides)
        {
            if (guide is null)
            {
                errors.Add("The guide catalog must not contain null guides.");
                continue;
            }
            if (string.IsNullOrWhiteSpace(guide.Id) || string.IsNullOrWhiteSpace(guide.Slug))
                errors.Add("Every guide must have an ID and slug.");
            if (guide.EditorialStatus != GuideEditorialStatus.Draft)
                errors.Add($"Guide '{guide.Id}' must remain Draft until explicit editorial approval.");
            if (!countryIds.Contains(guide.CountryId))
                errors.Add($"Guide '{guide.Id}' refers to a missing country.");
            ValidateLanguages(guide.Translations, $"Guide '{guide.Id}'", errors);
            foreach (var language in Languages)
            {
                if (!guide.Translations.TryGetValue(language, out var text) ||
                    string.IsNullOrWhiteSpace(text.Title) || string.IsNullOrWhiteSpace(text.Description))
                    errors.Add($"Guide '{guide.Id}' is missing complete '{language}' translation.");
            }

            if (guide.Scenarios is null || guide.Scenarios.Count == 0)
            {
                errors.Add($"Guide '{guide.Id}' must have scenarios.");
                continue;
            }
            if (guide.Scenarios.Any(scenario => scenario is null) || guide.Scenarios.Select(scenario => scenario.Id).Distinct(StringComparer.Ordinal).Count() != guide.Scenarios.Count)
                errors.Add($"Guide '{guide.Id}' must have uniquely identified scenarios.");
            if (!guide.Scenarios.Select(scenario => scenario.Order).Order().SequenceEqual(Enumerable.Range(1, guide.Scenarios.Count)))
                errors.Add($"Guide '{guide.Id}' scenario order must be consecutive starting at 1.");

            foreach (var scenario in guide.Scenarios)
            {
                if (scenario is null)
                {
                    errors.Add($"Guide '{guide.Id}' must not contain null scenarios.");
                    continue;
                }
                if (string.IsNullOrWhiteSpace(scenario.Id) || scenario.Steps.Count == 0)
                    errors.Add($"Guide '{guide.Id}' contains a scenario without an ID or steps.");
                ValidateLanguages(scenario.Translations, $"Scenario '{guide.Id}/{scenario.Id}'", errors);
                foreach (var language in Languages)
                {
                    if (!scenario.Translations.TryGetValue(language, out var text) ||
                        string.IsNullOrWhiteSpace(text.Title) || string.IsNullOrWhiteSpace(text.Description))
                        errors.Add($"Scenario '{guide.Id}/{scenario.Id}' is missing complete '{language}' translation.");
                }

                if (scenario.Steps.Select(step => step.Id).Distinct(StringComparer.Ordinal).Count() != scenario.Steps.Count)
                    errors.Add($"Scenario '{guide.Id}/{scenario.Id}' contains duplicate step IDs.");
                if (!scenario.Steps.Select(step => step.Order).Order().SequenceEqual(Enumerable.Range(1, scenario.Steps.Count)))
                    errors.Add($"Scenario '{guide.Id}/{scenario.Id}' step order must be consecutive starting at 1.");

                foreach (var step in scenario.Steps)
                {
                    if (step is null)
                    {
                        errors.Add($"Scenario '{guide.Id}/{scenario.Id}' must not contain null steps.");
                        continue;
                    }
                    if (!entries.TryGetValue(step.ArticleId, out var entry) || entry.CountryId != guide.CountryId)
                    {
                        errors.Add($"Step '{guide.Id}/{scenario.Id}/{step.Id}' refers to a missing article or an article from another country.");
                    }

                    if (step.OfficialSourceIds.Count == 0 || step.OfficialSourceIds.Distinct().Count() != step.OfficialSourceIds.Count)
                        errors.Add($"Step '{guide.Id}/{scenario.Id}/{step.Id}' must refer to unique official sources.");
                    foreach (var sourceId in step.OfficialSourceIds)
                    {
                        if (!sources.TryGetValue(sourceId, out var source) || source.InformationEntryId != step.ArticleId)
                            errors.Add($"Step '{guide.Id}/{scenario.Id}/{step.Id}' refers to an unrelated or missing official source.");
                    }

                    if (step.ClaimIds.Count == 0 || step.ClaimIds.Any(string.IsNullOrWhiteSpace))
                        errors.Add($"Step '{guide.Id}/{scenario.Id}/{step.Id}' must identify its supporting research claims.");

                    ValidateLanguages(step.Translations, $"Step '{guide.Id}/{scenario.Id}/{step.Id}'", errors);
                    foreach (var language in Languages)
                    {
                        if (!step.Translations.TryGetValue(language, out var text) ||
                            string.IsNullOrWhiteSpace(text.Title) || string.IsNullOrWhiteSpace(text.Action) ||
                            string.IsNullOrWhiteSpace(text.Explanation) || string.IsNullOrWhiteSpace(text.Authority) ||
                            text.Documents is null || string.IsNullOrWhiteSpace(text.Warning))
                            errors.Add($"Step '{guide.Id}/{scenario.Id}/{step.Id}' is missing complete '{language}' translation.");
                    }
                }
            }
        }

        if (errors.Count > 0)
            throw new InvalidDataException($"Step-by-step guide catalog is invalid: {string.Join(" ", errors)}");
    }

    private static void ValidateLanguages<T>(IReadOnlyDictionary<string, T>? translations, string owner, ICollection<string> errors)
    {
        if (translations is null || translations.Count != Languages.Length || Languages.Any(language => !translations.ContainsKey(language)))
        {
            errors.Add($"{owner} must contain exactly Ukrainian, English, and Russian translations.");
        }
    }
}
