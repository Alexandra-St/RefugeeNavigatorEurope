using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Tests;

public sealed class CircuitCultureTests
{
    [Theory]
    [InlineData("en", "ru")]
    [InlineData("uk", "en")]
    [InlineData("ru", "uk")]
    public async Task Guide_document_and_both_circuit_requests_use_route_language(string language, string preference)
    {
        var guide = $"/{language}/guides/germany/temporary-protection";
        var documentCulture = await ResolveCulture(guide, "?routeCulture=" + preference, preference);
        Assert.Equal((language, language), documentCulture);

        var query = "?routeCulture=" + documentCulture.UiCulture;
        Assert.Equal(documentCulture, await ResolveCulture("/_blazor/negotiate", query, preference));
        Assert.Equal(documentCulture, await ResolveCulture("/_blazor", query + "&id=test", preference));
    }

    [Fact]
    public async Task Explicit_switches_and_parallel_circuits_keep_independent_cultures()
    {
        var initial = new Uri("http://localhost/en/guides/germany/temporary-protection");
        var destinations = new[] { "en", "uk", "ru" };
        var cultures = await Task.WhenAll(destinations.Select(async language =>
        {
            var path = LocalizedUrl.ForLanguageSwitch(initial, language);
            var document = await ResolveCulture(path, "", "ru");
            return await ResolveCulture("/_blazor", "?routeCulture=" + document.UiCulture, "ru");
        }));

        Assert.Equal(destinations.Select(language => (language, language)), cultures);
    }

    [Theory]
    [InlineData("/_blazor", "", "ru")]
    [InlineData("/_blazor", "?routeCulture=es", "ru")]
    [InlineData("/_blazor/negotiate", "?routeCulture=en&routeCulture=uk", "ru")]
    [InlineData("/_framework/blazor.web.js", "?routeCulture=en", "ru")]
    [InlineData("/_blazor/other", "?routeCulture=en", "ru")]
    public async Task Circuit_query_preserves_existing_fallback_elsewhere(string path, string query, string expected)
        => Assert.Equal((expected, expected), await ResolveCulture(path, query, "ru"));

    private static async Task<(string Culture, string UiCulture)> ResolveCulture(
        string path, string query, string preference)
    {
        var context = new DefaultHttpContext();
        context.Request.Path = path;
        context.Request.QueryString = new QueryString(query);
        context.Request.Headers.Cookie = $".AspNetCore.Culture=c%3D{preference}%7Cuic%3D{preference}";
        context.Request.Headers.AcceptLanguage = preference;
        var options = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("uk"),
            SupportedCultures = SupportedCultures.All.ToArray(),
            SupportedUICultures = SupportedCultures.All.ToArray(),
            RequestCultureProviders = new IRequestCultureProvider[]
            {
                new CircuitRequestCultureProvider(),
                new CookieRequestCultureProvider(),
                new AcceptLanguageHeaderRequestCultureProvider()
            }
        };
        (string Culture, string UiCulture) resolved = default;
        var routing = new LanguagePathMiddleware(async _ =>
        {
            await Task.Yield();
            resolved = (CultureInfo.CurrentCulture.Name, CultureInfo.CurrentUICulture.Name);
        });
        var localization = new RequestLocalizationMiddleware(
            routing.InvokeAsync, Options.Create(options), NullLoggerFactory.Instance);

        await localization.Invoke(context);
        return resolved;
    }
}
