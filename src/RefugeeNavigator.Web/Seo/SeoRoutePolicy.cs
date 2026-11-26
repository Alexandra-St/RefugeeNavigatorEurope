using Microsoft.Extensions.Primitives;
using RefugeeNavigator.Web.Security;

namespace RefugeeNavigator.Web.Seo;

public static class SeoRoutePolicy
{
    public static IReadOnlyList<string> IndexableStaticPaths { get; } =
        Array.AsReadOnly(new[] { "/", "/about", "/compare", "/contact", "/disclaimer", "/privacy", "/search" });

    public static bool HasFunctionalQuery(string path, IReadOnlyDictionary<string, StringValues> query) =>
        path switch
        {
            "/search" => query.ContainsKey("q") || query.ContainsKey("country") || query.ContainsKey("category"),
            "/compare" => query.ContainsKey("country1") || query.ContainsKey("country2") || query.ContainsKey("category"),
            _ => false
        };

    public static bool ExposesSitemap(PrivateAlphaAccessMode mode) => mode == PrivateAlphaAccessMode.Public;

    public static string Robots(PrivateAlphaAccessMode mode, SeoMetadataGenerator seo) =>
        ExposesSitemap(mode)
            ? "User-agent: *\nAllow: /\nDisallow: /_blazor\nDisallow: /api/\nDisallow: /set-language\nSitemap: "
                + seo.BuildAbsoluteUrl("/sitemap.xml") + "\n"
            : "User-agent: *\nDisallow: /\n";
}
