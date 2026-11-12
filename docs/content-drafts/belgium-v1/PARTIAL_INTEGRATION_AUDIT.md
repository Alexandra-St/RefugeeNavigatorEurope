# Belgium v1 Partial Integration Audit

Baseline: `c8357f5e9fd238d85d646c525647f35ca2031a50`

Review date: 12 November 2026

## Summary

- BLOCKER: **0**
- CORRECTION: **0**
- POLISH: **0**
- Catalog parity: **PASS** — 17 countries, 131 entries, 393 translations, 309 Verified / 84 NeedsReview / 0 Outdated, and 1260 sources.
- Source ownership: **PASS** — 146 exact Belgium source records; citations resolve to article-owned sources; all global source UUIDs and ownership/title/URL triples are unique.
- S21/S28 correction: **PASS** — distinct records have human-readable, draft-matched titles; internal keys no longer appear in the rendered Development education page.
- Production hold: **PASS** — all Belgium translations remain NeedsReview and are hidden from Production by the existing status-based visibility rules.
- Development preview: **PASS** — Belgium renders in Development country/category pages, selectors and search.
- Search preview contract: **PASS** — normal search returns 8 entries; an Outdated Belgium translation is excluded, leaving 7.
- Compare exclusion: **PASS** — Belgium is absent from the 13-country mapping and both Compare selectors; there are no Belgium comparison facts.
- Existing-country regression: **PASS** — Ireland, France and Finland remain Development-only with their expected counts.
- Runtime scope: **PASS** — the source-title correction changed metadata and tests only; no runtime masking or implementation change was made.
- Test coverage: **PASS** — catalog parity and UI regression assertions cover the corrected source titles and visibility gates.
- Recommendation: **PARTIAL INTEGRATION ACCEPTED — KEEP DEVELOPMENT-ONLY**.

No BLOCKER, CORRECTION or POLISH findings were identified. The source-key leakage identified in the preceding audit is resolved by giving the two source records distinct, human-readable editorial titles. No runtime stripping or hiding logic was introduced. This audit does not approve Production, Verified status, Compare, comparison facts or deployment.

## Findings

No BLOCKER, CORRECTION or POLISH findings.

The prior S21/S28 source-title leakage correction is resolved. The source titles now distinguish the general tuition guidance from the temporary-protection fee category, while retaining the same official page URL and independent source UUIDs.

## Catalog snapshot

| Measure | Expected | Audited result | Result |
| --- | ---: | ---: | --- |
| Countries | 17 | 17 | PASS |
| Information entries | 131 | 131 | PASS |
| Translations | 393 | 393 | PASS |
| Translation statuses | 309 Verified / 84 NeedsReview / 0 Outdated | 309 / 84 / 0 | PASS |
| Official sources | 1260 | 1260 | PASS |
| Global source UUID uniqueness | 1260 unique | 1260 unique | PASS |
| Global `(InformationEntryId, Title, URL)` uniqueness | Unique | Unique | PASS |
| Belgium identity | `10000000-0000-0000-0000-000000000017` / Belgium / `belgium` | Exact match | PASS |
| Belgium entries | 8 | 8 | PASS |
| Belgium translations | 24, all NeedsReview | 24, all NeedsReview | PASS |
| Belgium sources | 146 | 146 | PASS |
| Belgium comparison facts | 0 | 0 | PASS |

Belgium article identities, categories and source counts:

| Draft | Article ID | Category | Sources |
| --- | --- | --- | ---: |
| `BE-TP-01` | `aef6ec65-f8ed-5054-bafb-c465e790dd57` | temporary-protection | 10 |
| `BE-TP-02` | `54c6f95f-589e-5901-beae-990bed994160` | temporary-protection | 16 |
| `BE-EMP-01` | `f2c1526a-a218-5bf0-ab2a-9167dbec0c26` | employment | 17 |
| `BE-EMP-02` | `55fa79e5-89a2-518b-90b2-85a2735f171b` | employment | 22 |
| `BE-HEALTH-01` | `673316a9-6ee9-50d6-9f66-e19659aeff0e` | healthcare | 13 |
| `BE-HOUSING-01` | `3646ae19-215c-5f0e-9075-85ef5bae88f7` | housing | 23 |
| `BE-EDU-01` | `ac80dcbd-4289-5c77-8340-f9e4226a47f6` | education | 25 |
| `BE-SOC-01` | `20eef3c3-fcc7-5b5b-b672-695f29faad61` | social-assistance | 20 |
| **Total** | **8 unique Article IDs** |  | **146** |

