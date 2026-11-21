# Romania Source Audit — Final State

As of 21 November 2026. Final source-audit closure after Task 191.

## Sources
- Production sources: 56/56 directly audited across four batches. Batch 4: 7/7, priorities P0 6, P1 1, P2 0.
- Currentness: CURRENT 52, LEGACY 4, SUPERSEDED 0, BROKEN 0, NOT_RELEVANT 0, UNCLEAR 0; sum 56.
- Actions: KEEP 43, UPDATE 12, REMOVE 0, RETAIN_HISTORICAL 0, REPLACE_CANDIDATE 1, REVIEW_BLOCKED 0; sum 56.
- Replacement action source: 2d6c45be-df4c-5064-ad7b-423a9d7bcea4. No new source record or replacement added.
- SourceId regeneration candidates: 501ff4ab-87d5-514d-b2a5-ac95fc628881, ee3461f2-62c1-5515-aff2-4abf1b958a8a, 643a3def-a38f-59f5-b83e-c09284188c61, 70cda0bf-de1c-54bc-b411-386e4dbec151, e4283f53-f70c-5eff-a2c7-3a78fc1615d7. Do not regenerate during source audit.

## Claim-source graph and coverage
- Effective claim-source tuples: 130; SUPPORTED 69; NOT_SUPPORTED 61; PENDING_SOURCE_REVIEW 0; REVIEW_BLOCKED 0.
- ACTIVE claims: 94. COVERED 44; COVERED_MULTIPLE 11; UNSUPPORTED 39; REQUIRES_SOURCE_REVIEW 0; NO_EXISTING_CANDIDATE 0.
- Unique unsupported claims: 39; by article: RO-EDU-01 3; RO-EMP-01 4; RO-EMP-02 4; RO-HEALTH-01 4; RO-HOUSING-01 4; RO-SOC-01 5; RO-TP-01 8; RO-TP-02 7.
- NEW_SOURCE_NEEDED (18): RO-TP-01-C003-S01, RO-TP-01-C006-S01, RO-TP-01-C006-S02, RO-TP-01-C006-S03, RO-TP-01-C007, RO-TP-01-C009, RO-TP-01-C010, RO-TP-02-C006-S02, RO-TP-02-C007-S01, RO-TP-02-C009, RO-EMP-01-C001-S02, RO-EMP-01-C001-S03, RO-EMP-01-C002, RO-HEALTH-01-C004-S01, RO-HEALTH-01-C004-S02, RO-HEALTH-01-C004-S03, RO-EDU-01-C004-S02, RO-EDU-01-C004-S03.
- NARROWER_REWRITE_NEEDED (21): RO-TP-01-C004-S01, RO-TP-02-C006-S01, RO-TP-02-C007-S02, RO-TP-02-C007-S03, RO-TP-02-C008, RO-EMP-01-C003, RO-EMP-02-C007-S01, RO-EMP-02-C007-S02, RO-EMP-02-C007-S03, RO-EMP-02-C008-S01, RO-HEALTH-01-C006, RO-EDU-01-C001-S02, RO-HOUSING-01-C002-S01, RO-HOUSING-01-C002-S02, RO-HOUSING-01-C002-S03, RO-HOUSING-01-C002-S04, RO-SOC-01-C002-S01, RO-SOC-01-C002-S02, RO-SOC-01-C002-S03, RO-SOC-01-C004, RO-SOC-01-C007-S03. No removal or temporal reclassification gap is currently assigned; no evidence gaps are review-blocked.
- NumericDate evidence gaps: RO-TP-01-C004-S01. High-risk subset: 0; high-risk evidence gaps total 30: RO-EDU-01-C001-S02, RO-EDU-01-C004-S02, RO-EDU-01-C004-S03, RO-EMP-01-C001-S02, RO-EMP-01-C001-S03, RO-EMP-01-C003, RO-EMP-02-C008-S01, RO-HEALTH-01-C004-S01, RO-HEALTH-01-C004-S02, RO-HEALTH-01-C004-S03, RO-HOUSING-01-C002-S01, RO-HOUSING-01-C002-S02, RO-HOUSING-01-C002-S03, RO-HOUSING-01-C002-S04, RO-SOC-01-C002-S01, RO-SOC-01-C002-S02, RO-SOC-01-C002-S03, RO-SOC-01-C004, RO-SOC-01-C007-S03, RO-TP-01-C003-S01, RO-TP-01-C006-S03, RO-TP-01-C007, RO-TP-01-C009, RO-TP-01-C010, RO-TP-02-C006-S02, RO-TP-02-C007-S01, RO-TP-02-C007-S02, RO-TP-02-C007-S03, RO-TP-02-C008, RO-TP-02-C009.

## Compare source audit
- Compare aspects: 24; effective source tuples: 67; SUPPORTED 54; NOT_SUPPORTED 13; PENDING 0; REVIEW_BLOCKED 0; ownership violations 0.
- Overall aspect dispositions are: KEEP 11; REVISE 13; UNAVAILABLE 0; NEWLY_AVAILABLE 0; BLOCKED 0; REVIEW_REQUIRED 0.
- Aspect list: protection-scheme REVISE; protection-application REVISE; protection-dates REVISE; residence-routes REVISE; residence-conditions KEEP; residence-filing KEEP; employment-vacancies REVISE; employment-institutions KEEP; employment-assistance-conditions REVISE; work-permission KEEP; work-employer-duties KEEP; work-minimum-pay KEEP; healthcare-access KEEP; healthcare-proof KEEP; healthcare-urgent KEEP; housing-initial REVISE; housing-private-rental KEEP; housing-financial-support REVISE; education-school-enrolment REVISE; education-pupil-language REVISE; education-higher-admission REVISE; social-subsistence KEEP; social-application REVISE; social-child-benefits REVISE.

## Watch and high risk
- Watch rows: 5. RO-W-001 (TP extension start 2027-03-05), RO-W-002 (scheme endpoint 2028-03-04), RO-W-003 (minimum wage/rate update; no next date), RO-W-004 (ANNUAL_ADMISSION_PLAN_CHANGE; no exact date), RO-W-005 (SCHOOL_YEAR_CHANGE; unlinked). Four rows are source-audited and ready for final reconciliation; W-005 remains an unlinked candidate for later assessment. Batch-4 relevant Watch rows: 0; no new Watch candidate.
- High-risk rows: 74; source-certified 44; evidence-gap 30; review-blocked 0; pending 0.

## Batch 4 disposition
- All seven sources CURRENT/KEEP; 26 claim tuples (17 supported, 9 not supported), 13 Compare tuples (9 supported, 4 not supported); 13 social claims source-audited. Evidence gaps: five social claims, all NARROWER_REWRITE_NEEDED. No source metadata/URL update, no SourceId regeneration, no production changes.
- Source audit has no blockers; ready for full reconciliation and evidence-gap resolution.
