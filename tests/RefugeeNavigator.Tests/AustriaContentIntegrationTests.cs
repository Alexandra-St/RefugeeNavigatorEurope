using System.Text.RegularExpressions;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Comparison;
using RefugeeNavigator.Web.Content;

namespace RefugeeNavigator.Tests;

public sealed class AustriaContentIntegrationTests
{
    private static readonly Guid CountryId = Guid.Parse("10000000-0000-0000-0000-000000000011");
    private static readonly IReadOnlyDictionary<string, (Guid Id, string Category)> Articles = new Dictionary<string, (Guid, string)>
    {
        ["AT-TP-01"] = (Guid.Parse("cae7f1a1-51c1-5b6c-a01d-b5bea8fb9cd2"), "temporary-protection"),
        ["AT-TP-02"] = (Guid.Parse("dc76be21-d9dc-5070-83ca-9a7468d78e71"), "temporary-protection"),
        ["AT-EMP-01"] = (Guid.Parse("7f03cea4-6c2e-5990-91eb-01b4933ba53d"), "employment"),
        ["AT-EMP-02"] = (Guid.Parse("a1ac34e6-0c0e-5bb2-870c-9d93aee852c2"), "employment"),
        ["AT-HEALTH-01"] = (Guid.Parse("44e8aebe-fd7e-5ed8-acb2-f949d6243e26"), "healthcare"),
        ["AT-HOUSING-01"] = (Guid.Parse("b7ae3606-2d3b-5ddc-b178-f1905d72996e"), "housing"),
        ["AT-EDU-01"] = (Guid.Parse("28c9cf8a-012f-5018-b9e0-48c8fcc4f8d5"), "education"),
        ["AT-SOC-01"] = (Guid.Parse("fc9332d0-9eda-5f09-b3ca-df3c9e63f755"), "social-assistance")
    };

    private static readonly string[] AspectIds =
    [
        "protection-scheme", "protection-application", "protection-dates",
        "residence-routes", "residence-conditions", "residence-filing",
        "employment-vacancies", "employment-institutions", "employment-assistance-conditions",
        "work-permission", "work-employer-duties", "work-minimum-pay",
        "healthcare-access", "healthcare-proof", "healthcare-urgent",
        "housing-initial", "housing-private-rental", "housing-financial-support",
        "education-school-enrolment", "education-pupil-language", "education-higher-admission",
        "social-subsistence", "social-application", "social-child-benefits"
    ];

    private static readonly Lazy<Task<CatalogData>> Catalog = new(() => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());
    private static readonly Lazy<Task<ComparisonFactCatalog>> Facts = new(async () =>
        await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(await Catalog.Value));

