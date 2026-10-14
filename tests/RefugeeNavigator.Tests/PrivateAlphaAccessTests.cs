using System.Net;
using System.Text;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RefugeeNavigator.Web.Security;

namespace RefugeeNavigator.Tests;

public sealed class PrivateAlphaAccessTests
{
    private const string TestUsername = "alpha-tester";
    private const string TestPassword = "Test-Alpha-Passphrase-2026!";

    [Fact]
    public void NonDevelopmentRequiresExplicitAccessMode()
    {
        Assert.Throws<InvalidOperationException>(() => CreateOptions(isDevelopment: false));
    }

    [Theory]
    [InlineData("unknown")]
    [InlineData("")]
    public void InvalidAccessModeFailsConfiguration(string mode)
    {
        Assert.Throws<InvalidOperationException>(() => CreateOptions(mode, isDevelopment: false));
    }

    [Fact]
    public void PrivateModeRequiresUsernameAndPassword()
    {
        Assert.Throws<InvalidOperationException>(() => CreateOptions("private", isDevelopment: false));
        Assert.Throws<InvalidOperationException>(() => CreateOptions(
            "private",
            isDevelopment: false,
            username: TestUsername));
        Assert.Throws<InvalidOperationException>(() => CreateOptions(
            "private",
            isDevelopment: false,
            password: TestPassword));
    }

    [Theory]
    [InlineData("", TestPassword)]
    [InlineData("alpha:tester", TestPassword)]
    [InlineData(TestUsername, "short")]
    [InlineData(TestUsername, "aaaaaaaaaaaaaaaa")]
    public void EmptyInvalidOrWeakPrivateCredentialsFailConfiguration(string username, string password)
    {
        Assert.Throws<InvalidOperationException>(() => CreateOptions(
            "private",
            isDevelopment: false,
            username,
            password));
    }

    [Fact]
    public void PublicModeDisablesAuthenticationExplicitly()
    {
        var options = CreateOptions("public", isDevelopment: false);

        Assert.False(options.RequiresAuthentication);
        Assert.Equal(PrivateAlphaAccessMode.Public, options.Mode);
    }

    [Fact]
    public void DevelopmentDefaultsToNoAuthentication()
    {
        var options = CreateOptions(isDevelopment: true);

        Assert.False(options.RequiresAuthentication);
        Assert.Equal(PrivateAlphaAccessMode.Development, options.Mode);
    }

    [Fact]
    public void DevelopmentCanExplicitlyEnablePrivateMode()
    {
        var options = CreateOptions("private", isDevelopment: true, TestUsername, TestPassword);

        Assert.True(options.RequiresAuthentication);
    }

    [Fact]
    public void RenderFreeTlsModeIsDisabledByDefault()
    {
        var options = CreateOptions(
            "private",
            isDevelopment: false,
            TestUsername,
            TestPassword,
            render: "true",
            renderServiceType: "web",
            renderExternalUrl: "https://refugee-navigator.onrender.com");

        Assert.Equal(PrivateAlphaTlsMode.Strict, options.TlsMode);
        Assert.False(options.UsesRenderFreeEdgeTls);
    }

    [Fact]
    public void RenderFreeTlsModeAcceptsExplicitValidConfiguration()
    {
        var options = CreateOptions(
            "private",
            isDevelopment: false,
            TestUsername,
            TestPassword,
            tlsMode: "render-free-edge",
            render: "true",
            renderServiceType: "web",
            renderExternalUrl: "https://refugee-navigator.onrender.com");

        Assert.Equal(PrivateAlphaTlsMode.RenderFreeEdge, options.TlsMode);
        Assert.True(options.UsesRenderFreeEdgeTls);
        Assert.True(options.RequiresAuthentication);
    }

    [Theory]
    [InlineData(null, "web", "https://refugee-navigator.onrender.com")]
    [InlineData("true", null, "https://refugee-navigator.onrender.com")]
    [InlineData("true", "worker", "https://refugee-navigator.onrender.com")]
    [InlineData("true", "web", null)]
    [InlineData("false", "web", "https://refugee-navigator.onrender.com")]
    public void RenderFreeTlsModeRequiresRenderWebRuntimeIndicators(
        string? render,
        string? renderServiceType,
        string? renderExternalUrl)
    {
        Assert.Throws<InvalidOperationException>(() => CreateOptions(
            "private",
            isDevelopment: false,
            TestUsername,
            TestPassword,
            tlsMode: "render-free-edge",
            render,
            renderServiceType,
            renderExternalUrl));
    }

