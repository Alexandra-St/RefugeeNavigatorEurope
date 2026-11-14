using System.Text.RegularExpressions;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Comparison;
using RefugeeNavigator.Web.Content;

namespace RefugeeNavigator.Tests;

public sealed class NorwayContentIntegrationTests
{
    private const string CountryId = "10000000-0000-0000-0000-000000000019";
    private static readonly IReadOnlyDictionary<string, (string Id, string Category, int Sources)> Articles =
        new Dictionary<string, (string, string, int)>
        {
            ["NO-TP-01"] = ("411f9a13-a59d-53e6-a25c-148d63aa3818", "temporary-protection", 5),
            ["NO-TP-02"] = ("3030161d-7da6-5e97-9a68-9a410aedecf2", "temporary-protection", 3),
            ["NO-EMP-01"] = ("027596b8-606a-58df-aae0-63a040f69064", "employment", 3),
            ["NO-EMP-02"] = ("8fd8c9c0-1b79-5e47-bf21-ce3f84dbe4b7", "employment", 4),
            ["NO-HEALTH-01"] = ("0073bdfa-5d2e-5fc6-a63f-35763fd61032", "healthcare", 5),
            ["NO-HOUSING-01"] = ("df086f92-efd1-50e6-b2dd-f2af63e00963", "housing", 6),
            ["NO-EDU-01"] = ("e77330ad-938f-5d9e-bdf7-3318ee0a7b96", "education", 4),
            ["NO-SOC-01"] = ("8bc5e2c5-0a6c-59f8-b37b-3b75300fe817", "social-assistance", 4)
        };

    [Fact]
    public async Task NorwayCoreIsLocalizedVerifiedVisibleAndSourceOwned()
    {
        var catalog = await new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();
        var country = Assert.Single(catalog.Countries, item => item.Slug == "norway");
        Assert.Equal(Guid.Parse(CountryId), country.Id);
        Assert.Equal(19, catalog.Countries.Count);
        Assert.Equal(8, catalog.InformationEntries.Count(item => item.CountryId == country.Id));
        Assert.Contains(CountryVisibility.GetVisibleCountries(catalog, false), item => item.Id == country.Id);
        Assert.Contains(CountryVisibility.GetVisibleCountries(catalog, true), item => item.Id == country.Id);
        Assert.Contains("norway", CountryComparisonTopicMap.SupportedCountrySlugs);

        var root = FindRepositoryRoot();
        var entries = catalog.InformationEntries.Where(item => item.CountryId == country.Id).ToArray();
        var translations = catalog.InformationEntryTranslations.Where(item => entries.Any(entry => entry.Id == item.InformationEntryId)).ToArray();
        var sources = catalog.OfficialSources.Where(item => entries.Any(entry => entry.Id == item.InformationEntryId)).ToArray();
        Assert.Equal(24, translations.Length);
        Assert.Equal(34, sources.Length);
        Assert.Equal(34, sources.Select(item => item.Id).Distinct().Count());
        Assert.All(sources, source => Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme));

