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

public sealed class BelgiumContentIntegrationTests
{
    private static readonly Guid CountryId = Guid.Parse("10000000-0000-0000-0000-000000000017");
    private static readonly IReadOnlyDictionary<string, (Guid Id, string Category)> Articles = new Dictionary<string, (Guid, string)>
    {
        ["BE-TP-01"] = (Guid.Parse("aef6ec65-f8ed-5054-bafb-c465e790dd57"), "temporary-protection"),
        ["BE-TP-02"] = (Guid.Parse("54c6f95f-589e-5901-beae-990bed994160"), "temporary-protection"),
        ["BE-EMP-01"] = (Guid.Parse("f2c1526a-a218-5bf0-ab2a-9167dbec0c26"), "employment"),
        ["BE-EMP-02"] = (Guid.Parse("55fa79e5-89a2-518b-90b2-85a2735f171b"), "employment"),
        ["BE-HEALTH-01"] = (Guid.Parse("673316a9-6ee9-50d6-9f66-e19659aeff0e"), "healthcare"),
        ["BE-HOUSING-01"] = (Guid.Parse("3646ae19-215c-5f0e-9075-85ef5bae88f7"), "housing"),
        ["BE-EDU-01"] = (Guid.Parse("ac80dcbd-4289-5c77-8340-f9e4226a47f6"), "education"),
        ["BE-SOC-01"] = (Guid.Parse("20eef3c3-fcc7-5b5b-b672-695f29faad61"), "social-assistance")
    };
    private static readonly IReadOnlyDictionary<string, int> ExpectedSourceCounts = new Dictionary<string, int>
    {
        ["BE-TP-01"] = 8,
        ["BE-TP-02"] = 15,
        ["BE-EMP-01"] = 16,
        ["BE-EMP-02"] = 23,
        ["BE-HEALTH-01"] = 13,
        ["BE-HOUSING-01"] = 23,
        ["BE-EDU-01"] = 24,
        ["BE-SOC-01"] = 20
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
    public async Task BelgiumHasEightProductionArticlesAndExpectedCatalogCounts()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "belgium");
        Assert.Equal(CountryId, country.Id);
        Assert.Equal("Belgium", country.Name);
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
                var updated = new HashSet<Guid>
                {
                    Guid.Parse("aef6ec65-f8ed-5054-bafb-c465e790dd57"),
                    Guid.Parse("f2c1526a-a218-5bf0-ab2a-9167dbec0c26"),
                    Guid.Parse("55fa79e5-89a2-518b-90b2-85a2735f171b"),
                    Guid.Parse("20eef3c3-fcc7-5b5b-b672-695f29faad61")
                };
                Assert.Equal(updated.Contains(item.InformationEntryId)
                    ? new DateTimeOffset(2026, 11, 16, 0, 0, 0, TimeSpan.Zero)
                    : new DateTimeOffset(2026, 11, 11, 0, 0, 0, TimeSpan.Zero), item.LastUpdatedAt);
            });
        }

        Assert.Equal(19, CountryComparisonTopicMap.SupportedCountrySlugs.Count);
        Assert.Contains("ireland", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Contains("france", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.Contains("belgium", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.All(CountryComparisonTopicMap.All, topic => Assert.Equal(19, topic.EntryIds.Count));
        var allFacts = (await Facts.Value).Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).ToArray();
        Assert.Equal(456, allFacts.Length);
        Assert.Equal(24, allFacts.Count(fact => fact.CountrySlug == "ireland"));
        Assert.Equal(24, allFacts.Count(fact => fact.CountrySlug == "france"));
        Assert.Equal(24, allFacts.Count(fact => fact.CountrySlug == "belgium"));
        Assert.Equal(72, allFacts.Where(fact => fact.CountrySlug == "belgium").Sum(fact => fact.Values.Count));
    }

    [Fact]
    public async Task BelgiumCompareFactsMatchTheReviewedDraftAndUseOnlyOwnedProductionSources()
    {
        var catalog = await Catalog.Value;
        var comparison = await Facts.Value;
        var repositoryRoot = FindRepositoryRoot();
        var draft = await File.ReadAllTextAsync(Path.Combine(repositoryRoot, "docs", "content-drafts", "belgium-v1", "COMPARE_DRAFT_01.md"));
        var aspectMatches = Regex.Matches(draft,
            @"(?ms)^### (?<aspect>[a-z][a-z-]*)\s*\n(?<body>.*?)(?=^### |\z)");
        Assert.Equal(24, aspectMatches.Count);
        var parsedAspectIds = aspectMatches.Select(match => match.Groups["aspect"].Value).ToArray();
        Assert.Equal(24, parsedAspectIds.Distinct(StringComparer.Ordinal).Count());
        var productionFacts = comparison.Topics.SelectMany(topic => topic.Aspects
                .SelectMany(aspect => aspect.Facts.Where(fact => fact.CountrySlug == "belgium")
                    .Select(fact => (aspect.Id, Fact: fact))))
            .ToDictionary(item => item.Id, item => item.Fact, StringComparer.Ordinal);
        Assert.Equal(parsedAspectIds.Order(StringComparer.Ordinal), productionFacts.Keys.Order(StringComparer.Ordinal));

        var totalBindings = 0;
        var sourceIds = new HashSet<Guid>();
        var sourceUrls = new HashSet<string>(StringComparer.Ordinal);
        foreach (Match match in aspectMatches)
        {
            var aspectId = match.Groups["aspect"].Value;
            var body = match.Groups["body"].Value;
            string Field(string name)
            {
                var field = Regex.Match(body, $@"(?m)^\*\*{Regex.Escape(name)}:\*\* (.+)$");
                Assert.True(field.Success, $"Missing {name} field for {aspectId}.");
                return field.Groups[1].Value.Trim();
            }

            var ownerId = Guid.Parse(Regex.Match(Field("InformationEntryId"), @"[0-9a-f-]{36}").Value);
            Assert.Contains(Articles.Values, article => article.Id == ownerId);
            var owner = Assert.Single(catalog.InformationEntries, entry => entry.Id == ownerId);
            Assert.Equal(CountryId, owner.CountryId);
            var topicFacts = Assert.Single(comparison.Topics, item => item.Aspects.Any(aspect => aspect.Id == aspectId));
            var mappedTopic = Assert.Single(CountryComparisonTopicMap.All, item => item.ResourceKey == topicFacts.TopicResourceKey);
            Assert.Equal(ownerId, CountryComparisonTopicMap.EntryIdFor(mappedTopic, "belgium"));
            Assert.Equal(topicFacts.CategorySlug,
                catalog.Categories.Single(category => category.Id == owner.CategoryId).Slug);
            var production = productionFacts[aspectId];
            Assert.Equal(ownerId, production.InformationEntryId);
            Assert.Equal(ComparisonFactStatus.Approved, production.Status);
            Assert.True(production.IsAvailable);

            foreach (var (language, fieldName) in new[] { ("en", "EN"), ("uk", "UK"), ("ru", "RU") })
            {
                Assert.Equal(Field(fieldName), production.Values[language]);
                Assert.DoesNotContain("[[source:", production.Values[language], StringComparison.Ordinal);
            }
            Assert.Equal("READY WITH QUALIFICATION", Field("Result"));

            var expectedSourceIds = Regex.Matches(Field("OfficialSourceIds"), @"[0-9a-f-]{36}")
                .Select(item => Guid.Parse(item.Value)).ToArray();
            Assert.Equal(expectedSourceIds, production.OfficialSourceIds);
            Assert.NotEmpty(expectedSourceIds);
            totalBindings += expectedSourceIds.Length;
            foreach (var sourceId in expectedSourceIds)
            {
                Assert.Contains(catalog.OfficialSources, source =>
                    source.Id == sourceId && source.InformationEntryId == ownerId && source.Url.Scheme == Uri.UriSchemeHttps);
                sourceIds.Add(sourceId);
                sourceUrls.Add(Assert.Single(catalog.OfficialSources, source => source.Id == sourceId).Url.AbsoluteUri);
            }
        }

        Assert.Equal(93, totalBindings);
        Assert.Equal(85, sourceIds.Count);
        Assert.Equal(81, sourceUrls.Count);
    }

    [Fact]
    public async Task BelgiumTranslationsAndOneHundredFortyTwoArticleOwnedSourcesMatchDraftsExactly()
    {
        var catalog = await Catalog.Value;
        Assert.Equal(1486, catalog.OfficialSources.Count);
        Assert.Equal(catalog.OfficialSources.Count, catalog.OfficialSources.Select(item => item.Id).Distinct().Count());
        var repositoryRoot = FindRepositoryRoot();
        var draftsRoot = Path.Combine(repositoryRoot, "docs", "content-drafts", "belgium-v1");
        var articleIds = Articles.Values.Select(item => item.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(item => articleIds.Contains(item.InformationEntryId)).ToArray();
        Assert.Equal(142, sources.Length);
        Assert.Equal(142, sources.Select(item => item.Id).Distinct().Count());
        Assert.Equal(132, sources.Select(item => item.Url.AbsoluteUri).Distinct().Count());
        var refreshedOnReleaseDate = new HashSet<Guid>
        {
            Guid.Parse("01044ed1-42f4-5d33-bbcd-931f2b1006b7"),
            Guid.Parse("efd6f672-6edb-5eac-bddc-3d56ed6b4d27"),
            Guid.Parse("2e8786e8-c502-5853-8ed7-ba5e9c1bc9f3"),
            Guid.Parse("d12c3b88-ba4a-55b4-9a41-5be3f35a97ab"),
            Guid.Parse("56f4c722-2026-56c5-bbe5-27e7440694d8"),
            Guid.Parse("3ebe4be7-0c7e-50bc-8222-183c6d151ede"),
            Guid.Parse("cab92bae-0b76-56b2-9581-7381ad653d3b"),
            Guid.Parse("7e9a47b2-6a01-57e3-ae65-9f64425869a2"),
            Guid.Parse("5b9b6430-79ba-5aee-9ef6-f6d8f21e47db"),
            Guid.Parse("a12e3080-07a0-5320-99de-631048c72889"),
            Guid.Parse("0e853ef8-2b3b-5592-833b-6d1ce463c196")
        };
        var checkedOnNov14 = Guid.Parse("38f01c1d-3688-5be0-b94b-97b5f0d8bdf3");
        Assert.All(sources, source =>
        {
            Assert.Contains(source.InformationEntryId, articleIds);
            Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme);
            Assert.Equal(
                refreshedOnReleaseDate.Contains(source.Id)
                    ? new DateTimeOffset(2026, 11, 16, 0, 0, 0, TimeSpan.Zero)
                    : source.Id == checkedOnNov14
                        ? new DateTimeOffset(2026, 11, 14, 0, 0, 0, TimeSpan.Zero)
                        : new DateTimeOffset(2026, 11, 11, 0, 0, 0, TimeSpan.Zero),
                source.LastCheckedAt);
        });
        Assert.All(catalog.OfficialSources
            .GroupBy(item => (item.InformationEntryId, item.Title, item.Url.AbsoluteUri)), group => Assert.Single(group));
        Assert.All(sources.GroupBy(item => (item.InformationEntryId, item.Url.AbsoluteUri)), group => Assert.Single(group));
        Assert.Equal(11, sources.Count(item => item.LastCheckedAt == new DateTimeOffset(2026, 11, 16, 0, 0, 0, TimeSpan.Zero)));

        var sourceById = catalog.OfficialSources.ToDictionary(item => item.Id);
        foreach (var (articleCode, expected) in Articles)
        {
            var draft = await File.ReadAllTextAsync(Path.Combine(draftsRoot, articleCode + ".md"));
            Assert.Contains($"Article ID: `{expected.Id}`", draft);
            var sourceMatches = Regex.Matches(draft,
                @"(?m)^- `(?<key>BE-[^`]+)` → `(?<id>[^`]+)` — (?<title>.*?) \(research source .*?; \[official source\]\((?<url>https://.*)\)\)$");
            Assert.Equal(ExpectedSourceCounts[articleCode], sourceMatches.Count);
            var ownedSources = sources.Where(item => item.InformationEntryId == expected.Id).ToDictionary(item => item.Id);
            Assert.Equal(ExpectedSourceCounts[articleCode], ownedSources.Count);
            foreach (Match sourceMatch in sourceMatches)
            {
                var sourceId = Guid.Parse(sourceMatch.Groups["id"].Value);
                Assert.True(ownedSources.TryGetValue(sourceId, out var source));
                Assert.Equal(expected.Id, source!.InformationEntryId);
                var expectedTitle = sourceMatch.Groups["title"].Value;
                Assert.Equal(expectedTitle, source.Title);
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
    public async Task BelgiumRefreshKeepsReviewedSafetyBoundariesAndReferenceAmounts()
    {
        var catalog = await Catalog.Value;
        var translations = catalog.InformationEntryTranslations.ToDictionary(item => (item.InformationEntryId, item.LanguageCode));
        string Content(string id, string lang) => translations[(Guid.Parse(id), lang)].Content;
        var tp = Content("aef6ec65-f8ed-5054-bafb-c465e790dd57", "en");
        Assert.Contains("5 August 2026", tp);
        Assert.Contains("4 August 2026", tp);
        Assert.Contains("Rue Belliard 68", tp);
        Assert.Contains("4 January 2027", tp);
        Assert.Contains("4 March 2028", tp);
        Assert.Contains("inconsistent legacy reference", tp, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("valid until 4 March 2025", tp, StringComparison.Ordinal);

        var employment = Content("55fa79e5-89a2-518b-90b2-85a2735f171b", "en");
        Assert.Contains("€2,189.81", employment);
        Assert.Contains("CCT 50", employment);
        Assert.Contains("student workers aged 18–20", employment);
        Assert.Contains("Annex 3", employment);
        Assert.Contains("pre-decision Annex 3 does not establish a right to start work", employment, StringComparison.OrdinalIgnoreCase);

        var health = Content("673316a9-6ee9-50d6-9f66-e19659aeff0e", "en");
        Assert.Contains("CAAMI", health);
        Assert.Contains("INAMI", health);
        Assert.Contains("confirm", health, StringComparison.OrdinalIgnoreCase);
        var facts = await Facts.Value;
        var belgiumFacts = facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts.Where(fact => fact.CountrySlug == "belgium")
            .Select(fact => (aspect.Id, Fact: fact))).ToDictionary(item => item.Id, item => item.Fact, StringComparer.Ordinal);
        Assert.Contains("capacity", belgiumFacts["housing-initial"].Values["en"], StringComparison.OrdinalIgnoreCase);
        Assert.True(belgiumFacts["housing-initial"].IsAvailable);
        Assert.Contains("Community", belgiumFacts["education-higher-admission"].Values["en"]);
        Assert.Contains("no award or amount is guaranteed", belgiumFacts["social-subsistence"].Values["en"], StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("guaranteed", belgiumFacts["social-application"].Values["en"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("where that service is active", belgiumFacts["healthcare-urgent"].Values["en"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Annex 15", belgiumFacts["work-permission"].Values["en"]);
        Assert.Contains("pre-decision Annex 3", belgiumFacts["work-permission"].Values["en"]);
        var education = Content("ac80dcbd-4289-5c77-8340-f9e4226a47f6", "en");
        Assert.Contains("€1,194", education);
        Assert.Contains("FWB", education);
        var social = Content("20eef3c3-fcc7-5b5b-b672-695f29faad61", "en");
        foreach (var rate in new[] { "€911.56", "€1,367.34", "€1,847.89" })
            Assert.Contains(rate, social);
        Assert.Contains("not guaranteed payments", social, StringComparison.OrdinalIgnoreCase);
        foreach (var language in new[] { "en", "uk", "ru" })
        {
            Assert.Contains("CCT 50", Content("55fa79e5-89a2-518b-90b2-85a2735f171b", language));
            Assert.Contains("911", Content("20eef3c3-fcc7-5b5b-b672-695f29faad61", language));
            Assert.Contains("1 194", Content("ac80dcbd-4289-5c77-8340-f9e4226a47f6", language).Replace(",", " "));
        }

        var belgium = catalog.InformationEntries.Where(item => item.CountryId == CountryId).Select(item => item.Id).ToHashSet();
        var updatedCount = catalog.InformationEntryTranslations.Count(item => belgium.Contains(item.InformationEntryId)
            && item.LastUpdatedAt == new DateTimeOffset(2026, 11, 16, 0, 0, 0, TimeSpan.Zero));
        Assert.Equal(12, updatedCount);
        Assert.Equal(1486, catalog.OfficialSources.Count);
        Assert.All(catalog.OfficialSources.Where(source => belgium.Contains(source.InformationEntryId)), source =>
            Assert.Contains(source.InformationEntryId, belgium));
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task BelgiumIsVisibleInProductionAndDevelopmentCompare(string language)
    {
        var catalog = await Catalog.Value;
        var productionHome = await RenderAsync<Home>("Production", language);
        Assert.Equal(19, Regex.Matches(productionHome.Html, "class=\"country-card\"").Count);
        Assert.Contains("href=\"/countries/belgium\"", productionHome.Html);

        var productionSearch = await RenderAsync<Search>("Production", language, "/search");
        Assert.Contains("belgium", SelectValues(productionSearch.Html, "country"));
        var productionOverview = await RenderAsync<CountryOverview>("Production", language, "/countries/belgium",
            new() { [nameof(CountryOverview.CountrySlug)] = "belgium" });
        Assert.False(productionOverview.NotFound);

        var categoryCounts = new Dictionary<string, int>
        {
            ["temporary-protection"] = 2,
            ["employment"] = 2,
            ["healthcare"] = 1,
            ["housing"] = 1,
            ["education"] = 1,
            ["social-assistance"] = 1
        };
        foreach (var (categorySlug, expectedCount) in categoryCounts)
        {
            var category = await RenderAsync<Category>("Production", language,
                $"/countries/belgium/categories/{categorySlug}", new()
                {
                    [nameof(Category.CountrySlug)] = "belgium",
                    [nameof(Category.CategorySlug)] = categorySlug
                });
            Assert.False(category.NotFound);
            Assert.Equal(expectedCount, Regex.Matches(category.Html, "class=\"information-entry\"").Count);
        }

        var productionResults = await RenderAsync<Search>("Production", language, "/search?country=belgium");
        Assert.Equal(8, Regex.Matches(productionResults.Html, "class=\"result-card\"").Count);

        var productionCompare = await RenderAsync<Compare>("Production", language, "/compare");
        Assert.Contains("belgium", SelectValues(productionCompare.Html, "country1"));
        Assert.Contains("belgium", SelectValues(productionCompare.Html, "country2"));
        Assert.Contains("ireland", SelectValues(productionCompare.Html, "country1"));
        Assert.Contains("ireland", SelectValues(productionCompare.Html, "country2"));
        var productionBelgiumCompare = await RenderAsync<Compare>("Production", language,
            "/compare?country1=belgium&country2=denmark&category=all");
        Assert.DoesNotContain("class=\"selection-message\"", productionBelgiumCompare.Html);
        Assert.Equal(8, Regex.Matches(productionBelgiumCompare.Html, "class=\"structured-topic\"").Count);
        Assert.Equal(24, Regex.Matches(productionBelgiumCompare.Html, "class=\"structured-aspect\"").Count);
        var belgiumFacts = (await Facts.Value).Topics.SelectMany(topic => topic.Aspects)
            .SelectMany(aspect => aspect.Facts.Where(fact => fact.CountrySlug == "belgium")).ToArray();
        foreach (var fact in belgiumFacts)
            Assert.Contains(fact.Values[language], productionBelgiumCompare.Html);
        var productionFactsHtml = ExtractStructuredComparison(productionBelgiumCompare.Html);
        Assert.DoesNotContain("[[source:", productionFactsHtml, StringComparison.Ordinal);
        foreach (var sourceId in belgiumFacts.SelectMany(fact => fact.OfficialSourceIds).Distinct())
        {
            var url = (await Catalog.Value).OfficialSources.Single(source => source.Id == sourceId).Url.AbsoluteUri;
            Assert.Contains($"href=\"{url}\"", productionBelgiumCompare.Html);
        }
        Assert.DoesNotContain("class=\"unavailable-state\"", productionBelgiumCompare.Html);

        var developmentHome = await RenderAsync<Home>("Development", language);
        Assert.Equal(19, Regex.Matches(developmentHome.Html, "class=\"country-card\"").Count);
        Assert.Contains("href=\"/countries/belgium\"", developmentHome.Html);
        var developmentOverview = await RenderAsync<CountryOverview>("Development", language, "/countries/belgium",
            new() { [nameof(CountryOverview.CountrySlug)] = "belgium" });
        Assert.False(developmentOverview.NotFound);
        var developmentSearch = await RenderAsync<Search>("Development", language, "/search");
        Assert.Contains("belgium", SelectValues(developmentSearch.Html, "country"));
        Assert.Equal(catalog.Countries.Select(country => country.Slug), SelectValues(developmentSearch.Html, "country"));
        var belgiumSearch = await RenderAsync<Search>("Development", language, "/search?country=belgium");
        Assert.Equal(8, Regex.Matches(belgiumSearch.Html, "class=\"result-card\"").Count);
        Assert.Equal(8, ArticleSearch.Search(catalog, null, "belgium", null, null, language).Count);

        var developmentCompare = await RenderAsync<Compare>("Development", language, "/compare");
        Assert.Contains("belgium", SelectValues(developmentCompare.Html, "country1"));
        Assert.Contains("belgium", SelectValues(developmentCompare.Html, "country2"));
        Assert.Contains("ireland", SelectValues(developmentCompare.Html, "country1"));
        Assert.Contains("ireland", SelectValues(developmentCompare.Html, "country2"));
        var developmentBelgiumCompare = await RenderAsync<Compare>("Development", language,
            "/compare?country1=belgium&country2=denmark&category=all");
        Assert.DoesNotContain("class=\"selection-message\"", developmentBelgiumCompare.Html);
        Assert.Equal(8, Regex.Matches(developmentBelgiumCompare.Html, "class=\"structured-topic\"").Count);
        Assert.Equal(24, Regex.Matches(developmentBelgiumCompare.Html, "class=\"structured-aspect\"").Count);
        foreach (var fact in belgiumFacts)
            Assert.Contains(fact.Values[language], developmentBelgiumCompare.Html);
        var developmentFactsHtml = ExtractStructuredComparison(developmentBelgiumCompare.Html);
        Assert.DoesNotContain("[[source:", developmentFactsHtml, StringComparison.Ordinal);
        Assert.Equal(19, CountryComparisonTopicMap.SupportedCountrySlugs.Count);
        Assert.Contains("belgium", CountryComparisonTopicMap.SupportedCountrySlugs);
        Assert.All(CountryComparisonTopicMap.All, topic => Assert.Equal(19, topic.EntryIds.Count));
        var facts = (await Facts.Value).Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).ToArray();
        Assert.Equal(456, facts.Length);
        Assert.Equal(24, facts.Count(fact => fact.CountrySlug == "belgium"));
    }

    [Fact]
    public async Task DevelopmentBelgiumSearchExcludesOutdatedTranslation()
    {
        const string language = "en";
        var catalog = await Catalog.Value;
        var outdatedTranslation = Assert.Single(catalog.InformationEntryTranslations,
            item => item.InformationEntryId == Articles["BE-EMP-01"].Id && item.LanguageCode == language);
        var previewCatalog = WithBelgiumTranslationStatus(catalog, outdatedTranslation.InformationEntryId,
            language, VerificationStatus.Outdated);

        var search = await RenderAsync<Search>("Development", language, "/search?country=belgium",
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
    public void BelgiumCountryLocalizationIsPresentInAllLanguages()
    {
        Assert.Equal("Belgium", Resources.GetString("Country.Belgium", CultureInfo.GetCultureInfo("en")));
        Assert.Equal("Belgium", Resources.GetString("Country.Belgium.In", CultureInfo.GetCultureInfo("en")));
        Assert.Equal("Бельгія", Resources.GetString("Country.Belgium", CultureInfo.GetCultureInfo("uk")));
        Assert.Equal("у Бельгії", Resources.GetString("Country.Belgium.In", CultureInfo.GetCultureInfo("uk")));
        Assert.Equal("Бельгия", Resources.GetString("Country.Belgium", CultureInfo.GetCultureInfo("ru")));
        Assert.Equal("в Бельгии", Resources.GetString("Country.Belgium.In", CultureInfo.GetCultureInfo("ru")));
    }

    private static string[] SelectValues(string html, string name)
    {
        var select = Regex.Match(html, $"<select\\b[^>]*name=\"{name}\"[^>]*>(.*?)</select>", RegexOptions.Singleline);
        Assert.True(select.Success);
        return Regex.Matches(select.Groups[1].Value, "<option\\b[^>]*value=\"([^\"]+)\"")
            .Select(match => match.Groups[1].Value).ToArray();
    }

    private static string ExtractStructuredComparison(string html)
    {
        var start = html.IndexOf("<section class=\"structured-comparison\"", StringComparison.Ordinal);
        Assert.True(start >= 0, "The structured comparison section was not rendered.");
        var end = html.IndexOf("<section class=\"comparison\"", start + 1, StringComparison.Ordinal);
        Assert.True(end > start, "The article comparison section did not follow the structured facts.");
        return html[start..end];
    }

    private static CatalogData WithBelgiumTranslationStatus(CatalogData catalog, Guid informationEntryId,
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
