using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        string? password = null)
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

        var configuration = new ConfigurationBuilder().AddInMemoryCollection(values).Build();
        return PrivateAlphaAccessOptions.FromConfiguration(configuration, isDevelopment);
    }

    private static string CreateAuthorization(string username, string password) =>
        "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

    private static async Task<MiddlewareResponse> InvokeMiddlewareAsync(
        PrivateAlphaAccessOptions options,
        string path,
        string? authorization,
        bool isHttps = false)
    {
        var context = new DefaultHttpContext();
        context.Request.Path = path.Split('?')[0];
        context.Request.Scheme = isHttps ? "https" : "http";
        context.Request.Headers.Authorization = authorization;
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

        await middleware.InvokeAsync(context);
        body.Position = 0;
        using var reader = new StreamReader(body);
        var bodyText = await reader.ReadToEndAsync();

        return new MiddlewareResponse(
            context.Response.StatusCode,
            bodyText,
            context.Response.Headers.WWWAuthenticate.ToString() is { Length: > 0 } challenge ? challenge : null,
            context.Response.Headers["X-Robots-Tag"].ToString() is { Length: > 0 } robots ? robots : null,
            nextWasCalled);
    }

    private sealed record MiddlewareResponse(
        int StatusCode,
        string Body,
        string? WwwAuthenticate,
        string? RobotsTag,
        bool NextWasCalled);
}
