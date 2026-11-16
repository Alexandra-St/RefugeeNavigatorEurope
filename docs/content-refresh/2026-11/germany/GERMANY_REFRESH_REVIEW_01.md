# Germany Refresh Review 01

**As of:** 16 November 2026<br>
**Scope:** Independent review of the Germany refresh drafts, Task 54 evidence, all 24 production Compare facts and current Germany production records. Production JSON, runtime and tests were not changed.

## Article decisions

| Article | Decision | Issues | Required edits |
|---|---|---|---|
| DE-TP-01 — Applying for Temporary Protection and Checking Residence Dates | **APPROVE** | EU scheme dates, German permit continuation, §81 effects, work annotation, and proposed/current distinction are consistent with the corrected EU decision and statute. The EU Article 2 date is 5 August 2026 under the 5 August corrigendum; the same-state continuity cutoff is 4 August. | None. Keep the supplied corrigendum and controlling statute links. |
| DE-TP-02 — Residence Options After Temporary Protection | **APPROVE** | §39(1) is procedural, not approval; §20a(4) bars an in-country Chancenkarte grant to a §24/Division 5 holder; routes and settlement conditions are distinguished. | None. Use the source additions in the production plan. |
| DE-EMP-01 — Finding a Job in Germany | **APPROVE** | BA/Jobcenter roles and benefit eligibility remain separate; LRA is described as a bill and current SGB II reform as enacted. Course access is conditional. | None. Replace the inaccessible course bindings during integration. |
| DE-EMP-02 — Work Authorization and Employee Rights | **APPROVE** | Work permission remains document-specific; pending filing is not blanket permission. €13.90 is current through 2026 and €14.60 is explicitly future from 1 January 2027. | None. Refresh the BMAS source check date; replace the inaccessible Customs FAQ binding. |
| DE-HEALTH-01 — Accessing Healthcare in Germany | **APPROVE** | Employment/SGB V, SGB II, SGB XII via Sozialamt/§264, AsylbLG §§4/6 and optional §417 routes are separated; §24 alone does not determine cover. The bill is not current law. | None to article draft. Add the §264 source record and replace inaccessible care/emergency/psychosocial bindings. |
| DE-HOUSING-01 — Finding Accommodation in Germany | **APPROVE** | §24(3)–(5) distribution/assignment, no chosen-place guarantee, assignment end on permit issuance, local capacity, scheme-specific costs, local reasonableness limits, heating and prior-consent conditions are qualified. SGB II/SGB XII changes effective 1 July 2026 are separated from the LRA bill. | None. Replace inaccessible Germany4Ukraine bindings with the cited statutes and local-authority routes. |
| DE-EDU-01 — Education and German-Language Learning | **APPROVE WITH EDITS** | Land competence, local school placement, childcare entitlements, course admission, university decisions, Ukrainian 2026+ certificate documents and fee exceptions are appropriately qualified. EN and UK had repeated school, higher-education and course paragraphs. | Repeated paragraphs removed in EN/UK only; no claims or thresholds changed. Source plan replaces the broken school/course links and limits the 2022/23 KMK source to historical context. |
| DE-SOC-01 — Financial and Social Assistance | **APPROVE** | SGB II/SGB XII/AsylbLG routes, §74, Jobcenter/Sozialamt, current 2026 rates, Kindergeld conditions and future-only bill wording are consistent. Standard rates are not promised as total awards. | None. Add the two current BMAS rate sources and remove the inaccessible Germany4Ukraine binding. |

## Translation parity

**PASS — 24/24 drafts reviewed.** Across EN/UK/RU, the dates, euro amounts, cited sections, work-right qualifications, benefit routes, Land/local limits and current-versus-proposed distinction agree in substance. The 5 August 2026 military-rule start date and 4 August same-Member-State exception cutoff agree with the corrected EU text. The €13.90 current / €14.60 future wage sequence, all SGB II/SGB XII and AsylbLG rate amounts, and €259 Kindergeld amount match. No translation turns “may” into “must”, or a conditional route into a guaranteed entitlement. Education EN/UK repeated blocks were removed; RU already contained one copy of each point.

## Compare decisions

All 24 production facts were checked against the refreshed article set. Final review decision: **12 KEEP / 12 REVISE / 0 UNAVAILABLE / 0 NEWLY AVAILABLE**. All facts remain attached to the correct Germany owner article. “Source plan” identifies bindings to retain/remove and source records to add; the exact planned new record IDs and URLs are listed under Production integration plan.

