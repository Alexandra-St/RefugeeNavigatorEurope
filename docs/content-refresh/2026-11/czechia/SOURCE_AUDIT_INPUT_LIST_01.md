# Czechia source and claim coverage input list — final

As of 17 November 2026. This file points to the completed staging audit artifacts and records the final validated plans.

- Complete 207-row claim-to-source matrix: [CLAIM_SOURCE_COVERAGE_01.csv](CLAIM_SOURCE_COVERAGE_01.csv).
- Complete 57-record existing source inventory, source dispositions, six ADD specifications, claim coverage summary, Core plan, Compare plan, ownership and count reconciliation: [CZECHIA_REFRESH_RESEARCH_01.md](CZECHIA_REFRESH_RESEARCH_01.md).
- Final 24-row Compare content and source binding plan: [COMPARE_DRAFT_01.md](COMPARE_DRAFT_01.md).
- Final review copies: `CZ-TP-01_DRAFT_01.md`, `CZ-TP-02_DRAFT_01.md`, `CZ-EMP-01_DRAFT_01.md`, `CZ-EMP-02_DRAFT_01.md`, `CZ-HEALTH-01_DRAFT_01.md`, `CZ-HOUSING-01_DRAFT_01.md`, `CZ-EDU-01_DRAFT_01.md`, `CZ-SOC-01_DRAFT_01.md`.

## Validated counts

- Existing Czechia-owned sources 57; distinct URLs 42; audited 57/57. Status: CURRENT 56, LEGACY 1, BROKEN 0, UNCLEAR 0. Actions: KEEP 52, UPDATE 3, RETAIN HISTORICAL 0, REMOVE 2.
- Proposed source records: ADD 6 (all approved); REMOVE 2 (both approved); planned Czechia total 61; planned global total 1,498 from 1,494.
- Claim ledger: 207/207 rows, 199 existing and 8 New Important. Status: CONFIRMED 193, UPDATED 6, INCORRECT 0, NO LONGER SUPPORTED 0, QUALIFICATION REQUIRED 0, NEW IMPORTANT INFORMATION 8.
- Materiality: 176 MATERIAL, 31 SUPPORTING. Coverage: 36 COVERED, 171 COVERED — MULTIPLE SOURCES, 0 NOT EXTERNALLY SOURCE-DEPENDENT, 0 UNSUPPORTED. Numeric/date 51/51. Unsupported MATERIAL 0.
- Resolved claim reopens: `CZ-TP-01-C007`, `CZ-EDU-01-C022`, `CZ-EMP-01-C011`; Task 95 qualified both course claims to distinguish the general two-retraining/person criteria from the FAQ’s current one-Czech-language-course/person funding limit and prior-course restriction. Unresolved 0.
- Core binding plan: ADD 61, REMOVE 0. Compare binding plan: ADD 3, REMOVE 0; 24/24 audited; dispositions KEEP 20 / REVISE 4 / UNAVAILABLE 0 / NEWLY AVAILABLE 0.
- All planned SourceIds resolve; no material evidence gaps, owner conflicts or unintended owner+URL duplicates remain.

Task 96 integration: all 8 canonical drafts and 8 production articles are integrated. All 24 Czechia translations are Verified and dated `2026-11-17T00:00:00+00:00`. The approved source plan is applied (6 ADD, 3 UPDATE, 2 REMOVE; 61 Czechia / 1,498 global sources), 61 Core and 3 Compare bindings are in production. The 24 final Compare facts are integrated with the reviewed 20 KEEP / 4 REVISE dispositions. Local Release build and all 721 .NET tests pass; all 104 Python checks pass. Local Docker build/smoke was not run because the Docker daemon is unavailable. Commit, push, and exact-SHA CI follow after final diff review.