Category totals: temporary-protection **2**, employment **2**, healthcare **1**, housing **1**, education **1**, and social-assistance **1**.

## Draft/catalog parity

All eight final drafts were compared with their catalog records. For EN / UK / RU, all **24/24** Title, Summary and Content values match exactly, including citation tokens. Catalog Content contains none of the draft metadata, Claim coverage and editorial exclusions, or Editorial source set. Every Belgium translation is `NeedsReview` with `LastUpdatedAt` `2026-11-11T00:00:00+00:00`.

Draft/catalog parity: **PASS**.

## Source ownership

All 146 Belgium source records match the draft UUID, owner, editorial title and exact URL. Each source is HTTPS with `LastCheckedAt` `2026-11-11T00:00:00+00:00`. Every inline citation token in all 24 translations resolves to a source owned by the same article, with zero orphan citations and zero unused Belgium source records. All 1260 global source UUIDs are unique, and `(InformationEntryId, Title, URL)` is unique across the catalog. Repeated URLs alone are allowed.

Source ownership and citation resolution: **PASS**.

## BE-EDU-01 S21/S28 correction verification

| Local editorial record | UUID | Final title | URL | Owner |
| --- | --- | --- | --- | --- |
| `BE-EDU-01-S21` / `BE-EDU-S21` | `4ae5b359-e5ab-5490-a662-b4dcedce7f79` | Study in Flanders / Flemish Government, “Tuition fees” — general tuition guidance | `https://www.studyinflanders.be/practical-information/tuition-fees` | `BE-EDU-01` |
| `BE-EDU-01-S28` / `BE-EDU-S28` | `757cd1e2-91e3-521a-9677-1bd011ca04f4` | Study in Flanders / Flemish Government, “Tuition fees” — temporary-protection fee category | `https://www.studyinflanders.be/practical-information/tuition-fees` | `BE-EDU-01` |

Both records remain separate. Their UUIDs, shared URL and BE-EDU-01 ownership are unchanged. Draft editorial titles match catalog titles exactly; they no longer contain `[BE-EDU-01-S21]` or `[BE-EDU-01-S28]`. No special S21/S28 title exception remains in the integration test. The new suffixes are human-readable distinctions, not local source keys.

The Development education-category regression renders `/countries/belgium/categories/education`, asserts that neither internal key appears in the HTML, and asserts that both human-readable titles appear in its visible text. The citation tokens and EN / UK / RU article Title, Summary and Content were not changed by the correction. Source identity remains semantically distinct and owned by the same article.

**User-visible key leakage: none.** The source link continues to render `OfficialSource.Title` directly; the metadata itself is now safe for display. No runtime masking was added.

S21/S28 correction: **PASS**.

## Localization

| Language | `Country.Belgium` | `Country.Belgium.In` | Result |
| --- | --- | --- | --- |
| EN | Belgium | in Belgium | PASS |
| UK | Бельгія | у Бельгії | PASS |
| RU | Бельгия | в Бельгии | PASS |

All six resource values are present and exact.

## Visibility matrix

The Belgium integration tests render the relevant surfaces in EN / UK / RU:

| Surface | Production | Development |
| --- | --- | --- |
| Home | 13 country cards; Belgium absent | 17 country cards; Belgium present |
| Search country selector | Belgium absent | All 17 catalog countries in catalog order |
| `/countries/belgium` | NotFound | Overview renders |
| Belgium category routes | All six return NotFound | All six render; counts 2 / 2 / 1 / 1 / 1 / 1 for TP / employment / healthcare / housing / education / social assistance |
| `/search?country=belgium` | 0 results | 8 results |
| Compare selectors | Belgium absent | Belgium absent |

Production visible countries remain **13**. Development visibility: **PASS**. Production hold: **PASS**.

## Search status analysis

Normal Development search for Belgium returns **8** results. The regression test changes one selected English Belgium translation in memory from `NeedsReview` to `Outdated`; the Development search then returns **7**, omits the Outdated article and retains the other seven.

The existing Search status filter uses `NeedsReview` for Development searches of countries hidden from the Verified-only country list, and `Verified` otherwise. No Search runtime implementation changed in the integration or correction commits.

Search preview contract: **PASS**.

## Comparison hold

The research readiness file still identifies exactly three blocked aspect IDs: `protection-application`, `protection-dates` and `residence-filing`.

