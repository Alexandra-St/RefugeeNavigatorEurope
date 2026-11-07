# Bulgaria v1 Post-Correction Editorial Review

**Baseline:** `4037050642ecc13954316ccec504fced2be83ffc`  
**Review date:** 2026-11-07  
**Previous audit:** `EDITORIAL_ACCEPTANCE_PREPARATION.md` (historical; unchanged)  
**Scope:** Bulgaria v1 drafts, catalog translations, source records, comparison facts and existing Bulgaria integration-test assertions. No new web research was performed. Only the named Bulgaria research packages were consulted for the two corrected source URLs and the TP stay/work evidence boundary. No application suite was rerun.

## Review verdict

All **1 blocker + 8 minor findings** from the previous audit are resolved. The review found no new material editorial or source defects. All eight articles and all 24 available Draft comparison facts are ready for owner acceptance, subject to their stated research qualifications. This review is not owner acceptance and does not change editorial or catalog status.

## 1. Previous correction resolution matrix

| Finding | Result | Post-correction basis |
| --- | --- | --- |
| Blocker — BG-TP-02 / `residence-filing` | **Resolved** | The TP stay/work and termination boundary now cites the Bulgarian Asylum and Refugees Act; the separate pending-work statement cites Bulgarian residence/employment laws. The research package supports TP stay/work during effective TP, identifies Art. 82’s termination boundary, and says no general pending-application stay bridge or new work right was established. Directive 2003/109 appears only with the EU long-term-resident exclusion. The application language is qualified as “not established” / “general,” not an absolute ban. |
| Minor 1 — BG-HEALTH-01-S09 URL | **Resolved** | Source `6dd90ade-3354-5160-97dc-fe4d66eed6cb` now exactly matches the BG-HEALTH-S15 AREF card-information URL in the scoped healthcare research inventory. |
| Minor 2 — BG-HOUSING-01-S01 URL | **Resolved** | Source `89d9cea9-d325-58fc-89fb-0b78c574b5a1` now exactly matches the BG-HOUSING-S01 AREF consolidated-PDF URL in the scoped housing research inventory, including the literal parentheses in the URL. |
| Minor 3 — `protection-dates` | **Resolved** | Uses the Bulgarian 2026 extension notice and the direct 2026 EU decision for the national 4 Mar 2027 and EU 4 Mar 2028 dates; the unrelated 2022 procedure source is absent. |
| Minor 4 — `employment-assistance-conditions` | **Resolved** | Sources now concern the Employment Promotion Act, jobseeker registration/service and the Refugee Employment and Training Programme. NSSI unemployment-benefit and qualification-recognition sources are absent. |
| Minor 5 — `housing-initial` | **Resolved** | Sources support the statutory accommodation right and Program 3 / provider-period boundaries. The Program 2 closure source is absent from this fact. |
| Minor 6 — `housing-financial-support` | **Resolved** | The source array contains only the Program 3 amendment and provider-period evidence; the general social-assistance source is absent. |
| Minor 7 — `social-subsistence` | **Resolved** | The array retains general social-assistance law and 2026 assistance guidance; the separate heating-allowance source is absent. |
| Minor 8 — `social-child-benefits` | **Resolved** | The array retains family-benefit law/guidance, relevant court material and NSSI maternity/childcare evidence. The People with Disabilities Act source is absent. |

**Previous findings:** blocker resolved **1/1**; minor findings resolved **8/8**.

## 2. Article review

All eight drafts have EN/UK/RU sections, matching catalog titles, summaries and content, citations that resolve to article-owned sources, and equivalent material qualifications across languages. Dates and amounts remain bounded by the relevant source and do not imply an individual entitlement or guaranteed service.

