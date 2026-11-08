# Austria v1 Editorial Acceptance Preparation

**Baseline:** `7df96388285cd2a1a254b6a0d74b8c1b21ad9a1b`
**Review date:** 2026-11-08
**Scope:** Eight Austria drafts, their eight focused research packages and readiness summary, Austria records in the four permitted catalogs, `CountryComparisonTopicMap.cs`, and the Austria integration-test assertions. No new web research was performed.
**Overall verdict:** **NEEDS CORRECTION BEFORE HUMAN ACCEPTANCE**

This report is an independent editorial/source audit. It is not owner acceptance and makes no change to draft or catalog state.

## 1. Batch snapshot

The Austria country and content records were counted directly in the permitted catalogs. Visibility was checked against the integration test's `CountryVisibility` assertions; topic mappings were checked in the map and test.

| Austria measure | Audited state |
| --- | ---: |
| Country records | 1 (`austria`) |
| Articles | 8 |
| EN / UK / RU translations | 24; all `NeedsReview` |
| Article-owned `OfficialSources` | 56; all HTTPS |
| Comparison facts | 24; all `Draft`, all `IsAvailable=true` |
| Country comparison aspect mappings | 24/24 |
| Development visibility | Visible; 11 countries |
| Production visibility | Hidden; 10 countries |

| Global catalog measure | Audited state |
| --- | ---: |
| Countries | 11 |
| Articles | 88 |
| Translations | 264: 237 `Verified`, 27 `NeedsReview`, 0 `Outdated` |
| `OfficialSources` | 722 |
| Comparison facts | 264: 235 `Approved`, 29 `Draft`; 257 available, 7 unavailable |

The research readiness summary records 8/8 topics as `Ready with qualification` and all 24 aspects as `Ready with qualification`. These are research-drafting assessments, not content acceptance.

## 2. Article acceptance matrix

Draft IDs, categories, EN/UK/RU titles, summaries and content were compared with the corresponding catalog translation records. Text matched in all 24 translations after whitespace normalization. Draft citation tokens resolve to the source set for the same article; catalog records retain the same article ownership. Each article's listed comparison aspects match its mapped facts.

| Draft | Article ID | Category | Sources | Aspects | Text / translation parity | Editorial result |
| --- | --- | --- | ---: | --- | --- | --- |
| `AT-TP-01` | `cae7f1a1-51c1-5b6c-a01d-b5bea8fb9cd2` | `temporary-protection` | 6 | `protection-scheme`, `protection-application`, `protection-dates` | Exact catalog parity; EN/UK/RU qualifications equivalent | Minor correction |
| `AT-TP-02` | `dc76be21-d9dc-5070-83ca-9a7468d78e71` | `temporary-protection` | 7 | `residence-routes`, `residence-conditions`, `residence-filing` | Exact catalog parity; EN/UK/RU route and waiver distinctions equivalent | Correction required |
| `AT-EMP-01` | `7f03cea4-6c2e-5990-91eb-01b4933ba53d` | `employment` | 6 | `employment-vacancies`, `employment-institutions`, `employment-assistance-conditions` | Exact catalog parity; AMS registration, benefit and service qualifications equivalent | Ready |
| `AT-EMP-02` | `a1ac34e6-0c0e-5bb2-870c-9d93aee852c2` | `employment` | 4 | `work-permission`, `work-employer-duties`, `work-minimum-pay` | Exact catalog parity; card condition, employer duties and wage caveats equivalent | Ready |
| `AT-HEALTH-01` | `44e8aebe-fd7e-5ed8-acb2-f949d6243e26` | `healthcare` | 5 | `healthcare-access`, `healthcare-proof`, `healthcare-urgent` | Exact catalog parity; end date, insurance proof and emergency-care limits equivalent | Ready |
| `AT-HOUSING-01` | `b7ae3606-2d3b-5ddc-b178-f1905d72996e` | `housing` | 8 | `housing-initial`, `housing-private-rental`, `housing-financial-support` | Exact catalog parity; needs test, provincial variation and capacity limits equivalent | Ready |
| `AT-EDU-01` | `28c9cf8a-012f-5018-b9e0-48c8fcc4f8d5` | `education` | 11 | `education-school-enrolment`, `education-pupil-language`, `education-higher-admission` | Exact catalog parity; all school, support, admission and fee qualifications equivalent | Ready |
| `AT-SOC-01` | `fc9332d0-9eda-5f09-b3ca-df3c9e63f755` | `social-assistance` | 9 | `social-subsistence`, `social-application`, `social-child-benefits` | Exact catalog parity; Grundversorgung, 2026 family-benefit and disability limits equivalent | Ready |
| **Total** | **8 unique IDs** |  | **56** | **24 unique aspects** | **24/24 exact catalog parity** | **Ready 6; Minor 1; Correction required 1** |

