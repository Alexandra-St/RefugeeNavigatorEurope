# Finland Refresh Review 01

**As of:** 17 November 2026
**Scope:** Independent review of the completed Finland research report, eight Core drafts in EN/UK/RU, 24 Compare aspects and the current Finland production source/fact records. Production JSON was inspected read-only. No full new research was undertaken.

## Article decisions

| Article | Decision | Issues | Required edits / disposition |
|---|---|---|---|
| FI-TP-01 — `ffd32dca-56d8-5dcf-b995-7522a159010a` | APPROVE WITH EDITS | Russian 2027/2028 paragraph described automatic extension without the same explicit conflict qualification used in EN/UK. | RU now reports both Migri statements, says neither establishes the person's updated permit or 2028 card validity, and directs holders to check the individual record. Retain the Information Bank source only as qualified evidence of the conflict and the separate `kotikunta` guidance. |
| FI-TP-02 — `344c5e31-45a9-5f22-8d4a-4a58d8fe8243` | APPROVE WITH EDITS | The unclear Information Bank source was included in the inline source binding although the article's current permit-route claims are supported by current route-specific sources. | Removed the `0d535e37-f80b-59c8-9a35-57038c1d876b` binding and source-set record; retained route-specific Migri, Finlex and EU sources. |
| FI-EMP-01 — `53743a68-bacb-5344-9aee-559e146cab54` | APPROVE WITH EDITS | Broken JMF Ukraine page remained in the editorial source set and in the staged Compare binding. | Removed the broken source from the Core source set and Compare binding. Current JMF registration and integration-client sources already cover the qualified claim; pending TP applicants are directed to the local authority. |
| FI-EMP-02 — `0abffa8c-cd42-571e-97f3-e160afad880c` | APPROVE WITH EDITS | Permit-holder/pending-applicant entrepreneurship, work authorization, employer notice, occupational healthcare and pay rules remain separately qualified. | Added an owner-bound Migri employer-announcements source for the TP early-ending-notice exemption. Preserve the two intentional Employment Contracts Act source records for the consolidated Act and §2:4. |
| FI-HEALTH-01 — `ea41d1e7-f921-50af-9886-2232d9fb06ef` | APPROVE | Pending adult, granted holder, child and reception-client routes remain distinct; no uniform post-reception handoff without `kotikunta` is invented. | No new correction required. Helsinki and regional 116117 qualifications remain. |
| FI-HOUSING-01 — `61b331e0-76bb-58f4-91e7-e5944ba77e31` | APPROVE | Reception accommodation, private rental, county emergency help and Kela housing/deposit support are not presented as guaranteed substitutes. | No new correction required. Eligibility-based three-month transition and local capacity limits remain. |
| FI-EDU-01 — `2a2094da-e321-50bd-8a45-bf3988be06c9` | APPROVE WITH EDITS | The August 2026 supplementary-preparatory rule was supported by a raw URL rather than an owner-bound source token. | Added EDUFI source `6bb34b1e-6949-58ff-99ba-9ffa3769e03b` and bound it in EN/UK/RU and the Compare language fact. The optional, teacher-assessed, up-to-one-year limit remains explicit. |
| FI-SOC-01 — `51691974-1c20-5b25-9572-66624984a52f` | APPROVE WITH EDITS | A dated 2023 UA1e form remained in the source set; current valid-permit and future-proposal links were raw URLs rather than owner-bound sources. | Removed the legacy form source record/binding, added Kela valid-permit and 2027-proposal source records, and bound them in EN/UK/RU. The 2027 proposal is marked future-only. |

**Overall:** 5 articles APPROVE WITH EDITS; 3 APPROVE; 0 BLOCK. No material unsupported claim remains after the staged corrections below.

## Translation parity

**PASS — 24/24 locales (8 articles × EN/UK/RU).** The reviewed translations agree on the critical distinctions: 5 August EU/Migri military-rule date versus the 13 August Finnish decision; EU protection through 4 March 2028 versus current Migri individual permit/card guidance through 4 March 2027 and unresolved rollout; registration-start work/study rights; pending certificate versus residence card; personal identity code versus `kotikunta`; eligibility-to-apply versus actual municipality registration; three-month reception transition; Kela's separate coverage tests; healthcare stage; current allowance categories; child-benefit application versus award; and may/must/can. FI-TP-01 RU was corrected to restore the same 2027/2028 conflict qualification present in EN/UK.

## Compare decisions

The 24 aspects below were checked against the reviewed Core copy, article owner, current official source bindings and the staged `COMPARE_DRAFT_01.md`. All remain available; the dispositions are 6 KEEP and 18 REVISE. The 18 revised fact texts must be synchronized from the reviewed Compare draft before production approval.

