# Belgium Production Readiness Review 01

**Review date:** 14 November 2026
**Scope:** Belgium Core V1 drafts and their Development catalog parity, using the existing post-correction editorial review, the three-gap assessment, the 14 November targeted research, the relevant focused research packages, current catalog data, and `BelgiumContentIntegrationTests.cs`. No new web research was conducted and no Production data was changed.

## Current state

- Country: Belgium — `10000000-0000-0000-0000-000000000017` (`belgium`).
- Core owner articles: 8; localized translations: 24 (EN / UK / RU); all current catalog translation statuses are `NeedsReview`.
- Articles and categories:

| Article | Article ID | Category | Compare aspects | Production source records |
|---|---|---|---|---:|
| `BE-TP-01` — Temporary Protection Application | `aef6ec65-f8ed-5054-bafb-c465e790dd57` | `temporary-protection` | `protection-scheme`, `protection-application`, `protection-dates` | 10 |
| `BE-TP-02` — Residence After Protection | `54c6f95f-589e-5901-beae-990bed994160` | `temporary-protection` | `residence-routes`, `residence-conditions`, `residence-filing` | 16 |
| `BE-EMP-01` — Finding Employment | `f2c1526a-a218-5bf0-ab2a-9167dbec0c26` | `employment` | `employment-vacancies`, `employment-institutions`, `employment-assistance-conditions` | 17 |
| `BE-EMP-02` — Work Authorization and Rights | `55fa79e5-89a2-518b-90b2-85a2735f171b` | `employment` | `work-permission`, `work-employer-duties`, `work-minimum-pay` | 22 |
| `BE-HEALTH-01` — Healthcare | `673316a9-6ee9-50d6-9f66-e19659aeff0e` | `healthcare` | `healthcare-access`, `healthcare-proof`, `healthcare-urgent` | 13 |
| `BE-HOUSING-01` — Housing | `3646ae19-215c-5f0e-9075-85ef5bae88f7` | `housing` | `housing-initial`, `housing-private-rental`, `housing-financial-support` | 23 |
| `BE-EDU-01` — Education and Language | `ac80dcbd-4289-5c77-8340-f9e4226a47f6` | `education` | `education-school-enrolment`, `education-pupil-language`, `education-higher-admission` | 25 |
| `BE-SOC-01` — Social Assistance | `20eef3c3-fcc7-5b5b-b672-695f29faad61` | `social-assistance` | `social-subsistence`, `social-application`, `social-child-benefits` | 20 |

The eight articles cover all 24 expected aspects exactly once. The catalog and integration test show Belgium visible in Development and hidden from Production and Compare. Existing aggregate review findings remain applicable: no Production promotion or Compare inclusion is approved by this review.

## Article review

`PASS` means no draft correction was needed in this review. `REVISED` means the English draft and both translations were corrected together; catalog parity is consequently pending for those six language records. `BLOCKED` is reserved for a material evidence gap that prevents safe draft wording.

| Article | EN | UK | RU | Sources | Corrections / review result | Production parity after edits |
|---|---|---|---|---:|---|---|
| `BE-TP-01` | REVISED | REVISED | REVISED | 10 existing | Added current federal intake mechanics and capacity qualification; added the 2027 municipality renewal instructions and made the legacy 2025 inconsistency explicit. Kept the EU scheme endpoint separate from individual A-card validity. | All 3 translations are outdated in catalog pending parity sync. One new official URL needs a `BE-TP-01` source record: the Temporary Protection page; the existing Registration Centre record remains bound for the address and hours. |
| `BE-TP-02` | REVISED | REVISED | REVISED | 16 existing | Added the qualified federal Ukraine long-stay filing route through the municipality for work, study or family reunification; retained route-specific conditions and explicit limits for TP holders and pending rights. | All 3 translations are outdated in catalog pending parity sync. One new official URL needs a `BE-TP-02` source record: Long Stay page. |
| `BE-EMP-01` | PASS | PASS | PASS | 17 | Existing regional employment services and their eligibility boundaries remain scoped; no national service or automatic assistance is implied. | Exact match, 3/3. |
| `BE-EMP-02` | PASS | PASS | PASS | 22 | Existing work-permission, employer-duty, regulated-work and wage qualifications remain intact; no general right to every job or universal minimum wage is implied. | Exact match, 3/3. |
| `BE-HEALTH-01` | PASS | PASS | PASS | 13 | Existing insurance affiliation, proof, urgent-care and reimbursement distinctions remain qualified; no automatic membership or universally free care is promised. | Exact match, 3/3. |
| `BE-HOUSING-01` | PASS | PASS | PASS | 23 | Existing federal/regional housing boundaries and capacity qualifications remain explicit; no guaranteed accommodation, social housing or allowance is promised. | Exact match, 3/3. |
| `BE-EDU-01` | PASS | PASS | PASS | 25 | Community competence and distinctions between school access, placement, language support and higher-education admission/fees remain explicit. | Exact match, 3/3. |
| `BE-SOC-01` | PASS | PASS | PASS | 20 | CPAS/OCMW assessment, subsistence support and the separate regional family-benefit systems remain distinct and conditional. | Exact match, 3/3. |

