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
    public async Task NorwayCoreIsLocalizedVerifiedVisibleAndSourceOwnedWithoutCompareFacts()
    {
        var catalog = await new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();
        var country = Assert.Single(catalog.Countries, item => item.Slug == "norway");
        Assert.Equal(Guid.Parse(CountryId), country.Id);
        Assert.Equal(19, catalog.Countries.Count);
        Assert.Equal(8, catalog.InformationEntries.Count(item => item.CountryId == country.Id));
        Assert.Contains(CountryVisibility.GetVisibleCountries(catalog, false), item => item.Id == country.Id);
        Assert.Contains(CountryVisibility.GetVisibleCountries(catalog, true), item => item.Id == country.Id);
        Assert.DoesNotContain("norway", CountryComparisonTopicMap.SupportedCountrySlugs);

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