| Aspect | Decision | Review result | Source plan |
|---|---|---|---|
| `protection-scheme` | REVISE | Defined applicant cohorts and military-obligation boundary; keep 5-August/13-August transition explicitly qualified. | Keep current bindings `4e930362-ce28-56f0-bbb9-404ea074c787`, `fce4f3be-d1cd-5fb1-946f-a336aea6e2c9`, `6e5f42b2-91df-5ad9-83cb-1ab1047024c5`; retain EU Decision `1012bff7-ff9e-59f7-89da-d184ca95f384` in the revised fact. |
| `protection-application` | REVISE | In-person police/Border Guard registration; Migri decision; no initial online filing claim; local intake varies. | Retain current `4e930362-ce28-56f0-bbb9-404ea074c787`, `81c3e3cd-a716-5908-b6fe-4841a2d1e910`, `51d64afb-b6d1-5cd2-96f8-cfa9d1d5888d`. |
| `protection-dates` | REVISE | The old Compare text falsely presented automatic extension as settled. Revised text states EU 2028 horizon, central Migri 2027 guidance, conflicting Information Bank wording, and absence of individual 2028 permit/card proof. | Keep `1012bff7-ff9e-59f7-89da-d184ca95f384`, `4e930362-ce28-56f0-bbb9-404ea074c787`, `844049fc-2256-57d1-af24-c5f395879707`, `9b91b261-5a80-566a-9630-8e4ec200966d`, `f8052aa5-7266-53ca-ba71-e1545ca27bf4`; retain `fa6d5f24-dab9-50d5-8c4a-500931079914` only to show the conflicting statement, never as proof of individual validity. |
| `residence-routes` | REVISE | No automatic TP conversion; ordinary qualifying routes are separate. | Retain current Migri route records `696d034d-535f-5ce2-a8e6-b458e763b728`, `3a0c0c61-944c-57da-9f0a-dd722ce6a588`, `6d8636bc-3ea5-51fa-9957-318d9e82fe3f`, `22b640c5-23d2-5d02-90ef-7c790a38080b`. |
| `residence-conditions` | REVISE | TP-only time is not treated as qualifying A-permit or P-EU residence; thresholds remain route/date-specific. | Retain current `3a0c0c61-944c-57da-9f0a-dd722ce6a588`, `6d8636bc-3ea5-51fa-9957-318d9e82fe3f`, `22b640c5-23d2-5d02-90ef-7c790a38080b`, `9adead71-d1cb-5937-9257-2b307c53aef3`, `02d799fb-a445-541b-95f1-4cc11a8edae6`. |
| `residence-filing` | REVISE | TP holder's qualifying extended-permit route in Finland is distinct from ordinary first permits; no blanket pending-work bridge after TP expiry. | Retain current `78a2c3aa-2a40-5fda-973d-8d4a074a145f`, `b3b18f34-977c-5c3f-8a93-01d4d7612df7`, `26f6e8c6-28cf-5bf8-91f6-4e655b03487f`, `226afc80-1d50-5871-86e3-cea9d75ec066`, `03c6e6fd-3f88-532e-b7cc-3775a730f224`, `22b640c5-23d2-5d02-90ef-7c790a38080b`. |
| `employment-vacancies` | KEEP | Official vacancy channels and limits remain sound. | Keep existing bindings. |
| `employment-institutions` | REVISE | Municipal employment areas, JMF and KEHA roles are distinct; TE Offices are not current. | Keep existing `f28125f7-1b8d-504d-8c6d-9a2898ea2c2e`, `80f71596-2660-53ce-8321-e29dc4681d27`, `3881ecc5-dadd-5b72-ab44-3ac2c0f9674a`. |
| `employment-assistance-conditions` | REVISE | Separate work authorization, jobseeker registration, local service assessment and benefit award. Remove obsolete/broken Ukraine page. | Remove `a8f043b1-b76c-532c-8d62-16a988efb1cd`; retain/rebind to current `80f71596-2660-53ce-8321-e29dc4681d27`, `3881ecc5-dadd-5b72-ab44-3ac2c0f9674a`, `2c0ecfdf-bea6-5c69-bd3e-5d4066318f8a`, `3a6478eb-d82b-54ea-b70f-dc6292a56ee3`, `8fe293c6-bec7-5224-9afb-8cc9061fd903`. |
| `work-permission` | KEEP | Registration-start work right, no card prerequisite, and certificate distinction remain correct. | Keep existing bindings. |
| `work-employer-duties` | REVISE | State verification/record duties, applicable hiring notices, TP permit-holder exemption from the separate early-end notice, and statutory preventive occupational healthcare. | Add current Migri notice source `0f5f6d43-b764-5cc5-9813-3e86e9ccb296`; retain relevant existing Migri/Finlex records. The Compare fact does not need the separate written-terms source `e0b8e4ac-3707-5842-8fc1-63f81bbb2532`. |
| `work-minimum-pay` | KEEP | No statutory national minimum wage; collective agreement and qualified fallback wording remain sound. | Keep existing bindings. |
| `healthcare-access` | REVISE | Distinguish pending adults, children, granted TP holders and active reception clients; no uniform post-reception handoff without `kotikunta`. | Keep existing bindings. |
| `healthcare-proof` | REVISE | No single identity, residence or Kela document proves every healthcare route. | Keep existing bindings. |
| `healthcare-urgent` | KEEP | 112/116117 distinction, Åland exception and regional details remain sound. | Keep existing bindings. |
| `housing-initial` | REVISE | Contact police/border authority and assigned reception centre; accommodation is capacity-dependent. | Keep existing bindings. |
| `housing-private-rental` | KEEP | Landlord process, lease, deposit and advance-rent limits remain sound. | Keep existing bindings. |
| `housing-financial-support` | REVISE | Kela housing/deposit assistance needs a separate decision; TP, reception allowance or `kotikunta` alone does not ensure support. | Keep existing bindings. |
| `education-school-enrolment` | REVISE | Include municipality-living TP children without `kotikunta`, eligible asylum applicants under EDUFI guidance, and local placement limits. | Keep current `81c5698c-a309-53d8-9d8d-f40c72837a6a`, `3cabfba2-d943-5ec7-a5c9-770bc8977f61`, `00b0ee24-ebaf-5544-a7db-e2b96b5b7abf`, `14b56222-4944-58f6-be6b-035ec0c7eab9`. |
| `education-pupil-language` | REVISE | Add optional, teacher-assessed supplementary preparatory teaching available from 1 August 2026 for up to one year. | Add `6bb34b1e-6949-58ff-99ba-9ffa3769e03b`; retain `3cabfba2-d943-5ec7-a5c9-770bc8977f61`, `9c00c687-87ca-5512-9f72-0494308bf522`, `c59e3fa6-6b4f-5dee-b370-2791e6a33f62`. |
| `education-higher-admission` | KEEP | Institutional admission/recognition and separate tuition/application-fee conditions remain qualified. | Keep existing bindings. |
| `social-subsistence` | REVISE | Separate reception allowance from Kela basic and county supplementary/preventive support; transition is triggered by eligibility, not registration; no continuous automatic payment. | Retain `b81175f1-eb6f-5e6a-a1af-db122bd8fa6a`, `a9734191-6060-5223-96b6-8acac2459063`, `f3fff495-e76e-5760-9381-1c6012124292`, `8a039a93-38cb-550d-a584-3632043c859b`; add `893de843-3cc2-5321-b43f-eea77dad270f` for county role. |
| `social-application` | REVISE | Separate reception-centre, Kela and county routes; add April 2026 valid-permit condition for residence-based Kela benefits. | Add `2c6d81a3-3527-5c5b-8fee-48615f8a7639`; retain `b81175f1-eb6f-5e6a-a1af-db122bd8fa6a`, `f3fff495-e76e-5760-9381-1c6012124292`, `43e9bd3e-ac6f-55e3-b329-aeba4d0c3d2b`, `a9734191-6060-5223-96b6-8acac2459063`, `8a039a93-38cb-550d-a584-3632043c859b`, `893de843-3cc2-5321-b43f-eea77dad270f`. |
| `social-child-benefits` | REVISE | Do not imply award after one year; child must be under 17 and live in Finland for the described TP application route; each case is decided. | Remove legacy `d5bed9b1-7eb7-5223-a1d1-c56b705c712e`; add `2c6d81a3-3527-5c5b-8fee-48615f8a7639`; retain current `54aeabdc-7ce2-56a9-b414-42acb9a18583`, `9ce83adb-45b3-5399-99e5-452e5e5f9edd`, `6343a83f-5bb6-5569-9636-c1c3ac175675`, `6f00998a-e0b4-5aa5-ae87-999b8401047e`. |



