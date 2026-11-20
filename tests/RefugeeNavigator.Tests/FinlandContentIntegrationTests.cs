using System.Globalization;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Comparison;
using RefugeeNavigator.Web.Components;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Content;
using RefugeeNavigator.Web.Guides;
using RefugeeNavigator.Web.Localization;
using RefugeeNavigator.Web.Search;

namespace RefugeeNavigator.Tests;

public sealed class FinlandContentIntegrationTests
{
    private static readonly Guid CountryId = Guid.Parse("10000000-0000-0000-0000-000000000016");
    private static readonly IReadOnlyDictionary<string, (Guid Id, string Category)> Articles = new Dictionary<string, (Guid, string)>
    {
        ["FI-TP-01"] = (Guid.Parse("ffd32dca-56d8-5dcf-b995-7522a159010a"), "temporary-protection"),
        ["FI-TP-02"] = (Guid.Parse("344c5e31-45a9-5f22-8d4a-4a58d8fe8243"), "temporary-protection"),
        ["FI-EMP-01"] = (Guid.Parse("53743a68-bacb-5344-9aee-559e146cab54"), "employment"),
        ["FI-EMP-02"] = (Guid.Parse("0abffa8c-cd42-571e-97f3-e160afad880c"), "employment"),
        ["FI-HEALTH-01"] = (Guid.Parse("ea41d1e7-f921-50af-9886-2232d9fb06ef"), "healthcare"),
        ["FI-HOUSING-01"] = (Guid.Parse("61b331e0-76bb-58f4-91e7-e5944ba77e31"), "housing"),
        ["FI-EDU-01"] = (Guid.Parse("2a2094da-e321-50bd-8a45-bf3988be06c9"), "education"),
        ["FI-SOC-01"] = (Guid.Parse("51691974-1c20-5b25-9572-66624984a52f"), "social-assistance")
    };
    private static readonly IReadOnlyDictionary<string, int> ExpectedSourceCounts = new Dictionary<string, int>
    {
        ["FI-TP-01"] = 10,
        ["FI-TP-02"] = 14,
        ["FI-EMP-01"] = 10,
        ["FI-EMP-02"] = 10,
        ["FI-HEALTH-01"] = 10,
        ["FI-HOUSING-01"] = 14,
        ["FI-EDU-01"] = 16,
        ["FI-SOC-01"] = 17
    };
    private static readonly ResourceManager Resources = new(
        "RefugeeNavigator.Web.Resources.Localization.SharedResource",
        typeof(SharedResource).Assembly);
    private static readonly Lazy<Task<CatalogData>> Catalog = new(
        () => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());
    private static readonly Lazy<Task<ComparisonFactCatalog>> Facts = new(async () =>
        await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json"))
            .LoadAsync(await Catalog.Value));

