using System.Text.RegularExpressions;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Comparison;
using RefugeeNavigator.Web.Content;

namespace RefugeeNavigator.Tests;

public sealed class HungaryContentIntegrationTests
{
    private static readonly Guid CountryId = Guid.Parse("10000000-0000-0000-0000-000000000018");
    private static readonly IReadOnlyDictionary<string, (Guid Id, string Category, int Sources)> Articles = new Dictionary<string, (Guid, string, int)>
    {
        ["HU-TP-01"] = (Guid.Parse("1bb2a2b8-cfe0-5595-8155-138443fade17"), "temporary-protection", 8),
        ["HU-TP-02"] = (Guid.Parse("914418c6-15f8-527c-85dd-be5bda5594fc"), "temporary-protection", 19),
        ["HU-EMP-01"] = (Guid.Parse("0e65e68f-5c42-58d5-b7b3-e941896bb532"), "employment", 14),
        ["HU-EMP-02"] = (Guid.Parse("4dde28d5-8f4c-5503-926b-eac8789b4e9f"), "employment", 14),
        ["HU-HEALTH-01"] = (Guid.Parse("8cb405ab-c562-5c22-81ad-0fd93ef50202"), "healthcare", 14),
        ["HU-HOUSING-01"] = (Guid.Parse("bbdf7f5c-da1d-59ad-9515-5bd8499e2332"), "housing", 11),
        ["HU-EDU-01"] = (Guid.Parse("e0fd251b-cf52-5a45-9a8d-b07e91580985"), "education", 12),
        ["HU-SOC-01"] = (Guid.Parse("f34f7c15-d879-5ba8-81cf-65f7e9ca416f"), "social-assistance", 14)
    };
    private static readonly Lazy<Task<CatalogData>> Catalog = new(
        () => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());