**Full staged source bindings (all 24 aspects):**

| Aspect | Exact `OfficialSourceIds` |
|---|---|
| `protection-scheme` | `4e930362-ce28-56f0-bbb9-404ea074c787`, `fce4f3be-d1cd-5fb1-946f-a336aea6e2c9`, `6e5f42b2-91df-5ad9-83cb-1ab1047024c5` |
| `protection-application` | `4e930362-ce28-56f0-bbb9-404ea074c787`, `81c3e3cd-a716-5908-b6fe-4841a2d1e910`, `51d64afb-b6d1-5cd2-96f8-cfa9d1d5888d` |
| `protection-dates` | `1012bff7-ff9e-59f7-89da-d184ca95f384`, `4e930362-ce28-56f0-bbb9-404ea074c787`, `fa6d5f24-dab9-50d5-8c4a-500931079914`, `f8052aa5-7266-53ca-ba71-e1545ca27bf4`, `844049fc-2256-57d1-af24-c5f395879707`, `9b91b261-5a80-566a-9630-8e4ec200966d` |
| `residence-routes` | `696d034d-535f-5ce2-a8e6-b458e763b728`, `3a0c0c61-944c-57da-9f0a-dd722ce6a588`, `6d8636bc-3ea5-51fa-9957-318d9e82fe3f`, `22b640c5-23d2-5d02-90ef-7c790a38080b` |
| `residence-conditions` | `3a0c0c61-944c-57da-9f0a-dd722ce6a588`, `6d8636bc-3ea5-51fa-9957-318d9e82fe3f`, `22b640c5-23d2-5d02-90ef-7c790a38080b`, `9adead71-d1cb-5937-9257-2b307c53aef3`, `02d799fb-a445-541b-95f1-4cc11a8edae6` |
| `residence-filing` | `78a2c3aa-2a40-5fda-973d-8d4a074a145f`, `b3b18f34-977c-5c3f-8a93-01d4d7612df7`, `26f6e8c6-28cf-5bf8-91f6-4e655b03487f`, `226afc80-1d50-5871-86e3-cea9d75ec066`, `03c6e6fd-3f88-532e-b7cc-3775a730f224`, `22b640c5-23d2-5d02-90ef-7c790a38080b` |
| `employment-vacancies` | `7650cf1e-db1e-51b1-93b5-e02e1fcd1390`, `fcc4dc11-ba3c-5ef2-b183-c991f6819880`, `4d707f64-326d-5813-844f-1aa10f4e6643`, `a1c7cca8-ddfc-536c-a1ac-f84ed7105e83` |
| `employment-institutions` | `f28125f7-1b8d-504d-8c6d-9a2898ea2c2e`, `80f71596-2660-53ce-8321-e29dc4681d27`, `3881ecc5-dadd-5b72-ab44-3ac2c0f9674a` |
| `employment-assistance-conditions` | `80f71596-2660-53ce-8321-e29dc4681d27`, `3881ecc5-dadd-5b72-ab44-3ac2c0f9674a`, `2c0ecfdf-bea6-5c69-bd3e-5d4066318f8a`, `3a6478eb-d82b-54ea-b70f-dc6292a56ee3`, `8fe293c6-bec7-5224-9afb-8cc9061fd903` |
| `work-permission` | `4c3bd163-cf98-56d0-9aa7-1079277fd77a`, `6ecc6516-eb7d-5f97-b7fd-0b2e2c999de8` |
| `work-employer-duties` | `221e9632-cf8c-5035-b23a-449e0def7e71`, `6a645e0c-7b9b-5914-ab3f-6f5ef7581c50`, `7ab4c5c4-4363-5083-9734-4a16e6d72e74`, `8d32c90d-d0a8-5028-9f2a-07c51f58d3f6`, `0f5f6d43-b764-5cc5-9813-3e86e9ccb296` |
| `work-minimum-pay` | `7ab4c5c4-4363-5083-9734-4a16e6d72e74`, `eaccb2b0-a4d1-52c8-825a-d2372b8401cd`, `5ce1133a-1543-5a2a-b4cc-7edcb703f0be` |
| `healthcare-access` | `b8e16bc1-d8a0-5234-a6e4-b6bb81486485`, `bf51d3f6-8c88-5d36-a681-756c6b03bbce`, `0ac7fc0a-c727-559b-a95e-02489e9fcd5a`, `a3760fa3-8c8d-57b6-8886-51276f5f7075` |
| `healthcare-proof` | `a3760fa3-8c8d-57b6-8886-51276f5f7075`, `629c7c47-cfd5-5310-9a5c-728add1d8e63`, `3b0d6451-599b-59b6-8575-4c04c04c97a0` |
| `healthcare-urgent` | `0ac7fc0a-c727-559b-a95e-02489e9fcd5a`, `629c7c47-cfd5-5310-9a5c-728add1d8e63`, `4ccdabf9-3601-530e-9d0e-f85baf01bb37`, `2af724c0-75b1-5f85-aaf5-28e639b4b7ca` |
| `housing-initial` | `45f577c1-dcea-5ba8-836a-9ae09b16045a`, `85d9daa4-fef0-5e7c-90d2-e05632281cd8`, `6830e02f-92d4-54f2-a3b7-6bedaae4d38a` |
| `housing-private-rental` | `e7e96b5a-d430-5c8c-b776-d55536fe6b57`, `c75631c7-d96f-56b9-a249-a5289d738718`, `39f3e43f-922d-5c15-a834-a61cf89db071`, `a60a728a-8d92-5802-a6f1-d0f66d378e68`, `666206b7-da88-5d2b-b519-6fbbc59830e1` |
| `housing-financial-support` | `70ceb378-7bca-5511-ad1e-ec0841db32bb`, `5ffd873e-3618-5f4d-a010-992c5b9ddf40`, `cd54f852-6cf1-5d43-9729-fcedb6aa719d`, `1991f1f3-0949-5f30-9b92-b8420d28e51b` |
| `education-school-enrolment` | `81c5698c-a309-53d8-9d8d-f40c72837a6a`, `3cabfba2-d943-5ec7-a5c9-770bc8977f61`, `00b0ee24-ebaf-5544-a7db-e2b96b5b7abf`, `14b56222-4944-58f6-be6b-035ec0c7eab9` |
| `education-pupil-language` | `3cabfba2-d943-5ec7-a5c9-770bc8977f61`, `9c00c687-87ca-5512-9f72-0494308bf522`, `c59e3fa6-6b4f-5dee-b370-2791e6a33f62`, `6bb34b1e-6949-58ff-99ba-9ffa3769e03b` |
| `education-higher-admission` | `c487c797-470c-5aac-872c-6e89fe4ba6d1`, `85cbafd1-a53c-5be7-bf9e-a2232fdf4fb5`, `b8a9d204-3da5-5656-abfd-1c2e35c12aae`, `92faeee3-cdff-5a92-a893-a122ff6ceee4`, `30ae9fc8-b5b0-5b55-b903-f1d6d5ed33cb`, `4f7c8a47-ae54-5a7b-8218-67a58f07dba0`, `7bca7090-6b21-5a3b-a0dc-7055d8899bdf`, `176450b5-8dc2-56d5-9d24-84643b444ce3` |
| `social-subsistence` | `b81175f1-eb6f-5e6a-a1af-db122bd8fa6a`, `a9734191-6060-5223-96b6-8acac2459063`, `f3fff495-e76e-5760-9381-1c6012124292`, `8a039a93-38cb-550d-a584-3632043c859b`, `893de843-3cc2-5321-b43f-eea77dad270f` |
| `social-application` | `b81175f1-eb6f-5e6a-a1af-db122bd8fa6a`, `f3fff495-e76e-5760-9381-1c6012124292`, `43e9bd3e-ac6f-55e3-b329-aeba4d0c3d2b`, `a9734191-6060-5223-96b6-8acac2459063`, `8a039a93-38cb-550d-a584-3632043c859b`, `893de843-3cc2-5321-b43f-eea77dad270f`, `2c6d81a3-3527-5c5b-8fee-48615f8a7639` |
| `social-child-benefits` | `54aeabdc-7ce2-56a9-b414-42acb9a18583`, `9ce83adb-45b3-5399-99e5-452e5e5f9edd`, `6343a83f-5bb6-5569-9636-c1c3ac175675`, `6f00998a-e0b4-5aa5-ae87-999b8401047e`, `2c6d81a3-3527-5c5b-8fee-48615f8a7639` |
**Compare result: 24/24 approved for integration** — KEEP 6, REVISE 18, UNAVAILABLE 0, NEWLY AVAILABLE 0. Revised EN/UK/RU text is staged in `docs/content-drafts/finland-v1/COMPARE_DRAFT_01.md`; the two corrected critical facts are `protection-dates` and `employment-assistance-conditions`, with additional social and education source/copy updates above.

