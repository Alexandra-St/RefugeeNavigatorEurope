# Austria v1 Post-Correction Editorial Review

**Baseline:** `6be1a191f5df7ef5275cfbcb0021c1ccf6a4a20c`
**Review date:** 2026-11-08
**Previous audit:** `EDITORIAL_ACCEPTANCE_PREPARATION.md` (unchanged)
**Scope:** Recheck C-01 and C-02, the affected Austria catalog translations/source, Austria fact immutability and the requested state invariants. No new web research was performed. The full test suite was not rerun; the correction baseline records 623/623 passing.

## Review verdict

**NEEDS FURTHER CORRECTION**

C-01 is resolved. C-02 is only partially resolved: the amendment citation remains in the registration paragraph in EN and UK, although it was removed in RU. The BMI registration citation remains in all three languages, and the amendment remains appropriately cited in the following military-rule paragraph. No other new defect from the correction commit was identified.

This is a review recommendation only. It is not owner acceptance, promotion, or a status change. No deployment was performed.

## 1. Correction resolution matrix

| Finding | Result | Post-correction basis |
| --- | --- | --- |
| C-01 — `AT-TP-02`, opening paragraph | **Resolved** | The new article-owned `Austria RIS, AsylG §62` source is `c4d20d35-d2e7-53c8-af33-d017a61f9702`, owned by article `dc76be21-d9dc-5070-83ca-9a7468d78e71`. Its metadata matches research source `AT-RES-S11`: URL `https://www.ris.bka.gv.at/Dokumente/Bundesnormen/NOR40262632/NOR40262632.pdf`; `LastCheckedAt: 2026-11-07T00:00:00+00:00`. EN/UK/RU cite it directly for the continuing temporary right. NAG §41a and VertriebenenVO §4a route sources support the no-automatic-conversion claim. NAG §21 remains on filing consequences. The claim wording and qualifications were not expanded. Catalog content exactly matches the updated draft in all three languages. |
| C-02 — `AT-TP-01`, registration/document paragraph | **Not resolved** | BMI source `ea5185f7-d875-5fa7-b4bc-1da82628c315` remains in the registration paragraph in EN/UK/RU. The unsupported BGBl II 304/2026 citation `aee98c48-02d0-51fa-8566-aa260901b80d` is absent there in RU, but still present in EN and UK. It remains correctly cited in the following military-rule paragraph in all three languages. The source set remains valid and all entries are still used. Catalog EN/UK/RU text exactly matches the draft. Remove only the remaining EN and UK amendment citations from the registration paragraph; retain the military-paragraph citations. |

## 2. Article review

The previous audit found the six non-TP articles Ready. The correction commit did not change their drafts or catalog records. The two affected articles were reassessed from their current drafts and catalog translations.

| Draft | Post-correction result | Basis |
| --- | --- | --- |
| `AT-TP-01` | **Correction required** | C-02 remains in EN and UK registration paragraphs. RU is corrected. BMI registration evidence is retained in all languages; the amendment remains correctly cited in the military-rule paragraph. Catalog parity is exact. |
| `AT-TP-02` | **Ready** | C-01's temporary-right claim now cites AsylG §62; no-automatic-conversion has route evidence; NAG §21 is attached to filing effects. No claim wording was broadened, and catalog parity is exact. |
| `AT-EMP-01` | **Ready** | Unchanged by the correction commit; previous audit result remains applicable. |
| `AT-EMP-02` | **Ready** | Unchanged by the correction commit; previous audit result remains applicable. |
| `AT-HEALTH-01` | **Ready** | Unchanged by the correction commit; previous audit result remains applicable. |
| `AT-HOUSING-01` | **Ready** | Unchanged by the correction commit; previous audit result remains applicable. |
| `AT-EDU-01` | **Ready** | Unchanged by the correction commit; previous audit result remains applicable. |
| `AT-SOC-01` | **Ready** | Unchanged by the correction commit; previous audit result remains applicable. |

