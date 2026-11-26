using System.Globalization;
using System.Xml.Linq;
using RefugeeNavigator.Data;
using RefugeeNavigator.Web.Guides;

namespace RefugeeNavigator.Web.Seo;

public sealed class SeoSitemap
{
    private static readonly XNamespace Sitemap = "http://www.sitemaps.org/schemas/sitemap/0.9";
    private static readonly XNamespace Xhtml = "http://www.w3.org/1999/xhtml";
    private readonly Lazy<string> _xml;

    public SeoSitemap(CatalogData catalog, StepByStepGuideCatalog guides, SeoTranslationAvailability availability,
        SeoMetadataGenerator seo)
    {
        _xml = new(() => Generate(catalog, guides, availability, seo));
    }

    public string Xml => _xml.Value;

    private static string Generate(CatalogData catalog, StepByStepGuideCatalog guides,
        SeoTranslationAvailability availability, SeoMetadataGenerator seo)
    {
        var urls = new SortedDictionary<string, XElement>(StringComparer.Ordinal);

        void Add(IReadOnlyList<(string Language, string Path)> localized, Func<string, DateTimeOffset?>? lastModified = null)
        {
            var alternates = localized.OrderBy(item => item.Language, StringComparer.Ordinal)
                .Select(item => (item.Language, Url: seo.BuildAbsoluteUrl(item.Path))).ToArray();
            foreach (var (language, url) in alternates)
            {
                var updated = lastModified?.Invoke(language);
                var element = new XElement(Sitemap + "url", new XElement(Sitemap + "loc", url));
                if (updated is { } date && date != default)
                {
                    element.Add(new XElement(Sitemap + "lastmod",
                        date.UtcDateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)));
                }
                element.Add(alternates.Select(alternate => new XElement(Xhtml + "link",
                    new XAttribute("rel", "alternate"), new XAttribute("hreflang", alternate.Language),
                    new XAttribute("href", alternate.Url))));
                if (!urls.TryAdd(url, element))
                    throw new InvalidOperationException($"Duplicate canonical sitemap URL: {url}");
            }
        }

        foreach (var path in SeoRoutePolicy.IndexableStaticPaths)
            Add(availability.StaticPage(path));

        foreach (var country in catalog.Countries)
        {
            var localized = availability.Country(country.Slug);
            if (localized.Count == 0) continue;
            Add(localized);
            foreach (var category in catalog.Categories)
                Add(availability.Category(country.Slug, category.Slug));
            foreach (var guide in guides.Guides.Where(guide => guide.CountryId == country.Id))
                Add(availability.Guide(country.Slug, guide.Slug));
        }

        var articleDates = catalog.InformationEntryTranslations.ToLookup(item => item.InformationEntryId);
        foreach (var article in catalog.InformationEntries)
        {
            Add(availability.Article(article.Id), language =>
                articleDates[article.Id].FirstOrDefault(item => item.LanguageCode == language)?.LastUpdatedAt);
        }

        return new XElement(Sitemap + "urlset", new XAttribute(XNamespace.Xmlns + "xhtml", Xhtml), urls.Values)
            .ToString(SaveOptions.DisableFormatting);
    }
}