| Draft | Editorial/source and evidence-boundary review | Result |
| --- | --- | --- |
| `BG-TP-01` | EU eligibility groups and Bulgaria’s national route remain distinct. The 4 Mar 2027 national and 4 Mar 2028 EU dates are not presented as individual card validity or renewal guarantees. Missing initial-document and local process details remain expressly unresolved. | **Ready** |
| `BG-TP-02` | No automatic status conversion is asserted. In-country single-permit eligibility for a TP card and later national residence-time credit remain unresolved. The revised pending-application paragraph cites the Bulgarian TP boundary and relevant residence/work laws; the EU directive is confined to its own scope exclusion. UK and RU preserve the same qualifications as EN. | **Ready** |
| `BG-EMP-01` | Vacancy search, address-based jobseeker registration and conditional counselling/referral/programme services are kept distinct from a job offer, benefit award or guaranteed placement. | **Ready** |
| `BG-EMP-02` | The employee work-permit exemption is limited to effective TP; self-employment and employer reporting remain separate. The 2026 floor is stated as gross pay (€620.20 monthly / €3.74 hourly), effective 1 Jan 2026, and not as net pay. | **Ready** |
| `BG-HEALTH-01` | TP insurance category, who pays contributions, active-insurance proof and covered care remain separate. The corrected card URL is used only for TP/card evidence, not current contribution status. Copayments and the emergency versus planned/follow-up distinction remain. | **Ready** |
| `BG-HOUSING-01` | Program 3 is bounded through 4 Mar 2027; individual assessment and capacity remain explicit, with no promised place or town. Provider reimbursement is not direct rent cash, and no current 2026 reimbursement amount is stated. Private-rental limits remain qualified. | **Ready** |
| `BG-EDU-01` | School access is not a chosen-school or seat guarantee; placement and records are assessed. Language support is not a guaranteed course/hours, Ukrainian online study is not an automatic attendance substitute, and recognition, admission, credit transfer and fees remain separate. | **Ready** |
| `BG-SOC-01` | Assistance remains application- and means-assessment-based. 2026 reference amounts are distinguished from a flat monthly award; the 2026/27 heating application deadline is stated as closed on 31 Oct 2026. Family-benefit ambiguity, NSSI insurance benefits and disability assessment remain separate. | **Ready** |

**Article results:** Ready **8/8**; Minor correction **0**; Correction required **0**.

## 3. Comparison facts review

All 24 Bulgaria aspects are present once, linked to the article for their topic, `Draft`, `IsAvailable=true`, and populated in EN/UK/RU. Fact answer text and status were not changed in Phase 21.13; the EN/UK/RU values remain materially equivalent. Source-array changes remove the unrelated source for each prior minor finding while retaining evidence relevant to the concise claim. Every source ID resolves to the linked article’s source owner.

| Aspect | Linked article | Claim-specific source review | Result |
| --- | --- | --- | --- |
| `protection-scheme` | BG-TP-01 | EU temporary-protection decision and Bulgarian overview support the distinct EU and national routes; no entitlement from nationality or departure alone. | **Ready** |
| `protection-application` | BG-TP-01 | Direct 2026 EU decision supports the conditional military-obligation rule and continuity exception; no unverified Bulgarian proof checklist is implied. | **Ready** |
| `protection-dates` | BG-TP-01 | Bulgarian 2026 extension notice and direct EU 2026 decision each support their respective end date; no individual card-validity claim. | **Ready** |
| `residence-routes` | BG-TP-02 | Foreigners Act and Asylum and Refugees Act support purpose-based ordinary routes and the distinction from TP. | **Ready** |
| `residence-conditions` | BG-TP-02 | Single-permit statute and Migration Directorate materials support limited filing conditions and the stated uncertainty for TP-card qualification. | **Ready** |
| `residence-filing` | BG-TP-02 | Bulgarian Asylum and Refugees Act, Foreigners Act and Labour Migration Act support the TP stay/work and ordinary-route boundary; Directive 2003/109 supports only the EU long-term-resident exclusion. No source is used to claim an absolute prohibition. | **Ready** |
| `employment-vacancies` | BG-EMP-01 | Employment Agency vacancy service and EURES support vacancy search; neither implies referral or an offer. | **Ready** |
| `employment-institutions` | BG-EMP-01 | Employment law, amendments and jobseeker registration service support registration routes and assessment boundaries. | **Ready** |
| `employment-assistance-conditions` | BG-EMP-01 | Employment law, registration evidence and the refugee employment programme support conditional mediation, counselling and referrals. No NSSI benefit or qualification source remains. | **Ready** |
| `work-permission` | BG-EMP-02 | Asylum and Refugees Act, Labour Migration Act and EU Temporary Protection Directive support the employee permit-exemption boundary during effective TP. | **Ready** |
| `work-employer-duties` | BG-EMP-02 | TP card guidance, labour inspection, NRA register guidance and Labour Code support the distinct employee documents, notices and registration duties. | **Ready** |
| `work-minimum-pay` | BG-EMP-02 | 2026 minimum-wage decree supports the dated gross monthly/hourly figures and qualification that these are not take-home amounts. | **Ready** |
| `healthcare-access` | BG-HEALTH-01 | Health Insurance Act and Decree No. 69 support the TP insurance category and distinct contribution-payment rules. | **Ready** |
| `healthcare-proof` | BG-HEALTH-01 | Corrected AREF card source establishes status/card evidence; NRA status lookup and NHIF patient guidance support the separate contribution and service checks. | **Ready** |
| `healthcare-urgent` | BG-HEALTH-01 | Health Act and Ministry 112 guidance support emergency-care routing without extending it to all planned or follow-up care. | **Ready** |
| `housing-initial` | BG-HOUSING-01 | Asylum and Refugees Act supports necessary accommodation rights; Program 3 decision and provider-period sources support current programme/provider boundaries. No Program 2 closure source remains. | **Ready** |
| `housing-private-rental` | BG-HOUSING-01 | Obligations and Contracts Act supports the ordinary private-lease boundary; no universal deposit cap is claimed. | **Ready** |
| `housing-financial-support` | BG-HOUSING-01 | Program 3 amendment and provider-period sources support provider reimbursement and the limitation that the located rate is not confirmed current. No general-assistance source remains. | **Ready** |
| `education-school-enrolment` | BG-EDU-01 | School law, government guidance, RDE guidance and implementing ordinance support access, process and placement boundaries; no chosen-school or automatic-placement promise. | **Ready** |
| `education-pupil-language` | BG-EDU-01 | Bulgarian-language ordinance and MES pupil-support guidance support needs-based support; maxima are not represented as guaranteed course hours or an exemption. | **Ready** |
| `education-higher-admission` | BG-EDU-01 | Higher Education Act, state-university route and place allocation, and NACID recognition materials support distinct admission/recognition/credit processes without universal admission or fee claims. | **Ready** |
| `social-subsistence` | BG-SOC-01 | Social Assistance Act and 2026 assistance guidance support application and means-assessment wording. Heating notice is not included in this fact. | **Ready** |
| `social-application` | BG-SOC-01 | ASP guidance and application-channel source support address-based, benefit-specific process; no universal checklist is claimed. | **Ready** |
| `social-child-benefits` | BG-SOC-01 | Family-benefit law and guidance, relevant court material and NSSI maternity/childcare evidence support the separate, qualified routes. No disability-law source remains. | **Ready** |