## Temporary-protection review

- **Military rule:** EU Decision 2026/1912's corrected rule applies to relevant new grants from 5 August 2026, with continuity for people protected in the same Member State on or before 4 August who continuously retained that status there. Migri's proof examples are not described as an exhaustive checklist. Finnish Decision 751/2026 took effect 13 August; its transition interaction with Migri's operational date remains expressly unresolved in the draft. No false reconciliation found.
- **Application/decision/card:** Initial application is in person to police or Border Guard; that authority registers and Migri decides. The draft does not invent online initial filing or national appointment rules. Pending certificate is not a residence-permit card; Migri says the registration certificate can evidence work right.
- **2027 vs 2028:** EU protection horizon is 4 March 2028. Migri central permit/card instructions state 4 March 2027 and say later 2028 instructions will follow; the Information Bank separately says automatic extension. The revised Core and Compare text reports that conflict and does not claim all permits/cards are valid to 2028 or that an individual record is updated.
- **Unclear source decisions:** `fa6d5f24-dab9-50d5-8c4a-500931079914` (FI-TP-01) — **RETAIN AS QUALIFIED**: supports the separate one-year `kotikunta` guidance and is cited only as the Information Bank statement in the explicit 2028 conflict; it is not sole evidence of an individual's 2028 validity. `0d535e37-f80b-59c8-9a35-57038c1d876b` (FI-TP-02) — **REMOVE**: the article has no current material claim requiring this broad Information Bank record; route-specific sources remain.
- **Personal identity code:** Positive decision may include it; if population registration is incomplete, DVV may be needed. Code is not `kotikunta`, Kela coverage or automatic municipal entitlement.
- **`kotikunta`:** Separate DVV application after applicable one-year permit/residence conditions; not automatic. Eligibility to apply is distinct from completed registration.
- **Residence filing:** Current TP holder may use an in-Finland extended-permit route on another qualifying basis. Ordinary first permits generally use abroad filing subject to specific exceptions. No automatic conversion or blanket pending residence/work bridge after TP expiry is asserted.
- **Permanent/P-EU:** TP-only time does not satisfy the specified qualifying A-permit/P-EU periods in the reviewed routes; no automatic accrual claim found.

