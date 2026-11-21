# Romania certified production integration plan

**Status: CERTIFIED — READY FOR PRODUCTION INTEGRATION**  
**As of:** 21 November 2026  
**Basis:** `ROMANIA_FINAL_CERTIFICATION_01.md`

## Planned integration scope

Update the production payloads for these 8 articles: RO-TP-01, RO-TP-02, RO-EMP-01, RO-EMP-02, RO-HEALTH-01, RO-HOUSING-01, RO-EDU-01, and RO-SOC-01. Carry forward the certified EN, UK-UA and RU content, including the repaired RO-EMP-01-C001-S01 localization. Preserve the active claim set, source citations, and article ownership as certified.

### Sources

- Production source baseline: 56.
- Remove these 6 old SourceIds: `501ff4ab-87d5-514d-b2a5-ac95fc628881`, `ee3461f2-62c1-5515-aff2-4abf1b958a8a`, `643a3def-a38f-59f5-b83e-c09284188c61`, `70cda0bf-de1c-54bc-b411-386e4dbec151`, `e4283f53-f70c-5eff-a2c7-3a78fc1615d7`, `2d6c45be-df4c-5064-ad7b-423a9d7bcea4`.
- Add these 7 final SourceIds: `d3a3d139-2774-5024-8cb7-02d3404dac48`, `643e0f53-5acd-5ad8-9371-b4cf42fe851e`, `c587c8b0-1ef3-5124-b335-44e82158a480`, `b672a40e-3adb-5f60-97ab-6303e8b060de`, `c5bf046c-6006-5e16-bd7a-9b30d6bc3554`, `eac2f2ab-c8a6-5b6f-a147-af80096ebce6`, `d4f3d26a-e604-5eb1-972b-de9ae691af0b`.
- Source ID arithmetic: 56 − 6 + 7 = 57 final sources. Five of the final IDs are regenerated UUIDv5 IDs for updated source records; two are genuinely new sources (`eac2f2ab…` and `d4f3d26a…`). The two old records 501… and 2d6… both resolve into the single final source `d3a3d139-2774-5024-8cb7-02d3404dac48`, counted once.
- Apply 12 source UPDATE actions: 7 stable-ID metadata updates and 5 regenerated-ID source updates. Apply the 2 genuine source ADD actions and the reconciled replacement for old 2d6. Do not retain any of the six old IDs in final bindings.

### Core, Compare, and Watch

- Core baseline: 56 pairs; KEEP 50; ADD 7; REMOVE 6; final 57. The seven additions and six removals are exactly those listed in the certified final source plan and final certification artifact.
- Compare: carry the 24 certified aspects (11 KEEP / 13 REVISE) and 66/66 supported effective source tuples, preserving owner equality between each source and fact.
- Watch: persist the 5 certified rows if Watch is part of the production model. Keep W001 at 2027-03-05 and W002 at 2028-03-04; preserve W003 rate update, W004 annual admission-plan change, and W005 school-year change without inventing exact dates.

## Certified staged content state

- ACTIVE 81; MATERIAL 62; SUPPORTING 19; NumericDate 12; CURRENT 74; FUTURE 2; HISTORICAL 0; TRANSITION 5.
- Claim-source graph: 104/104 SUPPORTED. Coverage: 61 COVERED and 20 COVERED_MULTIPLE. Translation divergence: 0. High-risk claims: 59/59 source-certified.
- Inactive split parents: 24; removed non-split claims: 15; gap dispositions: 39/39 resolved.

## Release validation after an authorized integration

1. Apply the 8 article payload updates, source changes, Core pair delta, Compare updates, and Watch changes in one reviewed production change.
2. Run `dotnet test RefugeeNavigatorEurope.slnx`, including `RomaniaContentIntegrationTests`, after the production data is integrated. Confirm all article IDs, 24 approved Romania Compare facts, owner-local sources, and translation statuses.
3. Run repository data relationship, uniqueness, localization, and citation validation. Confirm no dangling or duplicate source IDs, old IDs, wrong-owner bindings, or missing citations.
4. Recompute the source and Core arithmetic, verify all 5 Watch records and 8 article payloads, then review the production diff before release.

No integration, commit, push, CI, or production test was performed as part of Task 197. Production/canonical data remains unchanged until the certified plan is separately carried out.
