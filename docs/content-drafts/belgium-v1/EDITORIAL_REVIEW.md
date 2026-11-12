# Belgium v1 — Editorial review

**Review date:** 12 November 2026
**Baseline:** `000838e69f84ae32c3028a9942595c2ff2d2ce01`
**Scope:** Exactly the eight Belgium v1 drafts, their eight matching focused research packages, the Belgium aggregate readiness file, and the Finland editorial review solely for structure/severity/audit depth. No external research or repository scan was performed. No draft content was changed.

## Recommendation

**READY FOR CORRECTION PASS.** Two drafts need bounded corrections supported by their existing focused packages. No article-level blocker remains after those corrections; the three research aspects `protection-application`, `protection-dates` and `residence-filing` remain blocked and must not be upgraded. This review does not authorize catalog integration while corrections remain. Belgium remains excluded from Compare. All eight drafts remain `NeedsReview`.

## Article review

| Draft | Review | Source UUIDs | Coverage and editorial boundaries |
|---|---:|---:|---|
| `BE-TP-01.md` | **PASS** | 10 | States the federal centre, weekday 08:30 arrival guidance and supported high-level sequence while disclosing missing appointment, document-list and no-document rules. Separates the EU 4 March 2028 endpoint from individual A-card validity and gives no 2027–2028 renewal instruction. |
| `BE-TP-02.md` | **PASS** | 16 | Maps work, study, family and self-employment routes with their conditions. Does not give universal in-country filing or pending-stay/work assurances; Blue Card and post-2028 transition remain constrained. |
| `BE-EMP-01.md` | **PASS** | 17 | Keeps the four regional services, vacancy search, formal registration, work authorization and unemployment benefits distinct. Regional document differences and uncertain address/BIS cases are qualified. |
| `BE-EMP-02.md` | **PASS** | 22 | Limits employee work proof to an A card or qualifying TP-related Annex 15 after grant; distinguishes self-employment. RMMMG is correctly scoped by date, age and CCT 43, and is not presented as an hourly or simple base-wage minimum. |
| `BE-HEALTH-01.md` | **PASS** | 13 | Separates TP status, insurer affiliation, reimbursement, emergency response and CPAS medical aid. Preserves the INAMI/CAAMI document-list discrepancy, context-specific Annex 15 use, 112/1733 distinction and cost/BIM qualifications. |
| `BE-HOUSING-01.md` | **PASS** | 23 | Treats Fedasil and regional placements as capacity-dependent; distinguishes lease, address registration, deposit and advance rent. Deposit limits are region-specific; no universal advance-rent limit or automatic CPAS support is claimed. |
| `BE-EDU-01.md` | **CORRECTION** | 25 | Community-specific school, language and higher-education routes are well bounded, and no seat, interpreter or nationwide fee waiver is promised. The school-access paragraph says access generally does not wait for an A card or final TP decision, but it does not explicitly identify children with a pending TP application as within the supported school-access route. The same omission appears in EN / UK / RU. One Russian citation label also says `Support for first enrolment in DG` while its source is DG newcomer enrolment rules; EN and UK labels identify enrolment rules. |
| `BE-SOC-01.md` | **PASS** | 20 | Distinguishes TP-equivalent social assistance from statutory RI/leefloon, documents the attestation-plus-municipal-presentation route, scopes CPAS competence, and labels the 1 September 2026 RI amounts as reference rates. Four family-benefit systems remain distinct and the open means-test question stays unresolved. |

## Required corrections

### 1. BE-EDU-01 — name the pending-TP school-access case and align its citation label

- **Issue:** The EN, UK and RU school-access paragraph says that access generally does not wait for an A card or final TP decision, but never directly states that a child whose TP application is pending may access school. In the RU version, the source label for UUID `914453bb-89bf-5ff2-96e8-0174c35a06a5` describes “first-enrolment support in DG,” while the cited research source `BE-EDU-S12` is “Rules for newcomer enrolment in DG.”
- **Why unsafe/incomplete:** The education package explicitly says a child’s basic school-access right is based on being a child in Belgium, not on a completed TP decision, and that pending TP does not itself guarantee a preferred seat or grade. The current general sentence may be understood to cover pending TP, but it leaves that material case implicit. The RU label describes the evidence imprecisely and is not parallel to EN/UK.
- **Supported correction boundary:** In all three languages, explicitly say that a child may request school enrolment while a TP application is pending and that this does not guarantee a preferred school, place or class. Keep the A-card distinction, Community rules and local capacity qualifications. Change the RU source label to accurately identify DG newcomer enrolment rules. No new evidence is required.
- **Languages:** EN, UK and RU for the explicit pending-TP statement; RU for the source-label wording.