## Employment review

- Work right starts when police/Border Guard registers the TP application; employment work is field-unrestricted under the reviewed Migri rule; physical card is not a prerequisite. Certificate may evidence the right.
- A granted TP permit holder may undertake stated business/freelance/light-entrepreneur activity under Migri guidance. Pending applicant entrepreneurship is not inferred from employee work right and remains unestablished.
- Current local authorities/municipal employment areas provide local services; JMF is the national service and EURES the EU mobility channel. No stale TE Office assignment remains. Registration as jobseeker, work authorization, labour-policy assessment and benefit payment are separate.
- Broken source `a8f043b1-b76c-532c-8d62-16a988efb1cd` is **REMOVE** from source catalog and every active binding. Its old URL is `https://tyomarkkinatori.fi/en/personal-customers/information-about-working-life/search-for-work/registration-as-a-job-seeker/registering-as-a-job-seeker-and-job-search-for-ukrainians-in-finland`. Use existing current JMF records `2c0ecfdf-bea6-5c69-bd3e-5d4066318f8a` (registration guidance) and `3881ecc5-dadd-5b72-ab44-3ac2c0f9674a` (integration-client guidance), plus the existing statutory/Kela sources where needed. No current claim needs the broken page.
- **Minimum pay:** No national statutory minimum wage amount. Applicable generally binding/relevant collective agreement and job classification control; usual/reasonable remuneration is a fallback only where no applicable agreement sets pay and no wage was agreed. No universal hourly amount.
- **Employer duties:** Right-to-work check and record retention and applicable hiring notice remain. Migri's reviewed guidance exempts TP permit holders from the separate early-ending notice applicable to work-based permits. Statutory preventive occupational healthcare is not described as full general healthcare.

