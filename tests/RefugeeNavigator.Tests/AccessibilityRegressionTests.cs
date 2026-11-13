using System.Text.RegularExpressions;

namespace RefugeeNavigator.Tests;

public sealed class AccessibilityRegressionTests
{
    [Theory]
    [InlineData("Search.razor.css", "input,\\s*select", "input:focus-visible")]
    [InlineData("Compare.razor.css", "select", "select:focus-visible")]
    public void FormControlBoundaryHasAtLeastThreeToOneContrastAndFocusRemainsVisible(
        string fileName,
        string controlSelector,
        string focusSelector)
    {
        var cssPath = Path.Combine(AppContext.BaseDirectory, "AccessibilityStyles", fileName);
        var css = File.ReadAllText(cssPath);
        var selector = new Regex($@"(?:^|\n)\s*{controlSelector}\s*\{{(?<body>[^}}]*)\}}", RegexOptions.Singleline);
        var rule = selector.Match(css);

        Assert.True(rule.Success, $"Could not find the form-control rule in {fileName}.");
        var border = GetDeclaration(rule.Groups["body"].Value, "border");
        var borderMatch = Regex.Match(border, @"^(?<width>\d+(?:\.\d+)?px)\s+solid\s+(?<color>var\(--color-control-border\))$");
        Assert.True(borderMatch.Success, $"Expected a solid pixel control border, found '{border}'.");
        Assert.Equal("1px", borderMatch.Groups["width"].Value);

        var background = GetDeclaration(rule.Groups["body"].Value, "background");
        Assert.Equal("var(--color-surface)", background);

        var themeCssPath = Path.GetFullPath(Path.Combine(
            AppContext.BaseDirectory,
            "..", "..", "..", "..", "..",
            "src", "RefugeeNavigator.Web", "wwwroot", "app.css"));
        var themeCss = File.ReadAllText(themeCssPath);
        AssertThemeControlContrast(themeCss, ":root", "Light");
        AssertThemeControlContrast(themeCss, ":root[data-theme=\"dark\"]", "Dark");

        var focusRule = Regex.Match(css, $@"{Regex.Escape(focusSelector)}\s*,?[^{{]*\{{(?<body>[^}}]*)\}}", RegexOptions.Singleline);
        Assert.True(focusRule.Success, $"The focus-visible rule for {focusSelector} is missing.");
        Assert.Contains("outline: 3px solid var(--color-primary)", focusRule.Groups["body"].Value, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("outline-offset: 3px", focusRule.Groups["body"].Value, StringComparison.OrdinalIgnoreCase);
    }

    private static void AssertThemeControlContrast(string css, string selector, string themeName)
    {
        var themeRule = Regex.Match(css, $@"{Regex.Escape(selector)}\s*\{{(?<body>[^}}]*)\}}");
        Assert.True(themeRule.Success, $"Could not find the {themeName} theme tokens.");

        var borderColor = GetDeclaration(themeRule.Groups["body"].Value, "--color-control-border");
        var surfaceColor = GetDeclaration(themeRule.Groups["body"].Value, "--color-surface");
        Assert.Matches(@"^#(?:[0-9a-fA-F]{3}|[0-9a-fA-F]{6})$", borderColor);
        Assert.Matches(@"^#(?:[0-9a-fA-F]{3}|[0-9a-fA-F]{6})$", surfaceColor);

        var contrast = ContrastRatio(borderColor, surfaceColor);
        Assert.True(contrast >= 3.0, $"{themeName} control boundary contrast is {contrast:F2}:1; required at least 3:1.");
    }

    private static string GetDeclaration(string declarations, string property)
    {
        var match = Regex.Match(declarations, $@"(?:^|;)\s*{Regex.Escape(property)}\s*:\s*(?<value>[^;]+)");
        Assert.True(match.Success, $"The {property} declaration is missing.");
        return match.Groups["value"].Value.Trim();
    }

    private static double ContrastRatio(string first, string second)
    {
        var firstLuminance = RelativeLuminance(first);
        var secondLuminance = RelativeLuminance(second);
        var lighter = Math.Max(firstLuminance, secondLuminance);
        var darker = Math.Min(firstLuminance, secondLuminance);
        return (lighter + 0.05) / (darker + 0.05);
    }

    private static double RelativeLuminance(string color)
    {
        var expandedColor = color.Length == 4
            ? $"#{color[1]}{color[1]}{color[2]}{color[2]}{color[3]}{color[3]}"
            : color;
        var red = Convert.ToInt32(expandedColor.Substring(1, 2), 16) / 255d;
        var green = Convert.ToInt32(expandedColor.Substring(3, 2), 16) / 255d;
        var blue = Convert.ToInt32(expandedColor.Substring(5, 2), 16) / 255d;

        static double Linearize(double channel) => channel <= 0.04045
            ? channel / 12.92
            : Math.Pow((channel + 0.055) / 1.055, 2.4);

        return 0.2126 * Linearize(red) + 0.7152 * Linearize(green) + 0.0722 * Linearize(blue);
    }
}
