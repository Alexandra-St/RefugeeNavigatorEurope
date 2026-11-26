using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RefugeeNavigator.Web.Localization;
using RefugeeNavigator.Web.Security;

namespace RefugeeNavigator.Tests;

public sealed class LanguagePathTests
{
    [Theory]
    [InlineData("en", "/search", "/en/search")]
    [InlineData("uk", "/compare?country1=fi", "/uk/compare?country1=fi")]
    [InlineData("ru", "/countries/fi", "/ru/countries/fi")]
    public void Builds_language_prefixed_urls(string culture, string path, string expected)
        => Assert.Equal(expected, LocalizedUrl.ForCurrentCulture(path, culture));

    [Fact]
    public void Does_not_substitute_a_different_language_for_unsupported_culture()
        => Assert.Throws<ArgumentOutOfRangeException>(() => LocalizedUrl.ForCurrentCulture("/search", "es"));

    [Fact]
    public void Language_switch_preserves_destination_query_and_fragment()
        => Assert.Equal("/ru/countries/fi?category=housing#article-1", LocalizedUrl.ForLanguageSwitch(
            new Uri("https://example.test/en/countries/fi?category=housing#article-1"), "ru"));

    [Fact]
    public async Task Explicit_prefix_overrides_cookie_and_preserves_prefixed_route_path()
    {
        var context = new DefaultHttpContext();
        context.Request.Path = "/en/search";
        context.Request.Headers.Cookie = ".AspNetCore.Culture=c%3Dru%7Cuic%3Dru";
        context.Request.Headers.AcceptLanguage = "ru";
        var invoked = false;
        var middleware = new LanguagePathMiddleware(_ =>
        {
            invoked = true;
            Assert.Equal("/en/search", context.Request.Path);
            Assert.Equal("en", System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
            return Task.CompletedTask;
        });

        await middleware.InvokeAsync(context);
        Assert.True(invoked);
    }

    [Theory]
    [InlineData("en", "en")]
    [InlineData("uk", "uk")]
    [InlineData("ru", "ru")]
    public async Task Every_supported_prefix_selects_its_culture(string prefix, string expectedCulture)
    {
        var context = new DefaultHttpContext();
        context.Request.Path = $"/{prefix}/search";
        context.Request.Headers.AcceptLanguage = "ru";
        var middleware = new LanguagePathMiddleware(_ =>
        {
            Assert.Equal(expectedCulture, System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
            Assert.Equal(expectedCulture, context.Features.Get<Microsoft.AspNetCore.Localization.IRequestCultureFeature>()?.RequestCulture.UICulture.TwoLetterISOLanguageName);
            return Task.CompletedTask;
        });

        await middleware.InvokeAsync(context);
    }

    [Theory]
    [InlineData("/search?q=permit", "/uk/search?q=permit")]
    [InlineData("/countries/fi", "/uk/countries/fi")]
    public async Task Legacy_indexable_urls_redirect_with_query(string pathAndQuery, string expected)
    {
        var context = new DefaultHttpContext();
        var uri = new Uri("https://example.test" + pathAndQuery);
        context.Request.Path = uri.AbsolutePath;
        context.Request.QueryString = new QueryString(uri.Query);
        var middleware = new LanguagePathMiddleware(_ => Task.CompletedTask);

        await middleware.InvokeAsync(context);

        Assert.Equal(StatusCodes.Status308PermanentRedirect, context.Response.StatusCode);
        Assert.Equal(expected, context.Response.Headers.Location);
    }

    [Theory]
    [InlineData("/_framework/blazor.web.js")]
    [InlineData("/set-language")]
    [InlineData("/favicon.ico")]
    public async Task Infrastructure_and_static_routes_are_not_redirected(string path)
    {
        var context = new DefaultHttpContext();
        context.Request.Path = path;
        var invoked = false;
        var middleware = new LanguagePathMiddleware(_ => { invoked = true; return Task.CompletedTask; });
        await middleware.InvokeAsync(context);
        Assert.True(invoked);
    }

    [Fact]
    public async Task Private_beta_authentication_still_blocks_prefixed_routes()
    {
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["PRIVATE_ALPHA_ACCESS_MODE"] = "private",
            ["PRIVATE_ALPHA_USERNAME"] = "test-user",
            ["PRIVATE_ALPHA_PASSWORD"] = "Strong-Test-Password-123!"
        }).Build();
        var options = PrivateAlphaAccessOptions.FromConfiguration(configuration, isDevelopment: false);
        var context = new DefaultHttpContext();
        context.Request.Path = "/en/search";
        context.Request.Scheme = "https";
        var languageMiddlewareCalled = false;
        var authentication = new PrivateAlphaAuthenticationMiddleware(
            async _ =>
            {
                var language = new LanguagePathMiddleware(__ =>
                {
                    languageMiddlewareCalled = true;
                    return Task.CompletedTask;
                });
                await language.InvokeAsync(context);
            },
            options);

        await authentication.InvokeAsync(context);

        Assert.Equal(StatusCodes.Status401Unauthorized, context.Response.StatusCode);
        Assert.False(languageMiddlewareCalled);
    }
}