## 3. Cross-language review

EN/UK/RU were checked against each other and the corresponding research boundaries. No material translation was found that increases a right or guarantee, removes a material qualification, changes a date/amount/period/condition, turns “not established” into a prohibition, or promises a result, placement, payment, permission or coverage.

| Review area | EN / UK / RU result |
| --- | --- |
| Temporary protection | Equivalent: right and card are distinct; 4 March 2028 is not represented as an individual's card validity; 30 July / 4 August / 5 August 2026 transition conflict remains unresolved. |
| Residence | Equivalent: special §41a(7b) RWR+ is distinguished from §4a routes; waiver, stay bridge and work authorization are not conflated; no automatic conversion is claimed. |
| Employment | Equivalent: cardholder work exemption is limited; police registration alone is not established as sufficient; no universal statutory national minimum wage is asserted. |
| Healthcare | Equivalent: Ukraine-specific automatic insurance ended 31 May 2025; TP/card is not proof of active insurance; emergency access is not described as universally free downstream care. |
| Housing | Equivalent: Grundversorgung is need-based and provincial; no bed or beneficiary payment is guaranteed; federal cost ceilings and Wohnbeihilfe are qualified. |
| Education | Equivalent: Ukrainian online schooling is not an automatic replacement for Schulpflicht; no chosen school, language hours, universal tuition waiver or expired scholarship is promised. |
| Social assistance | Equivalent: Grundversorgung remains distinct from ordinary Sozialhilfe; 1 July 2026 Familienbeihilfe rule, Grundversorgung interaction, narrow disability exception and removal of the former employment/AMS condition are retained. |

**Translation parity issues:** 0.

## 4. Article-specific findings

| Draft / article | Claim-support review | Result |
| --- | --- | --- |
| `AT-TP-01` | The eligibility groups, registration route, current 4 March 2028 scheme endpoint, card distinction and military-date conflict follow the research package without resolving the conflict or promising a checklist or timing. In the second paragraph, the 2026 amendment citation is attached to registration/document-checklist claims that it does not substantiate; the BMI registration source supports those claims, and the amendment is appropriately cited in the following military-rule paragraph. See finding C-02. | Minor correction |
| `AT-TP-02` | Special RWR+ conditions and its no-waiver rule are separated from other §4a titles; the conditional waiver, qualified stay bridge and lack of work authorization are preserved. The opening paragraph cites only NAG §21 filing rules for both the continuing temporary-protection right and no-automatic-conversion statement. The cited filing rule establishes filing/stay consequences, not the legal basis for the temporary right or sufficiently direct evidence for the no-conversion claim. Add an appropriate temporary-protection / route source at those claims. See finding C-01. | Correction required |
| `AT-EMP-01` | Public vacancy browsing, AMS jobseeker registration, counselling/training, ÖIF services and insurance-based unemployment benefit remain separate. The 52-in-24-month statement is qualified as usual and subject to exceptions; no automatic Ukrainian insurance credit is claimed. | Ready |
| `AT-EMP-02` | Dependent-employment exemption is tied to the covered cardholder; police registration alone is not established; self-employment and regulated-profession requirements remain separate. ÖGK registration and no-universal-minimum-wage boundaries match research. | Ready |
| `AT-HEALTH-01` | The 31 May 2025 end of automatic Ukraine-specific insurance, alternative insurance routes, proof/current-entitlement distinction and emergency versus subsequent care are preserved. | Ready |
| `AT-HOUSING-01` | Needs-based provincial Grundversorgung, no guaranteed bed, limited first-arrival capacity, ordinary lease/MRG boundaries, Meldezettel separation and provincial financial support are stated with the required caveats. No federal ceiling is treated as an individual award. | Ready |
| `AT-EDU-01` | Nine-year schoolpflicht boundary and age start, non-substitution by Ukrainian online schooling, local placement, German support limits, provider-level recognition/admission/credit decisions, semester-limited tuition waiver and ended scholarship are preserved. | Ready |
| `AT-SOC-01` | Basic care is not treated as ordinary Sozialhilfe or guaranteed cash. Income reporting, the current family-benefit rule from 1 July 2026, Grundversorgung interaction, narrow disability exception and separate insurance benefits match the research boundaries. | Ready |

## 5. Comparison acceptance matrix

Each of the 24 Austria facts is linked to the article assigned by `CountryComparisonTopicMap.cs`, is `Draft` and available, has non-empty EN/UK/RU values, and lists only source IDs owned by that article. The three language values for each fact express the same qualified answer. Each source list was reviewed for relevance to the statement; no unrelated source or missing material evidence source was identified.

