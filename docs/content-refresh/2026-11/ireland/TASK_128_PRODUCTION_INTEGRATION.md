# Task 128 — Ireland production integration record

**Status:** BLOCKED before commit/push
**Branch at start:** `main`, clean tracked working tree at `a20c6be7dcbed46c97152b6f4f439505d3c39335` (`feat: refresh Norway content`)
**Approval reviewed:** Task 127 independent review, approved for integration.

## Partial integration completed

The working tree contains the approved 8 Ireland information-entry translations, 13 source-record additions, 3 source URL/title updates, 24 Compare facts and the final article/source drafts. Ireland source count is 119 and global source count is 1,529. The three historical source records remain present. The two requested copy fixes were applied: the repeated Ukrainian `тому` was removed from IE-EMP-02, and the duplicate English HSE ED-charge citation was removed from IE-HEALTH-01. The global source-count assertions in integration tests were advanced from 1,516 to 1,529.

## Release blocker

The targeted Ireland integration test found a source-ownership conflict in the approved Core binding plan and review copy:

- `docs/content-refresh/2026-11/ireland/CORE_SOURCE_BINDING_PLAN_01.csv` marks SourceId `185a8f60-39cf-5be9-8d05-29441e49dfc5` as **KEEP** for IE-TP-01 and **ADD** for IE-TP-02.
- The IE-TP-02 ADD row's `FinalActiveClaimIds` contain IE-TP-01 claim IDs only.
- The approved official-source inventory and production record assign this SourceId to IE-TP-01 (`51fc8ed8-123d-5a6f-b4fa-16cd2e41f3bf`). The IE-TP-02 review copy cites it in all three languages, while the repository's established Ireland integration test requires every cited source to be owned by that article.

The targeted test failed at `IrelandContentIntegrationTests.IrelandTranslationsAndOneHundredNineteenArticleOwnedSourcesMatchDraftsExactly` when IE-TP-02 English cited that non-owned SourceId. The other 7 tests in the targeted class passed. Resolving this requires changing the approved source-binding/citation decision or the source ownership model. Task 128 prohibits improvising that decision; therefore integration stopped before commit and push. Production, canonical drafts and test files are currently modified in the working tree for review, but the release is not validated for integration.

## Validation performed

- `dotnet test tests/RefugeeNavigator.Tests/RefugeeNavigator.Tests.csproj --configuration Release --filter FullyQualifiedName~IrelandContentIntegrationTests` — **failed: 1 failed, 7 passed**. Restore emitted NU1900 warnings because NuGet's vulnerability-cache path was inaccessible; dependencies were otherwise reported up to date.
- The test project compiled successfully as part of the command. Full solution build/test, Python CI checks and Docker validation were not run after the source-ownership blocker was found.
- JSON files parsed and the 24 Ireland Compare facts have approved status, all three translations, and resolving source IDs.

## Git/release outcome

- Final full diff review: **not completed; stopped at the blocker**.
- Commit: **none**. Subject: **not created**. SHA: **none**.
- Push: **not attempted**. Branch remains `main`; no force-push or history rewrite occurred.
- CI/workflow run: **none**.
- Publication status: **not published**.



## Task 129 correction and release outcome

The article-owned citation blocker was corrected by creating owner-local source 7c79b996-e285-5c86-971c-8676540d1226 for IE-TP-02 (UUIDv5 with the repository's documented NAMESPACE_URL key refugeenavigator:official-source:<owner-UUID>:<canonical-URL>). The exact invalid source 185a8f60-39cf-5be9-8d05-29441e49dfc5 remains owned and used by IE-TP-01. The new record repeats the same approved ISD FAQ URL/content under IE-TP-02 solely to meet article-owned citation architecture. The one binding is substituted, not duplicated. IE-TP-02 family-route citations in EN/UK/RU now resolve to the new owner-local ID. No content wording or evidence semantics changed. Claim/source audit artifacts and the final input list record the corrected IE-TP-02-C016 tuple. The same-URL cross-owner duplicate is intentional; same-owner+URL duplicates remain zero.

Revised counts: Ireland 120 source records; global 1,530; source ADD 14; Core binding deltas 100 KEEP / 14 ADD / 6 REMOVE. Compare remains unchanged at 24/24 source-certified, 17 KEEP / 7 REVISE / 0 unavailable / 0 newly available, with 77 KEEP / 2 ADD / 0 REMOVE bindings.

Task 129 validation completed before the release commit:

- Targeted Ireland integration suite: **8 passed / 0 failed** (`dotnet test tests/RefugeeNavigator.Tests/RefugeeNavigator.Tests.csproj --configuration Release --filter FullyQualifiedName~IrelandContentIntegrationTests`).
- Full solution suite: **732 passed / 0 failed** (`dotnet test RefugeeNavigatorEurope.slnx --configuration Release`).
- Repository Python CI checks: **104 passed / 0 failed** (`python3 -m unittest discover -s .github/scripts -p 'test_*.py' -v`).
- Structured JSON and Compare provider validation: **PASS** in the Ireland integration tests; Ireland Compare aspects **24/24** certified.
- Article citation ownership conflicts: **0**; Core dangling SourceIds: **0**; Compare dangling SourceIds: **0**; unintended Ireland same-owner+URL duplicates: **0**.
- Final counts: Ireland sources **120**, global sources **1,530**, source ADD **14**; Core binding deltas **100 KEEP / 14 ADD / 6 REMOVE**. Compare plan/dispositions were unchanged by Task 129.
- Mechanical language check: IE-TP-02 wording in EN/UK/RU is unchanged; only the owner-local source identifier changed.
- `git diff --check`: **PASS**. The final diff was reviewed; all changes are Ireland release content, its audit records, validation fixtures/counts and the two previously approved copy fixes.
- Docker build and smoke checks could not run locally because the Docker daemon socket was unavailable. The pushed GitHub CI workflow is expected to run the repository Docker steps; its result will be reported separately and is not presumed successful.

The single release commit and push are the next authorized actions on `main`; their SHA and resulting workflow run metadata are returned in the Task 129 completion response so this release record remains inside the single commit.
