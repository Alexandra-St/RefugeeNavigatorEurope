# Belgium v1 — Post-correction editorial review

**Review date:** 12 November 2026
**Baseline:** `9a833ad79c0ae4f9ffa10a0d0397ed6c94d4d3d8`
**Content correction commit:** `9a833ad79c0ae4f9ffa10a0d0397ed6c94d4d3d8`
**Scope:** The eight Belgium v1 drafts, the initial editorial review, Belgium aggregate research readiness and all eight matching focused research packages. Finland's post-correction review was used only as a structural reference. No external research was used; no draft content was changed for this review.

## Recommendation

**READY FOR DEVELOPMENT-ONLY CATALOG INTEGRATION.** The single valid correction to BE-EDU-01 is resolved across EN / UK / RU. The proposed RU source-label change in the initial review was based on a source-mapping error and was correctly not applied; the existing labels accurately describe BE-EDU-S11. All eight articles pass, with clean source ownership, citations and material translation parity.

This permits only Development-only catalog integration with all translations remaining `NeedsReview`. It does not approve Production visibility, `Verified` translations, Belgium Compare support or comparison facts/placeholders. The three research blockers remain unresolved, and Belgium must remain excluded from Compare while any of them is blocked. No catalog integration was performed in this review.

## Correction verification

### 1. BE-EDU-01 — pending-TP school access — Resolved

EN, UK and RU now explicitly say that a child whose Belgian temporary-protection application is still pending may request school enrolment. Each version preserves the same boundary: school access generally does not require waiting for an A card or the final TP decision. Each also says that a preferred school or immediate place is not guaranteed and that the school or education authority determines the child's class/year, potentially assessing age, prior study, language and current level.

This is supported by the education package, which states that a child's basic school-access right is based on being a child in Belgium, not on a completed TP decision, and that pending TP does not guarantee a preferred seat or grade. The paragraph introduces no immigration status, immigration entitlement, guaranteed placement, new document requirement or school-admission outcome. Existing citations and document/capacity qualifications remain intact.

### 2. Initial-review source-mapping finding — Resolved as review error

The UUID `914453bb-89bf-5ff2-96e8-0174c35a06a5` belongs to `BE-EDU-01-S11`, which maps to research source `BE-EDU-S11`: the German-speaking Community's 21 October 2026 decision appointing support councils for first-arrival pupils. It is not `BE-EDU-S12`. `BE-EDU-01-S12`, UUID `33ad0f2a-4e8d-5dfd-9f12-b3af21e08074`, maps separately to `BE-EDU-S12`, the 2025 decree covering first-arrival enrolment rules.

The current EN / UK / RU labels for S11 describe DG first-arrival school/enrolment support. That is materially accurate for the source's appointment of first-arrival pupil support councils; a literal translation of its formal title is not required. The labels were correctly left unchanged. No source-label correction is required.

## Article disposition

| Draft | Final disposition | Result |
|---|---|---|
| `BE-TP-01` | **PASS** | Intake guidance remains bounded; 4 March 2028 is the EU scheme endpoint only, distinct from individual Belgian A-card expiry and renewal mechanics. |
| `BE-TP-02` | **PASS** | Residence routes remain conditional and route-specific; no universal in-country filing or pending-status bridge is promised. |
| `BE-EMP-01` | **PASS** | Four regional services remain distinct; portal use, jobseeker registration, work permission and unemployment benefits are separate. |
| `BE-EMP-02` | **PASS** | Only granted-TP employee work evidence and qualifying Annex 15 are described; RMMMG is scoped to date, age and CCT 43. |
| `BE-HEALTH-01` | **PASS** | TP, insurer affiliation, CAAMI/INAMI proof, AMU/DMH, emergency services, costs and BIM remain qualified. |
| `BE-HOUSING-01` | **PASS** | Placement/capacity are not guaranteed; regional rental and deposit rules are distinct from advance rent, address and social-housing allocation. |
| `BE-EDU-01` | **PASS** | Pending-TP school access is explicit and bounded; Community routes, placement, language support and higher-education fee limits remain qualified. |
| `BE-SOC-01` | **PASS** | TP-equivalent assistance is distinguished from RI/leefloon; CPAS assessment, reference rates and four family-benefit systems remain qualified. |

**Disposition counts: 8 PASS, 0 CORRECTION, 0 BLOCKER.** The initial review's phrase “Two drafts need bounded corrections” was an inaccurate summary: its table and final disposition identified one correction draft, BE-EDU-01, with two proposed items. Only the pending-application school-access clarification was a valid correction; the source-label item was caused by the S11/S12 mapping error described above.

## Metadata, source ownership and citation audit

