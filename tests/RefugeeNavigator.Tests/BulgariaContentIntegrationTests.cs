using System.Text.RegularExpressions;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Comparison;
using RefugeeNavigator.Web.Content;

namespace RefugeeNavigator.Tests;

public sealed class BulgariaContentIntegrationTests
{
    private static readonly Guid CountryId = Guid.Parse("10000000-0000-0000-0000-000000000010");
    private static readonly IReadOnlyDictionary<string, (Guid Id, string Category)> Articles = new Dictionary<string, (Guid, string)>
    {
        ["BG-TP-01"] = (Guid.Parse("4a1e4c9b-a09c-5c92-bc37-2c9a15b459bc"), "temporary-protection"),
        ["BG-TP-02"] = (Guid.Parse("c276de77-a6b9-5ff7-bbc7-685349b1009a"), "temporary-protection"),
        ["BG-EMP-01"] = (Guid.Parse("d210414c-a502-59ef-b10e-1ae642b50207"), "employment"),
        ["BG-EMP-02"] = (Guid.Parse("26a17913-a1f2-5710-b11b-df406dab09cc"), "employment"),
        ["BG-HEALTH-01"] = (Guid.Parse("47c384e4-2d8f-50bb-af6a-356d49a4c089"), "healthcare"),
        ["BG-HOUSING-01"] = (Guid.Parse("f763854d-dee2-5cb3-aacb-6c01203b52fc"), "housing"),
        ["BG-EDU-01"] = (Guid.Parse("db6f961d-e976-52c1-8ef3-d826ee46b3fc"), "education"),
        ["BG-SOC-01"] = (Guid.Parse("7fede68e-bccb-53f8-9648-984d7ac6fb68"), "social-assistance")
    };
    private static readonly Lazy<Task<CatalogData>> Catalog = new(() => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());

