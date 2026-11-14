using System.Text.RegularExpressions;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Comparison;
using RefugeeNavigator.Web.Content;
using RefugeeNavigator.Web.Search;

namespace RefugeeNavigator.Tests;

public sealed class RomaniaContentIntegrationTests
{
    private static readonly Guid RomaniaId = Guid.Parse("10000000-0000-0000-0000-000000000008");
    private static readonly string[] Languages = ["en", "uk", "ru"];
    private static readonly IReadOnlyDictionary<string, (Guid Id, string Category)> Articles = new Dictionary<string, (Guid, string)>
    {
        ["RO-TP-01"] = (Guid.Parse("24d0ab3d-935c-50b7-8385-c1c54583ec51"), "temporary-protection"),
        ["RO-TP-02"] = (Guid.Parse("6b966a1f-335e-596a-9825-005d30ab23d4"), "temporary-protection"),
        ["RO-EMP-01"] = (Guid.Parse("54197f49-594b-51d2-a40c-2fcded05f791"), "employment"),
        ["RO-EMP-02"] = (Guid.Parse("61d13a69-6704-50ee-8b7b-e673a2828e65"), "employment"),
        ["RO-HEALTH-01"] = (Guid.Parse("be12596b-8657-5b23-a17c-dead174859f3"), "healthcare"),
        ["RO-HOUSING-01"] = (Guid.Parse("35bb6fe6-b0b9-5517-af04-e022c7105ec7"), "housing"),
        ["RO-EDU-01"] = (Guid.Parse("bfd554ff-a41a-5338-ab3e-25bb3c827a73"), "education"),
        ["RO-SOC-01"] = (Guid.Parse("97f02ab3-3352-597b-ad48-11ebf19993c2"), "social-assistance")
    };
    private static readonly IReadOnlyDictionary<string, string[]> Aspects = new Dictionary<string, string[]>
    {
        ["RO-TP-01"] = ["protection-scheme", "protection-application", "protection-dates"],
        ["RO-TP-02"] = ["residence-routes", "residence-conditions", "residence-filing"],
        ["RO-EMP-01"] = ["employment-vacancies", "employment-institutions", "employment-assistance-conditions"],
        ["RO-EMP-02"] = ["work-permission", "work-employer-duties", "work-minimum-pay"],
        ["RO-HEALTH-01"] = ["healthcare-access", "healthcare-proof", "healthcare-urgent"],
        ["RO-HOUSING-01"] = ["housing-initial", "housing-private-rental", "housing-financial-support"],
        ["RO-EDU-01"] = ["education-school-enrolment", "education-pupil-language", "education-higher-admission"],
        ["RO-SOC-01"] = ["social-subsistence", "social-application", "social-child-benefits"]
    };
    private static readonly Lazy<Task<CatalogData>> Catalog = new(() =>
        new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());

    [Fact]
    public async Task RomaniaHasExpectedAcceptedCatalogAndStatusCounts()
    {
        var catalog = await Catalog.Value;
        var romania = Assert.Single(catalog.Countries, country => country.Slug == "romania");
        Assert.Equal(RomaniaId, romania.Id);
        Assert.Equal("Romania", romania.Name);
        Assert.Equal(19, catalog.Countries.Count);
        Assert.Equal(147, catalog.InformationEntries.Count);
        Assert.Equal(441, catalog.InformationEntryTranslations.Count);
        Assert.Equal(375, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(66, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Outdated));

        var entries = catalog.InformationEntries.Where(entry => entry.CountryId == RomaniaId).ToArray();
        Assert.Equal(8, entries.Length);
        Assert.Equal(Articles.Values.Select(item => item.Id).Order().ToArray(), entries.Select(entry => entry.Id).Order().ToArray());
        foreach (var (code, expected) in Articles)
        {
            var entry = Assert.Single(entries, item => item.Id == expected.Id);
            Assert.Equal(expected.Category, catalog.Categories.Single(category => category.Id == entry.CategoryId).Slug);
            var translations = catalog.InformationEntryTranslations.Where(item => item.InformationEntryId == entry.Id).ToArray();
            Assert.Equal(Languages.Order(StringComparer.Ordinal).ToArray(), translations.Select(item => item.LanguageCode).Order(StringComparer.Ordinal).ToArray());
            Assert.All(translations, translation =>
            {
                Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
                Assert.Equal(new DateTimeOffset(2026, 11, 5, 12, 0, 0, TimeSpan.Zero), translation.LastUpdatedAt);
                Assert.False(string.IsNullOrWhiteSpace(translation.Title));
                Assert.False(string.IsNullOrWhiteSpace(translation.Summary));
                Assert.False(string.IsNullOrWhiteSpace(translation.Content));
            });
            Assert.Contains(code, Articles.Keys);
        }

        Assert.Contains(CountryVisibility.GetVisibleCountries(catalog, false), country => country.Slug == "romania");
        Assert.Contains(CountryVisibility.GetVisibleCountries(catalog, true), country => country.Slug == "romania");
        Assert.Equal(16, CountryVisibility.GetVisibleCountries(catalog, false).Count);
        Assert.Equal(19, CountryVisibility.GetVisibleCountries(catalog, true).Count);
    }

