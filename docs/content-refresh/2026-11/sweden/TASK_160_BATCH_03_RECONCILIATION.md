# Task 160 — Batch 3 reconciliation

AS-OF: 20 November 2026. Reconciliation is limited to Sweden staging. Task 159’s original BLOCKED verdict is preserved in its report; this reconciliation closes its bookkeeping blocker. No new OfficialSources were added.

## Claims and coverage

Task 159’s 11 claim decisions were preserved. Active claims: **272** (TP-01 41; TP-02 28; EMP-01 26; EMP-02 25; HEALTH 21; HOUSING 24; EDU 50; SOC 57). Material 175; supporting 97; NumericDate 45; CURRENT 255, FUTURE 3, HISTORICAL 5, TRANSITION 9. Direct recomputation from active claims and effective SUPPORTED links gives COVERED 219, COVERED_MULTIPLE 50, UNSUPPORTED 3, REQUIRES_SOURCE_REVIEW 0. The only unsupported active claims are SE-SOC-01-C056, C072 and C073.

## Effective Core graph

The Task 158 baseline is 620 tuples (313 SUPPORTED, 307 NOT_SUPPORTED, 0 unresolved). Task 159’s exact nine new SUPPORTED pairs are:

- `SE-EDU-01-C007` × `ce3256c3-3694-5a83-90c5-b0edc3784fa0`
- `SE-EDU-01-C008` × `ce3256c3-3694-5a83-90c5-b0edc3784fa0`
- `SE-EDU-01-C010` × `ce3256c3-3694-5a83-90c5-b0edc3784fa0`
- `SE-EDU-01-C013` × `be9c8da3-450b-599a-afcb-b6f26de3958e`
- `SE-EDU-01-C034` × `be9c8da3-450b-599a-afcb-b6f26de3958e`
- `SE-EDU-01-C061` × `320c1cb2-afa2-5a5c-a009-2ae8cbba2046`
- `SE-EDU-01-C063` × `be9c8da3-450b-599a-afcb-b6f26de3958e`
- `SE-EDU-01-C064` × `be9c8da3-450b-599a-afcb-b6f26de3958e`
- `SE-HOUSING-01-C005` × `1c223817-2971-5ce2-bac3-2d2baa41011e`

Task 159’s sixteen superseded old proposition/source pairs are retired from the effective graph and retained as history:

- `SE-EDU-01-C007` × `091fbf01-e458-4e47-8e5b-18854fa2888e`
- `SE-EDU-01-C008` × `3e123ec8-d1d8-4685-ad4d-1a4f238d0e75`
- `SE-EDU-01-C010` × `091fbf01-e458-4e47-8e5b-18854fa2888e`
- `SE-EDU-01-C013` × `091fbf01-e458-4e47-8e5b-18854fa2888e`
- `SE-EDU-01-C027` × `091fbf01-e458-4e47-8e5b-18854fa2888e`
- `SE-EDU-01-C034` × `3e123ec8-d1d8-4685-ad4d-1a4f238d0e75`
- `SE-EDU-01-C034` × `c8d6a8e9-0512-4933-b223-2ea0e685b0d7`
- `SE-EDU-01-C061` × `3e123ec8-d1d8-4685-ad4d-1a4f238d0e75`
- `SE-EDU-01-C061` × `c8d6a8e9-0512-4933-b223-2ea0e685b0d7`
- `SE-EDU-01-C063` × `3e123ec8-d1d8-4685-ad4d-1a4f238d0e75`
- `SE-EDU-01-C063` × `c8d6a8e9-0512-4933-b223-2ea0e685b0d7`
- `SE-EDU-01-C064` × `3e123ec8-d1d8-4685-ad4d-1a4f238d0e75`
- `SE-EDU-01-C064` × `9b90dd74-5ac0-46ae-8a03-8a864fa59844`
- `SE-HOUSING-01-C003` × `48a66397-3f08-4dd6-83fd-be6273d70663`
- `SE-HOUSING-01-C003` × `90a45dd7-4cbd-4b8c-bd43-1255df682a22`
- `SE-HOUSING-01-C005` × `5cb5eec5-c490-4588-992e-5ad516483f12`

