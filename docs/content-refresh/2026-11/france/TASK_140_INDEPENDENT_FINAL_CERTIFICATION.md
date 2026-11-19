# Task 140 — France independent final certification

**As of:** 19 November 2026  
**Result:** **FRANCE FINAL CERTIFICATION BLOCKED**  
**Scope:** Independent audit of France staging only. No production files were changed.

## Recomputed Core claims

The ledger has 283 total rows: 175 ACTIVE, 64 SPLIT_PARENT, 32 INACTIVE and 12 INACTIVE_REMOVED. Active rows by article are:

| Article | Article UUID | Active |
|---|---|---:|
| FR-TP-01 | `d16ad009-3391-5e3b-80b2-5b0d348503d3` | 20 |
| FR-TP-02 | `78be24cd-82f5-5f29-ab54-0013aa15ab93` | 24 |
| FR-EMP-01 | `82179b12-c3bd-5bf1-be0a-374502fc0386` | 17 |
| FR-EMP-02 | `084c5497-7b96-59a2-9d9a-4b0be9b5a2ff` | 19 |
| FR-HEALTH-01 | `ffc21bf8-901c-5ec3-9f54-fce1dc797974` | 20 |
| FR-HOUSING-01 | `a308b75d-4b39-56d0-9ce9-effb4418400c` | 26 |
| FR-EDU-01 | `0a1c276b-9a49-5bf5-8be8-104974dd0e5e` | 22 |
| FR-SOC-01 | `8d7dd894-f0c5-53f4-831c-4c936f27fdff` | 27 |
| **Total** |  | **175** |

Materiality is 117 MATERIAL and 58 SUPPORTING. NumericDate=YES is 39. Temporal classes are 168 CURRENT and 7 TRANSITION; FUTURE and HISTORICAL are both 0. There are no duplicate active ClaimIds, orphan lineage rows, active split parents, or open reopen rows. Lineage is complete. Three active FR-TP-02 rows are not propositions and prevent certification of the active universe as fully atomic: C004 `CESEDA Article L.`, C005 `412-1 CESEDA Article L.`, and C006 `412-2 CESEDA Article L.`. C005 and C006 are also marked NumericDate=YES despite being malformed citation fragments.

Coverage rows independently total 175: 148 COVERED, 27 COVERED — MULTIPLE SOURCES, 0 NOT EXTERNALLY SOURCE-DEPENDENT, 0 UNSUPPORTED and 0 REQUIRES_SOURCE_REVIEW. This ledger coverage result does not resolve the malformed active fragments or the review-copy mismatches below. No active MATERIAL or NumericDate claim is marked unsupported or requires source review. All four reopen-candidate rows are resolved.

Tasks 135–139 inactivated 44 claims (32 `INACTIVE` plus 12 `INACTIVE_REMOVED`):

`FR-TP-01-C012`, `FR-TP-01-C013`, `FR-TP-01-C018`, `FR-TP-01-C019`; `FR-TP-02-C023`, `FR-TP-02-C024`, `FR-TP-02-C026`, `FR-TP-02-C009`, `FR-TP-02-C010`, `FR-TP-02-C014`, `FR-TP-02-C037`, `FR-TP-02-C041`; `FR-EMP-01-C004`, `FR-EMP-01-C005`, `FR-EMP-01-C009`, `FR-EMP-01-C023`, `FR-EMP-01-C012`, `FR-EMP-01-C027`, `FR-EMP-01-C028`, `FR-EMP-01-C031`; `FR-EMP-02-C005`, `FR-EMP-02-C025`, `FR-EMP-02-C026`; `FR-HEALTH-01-C010`, `FR-HEALTH-01-C011`, `FR-HEALTH-01-C031`; `FR-HOUSING-01-C003`, `FR-HOUSING-01-C005`, `FR-HOUSING-01-C027`, `FR-HOUSING-01-C014`, `FR-HOUSING-01-C032`; `FR-EDU-01-C024`, `FR-EDU-01-C025`, `FR-EDU-01-C027`, `FR-EDU-01-C011`, `FR-EDU-01-C034`, `FR-EDU-01-C021`, `FR-EDU-01-C037`; `FR-SOC-01-C027`, `FR-SOC-01-C005`, `FR-SOC-01-C007`, `FR-SOC-01-C008`, `FR-SOC-01-C015`, `FR-SOC-01-C044`.