**Article counts:** 6 PASS, 2 REVISED, 0 BLOCKED.
**Translation counts:** 18 PASS, 6 REVISED, 0 BLOCKED. The revised UK and RU wording tracks the EN changes, including certainty, dates, conditions and caveats.

## Targeted-gap review

### `protection-application` — READY WITH QUALIFICATION

The current federal Immigration Office instructions say to appear in person at the Registration Centre and bring available identity and eligibility evidence. Registration records identity and fingerprints and copies available eligibility documents. Where capacity prevents processing everyone, the Office may ask some applicants to return. The federal TP page describes issuance of the certificate if criteria are met, followed by the municipality step for the Temporary A card; the Registration Centre page supplies the current address and opening time.

The revised article states these bounded steps in EN / UK / RU. It does not invent an appointment or walk-in rule, a complete checklist, a no-document route, or guaranteed same-day processing. It preserves the federal decision/certificate versus municipal card distinction.

### `protection-dates` — READY WITH QUALIFICATION

Decision (EU) 2026/1912 extends the EU scheme to **4 March 2028**. Separate Immigration Office guidance says A-card renewal may be requested through the municipality from **4 January 2027**, and describes the renewed card as valid to 4 March 2028 plus interim registration evidence if a new card is not issued before 4 March 2027. The same federal page retains a stale/inconsistent reference to an A card valid until 4 March 2025.

The revised article keeps the EU scheme end date distinct from an individual's card validity, presents renewal as the published municipal process rather than automatic extension, and flags the conflicting legacy date. Readers are directed to check their card and municipality. The individual holder's expiry/cohort-specific steps remain for local confirmation.

### `residence-filing` — READY WITH QUALIFICATION

Federal Ukraine guidance says Ukrainian citizens who left Ukraine and are already in Belgium may apply for a long-stay residence permit through their municipality for work, study or family reunification, subject to the selected route's conditions. The page does not establish that every TP holder can use every in-country route, or that filing preserves residence or work rights while pending. Existing law and focused research continue to show route-specific procedures and competent authorities.

The revised article states the route narrowly and preserves employer/regional work steps, admission and timing conditions for study, defined family-route exceptions, and the absence of a universal pending-status bridge. It makes no automatic conversion claim.

## Cross-language review

All 8 drafts contain EN / UK / RU Title, Summary and Content sections. Existing post-correction review records material equivalence for the six unchanged articles. The two revised articles were updated in all three languages in parallel. Dates, address/time, document names, named authorities, legal status, conditions, exceptions, and certainty level align. No `may` / `can` guarantee expansion or omission of a material qualification was found.

The live catalog currently exactly matches 18/24 translations. The six mismatches are the revised EN / UK / RU entries for `BE-TP-01` and `BE-TP-02`; they are expected until parity sync and are not translation defects in the drafts.

## Regional-scope review

The six unchanged articles retain the prior focused-review conclusions:

- Employment services remain identified separately for Flanders, Brussels, Wallonia and the German-speaking Community; regional work authorization and support requirements are not generalized nationally.
- Healthcare coverage remains tied to the stated insurer/aid route and evidence; urgent-care and reimbursement conditions are not presented as automatic.
- Housing keeps regional rental/deposit and social-housing competence distinct from federal arrival support; placement remains capacity- and assessment-dependent.
- Education names the Flemish, French and German-speaking Communities and preserves Brussels' language-system choice; school placement and newcomer-language support are not guaranteed, and higher education is treated separately.
- Social assistance distinguishes CPAS/OCMW decisions from unemployment and from the four regional family-benefit systems.