**Fact results:** Ready **24/24**; Minor correction **0**; Correction required **0**.

## 4. Source audit

| Check | Result |
| --- | --- |
| Bulgaria `OfficialSources` | **65** across eight articles |
| Article-owned UUIDs | **65/65**; each UUID belongs to exactly one Bulgaria article |
| Cross-article UUID conflicts | **0** |
| Source URLs | **65/65 HTTPS** |
| Inline citation tokens | All resolve to an existing source owned by the citing article; all 65 source-set IDs are cited in the EN/UK/RU content of their respective articles |
| Comparison fact source IDs | All resolve and belong to each fact’s linked article; no dangling IDs |
| BG-HEALTH-01-S09 / BG-HEALTH-S15 | Catalog URL exactly matches the scoped healthcare research inventory |
| BG-HOUSING-01-S01 / BG-HOUSING-S01 | Catalog URL exactly matches the scoped housing research inventory |
| Draft/catalog text parity | All eight articles match for EN/UK/RU titles, summaries and content |

## 5. State invariants

The current catalog and the existing `BulgariaContentIntegrationTests` assertions agree with the prior audit’s integration baseline:

| Measure | Current state | Baseline state | Result |
| --- | ---: | ---: | --- |
| Bulgaria articles | 8 | 8 | Unchanged |
| Bulgaria translations | 24 `NeedsReview` | 24 `NeedsReview` | Unchanged |
| Bulgaria OfficialSources | 65 | 65 | Unchanged |
| Bulgaria comparison facts | 24 `Draft` | 24 `Draft` | Unchanged |
| Bulgaria available facts | 24 | 24 | Unchanged |
| Bulgaria visibility | Development-visible; Production-hidden | Development-visible; Production-hidden | Unchanged |
| Global articles | 80 | 80 | Unchanged |
| Global translations | 240: 213 `Verified`, 27 `NeedsReview`, 0 `Outdated` | Same | Unchanged |
| Global OfficialSources | 666 | 666 | Unchanged |
| Global comparison facts | 240: 211 `Approved`, 29 `Draft`, 233 available | Same | Unchanged |

No tests or application suites were rerun for this audit.

## 6. Final findings

No material editorial or source defects remain within the reviewed scope. Existing research qualifications remain visible; their presence is not treated as a defect or a guarantee of an individual outcome.

| Item | Issue | Required correction | Severity |
| --- | --- | --- | --- |
| None | No unresolved blocker or minor source/editorial defect found. | None | — |

**Blockers: 0. Minor corrections: 0.**

## 7. Owner decision boundary

**READY FOR OWNER ACCEPTANCE**

This is a review recommendation only. The owner must make the acceptance decision; no translation or fact status was promoted, and no deployment is authorized by this report.
