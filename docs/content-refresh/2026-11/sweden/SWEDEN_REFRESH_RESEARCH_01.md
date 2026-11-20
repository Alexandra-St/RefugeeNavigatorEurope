# Sweden refresh research — structural certification (19 November 2026)

## Production baseline

Eight Sweden articles have EN/UK/RU translations. Sweden owns 69 OfficialSources and 69 Core source/owner pairs. Global OfficialSource baseline: 1,544. Compare contains 24 aspects and 52 source bindings. The exact article IDs, aspect set, source ownership convention, duplicate URL groups and baseline are in `SWEDEN_PRODUCTION_BASELINE_01.md`. Production files, tests and fixtures are unchanged.

## Claim universe and atomicity

Task 150 began with 219 active claims from 233 English sentence units. Task 151 added 67 split parents and 152 atomic child rows, restored two production propositions omitted by the initial extractor, and inactivated 20 non-claims plus 3 redundant duplicates. Final ACTIVE total: 283 (SE-TP-01 43, SE-TP-02 30, SE-EMP-01 27, SE-EMP-02 28, SE-HEALTH-01 22, SE-HOUSING-01 25, SE-EDU-01 51, SE-SOC-01 57). MATERIAL 180; SUPPORTING 103; NumericDate 45; CURRENT 267; FUTURE 2; HISTORICAL 5; TRANSITION 9. Both sums reconcile. No active compounds, non-propositional rows, duplicate ClaimIds, active split parents, or orphan children remain. Duplicate dispositions are in `DUPLICATE_CLAIM_AUDIT_01.csv`; intentional contextual repetitions remain documented, and redundant split children are inactive.

## Translation alignment

All eight articles were reviewed in EN/UK/RU; the register covers 283/283 active claims. Material divergences, numeric/date divergences, missing propositions, and extra propositions: 0. Numeric/date token sequences match. No staging fixes were required.

## Candidate evidence graph and coverage

The 1,984 broad source/claim tuples were replaced with 518 claim-specific candidate tuples (1466 pruned). 130 claims have one candidate, 153 have multiple, and 0 have none. 60 sources have claim candidates; 9 have none: 6fd70e1b-fb73-46f1-aa8e-176ec5c280a3, 67b35a29-610f-49e3-b439-c2521683a619, 17fa9717-a2fb-467e-815a-f0628c3f2645, f241d32f-d41c-46f1-90c2-8d61eb4b2aea, 61666e4b-67f1-4ca3-894a-3717737eaf40, f8308a50-caa9-4628-9a6e-9b8311020dc5, c1ba3594-7cfc-4c92-abcb-c1819fdf63cd, 9ce9cc8b-cd96-4ba7-a62f-022c049e2b48, 92eff09d-9d6e-4478-9693-bba2cc41379c. All candidate decisions are pending substantive review. Current claims with a sole legacy candidate: SE-TP-02-C032.

Coverage: REQUIRES_SOURCE_REVIEW 283; UNSUPPORTED 0; COVERED/COVERED_MULTIPLE/NOT_EXTERNALLY_SOURCE_DEPENDENT 0. MATERIAL requiring review 180; NumericDate requiring review 45.

## Compare, Watch, and high-risk

Compare: 24 aspects reviewed; 52 candidate tuples remain pending. The multi-component aspects and their separate evidence needs are annotated in `COMPARE_DRAFT_01.md`. Six unique Watch candidates point to active claims; no unknown/inactive references remain. The high-risk register has 157 rows, all linked to active claims. The earliest future effective trigger is 1 January 2027 (SFI course structure, `SE-EDU-01-C046`); the next is 2 January 2027 (`SE-SOC-01-C055`). Recommended pre-check: 1 December 2026.

## Source audit plan

The 69-source inventory remains unchanged: P0 49, P1 20, P2 0. Propose four domain batches: TP/residence (21 sources); employment/healthcare (22); housing/education (17); social assistance (9). Include sole-legacy-candidate claim SE-TP-02-C032 as an early P0 review item.

All source and Compare evidence decisions remain pending; no substantive source audit was performed.


## Task 152 — Source Audit Batch 1 (19 November 2026)

Batch 1 audited 21 TP/residence sources (all P0), reviewed 160 existing/added candidate tuples: 47 SUPPORTED and 113 NOT_SUPPORTED, 0 pending. One existing-source candidate tuple was added for C032, taking graph total 519 (47 supported, 113 not supported, 359 pending). Source outcomes: 20 CURRENT/KEEP and one LEGACY/RETAIN_HISTORICAL (2025 change-of-track notice). Coverage and Compare/Watch audits are recorded in `TASK_152_SOURCE_AUDIT_BATCH_01_TP_RESIDENCE.md`. C032 is supported by current work-permit extension guidance; C008 is registered for claim reopening. Production remains unchanged.


## Task 153 — Source Audit Batch 2 (19 November 2026)

Inspected 22 employment/health sources (13 P0, 9 P1); 21 KEEP and one current but unused EURES source staged REMOVE. All 139 pre-existing Batch-2 Core tuples were adjudicated; 13 exact mapping-omission tuples were added (11 Socialstyrelsen healthcare pairs and two Arbetsförmedlingen compensation pairs), with all new tuples supported. C032’s persisted tuple/coverage/high-risk state was restored to the Task-152 SUPPORTED_CURRENT conclusion. See `TASK_153_SOURCE_AUDIT_BATCH_02_EMPLOYMENT_HEALTHCARE.md`.


