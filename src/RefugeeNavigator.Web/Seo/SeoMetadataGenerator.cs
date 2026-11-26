using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Web.Seo;

public sealed class SeoMetadataGenerator
{
    public const string DefaultCanonicalOrigin = "https://refugeenavigatoreurope.onrender.com";
    private readonly Uri _origin;
    private readonly SeoTranslationAvailability? _availability;

    public SeoMetadataGenerator(string? configuredOrigin, SeoTranslationAvailability? availability = null)
    {
        _availability = availability;
        var value = string.IsNullOrWhiteSpace(configuredOrigin) ? DefaultCanonicalOrigin : configuredOrigin.Trim();
        if (!Uri.TryCreate(value, UriKind.Absolute, out var origin)
            || origin.Scheme != Uri.UriSchemeHttps
            || string.IsNullOrEmpty(origin.Host)
            || !string.IsNullOrEmpty(origin.UserInfo)
            || origin.AbsolutePath != "/"
            || !string.IsNullOrEmpty(origin.Query)
            || !string.IsNullOrEmpty(origin.Fragment))
        {
            throw new ArgumentException("Seo:CanonicalOrigin must be an absolute HTTPS origin without credentials, path, query, or fragment.", nameof(configuredOrigin));
        }

        _origin = new UriBuilder(origin) { Path = string.Empty }.Uri;
    }

    public SeoMetadata CreateStaticPage(string title, string description, string path) =>
        Create(title, description,
            LocalizedUrl.ForCurrentCulture(path, System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName),
            RequireAvailability().StaticPage(path));

    public SeoMetadata? CreateCountry(string title, string description, string slug) =>
        CreateAvailablePage(title, description, RequireAvailability().Country(slug));

    public SeoMetadata? CreateCategory(string title, string description, string countrySlug, string categorySlug) =>
        CreateAvailablePage(title, description, RequireAvailability().Category(countrySlug, categorySlug));

    public SeoMetadata? CreateGuide(string title, string description, string countrySlug, string guideSlug) =>
        CreateAvailablePage(title, description, RequireAvailability().Guide(countrySlug, guideSlug));

    private SeoMetadata? CreateAvailablePage(
        string title, string description, IReadOnlyList<(string Language, string Path)> alternates)
    {
        var language = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        var canonical = alternates.FirstOrDefault(item => item.Language == language).Path;
        return canonical is null ? null : Create(title, description, canonical, alternates);
    }

    private SeoTranslationAvailability RequireAvailability() => _availability
        ?? throw new InvalidOperationException("Translation availability was not configured for this SEO generator.");

    public SeoMetadata Create(
        string title,
        string description,
        string canonicalPath,
        IEnumerable<(string Language, string Path)> alternates)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        var canonical = BuildAbsoluteUrl(canonicalPath);
        var availableAlternates = alternates
            .Where(item => !string.IsNullOrWhiteSpace(item.Language)
                && !string.IsNullOrWhiteSpace(item.Path)
                && !item.Path.Contains("#", StringComparison.Ordinal))
            .GroupBy(item => item.Language, StringComparer.OrdinalIgnoreCase)
            .Select(group => new SeoAlternate(group.Key, BuildAbsoluteUrl(group.First().Path)))
            .ToArray();

        return new SeoMetadata(title, description, canonical, availableAlternates, title, description, canonical);
    }

    public string BuildAbsoluteUrl(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !path.StartsWith("/", StringComparison.Ordinal)
            || path.StartsWith("//", StringComparison.Ordinal) || path.Contains('\\'))
            throw new ArgumentException("SEO paths must be absolute site paths.", nameof(path));

        var normalizedPath = path.Split('?', '#')[0];
        return new Uri(_origin, normalizedPath).AbsoluteUri;
    }
}