    [Theory]
    [InlineData("http://refugee-navigator.onrender.com")]
    [InlineData("https://example.com")]
    [InlineData("not-a-url")]
    public void RenderFreeTlsModeRequiresHttpsOnRenderExternalUrl(string renderExternalUrl)
    {
        Assert.Throws<InvalidOperationException>(() => CreateOptions(
            "private",
            isDevelopment: false,
            TestUsername,
            TestPassword,
            tlsMode: "render-free-edge",
            render: "true",
            renderServiceType: "web",
            renderExternalUrl));
    }

    [Theory]
    [InlineData("public", false)]
    [InlineData(null, true)]
    [InlineData("private", true)]
    public void RenderFreeTlsModeRequiresNonDevelopmentPrivateMode(string? mode, bool isDevelopment)
    {
        Assert.Throws<InvalidOperationException>(() => CreateOptions(
            mode,
            isDevelopment,
            TestUsername,
            TestPassword,
            tlsMode: "render-free-edge",
            render: "true",
            renderServiceType: "web",
            renderExternalUrl: "https://refugee-navigator.onrender.com"));
    }

    [Fact]
    public void RenderFreeTlsModeRequiresPrivateCredentials()
    {
        Assert.Throws<InvalidOperationException>(() => CreateOptions(
            "private",
            isDevelopment: false,
            tlsMode: "render-free-edge",
            render: "true",
            renderServiceType: "web",
            renderExternalUrl: "https://refugee-navigator.onrender.com"));
    }

    [Fact]
    public void RenderFreeTlsModeRejectsSimultaneousTrustedProxyConfiguration()
    {
        Assert.Throws<InvalidOperationException>(() => CreateOptions(
            "private",
            isDevelopment: false,
            TestUsername,
            TestPassword,
            tlsMode: "render-free-edge",
            render: "true",
            renderServiceType: "web",
            renderExternalUrl: "https://refugee-navigator.onrender.com",
            trustedProxyIps: "10.0.0.1"));
    }

    [Fact]
    public void UnknownTlsModeFailsConfiguration()
    {
        Assert.Throws<InvalidOperationException>(() => CreateOptions(
            "private",
            isDevelopment: false,
            TestUsername,
            TestPassword,
            tlsMode: "trust-every-proxy"));
    }

    [Theory]
    [InlineData("/")]
    [InlineData("/search")]
    [InlineData("/compare")]
    [InlineData("/countries/denmark")]
    [InlineData("/countries/denmark/categories/employment")]
    [InlineData("/app.css")]
    [InlineData("/set-language?culture=en&returnUrl=%2F")]
    public async Task PrivateModeRejectsAnonymousRequestsForAllEndpointTypes(string path)
    {
        var options = CreateOptions("private", isDevelopment: true, TestUsername, TestPassword);
        var response = await InvokeMiddlewareAsync(options, path, authorization: null);

        Assert.Equal(StatusCodes.Status401Unauthorized, response.StatusCode);
        Assert.Equal(0, response.Body.Length);
        Assert.Contains("Basic", response.WwwAuthenticate);
        Assert.Equal("noindex, nofollow", response.RobotsTag);
        Assert.False(response.NextWasCalled);
        Assert.DoesNotContain("protected page content", response.Body);
    }

    [Fact]
    public async Task PrivateModeRejectsIncorrectCredentialsWithoutReturningPageContent()
    {
        var options = CreateOptions("private", isDevelopment: true, TestUsername, TestPassword);
        var response = await InvokeMiddlewareAsync(
            options,
            "/",
            CreateAuthorization(TestUsername, "Wrong-Alpha-Passphrase-2026!"));

        Assert.Equal(StatusCodes.Status401Unauthorized, response.StatusCode);
        Assert.Equal(0, response.Body.Length);
        Assert.False(response.NextWasCalled);
        Assert.DoesNotContain("protected page content", response.Body);
    }

    [Fact]
    public async Task PrivateModeAllowsCorrectCredentials()
    {
        var options = CreateOptions("private", isDevelopment: true, TestUsername, TestPassword);
        var response = await InvokeMiddlewareAsync(
            options,
            "/",
            CreateAuthorization(TestUsername, TestPassword));

        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        Assert.Contains("protected page content", response.Body);
        Assert.True(response.NextWasCalled);
    }

    [Fact]
    public async Task PublicModeAllowsAnonymousRequests()
    {
        var options = CreateOptions("public", isDevelopment: false);
        var response = await InvokeMiddlewareAsync(options, "/", authorization: null, isHttps: true);

        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        Assert.True(response.NextWasCalled);
        Assert.Null(response.WwwAuthenticate);
        Assert.Null(response.RobotsTag);
    }

    [Fact]
    public async Task DevelopmentDefaultAllowsAnonymousRequests()
    {
        var options = CreateOptions(isDevelopment: true);
        var response = await InvokeMiddlewareAsync(options, "/", authorization: null);

        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        Assert.True(response.NextWasCalled);
    }

