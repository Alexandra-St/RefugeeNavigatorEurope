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
        ["HU-TP-01"] = (Guid.Parse("1bb2a2b8-cfe0-5595-8155-138443fade17"), "temporary-protection", 10),
        ["HU-TP-02"] = (Guid.Parse("914418c6-15f8-527c-85dd-be5bda5594fc"), "temporary-protection", 18),
        ["HU-EMP-01"] = (Guid.Parse("0e65e68f-5c42-58d5-b7b3-e941896bb532"), "employment", 14),
        ["HU-EMP-02"] = (Guid.Parse("4dde28d5-8f4c-5503-926b-eac8789b4e9f"), "employment", 15),
        ["HU-HEALTH-01"] = (Guid.Parse("8cb405ab-c562-5c22-81ad-0fd93ef50202"), "healthcare", 14),
        ["HU-HOUSING-01"] = (Guid.Parse("bbdf7f5c-da1d-59ad-9515-5bd8499e2332"), "housing", 11),
        ["HU-EDU-01"] = (Guid.Parse("e0fd251b-cf52-5a45-9a8d-b07e91580985"), "education", 15),
        ["HU-SOC-01"] = (Guid.Parse("f34f7c15-d879-5ba8-81cf-65f7e9ca416f"), "social-assistance", 15)
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
        Assert.Equal(19, CountryComparisonTopicMap.SupportedCountrySlugs.Count);
        Assert.All(CountryComparisonTopicMap.All, topic =>
        {
            Assert.Equal(19, topic.EntryIds.Count);
            Assert.Equal(expectedMappings[topic.ResourceKey], CountryComparisonTopicMap.EntryIdFor(topic, "hungary"));
        });
        var comparison = await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(catalog);
        var allFacts = comparison.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).ToArray();
        Assert.Equal(456, allFacts.Length);
        Assert.Equal(454, allFacts.Count(fact => fact.IsAvailable));
        Assert.Equal(2, allFacts.Count(fact => !fact.IsAvailable));
        Assert.Equal(454, allFacts.Count(fact => fact.Status == ComparisonFactStatus.Approved));
        Assert.Equal(2, allFacts.Count(fact => fact.Status == ComparisonFactStatus.Draft));
        var hungaryFacts = allFacts.Where(fact => fact.CountrySlug == "hungary").ToArray();
        Assert.Equal(24, hungaryFacts.Length);
        Assert.All(hungaryFacts, fact =>
        {
            if (!fact.IsAvailable) { Assert.Equal(ComparisonFactStatus.Draft, fact.Status); Assert.Empty(fact.OfficialSourceIds); Assert.Equal("Not established in the reviewed sources.", fact.Values["en"]); Assert.Equal("Не встановлено за перевіреними джерелами.", fact.Values["uk"]); Assert.Equal("Не установлено по проверенным источникам.", fact.Values["ru"]); }
            else Assert.Equal(ComparisonFactStatus.Approved, fact.Status);
            Assert.Equal(expectedMappings[comparison.Topics.Single(topic => topic.Aspects.Any(aspect => aspect.Facts.Contains(fact))).TopicResourceKey], fact.InformationEntryId);
            Assert.Equal(new[] { "en", "ru", "uk" }, fact.Values.Keys.Order(StringComparer.Ordinal).ToArray());
            Assert.All(fact.Values.Values, value => Assert.False(string.IsNullOrWhiteSpace(value)));
            if (fact.IsAvailable) Assert.NotEmpty(fact.OfficialSourceIds);
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
            Assert.Equal(new DateTimeOffset(2026, 11, 16, 0, 0, 0, TimeSpan.Zero), item.LastUpdatedAt);
        });

        var sources = catalog.OfficialSources.Where(item => entries.Any(entry => entry.Id == item.InformationEntryId)).ToArray();
        Assert.Equal(112, sources.Length);
        Assert.Equal(112, sources.Select(item => item.Id).Distinct().Count());
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
                Assert.Contains(Guid.Parse("8a079563-a515-4f54-badb-9cb85a812171"), citedIds);
                Assert.Equal(owned.Length, owned.Select(source => source.Url.AbsoluteUri).Distinct(StringComparer.Ordinal).Count());
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

    [Fact]
    public async Task HungaryRefreshPreservesReviewedCriticalRulesInAllLocales()
    {
        var catalog = await Catalog.Value;
        string Content(string id, string language) => Assert.Single(catalog.InformationEntryTranslations, item => item.InformationEntryId == Guid.Parse(id) && item.LanguageCode == language).Content;
        foreach (var language in new[] { "en", "uk", "ru" })
        {
            var tp = Content("1bb2a2b8-cfe0-5595-8155-138443fade17", language);
            Assert.Contains("2026", tp);
            Assert.Contains("2028", tp);
            Assert.Contains("145/2026", tp);
            Assert.Contains(language == "en" ? "7 September 2026" : language == "uk" ? "7 вересня 2026" : "7 сентября 2026", tp);
            Assert.Contains(language == "en" ? "not current operative law" : language == "uk" ? "не чинною правовою підставою" : "не является действующим правовым основанием", tp);
            Assert.DoesNotContain("universal rule allows every active beneficiary", tp, StringComparison.OrdinalIgnoreCase);
            var employment = Content("4dde28d5-8f4c-5503-926b-eac8789b4e9f", language);
            var normalizedEmployment = employment.Replace(",", "", StringComparison.Ordinal).Replace(" ", "", StringComparison.Ordinal);
            Assert.Contains("322800", normalizedEmployment);
            Assert.Contains("373200", normalizedEmployment);
            Assert.Contains(language == "en" ? "secondary vocational qualification" : language == "uk" ? "професійної кваліфікації" : "профессиональной квалификации", employment);
            var social = Content("f34f7c15-d879-5ba8-81cf-65f7e9ca416f", language);
            Assert.Contains(language == "en" ? "22,800" : language == "uk" ? "22 800" : "22 800", social);
            Assert.Contains(language == "en" ? "13,700" : language == "uk" ? "13 700" : "13 700", social);
        }
        var comparison = await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(catalog);
        var housing = Assert.Single(comparison.Topics.SelectMany(topic => topic.Aspects).Single(aspect => aspect.Id == "housing-initial").Facts, fact => fact.CountrySlug == "hungary");
        Assert.False(housing.IsAvailable);
        Assert.Equal(ComparisonFactStatus.Draft, housing.Status);
        Assert.Empty(housing.OfficialSourceIds);
        var sources = catalog.OfficialSources.Where(source => source.InformationEntryId == Articles["HU-TP-01"].Id).ToArray();
        Assert.Contains(sources, source => source.Id == Guid.Parse("8a079563-a515-4f54-badb-9cb85a812171"));
        Assert.Contains(sources, source => source.Id == Guid.Parse("00b903f1-1bf2-44ad-b105-63936eaa5d42"));
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
