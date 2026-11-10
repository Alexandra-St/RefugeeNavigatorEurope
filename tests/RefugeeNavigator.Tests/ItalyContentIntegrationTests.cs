using System.Globalization;
using System.Resources;
using System.Text.RegularExpressions;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Comparison;
using RefugeeNavigator.Web.Content;
using RefugeeNavigator.Web.Localization;
using RefugeeNavigator.Web.Search;

namespace RefugeeNavigator.Tests;

public sealed class ItalyContentIntegrationTests
{
    private static readonly Guid CountryId = Guid.Parse("10000000-0000-0000-0000-000000000012");
    private static readonly IReadOnlyDictionary<string, (Guid Id, string Category)> Articles = new Dictionary<string, (Guid, string)>
    {
        ["IT-TP-01"] = (Guid.Parse("c8c06066-d3cd-55ce-88b0-3499384eece7"), "temporary-protection"),
        ["IT-TP-02"] = (Guid.Parse("28aee635-27ff-525c-9cd3-8673b5ba2be3"), "temporary-protection"),
        ["IT-EMP-01"] = (Guid.Parse("39091eb7-04c7-5b45-83b7-585f5f1ce719"), "employment"),
        ["IT-EMP-02"] = (Guid.Parse("d988941a-e709-53ba-a2bf-47f1ac2f9956"), "employment"),
        ["IT-HEALTH-01"] = (Guid.Parse("462ace7d-8deb-5cd7-a530-548c79a8e144"), "healthcare"),
        ["IT-HOUSING-01"] = (Guid.Parse("5aafddcb-e302-52b7-9365-f95e7261d3a4"), "housing"),
        ["IT-EDU-01"] = (Guid.Parse("ba3d3110-730d-5340-a89a-2b6722430bdd"), "education"),
        ["IT-SOC-01"] = (Guid.Parse("55696570-3337-5e2f-8a24-71e256e869e9"), "social-assistance")
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

    private static readonly IReadOnlyDictionary<string, Guid> ArticleByAspect = new Dictionary<string, Guid>
    {
        ["protection-scheme"] = Articles["IT-TP-01"].Id,
        ["protection-application"] = Articles["IT-TP-01"].Id,
        ["protection-dates"] = Articles["IT-TP-01"].Id,
        ["residence-routes"] = Articles["IT-TP-02"].Id,
        ["residence-conditions"] = Articles["IT-TP-02"].Id,
        ["residence-filing"] = Articles["IT-TP-02"].Id,
        ["employment-vacancies"] = Articles["IT-EMP-01"].Id,
        ["employment-institutions"] = Articles["IT-EMP-01"].Id,
        ["employment-assistance-conditions"] = Articles["IT-EMP-01"].Id,
        ["work-permission"] = Articles["IT-EMP-02"].Id,
        ["work-employer-duties"] = Articles["IT-EMP-02"].Id,
        ["work-minimum-pay"] = Articles["IT-EMP-02"].Id,
        ["healthcare-access"] = Articles["IT-HEALTH-01"].Id,
        ["healthcare-proof"] = Articles["IT-HEALTH-01"].Id,
        ["healthcare-urgent"] = Articles["IT-HEALTH-01"].Id,
        ["housing-initial"] = Articles["IT-HOUSING-01"].Id,
        ["housing-private-rental"] = Articles["IT-HOUSING-01"].Id,
        ["housing-financial-support"] = Articles["IT-HOUSING-01"].Id,
        ["education-school-enrolment"] = Articles["IT-EDU-01"].Id,
        ["education-pupil-language"] = Articles["IT-EDU-01"].Id,
        ["education-higher-admission"] = Articles["IT-EDU-01"].Id,
        ["social-subsistence"] = Articles["IT-SOC-01"].Id,
        ["social-application"] = Articles["IT-SOC-01"].Id,
        ["social-child-benefits"] = Articles["IT-SOC-01"].Id
    };

    private static readonly Lazy<Task<CatalogData>> Catalog = new(() => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());
    private static readonly Lazy<Task<ComparisonFactCatalog>> Facts = new(async () =>
        await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(await Catalog.Value));

    [Fact]
    public async Task ItalyHasEightVerifiedArticlesAndIsVisibleInProductionAndDevelopment()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "italy");
        Assert.Equal(CountryId, country.Id);
        Assert.Equal("Italy", country.Name);
        Assert.Equal(13, catalog.Countries.Count);
        Assert.Equal(104, catalog.InformationEntries.Count);
        Assert.Equal(312, catalog.InformationEntryTranslations.Count);
        Assert.Equal(309, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(3, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Outdated));

