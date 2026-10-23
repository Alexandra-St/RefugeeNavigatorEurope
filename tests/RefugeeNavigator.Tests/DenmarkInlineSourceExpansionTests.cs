using System.Globalization;
using System.Net;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Components;
using RefugeeNavigator.Web.Components.Pages;

namespace RefugeeNavigator.Tests;

public sealed class DenmarkInlineSourceExpansionTests
{
    private static readonly Guid JobSearchArticleId = Guid.Parse("c2356631-3fa2-4553-8fda-50a4419262c0");
    private static readonly Guid HousingArticleId = Guid.Parse("19afdd19-e2cc-4da6-bde3-6dc3d64b7503");
    private static readonly Guid DenmarkId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly (Guid ArticleId, string Category)[] Articles =
    [
        (JobSearchArticleId, "employment"),
        (HousingArticleId, "housing")
    ];

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task InlineLinksRenderInCategoryAndCompareWithoutPublishingChangedTranslations(string language)
    {
        var catalog = await LoadProductionCatalogAsync();
        foreach (var translation in catalog.InformationEntryTranslations.Where(item =>
                     Articles.Any(article => article.ArticleId == item.InformationEntryId)))
        {
            Assert.Equal(VerificationStatus.NeedsReview, translation.VerificationStatus);
        }
        Assert.Equal(6, catalog.InformationEntryTranslations.Count(item =>
            Articles.Any(article => article.ArticleId == item.InformationEntryId) &&
            item.VerificationStatus == VerificationStatus.NeedsReview));

        var compareCatalog = await LoadComparisonFixtureWithApprovedTargetTranslationsAsync(language);
        foreach (var article in Articles)
        {
            var categoryHtml = await RenderAsync<Category>(catalog, language,
                $"/countries/denmark/categories/{article.Category}",
                new Dictionary<string, object?>
                {
                    [nameof(Category.CountrySlug)] = "denmark",
                    [nameof(Category.CategorySlug)] = article.Category
                });
            var localizedTitle = Assert.Single(catalog.InformationEntryTranslations,
                item => item.InformationEntryId == article.ArticleId && item.LanguageCode == language).Title;
            Assert.Contains(localizedTitle, WebUtility.HtmlDecode(categoryHtml));
            AssertInlineLinks(catalog, article.ArticleId, language, categoryHtml);

            var compareHtml = await RenderAsync<Compare>(compareCatalog, language,
                $"/compare?country1=denmark&country2=germany&category={article.Category}");
            Assert.Contains(localizedTitle, WebUtility.HtmlDecode(compareHtml));
            AssertInlineLinks(compareCatalog, article.ArticleId, language, compareHtml);
            Assert.Contains("<details class=\"source-details\"", compareHtml);
            Assert.DoesNotContain("<details class=\"source-details\" open", compareHtml);

            if (article.ArticleId == HousingArticleId)
            {
                Assert.Contains("<details class=\"article-toc\"", categoryHtml);
                Assert.Contains("<details class=\"article-toc\"", compareHtml);
                Assert.Contains("<details class=\"entry-sources\"", categoryHtml);
                Assert.DoesNotContain("<details class=\"entry-sources\" open", categoryHtml);
            }
        }
    }