    [Fact]
    public async Task RomaniaDraftsMatchCatalogTranslationsAndKeepEditorialNotesOutOfContent()
    {
        var catalog = await Catalog.Value;
        var draftRoot = Path.Combine(FindRepositoryRoot(), "docs", "content-drafts", "romania-v1");
        var files = Directory.GetFiles(draftRoot, "RO-*.md");
        Assert.Equal(8, files.Length);

        foreach (var (code, expected) in Articles)
        {
            var draft = await File.ReadAllTextAsync(Path.Combine(draftRoot, code + ".md"));
            Assert.Contains($"Article ID: `{expected.Id}`", draft);
            foreach (var (language, section) in new[] { ("en", "EN"), ("uk", "UK"), ("ru", "RU") })
            {
                var match = Regex.Match(draft,
                    $@"(?ms)^## {section}\s+Title: (?<title>.*?)\s+Summary: (?<summary>.*?)\s+Content:\s+(?<content>.*?)(?=^### Claim coverage and editorial exclusions$)");
                Assert.True(match.Success, $"Missing {section} translation in {code}.");
                var translation = Assert.Single(catalog.InformationEntryTranslations,
                    item => item.InformationEntryId == expected.Id && item.LanguageCode == language);
                Assert.Equal(match.Groups["title"].Value.Trim(), translation.Title);
                Assert.Equal(match.Groups["summary"].Value.Trim(), translation.Summary);
                Assert.Equal(NormalizeLines(match.Groups["content"].Value.Trim()), NormalizeLines(translation.Content));
                Assert.DoesNotContain("Claim coverage", translation.Content, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("Editorial exclusions", translation.Content, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("https://", translation.Content, StringComparison.OrdinalIgnoreCase);
            }
        }
    }

    [Fact]
    public async Task RomaniaSourcesAreArticleOwnedHttpsAndMatchResearchPackages()
    {
        var catalog = await Catalog.Value;
        var sourceById = catalog.OfficialSources.ToDictionary(source => source.Id);
        var romaniaEntryIds = Articles.Values.Select(item => item.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(source => romaniaEntryIds.Contains(source.InformationEntryId)).ToArray();
        Assert.Equal(56, sources.Length);
        Assert.Equal(1400, catalog.OfficialSources.Count);
        Assert.Equal(catalog.OfficialSources.Count, catalog.OfficialSources.Select(source => source.Id).Distinct().Count());
        Assert.All(sources, source =>
        {
            Assert.Contains(source.InformationEntryId, romaniaEntryIds);
            Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme);
            Assert.False(string.IsNullOrWhiteSpace(source.Title));
            Assert.Equal(new DateTimeOffset(2026, 11, 5, 0, 0, 0, TimeSpan.Zero), source.LastCheckedAt);
        });

        foreach (var (code, expected) in Articles)
        {
            var entrySources = sources.Where(source => source.InformationEntryId == expected.Id).ToArray();
            var expectedSourceCount = code switch
            {
                "RO-TP-01" => 3,
                "RO-TP-02" => 8,
                "RO-EMP-01" => 9,
                "RO-EMP-02" => 9,
                "RO-HEALTH-01" => 6,
                "RO-HOUSING-01" => 4,
                "RO-EDU-01" => 10,
                "RO-SOC-01" => 7,
                _ => throw new InvalidOperationException($"Unexpected draft {code}.")
            };
            Assert.Equal(expectedSourceCount, entrySources.Length);
            var ownedIds = entrySources.Select(source => source.Id.ToString()).ToHashSet(StringComparer.OrdinalIgnoreCase);
            foreach (var translation in catalog.InformationEntryTranslations.Where(item => item.InformationEntryId == expected.Id))
            {
                var tokens = Regex.Matches(translation.Content, @"\[\[source:([^|\]]+)\|([^\]]+)\]\]");
                Assert.NotEmpty(tokens);
                foreach (Match token in tokens)
                {
                    Assert.Contains(token.Groups[1].Value, ownedIds);
                    Assert.True(sourceById.ContainsKey(Guid.Parse(token.Groups[1].Value)));
                }
            }
            var draft = await File.ReadAllTextAsync(Path.Combine(FindRepositoryRoot(), "docs", "content-drafts", "romania-v1", code + ".md"));
            Assert.Contains("## Editorial source set", draft);
        }
    }

    [Fact]
    public async Task RomaniaComparisonGridHasEightApprovedFactsPerAspectAndIsProductionVisible()
    {
        var catalog = await Catalog.Value;
        var path = Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json");
        var comparison = await new JsonComparisonFactProvider(path).LoadAsync(catalog);
        CountryComparisonTopicMap.Validate(catalog);
        Assert.Contains("romania", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Equal(120, CountryComparisonTopicMap.All.SelectMany(topic => topic.EntryIds.Values).Distinct().Count());

        var facts = comparison.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts)
            .Where(fact => fact.CountrySlug == "romania").ToArray();
        Assert.Equal(24, facts.Length);
        Assert.All(comparison.Topics.SelectMany(topic => topic.Aspects), aspect =>
        {
            var fact = Assert.Single(aspect.Facts, item => item.CountrySlug == "romania");
            Assert.Equal("Approved", fact.Status.ToString());
            Assert.True(fact.IsAvailable);
            Assert.Contains(Articles.Values.Select(item => item.Id), id => id == fact.InformationEntryId);
            Assert.NotEmpty(fact.OfficialSourceIds);
            Assert.All(fact.OfficialSourceIds, id => Assert.Equal(fact.InformationEntryId, sourceById(catalog, id).InformationEntryId));
            Assert.All(Languages, language => Assert.False(string.IsNullOrWhiteSpace(fact.Values[language])));
        });
        foreach (var (code, expected) in Articles)
        {
            Assert.Equal(Aspects[code].Order(StringComparer.Ordinal), facts.Where(fact => fact.InformationEntryId == expected.Id)
                .Select(fact => comparison.Topics.SelectMany(topic => topic.Aspects).Single(aspect => aspect.Facts.Contains(fact)).Id).Order(StringComparer.Ordinal));
        }
        var wageAspect = Assert.Single(comparison.Topics.SelectMany(topic => topic.Aspects), aspect => aspect.Id == "work-minimum-pay");
        var wage = Assert.Single(wageAspect.Facts, fact => fact.CountrySlug == "romania");
        Assert.Equal("Approved", wage.Status.ToString());
        Assert.True(wage.IsAvailable);
        Assert.Equal(new[] { Guid.Parse("8e63ea72-f1ca-52d0-bc87-d5fc2f4e25e8"), Guid.Parse("e4283f53-f70c-5eff-a2c7-3a78fc1615d7") }, wage.OfficialSourceIds);
        Assert.DoesNotContain(Guid.Parse("70cda0bf-de1c-54bc-b411-386e4dbec151"), wage.OfficialSourceIds);
        Assert.All(Languages, language =>
        {
            var normalizedWage = Regex.Replace(wage.Values[language], @"[^0-9]", "");
            Assert.Contains("4325", normalizedWage, StringComparison.Ordinal);
            Assert.Contains("4582", normalizedWage, StringComparison.Ordinal);
            Assert.True(wage.Values[language].Contains("gross", StringComparison.OrdinalIgnoreCase)
                || wage.Values[language].Contains("брутто", StringComparison.OrdinalIgnoreCase));
        });
        AssertRomaniaFactSources(comparison, catalog, "employment-vacancies", "54197f49-594b-51d2-a40c-2fcded05f791",
            "8b4add6a-c2bf-549f-8066-6bcd8a7e9789");
        AssertRomaniaFactSources(comparison, catalog, "employment-institutions", "54197f49-594b-51d2-a40c-2fcded05f791",
            "34e1344d-a0c0-5efd-bb74-261a662a5244");
        AssertRomaniaFactSources(comparison, catalog, "social-application", "97f02ab3-3352-597b-ad48-11ebf19993c2",
            "695597de-0102-535c-958f-fcb06159773d",
            "b8c201b0-ab0a-502e-ac96-37cd7b2f5d13",
            "4944c633-304a-5114-8213-2f843879df73",
            "6ddd1604-702c-5735-a7c8-059202dd424a");
    }

    private static void AssertRomaniaFactSources(
        ComparisonFactCatalog comparison,
        CatalogData catalog,
        string aspectId,
        string articleId,
        params string[] expectedSourceIds)
    {
        var aspect = Assert.Single(comparison.Topics.SelectMany(topic => topic.Aspects), item => item.Id == aspectId);
        var fact = Assert.Single(aspect.Facts, item => item.CountrySlug == "romania");
        Assert.Equal(Guid.Parse(articleId), fact.InformationEntryId);
        Assert.Equal("Approved", fact.Status.ToString());
        Assert.True(fact.IsAvailable);
        foreach (var sourceId in expectedSourceIds)
        {
            var source = sourceById(catalog, Guid.Parse(sourceId));
            Assert.Equal(fact.InformationEntryId, source.InformationEntryId);
            Assert.Contains(source.Id, fact.OfficialSourceIds);
        }
    }

    private static OfficialSource sourceById(CatalogData catalog, Guid id) =>
        Assert.Single(catalog.OfficialSources, source => source.Id == id);

    private static string NormalizeLines(string value) => value.Replace("\r\n", "\n", StringComparison.Ordinal).Trim();

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !Directory.Exists(Path.Combine(directory.FullName, ".git")))
            directory = directory.Parent;
        if (directory is null) throw new DirectoryNotFoundException("Could not locate repository root.");
        return directory.FullName;
    }
}