        var entries = catalog.InformationEntries.Where(item => item.CountryId == CountryId).ToArray();
        Assert.Equal(8, entries.Length);
        Assert.Equal(Articles.Values.Select(item => item.Id).Order().ToArray(), entries.Select(item => item.Id).Order().ToArray());
        Assert.Equal(8, Articles.Values.Select(item => item.Id).Distinct().Count());
        var categoryCounts = entries.GroupBy(item => catalog.Categories.Single(category => category.Id == item.CategoryId).Slug)
            .ToDictionary(group => group.Key, group => group.Count());
        Assert.Equal(2, categoryCounts["temporary-protection"]);
        Assert.Equal(2, categoryCounts["employment"]);
        Assert.Equal(1, categoryCounts["healthcare"]);
        Assert.Equal(1, categoryCounts["housing"]);
        Assert.Equal(1, categoryCounts["education"]);
        Assert.Equal(1, categoryCounts["social-assistance"]);

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
        Assert.Contains(production, item => item.Slug == "italy");
        Assert.Contains(development, item => item.Slug == "italy");
        Assert.Equal(13, production.Count);
        Assert.Equal(13, development.Count);
    }

    [Fact]
    public async Task ItalyTranslationsAndSeventyArticleOwnedSourcesMatchExactly()
    {
        var catalog = await Catalog.Value;
        var root = Path.Combine(FindRepositoryRoot(), "docs", "content-drafts", "italy-v1");
        Assert.Equal(8, Directory.GetFiles(root, "IT-*.md").Length);
        var entryIds = Articles.Values.Select(item => item.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(item => entryIds.Contains(item.InformationEntryId)).ToArray();
        Assert.Equal(70, sources.Length);
        Assert.Equal(864, catalog.OfficialSources.Count);
        Assert.Equal(70, sources.Select(item => item.Id).Distinct().Count());
        Assert.Equal(catalog.OfficialSources.Count, catalog.OfficialSources.Select(item => item.Id).Distinct().Count());
        var sourceById = catalog.OfficialSources.ToDictionary(item => item.Id);
        Assert.All(sources, item =>
        {
            Assert.Contains(item.InformationEntryId, entryIds);
            Assert.Equal(Uri.UriSchemeHttps, item.Url.Scheme);
            Assert.False(string.IsNullOrWhiteSpace(item.Title));
            Assert.Equal(new DateTimeOffset(2026, 11, 8, 0, 0, 0, TimeSpan.Zero), item.LastCheckedAt);
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
                var translation = Assert.Single(catalog.InformationEntryTranslations,
                    item => item.InformationEntryId == expected.Id && item.LanguageCode == language);
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
    public async Task ItalyHasTwentyFourApprovedAvailableFactsWithExactMappingsAndPreservedHolds()
    {
        var catalog = await Catalog.Value;
        var comparison = await Facts.Value;
        CountryComparisonTopicMap.Validate(catalog);
        Assert.Contains("italy", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Equal(104, CountryComparisonTopicMap.All.SelectMany(item => item.EntryIds.Values).Distinct().Count());
        var expectedTopicArticles = new Dictionary<string, Guid>
        {
            ["Compare.Topic.TemporaryProtectionApplication"] = Articles["IT-TP-01"].Id,
            ["Compare.Topic.ResidenceAfterProtection"] = Articles["IT-TP-02"].Id,
            ["Compare.Topic.FindingEmployment"] = Articles["IT-EMP-01"].Id,
            ["Compare.Topic.WorkAuthorizationRights"] = Articles["IT-EMP-02"].Id,
            ["Compare.Topic.Healthcare"] = Articles["IT-HEALTH-01"].Id,
            ["Compare.Topic.Housing"] = Articles["IT-HOUSING-01"].Id,
            ["Compare.Topic.EducationLanguage"] = Articles["IT-EDU-01"].Id,
            ["Compare.Topic.SocialAssistance"] = Articles["IT-SOC-01"].Id
        };
        Assert.All(CountryComparisonTopicMap.All, topic =>
        {
            Assert.Equal(13, topic.EntryIds.Count);
            Assert.Equal(expectedTopicArticles[topic.ResourceKey], CountryComparisonTopicMap.EntryIdFor(topic, "italy"));
        });

        var allFacts = comparison.Topics.SelectMany(item => item.Aspects).SelectMany(item => item.Facts).ToArray();
        var italyFacts = allFacts.Where(item => item.CountrySlug == "italy").ToArray();
        Assert.Equal(312, allFacts.Length);
        Assert.Equal(305, allFacts.Count(item => item.IsAvailable));
        Assert.Equal(7, allFacts.Count(item => !item.IsAvailable));
        Assert.Equal(307, allFacts.Count(item => item.Status == ComparisonFactStatus.Approved));
        Assert.Equal(5, allFacts.Count(item => item.Status == ComparisonFactStatus.Draft));
        Assert.Equal(24, italyFacts.Length);
        Assert.Equal(24, italyFacts.Count(item => item.Status == ComparisonFactStatus.Approved));
        Assert.All(italyFacts, item => Assert.True(item.IsAvailable));
        var italyAspectIds = comparison.Topics.SelectMany(topic => topic.Aspects)
            .Where(aspect => aspect.Facts.Any(fact => fact.CountrySlug == "italy"))
            .Select(aspect => aspect.Id).ToArray();
        Assert.Equal(AspectIds.Order(StringComparer.Ordinal), italyAspectIds.Order(StringComparer.Ordinal));
        Assert.Equal(24, italyAspectIds.Distinct().Count());

        var sourcesById = catalog.OfficialSources.ToDictionary(item => item.Id);
        Assert.All(italyFacts, fact =>
        {
            Assert.Equal(ArticleByAspect[Assert.Single(italyAspectIds, aspectId =>
                comparison.Topics.SelectMany(topic => topic.Aspects).Any(aspect => aspect.Id == aspectId && aspect.Facts.Contains(fact)))], fact.InformationEntryId);
            Assert.NotEmpty(fact.OfficialSourceIds);
            Assert.Equal(fact.OfficialSourceIds.Count, fact.OfficialSourceIds.Distinct().Count());
            Assert.All(fact.OfficialSourceIds, sourceId =>
            {
                Assert.True(sourcesById.TryGetValue(sourceId, out var source));
                Assert.Equal(fact.InformationEntryId, source!.InformationEntryId);
            });
            Assert.Equal(new[] { "en", "ru", "uk" }, fact.Values.Keys.Order(StringComparer.Ordinal).ToArray());
            Assert.All(fact.Values.Values, value => Assert.False(string.IsNullOrWhiteSpace(value)));
        });

        Assert.Equal(13, catalog.Countries.Count);
        Assert.Equal(104, catalog.InformationEntries.Count);
        Assert.Equal(312, catalog.InformationEntryTranslations.Count);
        Assert.Equal(309, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(3, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));

        var netherlandsEntries = catalog.InformationEntries.Where(item => item.CountryId == Guid.Parse("10000000-0000-0000-0000-000000000007"))
            .Select(item => item.Id).ToHashSet();
        Assert.Equal(21, catalog.InformationEntryTranslations.Count(item => netherlandsEntries.Contains(item.InformationEntryId)
            && item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(3, catalog.InformationEntryTranslations.Count(item => netherlandsEntries.Contains(item.InformationEntryId)
            && item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(7, ArticleSearch.Search(catalog, null, "netherlands", null, "Verified", "en").Count);
        var nlTp01 = Assert.Single(catalog.InformationEntryTranslations, item =>
            item.InformationEntryId == Guid.Parse("244b1055-4d8c-55b2-820e-60ff018c3b1f") && item.LanguageCode == "en");
        Assert.Equal(VerificationStatus.NeedsReview, nlTp01.VerificationStatus);

        var netherlandsFacts = allFacts.Where(item => item.CountrySlug == "netherlands").ToArray();
        Assert.Equal(20, netherlandsFacts.Count(item => item.Status == ComparisonFactStatus.Approved));
        Assert.Equal(4, netherlandsFacts.Count(item => item.Status == ComparisonFactStatus.Draft));
        foreach (var aspectId in new[] { "protection-scheme", "protection-application", "protection-dates", "residence-filing" })
            Assert.Equal(ComparisonFactStatus.Draft, Assert.Single(comparison.Topics.SelectMany(topic => topic.Aspects)
                .Single(aspect => aspect.Id == aspectId).Facts, fact => fact.CountrySlug == "netherlands").Status);
        var nlResidenceFiling = Assert.Single(comparison.Topics.SelectMany(topic => topic.Aspects)
            .Single(aspect => aspect.Id == "residence-filing").Facts, fact => fact.CountrySlug == "netherlands");
        Assert.False(nlResidenceFiling.IsAvailable);
        Assert.Empty(nlResidenceFiling.OfficialSourceIds);

        var swedenChildBenefits = Assert.Single(comparison.Topics.SelectMany(topic => topic.Aspects)
            .Single(aspect => aspect.Id == "social-child-benefits").Facts, fact => fact.CountrySlug == "sweden");
        Assert.Equal(ComparisonFactStatus.Draft, swedenChildBenefits.Status);
        Assert.False(swedenChildBenefits.IsAvailable);
        Assert.Empty(swedenChildBenefits.OfficialSourceIds);
    }

    [Fact]
    public void ItalyCountryLocalizationIsPresentInAllLanguages()
    {
        var resources = new ResourceManager("RefugeeNavigator.Web.Resources.Localization.SharedResource", typeof(SharedResource).Assembly);
        Assert.Equal("Italy", resources.GetString("Country.Italy", CultureInfo.GetCultureInfo("en")));
        Assert.Equal("in Italy", resources.GetString("Country.Italy.In", CultureInfo.GetCultureInfo("en")));
        Assert.Equal("Італія", resources.GetString("Country.Italy", CultureInfo.GetCultureInfo("uk")));
        Assert.Equal("в Італії", resources.GetString("Country.Italy.In", CultureInfo.GetCultureInfo("uk")));
        Assert.Equal("Италия", resources.GetString("Country.Italy", CultureInfo.GetCultureInfo("ru")));
        Assert.Equal("в Италии", resources.GetString("Country.Italy.In", CultureInfo.GetCultureInfo("ru")));
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