| Aspect | KEEP/REVISE | Review result | Source plan |
|---|---|---|---|
| `protection-scheme` | KEEP | §24 remains the German route for the EU temporary-protection category, subject to EU eligibility and exclusions. | Retain `5c1ef33d-3718-571e-b629-04d45282c9c4`. |
| `protection-application` | KEEP | Competent Ausländerbehörde and local appointment/registration routes are accurate; Brandenburg e-service is not described as nationwide. | Remove broken `e4bb8ae0-6e0a-573b-8ab9-c6ea8fffb30a`; retain `1d973c40-5d66-5b6a-ae13-78f667576a0a` and `8a620e3a-0f27-4ce4-8b16-47f8c022f171`; add `a71ea73c-f2db-5545-81c8-1a4c5016c361` where the 90-day exemption is stated. |
| `protection-dates` | REVISE | State current EU period through 4 March 2027; adopted next period 5 March 2027–4 March 2028; separately state the specified German automatic §24 permit continuation through 4 March 2027. Explicitly say these do not decide an individual document or work right. | Retain `44ce6030-635a-5dd4-bf37-b1729e464399`, `995812d6-001c-53e3-9067-d294940375a4`, and corrigendum `d817d9ac-37ce-5d8e-afe3-82bbf9e06058`; add planned `71b930ae-07df-43ce-98d1-3e318024e9ec` (Decision 2025/1460). |
| `residence-conditions` | KEEP | Separate route evidence, qualification, visa and general issuance conditions remain qualified by statutory exceptions. | Retain `52ae54a2-8722-5126-be29-2f0b21317e9a`, `6e58a584-7b8e-5eb3-8052-8c7a1c2298f1`, `474e9cf8-a42d-5e1c-a460-fdd7d8ff93ba`, `fefac5aa-e072-5beb-b725-266b562add44`. |
| `residence-filing` | REVISE | §39(1) is procedural only. In-country filing is route-specific; §20a(4) requires an existing Division 3 or 4 title for an in-country Chancenkarte grant, so §24 is not enough. | Retain `6e93b253-17e8-52e1-b409-856eec6c93f7`, `b223d9dd-b2a0-5691-b86f-f7c1f768b6ac`; add planned §20a record `cd204676-aad5-4b2a-85c0-6458a7a4f878`. |
| `residence-routes` | REVISE | Include §18g and §20a among separate titles, identify the Chancenkarte limitation, and qualify §§26(4), 9a and 9b. No automatic switch or settlement entitlement. | Retain current §16a/§16b/§16d/§18a/§18b records; add planned `b8841c5a-0cf9-4d14-8ee6-6e6763b7ef1d` (§18g), `cd204676-aad5-4b2a-85c0-6458a7a4f878` (§20a), `f9c976fe-55de-445d-8ff3-2c4494d14a35` (§26), `b479b1ca-5ace-48e5-a19b-f54ba60c1a07` (§9a), `08b1e9d9-2a4c-4b3b-9218-145ccf931c1c` (§9b). |
| `employment-vacancies` | KEEP | BA job portal is an appropriate national starting point; a listing is not a work permit. | Retain `de3e9bbb-3c43-5954-b2da-1738363d13da`. |
| `employment-institutions` | KEEP | BA advice and Jobcenter’s case-specific role are distinguished. | Retain `95d62fd8-28c0-5c91-8cbc-5ab7701693b3`, `95178c6b-4f5c-5062-8e4f-a69ce4fa45e3`. |
| `employment-assistance-conditions` | KEEP | SGB II conditions and the limited §74 route after identity processing/corresponding certificate are accurately stated. | Retain `d5cdfb62-8c92-5a38-b457-efdd6fbc3f3b`, `7b1f5206-fa2d-5bb4-b9bb-b0302a13636c`. |
| `work-permission` | REVISE | Explicitly distinguish §81(3), §81(4), the certificate entry and §81(5a); pending application alone is not blanket work permission. | Retain `cac1773b-211a-5b77-9f96-8056401b5902`, `dabe7e22-2c34-5c78-b0a2-3d29c9677d14`. |
| `work-employer-duties` | KEEP | §4a(5) employer checking/copy duties and the narrower notification scope remain accurate. | Retain `cac1773b-211a-5b77-9f96-8056401b5902`. |
| `work-minimum-pay` | REVISE | €13.90 gross/hour is current from 1 January 2026; €14.60 starts 1 January 2027. Exceptions and higher sector floors remain visible. | Retain current BMAS source `a8f6a845-d29b-57dd-b873-73fd15b498c8`; optionally bind MiLoV5 from the draft link as a second primary source during integration. |
| `healthcare-access` | REVISE | Retain SGB V for specified insured groups and AsylbLG §§4/6 as a separate route; add SGB XII medical treatment through Sozialamt/§264 and conditional §417 voluntary membership. §24 alone does not choose coverage. | Retain `c4de5dcd-311c-541f-8444-8829c5be3923`, SGB II §7 `2b09bea9-3062-540d-83b4-0148f8eebd39`, `79f1bd04-1d45-5170-9f54-ccdf448a8a74`, `ec0c23f7-1858-5025-8c5d-67200f6e5d8a`; add planned SGB V §264 `61c55ab9-e112-4d40-b9ac-4fd7e79cbff3`; add planned SGB V §417 record `5b2f0558-a269-43ef-9f8c-3b880ba92ec5`. |
| `healthcare-proof` | KEEP | Card, treatment certificate and local health-card routes are scheme/locality dependent. | Remove broken `2c177ebd-53e0-5641-8bae-6f08f1df20de` and `8e121eb3-d156-53b9-a76f-283f35e83d43`; retain BMG `e273f820-0f99-5003-95ce-ff3b110a6b44` and bind the relevant current statutes. |
| `healthcare-urgent` | KEEP | 112 and 116117 distinction and language limitation are safe. | Remove broken `41b9dbd1-241b-5c36-9a58-728a98b5ca85`; retain `45a3c454-6c68-58bf-ae29-6016670c392b`. |
| `housing-initial` | REVISE | Distinguish Asylum Act reception from §24 distribution/assignment under §24(3)–(5); no chosen place entitlement; assignment ends when §24 permit issues; local shelter capacity is not guaranteed. | Remove broken `74aadb89-3efe-5bec-bc79-28d2a097de27`, `6f604127-6636-5674-b7a7-fdaa20a379dc`; add planned `fd6064f4-cac4-47bf-aa79-18ab75bd522c` (AufenthG §24) and `c1944f9d-c7aa-497a-9738-18e7cc5888a5` (AsylG §44). |
| `housing-private-rental` | KEEP | Written offer, local limits and asking the correct authority before signing remain appropriate. | Remove broken `6f604127-6636-5674-b7a7-fdaa20a379dc`; retain `469e84d9-6769-5fa1-8137-dea153b2f2c6`, `ec4a811e-06af-5ed2-9a45-20ec6f17c0bb`. |
| `housing-financial-support` | REVISE | State SGB II §22, SGB XII §35 and AsylbLG §3 routes; local reasonableness, separate heating assessment, 2026 waiting-period cap and prior-assurance conditions. Do not state a nationwide rent threshold. | Retain `469e84d9-6769-5fa1-8137-dea153b2f2c6`, `ec4a811e-06af-5ed2-9a45-20ec6f17c0bb`, `39e9b8cf-22f8-5a51-9bd2-b57f13cc67e7`. |
| `education-school-enrolment` | KEEP | Rules, documents and placement are correctly assigned to the Land/local authority; no national form is claimed. | Remove broken `a9192f33-66b2-56fc-b24f-ade0c3d2c6f0`; update current KMK record `6ab0d347-e98f-5243-84ac-d06e9b3377bd` to the active Länder-guidance URL below. |
| `education-pupil-language` | REVISE | State that each Land handles placement and German support; ask the local authority about current provision; format and places vary. The 2022/23 KMK item is historical context only. | Remove broken `a9192f33-66b2-56fc-b24f-ade0c3d2c6f0`; update/bind current KMK `6ab0d347-e98f-5243-84ac-d06e9b3377bd`; remove legacy `1a7ade55-783f-59ae-97a2-d2b620104d9f` from the Compare binding (retain it only on the article’s dated historical sentence). |
| `education-higher-admission` | REVISE | University decides admission; uni-assist handles applications/document checks where used. 2026+ Ukrainian school certificates need the additional curriculum statement. Tuition and semester contributions are qualified; §24 does not guarantee admission or BAföG. | Add planned `f7d628fd-2492-44c9-99ec-935e3b0b9a94` (uni-assist UA), `76c2d717-0134-4766-96d3-91a987da74ca` (DAAD admission), `e06b4f58-5576-4368-ae88-f9a1c8852483` (DAAD fees), `8d53addb-7f0c-423c-9aa3-e876447b8dad` (BAföG §61). |
| `social-application` | KEEP | Application office follows the applicable scheme; actual evidence list and award remain case-specific. | Remove broken `d075064c-0110-59a3-9fd7-1164ebd2faa1`; retain `346200f4-e8dc-5be8-949f-d9748e0575fe`, `48566923-6113-5583-bb40-2937fb8874aa`, `cedffb49-4e83-5e1c-aba3-b109bdf4d0e4`. |
| `social-child-benefits` | REVISE | Qualify parent/child location, §24 work entitlement for at least six months, actual-employment exception, €259 monthly rate and Familienkasse decision. | Retain BA source `261f2bd0-1cf1-55e2-9193-55a946d094e2`; retain the relevant current BA forms/eligibility source records. |
| `social-subsistence` | REVISE | Separate SGB II/SGB XII/AsylbLG routes, §74 limits, correct 2026 rate groups, income/assets and individualized decisions; the LRA remains prospective. | Retain existing statutory sources; add planned `5f731bf8-642a-49e6-a34e-0a40399f289d` (BMAS SGB II/XII rates) and `56eeb4db-8e18-4306-9888-6427c4103f29` (BMAS AsylbLG rates). |

