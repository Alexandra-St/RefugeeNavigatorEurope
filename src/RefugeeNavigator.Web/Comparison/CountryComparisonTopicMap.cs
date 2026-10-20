using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Web.Comparison;

public sealed record CountryComparisonTopic(
    string ResourceKey,
    string CategorySlug,
    IReadOnlyDictionary<string, Guid> EntryIds);

public static class CountryComparisonTopicMap
{
    private static readonly IReadOnlyList<CountryComparisonTopic> Topics =
    [
        Topic("Compare.Topic.TemporaryProtectionApplication", "temporary-protection",
            "afc14824-4035-4ca7-9662-6d5352ba63cb", "1a6b97c4-eb9e-5590-8715-c95f5f95677e", "218207bd-2a85-59e1-a945-eedb0604392e", "49d3c355-af96-54f7-92d5-68062647d464", "b278c1d6-1add-4e65-8af1-e16431eaffb3"),
        Topic("Compare.Topic.ResidenceAfterProtection", "temporary-protection",
            "2d106fd3-e981-4ef9-9733-568b654dfca5", "33106e71-115f-5809-9066-7e3d3e5c57a3", "c230d45f-9c76-5912-8c97-dc7d0689baa6", "2d35d95d-3524-5f3c-b7bd-ff61bd67ccd5", "06ce2f10-d309-45b2-aced-e9dd8367ffed"),
        Topic("Compare.Topic.FindingEmployment", "employment",
            "c2356631-3fa2-4553-8fda-50a4419262c0", "283d4519-21f5-5be2-a534-1bd2606d95fc", "a323701f-367e-52bb-9c33-45c4156a1350", "e8a5e6d5-a989-51de-ae14-b58e724604b8", "a5107970-958a-4be6-9c98-ae484fc24a15"),
        Topic("Compare.Topic.WorkAuthorizationRights", "employment",
            "35da68f6-8880-40b2-9b3f-eb2b809b1caf", "c4895b5a-eacd-53d7-a86f-f504dd615c76", "7797d03c-6873-50cd-aef6-0d94a2f5fcab", "829b9986-d949-5dd8-80c4-b9b4b7cf4d44", "b76b631c-92de-4379-8136-c9e4e08e2fd2"),
        Topic("Compare.Topic.Healthcare", "healthcare",
            "b1e4aed1-bbaf-4f09-9468-74000bf96430", "895b79b4-ce0d-5bee-9919-42ce3d76ecdb", "d7e0716b-154d-5db3-8790-f73684c07271", "d252adbc-48c0-5546-bbe1-2b08e2293f27", "50829c0b-7ee0-4ee4-be7b-47dfd0dc408d"),
        Topic("Compare.Topic.Housing", "housing",
            "19afdd19-e2cc-4da6-bde3-6dc3d64b7503", "fd041e53-7794-5c1f-9ae4-131930fe69ee", "1f310b3c-dceb-5e3a-90d0-d3a705ca5c53", "957ae315-47a4-5a73-806e-ca1d6cf258d1", "bd08644f-dd27-412c-9bfd-888de1fe304d"),
        Topic("Compare.Topic.EducationLanguage", "education",
            "fee9dcf0-447d-4402-92a2-8b1bec125944", "f5e0f499-0f60-5465-85f8-de6345029b59", "32ca34e9-22d9-5f59-88a9-bbd7ffd7b5cd", "954761f7-bc2b-5725-aeba-c8f1d2192588", "084c06c7-592b-4eed-bb20-6cd1913f14a2"),
        Topic("Compare.Topic.SocialAssistance", "social-assistance",
            "1a19c1f9-1111-4d60-8490-da548f33083f", "31f5802b-26a0-5cd0-bb24-0a9a4afa706a", "a5892591-d082-57ce-acf9-5c39ab4f9b84", "f408e721-31a3-572a-a702-69ab600023a3", "d33628da-cb49-44b5-a7aa-ae6c6ac8f389")
    ];

    private static readonly IReadOnlySet<string> MappedCountrySlugs =
        new HashSet<string>(Topics[0].EntryIds.Keys, StringComparer.OrdinalIgnoreCase);

    public static IReadOnlyList<CountryComparisonTopic> All => Topics;

    public static IReadOnlySet<string> SupportedCountrySlugs => MappedCountrySlugs;

    public static IReadOnlyList<CountryComparisonTopic> ForCategory(string categorySlug) =>
        Topics.Where(topic => string.Equals(topic.CategorySlug, categorySlug, StringComparison.OrdinalIgnoreCase)).ToArray();

    public static Guid EntryIdFor(CountryComparisonTopic topic, string countrySlug) =>
        topic.EntryIds.TryGetValue(countrySlug, out var entryId)
            ? entryId
            : throw new InvalidOperationException($"No comparison entry is mapped for '{countrySlug}' and '{topic.ResourceKey}'.");

    public static void Validate(CatalogData catalog)
    {
        ArgumentNullException.ThrowIfNull(catalog);

        var countries = catalog.Countries.ToDictionary(country => country.Slug, StringComparer.OrdinalIgnoreCase);
        var categories = catalog.Categories.ToDictionary(category => category.Slug, StringComparer.OrdinalIgnoreCase);
        var entries = catalog.InformationEntries.ToDictionary(entry => entry.Id);

        foreach (var topic in Topics)
        {
            if (!categories.TryGetValue(topic.CategorySlug, out var category))
            {
                throw new InvalidOperationException($"Comparison topic '{topic.ResourceKey}' refers to unknown category '{topic.CategorySlug}'.");
            }

            if (topic.EntryIds.Count != MappedCountrySlugs.Count)
            {
                throw new InvalidOperationException($"Comparison topic '{topic.ResourceKey}' must map exactly one article for each supported country.");
            }

            if (!MappedCountrySlugs.SetEquals(topic.EntryIds.Keys))
            {
                throw new InvalidOperationException($"Comparison topic '{topic.ResourceKey}' does not map the same supported countries as the other topics.");
            }

            foreach (var (countrySlug, entryId) in topic.EntryIds)
            {
                if (!countries.TryGetValue(countrySlug, out var country))
                {
                    throw new InvalidOperationException($"Comparison topic '{topic.ResourceKey}' refers to unknown country '{countrySlug}'.");
                }

                if (!entries.TryGetValue(entryId, out var entry))
                {
                    throw new InvalidOperationException($"Comparison topic '{topic.ResourceKey}' refers to missing article '{entryId}'.");
                }

                if (entry.CountryId != country.Id || entry.CategoryId != category.Id)
                {
                    throw new InvalidOperationException(
                        $"Article '{entryId}' mapped to '{countrySlug}' / '{topic.CategorySlug}' has a mismatched country or category relationship.");
                }
            }
        }
    }

    private static CountryComparisonTopic Topic(
        string resourceKey,
        string categorySlug,
        string denmarkId,
        string germanyId,
        string polandId,
        string czechiaId,
        string spainId) =>
        new(resourceKey, categorySlug, new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase)
        {
            ["denmark"] = Guid.Parse(denmarkId),
            ["germany"] = Guid.Parse(germanyId),
            ["poland"] = Guid.Parse(polandId),
            ["czechia"] = Guid.Parse(czechiaId),
            ["spain"] = Guid.Parse(spainId)
        });
}
