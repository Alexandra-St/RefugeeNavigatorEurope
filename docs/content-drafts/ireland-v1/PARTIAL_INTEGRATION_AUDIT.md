# Ireland v1 Partial Integration Audit

Baseline: `e169036c970c5682f3ff6c8adcbbcaab16a590ca`

Review date: 10 November 2026
Scope: partial catalog integration only.

## Summary

- BLOCKER: 0
- CORRECTION: 1
- POLISH: 0
- Catalog parity: PASS
- Source ownership: PASS
- Production hold: PASS (runtime visibility gates reviewed; direct category-route regression assertion is missing)
- Development preview: PASS
- Compare exclusion: PASS
- Search preview contract: CORRECTION REQUIRED
- Recommendation: **CORRECTION REQUIRED BEFORE PARTIAL INTEGRATION ACCEPTANCE**

## Findings

### IE-INT-01

- Severity: CORRECTION
- Area: Development search verification-status boundary
- Evidence: `Search.razor` returns a null `SearchStatusFilter` when Development has a selected country that is hidden in Production. `ArticleSearch.Search` only filters by verification status when that value parses to a status; null leaves translations of every status eligible. Ireland currently has 15 `NeedsReview` translations and no `Outdated` translations, so selecting Ireland returns its five articles as intended. The null status also permits any `Verified` or future `Outdated` translations in a manually selected hidden country. With no country selected, the filter remains `Verified`; Production also remains `Verified`.
- Impact: The current catalog does not leak Outdated Ireland content, but the preview route has a broader status boundary than the intended NeedsReview-only preview and could include future Outdated or Verified entries for a hidden country.
- Required correction: Narrow the hidden-country Development preview to `NeedsReview` and add a regression assertion for an Outdated translation. No correction was made in this audit.

## Catalog snapshot

| Measure | Audited result | Expected | Result |
|---|---:|---:|---|
| Countries | 14 | 14 | PASS |
| Ireland country ID / name / slug | `10000000-0000-0000-0000-000000000014` / Ireland / `ireland` | Exact match, no duplicate ID or slug | PASS |
| Ireland entries | 5 | 5 | PASS |
| Ireland translations | 15, all `NeedsReview` | 15 | PASS |
| Global entries | 109 | 109 | PASS |
| Global translations | 327 | 327 | PASS |
| Global translation statuses | 309 Verified / 18 NeedsReview / 0 Outdated | Same | PASS |
| Ireland official sources | 70; all IDs unique | 70 | PASS |
| Global official sources | 934 | 934 | PASS |

Ireland article and category counts match the drafts and integration expectations: employment 2, healthcare 1, education 1, social-assistance 1, temporary-protection 0, and housing 0. No Ireland placeholder entries exist for temporary protection, Residence Options, or housing.

All 15 runtime translations match the final draft title, summary, content, language, status, and `2026-11-10T00:00:00+00:00` timestamp. Editorial claim-coverage and exclusion notes are not present in runtime content. English, Ukrainian, and Russian names and prepositional forms match the specified localization values.

Each draft's source set matches its integrated source records: IE-EMP-01 9, IE-EMP-02 20, IE-HEALTH-01 10, IE-EDU-01 15, IE-SOC-01 16. Source IDs, article ownership, titles, URLs, and last-checked dates match the normalized drafts; URLs use HTTPS; `(InformationEntryId, URL)` pairs are unique; inline citations resolve to sources owned by the cited Ireland article. No cross-article Ireland source ownership remains.

## Visibility matrix

| Surface | Production | Development |
|---|---|---|
| Home | Ireland absent; 13 country cards | Ireland present; 14 country cards |
| Country overview | Direct Ireland route is NotFound | Ireland overview renders |
| Category pages | Shared Production country visibility gate rejects Ireland | Expected counts: 2 / 1 / 1 / 1 / 0 / 0 for employment / healthcare / education / social-assistance / temporary-protection / housing |
| Search selector | Ireland absent | Ireland present |
| Search results | `/search?country=ireland` has no result cards | Selected Ireland returns 5 results in EN/UK/RU |
| Compare selector | Ireland absent | Ireland absent |

The Production category route uses the same Production-visible-country gate as the overview and therefore rejects Ireland. The Ireland integration test directly asserts Production overview NotFound but does not render a direct Production Ireland category route; this remains a route-specific regression-test gap. No category route was observed to expose NeedsReview content.

## Comparison hold

Ireland is not a comparison-supported country. `CountryComparisonTopicMap` retains 13 supported slugs and 13 mappings per topic; the comparison catalog remains at 312 facts with zero Ireland facts. Both Production and Development Compare selectors exclude Ireland. This is the intended architecture hold; partial Ireland topics must not be added to Compare.

## Search status analysis

`SearchStatusFilter` is `Verified` for Production, for accepted countries, and for Development searches without a selected hidden country. It becomes null only for Development when a selected country is not Production-visible. `ArticleSearch.Search` treats null as no status filter, not as a NeedsReview preview mode. That explains why the current five Ireland articles appear, but the behavior is broader than the intended preview contract. The current global catalog has zero Outdated translations, so this is a forward-looking boundary defect rather than a current Outdated data leak.

## Test coverage

`IrelandContentIntegrationTests` covers the five article identities and categories, 15 NeedsReview translations, 70 uniquely owned sources, draft parity, localization, Production home/search/overview/Compare hold, Development home/overview/search/category preview, and Compare exclusion. `CountryComparisonTests` and `CountryVisibilityTests` preserve 13-country Production/comparison counts while updating catalog-wide totals and Development's country count.

Two material assertions are absent: a direct Production Ireland category-route assertion, and a SearchStatusFilter boundary regression using a hypothetical Outdated hidden-country translation. The latter belongs with IE-INT-01. The full test suite passed: 640 passed, 0 failed, 0 skipped. Restore emitted NU1900 warnings because the local NuGet vulnerability-cache path was denied; these did not prevent the test run.

## Aggregate assertion audit

Catalog-wide expectations correctly changed to 14 countries, 109 entries, 327 translations, 18 NeedsReview translations, and 934 sources. Development catalog visibility is 14 countries. Production remains 13 countries; comparison support and per-topic mappings remain 13; comparison facts remain 312. No inspected aggregate expectation incorrectly substitutes 14 for a Production/comparison count or 327 for the comparison-fact count.

## Final disposition

This audit is not human acceptance. Ireland remains Development-only and its translations remain `NeedsReview`. Production promotion and Compare integration remain prohibited. Blocked research topics remain blocked. Resolve IE-INT-01 and review the direct Production category-route test gap before accepting the partial integration.
