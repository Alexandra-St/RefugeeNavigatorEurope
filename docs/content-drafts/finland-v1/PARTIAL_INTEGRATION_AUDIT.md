# Finland v1 Partial Integration Audit

Baseline: `b4d62bb30dea8710a5616eccf951cf78ffbe120c`

Review date: 11 November 2026

## Summary

- BLOCKER: **0**
- CORRECTION: **0**
- POLISH: **0**
- Catalog parity: **PASS**
- Source ownership: **PASS**
- Production hold: **PASS**
- Development preview: **PASS**
- Search preview contract: **PASS**
- Compare exclusion: **PASS**
- Existing-country regression: **PASS**
- Test coverage: **PASS**
- Recommendation: **PARTIAL INTEGRATION ACCEPTED — KEEP DEVELOPMENT-ONLY**

No findings were identified. The audit made no content, catalog, runtime, test, comparison or localization changes. No external research was used.

## Findings

No BLOCKER, CORRECTION or POLISH findings.

## Catalog snapshot

| Measure | Expected | Audited result | Result |
| --- | ---: | ---: | --- |
| Countries | 16 | 16 | PASS |
| Information entries | 123 | 123 | PASS |
| Translations | 369 | 369 | PASS |
| Translation statuses | 309 Verified / 60 NeedsReview / 0 Outdated | 309 / 60 / 0 | PASS |
| Official sources | 1114 | 1114 | PASS |
| Finland identity | `10000000-0000-0000-0000-000000000016` / Finland / `finland` | Exact match | PASS |
| Finland entries | 8 | 8 | PASS |
| Finland translations | 24, all NeedsReview | 24, all NeedsReview | PASS |
| Finland sources | 100 | 100 | PASS |
| Finland comparison facts | 0 | 0 | PASS |

Article identities, categories and source counts:

| Draft | Article ID | Category | Sources |
| --- | --- | --- | ---: |
| FI-TP-01 | `ffd32dca-56d8-5dcf-b995-7522a159010a` | temporary-protection | 10 |
| FI-TP-02 | `344c5e31-45a9-5f22-8d4a-4a58d8fe8243` | temporary-protection | 15 |
| FI-EMP-01 | `53743a68-bacb-5344-9aee-559e146cab54` | employment | 11 |
| FI-EMP-02 | `0abffa8c-cd42-571e-97f3-e160afad880c` | employment | 9 |
| FI-HEALTH-01 | `ea41d1e7-f921-50af-9886-2232d9fb06ef` | healthcare | 10 |
| FI-HOUSING-01 | `61b331e0-76bb-58f4-91e7-e5944ba77e31` | housing | 14 |
| FI-EDU-01 | `2a2094da-e321-50bd-8a45-bf3988be06c9` | education | 15 |
| FI-SOC-01 | `51691974-1c20-5b25-9572-66624984a52f` | social-assistance | 16 |
| **Total** |  |  | **100** |

Category totals are temporary-protection **2**, employment **2**, healthcare **1**, housing **1**, education **1**, and social-assistance **1**.

## Draft/catalog parity

All eight final drafts were compared with their catalog records. For EN / UK / RU, every Title, Summary and Content field matches exactly, including inline citation tokens. Catalog content contains none of the draft metadata, claim-coverage/editorial-exclusion sections or editorial source sets. All 24 translations are `NeedsReview` with `LastUpdatedAt` `2026-11-11T00:00:00+00:00`; none is Verified.

Catalog parity: **PASS**.

## Source ownership

All 100 Finland sources match their draft's UUID, title and URL. Each `InformationEntryId` identifies the owning article; every URL is HTTPS and every `LastCheckedAt` is `2026-11-11T00:00:00+00:00`. All 100 Finland UUIDs are unique, all 1114 global source UUIDs are unique, and all 318 Finland inline citation tokens resolve to sources owned by the same article.

The triple `(InformationEntryId, Title, URL)` is unique throughout the catalog. FI-EMP-02 intentionally has two distinct editorial records for the same article and URL: UUID `7ab4c5c4-4363-5083-9734-4a16e6d72e74` for the current consolidated Employment Contracts Act and UUID `e0b8e4ac-3707-5842-8fc1-63f81bbb2532` for §2:4. Their titles differ, and both match the draft. The shared URL is intentional and is not a finding.

Source ownership: **PASS**.

## Localization

| Language | `Country.Finland` | `Country.Finland.In` | Result |
| --- | --- | --- | --- |
| EN | Finland | in Finland | PASS |
| UK | Фінляндія | у Фінляндії | PASS |
| RU | Финляндия | в Финляндии | PASS |

All six resource values are present.

## Visibility matrix

Verified in EN / UK / RU by `FinlandContentIntegrationTests`:

| Surface | Production | Development |
| --- | --- | --- |
| Home | Finland absent; 13 country cards | Finland present; 16 country cards |
| Country selector | Finland absent | Finland present; all 16 catalog countries listed |
| `/countries/finland` | NotFound | Overview renders |
| Finland category routes | All six return NotFound | All six render; counts 2 / 2 / 1 / 1 / 1 / 1 for TP / employment / healthcare / housing / education / social assistance |
| `/search?country=finland` | 0 results | 8 results |
| Compare selectors | Finland absent from both selectors | Finland absent from both selectors |

Production remains at 13 accepted countries. The Finland translations are all NeedsReview, so their catalog presence does not make Finland Production-visible.

Production hold: **PASS**. Development preview: **PASS**.

## Search status analysis

For a selected country hidden from Production, the Development Search component passes `NeedsReview` as its status filter. It does not pass `null` (which means no status filtering). The Finland regression test renders the actual Search component with an in-memory catalog where one English Finland translation is changed from NeedsReview to Outdated. The component returns seven results, omits the outdated article and retains the other seven Finland article titles. Normal Finland Development search returns eight results. No Search runtime change was made in the integration commit.

Search preview contract: **PASS**.

## Comparison hold

`CountryComparisonTopicMap` supports exactly **13** country slugs, with **13** entry mappings in each topic. `comparison-facts.json` contains **312** facts. It contains zero facts for Finland, France and Ireland. Finland integration tests also verify that Finland, France and Ireland are absent from Production and Development Compare selectors.

Aggregate research readiness still marks `protection-dates` **Blocked — material evidence gap**. The known EU temporary-protection endpoint is 4 March 2028, but individual Finnish permit validity, physical card validity, 2027/2028 extension mechanics and holder action remain unresolved. The editorial review's constrained FI-TP-01 wording remains in the catalog; integration has not resolved this blocker.

Compare exclusion: **PASS**. Finland remains excluded from full Compare.

## Existing-country regression audit

The integration commit changes exactly **23 files**: three catalog JSON files, three RESX files and 17 tests. Existing-country test changes update affected global counts and Development country totals; preserve country-specific article/source assertions; exclude Finland IDs from the Verified-only blanket check while explicitly asserting Finland's NeedsReview status; and use semantic source identity `(InformationEntryId, Title, URL)` where duplicate URLs can represent distinct records. The remaining changes add Finland-specific integration coverage. No existing-country article identity or content was changed.

Ireland remains Development-only with **5** articles, **15** NeedsReview translations, **70** owned sources and **0** comparison facts. France remains Development-only with **6** articles, **18** NeedsReview translations, **80** owned sources and **0** comparison facts. Production remains at **13** countries; Verified remains **309**; Outdated remains **0**; Compare remains **13** countries / **312** facts.

Existing-country regression: **PASS**.

## Runtime-scope audit

The 23-file integration change contains only the three catalog JSON files, three localization RESX files and 17 test files. It changes no `.razor` component, `CountryVisibility` implementation, `ArticleSearch`, domain model, catalog provider, comparison implementation, `comparison-facts.json`, deployment file or workflow. The Search component continues to apply the selected hidden-country NeedsReview filter. Runtime scope: **PASS**.

## Test coverage

`FinlandContentIntegrationTests` covers Finland identity, all eight article IDs/categories, global and Finland counts, all 24 NeedsReview translations and timestamps, exact EN / UK / RU draft parity, all 100 source records and citation ownership, the semantic duplicate guard, localization, Production route/search/selector hiding, Development home/search/country/category routes and counts, hypothetical Outdated filtering, and exclusion from Compare alongside France and Ireland. No material integration regression gap was identified.

The local full suite recorded **655 passed, 0 failed, 0 skipped**. GitHub CI for the audited SHA also reports **Failed 0, Passed 655, Skipped 0, Total 655**; job `build-and-test` succeeded. The GitHub run emitted non-blocking NU1900 vulnerability-cache access warnings. GitHub `Netherlands visual evidence` also succeeded, including browser installation, assertions/capture and artifact upload.

## Final disposition

**PARTIAL INTEGRATION ACCEPTED — KEEP DEVELOPMENT-ONLY**

Finland remains Development-only with all translations `NeedsReview`, absent from Production and absent from Compare. This audit does not promote any translation to Verified, authorize Production, resolve `protection-dates`, or authorize Finland comparison facts.

## Validation

- Audited repository state at `b4d62bb30dea8710a5616eccf951cf78ffbe120c`, catalog data, draft parity, sources, localization, relevant runtime and tests, and the specified integration change scope.
- Confirmed the France and Ireland regression counters and zero comparison facts.
- Verified the recorded 655/655 GitHub CI result and successful Netherlands visual-evidence run.
- No external research or full test rerun was needed for this read-only audit.
