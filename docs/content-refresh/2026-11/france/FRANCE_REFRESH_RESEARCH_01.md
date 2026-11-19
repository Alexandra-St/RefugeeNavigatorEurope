# France content refresh research baseline — Task 130

As of 19 November 2026. This is a structural baseline, not a source certification.

## Article inventory

| Code | ArticleId | Title | EN/UK/RU | Sources | Claims |
|---|---|---|---|---:|---:|
| FR-TP-01 | d16ad009-3391-5e3b-80b2-5b0d348503d3 | Applying for temporary protection in France | yes/yes/yes | 7 | 20 |
| FR-EMP-01 | 82179b12-c3bd-5bf1-be0a-374502fc0386 | Finding a job in France | yes/yes/yes | 12 | 17 |
| FR-EMP-02 | 084c5497-7b96-59a2-9d9a-4b0be9b5a2ff | Work rights and employee conditions in France | yes/yes/yes | 12 | 18 |
| FR-HEALTH-01 | ffc21bf8-901c-5ec3-9f54-fce1dc797974 | Healthcare in France under temporary protection | yes/yes/yes | 12 | 17 |
| FR-EDU-01 | 0a1c276b-9a49-5bf5-8be8-104974dd0e5e | Education in France under temporary protection | yes/yes/yes | 17 | 21 |
| FR-SOC-01 | 8d7dd894-f0c5-53f4-831c-4c936f27fdff | Social assistance and family benefits in France | yes/yes/yes | 20 | 22 |
| FR-TP-02 | 78be24cd-82f5-5f29-ab54-0013aa15ab93 | Residence Options Beyond Temporary Protection | yes/yes/yes | 10 | 22 |
| FR-HOUSING-01 | a308b75d-4b39-56d0-9ce9-effb4418400c | Finding Accommodation in France | yes/yes/yes | 14 | 21 |

Active claims: 158; MATERIAL 122; SUPPORTING 36; NumericDate YES 62. Temporal labels: {'CURRENT': 150, 'TRANSITION': 5, 'FUTURE': 1, 'HISTORICAL': 2}. Sentence-level extraction flags are provisional; potential coordination is annotated in ledger Notes and must be split before certification.

France production OfficialSources owned: 104; unique URL: 100; Core referenced France sources: 104; Compare referenced France sources: 66; shared 66; Core-only 38; Compare-only 0; owned unreferenced 0.

Core candidate tuples: 712; tuples substantively verified: 0 SUPPORTED, 0 NOT_SUPPORTED, 712 REQUIRES_SOURCE_REVIEW. Coverage claims: 0 COVERED; 0 multi; 0 UNSUPPORTED; 158 REQUIRES_SOURCE_REVIEW. All material and numeric claims lack certified evidence in this task.

Ownership conflicts: 0 among valid inline Core bindings (exact owner convention); dangling Core SourceIds: 0; dangling Compare SourceIds: 0. Same-owner+URL duplicate groups: 0. Cross-owner same-URL records are intentional only where architecture requires owner-specific records; separately review duplicates before release.

## Cross-article and translation review

Material cross-article conflicts: none identified from the current production snapshot in this structural pass; this does not replace source adjudication. Potential article-level overbinding is flagged: all claim-source pairs from same-paragraph citation co-location are candidates only, and paragraphs with multiple claims or multiple citations require exact adjudication (712 candidate tuples across 158 sentence handles). EN/UK/RU were inspected as production translations. No clearly material semantic divergences were identified in this pass; exact line-by-line legal translation certification is deferred.

## Compare

Aspects: 24; all current approved/available France facts provisionally KEEP. Source links: 71; source-certified aspects: 0/24.

## Date/Event watches and unresolved risk

Candidates are in DATE_EVENT_WATCH_CANDIDATES_01.md. The first known future legal horizon visible in production is EU TP 4 March 2028; a nearer SMIC rate change was not established from this baseline. Recommended maintenance pre-check: 1 December 2026 as an internal recheck date, not an official effective date. Highest risks: TP eligibility/document validity and renewal; 2028 EU horizon; work authorization and wage; ADA/social benefits; healthcare access/coverage; local housing procedures; school and higher education annual rules; exact evidence for all material claims.

## Method and next step

Current production EN/UK/RU, source inventory and Compare facts were read. Core candidate tuples are generated only where the paragraph containing a claim has an inline production source marker; each tuple remains REQUIRES_SOURCE_REVIEW. Sentence boundaries are provisional claim handles and not an assertion that all compound propositions have been atomized. Task 131 should first split flagged compound claims, then audit each exact tuple against sources published on or before the cutoff, and record SUPPORTED/NOT_SUPPORTED per tuple. No production/canonical/test/other-country files were changed.


## FINAL ATOMIC BASELINE — Task 131 (authoritative)

