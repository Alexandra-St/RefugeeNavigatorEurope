using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;

namespace RefugeeNavigator.Tests;

internal static class LocalizedRouteAssertions
{
    public static void HasRouteSet(Type component, string legacyRoute)
    {
        var expected = new[] { legacyRoute, "/en" + legacyRoute, "/uk" + legacyRoute, "/ru" + legacyRoute };
        var actual = component.GetCustomAttributes<RouteAttribute>().Select(attribute => attribute.Template);
        Assert.Equal(expected.Order(StringComparer.Ordinal), actual.Order(StringComparer.Ordinal));
    }

    public static void HasCategoryLinks(string html, string language, string country, IReadOnlyCollection<string> categories)
    {
        var expected = categories.Select(slug => $"/{language}/countries/{country}/categories/{slug}");
        var actual = Hrefs(html).Where(href => Regex.IsMatch(href,
            $@"/countries/{Regex.Escape(country)}/categories/"));
        Assert.Equal(expected.Order(StringComparer.Ordinal), actual.Order(StringComparer.Ordinal));
    }

    public static void HasNoGuideLinks(string html, string country) =>
        Assert.DoesNotContain(Hrefs(html), href =>
            Regex.IsMatch(href, $@"/guides/{Regex.Escape(country)}/"));

    private static IEnumerable<string> Hrefs(string html) =>
        Regex.Matches(html, """<a\b[^>]*?\shref="([^"]+)"[^>]*>""", RegexOptions.IgnoreCase)
            .Select(match => WebUtility.HtmlDecode(match.Groups[1].Value));
}
