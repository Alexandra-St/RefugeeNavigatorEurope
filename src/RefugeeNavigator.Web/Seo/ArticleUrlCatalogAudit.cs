using RefugeeNavigator.Data;
using RefugeeNavigator.Domain;
using RefugeeNavigator.Web.Content;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Web.Seo;

public sealed record ArticleUrlCatalogAudit(int ProductionArticles, int LocalizedArticleUrls,
    IReadOnlyList<string> MissingTranslations, IReadOnlyList<string> Collisions);

public static class ArticleUrlCatalogAuditRunner
{
    // These are the existing routable page families. The article route is checked against each
    // concrete production URL so future static routes can be included here without changing data.
    private static readonly string[] ExistingRoutePatterns =
    ["/", "/about", "/compare", "/contact", "/disclaimer", "/privacy", "/search", "/not-found",
     "/countries/{country}", "/countries/{country}/categories/{category}", "/guides/{country}/{guide}"];

    public static ArticleUrlCatalogAudit Run(CatalogData catalog)
    {
        var resolver = new ArticleUrlResolver(catalog);
        var visibleCountries = CountryVisibility.GetVisibleCountries(catalog, false).Select(c => c.Id).ToHashSet();
        var entries = catalog.InformationEntries.Where(e => visibleCountries.Contains(e.CountryId) &&
            catalog.InformationEntryTranslations.Any(t => t.InformationEntryId == e.Id && t.VerificationStatus == VerificationStatus.Verified)).ToArray();
        var missing = new List<string>();
        var urls = new List<string>();
        foreach (var entry in entries)
        {
            foreach (var language in SupportedCultures.All.Select(c => c.TwoLetterISOLanguageName))
            {
                var url = resolver.Generate(entry, language);
                if (url is null) missing.Add($"{entry.Id}:{language}"); else urls.Add(url);
            }
        }

        var collisions = urls.GroupBy(url => url, StringComparer.Ordinal).Where(group => group.Count() > 1)
            .Select(group => $"duplicate:{group.Key}").ToList();
        foreach (var url in urls)
        {
            if (ExistingRoutePatterns.Contains(url, StringComparer.OrdinalIgnoreCase)) collisions.Add($"route:{url}");
        }
        return new ArticleUrlCatalogAudit(entries.Length, urls.Count, missing, collisions);
    }
}
