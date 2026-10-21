using System.Globalization;
using System.Net;
using System.Resources;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Components;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Guides;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Tests;

public sealed class NetherlandsContentIntegrationTests
{
    private static readonly Guid NetherlandsId = Guid.Parse("10000000-0000-0000-0000-000000000007");
    private static readonly IReadOnlyDictionary<string, Guid> CategoryIds = new Dictionary<string, Guid>
    {
        ["temporary-protection"] = Guid.Parse("20000000-0000-0000-0000-000000000001"),
        ["employment"] = Guid.Parse("20000000-0000-0000-0000-000000000002"),
        ["healthcare"] = Guid.Parse("20000000-0000-0000-0000-000000000003"),
        ["housing"] = Guid.Parse("20000000-0000-0000-0000-000000000004"),
        ["education"] = Guid.Parse("20000000-0000-0000-0000-000000000005"),
        ["social-assistance"] = Guid.Parse("20000000-0000-0000-0000-000000000006")
    };
    private static readonly IReadOnlyDictionary<string, Guid> StableArticleIds = new Dictionary<string, Guid>
    {
        ["NL-TP-01"] = Guid.Parse("244b1055-4d8c-55b2-820e-60ff018c3b1f"),
        ["NL-TP-02"] = Guid.Parse("c5f5f833-0ab7-5e1a-9325-87a92b21d2ad"),
        ["NL-EMP-01"] = Guid.Parse("757c1db6-b66b-54b9-ac14-d8a3da958ef5"),
        ["NL-EMP-02"] = Guid.Parse("6a077f79-89c2-55d6-81f3-9ba7279979c9"),
        ["NL-HEALTH-01"] = Guid.Parse("d5c4d5fa-3b0f-5cc2-865f-841bd40911d1"),
        ["NL-HOUSING-01"] = Guid.Parse("19dde97c-cb42-5a90-97f7-e344f2139d2e"),
        ["NL-EDU-01"] = Guid.Parse("7633a085-4555-5a8f-a9a8-747a0f6e0545"),
        ["NL-SOC-01"] = Guid.Parse("8d466e7d-93f7-596e-a44f-188e04396aa0")
    };
    private static readonly IReadOnlyDictionary<string, int> ExpectedSourceCounts = new Dictionary<string, int>
    {
        ["NL-TP-01"] = 8,
        ["NL-TP-02"] = 8,
        ["NL-EMP-01"] = 3,
        ["NL-EMP-02"] = 6,
        ["NL-HEALTH-01"] = 11,
        ["NL-HOUSING-01"] = 7,
        ["NL-EDU-01"] = 9,
        ["NL-SOC-01"] = 4
    };
    private static readonly IReadOnlyDictionary<string, string> DraftFolders = new Dictionary<string, string>
    {
        ["NL-TP-01"] = "temporary-protection-application",
        ["NL-TP-02"] = "after-temporary-protection",
        ["NL-EMP-01"] = "finding-a-job",
        ["NL-EMP-02"] = "employment-rights",
        ["NL-HEALTH-01"] = "healthcare",
        ["NL-HOUSING-01"] = "housing",
        ["NL-EDU-01"] = "education",
        ["NL-SOC-01"] = "social-assistance"
    };
    private static readonly string[] Languages = ["en", "uk", "ru"];
    private static readonly HashSet<Guid> PromotedArticleIds = new()
    {
        StableArticleIds["NL-EMP-02"], StableArticleIds["NL-HEALTH-01"],
        StableArticleIds["NL-HOUSING-01"], StableArticleIds["NL-SOC-01"]
    };
    private static readonly string[] Categories =
    [
        "temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance"
    ];
    private static readonly DateTime SourceCheckDate = new(2026, 10, 20);
    private static readonly Lazy<Task<CatalogData>> Catalog = new(() =>
        new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());
    private static readonly ResourceManager Resources = new(
        "RefugeeNavigator.Web.Resources.Localization.SharedResource", typeof(SharedResource).Assembly);