## Critical legal/date checks

- **Protection dates:** PASS. The currently operative EU period runs through 4 March 2027. Decision 2026/1912 adopts the following period from 5 March 2027 through 4 March 2028. UkraineAufenthFGV §2 separately continues specified §24 permits valid on 1 February 2026 through 4 March 2027. No individual printed date or work right is inferred. The military-obligation rule under Decision 2026/1912 Article 2 applies from 5 August 2026, and the corrigendum sets the same-Member-State continuous-protection cutoff at 4 August 2026. The draft’s 5 August/4 August wording is correct.
- **§81/work:** PASS. §81(3) permitted stay for specified lawful applicants differs from §81(4) continuation of an existing permit on timely filing. §81(5) documents the effect; the certificate’s work entry controls. §81(5a) concerns specified future titles under Chapter 2 Sections 3/4, not blanket work permission for §24 in Section 5. Pending application alone does not mean “may work.”
- **Residence filing/Chancenkarte:** PASS. §39(1) is procedural; the in-country route is conditional. §20a(4) restricts an in-country Chancenkarte grant to a holder of a Division 3 or 4 residence title. §24 is Division 5. The drafts and revised Compare wording do not imply every §24 holder can switch in-country.
- **Minimum wage:** PASS. €13.90 gross/hour is current on 16 November 2026. €14.60 from 1 January 2027 is future and labelled as such. Exceptions and higher sector rates are qualified.
- **Leistungsrechtsanpassungsgesetz:** PASS. It remains an unpromulgated bill, not in force, with no effective date. The enacted 13th SGB II amendment and its 1 July 2026 changes are separately identified. Employment, healthcare, housing and social assistance preserve that distinction.
- **SGB II reform:** PASS. Current Grundsicherung/Grundsicherungsgeld terminology and the effective date are distinct from the Ukraine-specific bill.

