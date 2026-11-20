# Task 165 — Sweden production integration and release

**As of:** 20 November 2026
**Certification authority:** Task 164 independent recertification
**Certification verdict:** SWEDEN APPROVED FOR INTEGRATION

## Integrated certified state

The integration started from the Task 164 approved state. No new legal or content research was performed. The eight final EN/UK/RU article copies were applied to the eight existing Sweden article records, preserving their IDs. All 24 translations match their certified review copies; the certified proposition and translation gates passed. The production set represents 272 active claims: 175 MATERIAL, 97 SUPPORTING, 45 NumericDate, 255 CURRENT, 3 FUTURE, 5 HISTORICAL, and 9 TRANSITION. Coverage remains 222 COVERED and 50 COVERED_MULTIPLE, with zero unsupported, source-review, reopen, temporal-reopen, or translation-reopen claims. Material and NumericDate divergences, missing or extra propositions, stale propositions, and user-facing audit/meta phrases: 0.

Article codes updated: `SE-TP-01`, `SE-TP-02`, `SE-EMP-01`, `SE-EMP-02`, `SE-HEALTH-01`, `SE-HOUSING-01`, `SE-EDU-01`, and `SE-SOC-01` (8/8).

## Source and binding deltas

- Sweden OfficialSources: 69 before; KEEP 67, UPDATE 0, RETAIN_HISTORICAL 1, REMOVE 1, ADD 9; 77 after. Global OfficialSources: 1,544 before and 1,552 after.
- Retained historical record: `78f08f69-cbf2-4715-8a6c-faedcec8be27`. Removed record: `f241d32f-d41c-46f1-90c2-8d61eb4b2aea`.
- Added records: `1be057c2-a5e8-5ff6-bae6-4ca54d853fd9`, `43dfe833-392f-5c17-8a44-0b4afe18213c`, `7bc8c19f-a8da-56a7-b243-ea0a4e6c528b`, `9fa47f80-b71c-5bab-acf4-c776609f1ce0`, `1c223817-2971-5ce2-bac3-2d2baa41011e`, `dfb4d353-b6be-57ae-b238-9e142907e55a`, `ce3256c3-3694-5a83-90c5-b0edc3784fa0`, `be9c8da3-450b-599a-afcb-b6f26de3958e`, and `320c1cb2-afa2-5a5c-a009-2ae8cbba2046`.
- Same-owner canonical URL duplicates, source-owner conflicts, global duplicate source IDs, dangling or wrong-owner Compare bindings, duplicate Compare pairs, and operative references to the removed employment ID: 0. Correct employment source ID `43dfe833-392f-5c17-8a44-0b4afe18213c` is present.
- Core article/source pairs: 69 before; KEEP 59, ADD 7, REMOVE 10; 66 after. Additions: `SE-EDU-01 × 320c1cb2-afa2-5a5c-a009-2ae8cbba2046`, `SE-EDU-01 × be9c8da3-450b-599a-afcb-b6f26de3958e`, `SE-EDU-01 × ce3256c3-3694-5a83-90c5-b0edc3784fa0`, `SE-EMP-01 × 43dfe833-392f-5c17-8a44-0b4afe18213c`, `SE-HEALTH-01 × 7bc8c19f-a8da-56a7-b243-ea0a4e6c528b`, `SE-HOUSING-01 × 1c223817-2971-5ce2-bac3-2d2baa41011e`, and `SE-TP-01 × 1be057c2-a5e8-5ff6-bae6-4ca54d853fd9`.
- Core removals: `SE-EDU-01 × c8d6a8e9-0512-4933-b223-2ea0e685b0d7`; `SE-EMP-01 × 4801dab3-29e2-446e-934f-3970ffa5f772`; `SE-EMP-02 × f241d32f-d41c-46f1-90c2-8d61eb4b2aea`; `SE-HEALTH-01 × 275c1c14-3caa-4343-aa7e-67689edfa9bb`; `SE-TP-01 × 108b10ab-13a5-4d5b-8102-dc7e65dbdf77`; `SE-TP-01 × 6fd70e1b-fb73-46f1-aa8e-176ec5c280a3`; and `SE-TP-02 × 06e41800-28b2-4cf4-9885-9453373bd7a0`, `SE-TP-02 × 17fa9717-a2fb-467e-815a-f0628c3f2645`, `SE-TP-02 × 78f08f69-cbf2-4715-8a6c-faedcec8be27`, `SE-TP-02 × 7a6e7566-6de2-4c7a-9b52-0f8b8ccfeaf7`. Set arithmetic and owner/reference checks passed.

