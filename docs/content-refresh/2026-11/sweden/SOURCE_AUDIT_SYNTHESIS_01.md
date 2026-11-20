# Sweden source-audit synthesis — 20 November 2026

## Audit closure

All 69 Sweden production sources have completed source audit in four batches: Batch 1 21, Batch 2 22, Batch 3 17, Batch 4 9. Each source has a final status and action. No production/canonical files or tests changed; no commit or push occurred.

## Final 69-source inventory

Status totals: CURRENT 68, LEGACY 1, UNCLEAR 0, BROKEN 0 (69 total). Original production action totals: KEEP 67, UPDATE 0, RETAIN_HISTORICAL 1, REMOVE 1 (69 production sources). Staging adds: Task 157 1, Task 158 3; planned count 72. UPDATE sources: none. RETAIN_HISTORICAL: 78f08f69-cbf2-4715-8a6c-faedcec8be27. REMOVE: f241d32f-d41c-46f1-90c2-8d61eb4b2aea. No CURRENT claim relies solely on a legacy or retained-historical source.

## Final Core evidence graph and coverage (after Tasks 157–158)

Core evidence graph after Task 158: 620 effective tuples; 313 SUPPORTED, 307 NOT_SUPPORTED, 0 unresolved. Task 158 added 20 supported tuples and retired 33 superseded claim/source tuples (net −13). Active claims: 274; COVERED 210, COVERED_MULTIPLE 50, NOT_EXTERNALLY_SOURCE_DEPENDENT 0, UNSUPPORTED 14, REQUIRES_SOURCE_REVIEW 0.

MATERIAL 176: covered 137, multiple 30, review 0, unsupported 9. NumericDate 45: covered 35, multiple 10, review 0, unsupported 0.

Final active unsupported ClaimIds (14): SE-EDU-01-C007, SE-EDU-01-C008, SE-EDU-01-C010, SE-EDU-01-C013, SE-EDU-01-C027, SE-EDU-01-C034, SE-EDU-01-C061, SE-EDU-01-C063, SE-EDU-01-C064, SE-HOUSING-01-C003, SE-HOUSING-01-C005, SE-SOC-01-C056, SE-SOC-01-C072, SE-SOC-01-C073. Full gap rows and directions are in `EVIDENCE_GAP_INPUT_01.csv`.

## Reopen registers

Unresolved claim reopens: 0. SE-TP-01-C008 was cleared in Task 157 and SE-HEALTH-01-C007 was cleared in Task 158; the resolved healthcare row is retained in the register for audit history. Temporal reopen candidates: none. Translation reopen candidates: none. No CURRENT claims rely solely on LEGACY/RETAIN_HISTORICAL support.

## Compare (after Task 158)

Staged Compare evidence tuples: 61 (61 SUPPORTED, 0 NOT_SUPPORTED, 0 unresolved). Certified aspects 22/24: protection-scheme, protection-application, protection-dates, residence-routes, residence-conditions, residence-filing, employment-vacancies, employment-institutions, employment-assistance-conditions, work-permission, work-employer-duties, work-minimum-pay, healthcare-access, healthcare-proof, healthcare-urgent, housing-initial, housing-private-rental, education-school-enrolment, education-pupil-language, social-subsistence, social-application, social-child-benefits. Uncertified: housing-financial-support, education-higher-admission. The five Task 158 target aspects were all adjudicated; two KEEP and three REVISE. `social-child-benefits` remains newly available as a staging candidate only. Production Compare remains 24 aspects/52 bindings.

Uncertified aspect gap components:

- **housing-financial-support:** reviewed accommodation guidance does not establish general rent reimbursement through that route or resolve separate benefit eligibility.
- **education-higher-admission:** admission, fee exemption, and all qualification/document exceptions are not jointly certified by the current candidate bindings.

## Watch and high-risk (after Task 158)

Watch candidates (6): W-SE-01 APPROVE, W-SE-02 APPROVE, W-SE-03 APPROVE, W-SE-04 EDIT, W-SE-05 APPROVE, W-SE-06 APPROVE. Counts: Counter({'APPROVE': 5, 'EDIT': 1}). Earliest certified future legal/effective trigger: 2027-01-01 (SFI implementation); earliest maintenance precheck: 2026-12-01. All Watch ClaimIds are active, WatchIds unique, SourceIds known.

High-risk register: 108 rows; source-certified 75; evidence gaps 33; claim reopens 0; temporal reopens 0. Batch-4 high-risk rows 17, certified 17, evidence-gap rows 0.

## Structural integrity and preliminary bindings

Owner conflicts 0; same-owner canonical duplicates 0; dangling Core SourceIds 0; wrong-owner Core bindings 0; dangling Compare SourceIds 0. Four cross-owner duplicate URL groups were touched in Batch 4; owner-specific records remain separate: Arbetsförmedlingen Ukraine guidance (`e4d07c65-fb07-4f6f-b46a-3d05dfc752d1` / `b4ec…`), Migration Agency 1 October Reception Act notice (`57d10098-a6a9-4cbb-9242-8ba46fe93418` / `90a…`), Reception Act 2026:999 (`c1ba3594-7cfc-4c92-abcb-c1819fdf63cd` / `15871…`), and Socialstyrelsen Ukraine rights FAQ (`2f022132-dcbb-4b15-817f-1e9542e63710` / `61666e4b-67f1-4ca3-894a-3717737eaf40`; URL differs only by `www.`). Production Core baseline: 69 pairs. Preliminary source/binding candidates: KEEP 68, ADD 3 claim/Core candidates; one additional Task 158 source is Compare-only; REMOVE 1. Production Compare baseline: 24 aspects/52 bindings. Active atomic claim universe after Task 158: 274.

## Recommended evidence-gap batches

1. P0: unsupported material and NumericDate legal/eligibility claims; the current legal and social-benefit claims should be handled first.
2. P1: practical process, agency routes, and lower-risk propositions.
3. Separate claim-reopen and Compare wording review; decide whether to obtain further official sources, narrow a claim, or remove it.

No evidence gap has been resolved by changing claims, temporal classes, translations, production bindings, or OfficialSources in Task 156.


## Task 158 evidence-gap closure

Task 158 resolved all 25 employment and healthcare gaps: 2 existing-source support, 5 new-source support, 13 rewrites, 5 removals, 0 temporal reclassifications, 0 merges, and 0 blocked gaps. Active claims are now 274 and the remaining queue is 14. The SE-HEALTH-01-C007 claim reopen is closed. Three deterministic UUIDv5 source additions are staged. All five employment/healthcare Compare targets are source-certified, raising cumulative Compare certification to 22/24. The remaining gap queue contains 14 housing, education, and social rows. See `TASK_158_EVIDENCE_GAP_BATCH_02_EMPLOYMENT_HEALTHCARE.md`.