## Healthcare review

PASS. Draft distinguishes compulsory employment insurance and SGB V, qualifying SGB II membership, SGB XII treatment through the Sozialamt under §264 (not necessarily GKV membership), AsylbLG §§4/6, and §417’s conditional six-month voluntary-insurance route. §24 by itself does not determine coverage. Local card/certificate procedures, cost-sharing, interpreter coverage, and capacity remain conditional. Compare `healthcare-access` should include the SGB XII/§264 route and §417 as specified above; `healthcare-proof` and `healthcare-urgent` remain KEEP after replacing broken bindings.

## Housing review

PASS. §24(3)–(5) supports federal distribution, Land assignment, no entitlement to select a Land/place and end of assignment on issue of the residence permit. Initial shelter capacity is local and no chosen dwelling is promised. Private rent support is correctly separated under SGB II §22, SGB XII §35 and AsylbLG §3. Local reasonableness thresholds are not represented as nationwide amounts; heating is assessed separately; prior assurance may apply to new housing costs, deposits or moving expenses. The 1 July 2026 SGB II accommodation-cost change is current law, separate from the LRA proposal.

## Education review

PASS. Compulsory-school rules, placement, language/newcomer support and school documents are correctly Land/local. The statutory childcare age rights and phased 2026/27 all-day entitlement are distinct from local provider availability. BAMF admission/course availability and vocational-language eligibility are conditional. University admission remains institution-specific; the 2026+ Ukrainian certificate statement, semester contribution and Land/institution tuition exceptions are accurately qualified. Recognition remains occupation-specific. The 2022/23 KMK resolution is suitable only for explicitly historical context; a current KMK Länder information page is the recommended binding.

## Social-assistance review

PASS. SGB II, SGB XII and AsylbLG routes and authorities are distinct; §74 is limited to qualifying applicants/certificates and identity processing. The 2026 SGB II/SGB XII and AsylbLG figures match the current rate schedules. They are standard/basic needs rather than guaranteed full awards; means, household, housing, additional needs and in-kind delivery matter. The BA simplified Kindergeld route and €259 monthly amount are properly qualified, and possible means-tested income treatment is noted. The Ukraine-specific bill is only future law unless enacted.

## Broken / unclear source remediation

Source recheck: **17 BROKEN, 1 UNCLEAR, 1 LEGACY, 68 CURRENT (87 total).** BMAS source `4ca2698c-a15c-519c-8c8f-eb5153cc63e2`, previously UNCLEAR because its owner page rendered navigation only, now opens with substantive current employee-rights guidance; reclassify it CURRENT and refresh `LastCheckedAt`. The remaining UNCLEAR record `76e5e3d9-9248-5a85-9b9e-f699b314d4f8` is the Germany4Ukraine military-obligation notice: retain it only as an attributed description of the notice’s discrepancy, never as the controlling legal rule; bind the corrected EU decision alongside it. The single LEGACY record remains only for its dated 2022/23 historical sentence.

