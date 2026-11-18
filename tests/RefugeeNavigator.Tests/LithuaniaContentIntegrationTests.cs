using System.Globalization;
using System.Resources;
using System.Text.RegularExpressions;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Comparison;
using RefugeeNavigator.Web.Content;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Tests;

public sealed class LithuaniaContentIntegrationTests
{
    private static readonly Guid CountryId = Guid.Parse("10000000-0000-0000-0000-000000000013");
    private static readonly IReadOnlyDictionary<string, (Guid Id, string Category)> Articles = new Dictionary<string, (Guid, string)>
    {
        ["LT-TP-01"] = (Guid.Parse("115cbf25-5ece-5cc9-8bde-c3dabcbda47f"), "temporary-protection"),
        ["LT-TP-02"] = (Guid.Parse("581150e2-3862-5a79-9e56-ad1f3fdd8424"), "temporary-protection"),
        ["LT-EMP-01"] = (Guid.Parse("26051216-75b3-5564-a2ec-9a0723bcfec6"), "employment"),
        ["LT-EMP-02"] = (Guid.Parse("5cdd5634-6d7a-5c02-aa2a-02f4758e6d2d"), "employment"),
        ["LT-HEALTH-01"] = (Guid.Parse("42863927-2979-5769-8092-c9393a0ea916"), "healthcare"),
        ["LT-HOUSING-01"] = (Guid.Parse("28169e82-6320-5f00-9b34-37e77f3c7d26"), "housing"),
        ["LT-EDU-01"] = (Guid.Parse("bacfcf91-7365-5c28-a240-b6db41c1aa0f"), "education"),
        ["LT-SOC-01"] = (Guid.Parse("c971e527-d7ca-5dff-b739-02e6df1f98e1"), "social-assistance")
    };
    private static readonly IReadOnlyDictionary<string, Guid> ArticleByAspect = new Dictionary<string, Guid>
    {
        ["protection-scheme"] = Articles["LT-TP-01"].Id, ["protection-application"] = Articles["LT-TP-01"].Id, ["protection-dates"] = Articles["LT-TP-01"].Id,
        ["residence-routes"] = Articles["LT-TP-02"].Id, ["residence-conditions"] = Articles["LT-TP-02"].Id, ["residence-filing"] = Articles["LT-TP-02"].Id,
        ["employment-vacancies"] = Articles["LT-EMP-01"].Id, ["employment-institutions"] = Articles["LT-EMP-01"].Id, ["employment-assistance-conditions"] = Articles["LT-EMP-01"].Id,
        ["work-permission"] = Articles["LT-EMP-02"].Id, ["work-employer-duties"] = Articles["LT-EMP-02"].Id, ["work-minimum-pay"] = Articles["LT-EMP-02"].Id,
        ["healthcare-access"] = Articles["LT-HEALTH-01"].Id, ["healthcare-proof"] = Articles["LT-HEALTH-01"].Id, ["healthcare-urgent"] = Articles["LT-HEALTH-01"].Id,
        ["housing-initial"] = Articles["LT-HOUSING-01"].Id, ["housing-private-rental"] = Articles["LT-HOUSING-01"].Id, ["housing-financial-support"] = Articles["LT-HOUSING-01"].Id,
        ["education-school-enrolment"] = Articles["LT-EDU-01"].Id, ["education-pupil-language"] = Articles["LT-EDU-01"].Id, ["education-higher-admission"] = Articles["LT-EDU-01"].Id,
        ["social-subsistence"] = Articles["LT-SOC-01"].Id, ["social-application"] = Articles["LT-SOC-01"].Id, ["social-child-benefits"] = Articles["LT-SOC-01"].Id
    };
    private static readonly Lazy<Task<CatalogData>> Catalog = new(() => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());
    private static readonly Lazy<Task<ComparisonFactCatalog>> Facts = new(async () =>
        await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(await Catalog.Value));