## Compare and Watch

- Sweden Compare: 24 aspects before and after; 52 source bindings before, 65 after. KEEP 16 aspects, REVISE 7 (`protection-scheme`, `residence-conditions`, `work-employer-duties`, `work-minimum-pay`, `healthcare-urgent`, `housing-financial-support`, `education-higher-admission`), NEWLY_AVAILABLE 1 (`social-child-benefits`), UNAVAILABLE 0. Binding actions: KEEP 52, ADD 13, REMOVE 0. The exact additions are listed in `COMPARE_BINDING_FINAL_PLAN_01.csv`. `protection-dates` adds `e17f7ee0-b423-42b3-9c9c-280d33c54298`; `62a4c75c-ef6f-4e14-8f74-74d188d0e858` was already in the baseline.
- Watch set: 6 rows; APPROVE 5, EDIT 1 (`W-SE-04`), REMOVE 0, REQUIRES_GAP_RESOLUTION 0. Invalid ClaimIds, dangling SourceIds, and duplicate WatchIds: 0. Earliest maintenance pre-check: 1 December 2026. Earliest future trigger: 1 January 2027; next exact future event: 2 January 2027. W-SE-04 remains an early-2027 extension opportunity with no exact opening date asserted.
- Active high-risk evidence gaps: 0. The seven non-certified high-risk rows remain inactive-only.

## Validation

- `dotnet test RefugeeNavigatorEurope.slnx --configuration Release --no-restore` — PASS, 733 passed, 0 failed, 0 skipped.
- `dotnet build RefugeeNavigatorEurope.slnx --configuration Release --no-restore` — PASS, 0 errors; two NU1900 warnings arose from denied access to the local NuGet vulnerability-cache path.
- `python3 -m unittest discover -s .github/scripts -p 'test_*.py' -v` — PASS, 104 tests.
- `dotnet run --project tools/RefugeeNavigator.ContentAudit --configuration Release --no-build -- --output /private/tmp/sweden-task165-content-audit.md` — PASS; 1,552 sources and 0 relationship issues.
- `git diff --cached --check` — PASS. Source/article ownership, translation-copy equality, exact Core and Compare plans, schema loading, source counts, and Sweden production regression tests passed.
- Local Docker build could not connect because the local Docker daemon socket was unavailable. The exact-SHA CI workflow successfully built the production Docker image and ran Docker integration smoke tests.
- Review-copy Markdown/CSV and canonical article text had end-of-line whitespace normalized only; no claim wording or decisions changed.

## Production commit and exact-SHA workflows

- Branch: `main`; commit subject: `feat: refresh Sweden content`.
- Production content commit: `9e41f090d62d0b580978f4c5db1cf5da3c8d41ce`; push to `origin/main` succeeded and the remote head matched this SHA immediately after the production push.
- CI run [44783680246](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/44783680246): head SHA `9e41f090d62d0b580978f4c5db1cf5da3c8d41ce`; success.
- Netherlands visual evidence run [44783680252](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/44783680252): head SHA `9e41f090d62d0b580978f4c5db1cf5da3c8d41ce`; success.
- Both required workflows completed successfully on the exact production content SHA. A docs-only release-report commit follows this content commit, consistent with the France release-report convention; it makes no production-content change.

## Final production counts

- Sweden articles: 8; OfficialSources: 77; Core pairs: 66; Compare aspects/bindings: 24/65; Watch rows: 6.
- Global OfficialSources: 1,552. Sweden source arithmetic, Core pair arithmetic, Compare binding arithmetic, and exact source-owner relationships passed.
- No unrelated country production data changed. The neighboring integration-test expectations were updated for the global source total and Sweden's newly available comparison fact.
- **Verdict:** SWEDEN PRODUCTION INTEGRATION RELEASED — READY FOR EXTERNAL VERIFICATION.
- Sweden is not declared closed; external independent GitHub verification remains outstanding.
