using System.Globalization;
using System.Text.Json;
using RefugeeNavigator.Web.Localization;

namespace RefugeeNavigator.Web.Seo;

public enum StructuredPageKind { WebPage, Home, CollectionPage, Article, AboutPage, ContactPage }

public sealed record SeoBreadcrumb(string Name, string Path);

public static class SeoStructuredData
{
    public static string Serialize(SeoMetadata metadata, SeoMetadataGenerator urls, StructuredPageKind kind,
        string language, string? name = null, IReadOnlyList<SeoBreadcrumb>? parents = null,
        DateTimeOffset? dateModified = null)
    {
        var url = metadata.CanonicalUrl;
        var pageId = url + "#webpage";
        var page = Entity(kind switch
        {
            StructuredPageKind.Article or StructuredPageKind.Home => "WebPage",
            _ => kind.ToString()
        }, pageId);
        page["url"] = url;
        page["name"] = name ?? metadata.Title;
        page["description"] = metadata.Description;
        page["inLanguage"] = language;

        var graph = new List<Dictionary<string, object>> { page };
        if (kind == StructuredPageKind.Home)
        {
            var websiteId = url + "#website";
            var website = Entity("WebSite", websiteId);
            website["name"] = "Refugee Navigator Europe";
            website["url"] = url;
            website["inLanguage"] = language;
            page["isPartOf"] = Reference(websiteId);
            graph.Add(website);
        }
        if (kind == StructuredPageKind.Article)
        {
            var articleId = url + "#article";
            var article = Entity("Article", articleId);
            article["headline"] = metadata.Title;
            article["description"] = metadata.Description;
            article["url"] = url;
            article["inLanguage"] = language;
            article["mainEntityOfPage"] = Reference(pageId);
            if (dateModified is { } updated && updated != default)
                article["dateModified"] = updated.ToString("O", CultureInfo.InvariantCulture);
            page["mainEntity"] = Reference(articleId);
            graph.Add(article);
        }
        if (parents is { Count: > 0 })
        {
            var breadcrumbId = url + "#breadcrumb";
            var breadcrumb = Entity("BreadcrumbList", breadcrumbId);
            var locations = parents.Select(parent => (parent.Name,
                    Url: urls.BuildAbsoluteUrl(LocalizedUrl.ForCurrentCulture(parent.Path, language))))
                .Append((Name: name ?? metadata.Title, Url: url));
            breadcrumb["itemListElement"] = locations.Select((location, index) => new Dictionary<string, object>
            {
                ["@type"] = "ListItem",
                ["position"] = index + 1,
                ["name"] = location.Name,
                ["item"] = location.Url
            }).ToArray();
            page["breadcrumb"] = Reference(breadcrumbId);
            graph.Add(breadcrumb);
        }

        // The default encoder escapes HTML-sensitive characters, including script termination.
        return JsonSerializer.Serialize(new Dictionary<string, object>
        {
            ["@context"] = "https://schema.org",
            ["@graph"] = graph
        });
    }

    private static Dictionary<string, object> Entity(string type, string id) =>
        new() { ["@type"] = type, ["@id"] = id };

    private static Dictionary<string, object> Reference(string id) => new() { ["@id"] = id };
}