    [Fact]
    public async Task FinlandHasEightArticlesAndExpectedCatalogCounts()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "finland");
        Assert.Equal(CountryId, country.Id);
        Assert.Equal("Finland", country.Name);
        Assert.Equal(19, catalog.Countries.Count);
        Assert.Equal(152, catalog.InformationEntries.Count);
        Assert.Equal(456, catalog.InformationEntryTranslations.Count);
        Assert.Equal(456, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Outdated));

        var entries = catalog.InformationEntries.Where(item => item.CountryId == CountryId).ToArray();
        Assert.Equal(8, entries.Length);
        Assert.Equal(24, catalog.InformationEntryTranslations.Count(item => entries.Any(entry => entry.Id == item.InformationEntryId)));
        Assert.Equal(Articles.Values.Select(item => item.Id).Order().ToArray(), entries.Select(item => item.Id).Order().ToArray());
        var categoryCounts = entries
            .GroupBy(item => catalog.Categories.Single(category => category.Id == item.CategoryId).Slug)
            .ToDictionary(group => group.Key, group => group.Count());
        Assert.Equal(2, categoryCounts["employment"]);
        Assert.Equal(1, categoryCounts["healthcare"]);
        Assert.Equal(1, categoryCounts["education"]);
        Assert.Equal(1, categoryCounts["social-assistance"]);
        Assert.Equal(2, categoryCounts["temporary-protection"]);
        Assert.Equal(1, categoryCounts["housing"]);

        foreach (var (articleCode, expected) in Articles)
        {
            var entry = Assert.Single(entries, item => item.Id == expected.Id);
            Assert.Equal(expected.Category, catalog.Categories.Single(item => item.Id == entry.CategoryId).Slug);
            var translations = catalog.InformationEntryTranslations.Where(item => item.InformationEntryId == expected.Id).ToArray();
            Assert.Equal(new[] { "en", "ru", "uk" }, translations.Select(item => item.LanguageCode).Order(StringComparer.Ordinal).ToArray());
            Assert.All(translations, item =>
            {
                Assert.Equal(VerificationStatus.Verified, item.VerificationStatus);
                Assert.Equal(new DateTimeOffset(2026, 11, 17, 0, 0, 0, TimeSpan.Zero), item.LastUpdatedAt);
            });
        }

        Assert.Equal(19, CountryComparisonTopicMap.SupportedCountrySlugs.Count);
        Assert.Contains("ireland", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Contains("france", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.All(CountryComparisonTopicMap.All, topic => Assert.Equal(19, topic.EntryIds.Count));
        var allFacts = (await Facts.Value).Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).ToArray();
        Assert.Equal(456, allFacts.Length);
        Assert.Equal(24, allFacts.Count(fact => fact.CountrySlug == "finland"));
        Assert.Equal(72, allFacts.Where(fact => fact.CountrySlug == "finland").Sum(fact => fact.Values.Count));
        Assert.Equal(24, allFacts.Count(fact => fact.CountrySlug == "ireland"));
        Assert.Equal(24, allFacts.Count(fact => fact.CountrySlug == "france"));
    }

    [Fact]
    public async Task FinlandTranslationsAndOneHundredOneArticleOwnedSourcesMatchDraftsExactly()
    {
        var catalog = await Catalog.Value;
        Assert.Equal(1552, catalog.OfficialSources.Count);
        Assert.Equal(catalog.OfficialSources.Count, catalog.OfficialSources.Select(item => item.Id).Distinct().Count());
        var repositoryRoot = FindRepositoryRoot();
        var draftsRoot = Path.Combine(repositoryRoot, "docs", "content-drafts", "finland-v1");
        var articleIds = Articles.Values.Select(item => item.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(item => articleIds.Contains(item.InformationEntryId)).ToArray();
        Assert.Equal(101, sources.Length);
        Assert.Equal(101, sources.Select(item => item.Id).Distinct().Count());
        Assert.Equal(91, sources.Select(item => item.Url.AbsoluteUri).Distinct(StringComparer.Ordinal).Count());
        Assert.All(sources, source =>
        {
            Assert.Contains(source.InformationEntryId, articleIds);
            Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme);
            Assert.Equal(source.Id == Guid.Parse("6bb34b1e-6949-58ff-99ba-9ffa3769e03b") || source.Id == Guid.Parse("2c6d81a3-3527-5c5b-8fee-48615f8a7639") || source.Id == Guid.Parse("cd369471-8188-5489-83e8-4802155b2617") || source.Id == Guid.Parse("0f5f6d43-b764-5cc5-9813-3e86e9ccb296")
                ? new DateTimeOffset(2026, 11, 17, 0, 0, 0, TimeSpan.Zero)
                : new DateTimeOffset(2026, 11, 11, 0, 0, 0, TimeSpan.Zero), source.LastCheckedAt);
        });
        Assert.All(catalog.OfficialSources
            .GroupBy(item => (item.InformationEntryId, item.Title, item.Url.AbsoluteUri)), group => Assert.Single(group));
        var removedSourceIds = new[]
        {
            Guid.Parse("a8f043b1-b76c-532c-8d62-16a988efb1cd"),
            Guid.Parse("0d535e37-f80b-59c8-9a35-57038c1d876b"),
            Guid.Parse("d5bed9b1-7eb7-5223-a1d1-c56b705c712e")
        };
        Assert.DoesNotContain(catalog.OfficialSources, source => removedSourceIds.Contains(source.Id));
        Assert.DoesNotContain(catalog.InformationEntryTranslations.SelectMany(item =>
            Regex.Matches(item.Content, @"\[\[source:([^|\]]+)\|").Select(match => match.Groups[1].Value)),
            sourceId => removedSourceIds.Contains(Guid.Parse(sourceId)));

        var sourceById = catalog.OfficialSources.ToDictionary(item => item.Id);
        foreach (var (articleCode, expected) in Articles)
        {
            var draft = await File.ReadAllTextAsync(Path.Combine(draftsRoot, articleCode + ".md"));
            Assert.Contains($"Article ID: `{expected.Id}`", draft);
            var sourceMatches = Regex.Matches(draft,
                @"(?m)^- `(?<key>FI-[^`]+)` → `(?<id>[^`]+)` — (?<title>.*?) \(research source .*?; \[official source\]\((?<url>https://[^)]+)\)\)$");
            var ownedSources = sources.Where(item => item.InformationEntryId == expected.Id).ToDictionary(item => item.Id);
            Assert.Equal(ExpectedSourceCounts[articleCode], ownedSources.Count);
            Assert.All(ownedSources.Keys, sourceId => Assert.Contains(sourceId.ToString(), draft, StringComparison.OrdinalIgnoreCase));
            foreach (Match sourceMatch in sourceMatches)
            {
                var sourceId = Guid.Parse(sourceMatch.Groups["id"].Value);
                Assert.True(ownedSources.TryGetValue(sourceId, out var source));
                Assert.Equal(expected.Id, source!.InformationEntryId);
                Assert.Equal(sourceMatch.Groups["url"].Value, source.Url.AbsoluteUri);
            }

            foreach (var (language, section) in new[] { ("en", "EN"), ("uk", "UK"), ("ru", "RU") })
            {
                var match = Regex.Match(draft,
                    $@"(?ms)^## {section}\s+Title: (?<title>.*?)\s+Summary: (?<summary>.*?)\s+Content:\s+(?<content>.*?)(?=^### Claim coverage and editorial exclusions$)");
                Assert.True(match.Success, $"Missing {section} in {articleCode}.");
                var translation = Assert.Single(catalog.InformationEntryTranslations,
                    item => item.InformationEntryId == expected.Id && item.LanguageCode == language);
                Assert.Equal(match.Groups["title"].Value.Trim(), translation.Title);
                Assert.Equal(match.Groups["summary"].Value.Trim(), translation.Summary);
                Assert.Equal(match.Groups["content"].Value.Trim(), translation.Content);
                var tokens = Regex.Matches(translation.Content, @"\[\[source:([^|\]]+)\|([^\]]+)\]\]");
                Assert.NotEmpty(tokens);
                foreach (Match token in tokens)
                {
                    var sourceId = Guid.Parse(token.Groups[1].Value);
                    Assert.True(ownedSources.ContainsKey(sourceId), $"{articleCode} {language} cites non-owned source {sourceId}.");
                    Assert.Equal(expected.Id, sourceById[sourceId].InformationEntryId);
                }
            }
        }
    }

    [Fact]
    public async Task FinlandRefreshPreservesReviewedLegalAndServiceBoundaries()
    {
        var catalog = await Catalog.Value;
        var english = catalog.InformationEntryTranslations
            .Where(item => Articles.Values.Any(article => article.Id == item.InformationEntryId) && item.LanguageCode == "en")
            .ToDictionary(item => item.InformationEntryId, item => item.Content);
        var allText = string.Join("\n", english.Values);
        var tp = english[Articles["FI-TP-01"].Id];
        var emp1 = english[Articles["FI-EMP-01"].Id];
        var emp2 = english[Articles["FI-EMP-02"].Id];
        var education = english[Articles["FI-EDU-01"].Id];
        var social = english[Articles["FI-SOC-01"].Id];

        Assert.Contains("EU temporary-protection scheme runs through 4 March 2028", tp);
        Assert.Contains("not an individual Finnish permit or card expiry date", tp);
        Assert.Contains("from 5 August 2026", tp);
        Assert.Contains("on or before 4 August", tp);
        Assert.Contains("took effect on 13 August 2026", tp);
        Assert.Contains("Police or Border Guard registers the application", allText);
        Assert.Contains("Migri decides", allText);
        Assert.Contains("certificate issued at registration can be used to prove your right to work", allText);
        Assert.Contains("personal identity code", tp);
        Assert.Contains("kotikunta", allText);
        Assert.Contains("eligible to apply for `kotikunta`", allText);
        Assert.DoesNotContain("three months after registering for `kotikunta`", allText, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("not an automatic conversion", allText);
        Assert.Contains("begins when police or Border Guard registers your application", emp2);
        Assert.Contains("Municipal employment areas provide local services", emp1);
        Assert.Contains("TE Offices closed on 1 January 2025", emp1);
        Assert.Contains("no universal statutory minimum wage", allText, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("not available everywhere", education);
        Assert.Contains("not automatic", social, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("116117", allText);
        Assert.Contains("except Åland", allText);
    }

    [Fact]
    public async Task FinlandSourceCatalogHasExpectedRemediationAndNoNewOwnerUrlDuplicates()
    {
        var catalog = await Catalog.Value;
        var articleIds = Articles.Values.Select(item => item.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(item => articleIds.Contains(item.InformationEntryId)).ToArray();
        var byOwnerUrl = sources.GroupBy(item => (item.InformationEntryId, item.Url.AbsoluteUri)).ToArray();
        var intendedEmploymentActPair = Assert.Single(byOwnerUrl, group =>
            group.Key.InformationEntryId == Articles["FI-EMP-02"].Id &&
            group.Key.AbsoluteUri == "https://www.finlex.fi/fi/lainsaadanto/2001/55");
        Assert.Equal(new[]
        {
            Guid.Parse("7ab4c5c4-4363-5083-9734-4a16e6d72e74"),
            Guid.Parse("e0b8e4ac-3707-5842-8fc1-63f81bbb2532")
        }, intendedEmploymentActPair.Select(item => item.Id).Order().ToArray());
        Assert.All(byOwnerUrl.Where(group => group != intendedEmploymentActPair), group => Assert.Single(group));
        var newSourceTitles = new Dictionary<Guid, string>
        {
            [Guid.Parse("6bb34b1e-6949-58ff-99ba-9ffa3769e03b")] = "Changes to the national core curricula for basic education and preparatory education from 1 August 2026",
            [Guid.Parse("2c6d81a3-3527-5c5b-8fee-48615f8a7639")] = "Persons without a valid residence permit no longer qualify for residence-based Kela benefits as of 1 April 2026",
            [Guid.Parse("cd369471-8188-5489-83e8-4802155b2617")] = "Government proposes extension of right to general social security benefit to individuals under temporary protection as of 1 April 2027",
            [Guid.Parse("0f5f6d43-b764-5cc5-9813-3e86e9ccb296")] = "Employer announcements on employment relationships"
        };
        foreach (var (id, title) in newSourceTitles)
            Assert.Equal(title, Assert.Single(catalog.OfficialSources, source => source.Id == id).Title);
        Assert.Contains(catalog.OfficialSources, source => source.Id == Guid.Parse("fa6d5f24-dab9-50d5-8c4a-500931079914"));
    }

    [Fact]
    public async Task FinlandCompareFactsMatchReviewedDraftExactly()
    {
        var facts = (await Facts.Value).Topics.SelectMany(topic => topic.Aspects).ToArray();
        var repositoryRoot = FindRepositoryRoot();
        var draft = await File.ReadAllTextAsync(Path.Combine(repositoryRoot, "docs", "content-drafts", "finland-v1", "COMPARE_DRAFT_01.md"));
        var aspectHeadings = Regex.Matches(draft, @"(?m)^### (?<id>[a-z][a-z0-9-]*)\s*$");
        var reviewedCount = 0;
        foreach (Match heading in aspectHeadings)
        {
            var aspectId = heading.Groups["id"].Value;
            var aspect = Assert.Single(facts, item => item.Id == aspectId);
            var end = heading.Index + heading.Length;
            var next = Regex.Match(draft[end..], @"(?m)^### [a-z][a-z0-9-]*\s*$");
            var body = draft[end..(next.Success ? end + next.Index : draft.Length)];
            string Value(string label) => Regex.Match(body,
                $@"(?m)^\*\*{label}:\*\*\s*(?<value>.*?)\s*$").Groups["value"].Value;
            var production = Assert.Single(aspect.Facts, item => item.CountrySlug == "finland");
            Assert.Equal(Value("InformationEntryId").Trim('`'), production.InformationEntryId.ToString());
            Assert.Equal(ComparisonFactStatus.Approved, production.Status);
            Assert.True(production.IsAvailable);
            Assert.Equal(Value("EN"), production.Values["en"]);
            Assert.Equal(Value("UK"), production.Values["uk"]);
            Assert.Equal(Value("RU"), production.Values["ru"]);
            var sourceIds = Regex.Matches(Value("OfficialSourceIds"), @"`(?<id>[0-9a-f-]{36})`")
                .Select(match => Guid.Parse(match.Groups["id"].Value)).ToArray();
            Assert.Equal(sourceIds, production.OfficialSourceIds);
            reviewedCount++;
        }
        Assert.Equal(24, reviewedCount);
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task FinlandIsVisibleInProductionAndDevelopmentCompareInAllLanguages(string language)
    {
        var catalog = await Catalog.Value;
        var categoryCounts = new Dictionary<string, int>
        {
            ["employment"] = 2,
            ["temporary-protection"] = 2,
            ["healthcare"] = 1,
            ["education"] = 1,
            ["social-assistance"] = 1,
            ["housing"] = 1
        };
        var productionHome = await RenderAsync<Home>("Production", language);
        Assert.Equal(19, Regex.Matches(productionHome.Html, "class=\"country-card\"").Count);
        Assert.Contains("href=\"/countries/finland\"", productionHome.Html);
        var productionSearch = await RenderAsync<Search>("Production", language, "/search");
        Assert.Contains("finland", SelectValues(productionSearch.Html, "country"));
        var productionOverview = await RenderAsync<CountryOverview>("Production", language, "/countries/finland",
            new() { [nameof(CountryOverview.CountrySlug)] = "finland" });
        Assert.False(productionOverview.NotFound);
        foreach (var (categorySlug, expectedCount) in categoryCounts)
        {
            var result = await RenderAsync<Category>("Production", language,
                $"/countries/finland/categories/{categorySlug}", new()
                {
                    [nameof(Category.CountrySlug)] = "finland",
                    [nameof(Category.CategorySlug)] = categorySlug
                });
            Assert.False(result.NotFound);
            Assert.Equal(expectedCount, Regex.Matches(result.Html, "class=\"information-entry\"").Count);
        }
        var productionResults = await RenderAsync<Search>("Production", language, "/search?country=finland");
        Assert.Equal(8, Regex.Matches(productionResults.Html, "class=\"result-card\"").Count);
        var productionCompare = await RenderAsync<Compare>("Production", language, "/compare");
        Assert.Contains("finland", SelectValues(productionCompare.Html, "country1"));
        Assert.Contains("finland", SelectValues(productionCompare.Html, "country2"));
        Assert.Contains("ireland", SelectValues(productionCompare.Html, "country1"));
        Assert.Contains("ireland", SelectValues(productionCompare.Html, "country2"));
        var finlandCompareFacts = (await Facts.Value).Topics.SelectMany(topic => topic.Aspects)
            .SelectMany(aspect => aspect.Facts.Where(fact => fact.CountrySlug == "finland")).ToArray();
        var productionFinlandCompare = await RenderAsync<Compare>("Production", language,
            "/compare?country1=finland&country2=denmark&category=all");
        Assert.Equal(8, Regex.Matches(productionFinlandCompare.Html, "class=\"comparison-topic\"").Count);
        Assert.Equal(24, Regex.Matches(productionFinlandCompare.Html, "class=\"structured-aspect\"").Count);
        Assert.Contains("class=\"fact-sources\"", productionFinlandCompare.Html);
        Assert.DoesNotContain("[[source:", productionFinlandCompare.Html);
        var productionFactText = NormalizeRenderedText(productionFinlandCompare.Html);
        foreach (var fact in finlandCompareFacts)
            Assert.Contains(NormalizeRenderedText(fact.Values[language]), productionFactText);
        foreach (var sourceId in finlandCompareFacts.SelectMany(fact => fact.OfficialSourceIds).Distinct())
        {
            var url = catalog.OfficialSources.Single(source => source.Id == sourceId).Url.AbsoluteUri;
            Assert.Contains($"href=\"{url}\"", productionFinlandCompare.Html);
        }

        var developmentHome = await RenderAsync<Home>("Development", language);
        Assert.Equal(19, Regex.Matches(developmentHome.Html, "class=\"country-card\"").Count);
        Assert.Contains("href=\"/countries/finland\"", developmentHome.Html);
        var developmentOverview = await RenderAsync<CountryOverview>("Development", language, "/countries/finland",
            new() { [nameof(CountryOverview.CountrySlug)] = "finland" });
        Assert.False(developmentOverview.NotFound);
        var developmentSearch = await RenderAsync<Search>("Development", language, "/search");
        Assert.Contains("finland", SelectValues(developmentSearch.Html, "country"));
        Assert.Equal(catalog.Countries.Select(country => country.Slug), SelectValues(developmentSearch.Html, "country"));
        var finlandSearch = await RenderAsync<Search>("Development", language, "/search?country=finland");
        Assert.Equal(8, Regex.Matches(finlandSearch.Html, "class=\"result-card\"").Count);
        Assert.Equal(8, ArticleSearch.Search(catalog, null, "finland", null, null, language).Count);

        foreach (var (categorySlug, expectedCount) in categoryCounts)
        {
            var category = await RenderAsync<Category>("Development", language,
                $"/countries/finland/categories/{categorySlug}", new()
                {
                    [nameof(Category.CountrySlug)] = "finland",
                    [nameof(Category.CategorySlug)] = categorySlug
                });
            Assert.False(category.NotFound);
            Assert.Equal(expectedCount, Regex.Matches(category.Html, "class=\"information-entry\"").Count);
        }

        var developmentCompare = await RenderAsync<Compare>("Development", language, "/compare");
        Assert.Contains("finland", SelectValues(developmentCompare.Html, "country1"));
        Assert.Contains("finland", SelectValues(developmentCompare.Html, "country2"));
        Assert.Contains("ireland", SelectValues(developmentCompare.Html, "country1"));
        Assert.Contains("ireland", SelectValues(developmentCompare.Html, "country2"));
        var developmentFinlandCompare = await RenderAsync<Compare>("Development", language,
            "/compare?country1=finland&country2=denmark&category=all");
        Assert.Equal(8, Regex.Matches(developmentFinlandCompare.Html, "class=\"comparison-topic\"").Count);
        Assert.Equal(24, Regex.Matches(developmentFinlandCompare.Html, "class=\"structured-aspect\"").Count);
        Assert.Contains("class=\"fact-sources\"", developmentFinlandCompare.Html);
        Assert.DoesNotContain("[[source:", developmentFinlandCompare.Html);
        var developmentFactText = NormalizeRenderedText(developmentFinlandCompare.Html);
        foreach (var fact in finlandCompareFacts)
            Assert.Contains(NormalizeRenderedText(fact.Values[language]), developmentFactText);
        foreach (var sourceId in finlandCompareFacts.SelectMany(fact => fact.OfficialSourceIds).Distinct())
        {
            var url = catalog.OfficialSources.Single(source => source.Id == sourceId).Url.AbsoluteUri;
            Assert.Contains($"href=\"{url}\"", developmentFinlandCompare.Html);
        }
        Assert.Contains("finland", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Contains("ireland", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Contains("france", CountryComparisonTopicMap.SupportedCountrySlugs);
    }

    [Fact]
    public async Task DevelopmentFinlandSearchExcludesOutdatedTranslation()
    {
        const string language = "en";
        var catalog = await Catalog.Value;
        var outdatedTranslation = Assert.Single(catalog.InformationEntryTranslations,
            item => item.InformationEntryId == Articles["FI-EMP-01"].Id && item.LanguageCode == language);
        var previewCatalog = WithFinlandTranslationStatus(catalog, outdatedTranslation.InformationEntryId,
            language, VerificationStatus.Outdated);

        var search = await RenderAsync<Search>("Development", language, "/search?country=finland",
            catalogOverride: previewCatalog);

        Assert.Equal(7, Regex.Matches(search.Html, "class=\"result-card\"").Count);
        Assert.DoesNotContain(outdatedTranslation.Title, search.Html);
        Assert.All(Articles.Values.Select(item => item.Id).Where(id => id != outdatedTranslation.InformationEntryId), id =>
        {
            Assert.Contains(catalog.InformationEntryTranslations.Single(item =>
                item.InformationEntryId == id && item.LanguageCode == language).Title, search.Html);
        });
    }

    [Fact]
    public void FinlandCountryLocalizationIsPresentInAllLanguages()
    {
        Assert.Equal("Finland", Resources.GetString("Country.Finland", CultureInfo.GetCultureInfo("en")));
        Assert.Equal("Finland", Resources.GetString("Country.Finland.In", CultureInfo.GetCultureInfo("en")));
        Assert.Equal("Фінляндія", Resources.GetString("Country.Finland", CultureInfo.GetCultureInfo("uk")));
        Assert.Equal("у Фінляндії", Resources.GetString("Country.Finland.In", CultureInfo.GetCultureInfo("uk")));
        Assert.Equal("Финляндия", Resources.GetString("Country.Finland", CultureInfo.GetCultureInfo("ru")));
        Assert.Equal("в Финляндии", Resources.GetString("Country.Finland.In", CultureInfo.GetCultureInfo("ru")));
    }

    private static string NormalizeRenderedText(string value)
    {
        var text = WebUtility.HtmlDecode(Regex.Replace(value, "<[^>]+>", string.Empty));
        return Regex.Replace(text, @"[^\p{L}\p{N}]+", string.Empty);
    }

    private static string[] SelectValues(string html, string name)
    {
        var select = Regex.Match(html, $"<select\\b[^>]*name=\"{name}\"[^>]*>(.*?)</select>", RegexOptions.Singleline);
        Assert.True(select.Success);
        return Regex.Matches(select.Groups[1].Value, "<option\\b[^>]*value=\"([^\"]+)\"")
            .Select(match => match.Groups[1].Value).ToArray();
    }

    private static CatalogData WithFinlandTranslationStatus(CatalogData catalog, Guid informationEntryId,
        string languageCode, VerificationStatus status)
    {
        var translations = catalog.InformationEntryTranslations.Select(item =>
            item.InformationEntryId == informationEntryId && item.LanguageCode == languageCode
                ? new InformationEntryTranslation(item.InformationEntryId, item.LanguageCode, item.Title,
                    item.Summary, item.Content, status, item.LastUpdatedAt)
                : item);
        return (CatalogData)Activator.CreateInstance(typeof(CatalogData),
            BindingFlags.Instance | BindingFlags.NonPublic, binder: null,
            args: [catalog.Countries, catalog.Categories, catalog.InformationEntries, translations, catalog.OfficialSources],
            culture: CultureInfo.InvariantCulture)!;
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !Directory.Exists(Path.Combine(directory.FullName, ".git")))
            directory = directory.Parent;
        return directory?.FullName ?? throw new DirectoryNotFoundException("Could not locate repository root.");
    }

    private static async Task<(string Html, bool NotFound)> RenderAsync<TComponent>(string environment,
        string language, string path = "/", Dictionary<string, object?>? parameters = null,
        CatalogData? catalogOverride = null) where TComponent : IComponent
    {
        var oldCulture = CultureInfo.CurrentCulture;
        var oldUiCulture = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(language);
        try
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton(catalogOverride ?? await Catalog.Value);
            services.AddSingleton(await Facts.Value);
            services.AddSingleton(new StepByStepGuideCatalog([]));
            services.AddSingleton<IWebHostEnvironment>(new TestEnvironment(environment));
            var navigation = new TestNavigationManager(path);
            var notFound = false;
            navigation.OnNotFound += (_, _) => notFound = true;
            services.AddSingleton<NavigationManager>(navigation);
            using var provider = services.BuildServiceProvider();
            await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());
            var html = await renderer.Dispatcher.InvokeAsync(async () =>
            {
                var component = await renderer.RenderComponentAsync<TComponent>(parameters is null
                    ? ParameterView.Empty : ParameterView.FromDictionary(parameters));
                return component.ToHtmlString();
            });
            return (WebUtility.HtmlDecode(html), notFound);
        }
        finally
        {
            CultureInfo.CurrentCulture = oldCulture;
            CultureInfo.CurrentUICulture = oldUiCulture;
        }
    }

    private sealed class TestNavigationManager : NavigationManager
    {
        public TestNavigationManager(string path) => Initialize("http://localhost/", $"http://localhost{path}");
        protected override void NavigateToCore(string uri, bool forceLoad) { }
    }

    private sealed class TestEnvironment(string environmentName) : IWebHostEnvironment
    {
        public string EnvironmentName { get; set; } = environmentName;
        public string ApplicationName { get; set; } = "RefugeeNavigator.Tests";
        public string ContentRootPath { get; set; } = AppContext.BaseDirectory;
        public string WebRootPath { get; set; } = AppContext.BaseDirectory;
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
    }
}