        foreach (var (articleId, expected) in Articles)
        {
            var entry = Assert.Single(entries, item => item.Id == Guid.Parse(expected.Id));
            Assert.Equal(expected.Category, catalog.Categories.Single(category => category.Id == entry.CategoryId).Slug);
            var ownedSources = sources.Where(source => source.InformationEntryId == entry.Id).ToArray();
            Assert.Equal(expected.Sources, ownedSources.Length);
            foreach (var language in new[] { "en", "uk", "ru" })
            {
                var translation = Assert.Single(translations, item => item.InformationEntryId == entry.Id && item.LanguageCode == language);
                Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
                var draft = File.ReadAllText(Path.Combine(root, "docs", "content-drafts", "norway-v1", $"{articleId}.{language}.md"));
                var expectedTitle = Regex.Match(draft, "(?m)^# (.+)$").Groups[1].Value;
                Assert.Equal(expectedTitle, translation.Title);
                var expectedContent = ExpectedContent(draft, ownedSources);
                Assert.Equal(expectedContent, translation.Content);
                var citationIds = Regex.Matches(translation.Content, @"\[\[source:([^|\]]+)\|([^\]]+)\]\]")
                    .Select(match => Guid.Parse(match.Groups[1].Value)).ToArray();
                Assert.NotEmpty(citationIds);
                Assert.All(citationIds, id => Assert.Contains(ownedSources, source => source.Id == id));
            }
        }
    }

    [Fact]
    public async Task NorwayCompareMatchesReviewedDraftAcrossAllAspectsAndTopics()
    {
        var catalog = await new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();
        var facts = await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(catalog);
        JsonComparisonFactProvider.Validate(facts, catalog);

        Assert.Equal(15, CountryComparisonTopicMap.SupportedCountrySlugs.Count);
        Assert.All(CountryComparisonTopicMap.All, topic => Assert.Equal(15, topic.EntryIds.Count));
        Assert.Equal(360, facts.Topics.SelectMany(topic => topic.Aspects).Sum(aspect => aspect.Facts.Count));
        var norwayFacts = facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts)
            .Where(fact => fact.CountrySlug == "norway").ToArray();
        Assert.Equal(24, norwayFacts.Length);
        Assert.Equal(72, norwayFacts.Sum(fact => fact.Values.Count));
        Assert.All(norwayFacts, fact => Assert.True(fact.IsAvailable));
        Assert.All(norwayFacts, fact => Assert.Equal(ComparisonFactStatus.Approved, fact.Status));
        Assert.Equal(31, norwayFacts.SelectMany(fact => fact.OfficialSourceIds).Distinct().Count());
        var norwaySources = catalog.OfficialSources.Where(source => norwayFacts.SelectMany(fact => fact.OfficialSourceIds).Contains(source.Id)).ToArray();
        Assert.Equal(26, norwaySources.Select(source => source.Url.AbsoluteUri).Distinct(StringComparer.Ordinal).Count());

        var expectedByTopic = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["Compare.Topic.TemporaryProtectionApplication"] = Articles["NO-TP-01"].Id,
            ["Compare.Topic.ResidenceAfterProtection"] = Articles["NO-TP-02"].Id,
            ["Compare.Topic.FindingEmployment"] = Articles["NO-EMP-01"].Id,
            ["Compare.Topic.WorkAuthorizationRights"] = Articles["NO-EMP-02"].Id,
            ["Compare.Topic.Healthcare"] = Articles["NO-HEALTH-01"].Id,
            ["Compare.Topic.Housing"] = Articles["NO-HOUSING-01"].Id,
            ["Compare.Topic.EducationLanguage"] = Articles["NO-EDU-01"].Id,
            ["Compare.Topic.SocialAssistance"] = Articles["NO-SOC-01"].Id
        };
        foreach (var topic in CountryComparisonTopicMap.All)
        {
            Assert.Equal(Guid.Parse(expectedByTopic[topic.ResourceKey]), CountryComparisonTopicMap.EntryIdFor(topic, "norway"));
        }

        var root = FindRepositoryRoot();
        var draft = File.ReadAllText(Path.Combine(root, "docs", "content-drafts", "norway-v1", "COMPARE_DRAFT_01.md"));
        var blocks = Regex.Split(draft, "(?m)^### (?<aspect>[a-z0-9-]+)\\s*$");
        Assert.Equal(49, blocks.Length);
        var actualByAspect = facts.Topics.SelectMany(topic => topic.Aspects)
            .ToDictionary(aspect => aspect.Id, aspect => Assert.Single(aspect.Facts, fact => fact.CountrySlug == "norway"), StringComparer.Ordinal);
        for (var index = 1; index < blocks.Length; index += 2)
        {
            var aspectId = blocks[index];
            var body = blocks[index + 1];
            var fact = actualByAspect[aspectId];
            var entryId = Regex.Match(body, "(?m)^InformationEntryId: `([^`]+)`$").Groups[1].Value;
            Assert.Equal(Guid.Parse(entryId), fact.InformationEntryId);
            foreach (var (label, language) in new[] { ("EN", "en"), ("UK", "uk"), ("RU", "ru") })
            {
                var expected = Regex.Match(body, $"(?m)^{label}: (.*)$").Groups[1].Value;
                Assert.Equal(expected, fact.Values[language]);
            }
            var expectedSourceIds = Regex.Matches(body, "(?m)^- `([^`]+)` —")
                .Select(match => Guid.Parse(match.Groups[1].Value)).Order().ToArray();
            Assert.Equal(expectedSourceIds, fact.OfficialSourceIds.Order().ToArray());
        }
    }

    private static string ExpectedContent(string draft, IReadOnlyCollection<OfficialSource> sources)
    {
        var lines = draft.Replace("\r\n", "\n", StringComparison.Ordinal).Split('\n');
        var verifiedLine = Array.FindIndex(lines, line => line.StartsWith("- **Last verified:** ", StringComparison.Ordinal));
        Assert.True(verifiedLine >= 0);
        var content = string.Join('\n', lines[(verifiedLine + 1)..]).Trim();
        foreach (Match match in Regex.Matches(content, "(?m)^- \\[(?<label>.+?)\\]\\((?<url>https://[^)]+)\\)$"))
        {
            var source = Assert.Single(sources, item => item.Url.AbsoluteUri == match.Groups["url"].Value);
            var replacement = $"- [[source:{source.Id}|{match.Groups["label"].Value}]]";
            content = content.Replace(match.Value, replacement, StringComparison.Ordinal);
        }
        return content;
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "RefugeeNavigatorEurope.slnx")))
        {
            directory = directory.Parent;
        }
        return directory?.FullName ?? throw new DirectoryNotFoundException("Could not find the repository root.");
    }
}
