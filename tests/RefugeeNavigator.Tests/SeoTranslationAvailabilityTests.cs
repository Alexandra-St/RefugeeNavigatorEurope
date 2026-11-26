using System.Globalization;
using System.Reflection;
using System.Xml.Linq;
using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Guides;
using RefugeeNavigator.Web.Seo;

namespace RefugeeNavigator.Tests;

public sealed class SeoTranslationAvailabilityTests
{
    private static readonly Guid PublicCountryId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
    private static readonly Guid HiddenCountryId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
    private static readonly Guid AllLanguagesCategoryId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
    private static readonly Guid PartialCategoryId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
    private static readonly Guid UnavailableCategoryId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");

    [Fact]
    public void Metadata_is_absent_for_missing_current_language_and_hidden_destinations()
    {
        var generator = new SeoMetadataGenerator("https://example.test",
            new SeoTranslationAvailability(CreateCatalog(), CreateGuides()));
        var previous = CultureInfo.CurrentUICulture;
        try
        {
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("ru");
            Assert.Null(generator.CreateCategory("Title", "Description", "alpha", "partial"));
            Assert.Null(generator.CreateGuide("Title", "Description", "alpha", "partial-guide"));
            Assert.Null(generator.CreateGuide("Title", "Description", "alpha", "draft"));
            Assert.Null(generator.CreateCountry("Title", "Description", "hidden"));
            Assert.Null(generator.CreateCategory("Title", "Description", "missing", "partial"));
        }
        finally
        {
            CultureInfo.CurrentUICulture = previous;
        }
    }

    [Fact]
    public void Country_availability_contains_only_existing_public_translations()
    {
        var catalog = CreateCatalog();
        var availability = new SeoTranslationAvailability(catalog, CreateGuides());

        var alternates = availability.Country("alpha");

        Assert.Equal(new[] { "en", "ru", "uk" }, alternates.Select(item => item.Language).OrderBy(x => x));
        Assert.Equal(new[] { "/en/countries/alpha", "/ru/countries/alpha", "/uk/countries/alpha" },
            alternates.Select(item => item.Path).OrderBy(x => x));
        Assert.Empty(availability.Country("hidden"));
    }

    [Fact]
    public void Category_availability_omits_a_language_without_a_public_translation()
    {
        var availability = new SeoTranslationAvailability(CreateCatalog(), CreateGuides());

        var alternates = availability.Category("alpha", "partial");

        Assert.Equal(new[] { "en", "uk" }, alternates.Select(item => item.Language).OrderBy(x => x));
        Assert.DoesNotContain(alternates, item => item.Language == "ru");

        var generator = new SeoMetadataGenerator("https://example.test", availability);
        var originalCulture = CultureInfo.CurrentUICulture;
        try
        {
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
            var metadata = generator.CreateCategory("Partial", "Description", "alpha", "partial");
            Assert.NotNull(metadata);
            Assert.Equal(new[] { "en", "uk" }, metadata.Alternates.Select(item => item.Language).OrderBy(x => x));
            Assert.Equal(new[] { "https://example.test/en/countries/alpha/categories/partial", "https://example.test/uk/countries/alpha/categories/partial" },
                metadata.Alternates.Select(item => item.Url).OrderBy(x => x));
        }
        finally
        {
            CultureInfo.CurrentUICulture = originalCulture;
        }
    }

    [Fact]
    public void Unavailable_translation_is_excluded_even_when_a_record_exists()
    {
        var availability = new SeoTranslationAvailability(CreateCatalog(unavailableRussianStatus: VerificationStatus.Outdated), CreateGuides());

        var alternates = availability.Category("alpha", "unavailable");

        Assert.Equal(new[] { "en", "uk" }, alternates.Select(item => item.Language).OrderBy(x => x));
        Assert.DoesNotContain(alternates, item => item.Language == "ru");
    }

    [Fact]
    public void Guide_availability_uses_only_approved_existing_translations()
    {
        var availability = new SeoTranslationAvailability(CreateCatalog(), CreateGuides());

        var available = availability.Guide("alpha", "approved");
        var partial = availability.Guide("alpha", "partial-guide");

        Assert.Equal(new[] { "en", "ru", "uk" }, available.Select(item => item.Language).OrderBy(x => x));
        Assert.Equal(new[] { "en", "uk" }, partial.Select(item => item.Language).OrderBy(x => x));
        Assert.Empty(availability.Guide("alpha", "draft"));
        Assert.Empty(availability.Guide("alpha", "missing-guide"));

        var generator = new SeoMetadataGenerator("https://example.test", availability);
        var originalCulture = CultureInfo.CurrentUICulture;
        try
        {
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
            var metadata = generator.CreateGuide("Partial guide", "Description", "alpha", "partial-guide");
            Assert.NotNull(metadata);
            Assert.Equal(new[] { "en", "uk" }, metadata.Alternates.Select(item => item.Language).OrderBy(x => x));
            Assert.DoesNotContain(metadata.Alternates, item => item.Url.Contains("/ru/", StringComparison.Ordinal));
        }
        finally
        {
            CultureInfo.CurrentUICulture = originalCulture;
        }
    }