All 158 provisional handles were adjudicated; 91/91 initially flagged were reviewed. Decisions: KEEP_ATOMIC 81, SPLIT 59, MERGE_DUPLICATE 0, REMOVE_NON_CLAIM 18, REMOVE_REDUNDANT 0. Active claims 219; by article {'FR-TP-01': 24, 'FR-EMP-01': 25, 'FR-EMP-02': 22, 'FR-HEALTH-01': 23, 'FR-EDU-01': 29, 'FR-SOC-01': 33, 'FR-TP-02': 32, 'FR-HOUSING-01': 31}. MATERIAL 144; SUPPORTING 75; NumericDate 45; temporal {'CURRENT': 210, 'TRANSITION': 9}. The independent final scan found 0 active compound candidates; duplicate active IDs 0; orphan lineage rows 0. EN/UK/RU proposition review: 8/8, no material divergence identified. Exact Core tuples 496, all REQUIRES_SOURCE_REVIEW; duplicate tuples 0. Claims requiring source review 219; MATERIAL 144; NumericDate 45. Source audit priorities: P0_HIGH_RISK 74, P1_MATERIAL 13, P2_SUPPORTING 17. France-owned sources 104; unique URLs 100; Production Core-referenced 104; final candidate-graph SourceIds 93; Compare-referenced France-owned 66; shared 66; Core-only production sources 38; Compare-only 0; production owned-unreferenced 0. Compare candidate tuples 71, duplicates 0, all REQUIRES_SOURCE_REVIEW. Watch candidates 13, all ClaimIds active. Earliest unverified prospective trigger: 4 March 2028; internal pre-check: 1 December 2026. Material cross-article conflicts identified: 0 in structural review. Next: exact official-source audit.


## SOURCE AUDIT BATCH 1 — 35 / 104

Selected the deterministic first 35 by P0_HIGH_RISK, P1_MATERIAL, P2_SUPPORTING then ascending SourceId. The exact list is in OFFICIALSOURCE_AUDIT_BATCH_01.csv. Status totals: {'CURRENT': 33, 'LEGACY': 1, 'UNCLEAR': 1}. Action totals: {'KEEP': 33, 'RETAIN_HISTORICAL': 1, 'UPDATE': 1}. Core tuples adjudicated 213: SUPPORTED 56, NOT_SUPPORTED 157, unresolved 0. Cumulative Core tuples: SUPPORTED 56, NOT_SUPPORTED 157, REQUIRES_SOURCE_REVIEW 283. Compare tuples adjudicated 28: SUPPORTED 27, NOT_SUPPORTED 1, unresolved 0. Compare source-certified aspects remain 0/24; some source links support only a component and other candidate evidence remains unaudited. Claim reopen candidates: FR-TP-01-C025 (EU extension is future from 5 Mar 2027); FR-EMP-01-C025 (official current label POE vs POEI); FR-HEALTH-01-C001 (expected PUMa contribution decree). Source successor URLs: none established; ADA page 6ab4e1c0-eea2-5c1a-b231-50aff97e701e is explicitly under update and needs a current successor when available. New ADD source candidates: 0. Watch changes: FR-W01 edited to EXPIRES_ON 4 Mar 2028; FR-W14 added for the expected PUMa decree; the other selected-linked watches were approved. Remaining unaudited source count: 69.

Coverage after Batch 1: COVERED 28; COVERED — MULTIPLE SOURCES 1; REQUIRES_SOURCE_REVIEW 167; UNSUPPORTED 23. MATERIAL certified 23/144; MATERIAL still requiring review 107; MATERIAL unsupported 14. NumericDate certified 9/45; NumericDate still requiring review 31; NumericDate unsupported 5.


## SOURCE AUDIT BATCH 2 — 35 / 104 (cumulative 70 / 104)

Selected the next deterministic 35 remaining P0_HIGH_RISK sources by ascending SourceId; P1 and P2 were not reached. Exact IDs and per-source scope are recorded in OFFICIALSOURCE_AUDIT_BATCH_02.csv. Status totals: {'CURRENT': 33, 'LEGACY': 1, 'UNCLEAR': 1}; action totals: {'KEEP': 31, 'UPDATE': 3, 'RETAIN_HISTORICAL': 1}. Core Batch 2 tuples: 208 adjudicated, 70 SUPPORTED, 138 NOT_SUPPORTED, unresolved 0. Cumulative Core: 126 SUPPORTED, 295 NOT_SUPPORTED, 75 REQUIRES_SOURCE_REVIEW. Coverage: {'COVERED': 81, 'UNSUPPORTED': 71, 'REQUIRES_SOURCE_REVIEW': 53, 'COVERED — MULTIPLE SOURCES': 14}. MATERIAL certified 63/144; still requiring review 37; unsupported 44. NumericDate certified 23/45; review 9; unsupported 13.

Compare Batch 2: 25 tuples adjudicated, 25 SUPPORTED, 0 NOT_SUPPORTED, unresolved 0. Cumulative Compare: 52 SUPPORTED, 1 NOT_SUPPORTED, 18 REQUIRES_SOURCE_REVIEW. Source-certified aspects remain 0/24; component-level audit does not certify a whole aspect. Dispositions remain 23 KEEP, 1 REVISE (protection-dates), 0 UNAVAILABLE, 0 NEWLY_AVAILABLE.

Cumulative claim-reopen candidates: FR-TP-01-C025, FR-EMP-01-C025, FR-HEALTH-01-C001, FR-SOC-01-C035. Batch 2 added FR-SOC-01-C035 for an overbroad ARS exclusion: Yvelines guidance is local, while the 2022 CNAF circular's ARS non-right instruction is case-specific. Source successor URLs: canonical Eduscol CASNAV URL; exact Légifrance R1112-13 article URL; Service-Public Oct 2026 lease-template update link (F920 needs freshness review). No new ADD source record was required to adjudicate selected tuples. Watches reviewed: FR-W06, FR-W09; both approved; no edit/remove/new watch. Remaining sources: 34 (P0 4, P1 13, P2 17).


## COMPLETE SOURCE AUDIT — 104/104

As of 19 November 2026. This closes Batch 3 without production changes.

## Source inventory