    [Fact]
    public async Task PrivateModeRequiresHttpsOutsideDevelopment()
    {
        var options = CreateOptions("private", isDevelopment: false, TestUsername, TestPassword);
        var response = await InvokeMiddlewareAsync(
            options,
            "/",
            CreateAuthorization(TestUsername, TestPassword),
            isHttps: false);

        Assert.Equal(StatusCodes.Status426UpgradeRequired, response.StatusCode);
        Assert.Null(response.WwwAuthenticate);
        Assert.False(response.NextWasCalled);
    }

    [Fact]
    public async Task SpoofedForwardedProtoCannotEnableStrictPrivateMode()
    {
        var options = CreateOptions("private", isDevelopment: false, TestUsername, TestPassword);
        var response = await InvokeMiddlewareAsync(
            options,
            "/",
            CreateAuthorization(TestUsername, TestPassword),
            forwardedProto: "https");

        Assert.Equal(StatusCodes.Status426UpgradeRequired, response.StatusCode);
        Assert.False(response.NextWasCalled);
        Assert.Equal("http", response.RequestScheme);
    }

    [Theory]
    [InlineData("/")]
    [InlineData("/countries/denmark")]
    [InlineData("/countries/denmark/categories/employment")]
    [InlineData("/search")]
    [InlineData("/compare")]
    [InlineData("/app.css")]
    [InlineData("/set-language?culture=en&returnUrl=%2F")]
    public async Task RenderFreeModeStillRejectsAnonymousRequests(string path)
    {
        var options = CreateRenderFreeOptions();
        var response = await InvokeMiddlewareAsync(
            options,
            path,
            authorization: null,
            applyRenderFreeSchemeMiddleware: true,
            forwardedProto: "https");

        Assert.Equal(StatusCodes.Status401Unauthorized, response.StatusCode);
        Assert.Equal(0, response.Body.Length);
        Assert.False(response.NextWasCalled);
        Assert.Equal("https", response.RequestScheme);
    }

    [Fact]
    public async Task RenderFreeModeRejectsIncorrectCredentials()
    {
        var options = CreateRenderFreeOptions();
        var response = await InvokeMiddlewareAsync(
            options,
            "/",
            CreateAuthorization(TestUsername, "Wrong-Alpha-Passphrase-2026!"),
            applyRenderFreeSchemeMiddleware: true);

        Assert.Equal(StatusCodes.Status401Unauthorized, response.StatusCode);
        Assert.Equal(0, response.Body.Length);
        Assert.False(response.NextWasCalled);
    }

    [Fact]
    public async Task RenderFreeModeAcceptsCorrectCredentialsAndIgnoresForwardedProto()
    {
        var options = CreateRenderFreeOptions();
        var response = await InvokeMiddlewareAsync(
            options,
            "/",
            CreateAuthorization(TestUsername, TestPassword),
            applyRenderFreeSchemeMiddleware: true,
            forwardedProto: "http");

        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        Assert.Contains("protected page content", response.Body);
        Assert.True(response.NextWasCalled);
        Assert.Equal("https", response.RequestScheme);
    }

    [Fact]
    public async Task RenderFreeModeProvidesHttpsSemanticsForSecureCookiesWithoutRedirecting()
    {
        var options = CreateRenderFreeOptions();
        var context = new DefaultHttpContext();
        context.Request.Scheme = "http";
        context.Request.Headers["X-Forwarded-Proto"] = "http";
        var endpointWasCalled = false;
        using var loggerFactory = LoggerFactory.Create(_ => { });

        var httpsRedirectionMiddleware = new HttpsRedirectionMiddleware(
            httpContext =>
            {
                endpointWasCalled = true;
                httpContext.Response.Cookies.Append(
                    "culture",
                    "en",
                    new CookieOptions { Secure = httpContext.Request.IsHttps, HttpOnly = true });
                return Task.CompletedTask;
            },
            Options.Create(new HttpsRedirectionOptions { HttpsPort = 443 }),
            new ConfigurationBuilder().Build(),
            loggerFactory);
        var middleware = new RenderFreeEdgeHttpsMiddleware(httpsRedirectionMiddleware.Invoke, options);

        await middleware.InvokeAsync(context);

        Assert.True(endpointWasCalled);
        Assert.Equal(StatusCodes.Status200OK, context.Response.StatusCode);
        Assert.Equal("https", context.Request.Scheme);
        Assert.Contains("secure", context.Response.Headers.SetCookie.ToString(), StringComparison.OrdinalIgnoreCase);
        Assert.False(context.Response.Headers.ContainsKey("Location"));
    }