| Source record ID | Affected owner/fact | Action | Current replacement and replacement binding |
|---|---|---|---|
| `e4bb8ae0-6e0a-573b-8ab9-c6ea8fffb30a` | DE-TP-01; `protection-application` | Remove broken record/binding. | Existing DE-TP-01 registration record `1d973c40-5d66-5b6a-ae13-78f667576a0a`; UkraineAufenthÜV `a71ea73c-f2db-5545-81c8-1a4c5016c361` for the exemption. |
| `e77a12f7-8f60-5e76-a485-99f0fc69be9f` | DE-EMP-01; integration-course eligibility | Remove broken record/binding. | Add `22a566e9-1765-4f01-9160-3a18b3a08e60` (BAMF Ukraine integration-course route/fee document) to DE-EMP-01. |
| `d99613b7-b59a-5a93-a443-2d4d2eb48d14` | DE-EMP-01; vocational-language courses | Remove broken record/binding. | Add `9d2807d2-c7d6-4c76-9298-a744848503ce` (BA integration and vocational-language course access) to DE-EMP-01. |
| `1d9ec59d-0aed-5574-b70a-b9cbd54d45e8` | DE-EMP-02; FKS reporting/enforcement | Remove broken record/binding. | Add `fb3ea379-c49c-43dc-9426-0ab2ae6c7168` (BMF official FKS/minimum-wage violations and reporting) to DE-EMP-02. |
| `2c177ebd-53e0-5641-8bae-6f08f1df20de` | DE-HEALTH-01; GKV/health proof | Remove broken record/binding. | Existing BMG Ukraine medical-help record `e273f820-0f99-5003-95ce-ff3b110a6b44` plus SGB V/AsylbLG statutes. |
| `8e121eb3-d156-53b9-a76f-283f35e83d43` | DE-HEALTH-01; treatment route | Remove broken record/binding. | Existing BMG record `e273f820-0f99-5003-95ce-ff3b110a6b44` plus applicable statute. |
| `41b9dbd1-241b-5c36-9a58-728a98b5ca85` | DE-HEALTH-01; urgent care | Remove broken record/binding. | Existing 116117 record `45a3c454-6c68-58bf-ae29-6016670c392b`. |
| `d77b1eaf-1396-5dfe-ac10-6f148d43cbeb` | DE-HEALTH-01; psychosocial support | Remove broken record/binding. | Add `5e8ba693-47df-4ab2-bce7-176a59117401` (BAfF national psychosocial-centre directory) to DE-HEALTH-01. |
| `74aadb89-3efe-5bec-bc79-28d2a097de27` | DE-HOUSING-01; initial arrival/placement | Remove broken record/binding. | Add `fd6064f4-cac4-47bf-aa79-18ab75bd522c` (AufenthG §24) and `c1944f9d-c7aa-497a-9738-18e7cc5888a5` (AsylG §44) to DE-HOUSING-01; local capacity stays local. |
| `6f604127-6636-5674-b7a7-fdaa20a379dc` | DE-HOUSING-01; reception/housing | Remove broken record/binding. | Same §24/AsylG statutory replacement records above, plus local Land/municipal links selected for the actual local route. |
| `ceab5bb5-83bf-5ae8-9020-a031e67383c1` | DE-HOUSING-01; scheme-specific housing support | Remove broken record/binding. | Existing SGB II §22 `469e84d9-6769-5fa1-8137-dea153b2f2c6`, SGB XII §35 `ec4a811e-06af-5ed2-9a45-20ec6f17c0bb`, AsylbLG §3 `39e9b8cf-22f8-5a51-9bd2-b57f13cc67e7`. |
| `01fd1631-d7da-5154-9883-79acf42b8b01` | DE-HOUSING-01; bill/current benefits distinction | Remove broken record/binding. | Existing BMAS bill status `a13af5dd-d701-56cc-bcaf-82292c6b91d7` plus housing statutes above. |
| `a9192f33-66b2-56fc-b24f-ade0c3d2c6f0` | DE-EDU-01; `education-school-enrolment`, `education-pupil-language` | Remove broken record/binding. | Update existing current KMK record `6ab0d347-e98f-5243-84ac-d06e9b3377bd` to https://www.kmk.org/bildungsministerkonferenz/vertiefende-bildungsinhalte/individuelle-foerderung/gefluechtete-aus-der-ukraine.html (16 Länder guidance). |
| `85164038-1935-52a7-a1cb-2e05f4066329` | DE-EDU-01; integration courses | Remove broken record/binding. | Add `17759f6a-a6d7-43ba-b898-cac488377239` (BAMF Ukraine integration-course route/fee document) to DE-EDU-01. |
| `02f7d48e-e06b-5a7e-9fc7-7b17f2bde2be` | DE-EDU-01; vocational-language support | Remove broken record/binding. | Add `0430b5f8-ad92-4748-be98-9e1fc02ecafa` (BA course route/access/availability) to DE-EDU-01. |
| `bbb43319-2619-50d6-9e08-6f4ead298bd2` | DE-EDU-01; vocational training | Remove broken record/binding. | Add `6c977aa9-8be5-4670-9255-0c1562422809` (Make it in Germany official vocational-training route) to DE-EDU-01. |
| `d075064c-0110-59a3-9fd7-1164ebd2faa1` | DE-SOC-01; `social-application`, `social-subsistence` | Remove broken record/binding. | Existing SGB II/SGB XII/AsylbLG law sources plus planned BMAS 2026 rate sources `5f731bf8-642a-49e6-a34e-0a40399f289d` and `56eeb4db-8e18-4306-9888-6427c4103f29`. |
| `4ca2698c-a15c-519c-8c8f-eb5153cc63e2` | DE-EMP-02; employee rights | Reclassify UNCLEAR → CURRENT; update `LastCheckedAt`. | Same source ID and URL, rechecked with substantive BMAS page content. No replacement binding needed. |
| `76e5e3d9-9248-5a85-9b9e-f699b314d4f8` | DE-TP-01; EU military-rule exception discrepancy | Retain with qualification; do not use as sole/current legal authority. | Bind corrected Decision/corrigendum records `995812d6-001c-53e3-9067-d294940375a4` and `d817d9ac-37ce-5d8e-afe3-82bbf9e06058`; keep the notice only to explain its own broad wording. |
| `1a7ade55-783f-59ae-97a2-d2b620104d9f` | DE-EDU-01; historical school-language context | Retain only for the explicitly dated 2022/23 historical statement. | Bind current KMK record `6ab0d347-e98f-5243-84ac-d06e9b3377bd` for present-day Land guidance; never use the legacy resolution to prove current availability. |