| Aspect ID | Linked article | State | Source relevance / language result | Result |
| --- | --- | --- | --- | --- |
| `protection-scheme` | AT-TP-01 | Draft / available | Austrian covered-group source and EU decision; equivalent | Ready |
| `protection-application` | AT-TP-01 | Draft / available | BMI registration guidance; equivalent | Ready |
| `protection-dates` | AT-TP-01 | Draft / available | Austrian amendment, BFA extension, EU decision and corrigendum; unresolved date conflict retained | Ready |
| `residence-routes` | AT-TP-02 | Draft / available | NAG §41a, §4a rule and amendment; special/general route distinction retained | Ready |
| `residence-conditions` | AT-TP-02 | Draft / available | Special-route rule, §4a rule and AMS condition; equivalent | Ready |
| `residence-filing` | AT-TP-02 | Draft / available | NAG §21 and §4a rule support timely filing/stay distinction; no work right claimed | Ready |
| `employment-vacancies` | AT-EMP-01 | Draft / available | Public AMS searches; search, registration and work authorization separated | Ready |
| `employment-institutions` | AT-EMP-01 | Draft / available | AMS cardholder registration guidance; police-only uncertainty retained | Ready |
| `employment-assistance-conditions` | AT-EMP-01 | Draft / available | AMS service/benefit and ÖIF sources relevant; assessment and availability limits retained | Ready |
| `work-permission` | AT-EMP-02 | Draft / available | AuslBG cardholder exemption; dependent employment and change-of-employer boundary retained | Ready |
| `work-employer-duties` | AT-EMP-02 | Draft / available | ÖGK employer registration source; limited to insurable employees | Ready |
| `work-minimum-pay` | AT-EMP-02 | Draft / available | Government minimum-pay framework; no universal statutory national wage asserted | Ready |
| `healthcare-access` | AT-HEALTH-01 | Draft / available | ÖGK end-date and BMI alternative-route sources; equivalent | Ready |
| `healthcare-proof` | AT-HEALTH-01 | Draft / available | Official e-card service; card/e-card distinguished from entitlement | Ready |
| `healthcare-urgent` | AT-HEALTH-01 | Draft / available | RIS emergency-admission rule and official emergency-number page; downstream care qualification retained | Ready |
| `housing-initial` | AT-HOUSING-01 | Draft / available | GVS agreement and BBU application/current-arrival guidance; no capacity promise | Ready |
| `housing-private-rental` | AT-HOUSING-01 | Draft / available | MRG scope, fixed-term rule and government deposit guidance; qualified | Ready |
| `housing-financial-support` | AT-HOUSING-01 | Draft / available | GVS cost framework and provincial allowance guidance; ceiling/payment distinction retained | Ready |
| `education-school-enrolment` | AT-EDU-01 | Draft / available | Schulpflicht law, allocation guidance and remote-schooling decision; language result equivalent | Ready |
| `education-pupil-language` | AT-EDU-01 | Draft / available | SchOG §8h and 2026/27 model; hours/course not guaranteed | Ready |
| `education-higher-admission` | AT-EDU-01 | Draft / available | Credential, admission, provider, fee and scholarship sources; limits retained | Ready |
| `social-subsistence` | AT-SOC-01 | Draft / available | SH-GG, GVS, BBU and AMS sources each support their corresponding distinction | Ready |
| `social-application` | AT-SOC-01 | Draft / available | BBU Land route, need and income-reporting guidance; equivalent | Ready |
| `social-child-benefits` | AT-SOC-01 | Draft / available | Current law, BKA guidance and separate KBG/Kinderabsetzbetrag/Wochengeld sources; narrow exception retained | Ready |
| **Total** | **8 articles** | **24 Draft / available** | **24/24 equivalent; source lists relevant and article-owned** | **Ready 24; Minor 0; Correction required 0** |

## 6. Source audit

