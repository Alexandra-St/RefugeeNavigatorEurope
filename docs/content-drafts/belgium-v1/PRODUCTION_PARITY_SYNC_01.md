# Belgium Production Parity Sync 01

**Date:** 14 November 2026
**Scope:** Sync the six revised `BE-TP-01` / `BE-TP-02` translations and add two owner-specific sources. Draft wording, translation statuses, timestamps for existing translations, country visibility, Compare data and runtime were not changed.

## Translation sync

Exact field comparison before sync found only `Content` differences for all six target translations. `Title` and `Summary` already matched. The pre-existing source-token sequences also matched; the drafts additionally had direct Markdown links to the two new sources, which have now been converted to owner-bound source citation tokens without changing their visible labels or surrounding prose.

| Article | Language | Title | Summary | Content | Citation update |
|---|---|---|---|---|---|
| `BE-TP-01` | EN | unchanged | unchanged | synced from reviewed draft | Temporary Protection token added for both intake and card guidance. |
| `BE-TP-01` | UK | unchanged | unchanged | synced from reviewed draft | Same owner-bound source token added for both claims. |
| `BE-TP-01` | RU | unchanged | unchanged | synced from reviewed draft | Same owner-bound source token added for both claims. |
| `BE-TP-02` | EN | unchanged | unchanged | synced from reviewed draft | Long Stay token added for the qualified municipality-filing route. |
| `BE-TP-02` | UK | unchanged | unchanged | synced from reviewed draft | Same owner-bound source token added. |
| `BE-TP-02` | RU | unchanged | unchanged | synced from reviewed draft | Same owner-bound source token added. |

The catalog content was copied verbatim from each reviewed draft section after the citation-only markup update. Existing translation `VerificationStatus` values and `LastUpdatedAt` timestamps remain unchanged (`NeedsReview`; 11 November 2026). The Belgium integration test already fixes that timestamp for the existing translations, so no parity-sync timestamp change was made.

## Source sync

| Draft source key | Production source ID | Owner article ID | Official title | URL | Checked |
|---|---|---|---|---|---|
| `BE-TP-01-S14` | `efd6f672-6edb-5eac-bddc-3d56ed6b4d27` | `aef6ec65-f8ed-5054-bafb-c465e790dd57` (`BE-TP-01`) | Belgian Immigration Office (FPS Home Affairs), “Temporary Protection” | `https://dofi.ibz.be/en/themes/ukraine/temporary-protection` | 14 Nov 2026 |
| `BE-TP-02-S21` | `38f01c1d-3688-5be0-b94b-97b5f0d8bdf3` | `54c6f95f-589e-5901-beae-990bed994160` (`BE-TP-02`) | Belgian Immigration Office (FPS Home Affairs), “Long stay [more than 90 days]” | `https://dofi.ibz.be/en/themes/ukraine/long-stay-more-90-days` | 14 Nov 2026 |

Both IDs follow the existing deterministic UUIDv5 convention, `UUIDv5(namespace = owner Article ID, name = local source key)`. Recomputed values match the stored IDs. Each source is bound to its own article, uses HTTPS, and is referenced by every language version of that article. The source registry's prior 146 records were preserved; exactly two records were added.

## Full parity

- Belgium articles: **8/8**.
- Belgium translations: **24/24 exact draft/catalog parity**. The 18 previously matching translations remain exact; the six revised TP translations now match.
- Belgium official-source records: **148**; unique URLs: **134**.
- Global official-source IDs: **1,426 records, 1,426 unique IDs**.
- Source ownership: all 148 Belgium source records belong to one of the 8 Belgium article owners; both new records have the intended owner.
- Citation integrity: all **480** source tokens resolve; zero orphan tokens and zero cross-owner references. All 148 owner records are cited at least once; both new records are used.
- Existing source records: all 146 retained with their previous values.

## Visibility/status hold

- Production visibility: **HIDDEN** (unchanged).
- Development visibility: **VISIBLE** (unchanged).
- Translation statuses: **24/24 `NeedsReview`** (unchanged).
- Compare: Belgium facts **0**; Belgium remains absent from `CountryComparisonTopicMap`. Compare remains **16 countries / 384 facts**.
- `countries.json`, `comparison-facts.json`, `CountryComparisonTopicMap.cs`, runtime and unrelated tests were not changed.

## Validation

- JSON parse and catalog-load validation: PASS.
- Deterministic UUIDv5, uniqueness, HTTPS and owner relationships: PASS.
- Draft/catalog Title, Summary and Content parity: **24/24 PASS**.
- Citation resolution, ownership and usage: PASS.
- Targeted `BelgiumContentIntegrationTests` Release run: **PASS, 7/7 tests**. Restore/build emitted a non-failing `NU1900` warning because the NuGet vulnerability-cache path was not writable; existing packages were already up to date.
- `git diff --check` and manual whitespace check for changed files: PASS.

## Verdict

**READY FOR PRODUCTION PROMOTION**

The parity sync is complete and the existing Development-only / `NeedsReview` hold is intact. This verdict records readiness for the separate promotion review; it does not itself change Production visibility, verification status or Compare support.
