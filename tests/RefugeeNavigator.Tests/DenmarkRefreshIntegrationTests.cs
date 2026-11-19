using System.Text.Json;
using System.Text.RegularExpressions;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;

namespace RefugeeNavigator.Tests;

public sealed class DenmarkRefreshIntegrationTests
{
    private static readonly Guid DenmarkId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly Guid[] ArticleIds =
    [
        Guid.Parse("afc14824-4035-4ca7-9662-6d5352ba63cb"),
        Guid.Parse("2d106fd3-e981-4ef9-9733-568b654dfca5"),
        Guid.Parse("c2356631-3fa2-4553-8fda-50a4419262c0"),
        Guid.Parse("35da68f6-8880-40b2-9b3f-eb2b809b1caf"),
        Guid.Parse("b1e4aed1-bbaf-4f09-9468-74000bf96430"),
        Guid.Parse("19afdd19-e2cc-4da6-bde3-6dc3d64b7503"),
        Guid.Parse("fee9dcf0-447d-4402-92a2-8b1bec125944"),
        Guid.Parse("1a19c1f9-1111-4d60-8490-da548f33083f")
    ];

    [Fact]
    public async Task DenmarkArticlesHaveTwentyFourVerifiedCurrentTranslations()
    {
        var catalog = await new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();
        var articles = catalog.InformationEntries.Where(entry => entry.CountryId == DenmarkId).ToArray();
        Assert.Equal(8, articles.Length);
        Assert.Equal(ArticleIds.Order(), articles.Select(entry => entry.Id).Order());

        var translations = catalog.InformationEntryTranslations
            .Where(translation => ArticleIds.Contains(translation.InformationEntryId)).ToArray();
        Assert.Equal(24, translations.Length);
        Assert.All(translations, translation =>
        {
            Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
            Assert.Equal(new DateOnly(2026, 11, 17), DateOnly.FromDateTime(translation.LastUpdatedAt.UtcDateTime));
            Assert.False(string.IsNullOrWhiteSpace(translation.Title));
            Assert.False(string.IsNullOrWhiteSpace(translation.Summary));
            Assert.False(string.IsNullOrWhiteSpace(translation.Content));
        });
    }

    [Fact]
    public async Task DenmarkSourcesAndArticleSourceReferencesReconcile()
    {
        var catalog = await new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();
        Assert.Equal(1530, catalog.OfficialSources.Count);
        var denmarkSources = catalog.OfficialSources.Where(source => ArticleIds.Contains(source.InformationEntryId)).ToArray();
        Assert.Equal(107, denmarkSources.Length);
        Assert.Equal(denmarkSources.Length, denmarkSources.Select(source => source.Id).Distinct().Count());
        Assert.Equal(105, denmarkSources.Count(source => DateOnly.FromDateTime(source.LastCheckedAt.UtcDateTime) == new DateOnly(2026, 11, 17)));
        Assert.Equal(2, denmarkSources.Count(source =>
            source.Id == Guid.Parse("d3520d2c-2fc2-4cdf-89dc-eb192aa9b64a") || source.Id == Guid.Parse("c1f2c424-b1e0-4cfe-8d3a-2c5573f2f59a")));

        var expectedOwners = new Dictionary<Guid, Guid>
        {
            [Guid.Parse("e1d08534-e55b-5025-ac23-75a2d7cdc5dd")] = ArticleIds[0],
            [Guid.Parse("cc72237b-6472-539c-94d9-ff73e3cbea02")] = ArticleIds[6],
            [Guid.Parse("6ea2a976-8834-55e3-8817-ef3d769c7349")] = ArticleIds[6],
            [Guid.Parse("5b063a3e-9643-5c83-9a57-a61b24138abc")] = ArticleIds[1],
            [Guid.Parse("f4e25072-b328-5469-ab1c-30169b92abfe")] = ArticleIds[6]
        };
        foreach (var (id, owner) in expectedOwners)
        {
            var source = Assert.Single(catalog.OfficialSources, item => item.Id == id);
            Assert.Equal(owner, source.InformationEntryId);
            Assert.Equal(new DateOnly(2026, 11, 17), DateOnly.FromDateTime(source.LastCheckedAt.UtcDateTime));
        }

        Assert.Equal("https://www.retsinformation.dk/eli/lta/2023/501", Assert.Single(catalog.OfficialSources, item => item.Id == Guid.Parse("2acf95dd-1fea-45bc-9478-fda84be4496d")).Url.AbsoluteUri);
        Assert.Equal("https://www.ku.dk/studies/admission/application-and-tuition-fees", Assert.Single(catalog.OfficialSources, item => item.Id == Guid.Parse("2c5248de-f006-4edd-86c2-e067a8f67016")).Url.AbsoluteUri);
        Assert.Equal("https://nyidanmark.dk/en-GB/Words-and-concepts/F%C3%A6lles/Information-to-Ukrainian-citizens", Assert.Single(catalog.OfficialSources, item => item.Id == Guid.Parse("826eb02a-ac4a-4d10-80d7-b32c4123dff7")).Url.AbsoluteUri);

        Assert.Contains(catalog.OfficialSources, item => item.Id == Guid.Parse("d3520d2c-2fc2-4cdf-89dc-eb192aa9b64a"));
        Assert.Contains(catalog.OfficialSources, item => item.Id == Guid.Parse("c1f2c424-b1e0-4cfe-8d3a-2c5573f2f59a"));
        Assert.Equal(0, denmarkSources.GroupBy(source => (source.InformationEntryId, source.Url.AbsoluteUri)).Count(group => group.Count() > 1));

        var sourceById = catalog.OfficialSources.ToDictionary(source => source.Id);
        foreach (var translation in catalog.InformationEntryTranslations.Where(item => ArticleIds.Contains(item.InformationEntryId)))
        {
            foreach (Match match in Regex.Matches(translation.Content, @"\[\[source:([0-9a-f-]{36})\|"))
            {
                var sourceId = Guid.Parse(match.Groups[1].Value);
                Assert.True(sourceById.TryGetValue(sourceId, out var source), $"Dangling SourceId {sourceId}");
                Assert.Equal(translation.InformationEntryId, source!.InformationEntryId);
            }
        }
    }