## Metadata, source ownership and citation audit

- All eight Article IDs match the expected values, are UUIDv5 and are unique.
- Source counts: BE-TP-01 **10**; BE-TP-02 **16**; BE-EMP-01 **17**; BE-EMP-02 **22**; BE-HEALTH-01 **13**; BE-HOUSING-01 **23**; BE-EDU-01 **25**; BE-SOC-01 **20**. **Total: 146.**
- All 146 source UUIDs were independently checked as deterministic UUIDv5 values derived from the owning Article ID and the exact local source key. All are globally unique; no source UUID is shared between articles.
- Every local key uses its owning article prefix. Every cited research source ID exists in that article’s matching focused package; authority and exact title correspond to the inventory; the official HTTPS URL exactly matches the inventory. No unofficial source or cross-topic source borrowing was found.
- All citation UUIDs resolve to an entry in the same article. **Orphan citations: 0. Unused source entries: 0.** There are no inline research-source-ID citations in place of editorial UUIDs.
- Every draft has `NeedsReview` for EN / UK / RU and includes all three language sections and a claim-coverage/editorial-exclusions section for each language.
- A single material editorial source-label mismatch was found: BE-EDU-01 RU, noted above. It does not change UUID ownership or source identity.

## Translation parity audit

Citation coverage sets are identical across EN / UK / RU for all eight articles: **146 citations per language across the set**, with no language-specific orphan or unused citation. The source inventory is aligned across all three versions.

Material dates, monetary values, qualification boundaries, regional/community distinctions and blocked-area warnings were compared. No material translation difference was found in eligibility, 4 March 2028 scheme endpoint, individual A-card warning, routes, work documents, RMMMG, housing deposits, RI reference rates, CAAMI/INAMI qualifications or CPAS/family-benefit boundaries. The two BE-EDU-01 correction details are the only parity/usability finding; no translation adds a new entitlement or requirement.

## Cross-article and blocked-area audit

### Blocked aspects

- **`protection-application`: no leakage.** BE-TP-01 does not say an appointment is or is not required; it does not promise walk-in or online pre-registration, an exhaustive checklist, universal passport substitutes, a complete no-document or minors/guardian route, interpreter entitlement, a fixed processing time or a prior-other-EU-TP transfer procedure. Supported location/time and high-level intake details remain bounded.
- **`protection-dates`: no leakage.** BE-TP-01 uses 4 March 2028 only for the EU scheme endpoint, states this does not establish individual Belgian A-card expiry, directs the reader to the printed date and current authority instructions, and does not assert automatic extension/no action, universal replacement or a 2027–2028 procedure.
- **`residence-filing`: no leakage.** BE-TP-02 describes route-specific possibilities without a universal “apply from Belgium” instruction, pending document, continuing stay/work guarantee, automatic TP/ordinary-permit coexistence or termination, or automatic post-2028 conversion. Its Blue Card conclusion remains constrained.

These findings do not change aggregate readiness: the three aspects remain blocked.

### Shared documents and cross-article consistency

