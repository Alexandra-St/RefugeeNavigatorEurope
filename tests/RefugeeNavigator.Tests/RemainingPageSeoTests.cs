using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using RefugeeNavigator.Data;
using RefugeeNavigator.Web.Comparison;
using RefugeeNavigator.Web.Components;
using RefugeeNavigator.Web.Components.Pages;
using RefugeeNavigator.Web.Contact;
using RefugeeNavigator.Web.Guides;
using RefugeeNavigator.Web.Localization;
using RefugeeNavigator.Web.Seo;
using GuidePage = RefugeeNavigator.Web.Components.Pages.StepByStepGuide;

namespace RefugeeNavigator.Tests;

public sealed class RemainingPageSeoTests(SeoHttpFixture http) : IClassFixture<SeoHttpFixture>
{
    private const string Origin = "https://example.test";
    private static readonly string[] Languages = ["en", "uk", "ru"];
    private static readonly ResourceManager Resources = new(
        "RefugeeNavigator.Web.Resources.Localization.SharedResource", typeof(SharedResource).Assembly);
    private static readonly Lazy<Task<CatalogData>> Catalog = new(
        () => new JsonCatalogProvider(AppContext.BaseDirectory).LoadAsync());

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task Static_pages_render_one_localized_head_and_reciprocal_alternates(string language)
    {
        var cases = new (Type Page, string Path, string Resource, string DescriptionKey)[]
        {
            (typeof(Home), "/", "Home", "IntroductionDescription"),
            (typeof(Contact), "/contact", "Contact", "MetaDescription"),
            (typeof(Privacy), "/privacy", "Privacy", "MetaDescription"),
            (typeof(Disclaimer), "/disclaimer", "Disclaimer", "MetaDescription"),
            (typeof(Search), "/search", "Search", "Description"),
            (typeof(Compare), "/compare", "Compare", "Description"),
            (typeof(About), "/about", "About", "MetaDescription")
        };

        foreach (var (page, path, resource, description) in cases)
        {
            var head = await Render(page, language, path + "?utm_source=test");
            AssertHead(head, language, path, Localize(resource + ".PageTitle", language),
                Localize(resource + "." + description, language), CanonicalOrigin(page));
        }
    }

    [Theory]
    [InlineData("en")]
    [InlineData("uk")]
    [InlineData("ru")]
    public async Task Catalog_pages_use_resolved_canonical_slugs_and_localized_metadata(string language)
    {
        var country = new Dictionary<string, object?> { ["CountrySlug"] = "germany" };
        var category = new Dictionary<string, object?>(country) { ["CategorySlug"] = "temporary-protection" };
        var guide = new Dictionary<string, object?>(country) { ["GuideSlug"] = "temporary-protection" };
        foreach (var (page, path, parameters) in new[]
        {
            (typeof(CountryOverview), "/countries/germany", country),
            (typeof(Category), "/countries/germany/categories/temporary-protection", category),
            (typeof(GuidePage), "/guides/germany/temporary-protection", guide)
        })
        {
            var head = await Render(page, language, path + "?utm_source=test", parameters);
            var title = ReadTag(head, "title");
            var description = ReadMeta(head, "name", "description");
            Assert.False(string.IsNullOrWhiteSpace(title));
            Assert.False(string.IsNullOrWhiteSpace(description));
            AssertHead(head, language, path, title, description, CanonicalOrigin(page));
            var otherLanguage = language == "en" ? "uk" : "en";
            var otherHead = await Render(page, otherLanguage, path, parameters);
            Assert.NotEqual(title, ReadTag(otherHead, "title"));
            Assert.NotEqual(description, ReadMeta(otherHead, "name", "description"));
        }
    }

    [Theory]
    [InlineData("/search?q=permit", typeof(Search))]
    [InlineData("/search?country=germany", typeof(Search))]
    [InlineData("/compare?country1=germany&country2=france", typeof(Compare))]
    public async Task Functional_query_variants_do_not_inherit_base_page_metadata(string path, Type page)
    {
        var head = await Render(page, "en", path);
        Assert.DoesNotContain("rel=\"canonical\"", head);
        Assert.DoesNotContain("hreflang=", head);
        Assert.DoesNotContain("property=\"og:url\"", head);
        Assert.Equal("noindex, follow", ReadMeta(head, "name", "robots"));
        Assert.DoesNotContain("name=\"description\"", head);
        Assert.DoesNotContain("property=\"og:", head);
        Assert.Equal(Localize(page == typeof(Search) ? "Search.PageTitle" : "Compare.PageTitle", "en"),
            ReadTag(head, "title"));
    }