    [Fact]
    public async Task PrivateModeAllowsCorrectCredentialsOverHttpsOutsideDevelopment()
    {
        var options = CreateOptions("private", isDevelopment: false, TestUsername, TestPassword);
        var response = await InvokeMiddlewareAsync(
            options,
            "/",
            CreateAuthorization(TestUsername, TestPassword),
            isHttps: true);

        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        Assert.Contains("protected page content", response.Body);
        Assert.True(response.NextWasCalled);
        Assert.Equal("noindex, nofollow", response.RobotsTag);
    }

    [Fact]
    public void TrustedProxyConfigurationAcceptsOnlyExplicitIpAddresses()
    {
        Assert.Equal(
            new[] { IPAddress.Parse("127.0.0.1"), IPAddress.Parse("10.0.0.2") },
            PrivateAlphaAccessOptions.ParseTrustedProxyAddresses("127.0.0.1, 10.0.0.2"));
        Assert.Empty(PrivateAlphaAccessOptions.ParseTrustedProxyAddresses(null));
        Assert.Throws<InvalidOperationException>(() =>
            PrivateAlphaAccessOptions.ParseTrustedProxyAddresses("*"));
    }

    private static PrivateAlphaAccessOptions CreateOptions(
        string? mode = null,
        bool isDevelopment = true,
        string? username = null,
        string? password = null,
        string? tlsMode = null,
        string? render = null,
        string? renderServiceType = null,
        string? renderExternalUrl = null,
        string? trustedProxyIps = null)
    {
        var values = new Dictionary<string, string?>();
        if (mode is not null)
        {
            values["PRIVATE_ALPHA_ACCESS_MODE"] = mode;
        }

        if (username is not null)
        {
            values["PRIVATE_ALPHA_USERNAME"] = username;
        }

        if (password is not null)
        {
            values["PRIVATE_ALPHA_PASSWORD"] = password;
        }

        if (tlsMode is not null)
        {
            values["PRIVATE_ALPHA_TLS_MODE"] = tlsMode;
        }

        if (render is not null)
        {
            values["RENDER"] = render;
        }

        if (renderServiceType is not null)
        {
            values["RENDER_SERVICE_TYPE"] = renderServiceType;
        }

        if (renderExternalUrl is not null)
        {
            values["RENDER_EXTERNAL_URL"] = renderExternalUrl;
        }

        if (trustedProxyIps is not null)
        {
            values["PRIVATE_ALPHA_TRUSTED_PROXY_IPS"] = trustedProxyIps;
        }

        var configuration = new ConfigurationBuilder().AddInMemoryCollection(values).Build();
        return PrivateAlphaAccessOptions.FromConfiguration(configuration, isDevelopment);
    }

    private static PrivateAlphaAccessOptions CreateRenderFreeOptions() => CreateOptions(
        "private",
        isDevelopment: false,
        TestUsername,
        TestPassword,
        tlsMode: "render-free-edge",
        render: "true",
        renderServiceType: "web",
        renderExternalUrl: "https://refugee-navigator.onrender.com");

    private static string CreateAuthorization(string username, string password) =>
        "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

    private static async Task<MiddlewareResponse> InvokeMiddlewareAsync(
        PrivateAlphaAccessOptions options,
        string path,
        string? authorization,
        bool isHttps = false,
        bool applyRenderFreeSchemeMiddleware = false,
        string? forwardedProto = null)
    {
        var context = new DefaultHttpContext();
        context.Request.Path = path.Split('?')[0];
        context.Request.Scheme = isHttps ? "https" : "http";
        context.Request.Headers.Authorization = authorization;
        if (forwardedProto is not null)
        {
            context.Request.Headers["X-Forwarded-Proto"] = forwardedProto;
        }
        await using var body = new MemoryStream();
        context.Response.Body = body;
        var nextWasCalled = false;
        var middleware = new PrivateAlphaAuthenticationMiddleware(
            _ =>
            {
                nextWasCalled = true;
                return context.Response.WriteAsync("protected page content");
            },
            options);

        if (applyRenderFreeSchemeMiddleware)
        {
            await new RenderFreeEdgeHttpsMiddleware(middleware.InvokeAsync, options).InvokeAsync(context);
        }
        else
        {
            await middleware.InvokeAsync(context);
        }
        body.Position = 0;
        using var reader = new StreamReader(body);
        var bodyText = await reader.ReadToEndAsync();

        return new MiddlewareResponse(
            context.Response.StatusCode,
            bodyText,
            context.Response.Headers.WWWAuthenticate.ToString() is { Length: > 0 } challenge ? challenge : null,
            context.Response.Headers["X-Robots-Tag"].ToString() is { Length: > 0 } robots ? robots : null,
            nextWasCalled,
            context.Request.Scheme);
    }

    private sealed record MiddlewareResponse(
        int StatusCode,
        string Body,
        string? WwwAuthenticate,
        string? RobotsTag,
        bool NextWasCalled,
        string RequestScheme);
}