## Healthcare review

Pending adults receive urgent/essential care via reception route; children under 18 have the stated resident-equivalent statutory basis; after TP grant the broader statutory basis applies. Reception centre arranges access during active reception services. After `kotikunta`, the wellbeing-services county organizes public health/social services; Helsinki has separate organization. No uniform post-reception handoff without `kotikunta` is invented. Kela sickness-insurance status is separate; Kela card is not required for public care. 112 is for life-threatening emergencies; 116117 is for urgent non-life-threatening advice where available, with Åland exception/regional variation.

## Housing review

A new applicant without accommodation is directed to inform police/border authority and follow the assigned-centre route; placement is capacity-dependent. Private accommodation is generally possible subject to reviewed exceptions. Reception does not reimburse ordinary private rent/utilities. Reception accommodation ends with reception-services entitlement; no universal move-out grace period or replacement municipal apartment is promised. County emergency accommodation is locally assessed. Housing allowance, basic assistance, supplementary/preventive aid and deposit help each require separate eligibility decisions; TP/`kotikunta` alone does not award support. Deposit and advance rent remain distinct.

## Education review

Children living in a municipality, including TP children without `kotikunta`, can access pre-primary/basic education under current guidance; eligible asylum-applicant access is included with EDUFI qualification. Compulsory education is separately scoped: pre-primary normally in the year the child turns six; compulsory duty begins in the year of seven and ends at 18 or earlier qualifying completion for children covered by law. Local placement and documents are not guaranteed. Preparatory education/S2/R2 are need- and locality-dependent. Supplementary preparatory teaching from 1 August 2026 is optional, teacher-assessed and up to one year. Studyinfo is a channel; HEIs decide admission and qualification evidence. No automatic admission, equivalence or universal fee exemption is implied.

## Social-assistance review

Reception allowance, Kela benefits, Kela basic social assistance, county supplementary/preventive support, unemployment support, family benefits and housing aid remain distinct. Reception allowance requires reception-service status/application and individual assessment; 2026 schedule and meals are in the research report. It is not a guaranteed payment after reception entitlement ends. The ordinary three-month service limit runs from eligibility to apply for `kotikunta`, not actual registration, with stated exception/discretion. Kela makes a separate means-tested decision; since 1 April 2026 the reviewed valid-permit rule applies to non-EU/EEA/Swiss nationals seeking residence-based benefits. The child-benefit route after one year is an application route for a child under 17 living in Finland, not an automatic award. Parental, home-care and housing benefits have separate criteria. `yleistuki` began 1 May 2026; the proposed 1 April 2027 TP expansion is future-only.

## Broken / unclear / legacy source remediation