Removal reasons are unsupported/nonessential claims, malformed composite headings or citation fragments, and redundant caveats. Coverage includes only active claims. No inactive ClaimId remains in the final active Watch set; the three removed Watch rows retain historical references to claims that were removed.

## Evidence graph and coverage

The complete source-claim audit has **561 rows**: 209 SUPPORTED and 352 NOT_SUPPORTED, with 0 unresolved/other decisions. The active supported subgraph covers all 175 active claim IDs and has 208 distinct ClaimId–SourceId pairs after excluding the explicitly historical/superseded Task 137 PUMa proposition snapshot. That historical pair is FR-HEALTH-01-C001 + `42d2785e-421c-5350-b47d-7c9fec5c49de`; its retested Task 137 row supports the expanded final proposition. There are no duplicate effective active tuples, dangling active ClaimIds, or unknown SourceIds. Historical rejected decisions are retained. The staging-only Task 139 CNAF source `c42ac694-fe38-52e6-b619-3f6b18542d89` is correctly marked `CurrentBinding=NO` in the claim and Compare audit rows because it has no production binding.

## Source inventory and integration delta

Production JSON independently contains 1,530 OfficialSources, of which **104** belong to the eight France entries (7, 10, 12, 12, 12, 14, 17 and 20 per article). Staging contains **117** records: 94 KEEP, 8 UPDATE, 2 RETAIN_HISTORICAL, 13 ADD and 0 REMOVE. The resulting source-record plan is 104 + 13 = 117 France records and 1,530 + 13 = **1,543** global records.

The 13 ADD IDs match the requested set and all 13 have the correct article owner and official source authority: `56c5f996-f13c-5df1-9cd2-ca2858889e6d`, `fecb425a-243b-5233-871d-2ce26b742a66`, `3a55eb20-dea4-5b00-bdef-7d932cf1fa31`, `d277a456-4e10-5a6c-849a-e23ba7474991`, `bf5e5f83-b27d-5b60-aa2d-9e5a924b342e`, `fd583be7-629f-5004-a38d-136e1a85b2d4`, `06d80225-6dde-5d61-94c8-48feac8d8e67`, `6ed80718-151e-52d3-bebd-dd8f9eb07d38`, `9003daa2-1315-58ef-8237-e8147d3a5592`, `77766134-f03b-5576-89df-6f38a08ed93c`, `ea7b16e1-7abc-5ebc-9521-2d45289a040d`, `a8de9820-9b53-5b98-b1e7-b89cb427f71d`, `c42ac694-fe38-52e6-b619-3f6b18542d89`. `fecb425a-243b-5233-871d-2ce26b742a66` is owned by FR-TP-02 UUID `78be24cd-82f5-5f29-ab54-0013aa15ab93`; no erroneous OwnerId `b43097d3-2524-5e94-8ab4-9a384efeb995` remains.

No owner conflicts, cross-owner active Core citations, Core/Compare dangling IDs, or unintended same-owner+URL duplicates were found. Five official URLs intentionally have article-specific cross-owner records: CESEDA R. 581-4; the France Travail foreign-worker registration page; Service-Public F23458; CAF des Yvelines Ukraine guidance; and Légifrance CESEDA temporary-protection chapter. These preserve article ownership and are not same-owner duplicates.

The eight UPDATE records all contain actionable successor URL(s):