    private static void AssertHead(string head, string language, string path, string title, string description, string origin)
    {
        Assert.Equal(title, ReadTag(head, "title"));
        Assert.Equal(description, ReadMeta(head, "name", "description"));
        var canonical = origin + LocalizedUrl.ForCurrentCulture(path, language);
        var link = Assert.Single(Regex.Matches(head, "<link rel=\"canonical\" href=\"([^\"]+)\""));
        Assert.Equal(canonical, link.Groups[1].Value);
        Assert.Equal(title, ReadMeta(head, "property", "og:title"));
        Assert.Equal(description, ReadMeta(head, "property", "og:description"));
        Assert.Equal(canonical, ReadMeta(head, "property", "og:url"));
        Assert.Equal(3, Regex.Matches(head, "hreflang=").Count);
        foreach (var alternate in Languages)
        {
            Assert.Contains($"hreflang=\"{alternate}\" href=\"{origin}{LocalizedUrl.ForCurrentCulture(path, alternate)}\"", head);
        }
    }

    private static string ReadTag(string head, string tag) =>
        WebUtility.HtmlDecode(Assert.Single(Regex.Matches(head, $"<{tag}>(.*?)</{tag}>")).Groups[1].Value);

    private static string ReadMeta(string head, string attribute, string value) =>
        WebUtility.HtmlDecode(Assert.Single(Regex.Matches(head,
            $"<meta {attribute}=\"{Regex.Escape(value)}\" content=\"([^\"]*)\"")).Groups[1].Value);

    private static string Localize(string key, string language) =>
        Resources.GetString(key, CultureInfo.GetCultureInfo(language))!;

    private static bool RequiresHttp(Type page) => page == typeof(Contact) || page == typeof(GuidePage);

    private static string CanonicalOrigin(Type page) =>
        RequiresHttp(page) ? SeoMetadataGenerator.DefaultCanonicalOrigin : Origin;

    private async Task<string> RenderInteractivePageOverHttp(string language, string path)
    {
        using var client = new HttpClient(new HttpClientHandler { AllowAutoRedirect = false, UseProxy = false });
        using var request = new HttpRequestMessage(HttpMethod.Get,
            new Uri(http.Origin, LocalizedUrl.ForCurrentCulture(path, language)));
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(Encoding.UTF8.GetBytes("rne-smoke:" + http.Password)));
        using var response = await client.SendAsync(request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var html = await response.Content.ReadAsStringAsync();
        var head = Regex.Match(html, "<head(?:\\s[^>]*)?>(.*?)</head>", RegexOptions.Singleline);
        Assert.True(head.Success, "The server response must contain a document head.");
        return head.Groups[1].Value;
    }

