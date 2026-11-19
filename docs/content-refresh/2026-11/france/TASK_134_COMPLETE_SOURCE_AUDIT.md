# France complete source audit — Task 134

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