All 104 France-owned sources are substantively audited: Batch 1 35, Batch 2 35, Batch 3 34. Batch 3 selected all remaining IDs in deterministic priority/source-ID order: P0 4, P1 13, P2 17. Batch 3 source statuses: {'CURRENT': 30, 'LEGACY': 3, 'UNCLEAR': 1}. Batch 3 actions: {'KEEP': 30, 'UPDATE': 4}. Cumulative status totals: {'LEGACY': 5, 'CURRENT': 96, 'UNCLEAR': 3} (104 total). Cumulative actions: {'RETAIN_HISTORICAL': 2, 'KEEP': 94, 'UPDATE': 8} (104 total).

## Core evidence graph and claim coverage

Core graph: 496 exact tuples; 144 SUPPORTED, 352 NOT_SUPPORTED, 0 pending; duplicate tuples 0. Coverage: {'COVERED': 102, 'UNSUPPORTED': 97, 'COVERED — MULTIPLE SOURCES': 20}. MATERIAL certified 81/144, unsupported 63. NumericDate certified 27/45, unsupported 18.

MATERIAL unsupported ClaimIds: FR-TP-01-C006, FR-TP-01-C008, FR-TP-01-C010, FR-TP-01-C012, FR-TP-01-C025, FR-TP-01-C026, FR-TP-01-C018, FR-EMP-01-C005, FR-EMP-01-C006, FR-EMP-01-C021, FR-EMP-01-C010, FR-EMP-01-C023, FR-EMP-01-C012, FR-EMP-01-C027, FR-EMP-01-C016, FR-EMP-02-C007, FR-EMP-02-C013, FR-EMP-02-C024, FR-EMP-02-C017, FR-HEALTH-01-C010, FR-HEALTH-01-C026, FR-HEALTH-01-C028, FR-EDU-01-C022, FR-EDU-01-C023, FR-EDU-01-C006, FR-EDU-01-C024, FR-EDU-01-C025, FR-EDU-01-C011, FR-EDU-01-C030, FR-EDU-01-C034, FR-EDU-01-C021, FR-SOC-01-C025, FR-SOC-01-C026, FR-SOC-01-C027, FR-SOC-01-C005, FR-SOC-01-C006, FR-SOC-01-C010, FR-SOC-01-C028, FR-SOC-01-C015, FR-SOC-01-C033, FR-SOC-01-C035, FR-SOC-01-C020, FR-TP-02-C023, FR-TP-02-C025, FR-TP-02-C026, FR-TP-02-C009, FR-TP-02-C010, FR-TP-02-C020, FR-TP-02-C036, FR-HOUSING-01-C005, FR-HOUSING-01-C026, FR-HOUSING-01-C030, FR-HOUSING-01-C014, FR-HOUSING-01-C032, FR-HOUSING-01-C034, FR-HOUSING-01-C037, FR-EMP-02-C025, FR-EMP-02-C026, FR-EMP-02-C027, FR-EDU-01-C037, FR-SOC-01-C038, FR-EMP-01-C031, FR-TP-02-C041.

NumericDate unsupported ClaimIds: FR-TP-01-C025, FR-TP-01-C018, FR-EMP-02-C017, FR-HEALTH-01-C026, FR-HEALTH-01-C027, FR-EDU-01-C022, FR-EDU-01-C023, FR-EDU-01-C006, FR-EDU-01-C030, FR-EDU-01-C034, FR-EDU-01-C021, FR-SOC-01-C005, FR-SOC-01-C008, FR-SOC-01-C028, FR-EDU-01-C037, FR-EMP-02-C030, FR-SOC-01-C041, FR-SOC-01-C042.

Full active unsupported set (97 claims) and the separate reopen rows are in EVIDENCE_GAP_INPUT_01.csv. There are no remaining source-review states.

## Compare evidence

Compare graph: 71 tuples; 66 SUPPORTED, 5 NOT_SUPPORTED, 0 pending. Whole-aspect certified: 0/24. The aspect-by-aspect no-certification reasons and component gaps are recorded in COMPARE_DRAFT_01.md. Dispositions remain 23 KEEP / 1 REVISE (protection-dates) / 0 UNAVAILABLE / 0 NEWLY_AVAILABLE; the revised copy needs Task 135 evidence to keep EU horizon, 2026 new-grant cutoff, and French APS validity distinct.

## Reopen candidates

Final set (5): FR-TP-01-C025, FR-EMP-01-C025, FR-HEALTH-01-C001, FR-SOC-01-C035, FR-TP-01-C009. Task 134 retains the prior four candidates and adds FR-TP-01-C009 for the 5 August 2026 first-application military-obligation proof and continuity exception; do not rewrite until Task 135.

## Watch

Total 14. Outcomes: {'REQUIRES_GAP_RESOLUTION': 7, 'APPROVE': 7}. FR-W14 remains DECISION_EXPECTED: the official Service-Public PUMa page checked 13 May 2026 still says the decree is expected and page update will follow; no known decree date was located as of cutoff. Trigger date unknown; candidate remains valid. FR-W14 requires gap resolution because FR-HEALTH-01-C001 is reopened.

## Updates, historical sources and add candidates

