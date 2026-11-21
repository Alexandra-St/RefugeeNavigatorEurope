# Task 188 research trace — Romania Batch 1 blocker resolution

**As of:** 2026-11-20. Official Romanian government legal sources only for the two target acts. No Batch 2 audit, production update, source-row creation, or Core binding change.

## Target A — OUG 194/2002 (SourceId `501ff4ab-87d5-514d-b2a5-ac95fc628881`)

### Retrieval paths

- Stored canonical URL `https://legislatie.just.ro/Public/DetaliiDocument/50236`: unavailable, returned HTTP 502/internal error.
- Official alternate Portal Legislativ view `https://legislatie.just.ro/Public/DetaliiDocumentAfis/50236`: **opened successfully**; exact same document number 50236 and act.
- Full official IGI-hosted PDF: `https://igi.mai.gov.ro/wp-content/uploads/2026/04/ORDONANTA-DE-URGENTA-nr.-194-din-12-decembrie-2002.pdf`: **opened successfully**, 118 pages, consolidated text with amendments through 27 April 2026. The Portal history likewise lists latest consolidation 27.04.2026.
- An initial guessed printable-form token returned no source and was discarded. The successful Portal alternate view and IGI PDF provide the authoritative text; no commercial legal summary was used.

### Legal scope and adjudication

OUG 194/2002 of 12 December 2002, republished/current consolidated form through 27 April 2026, including the OUG 32/2026 amendments. Articles 45–73 describe ordinary study, family reunification, work, and long-term residence categories and related eligibility/filing procedures. Article 46 identifies family-reunification sponsor categories, including qualifying temporary-residence holders, EU Blue Card holders, ICT/Mobile ICT holders, long-term residents and international-protection beneficiaries. Article 50 provides extension conditions and same-purpose rules with exceptions. Article 51(4²) preserves the right of stay until an extension decision when the applicant was legally staying at filing; this is not, by itself, a work authorization. Article 56 sets work-purpose residence routes. Article 70(2)(b) bars obtaining long-term residence while the person is a TP applicant/beneficiary. Article 71 counts temporary-residence and international-protection periods for the five-year condition, but does not expressly classify TP time after leaving TP; do not infer such credit from lawful presence.

- C001: `SUPPORTED` — ordinary purpose-specific route categories and procedures are present; this does not establish direct TP conversion.
- C002: `SUPPORTED` — ordinary visa/residence procedures are described in the cited official sources.
- C003: `SUPPORTED` — route existence does not establish automatic/general direct transition; separate TP-specific and ordinary routes remain distinct.
- C008: `NOT_SUPPORTED` / `PARTIAL_SUPPORT` — Blue Card and work/family rules appear, but no TP-to-Blue-Card transition or prohibition is stated.
- C009: `NOT_SUPPORTED` / `PARTIAL_SUPPORT` — lawful stay pending extension is addressed conditionally; independent pending work authorization is not. TP status bars LTR while held; post-TP TP-period credit is not resolved by Article 71. Claim needs narrowing/splitting and direct evidence for the unresolved points.

Final source status/action: `CURRENT` / `UPDATE`. Recommended Title: “Portal Legislativ, OUG no. 194/2002 (republished), consolidated form through 27 April 2026, including OUG 32/2026 amendments.” Set URL and CanonicalURL to the successful Portal alternate view; keep the IGI PDF as an audit trace/corroborating representation. Canonical URL is part of the repository UUIDv5 input, so `SOURCE_ID_REGEN_REQUIRED_LATER`; no new UUID is generated in Task 188.

## Target B — OUG 32/2026 (SourceId `ee3461f2-62c1-5515-aff2-4abf1b958a8a`)

### Retrieval paths

- Stored Portal URL `https://legislatie.just.ro/Public/DetaliiDocument/309832`: unavailable (502).
- Official Portal printable/query and alternate `DetaliiDocumentAfis/309832` attempts: unavailable (502/inaccessible).
- Official Portal title/document-number searches: no exact act result.
- Official Monitorul Oficial website title query: no accessible exact-text result.
- IGI official legislation landing page `https://igi.mai.gov.ro/legislatie/`: opened; its OUG 32 link led to the full PDF below.
- Exact official IGI-hosted full PDF `https://igi.mai.gov.ro/wp-content/uploads/2026/05/ORDONANTA-DE-URGENTA-nr.-32-din-23-aprilie-2026.pdf`: **opened successfully**. The heading identifies Government Emergency Ordinance no. 32 of 23 April 2026; it states publication in Monitorul Oficial no. 335 on 27 April 2026 and entry into force on 27 April 2026.

### Legal scope and adjudication

Articles 48–52 establish employment-market and associated residence-transition procedures. The testing/registration phase under Article 48 runs until 7 August; Article 51 amended employer-change provisions apply from 8 August 2026. Article 52’s transitional route through 31 December 2026 is limited to the specified people remaining after expiry of an employment long-stay visa/single permit without a return measure; its conditions and filing are status-specific. Article 52(4) makes work commencement contingent on single-permit issuance, and Article 52(6) retains the former law for long-term-residence applications filed through 31 December 2026. As of 20 November 2026 the act is current and these transition dates are in force.

- C007-S01: `NOT_SUPPORTED` / `PARTIAL_SUPPORT` — OUG 32 is labor-market focused and does not state all ordinary study/family/other residence procedures.
- C007-S02: `NOT_SUPPORTED` / `PARTIAL_SUPPORT` — it does not establish a general direct TP-to-study/family transition; its limited work transition provisions cannot establish universal absence.
- C007-S03: `NOT_SUPPORTED` / `PARTIAL_SUPPORT` — the act does not state a prohibition; silence is not affirmative evidence for a broader negative claim.
- C008: `NOT_SUPPORTED` / `PARTIAL_SUPPORT` — no TP-to-Blue-Card route or bar stated.
- C009: `NOT_SUPPORTED` / `PARTIAL_SUPPORT` — Article 52 is a specified expired-work-permit cohort and does not resolve TP pending-work rights or prior TP-time credit.

Final source status/action: `CURRENT` / `UPDATE`. Recommended Title: “Emergency Ordinance no. 32 of 23 April 2026 on foreign access to the Romanian labour market and amendments to immigration legislation; published Monitorul Oficial no. 335 on 27 April 2026.” Set Authority to IGI, Romania and URL/CanonicalURL to the official IGI-hosted PDF. Canonical URL is part of UUIDv5 input, so `SOURCE_ID_REGEN_REQUIRED_LATER`; no new UUID is generated now.

## Reconciliation

- The ten target claim-source tuples: 3 `SUPPORTED`, 7 `NOT_SUPPORTED`; zero `REVIEW_BLOCKED` or pending.
- Four Compare tuples: all 4 newly `SUPPORTED` as relevant evidence for their aspect, with status-specific limitations recorded; overall ownership violations remain zero.
- Active TP/residence coverage: 15 `COVERED`, 15 `UNSUPPORTED`, 0 multiple, 0 review-required, 0 with no candidate. Remaining unsupported claims are evidence/rewrite gaps, not retrieval blockers.
- The Task 187 typo `d22b2dda-6a8a-5a85-ae33-aa40b6a778fe` is a report-only typo. Correct existing SourceId is `d22b2dda-6a08-5a85-ae33-aa40b6a778fe`; no phantom ID/source record was created.
- Watch dates remain RO-W-001 2027-03-05 and RO-W-002 2028-03-04.