| Draft | Editorial source entries | Ownership / UUID | HTTPS and research traceability | Inline citations / use |
| --- | ---: | --- | --- | --- |
| `AT-TP-01` | 6 | Correct article; unique | HTTPS; URLs match the intended TP inventory records | All tokens resolve; all entries used |
| `AT-TP-02` | 7 | Correct article; unique | HTTPS; URLs match the intended residence inventory records | All tokens resolve; all entries used |
| `AT-EMP-01` | 6 | Correct article; unique | HTTPS; URLs match the intended finding-a-job inventory records | All tokens resolve; all entries used |
| `AT-EMP-02` | 4 | Correct article; unique | HTTPS; URLs match the intended work-rights inventory records | All tokens resolve; all entries used |
| `AT-HEALTH-01` | 5 | Correct article; unique | HTTPS; URLs match the intended healthcare inventory records | All tokens resolve; all entries used |
| `AT-HOUSING-01` | 8 | Correct article; unique | HTTPS; URLs match the intended housing inventory records | All tokens resolve; all entries used |
| `AT-EDU-01` | 11 | Correct article; unique | HTTPS; URLs match the intended education inventory records | All tokens resolve; all entries used |
| `AT-SOC-01` | 9 | Correct article; unique | HTTPS; URLs match the intended social-assistance inventory records | All tokens resolve; all entries used |
| **Total** | **56** | **56 unique UUIDs; 0 cross-article conflicts** | **56 HTTPS URLs; 0 material source/version swaps identified** | **All inline tokens resolve; 0 unused entries** |

All comparison-fact sources resolve to the linked article's owned sources; no dangling source IDs were found. Reuse of a physical URL across different Austria articles is represented by different article-owned source UUIDs. No duplicate URL within one article was found. The two citation-placement/source-boundary findings below do not indicate a catalog URL mismatch or UUID ownership defect.

## 7. Cross-article consistency

| Boundary | Result |
| --- | --- |
| TP legal right versus `Ausweis für Vertriebene` | Consistent: right and card are distinct; card-specific processes are not stated as the source of the underlying right. |
| Residence filing versus work authorization | Consistent: §4a's conditional lawful-stay bridge is not described as work authorization; special RWR+ has separate rules. |
| AMS registration versus authorization to work | Consistent: vacancy search/jobseeker registration does not establish a work right; cardholder work exemption is covered separately. |
| Employment versus ÖGK coverage | Consistent: employer registration is addressed for insurable employees and does not reverse the ended blanket Ukraine-specific coverage. |
| Grundversorgung across healthcare, housing and social assistance | Consistent: eligibility is conditional/needs-based; health, accommodation and cash components are not inferred automatically from one another. |
| Family benefits | Consistent: current 1 July 2026 rule and Grundversorgung interaction are retained in the social article; no other article asserts a conflicting entitlement. |
| TP duration | Consistent: 4 March 2028 is the scheme endpoint, not a promise about an individual's printed card validity; the military-transition date conflict remains unresolved. |

## 8. Existing holds / regression check

The current permitted integration-test assertions confirm the existing holds remain represented: Netherlands has 3 `NeedsReview` translations and 4 `Draft` comparison facts; Sweden `social-child-benefits` remains `Draft`, unavailable and source-less. No hold was changed by the audited integration baseline.

## 9. Exact correction list

| ID | File / article / aspect | Exact issue | Research boundary | Required correction | Severity |
| --- | --- | --- | --- | --- | --- |
| C-01 | `AT-TP-02.md`, opening paragraph; residence overview | The sole inline citation is NAG §21 filing rules, but the paragraph also states the basis/continuance of the Austrian temporary right and that it does not automatically convert to another title. The cited rule is direct evidence for filing consequences and the lack of a filing-based stay extension, not a sufficient direct citation for both preceding status/conversion claims. | Residence research distinguishes existing TP status, application-based NAG routes and filing/stay consequences; the temporary-protection legal basis is established in the TP package. | Add the appropriate current temporary-protection and route evidence to the opening claims; keep the NAG §21 citation on filing consequences. No wording change is required if the citations are corrected and the claim remains as qualified. | correction |
| C-02 | `AT-TP-01.md`, second paragraph; `protection-application` | The BGBl II 304/2026 amendment is cited after a paragraph about registration points, document availability, checklist, appointment and processing-time limits. The amendment source concerns the 2026 military-condition change and does not establish those administrative-process claims. It is separately and appropriately cited in the following military paragraph. | TP research identifies BMI/BFA registration sources for the process and treats the amendment as the military-rule legal source. | Remove the amendment citation from the registration paragraph; retain the BMI/BFA process citation and keep the amendment citation with the military-rule discussion. | minor |

No other material article, translation, fact, source-ownership, source-relevance, state or cross-article defect was identified in the permitted scope.

**Findings:** blockers 0; corrections 1; minor corrections 1.
**Article results:** Ready 6/8; Minor correction 1/8; Correction required 1/8.
**Fact results:** Ready 24/24; Minor correction 0/24; Correction required 0/24.
**Source ownership/linkage defects:** 0.
**Translation parity issues:** 0.
**Cross-article conflicts:** 0.

## 10. Final recommendation

**NEEDS CORRECTION BEFORE HUMAN ACCEPTANCE.** Resolve C-01 and C-02, then have the owner review the corrected material. This audit does not make corrections or perform owner acceptance.
