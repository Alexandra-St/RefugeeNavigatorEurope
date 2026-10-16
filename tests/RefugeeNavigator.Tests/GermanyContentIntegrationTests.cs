using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Search;

namespace RefugeeNavigator.Tests;

public sealed class GermanyContentIntegrationTests
{
    private static readonly Guid GermanyId = Guid.Parse("10000000-0000-0000-0000-000000000002");
    private static readonly Guid DenmarkId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly Guid PolandId = Guid.Parse("10000000-0000-0000-0000-000000000003");
    private static readonly Guid[] GermanyCategoryIds =
    [
        Guid.Parse("20000000-0000-0000-0000-000000000001"),
        Guid.Parse("20000000-0000-0000-0000-000000000002"),
        Guid.Parse("20000000-0000-0000-0000-000000000003"),
        Guid.Parse("20000000-0000-0000-0000-000000000004"),
        Guid.Parse("20000000-0000-0000-0000-000000000005"),
        Guid.Parse("20000000-0000-0000-0000-000000000006")
    ];
    private static readonly IReadOnlyDictionary<string, Guid> ExpectedCategoryIds = new Dictionary<string, Guid>
    {
        ["1a6b97c4-eb9e-5590-8715-c95f5f95677e"] = GermanyCategoryIds[0],
        ["33106e71-115f-5809-9066-7e3d3e5c57a3"] = GermanyCategoryIds[0],
        ["283d4519-21f5-5be2-a534-1bd2606d95fc"] = GermanyCategoryIds[1],
        ["c4895b5a-eacd-53d7-a86f-f504dd615c76"] = GermanyCategoryIds[1],
        ["895b79b4-ce0d-5bee-9919-42ce3d76ecdb"] = GermanyCategoryIds[2],
        ["fd041e53-7794-5c1f-9ae4-131930fe69ee"] = GermanyCategoryIds[3],
        ["f5e0f499-0f60-5465-85f8-de6345029b59"] = GermanyCategoryIds[4],
        ["31f5802b-26a0-5cd0-bb24-0a9a4afa706a"] = GermanyCategoryIds[5]
    };
    private static readonly string[] Languages = ["uk", "en", "ru"];
    private static readonly IReadOnlyDictionary<string, int> SourceCounts = new Dictionary<string, int>
    {
        ["1a6b97c4-eb9e-5590-8715-c95f5f95677e"] = 10,
        ["33106e71-115f-5809-9066-7e3d3e5c57a3"] = 10,
        ["283d4519-21f5-5be2-a534-1bd2606d95fc"] = 11,
        ["c4895b5a-eacd-53d7-a86f-f504dd615c76"] = 9,
        ["895b79b4-ce0d-5bee-9919-42ce3d76ecdb"] = 12,
        ["fd041e53-7794-5c1f-9ae4-131930fe69ee"] = 9,
        ["f5e0f499-0f60-5465-85f8-de6345029b59"] = 10,
        ["31f5802b-26a0-5cd0-bb24-0a9a4afa706a"] = 15
    };

