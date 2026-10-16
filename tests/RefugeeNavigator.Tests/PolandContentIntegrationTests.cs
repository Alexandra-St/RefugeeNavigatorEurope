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

public sealed class PolandContentIntegrationTests
{
    private static readonly Guid PolandId = Guid.Parse("10000000-0000-0000-0000-000000000003");
    private static readonly Guid DenmarkId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly Guid GermanyId = Guid.Parse("10000000-0000-0000-0000-000000000002");
    private static readonly Guid[] CategoryIds =
    [
        Guid.Parse("20000000-0000-0000-0000-000000000001"),
        Guid.Parse("20000000-0000-0000-0000-000000000002"),
        Guid.Parse("20000000-0000-0000-0000-000000000003"),
        Guid.Parse("20000000-0000-0000-0000-000000000004"),
        Guid.Parse("20000000-0000-0000-0000-000000000005"),
        Guid.Parse("20000000-0000-0000-0000-000000000006")
    ];
    private static readonly string[] Languages = ["uk", "en", "ru"];
    private static readonly IReadOnlyDictionary<string, int> ExpectedSourceCounts = new Dictionary<string, int>
    {
        ["218207bd-2a85-59e1-a945-eedb0604392e"] = 10,
        ["c230d45f-9c76-5912-8c97-dc7d0689baa6"] = 5,
        ["a323701f-367e-52bb-9c33-45c4156a1350"] = 5,
        ["7797d03c-6873-50cd-aef6-0d94a2f5fcab"] = 5,
        ["d7e0716b-154d-5db3-8790-f73684c07271"] = 4,
        ["1f310b3c-dceb-5e3a-90d0-d3a705ca5c53"] = 5,
        ["32ca34e9-22d9-5f59-88a9-bbd7ffd7b5cd"] = 3,
        ["a5892591-d082-57ce-acf9-5c39ab4f9b84"] = 8
    };
    private static readonly IReadOnlyDictionary<string, Guid> ArticleIds = new Dictionary<string, Guid>
    {
        ["temporary-protection-application"] = Guid.Parse("218207bd-2a85-59e1-a945-eedb0604392e"),
        ["after-temporary-protection"] = Guid.Parse("c230d45f-9c76-5912-8c97-dc7d0689baa6"),
        ["finding-employment"] = Guid.Parse("a323701f-367e-52bb-9c33-45c4156a1350"),
        ["work-authorization-rights"] = Guid.Parse("7797d03c-6873-50cd-aef6-0d94a2f5fcab"),
        ["healthcare"] = Guid.Parse("d7e0716b-154d-5db3-8790-f73684c07271"),
        ["housing"] = Guid.Parse("1f310b3c-dceb-5e3a-90d0-d3a705ca5c53"),
        ["education-language"] = Guid.Parse("32ca34e9-22d9-5f59-88a9-bbd7ffd7b5cd"),
        ["social-assistance"] = Guid.Parse("a5892591-d082-57ce-acf9-5c39ab4f9b84")
    };