| SourceId | Old URL | Final successor/canonical URL(s) | Role |
|---|---|---|---|
| `bec70186-5af2-54e6-8ba3-2a4f8ce770c1` | `https://www.alpes-maritimes.gouv.fr/Demarches/Espace-ressortissants-Ukrainiens/Protection-temporaire-premiere-demande-et-renouvellement` | Same current APS page plus `https://www.alpes-maritimes.gouv.fr/Demarches/Prendre-un-rendez-vous` | Local APS procedure and appointment schedule |
| `d9e7aa4b-7888-5f10-9781-05dadd3ca055` | `https://www.legifrance.gouv.fr/codes/article_lc/LEGIARTI000033024092/2024-02-05` | `https://www.legifrance.gouv.fr/codes/article_lc/LEGIARTI000033024092/2026-11-19` | Current Labour Code payslip provision |
| `f923322c-4336-5c6a-a971-f23901bb4ce2` | `https://www.legifrance.gouv.fr/codes/id/LEGISCTA000006160947` | `https://www.legifrance.gouv.fr/codes/article_lc/LEGIARTI000006908175` | Direct Article R. 1112-13 page |
| `fe6b9f9e-ea2b-5864-bd31-e4f19b566e7b` | `https://www.education.gouv.fr/botexte/sp10020425/MENE0200681C.htm` | `https://www.service-public.gouv.fr/particuliers/vosdroits/F1866`; `https://www.service-public.gouv.fr/particuliers/vosdroits/F1898`; `https://www.service-public.gouv.fr/particuliers/vosdroits/F21304` | Current school-access/age rules |
| `8e80f891-0c43-5ad7-a308-c54c4c55d097` | `https://eduscol.education.fr/1201/centre-academique-pour-la-scolarisation-des-eleves-allophones-nouvellement-arrives-et-des-enfants-issus-de-familles-itinerantes-et-de-voyageurs-casnav` | `https://eduscol.education.gouv.fr/5493/centre-academique-pour-la-scolarisation-des-eleves-allophones-nouvellement-arrives-et-des-enfants-issus-de-familles-itinerantes-et-de-voyageurs-casnav` | Current CASNAV directory |
| `e7915cbf-568f-5224-9872-9331f849e885` | `https://www.legifrance.gouv.fr/codes/section_lc/LEGITEXT000006070158/LEGISCTA000042906403/2026-07-16` | `https://www.legifrance.gouv.fr/codes/section_lc/LEGITEXT000006070158/LEGISCTA000042906403` | Current ADA Annex 8 tariff |
| `6ab4e1c0-eea2-5c1a-b231-50aff97e701e` | `https://www.service-public.gouv.fr/particuliers/vosdroits/F33314` | `https://www.legifrance.gouv.fr/codes/section_lc/LEGITEXT000006070158/LEGISCTA000042806148`; `https://www.legifrance.gouv.fr/codes/article_lc/LEGIARTI000042775671`; `https://www.legifrance.gouv.fr/codes/section_lc/LEGITEXT000006070158/LEGISCTA000042906403` | ADA legal successor set |
| `d8411755-c986-5660-83c1-cbfb80100896` | `https://www.service-public.gouv.fr/particuliers/vosdroits/F920` | `https://www.service-public.gouv.fr/particuliers/actualites/A19067` | Updated lease-template information |

The two RETAIN_HISTORICAL records are `04f61765-f6e7-5f22-87c0-60f6382fa42c` (2022 Interior Ministry Ukraine/TP eligibility context, paired with later EU rules) and `aa0f80be-7a98-5b27-a6e2-38286081f4ed` (May 2024 OFII ADA continuation form, used only as a dated continuation-process reference). Existing-source actions remain 94 KEEP, 8 UPDATE, 2 RETAIN_HISTORICAL, 0 REMOVE; actions sum to 104.

From the final review-copy source tags compared with current production article bindings, Core binding delta is **KEEP 79, ADD 12, REMOVE 25**. This is distinct from source-record actions. Staging source `6ed80718-151e-52d3-bebd-dd8f9eb07d38` is the thirteenth source-record ADD but is not cited in the FR-EMP-02 review copy; its source inventory says CoreMappingTotals=0 although the source-claim audit has a supported row for active FR-EMP-02-C027. The copy/ledger/source map therefore needs reconciliation before integration.