## Cross-article consistency

**No unresolved cross-article conflicts.** Checked TP ↔ Employment, Social, Healthcare and Housing; Employment ↔ Social; Housing ↔ Social; Education ↔ Employment. §81 work permissions and residence-extension dates agree. Current SGB II changes are distinguished from the proposed LRA in all four affected topics. Scheme authorities and benefit amounts do not conflict. School and course availability remain Land/local/provider-specific. The protection draft’s military-rule dates were checked against the official corrigendum, which supersedes the uncorrected text.

## LastUpdatedAt integration plan

| Article ID | Plan | Reason |
|---|---|---|
| `1a6b97c4-eb9e-5590-8715-c95f5f95677e` | UPDATE TO 2026-11-16 | Revised factual dates/extension distinction and pending-document work/benefit qualifications in the refreshed article set. |
| `33106e71-115f-5809-9066-7e3d3e5c57a3` | UPDATE TO 2026-11-16 | Added route-specific §18g/§20a options, in-country Chancenkarte restriction and conditional long-term routes. |
| `283d4519-21f5-5be2-a534-1bd2606d95fc` | UPDATE TO 2026-11-16 | Updated current benefit routes, current SGB II reform and pending-bill distinction. |
| `c4895b5a-eacd-53d7-a86f-f504dd615c76` | UPDATE TO 2026-11-16 | Updated current/future wage rates and §81(5a) work-permission qualification. |
| `895b79b4-ce0d-5bee-9919-42ce3d76ecdb` | UPDATE TO 2026-11-16 | Updated the conditional §417, SGB II/SGB XII/AsylbLG cover routes and bill/current-law distinction. |
| `fd041e53-7794-5c1f-9ae4-131930fe69ee` | UPDATE TO 2026-11-16 | Updated §24 assignment and 2026 housing-cost rules, local limits and prior-consent wording. |
| `f5e0f499-0f60-5465-85f8-de6345029b59` | UPDATE TO 2026-11-16 | Updated childcare entitlement, course/admission/fee guidance and 2026+ Ukrainian certificate requirements. |
| `31f5802b-26a0-5cd0-bb24-0a9a4afa706a` | UPDATE TO 2026-11-16 | Updated benefit routes, new 2026 standard rates, Kindergeld and bill status. |

For integration, set `LastUpdatedAt` to 2026-11-16 for all three locale translations of each listed article. The production information-entry schema stores this value per translation.

## Remaining evidence ceilings

- School placement, compulsory-school procedure, language support and course/provider vacancies depend on Land, municipality and current capacity.
- Reception/shelter capacity and local housing reasonableness/approval procedures vary by place and individual case.
- Residence, work, benefit, insurance, childcare, course and family-benefit outcomes depend on the authority’s decision and the person’s document/history.
- University admission and tuition are institution/programme-specific; the review gives navigation only.
- The LRA is a pending proposal and must be rechecked before public release or production integration if the as-of date changes.
- The Germany4Ukraine military notice is retained only as a source-attributed discrepancy; the corrected EU decision controls.

These are bounded local or individual decisions, not unresolved national factual gaps. No broken or unclear record remains the sole support for a material current claim after the listed source remediation.

## Production integration plan

**Core articles to replace:** all eight Germany article entries, preserving their existing IDs and applying their EN/UK/RU draft translations.

**Compare facts to revise (12):** `protection-dates`, `residence-filing`, `residence-routes`, `work-permission`, `work-minimum-pay`, `healthcare-access`, `housing-initial`, `housing-financial-support`, `education-pupil-language`, `education-higher-admission`, `social-child-benefits`, `social-subsistence`. Keep the other 12 facts identified above.

