# Task 149 — France production integration and release

**As of:** 19 November 2026
**Certification authority:** `TASK_148_FIFTH_INDEPENDENT_FINAL_CERTIFICATION.md`
**Certification verdict:** FRANCE APPROVED FOR INTEGRATION

## Production integration

The eight certified EN/UK/RU review copies were applied to the eight canonical France articles. All certified article UUIDs remain unchanged. Historical claims remain date-scoped in the user-facing copy, including the 2022 Ministry page and the May 2024 OFII form. The TP Article 2(2) wording retains the lawful-residence-before-24-February-2022, valid Ukrainian-law permanent-residence-permit, and safe-and-durable-return conditions.

Production Compare content now matches the final certified 24-aspect copy. `protection-scheme` preserves the Article 2(1) / Article 2(2) distinction and the Decision (EU) 2022/382 or adequate national protection alternative. The certified SMIC amounts, effective date, and 35-hour monthly reference are preserved. The PUMa copy continues to distinguish coverage basis from financial participation, whose amount and collection details remain decree-dependent. Social claims C039 and C040 retain current evidence for C039 and explicitly historical May 2024 scope for C040.

## Applied source and binding deltas

- France OfficialSources: 104 before; KEEP 94, UPDATE 8, RETAIN_HISTORICAL 2, REMOVE 0, ADD 14; 118 after.
- Global OfficialSources: 1,530 before; 1,544 after.
- The 14 additions are exactly: `56c5f996-f13c-5df1-9cd2-ca2858889e6d`, `fecb425a-243b-5233-871d-2ce26b742a66`, `3a55eb20-dea4-5b00-bdef-7d932cf1fa31`, `d277a456-4e10-5a6c-849a-e23ba7474991`, `bf5e5f83-b27d-5b60-aa2d-9e5a924b342e`, `fd583be7-629f-5004-a38d-136e1a85b2d4`, `06d80225-6dde-5d61-94c8-48feac8d8e67`, `6ed80718-151e-52d3-bebd-dd8f9eb07d38`, `9003daa2-1315-58ef-8237-e8147d3a5592`, `77766134-f03b-5576-89df-6f38a08ed93c`, `ea7b16e1-7abc-5ebc-9521-2d45289a040d`, `a8de9820-9b53-5b98-b1e7-b89cb427f71d`, `c42ac694-fe38-52e6-b619-3f6b18542d89`, `fdf859e7-da78-52eb-88e9-0343805006d8`.
- Core article/source pairs: 104 before; KEEP 83, ADD 14, REMOVE 21; 97 after. Exact pair sets match the certified staging set.
- Compare content: KEEP 11, REVISE 13, UNAVAILABLE 0, NEWLY_AVAILABLE 0. The revised aspects are `protection-scheme`, `healthcare-access`, `healthcare-proof`, `healthcare-urgent`, `housing-initial`, `housing-private-rental`, `housing-financial-support`, `education-school-enrolment`, `education-pupil-language`, `education-higher-admission`, `social-subsistence`, `social-application`, and `social-child-benefits`.
- Compare article/source pairs: 71 before; KEEP 59, ADD 18, REMOVE 12; 77 after. Exact pair sets match `COMPARE_BINDING_DELTA_FINAL_01.csv`.

The deterministic EUR-Lex Article 2 record is UUIDv5 `fdf859e7-da78-52eb-88e9-0343805006d8`, owned by FR-TP-01. `fecb425a-243b-5233-871d-2ce26b742a66` is owned by FR-TP-02; `6ed80718-151e-52d3-bebd-dd8f9eb07d38` remains owned by FR-EMP-02. Operative references to the invalid UUIDv4 `d0f6fb72-2f27-487d-8cc2-0cfdd32506f9` and stale ID `2e094655-76bf-4800-91a5-7b6e62823d20`: 0. Owner conflicts and dangling Core/Compare references: 0. One same-owner/same-URL source overlap is intentional: the updated school-access successor and the separately titled primary-enrolment record both point to the certified Service-Public school page and are cited for distinct propositions.

## Validation

- `dotnet test RefugeeNavigatorEurope.slnx --configuration Release --no-restore` — PASS, 732 passed, 0 failed, 0 skipped.
- `dotnet build RefugeeNavigatorEurope.slnx --configuration Release --no-restore` — PASS, 0 errors (2 NuGet vulnerability-cache access warnings).
- `python3 -m unittest discover -s .github/scripts -p 'test_*.py' -v` — PASS, 104 tests.
- `dotnet run --project tools/RefugeeNavigator.ContentAudit --configuration Release --no-build -- --output /private/tmp/france-task149-content-audit.md` — PASS, 0 relationship issues.
- JSON, source-owner, article/source citation, Compare binding, exact-delta, stale-ID, article UUID, and certified-copy checks — PASS.
- `git diff --check` — PASS.
- Local `docker build --file Dockerfile --tag refugee-navigator-europe:task149 .` — NOT RUN successfully: the local Docker daemon socket is unavailable. The CI workflow runs the production Docker build and smoke test.

## Release commit and workflows

- Production commit subject: `feat: refresh France content`
- Production commit SHA: pending
- Branch push: pending
- CI workflow (`CI`): run ID/SHA/conclusion pending
- Visual workflow (`Netherlands visual evidence`, the repository's available visual-evidence workflow): run ID/SHA/conclusion pending

## Final production counts

- France articles: 8; preserved certified UUIDs: 8/8.
- France OfficialSources: 118; global OfficialSources: 1,544.
- France Core pairs: 97.
- France Compare aspects: 24; France Compare pairs: 77.
- Owner conflicts: 0; dangling Core SourceIds: 0; dangling Compare SourceIds: 0; invalid/stale operative IDs: 0.
