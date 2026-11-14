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
            "afc14824-4035-4ca7-9662-6d5352ba63cb", "1a6b97c4-eb9e-5590-8715-c95f5f95677e", "218207bd-2a85-59e1-a945-eedb0604392e", "49d3c355-af96-54f7-92d5-68062647d464", "b278c1d6-1add-4e65-8af1-e16431eaffb3", "d2ab273d-21b7-44e4-b408-f4269a8753d8", "244b1055-4d8c-55b2-820e-60ff018c3b1f", "24d0ab3d-935c-50b7-8385-c1c54583ec51", "09d72fd2-1fa7-51a2-886b-9ebd93f4678d", "4a1e4c9b-a09c-5c92-bc37-2c9a15b459bc", "cae7f1a1-51c1-5b6c-a01d-b5bea8fb9cd2", "c8c06066-d3cd-55ce-88b0-3499384eece7", "115cbf25-5ece-5cc9-8bde-c3dabcbda47f", "1bb2a2b8-cfe0-5595-8155-138443fade17", "411f9a13-a59d-53e6-a25c-148d63aa3818"),
        Topic("Compare.Topic.ResidenceAfterProtection", "temporary-protection",
            "2d106fd3-e981-4ef9-9733-568b654dfca5", "33106e71-115f-5809-9066-7e3d3e5c57a3", "c230d45f-9c76-5912-8c97-dc7d0689baa6", "2d35d95d-3524-5f3c-b7bd-ff61bd67ccd5", "06ce2f10-d309-45b2-aced-e9dd8367ffed", "befa7777-af70-4269-883c-8918961554dc", "c5f5f833-0ab7-5e1a-9325-87a92b21d2ad", "6b966a1f-335e-596a-9825-005d30ab23d4", "53272a98-60f0-507d-be15-eea36295bdf2", "c276de77-a6b9-5ff7-bbc7-685349b1009a", "dc76be21-d9dc-5070-83ca-9a7468d78e71", "28aee635-27ff-525c-9cd3-8673b5ba2be3", "581150e2-3862-5a79-9e56-ad1f3fdd8424", "914418c6-15f8-527c-85dd-be5bda5594fc", "3030161d-7da6-5e97-9a68-9a410aedecf2"),
        Topic("Compare.Topic.FindingEmployment", "employment",
            "c2356631-3fa2-4553-8fda-50a4419262c0", "283d4519-21f5-5be2-a534-1bd2606d95fc", "a323701f-367e-52bb-9c33-45c4156a1350", "e8a5e6d5-a989-51de-ae14-b58e724604b8", "a5107970-958a-4be6-9c98-ae484fc24a15", "a218013b-3a77-4ba2-829a-d724689837f2", "757c1db6-b66b-54b9-ac14-d8a3da958ef5", "54197f49-594b-51d2-a40c-2fcded05f791", "c26f0cc5-6316-5051-8ea8-8aa53d5d8742", "d210414c-a502-59ef-b10e-1ae642b50207", "7f03cea4-6c2e-5990-91eb-01b4933ba53d", "39091eb7-04c7-5b45-83b7-585f5f1ce719", "26051216-75b3-5564-a2ec-9a0723bcfec6", "0e65e68f-5c42-58d5-b7b3-e941896bb532", "027596b8-606a-58df-aae0-63a040f69064"),
        Topic("Compare.Topic.WorkAuthorizationRights", "employment",
            "35da68f6-8880-40b2-9b3f-eb2b809b1caf", "c4895b5a-eacd-53d7-a86f-f504dd615c76", "7797d03c-6873-50cd-aef6-0d94a2f5fcab", "829b9986-d949-5dd8-80c4-b9b4b7cf4d44", "b76b631c-92de-4379-8136-c9e4e08e2fd2", "55949af5-b4fe-404e-bcb7-6ebb37131bab", "6a077f79-89c2-55d6-81f3-9ba7279979c9", "61d13a69-6704-50ee-8b7b-e673a2828e65", "7e043916-46d8-5767-b00e-0ddc38c6c24e", "26a17913-a1f2-5710-b11b-df406dab09cc", "a1ac34e6-0c0e-5bb2-870c-9d93aee852c2", "d988941a-e709-53ba-a2bf-47f1ac2f9956", "5cdd5634-6d7a-5c02-aa2a-02f4758e6d2d", "4dde28d5-8f4c-5503-926b-eac8789b4e9f", "8fd8c9c0-1b79-5e47-bf21-ce3f84dbe4b7"),
        Topic("Compare.Topic.Healthcare", "healthcare",
            "b1e4aed1-bbaf-4f09-9468-74000bf96430", "895b79b4-ce0d-5bee-9919-42ce3d76ecdb", "d7e0716b-154d-5db3-8790-f73684c07271", "d252adbc-48c0-5546-bbe1-2b08e2293f27", "50829c0b-7ee0-4ee4-be7b-47dfd0dc408d", "2234b91f-4021-40b2-b9d5-a029a6b533f2", "d5c4d5fa-3b0f-5cc2-865f-841bd40911d1", "be12596b-8657-5b23-a17c-dead174859f3", "88047ea9-f8a9-5e74-9af7-9b8aeb684a24", "47c384e4-2d8f-50bb-af6a-356d49a4c089", "44e8aebe-fd7e-5ed8-acb2-f949d6243e26", "462ace7d-8deb-5cd7-a530-548c79a8e144", "42863927-2979-5769-8092-c9393a0ea916", "8cb405ab-c562-5c22-81ad-0fd93ef50202", "0073bdfa-5d2e-5fc6-a63f-35763fd61032"),
        Topic("Compare.Topic.Housing", "housing",
            "19afdd19-e2cc-4da6-bde3-6dc3d64b7503", "fd041e53-7794-5c1f-9ae4-131930fe69ee", "1f310b3c-dceb-5e3a-90d0-d3a705ca5c53", "957ae315-47a4-5a73-806e-ca1d6cf258d1", "bd08644f-dd27-412c-9bfd-888de1fe304d", "e0f307e9-7991-4350-89f4-2f84a9047393", "19dde97c-cb42-5a90-97f7-e344f2139d2e", "35bb6fe6-b0b9-5517-af04-e022c7105ec7", "47762263-5800-578e-964b-13806a059f7f", "f763854d-dee2-5cb3-aacb-6c01203b52fc", "b7ae3606-2d3b-5ddc-b178-f1905d72996e", "5aafddcb-e302-52b7-9365-f95e7261d3a4", "28169e82-6320-5f00-9b34-37e77f3c7d26", "bbdf7f5c-da1d-59ad-9515-5bd8499e2332", "df086f92-efd1-50e6-b2dd-f2af63e00963"),
        Topic("Compare.Topic.EducationLanguage", "education",
            "fee9dcf0-447d-4402-92a2-8b1bec125944", "f5e0f499-0f60-5465-85f8-de6345029b59", "32ca34e9-22d9-5f59-88a9-bbd7ffd7b5cd", "954761f7-bc2b-5725-aeba-c8f1d2192588", "084c06c7-592b-4eed-bb20-6cd1913f14a2", "cac58c0c-ce69-41ac-877e-4fd124105b89", "7633a085-4555-5a8f-a9a8-747a0f6e0545", "bfd554ff-a41a-5338-ab3e-25bb3c827a73", "c277d784-6c19-5edb-9137-a1289f9edc94", "db6f961d-e976-52c1-8ef3-d826ee46b3fc", "28c9cf8a-012f-5018-b9e0-48c8fcc4f8d5", "ba3d3110-730d-5340-a89a-2b6722430bdd", "bacfcf91-7365-5c28-a240-b6db41c1aa0f", "e0fd251b-cf52-5a45-9a8d-b07e91580985", "e77330ad-938f-5d9e-bdf7-3318ee0a7b96"),
        Topic("Compare.Topic.SocialAssistance", "social-assistance",
            "1a19c1f9-1111-4d60-8490-da548f33083f", "31f5802b-26a0-5cd0-bb24-0a9a4afa706a", "a5892591-d082-57ce-acf9-5c39ab4f9b84", "f408e721-31a3-572a-a702-69ab600023a3", "d33628da-cb49-44b5-a7aa-ae6c6ac8f389", "8b5cc53a-9933-4366-8421-d795966267ec", "8d466e7d-93f7-596e-a44f-188e04396aa0", "97f02ab3-3352-597b-ad48-11ebf19993c2", "3e90b7d3-8cc3-5e78-a375-878d1b71b054", "7fede68e-bccb-53f8-9648-984d7ac6fb68", "fc9332d0-9eda-5f09-b3ca-df3c9e63f755", "55696570-3337-5e2f-8a24-71e256e869e9", "c971e527-d7ca-5dff-b739-02e6df1f98e1", "f34f7c15-d879-5ba8-81cf-65f7e9ca416f", "8bc5e2c5-0a6c-59f8-b37b-3b75300fe817"),
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
        string spainId,
        string swedenId,
        string netherlandsId,
        string romaniaId,
        string slovakiaId,
        string bulgariaId,
        string austriaId,
        string italyId,
        string lithuaniaId,
        string hungaryId,
        string norwayId) =>
        new(resourceKey, categorySlug, new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase)
        {
            ["denmark"] = Guid.Parse(denmarkId),
            ["germany"] = Guid.Parse(germanyId),
            ["poland"] = Guid.Parse(polandId),
            ["czechia"] = Guid.Parse(czechiaId),
            ["spain"] = Guid.Parse(spainId),
            ["sweden"] = Guid.Parse(swedenId),
            ["netherlands"] = Guid.Parse(netherlandsId),
            ["romania"] = Guid.Parse(romaniaId),
            ["slovakia"] = Guid.Parse(slovakiaId),
            ["bulgaria"] = Guid.Parse(bulgariaId),
            ["austria"] = Guid.Parse(austriaId),
            ["italy"] = Guid.Parse(italyId),
            ["lithuania"] = Guid.Parse(lithuaniaId),
            ["hungary"] = Guid.Parse(hungaryId),
            ["norway"] = Guid.Parse(norwayId)
        });
}