| Draft | Article ID | Source entries |
|---|---|---:|
| `BE-TP-01` | `aef6ec65-f8ed-5054-bafb-c465e790dd57` | 10 |
| `BE-TP-02` | `54c6f95f-589e-5901-beae-990bed994160` | 16 |
| `BE-EMP-01` | `f2c1526a-a218-5bf0-ab2a-9167dbec0c26` | 17 |
| `BE-EMP-02` | `55fa79e5-89a2-518b-90b2-85a2735f171b` | 22 |
| `BE-HEALTH-01` | `673316a9-6ee9-50d6-9f66-e19659aeff0e` | 13 |
| `BE-HOUSING-01` | `3646ae19-215c-5f0e-9075-85ef5bae88f7` | 23 |
| `BE-EDU-01` | `ac80dcbd-4289-5c77-8340-f9e4226a47f6` | 25 |
| `BE-SOC-01` | `20eef3c3-fcc7-5b5b-b672-695f29faad61` | 20 |
| **Total** | **8 unique Article IDs** | **146** |

All Article IDs are unique UUIDv5 values and match the expected IDs. All translations remain `NeedsReview` (EN / UK / RU). All 146 source UUIDs are UUIDv5 values reproduced deterministically from the owning Article ID and exact local source key. All local keys belong to their article; source UUIDs are globally unique, with **0 shared** across articles.

Each source entry was matched to its corresponding article's focused package. Research IDs, authorities, titles and exact inventory URLs match; all cited URLs use HTTPS and point to the official or public authority recorded in the package. No unofficial or cross-topic source borrowing was found. The education correction added or removed no source entry or citation UUID. The erroneous S11/S12 assertion in the initial review did not change source ownership.

Current citation-token counts across all three language sections:

| Draft | EN | UK | RU | Total |
|---|---:|---:|---:|---:|
| `BE-TP-01` | 15 | 15 | 15 | 45 |
| `BE-TP-02` | 18 | 18 | 18 | 54 |
| `BE-EMP-01` | 17 | 17 | 17 | 51 |
| `BE-EMP-02` | 22 | 22 | 22 | 66 |
| `BE-HEALTH-01` | 14 | 14 | 14 | 42 |
| `BE-HOUSING-01` | 23 | 23 | 23 | 69 |
| `BE-EDU-01` | 25 | 25 | 25 | 75 |
| `BE-SOC-01` | 23 | 23 | 23 | 69 |
| **Total** | **157** | **157** | **157** | **471** |

Citation sets match their article-owned source entries in every language. **Orphan citation references: 0. Unused source entries: 0.** No public inline `BE-…-Sxx` research-ID citations were found.

## Translation parity audit

All eight drafts have EN / UK / RU sections, remain `NeedsReview`, and use the same article-owned citation set in all three languages. The material boundaries were compared across the current language versions. No material translation difference was found in eligibility, blocked-area cautions, document status, dates, amounts, regional or Community scope, application routes, or conditional outcomes.

For corrected BE-EDU-01, all three versions say that a child with a pending Belgian TP application may request school enrolment; generally need not wait for an A card or final TP decision; is not promised a preferred school or immediate place; and receives class/year placement through the school or education authority. No language version broadens the entitlement or omits the placement/capacity caveat.

The same material parity check found the EU-only 4 March 2028 endpoint and separate A-card warning; conditional residence transition; jobseeker-registration versus work-permission distinction; €2,189.81 RMMMG framework; CAAMI/INAMI qualification; region-specific housing deposits; Community-specific education and fee boundaries; CPAS RI reference rates; and four distinct regional child-benefit systems aligned across EN / UK / RU.

## Cross-article and blocked-area audit

### Blocked-aspect leakage

- **`protection-application`: no leakage.** BE-TP-01 retains the supported Immigration Office location, weekday 08:30 appearance and high-level federal-to-municipal sequence, while saying appointment/walk-in rules and a complete no-document route are not established. It does not promise online pre-registration, exhaustive document lists or substitutes, a complete child/guardian procedure, interpreter entitlement, processing time, or an exact prior-other-EU-TP transfer process.
- **`protection-dates`: no leakage.** BE-TP-01 states 4 March 2028 only as the EU scheme endpoint; it distinguishes that from individual Belgian A-card expiry, directs readers to the printed date/current instructions and does not assert automatic extension, no holder action, universal replacement or 2027–2028 card mechanics.
- **`residence-filing`: no leakage.** BE-TP-02 keeps filing conditional on each route, does not promise continued lawful stay or work while an ordinary application is pending after TP, and invents no interim document, automatic TP/ordinary-permit coexistence or termination, post-2028 conversion, or confirmed concurrent Blue Card transition.

All three aspects remain blocked in aggregate research readiness. The constrained text does not resolve them.

### Cross-system document and topic consistency

