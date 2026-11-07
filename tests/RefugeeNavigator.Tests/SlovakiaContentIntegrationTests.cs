using System.Text.RegularExpressions;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Comparison;
using RefugeeNavigator.Web.Content;

namespace RefugeeNavigator.Tests;

public sealed class SlovakiaContentIntegrationTests
{
    private static readonly Guid CountryId = Guid.Parse("10000000-0000-0000-0000-000000000009");
    private static readonly IReadOnlyDictionary<string, (Guid Id, string Category)> Articles = new Dictionary<string, (Guid, string)>
    {
        ["SK-TP-01"] = (Guid.Parse("09d72fd2-1fa7-51a2-886b-9ebd93f4678d"), "temporary-protection"),
        ["SK-TP-02"] = (Guid.Parse("53272a98-60f0-507d-be15-eea36295bdf2"), "temporary-protection"),
        ["SK-EMP-01"] = (Guid.Parse("c26f0cc5-6316-5051-8ea8-8aa53d5d8742"), "employment"),
        ["SK-EMP-02"] = (Guid.Parse("7e043916-46d8-5767-b00e-0ddc38c6c24e"), "employment"),
        ["SK-HEALTH-01"] = (Guid.Parse("88047ea9-f8a9-5e74-9af7-9b8aeb684a24"), "healthcare"),
        ["SK-HOUSING-01"] = (Guid.Parse("47762263-5800-578e-964b-13806a059f7f"), "housing"),
        ["SK-EDU-01"] = (Guid.Parse("c277d784-6c19-5edb-9137-a1289f9edc94"), "education"),
        ["SK-SOC-01"] = (Guid.Parse("3e90b7d3-8cc3-5e78-a375-878d1b71b054"), "social-assistance")
    };
    private static readonly Lazy<Task<CatalogData>> Catalog = new(() => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());

