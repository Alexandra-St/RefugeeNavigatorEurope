# Sweden final certification snapshot

As-of: 2026-11-20
Verdict: SWEDEN FINAL CERTIFICATION BLOCKED

## Articles and claim totals

| Article | UUID | Active |
|---|---|---:|
| SE-TP-01 | d2ab273d-21b7-44e4-b408-f4269a8753d8 | 41 |
| SE-TP-02 | befa7777-af70-4269-883c-8918961554dc | 28 |
| SE-EMP-01 | a218013b-3a77-4ba2-829a-d724689837f2 | 26 |
| SE-EMP-02 | 55949af5-b4fe-404e-bcb7-6ebb37131bab | 25 |
| SE-HEALTH-01 | 2234b91f-4021-40b2-b9d5-a029a6b533f2 | 21 |
| SE-HOUSING-01 | e0f307e9-7991-4350-89f4-2f84a9047393 | 24 |
| SE-EDU-01 | cac58c0c-ce69-41ac-877e-4fd124105b89 | 50 |
| SE-SOC-01 | 8b5cc53a-9933-4366-8421-d795966267ec | 57 |

Active 272; MATERIAL 175; SUPPORTING 97. NumericDate 45. Temporal: CURRENT 255; FUTURE 3; HISTORICAL 5; TRANSITION 9. Coverage: COVERED 222; COVERED_MULTIPLE 50; UNSUPPORTED 0; REQUIRES_SOURCE_REVIEW 0. Active compounds 0; active nonclaims 0; duplicate active ClaimIds 0; orphan lineage 0; claim/temporal/translation reopens 0.

Future: SE-TP-01-C042; SE-EDU-01-C046 (2027-01-01); SE-SOC-01-C055 (2027-01-02). Historical: SE-TP-01-C041; SE-SOC-01-C064/C065/C066/C067. Transition: SE-TP-01-C026/C055/C056; SE-TP-02-C010/C038; SE-EDU-01-C067; SE-SOC-01-C019/C022/C057. Current claims solely supported by legacy/retained historical sources: 0.

## Sources and evidence graphs

Production Sweden sources 69; physical staged records 78; final planned 77; net delta +8. Actions: KEEP 67; UPDATE 0; RETAIN_HISTORICAL 1; REMOVE 1; ADD 9.

RETAIN_HISTORICAL: `78f08f69-cbf2-4715-8a6c-faedcec8be27`
REMOVE: `f241d32f-d41c-46f1-90c2-8d61eb4b2aea`

ADD IDs (9/9 deterministic UUIDv5 and valid owners):

- `1be057c2-a5e8-5ff6-bae6-4ca54d853fd9`
- `43dfe833-392f-5c17-8a44-0b4afe18213c`
- `7bc8c19f-a8da-56a7-b243-ea0a4e6c528b`
- `9fa47f80-b71c-5bab-acf4-c776609f1ce0`
- `1c223817-2971-5ce2-bac3-2d2baa41011e`
- `dfb4d353-b6be-57ae-b238-9e142907e55a`
- `ce3256c3-3694-5a83-90c5-b0edc3784fa0`
- `be9c8da3-450b-599a-afcb-b6f26de3958e`
- `320c1cb2-afa2-5a5c-a009-2ae8cbba2046`

Same-owner canonical URL duplicates 0; ownership conflicts 0. Removal and historical retention independently justified: yes/yes.

Core effective tuples 612: SUPPORTED 325; NOT_SUPPORTED 287; unresolved 0. Inactive tuples 0; duplicate pairs 0; dangling SourceIds 0; unknown ClaimIds 0; wrong-owner bindings 0. Coverage/effective graph arithmetic reconciles.

Core binding plan: KEEP 59; ADD 7; REMOVE 10; final 66. Exact pairs: `CORE_BINDING_FINAL_PLAN_01.csv`.

Compare: 24/24 aspects reviewed and source-certified; 65 effective SUPPORTED tuples; NOT_SUPPORTED 0; unresolved/dangling/unknown 0. Dispositions KEEP 16; REVISE 7; UNAVAILABLE 0; NEWLY_AVAILABLE 1. Compare binding plan: KEEP 52; ADD 13; REMOVE 0; final 65. Exact pairs: `COMPARE_BINDING_FINAL_PLAN_01.csv`. New protection-dates pair is `protection-dates × e17f7ee0-b423-42b3-9c9c-280d33c54298`; the pair with `62a4c75c-ef6f-4e14-8f74-74d188d0e858` is baseline, not new. Wrong employment SourceId `43dfe833-392f-5c17-8a44-0b4afe41011e` occurrences: 0; correct ID present: yes.

## Watch and high-risk

Watch rows 6: APPROVE 5; EDIT 1 (W-SE-04); REMOVE 0; REQUIRES_GAP_RESOLUTION 0. W-SE-04's final edit is defined and integration-ready. Earliest pre-check 2026-12-01; earliest legal/effective trigger 2027-01-01. Invalid claim/source references 0; duplicate WatchIds 0.

High-risk rows 118; source-certified 111; non-source-certified 7, all inactive structural records: SE-TP-02-C018 ×2 (merged into C024), SE-TP-02-C026 ×2 (merged into C017), SE-HOUSING-01-C003 ×2 (removed), SE-EDU-01-C027 ×1 (removed). Active high-risk evidence gaps 0.

## Language and content blockers

- SE-EMP-02 UK/UA and RU each omit active C027, C034, C018, C020, C021, C022: 12 missing instances.
- SE-TP-01 RU omits active C048: 1 missing instance.
- SE-SOC-01 UK/UA and RU each retain the superseded broad “each benefit has its own decision” proposition: 2 stale extra instances.
- SE-EDU-01 English contains user-facing “reviewed guidance/rule” audit phrasing.
- Material language divergences: present. NumericDate language divergences: 0 identified. Cross-article contradiction identified: 0. Missing active claims in at least one translation: 7 unique IDs. Orphan/stale proposition instances: 2. Missing language proposition instances: 13.

Expected global OfficialSource count after integration: 1,552 (baseline 1,544 + Sweden net 8). Integration ready: no. Production/canonical changed: no. Commit/push: none. No tests, CI, or Docker run.
