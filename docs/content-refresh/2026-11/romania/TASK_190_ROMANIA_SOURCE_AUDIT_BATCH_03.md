# Task 190 — Romania Source Audit Batch 3

**As of:** 21 November 2026. **Recorded:** 21 November 2026.

## Completion and dispositions

- All 14/14 assigned sources directly audited; priorities reconcile to P0 12, P1 2, P2 0.
- All 14 sources are CURRENT; source actions: KEEP 12, UPDATE 2; no remove, historical-retention, replacement-candidate or review-blocked outcomes.
- The two updates are scope/use notes for the 2024 ROFUIP original PDF and the 2022 adult Romanian course methodology, with current fees set separately by Order 4649/2026. No URL/canonical URL changed; all 14 SourceIds remain stable.
- The OUG 96 source rows `491030f2…` (housing) and `4b7f9f52…` (education) remain valid owner-specific entries despite the shared instrument. The Task-184 legacy candidate `3ef0a209…` was audited and is CURRENT/UPDATE because the course methodology remains relevant, with fees set separately by 2026 Order 4649.

## Claim graph and coverage

- All 29 Batch-3 candidate claim-source tuples were adjudicated; 21 SUPPORTED, 8 NOT_SUPPORTED, 0 PENDING/REVIEW_BLOCKED. Two NOT_SUPPORTED tuples have PARTIAL_SUPPORT: `RO-EDU-01-C001-S02` (the source supports the statutory placement route, not the broad guarantee boundary) and `RO-HOUSING-01-C002-S01` (the source supports programme/local designation, not current-capacity generality).
- 23 claims had their complete candidate sets audited: 11 COVERED, 5 COVERED_MULTIPLE, 7 UNSUPPORTED, 0 REQUIRES_SOURCE_REVIEW and 0 NO_EXISTING_CANDIDATE.
- Unsupported claims: `RO-EDU-01-C001-S02`, `RO-EDU-01-C004-S02`, `RO-EDU-01-C004-S03`, `RO-HOUSING-01-C002-S01`, `RO-HOUSING-01-C002-S02`, `RO-HOUSING-01-C002-S03`, `RO-HOUSING-01-C002-S04`.
- Evidence-gap dispositions: NARROWER_REWRITE_NEEDED—`RO-EDU-01-C001-S02`, `RO-HOUSING-01-C002-S01/S02/S03/S04`; NEW_SOURCE_NEEDED—`RO-EDU-01-C004-S02/S03`. No claim was removed or temporally reclassified.
- Housing registration claims C004-S01/S02/S03 are supported: qualifying lessor/designated owner, not tenant, files under the ANAF procedure within 30 days; defined personal-property income scope and exceptions apply. Housing C005’s cohort, 750/2,000 lei amount and one three-month period are supported; C006’s local application and months-two/three conditions/exceptions are supported; C007’s lump-sum, non-open-ended description is supported.
- Temporal recommendations: RO-HOUSING-01-C004-S01 CURRENT; C005 CURRENT; C006 CURRENT; RO-EDU-01-C004-S01 CURRENT (ongoing plan measure with annual allocation). Temporal reopen candidates were resolved with no reclassification.

## Translation reopens

- `RO-HOUSING-01-C001`: source baseline is the statutory new-beneficiary TP cohort and two distinct accommodation channels (temporary camps; locally designated accommodation/lump-sum route), with an expired separate transition for earlier beneficiaries. Decision: NARROW_ALL_LANGUAGES. EN/UK-UA mention eligibility/designated settings without exact cohort/channel limits; RU omits the statutory cohort and precise settings. Future correction: narrow all versions to eligibility and distinguish accommodation channels.
- `RO-EDU-01-C006`: source baseline is the covered Ukraine-minor cohort, expressly including temporary-protection permit holders and minors receiving humanitarian support under the stated asylum/international-protection scope. Decision: NARROW_ALL_LANGUAGES. EN names TP but omits full cohort/exclusions; UK-UA/RU use vague “defined/certain children” and omit explicit TP/humanitarian routes. Future correction: align all versions to the exact cohort.
- Both translation reopens fully adjudicated (2/2); no production translation was edited.

## Compare, watch and high-risk

- All 15 Batch-3 Compare tuples adjudicated: 13 SUPPORTED, 2 NOT_SUPPORTED, none pending. Aspect dispositions: housing-initial REVISE; housing-private-rental KEEP; housing-financial-support REVISE; education-school-enrolment REVISE; education-pupil-language REVISE; education-higher-admission REVISE. Compare ownership violations: 0.
- Relevant Watch rows: W-004 only; EDIT to use `RO-EDU-01-C003` for annual admission-information changes. Keep ANNUAL_ADMISSION_PLAN_CHANGE, no exact date/precheck date. W-005 has no linked claim/source and is not a Batch-3-linked row. No exact dates were invented.
- Reviewed 18 Batch-3 high-risk rows: 11 SOURCE_CERTIFIED; 7 EVIDENCE_GAP (`RO-EDU-01-C001-S02`, `RO-EDU-01-C004-S02/S03`, `RO-HOUSING-01-C002-S01/S02/S03/S04`); 0 STILL_PENDING_CROSS_BATCH.

## Readiness

No new or replacement sources were added. The remaining evidence gaps are scoped for later resolution and do not block Batch 4. Canonical content, production OfficialSources/Core/Compare, tests and other countries were not changed. No commit, push, CI or Docker action was performed.

**Verdict: ROMANIA SOURCE AUDIT BATCH 3 COMPLETE — READY FOR BATCH 4**
