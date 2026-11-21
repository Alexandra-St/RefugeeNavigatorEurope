# Task 189 — Romania Source Audit Batch 2

**As of:** 20 November 2026. **Audit recorded:** 21 November 2026.

## Completion

- Audited all 24/24 assigned sources directly; priorities reconcile to P0 14, P1 6, P2 4.
- Currentness: 22 CURRENT, 2 LEGACY; 0 SUPERSEDED, BROKEN, NOT_RELEVANT or UNCLEAR.
- Source action: KEEP 18, UPDATE 6; 0 REMOVE, RETAIN_HISTORICAL, REPLACE_CANDIDATE or REVIEW_BLOCKED.
- Source-action arithmetic is 24/24. No source IDs were regenerated and no source records were added.

## Graph and coverage delta

- Adjudicated all 33 candidate claim-source tuples: 16 SUPPORTED; 17 NOT_SUPPORTED; 0 PENDING / REVIEW_BLOCKED. Partial limits are explicitly described in the rationale fields.
- 28 claims received Batch-2 source review. Final coverage: 16 COVERED; 0 COVERED_MULTIPLE; 12 UNSUPPORTED; 0 REQUIRES_SOURCE_REVIEW; 0 NO_EXISTING_CANDIDATE.
- Unsupported claims: RO-EMP-01-C001-S02, RO-EMP-01-C001-S03, RO-EMP-01-C002, RO-EMP-01-C003, RO-EMP-02-C007-S01, RO-EMP-02-C007-S02, RO-EMP-02-C007-S03, RO-EMP-02-C008-S01, RO-HEALTH-01-C004-S01, RO-HEALTH-01-C004-S02, RO-HEALTH-01-C004-S03, RO-HEALTH-01-C006.
- NEW_SOURCE_NEEDED: RO-EMP-01-C001-S02/S03, RO-EMP-01-C002, RO-HEALTH-01-C004-S01/S02/S03. NARROWER_REWRITE_NEEDED: RO-EMP-01-C003, RO-EMP-02-C007-S01/S02/S03, RO-EMP-02-C008-S01, RO-HEALTH-01-C006. No removal or temporal reclassification was recommended.

## Employment, work and healthcare findings

- RO-EMP-02-C005: SUPPORTED_AS_WRITTEN; CURRENT. General minimum wage is 4,325 lei gross/month from 1 July 2026 (166.667 normal hours/month; 25.949 lei/hour).
- RO-EMP-02-C006: SUPPORTED_AS_WRITTEN; CURRENT as of 20 November 2026. Construction minimum is 4,582 lei gross/month from 1 January 2025 (165.334 normal hours/month; 27.714 lei/hour). It coexists with the general rate. OUG 29/2026 repealed the separate agricultural/food provision.
- RO-EMP-02-C008-S01 needs narrower wording: OUG 96/2024 art.19(3)-(5) specifies qualification self-declaration, aptitude verification and probation for the particular no-documents route; it does not establish generic “formalities still apply.” C008-S02 is supported by Labour Code job description and aptitude requirements.
- RO-HEALTH-01-C003 is supported with qualification: current Tax Code CASS rules may apply to covered salary income subject to exceptions; the OUG 96 special package is separately contribution-free for its covered categories.
- Temporal recommendations: RO-EMP-02-C001 TRANSITION; RO-EMP-02-C005 CURRENT; RO-EMP-02-C006 CURRENT; RO-HEALTH-01-C005 CURRENT. No additional temporal reopen candidates.

## Compare, watch and high-risk

- 22 Compare tuples adjudicated: 16 SUPPORTED, 6 NOT_SUPPORTED, none pending. Aspect dispositions: employment-vacancies REVISE; employment-institutions KEEP; employment-assistance-conditions REVISE; work-permission KEEP; work-employer-duties KEEP; work-minimum-pay KEEP; healthcare-access KEEP; healthcare-proof KEEP; healthcare-urgent KEEP. No ownership violations.
- Watch: W-003 wage-rate update watch EDIT; all other Batch-2 relevant watch rows assessed with no change. Exact event: general minimum wage 4,325 lei gross/month effective 1 July 2026; no next future effective date identified. The construction rate remains a separate current rate.
- Reviewed 21 high-risk rows: 14 SOURCE_CERTIFIED; 7 EVIDENCE_GAP; 0 pending cross-batch. Evidence-gap rows: RO-EMP-01-C001-S02/S03, RO-EMP-01-C003, RO-EMP-02-C008-S01, RO-HEALTH-01-C004-S01/S02/S03.

## Metadata and legacy candidates

- Updates: 0320ee26-8918-5e10-8022-a64bd60d708c Authority/title/use; 643a3def-a38f-59f5-b83e-c09284188c61 URL/canonical official alternate; 70cda0bf-de1c-54bc-b411-386e4dbec151 Authority/title/URL/canonical official IGI PDF; 33154c72-87f0-5edb-bbcd-445c9fa66ba3 date-limited role; 0168ea01-48ba-5a2b-93fd-b42cfcd1d54d date-limited role; e4283f53-f70c-5eff-a2c7-3a78fc1615d7 URL/canonical official PDF.
- SourceIds requiring regeneration later after canonical URL changes: 643a3def-a38f-59f5-b83e-c09284188c61, 70cda0bf-de1c-54bc-b411-386e4dbec151, e4283f53-f70c-5eff-a2c7-3a78fc1615d7. No regeneration in this task.
- Owner-specific 70cda… employment role and abdf3c49-b02d-56bd-abbb-486792f2901e healthcare role are valid despite shared instruments/URLs.
- Batch-2 Task 184 legacy candidates audited: 33154… LEGACY/UPDATE; 0168… LEGACY/UPDATE. No replacement source was created; no explicit replacement candidate was recorded.

## Readiness

Batch 2 is complete with no pending tuple or review blocker. Evidence gaps remain for downstream evidence-gap resolution; they do not prevent continuing to Batch 3. Production/canonical content, OfficialSources, Core, Compare, tests and other countries were not modified. No commit, push, CI or Docker action was performed.

**Verdict: ROMANIA SOURCE AUDIT BATCH 2 COMPLETE — READY FOR BATCH 3**