    private async Task<string> Render(Type component, string language, string path,
        Dictionary<string, object?>? parameters = null)
    {
        if (RequiresHttp(component))
        {
            return await RenderInteractivePageOverHttp(language, path);
        }

        var previous = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(language);
        try
        {
            var catalog = await Catalog.Value;
            var data = Path.Combine(AppContext.BaseDirectory, "Data");
            var guides = await new JsonStepByStepGuideProvider(Path.Combine(data, "step-by-step-guides.json")).LoadAsync(catalog);
            var facts = await new JsonComparisonFactProvider(Path.Combine(data, "comparison-facts.json")).LoadAsync(catalog);
            var availability = new SeoTranslationAvailability(catalog, guides);
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
            services.AddHttpClient<ContactSubmissionService>();
            services.AddSingleton(catalog);
            services.AddSingleton(guides);
            services.AddSingleton(facts);
            services.AddSingleton<IWebHostEnvironment>(new TestEnvironment());
            services.AddSingleton<NavigationManager>(new TestNavigation(LocalizedUrl.ForCurrentCulture(path, language)));
            services.AddSingleton<IJSRuntime, TestJsRuntime>();
            services.AddSingleton(availability);
            services.AddSingleton(new SeoMetadataGenerator(Origin, availability));

            using var provider = services.BuildServiceProvider();
            await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());
            return await renderer.Dispatcher.InvokeAsync(async () =>
            {
                await renderer.RenderComponentAsync(component, parameters is null
                    ? ParameterView.Empty : ParameterView.FromDictionary(parameters));
                var output = await renderer.RenderComponentAsync<HeadOutlet>(ParameterView.Empty);
                return output.ToHtmlString();
            });
        }
        finally
        {
            CultureInfo.CurrentUICulture = previous;
        }
    }

    private sealed class TestNavigation : NavigationManager
    {
        public TestNavigation(string path) => Initialize(Origin + "/", Origin + path);
        protected override void NavigateToCore(string uri, bool forceLoad) { }
    }

    private sealed class TestJsRuntime : IJSRuntime
    {
        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args) =>
            ValueTask.FromResult(default(TValue)!);
        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken token, object?[]? args) =>
            ValueTask.FromResult(default(TValue)!);
    }

    private sealed class TestEnvironment : IWebHostEnvironment
    {
        public string ApplicationName { get; set; } = "RefugeeNavigator.Tests";
        public string EnvironmentName { get; set; } = "Production";
        public string WebRootPath { get; set; } = AppContext.BaseDirectory;
        public string ContentRootPath { get; set; } = AppContext.BaseDirectory;
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }
}

public sealed class SeoHttpFixture : IAsyncLifetime
{
    private Process? _server;
    public Uri Origin { get; private set; } = null!;
    public string Password { get; private set; } = string.Empty;

    public async Task InitializeAsync()
    {
        if (Environment.GetEnvironmentVariable("RNE_SEO_TEST_URL") is { } endpoint)
        {
            Origin = new Uri(endpoint);
            if (Origin.Scheme != "http" || Origin.Host != "127.0.0.1" || Origin.Port == 5088)
                throw new InvalidOperationException("The SEO test publication must use a fresh IPv4 loopback port.");
            Password = File.ReadAllText("/tmp/rne-4b.7Y8oQY/local-password").TrimEnd('\r', '\n');
            return;
        }

        Password = $"Rne-Test!{Guid.NewGuid():N}";
        var start = new ProcessStartInfo("dotnet")
        {
            WorkingDirectory = AppContext.BaseDirectory,
            RedirectStandardOutput = true,
            UseShellExecute = false
        };
        start.ArgumentList.Add(Path.Combine(AppContext.BaseDirectory, "RefugeeNavigator.Web.dll"));
        start.ArgumentList.Add("--urls");
        start.ArgumentList.Add("http://127.0.0.1:0");
        start.Environment["ASPNETCORE_ENVIRONMENT"] = "Development";
        start.Environment["DOTNET_ENVIRONMENT"] = "Development";
        start.Environment["PRIVATE_ALPHA_ACCESS_MODE"] = "private";
        start.Environment["PRIVATE_ALPHA_TLS_MODE"] = "strict";
        start.Environment["PRIVATE_ALPHA_USERNAME"] = "rne-smoke";
        start.Environment["PRIVATE_ALPHA_PASSWORD"] = Password;
        _server = Process.Start(start) ?? throw new InvalidOperationException("Could not start the SEO test server.");
        var output = new StringBuilder();
        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        try
        {
            while (await _server.StandardOutput.ReadLineAsync(timeout.Token) is { } line)
            {
                output.AppendLine(line);
                if (Regex.Match(line, @"Now listening on: (http://127\.0\.0\.1:\d+)") is { Success: true } match)
                {
                    Origin = new Uri(match.Groups[1].Value);
                    _ = _server.StandardOutput.ReadToEndAsync();
                    return;
                }
            }
            throw new InvalidOperationException("SEO test server exited before listening. " + output);
        }
        catch
        {
            await DisposeAsync();
            throw;
        }
    }

    public async Task DisposeAsync()
    {
        if (_server is not { } server) return;
        _server = null;
        using (server)
        {
            if (!server.HasExited) server.Kill(entireProcessTree: true);
            await server.WaitForExitAsync();
        }
    }
}
