# Actual Live Country Scope Audit — 26 October 2026

## Scope and method

This audit evaluates the production catalogs and rendering rules at the deployed SHA `623fbb39a936298d45d1d6ae5f70d7c1501cdabb`, identified by the owner from Render. The inspected files were `countries.json`, `information-entries.json`, `comparison-facts.json`, and the deployed `Home.razor`, `CountryOverview.razor`, `Category.razor`, `Search.razor`, `Compare.razor`, `ArticleSearch`, and `CountryComparisonTopicMap`.

The production source and catalog were read directly from that commit. Anonymous requests to `/`, the country routes, Search, and Compare returned HTTP 401, as expected for the authenticated beta. No authenticated credentials were available in this audit, so the rendered authenticated HTML was not independently inspected. Route and page behavior below is therefore derived from the exact deployed renderer/catalog, not claimed as an authenticated browser observation.

## Country visibility and accepted content

`Home.razor` renders every record in `Catalog.Countries` without a release-scope filter. The deployed catalog contains seven countries, so seven cards are rendered on Home. `CountryOverview.razor` accepts every catalog slug and renders all six category cards without checking whether that country has accepted articles.

| Country | Home card / country route / six category cards | Category articles rendered in EN / UK / RU | Searchable accepted articles EN / UK / RU | Approved comparison facts | Approved facts available / unavailable |
| --- | --- | ---: | ---: | ---: | ---: |
| Denmark | Yes | 8 / 8 / 8 | 8 / 8 / 8 | 24 | 23 / 1 |
| Germany | Yes | 8 / 8 / 8 | 8 / 8 / 8 | 24 | 23 / 1 |
| Poland | Yes | 8 / 8 / 8 | 8 / 8 / 8 | 24 | 24 / 0 |
| Czechia | Yes | 8 / 8 / 8 | 8 / 8 / 8 | 24 | 24 / 0 |
| Spain | Yes | 8 / 8 / 8 | 8 / 8 / 8 | 24 | 21 / 3 |
| Sweden | Yes | 0 / 0 / 0 | 0 / 0 / 0 | 0 approved (24 Draft) | No accepted facts |
| Netherlands | Yes | 7 / 7 / 7 | 7 / 7 / 7 | 20 (4 Draft) | 20 / 0 |

“Articles rendered” and “searchable” are counts per language. Production Category filters out translations whose verification status is not `Verified`; Search explicitly requests `Verified` results. Therefore the category and search counts match for the deployed catalog. The 24 accepted translations in a fully accepted country mean eight articles in each of EN, UK, and RU.

## Czechia and Spain

Czechia and Spain each have eight `Verified` articles in each language (24 translations total) and 24 `Approved` comparison facts. Their country pages, six category cards, Category articles, Search results, and Compare selector entries are enabled by the deployed code. Spain has three approved facts marked unavailable; these render as unavailable answers rather than accepted factual answers. Czechia has no unavailable approved facts.

Both relevant approval/publish commits are ancestors of the deployed SHA:

- Czechia approval `0d2e01f4ea4682c2371d16bf6f532de620909293` — confirmed ancestor.
- Spain publish `ddf9fc5797e400e23e9fc071b6287b37d3a46bd1` — confirmed ancestor.

## Sweden user experience

Sweden is visible on Home, its `/countries/sweden` route is valid for the deployed country catalog, and its country page offers all six category cards despite having zero `Verified` translations (24 are `NeedsReview`) and zero `Approved` comparison facts (all 24 are `Draft`). Its Category routes consequently render no article cards, and Search has no accepted Sweden articles to return.

Sweden is also present in the comparison-country mapping and selector. When compared with a country that has accepted facts, the structured comparison can show Sweden’s answers as unavailable, while the full-article comparison has no verified Sweden article to display. Thus Sweden is presented as an available country with empty/unaccepted content. This can mislead users about whether the country has information and risks users interpreting “information unavailable” as a substantive absence.

## Comparison behavior across countries

The Compare selector includes all seven mapped countries. Approved facts are candidates for production rendering; unavailable approved facts are rendered as unavailable responses. Draft facts are not rendered as accepted answers in production. A country with no approved facts, such as Sweden, remains selectable; the structured comparison is shown when the selected pair has approved facts, and Sweden’s own Draft answers are rendered through the unavailable/evidence-gap path. The full-article panels only render verified article translations and their sources.

## Difference from the release record and tester pack

`docs/releases/FOUR_COUNTRY_PRIVATE_BETA_2026-10-26.md` documents Denmark, Germany, Poland, and the accepted Netherlands set as the release scope. The beta testing pack was refreshed in Phase 18.10.1 to describe the same four-country scope. Neither matches the deployed UI, which exposes seven country cards and seven Compare selector choices. Czechia and Spain each have a fully accepted 24-translation / 24-fact set; Sweden is exposed but has no accepted content.

The accepted-content count is six countries: Denmark, Germany, Poland, Czechia, Spain, and the accepted Netherlands subset. The visible UI scope is seven because Sweden is not filtered out.

## Recommendation

Before the next tester session, do both of the following:

1. Hide Sweden from Home, country routes/category navigation, Search filters, and Compare selectors until it has accepted content. Hiding only the Home card would leave direct routes and Compare exposure intact.
2. After the UI scope is corrected, update the release record and tester pack to describe the actual six-country accepted scope: Denmark, Germany, Poland, Czechia, Spain, and Netherlands. State that Netherlands coverage varies by topic and unavailable answers indicate an evidence gap.

Do not describe the current live UI as a six-country interface until Sweden is suppressed. This report makes no application, catalog, status, release-record, or tester-pack changes.

## Verdict

The deployed application exposes **7 countries**; **6 have accepted production content**. Czechia and Spain are fully available. Sweden is visible and selectable despite having no accepted articles or comparison facts. The current four-country release documentation and tester pack do not match the deployed UI.