    [Fact]
    public async Task GermanyBatchLoadsWithCompleteNeedsReviewTranslationsAndLinkedSources()
    {
        var catalog = await LoadCatalogAsync();
        var entries = catalog.InformationEntries.Where(entry => entry.CountryId == GermanyId).ToArray();
        var entryIds = entries.Select(entry => entry.Id).ToHashSet();
        var translations = catalog.InformationEntryTranslations.Where(item => entryIds.Contains(item.InformationEntryId)).ToArray();
        var sources = catalog.OfficialSources.Where(source => entryIds.Contains(source.InformationEntryId)).ToArray();

        Assert.Equal(8, entries.Length);
        Assert.Equal(SourceCounts.Keys.Select(Guid.Parse).Order().ToArray(), entries.Select(entry => entry.Id).Order().ToArray());
        Assert.Equal(24, translations.Length);
        Assert.Equal(86, sources.Length);
        Assert.DoesNotContain(sources, source => source.Url.ToString() == "https://www.germany4ukraine.de/EN/arbeit-und-soziales/ukrainer-arbeiten-in-deutschland/seite_node.html");
        Assert.Equal(GermanyCategoryIds.Order().ToArray(), entries.Select(entry => entry.CategoryId).Distinct().Order().ToArray());
        Assert.All(entries, entry => Assert.Equal(ExpectedCategoryIds[entry.Id.ToString()], entry.CategoryId));
        Assert.All(translations, translation =>
        {
            Assert.Contains(translation.LanguageCode, Languages);
            Assert.Equal(VerificationStatus.NeedsReview, translation.VerificationStatus);
            Assert.False(string.IsNullOrWhiteSpace(translation.Title));
            Assert.False(string.IsNullOrWhiteSpace(translation.Summary));
            Assert.False(string.IsNullOrWhiteSpace(translation.Content));
            Assert.Equal(3, translations.Count(other => other.InformationEntryId == translation.InformationEntryId));
            Assert.All(Languages, language => Assert.Contains(translations, other =>
                other.InformationEntryId == translation.InformationEntryId && other.LanguageCode == language));
        });
        Assert.All(entries, entry => Assert.Equal(SourceCounts[entry.Id.ToString()], sources.Count(source => source.InformationEntryId == entry.Id)));
        Assert.Equal(sources.Length, sources.Select(source => source.Id).Distinct().Count());
        Assert.All(sources, source =>
        {
            Assert.Contains(source.InformationEntryId, entryIds);
            Assert.Equal("https", source.Url.Scheme);
            Assert.False(string.IsNullOrWhiteSpace(source.Title));
            Assert.InRange(source.LastCheckedAt,
                new DateTimeOffset(2026, 10, 15, 23, 22, 10, TimeSpan.Zero),
                new DateTimeOffset(2026, 10, 16, 0, 21, 41, TimeSpan.Zero));
        });

        Assert.Equal(8, catalog.InformationEntries.Count(entry => entry.CountryId == DenmarkId));
        Assert.Equal(101, catalog.OfficialSources.Count(source =>
            catalog.InformationEntries.Any(entry => entry.Id == source.InformationEntryId && entry.CountryId == DenmarkId)));
        Assert.DoesNotContain(catalog.InformationEntries, entry => entry.CountryId == PolandId);
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task GermanyArticlesRenderInCategoryPagesAndComparison(string language)
    {
        var catalog = await LoadCatalogAsync();
        var germanyEntries = catalog.InformationEntries.Where(entry => entry.CountryId == GermanyId).ToArray();
        foreach (var category in catalog.Categories.Where(category => GermanyCategoryIds.Contains(category.Id)))
        {
            var expectedEntries = germanyEntries.Where(entry => entry.CategoryId == category.Id).ToArray();
            var expectedTranslations = expectedEntries.Select(entry => catalog.InformationEntryTranslations.Single(item =>
                item.InformationEntryId == entry.Id && item.LanguageCode == language)).ToArray();

            var categoryHtml = await RenderCategoryAsync(catalog, category.Slug, language);
            var visibleCategoryHtml = WebUtility.HtmlDecode(categoryHtml);
            Assert.Equal(expectedEntries.Length, Regex.Matches(categoryHtml, "<article class=\"information-entry\"").Count);
            Assert.Equal(expectedEntries.Sum(entry => catalog.OfficialSources.Count(source => source.InformationEntryId == entry.Id)),
                Regex.Matches(categoryHtml, "<a href=\"https://").Count);
            Assert.Equal(expectedTranslations.Sum(item => Regex.Matches(item.Content, "(?m)^## ").Count),
                Regex.Matches(categoryHtml, "<h3 class=\"entry-section-heading\"").Count);
            Assert.DoesNotContain("## ", visibleCategoryHtml, StringComparison.Ordinal);
            var renderedEntryLists = Regex.Matches(categoryHtml, "<ul\\b[^>]*class=\"entry-list\"[^>]*>(.*?)</ul>", RegexOptions.Singleline)
                .Cast<Match>().ToArray();
            Assert.Equal(expectedTranslations.Sum(item => Regex.Matches(item.Content, "(?m)^- ").Count),
                renderedEntryLists.Sum(list => Regex.Matches(list.Groups[1].Value, "<li\\b").Count));

            foreach (var translation in expectedTranslations)
            {
                Assert.Contains(translation.Title, visibleCategoryHtml);
                Assert.Contains(translation.Summary, visibleCategoryHtml);
                Assert.Contains(StatusLabel(language), visibleCategoryHtml);
                Assert.DoesNotContain("<script", categoryHtml, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("https://", translation.Content, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("**", translation.Content, StringComparison.Ordinal);
                foreach (var heading in Regex.Matches(translation.Content, "(?m)^## (.+)$").Cast<Match>())
                {
                    Assert.Contains(heading.Groups[1].Value, visibleCategoryHtml);
                }
            }

            var compareHtml = await RenderCompareAsync(catalog, category.Slug, language);
            var visibleCompareHtml = WebUtility.HtmlDecode(compareHtml);
            foreach (var translation in expectedTranslations)
            {
                Assert.Contains(translation.Title, visibleCompareHtml);
            }

            foreach (var translation in expectedTranslations)
            {
                var results = ArticleSearch.Search(
                    catalog,
                    translation.Title,
                    "germany",
                    category.Slug,
                    VerificationStatus.NeedsReview.ToString(),
                    language);
                var result = Assert.Single(results);
                Assert.Equal(translation.InformationEntryId, result.Entry.Id);
                Assert.Equal(language, result.Translation.LanguageCode);
            }
        }
    }

    [Fact]
    public async Task ImportantGermanyContentRetainsRecordedConditionsAndUncertainty()
    {
        var catalog = await LoadCatalogAsync();
        var articles = catalog.InformationEntries.Where(entry => entry.CountryId == GermanyId)
            .ToDictionary(entry => entry.Id.ToString(), entry => Languages.ToDictionary(language => language,
                language => catalog.InformationEntryTranslations.Single(item =>
                    item.InformationEntryId == entry.Id && item.LanguageCode == language).Content));
        var protection = articles["1a6b97c4-eb9e-5590-8715-c95f5f95677e"];
        var afterProtection = articles["33106e71-115f-5809-9066-7e3d3e5c57a3"];
        var workRights = articles["c4895b5a-eacd-53d7-a86f-f504dd615c76"];
        var healthcare = articles["895b79b4-ce0d-5bee-9919-42ce3d76ecdb"];
        var education = articles["f5e0f499-0f60-5465-85f8-de6345029b59"];
        var social = articles["31f5802b-26a0-5cd0-bb24-0a9a4afa706a"];

        foreach (var language in Languages)
        {
            var protectionText = protection[language];
            Assert.Contains("2027", protectionText);
            Assert.Contains("2028", protectionText);
            Assert.Contains("§2(3)", protectionText);
            Assert.Contains("UkraineAufenthFGV", protectionText);
            Assert.Contains("Germany4Ukraine", protectionText);

            Assert.Contains("2027", workRights[language]);
            Assert.Contains("2028", workRights[language]);
            Assert.Contains("SGB V", healthcare[language]);
            Assert.Contains("AsylbLG", healthcare[language]);
            Assert.Contains("§24(4)", education[language]);
            Assert.Contains("Leistungsrechtsanpassungsgesetz", social[language]);
            Assert.Contains("31", social[language]);
            Assert.Contains("2025", social[language]);
        }

        Assert.Contains("entry by 4 December 2027", protection["en"]);
        Assert.Contains("в’їзд не пізніше 4 грудня 2027 року", protection["uk"]);
        Assert.Contains("въезд не позднее 4 декабря 2027 года", protection["ru"]);
        Assert.Contains("От одного года до трёх лет ребёнок", education["ru"]);
        Assert.DoesNotContain("С одного до трёх лет ребёнок", education["ru"]);
        Assert.Contains("см. также статьи о медицинской помощи и жилье", social["ru"]);
        Assert.DoesNotContain("см. также статьям о медицинской помощи и жилье", social["ru"]);
        Assert.Contains("through 4 March 2028", protection["en"]);
        Assert.Contains("through 4 March 2027", protection["en"]);
        Assert.Contains("до 4 березня 2028 року", protection["uk"]);
        Assert.Contains("до 4 березня 2027 року", protection["uk"]);
        Assert.Contains("до 4 марта 2028 года", protection["ru"]);
        Assert.Contains("до 4 марта 2027 года", protection["ru"]);
        foreach (var language in Languages)
        {
            var dateScopeText = language switch
            {
                "uk" => "різних правових статусів",
                "en" => "different legal statuses",
                "ru" => "разным правовым статусам",
                _ => throw new InvalidOperationException($"Unexpected language '{language}'.")
            };

            foreach (var article in new[] { protection, afterProtection, workRights })
            {
                Assert.Contains("2028", article[language]);
                Assert.Contains("2027", article[language]);
                Assert.Contains(dateScopeText, article[language], StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("indexed", article[language], StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("індекс", article[language], StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("индекс", article[language], StringComparison.OrdinalIgnoreCase);
            }
        }

        Assert.DoesNotContain("Labour Law", protection["en"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("ordinary visa-free travel", protection["en"]);
        Assert.Contains("Brandenburg state ministry", protection["en"]);
        Assert.Contains("same Member State", protection["en"]);
        Assert.DoesNotContain("Germany4Ukraine’s Labour Law", protection["en"], StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Labour Law", workRights["en"], StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("inconsistent", workRights["en"], StringComparison.OrdinalIgnoreCase);
        Assert.Contains("в том же государстве-члене", protection["ru"]);
        Assert.Contains("тій самій державі-члені", protection["uk"]);
        Assert.Contains("Treatment, insurance and interpretation are not universally free.", healthcare["en"]);
        Assert.Contains("Land", education["en"]);
        Assert.Contains("bill", social["en"], StringComparison.OrdinalIgnoreCase);
    }

    private static string StatusLabel(string language) => language switch
    {
        "uk" => "Потребує перевірки",
        "en" => "Needs review",
        "ru" => "Требует проверки",
        _ => throw new ArgumentOutOfRangeException(nameof(language))
    };

    private static async Task<CatalogData> LoadCatalogAsync() =>
        await new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();

    private static Task<string> RenderCategoryAsync(CatalogData catalog, string categorySlug, string language) =>
        RenderAsync(catalog, language, async renderer =>
        {
            var parameters = ParameterView.FromDictionary(new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = "germany",
                [nameof(Category.CategorySlug)] = categorySlug
            });
            var component = await renderer.RenderComponentAsync<Category>(parameters);
            return component.ToHtmlString();
        });

    private static Task<string> RenderCompareAsync(CatalogData catalog, string categorySlug, string language) =>
        RenderAsync(catalog, language, async renderer =>
        {
            var component = await renderer.RenderComponentAsync<Compare>(ParameterView.Empty);
            return component.ToHtmlString();
        }, $"http://localhost/compare?country1=denmark&country2=germany&category={categorySlug}");

    private static async Task<string> RenderAsync(
        CatalogData catalog,
        string language,
        Func<HtmlRenderer, Task<string>> render,
        string navigationUri = "http://localhost/")
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
            services.AddSingleton(catalog);
            services.AddSingleton<NavigationManager>(_ => new TestNavigationManager(navigationUri));
            using var provider = services.BuildServiceProvider();
            await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());
            return await renderer.Dispatcher.InvokeAsync(() => render(renderer));
        }
        finally
        {
            CultureInfo.CurrentCulture = oldCulture;
            CultureInfo.CurrentUICulture = oldUiCulture;
        }
    }

    private sealed class TestNavigationManager : NavigationManager
    {
        public TestNavigationManager(string uri)
        {
            Initialize("http://localhost/", uri);
        }

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            Uri = ToAbsoluteUri(uri).AbsoluteUri;
            NotifyLocationChanged(isInterceptedLink: false);
        }
    }
}
