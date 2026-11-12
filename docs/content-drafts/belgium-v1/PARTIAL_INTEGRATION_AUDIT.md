# Belgium v1 Partial Integration Audit

Baseline: `c8357f5e9fd238d85d646c525647f35ca2031a50`

Review date: 12 November 2026

## Summary

- BLOCKER: **0**
- CORRECTION: **0**
- POLISH: **0**
- Catalog parity: **PASS** — 17 countries, 131 entries, 393 translations, 309 Verified / 84 NeedsReview / 0 Outdated, 1260 official sources.
- Source ownership: **PASS** — 146 Belgium sources match their drafts; all citations resolve to article-owned sources; global source UUIDs and `(InformationEntryId, Title, URL)` triples are unique.
- S21/S28 correction: **PASS** — distinct records retain their UUIDs and shared URL, with human-readable draft/catalog-matched titles and no key leakage in Development.
- Production hold: **PASS** — all 24 Belgium translations remain NeedsReview; Belgium is absent from Production.
- Development preview: **PASS** — Belgium renders in Development routes, selectors and search.
- Search preview contract: **PASS** — normal search returns 8; the hypothetical Outdated case returns 7 and omits the outdated entry.
- Compare exclusion: **PASS** — 13 supported countries / 312 facts; Belgium has no facts and is absent from both Compare selectors.
- Existing-country regression: **PASS** — Ireland, France and Finland retain expected Development-only counts.
- Runtime scope: **PASS** — the source-title correction changed metadata and tests only; no runtime masking was introduced.
- Test coverage: **PASS** — Belgium integration tests cover data parity, visibility, search, Compare and source-title rendering.
- Recommendation: **PARTIAL INTEGRATION ACCEPTED — KEEP DEVELOPMENT-ONLY**.

The previous source-key leakage correction is resolved. No BLOCKER, CORRECTION or POLISH findings were found. No external research was used. This audit does not approve Production visibility, Verified status, Compare, comparison facts or deployment.

## Findings

No BLOCKER, CORRECTION or POLISH findings.

The prior S21/S28 source-title leakage correction is resolved by distinct human-readable source titles. The UI renders those titles directly and does not need key-stripping or hiding logic.

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

Category totals: temporary-protection **2**, employment **2**, healthcare **1**, housing **1**, education **1**, social-assistance **1**.

## Draft/catalog parity

All eight final drafts were compared with the catalog. All **24/24** EN / UK / RU Title, Summary and Content values match exactly, including citation tokens. Catalog Content contains no draft metadata, Claim coverage and editorial exclusions, or Editorial source set. All 24 translations are `NeedsReview` with `LastUpdatedAt` `2026-11-11T00:00:00+00:00`.

Draft/catalog parity: **PASS**.

## Source ownership

All 146 Belgium source records match the owning draft's UUID, InformationEntryId, editorial title and exact URL. All use HTTPS and `LastCheckedAt` `2026-11-11T00:00:00+00:00`. Every inline citation in all 24 translations resolves to a source owned by that article. Orphan citations: **0**. Unused Belgium source records: **0**. All 1260 global UUIDs are unique and the global `(InformationEntryId, Title, URL)` triple is unique. Repeated URLs alone remain permitted.

Source ownership: **PASS**.

## BE-EDU-01 S21/S28 correction verification

| Local editorial record | UUID | Final title | URL | Owner |
| --- | --- | --- | --- | --- |
| `BE-EDU-01-S21` / `BE-EDU-S21` | `4ae5b359-e5ab-5490-a662-b4dcedce7f79` | Study in Flanders / Flemish Government, “Tuition fees” — general tuition guidance | `https://www.studyinflanders.be/practical-information/tuition-fees` | `BE-EDU-01` |
| `BE-EDU-01-S28` / `BE-EDU-S28` | `757cd1e2-91e3-521a-9677-1bd011ca04f4` | Study in Flanders / Flemish Government, “Tuition fees” — temporary-protection fee category | `https://www.studyinflanders.be/practical-information/tuition-fees` | `BE-EDU-01` |

Both records remain separate; UUIDs, shared URL and owner are unchanged. The draft editorial titles match the catalog titles exactly. Neither catalog title contains `[BE-EDU-01-S21]` or `[BE-EDU-01-S28]`, and the integration test no longer has a special-case title mutation.

The Development education render asserts that both internal keys are absent from its HTML and both human-readable titles appear in visible text. EN / UK / RU article Title, Summary and Content were not altered by the correction. The public titles distinguish the two records without leaking local keys. No runtime masking was introduced.

S21 audit: **PASS**. S28 audit: **PASS**. User-visible key leakage: **none**.

## Localization

| Language | `Country.Belgium` | `Country.Belgium.In` | Result |
| --- | --- | --- | --- |
| EN | Belgium | in Belgium | PASS |
| UK | Бельгія | у Бельгії | PASS |
| RU | Бельгия | в Бельгии | PASS |

All six values match the expected strings.

## Visibility matrix

Belgium tests render relevant surfaces in EN / UK / RU:

| Surface | Production | Development |
| --- | --- | --- |
| Home | 13 country cards; Belgium absent | 17 country cards; Belgium present |
| Search country selector | Belgium absent | All 17 catalog countries |
| `/countries/belgium` | NotFound | Overview renders |
| Belgium category routes | All six return NotFound | All six render; 2 / 2 / 1 / 1 / 1 / 1 for TP / employment / healthcare / housing / education / social assistance |
| `/search?country=belgium` | 0 results | 8 results |
| Compare selectors | Belgium absent | Belgium absent |