Two older NOT_SUPPORTED audit rows for SE-TP-02-C002 and SE-TP-02-C017 against source `dc8c2eb8-2fdd-4e68-bec4-4fcf83456555` were also explicitly marked historical: Task 157 had retested each same pair as SUPPORTED, and both supported retests remain effective.

Arithmetic: 620 − 16 + 9 = **613** effective tuples; 313 + 9 = **322 SUPPORTED**; 307 − 16 = **291 NOT_SUPPORTED**; unresolved 0. Total equals the decision counts. Inactive claims have zero effective pairs; no duplicate effective pair remains. Core audit reference checks: dangling SourceIds 0, unknown ClaimIds 0, wrong-owner bindings 0.

## Compare graph and certification

The 61-tuple pre-Task-159 graph is confirmed by Task 158. Task 159 added four supported Compare tuples (two housing and two higher-education). No Compare tuples were retired. The effective graph contains **65 SUPPORTED**, 0 NOT_SUPPORTED, 0 unresolved tuples. All 24 aspects have source tuples, the effective source IDs are inventoried, and there are no duplicate aspect/source pairs or dangling IDs.

Both Task 159 target aspects are certified:

- `housing-financial-support` — **REVISE, source-certified**. Effective SourceIds: `48a66397-3f08-4dd6-83fd-be6273d70663`, `1c223817-2971-5ce2-bac3-2d2baa41011e`, `dfb4d353-b6be-57ae-b238-9e142907e55a`.
- `education-higher-admission` — **REVISE, source-certified**. Effective SourceIds: `596cc524-ff1d-46d3-98b7-1c18262b0d86`, `9b90dd74-5ac0-46ae-8a03-8a864fa59844`, `c8d6a8e9-0512-4933-b223-2ea0e685b0d7`, `320c1cb2-afa2-5a5c-a009-2ae8cbba2046`, `be9c8da3-450b-599a-afcb-b6f26de3958e`.

All 24 aspect dispositions:

- **KEEP (16):** protection-application, protection-dates, residence-routes, residence-filing, employment-vacancies, employment-institutions, employment-assistance-conditions, work-permission, healthcare-access, healthcare-proof, housing-initial, housing-private-rental, education-school-enrolment, education-pupil-language, social-subsistence, social-application
- **REVISE (7):** protection-scheme, residence-conditions, work-employer-duties, work-minimum-pay, healthcare-urgent, housing-financial-support, education-higher-admission
- **UNAVAILABLE (0):** None
- **NEWLY_AVAILABLE (1):** social-child-benefits

The four newly staged Compare pairs are the two housing sources and the UHR FAQ/Higher Education Ordinance pairs for higher admission.

## Binding deltas

Core article/source binding set, compared as exact effective supported source relationships against the 69-pair production baseline: KEEP 59, ADD 7, REMOVE 10. ADD pairs:

- `SE-EDU-01` × `320c1cb2-afa2-5a5c-a009-2ae8cbba2046`
- `SE-EDU-01` × `be9c8da3-450b-599a-afcb-b6f26de3958e`
- `SE-EDU-01` × `ce3256c3-3694-5a83-90c5-b0edc3784fa0`
- `SE-EMP-01` × `43dfe833-392f-5c17-8a44-0b4afe18213c`
- `SE-HEALTH-01` × `7bc8c19f-a8da-56a7-b243-ea0a4e6c528b`
- `SE-HOUSING-01` × `1c223817-2971-5ce2-bac3-2d2baa41011e`
- `SE-TP-01` × `1be057c2-a5e8-5ff6-bae6-4ca54d853fd9`

REMOVE pairs:

