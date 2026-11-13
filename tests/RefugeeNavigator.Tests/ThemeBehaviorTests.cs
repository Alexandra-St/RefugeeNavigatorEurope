using System.IO;

namespace RefugeeNavigator.Tests;

public sealed class ThemeBehaviorTests
{
    private static readonly string RepositoryRoot = FindRepositoryRoot();

    [Fact]
    public void HeaderContainsKeyboardAccessibleThemeToggle()
    {
        var header = File.ReadAllText(Path.Combine(RepositoryRoot, "src/RefugeeNavigator.Web/Components/SiteHeader.razor"));
        var css = File.ReadAllText(Path.Combine(RepositoryRoot, "src/RefugeeNavigator.Web/Components/SiteHeader.razor.css"));

        Assert.Contains("data-theme-toggle", header);
        Assert.Contains("aria-label=", header);
        Assert.Contains(":focus-visible", css);
    }

    [Fact]
    public void BootstrapReadsValidPreferenceFallsBackToSystemAndPersistsToggle()
    {
        var script = File.ReadAllText(Path.Combine(RepositoryRoot, "src/RefugeeNavigator.Web/wwwroot/theme.js"));

        Assert.Contains("localStorage.getItem(\"theme\")", script);
        Assert.Contains("localStorage.setItem(\"theme\", nextTheme)", script);
        Assert.Contains("value === \"light\" || value === \"dark\"", script);
        Assert.Contains("preference.matches ? \"dark\" : \"light\"", script);
        Assert.Contains("root.dataset.theme = theme", script);
        Assert.Contains("document.addEventListener(\"click\"", script);
    }

    [Fact]
    public void BootstrapRunsInDocumentHeadBeforeBlazorInteractiveScript()
    {
        var app = File.ReadAllText(Path.Combine(RepositoryRoot, "src/RefugeeNavigator.Web/Components/App.razor"));
        var earlyThemeScript = app.IndexOf("<script src=\"/theme.js\"></script>", StringComparison.Ordinal);
        var baseHref = app.IndexOf("<base href=\"/\" />", StringComparison.Ordinal);
        var blazorScript = app.IndexOf("_framework/blazor.web.js", StringComparison.Ordinal);

        Assert.True(earlyThemeScript >= 0, "The early theme script must use a root-relative URL.");
        Assert.True(earlyThemeScript < baseHref, "The early theme script must remain before the base element.");
        Assert.DoesNotContain("<script src=\"theme.js\"></script>", app[..baseHref]);
        Assert.True(earlyThemeScript < blazorScript, "Theme bootstrap must run before Blazor startup.");
        Assert.True(app.IndexOf("_framework/blazor.web.js", StringComparison.Ordinal) <
                    app.IndexOf("Blazor.addEventListener(\"enhancedload\"", StringComparison.Ordinal));
    }

    [Fact]
    public void ClickHandlerGuardsNonElementTargetsBeforeCallingClosest()
    {
        var script = File.ReadAllText(Path.Combine(RepositoryRoot, "src/RefugeeNavigator.Web/wwwroot/theme.js"));
        var guard = script.IndexOf("if (!(target instanceof Element)) return;", StringComparison.Ordinal);
        var closest = script.IndexOf("target.closest(\"[data-theme-toggle]\")", StringComparison.Ordinal);

        Assert.True(guard >= 0, "Click handler must return when event.target is not an Element.");
        Assert.True(closest > guard, "closest must only be called after the Element guard.");
        Assert.Contains("if (!target.closest(\"[data-theme-toggle]\")) return;", script);
    }

    [Fact]
    public void EnhancedNavigationResynchronizesToggleWithoutRebindingClickHandler()
    {
        var script = File.ReadAllText(Path.Combine(RepositoryRoot, "src/RefugeeNavigator.Web/wwwroot/theme.js"));

        var app = File.ReadAllText(Path.Combine(RepositoryRoot, "src/RefugeeNavigator.Web/Components/App.razor"));
        var enhancedLoad = app.IndexOf("Blazor.addEventListener(\"enhancedload\", () => {", StringComparison.Ordinal);
        var reapply = app.IndexOf("window.applyEffectiveTheme();", enhancedLoad, StringComparison.Ordinal);
        var sync = app.IndexOf("window.syncThemeToggle();", reapply, StringComparison.Ordinal);
        Assert.True(enhancedLoad >= 0, "Enhanced navigation must register its lifecycle handler.");
        Assert.True(reapply > enhancedLoad, "Enhanced navigation must reapply the effective theme.");
        Assert.True(sync > reapply, "The replacement toggle must sync after the root theme is reapplied.");
        Assert.DoesNotContain("document.addEventListener(\"enhancedload\"", script);
        Assert.Contains("const syncToggle = () =>", script);
        Assert.Contains("window.syncThemeToggle = syncToggle", script);
        Assert.Contains("const apply = () =>", script);
        Assert.Contains("window.applyEffectiveTheme = apply", script);
        var applyStart = script.IndexOf("const apply = () =>", StringComparison.Ordinal);
        var applyEnd = script.IndexOf("window.syncThemeToggle = syncToggle", applyStart, StringComparison.Ordinal);
        var applyBody = script[applyStart..applyEnd];
        Assert.Contains("localStorage.getItem(\"theme\")", applyBody);
        Assert.Contains("preference.matches ? \"dark\" : \"light\"", applyBody);
        Assert.Contains("root.dataset.theme = theme", applyBody);
        Assert.DoesNotContain("localStorage.setItem", applyBody);
        Assert.Contains("document.addEventListener(\"DOMContentLoaded\", () => {", script);
        Assert.Contains("syncToggle();", script);
        Assert.Contains("window.syncThemeToggle();", app);
        Assert.True(app.IndexOf("_framework/blazor.web.js", StringComparison.Ordinal) <
                    app.IndexOf("window.syncThemeToggle();", StringComparison.Ordinal));
        Assert.Equal(2, Count(app, "window.syncThemeToggle();"));
        Assert.Equal(1, Count(app, "window.applyEffectiveTheme();"));
        Assert.Contains("document.addEventListener(\"click\"", script);
        Assert.Equal(1, Count(script, "document.addEventListener(\"click\""));
        Assert.Equal(1, Count(app, "Blazor.addEventListener(\"enhancedload\""));
        Assert.Contains("if (!(target instanceof Element)) return;", script);
        Assert.Contains("localStorage.setItem(\"theme\", nextTheme)", script);
        Assert.Contains("const valid = value => value === \"light\" || value === \"dark\"", script);
    }

    private static int Count(string value, string fragment) =>
        value.Split(fragment, StringSplitOptions.None).Length - 1;

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "RefugeeNavigatorEurope.slnx")))
            directory = directory.Parent;
        return directory?.FullName ?? throw new DirectoryNotFoundException("Could not locate repository root.");
    }
}