    [Fact]
    public async Task DenmarkCompareFactsHaveExpectedCoverageAndBindingDelta()
    {
        using var document = JsonDocument.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "comparison-facts.json")));
        var facts = document.RootElement.GetProperty("Topics").EnumerateArray()
            .SelectMany(topic => topic.GetProperty("Aspects").EnumerateArray())
            .SelectMany(aspect => aspect.GetProperty("Facts").EnumerateArray())
            .Where(fact => fact.GetProperty("CountrySlug").GetString() == "denmark").ToArray();
        Assert.Equal(24, facts.Length);
        var pay = facts.Single(fact => fact.GetProperty("Values").GetProperty("en").GetString()!.Contains("statutory nationwide minimum wage", StringComparison.OrdinalIgnoreCase));
        Assert.DoesNotContain("DKK", pay.GetProperty("Values").GetProperty("en").GetString(), StringComparison.OrdinalIgnoreCase);

        var dates = facts.Single(fact => fact.GetProperty("Values").GetProperty("en").GetString()!.Contains("17 March 2028", StringComparison.Ordinal));
        var dateText = dates.GetProperty("Values").GetProperty("en").GetString()!;
        Assert.Contains("17 March 2027", dateText, StringComparison.Ordinal);
        Assert.Contains("17 March 2028", dateText, StringComparison.Ordinal);
        Assert.Contains("e1d08534-e55b-5025-ac23-75a2d7cdc5dd", dates.GetProperty("OfficialSourceIds").EnumerateArray().Select(item => item.GetString()));
        Assert.All(new[] { "uk", "ru" }, language =>
        {
            var localized = dates.GetProperty("Values").GetProperty(language).GetString()!;
            Assert.Contains("2028", localized, StringComparison.Ordinal);
            Assert.Contains("2027", localized, StringComparison.Ordinal);
        });

        var catalog = await new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();
        var sourceById = catalog.OfficialSources.ToDictionary(source => source.Id.ToString());
        foreach (var fact in facts)
        {
            Assert.Equal("Approved", fact.GetProperty("Status").GetString());
            Assert.True(fact.GetProperty("IsAvailable").GetBoolean());
            foreach (var item in fact.GetProperty("OfficialSourceIds").EnumerateArray())
            {
                var sourceId = item.GetString();
                Assert.NotNull(sourceId);
                Assert.True(sourceById.TryGetValue(sourceId, out var source), $"Dangling Compare SourceId {sourceId}");
                Assert.Contains(source!.InformationEntryId, ArticleIds);
            }
        }
    }

    [Fact]
    public async Task HighRiskDenmarkWordingKeepsApprovedScopeAcrossLocales()
    {
        var catalog = await new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();
        var cases = new (Guid Id, string Locale, string[] Required)[]
        {
            (ArticleIds[0], "en", ["national scheme", "does not participate in the EU Temporary Protection Directive", "23–59", "25 June 2026", "35 days", "not a promise", "conditional", "certain healthcare services"]),
            (ArticleIds[0], "uk", ["національною схемою", "23–59", "25 червня 2026", "35 днів", "має умови"]),
            (ArticleIds[0], "ru", ["национальная схема", "23–59", "25 июня 2026", "35 дней", "имеет условия"]),
            (ArticleIds[1], "en", ["17 March 2028", "17 March 2027", "same rights", "Self-employment cannot be the basis"]),
            (ArticleIds[1], "uk", ["17 березня 2028", "17 березня 2027", "тими самими правами"]),
            (ArticleIds[1], "ru", ["17 марта 2028", "17 марта 2027", "с теми же правами"]),
            (ArticleIds[3], "en", ["no statutory nationwide minimum wage", "having an application, CPR number or tax card is not the same as having a residence permit"]),
            (ArticleIds[4], "en", ["pending", "health card", "three years"]),
            (ArticleIds[6], "en", ["17 March 2026", "26 weeks", "notify the municipality in writing", "municipality"]),
            (ArticleIds[7], "en", ["not covered by the separate arbejdspligt regime"])
        };
        foreach (var (id, locale, required) in cases)
        {
            var translation = Assert.Single(catalog.InformationEntryTranslations, item => item.InformationEntryId == id && item.LanguageCode == locale);
            foreach (var fragment in required) Assert.Contains(fragment, translation.Content, StringComparison.OrdinalIgnoreCase);
        }
        Assert.All(catalog.InformationEntryTranslations.Where(item => ArticleIds.Contains(item.InformationEntryId)), translation => Assert.DoesNotContain("L 43", translation.Title + translation.Summary + translation.Content, StringComparison.OrdinalIgnoreCase));
    }
}
