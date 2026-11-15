# Ireland Missing Core Production Integration 01

**Date:** 15 November 2026

**Scope:** Integrated the three reviewed Ireland Core articles into the Development catalog. Drafts and research files were not changed. No Production promotion or Compare integration was performed.

## Integrated articles

| Article ID | Article UUID | Category | Languages | Translation status |
| --- | --- | --- | --- | --- |
| `IE-TP-01` | `51fc8ed8-123d-5a6f-b4fa-16cd2e41f3bf` | `temporary-protection` | EN / UK / RU | NeedsReview |
| `IE-TP-02` | `6bca20f8-b06d-5068-bd45-c78155111354` | `temporary-protection` | EN / UK / RU | NeedsReview |
| `IE-HOUSING-01` | `d0d74e2e-9a2a-50bc-970f-605997565456` | `housing` | EN / UK / RU | NeedsReview |

The three records bring Ireland Core to **8/8 articles** and **24/24 translations**. All nine new translations copy the reviewed draft title, summary and content. Their `LastUpdatedAt` is 15 November 2026, matching the review date and recent-content convention. Existing Ireland records and their 15 translations were left unchanged.

## Sources

- Added **36** owner-specific source records: 11 for `IE-TP-01`, 10 for `IE-TP-02`, and 15 for `IE-HOUSING-01`.
- Ireland now has **106** source records and **96 unique HTTPS URLs**.
- All 36 IDs match their deterministic IDs from the reviewed draft maps. Every source points to its assigned article; URLs and titles match the draft mappings.
- New records retain the source check dates supported by the research package: 25 use 15 November 2026 and 11 package-only records use 10 November 2026.

Five selected URLs already existed in the Production source registry before this integration: ISD's Ukraine FAQ, International Protection Act 2026 §237, consolidated EU Decision 2026/1912, Designated Accommodation Centres, and Additional Needs Payment. Existing records remain attached to their original owners. The new drafts' records use their own deterministic owner-specific IDs; no existing source ID was reused. This includes the Additional Needs Payment URL already owned by `IE-SOC-01`.

## Citation integrity

- Existing Ireland citation tokens: **216**; all remain resolvable.
- New citation tokens: **123**.
- Total across Ireland's 8 articles and 24 translations: **339**.
- Every token parses, resolves to an existing source owned by the citing article, and uses no draft-only key. All 106 Ireland-owned source records are cited; no orphan source or cross-owner binding was found.
- Article and source UUIDs are unique in their catalogs. The three article UUIDs and all 36 new source UUIDs reproduce the deterministic draft IDs.

## Full parity

- Articles: **8/8** present.
- Translations: **24/24** exact title, summary and content parity with the eight approved article drafts.
- New article relationships: correct Ireland country IDs and `temporary-protection` / `housing` category IDs.

## Status and visibility

- Ireland translations: **24 NeedsReview / 0 Verified**.
- Production: **hidden**. Ireland is absent from Production home and search selectors; the overview and category route remain unavailable, and Production search returns no Ireland articles.
- Development: **visible** in EN / UK / RU. The overview and categories are available; Ireland search returns **8 results** in each language.
- Development category counts: `temporary-protection` 2; `employment` 2; `healthcare` 1; `housing` 1; `education` 1; `social-assistance` 1.

## Compare hold

Compare is unchanged: **18 countries / 432 facts**. Ireland has **0 Compare facts** and is absent from the supported comparison country map.

## Evidence ceilings

**4/4 preserved** in the integrated copy:

1. `IE-TP-01`: no invented universal in-country §237 first-application route; no automatic Irish permission or document validity through 2028.
2. `IE-TP-02`: no automatic conversion, universal in-country filing, universal pending stay/work rights, or automatic permanent-residence credit.
3. `IE-HOUSING-01` initial State accommodation: no guaranteed placement, 30-day stay, or fallback when capacity is unavailable.
4. `IE-HOUSING-01` ongoing housing support: no automatic social housing, HAP, Rent Supplement, or other housing assistance.

## Catalog totals and checks

| Measure | Result |
| --- | ---: |
| Global information entries / articles | 152 |
| Global translations | 456 |
| Global official sources | 1,462 |
| Global Verified / NeedsReview / Outdated | 429 / 27 / 0 |
| Ireland articles / translations | 8 / 24 |
| Ireland source records / unique HTTPS URLs | 106 / 96 |
| Ireland citation tokens | 339, all valid |
| Ireland draft/catalog parity | 24/24 |
| Targeted `IrelandContentIntegrationTests` | 7/7 passed |
| JSON, UUID, ownership, relationship and citation checks | PASS |
| Production visibility hold / Development visibility | PASS / PASS |
| Compare hold | PASS; 18 countries / 432 facts / 0 Ireland facts |

## Verdict

**READY FOR FINAL CORE REVIEW**