Production visibility: **PASS**. Development visibility: **PASS**.

## Search status analysis

Normal Belgium Development search returns **8** results. The regression test changes one English Belgium translation in memory from `NeedsReview` to `Outdated`; Development search then returns **7**, omits the Outdated article and retains the other seven. The existing Search status filter is unchanged.

Outdated=7 search result: **PASS**.

## Comparison hold

`RESEARCH_READINESS.md` records exactly three remaining blockers: `protection-application`, `protection-dates` and `residence-filing`.

`CountryComparisonTopicMap` supports **13** country slugs and each topic maps **13** entry IDs. `comparison-facts.json` contains **312** facts and zero Belgium facts. Belgium, Finland, France and Ireland are absent from Development and Production Compare selectors. Neither the Belgium integration nor correction changed `CountryComparisonTopicMap.cs` or `comparison-facts.json`.

Compare exclusion: **PASS**. Belgium remains outside Compare while research blockers remain.

## Existing Development-only country regression

| Country | Entries | NeedsReview translations | Sources | Comparison facts |
| --- | ---: | ---: | ---: | ---: |
| Ireland | 5 | 15 | 70 | 0 |
| France | 6 | 18 | 80 | 0 |
| Finland | 8 | 24 | 100 | 0 |
| Belgium | 8 | 24 | 146 | 0 |

Ireland, France and Finland retain the expected Development-only catalog state.

## Existing public-country regression

Compared with pre-Belgium commit `400b2d9687f200a9577e28aed97dcbc0fdbaeb5e`, all existing country, information-entry and official-source records remain present and unchanged. `comparison-facts.json` is byte-for-byte equivalent as parsed JSON. The only additions in the catalog are Belgium's country, eight entries and 146 sources.

Production remains at **13** countries with **309 Verified** translations. Compare remains **13** countries / **312** facts.

Existing public-country regression: **PASS**.

## Runtime-scope audit

The Belgium integration commit `21494af70dc0432136d79a9ead1ae64430633ca7` changed 24 files: three catalog JSON files, three localization RESX files and 18 test files. The correction commit `c8357f5e9fd238d85d646c525647f35ca2031a50` changed exactly:

- `docs/content-drafts/belgium-v1/BE-EDU-01.md`
- `src/RefugeeNavigator.Web/Data/official-sources.json`
- `tests/RefugeeNavigator.Tests/BelgiumContentIntegrationTests.cs`

Its draft diff changes only the two Editorial source titles; catalog diff changes only those two source titles; test diff removes the title special case and adds rendered Development leakage assertions. No runtime implementation, deployment/workflow, or comparison file changed. The leakage was resolved in source metadata rather than hidden by UI logic.

Runtime scope: **PASS**.

## Test coverage

`BelgiumContentIntegrationTests` covers Belgium identity, all eight article IDs/categories, catalog and translation counts/status/timestamps, exact draft parity, 146 source records, exact title parity without an S21/S28 special case, citation ownership, global UUID and semantic-triple uniqueness, localization, Production hiding, Development routes/search/category counts, normal search 8, Outdated search 7, and Compare exclusion. It also checks S21/S28 internal-key non-leakage and visible human-readable titles.

The local test suite was not rerun for this audit. GitHub CI for exact correction SHA `c8357f5e9fd238d85d646c525647f35ca2031a50` passed **662/662** tests (0 failed, 0 skipped), including the correction tests.

Test coverage: **PASS**.

## CI / visual evidence

For exact correction SHA `c8357f5e9fd238d85d646c525647f35ca2031a50`:

- GitHub `CI`, run `43238434349`: **success**.
- `build-and-test`: **success**; log reports Failed 0, Passed 662, Skipped 0, Total 662.
- GitHub `Netherlands visual evidence`, run `43238434260`: **success**.

## Final disposition

**PARTIAL INTEGRATION ACCEPTED — KEEP DEVELOPMENT-ONLY**

Belgium's 24 translations remain `NeedsReview`; Belgium remains hidden from Production and excluded from Compare. Belgium has zero comparison facts. The research blockers `protection-application`, `protection-dates` and `residence-filing` remain open. This audit does not authorize Verified status, Production, Compare, comparison facts or deployment.

## Validation

- Re-audited repository state at baseline `c8357f5e9fd238d85d646c525647f35ca2031a50`, including the eight final drafts, post-correction editorial review, aggregate readiness, catalog, localization, relevant runtime and tests.
- Compared all EN / UK / RU draft fields to catalog values and checked source ownership, citations, counts, status values, S21/S28 rendering assertions, visibility, search and Compare behavior.
- Rechecked existing Ireland / France / Finland counters and compared pre-Belgium public catalog records and comparison facts.
- Audited correction commit scope against its parent `21494af70dc0432136d79a9ead1ae64430633ca7` because the requested baseline is the correction commit itself; the exact three-file correction diff passed.
- Verified exact-SHA CI and Netherlands visual-evidence runs directly from GitHub Actions.
- No external research or local test rerun. No content, catalog, test, runtime, comparison, localization or deployment changes were made by this audit.