Actionable UPDATE records (8):
- `bec70186-5af2-54e6-8ba3-2a4f8ce770c1` — https://www.alpes-maritimes.gouv.fr/Demarches/Espace-ressortissants-Ukrainiens/Protection-temporaire-premiere-demande-et-renouvellement → https://www.alpes-maritimes.gouv.fr/Demarches/Espace-ressortissants-Ukrainiens/Protection-temporaire-premiere-demande-et-renouvellement; https://www.alpes-maritimes.gouv.fr/Demarches/Prendre-un-rendez-vous; Alpes-Maritimes local page has 2026 fee/rule information but its listed next open weekends (17–18 Oct and 14–15 Nov 2026) have passed by the cutoff. Refresh the maintained local APS page/schedule before relying on appointment dates.
- `d9e7aa4b-7888-5f10-9781-05dadd3ca055` — https://www.legifrance.gouv.fr/codes/article_lc/LEGIARTI000033024092/2024-02-05 → https://www.legifrance.gouv.fr/codes/article_lc/LEGIARTI000033024092/2026-11-19; Pinned 5 February 2024 Code du travail version; same current article text can be linked at the current cutoff version.
- `f923322c-4336-5c6a-a971-f23901bb4ce2` — https://www.legifrance.gouv.fr/codes/id/LEGISCTA000006160947 → https://www.legifrance.gouv.fr/codes/article_lc/LEGIARTI000006908175; The linked section is an official current title page, but the exact cited Article R1112-13 has a direct canonical article URL. Article text requires urgent care and admission even without civil-status or reimbursement information; it says nothing about emergency phone numbers or charges.
- `fe6b9f9e-ea2b-5864-bd31-e4f19b566e7b` — https://www.education.gouv.fr/botexte/sp10020425/MENE0200681C.htm → https://www.service-public.gouv.fr/particuliers/vosdroits/F1866; https://www.service-public.gouv.fr/particuliers/vosdroits/F1898; https://www.service-public.gouv.fr/particuliers/vosdroits/F21304; 2002 circular remains useful for the no-residence-permit principle and mairie-to-school sequence but its 6–16 age rule predates the current 3–16 and 16–18 framework.
- `8e80f891-0c43-5ad7-a308-c54c4c55d097` — https://eduscol.education.fr/1201/centre-academique-pour-la-scolarisation-des-eleves-allophones-nouvellement-arrives-et-des-enfants-issus-de-familles-itinerantes-et-de-voyageurs-casnav → https://eduscol.education.gouv.fr/5493/centre-academique-pour-la-scolarisation-des-eleves-allophones-nouvellement-arrives-et-des-enfants-issus-de-familles-itinerantes-et-de-voyageurs-casnav; Eduscol content is current (Feb 2026) and describes CASNAV support, expertise and contacts. The legacy URL redirects to the current canonical URL, so update the record to that URL.
- `e7915cbf-568f-5224-9872-9331f849e885` — https://www.legifrance.gouv.fr/codes/section_lc/LEGITEXT000006070158/LEGISCTA000042906403/2026-07-16 → https://www.legifrance.gouv.fr/codes/section_lc/LEGITEXT000006070158/LEGISCTA000042906403; Pinned 16 July 2026 Annex 8 predates Decree 2026-817 effective 27 August 2026; update to current unpinned Annex 8/code schedule.
- `6ab4e1c0-eea2-5c1a-b231-50aff97e701e` — https://www.service-public.gouv.fr/particuliers/vosdroits/F33314 → https://www.legifrance.gouv.fr/codes/section_lc/LEGITEXT000006070158/LEGISCTA000042806148; https://www.legifrance.gouv.fr/codes/article_lc/LEGIARTI000042775671; https://www.legifrance.gouv.fr/codes/section_lc/LEGITEXT000006070158/LEGISCTA000042906403; Service-Public ADA page is accessible and explicitly says it is under revision after 2026 Pact decrees, including a decree of 6 Oct 2026. It also states TP beneficiaries may qualify and OFII manages ADA; full current eligibility/rate guidance is not yet settled on this page. No single official successor page was found in this targeted search.
- `d8411755-c986-5660-83c1-cbfb80100896` — https://www.service-public.gouv.fr/particuliers/vosdroits/F920 → https://www.service-public.gouv.fr/particuliers/actualites/A19067; F920 is official but last verified 6 Jun 2025; the required residential-lease templates changed from 1 Oct 2026 and the information notice changed from 18 Oct 2026. Existing rent/term/deposit/state-of-entry clauses remain relevant, but the page needs review against the 2026 official update.

UPDATE records without successor: 0 (none).

RETAIN_HISTORICAL records (2):
- `04f61765-f6e7-5f22-87c0-60f6382fa42c` — Interior Ministry page published 10 Mar 2022. It lists the original Ukraine-related categories and 2022 procedure; the 2026 EU military-obligation condition is absent. Keep for historical/category context, paired with the 2026 EU decision for current conditions.
- `aa0f80be-7a98-5b27-a6e2-38286081f4ed` — OFII document is a May 2024 ADA-rights extension form for APS-BPT holders, not a current initial-claim checklist. Targeted official search found no single replacement continuation form; retain only as historical/transition procedure evidence.

REMOVE records: 0; no REMOVE candidate is staged. ADD candidates from all three batches: 0.

Batch 1’s ADA overview successor issue is resolved: use the current CESEDA ADA provisions, CESEDA L. 581-9 for the temporary-protection route, and current Annex 8 tariff; the overview itself flags that it is being updated. The source remains an UPDATE recommendation with actionable official URLs.

## Integrity and Task 135 input

Owner conflicts 0; unintended same-owner+URL duplicates 0; Core dangling SourceIds 0; Compare dangling SourceIds 0. Source input file shows 104 audited and OFFICIALSOURCE_UNAUDITED_IDS_01.txt is finalized with zero IDs. EVIDENCE_GAP_INPUT_01.csv is the exact unsupported/reopen input for Task 135; broad gap research is not performed here.