| Source ID | Owner | URL | Decision | Reason / current claim dependency |
|---|---|---|---|---|
| `a8f043b1-b76c-532c-8d62-16a988efb1cd` | FI-EMP-01 | `https://tyomarkkinatori.fi/en/personal-customers/information-about-working-life/search-for-work/registration-as-a-job-seeker/registering-as-a-job-seeker-and-job-search-for-ukrainians-in-finland` | REMOVE | Broken/404 and stale TE Office/phone directions. No active claim may depend on it. Rebind the safe registration distinction to existing JMF current IDs `2c0ecfdf-bea6-5c69-bd3e-5d4066318f8a` and `3881ecc5-dadd-5b72-ab44-3ac2c0f9674a`. |
| `fa6d5f24-dab9-50d5-8c4a-500931079914` | FI-TP-01 | `https://migri.fi/information-bank/temporary-protection` | RETAIN AS QUALIFIED | Source record says automatic 2028 extension, conflicting with central/extension pages. Keep only to report this official conflict and the separately supported `kotikunta` guidance; never use alone to state individual validity. |
| `0d535e37-f80b-59c8-9a35-57038c1d876b` | FI-TP-02 | `https://migri.fi/en/information-bank/temporary-protection` | REMOVE | Same unresolved automatic-2028 wording, but TP-02 has no necessary current claim depending on it; route-specific source set suffices. |
| `d5bed9b1-7eb7-5223-a1d1-c56b705c712e` | FI-SOC-01 | `https://www.kela.fi/benefit-forms/UA1e.pdf` | REMOVE | Dated April 2023 application form is legacy and not the current English Kela Ukraine-page link. No current claim depends on it; current Kela Ukraine guidance and child-benefit page remain. |

## Source integration plan

### Source records

- **ADD (4):**
  - `6bb34b1e-6949-58ff-99ba-9ffa3769e03b` — FI-EDU-01 — EDUFI, 2026 curriculum changes — `https://www.oph.fi/fi/uutiset/2026/perustemuutokset-voimaan-182026-muutoksilla-edistetaan-maahanmuuttotaustaisten` — created because optional supplementary preparatory teaching is a material dated claim and needs an owner-bound source.
  - `2c6d81a3-3527-5c5b-8fee-48615f8a7639` — FI-SOC-01 — Kela, valid residence permit rule from 1 April 2026 — `https://www.kela.fi/news/persons-without-a-valid-residence-permit-no-longer-qualify-for-residence-based-kela-benefits-as-of-1-april-2026` — supports scope and start date of residence-based Kela condition.
  - `cd369471-8188-5489-83e8-4802155b2617` — FI-SOC-01 — Kela, proposed TP extension of `yleistuki` from 1 April 2027 — `https://www.kela.fi/news/government-proposes-extension-of-right-to-general-social-security-benefit-to-individuals-under-temporary-protection-as-of-1-april-2027` — identifies proposal as not in force.
  - `0f5f6d43-b764-5cc5-9813-3e86e9ccb296` — FI-EMP-02 — Migri, employer announcements on employment relationships — `https://migri.fi/en/employer-s-announcements-on-employment-relationships` — direct support for covered hiring notices and the TP permit-holder early-ending-notice exemption.
- **UPDATE existing records:** none. New records should be stamped `LastCheckedAt: 2026-11-17T00:00:00+00:00`; preserve existing source IDs/URLs for current replacements.
- **REMOVE (3):** `a8f043b1-b76c-532c-8d62-16a988efb1cd` broken JMF; `0d535e37-f80b-59c8-9a35-57038c1d876b` unused unclear TP-02 Information Bank; `d5bed9b1-7eb7-5223-a1d1-c56b705c712e` legacy Kela UA1e.
- **RETAIN AS QUALIFIED (1):** `fa6d5f24-dab9-50d5-8c4a-500931079914`, FI-TP-01. It remains useful for the municipality-of-residence statement and to evidence the wording conflict, but not to establish an individual's 2028 validity.
- **Duplicates:** Production source inventory has 100 Finland IDs and 90 distinct URLs. Cross-article URL reuse is owner-specific. One same-owner FI-EMP-02 URL pair (`7ab4c5c4-4363-5083-9734-4a16e6d72e74` and `e0b8e4ac-3707-5842-8fc1-63f81bbb2532`, both Employment Contracts Act 55/2001) is intentionally retained for distinct consolidated-law and §2:4 citation roles; do not merge. No duplicate exists among the three proposed additions.

### Bindings

