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
        ["HU-TP-02"] = (Guid.Parse("914418c6-15f8-527c-85dd-be5bda5594fc"), "temporary-protection", 18),
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
    public async Task HungaryIsDevelopmentOnlyAndExcludedFromCompare()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "hungary");
        Assert.Equal(CountryId, country.Id);
        var entries = catalog.InformationEntries.Where(item => item.CountryId == CountryId).ToArray();
        Assert.Equal(7, entries.Length);
        Assert.Equal(Articles.Values.Select(item => item.Id).Order().ToArray(), entries.Select(item => item.Id).Order().ToArray());
        Assert.DoesNotContain("hungary", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.All(CountryComparisonTopicMap.All, topic => Assert.DoesNotContain("hungary", topic.EntryIds.Keys));
        Assert.DoesNotContain(CountryVisibility.GetVisibleCountries(catalog, false), item => item.Slug == "hungary");
        Assert.Contains(CountryVisibility.GetVisibleCountries(catalog, true), item => item.Slug == "hungary");

        var translations = catalog.InformationEntryTranslations.Where(item => entries.Any(entry => entry.Id == item.InformationEntryId)).ToArray();
        Assert.Equal(21, translations.Length);
        Assert.All(translations, item =>
        {
            Assert.Contains(item.LanguageCode, new[] { "en", "uk", "ru" });
            Assert.Equal(VerificationStatus.NeedsReview, item.VerificationStatus);
        });

        var sources = catalog.OfficialSources.Where(item => entries.Any(entry => entry.Id == item.InformationEntryId)).ToArray();
        Assert.Equal(97, sources.Length);
        Assert.Equal(97, sources.Select(item => item.Id).Distinct().Count());
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
                var body = Regex.Match(draft,
                    $@"(?ms)^## {heading}\s+Title: (?<title>.*?)\s+Summary: (?<summary>.*?)\s+Content:\s+(?<content>.*?)(?=^### Claim coverage and editorial exclusions$)");
                Assert.True(body.Success, $"Missing {heading} for {article}.");
                var translation = Assert.Single(translationSet, item => item.LanguageCode == language);
                Assert.Equal(body.Groups["title"].Value.Trim(), translation.Title);
                Assert.Equal(body.Groups["summary"].Value.Trim(), translation.Summary);
                Assert.Equal(body.Groups["content"].Value.Trim(), translation.Content);
            }
        }

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