- **Federal TP attestation:** Correctly distinguished from A card and from employee work proof. It is used for the specific INAMI affiliation route and the CPAS application route only where those packages support it. It is not described as completed insurance membership or family-benefit approval.
- **Annex 15:** Remains context-specific: qualifying post-grant document for employee work; TP-related evidence for healthcare affiliation; municipal-presentation evidence for CPAS; specific listed cases for Forem. No general TP rights are assigned to every Annex 15.
- **A card:** Remains residence evidence. It is not treated as insurance membership, CPAS award or family-benefit award, and it is not stated as a general school-access prerequisite. No cross-article contradiction was identified.
- **Employment:** BE-EMP-01 and BE-EMP-02 preserve VDAB / Actiris / Le Forem / Arbeitsamt-ADG regional routing; portal use, jobseeker registration, work permission and unemployment benefits are distinct. Pending TP is not work authorization; federal attestation alone is not established employee work proof; Annex 15 is qualified; A card and Annex 15 remain distinct. The €2,189.81 RMMMG is dated from 1 April 2026, gross per month for covered workers aged 18+ under CCT 43, described as an average-income guarantee, not hourly pay or take-home pay; higher sector CCT minima are retained.
- **Healthcare:** The later INAMI rule and CAAMI checklist discrepancy remains visible and qualified. Annex 15 and A card are not insurance cards. AMU/DMH is distinguished from ordinary insurance and emergency-department care; 112 and locally available 1733 are separate. No-address/admin-delay handling is not promised as a universal workaround.
- **Housing:** No guaranteed Fedasil bed, long-term state housing, or live programme capacity is claimed. Regional programmes are not generalized. Lease, actual-address registration and A-card issuance remain distinct; social-housing application is not an allocation; CPAS support is individually assessed. Deposit rules are stated by region and separated from advance rent.
- **Education:** Flemish / FWB / DG rules and newcomer programmes remain distinct. A card is not a general school-access prerequisite; school access is not a seat guarantee. Fee treatment is community-specific: Flemish protected increased-fee treatment is not a universal waiver; FWB EU assimilation concerns the additional contribution while base minerval remains; no general DG TP exemption is claimed.
- **Social assistance:** Equivalent aid is not statutory RI/leefloon; TP attestation plus municipal presentation supports filing without guaranteeing an award. Pending TP is not a general cash route. The CPAS competence and means test remain case-specific; unemployment benefits are separate. Family benefits retain four regional systems; the open means-test question is not resolved. The proposed 70%-to-100% measure is not described as enacted.
- **TP/Residence:** BE-TP-01 distinguishes the EU 4 March 2028 endpoint from individual A-card dates; BE-TP-02 makes no automatic transition claim. Neither expands the other article’s blocked mechanics.

## Freshness checks before publication

No facts were refreshed for this review. Before publication, recheck only the time-sensitive facts identified in the scoped readiness/packages:

- EU TP endpoint and any new authoritative Belgian instructions on individual A-card expiry/renewal/replacement and holder action for 2027–2028.
- Current federal registration-centre location/intake time and any newly published appointment, walk-in, document or child/guardian procedure; do not reuse legacy intake instructions.
- The residence-route requirements and any Belgian decision on in-country filing, pending rights, Blue Card sequencing or a post-2028 transition.
- Employment-service registration/document requirements, employer work-document guidance and current RMMMG/sectoral CCT rates.
- INAMI affiliation evidence and CAAMI’s operational document checklist; local availability of 1733.
- Live capacity and current regional housing-programme periods; applicable regional rental/deposit rules.
- Community programme eligibility and availability, and current higher-education fee schedules (including the cited FWB 2026–27 amount).
- Current 1 September 2026 RI reference-rate schedule, the status of the proposed 70%-to-100% measure, and regional family-benefit requirements. Keep the family-benefit means-test question open unless authoritative TP-specific evidence resolves it.

## Final disposition

- **PASS:** BE-TP-01, BE-TP-02, BE-EMP-01, BE-EMP-02, BE-HEALTH-01, BE-HOUSING-01, BE-SOC-01.
- **CORRECTION:** BE-EDU-01.
- **BLOCKER:** none at draft level.
- **Exact correction count:** 2 bounded corrections in 1 draft: clarify pending-TP school access across EN/UK/RU; correct the RU source label for the DG enrolment source.
- **Overall verdict:** **READY FOR CORRECTION PASS.**
- All drafts remain `NeedsReview`; this review is not editorial acceptance.
- The research blockers remain `protection-application`, `protection-dates` and `residence-filing`.
- Belgium remains excluded from Compare. No catalog integration is authorized while the correction remains.

## Validation

- Exactly one new file is in scope: `docs/content-drafts/belgium-v1/EDITORIAL_REVIEW.md`.
- No Belgium draft or research file was changed.
- `git diff --check` must pass before commit.
- No `dotnet test` was required for this documentation-only review.