    [Fact]
    public async Task NetherlandsCatalogHasSeventhCountryAndNeedsReviewTranslations()
    {
        var catalog = await Catalog.Value;
        var country = Assert.Single(catalog.Countries, item => item.Slug == "netherlands");
        Assert.Equal(NetherlandsId, country.Id);
        Assert.Equal("Netherlands", country.Name);

        var existingCountries = new Dictionary<string, (Guid Id, string Name)>
        {
            ["denmark"] = (Guid.Parse("10000000-0000-0000-0000-000000000001"), "Denmark"),
            ["germany"] = (Guid.Parse("10000000-0000-0000-0000-000000000002"), "Germany"),
            ["poland"] = (Guid.Parse("10000000-0000-0000-0000-000000000003"), "Poland"),
            ["czechia"] = (Guid.Parse("10000000-0000-0000-0000-000000000004"), "Czechia"),
            ["spain"] = (Guid.Parse("10000000-0000-0000-0000-000000000005"), "Spain"),
            ["sweden"] = (Guid.Parse("10000000-0000-0000-0000-000000000006"), "Sweden")
        };
        Assert.Equal(7, catalog.Countries.Count);
        foreach (var (slug, expected) in existingCountries)
        {
            var existing = Assert.Single(catalog.Countries, item => item.Slug == slug);
            Assert.Equal(expected.Id, existing.Id);
            Assert.Equal(expected.Name, existing.Name);
        }

        Assert.Equal(56, catalog.InformationEntries.Count);
        Assert.Equal(168, catalog.InformationEntryTranslations.Count);
        Assert.Equal(132, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(36, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, catalog.InformationEntryTranslations.Count(item => item.VerificationStatus == VerificationStatus.Outdated));

        var entries = catalog.InformationEntries.Where(item => item.CountryId == NetherlandsId).ToArray();
        Assert.Equal(8, entries.Length);
        Assert.Equal(StableArticleIds.Values.Order().ToArray(), entries.Select(item => item.Id).Order().ToArray());
        Assert.Equal(new[] { 2, 2, 1, 1, 1, 1 }, Categories.Select(category =>
            entries.Count(entry => entry.CategoryId == CategoryIds[category])).ToArray());

        var translations = catalog.InformationEntryTranslations
            .Where(item => entries.Any(entry => entry.Id == item.InformationEntryId)).ToArray();
        Assert.Equal(24, translations.Length);
        Assert.All(entries, entry =>
        {
            var entryTranslations = translations.Where(item => item.InformationEntryId == entry.Id).ToArray();
            Assert.Equal(Languages.Order(StringComparer.Ordinal).ToArray(),
                entryTranslations.Select(item => item.LanguageCode).Order(StringComparer.Ordinal).ToArray());
            Assert.All(entryTranslations, translation =>
            {
                Assert.Equal(PromotedArticleIds.Contains(entry.Id) ? VerificationStatus.Verified : VerificationStatus.NeedsReview,
                    translation.VerificationStatus);
                Assert.False(string.IsNullOrWhiteSpace(translation.Title));
                Assert.False(string.IsNullOrWhiteSpace(translation.Summary));
                Assert.False(string.IsNullOrWhiteSpace(translation.Content));
                Assert.NotEqual(default, translation.LastUpdatedAt);
                Assert.DoesNotContain("DRAFT — NOT APPROVED FOR PUBLICATION", translation.Content, StringComparison.Ordinal);
                Assert.DoesNotContain("Claim coverage", translation.Content, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("Editorial source set", translation.Content, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("https://", translation.Content, StringComparison.OrdinalIgnoreCase);
            });
        });
    }

    [Fact]
    public async Task NetherlandsEducationDuoRemissionExplainsNationalityCriterionInAllLanguages()
    {
        var catalog = await Catalog.Value;
        var entry = Assert.Single(catalog.InformationEntries,
            item => item.Id == StableArticleIds["NL-EDU-01"]);
        var expectedConditions = new Dictionary<string, string[]>
        {
            ["en"] =
            [
                "because you do not meet the nationality criteria",
                "only when all listed conditions are met",
                "specified MBO applicants under temporary protection",
                "aged 18 to under 31 on 1 August",
                "being unable to pay and having no sponsor",
                "31 July 2027",
                "not a general right"
            ],
            ["uk"] =
            [
                "через невідповідність критеріям громадянства",
                "лише за виконання всіх перелічених умов",
                "заявників MBO під тимчасовим захистом",
                "від 18 років до досягнення 31 року станом на 1 серпня",
                "неможливість сплатити й відсутність спонсора",
                "31 липня 2027 року",
                "не загальне право"
            ],
            ["ru"] =
            [
                "из-за несоответствия критериям гражданства",
                "только при выполнении всех перечисленных условий",
                "заявителей MBO под временной защитой",
                "от 18 лет до достижения 31 года на 1 августа",
                "отсутствие возможности оплатить обучение и спонсора",
                "31 июля 2027 года",
                "не общее право"
            ]
        };

        foreach (var (language, conditions) in expectedConditions)
        {
            var translation = Assert.Single(catalog.InformationEntryTranslations,
                item => item.InformationEntryId == entry.Id && item.LanguageCode == language);
            Assert.Equal(VerificationStatus.NeedsReview, translation.VerificationStatus);
            Assert.Contains("2026–27", translation.Content, StringComparison.Ordinal);
            foreach (var condition in conditions)
                Assert.Contains(condition, translation.Content, StringComparison.OrdinalIgnoreCase);
        }

        var duoSource = Assert.Single(catalog.OfficialSources, source =>
            source.InformationEntryId == entry.Id &&
            source.Url.AbsoluteUri == "https://www.duo.nl/particulier/course-fees/asylum-seeker-or-ukrainian.jsp");
        Assert.Equal(entry.Id, duoSource.InformationEntryId);
    }

    [Fact]
    public async Task IntegratedTranslationsMatchAllNetherlandsDrafts()
    {
        var catalog = await Catalog.Value;
        var repositoryRoot = FindRepositoryRoot();
        var drafts = Directory.GetFiles(Path.Combine(repositoryRoot, "docs", "content-drafts", "netherlands-v1"), "NL-*.md");
        Assert.Equal(8, drafts.Length);
        var entries = catalog.InformationEntries.Where(item => item.CountryId == NetherlandsId).ToArray();
        var languageMap = new Dictionary<string, string> { ["en"] = "EN", ["uk"] = "UK", ["ru"] = "RU" };

        foreach (var (key, folder) in DraftFolders)
        {
            var draft = await File.ReadAllTextAsync(Path.Combine(repositoryRoot, "docs", "content-drafts", "netherlands-v1", key + ".md"));
            var entry = Assert.Single(entries, item => item.Id == StableArticleIds[key]);
            Assert.Equal(NetherlandsId, entry.CountryId);
            var category = Regex.Match(draft, @"^- Category:\s+`([^`]+)`$", RegexOptions.Multiline).Groups[1].Value;
            Assert.Equal(CategoryIds[category], entry.CategoryId);
            foreach (var (languageCode, section) in languageMap)
            {
                var expected = ParseDraft(draft, section);
                var actual = Assert.Single(catalog.InformationEntryTranslations,
                    item => item.InformationEntryId == entry.Id && item.LanguageCode == languageCode);
                Assert.Equal(expected.Title, actual.Title);
                Assert.Equal(expected.Summary, actual.Summary);
                Assert.Equal(NormalizeLines(expected.Content), NormalizeLines(actual.Content));
            }
        }
    }

    [Fact]
    public async Task NetherlandsSourcesAreUniqueOwnedValidAndCheckedOnResearchDate()
    {
        var catalog = await Catalog.Value;
        var repositoryRoot = FindRepositoryRoot();
        var entries = catalog.InformationEntries.Where(item => item.CountryId == NetherlandsId).ToArray();
        var entryIds = entries.Select(item => item.Id).ToHashSet();
        var sources = catalog.OfficialSources.Where(item => entryIds.Contains(item.InformationEntryId)).ToArray();

        Assert.Equal(56, sources.Length);
        Assert.Equal(463, catalog.OfficialSources.Count);
        Assert.Equal(42, sources.Select(item => item.Url.AbsoluteUri).Distinct(StringComparer.OrdinalIgnoreCase).Count());
        Assert.Equal(catalog.OfficialSources.Count, catalog.OfficialSources.Select(item => item.Id).Distinct().Count());
        Assert.All(sources, source =>
        {
            Assert.Contains(source.InformationEntryId, entryIds);
            Assert.True(source.Url.IsAbsoluteUri);
            Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme);
            Assert.False(string.IsNullOrWhiteSpace(source.Title));
            var expectedCheckDate = source.Id == Guid.Parse("e59632da-eb67-5b56-840f-9749cdc9f171")
                ? new DateTime(2026, 10, 21)
                : SourceCheckDate;
            Assert.Equal(expectedCheckDate, source.LastCheckedAt.UtcDateTime.Date);
        });

        foreach (var (key, expectedCount) in ExpectedSourceCounts)
        {
            var entryId = StableArticleIds[key];
            var ownedSources = sources.Where(item => item.InformationEntryId == entryId).ToArray();
            Assert.Equal(expectedCount, ownedSources.Length);
            var draft = await File.ReadAllTextAsync(Path.Combine(repositoryRoot, "docs", "content-drafts", "netherlands-v1", key + ".md"));
            var expected = ReadEditorialSources(repositoryRoot, key, draft);
            if (key == "NL-HEALTH-01" && expected.Remove(
                    "https://www.rijksoverheid.nl/themas/familie-en-zorg/zorgverzekering/vraag-en-antwoord/eerstelijnszorg/wat-moet-ik-doen-als-iemand-dringend-medische-hulp-nodig-heeft",
                    out var urgentCareTitle))
            {
                expected.Add(
                    "https://www.rijksoverheid.nl/documenten/vraag-en-antwoord/eerstelijnszorg/wat-moet-ik-doen-als-iemand-dringend-medische-hulp-nodig-heeft",
                    urgentCareTitle);
            }
            Assert.Equal(expectedCount, expected.Count);
            Assert.Equal(expected.Keys.Order(StringComparer.OrdinalIgnoreCase).ToArray(),
                ownedSources.Select(item => item.Url.AbsoluteUri).Order(StringComparer.OrdinalIgnoreCase).ToArray());
            Assert.All(ownedSources, source =>
                Assert.Equal(expected[source.Url.AbsoluteUri], source.Title));
        }
    }

    [Theory]
    [InlineData("en", "Netherlands", "the Netherlands")]
    [InlineData("uk", "Нідерланди", "у Нідерландах")]
    [InlineData("ru", "Нидерланды", "в Нидерландах")]
    public async Task NetherlandsLocalizationHomeCardAndOverviewRender(string language, string name, string locationName)
    {
        var culture = CultureInfo.GetCultureInfo(language);
        Assert.Equal(name, Resources.GetString("Country.Netherlands", culture));
        Assert.Equal(locationName, Resources.GetString("Country.Netherlands.In", culture));

        var home = WebUtility.HtmlDecode(await RenderAsync<Home>(language));
        Assert.Contains(name, home);
        Assert.Contains(locationName, home);
        Assert.Contains("🇳🇱", home);
        Assert.Contains("href=\"/countries/netherlands\"", home);
        Assert.DoesNotContain("Country.Netherlands", home, StringComparison.Ordinal);

        var overview = WebUtility.HtmlDecode(await RenderAsync<CountryOverview>(language,
            new Dictionary<string, object?> { [nameof(CountryOverview.CountrySlug)] = "netherlands" },
            "http://localhost/countries/netherlands"));
        Assert.Contains(name, overview);
        Assert.Contains(locationName, overview);
        Assert.Equal(6, overview.Split("href=\"/countries/netherlands/categories/", StringSplitOptions.None).Length - 1);
        foreach (var category in Categories)
            Assert.Contains($"/countries/netherlands/categories/{category}", overview);
        Assert.DoesNotContain("Country.Netherlands", overview, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task NetherlandsCategoryPagesRespectProductionTranslationGateForAllLanguages(string language)
    {
        var catalog = await Catalog.Value;
        var entries = catalog.InformationEntries.Where(item => item.CountryId == NetherlandsId).ToArray();
        foreach (var category in Categories)
        {
            var parameters = new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = "netherlands",
                [nameof(Category.CategorySlug)] = category
            };
            var uri = $"http://localhost/countries/netherlands/categories/{category}";
            var developmentHtml = WebUtility.HtmlDecode(await RenderAsync<Category>(language, parameters, uri));
            var productionHtml = WebUtility.HtmlDecode(await RenderAsync<Category>(language, parameters, uri, Environments.Production));
            var categoryId = CategoryIds[category];
            var matching = entries.Where(entry => entry.CategoryId == categoryId).ToArray();
            Assert.Equal(category is "temporary-protection" or "employment" ? 2 : 1, matching.Length);
            foreach (var entry in matching)
            {
                var translation = Assert.Single(catalog.InformationEntryTranslations,
                    item => item.InformationEntryId == entry.Id && item.LanguageCode == language);
                Assert.Equal(PromotedArticleIds.Contains(entry.Id) ? VerificationStatus.Verified : VerificationStatus.NeedsReview,
                    translation.VerificationStatus);
                Assert.Contains(translation.Title, developmentHtml);
                Assert.Contains(translation.Summary, developmentHtml);
                if (PromotedArticleIds.Contains(entry.Id))
                {
                    Assert.Contains(translation.Title, productionHtml);
                    Assert.Contains(translation.Summary, productionHtml);
                }
                Assert.DoesNotContain("Editorial source set", developmentHtml, StringComparison.OrdinalIgnoreCase);
                foreach (var source in catalog.OfficialSources.Where(item => item.InformationEntryId == entry.Id))
                {
                    Assert.Contains($"href=\"{source.Url.AbsoluteUri}\"", developmentHtml);
                    if (PromotedArticleIds.Contains(entry.Id))
                        Assert.Contains($"href=\"{source.Url.AbsoluteUri}\"", productionHtml);
                }
            }

            foreach (var heldEntry in matching.Where(entry => !PromotedArticleIds.Contains(entry.Id)))
            {
                var translation = Assert.Single(catalog.InformationEntryTranslations,
                    item => item.InformationEntryId == heldEntry.Id && item.LanguageCode == language);
                Assert.DoesNotContain(translation.Title, productionHtml);
                Assert.DoesNotContain(translation.Summary, productionHtml);
                var firstContentLine = translation.Content.Split('\n', StringSplitOptions.RemoveEmptyEntries)[0];
                var firstContentText = firstContentLine.StartsWith("## ", StringComparison.Ordinal)
                    ? firstContentLine[3..]
                    : firstContentLine;
                Assert.DoesNotContain(firstContentText, productionHtml);
                Assert.DoesNotContain($"id=\"entry-{heldEntry.Id:N}\"", productionHtml);
            }
            Assert.DoesNotContain("Country.Netherlands", productionHtml, StringComparison.Ordinal);
            Assert.DoesNotContain("Category.", productionHtml, StringComparison.Ordinal);
        }
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task NetherlandsIsSelectableInSearchButNeedsReviewEntriesStayOutsideSearchResults(string language)
    {
        var catalog = await Catalog.Value;
        var html = WebUtility.HtmlDecode(await RenderAsync<Search>(language,
            uri: "http://localhost/search?country=netherlands"));
        Assert.Contains("value=\"netherlands\"", html);
        Assert.Contains(Resources.GetString("Country.Netherlands", CultureInfo.GetCultureInfo(language))!, html);
        foreach (var entry in catalog.InformationEntries.Where(item => item.CountryId == NetherlandsId))
        foreach (var translation in catalog.InformationEntryTranslations.Where(item =>
                     item.InformationEntryId == entry.Id && item.LanguageCode == language))
        {
            if (PromotedArticleIds.Contains(entry.Id))
                Assert.Contains(translation.Title, html);
            else
                Assert.DoesNotContain(translation.Title, html);
        }
    }

    [Fact]
    public async Task CountryComparisonV2HasTwelveApprovedAndTwelveDraftNetherlandsFacts()
    {
        var json = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json"));
        using var document = JsonDocument.Parse(json);
        var countrySlugs = FindCountrySlugs(document.RootElement).ToArray();
        Assert.Equal(24, countrySlugs.Count(slug => string.Equals(slug, "netherlands", StringComparison.OrdinalIgnoreCase)));
        var statuses = FindCountryStatuses(document.RootElement, "netherlands").ToArray();
        Assert.Equal(24, statuses.Length);
        Assert.Equal(12, statuses.Count(status => status == "Approved"));
        Assert.Equal(12, statuses.Count(status => status == "Draft"));
    }

    private static IEnumerable<string> FindCountrySlugs(JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("CountrySlug") && property.Value.ValueKind == JsonValueKind.String)
                    yield return property.Value.GetString()!;
                foreach (var nested in FindCountrySlugs(property.Value))
                    yield return nested;
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in element.EnumerateArray())
                foreach (var nested in FindCountrySlugs(item))
                    yield return nested;
        }
    }

    private static IEnumerable<string> FindCountryStatuses(JsonElement element, string countrySlug)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            var isTargetCountry = element.TryGetProperty("CountrySlug", out var slug) &&
                slug.ValueKind == JsonValueKind.String &&
                string.Equals(slug.GetString(), countrySlug, StringComparison.OrdinalIgnoreCase);
            if (isTargetCountry && element.TryGetProperty("Status", out var status) && status.ValueKind == JsonValueKind.String)
                yield return status.GetString()!;
            foreach (var property in element.EnumerateObject())
                foreach (var nested in FindCountryStatuses(property.Value, countrySlug))
                    yield return nested;
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in element.EnumerateArray())
                foreach (var nested in FindCountryStatuses(item, countrySlug))
                    yield return nested;
        }
    }

    private static Dictionary<string, string> ReadEditorialSources(string root, string articleKey, string draft)
    {
        var folder = DraftFolders[articleKey];
        var matrixPath = Path.Combine(root, "docs", "content-research", "netherlands-batch-2026-10-20", folder, "SOURCES_AND_CLAIMS.md");
        var sourceSet = draft.Split("## Editorial source set", 2, StringSplitOptions.None)[1];
        var ids = Regex.Matches(sourceSet, @"\b[A-Z]+\d*-S\d+\b")
            .Cast<Match>().Select(match => match.Value).Where(id => id != "TP01-S09")
            .ToHashSet(StringComparer.Ordinal);
        var matrix = File.ReadAllText(matrixPath);
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var id in ids)
        {
            var row = matrix.Split('\n').FirstOrDefault(line => line.StartsWith($"| {id} |", StringComparison.Ordinal));
            Assert.NotNull(row);
            var cells = row!.Trim('|').Split('|').Select(cell => cell.Trim()).ToArray();
            Assert.True(cells.Length >= 3, $"Source row {id} is malformed.");
            var url = new Uri(cells[2], UriKind.Absolute).AbsoluteUri;
            Assert.True(result.TryAdd(url, cells[1]), $"Duplicate URL {url} in article source set.");
        }
        return result;
    }

    private static (string Title, string Summary, string Content) ParseDraft(string markdown, string language)
    {
        var match = Regex.Match(markdown,
            $"^## {language}\\s*\\n(.*?)(?=^## (?:EN|UK|RU)\\s*$|\\z)", RegexOptions.Multiline | RegexOptions.Singleline);
        Assert.True(match.Success, $"Draft section {language} is missing.");
        var section = match.Groups[1].Value.Split("### Claim coverage", 2, StringSplitOptions.None)[0];
        string Field(string name)
        {
            var field = Regex.Match(section, $"^{name}:\\s*\\n([^\\r\\n]+)", RegexOptions.Multiline);
            Assert.True(field.Success, $"Draft field {name} is missing.");
            return field.Groups[1].Value.Trim();
        }
        var content = Regex.Match(section, "^Content:\\s*\\n(.*)$", RegexOptions.Multiline | RegexOptions.Singleline);
        Assert.True(content.Success, $"Content field for {language} is missing.");
        return (Field("Title"), Field("Summary"), content.Groups[1].Value.Trim());
    }

    private static string NormalizeLines(string value) => value.Replace("\r\n", "\n").Trim();

    private static string FindRepositoryRoot()
    {
        for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "docs", "content-drafts", "netherlands-v1", "NL-TP-01.md")))
                return directory.FullName;
        }
        throw new DirectoryNotFoundException("Could not locate the Netherlands editorial drafts.");
    }

    private static async Task<string> RenderAsync<TComponent>(string language,
        IDictionary<string, object?>? parameters = null, string uri = "http://localhost/",
        string environmentName = "Development") where TComponent : IComponent
    {
        var oldCulture = CultureInfo.CurrentCulture;
        var oldUiCulture = CultureInfo.CurrentUICulture;
        var culture = CultureInfo.GetCultureInfo(language);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        try
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton(await Catalog.Value);
            services.AddSingleton(new StepByStepGuideCatalog([]));
            services.AddSingleton<NavigationManager>(_ => new TestNavigationManager(uri));
            await ComparisonTestServices.RegisterAsync(services);
            services.AddSingleton<IWebHostEnvironment>(new TestEnvironment(environmentName));
            using var provider = services.BuildServiceProvider();
            await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());
            var parameterView = parameters is null ? ParameterView.Empty : ParameterView.FromDictionary(parameters);
            return await renderer.Dispatcher.InvokeAsync(async () =>
            {
                var component = await renderer.RenderComponentAsync<TComponent>(parameterView);
                return component.ToHtmlString();
            });
        }
        finally
        {
            CultureInfo.CurrentCulture = oldCulture;
            CultureInfo.CurrentUICulture = oldUiCulture;
        }
    }

    private sealed class TestNavigationManager : NavigationManager
    {
        public TestNavigationManager(string uri) => Initialize("http://localhost/", uri);
        protected override void NavigateToCore(string uri, bool forceLoad) => Uri = ToAbsoluteUri(uri).AbsoluteUri;
    }

    private sealed class TestEnvironment(string environmentName) : IWebHostEnvironment
    {
        public string ApplicationName { get; set; } = "RefugeeNavigator.Tests";
        public string EnvironmentName { get; set; } = environmentName;
        public string WebRootPath { get; set; } = AppContext.BaseDirectory;
        public string ContentRootPath { get; set; } = AppContext.BaseDirectory;
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }
}