- `SE-EDU-01` × `c8d6a8e9-0512-4933-b223-2ea0e685b0d7`
- `SE-EMP-01` × `4801dab3-29e2-446e-934f-3970ffa5f772`
- `SE-EMP-02` × `f241d32f-d41c-46f1-90c2-8d61eb4b2aea`
- `SE-HEALTH-01` × `275c1c14-3caa-4343-aa7e-67689edfa9bb`
- `SE-TP-01` × `108b10ab-13a5-4d5b-8102-dc7e65dbdf77`
- `SE-TP-01` × `6fd70e1b-fb73-46f1-aa8e-176ec5c280a3`
- `SE-TP-02` × `06e41800-28b2-4cf4-9885-9453373bd7a0`
- `SE-TP-02` × `17fa9717-a2fb-467e-815a-f0628c3f2645`
- `SE-TP-02` × `78f08f69-cbf2-4715-8a6c-faedcec8be27`
- `SE-TP-02` × `7a6e7566-6de2-4c7a-9b52-0f8b8ccfeaf7`

Compare binding set: KEEP 52, ADD 13, REMOVE 0. Exact ADD aspect/source pairs (the source owner maps each aspect to its article) are:

- `protection-dates` × `62a4c75c-ef6f-4e14-8f74-74d188d0e858`
- `protection-scheme` × `1be057c2-a5e8-5ff6-bae6-4ca54d853fd9`
- `protection-scheme` × `2db46f66-3651-4889-81ae-0dd1dce97bbb`
- `residence-conditions` × `d4b72ce1-6f3e-4f28-ac6d-ff144ad283d7`
- `social-child-benefits` × `2f022132-dcbb-4b15-817f-1e9542e63710`
- `social-child-benefits` × `92eff09d-9d6e-4478-9693-bba2cc41379c`
- `social-subsistence` × `2f022132-dcbb-4b15-817f-1e9542e63710`
- `employment-institutions` × `43dfe833-392f-5c17-8a44-0b4afe18213c`
- `work-minimum-pay` × `9fa47f80-b71c-5bab-acf4-c776609f1ce0`
- `housing-financial-support` × `1c223817-2971-5ce2-bac3-2d2baa41011e`
- `housing-financial-support` × `dfb4d353-b6be-57ae-b238-9e142907e55a`
- `education-higher-admission` × `320c1cb2-afa2-5a5c-a009-2ae8cbba2046`
- `education-higher-admission` × `be9c8da3-450b-599a-afcb-b6f26de3958e`

The reconciliation CSV records these additions; Task 159 itself introduced the final four pairs. The comparison uses `(aspect, article, source)` keys.

## Source inventory and Watch

Source inventory reconciles to 69 original production sources (KEEP 67, UPDATE 0, RETAIN_HISTORICAL 1, REMOVE 1) plus 9 deterministic owner-valid staged ADDs = **77 planned sources**. All nine UUIDv5 stable keys validate; same-owner canonical duplicates: 0.

Watch integrity: 6 rows (APPROVE 5, EDIT 1, REMOVE 0, REQUIRES_GAP_RESOLUTION 0); unknown/inactive ClaimIds 0, dangling SourceIds 0, duplicate WatchIds 0. Earliest trigger 1 January 2027; earliest pre-check 1 December 2026.

The Task 159 EN/UK/RU review-copy mismatches were corrected to match its final ledger propositions, including removal of C003 text from UK/RU, removal of the unsupported class-placement extension, and alignment of higher-admission and UHR wording. The target translation rows now show zero material, numeric/date, or missing/extra proposition divergence. High-risk register now represents all 11 unique targeted ClaimIds: 9 retained/source-certified, 2 removed, 0 blocked.

Remaining evidence gaps: three P0 Social claims only — SE-SOC-01-C056, C072, C073. Production/canonical content is unchanged. No commit or push was made.

## Verdict

**SWEDEN EVIDENCE GAP BATCH 3 RECONCILED — READY FOR FINAL SOCIAL GAP BATCH**
