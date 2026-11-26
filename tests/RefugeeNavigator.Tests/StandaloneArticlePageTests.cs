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
using Microsoft.JSInterop;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Content;
using RefugeeNavigator.Web.Guides;
using RefugeeNavigator.Web.Localization;
using RefugeeNavigator.Web.Seo;

namespace RefugeeNavigator.Tests;

public sealed class StandaloneArticlePageTests
{
    private static readonly Lazy<Task<CatalogData>> Catalog = new(() => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());
    private static readonly ResourceManager Resources = new(
        "RefugeeNavigator.Web.Resources.Localization.SharedResource", typeof(SharedResource).Assembly);

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task StandalonePageRendersLocalizedArticleOwnedSourcesAndSeo(string language)
    {
        var catalog = await Catalog.Value;
        var entry = FindPublishedArticleWithSources(catalog);
        var country = catalog.Countries.Single(item => item.Id == entry.CountryId);
        var category = catalog.Categories.Single(item => item.Id == entry.CategoryId);
        var translation = catalog.InformationEntryTranslations.Single(item => item.InformationEntryId == entry.Id && item.LanguageCode == language);
        var expectedPath = ArticleUrlResolver.BuildPath(language, country.Slug, category.Slug, entry.Id);
        var (html, head, notFound) = await RenderAsync(catalog, language, expectedPath,
            new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = country.Slug,
                [nameof(Category.CategorySlug)] = category.Slug,
                [nameof(Category.ArticleKey)] = entry.Id.ToString("D")
            });
        var visibleHtml = WebUtility.HtmlDecode(html);
        var culture = CultureInfo.GetCultureInfo(language);

        var failures = new List<string>();
        void Check(string property, string expected, string actual, Action assertion, string excerpt)
        {
            try { assertion(); }
            catch (Xunit.Sdk.XunitException error)
            {
                failures.Add($"[{language}] {property}\nExpected: {expected}\nActual: {actual}\n{error.Message}\nRendered HTML: {excerpt}");
            }
        }
        void Contains(string property, string expected, string actual, string excerpt) =>
            Check(property, expected, actual, () => Assert.Contains(expected, actual), excerpt);

