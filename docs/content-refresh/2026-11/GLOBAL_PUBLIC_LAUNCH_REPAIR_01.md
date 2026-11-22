# Global public-launch repair 01

Date: 22 November 2026

## Starting state

- Starting SHA: `b340797dfe757c250dd8bccbd9d7457ef26cc3ab` (clean `main`; `origin/main` matched at start).
- Reconciled all 19 country histories against their committed audit/repair artifacts and the current production catalog.
- Expected already-clean countries: NL,DE,HU,BE,FI,LT,PL,CZ.
- Expected open-finding countries: IT,SK,DK,BG,ES,NO,IE,FR,SE,AT,RO.
- Expected ledger: P0=0, P1=12, P2=48, total=60.
- Current confirmed open before edits: 60. Already resolved on current main: 0. Not a defect: 0.
- Every expected open finding is listed once in the ledger and recorded RESOLVED after the content/source/Compare changes below.

## Country reconciliation

| Country | Start P0 | Start P1 | Start P2 | Remaining P0 | Remaining P1 | Remaining P2 | Disposition |
|---|---:|---:|---:|---:|---:|---:|---|
| NL | 0 | 0 | 0 | 0 | 0 | 0 | RECHECKED_NO_BLOCKER / prior repair retained |
| DE | 0 | 0 | 0 | 0 | 0 | 0 | RECHECKED_NO_BLOCKER |
| HU | 0 | 0 | 0 | 0 | 0 | 0 | RECHECKED_NO_BLOCKER / housing P2 already repaired |
| BE | 0 | 0 | 0 | 0 | 0 | 0 | RECHECKED_NO_BLOCKER / BE-P1-001 already repaired |
| IT | 0 | 0 | 2 | 0 | 0 | 0 | OPEN_FINDINGS_REPAIRED |
| FI | 0 | 0 | 0 | 0 | 0 | 0 | CLEAN / former process placeholder removed |
| LT | 0 | 0 | 0 | 0 | 0 | 0 | CLEAN / watch candidates only |
| PL | 0 | 0 | 0 | 0 | 0 | 0 | CLEAN / four prior findings already repaired |
| SK | 0 | 2 | 5 | 0 | 0 | 0 | OPEN_FINDINGS_REPAIRED |
| DK | 0 | 1 | 3 | 0 | 0 | 0 | OPEN_FINDINGS_REPAIRED |
| CZ | 0 | 0 | 0 | 0 | 0 | 0 | CLEAN / Task 221 no findings |
| BG | 0 | 2 | 4 | 0 | 0 | 0 | OPEN_FINDINGS_REPAIRED |
| ES | 0 | 0 | 5 | 0 | 0 | 0 | OPEN_FINDINGS_REPAIRED |
| NO | 0 | 0 | 4 | 0 | 0 | 0 | OPEN_FINDINGS_REPAIRED |
| IE | 0 | 0 | 2 | 0 | 0 | 0 | OPEN_FINDINGS_REPAIRED |
| FR | 0 | 3 | 6 | 0 | 0 | 0 | OPEN_FINDINGS_REPAIRED |
| SE | 0 | 1 | 5 | 0 | 0 | 0 | OPEN_FINDINGS_REPAIRED |
| AT | 0 | 3 | 5 | 0 | 0 | 0 | OPEN_FINDINGS_REPAIRED |
| RO | 0 | 0 | 7 | 0 | 0 | 0 | OPEN_FINDINGS_REPAIRED |

## Disposition summary

P0 remaining: 0. P1 remaining: 0. P2 remaining: 0. Unresolved high-risk: 0.

All 60 expected findings are marked `RESOLVED` in `GLOBAL_PUBLIC_LAUNCH_REPAIR_LEDGER_01.csv`. Affected source metadata, Core citations, Compare text and bindings, and EN/UK/RU propositions are recorded per finding there. No historical audit report was edited.

## Changed production files

- `src/RefugeeNavigator.Web/Data/information-entries.json`: localized content updates for the affected findings, including the 12 P1 and 48 P2 dispositions.
- `src/RefugeeNavigator.Web/Data/official-sources.json`: updated official source URLs/titles, added owner-local EURES and law records, removed duplicate/dead records, and preserved deterministic owner-based IDs.
- `src/RefugeeNavigator.Web/Data/comparison-facts.json`: localized Compare proposition and binding repairs for affected aspects.

Changed catalog counts from a semantic comparison with the starting SHA:

- Countries changed: 11 (IT, SK, DK, BG, ES, NO, IE, FR, SE, AT, RO).
- Article entries changed: 39.
- Compare fact records changed: 40.
- Translation records changed: 115.
- Official sources added: 6; updated: 12; removed: 3.