    [Fact]
    public async Task HungaryIsProductionCoreVisibleAndIncludedInCompare()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "hungary");
        Assert.Equal(CountryId, country.Id);
        Assert.Equal(6, catalog.Categories.Count);
        Assert.Equal(Guid.Parse("1bb2a2b8-cfe0-5595-8155-138443fade17"), Articles["HU-TP-01"].Id);
        var entries = catalog.InformationEntries.Where(item => item.CountryId == CountryId).ToArray();
        Assert.Equal(8, entries.Length);
        Assert.Equal(Articles.Values.Select(item => item.Id).Order().ToArray(), entries.Select(item => item.Id).Order().ToArray());
        Assert.Contains("hungary", CountryComparisonTopicMap.SupportedCountrySlugs);
        var expectedMappings = new Dictionary<string, Guid>
        {
            ["Compare.Topic.TemporaryProtectionApplication"] = Articles["HU-TP-01"].Id,
            ["Compare.Topic.ResidenceAfterProtection"] = Articles["HU-TP-02"].Id,
            ["Compare.Topic.FindingEmployment"] = Articles["HU-EMP-01"].Id,
            ["Compare.Topic.WorkAuthorizationRights"] = Articles["HU-EMP-02"].Id,
            ["Compare.Topic.Healthcare"] = Articles["HU-HEALTH-01"].Id,
            ["Compare.Topic.Housing"] = Articles["HU-HOUSING-01"].Id,
            ["Compare.Topic.EducationLanguage"] = Articles["HU-EDU-01"].Id,
            ["Compare.Topic.SocialAssistance"] = Articles["HU-SOC-01"].Id
        };
        Assert.Equal(15, CountryComparisonTopicMap.SupportedCountrySlugs.Count);
        Assert.All(CountryComparisonTopicMap.All, topic =>
        {
            Assert.Equal(15, topic.EntryIds.Count);
            Assert.Equal(expectedMappings[topic.ResourceKey], CountryComparisonTopicMap.EntryIdFor(topic, "hungary"));
        });
        var comparison = await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(catalog);
        var allFacts = comparison.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).ToArray();
        Assert.Equal(360, allFacts.Length);
        Assert.Equal(353, allFacts.Count(fact => fact.IsAvailable));
        Assert.Equal(7, allFacts.Count(fact => !fact.IsAvailable));
        Assert.Equal(355, allFacts.Count(fact => fact.Status == ComparisonFactStatus.Approved));
        Assert.Equal(5, allFacts.Count(fact => fact.Status == ComparisonFactStatus.Draft));
        var hungaryFacts = allFacts.Where(fact => fact.CountrySlug == "hungary").ToArray();
        Assert.Equal(24, hungaryFacts.Length);
        Assert.All(hungaryFacts, fact =>
        {
            Assert.True(fact.IsAvailable);
            Assert.Equal(ComparisonFactStatus.Approved, fact.Status);
            Assert.Equal(expectedMappings[comparison.Topics.Single(topic => topic.Aspects.Any(aspect => aspect.Facts.Contains(fact))).TopicResourceKey], fact.InformationEntryId);
            Assert.Equal(new[] { "en", "ru", "uk" }, fact.Values.Keys.Order(StringComparer.Ordinal).ToArray());
            Assert.All(fact.Values.Values, value => Assert.False(string.IsNullOrWhiteSpace(value)));
            Assert.NotEmpty(fact.OfficialSourceIds);
            Assert.All(fact.OfficialSourceIds, id => Assert.Equal(fact.InformationEntryId, catalog.OfficialSources.Single(source => source.Id == id).InformationEntryId));
        });
        Assert.Contains(CountryVisibility.GetVisibleCountries(catalog, false), item => item.Slug == "hungary");
        Assert.Contains(CountryVisibility.GetVisibleCountries(catalog, true), item => item.Slug == "hungary");

        var translations = catalog.InformationEntryTranslations.Where(item => entries.Any(entry => entry.Id == item.InformationEntryId)).ToArray();
        Assert.Equal(24, translations.Length);
        Assert.All(translations, item =>
        {
            Assert.Contains(item.LanguageCode, new[] { "en", "uk", "ru" });
            Assert.Equal(VerificationStatus.Verified, item.VerificationStatus);
        });

        var sources = catalog.OfficialSources.Where(item => entries.Any(entry => entry.Id == item.InformationEntryId)).ToArray();
        Assert.Equal(106, sources.Length);
        Assert.Equal(106, sources.Select(item => item.Id).Distinct().Count());
        Assert.All(sources, item => Assert.Equal(Uri.UriSchemeHttps, item.Url.Scheme));
        foreach (var (article, expected) in Articles)
        {
            var entry = Assert.Single(entries, item => item.Id == expected.Id);
            Assert.Equal(expected.Category, catalog.Categories.Single(item => item.Id == entry.CategoryId).Slug);
            var owned = sources.Where(item => item.InformationEntryId == expected.Id).ToArray();
            Assert.Equal(expected.Sources, owned.Length);
            var translationSet = translations.Where(item => item.InformationEntryId == expected.Id).ToArray();
            Assert.Equal(new[] { "en", "ru", "uk" }, translationSet.Select(item => item.LanguageCode).Order(StringComparer.Ordinal).ToArray());
            foreach (var translation in translationSet)
            {
                foreach (Match citation in Regex.Matches(translation.Content, @"\[\[source:([^|\]]+)\|([^\]]+)\]\]"))
                {
                    Assert.Contains(owned, source => source.Id == Guid.Parse(citation.Groups[1].Value));
                }
            }

            var citedIds = translationSet
                .SelectMany(translation => Regex.Matches(translation.Content, @"\[\[source:([^|\]]+)\|([^\]]+)\]\]")
                    .Cast<Match>()
                    .Select(citation => Guid.Parse(citation.Groups[1].Value)))
                .Distinct()
                .Order()
                .ToArray();
            if (article == "HU-TP-01")
            {
                Assert.Equal(owned.Select(source => source.Id).Order().ToArray(), citedIds);
                Assert.Equal(owned.Length, owned.Select(source => source.Url.AbsoluteUri).Distinct(StringComparer.Ordinal).Count());
            }

            var draft = await File.ReadAllTextAsync(Path.Combine(FindRepositoryRoot(), "docs", "content-drafts", "hungary-v1", article + ".md"));
            Assert.Contains($"Article ID: `{expected.Id}`", draft);
            var draftSources = Regex.Matches(draft,
                    @"(?m)^- `HU-[^`]+` → `(?<id>[^`]+)` — (?<title>.*?) \(research source .*?; \[official sources?\]\((?<url>https://[^) ]+)")
                .Cast<Match>()
                .ToDictionary(match => Guid.Parse(match.Groups["id"].Value));
            Assert.True(expected.Sources == draftSources.Count, $"Unexpected draft source count for {article}: {draftSources.Count}.");
            Assert.Equal(draftSources.Keys.Order().ToArray(), owned.Select(item => item.Id).Order().ToArray());
            foreach (var source in owned)
            {
                Assert.Equal(draftSources[source.Id].Groups["title"].Value, source.Title);
                Assert.Equal(draftSources[source.Id].Groups["url"].Value, source.Url.OriginalString);
            }

            foreach (var (language, heading) in new[] { ("en", "EN"), ("uk", "UK"), ("ru", "RU") })
            {
                var sectionStart = draft.IndexOf($"## {heading}\n", StringComparison.Ordinal);
                Assert.True(sectionStart >= 0, $"Missing {heading} for {article}.");
                var contentMarker = draft.IndexOf("Content:\n\n", sectionStart, StringComparison.Ordinal);
                Assert.True(contentMarker >= 0, $"Missing {heading} content for {article}.");
                var contentStart = contentMarker + "Content:\n\n".Length;
                var editorialBoundary = Regex.Match(draft[contentStart..],
                    @"(?m)^### (?:Claim coverage and editorial exclusions|Охоплення тверджень і редакційні межі|Охват утверждений и редакционные ограничения)$");
                var contentEnd = editorialBoundary.Success ? contentStart + editorialBoundary.Index : -1;
                Assert.True(contentEnd >= 0, $"Missing {heading} editorial boundary for {article}.");
                var metadata = Regex.Match(draft[sectionStart..contentMarker],
                    @"(?s)^## [^\r\n]+\s+Title: (?<title>.*?)\s+Summary: (?<summary>.*)$");
                Assert.True(metadata.Success, $"Missing {heading} title or summary for {article}.");
                var translation = Assert.Single(translationSet, item => item.LanguageCode == language);
                Assert.Equal(metadata.Groups["title"].Value.Trim(), translation.Title);
                Assert.Equal(metadata.Groups["summary"].Value.Trim(), translation.Summary);
                Assert.Equal(draft[contentStart..contentEnd].Trim(), translation.Content);
            }
        }

        var temporaryProtectionEntries = entries.Where(entry =>
            catalog.Categories.Single(category => category.Id == entry.CategoryId).Slug == "temporary-protection").ToArray();
        Assert.Equal(2, temporaryProtectionEntries.Length);
        Assert.Equal(new[]
        {
            Guid.Parse("1bb2a2b8-cfe0-5595-8155-138443fade17"),
            Guid.Parse("914418c6-15f8-527c-85dd-be5bda5594fc")
        }.Order().ToArray(), temporaryProtectionEntries.Select(entry => entry.Id).Order().ToArray());
        Assert.Equal(6, entries.Select(entry => entry.CategoryId).Distinct().Count());

        var duplicatedStatute = Assert.Single(sources, item => item.Id == Guid.Parse("94b90de6-7250-5e47-89d7-22eb085d0ee2"));
        Assert.Equal(Articles["HU-TP-02"].Id, duplicatedStatute.InformationEntryId);
        Assert.Equal("https://njt.hu/jogszabaly/2007-80-00-00.33", duplicatedStatute.Url.AbsoluteUri);
        Assert.Equal("National Legislation Database, Act LXXX of 2007 on the Right of Asylum, §1(3)", duplicatedStatute.Title);
        var originalStatute = Assert.Single(catalog.OfficialSources, item => item.Id == Guid.Parse("e724b5fb-a79d-54c3-bc21-4444975d0052"));
        Assert.Equal(Articles["HU-TP-01"].Id, originalStatute.InformationEntryId);
        Assert.Equal(originalStatute.LastCheckedAt, duplicatedStatute.LastCheckedAt);
        var housingSource = Assert.Single(sources, item => item.Id == Guid.Parse("c7daa0bb-96e5-5d55-a419-8a53613f0608"));
        Assert.Equal("https://www.oif.gov.hu/menedekeseknek-nyujtott-ellatasok-es-tamogatasok", housingSource.Url.AbsoluteUri);
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