    [Fact]
    public async Task PolandBatchLoadsWithCompleteNeedsReviewTranslationsAndLinkedSources()
    {
        var catalog = await LoadCatalogAsync();
        var entries = catalog.InformationEntries.Where(entry => entry.CountryId == PolandId).ToArray();
        var entryIds = entries.Select(entry => entry.Id).ToHashSet();
        var translations = catalog.InformationEntryTranslations
            .Where(translation => entryIds.Contains(translation.InformationEntryId)).ToArray();
        var sources = catalog.OfficialSources.Where(source => entryIds.Contains(source.InformationEntryId)).ToArray();

        Assert.Equal(24, catalog.InformationEntries.Count);
        Assert.Equal(8, entries.Length);
        Assert.Equal(8, catalog.InformationEntries.Count(entry => entry.CountryId == DenmarkId));
        Assert.Equal(8, catalog.InformationEntries.Count(entry => entry.CountryId == GermanyId));
        Assert.Equal(8, catalog.InformationEntries.Count(entry => entry.CountryId == PolandId));
        Assert.Equal(24, translations.Length);
        Assert.Equal(24, translations.Count(translation => translation.VerificationStatus == VerificationStatus.NeedsReview));
        Assert.Equal(0, translations.Count(translation => translation.VerificationStatus == VerificationStatus.Verified));
        Assert.Equal(24, translations.Select(translation => (translation.InformationEntryId, translation.LanguageCode)).Distinct().Count());
        Assert.Equal(entries.Length, entries.Select(entry => entry.Id).Distinct().Count());
        Assert.Equal(CategoryIds.Order().ToArray(), entries.Select(entry => entry.CategoryId).Distinct().Order().ToArray());
        Assert.All(entries, entry => Assert.Equal(3, translations.Count(translation => translation.InformationEntryId == entry.Id)));
        Assert.All(entries, entry => Assert.Equal(Languages.Order().ToArray(), translations
            .Where(translation => translation.InformationEntryId == entry.Id)
            .Select(translation => translation.LanguageCode).Order().ToArray()));
        Assert.All(translations, translation =>
        {
            Assert.Equal(VerificationStatus.NeedsReview, translation.VerificationStatus);
            Assert.False(string.IsNullOrWhiteSpace(translation.Title));
            Assert.False(string.IsNullOrWhiteSpace(translation.Summary));
            Assert.False(string.IsNullOrWhiteSpace(translation.Content));
            Assert.Equal(new DateOnly(2026, 10, 16), DateOnly.FromDateTime(translation.LastUpdatedAt.UtcDateTime));
            Assert.DoesNotContain("Research draft", translation.Content, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("OFFICIAL SOURCES", translation.Content, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("SUPPORTED", translation.Content, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("UNVERIFIED", translation.Content, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("https://", translation.Content, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("](", translation.Content, StringComparison.Ordinal);
            Assert.DoesNotContain("**", translation.Content, StringComparison.Ordinal);
            Assert.Equal(4, Regex.Matches(translation.Content, "(?m)^## ").Count);
        });
        var correctedTemporaryProtectionTranslations = translations
            .Where(translation => translation.InformationEntryId == ArticleIds["temporary-protection-application"])
            .ToDictionary(translation => translation.LanguageCode);
        Assert.Equal(new DateTimeOffset(2026, 10, 16, 2, 17, 2, TimeSpan.Zero),
            correctedTemporaryProtectionTranslations["uk"].LastUpdatedAt);
        Assert.Equal(new DateTimeOffset(2026, 10, 16, 2, 17, 2, TimeSpan.Zero),
            correctedTemporaryProtectionTranslations["ru"].LastUpdatedAt);
        Assert.Equal(new DateTimeOffset(2026, 10, 16, 1, 56, 53, TimeSpan.Zero),
            correctedTemporaryProtectionTranslations["en"].LastUpdatedAt);
        Assert.Equal(2, translations.Select(translation => translation.LastUpdatedAt).Distinct().Count());

        Assert.Equal(45, sources.Length);
        Assert.Equal(45, sources.Select(source => source.Id).Distinct().Count());
        Assert.All(entries, entry => Assert.Equal(ExpectedSourceCounts[entry.Id.ToString()], sources.Count(source => source.InformationEntryId == entry.Id)));
        Assert.All(sources, source =>
        {
            Assert.Contains(source.InformationEntryId, entryIds);
            Assert.Equal("https", source.Url.Scheme);
            Assert.False(string.IsNullOrWhiteSpace(source.Title));
            Assert.Equal(new DateTimeOffset(2026, 10, 16, 0, 0, 0, TimeSpan.Zero), source.LastCheckedAt);
        });
        Assert.Equal(34, sources.Select(source => source.Url.AbsoluteUri).Distinct(StringComparer.Ordinal).Count());
        Assert.Equal(2, entries.Count(entry => entry.CategoryId == CategoryIds[0]));
        Assert.Equal(2, entries.Count(entry => entry.CategoryId == CategoryIds[1]));
        Assert.Equal(1, entries.Count(entry => entry.CategoryId == CategoryIds[2]));
        Assert.Equal(1, entries.Count(entry => entry.CategoryId == CategoryIds[3]));
        Assert.Equal(1, entries.Count(entry => entry.CategoryId == CategoryIds[4]));
        Assert.Equal(1, entries.Count(entry => entry.CategoryId == CategoryIds[5]));
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task PolandArticlesRenderSearchAndCompareAcrossAllCategories(string language)
    {
        var catalog = await LoadCatalogAsync();
        var entries = catalog.InformationEntries.Where(entry => entry.CountryId == PolandId).ToArray();

        foreach (var category in catalog.Categories.Where(category => CategoryIds.Contains(category.Id)))
        {
            var expectedEntries = entries.Where(entry => entry.CategoryId == category.Id).ToArray();
            var expectedTranslations = expectedEntries.Select(entry => catalog.InformationEntryTranslations.Single(translation =>
                translation.InformationEntryId == entry.Id && translation.LanguageCode == language)).ToArray();
            var html = await RenderCategoryAsync(catalog, category.Slug, language);
            var visibleHtml = WebUtility.HtmlDecode(html);

            Assert.Equal(expectedEntries.Length, Regex.Matches(html, "<article class=\"information-entry\"").Count);
            Assert.Equal(expectedEntries.Sum(entry => catalog.OfficialSources.Count(source => source.InformationEntryId == entry.Id)),
                Regex.Matches(html, "<a href=\"https://").Count);
            Assert.Equal(expectedTranslations.Length * 4, Regex.Matches(html, "<h3 class=\"entry-section-heading\"").Count);
            Assert.DoesNotContain("## ", visibleHtml, StringComparison.Ordinal);
            Assert.DoesNotContain("[", visibleHtml, StringComparison.Ordinal);
            Assert.DoesNotContain("](", visibleHtml, StringComparison.Ordinal);

            foreach (var translation in expectedTranslations)
            {
                Assert.Contains(translation.Title, visibleHtml);
                Assert.Contains(translation.Summary, visibleHtml);
                Assert.Contains(StatusLabel(language), visibleHtml);
                Assert.Contains("datetime=\"2026-10-16T", html);
                Assert.DoesNotContain("<script", html, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("**", visibleHtml, StringComparison.Ordinal);
                foreach (var source in catalog.OfficialSources.Where(source => source.InformationEntryId == translation.InformationEntryId))
                {
                    Assert.Contains($"href=\"{source.Url.AbsoluteUri}\"", html);
                    Assert.Contains(source.Title, visibleHtml);
                }

                var searchResults = ArticleSearch.Search(catalog, translation.Title, "poland", category.Slug,
                    VerificationStatus.NeedsReview.ToString(), language);
                var result = Assert.Single(searchResults);
                Assert.Equal(translation.InformationEntryId, result.Entry.Id);
                Assert.Equal(language, result.Translation.LanguageCode);
            }

            var comparisonHtml = await RenderCompareAsync(catalog, category.Slug, language);
            var visibleComparisonHtml = WebUtility.HtmlDecode(comparisonHtml);
            foreach (var translation in expectedTranslations)
            {
                Assert.Contains(translation.Title, visibleComparisonHtml);
                Assert.Contains(translation.Title, visibleHtml);
            }

            var denmarkTranslation = catalog.InformationEntryTranslations.First(translation =>
                translation.LanguageCode == language && catalog.InformationEntries.Any(entry =>
                    entry.Id == translation.InformationEntryId && entry.CountryId == DenmarkId && entry.CategoryId == category.Id));
            Assert.Contains(denmarkTranslation.Title, visibleComparisonHtml);
        }
    }

    [Theory]
    [InlineData("uk", "ПРАВИЛА")]
    [InlineData("en", "RULES")]
    [InlineData("ru", "ПРАВИЛА")]
    public async Task LongTemporaryProtectionRulesRenderAsFourSeparateParagraphs(string language, string rulesHeading)
    {
        var catalog = await LoadCatalogAsync();
        var html = await RenderCategoryAsync(catalog, "temporary-protection", language);
        var firstArticle = Regex.Match(html, "<article class=\"information-entry\".*?</article>", RegexOptions.Singleline);
        Assert.True(firstArticle.Success);
        var decodedArticle = WebUtility.HtmlDecode(firstArticle.Value);
        var rulesContent = Regex.Match(decodedArticle,
            $"<h3\\b[^>]*class=\"entry-section-heading\"[^>]*>{Regex.Escape(rulesHeading)}</h3>(.*?)<h3\\b[^>]*class=\"entry-section-heading\"",
            RegexOptions.Singleline);
        Assert.True(rulesContent.Success, decodedArticle);
        Assert.Equal(4, Regex.Matches(rulesContent.Groups[1].Value, "<p\\b[^>]*class=\"entry-content\"").Count);
    }

    [Fact]
    public async Task IntegratedContentPreservesCriticalPolishConditionsAndLimits()
    {
        var catalog = await LoadCatalogAsync();
        string Content(string articleKey, string language) => catalog.InformationEntryTranslations.Single(translation =>
            translation.InformationEntryId == ArticleIds[articleKey] && translation.LanguageCode == language).Content;

        foreach (var language in Languages)
        {
            var protection = Content("temporary-protection-application", language);
            var (euExpiry, correctedStart, correctedException, sameMemberState, staleKsiDates, identityDeadline, declarationCohort) = language switch
            {
                "uk" => ("до 4 березня 2028 року", "5 серпня 2026 року", "до 4 серпня включно",
                    "саме в цій державі", "31/30 липня", "31 серпня 2026 року", "лише на підставі декларації"),
                "en" => ("to 4 March 2028", "5 August 2026", "by 4 August", "same Member State",
                    "31/30 July", "31 August 2026", "only on the basis of a declaration"),
                "ru" => ("до 4 марта 2028 года", "5 августа 2026 года", "до 4 августа включительно",
                    "именно в этом государстве", "31/30 июля", "31 августа 2026 года", "только на основании декларации"),
                _ => throw new ArgumentOutOfRangeException(nameof(language))
            };
            Assert.Contains(euExpiry, protection);
            Assert.Contains("2027", protection);
            Assert.Contains("106", protection);
            Assert.Contains(correctedStart, protection);
            Assert.Contains(correctedException, protection);
            Assert.Contains(sameMemberState, protection);
            Assert.Contains(staleKsiDates, protection);
            Assert.Contains(identityDeadline, protection);
            Assert.Contains(declarationCohort, protection);

            if (language == "uk")
            {
                Assert.Contains("особи, на яку поширюється стаття 106", protection);
                Assert.Contains("Офіційне виправлення до рішення (ЄС) 2026/1912", protection);
                Assert.DoesNotContain("бенефіціар", protection, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("Corrigendum", protection, StringComparison.OrdinalIgnoreCase);
            }
            else if (language == "ru")
            {
                Assert.Contains("человека, на которого распространяется статья 106", protection);
                Assert.Contains("Официальное исправление к решению (ЕС) 2026/1912", protection);
                Assert.DoesNotContain("бенефициар", protection, StringComparison.OrdinalIgnoreCase);
                Assert.DoesNotContain("Corrigendum", protection, StringComparison.OrdinalIgnoreCase);
            }

            var healthcare = Content("healthcare", language);
            Assert.Contains("PESEL UKR", healthcare);
            Assert.Contains("112", healthcare);
            var coverageLimit = language switch
            {
                "uk" => "не є повним доступом до всіх послуг",
                "en" => "not access to every public healthcare service",
                "ru" => "не означает доступа ко всем государственным медицинским услугам",
                _ => throw new ArgumentOutOfRangeException(nameof(language))
            };
            Assert.Contains(coverageLimit, healthcare);
            Assert.Contains("PESEL UKR", healthcare);
            var education = Content("education-language", language);
            Assert.Contains("24", education);
            Assert.Contains("12", education);
            var social = Content("social-assistance", language);
            Assert.Contains("800+", social);
            Assert.Contains("2026", social);
        }

        var residenceText = Content("after-temporary-protection", "en");
        Assert.Contains("365 uninterrupted days", residenceText);
        Assert.Contains("4 March 2027", residenceText);
        Assert.Contains("paper-filing exceptions", residenceText);
        Assert.Contains("365 днів", Content("after-temporary-protection", "uk"));
        Assert.Contains("винятки з електронного подання", Content("after-temporary-protection", "uk"));
        Assert.Contains("365 дней", Content("after-temporary-protection", "ru"));
        Assert.Contains("исключения из электронного способа", Content("after-temporary-protection", "ru"));

        var work = Content("work-authorization-rights", "en");
        Assert.Contains("PLN 4,806", work);
        Assert.Contains("7 days", work);
        var health = Content("healthcare", "en");
        Assert.Contains("under 18", health);
        Assert.Contains("compulsory-insurance", health);
        Assert.Contains("not access to every public healthcare service", health);
        var housing = Content("housing", "en");
        Assert.Contains("not guaranteed", housing);
        Assert.Contains("local implementation", housing);
        Assert.Contains("Do not assume that address registration", housing);
        Assert.Contains("не гарантія місця або житла", Content("housing", "uk"));
        Assert.Contains("не гарантируется каждому", Content("housing", "ru"));
        var educationContent = Content("education-language", "en");
        Assert.Contains("turn six", educationContent);
        Assert.Contains("turns seven", educationContent);
        Assert.Contains("up to 24 months", educationContent);
        Assert.Contains("up to 12 months", educationContent);
        var socialContent = Content("social-assistance", "en");
        Assert.Contains("work-activity condition", socialContent);
        Assert.Contains("school-age child must attend", socialContent);
        Assert.Contains("does not guarantee a payment", socialContent);
        Assert.Contains("професійної активності", Content("social-assistance", "uk"));
        Assert.Contains("шкільного віку — навчатися", Content("social-assistance", "uk"));
        Assert.Contains("профессиональной активности", Content("social-assistance", "ru"));
        Assert.Contains("школьного возраста — учиться", Content("social-assistance", "ru"));
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
                [nameof(Category.CountrySlug)] = "poland",
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
        }, $"http://localhost/compare?country1=poland&country2=denmark&category={categorySlug}");

    private static async Task<string> RenderAsync(CatalogData catalog, string language,
        Func<HtmlRenderer, Task<string>> render, string navigationUri = "http://localhost/")
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