## Validation

- JSON parsing: PASS for all three production catalogs.
- Static source/Core/Compare ownership and reference checks: PASS (0 dangling Core refs, 0 wrong-owner Core refs, 0 dangling Compare source refs, 0 wrong-owner Compare refs, 0 duplicate Compare bindings, 0 duplicate source IDs).
- `git diff --check`: PASS.
- .NET changed-country integration tests and source/localization integrity tests: BLOCKED. The checkout has no restored `project.assets.json`; `dotnet test --no-restore` did not execute test targets. `dotnet restore` failed in NuGet migration with an unavailable named mutex (`NuGet-Migrations`) in the sandbox.
- Release build: BLOCKED for missing `project.assets.json`; the prescribed `--no-restore` build was attempted and failed with NETSDK1004.
- Static comparison of the three affected locales confirms all changed propositions are present in EN/UK/RU with aligned dates, scope and exceptions; known semantic divergences: 0.

## Repair commit and launch state

Repair commit SHA: the single `fix: resolve global launch audit backlog` commit containing this report; its full SHA is recorded in the Task 230 final return. A commit cannot embed its own full object SHA without a second commit.

Task 230 repair content and source work is complete; final recertification readiness remains `NO` until the blocked .NET tests, integrity suite and Release build pass in a toolchain with restored NuGet assets.

The final global recertification / CI task remains pending. Password protection and private beta status were not changed; no site was published.

## Source record changes

- Added (6): 01c93379-a45d-5da0-9b7f-07cfd595013b, 13031914-aded-56ff-8034-db4e198070b1, 3ec94ac7-3c1a-51f1-9cc6-93118307cc0b, 436991bc-fea3-5d09-826f-d8cfdee31573, 7634654d-0321-5a85-8f1b-5b0d4327e331, 7eee6a0a-96ec-5963-8e10-f8080468459b.
- Updated (12): 44c2a53e-f0ab-5eb3-b167-016b7e89c86d, 4639994e-692e-5fdd-80a1-e65fbb61f9da, 49ae3d06-a9a3-5319-bf00-520c542d44c2, 4cd09057-d389-5bf3-bcbc-5e4853bff7fa, 55dfb9f1-bee9-54a7-867a-0c2379dca38e, 7343237a-23ed-546e-96be-3e308e41aa33, 7e6c51ae-784c-5494-acdd-6e44526fef30, 823a0fe1-53b7-57ee-ae9b-69719219eb3e, 92763c9a-e086-5432-be40-770681f92d95, a3d23582-5dd0-5ae8-ba1e-c7fb48e46e7a, a73b7d5b-720e-5d02-8014-32cc416c5b5d, d3520d2c-2fc2-4cdf-89dc-eb192aa9b64a.
- Removed (3): 2b033f12-8f12-5d32-a767-66b34dbfaa08, aeec0350-1919-56f6-9a21-872a8fb11f58, fe6b9f9e-ea2b-5864-bd31-e4f19b566e7b.
- Replacement details: current MVR Migration Directorate and Sofia RDE routes; France Travail first appointment/workshop and Haute-Garonne Ukraine routes; current Seine-Saint-Denis APS route; Spain BOE 2027 SMI decree; WRC NMW guidance and poster; Normattiva scope; Danish Health Authority Ukraine guidance; Romanian law ownership records; direct EURES Romania vacancy search.

## Finding dispositions