    [Fact]
    public async Task LithuaniaHasEightVerifiedArticlesAndIsVisibleInProduction()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "lithuania");
        Assert.Equal(CountryId, country.Id);
        Assert.Equal("Lithuania", country.Name);
        Assert.Equal(19, catalog.Countries.Count);
        Assert.Equal(152, catalog.InformationEntries.Count);
        Assert.Equal(456, catalog.InformationEntryTranslations.Count);
        Assert.Equal(456, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Outdated));
        var entries = catalog.InformationEntries.Where(item => item.CountryId == CountryId).ToArray();
        Assert.Equal(8, entries.Length);
        Assert.Equal(Articles.Values.Select(x => x.Id).Order().ToArray(), entries.Select(x => x.Id).Order().ToArray());
        var counts = entries.GroupBy(e => catalog.Categories.Single(c => c.Id == e.CategoryId).Slug).ToDictionary(g => g.Key, g => g.Count());
        Assert.Equal(2, counts["temporary-protection"]); Assert.Equal(2, counts["employment"]);
        Assert.Equal(1, counts["healthcare"]); Assert.Equal(1, counts["housing"]); Assert.Equal(1, counts["education"]); Assert.Equal(1, counts["social-assistance"]);
        foreach (var entry in entries)
        {
            var translations = catalog.InformationEntryTranslations.Where(t => t.InformationEntryId == entry.Id).ToArray();
            Assert.Equal(3, translations.Length);
            Assert.All(translations, t => { Assert.Equal(VerificationStatus.Verified, t.VerificationStatus); Assert.Equal(new DateTimeOffset(2026, 11, 17, 0, 0, 0, TimeSpan.Zero), t.LastUpdatedAt); });
        }
        Assert.Contains(CountryVisibility.GetVisibleCountries(catalog, false), c => c.Slug == "lithuania");
        Assert.Contains(CountryVisibility.GetVisibleCountries(catalog, true), c => c.Slug == "lithuania");
        Assert.Equal(19, CountryVisibility.GetVisibleCountries(catalog, false).Count);
        Assert.Equal(19, CountryVisibility.GetVisibleCountries(catalog, true).Count);
    }

    [Fact]
    public async Task LithuaniaTranslationsAndSeventyOneOwnedSourcesMatchDraftsExactly()
    {
        var catalog = await Catalog.Value;
        var root = Path.Combine(FindRepositoryRoot(), "docs", "content-drafts", "lithuania-v1");
        Assert.Equal(8, Directory.GetFiles(root, "LT-*.md").Length);
        var ids = Articles.Values.Select(x => x.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(s => ids.Contains(s.InformationEntryId)).ToArray();
        Assert.Equal(71, sources.Length);
        Assert.Equal(1503, catalog.OfficialSources.Count);
        Assert.Equal(71, sources.Select(s => s.Id).Distinct().Count());
        Assert.Equal(catalog.OfficialSources.Count, catalog.OfficialSources.Select(s => s.Id).Distinct().Count());
        var historicalDeclarationId = Guid.Parse("c8dedc7c-e90b-5d75-8d9a-1099a67d17a1");
        Assert.All(sources, s => { Assert.Contains(s.InformationEntryId, ids); Assert.Equal("https", s.Url.Scheme); Assert.Equal(historicalDeclarationId == s.Id ? new DateTimeOffset(2026, 11, 9, 0, 0, 0, TimeSpan.Zero) : new DateTimeOffset(2026, 11, 17, 0, 0, 0, TimeSpan.Zero), s.LastCheckedAt); });
        Assert.All(sources.GroupBy(s => (s.InformationEntryId, s.Url.AbsoluteUri)), g => Assert.Single(g));
        var sourceById = catalog.OfficialSources.ToDictionary(s => s.Id);
        var inlineCitationCount = 0;
        foreach (var (code, expected) in Articles)
        {
            var draft = await File.ReadAllTextAsync(Path.Combine(root, code + ".md"));
            Assert.Contains($"Article ID: `{expected.Id}`", draft);
            var sourceLines = Regex.Matches(draft, @"(?m)^- `[^`]+` → `([^`]+)` — (.*)$").Select(m => (Id: Guid.Parse(m.Groups[1].Value), Title: m.Groups[2].Value.Split(" (research source ", 2)[0], Url: Regex.Match(m.Groups[2].Value, @"https://[^)\s]+").Value.TrimEnd(']'))).ToArray();
            var owned = sources.Where(s => s.InformationEntryId == expected.Id).ToDictionary(s => s.Id);
            Assert.Equal(sourceLines.Length, owned.Count);
            foreach (var (sourceId, title, url) in sourceLines) { Assert.True(owned.TryGetValue(sourceId, out var source)); Assert.Equal(title, source!.Title); Assert.Equal(url, source.Url.AbsoluteUri); Assert.Equal("https", source.Url.Scheme); }
            foreach (var (language, section) in new[] { ("en", "EN"), ("uk", "UK"), ("ru", "RU") })
            {
                var m = Regex.Match(draft, $@"(?ms)^## {section}\s+Title: (?<title>.*?)\s+Summary: (?<summary>.*?)\s+Content:\s+(?<content>.*?)(?=^### Claim coverage and editorial exclusions$|^## (?:EN|UK|RU)$|^## Claim coverage and editorial exclusions$|^## Editorial source set)");
                Assert.True(m.Success);
                var translation = Assert.Single(catalog.InformationEntryTranslations, t => t.InformationEntryId == expected.Id && t.LanguageCode == language);
                Assert.Equal(m.Groups["title"].Value.Trim(), translation.Title); Assert.Equal(m.Groups["summary"].Value.Trim(), translation.Summary); Assert.Equal(Normalize(m.Groups["content"].Value), Normalize(translation.Content));
                var tokens = Regex.Matches(translation.Content, @"\[\[source:([^|\]]+)\|([^\]]+)\]\]");
                inlineCitationCount += tokens.Count;
                foreach (Match token in tokens)
                {
                    var sourceId = Guid.Parse(token.Groups[1].Value); Assert.Contains(sourceId, owned.Keys); Assert.Equal(expected.Id, sourceById[sourceId].InformationEntryId);
                }
            }
        }
        Assert.Equal(228, inlineCitationCount);
    }

    [Fact]
    public async Task LithuaniaHasTwentyFourApprovedFactsWithOwnedSourcesAndExistingHoldsRemain()
    {
        var catalog = await Catalog.Value; var comparison = await Facts.Value; CountryComparisonTopicMap.Validate(catalog);
        Assert.Contains("lithuania", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Equal(152, CountryComparisonTopicMap.All.SelectMany(t => t.EntryIds.Values).Distinct().Count());
        var mapped = new Dictionary<string, Guid>
        {
            ["Compare.Topic.TemporaryProtectionApplication"] = Articles["LT-TP-01"].Id, ["Compare.Topic.ResidenceAfterProtection"] = Articles["LT-TP-02"].Id,
            ["Compare.Topic.FindingEmployment"] = Articles["LT-EMP-01"].Id, ["Compare.Topic.WorkAuthorizationRights"] = Articles["LT-EMP-02"].Id,
            ["Compare.Topic.Healthcare"] = Articles["LT-HEALTH-01"].Id, ["Compare.Topic.Housing"] = Articles["LT-HOUSING-01"].Id,
            ["Compare.Topic.EducationLanguage"] = Articles["LT-EDU-01"].Id, ["Compare.Topic.SocialAssistance"] = Articles["LT-SOC-01"].Id
        };
        foreach (var topic in CountryComparisonTopicMap.All) { Assert.Equal(19, topic.EntryIds.Count); Assert.Equal(mapped[topic.ResourceKey], CountryComparisonTopicMap.EntryIdFor(topic, "lithuania")); }
        var all = comparison.Topics.SelectMany(t => t.Aspects).SelectMany(a => a.Facts).ToArray(); var lt = all.Where(f => f.CountrySlug == "lithuania").ToArray();
        Assert.Equal(456, all.Length); Assert.Equal(453, all.Count(f => f.IsAvailable)); Assert.Equal(3, all.Count(f => !f.IsAvailable));
        Assert.Equal(453, all.Count(f => f.Status == ComparisonFactStatus.Approved)); Assert.Equal(3, all.Count(f => f.Status == ComparisonFactStatus.Draft));
        Assert.Equal(24, lt.Length); Assert.All(lt, f => { Assert.Equal(ComparisonFactStatus.Approved, f.Status); Assert.True(f.IsAvailable); Assert.Equal(ArticleByAspect[Assert.Single(comparison.Topics.SelectMany(t => t.Aspects).Where(a => a.Facts.Contains(f)).Select(a => a.Id))], f.InformationEntryId); Assert.Equal(new[] { "en", "ru", "uk" }, f.Values.Keys.Order(StringComparer.Ordinal).ToArray()); Assert.All(f.Values.Values, v => Assert.False(string.IsNullOrWhiteSpace(v))); Assert.NotEmpty(f.OfficialSourceIds); Assert.All(f.OfficialSourceIds, id => Assert.Equal(f.InformationEntryId, catalog.OfficialSources.Single(s => s.Id == id).InformationEntryId)); });
        var nl = all.Where(f => f.CountrySlug == "netherlands").ToArray(); Assert.Equal(23, nl.Count(f => f.Status == ComparisonFactStatus.Approved)); Assert.Equal(1, nl.Count(f => f.Status == ComparisonFactStatus.Draft));
        foreach (var aspect in new[] { "protection-scheme", "protection-application", "protection-dates" }) Assert.Equal(ComparisonFactStatus.Approved, Assert.Single(comparison.Topics.SelectMany(t => t.Aspects).Single(a => a.Id == aspect).Facts, f => f.CountrySlug == "netherlands").Status);
        Assert.Equal(ComparisonFactStatus.Draft, Assert.Single(comparison.Topics.SelectMany(t => t.Aspects).Single(a => a.Id == "residence-filing").Facts, f => f.CountrySlug == "netherlands").Status);
        var nlFiling = Assert.Single(comparison.Topics.SelectMany(t => t.Aspects).Single(a => a.Id == "residence-filing").Facts, f => f.CountrySlug == "netherlands"); Assert.False(nlFiling.IsAvailable); Assert.Empty(nlFiling.OfficialSourceIds);
        var sweden = Assert.Single(comparison.Topics.SelectMany(t => t.Aspects).Single(a => a.Id == "social-child-benefits").Facts, f => f.CountrySlug == "sweden"); Assert.Equal(ComparisonFactStatus.Draft, sweden.Status); Assert.False(sweden.IsAvailable); Assert.Empty(sweden.OfficialSourceIds);
        var ltAspects = comparison.Topics.SelectMany(t => t.Aspects).Where(a => a.Facts.Any(f => f.CountrySlug == "lithuania")).Select(a => a.Id).ToArray(); Assert.Equal(24, ltAspects.Length); Assert.Equal(ArticleByAspect.Keys.Order(StringComparer.Ordinal), ltAspects.Order(StringComparer.Ordinal));
    }

    [Fact]
    public async Task LithuaniaRefreshCriticalRulesAndSourceRemediationAreIntegrated()
    {
        var catalog = await Catalog.Value;
        var facts = await Facts.Value;
        var translations = catalog.InformationEntryTranslations.ToDictionary(t => (t.InformationEntryId, t.LanguageCode));
        string Content(string article, string language) => translations[(Articles[article].Id, language)].Content;

        var tp = new Dictionary<string, string>
        {
            ["en"] = Content("LT-TP-01", "en"), ["uk"] = Content("LT-TP-01", "uk"), ["ru"] = Content("LT-TP-01", "ru")
        };
        Assert.Contains("5 August 2026", tp["en"]); Assert.Contains("4 August 2026", tp["en"]); Assert.Contains("any EU Member State", tp["en"]); Assert.Contains("same Member State", tp["en"]);
        Assert.Contains("5 серпня 2026 року", tp["uk"]); Assert.Contains("4 серпня 2026 року", tp["uk"]); Assert.Contains("будь-якій державі-члені ЄС", tp["uk"]); Assert.Contains("тій самій державі-члені", tp["uk"]);
        Assert.Contains("5 августа 2026 года", tp["ru"]); Assert.Contains("4 августа 2026 года", tp["ru"]); Assert.Contains("любом государстве-члене ЕС", tp["ru"]); Assert.Contains("том же государстве-члене", tp["ru"]);
        Assert.Contains("digital temporary residence permit", tp["en"]); Assert.Contains("4 March 2028", tp["en"]); Assert.Contains("does not establish the expiry printed on every person’s document", tp["en"]);
        Assert.Contains("replacement", tp["en"]); Assert.Contains("not a complete first-application checklist", tp["en"]);

        var residenceFiling = Assert.Single(facts.Topics.SelectMany(t => t.Aspects).Single(a => a.Id == "residence-filing").Facts, f => f.CountrySlug == "lithuania");
        Assert.Contains("From 1 November 2026", residenceFiling.Values["en"]); Assert.Contains("does not guarantee approval", residenceFiling.Values["en"]);
        var work = Content("LT-EMP-02", "en");
        Assert.Contains("may start working after applying for TP", work); Assert.Contains("This specific trigger is not arrival", work);
        Assert.Contains("1-SD", work); Assert.Contains("one working day", work); Assert.Contains("SAM", work); Assert.Contains("2-SD", work);
        var employerDuties = Assert.Single(facts.Topics.SelectMany(t => t.Aspects).Single(a => a.Id == "work-employer-duties").Facts, f => f.CountrySlug == "lithuania"); Assert.Contains("15th", employerDuties.Values["en"]);
        var minimumPay = Assert.Single(facts.Topics.SelectMany(t => t.Aspects).Single(a => a.Id == "work-minimum-pay").Facts, f => f.CountrySlug == "lithuania");
        Assert.Contains("€1,153", minimumPay.Values["en"]); Assert.Contains("€7.05", minimumPay.Values["en"]); Assert.Contains("unqualified", minimumPay.Values["en"]);
        Assert.Contains("€1,245", minimumPay.Values["en"]); Assert.Contains("€7.61", minimumPay.Values["en"]); Assert.Contains("1 January 2027", minimumPay.Values["en"]);
        Assert.Contains("separate from route-specific immigration salary tests", Content("LT-EMP-02", "en"));
        Assert.Contains("€1 153", minimumPay.Values["uk"]); Assert.Contains("€7,05", minimumPay.Values["uk"]); Assert.Contains("€1 245", minimumPay.Values["uk"]); Assert.Contains("€7,61", minimumPay.Values["uk"]);
        Assert.Contains("€1 153", minimumPay.Values["ru"]); Assert.Contains("€7,05", minimumPay.Values["ru"]); Assert.Contains("€1 245", minimumPay.Values["ru"]); Assert.Contains("€7,61", minimumPay.Values["ru"]);

        var health = Content("LT-HEALTH-01", "en");
        Assert.Contains("not automatically established for every Ukrainian or every adult TP holder", health);
        Assert.Contains("generic pending receipt alone", health); Assert.Contains("113", health); Assert.Contains("112", health);
        var housing = Content("LT-HOUSING-01", "en");
        Assert.Contains("generic receipt", housing); Assert.Contains("six months", housing); Assert.Contains("not a guarantee of an available bed", housing);
        Assert.Contains("no more than the first month", housing); Assert.Contains("do not establish a general statutory security-deposit cap", housing);
        var education = Content("LT-EDU-01", "en");
        Assert.Contains("until age 16", education); Assert.Contains("generally begins", education); Assert.Contains("normally starts", education);
        Assert.Contains("generic receipt alone", education);
        Assert.Contains("general receipt alone", Content("LT-SOC-01", "en"));
        Assert.Contains("unrelated receipt", work);

        var subsistence = Assert.Single(facts.Topics.SelectMany(t => t.Aspects).Single(a => a.Id == "social-subsistence").Facts, f => f.CountrySlug == "lithuania");
        Assert.Contains("256.30", subsistence.Values["en"]); Assert.Contains("256,30", subsistence.Values["uk"]); Assert.Contains("256,30", subsistence.Values["ru"]);
        Assert.Contains("assessment threshold, not the amount paid", subsistence.Values["en"]);
        var children = Assert.Single(facts.Topics.SelectMany(t => t.Aspects).Single(a => a.Id == "social-child-benefits").Facts, f => f.CountrySlug == "lithuania");
        Assert.Contains("€129.50", children.Values["en"]); Assert.Contains("€76.22", children.Values["en"]); Assert.Contains("conditional", children.Values["en"]);
        Assert.Contains("€129,50", children.Values["uk"]); Assert.Contains("€76,22", children.Values["uk"]); Assert.Contains("€129,50", children.Values["ru"]); Assert.Contains("€76,22", children.Values["ru"]);

        var sources = catalog.OfficialSources;
        var removed = Guid.Parse("fb747279-3f8b-5001-99cb-3478c6e17f5c");
        var historical = Guid.Parse("c8dedc7c-e90b-5d75-8d9a-1099a67d17a1");
        var healthLaw = Guid.Parse("928b6d64-fcd6-5f98-9aa0-36e91eda9fc8");
        var newHousing = Guid.Parse("f8b8d6e4-d750-544d-a112-0fbef0a3658f");
        Assert.DoesNotContain(sources, s => s.Id == removed);
        var historicalSource = Assert.Single(sources, s => s.Id == historical);
        Assert.StartsWith("Historical (2021-03-05)", historicalSource.Title);
        Assert.Equal(Articles["LT-HOUSING-01"].Id, historicalSource.InformationEntryId);
        Assert.Contains(historical.ToString(), housing); Assert.Contains("36ff51ff-ff51-529a-9424-ce4b1c325a6e", housing);
        var insuranceLaw = Assert.Single(sources, s => s.Id == healthLaw);
        Assert.Contains("Health Insurance Law", insuranceLaw.Title);
        var housingGuide = Assert.Single(sources, s => s.Id == newHousing);
        Assert.Equal(Articles["LT-HOUSING-01"].Id, housingGuide.InformationEntryId);
        Assert.Equal(new DateTimeOffset(2026, 11, 17, 0, 0, 0, TimeSpan.Zero), housingGuide.LastCheckedAt);
        Assert.Contains(newHousing.ToString(), housing);
        Assert.DoesNotContain(removed.ToString(), string.Join("\n", catalog.InformationEntryTranslations.Where(t => Articles.Values.Any(a => a.Id == t.InformationEntryId)).Select(t => t.Content)));
        var housingFact = Assert.Single(facts.Topics.SelectMany(t => t.Aspects).Single(a => a.Id == "housing-financial-support").Facts, f => f.CountrySlug == "lithuania");
        Assert.Contains(newHousing, housingFact.OfficialSourceIds); Assert.DoesNotContain(removed, housingFact.OfficialSourceIds);
        Assert.All(facts.Topics.SelectMany(t => t.Aspects).SelectMany(a => a.Facts).Where(f => f.CountrySlug == "lithuania"),
            fact => Assert.All(fact.OfficialSourceIds, sourceId => Assert.Contains(sources, s => s.Id == sourceId && s.InformationEntryId == fact.InformationEntryId)));
    }

    [Fact]
    public void LithuaniaCountryLocalizationIsPresentInAllLanguages()
    {
        var resources = new ResourceManager("RefugeeNavigator.Web.Resources.Localization.SharedResource", typeof(SharedResource).Assembly);
        Assert.Equal("Lithuania", resources.GetString("Country.Lithuania", CultureInfo.GetCultureInfo("en"))); Assert.Equal("Lithuania", resources.GetString("Country.Lithuania.In", CultureInfo.GetCultureInfo("en")));
        Assert.Equal("Литва", resources.GetString("Country.Lithuania", CultureInfo.GetCultureInfo("uk"))); Assert.Equal("у Литві", resources.GetString("Country.Lithuania.In", CultureInfo.GetCultureInfo("uk")));
        Assert.Equal("Литва", resources.GetString("Country.Lithuania", CultureInfo.GetCultureInfo("ru"))); Assert.Equal("в Литве", resources.GetString("Country.Lithuania.In", CultureInfo.GetCultureInfo("ru")));
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !Directory.Exists(Path.Combine(directory.FullName, ".git"))) directory = directory.Parent;
        return directory?.FullName ?? throw new DirectoryNotFoundException();
    }
    private static string Normalize(string value) => Regex.Replace(value.Trim(), @"\s+", " ");
}
