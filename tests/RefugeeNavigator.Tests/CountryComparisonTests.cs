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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Comparison;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Tests;

public sealed class CountryComparisonTests
{
    private static readonly ResourceManager Resources = new(
        "RefugeeNavigator.Web.Resources.Localization.SharedResource",
        typeof(SharedResource).Assembly);

    private static readonly Lazy<Task<CatalogData>> ProductionCatalog = new(
        () => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());

    private static readonly Lazy<Task<ComparisonFactCatalog>> FactCatalog = new(async () =>
    {
        var catalog = await LoadCatalogAsync();
        return await new JsonComparisonFactProvider(Path.Combine(AppContext.BaseDirectory, "Data", "comparison-facts.json")).LoadAsync(catalog);
    });

    [Fact]
    public async Task StructuredCatalogHasEightTopicsThreeOrderedAspectsAndLocalizedDraftFactsForEveryCountry()
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        JsonComparisonFactProvider.Validate(facts, catalog);

        Assert.Equal(8, facts.Topics.Count);
        Assert.Equal(24, facts.Topics.Sum(topic => topic.Aspects.Count));
        Assert.Equal(24, facts.Topics.SelectMany(topic => topic.Aspects).Select(aspect => aspect.QuestionResourceKey).Distinct(StringComparer.Ordinal).Count());
        Assert.Equal(72, facts.Topics.SelectMany(topic => topic.Aspects).Sum(aspect => aspect.Facts.Count));
        Assert.Equal(70, facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).Count(fact => fact.IsAvailable));
        Assert.Equal(2, facts.Topics.SelectMany(topic => topic.Aspects).SelectMany(aspect => aspect.Facts).Count(fact => !fact.IsAvailable));
        Assert.Equal(24, catalog.InformationEntries.Count);
        Assert.Equal(72, catalog.InformationEntryTranslations.Count);
        Assert.All(catalog.InformationEntryTranslations, translation => Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus));
        var unavailableFacts = facts.Topics.SelectMany(topic => topic.Aspects)
            .SelectMany(aspect => aspect.Facts.Where(fact => !fact.IsAvailable)
                .Select(fact => (aspect.Id, fact.CountrySlug, fact))).ToArray();
        Assert.Equal(new[]
        {
            ("education-pupil-language", "germany"),
            ("employment-assistance-conditions", "denmark")
        }, unavailableFacts.Select(item => (item.Id, item.CountrySlug)).OrderBy(item => item.Id).ToArray());
        foreach (var (_, _, fact) in unavailableFacts)
        foreach (var language in new[] { "uk", "en", "ru" })
        {
            Assert.Equal(GetResource("Compare.NotEstablishedInReviewedSources", language), fact.Values[language]);
        }

        var employerDutiesAspect = Assert.Single(facts.Topics.SelectMany(topic => topic.Aspects), aspect => aspect.Id == "work-employer-duties");
        var germanEmployerDuties = Assert.Single(employerDutiesAspect.Facts, fact => fact.CountrySlug == "germany");
        Assert.True(germanEmployerDuties.IsAvailable);
        Assert.Equal(ComparisonFactStatus.Draft, germanEmployerDuties.Status);
        Assert.Equal(new Guid("cac1773b-211a-5b77-9f96-8056401b5902"), Assert.Single(germanEmployerDuties.OfficialSourceIds));
        Assert.Contains("§4a(4)", germanEmployerDuties.Values["uk"]);
        Assert.Contains("§4a(4)", germanEmployerDuties.Values["en"]);
        Assert.Contains("§4a(4)", germanEmployerDuties.Values["ru"]);
        Assert.Contains("тривалих платних послуг", germanEmployerDuties.Values["uk"]);
        Assert.Contains("sustained paid services", germanEmployerDuties.Values["en"]);
        Assert.Contains("длительных платных услуг", germanEmployerDuties.Values["ru"]);
        Assert.Contains("Протягом зайнятості роботодавець зберігає копію", germanEmployerDuties.Values["uk"]);
        Assert.Contains("For the duration of employment", germanEmployerDuties.Values["en"]);
        Assert.Contains("В течение всего периода занятости", germanEmployerDuties.Values["ru"]);
        Assert.Contains("главою 2, розділом 4", germanEmployerDuties.Values["uk"]);
        Assert.Contains("Chapter 2, Section 4", germanEmployerDuties.Values["en"]);
        Assert.Contains("главы 2, раздела 4", germanEmployerDuties.Values["ru"]);

        foreach (var topic in facts.Topics)
        {
            Assert.Equal(3, topic.Aspects.SelectMany(aspect => aspect.Facts).Select(fact => fact.CountrySlug).Distinct().Count());
            Assert.Equal(topic.CategorySlug, Assert.Single(CountryComparisonTopicMap.All, mapped => mapped.ResourceKey == topic.TopicResourceKey).CategorySlug);
            foreach (var aspect in topic.Aspects)
            foreach (var fact in aspect.Facts)
            {
                Assert.Equal(ComparisonFactStatus.Draft, fact.Status);
                Assert.All(new[] { "uk", "en", "ru" }, language => Assert.False(string.IsNullOrWhiteSpace(fact.Values.GetValueOrDefault(language))));
                if (fact.IsAvailable)
                {
                    Assert.NotEmpty(fact.OfficialSourceIds);
                    Assert.All(fact.OfficialSourceIds, sourceId => Assert.Contains(catalog.OfficialSources, source => source.Id == sourceId && source.InformationEntryId == fact.InformationEntryId));
                }
                else
                {
                    Assert.Empty(fact.OfficialSourceIds);
                    Assert.Equal(GetResource("Compare.NotEstablishedInReviewedSources", "en"), fact.Values["en"]);
                }
            }
        }
    }

    [Fact]
    public async Task IncorrectArticleAndSourceRelationshipsFailStructuredCatalogValidation()
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var firstTopic = facts.Topics[0];
        var firstAspect = firstTopic.Aspects[0];
        var firstFact = firstAspect.Facts[0];

        var wrongArticle = ReplaceFact(facts, firstTopic, firstAspect, firstFact with { InformationEntryId = Guid.NewGuid() });
        Assert.Throws<InvalidDataException>(() => JsonComparisonFactProvider.Validate(wrongArticle, catalog));

        var wrongSource = ReplaceFact(facts, firstTopic, firstAspect, firstFact with { OfficialSourceIds = [Guid.NewGuid()] });
        Assert.Throws<InvalidDataException>(() => JsonComparisonFactProvider.Validate(wrongSource, catalog));
    }

    [Fact]
    public async Task DraftFactsArePreviewableLocallyButHiddenInProductionWhileV1RemainsAvailable()
    {
        var catalog = await LoadCatalogAsync();
        const string query = "?country1=germany&country2=poland&category=employment";
        var localHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, "en", query));
        var productionHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, "en", query, Environments.Production));

        Assert.Contains("structured-comparison", localHtml);
        Assert.Contains(GetResource("Compare.DraftPreviewNotice", "en"), localHtml);
        Assert.Contains(GetResource("Compare.DraftStatus", "en"), localHtml);
        Assert.DoesNotContain("structured-comparison", productionHtml);
        Assert.DoesNotContain(GetResource("Compare.DraftPreviewNotice", "en"), productionHtml);
        Assert.Contains("class=\"comparison-topic\"", productionHtml);
        Assert.Contains(GetResource("Compare.ReadDetails", "en"), productionHtml);
        Assert.Contains("class=\"source-details\"", productionHtml);
        Assert.Contains("href=\"/countries/germany/categories/employment#entry-", productionHtml);
        Assert.DoesNotContain("class=\"full-article-comparison\"", productionHtml);
        Assert.Matches("<details\\b[^>]*class=\"full-article-comparison\"[^>]*>", localHtml);
        Assert.DoesNotMatch("<details\\b[^>]*class=\"full-article-comparison\"[^>]*\\bopen(?:\\s|>)", localHtml);
        Assert.Contains("class=\"comparison-topic\"", localHtml);
        Assert.Contains(GetResource("Compare.ReadFullArticles", "en"), localHtml);
    }

    [Fact]
    public async Task StructuredFactAndMissingInformationFallbackAreHtmlEncodedAndLocalized()
    {
        var catalog = await LoadCatalogAsync();
        var facts = await FactCatalog.Value;
        var topic = facts.Topics.Single(item => item.CategorySlug == "healthcare");
        var aspect = topic.Aspects[0];
        var denmarkFact = aspect.Facts.Single(fact => fact.CountrySlug == "denmark");
        var injected = WithFact(facts, topic, aspect, denmarkFact with
        {
            Values = new Dictionary<string, string>(denmarkFact.Values) { ["en"] = "<script>alert(1)</script>" }
        });
        var html = await RenderAsync(catalog, "en", "?country1=denmark&country2=germany&category=healthcare", comparisonFacts: injected);
        Assert.Contains("&lt;script&gt;alert(1)&lt;/script&gt;", html);
        Assert.DoesNotContain("<script>alert(1)</script>", html);

        foreach (var (language, fallback) in new[]
        {
            ("uk", "Не встановлено за перевіреними джерелами."),
            ("en", "Not established in the reviewed sources."),
            ("ru", "Не установлено по проверенным источникам.")
        })
        {
            var unavailable = WithFact(facts, topic, aspect, denmarkFact with
            {
                IsAvailable = false,
                OfficialSourceIds = [],
                Values = new Dictionary<string, string>
                {
                    ["uk"] = "Не встановлено за перевіреними джерелами.",
                    ["en"] = "Not established in the reviewed sources.",
                    ["ru"] = "Не установлено по проверенным источникам."
                }
            });
            var unavailableHtml = WebUtility.HtmlDecode(await RenderAsync(catalog, language,
                "?country1=denmark&country2=germany&category=healthcare", comparisonFacts: unavailable));
            Assert.Contains(fallback, unavailableHtml);
            JsonComparisonFactProvider.Validate(unavailable, catalog);
        }
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task EveryStructuredAspectAndPreviewLabelHasCompleteLocalization(string language)
    {
        var facts = await FactCatalog.Value;
        var aspectKeys = facts.Topics.SelectMany(topic => topic.Aspects).Select(aspect => aspect.QuestionResourceKey)
            .Concat(new[]
            {
                "Compare.StructuredHeading", "Compare.DraftPreviewHeading", "Compare.DraftPreviewNotice", "Compare.DraftStatus", "Compare.FactSources",
                "Compare.ApprovedEditorialStatus", "Compare.NotEstablishedInReviewedSources", "Compare.ReadFullArticles"
            }).Distinct(StringComparer.Ordinal);
        foreach (var aspectKey in aspectKeys)
        {
            var value = GetResource(aspectKey, language);
            Assert.False(string.IsNullOrWhiteSpace(value), $"Missing {language} localization for {aspectKey}.");
            Assert.NotEqual(aspectKey, value);
        }
    }

    [Fact]
    public async Task MissingComparisonLocalizationFailsCoverageValidation()
    {
        var facts = await FactCatalog.Value;
        Assert.Throws<InvalidDataException>(() => ComparisonFactLocalizationValidator.Validate(
            facts,
            (key, culture) => key == "Compare.DraftPreviewNotice" && culture.Name == "ru"
                ? null
                : Resources.GetString(key, culture)));
    }

    [Fact]
    public async Task ComparisonMapContainsEightOrderedTopicsAndMapsRealArticlesToExpectedRelationships()
    {
        var catalog = await LoadCatalogAsync();
        CountryComparisonTopicMap.Validate(catalog);

        Assert.Equal(8, CountryComparisonTopicMap.All.Count);
        Assert.Equal(
            ["temporary-protection", "temporary-protection", "employment", "employment", "healthcare", "housing", "education", "social-assistance"],
            CountryComparisonTopicMap.All.Select(topic => topic.CategorySlug));
        Assert.Equal(24, CountryComparisonTopicMap.All.SelectMany(topic => topic.EntryIds.Values).Distinct().Count());

        foreach (var topic in CountryComparisonTopicMap.All)
        {
            Assert.Equal(3, topic.EntryIds.Count);
            var category = Assert.Single(catalog.Categories, item => item.Slug == topic.CategorySlug);
            foreach (var (countrySlug, entryId) in topic.EntryIds)
            {
                var country = Assert.Single(catalog.Countries, item => item.Slug == countrySlug);
                var entry = Assert.Single(catalog.InformationEntries, item => item.Id == entryId);
                Assert.Equal(country.Id, entry.CountryId);
                Assert.Equal(category.Id, entry.CategoryId);
            }
        }
    }

    [Fact]
    public async Task AllCountryPairsCategoriesAndLanguagesRenderAlignedReviewedCatalogContent()
    {
        var catalog = await LoadCatalogAsync();
        var factCatalog = await FactCatalog.Value;
        var languages = new[] { "uk", "en", "ru" };
        var categories = new[]
        {
            "temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance"
        };
        var countryPairs = new[]
        {
            ("denmark", "germany"), ("germany", "poland"), ("poland", "denmark")
        };

        foreach (var language in languages)
        foreach (var categorySlug in categories)
        foreach (var (countryOneSlug, countryTwoSlug) in countryPairs)
        {
            var html = await RenderAsync(catalog, language,
                $"?country1={countryOneSlug}&country2={countryTwoSlug}&category={categorySlug}");
            var visibleHtml = WebUtility.HtmlDecode(html);
            var topics = CountryComparisonTopicMap.ForCategory(categorySlug);

            Assert.Equal(topics.Count, Regex.Matches(html, "class=\"comparison-topic\"").Count);
            Assert.Equal(topics.Count * 2, Regex.Matches(html, "class=\"topic-country\"").Count);
            Assert.Contains("name=\"country1\"", html);
            Assert.Contains("name=\"country2\"", html);
            Assert.Contains("name=\"category\"", html);
            Assert.Contains("selected", html);
            Assert.Contains(GetResource("Compare.ScopeNotice", language), visibleHtml);
            Assert.Contains(GetResource("Common.Disclaimer", language), visibleHtml);
            Assert.Contains(GetResource("Compare.ReadFullArticles", language), visibleHtml);
            Assert.Matches("<details\\b[^>]*class=\"full-article-comparison\"[^>]*>", html);
            Assert.DoesNotMatch("<details\\b[^>]*class=\"full-article-comparison\"[^>]*\\bopen(?:\\s|>)", html);
            Assert.DoesNotContain("Compare.", visibleHtml);
            Assert.DoesNotContain("preview layout", visibleHtml, StringComparison.OrdinalIgnoreCase);

            foreach (var countrySlug in new[] { countryOneSlug, countryTwoSlug })
            foreach (var topic in topics)
            {
                var entryId = CountryComparisonTopicMap.EntryIdFor(topic, countrySlug);
                var entry = Assert.Single(catalog.InformationEntries, item => item.Id == entryId);
                var translation = Assert.Single(catalog.InformationEntryTranslations, item =>
                    item.InformationEntryId == entryId && item.LanguageCode == language);
                Assert.Equal(VerificationStatus.Verified, translation.VerificationStatus);
                Assert.Contains(translation.Title, visibleHtml);
                Assert.Contains(translation.Summary, visibleHtml);
                AssertFullContentIsRendered(translation.Content, visibleHtml);
                Assert.Contains(GetResource("Category.Status.Verified", language), visibleHtml);
                Assert.Contains($"href=\"/countries/{countrySlug}/categories/{categorySlug}#entry-{entryId:N}\"", html);

                var sources = catalog.OfficialSources.Where(source => source.InformationEntryId == entryId).ToArray();
                Assert.NotEmpty(sources);
                foreach (var source in sources)
                {
                    Assert.Contains(source.Title, visibleHtml);
                    Assert.Contains($"href=\"{WebUtility.HtmlEncode(source.Url.AbsoluteUri)}\" target=\"_blank\" rel=\"noopener noreferrer\"", html);
                    Assert.Contains(source.LastCheckedAt.ToString("d MMM yyyy", CultureInfo.GetCultureInfo(language)), visibleHtml);
                }
            }

            foreach (var topic in topics)
            {
                var structuredTopic = Assert.Single(factCatalog.Topics, item => item.TopicResourceKey == topic.ResourceKey);
                foreach (var aspect in structuredTopic.Aspects)
                foreach (var countrySlug in new[] { countryOneSlug, countryTwoSlug })
                {
                    var fact = Assert.Single(aspect.Facts, item => item.CountrySlug == countrySlug);
                    Assert.Equal(ComparisonFactStatus.Draft, fact.Status);
                    if (fact.IsAvailable)
                    {
                        Assert.Contains(fact.Values[language], visibleHtml);
                        foreach (var sourceId in fact.OfficialSourceIds)
                        {
                            var source = Assert.Single(catalog.OfficialSources, item => item.Id == sourceId);
                            Assert.Contains(source.Title, visibleHtml);
                            Assert.Contains($"href=\"{WebUtility.HtmlEncode(source.Url.AbsoluteUri)}\" target=\"_blank\" rel=\"noopener noreferrer\"", html);
                        }
                    }
                    else
                    {
                        Assert.Contains(GetResource("Compare.NotEstablishedInReviewedSources", language), visibleHtml);
                    }
                    Assert.Contains(GetResource(aspect.QuestionResourceKey, language), visibleHtml);
                }
            }

            Assert.Contains("<details class=\"article-details\"", html);
            Assert.DoesNotContain("article-details\" open", html);
            Assert.Contains("<details class=\"source-details\"", html);
            Assert.Contains(GetResource("Compare.ReadDetails", language), visibleHtml);
            Assert.Contains(GetResource("Compare.OfficialSources", language), visibleHtml);
            Assert.Matches("<h5\\b", html);
            Assert.Matches("<ul\\b", html);
            Assert.DoesNotContain("<p>## ", html);
            Assert.DoesNotContain("<p>- ", html);
        }
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public Task EveryComparisonTopicHasLocalizedHeading(string language)
    {
        foreach (var topic in CountryComparisonTopicMap.All)
        {
            var value = GetResource(topic.ResourceKey, language);
            Assert.False(string.IsNullOrWhiteSpace(value), $"Missing {language} translation for {topic.ResourceKey}.");
            Assert.DoesNotContain(topic.ResourceKey, value);
        }

        return Task.CompletedTask;
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task NeedsReviewAndOutdatedTranslationsAreNotRenderedAsConfirmedContent(string language)
    {
        var catalog = await LoadCatalogAsync();
        var topic = CountryComparisonTopicMap.ForCategory("employment")[0];
        var entryId = CountryComparisonTopicMap.EntryIdFor(topic, "denmark");
        var original = Assert.Single(catalog.InformationEntryTranslations, item =>
            item.InformationEntryId == entryId && item.LanguageCode == language);

        foreach (var status in new[] { VerificationStatus.NeedsReview, VerificationStatus.Outdated })
        {
            var changedCatalog = WithTranslations(catalog, entryId, language, status);
            var html = await RenderAsync(changedCatalog, language,
                "?country1=denmark&country2=germany&category=employment");

            Assert.Contains(GetResource("Compare.InformationUnavailable", language), WebUtility.HtmlDecode(html));
            Assert.Contains(GetResource(StatusResourceKey(status), language), WebUtility.HtmlDecode(html));
            Assert.DoesNotContain(WebUtility.HtmlEncode(original.Title), html);
            Assert.DoesNotContain(WebUtility.HtmlEncode(original.Summary), html);
            Assert.DoesNotContain(WebUtility.HtmlEncode(original.Content), html);
            Assert.Contains(catalog.OfficialSources.First(source => source.InformationEntryId == entryId).Title, WebUtility.HtmlDecode(html));
        }
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task MissingMappedArticleUsesUnavailableFallback(string language)
    {
        var catalog = await LoadCatalogAsync();
        var entryId = CountryComparisonTopicMap.EntryIdFor(CountryComparisonTopicMap.ForCategory("healthcare")[0], "denmark");
        var withoutEntry = CreateCatalog(
            catalog.Countries,
            catalog.Categories,
            catalog.InformationEntries.Where(entry => entry.Id != entryId),
            catalog.InformationEntryTranslations.Where(translation => translation.InformationEntryId != entryId),
            catalog.OfficialSources.Where(source => source.InformationEntryId != entryId));

        var html = await RenderAsync(withoutEntry, language, "?country1=denmark&country2=germany&category=healthcare");
        var visibleHtml = WebUtility.HtmlDecode(html);

        Assert.Contains(GetResource("Compare.InformationUnavailable", language), visibleHtml);
        Assert.Contains("class=\"topic-country\"", html);
        Assert.Contains("href=\"/countries/germany/categories/healthcare#entry-", html);
    }

    [Fact]
    public async Task ArticleDetailsHtmlEncodesUntrustedMarkupAndKeepsFormattingSafe()
    {
        var catalog = await LoadCatalogAsync();
        var entryId = CountryComparisonTopicMap.EntryIdFor(CountryComparisonTopicMap.ForCategory("healthcare")[0], "denmark");
        var injected = WithContent(catalog, entryId, "en", "<script>alert(1)</script>\n\n## Safe heading\n\n- Safe list item");
        var html = await RenderAsync(injected, "en", "?country1=denmark&country2=germany&category=healthcare");
        var visibleHtml = WebUtility.HtmlDecode(html);

        Assert.Contains("&lt;script&gt;alert(1)&lt;/script&gt;", html);
        Assert.DoesNotContain("<script>alert(1)</script>", html);
        Assert.Matches("<h5\\b[^>]*>Safe heading</h5>", html);
        Assert.Matches("<li\\b[^>]*>Safe list item</li>", html);
        Assert.DoesNotContain("## Safe heading", visibleHtml);
        Assert.DoesNotContain("- Safe list item", visibleHtml);
    }

    [Theory]
    [InlineData("uk")]
    [InlineData("en")]
    [InlineData("ru")]
    public async Task EmptyAndPartialStateShowsInstructionsAndReset(string language)
    {
        var initial = WebUtility.HtmlDecode(await RenderAsync(await LoadCatalogAsync(), language));
        Assert.Contains(GetResource("Compare.SelectionInstructions", language), initial);
        Assert.Contains("name=\"country1\" required", initial);
        Assert.Contains("name=\"country2\" required", initial);
        Assert.Contains("name=\"category\" required", initial);
        Assert.DoesNotContain("class=\"comparison-topic\"", initial);
        Assert.Contains("href=\"/compare\"", initial);

        var partial = await RenderAsync(await LoadCatalogAsync(), language, "?country1=denmark");
        Assert.Contains("selected", partial);
        Assert.DoesNotContain("class=\"comparison-topic\"", partial);
    }

    [Theory]
    [InlineData("?country1=denmark&country2=denmark&category=employment", "Compare.SameCountry")]
    [InlineData("?country1=unknown&country2=germany&category=employment", "Compare.InvalidCountry")]
    [InlineData("?country1=denmark&country2=germany&category=unknown", "Compare.InvalidCategory")]
    [InlineData("?country1=denmark&category=employment", "Compare.SelectionInstructions")]
    public async Task InvalidAndIncompleteQueryParametersRenderLocalizedMessages(string query, string resourceKey)
    {
        var html = WebUtility.HtmlDecode(await RenderAsync(await LoadCatalogAsync(), "en", query));

        Assert.Contains(GetResource(resourceKey, "en"), html);
        Assert.DoesNotContain("class=\"comparison-topic\"", html);
        Assert.DoesNotContain("HTTP 500", html);
    }

    private static string StatusResourceKey(VerificationStatus status) => status switch
    {
        VerificationStatus.NeedsReview => "Category.Status.NeedsReview",
        VerificationStatus.Outdated => "Category.Status.Outdated",
        _ => throw new ArgumentOutOfRangeException(nameof(status))
    };

    private static void AssertFullContentIsRendered(string content, string renderedHtml)
    {
        var blocks = Regex.Split(content, @"\r?\n\s*\r?\n+")
            .Select(block => block.Trim())
            .Where(block => block.Length > 0);

        foreach (var block in blocks)
        {
            if (block.StartsWith("## ", StringComparison.Ordinal))
            {
                Assert.Matches($"<h5\\b[^>]*>{Regex.Escape(block[3..].Trim())}</h5>", renderedHtml);
            }
            else if (block.Split('\n').All(line => line.TrimEnd('\r').StartsWith("- ", StringComparison.Ordinal)))
            {
                foreach (var item in block.Split('\n').Select(line => line.TrimEnd('\r')[2..].Trim()))
                {
                    Assert.Matches($"<li\\b[^>]*>{Regex.Escape(item)}</li>", renderedHtml);
                }
            }
            else
            {
                Assert.Contains(block, renderedHtml);
            }
        }
    }

    private static string GetResource(string key, string language) =>
        Resources.GetString(key, CultureInfo.GetCultureInfo(language)) ?? string.Empty;

    private static Task<CatalogData> LoadCatalogAsync() => ProductionCatalog.Value;

    private static CatalogData WithTranslations(CatalogData catalog, Guid entryId, string language, VerificationStatus status)
    {
        var translations = catalog.InformationEntryTranslations.Select(translation =>
            translation.InformationEntryId == entryId && translation.LanguageCode == language
                ? new InformationEntryTranslation(
                    translation.InformationEntryId,
                    translation.LanguageCode,
                    translation.Title,
                    translation.Summary,
                    translation.Content,
                    status,
                    translation.LastUpdatedAt)
                : translation).ToArray();
        return CreateCatalog(catalog.Countries, catalog.Categories, catalog.InformationEntries, translations, catalog.OfficialSources);
    }

    private static CatalogData WithContent(CatalogData catalog, Guid entryId, string language, string content)
    {
        var translations = catalog.InformationEntryTranslations.Select(translation =>
            translation.InformationEntryId == entryId && translation.LanguageCode == language
                ? new InformationEntryTranslation(
                    translation.InformationEntryId,
                    translation.LanguageCode,
                    translation.Title,
                    translation.Summary,
                    content,
                    translation.VerificationStatus,
                    translation.LastUpdatedAt)
                : translation).ToArray();
        return CreateCatalog(catalog.Countries, catalog.Categories, catalog.InformationEntries, translations, catalog.OfficialSources);
    }

    private static CatalogData CreateCatalog(
        IEnumerable<Country> countries,
        IEnumerable<InformationCategory> categories,
        IEnumerable<InformationEntry> entries,
        IEnumerable<InformationEntryTranslation> translations,
        IEnumerable<OfficialSource> sources) =>
        (CatalogData)Activator.CreateInstance(
            typeof(CatalogData),
            BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            args: [countries, categories, entries, translations, sources],
            culture: CultureInfo.InvariantCulture)!;

    private static async Task<string> RenderAsync(
        CatalogData catalog,
        string language,
        string query = "",
        string environmentName = "Development",
        ComparisonFactCatalog? comparisonFacts = null)
    {
        var originalCulture = CultureInfo.CurrentCulture;
        var originalUiCulture = CultureInfo.CurrentUICulture;
        var culture = CultureInfo.GetCultureInfo(language);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        try
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton(catalog);
            services.AddSingleton(comparisonFacts ?? await FactCatalog.Value);
            services.AddSingleton<IWebHostEnvironment>(new TestWebHostEnvironment(environmentName));
            services.AddSingleton<NavigationManager>(new TestNavigationManager($"http://localhost/compare{query}"));
            using var provider = services.BuildServiceProvider();
            await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());

            return await renderer.Dispatcher.InvokeAsync(async () =>
            {
                var component = await renderer.RenderComponentAsync<Compare>(ParameterView.Empty);
                return component.ToHtmlString();
            });
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
            CultureInfo.CurrentUICulture = originalUiCulture;
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
        }
    }

    private static ComparisonFactCatalog ReplaceFact(
        ComparisonFactCatalog catalog,
        ComparisonTopicFacts topic,
        ComparisonAspect aspect,
        CountryComparisonFact replacement) => WithFact(catalog, topic, aspect, replacement);

    private static ComparisonFactCatalog WithFact(
        ComparisonFactCatalog catalog,
        ComparisonTopicFacts targetTopic,
        ComparisonAspect targetAspect,
        CountryComparisonFact replacement) => catalog with
    {
        Topics = catalog.Topics.Select(topic => topic != targetTopic ? topic : topic with
        {
            Aspects = topic.Aspects.Select(aspect => aspect != targetAspect ? aspect : aspect with
            {
                Facts = aspect.Facts.Select(fact => fact.CountrySlug == replacement.CountrySlug ? replacement : fact).ToArray()
            }).ToArray()
        }).ToArray()
    };

    private sealed class TestWebHostEnvironment(string environmentName) : IWebHostEnvironment
    {
        public string ApplicationName { get; set; } = "RefugeeNavigator.Tests";
        public string EnvironmentName { get; set; } = environmentName;
        public string WebRootPath { get; set; } = AppContext.BaseDirectory;
        public string ContentRootPath { get; set; } = AppContext.BaseDirectory;
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }
}