    [Fact]
    public void Generated_country_hreflang_is_reciprocal_and_uses_language_specific_urls()
    {
        var availability = new SeoTranslationAvailability(CreateCatalog(), CreateGuides());
        var generator = new SeoMetadataGenerator("https://example.test", availability);
        var originalCulture = CultureInfo.CurrentUICulture;

        try
        {
            var metadataByLanguage = new Dictionary<string, SeoMetadata>();
            foreach (var language in new[] { "en", "uk", "ru" })
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(language);
                var metadata = generator.CreateCountry("Alpha", "Description", "alpha");
                Assert.NotNull(metadata);
                metadataByLanguage[language] = metadata;
            }

            foreach (var (language, metadata) in metadataByLanguage)
            {
                var expectedUrls = metadataByLanguage.Values.Select(item => item.CanonicalUrl).OrderBy(x => x);
                Assert.Equal(expectedUrls, metadata.Alternates.Select(item => item.Url).OrderBy(x => x));
                Assert.Contains(metadata.Alternates, item => item.Language == language && item.Url == metadata.CanonicalUrl);
            }
        }
        finally
        {
            CultureInfo.CurrentUICulture = originalCulture;
        }
    }

    [Fact]
    public void Static_page_availability_has_all_supported_languages()
    {
        var availability = new SeoTranslationAvailability(CreateCatalog(), CreateGuides());

        var alternates = availability.StaticPage("/about");

        Assert.Equal(new[] { "en", "ru", "uk" }, alternates.Select(item => item.Language).OrderBy(x => x));
        Assert.Equal(new[] { "/en/about", "/ru/about", "/uk/about" }, alternates.Select(item => item.Path).OrderBy(x => x));
    }

    [Fact]
    public void Sitemap_is_deterministic_and_contains_only_eligible_localized_destinations()
    {
        var catalog = CreateCatalog(VerificationStatus.Outdated);
        var guides = CreateGuides();
        var availability = new SeoTranslationAvailability(catalog, guides);
        var seo = new SeoMetadataGenerator("https://canonical.example", availability);
        var xml = new SeoSitemap(catalog, guides, availability, seo).Xml;
        Assert.Equal(xml, new SeoSitemap(catalog, guides, availability, seo).Xml);
        XNamespace sitemap = "http://www.sitemaps.org/schemas/sitemap/0.9";
        XNamespace xhtml = "http://www.w3.org/1999/xhtml";
        var root = XElement.Parse(xml);
        Assert.Equal(sitemap + "urlset", root.Name);
        var nodes = root.Elements(sitemap + "url").ToArray();
        var locations = nodes.Select(node => node.Element(sitemap + "loc")!.Value).ToArray();
        Assert.Equal(43, locations.Length); // 21 static, 3 country, 7 category, 5 guide and 7 article.
        Assert.Equal(locations.OrderBy(url => url, StringComparer.Ordinal), locations);
        Assert.Equal(locations.Length, locations.Distinct().Count());
        Assert.All(locations, url =>
        {
            Assert.StartsWith("https://canonical.example/", url);
            Assert.DoesNotContain("?", url);
            Assert.DoesNotContain("#", url);
            Assert.DoesNotContain("/hidden", url);
            Assert.DoesNotContain("/draft", url);
        });
        Assert.DoesNotContain("https://canonical.example/ru/countries/alpha/categories/partial", locations);
        Assert.DoesNotContain("https://canonical.example/ru/guides/alpha/partial-guide", locations);
        Assert.DoesNotContain(locations, url => url.Contains("/ru/articles/alpha/unavailable/", StringComparison.Ordinal));
        Assert.Equal(7, nodes.Count(node => node.Element(sitemap + "lastmod")?.Value == "1970-01-01"));
        var byLocation = nodes.ToDictionary(node => node.Element(sitemap + "loc")!.Value);
        foreach (var node in nodes)
        {
            var alternatives = node.Elements(xhtml + "link").ToArray();
            Assert.Contains(alternatives, link => (string?)link.Attribute("href") == node.Element(sitemap + "loc")!.Value);
            Assert.All(alternatives, link =>
            {
                Assert.NotEqual("x-default", (string?)link.Attribute("hreflang"));
                var reciprocal = byLocation[link.Attribute("href")!.Value].Elements(xhtml + "link");
                Assert.Equal(alternatives.Select(item => item.ToString()), reciprocal.Select(item => item.ToString()));
            });
        }
    }

    [Fact]
    public void Sitemap_XML_escapes_URL_values()
    {
        // A synthetic slug exercises XML serialization independently of catalog slug validation.
        var catalog = CreateCatalog(publicSlug: "alpha&beta");
        var guides = CreateGuides();
        var availability = new SeoTranslationAvailability(catalog, guides);
        var xml = new SeoSitemap(catalog, guides, availability, new SeoMetadataGenerator("https://canonical.example")).Xml;
        Assert.Contains("/countries/alpha&amp;beta", xml);
        XNamespace sitemap = "http://www.sitemaps.org/schemas/sitemap/0.9";
        Assert.Contains(XElement.Parse(xml).Descendants(sitemap + "loc"),
            location => location.Value == "https://canonical.example/en/countries/alpha&beta");
    }

    private static CatalogData CreateCatalog(VerificationStatus unavailableRussianStatus = VerificationStatus.Verified, string publicSlug = "alpha")
    {
        var countries = new[]
        {
            new Country(PublicCountryId, "Alpha", publicSlug),
            new Country(HiddenCountryId, "Hidden", "hidden")
        };
        var categories = new[]
        {
            new InformationCategory(AllLanguagesCategoryId, "All languages", "all"),
            new InformationCategory(PartialCategoryId, "Partial", "partial"),
            new InformationCategory(UnavailableCategoryId, "Unavailable", "unavailable")
        };
        var allEntryId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var partialEntryId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var unavailableEntryId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var hiddenEntryId = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var entries = new[]
        {
            new InformationEntry(allEntryId, PublicCountryId, AllLanguagesCategoryId),
            new InformationEntry(partialEntryId, PublicCountryId, PartialCategoryId),
            new InformationEntry(unavailableEntryId, PublicCountryId, UnavailableCategoryId),
            new InformationEntry(hiddenEntryId, HiddenCountryId, AllLanguagesCategoryId)
        };
        var translations = new List<InformationEntryTranslation>();
        AddTranslations(translations, allEntryId, ["en", "uk", "ru"], VerificationStatus.Verified);
        AddTranslations(translations, partialEntryId, ["en", "uk"], VerificationStatus.Verified);
        AddTranslations(translations, unavailableEntryId, ["en", "uk", "ru"], VerificationStatus.Verified, unavailableRussianStatus);
        AddTranslations(translations, hiddenEntryId, ["en", "uk", "ru"], VerificationStatus.NeedsReview);

        var constructor = typeof(CatalogData).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Single();
        return (CatalogData)constructor.Invoke(new object?[] { countries, categories, entries, translations, Array.Empty<OfficialSource>() });
    }

    private static void AddTranslations(
        ICollection<InformationEntryTranslation> translations,
        Guid entryId,
        IEnumerable<string> languages,
        VerificationStatus status,
        VerificationStatus? russianStatus = null)
    {
        foreach (var language in languages)
        {
            var translationStatus = language == "ru" ? russianStatus ?? status : status;
            translations.Add(new InformationEntryTranslation(entryId, language, $"Title {language}", $"Summary {language}",
                $"Content {language}", translationStatus, DateTimeOffset.UnixEpoch));
        }
    }

    private static StepByStepGuideCatalog CreateGuides()
    {
        var approved = new StepByStepGuide("approved-id", "approved", PublicCountryId, GuideEditorialStatus.Approved,
            LocalizedTexts("en", "uk", "ru"), []);
        var partial = new StepByStepGuide("partial-id", "partial-guide", PublicCountryId, GuideEditorialStatus.Approved,
            LocalizedTexts("en", "uk"), []);
        var draft = new StepByStepGuide("draft-id", "draft", PublicCountryId, GuideEditorialStatus.Draft,
            LocalizedTexts("en", "uk", "ru"), []);
        return new StepByStepGuideCatalog([approved, partial, draft]);
    }

    private static IReadOnlyDictionary<string, LocalizedGuideText> LocalizedTexts(params string[] languages) =>
        languages.ToDictionary(language => language, language => new LocalizedGuideText($"Title {language}", $"Description {language}"));
}