Production/canonical: unchanged. Commit/push: none.


## Task 135 targeted official research (19 November 2026)

- EUR-Lex Decision (EU) 2026/1912, consolidated with Corrigendum C1: extension applies 5 March 2027–4 March 2028; military-condition proof may be an easily verifiable official paper/electronic document. https://www.legifrance.gouv.fr/codes/id/LEGISCTA000042775689/
- Légifrance CESEDA Chapter I: L.581-1 establishes French framework; L.581-3 distinguishes temporary protection benefit from temporary document; L.581-4 states it does not prejudge refugee recognition and cannot be combined with asylum-seeker status.
- Existing audited R.581-1 and R.581-4 records support adult application location/file categories and APS validity/renewal. Seine-Maritime source supports only its local filing and APS-renewal instructions.

Task 135 resolved 22 unsupported TP claims and cleared TP-01-C009 unchanged. Batch ledger and evidence graph are in EVIDENCE_GAP_BATCH_01.csv and SOURCE_CLAIM_LINK_AUDIT_01.csv. The corrected protection-dates Compare aspect is source-certified; FR-W01 is approved as a future review trigger.


## Task 136 source-owner preflight

The staging record `fecb425a-243b-5233-871d-2ce26b742a66` had OwnerId `b43097d3-2524-5e94-8ab4-9a384efeb995`, which is not the certified ArticleId for FR-TP-02. Corrected the staging OwnerId to `78be24cd-82f5-5f29-ab54-0013aa15ab93`. The SourceId remains unchanged because its recorded deterministic input is `france:TP02:CESEDA-Chapter-I:L581-3` and does not include OwnerId. No claim wording, evidence decision, or SourceId reference changed.


## EVIDENCE GAP BATCH 2 — EMPLOYMENT

Resolved all 22 unsupported employment claims and the FR-EMP-01-C025 reopen item. Outcomes: 7 SUPPORTED_NEW_OFFICIAL_SOURCE, 2 SUPPORTED_EXISTING_REEVALUATED, 2 REWRITE_TO_SUPPORTED_SAFE_CLAIM, 11 REMOVE_UNSUPPORTED_CLAIM, 1 REOPEN_CLEARED_NO_CHANGE; no blocked claims. All 23 batch rows are recorded in EVIDENCE_GAP_BATCH_02.csv. The two numeric/date gaps (FR-EMP-02-C017 and C030) are resolved.

New source records (all ADD, staging-only): 3a55eb20-dea4-5b00-bdef-7d932cf1fa31 (Légifrance L.5311-7, owned by FR-EMP-01); d277a456-4e10-5a6c-849a-e23ba7474991 (France Travail workshops, FR-EMP-01); bf5e5f83-b27d-5b60-aa2d-9e5a924b342e (France Travail registration/ARE, FR-EMP-01); fd583be7-629f-5004-a38d-136e1a85b2d4 (Service-Public F2300, gross/net and higher convention minimum, FR-EMP-02); 06d80225-6dde-5d61-94c8-48feac8d8e67 (Service-Public F2258, paid leave, FR-EMP-02); 6ed80718-151e-52d3-bebd-dd8f9eb07d38 (separate FR-EMP-02-owned Service-Public F23458 record for regulated professions). All records remain non-production.

France Travail: registration and an ARE application are typically completed together online for a first applicant, but payment is not automatic and entitlement conditions still apply. Employer-facing France Travail terminology is POEI for the individual employer-linked scheme; the candidate page also uses POE as an umbrella label, while POEC remains distinct. The review copy now states that distinction and avoids implying a funding/course/job guarantee.

SMIC: the 22 May 2026 Légifrance order establishes metropolitan gross rates from 1 June 2026 (€12.31/hour; €1,867.02/month at 35 hours) and Mayotte rates (€9.56/hour; €1,449.93/month). Service-Public confirms gross/net distinction, an illustrative net amount, and that a higher applicable collective minimum must be paid. No later enacted future rate was found in the selected official records as of 19 November 2026. The existing date watch remains approved; W13 was edited to track apprenticeship minima alongside SMIC review, clearing its gap-resolution status.

Compare impact: updated the employment-assistance wording to distinguish POEI/POE and added the Service-Public gross/net/higher-minimum tuple to work-minimum-pay. The latter is now source-certified for the France-side proposition; all Compare dispositions remain KEEP (24), REVISE (0), UNAVAILABLE (0), NEWLY_AVAILABLE (1). No other complete aspect was newly certified. Watch impact: W13 edited and approved; no new watch added; W02/W12 remain approved.

Post-batch active Core claims: 196 (130 MATERIAL, 66 SUPPORTING; 44 NumericDate). Active unsupported claims: 53 (32 MATERIAL; 14 NumericDate). Targeted active unsupported employment claims: 0. Reopen candidates remaining: FR-HEALTH-01-C001 and FR-SOC-01-C035. Source inventory: 112 staging records, 104 production-existing, 8 proposed ADD, 8 UPDATE, 2 RETAIN_HISTORICAL, 0 REMOVE.


## EVIDENCE GAP BATCH 3 — HEALTHCARE + HOUSING

Processed 20/20 items (19 unsupported claims plus the Healthcare reopen). Unsupported-claim outcomes: 3 SUPPORTED_EXISTING_REEVALUATED, 1 SUPPORTED_NEW_OFFICIAL_SOURCE, 7 REWRITE_TO_SUPPORTED_SAFE_CLAIM, and 8 REMOVE_UNSUPPORTED_CLAIM. The reopen FR-HEALTH-01-C001 was rewritten; no item is blocked. Two targeted NumericDate gaps are resolved.