## Compare: independent result

All **24/24** France Compare aspects were checked against their final draft propositions, binding lists, source-link audit and source inventory. Primary dispositions are KEEP 12, REVISE 12, UNAVAILABLE 0 and NEWLY_AVAILABLE 0, totaling 24; one NEWLY_AVAILABLE marker remains secondary metadata. The exact REVISE set is `healthcare-access`, `healthcare-proof`, `healthcare-urgent`, `housing-initial`, `housing-private-rental`, `housing-financial-support`, `education-school-enrolment`, `education-pupil-language`, `education-higher-admission`, `social-subsistence`, `social-application`, `social-child-benefits`.

**12/24** aspects are independently source-certified: `protection-dates`, `work-minimum-pay`, `healthcare-access`, `healthcare-proof`, `healthcare-urgent`, `housing-initial`, `housing-private-rental`, `housing-financial-support`, `education-school-enrolment`, `education-pupil-language`, `education-higher-admission`, `social-child-benefits`.

**12/24 are not source-certified:**

- `protection-scheme`, `protection-application`, `residence-routes`, `residence-conditions`, `residence-filing`, `employment-vacancies`, `employment-institutions`, `employment-assistance-conditions`, `work-permission`, `work-employer-duties` remain expressly `REQUIRES_SOURCE_REVIEW` in the Compare draft.
- `social-subsistence` lacks an exact aspect/source tuple for its CNAF temporary-protection scope source `d9b295aa-1f88-5757-8f43-2ac5380c3323`.
- `social-application` lacks an exact aspect/source tuple for its France Travail/ARE source `0e30784a-4434-5fd1-8ff8-6d09c8bbc9f1`.

The previously reported **11/24** counter was stale. The 14 aspects explicitly marked certified before this independent review were the 12 listed above plus `social-subsistence` and `social-application`. Task 140 found those two lack exact aspect-source audit rows and changed only their evidence-status labels to `REQUIRES_SOURCE_REVIEW`; no factual proposition was changed. The 11/24 to 14/24 correction was bookkeeping; the independently auditable current result is **12/24**.

Compare source-binding delta from the final candidate source lists vs production is **KEEP 60, ADD 13, REMOVE 11**, with 0 dangling IDs. This delta is derivable but not approved for integration because 12 aspects remain uncertified.

## Watch: normalized final state

There are 14 Watch rows. FinalStatus gives **11 APPROVE and 3 REMOVE**; there are 0 separately represented APPROVE_AFTER_EDIT rows and 0 REQUIRES_GAP_RESOLUTION rows. HistoricalAuditAction counts are **9 APPROVE, 2 EDIT, 3 REMOVE**. W13 and W14 have final status APPROVE after their historical EDIT action; audit actions are not additional final dispositions. No stale/inactive ClaimId appears on an APPROVE row; no unknown SourceId or duplicate WatchId exists. W07 (EDU-C034), W08 (EDU-C021) and W09 (SOC-C005) are REMOVE because the related unsupported claims were inactivated. FR-W14 remains approved with DECISION_EXPECTED, unknown trigger date, and underlying PUMa proposition supported. Earliest known future legal/effective event is the EU protection extension start on 5 March 2027; earliest maintenance pre-check is 1 December 2026.

## Language and high-risk review

All eight article drafts contain EN, UK and RU sections and the correct article UUID. No material or numeric/date translation divergence was identified in the text reviewed. Seven active Core propositions are absent or materially narrower in all three corresponding language copies: FR-EMP-01-C020, C022; FR-EMP-01-C033; FR-EMP-02-C027; FR-SOC-01-C017, C026 and C034. C026’s copy mentions resource disclosure but omits the spouse/partner detail. The copy also omits the exact two-child allocations familiales condition (SOC-C017) and all PAJE-specific wording (SOC-C034). These are ledger-to-copy omissions, not EN/UK/RU translation differences.

