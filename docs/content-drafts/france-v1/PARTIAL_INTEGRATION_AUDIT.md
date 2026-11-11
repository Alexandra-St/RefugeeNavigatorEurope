# France v1 Partial Integration Audit

Baseline: `2b65d5b8b2cf9b62f905e94e417bfc658e6b3bc1`

Review date: 11 November 2026

Scope: Development-only France catalog integration.

No external research was used. This audit made no code, catalog, draft, comparison, runtime or test changes.

## Summary

- BLOCKER: 0
- CORRECTION: 0
- POLISH: 0
- Catalog parity: **PASS**
- Source ownership: **PASS**
- Production hold: **PASS**
- Development preview: **PASS**
- Search preview contract: **PASS**
- Compare exclusion: **PASS**
- Existing-country regression: **PASS**
- Test coverage: **PASS**
- Recommendation: **PARTIAL INTEGRATION ACCEPTED — KEEP DEVELOPMENT-ONLY**

## Findings

No BLOCKER, CORRECTION or POLISH findings.

## Catalog snapshot

| Measure | Expected | Audited result | Result |
|---|---:|---:|---|
| Countries | 15 | 15 | PASS |
| Information entries | 115 | 115 | PASS |
| Translations | 345 | 345 | PASS |
| Translation status totals | 309 Verified / 36 NeedsReview / 0 Outdated | 309 / 36 / 0 | PASS |
| Official sources | 1014 | 1014 | PASS |
| France identity | `10000000-0000-0000-0000-000000000015` / France / `france` | Exact match | PASS |
| France entries | 6 | 6 | PASS |
| France translations | 18, all NeedsReview | 18, all NeedsReview | PASS |
| France sources | 80 | 80 | PASS |
| France comparison facts | 0 | 0 | PASS |

France’s six article IDs and categories match the integration specification. Category counts are: temporary-protection 1, employment 2, healthcare 1, housing 0, education 1 and social-assistance 1. No placeholder entry was added for the empty Housing category.

## Draft/catalog parity

All six final corrected drafts were checked against their integrated records. For EN, UK and RU, every title, summary and content field matches exactly. The integrated content includes its inline source tokens and excludes draft metadata, claim coverage/editorial exclusions and the editorial source-set section. All 18 translations have status `NeedsReview` and timestamp `2026-11-11T00:00:00+00:00`; none is `Verified`.

## Source ownership

All 80 source records match the owning draft’s source UUID, title and URL, with `InformationEntryId` set to that article’s ID. Per-article counts are FR-TP-01 **7**, FR-EMP-01 **12**, FR-EMP-02 **12**, FR-HEALTH-01 **12**, FR-EDU-01 **17** and FR-SOC-01 **20**.

All 80 URLs use HTTPS and have `LastCheckedAt` set to `2026-11-11T00:00:00+00:00`. Every inline citation resolves to a source owned by the same article. The France UUIDs are unique, all global source IDs remain unique, and there are no duplicate France `(InformationEntryId, URL)` pairs. Sources remain attached to their article ownership; no cross-article deduplication was applied.

## Localization

| Language | `Country.France` | `Country.France.In` | Result |
|---|---|---|---|
| EN | France | in France | PASS |
| UK | Франція | у Франції | PASS |
| RU | Франция | во Франции | PASS |

All six resource values are present.

## Visibility matrix

Verified for EN / UK / RU:

| Surface | Production | Development |
|---|---|---|
| Home | France absent; 13 visible country cards | France present; 15 visible country cards |
| Country selector | France absent | France present |
| `/countries/france` | NotFound | Country overview renders |
| France category routes | All six return NotFound | All six render; counts 1 / 2 / 1 / 0 / 1 / 1 for TP / employment / healthcare / housing / education / social assistance |
| `/search?country=france` | 0 results | 6 results; `ArticleSearch` also returns 6 |
| Compare selectors | France absent | France absent |

The `temporary-protection` entry does not make France Production-visible because its translations remain `NeedsReview`. Production visibility remains based on verified translations.

## Search status analysis

`Search.razor` selects `Verified` for Production, Development searches without a selected country, and Development searches for countries visible in Production. Only a selected country hidden from Production in Development receives the `NeedsReview` filter. This value is non-null, so `ArticleSearch.Search` applies the status restriction; it does not treat the preview as unrestricted.

The France regression test changes one in-memory English translation to `Outdated`, renders the Development France search, and verifies that the outdated article is absent while five other results remain, including another valid France article. The checked-in catalog is not modified by this test. Search preview boundary: **PASS**.

## Comparison hold

`CountryComparisonTopicMap` has 13 supported countries, excludes both France and Ireland, and each topic retains 13 mappings. The facts catalog has 312 facts, with zero France facts and zero Ireland facts. France is absent from both Production and Development Compare selectors. No France comparison facts or placeholders were added. Comparison hold: **PASS**.

## Existing-country regression audit

The integration commit’s existing-test changes update only aggregate values affected by the catalog addition: countries 14 → 15; entries 109 → 115; translations 327 → 345; NeedsReview 18 → 36; sources 934 → 1014; and Development visible countries 14 → 15. Production visible countries remain 13, Verified remains 309, Outdated remains 0, comparison support remains 13 countries and 312 facts, and prior per-country article/source counts remain unchanged.

Ireland remains Development-only with 5 articles, 15 NeedsReview translations, 70 owned sources and zero comparison facts. Other public-country visibility, article identities and translation statuses were not changed. The `CountryComparisonTests` addition excludes the new France IDs from its existing Verified-only assertion; it does not alter comparison mappings or facts.

## Runtime-scope audit

The integration commit changes exactly the six catalog/localization files and sixteen test files specified in its scope (22 files total). It changes no `.razor` component, visibility implementation, search implementation, provider/domain model, comparison code or `comparison-facts.json`. Runtime and comparison implementation remain unchanged.

## Test coverage

`FranceContentIntegrationTests` materially covers France identity and article IDs/categories, global and France counts, translation status and exact draft parity, source ownership and citation integrity, localization, Production hiding, Development preview, direct Production category rejection, the empty Housing category, hypothetical Outdated exclusion and Compare hold. No material regression-test gap was identified.

The integration run recorded for the audited commit passed **648 tests, 0 failed, 0 skipped**. Restore emitted NU1900 warnings only because access to the local NuGet vulnerability cache was denied; this did not affect the test result. GitHub checks for the baseline are both successful: `build-and-test` and `Netherlands visual evidence`.

## Final disposition

**PARTIAL INTEGRATION ACCEPTED — KEEP DEVELOPMENT-ONLY**

France remains visible in Development only. All translations remain `NeedsReview`; this audit is not human acceptance. Production promotion and Compare integration are prohibited. `residence-filing` and `housing-initial` remain blocked.