New official ADD sources, both owned by FR-HEALTH-01 and staging-only: `9003daa2-1315-58ef-8237-e8147d3a5592`, Légifrance Social Security Code Article L.160-1-1, https://www.legifrance.gouv.fr/codes/article_lc/LEGIARTI000053266871/2026-10-16; and `77766134-f03b-5576-89df-6f38a08ed93c`, Assurance Maladie reimbursement-rate table, https://www.ameli.fr/assure/remboursements/rembourse/tableau-recapitulatif-taux-remboursement. No existing audited source action changed.

PUMa: the general coverage proposition was narrowed to distinguish the established coverage basis from the financial participation. Social Security Code L.160-1-1 has been in force since 31 Dec 2025; it imposes participation for the specified stable/regular non-working resident cohort, while the amount and collection/suspension modalities remain for a Conseil d’État decree. The current Service-Public PUMa page still says the decree is awaited, and the codified text in force on 16 Oct 2026 still delegates those modalities. No later implementing decree was found in the official records checked as of 19 Nov 2026. FR-W14 is therefore approved as DECISION_EXPECTED and edited to track that decree.

Healthcare claims: the current Service-Public emergency page directly supports 15 for medical emergencies, 112 for European emergencies and 114 access via SMS/app/internet for deaf, hard-of-hearing, deafblind and aphasic people. The old NOT_SUPPORTED call-number tuple was demonstrably erroneous and is marked superseded while preserved. The new Assurance Maladie rate table supports only the narrowed statement that reimbursement rates vary by care type. APS/CPAM proof and the malformed CESEDA fragment were removed; existing attestation, NIA/NIR, Carte Vitale, hospital admission and cost clauses remain scoped to their own sources. No targeted AME, medicine, maternity or separate CSS-eligibility claim required adjudication.

Housing claims: unsupported local-variation and “check local guidance” assertions were removed; the 115 route and Haute-Garonne local example remain expressly scoped. Private-rental wording now uses the allowed-document list without inferring APS acceptance, states the current prohibited discrimination grounds and objective criteria, and distinguishes specified-commune maximum rent caps from broader zone-tendue rent-increase controls. HLM application/allocation and wait times remain separate; APL conditions are stated separately from the local Caf des Yvelines APS-BPT guidance; FSL remains local; Visale is described only as conditional on current scheme eligibility. Lease, deposit, advance-rent and guarantee clauses were not broadened.

Compare impact: all six in-scope aspects were updated and source-certified for the final France-side wording: healthcare-access, healthcare-proof, healthcare-urgent, housing-initial, housing-private-rental, and housing-financial-support. Dispositions now total 18 KEEP and 6 REVISE, with 0 UNAVAILABLE. The existing NEWLY_AVAILABLE count of 1 remains an overlapping availability marker (protection-dates), not an additional aspect disposition.

Watch impact: FR-W14 edited and approved; it remains DECISION_EXPECTED because amount and implementation modalities are still pending. No housing watch or new candidate was warranted.

Post-batch active Core: 188 claims (126 MATERIAL, 62 SUPPORTING; 44 NumericDate). Remaining active unsupported: 34 (22 MATERIAL; 12 NumericDate). Targeted Healthcare/Housing active unsupported: 0. Remaining reopen candidates: FR-SOC-01-C035. Source inventory: 114 staging records, 104 production-existing, 10 ADD, 8 UPDATE, 2 RETAIN_HISTORICAL, 0 REMOVE.

## EVIDENCE GAP BATCH 4 — EDUCATION

Processed all 12 targeted unsupported Education claims. Outcomes: 1 SUPPORTED_EXISTING_REEVALUATED (FR-EDU-01-C030), 1 SUPPORTED_NEW_OFFICIAL_SOURCE (FR-EDU-01-C022), 3 REWRITE_TO_SUPPORTED_SAFE_CLAIM (FR-EDU-01-C023, C006, C035), 7 REMOVE_UNSUPPORTED_CLAIM (C024, C025, C027, C011, C034, C021, C037), and 0 blocked. Targeted active unsupported claims: 0. NumericDate targets resolved/removed: 7/7. No prior NOT_SUPPORTED tuple was incorrectly flipped; five exact new/retested tuples were added and all are SUPPORTED.

New staging-only official sources, both owned by FR-EDU-01: `ea7b16e1-7abc-5ebc-9521-2d45289a040d`, éduscol, “L’obligation de formation,” https://eduscol.education.gouv.fr/5301/l-obligation-de-formation; and `a8de9820-9b53-5b98-b1e7-b89cb427f71d`, éduscol, “Fiches repères — accueil au premier degré,” https://eduscol.education.gouv.fr/sites/default/files/document/eanafiche2accueilpremierdegre354152pdf-81183.pdf. The first page (Feb 2026) lists the distinct formation obligation for ages 16–18 and qualifying routes. The second sets out diagnostic assessment and class-selection factors for newly arrived elementary pupils, including prior schooling, needs and age.

School/CASNAV result: retain compulsory instruction at 3–16 and the separate 16–18 formation obligation; primary class selection is assessed against the pupil’s profile. CASNAV contributes expertise and support, while placement and local procedure remain with the relevant education authorities. Provisional admission and universal alternative-document checklist claims were removed because the official enrollment guidance does not establish them.

Language support result: retain assessment-led EANA/FLS/UPE2A support organized around pupil needs. Remove the unsupported fixed-hours negative and Ukrainian online-school equivalency claim. CNED was not implicated by any surviving proposition.