- **Federal TP attestation:** Used only for the supported INAMI affiliation route and CPAS application after municipal presentation. It is not an A card, sufficient employee work proof, completed insurer membership or family-benefit approval.
- **Annex 15:** Remains context-specific. Only qualifying TP-related cases are used for employee work proof; healthcare, CPAS and employment-service contexts retain their own accepted-document limits. The drafts do not assign every Annex 15 the same effect.
- **A card:** Treated as residence evidence. It is not insurer membership, a CPAS award or a child-benefit award, and it is not a general prerequisite for school access.
- **Employment:** BE-EMP-01 names VDAB, Actiris, Le Forem and Arbeitsamt/ADG. Portal account/use is separate from formal registration; registration is separate from work permission and unemployment benefits. Pending TP is not work authorization; a federal attestation alone is not sufficient employee work proof; only a qualifying TP Annex 15 is included. RMMMG remains **€2,189.81 gross/month from 1 April 2026**, under the covered CCT 43 framework for workers aged 18+, an average monthly guarantee rather than a universal hourly or net wage, with higher sector minima possible.
- **Healthcare:** TP is not insurer membership. INAMI's attestation route and CAAMI's A-card checklist discrepancy remain visible. An A card or Annex 15 is not an insurance card. Ordinary insurance is separate from AMU/DMH; 112 is separate from 1733, whose local service availability is qualified. Costs remain qualified and BIM is not automatic.
- **Housing:** No Fedasil bed, long-term accommodation or capacity is guaranteed. Regional schemes remain regional; a reference address is not housing; CPAS support is individual; a social-housing application is not allocation. Deposits remain Flanders up to 3 months; Brussels 2 months under the applicable regime for leases concluded/renewed from 1 November 2024; Wallonia/DG 2-month blocked account or bank guarantee up to 3 months. No universal advance-rent cap is stated.
- **Education:** Flemish, FWB and DG systems remain distinct. A card is not a general school prerequisite and access is not a seat guarantee. OKAN / taalheldklas, DASPA and DG language support are not conflated; no nationwide Ukrainian-language or interpreter guarantee is made; Ukrainian online school is not an automatic substitute. Higher-education treatment remains distinct: Flemish protected increased-fee treatment is not free tuition; FWB EU assimilation for the additional contribution does not waive base minerval; no general DG TP fee exemption is claimed. Diploma/credit recognition is not automatic.
- **Social assistance:** Equivalent TP financial social assistance is not statutory RI/leefloon. The supported filing route can begin before an A card after TP grant and municipal presentation; pending TP is not general cash entitlement. CPAS competence and means tests are individual, and unemployment is separate. The four regional family-benefit systems remain distinct. Rates effective 1 September 2026—**€911.56, €1,367.34 and €1,847.89**—are reference amounts, not guaranteed TP awards. The 70%-to-100% proposal remains expressly not enacted.

No cross-article contradiction or blocked-aspect leakage was identified.

## Remaining research blockers

The Belgium aggregate research readiness file still has exactly three blocked canonical aspects:

- `protection-application`
- `protection-dates`
- `residence-filing`

They remain unresolved and continue to exclude Belgium from Compare.

## Final disposition

- **PASS:** BE-TP-01, BE-TP-02, BE-EMP-01, BE-EMP-02, BE-HEALTH-01, BE-HOUSING-01, BE-EDU-01, BE-SOC-01.
- **CORRECTION:** none.
- **BLOCKER:** none at article level; the three aggregate research blockers above remain.
- **Exact article disposition:** 8 PASS, 0 CORRECTION, 0 BLOCKER.
- **Overall recommendation:** **READY FOR DEVELOPMENT-ONLY CATALOG INTEGRATION.**
- **Allowed next step:** Development-only catalog integration as `NeedsReview`.
- **Not approved:** Production visibility, `Verified` status, Belgium Compare support, or comparison facts/placeholders. Keep Belgium excluded from Compare while `protection-application`, `protection-dates` or `residence-filing` is blocked.
- No catalog integration, country addition, comparison, Production change, translation verification or deployment was performed.

## Checks

- Confirmed the baseline and content-correction commit both equal `9a833ad79c0ae4f9ffa10a0d0397ed6c94d4d3d8`.
- Verified all eight expected Article IDs, `NeedsReview` translation states, per-article source counts, deterministic UUIDv5 ownership, global UUID uniqueness, package/source metadata matches and EN / UK / RU citation parity.
- Verified 471 current inline citation tokens, 0 orphan citation references, 0 unused source entries and no inline research-source IDs.
- Reviewed the current drafts against the eight focused packages and aggregate readiness; no external research or repository/history scan was used.
- `git diff --check` passed.
- Documentation-only review; no `dotnet test` was required.
- Only `docs/content-drafts/belgium-v1/POST_CORRECTION_EDITORIAL_REVIEW.md` was created. No draft, initial review, research, catalog, runtime, localization, test or deployment file was changed.
