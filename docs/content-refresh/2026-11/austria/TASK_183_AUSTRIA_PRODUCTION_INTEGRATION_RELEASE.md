# Task 183 — Austria production integration and release validation

As of 20 November 2026. Verdict: **AUSTRIA PRODUCTION INTEGRATION RELEASED**.

## Certified baseline and production integration

Applied the Task 182 approved plan without substantive additions or changes. Production commit: `cb41da55a280251f1ece9fafefc0faf3664c7197` (`feat: refresh Austria content`). It is on `main` and is the exact SHA validated by both required workflows.

- All 8 Austria articles were updated in EN, UK-UA, and RU from the certified review copies. Production has 104 active claims: TP-01 11; TP-02 12; EMP-01 10; EMP-02 10; HEALTH-01 16; HOUSING-01 16; EDU-01 15; SOC-01 14. Material/supporting: 78/26; NumericDate: 18; temporal Current/Future/Historical/Transition: 98/1/3/2. The certified coverage ledger records 67 Covered, 37 Covered_Multiple, 143 supported claim-source tuples, and 0 unresolved.
- OfficialSources: 57 baseline; KEEP 53, UPDATE 2 (`0d13b6f8-ebfd-544e-866f-a5881902da1f`, `17de6eb2-d2e0-5a2d-8d86-bfe8582526e2`), REMOVE 1 (`f336a2fc-f1ad-5a37-8041-03462e7d842a`), RETAIN_HISTORICAL 1 (`bb9aa455-4d25-5e92-b61a-6c2cbbc56ea1`), ADD 7. Final Austria sources: 63. Additions: `b96e9edb-4ec2-5dcf-a799-ed2dab72d728`, `b6e032c0-80ac-5582-9655-5dfaa3052919`, `3ffef08a-6045-5fad-8770-fe8c26e93915`, `bb44f895-eabf-518c-b336-99dd5c63021d`, `f9621fb0-0021-56b8-9e7a-048c81693147`, `66dbb552-df18-5624-9c79-d5c3b2b5c47e`, `b9903026-fbc3-509c-b629-deabd5e1faab`.
- Core ownership pairs: 57 baseline; KEEP 56, ADD 7, REMOVE 1; final 63. The b990 source is owned by AT-TP-02 (`dc76be21-d9dc-5070-83ca-9a7468d78e71`) and its matching Core pair exists exactly once. AT-TP-02 × `21a8767c-cd2c-5b9c-b1f5-eb14958d2def` remains present.
- Compare: 24/24 source-certified aspects; KEEP 13, REVISE 11, BLOCKED 0; 57 baseline bindings, KEEP 52, ADD 3, REMOVE 5; final 55, all supported. Residence-filing uses only the three AT-TP-02-owned sources `867f2e4e-1f48-591d-9819-8de70bd8c903`, `fe7206c3-7361-5a98-be23-e10902661348`, and `b9903026-fbc3-509c-b629-deabd5e1faab`. The old cross-owner residence-filing × `e7b5f714-7b45-5cce-9ea3-e1c3ff1a2ec0` binding is absent.
- Watch remains documentation-only: 3 rows (AT-W-001 APPROVE, AT-W-002 EDIT, AT-W-003 EDIT); no runtime Watch mechanism was introduced. High-risk: 82/82 source-certified; evidence-gap dispositions: 15/15 resolved, unresolved 0. Translation divergences: 0.

## Production-shaped preflight

Validated the actual production data files that were committed: 8 Austria entries, 24 verified locale records, 63 Austria OfficialSources (1,558 total across the repository), 63 source-owner Core pairs, 24 Austria Compare facts, and 55 effective Compare bindings. Source IDs are unique; no same-owner canonical duplicates or owner conflicts; all source and Core owners match. All inline article citations and Compare bindings resolve to sources owned by their article. The removed source has no Austria content, Core, Compare, or Watch reference. The b990 OfficialSource, owner pair, and residence-filing Compare binding are present; the old e7b5 residence-filing link is absent. No active current claim is solely supported by the retained historical source. The four removed claim propositions are absent; the social-care successor retains “at least six months” and “more than 65 hours”; 4 August and 5 August 2026 remain distinct; no NAG §24 pending-stay protection was generalized to §41a(7b).

Pre-commit review found only the three intended production catalog files, eight certified Austria review copies used by the integration checks, and related tests. Other-country production content was unchanged; other-country test edits only update the repository-wide source-count expectation from 1,552 to 1,558.

## Local validation

- Targeted Compare ownership test: `dotnet test tests/RefugeeNavigator.Tests/RefugeeNavigator.Tests.csproj --configuration Release --no-build --no-restore --filter 'FullyQualifiedName~AustriaHasTwentyFourAvailableApprovedFactsWithCompleteMappingsAndPreservedHolds'` — 1 passed, 0 failed, 0 skipped.
- Article/source ownership test: `dotnet test tests/RefugeeNavigator.Tests/RefugeeNavigator.Tests.csproj --configuration Release --no-build --no-restore --filter 'FullyQualifiedName~AustriaDraftsMatchCatalogAndSourcesAreArticleOwnedAndCited'` — 1 passed, 0 failed, 0 skipped.
- Full suite: `dotnet test RefugeeNavigatorEurope.slnx --configuration Release --no-restore` — 733 total, 733 passed, 0 failed, 0 skipped.
- Release build: `dotnet build RefugeeNavigatorEurope.slnx --configuration Release --no-restore` — succeeded, 0 errors. NU1900 warnings noted restricted access to the local NuGet vulnerability-cache path.

## Remote workflow validation

Both repository workflows were triggered by the production commit push to `main`, and each run reports that exact full head SHA:

| Workflow | Run ID | Event / branch | Head SHA | Conclusion |
|---|---:|---|---|---|
| CI | 44831075278 | push / main | `cb41da55a280251f1ece9fafefc0faf3664c7197` | success |
| Netherlands visual evidence | 44831075345 | push / main | `cb41da55a280251f1ece9fafefc0faf3664c7197` | success |

Remote `main` was queried after the production push and returned `cb41da55a280251f1ece9fafefc0faf3664c7197`, matching and containing the production SHA. No workflow dispatch or alternate-SHA result was used.

## Release disposition

The production state matches the Task 182 certified final plan: 104 active claims; 63 Austria OfficialSources; 63 Core pairs; 55 Compare bindings across 24/24 certified aspects; 3 Watch documentation records; 82/82 high-risk claims source-certified; 15/15 evidence gaps resolved. Production integration is released. This report is the only file in the subsequent docs-only validation commit; no production data is changed in that commit.