Higher-education result: retain distinct Parcoursup/DAP routes, the source-backed 2026 Ukrainian applicant advice to contact the institution, and institution-specific route confirmation. Preserve separation among admission, language evidence, ENIC-NARIC comparability, fees/CVEC and CROUS support. Rewrite fee guidance to state only that under the 2026/27 public-institution framework an institution may grant an individual exemption on application based on personal situation. Remove the malformed composite higher-education claim and unsupported blanket negatives about fee category and language exemption. Mon Master, CVEC amounts and fee totals were not changed.

Compare impact: revised and source-certified the three Education aspects `education-school-enrolment`, `education-pupil-language`, and `education-higher-admission`; all three have exact official source-link tuples. Compare disposition totals are now KEEP 15, REVISE 9, UNAVAILABLE 0, with the single NEWLY_AVAILABLE marker remaining an overlapping metadata marker.

Watch impact: FR-W05 is approved as a school-year-change review for current 2026 Ukrainian applicant guidance. FR-W07 and FR-W08 are removed because their fee-category and malformed-composite claims were inactivated. No new Watch candidate was added. Other Education Watch candidates were not changed.

Post-batch active Core: 181 claims (120 MATERIAL, 61 SUPPORTING; 41 NumericDate). Remaining active unsupported: 22 (12 MATERIAL; 5 NumericDate); all are Social claims. Cumulative open reopen candidates remain FR-SOC-01-C035. Source inventory: 116 staging records, 104 production-existing, 12 proposed ADD, 8 UPDATE, 2 RETAIN_HISTORICAL, 0 REMOVE. Production/canonical remain unchanged; no commit, push, CI, Docker or tests.


## EVIDENCE GAP BATCH 5 — SOCIAL (Task 139)

Processed the exact 22 remaining unsupported Social claims (12 MATERIAL, 10 SUPPORTING; 5 NumericDate). Outcomes: 3 SUPPORTED_EXISTING_REEVALUATED, 1 SUPPORTED_NEW_OFFICIAL_SOURCE, 12 REWRITE_TO_SUPPORTED_SAFE_CLAIM, 6 REMOVE_UNSUPPORTED_CLAIM, 0 blocked. CESEDA L. 581-9 was applied to the TP-specific ADA route only; OFII continuation-form instructions were not generalized to first claims or asylum status. RSA, Prime d’activité, family benefits, housing aid and ARE remain separate. C035 was restricted to CAF des Yvelines guidance and its reopen item was resolved. CNAF IT-2025-005 was added as a staging-only source under FR-SOC-01. Task 139 appended 23 supported exact tuples and preserved historical negative decisions. The Task 139 summary records exact propositions, source IDs, and metrics.

## EVIDENCE GAP RESOLUTION COMPLETE

Across Batches 1–5, the starting universe of 97 unsupported claims is resolved; the batch files also record reopen-only rows, which are not additional unsupported claims. Final staging claim universe: 175 active claims (117 MATERIAL, 58 SUPPORTING; 39 NumericDate), with 148 COVERED and 27 COVERED — MULTIPLE SOURCES; unsupported 0 and requires-source-review 0. Open reopen candidates 0. Staging inventory: 117 records (13 ADD, 8 UPDATE, 2 RETAIN_HISTORICAL, 94 KEEP, 0 REMOVE), with 104 production-existing sources. Compare dispositions: KEEP 12, REVISE 12, UNAVAILABLE 0; NEWLY_AVAILABLE remains one overlapping marker; 14/24 aspects currently marked source-certified. Watch: 14 total; candidate statuses 11 APPROVE, 0 EDIT, 3 REMOVE, 0 REQUIRES_GAP_RESOLUTION (separate audit outcomes: 9 APPROVE, 2 EDIT, 3 REMOVE; the two EDITs are pre-existing and outside Social scope). Owner conflicts and dangling Core/Compare source IDs are 0. EN/UK/RU Social copy is aligned. Next step: Task 140 independent final certification. No production/canonical integration or release action was performed.


## TASK 140 INDEPENDENT CERTIFICATION RESULT

Task 140 independently recomputed the active ledger, coverage, graph, source inventory, Compare, Watch and review-copy consistency. It corrected the stale Compare certification counter and normalized Watch final-status vs historical-audit-action semantics. The current Compare staging is 12/24 source-certified: ten aspects remain explicitly under source review and two Social aspects lack exact Compare source tuples. Three malformed active FR-TP-02 citation fragments, seven active proposition/copy omissions, and one unbound proposed ADD source also prevent integration. The package is **FRANCE FINAL CERTIFICATION BLOCKED**; no production change or new broad source research was performed. See `TASK_140_INDEPENDENT_FINAL_CERTIFICATION.md` for the full recomputation and exact blockers.


## FINAL CERTIFICATION BLOCKER REPAIR — Task 141 (19 November 2026)