    [Fact]
    public async Task SlovakiaCatalogHasAcceptedTranslationsAndIsVisibleInProductionAndDevelopment()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "slovakia");
        Assert.Equal(CountryId, country.Id);
        Assert.Equal("Slovakia", country.Name);
        Assert.Equal(10, catalog.Countries.Count);
        Assert.Equal(80, catalog.InformationEntries.Count);
        Assert.Equal(240, catalog.InformationEntryTranslations.Count);
        Assert.Equal(213, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(27, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Outdated));
        var entries = catalog.InformationEntries.Where(item => item.CountryId == CountryId).ToArray();
        Assert.Equal(8, entries.Length);
        Assert.Equal(Articles.Values.Select(item => item.Id).Order().ToArray(), entries.Select(item => item.Id).Order().ToArray());
        foreach (var (code, expected) in Articles)
        {
            var entry = Assert.Single(entries, item => item.Id == expected.Id);
            Assert.Equal(expected.Category, catalog.Categories.Single(item => item.Id == entry.CategoryId).Slug);
            var translations = catalog.InformationEntryTranslations.Where(item => item.InformationEntryId == expected.Id).ToArray();
            Assert.Equal(new[] { "en", "ru", "uk" }, translations.Select(item => item.LanguageCode).Order(StringComparer.Ordinal).ToArray());
            Assert.Equal(3, translations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
            Assert.Equal(0, translations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
            Assert.All(translations, item =>
            {
                Assert.Equal(VerificationStatus.Verified, item.VerificationStatus);
                Assert.Equal(new DateTimeOffset(2026, 11, 6, 12, 0, 0, TimeSpan.Zero), item.LastUpdatedAt);
                Assert.False(string.IsNullOrWhiteSpace(item.Title));
                Assert.False(string.IsNullOrWhiteSpace(item.Summary));
                Assert.False(string.IsNullOrWhiteSpace(item.Content));
            });
            Assert.Contains(code, Articles.Keys);
        }
        Assert.Contains(CountryVisibility.GetVisibleCountries(catalog, false), item => item.Slug == "slovakia");
        Assert.Contains(CountryVisibility.GetVisibleCountries(catalog, true), item => item.Slug == "slovakia");
        Assert.Equal(9, CountryVisibility.GetVisibleCountries(catalog, false).Count);
        Assert.Equal(10, CountryVisibility.GetVisibleCountries(catalog, true).Count);
    }

    [Fact]
    public async Task DraftsMatchTranslationsAndSourcesAreOwnedAndCurrent()
    {
        var catalog = await Catalog.Value;
        var root = Path.Combine(FindRepositoryRoot(), "docs", "content-drafts", "slovakia-v1");
        Assert.Equal(8, Directory.GetFiles(root, "SK-*.md").Length);
        var ids = Articles.Values.Select(item => item.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(item => ids.Contains(item.InformationEntryId)).ToArray();
        Assert.Equal(81, sources.Length);
        Assert.Equal(666, catalog.OfficialSources.Count);
        Assert.All(sources, item =>
        {
            Assert.Contains(item.InformationEntryId, ids);
            Assert.Equal(Uri.UriSchemeHttps, item.Url.Scheme);
            Assert.False(string.IsNullOrWhiteSpace(item.Title));
            Assert.Equal(new DateTimeOffset(2026, 11, 6, 0, 0, 0, TimeSpan.Zero), item.LastCheckedAt);
        });
        var sourceById = catalog.OfficialSources.ToDictionary(item => item.Id);
        foreach (var (code, expected) in Articles)
        {
            var draft = await File.ReadAllTextAsync(Path.Combine(root, code + ".md"));
            Assert.Contains($"Article ID: `{expected.Id}`", draft);
            Assert.Contains("## Editorial source set", draft);
            foreach (var (language, section) in new[] { ("en", "EN"), ("uk", "UK"), ("ru", "RU") })
            {
                var match = Regex.Match(draft, $@"(?ms)^## {section}\s+Title: (?<title>.*?)\s+Summary: (?<summary>.*?)\s+Content:\s+(?<content>.*?)(?=^### Claim coverage and editorial exclusions$)");
                Assert.True(match.Success, $"Missing {section} in {code}.");
                var translation = Assert.Single(catalog.InformationEntryTranslations, item => item.InformationEntryId == expected.Id && item.LanguageCode == language);
                Assert.Equal(match.Groups["title"].Value.Trim(), translation.Title);
                Assert.Equal(match.Groups["summary"].Value.Trim(), translation.Summary);
                Assert.Equal(Normalize(match.Groups["content"].Value), Normalize(translation.Content));
                Assert.DoesNotContain("https://", translation.Content, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("Claim coverage", translation.Content, StringComparison.OrdinalIgnoreCase);
                var owned = sources.Where(item => item.InformationEntryId == expected.Id).Select(item => item.Id).ToHashSet();
                var tokens = Regex.Matches(translation.Content, @"\[\[source:([^|\]]+)\|([^\]]+)\]\]");
                Assert.NotEmpty(tokens);
                foreach (Match token in tokens)
                {
                    var sourceId = Guid.Parse(token.Groups[1].Value);
                    Assert.Contains(sourceId, owned);
                    Assert.Equal(expected.Id, sourceById[sourceId].InformationEntryId);
                }
            }
        }
    }

    [Fact]
    public async Task SlovakiaHasTwentyFourAvailableApprovedFactsAndTopicMappings()
    {
        var catalog = await Catalog.Value;
        CountryComparisonTopicMap.Validate(catalog);
        Assert.Contains("slovakia", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Equal(80, CountryComparisonTopicMap.All.SelectMany(item => item.EntryIds.Values).Distinct().Count());
        var comparison = await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(catalog);
        var facts = comparison.Topics.SelectMany(item => item.Aspects).SelectMany(item => item.Facts).Where(item => item.CountrySlug == "slovakia").ToArray();
        Assert.Equal(24, facts.Length);
        Assert.Equal(24, facts.Count(fact => fact.Status.ToString() == "Approved"));
        Assert.Equal(0, facts.Count(fact => fact.Status.ToString() == "Draft"));
        Assert.Equal(24, facts.Count(fact => fact.IsAvailable));
        Assert.All(facts, fact =>
        {
            Assert.Equal("Approved", fact.Status.ToString());
            Assert.True(fact.IsAvailable);
            Assert.Contains(Articles.Values.Select(item => item.Id), id => id == fact.InformationEntryId);
            Assert.NotEmpty(fact.OfficialSourceIds);
            Assert.All(fact.OfficialSourceIds, id => Assert.Equal(fact.InformationEntryId, Assert.Single(catalog.OfficialSources, source => source.Id == id).InformationEntryId));
            Assert.All(new[] { "en", "uk", "ru" }, language => Assert.False(string.IsNullOrWhiteSpace(fact.Values[language])));
        });
    }

    [Fact]
    public async Task SlovakiaEditorialCorrectionsRemainPresentAcrossArticlesAndComparisonFacts()
    {
        var catalog = await Catalog.Value;
        var comparison = await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(catalog);
        var facts = comparison.Topics.SelectMany(topic => topic.Aspects)
            .SelectMany(aspect => aspect.Facts
                .Where(fact => fact.CountrySlug == "slovakia")
                .Select(fact => (aspect.Id, Fact: fact)))
            .ToDictionary(item => item.Id, item => item.Fact, StringComparer.Ordinal);

        Assert.Contains("§131o", Article("SK-TP-02", "en"));
        Assert.Contains("§31(4)", Article("SK-TP-02", "en"));
        Assert.Contains("ordinary residence routes use their applicable", Article("SK-TP-02", "en"));
        Assert.Contains("commencement and termination", Fact("work-employer-duties", "en"));
        Assert.Contains("seven working days", Fact("work-employer-duties", "en"));
        Assert.Contains("other EU Member State", Fact("healthcare-access", "en"));
        Assert.Contains("15 days", Fact("housing-initial", "en"));
        Assert.Contains("3 working days", Fact("housing-financial-support", "en"));
        Assert.Contains("twice", Fact("housing-financial-support", "en"));
        Assert.Contains("14 days", Fact("housing-financial-support", "en"));
        Assert.Contains("5 working days", Fact("housing-financial-support", "en"));
        Assert.Contains("after 31 Aug 2025", Fact("education-school-enrolment", "en"));
        Assert.Contains("€8.592", Fact("work-minimum-pay", "en"));
        Assert.Contains("DOČASNÉ ÚTOČISKO", Fact("healthcare-proof", "en"));
        Assert.Contains("155", Fact("healthcare-urgent", "en"));
        Assert.Contains("112", Fact("healthcare-urgent", "en"));
        Assert.Contains("обычное пособие", Fact("social-child-benefits", "ru"));
        Assert.Contains("пособия по уходу", Fact("social-child-benefits", "ru"));
        Assert.Contains("трансграничная координация", Fact("social-child-benefits", "ru"));
        Assert.DoesNotContain("инвалидност", Fact("social-child-benefits", "ru"), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("гуманитар", Fact("social-child-benefits", "ru"), StringComparison.OrdinalIgnoreCase);

        string Article(string code, string language) => Assert.Single(
            catalog.InformationEntryTranslations,
            item => item.InformationEntryId == Articles[code].Id && item.LanguageCode == language).Content;

        string Fact(string aspect, string language) => facts[aspect].Values[language];
    }

    [Fact]
    public async Task SlovakiaComparisonFactsHaveClaimSpecificOwnedSourcesAndSocialBenefitParity()
    {
        var catalog = await Catalog.Value;
        var comparison = await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(catalog);
        var facts = comparison.Topics.SelectMany(topic => topic.Aspects)
            .SelectMany(aspect => aspect.Facts
                .Where(fact => fact.CountrySlug == "slovakia")
                .Select(fact => (aspect.Id, Fact: fact)))
            .ToDictionary(item => item.Id, item => item.Fact, StringComparer.Ordinal);

        var expectedSourceIds = new Dictionary<string, string[]>(StringComparer.Ordinal)
        {
            ["protection-scheme"] = ["a1509aff-d7fa-5e45-93ee-530d44b2101e", "bf463b9b-1bac-5914-ae1d-64fce762a7dc", "38918d42-3122-587a-b06a-a70fd99520bc"],
            ["protection-application"] = ["fca86339-e32a-5489-907c-b93555272dc5", "80a28bba-0a9a-54fc-b3c8-7325c9b0091c"],
            ["protection-dates"] = ["bf463b9b-1bac-5914-ae1d-64fce762a7dc", "38918d42-3122-587a-b06a-a70fd99520bc", "ebd39fa2-8a33-53e1-8858-5d90b09b6642"],
            ["residence-filing"] = ["764171d4-60e3-52a1-818c-d893f6388d68", "0ad1bc01-1675-5d83-86d5-a410cdcda966", "f37818da-31fc-57c8-9406-379be23970be"],
            ["employment-institutions"] = ["3efe28b7-6ffd-5510-b00f-317f84c1176d", "273c5aad-7526-5161-884b-72d93b43a8bb"],
            ["employment-assistance-conditions"] = ["3efe28b7-6ffd-5510-b00f-317f84c1176d", "3ea86fc1-1e8a-51e6-bc3e-820e241f465b", "a0f81409-3a9d-5d0e-acca-4ae5618f92c0"],
            ["work-minimum-pay"] = ["0ee76a27-1a5b-55c6-94cd-f7ce78a0f0b3", "ec5c2ea1-fd6e-564f-825e-73fd0a5c7429", "6ab4517b-bc98-5890-aef1-ff0bb9c6f227", "ae4b6e26-5c1f-598e-ba51-8f67451bc621"],
            ["healthcare-access"] = ["88261d23-1430-5fda-bd4c-666ba5bc0ac9", "ab4942bd-3d65-56d0-bac6-fa3a247a9a65", "5604f5aa-f855-56a1-a76a-05e0bc267711", "84ac063a-2b96-5676-b97c-ab372f94fd75"],
            ["healthcare-proof"] = ["ab4942bd-3d65-56d0-bac6-fa3a247a9a65", "5604f5aa-f855-56a1-a76a-05e0bc267711", "47be309d-df73-563f-9833-5dbe138239f0", "88261d23-1430-5fda-bd4c-666ba5bc0ac9"],
            ["healthcare-urgent"] = ["88261d23-1430-5fda-bd4c-666ba5bc0ac9", "8b5b3ce8-2067-5ebc-abde-1d66f6ccd0cf", "84ac063a-2b96-5676-b97c-ab372f94fd75", "40dc0568-def5-51d8-9ca3-848ca71ec70e", "2586fa5d-8fc5-5387-a15f-97570efbcd2f", "88dc09a3-92be-5b0e-aaa3-0b7186ee3d9c"],
            ["housing-initial"] = ["de4856af-6e9b-5754-9a0a-6beb8e2e3727"],
            ["housing-private-rental"] = ["9619f980-71d1-507f-888e-7edfa11f4723", "91b1b3e5-bd5d-57b8-bde2-6dcc1d6796b7"],
            ["education-higher-admission"] = ["63e4e7e7-3b0c-59c8-9843-651e2c771549", "571dfa10-4107-5bc8-bd70-9d9ba0feae65", "6333aeb6-ccf9-5c79-a620-e7e9190272be"],
            ["social-application"] = ["986d5dea-6083-549e-8925-25930cd949bf", "d85af0e9-1f84-5056-a9d3-9fe1c52ee579", "20699886-d108-5264-85c6-59406602ab74", "97dc0534-796d-54e0-ac9e-d7c355adcb7b", "13f453fc-27a8-50f8-b35a-39e019e73942"],
            ["social-child-benefits"] = ["479edcef-5ade-5df7-9fe4-53493e06d0e5", "7c461900-1b2c-52fd-83a4-679e15da8ef4", "0fce5bcf-9acd-5ede-bd57-f880b05a1ce4", "65b6de84-ab98-528b-97d4-9e29dcb0101b", "16837ec6-2b25-5643-b854-d3e478623c96", "532001ba-80ea-5b9b-b4e8-c8feed35a661", "11f5ef69-4ca5-5306-b447-7be40502d021"]
        };

        foreach (var (aspectId, sourceIds) in expectedSourceIds)
        {
            var fact = facts[aspectId];
            Assert.Equal(ComparisonFactStatus.Approved, fact.Status);
            Assert.True(fact.IsAvailable);
            Assert.Equal(sourceIds.Select(Guid.Parse).Order().ToArray(), fact.OfficialSourceIds.Order().ToArray());
        }

        Assert.Contains(Guid.Parse("de4856af-6e9b-5754-9a0a-6beb8e2e3727"), facts["housing-initial"].OfficialSourceIds);
        Assert.DoesNotContain(Guid.Parse("6422bbd8-e3d1-51c7-9487-a3680b24a07d"), facts["housing-initial"].OfficialSourceIds);
        Assert.Contains(Guid.Parse("6422bbd8-e3d1-51c7-9487-a3680b24a07d"), facts["housing-financial-support"].OfficialSourceIds);

        var childBenefits = facts["social-child-benefits"];
        Assert.Contains("ordinary child or parental allowance", childBenefits.Values["en"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("childcare allowance", childBenefits.Values["en"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("cross-border", childBenefits.Values["en"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("звичайну допомогу на дитину чи батьківську допомогу", childBenefits.Values["uk"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("окремий шлях спеціального захисту", childBenefits.Values["uk"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("транскордонна координація", childBenefits.Values["uk"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("обычное пособие на ребёнка или родительское пособие", childBenefits.Values["ru"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("отдельный маршрут специальной защиты", childBenefits.Values["ru"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("трансграничная координация", childBenefits.Values["ru"], StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("инвалидност", childBenefits.Values["ru"], StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("гуманитар", childBenefits.Values["ru"], StringComparison.OrdinalIgnoreCase);
    }

    private static string Normalize(string value) => value.Replace("\r\n", "\n", StringComparison.Ordinal).Trim();

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !Directory.Exists(Path.Combine(directory.FullName, ".git"))) directory = directory.Parent;
        return directory?.FullName ?? throw new DirectoryNotFoundException("Could not locate repository root.");
    }
}