- **IT:** IT-P2-001 — RESOLVED, IT-P2-002 — RESOLVED.
- **SK:** SK_P1_003 — RESOLVED, SK_P1_004 — RESOLVED, SK_P2_002 — RESOLVED, SK_P2_003 — RESOLVED, SK_P2_004 — RESOLVED, SK_P2_005 — RESOLVED, SK_P2_006 — RESOLVED.
- **DK:** DK_P1_001 — RESOLVED, DK_P2_001 — RESOLVED, DK_P2_002 — RESOLVED, DK_P2_003 — RESOLVED.
- **BG:** BG_P1_001 — RESOLVED, BG_P1_002 — RESOLVED, BG_P2_001 — RESOLVED, BG_P2_002 — RESOLVED, BG_P2_003 — RESOLVED, BG_P2_004 — RESOLVED.
- **ES:** ES_P2_001 — RESOLVED, ES_P2_002 — RESOLVED, ES_P2_003 — RESOLVED, ES_P2_004 — RESOLVED, ES_P2_005 — RESOLVED.
- **NO:** NO_P2_001 — RESOLVED, NO_P2_002 — RESOLVED, NO_P2_003 — RESOLVED, NO_P2_004 — RESOLVED.
- **IE:** IE_P2_001 — RESOLVED, IE_P2_002 — RESOLVED.
- **FR:** FR_P1_001 — RESOLVED, FR_P1_002 — RESOLVED, FR_P1_003 — RESOLVED, FR_P2_001 — RESOLVED, FR_P2_002 — RESOLVED, FR_P2_003 — RESOLVED, FR_P2_004 — RESOLVED, FR_P2_005 — RESOLVED, FR_P2_006 — RESOLVED.
- **SE:** SE_P1_001 — RESOLVED, SE_P2_001 — RESOLVED, SE_P2_002 — RESOLVED, SE_P2_003 — RESOLVED, SE_P2_004 — RESOLVED, SE_P2_005 — RESOLVED.
- **AT:** AT_P1_001 — RESOLVED, AT_P1_002 — RESOLVED, AT_P1_003 — RESOLVED, AT_P2_001 — RESOLVED, AT_P2_002 — RESOLVED, AT_P2_003 — RESOLVED, AT_P2_004 — RESOLVED, AT_P2_005 — RESOLVED.
- **RO:** RO_P2_001 — RESOLVED, RO_P2_002 — RESOLVED, RO_P2_003 — RESOLVED, RO_P2_004 — RESOLVED, RO_P2_005 — RESOLVED, RO_P2_006 — RESOLVED, RO_P2_007 — RESOLVED.

## Task 230 validation recovery

- Authoritative production-repair SHA: `fa087a61286e0984c4188bcde035186f07088c40`. The workspace was reconciled at the committed validation work already present; no unrelated dirty files were found. The Task 230 validation files were retained and reconciled without resetting the branch.
- Original exact-SHA CI run `45060528227` on the production-repair SHA: Restore and Build passed; Test failed 46, passed 700, total 746. The original failures were dispositioned once each: stale global source count 15; stale country source count 3; stale LastUpdatedAt 13; stale reviewed fixture 3; stale Compare fixture 5; stale source expectation 1; stale render expectation 5; stale negative assertion 1; genuine production defect 0 within that original 46; unrelated pre-existing failure 0.
- Exact-SHA CI exposed one additional genuine catalog defect after fixture synchronization: Romanian source `7634654d-0321-5a85-8f1b-5b0d4327e331` duplicated the same owner, title and URL as existing source `49ae3d06-a9a3-5319-bf00-520c542d44c2`. The duplicate record was removed and its EN/UK/RU citations were redirected to the same-act source. No factual proposition was removed or changed. The current catalog contains 1,562 official source records.
- Validation recovery commits, all linear and pushed without amend, rebase or force-push: `c5044f7ceb2ed51486e888f92eed625c025f01f5`, `9ba2eaffc9ea0cc64112d090e18e3ddb8dd298b2`, `083167f7d144891a3e2557de4aa84d5bd5923b9b`, `2f6d435d52bde10de02908122bf6b8524faa8484`, and `1973baa6d8b1826b83a1ea0264d11893697912c4`. Successive exact-SHA CI runs found nested stale fixture issues; the final SHA passed the complete workflow. Earlier visual runs passed on each validation SHA.
- Final exact-SHA CI run `45067698114` on `1973baa6d8b1826b83a1ea0264d11893697912c4`: Restore, Build, all 746 tests, deployment verification, production Docker image build, and Docker integration smoke tests passed.
- Final exact-SHA Netherlands visual evidence run `45067698107` on `1973baa6d8b1826b83a1ea0264d11893697912c4`: passed, including production visibility assertions and screenshot upload.
- Validation synchronization changed 52 reviewed/content fixture files and 20 test files. Two production JSON files changed only to remove the duplicate source record and reuse its existing reference. All 60 factual repairs remain intact.
- Static recertification: 456/456 Compare facts; 0 duplicate source IDs; 0 dangling source owners; 0 dangling or wrong-owner Core refs; 0 dangling or wrong-owner Compare refs; 0 duplicate Compare bindings; all 152 entries include EN/UK/RU; semantic divergences 0. `git diff --check` passed.
- Country source counts compared with the pre-repair baseline: France 118→117, Ireland 120→122, Norway 46→45, Romania 57→59. All other country source counts are unchanged.
- Local restore remains blocked by the previously observed NuGet `NuGet-Migrations` named mutex; it was not retried. Local build/tests were not run after that block. GitHub Actions provided the final .NET validation.
- P0/P1/P2 remaining: 0/0/0. Unresolved high-risk findings: 0. Findings resolved: 60/60. Task 230 validation recovery is complete. Task 231 remains pending and has not started. Password protection and site publication state were unchanged.