    [Fact]
    public async Task BulgariaCatalogHasEightAcceptedArticlesAndIsVisibleInProductionAndDevelopment()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "bulgaria");
        Assert.Equal(CountryId, country.Id);
        Assert.Equal("Bulgaria", country.Name);
        Assert.Equal(19, catalog.Countries.Count);
        Assert.Equal(147, catalog.InformationEntries.Count);
        Assert.Equal(441, catalog.InformationEntryTranslations.Count);
        Assert.Equal(375, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(66, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
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
                Assert.Equal(new DateTimeOffset(2026, 11, 7, 12, 0, 0, TimeSpan.Zero), item.LastUpdatedAt);
                Assert.False(string.IsNullOrWhiteSpace(item.Title));
                Assert.False(string.IsNullOrWhiteSpace(item.Summary));
                Assert.False(string.IsNullOrWhiteSpace(item.Content));
            });
        }

        var production = CountryVisibility.GetVisibleCountries(catalog, false);
        var development = CountryVisibility.GetVisibleCountries(catalog, true);
        Assert.Contains(production, item => item.Slug == "bulgaria");
        Assert.Contains(development, item => item.Slug == "bulgaria");
        Assert.Equal(16, production.Count);
        Assert.Equal(19, development.Count);
    }

    [Fact]
    public async Task BulgariaDraftsMatchCatalogAndSourcesAreOwnedAndCited()
    {
        var catalog = await Catalog.Value;
        var root = Path.Combine(FindRepositoryRoot(), "docs", "content-drafts", "bulgaria-v1");
        Assert.Equal(8, Directory.GetFiles(root, "BG-*.md").Length);
        var entryIds = Articles.Values.Select(item => item.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(item => entryIds.Contains(item.InformationEntryId)).ToArray();
        Assert.Equal(65, sources.Length);
        Assert.Equal(1400, catalog.OfficialSources.Count);
        Assert.Equal(65, sources.Select(item => item.Id).Distinct().Count());
        var sourceById = catalog.OfficialSources.ToDictionary(item => item.Id);
        Assert.All(sources, item =>
        {
            Assert.Contains(item.InformationEntryId, entryIds);
            Assert.Equal(Uri.UriSchemeHttps, item.Url.Scheme);
            Assert.False(string.IsNullOrWhiteSpace(item.Title));
            Assert.Equal(new DateTimeOffset(2026, 11, 7, 0, 0, 0, TimeSpan.Zero), item.LastCheckedAt);
        });

        foreach (var (code, expected) in Articles)
        {
            var draft = await File.ReadAllTextAsync(Path.Combine(root, code + ".md"));
            Assert.Contains($"Article ID: `{expected.Id}`", draft);
            Assert.Contains("## Editorial source set", draft);
            var editorialIds = Regex.Matches(draft, @"(?m)^- `[^`]+` → `([^`]+)` — (.*)$")
                .Select(match => (Id: Guid.Parse(match.Groups[1].Value), Title: match.Groups[2].Value))
                .ToArray();
            Assert.NotEmpty(editorialIds);
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
                Assert.DoesNotContain("https://", translation.Content, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("Claim coverage", translation.Content, StringComparison.OrdinalIgnoreCase);
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
    public async Task BulgariaHasTwentyFourAvailableApprovedFactsAndCompleteTopicMappings()
    {
        var catalog = await Catalog.Value;
        CountryComparisonTopicMap.Validate(catalog);
        Assert.Contains("bulgaria", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Equal(120, CountryComparisonTopicMap.All.SelectMany(item => item.EntryIds.Values).Distinct().Count());
        Assert.All(CountryComparisonTopicMap.All, topic => Assert.Equal(Articles[topic.CategorySlug switch
        {
            "temporary-protection" when topic.ResourceKey == "Compare.Topic.TemporaryProtectionApplication" => "BG-TP-01",
            "temporary-protection" => "BG-TP-02",
            "employment" when topic.ResourceKey == "Compare.Topic.FindingEmployment" => "BG-EMP-01",
            "employment" => "BG-EMP-02",
            "healthcare" => "BG-HEALTH-01",
            "housing" => "BG-HOUSING-01",
            "education" => "BG-EDU-01",
            "social-assistance" => "BG-SOC-01",
            _ => throw new InvalidOperationException(topic.CategorySlug)
        }].Id, CountryComparisonTopicMap.EntryIdFor(topic, "bulgaria")));

        var comparison = await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(catalog);
        var allFacts = comparison.Topics.SelectMany(item => item.Aspects).SelectMany(item => item.Facts).ToArray();
        var facts = allFacts.Where(item => item.CountrySlug == "bulgaria").ToArray();
        Assert.Equal(24, facts.Length);
        Assert.Equal(24, facts.Count(item => item.Status == ComparisonFactStatus.Approved));
        Assert.Equal(0, facts.Count(item => item.Status == ComparisonFactStatus.Draft));
        Assert.Equal(24, facts.Count(item => item.IsAvailable));
        Assert.Equal(8, facts.Select(item => item.InformationEntryId).Distinct().Count());

        var netherlandsId = catalog.Countries.Single(item => item.Slug == "netherlands").Id;
        var netherlandsEntryIds = catalog.InformationEntries.Where(item => item.CountryId == netherlandsId)
            .Select(item => item.Id).ToHashSet();
        Assert.Equal(3, catalog.InformationEntryTranslations.Count(item => netherlandsEntryIds.Contains(item.InformationEntryId)
            && item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(4, allFacts.Count(item => item.CountrySlug == "netherlands" && item.Status == ComparisonFactStatus.Draft));
        var socialChildBenefits = Assert.Single(comparison.Topics.SelectMany(item => item.Aspects), item => item.Id == "social-child-benefits");
        var swedenChildBenefits = Assert.Single(socialChildBenefits.Facts, item => item.CountrySlug == "sweden");
        Assert.Equal(ComparisonFactStatus.Draft, swedenChildBenefits.Status);
        Assert.False(swedenChildBenefits.IsAvailable);
        Assert.Empty(swedenChildBenefits.OfficialSourceIds);

        var sourcesById = catalog.OfficialSources.ToDictionary(item => item.Id);
        Assert.All(facts, fact =>
        {
            Assert.Contains(Articles.Values.Select(item => item.Id), id => id == fact.InformationEntryId);
            Assert.NotEmpty(fact.OfficialSourceIds);
            Assert.All(fact.OfficialSourceIds, sourceId => Assert.Equal(fact.InformationEntryId, sourcesById[sourceId].InformationEntryId));
            Assert.All(new[] { "en", "uk", "ru" }, language => Assert.False(string.IsNullOrWhiteSpace(fact.Values[language])));
        });
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