        Check("availability", "available", notFound.ToString(), () => Assert.False(notFound), html);
        var articleHeading = Regex.Match(html, "<h1\\b(?=[^>]*\\bid=\"article-title-" +
            Regex.Escape(entry.Id.ToString("N")) + "\")[^>]*>(?<title>.*?)</h1>", RegexOptions.Singleline);
        Check("article heading", $"h1 id=article-title-{entry.Id:N}", articleHeading.Value,
            () => Assert.True(articleHeading.Success), html);
        if (articleHeading.Success)
        {
            var renderedTitle = WebUtility.HtmlDecode(Regex.Replace(articleHeading.Groups["title"].Value, "<[^>]*>", string.Empty));
            Check("localized title", translation.Title, renderedTitle, () => Assert.Equal(translation.Title, renderedTitle), articleHeading.Value);
        }
        else
            failures.Add($"[{language}] localized title: prerequisite heading missing; expected {translation.Title}; actual unavailable; HTML: {html}");
        Contains("country", Resources.GetString(CatalogLocalizationKeyResolver.CountryName(country), culture)!, visibleHtml, html);
        Contains("category", Resources.GetString(CatalogLocalizationKeyResolver.CategoryName(category), culture)!, visibleHtml, html);
        Contains("country navigation", LocalizedUrl.ForCurrentCulture($"/countries/{country.Slug}", language), WebUtility.HtmlDecode(html), html);
        Contains("summary", translation.Summary, visibleHtml, html);
        Contains("content", translation.Content.Split('\n', StringSplitOptions.RemoveEmptyEntries)[0].Trim('#', ' '), visibleHtml, html);
        Contains("review label", Resources.GetString("Article.ReviewStatus", culture)!, visibleHtml, html);
        Contains("review status", Resources.GetString("Category.Status.Verified", culture)!, visibleHtml, html);
        Check("update date", translation.LastUpdatedAt.ToString("O"), html, () => AssertRenderedDate(html, translation.LastUpdatedAt, culture), html);
        Contains("table of contents", "class=\"article-toc\"", html, html);
        var sectionIds = Regex.Matches(html, "<h3\\b[^>]*id=\"(?<id>[^\"]+)\"")
            .Cast<Match>().Select(match => match.Groups["id"].Value).ToArray();
        Check("section ids", "at least one", string.Join(", ", sectionIds), () => Assert.NotEmpty(sectionIds), html);
        foreach (var id in sectionIds)
            Contains("section link", $"href=\"{expectedPath}#{id}\"", html, html);
        var ownedSources = catalog.OfficialSources.Where(source => source.InformationEntryId == entry.Id).ToArray();
        Check("owned sources", "at least one", ownedSources.Length.ToString(), () => Assert.NotEmpty(ownedSources), html);
        var inlineSourceIds = Regex.Matches(translation.Content + translation.Summary, @"\[\[source:([0-9a-fA-F-]{36})\|")
            .Cast<Match>().Select(match => Guid.Parse(match.Groups[1].Value)).ToArray();
        Check("inline source ids", "at least one", inlineSourceIds.Length.ToString(), () => Assert.NotEmpty(inlineSourceIds), html);
        var inlineCount = Regex.Matches(html, "class=\"article-source-link\"").Count;
        Check("inline source count", inlineSourceIds.Length.ToString(), inlineCount.ToString(), () => Assert.Equal(inlineSourceIds.Length, inlineCount), html);
        foreach (var sourceId in inlineSourceIds)
        {
            var matches = ownedSources.Where(item => item.Id == sourceId).ToArray();
            Check("inline source ownership", sourceId.ToString(), matches.Length.ToString(), () => Assert.Single(matches), html);
            if (matches.Length == 1)
                Contains("inline source URL", matches[0].Url.AbsoluteUri, WebUtility.HtmlDecode(html), html);
            else
                failures.Add($"[{language}] inline source URL: prerequisite source {sourceId} missing or ambiguous; actual unavailable; HTML: {html}");
        }
        foreach (var source in ownedSources)
        {
            Contains("official source URL", source.Url.AbsoluteUri, WebUtility.HtmlDecode(html), html);
            Check("source verification date", source.LastCheckedAt.ToString("O"), html, () => AssertRenderedDate(html, source.LastCheckedAt, culture), html);
        }
        var unownedSource = catalog.OfficialSources.FirstOrDefault(source => source.InformationEntryId != entry.Id);
        if (unownedSource is not null)
            Check("unowned source", "absent: " + unownedSource.Url.AbsoluteUri, WebUtility.HtmlDecode(html), () => Assert.DoesNotContain(unownedSource.Url.AbsoluteUri, WebUtility.HtmlDecode(html)), html);
        Contains("category navigation", LocalizedUrl.ForCurrentCulture($"/countries/{country.Slug}/categories/{category.Slug}", language), WebUtility.HtmlDecode(html), html);

