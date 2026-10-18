using System.Globalization;

namespace RefugeeNavigator.Web.Guides;

public static class StepByStepGuideLocalizationValidator
{
    private static readonly CultureInfo[] Cultures =
    [
        CultureInfo.GetCultureInfo("uk"),
        CultureInfo.GetCultureInfo("en"),
        CultureInfo.GetCultureInfo("ru")
    ];

    private static readonly string[] InterfaceKeys =
    [
        "Guide.DraftPreviewNotice", "Guide.SelectSituation", "Guide.Back", "Guide.Next",
        "Guide.Previous", "Guide.ChangeSituation", "Guide.ReadArticle", "Guide.Progress", "Guide.ProgressNotice",
        "Guide.Documents", "Guide.Authority", "Guide.Warning", "Guide.Required",
        "Guide.Recommendation", "Guide.Optional", "Guide.Unavailable", "Guide.Step"
    ];

    public static void Validate(StepByStepGuideCatalog catalog, Func<string, CultureInfo, string?> getString)
    {
        ArgumentNullException.ThrowIfNull(catalog);
        ArgumentNullException.ThrowIfNull(getString);
        var errors = new List<string>();
        foreach (var key in InterfaceKeys)
        foreach (var culture in Cultures)
        {
            var value = getString(key, culture);
            if (string.IsNullOrWhiteSpace(value) || string.Equals(value, key, StringComparison.Ordinal))
                errors.Add($"Missing '{culture.TwoLetterISOLanguageName}' localization for '{key}'.");
        }

        if (errors.Count > 0)
            throw new InvalidDataException($"Step-by-step guide localization is invalid: {string.Join(" ", errors)}");
    }
}