Task 141 resolved the four Task 140 blockers in staging. The final active claim count is 172: 117 MATERIAL, 55 SUPPORTING, 36 NumericDate, with 165 CURRENT and 7 TRANSITION. Final article counts are FR-TP-01 20, FR-TP-02 21, FR-EMP-01 17, FR-EMP-02 19, FR-HEALTH-01 20, FR-HOUSING-01 26, FR-EDU-01 22, FR-SOC-01 27. Active unsupported claims and source-review gaps are both zero. Compare is source-certified 24/24 (KEEP 12, REVISE 12; zero unavailable/newly available primary dispositions; one secondary NEWLY_AVAILABLE marker remains on protection-dates); binding delta is KEEP 59 / ADD 15 / REMOVE 12 after final source-list reconciliation. The staged source plan remains 117 France records (13 ADD, 8 UPDATE, 2 RETAIN_HISTORICAL, 0 REMOVE; global planned 1,543). Core binding delta is KEEP 79 / ADD 13 / REMOVE 25. Task 140’s historical blocking verdict is preserved, with a repair note appended. Next step: Task 142, second independent certification; no production integration authorization is claimed here. See `TASK_141_FINAL_CERTIFICATION_BLOCKER_REPAIR.md` and `COMPARE_CERTIFICATION_REPAIR_01.md`.


## FINAL CERTIFICATION BLOCKER REPAIR — Task 141 (19 November 2026)

Task 141 resolved the four Task 140 blockers in staging. The final active claim count is 172: 117 MATERIAL, 55 SUPPORTING, 36 NumericDate, with 165 CURRENT and 7 TRANSITION. Final article counts are FR-TP-01 20, FR-TP-02 21, FR-EMP-01 17, FR-EMP-02 19, FR-HEALTH-01 20, FR-HOUSING-01 26, FR-EDU-01 22, FR-SOC-01 27. Active unsupported claims and source-review gaps are both zero. Compare is source-certified 24/24 (KEEP 12, REVISE 12; zero unavailable/newly available primary dispositions; one secondary NEWLY_AVAILABLE marker remains on protection-dates); binding delta is KEEP 59 / ADD 15 / REMOVE 12 after final source-list reconciliation. The staged source plan remains 117 France records (13 ADD, 8 UPDATE, 2 RETAIN_HISTORICAL, 0 REMOVE; global planned 1,543). Core binding delta is KEEP 79 / ADD 13 / REMOVE 25. Task 140’s historical blocking verdict is preserved, with a repair note appended. Next step: Task 142, second independent certification; no production integration authorization is claimed here. See `TASK_141_FINAL_CERTIFICATION_BLOCKER_REPAIR.md` and `COMPARE_CERTIFICATION_REPAIR_01.md`.


## SECOND CERTIFICATION BLOCKER REPAIR — TASK 143

Task 143 resolved the three Task 142 blockers without reopening the broader audit. Article 2 of EU Decision 2022/382 now supports narrowed current TP eligibility propositions C021–C024 through France-owned staging source `fdf859e7-da78-52eb-88e9-0343805006d8`; the 2022 Interior Ministry source remains LEGACY / RETAIN_HISTORICAL. Current CESEDA D.581-7 supports C039 through existing source `ba87b018-5dbb-5a3e-aeb9-c84e254ad507`; the 2024 OFII continuation form remains historical-only. The active Mayotte rate proposition C028, all EN/UK/RU review copies, and the work-minimum-pay Compare aspect now use the exact €9.56/hour and €1,449.93/month figures from 1 June 2026, aligned with the metropolitan figures. FR-W02 now tracks both territory rates. No production changes were made. Next step: Task 144 independent third final certification. See `TASK_143_SECOND_CERTIFICATION_BLOCKER_REPAIR.md` for adjudication, tuple counts and gates.


## THIRD CERTIFICATION BLOCKER REPAIR

Task 145 resolved Task 144’s three blockers in France staging. Decision 2022/382 received the recomputed deterministic UUIDv5 `fdf859e7-da78-52eb-88e9-0343805006d8`; C023 now states the Article 2(2) residence cutoff, valid Ukrainian-law permanent permit, and safe/durable return condition; and `protection-scheme` has a supported current source tuple for that eligibility cohort. The six historical-only CURRENT claims were narrowed to dated 2022/May 2024 artifact facts and reclassified HISTORICAL. Source plan remains 104 production / 118 staging, with 14 ADDs. Current next step: **Task 146 — fourth independent final certification**. Task 145 did not self-approve production integration.


## FOURTH CERTIFICATION BLOCKER REPAIR — Task 147

Task 147 resolves the four localized issues identified by Task 146. Compare `protection-scheme` now distinguishes Article 2(1) from Article 2(2), preserving the alternative of Decision (EU) 2022/382 or adequate protection under national law; the exact EUR-Lex tuple was retested as SUPPORTED and the primary disposition is REVISE. FR-SOC-01-C040 now appears in EN/UK/RU as an instruction in the dated May 2024 OFII form, explicitly not current filing guidance. Stale unrecognized SourceId `2e094655-76bf-4800-91a5-7b6e62823d20` was removed from four active ledger and four coverage candidate lists.

Production has 71 Compare aspect/SourceId pairs; the 24 staging candidate lists have 77. Set-based delta is KEEP 59 / ADD 18 / REMOVE 12, confirming Task 145. Task 146’s 71/18/0 used the 89-pair union as the staging set; this correction is documented in Task 146’s appended repair note. Pair-level audit: `COMPARE_BINDING_DELTA_FINAL_01.csv`.

The clean baseline remains 172 active claims; 117 MATERIAL / 55 SUPPORTING; 42 NumericDate; CURRENT 159 / FUTURE 0 / HISTORICAL 6 / TRANSITION 7; 0 unsupported; 0 requiring source review; Core graph 423 (210 SUPPORTED / 213 NOT_SUPPORTED / 0 unresolved). Source inventory remains 104 production / 118 staging / 14 ADD / 8 UPDATE / 2 RETAIN_HISTORICAL / 0 existing REMOVE.

**Next step: Task 148 — fifth independent final certification.** Task 147 does not approve integration.