**New official-source records to add (23 total; IDs are reserved in this report for integration; use these exact UUIDs):

| New source ID | InformationEntryId | Title | URL | Bind to |
|---|---|---|---|---|
| `71b930ae-07df-43ce-98d1-3e318024e9ec` | `1a6b97c4-eb9e-5590-8715-c95f5f95677e` | Council Implementing Decision (EU) 2025/1460 — current EU period through 4 March 2027 | https://eur-lex.europa.eu/eli/dec_impl/2025/1460/oj/eng | `protection-dates` |
| `b8841c5a-0cf9-4d14-8ee6-6e6763b7ef1d` | `33106e71-115f-5809-9066-7e3d3e5c57a3` | AufenthG §18g — EU Blue Card | https://www.gesetze-im-internet.de/aufenthg_2004/__18g.html | `residence-routes` |
| `cd204676-aad5-4b2a-85c0-6458a7a4f878` | `33106e71-115f-5809-9066-7e3d3e5c57a3` | AufenthG §20a — Chancenkarte and in-country limitation | https://www.gesetze-im-internet.de/aufenthg_2004/__20a.html | `residence-filing`, `residence-routes` |
| `f9c976fe-55de-445d-8ff3-2c4494d14a35` | `33106e71-115f-5809-9066-7e3d3e5c57a3` | AufenthG §26 — humanitarian settlement routes | https://www.gesetze-im-internet.de/aufenthg_2004/__26.html | `residence-routes` |
| `b479b1ca-5ace-48e5-a19b-f54ba60c1a07` | `33106e71-115f-5809-9066-7e3d3e5c57a3` | AufenthG §9a — EU long-term residence | https://www.gesetze-im-internet.de/aufenthg_2004/__9a.html | `residence-routes` |
| `08b1e9d9-2a4c-4b3b-9218-145ccf931c1c` | `33106e71-115f-5809-9066-7e3d3e5c57a3` | AufenthG §9b — residence periods | https://www.gesetze-im-internet.de/aufenthg_2004/__9b.html | `residence-routes` |
| `22a566e9-1765-4f01-9160-3a18b3a08e60` | `283d4519-21f5-5be2-a534-1bd2606d95fc` | BAMF — Ukraine integration-course admission and fee | https://www.bamf.de/SharedDocs/Anlagen/DE/Integration/Integrationskurse/Kurstraeger/Traegerrundschreiben/Anlagen/2022/traegerrundschreiben-20220318-04-anlage1.pdf?__blob=publicationFile&v=5 | DE-EMP-01 course guidance |
| `9d2807d2-c7d6-4c76-9298-a744848503ce` | `283d4519-21f5-5be2-a534-1bd2606d95fc` | BA — integration and vocational-language courses | https://www.arbeitsagentur.de/arbeitslos-arbeit-finden/sprachkurse | DE-EMP-01 course guidance |
| `fb3ea379-c49c-43dc-9426-0ab2ae6c7168` | `c4895b5a-eacd-53d7-a86f-f504dd615c76` | Federal Ministry of Finance — FKS and reporting suspected minimum-wage violations | https://www.bundesfinanzministerium.de/Content/DE/Standardartikel/Themen/Zoll/Gerechtigkeit-faire-Arbeit/fks-im-einsatz-fuer-mindestlohn.html | `work-employer-duties` / FKS next-step link |
| `61c55ab9-e112-4d40-b9ac-4fd7e79cbff3` | `895b79b4-ce0d-5bee-9919-42ce3d76ecdb` | SGB V §264 — treatment for certain non-members | https://www.gesetze-im-internet.de/sgb_5/__264.html | `healthcare-access` |
| `5b2f0558-a269-43ef-9f8c-3b880ba92ec5` | `895b79b4-ce0d-5bee-9919-42ce3d76ecdb` | SGB V §417 — voluntary insurance for qualifying §24 holders | https://www.gesetze-im-internet.de/sgb_5/__417.html | `healthcare-access` |
| `5e8ba693-47df-4ab2-bce7-176a59117401` | `895b79b4-ce0d-5bee-9919-42ce3d76ecdb` | Federal Association of Psychosocial Centres — regional service directory | https://www.baff-zentren.org/hilfe-vor-ort/psychosoziale-zentren/ | DE-HEALTH-01 psychosocial support |
| `fd6064f4-cac4-47bf-aa79-18ab75bd522c` | `fd041e53-7794-5c1f-9ae4-131930fe69ee` | AufenthG §24 — temporary protection distribution and assignment | https://www.gesetze-im-internet.de/aufenthg_2004/__24.html | `housing-initial` |
| `c1944f9d-c7aa-497a-9738-18e7cc5888a5` | `fd041e53-7794-5c1f-9ae4-131930fe69ee` | AsylG §44 — reception centres for asylum applicants | https://www.gesetze-im-internet.de/asylg/__44.html | `housing-initial` |
| `17759f6a-a6d7-43ba-b898-cac488377239` | `f5e0f499-0f60-5465-85f8-de6345029b59` | BAMF — Ukraine integration-course admission and fee | https://www.bamf.de/SharedDocs/Anlagen/DE/Integration/Integrationskurse/Kurstraeger/Traegerrundschreiben/Anlagen/2022/traegerrundschreiben-20220318-04-anlage1.pdf?__blob=publicationFile&v=5 | DE-EDU-01 course guidance |
| `0430b5f8-ad92-4748-be98-9e1fc02ecafa` | `f5e0f499-0f60-5465-85f8-de6345029b59` | BA — integration and vocational-language courses | https://www.arbeitsagentur.de/arbeitslos-arbeit-finden/sprachkurse | DE-EDU-01 course guidance |
| `6c977aa9-8be5-4670-9255-0c1562422809` | `f5e0f499-0f60-5465-85f8-de6345029b59` | Make it in Germany — vocational training | https://www.make-it-in-germany.com/en/visa-residence/types/training | DE-EDU-01 vocational training |
| `f7d628fd-2492-44c9-99ec-935e3b0b9a94` | `f5e0f499-0f60-5465-85f8-de6345029b59` | uni-assist — Ukraine school certificate documents | https://www.uni-assist.de/en/tools/info-country-by-country/details-country/country/ua/ | `education-higher-admission` |
| `76c2d717-0134-4766-96d3-91a987da74ca` | `f5e0f499-0f60-5465-85f8-de6345029b59` | DAAD — university admission requirements | https://www.daad.de/en/studying-in-germany/requirements/overview/ | `education-higher-admission` |
| `e06b4f58-5576-4368-ae88-f9a1c8852483` | `f5e0f499-0f60-5465-85f8-de6345029b59` | DAAD — tuition and semester contributions | https://www.daad.de/en/studying-in-germany/living-in-germany/finances/ | `education-higher-admission` |
| `8d53addb-7f0c-423c-9aa3-e876447b8dad` | `f5e0f499-0f60-5465-85f8-de6345029b59` | BAföG §61 — qualifying §24 applicants | https://www.gesetze-im-internet.de/baf_g/__61.html | `education-higher-admission` |
| `5f731bf8-642a-49e6-a34e-0a40399f289d` | `31f5802b-26a0-5cd0-bb24-0a9a4afa706a` | BMAS — 2026 SGB II/SGB XII standard rates | https://www.bmas.de/DE/Arbeit/Grundsicherung-fuer-Arbeitsuchende/Leistungen-und-Bedarfe-in-der-Grundsicherung-fuer-Arbeitsuchende/leistungen-und-bedarfe-in-der-grundsicherung-fuer-arbeitsuchende.html | `social-subsistence` |
| `56eeb4db-8e18-4306-9888-6427c4103f29` | `31f5802b-26a0-5cd0-bb24-0a9a4afa706a` | BMAS — 2026 AsylbLG rates | https://www.bmas.de/DE/Soziales/Sozialhilfe/LeistungenAsylbewerberleistungsgesetz/leistungssaetze-asylbewerberleistungsgesetz.html | `social-subsistence` |