    private static void AssertInlineLinks(CatalogData catalog, Guid articleId, string language, string html)
    {
        var expected = articleId == JobSearchArticleId
            ? language switch
            {
                "en" => new[]
                {
                    (Guid.Parse("fffe5fdb-0c81-47fc-9539-6bcc90949435"), "Workindenmark’s job portal"),
                    (Guid.Parse("b9577c82-6be2-4a73-a159-fcaa85968b8e"), "Jobnet")
                },
                "uk" => new[]
                {
                    (Guid.Parse("fffe5fdb-0c81-47fc-9539-6bcc90949435"), "порталі Workindenmark"),
                    (Guid.Parse("b9577c82-6be2-4a73-a159-fcaa85968b8e"), "Jobnet")
                },
                _ => new[]
                {
                    (Guid.Parse("fffe5fdb-0c81-47fc-9539-6bcc90949435"), "портале Workindenmark"),
                    (Guid.Parse("b9577c82-6be2-4a73-a159-fcaa85968b8e"), "Jobnet")
                }
            }
            : language switch
            {
                "en" => new[]
                {
                    (Guid.Parse("771e221f-dbe5-423b-995c-196ef247f361"), "official guidance"),
                    (Guid.Parse("ee40a28c-8be0-4471-8336-aab8ce453f0d"), "Udbetaling Danmark"),
                    (Guid.Parse("ce1d50d7-9177-4e11-ada4-7475053a7c2f"), "A deposit loan")
                },
                "uk" => new[]
                {
                    (Guid.Parse("771e221f-dbe5-423b-995c-196ef247f361"), "офіційних рекомендацій"),
                    (Guid.Parse("ee40a28c-8be0-4471-8336-aab8ce453f0d"), "Udbetaling Danmark"),
                    (Guid.Parse("ce1d50d7-9177-4e11-ada4-7475053a7c2f"), "Позика на заставу")
                },
                _ => new[]
                {
                    (Guid.Parse("771e221f-dbe5-423b-995c-196ef247f361"), "официальные рекомендации"),
                    (Guid.Parse("ee40a28c-8be0-4471-8336-aab8ce453f0d"), "Udbetaling Danmark"),
                    (Guid.Parse("ce1d50d7-9177-4e11-ada4-7475053a7c2f"), "Заём на залог")
                }
            };

        var visibleHtml = WebUtility.HtmlDecode(html);
        foreach (var (sourceId, label) in expected)
        {
            var source = Assert.Single(catalog.OfficialSources,
                item => item.Id == sourceId && item.InformationEntryId == articleId);
            Assert.Equal(Uri.UriSchemeHttps, source.Url.Scheme);
            var linkPattern = $"<a\\b(?=[^>]*class=\"article-source-link\")(?=[^>]*href=\"{Regex.Escape(source.Url.AbsoluteUri)}\")[^>]*>{Regex.Escape(label)}(?:<span\\b|</a>)";
            Assert.Matches(linkPattern, visibleHtml);
        }
    }

    private static Task<CatalogData> LoadProductionCatalogAsync() =>
        new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync();

    private static async Task<CatalogData> LoadComparisonFixtureWithApprovedTargetTranslationsAsync(string language)
    {
        var temporaryRoot = Path.Combine(Path.GetTempPath(), "RefugeeNavigatorDenmarkInlineExpansion", Guid.NewGuid().ToString("N"));
        var dataDirectory = Path.Combine(temporaryRoot, "Data");
        Directory.CreateDirectory(dataDirectory);
        try
        {
            foreach (var fileName in new[] { "countries.json", "categories.json", "information-entries.json", "official-sources.json" })
            {
                File.Copy(Path.Combine(AppContext.BaseDirectory, "Data", fileName), Path.Combine(dataDirectory, fileName));
            }

            var entriesPath = Path.Combine(dataDirectory, "information-entries.json");
            var entries = JsonNode.Parse(await File.ReadAllTextAsync(entriesPath))!.AsArray();
            foreach (var article in Articles)
            {
                var entry = entries.Single(item => item!["Id"]!.GetValue<string>() == article.ArticleId.ToString());
                var translation = entry!["Translations"]!.AsArray()
                    .Single(item => item!["LanguageCode"]!.GetValue<string>() == language);
                translation!["VerificationStatus"] = (int)VerificationStatus.Verified;
            }

            await File.WriteAllTextAsync(entriesPath, entries.ToJsonString());
            return await new JsonCatalogProvider(temporaryRoot).LoadAsync();
        }
        finally
        {
            Directory.Delete(temporaryRoot, recursive: true);
        }
    }

    private static async Task<string> RenderAsync<TComponent>(CatalogData catalog, string language, string currentPath,
        IDictionary<string, object?>? parameters = null) where TComponent : IComponent
    {
        var previousCulture = CultureInfo.CurrentCulture;
        var previousUiCulture = CultureInfo.CurrentUICulture;
        var culture = CultureInfo.GetCultureInfo(language);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        try
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton(catalog);
            await ComparisonTestServices.RegisterAsync(services);
            services.AddSingleton<NavigationManager>(new TestNavigationManager(currentPath));
            using var provider = services.BuildServiceProvider();
            await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());

            return await renderer.Dispatcher.InvokeAsync(async () =>
            {
                var parameterView = parameters is null ? ParameterView.Empty : ParameterView.FromDictionary(parameters);
                var component = await renderer.RenderComponentAsync<TComponent>(parameterView);
                return component.ToHtmlString();
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
        public TestNavigationManager(string currentPath) => Initialize("http://localhost/", $"http://localhost{currentPath}");
        protected override void NavigateToCore(string uri, bool forceLoad) { }
    }
}
