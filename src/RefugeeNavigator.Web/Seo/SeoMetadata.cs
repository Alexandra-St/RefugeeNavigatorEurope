namespace RefugeeNavigator.Web.Seo;

public sealed record SeoAlternate(string Language, string Url);

public sealed record SeoMetadata(
    string Title,
    string Description,
    string CanonicalUrl,
    IReadOnlyList<SeoAlternate> Alternates,
    string OpenGraphTitle,
    string OpenGraphDescription,
    string OpenGraphUrl);
