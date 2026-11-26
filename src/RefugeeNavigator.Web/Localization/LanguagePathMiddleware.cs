using System.Globalization;
using Microsoft.AspNetCore.Localization;
using RefugeeNavigator.Web.Seo;

namespace RefugeeNavigator.Web.Localization;

public sealed class LanguagePathMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value ?? "/";
        var first = path.Split('/', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        if (first is "en" or "uk" or "ru")
        {
            var remainder = path.Length == first.Length + 1 ? "/" : path[(first.Length + 1)..];
            if (IsStaticAssetPath(remainder))
            {
                context.Request.Path = remainder;
            }
            SetCulture(context, first);
        }
        else if (!IsExcluded(path) && IsIndexablePath(path))
        {
            context.Response.StatusCode = StatusCodes.Status308PermanentRedirect;
            context.Response.Headers.Location = $"/uk{path}{context.Request.QueryString}";
            return;
        }

        await next(context);
    }

    private static void SetCulture(HttpContext context, string culture)
    {
        var requestCulture = new RequestCulture(culture);
        CultureInfo.CurrentCulture = requestCulture.Culture;
        CultureInfo.CurrentUICulture = requestCulture.UICulture;
        context.Features.Set<IRequestCultureFeature>(new RequestCultureFeature(
            requestCulture,
            new CustomRequestCultureProvider(_ => Task.FromResult<ProviderCultureResult?>(
                new ProviderCultureResult(culture)))));
    }

    private static bool IsExcluded(string path) =>
        path.StartsWith("/_", StringComparison.Ordinal) ||
        path.StartsWith("/api/", StringComparison.Ordinal) ||
        path.StartsWith("/set-language", StringComparison.Ordinal) ||
        Path.HasExtension(path);

    private static bool IsStaticAssetPath(string path) =>
        path.StartsWith("/_framework/", StringComparison.Ordinal) ||
        Path.HasExtension(path);

    private static bool IsIndexablePath(string path)
    {
        if (SeoRoutePolicy.IndexableStaticPaths.Contains(path, StringComparer.Ordinal)) return true;
        var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        return segments.Length == 2 && segments[0] == "countries"
            || segments.Length == 4 && segments[0] == "countries" && segments[2] == "categories"
            || segments.Length == 3 && segments[0] == "guides";
    }
}

public static class LocalizedUrl
{
    public static string ForLanguageSwitch(Uri currentUri, string culture)
    {
        ArgumentNullException.ThrowIfNull(currentUri);
        var path = currentUri.AbsolutePath;
        var first = path.Split('/', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        if (first is "en" or "uk" or "ru")
        {
            path = path.Length == first.Length + 1 ? "/" : path[(first.Length + 1)..];
        }

        return ForCurrentCulture(path + currentUri.Query + currentUri.Fragment, culture);
    }

    public static string ForCurrentCulture(string path, string culture)
    {
        if (!SupportedCultures.All.Any(item => item.TwoLetterISOLanguageName == culture))
        {
            throw new ArgumentOutOfRangeException(nameof(culture), culture, "The language is not supported.");
        }

        return $"/{culture}{(path.StartsWith('/') ? path : "/" + path)}";
    }
}