**Source records to update:**

- `6ab0d347-e98f-5243-84ac-d06e9b3377bd`: update title/URL to “KMK — Ukraine refugee education and current Länder information”, `https://www.kmk.org/bildungsministerkonferenz/vertiefende-bildungsinhalte/individuelle-foerderung/gefluechtete-aus-der-ukraine.html`; set `LastCheckedAt` to 2026-11-16.
- `4ca2698c-a15c-5191-8c8f-eb5153cc63e2`: retain its current URL/title and update `LastCheckedAt` to 2026-11-16; reviewed BMAS page now has substantive content.

**Bindings/source records to remove:** remove the 17 broken records listed above from their owner entries and all Compare bindings; remove any binding to the legacy KMK PDF except the dated historical language-support context. Do not use the remaining unclear Germany4Ukraine notice as a source for the controlling legal rule.

**Statuses to change:** in the research source-status ledger, change `4ca2698c-a15c-5191-8c8f-eb5153cc63e2` from UNCLEAR to CURRENT after this direct recheck. Keep `76e5e3d9-9248-5a85-9b9e-f699b314d4f8` UNCLEAR/qualified and `1a7ade55-783f-59ae-97a2-d2b620104d9f` LEGACY. The production source JSON has no status field; do not invent one.

**LastUpdatedAt changes:** all eight article IDs above, for EN/UK/RU translations, to 2026-11-16.

## Verdict

**READY FOR GERMANY PRODUCTION INTEGRATION.** The only draft edits from this review remove repeated EN/UK education blocks; no factual date correction was required after checking the official corrigendum. The current proposed Compare wording is evidence-qualified, the 17 inaccessible bindings have specific replacements or supporting current statutes, the one remaining unclear source is disclosed and paired with controlling EU text, and the historical KMK source is limited to historical context. Production integration must apply this report’s source, Compare and LastUpdatedAt plan; production files remain untouched in this review.