No new national generalization or translation drift was found in these regional claims.

## Source integrity

- Current registry: **146 Belgium source records**, **132 unique URLs**; global registry: **1,424 source records**. The existing review/test confirms globally unique source IDs, HTTPS URLs, owner-bound records, and article-owned citation references.
- The 5 official URLs used for the 3 targeted gaps resolve as follows:

| Official URL | Current owner binding | Review disposition |
|---|---|---|
| `https://dofi.ibz.be/en/themes/ukraine/registration-centre` | Existing `BE-TP-01` record; same official page also has a separate `BE-HOUSING-01` record for its own claim | Reuse existing `BE-TP-01` record for address and hours. |
| `https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX:02026D1912-20260804` | Existing `BE-TP-01` and `BE-TP-02` records | Reuse; supports the EU scheme period, not individual card expiry. |
| `https://www.ejustice.just.fgov.be/eli/loi/1980/12/15/1980121550/justel` | Existing `BE-TP-01` and `BE-TP-02` records, plus route-specific `BE-TP-02` records | Reuse for the statutory route boundaries. |
| `https://dofi.ibz.be/en/themes/ukraine/temporary-protection` | No current registry record | **New owner-specific record required for `BE-TP-01`** to bind the corrected intake and A-card renewal claims. Authority: Belgian Immigration Office / FPS Home Affairs; title: “Temporary Protection”; scope: federal Ukraine TP registration and Belgian card guidance; checked 14 Nov 2026. Preserve the source's appointment/document limitations and stale 2025 wording in the article. |
| `https://dofi.ibz.be/en/themes/ukraine/long-stay-more-90-days` | No current registry record | **New owner-specific record required for `BE-TP-02`** to bind the qualified in-country filing claim. Authority: Belgian Immigration Office / FPS Home Affairs; title: “Long stay [more than 90 days]”; scope: Ukrainian citizens already in Belgium applying via municipality; checked 14 Nov 2026. The page has no displayed current revision date and does not confirm universal TP-holder eligibility or pending rights. |

The current registry has 3 of these 5 URLs; the two new URLs require **2 owner-specific records**. No UUIDs or production source records were created. Existing source tokens in all 24 draft language sections resolve to sources owned by the matching article; the revised claims are linked directly to the two official pages and need their owner-bound source records during parity sync.

## Production parity

- Before the two targeted corrections, the 24 draft translations matched the Development catalog exactly, as recorded by the prior review and integration test.
- Current exact matches: **18/24** (all six unchanged articles).
- Outdated catalog entries: **6/24** (EN / UK / RU for `BE-TP-01` and `BE-TP-02`). Sync their Title, Summary and Content from the revised drafts.
- Source records currently remain 146 / 132 URLs. Add the two owner-bound official source records above during the sync and bind the corresponding claims. Do not reuse another article's owner binding.
- All 24 catalog statuses remain `NeedsReview`; Belgium remains Development-only and excluded from Production/Compare. This review does not authorize visibility or status changes.

## Remaining issues

1. Parity sync is required for the six revised catalog translations and two new owner-specific source records.
2. The federal TP page's stale 4 March 2025 card reference conflicts with its 2027 renewal guidance. The draft safely flags this; an individual's card expiry and applicable steps still require municipality confirmation.
3. The long-stay page supports a qualified route for Ukrainian citizens already in Belgium, but does not establish that every TP holder qualifies for every route or has rights preserved while an application is pending. The draft excludes those broader claims.
4. Existing review status remains `NeedsReview`, and Belgium remains hidden from Production and Compare. Neither was changed.

These limits do not block the corrected editorial text; they must remain visible through parity sync and any later promotion review.

## Verdict

**READY FOR PRODUCTION PARITY SYNC**

The eight draft articles are safe at the reviewed scope: 6 PASS and 2 REVISED, with 24/24 language versions aligned and no remaining draft-level blocker. Production promotion is not approved because six catalog translations are stale and two owner-specific source records remain to be added and bound. Keep all current `NeedsReview` statuses and Development-only visibility unchanged until the separate parity and promotion steps are completed.
