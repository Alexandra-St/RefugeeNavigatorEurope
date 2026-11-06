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
    public async Task SlovakiaCatalogIsDevelopmentOnlyAndAllTranslationsNeedReview()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "slovakia");
        Assert.Equal(CountryId, country.Id);
        Assert.Equal("Slovakia", country.Name);
        Assert.Equal(9, catalog.Countries.Count);
        Assert.Equal(72, catalog.InformationEntries.Count);
        Assert.Equal(216, catalog.InformationEntryTranslations.Count);
        Assert.Equal(189, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
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
            Assert.All(translations, item =>
            {
                Assert.Equal(VerificationStatus.NeedsReview, item.VerificationStatus);
                Assert.Equal(new DateTimeOffset(2026, 11, 6, 12, 0, 0, TimeSpan.Zero), item.LastUpdatedAt);
                Assert.False(string.IsNullOrWhiteSpace(item.Title));
                Assert.False(string.IsNullOrWhiteSpace(item.Summary));
                Assert.False(string.IsNullOrWhiteSpace(item.Content));
            });
            Assert.Contains(code, Articles.Keys);
        }
        Assert.DoesNotContain(CountryVisibility.GetVisibleCountries(catalog, false), item => item.Slug == "slovakia");
        Assert.Contains(CountryVisibility.GetVisibleCountries(catalog, true), item => item.Slug == "slovakia");
        Assert.Equal(8, CountryVisibility.GetVisibleCountries(catalog, false).Count);
        Assert.Equal(9, CountryVisibility.GetVisibleCountries(catalog, true).Count);
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
        Assert.Equal(601, catalog.OfficialSources.Count);
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
    public async Task SlovakiaHasTwentyFourAvailableDraftFactsAndTopicMappings()
    {
        var catalog = await Catalog.Value;
        CountryComparisonTopicMap.Validate(catalog);
        Assert.Contains("slovakia", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Equal(72, CountryComparisonTopicMap.All.SelectMany(item => item.EntryIds.Values).Distinct().Count());
        var comparison = await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(catalog);
        var facts = comparison.Topics.SelectMany(item => item.Aspects).SelectMany(item => item.Facts).Where(item => item.CountrySlug == "slovakia").ToArray();
        Assert.Equal(24, facts.Length);
        Assert.All(facts, fact =>
        {
            Assert.Equal("Draft", fact.Status.ToString());
            Assert.True(fact.IsAvailable);
            Assert.Contains(Articles.Values.Select(item => item.Id), id => id == fact.InformationEntryId);
            Assert.NotEmpty(fact.OfficialSourceIds);
            Assert.All(fact.OfficialSourceIds, id => Assert.Equal(fact.InformationEntryId, Assert.Single(catalog.OfficialSources, source => source.Id == id).InformationEntryId));
            Assert.All(new[] { "en", "uk", "ru" }, language => Assert.False(string.IsNullOrWhiteSpace(fact.Values[language])));
        });
    }

    private static string Normalize(string value) => value.Replace("\r\n", "\n", StringComparison.Ordinal).Trim();

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !Directory.Exists(Path.Combine(directory.FullName, ".git"))) directory = directory.Parent;
        return directory?.FullName ?? throw new DirectoryNotFoundException("Could not locate repository root.");
    }
}