High-risk wording check: EU temporary-protection extension and French APS validity are separately stated; the 5 August 2026 new-grant military-obligation/proof condition and continuous same-Member-State exception are scoped as staged; temporary-protection work permission is distinguished from employer duties, France Travail registration, ARE and training; SMIC figures are €12.31 gross/hour and €1,867.02 gross/month in metropolitan France from 1 June 2026, and €9.56/hour / €1,449.93/month in Mayotte; PUMa’s participation rule is distinguished from decree-dependent amount/collection; emergency numbers 15/112 and scoped 114 access are stated without free-care promises; housing copy makes no guaranteed 115, APL, HLM or Visale claim; education distinguishes compulsory instruction ages 3–16 from the 16–18 formation obligation and contains no fixed UPE2A hours; Social wording keeps TP distinct from asylum, limits ADA accommodation to the additional component, states the RSA five-year condition with exceptions, scopes Yvelines locally and makes CCAS support non-guaranteed. These checks do not clear the separate Compare, malformed-claim or copy/ledger blockers.

## Mechanical corrections and blockers

Staging-only corrections made in Task 140:

1. Corrected Task 139’s stale Compare counter from 11/24 to the 14/24 then explicitly marked as certified; independent review then downgraded the two Social aspects with missing exact tuples, leaving 12/24 currently certified.
2. Changed the Watch column label `AuditOutcome` to `HistoricalAuditAction` and documented that FinalStatus alone controls final dispositions.
3. Marked the CNAF ADD tuple as not currently production-bound.
4. Marked the older PUMa C001 tuple as a historical proposition snapshot superseded by the Task 137 exact expanded-proposition retest, retaining the original evidence row.
5. Changed only the evidence-status metadata for `social-subsistence` and `social-application` to REQUIRES_SOURCE_REVIEW; propositions and dispositions are unchanged.

Substantive blockers remain: (a) only 12/24 Compare aspects have an independently auditable source certification; (b) three active FR-TP-02 citation fragments are not propositions; (c) seven active propositions are missing or materially narrower in final review copies; and (d) an ADD source is not bound in the final copy despite an active claim-source tuple. No new gap research was performed.

Production/canonical JSON is unchanged. No commit, push, CI, Docker or tests were run.


## POST-CERTIFICATION REPAIR NOTE — Task 141 (19 November 2026)

Task 140’s original verdict remains **FRANCE FINAL CERTIFICATION BLOCKED** as the historical state it reviewed. Task 141 repaired all four recorded blocker categories in staging: Compare source certification is now 24/24 with four explicit aspect/source tuples added (all SUPPORTED); FR-TP-02-C004/C005/C006 are inactive citation fragments; all seven supported propositions are present in EN/UK/RU copies; and source `6ed80718-151e-52d3-bebd-dd8f9eb07d38` is cited for active FR-EMP-02-C027. The active claim universe is now 172 (117 MATERIAL, 55 SUPPORTING, 36 NumericDate). The package is queued for independent Task 142 certification; this note does not approve integration. Full details: `TASK_141_FINAL_CERTIFICATION_BLOCKER_REPAIR.md` and `COMPARE_CERTIFICATION_REPAIR_01.md`.


## POST-CERTIFICATION REPAIR NOTE — Task 141 (19 November 2026)

Task 140’s original verdict remains **FRANCE FINAL CERTIFICATION BLOCKED** as the historical state it reviewed. Task 141 repaired all four recorded blocker categories in staging: Compare source certification is now 24/24 with four explicit aspect/source tuples added (all SUPPORTED); FR-TP-02-C004/C005/C006 are inactive citation fragments; all seven supported propositions are present in EN/UK/RU copies; and source `6ed80718-151e-52d3-bebd-dd8f9eb07d38` is cited for active FR-EMP-02-C027. The active claim universe is now 172 (117 MATERIAL, 55 SUPPORTING, 36 NumericDate). The package is queued for independent Task 142 certification; this note does not approve integration. Full details: `TASK_141_FINAL_CERTIFICATION_BLOCKER_REPAIR.md` and `COMPARE_CERTIFICATION_REPAIR_01.md`.
