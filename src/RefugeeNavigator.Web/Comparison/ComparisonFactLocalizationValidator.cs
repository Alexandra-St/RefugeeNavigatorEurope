using System.Globalization;

namespace RefugeeNavigator.Web.Comparison;

public static class ComparisonFactLocalizationValidator
{
    private static readonly CultureInfo[] Cultures =
    [
        CultureInfo.GetCultureInfo("uk"),
        CultureInfo.GetCultureInfo("en"),
        CultureInfo.GetCultureInfo("ru")
    ];

    private static readonly string[] InterfaceKeys =
    [
        "Compare.StructuredHeading", "Compare.DraftPreviewHeading", "Compare.DraftPreviewNotice",
        "Compare.DraftStatus", "Compare.ApprovedEditorialStatus", "Compare.FactSources", "Compare.NotEstablishedInReviewedSources"
    ];

    public static void Validate(ComparisonFactCatalog catalog, Func<string, CultureInfo, string?> getString)
    {
        ArgumentNullException.ThrowIfNull(catalog);
        ArgumentNullException.ThrowIfNull(getString);
        var keys = InterfaceKeys.Concat(catalog.Topics.SelectMany(topic => topic.Aspects).Select(aspect => aspect.QuestionResourceKey)).Distinct(StringComparer.Ordinal);
        var errors = new List<string>();
        foreach (var key in keys)
        foreach (var culture in Cultures)
        {
            var value = getString(key, culture);
            if (string.IsNullOrWhiteSpace(value) || string.Equals(value, key, StringComparison.Ordinal))
            {
                errors.Add($"Missing '{culture.TwoLetterISOLanguageName}' localization for '{key}'.");
            }
        }

        if (errors.Count > 0)
        {
            throw new InvalidDataException($"Comparison fact localization is invalid: {string.Join(" ", errors)}");
        }
    }
}
