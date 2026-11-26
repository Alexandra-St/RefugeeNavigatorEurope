# Localized page metadata

Public page metadata uses the canonical HTTPS origin and explicit EN, UK and RU URL prefixes. Pages with available translations advertise reciprocal alternates. There is no `x-default` because the project has no designated language-neutral or fallback destination for these alternate sets.

The base Search and Compare pages retain their localized canonical metadata and current indexability. Search parameters `q`, `country` and `category`, and Compare parameters `country1`, `country2` and `category`, identify functional variants. Those variants retain their translated page title and emit `noindex, follow`. They have no canonical, hreflang, base-page description or Open Graph metadata and are ineligible for a future sitemap. Tracking parameters alone do not create a functional variant and are omitted from canonical URLs.

Private-alpha responses retain their existing `X-Robots-Tag: noindex, nofollow`. The restrictive private-alpha directives apply even when the page requests `follow`.

## Sitemap, crawl rules and error pages

Public mode exposes the canonical sitemap and advertises it in `robots.txt`. The sitemap includes base Search and Compare, the other canonical static pages, and the production catalog destinations with verified SEO availability. Its localized sets match each page's hreflang. Article `lastmod` comes from the translation's catalog update date. Other routes omit it. Public robots permits crawling filtered pages so crawlers can see their `noindex` directive.

Private mode keeps the authentication gate on both endpoints. Authenticated robots disallows all crawling and the sitemap returns 404. The private `X-Robots-Tag` remains in effect. Development without an explicit public mode also does not advertise a sitemap.

The explicit error routes return 404 and error pages do not advertise canonical URLs or alternates. Existing country/category pairs without published translations can still show the empty category UI. They are excluded from the sitemap. These localized routes remain HTTP 200 and emit `noindex, follow` without canonical metadata, hreflang or JSON-LD. Unknown countries and categories remain HTTP 404.

## Structured data

Eligible pages in public mode emit one Schema.org graph in the document head. The localized canonical URL identifies each page and anchors related entity IDs. Home links a WebSite entity, countries and categories use CollectionPage, About and Contact use their corresponding page subtypes, and eligible standalone articles link an Article entity. Breadcrumbs follow the localized home, country and category navigation where applicable.

Article `dateModified` is the translation's actual catalog update timestamp. The graph does not infer a publication date, author, publisher, image or organization from referenced sources. Filtered, empty, unavailable and invalid pages do not emit JSON-LD. Private and development access modes do not emit the public graph.