`CountryComparisonTopicMap.SupportedCountrySlugs` contains **13** countries; each topic maps **13** entry IDs. `comparison-facts.json` contains **312** facts and zero Belgium facts. Belgium is absent from both Production and Development Compare selectors. Finland, France and Ireland also remain absent from both selectors, the support map and the comparison facts.

Neither the Belgium integration nor the S21/S28 correction changed `CountryComparisonTopicMap.cs` or `comparison-facts.json`.

Compare exclusion: **PASS**. Belgium Compare support and facts remain unauthorized while the blockers remain unresolved.

## Existing Development-only country regression

| Country | Entries | NeedsReview translations | Sources | Comparison facts |
| --- | ---: | ---: | ---: | ---: |
| Ireland | 5 | 15 | 70 | 0 |
| France | 6 | 18 | 80 | 0 |
| Finland | 8 | 24 | 100 | 0 |
| Belgium | 8 | 24 | 146 | 0 |

The existing countries, entries and source rows from before Belgium integration remain unchanged. Development-only regression counters: **PASS**.

## Existing public-country regression

Compared with the pre-Belgium baseline `400b2d9687f200a9577e28aed97dcbc0fdbaeb5e`, every existing country, information-entry and official-source record is still present and unchanged. No existing public-country article ID, translation content/status, source record or comparison fact changed.

Production remains at **13** countries with **309 Verified** translations. Compare remains **13** countries / **312** facts.

Existing public-country regression: **PASS**.

## Runtime-scope audit

The Belgium integration commit changed 24 files: three catalog JSON files, three localization RESX files and 18 tests. The correction commit changed exactly three files: `BE-EDU-01.md`, `official-sources.json` and `BelgiumContentIntegrationTests.cs`. Its draft diff changes only the two Editorial source set titles; its catalog diff changes only the two matching source `Title` fields; its test diff removes the special-case title mutation and adds the rendered Development education leakage assertion.

Neither commit changed `Category.razor`, `ArticleContentFormatter`, `ArticleSearch`, `CountryVisibility`, the catalog provider or validators, domain models, comparison runtime, `CountryComparisonTopicMap.cs`, `comparison-facts.json`, deployment files or workflows. The leakage was fixed in source metadata, not hidden by UI logic.

Runtime scope: **PASS**.

## Test coverage

`BelgiumContentIntegrationTests` covers Belgium identity, all eight Article IDs/categories, global counts, all 24 NeedsReview statuses and timestamps, exact draft parity, 146 sources, exact source-title parity without an S21/S28 exception, citation ownership, global UUID and semantic-tuple uniqueness, localization, Production hiding, Development visibility, search results 8 and hypothetical Outdated results 7, category routes/counts, and Compare exclusion.

The Development education render additionally asserts that internal S21/S28 keys are absent from the HTML and both corrected human-readable source titles appear. The test suite was not rerun for this audit; exact correction-SHA GitHub CI passed all 662 tests, and the targeted Belgium suite was run after the correction.

Test coverage: **PASS**.

## CI / visual evidence

For exact correction SHA `c8357f5e9fd238d85d646c525647f35ca2031a50`:

- GitHub `CI`, run `43238434349`: **success**.
- `build-and-test`: **success**; GitHub test log: **Failed 0, Passed 662, Skipped 0, Total 662**.
- GitHub `Netherlands visual evidence`, run `43238434260`: **success**.

## Final disposition

**PARTIAL INTEGRATION ACCEPTED — KEEP DEVELOPMENT-ONLY**

All 24 Belgium translations remain `NeedsReview`; Belgium remains hidden from Production and excluded from Compare. No Belgium comparison facts exist. The research blockers `protection-application`, `protection-dates` and `residence-filing` remain unresolved. This disposition does not authorize Production, Verified status, Compare, comparison facts or deployment.

## Validation

- Re-audited the repository at exact baseline/correction SHA `c8357f5e9fd238d85d646c525647f35ca2031a50`, the eight final drafts, post-correction editorial review, aggregate research readiness, catalog, localization, relevant runtime/rendering paths and Belgium tests.
- Rechecked exact draft/catalog translation and source metadata parity, citation ownership, S21/S28 rendering assertions, catalog counts, visibility/search/Compare contracts, existing-country counters and commit scopes.
- Verified correction-SHA GitHub CI and Netherlands visual-evidence runs.
- No external research was used. No tests were rerun for this audit because exact-SHA CI already records 662/662 passing and the targeted Belgium tests passed after the correction.
- No content, catalog, test, runtime, comparison, localization or deployment changes were made by this audit.
