using System.Globalization;

namespace RefugeeNavigator.Web.Localization;

public static class SupportedCultures
{
    public static IReadOnlyList<CultureInfo> All { get; } = Array.AsReadOnly(new[]
    {
        new CultureInfo("uk"),
        new CultureInfo("en"),
        new CultureInfo("ru")
    });
}