## Task 155 — Batch 3 scope reconciliation and completion (2026-11-20)

Task 154's expected 17-source count was correct. Task 155 incorrectly claimed 18 owner records; source inventory and production integration counts confirm 17 (6 P0, 11 P1), all audited CURRENT/KEEP. The zero-Core Konsumentverket page was cited in the housing review copy, so two omitted claim mappings (C030/C031) were repaired; additional clear Skolverket/UHR mapping omissions were repaired and exact candidate dispositions recorded. Batch Core: 179 tuples, 88 supported, 91 not supported, zero pending. Cumulative Core: 582 (201/290/91). Coverage is 114 covered, 42 multiple, 70 unsupported, 57 review. C046/W-SE-05 was verified: the 2027-01-01 implementation boundary is supported; course plans entered into force 2026-08-15 and apply to courses starting after 2026-12-31. Compare source links reviewed; four new aspects certified and two affected aspects remain uncertified. See `BATCH_03_SCOPE_RECONCILIATION_01.md` and `TASK_155_SOURCE_AUDIT_BATCH_03_HOUSING_EDUCATION.md`. Production remains unchanged.


## Task 156 — Batch 4 and cumulative source-audit closure (2026-11-20)

Audited all 9 Social Assistance sources (all CURRENT/KEEP; all P0), adjudicated 123 Batch-4 Core tuples (61 supported, 62 not supported, zero pending) and 9 affected Compare pairs. C055 is SUPPORTED_FUTURE: SFS 2026:1552 takes effect 2027-01-02; W-SE-06 approved, with 2026-12-01 precheck. Four batches now cover 69/69 sources. Final Core 614 (262/352/0); final coverage 161 covered, 49 multiple, 73 unsupported, zero source-review pending. Compare 55 staged tuples, all adjudicated; 11/24 aspects certified. See `TASK_156_SOURCE_AUDIT_BATCH_04_SOCIAL.md`, `SOURCE_AUDIT_SYNTHESIS_01.md`, and `EVIDENCE_GAP_INPUT_01.csv`. Production/canonical files unchanged; ready for targeted evidence-gap resolution.


## Task 157 — evidence-gap Batch 1 (TP/residence)

Resolved the 34 targeted gaps on 20 November 2026. Deterministic owner-local ADD candidate 1be057c2-a5e8-5ff6-bae6-4ca54d853fd9 uses UUIDv5/NAMESPACE_URL with stable key `refugeenavigator:official-source:d2ab273d-21b7-44e4-b408-f4269a8753d8:https://eur-lex.europa.eu/eli/dir/2001/55/oj/eng` for Directive 2001/55/EC, Articles 26(1) and 26(4). Four claims were inactivated through one removal and three documented merges; 30 retained claims have exact official support. C008’s negative-scope reopen was rewritten to the exact Article 2(2) cohort. Six TP/residence Compare aspects were adjudicated and certified in staging. See TASK_157_EVIDENCE_GAP_BATCH_01_TP_RESIDENCE.md for claim-level dispositions and official-source links. Prior source-audit history remains intact. Production files were not changed.


## Task 158 — employment and healthcare evidence closure

The employment/healthcare gap batch resolved all 25 targets (20 retained claims covered; 5 unsupported low-value propositions removed), cleared SE-HEALTH-01-C007, and certified five Compare aspects. Three deterministic official source additions are staged: Arbetsförmedlingen introduction-programme guidance, national 1177 patient-fee guidance, and Arbetsmiljöverket posted-worker wage guidance. Sweden now has 274 active claims and 14 out-of-scope evidence gaps. See `TASK_158_EVIDENCE_GAP_BATCH_02_EMPLOYMENT_HEALTHCARE.md`. Production remains untouched.


## Task 159 housing and education evidence update (20 November 2026)

See `TASK_159_EVIDENCE_GAP_BATCH_03_HOUSING_EDUCATION.md`. Five deterministic staging ADD candidates were recorded for Migration Agency financial aid, Försäkringskassan housing allowance, Skolverket compulsory-school placement, UHR recognition FAQ, and the Higher Education Ordinance. The remaining three evidence gaps are in Social. Aggregate Core/Compare reconciliation remains pending.


## Task 160 Batch 3 reconciliation (20 November 2026)

Task 159 claim outcomes were preserved. The row-level transition contains 9 new Core SUPPORTED pairs and 16 retired old proposition/source pairs (net −7), yielding 613 effective Core tuples: 322 SUPPORTED, 291 NOT_SUPPORTED, 0 unresolved. The two remaining Compare aspects were verified and certified; all 24 aspects now have final staging dispositions. See `TASK_160_BATCH_03_RECONCILIATION.md`. Task 159’s original BLOCKED verdict remains preserved in its report.


## Task 161 final Social evidence-gap closure (20 November 2026)

Resolved the last three P0 gaps by narrowing each proposition to direct support from existing Socialstyrelsen and Migration Agency sources. The final active universe remains 272 claims; coverage, Core graph and staging binding deltas are reconciled in `SWEDEN_PRE_CERTIFICATION_01.md`. No new source was needed. Production remains unchanged.
