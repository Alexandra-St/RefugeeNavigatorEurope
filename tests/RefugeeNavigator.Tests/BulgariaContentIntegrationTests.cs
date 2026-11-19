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
        Assert.Equal(152, catalog.InformationEntries.Count);
        Assert.Equal(456, catalog.InformationEntryTranslations.Count);
        Assert.Equal(456, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
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
                Assert.Equal(new DateTimeOffset(2026, 11, 17, 0, 0, 0, TimeSpan.Zero), item.LastUpdatedAt);
                Assert.False(string.IsNullOrWhiteSpace(item.Title));
                Assert.False(string.IsNullOrWhiteSpace(item.Summary));
                Assert.False(string.IsNullOrWhiteSpace(item.Content));
            });
        }

        var production = CountryVisibility.GetVisibleCountries(catalog, false);
        var development = CountryVisibility.GetVisibleCountries(catalog, true);
        Assert.Contains(production, item => item.Slug == "bulgaria");
        Assert.Contains(development, item => item.Slug == "bulgaria");
        Assert.Equal(19, production.Count);
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
        Assert.Equal(70, sources.Length);
        Assert.Equal(1544, catalog.OfficialSources.Count);
        Assert.Equal(70, sources.Select(item => item.Id).Distinct().Count());
        var sourceById = catalog.OfficialSources.ToDictionary(item => item.Id);
        var citedSourceIds = new HashSet<Guid>();
        Assert.All(sources, item =>
        {
            Assert.Contains(item.InformationEntryId, entryIds);
            Assert.Equal(Uri.UriSchemeHttps, item.Url.Scheme);
            Assert.False(string.IsNullOrWhiteSpace(item.Title));
            if (new[] { "6dd90ade-3354-5160-97dc-fe4d66eed6cb", "62ec9f9f-0d3a-51ba-88aa-6c5aade59a71", "c233cdfc-267b-58d8-9fd5-887650066d8a", "d584362c-3990-5055-8833-158f2b594773", "bb443266-cbc9-55a9-bcbf-eb01ea44d1fc", "f5d17e23-dfba-5cd0-bde0-b3f0b8dbface", "989cc461-fa87-5e26-8058-a8404fbe6d52", "a2981f25-2a47-5ef4-ad62-349dee64690b" }.Contains(item.Id.ToString()))
                Assert.Equal(new DateTimeOffset(2026, 11, 7, 0, 0, 0, TimeSpan.Zero), item.LastCheckedAt);
            else
                Assert.Equal(new DateTimeOffset(2026, 11, 17, 0, 0, 0, TimeSpan.Zero), item.LastCheckedAt);
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
                    citedSourceIds.Add(sourceId);
                    Assert.Contains(sourceId, owned.Keys);
                    Assert.Equal(expected.Id, sourceById[sourceId].InformationEntryId);
                }
            }
        }
        Assert.Equal(sources.Select(item => item.Id).ToHashSet(), citedSourceIds);
    }

    [Fact]
    public async Task BulgariaHasTwentyFourAvailableApprovedFactsAndCompleteTopicMappings()
    {
        var catalog = await Catalog.Value;
        CountryComparisonTopicMap.Validate(catalog);
        Assert.Contains("bulgaria", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Equal(152, CountryComparisonTopicMap.All.SelectMany(item => item.EntryIds.Values).Distinct().Count());
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
        var protectionDates = Assert.Single(comparison.Topics.SelectMany(item => item.Aspects), item => item.Id == "protection-dates");
        var bulgariaProtectionDates = Assert.Single(protectionDates.Facts, item => item.CountrySlug == "bulgaria");
        Assert.Contains(Guid.Parse("1b7d23f4-0c08-581b-b156-ba4c119f92bc"), bulgariaProtectionDates.OfficialSourceIds);
        Assert.DoesNotContain(Guid.Parse("d584362c-3990-5055-8833-158f2b594773"), bulgariaProtectionDates.OfficialSourceIds);

        var netherlandsId = catalog.Countries.Single(item => item.Slug == "netherlands").Id;
        var netherlandsEntryIds = catalog.InformationEntries.Where(item => item.CountryId == netherlandsId)
            .Select(item => item.Id).ToHashSet();
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => netherlandsEntryIds.Contains(item.InformationEntryId)
            && item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(1, allFacts.Count(item => item.CountrySlug == "netherlands" && item.Status == ComparisonFactStatus.Draft));
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

    [Fact]
    public async Task BulgariaReleaseBaselinePreservesReviewedDatesWagesAndSourceBindings()
    {
        var catalog = await Catalog.Value;
        var translations = catalog.InformationEntryTranslations
            .Where(item => Articles.Values.Select(value => value.Id).Contains(item.InformationEntryId)).ToArray();
        var content = string.Join("\n", translations.Select(item => item.Content));

        Assert.Equal(24, translations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.All(translations, item => Assert.Equal(new DateTimeOffset(2026, 11, 17, 0, 0, 0, TimeSpan.Zero), item.LastUpdatedAt));
        Assert.Contains("4 March 2028", content);
        Assert.DoesNotContain("national temporary-protection period was extended through 4 March 2027", content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("30 April 2026", content);
        Assert.Contains("5 March–30 April 2027", content);
        Assert.Contains("does not describe the 2027 procedure", content);
        Assert.Contains("temporary protection is", content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Temporary protection is a separate status", content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("without a separate work permit", content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Labour Inspectorate within seven days", content);
        Assert.Contains("Employment Agency notice", content);
        Assert.Contains("free profession", content, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("€620.20", content);
        Assert.Contains("€3.74", content);
        Assert.Contains("€660", content);
        Assert.Contains("€3.98", content);
        Assert.Contains("90 days", content);
        Assert.DoesNotContain("BGN 28.40", content);
        Assert.Contains("4 March 2027", content); // Program 3 remains separately scoped.
        Assert.Contains("€390.63", content);
        Assert.Contains("€117.19", content);
        Assert.Contains("€64.39", content);
        Assert.Contains("31 October 2026", content);
        Assert.Contains("31 March 2027", content);
        var social = translations.Single(item => item.InformationEntryId == Articles["BG-SOC-01"].Id && item.LanguageCode == "en").Content;
        Assert.Contains("1 January 2026", social);
        Assert.Contains("sole legal tender from 1 February 2026", social);
        Assert.Contains("1.95583", social);
        Assert.Contains("8 August 2026", social);
        var education = translations.Single(item => item.InformationEntryId == Articles["BG-EDU-01"].Id && item.LanguageCode == "en").Content;
        Assert.Contains("school year in which the child turns seven", education);
        Assert.Contains("at a parent’s choice", education);
        Assert.Contains("readiness assessment", education);
        Assert.Contains("continues to 16", education);

        var sourceIds = catalog.OfficialSources.Select(item => item.Id).ToHashSet();
        Assert.All(new[] { "1b7d23f4-0c08-581b-b156-ba4c119f92bc", "399a3a4e-75e4-59d3-bdb1-315585052ea8", "8b439e25-9430-5ab2-92b9-0df6343fa660", "44c74fe5-d729-5690-b037-fd1d96bf502d", "50f0a447-2fb8-532c-920e-8f24e69a637a" }, id => Assert.Contains(Guid.Parse(id), sourceIds));
        var sourcesById = catalog.OfficialSources.ToDictionary(item => item.Id);
        Assert.Equal("https://www.mvr.bg/migration/%D0%B0%D0%B4%D0%BC%D0%B8%D0%BD%D0%B8%D1%81%D1%82%D1%80%D0%B0%D1%82%D0%B8%D0%B2%D0%BD%D0%B8-%D1%83%D1%81%D0%BB%D1%83%D0%B3%D0%B8-%D0%B7%D0%B0-%D0%B3%D1%80%D0%B0%D0%B6%D0%B4%D0%B0%D0%BD%D0%B8/rpr", sourcesById[Guid.Parse("92763c9a-e086-5432-be40-770681f92d95")].Url.AbsoluteUri);
        Assert.Equal("https://ruo.mon.bg/sofia-grad/regulation/naredba-%E2%84%96-3-ot-6-april-2017-g-za-usloviyata-i-reda-za-priemane-i-obuchenie-na-liczata-tarsesthi-ili-poluchili-mezhdunarodna-zakrila/", sourcesById[Guid.Parse("b76ea3f3-4ee1-5b97-b724-5cbf35d67f34")].Url.AbsoluteUri);
        Assert.Equal(38, File.ReadAllLines(Path.Combine(FindRepositoryRoot(), "docs", "content-refresh", "2026-11", "bulgaria", "CLAIM_SOURCE_COVERAGE_01.csv")).Count(line => line.Contains(",YES", StringComparison.Ordinal)));
        var claims = File.ReadAllLines(Path.Combine(FindRepositoryRoot(), "docs", "content-refresh", "2026-11", "bulgaria", "CLAIM_SOURCE_COVERAGE_01.csv"));
        Assert.Equal(111, claims.Length - 1);
        Assert.Equal(97, claims.Count(line => line.Contains(",MATERIAL,", StringComparison.Ordinal)));
        Assert.Equal(0, claims.Count(line => line.Contains(",MATERIAL,", StringComparison.Ordinal) && line.Contains(",UNSUPPORTED,", StringComparison.Ordinal)));
        Assert.Equal(76, claims.Count(line => line.Contains(",CONFIRMED,", StringComparison.Ordinal)));
        Assert.Equal(3, claims.Count(line => line.Contains(",UPDATED,", StringComparison.Ordinal)));
        Assert.Equal(16, claims.Count(line => line.Contains(",QUALIFICATION REQUIRED,", StringComparison.Ordinal)));
        Assert.Equal(16, claims.Count(line => line.Contains(",NEW IMPORTANT INFORMATION,", StringComparison.Ordinal)));
        Assert.Equal(8, File.ReadAllLines(Path.Combine(FindRepositoryRoot(), "docs", "content-refresh", "2026-11", "bulgaria", "DATE_EVENT_WATCH_CANDIDATES_01.md")).Count(line => line.StartsWith("- Trigger date:", StringComparison.Ordinal)));
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