**Article results:** Ready 7/8; Minor correction 0/8; Correction required 1/8.
**Draft/catalog parity:** exact for the two affected articles in EN/UK/RU; the other six were unchanged from the previous exact-parity audit.
**Translation parity issues:** 0. The remaining C-02 issue is citation placement, not a translation difference in claim meaning.

## 3. Source state

| Check | Result |
| --- | --- |
| `AT-TP-01` article-owned sources | 6 |
| `AT-TP-02` article-owned sources | 8, including the new AsylG §62 source |
| Austria sources total | 57 |
| Global `OfficialSources` | 723 |
| AsylG §62 ownership and metadata | Correct article owner; title, URL and check timestamp match `AT-RES-S11` |
| Austria source URLs | 57/57 HTTPS |
| Cross-article Austria UUID conflicts | 0 |
| Unresolved inline citations | 0 |
| Unused Austria source-set entries | 0 |

The existing 56 source entries were confirmed used in the previous audit. The new source is cited by the opening paragraph of `AT-TP-02` in EN/UK/RU. The BGBl amendment source remains used by the military-rule paragraphs; its residual EN/UK citation in the registration paragraphs does not create a dangling or unused source.

## 4. Comparison facts

All 24 Austria facts remain present, `Draft`, and `IsAvailable=true`. Their values and source arrays were not changed by correction commit `6be1a191...` (the commit diff contains no `comparison-facts.json` change). All listed source IDs still resolve to sources owned by the linked article. The previous audit classified all 24 facts Ready, and this citation-only correction does not change that assessment.

**Fact results:** Ready 24/24; Minor correction 0; Correction required 0.

## 5. Critical boundaries

The correction commit changed citation placement and added one source record; it did not alter claim wording or comparison facts. The prior audit's substantive review remains applicable, and the following boundaries are preserved:

- TP military transition conflict of 30 July / 4 August / 5 August 2026 remains unresolved.
- Special RWR+ under §41a(7b) remains distinct from §4a routes.
- Lawful-stay bridge remains distinct from work authorization.
- No automatic residence conversion is claimed.
- Austria is not described as having a universal statutory minimum wage.
- Automatic Ukraine-specific health insurance ended 31 May 2025.
- `Grundversorgung` remains conditional, needs-based and provincial; no accommodation or payment is guaranteed.
- Ukrainian online schooling is not described as replacing `Schulpflicht`.
- Tuition relief is not represented as a universal TP benefit.
- The current `Familienbeihilfe` rule from 1 July 2026 and its qualifications remain intact.

## 6. State invariants

| Measure | Current state |
| --- | ---: |
| Austria articles | 8 |
| Austria translations | 24; all `NeedsReview` |
| Austria sources | 57 |
| Austria facts | 24; all `Draft`, all available |
| Austria Development visibility | Visible |
| Austria Production visibility | Hidden |
| Global countries | 11 |
| Global articles | 88 |
| Global translations | 264 |
| Global `OfficialSources` | 723 |
| Global comparison facts | 264 |

Existing holds remain unchanged: Netherlands has 3 `NeedsReview` translations and 4 `Draft` facts. Sweden `social-child-benefits` remains `Draft`, unavailable and source-less.

## 7. Final findings and recommendation

| Item | Issue | Required correction | Severity |
| --- | --- | --- | --- |
| C-02 / `AT-TP-01` EN and UK | The registration paragraph still cites the 2026 amendment, which does not support its administrative registration/checklist claims. | Remove the amendment citation from the EN and UK registration paragraphs only. Preserve the BMI citation there and the amendment citation in the military-rule paragraph. | correction |

**Unresolved findings:** 0 blockers; 1 correction; 0 minor corrections.

**NEEDS FURTHER CORRECTION.** Resolve the remaining EN and UK C-02 citation placements, then conduct owner review. This report does not make that correction or perform acceptance/promotion.