    [Fact]
    public async Task AustriaHasEightVerifiedArticlesAndIsVisibleInProductionAndDevelopment()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "austria");
        Assert.Equal(CountryId, country.Id);
        Assert.Equal("Austria", country.Name);
        Assert.Equal(18, catalog.Countries.Count);
        Assert.Equal(139, catalog.InformationEntries.Count);
        Assert.Equal(417, catalog.InformationEntryTranslations.Count);
        Assert.Equal(333, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(84, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Outdated));

        var entries = catalog.InformationEntries.Where(item => item.CountryId == CountryId).ToArray();
        Assert.Equal(8, entries.Length);
        Assert.Equal(Articles.Values.Select(item => item.Id).Order().ToArray(), entries.Select(item => item.Id).Order().ToArray());
        Assert.Equal(8, Articles.Values.Select(item => item.Id).Distinct().Count());
        foreach (var expected in Articles.Values)
        {
            var entry = Assert.Single(entries, item => item.Id == expected.Id);
            Assert.Equal(expected.Category, catalog.Categories.Single(item => item.Id == entry.CategoryId).Slug);
            var translations = catalog.InformationEntryTranslations.Where(item => item.InformationEntryId == expected.Id).ToArray();
            Assert.Equal(new[] { "en", "ru", "uk" }, translations.Select(item => item.LanguageCode).Order(StringComparer.Ordinal).ToArray());
            Assert.All(translations, item =>
            {
                Assert.Equal(VerificationStatus.Verified, item.VerificationStatus);
                Assert.Equal(new DateTimeOffset(2026, 11, 8, 0, 0, 0, TimeSpan.Zero), item.LastUpdatedAt);
            });
        }

        var production = CountryVisibility.GetVisibleCountries(catalog, false);
        var development = CountryVisibility.GetVisibleCountries(catalog, true);
        Assert.Contains(production, item => item.Slug == "austria");
        Assert.Contains(development, item => item.Slug == "austria");
        Assert.Equal(14, production.Count);
        Assert.Equal(18, development.Count);
    }

    [Fact]
    public async Task AustriaDraftsMatchCatalogAndSourcesAreArticleOwnedAndCited()
    {
        var catalog = await Catalog.Value;
        var root = Path.Combine(FindRepositoryRoot(), "docs", "content-drafts", "austria-v1");
        Assert.Equal(8, Directory.GetFiles(root, "AT-*.md").Length);
        var entryIds = Articles.Values.Select(item => item.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(item => entryIds.Contains(item.InformationEntryId)).ToArray();
        Assert.Equal(57, sources.Length);
        Assert.Equal(1365, catalog.OfficialSources.Count);
        Assert.Equal(57, sources.Select(item => item.Id).Distinct().Count());
        var sourceById = catalog.OfficialSources.ToDictionary(item => item.Id);
        Assert.All(sources, item =>
        {
            Assert.Contains(item.InformationEntryId, entryIds);
            Assert.Equal(Uri.UriSchemeHttps, item.Url.Scheme);
            Assert.False(string.IsNullOrWhiteSpace(item.Title));
            Assert.Equal(new DateTimeOffset(2026, 11, 7, 0, 0, 0, TimeSpan.Zero), item.LastCheckedAt);
        });
        Assert.All(sources.GroupBy(item => item.Url.AbsoluteUri).Where(group => group.Select(item => item.InformationEntryId).Distinct().Count() > 1), group =>
            Assert.Equal(group.Count(), group.Select(item => item.Id).Distinct().Count()));
        Assert.All(sources.GroupBy(item => (item.InformationEntryId, item.Url.AbsoluteUri)), group => Assert.Single(group));

        foreach (var (code, expected) in Articles)
        {
            var draft = await File.ReadAllTextAsync(Path.Combine(root, code + ".md"));
            Assert.Contains($"Article ID: `{expected.Id}`", draft);
            var editorialIds = Regex.Matches(draft, @"(?m)^- `[^`]+` → `([^`]+)` — (.*)$")
                .Select(match => (Id: Guid.Parse(match.Groups[1].Value), Title: match.Groups[2].Value))
                .ToArray();
            Assert.NotEmpty(editorialIds);
            Assert.Equal(editorialIds.Length, editorialIds.Select(item => item.Id).Distinct().Count());
            var owned = sources.Where(item => item.InformationEntryId == expected.Id).ToDictionary(item => item.Id);
            Assert.Equal(editorialIds.Length, owned.Count);
            foreach (var (sourceId, title) in editorialIds)
            {
                Assert.True(owned.TryGetValue(sourceId, out var source));
                Assert.Equal(title, source!.Title);
            }

            foreach (var (language, section) in new[] { ("en", "EN"), ("uk", "UK"), ("ru", "RU") })
            {
                var match = Regex.Match(draft, $@"(?ms)^## {section}\s+Title: (?<title>.*?)\s+Summary: (?<summary>.*?)\s+Content:\s+(?<content>.*?)(?=^### Claim coverage and editorial exclusions$)");
                Assert.True(match.Success, $"Missing {section} in {code}.");
                var translation = Assert.Single(catalog.InformationEntryTranslations, item => item.InformationEntryId == expected.Id && item.LanguageCode == language);
                Assert.Equal(match.Groups["title"].Value.Trim(), translation.Title);
                Assert.Equal(match.Groups["summary"].Value.Trim(), translation.Summary);
                Assert.Equal(Normalize(match.Groups["content"].Value), Normalize(translation.Content));
                var tokens = Regex.Matches(translation.Content, @"\[\[source:([^|\]]+)\|([^\]]+)\]\]");
                Assert.NotEmpty(tokens);
                foreach (Match token in tokens)
                {
                    var sourceId = Guid.Parse(token.Groups[1].Value);
                    Assert.Contains(sourceId, owned.Keys);
                    Assert.Equal(expected.Id, sourceById[sourceId].InformationEntryId);
                }
            }
        }
    }

    [Fact]
    public async Task AustriaHasTwentyFourAvailableApprovedFactsWithCompleteMappingsAndPreservedHolds()
    {
        var catalog = await Catalog.Value;
        var comparison = await Facts.Value;
        CountryComparisonTopicMap.Validate(catalog);
        Assert.Contains("austria", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Equal(104, CountryComparisonTopicMap.All.SelectMany(item => item.EntryIds.Values).Distinct().Count());
        Assert.All(CountryComparisonTopicMap.All, topic => Assert.Equal(Articles[topic.CategorySlug switch
        {
            "temporary-protection" when topic.ResourceKey == "Compare.Topic.TemporaryProtectionApplication" => "AT-TP-01",
            "temporary-protection" => "AT-TP-02",
            "employment" when topic.ResourceKey == "Compare.Topic.FindingEmployment" => "AT-EMP-01",
            "employment" => "AT-EMP-02",
            "healthcare" => "AT-HEALTH-01",
            "housing" => "AT-HOUSING-01",
            "education" => "AT-EDU-01",
            "social-assistance" => "AT-SOC-01",
            _ => throw new InvalidOperationException(topic.CategorySlug)
        }].Id, CountryComparisonTopicMap.EntryIdFor(topic, "austria")));

        var allFacts = comparison.Topics.SelectMany(item => item.Aspects).SelectMany(item => item.Facts).ToArray();
        var facts = allFacts.Where(item => item.CountrySlug == "austria").ToArray();
        Assert.Equal(24, facts.Length);
        Assert.Equal(AspectIds.Order(StringComparer.Ordinal), comparison.Topics.SelectMany(item => item.Aspects)
            .Where(aspect => aspect.Facts.Any(fact => fact.CountrySlug == "austria")).Select(aspect => aspect.Id).Order(StringComparer.Ordinal));
        Assert.Equal(24, facts.Count(item => item.Status == ComparisonFactStatus.Approved));
        Assert.All(facts, item => Assert.True(item.IsAvailable));
        Assert.Equal(8, facts.Select(item => item.InformationEntryId).Distinct().Count());
        var sourcesById = catalog.OfficialSources.ToDictionary(item => item.Id);
        Assert.All(facts, fact =>
        {
            Assert.Contains(Articles.Values.Select(item => item.Id), id => id == fact.InformationEntryId);
            Assert.NotEmpty(fact.OfficialSourceIds);
            Assert.Equal(fact.OfficialSourceIds.Count, fact.OfficialSourceIds.Distinct().Count());
            Assert.All(fact.OfficialSourceIds, sourceId =>
            {
                Assert.True(sourcesById.TryGetValue(sourceId, out var source));
                Assert.Equal(fact.InformationEntryId, source!.InformationEntryId);
            });
            Assert.All(new[] { "en", "uk", "ru" }, language => Assert.False(string.IsNullOrWhiteSpace(fact.Values[language])));
        });

        var netherlandsId = catalog.Countries.Single(item => item.Slug == "netherlands").Id;
        var netherlandsEntries = catalog.InformationEntries.Where(item => item.CountryId == netherlandsId).Select(item => item.Id).ToHashSet();
        Assert.Equal(3, catalog.InformationEntryTranslations.Count(item => netherlandsEntries.Contains(item.InformationEntryId)
            && item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(4, allFacts.Count(item => item.CountrySlug == "netherlands" && item.Status == ComparisonFactStatus.Draft));
        var childBenefits = Assert.Single(comparison.Topics.SelectMany(item => item.Aspects), item => item.Id == "social-child-benefits");
        var swedenHold = Assert.Single(childBenefits.Facts, item => item.CountrySlug == "sweden");
        Assert.Equal(ComparisonFactStatus.Draft, swedenHold.Status);
        Assert.False(swedenHold.IsAvailable);
        Assert.Empty(swedenHold.OfficialSourceIds);
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !Directory.Exists(Path.Combine(directory.FullName, ".git")))
            directory = directory.Parent;
        return directory?.FullName ?? throw new DirectoryNotFoundException("Could not locate repository root.");
    }

    private static string Normalize(string value) => Regex.Replace(value.Trim(), @"\s+", " ");
}