        var expectedCanonical = $"https://example.test{expectedPath}";
        var summaryMatch = Regex.Match(html, "<p\\b(?=[^>]*\\bclass=\"[^\"]*\\bentry-summary\\b[^\"]*\")[^>]*>(?<summary>.*?)</p>", RegexOptions.Singleline);
        Check("summary element", "p with class entry-summary", summaryMatch.Value, () => Assert.True(summaryMatch.Success), html);
        var decodedHead = WebUtility.HtmlDecode(head);
        Contains("canonical", $"rel=\"canonical\" href=\"{expectedCanonical}\"", decodedHead, head);
        Check("canonical fragments", "no #", decodedHead, () => Assert.DoesNotContain("#", decodedHead), head);
        foreach (var alternateLanguage in new[] { "en", "uk", "ru" })
        {
            var alternatePath = ArticleUrlResolver.BuildPath(alternateLanguage, country.Slug, category.Slug, entry.Id);
            Contains("hreflang " + alternateLanguage, $"hreflang=\"{alternateLanguage}\" href=\"https://example.test{alternatePath}\"", decodedHead, head);
        }
        Contains("Open Graph title", $"property=\"og:title\" content=\"{translation.Title}\"", decodedHead, head);
        if (summaryMatch.Success)
        {
            var expectedDescription = WebUtility.HtmlDecode(Regex.Replace(summaryMatch.Groups["summary"].Value, "<[^>]+>", string.Empty)).Trim();
            Contains("description", $"name=\"description\" content=\"{expectedDescription}\"", decodedHead, head);
            Contains("Open Graph description", $"property=\"og:description\" content=\"{expectedDescription}\"", decodedHead, head);
        }
        else
            failures.Add($"[{language}] description and Open Graph description: prerequisite summary missing; expected summary text; actual unavailable; HTML: {html}");
        Contains("Open Graph URL", $"property=\"og:url\" content=\"{expectedCanonical}\"", decodedHead, head);
        Assert.True(failures.Count == 0, string.Join("\n\n", failures));
    }

    [Fact]
    public async Task CategoryPageKeepsLegacyAnchorAndLinksArticleTitleToStandaloneUrl()
    {
        var catalog = await Catalog.Value;
        var entry = FindPublishedArticleWithSources(catalog);
        var country = catalog.Countries.Single(item => item.Id == entry.CountryId);
        var category = catalog.Categories.Single(item => item.Id == entry.CategoryId);
        var path = $"/en/countries/{country.Slug}/categories/{category.Slug}";
        var (html, _, notFound) = await RenderAsync(catalog, "en", path,
            new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = country.Slug,
                [nameof(Category.CategorySlug)] = category.Slug
            });

        Assert.False(notFound);
        Assert.Contains($"id=\"entry-{entry.Id:N}\"", html);
        Assert.Contains($"href=\"{ArticleUrlResolver.BuildPath("en", country.Slug, category.Slug, entry.Id)}\"", html);
        Assert.Contains($"href=\"{path}#article-section-{entry.Id:N}-", html);
    }

    [Fact]
    public async Task LanguageSwitchKeepsArticleIdentityAndSectionFragment()
    {
        var catalog = await Catalog.Value;
        var entry = FindPublishedArticleWithSources(catalog);
        var country = catalog.Countries.Single(item => item.Id == entry.CountryId);
        var category = catalog.Categories.Single(item => item.Id == entry.CategoryId);
        var sourcePath = ArticleUrlResolver.BuildPath("en", country.Slug, category.Slug, entry.Id) + "#section-intro";

        foreach (var language in new[] { "en", "uk", "ru" })
        {
            Assert.Equal(
                ArticleUrlResolver.BuildPath(language, country.Slug, category.Slug, entry.Id) + "#section-intro",
                LocalizedUrl.ForLanguageSwitch(new Uri($"https://example.test{sourcePath}"), language));
        }
    }

    [Fact]
    public async Task UnknownMismatchedAndUnavailableArticlesAreNotFoundWithoutSeoMetadata()
    {
        var catalog = await Catalog.Value;
        var entry = FindPublishedArticleWithSources(catalog);
        var country = catalog.Countries.Single(item => item.Id == entry.CountryId);
        var category = catalog.Categories.Single(item => item.Id == entry.CategoryId);
        var invalidId = Guid.NewGuid();
        var invalid = await RenderAsync(catalog, "en",
            ArticleUrlResolver.BuildPath("en", country.Slug, category.Slug, invalidId),
            new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = country.Slug,
                [nameof(Category.CategorySlug)] = category.Slug,
                [nameof(Category.ArticleKey)] = invalidId.ToString("D")
            });
        Assert.True(invalid.NotFound);
        Assert.DoesNotContain("rel=\"canonical\"", invalid.Head);
        Assert.DoesNotContain("property=\"og:title\"", invalid.Head);

        var otherCountry = catalog.Countries.First(item => item.Id != country.Id);
        var mismatch = await RenderAsync(catalog, "en",
            ArticleUrlResolver.BuildPath("en", otherCountry.Slug, category.Slug, entry.Id),
            new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = otherCountry.Slug,
                [nameof(Category.CategorySlug)] = category.Slug,
                [nameof(Category.ArticleKey)] = entry.Id.ToString("D")
            });
        Assert.True(mismatch.NotFound);

        var withoutRussian = CloneWithoutTranslation(catalog, entry.Id, "ru");
        var unavailable = await RenderAsync(withoutRussian, "ru",
            ArticleUrlResolver.BuildPath("ru", country.Slug, category.Slug, entry.Id),
            new Dictionary<string, object?>
            {
                [nameof(Category.CountrySlug)] = country.Slug,
                [nameof(Category.CategorySlug)] = category.Slug,
                [nameof(Category.ArticleKey)] = entry.Id.ToString("D")
            });
        Assert.True(unavailable.NotFound);
        Assert.DoesNotContain("rel=\"canonical\"", unavailable.Head);
    }

    private static InformationEntry FindPublishedArticleWithSources(CatalogData catalog) =>
        catalog.InformationEntries.First(entry =>
            catalog.InformationEntryTranslations.Count(translation => translation.InformationEntryId == entry.Id &&
                translation.VerificationStatus == VerificationStatus.Verified && new[] { "en", "uk", "ru" }.Contains(translation.LanguageCode)) == 3 &&
            catalog.InformationEntryTranslations.Single(translation => translation.InformationEntryId == entry.Id && translation.LanguageCode == "en").Content
                .Split('\n').Count(line => line.StartsWith("## ", StringComparison.Ordinal)) >= 3 &&
            Regex.IsMatch(catalog.InformationEntryTranslations.Single(translation => translation.InformationEntryId == entry.Id && translation.LanguageCode == "en").Content,
                @"\[\[source:[0-9a-fA-F-]{36}\|") &&
            catalog.OfficialSources.Any(source => source.InformationEntryId == entry.Id));

    private static void AssertRenderedDate(string html, DateTimeOffset expected, CultureInfo culture)
    {
        var matches = Regex.Matches(html,
                "<time\\b[^>]*datetime=\"(?<datetime>[^\"]+)\"[^>]*>(?<visible>.*?)</time>",
                RegexOptions.Singleline)
            .Cast<Match>()
            .Where(match => DateTimeOffset.TryParse(
                WebUtility.HtmlDecode(match.Groups["datetime"].Value),
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var rendered) && rendered.EqualsExact(expected))
            .ToArray();

        Assert.NotEmpty(matches);
        Assert.Contains(matches, match =>
            WebUtility.HtmlDecode(Regex.Replace(match.Groups["visible"].Value, "<[^>]*>", string.Empty)) ==
            expected.ToString("d MMM yyyy", culture));
    }

    private static CatalogData CloneWithoutTranslation(CatalogData catalog, Guid entryId, string language) =>
        (CatalogData)Activator.CreateInstance(typeof(CatalogData), BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            args: [catalog.Countries, catalog.Categories, catalog.InformationEntries,
                catalog.InformationEntryTranslations.Where(item => item.InformationEntryId != entryId || item.LanguageCode != language),
                catalog.OfficialSources],
            culture: CultureInfo.InvariantCulture)!;

    private static async Task<(string Html, string Head, bool NotFound)> RenderAsync(
        CatalogData catalog, string language, string currentPath, IDictionary<string, object?> parameters)
    {
        var previousCulture = CultureInfo.CurrentCulture;
        var previousUiCulture = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(language);
        try
        {
            var guides = new StepByStepGuideCatalog([]);
            var availability = new SeoTranslationAvailability(catalog, guides);
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton(catalog);
            services.AddSingleton(guides);
            services.AddSingleton<IWebHostEnvironment>(new TestEnvironment());
            services.AddSingleton<ArticleUrlResolver>();
            services.AddSingleton(availability);
            services.AddSingleton(new SeoMetadataGenerator("https://example.test", availability));
            var navigation = new TestNavigationManager(currentPath);
            var notFound = false;
            navigation.OnNotFound += (_, _) => notFound = true;
            services.AddSingleton<NavigationManager>(navigation);
            services.AddSingleton<IJSRuntime, TestJsRuntime>();

            using var provider = services.BuildServiceProvider();
            await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());
            return await renderer.Dispatcher.InvokeAsync(async () =>
            {
                var body = await renderer.RenderComponentAsync<Category>(ParameterView.FromDictionary(parameters));
                var head = await renderer.RenderComponentAsync<HeadOutlet>(ParameterView.Empty);
                return (body.ToHtmlString(), head.ToHtmlString(), notFound);
            });
        }
        finally
        {
            CultureInfo.CurrentCulture = previousCulture;
            CultureInfo.CurrentUICulture = previousUiCulture;
        }
    }

    private sealed class TestNavigationManager : NavigationManager
    {
        public TestNavigationManager(string path) => Initialize("https://example.test/", $"https://example.test{path}");
        protected override void NavigateToCore(string uri, bool forceLoad) { }
    }

    private sealed class TestJsRuntime : IJSRuntime
    {
        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args) =>
            ValueTask.FromResult(default(TValue)!);

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) =>
            ValueTask.FromResult(default(TValue)!);
    }

    private sealed class TestEnvironment : IWebHostEnvironment
    {
        public string ApplicationName { get; set; } = "RefugeeNavigator.Tests";
        public string EnvironmentName { get; set; } = "Production";
        public string WebRootPath { get; set; } = AppContext.BaseDirectory;
        public string ContentRootPath { get; set; } = AppContext.BaseDirectory;
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }
}
