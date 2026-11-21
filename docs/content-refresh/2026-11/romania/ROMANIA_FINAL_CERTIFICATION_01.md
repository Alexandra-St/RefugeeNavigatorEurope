# Romania final independent certification

**Status: APPROVED — READY FOR PRODUCTION INTEGRATION**  
**As of:** 21 November 2026  
**Certification basis:** independent read-only review after Task 196

## Certified claim state

- 81 unique active claims: RO-TP-01 13; RO-TP-02 13; RO-EMP-01 3; RO-EMP-02 13; RO-HEALTH-01 9; RO-HOUSING-01 9; RO-EDU-01 11; RO-SOC-01 10.
- Materiality: 62 MATERIAL + 19 SUPPORTING = 81.
- NumericDate: 12. Temporal classes: 74 CURRENT + 2 FUTURE + 0 HISTORICAL + 5 TRANSITION = 81.
- Inactive split parents: 24 (23 pre-existing plus RO-TP-02-C009). C009 is inactive and S01/S02/S03 are active. No duplicate active IDs, orphan lineage, missing parents, active split parents, or active compound claims.
- The 15 specified non-split removals remain inactive and have no active mapping.

## Localization and review copies

- All 81 active claims have one alignment row. Missing, duplicate, stale, placeholder and compound rows: 0. Material, NumericDate, missing-proposition, extra-proposition and temporal divergences: 0.
- Former blocker `RO-EMP-01-C001-S01` is aligned: EN “Job listings change over time.”; UK-UA “Оголошення про вакансії змінюються з часом.”; RU “Объявления о вакансиях со временем меняются.” The unsupported interview/employment disclaimer is absent from both localized spans.
- Checked all 8 review copies against their active ClaimId locator sets and EN / UK-UA / RU sections. Active claims missing from copies: 0; inactive/removed claims reintroduced: 0; orphan propositions and duplicate locators: 0. Task 196's target copy contains the corrected localized proposition.
- Review-copy citation graph: citations to old replaced IDs 0; unknown IDs 0; citations without an effective SUPPORTED tuple 0; effective supported tuples not locatable under the repository's article-level citation convention 0.

## Evidence, sources, and coverage

- Effective claim-source tuples: 104/104 SUPPORTED; NOT_SUPPORTED, PENDING and REVIEW_BLOCKED: 0.
- The two RO-EDU-01-C002 tuples using `1244a27d-7d6c-502f-b8f2-4cf68a5e7671` are effective SUPPORTED tuples. The source-claim audit records the direct Batch 3 adjudication of Order 4649/2026 for the claim-specific adult-course fee scope.
- Coverage: 61 COVERED + 20 COVERED_MULTIPLE = 81; UNSUPPORTED, REQUIRES_SOURCE_REVIEW and NO_EXISTING_CANDIDATE: 0.
- Active high-risk claims: 59; all 59 have final state SOURCE_CERTIFIED and COVERED with final source IDs. Final evidence gaps, pending items, and review-blocked items: 0.
- Gap dispositions: 39/39 RESOLVED; duplicate effective dispositions: 0.
- Source set: 56 production baseline − 6 old IDs + 7 final IDs = 57 final sources. The six old IDs are absent from active claim refs, final Core, Compare, Watch and review-copy refs. Final SourceIds are unique; same-owner canonical duplicates, owner conflicts, dangling owners and provisional final actions: 0.
- All seven final-added IDs reproduce under UUIDv5 URL namespace using `refugeenavigator:official-source:{OwnerArticleId}:{CanonicalURL}`. The historical audit action for old `2d6c45be-df4c-5064-ad7b-423a9d7bcea4` remains REPLACE_CANDIDATE; its final reconciled action is REPLACE to `d3a3d139-2774-5024-8cb7-02d3404dac48`. Both old `501ff4ab-87d5-514d-b2a5-ac95fc628881` and old 2d6 resolve into that one final source.

## Core, Compare, and Watch

- Core set comparison: 56 production pairs; 50 KEEP, 7 ADD, 6 REMOVE; 57 final pairs. Independently reconstructed set difference is exactly 7 additions and 6 removals. Multiple effective actions, dangling sources, unknown articles, wrong-owner pairs, duplicate final pairs, removed-source final pairs and missing required owner pairs: 0. The seven repaired former dual-action pairs are KEEP-only.
- Exact Core ADD pairs: RO-TP-01 × `eac2f2ab-c8a6-5b6f-a147-af80096ebce6`; RO-TP-01 × `d4f3d26a-e604-5eb1-972b-de9ae691af0b`; RO-TP-02 × `d3a3d139-2774-5024-8cb7-02d3404dac48`; RO-TP-02 × `643e0f53-5acd-5ad8-9371-b4cf42fe851e`; RO-EMP-02 × `c587c8b0-1ef3-5124-b335-44e82158a480`; RO-EMP-02 × `b672a40e-3adb-5f60-97ab-6303e8b060de`; RO-EMP-02 × `c5bf046c-6006-5e16-bd7a-9b30d6bc3554`.
- Exact Core REMOVE pairs: RO-TP-02 × `501ff4ab-87d5-514d-b2a5-ac95fc628881`; RO-TP-02 × `2d6c45be-df4c-5064-ad7b-423a9d7bcea4`; RO-TP-02 × `ee3461f2-62c1-5515-aff2-4abf1b958a8a`; RO-EMP-02 × `643a3def-a38f-59f5-b83e-c09284188c61`; RO-EMP-02 × `70cda0bf-de1c-54bc-b411-386e4dbec151`; RO-EMP-02 × `e4283f53-f70c-5eff-a2c7-3a78fc1615d7`.
- Compare: 24 aspects (11 KEEP / 13 REVISE); 66/66 effective tuples SUPPORTED; ownership violations, dangling sources, duplicate pairs and old-ID bindings: 0.
- Watch: 5/5 valid and linked. W001 exact start 2027-03-05; W002 expires 2028-03-04; W003 rate update with no exact next date; W004 annual admission-plan change; W005 school-year change. Invalid/inactive claims, dangling sources, duplicate IDs, duplicate semantic watches, unlinked rows and old-source references: 0.

## Validation and release boundary

Read-only checks recomputed claim counts and lineage; checked all active alignment rows and the Task 196 target in the review copy; checked effective claim-source and coverage rows; checked all review-copy citation IDs against final sources and supported tuples; recomputed source set and UUIDv5 values; compared production and final Core pairs; checked Compare ownership, Watch references, gap dispositions, high-risk final state, and all eight review-copy locator sets. Production/canonical files are unchanged. No commit, push, CI, tests, or production integration was performed.

No Romania-specific staging test target exists. The repository has Romania production integration tests; they are release validation and were not run against staging.

**Certification verdict: ROMANIA FINAL CERTIFICATION APPROVED — READY FOR PRODUCTION INTEGRATION**
