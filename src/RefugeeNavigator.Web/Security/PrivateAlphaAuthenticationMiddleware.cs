using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace RefugeeNavigator.Web.Security;

public sealed class PrivateAlphaAuthenticationMiddleware(
    RequestDelegate next,
    PrivateAlphaAccessOptions options)
{
    private const string Challenge = "Basic realm=\"Refugee Navigator Europe Private Alpha\", charset=\"UTF-8\"";
    private static readonly Encoding StrictUtf8 = new UTF8Encoding(false, true);

    public async Task InvokeAsync(HttpContext context)
    {
        if (!options.RequiresAuthentication)
        {
            await next(context);
            return;
        }

        context.Response.Headers["X-Robots-Tag"] = "noindex, nofollow";

        if (!options.IsDevelopment && !context.Request.IsHttps)
        {
            context.Response.StatusCode = StatusCodes.Status426UpgradeRequired;
            context.Response.Headers.Upgrade = "TLS/1.2";
            context.Response.ContentLength = 0;
            return;
        }

        if (TryReadCredentials(context.Request.Headers.Authorization, out var username, out var password)
            && options.MatchesCredentials(username, password))
        {
            await next(context);
            return;
        }

        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.Headers.WWWAuthenticate = Challenge;
        context.Response.Headers.CacheControl = "no-store";
        context.Response.ContentLength = 0;
    }

    private static bool TryReadCredentials(
        string? authorizationHeader,
        out string username,
        out string password)
    {
        username = string.Empty;
        password = string.Empty;

        if (!AuthenticationHeaderValue.TryParse(authorizationHeader, out var header)
            || !string.Equals(header.Scheme, "Basic", StringComparison.OrdinalIgnoreCase)
            || string.IsNullOrWhiteSpace(header.Parameter))
        {
            return false;
        }

        try
        {
            var credentialBytes = Convert.FromBase64String(header.Parameter);
            var credentials = StrictUtf8.GetString(credentialBytes);
            var separatorIndex = credentials.IndexOf(':');
            if (separatorIndex <= 0)
            {
                return false;
            }

            username = credentials[..separatorIndex];
            password = credentials[(separatorIndex + 1)..];
            return password.Length > 0;
        }
        catch (FormatException)
        {
            return false;
        }
        catch (DecoderFallbackException)
        {
            return false;
        }
    }
}