- **ADD:** Bind `6bb34b1e-6949-58ff-99ba-9ffa3769e03b` to FI-EDU-01's supplementary-preparatory statement and `education-pupil-language`.
- **ADD:** Bind `2c6d81a3-3527-5c5b-8fee-48615f8a7639` to FI-SOC-01's valid-permit statement and Compare `social-application` / `social-child-benefits`.
- **ADD:** Bind `cd369471-8188-5489-83e8-4802155b2617` to FI-SOC-01's future-only 2027 proposal statement.
- **ADD:** Bind `0f5f6d43-b764-5cc5-9813-3e86e9ccb296` to FI-EMP-02's employer-notification paragraph and Compare `work-employer-duties`.
- **REMOVE:** Unbind `a8f043b1-b76c-532c-8d62-16a988efb1cd` from `employment-assistance-conditions`; retain current JMF registration/integration, law and Kela sources listed above.
- **REMOVE:** Unbind `0d535e37-f80b-59c8-9a35-57038c1d876b` from FI-TP-02 and remove the article's inline token. Keep `fa6d5f24…` only in FI-TP-01 qualified copy and `protection-dates` conflict explanation.
- **REMOVE:** Unbind `d5bed9b1-7eb7-5223-a1d1-c56b705c712e` from `social-child-benefits`; preserve current Kela Ukraine guidance and child-benefit sources.
- **UPDATE:** Replace `protection-dates` EN/UK/RU with the revised conflict wording staged in `COMPARE_DRAFT_01.md`. Replace `employment-assistance-conditions`, `work-employer-duties`, education enrolment/language and social application/child-benefit/subsistence text with their reviewed Compare-draft versions. No source-only fact should remain active after the binding removals.

## Cross-article consistency

**Material conflicts: 0 after staged corrections.** TP ↔ Employment uses registration-stage work rights and does not infer pending entrepreneurship or jobseeker eligibility. TP ↔ Healthcare distinguishes pending/granted/reception stages. TP ↔ Housing and Social use eligibility to apply—not registration—as the three-month reception transition trigger. TP ↔ Education separates access from compulsory duty and local placement. Kela eligibility is not inferred from TP, identity code or `kotikunta`; county assistance remains separate. The same 2027/2028 conflict is retained in TP-01 and Compare `protection-dates`.

## LastUpdatedAt plan

**UPDATE TO 2026-11-17 for all 8 articles (24 language records):**

- FI-TP-01 — `ffd32dca-56d8-5dcf-b995-7522a159010a`
- FI-TP-02 — `344c5e31-45a9-5f22-8d4a-4a58d8fe8243`
- FI-EMP-01 — `53743a68-bacb-5344-9aee-559e146cab54`
- FI-EMP-02 — `0abffa8c-cd42-571e-97f3-e160afad880c`
- FI-HEALTH-01 — `ea41d1e7-f921-50af-9886-2232d9fb06ef`
- FI-HOUSING-01 — `61b331e0-76bb-58f4-91e7-e5944ba77e31`
- FI-EDU-01 — `2a2094da-e321-50bd-8a45-bf3988be06c9`
- FI-SOC-01 — `51691974-1c20-5b25-9572-66624984a52f`

Each article has substantive research-driven copy changes in the completed refresh; use timestamp `2026-11-17T00:00:00+00:00` for its EN, UK and RU production records at integration. No date was changed in production during this review.

## Remaining evidence ceilings

Finnish military-rule transition wording and exhaustive proof checklist; individual 2027–2028 permit/card record and travel validity; local/no-document intake details; Blue Card and pending rights after TP expiry; pending-applicant jobseeker registration and entrepreneurship; exceptional no-`kotikunta` employment route; post-reception healthcare handoff without `kotikunta`; work-based Kela decisions; local emergency accommodation and regional 116117 coverage; local school/programme availability; institution-specific admission/qualification decisions; individual Kela/county decisions; and post-2028 transition. Copy qualifies each; these are not blockers.

## Production integration plan

This is a handoff plan only; production remains unchanged.

- **Core replacements:** synchronize EN/UK/RU drafts for all eight article IDs above. FI-TP-01 includes the corrected RU 2027/2028 wording; FI-TP-02 omits the unused unclear source; FI-EMP-01 omits the broken source; FI-EMP-02 binds Migri’s current employer-notice guidance; FI-EDU-01 and FI-SOC-01 use new owner-bound sources and corrected tokens.
- **Compare revisions:** synchronize all 18 `REVISE` aspects in `COMPARE_DRAFT_01.md`; retain the six `KEEP` facts unchanged. Update `protection-dates` to the explicit conflict statement and remove `fa6d` as proof of any individual validity while retaining it only as qualified conflict evidence. Update `employment-assistance-conditions` without the broken source. All four locale/source bindings for social/education edits are listed in the Compare table above.
- **Source changes:** add four records, remove three records, retain one unclear record only as qualified; no existing source-record updates. After 4 additions/3 removals, projected Finland source-record count is 101 with 91 distinct URLs (from the 100-record/90-URL review baseline), if no unrelated catalog change occurs.
- **Statuses:** preserve the existing Core `VerificationStatus=1` values and Compare `Status=Approved` / `IsAvailable=true` values; this review proposes no status changes. The current Compare records should be synchronized to the reviewed content before final approval.
- **LastUpdatedAt:** set all 24 Core locale records to `2026-11-17T00:00:00+00:00` during the authorized integration step.

## Verdict

**READY FOR FINLAND PRODUCTION INTEGRATION.** The production sync must apply the exact source removals/rebindings and revised Compare wording above; do not carry forward the broken JMF source, the unused TP-02 Information Bank source or the dated UA1e form, and do not present the 2028 individual permit/card outcome as settled.
