# Bulgaria v1 Editorial Acceptance Preparation

**Baseline:** `c4329506c9180e056d46d81dc32ee75158da83d4`
**Review date:** 2026-11-07
**Scope:** Bulgaria v1 drafts, aggregated research readiness, Bulgaria-owned catalog records, comparison facts and integration-test assertions. No new web research was performed.
**Overall verdict:** **NEEDS CORRECTION BEFORE HUMAN ACCEPTANCE**

This is an audit record. It is not owner acceptance and does not change editorial or catalog state.

## 1. Batch snapshot

| Item | Verified baseline state |
| --- | --- |
| Bulgaria articles | 8 |
| EN / UK / RU translations | 24; all `NeedsReview` |
| Bulgaria comparison facts | 24; all `Draft`, all `IsAvailable=true` |
| Bulgaria-owned `OfficialSources` | 65 across eight articles; all HTTPS |
| Development visibility | Visible; 10 countries |
| Production visibility | Hidden; 9 countries |
| Research readiness | 8/8 topics `Ready with qualification`; 24/24 aspects (2 `Ready`, 22 `Ready with qualification`) |

| Global catalog measure | Verified state |
| --- | ---: |
| Countries | 10 |
| Articles | 80 |
| Translations | 240: 213 `Verified`, 27 `NeedsReview`, 0 `Outdated` |
| OfficialSources | 666 |
| Comparison facts | 240: 211 `Approved`, 29 `Draft`, 233 available |

Bulgaria contributes 24 of the 27 `NeedsReview` translations, 65 sources and 24 available `Draft` facts. The 102 source entries in the research readiness inventory are research-package inventory rows, not a required number of catalog sources.

## 2. Article acceptance matrix

Catalog records were compared with draft article IDs, categories, language sections, titles, summaries, content and source sets. The integration test asserts exact draft-to-catalog text parity and the catalog counts shown above. Each draft has EN/UK/RU; all 24 catalog translations are `NeedsReview`. Every draft source-set UUID is owned by that article, all 65 are unique across Bulgaria articles, and every inline citation resolves to an owned source.

| Draft | Article ID | Category | EN / UK / RU | Owned sources | Comparison aspects | Key boundary retained | Editorial result |
| --- | --- | --- | --- | ---: | --- | --- | --- |
| `BG-TP-01` | `4a1e4c9b-a09c-5c92-bc37-2c9a15b459bc` | `temporary-protection` | Present; catalog matches draft | 5 | `protection-scheme`, `protection-application`, `protection-dates` | EU categories and Bulgarian national route stay separate; no complete current initial checklist, local military-proof process, individual card validity or processing-time promise. | Ready |
| `BG-TP-02` | `c276de77-a6b9-5ff7-bbc7-685349b1009a` | `temporary-protection` | Present; catalog matches draft | 5 | `residence-routes`, `residence-conditions`, `residence-filing` | No automatic conversion; TP-card in-country single-permit eligibility and national TP-time credit remain unresolved; pending application is not stated as a general stay/work bridge; Blue Card TP exclusion retained. | Correction required |
| `BG-EMP-01` | `d210414c-a502-59ef-b10e-1ae642b50207` | `employment` | Present; catalog matches draft | 8 | `employment-vacancies`, `employment-institutions`, `employment-assistance-conditions` | Browsing is not registration or a job offer; jobseeker registration, programme intake, benefits and recognition are distinct and conditional. | Ready |
| `BG-EMP-02` | `26a17913-a1f2-5710-b11b-df406dab09cc` | `employment` | Present; catalog matches draft | 9 | `work-permission`, `work-employer-duties`, `work-minimum-pay` | Employee permit exemption applies only while TP is effective; self-employment is separate; employer notices and register deadlines are distinguished; 2026 wage figures are gross and dated. | Ready |
| `BG-HEALTH-01` | `47c384e4-2d8f-50bb-af6a-356d49a4c089` | `healthcare` | Present; catalog matches draft | 9 | `healthcare-access`, `healthcare-proof`, `healthcare-urgent` | TP insurance category does not mean contributions are paid for every adult throughout TP; card is not proof of active contributions; emergency care is distinct from all other services. | Minor correction |
| `BG-HOUSING-01` | `f763854d-dee2-5cb3-aacb-6c01203b52fc` | `housing` | Present; catalog matches draft | 6 | `housing-initial`, `housing-private-rental`, `housing-financial-support` | Program 3 is through 4 Mar 2027; no place or town is guaranteed; provider reimbursement is not cash rent; no current 2026 rate is asserted. | Minor correction |
| `BG-EDU-01` | `db6f961d-e976-52c1-8ef3-d826ee46b3fc` | `education` | Present; catalog matches draft | 12 | `education-school-enrolment`, `education-pupil-language`, `education-higher-admission` | School preference is not a seat guarantee; age/placement is assessed; Ukrainian online study is not an automatic substitute; language maxima are not guaranteed hours; NACID recognition, admission, credit transfer and the limited state route stay distinct. | Ready |
| `BG-SOC-01` | `7fede68e-bccb-53f8-9648-984d7ac6fb68` | `social-assistance` | Present; catalog matches draft | 11 | `social-subsistence`, `social-application`, `social-child-benefits` | Means test and individual award retained; heating application deadline is explicitly closed; family-benefit ambiguity, NSSI insurance benefits and disability assessment remain separate. | Ready |

### Cross-language review

EN/UK/RU were checked for the eligibility limits, dates, amounts, deadlines and material caveats listed in the request. No material translation strengthens an entitlement or drops a caveat.

| Review area | Parity result |
| --- | --- |
| Temporary protection | Equivalent: 4 Mar 2027 Bulgarian notice versus 4 Mar 2028 EU scheme; the 2026 military-obligation qualification and same-Member-State continuous-protection exception are present in all three languages. No universal initial checklist is claimed. |
| Residence | Equivalent: no automatic conversion; Blue Card exclusion; no general pending-application stay/work bridge; single-permit uncertainty and TP-time counting uncertainty retained. |
| Employment | Equivalent: TP work only while effective; self-employment/business formalities separate; employer duties and 2026-01-01 effective date retained; €620.20 monthly / €3.74 hourly gross figures match. |
| Healthcare | Equivalent: compulsory-insurance category is not universal state-paid contributions; active-insurance distinction, copayments, limited medicines/dental coverage, and emergency versus planned/follow-up care remain. |
| Housing | Equivalent: Program 3 end date, no placement guarantee, provider reimbursement versus beneficiary rent cash, and no established current euro rate are retained. |
| Education | Equivalent: access is not a chosen-school guarantee; online Ukrainian study is not an automatic substitute; support is not guaranteed; diploma recognition, admission, credits, tuition and language requirements remain separate. |
| Social assistance | Equivalent: application is not award; means assessment, 2026 amounts and closed 31 Oct 2026 heating application window match; family-law ambiguity, NSSI and disability routes remain separate. |

### Article-specific findings

| Draft | Substantive review and research boundary | Severity |
| --- | --- | --- |
| `BG-TP-01` | Eligibility and military rule remain qualified. The 2022 AREF procedure is labelled as initial-application information; the article advises confirming intake location and does not reuse the re-registration list as an initial checklist. Dates do not imply individual card validity. | None |
| `BG-TP-02` | The pending ordinary-application paragraph says it does not generally extend TP, create a stay bridge or authorize work, but its only inline source is the EU long-term-resident directive. That directive supports the TP exclusion from EU long-term-resident status, not the Bulgarian stay/work conclusions. The research package identifies Bulgarian Asylum and Refugees Act Arts. 39 and 82 for the stay/termination boundary and separately says a pending application itself establishes no work right. Add the appropriate Bulgarian legal citations at those claims and retain the EU directive citation only for the EU long-term-residence clause. | **blocker** |
| `BG-EMP-01` | Vacancy search, address-based registration, 14-day readiness condition, conditional services and NSSI separation match the research boundaries. No placement or benefit award is promised. | None |
| `BG-EMP-02` | Work right ends with effective TP; the separate employer notices, NRA register timing and premature-termination rule are not conflated. The minimum wage is correctly described as gross, dated and not take-home pay. | None |
| `BG-HEALTH-01` | Insurance, contribution payer, card proof, cost sharing, listed medicines/dental and emergency-care boundaries are preserved. The AREF card source record URL differs from the URL listed for that source in the corresponding research inventory; equivalence is not established within this audit scope. | minor |
| `BG-HOUSING-01` | Program 3 date, individual assessment/capacity boundary, Program 2 closure, private lease caveats and provider-side reimbursement are preserved without a current unsupported euro amount. The Asylum and Refugees Act catalog URL differs from the research inventory URL; exact resource/version equivalence is not established within this audit scope. | minor |
| `BG-EDU-01` | Public-school access, compulsory age boundaries, RDE capacity and placement, records, language-support maxima, online-study boundary and specific state-university route are presented without chosen-school, automatic-grade, universal-language or tuition-waiver promises. NACID recognition is not presented as admission or credit transfer. | None |
| `BG-SOC-01` | Means testing, application versus award, non-flat 2026 calculation values, closed heating window, statutory/case-law ambiguity for family benefits, separate NSSI benefits and separate disability assessment are retained. | None |

## 3. Comparison acceptance matrix

All 24 expected Bulgaria aspect IDs appear exactly once. Each fact is `Draft`, `IsAvailable=true`, linked to the article mapped for its topic, has non-empty EN/UK/RU values, and uses only sources owned by that linked Bulgaria article. The values are concise and materially equivalent across languages. “Qualification” records the limit that must remain visible; `Minor correction` flags source-list precision or URL traceability, not a change to the answer.

| Aspect ID | Linked article | State | Sources | Concise answer | Qualification | EN / UK / RU | Result |
| --- | --- | --- | ---: | --- | --- | --- | --- |
| `protection-scheme` | BG-TP-01 | Draft / available | 2 | EU groups and a separate Bulgarian national route. | Nationality or departure alone does not establish eligibility. | Equivalent | Ready |
| `protection-application` | BG-TP-01 | Draft / available | 1 | EU 2026 military-obligation rule applies “where applicable,” with a continuity exception. | No Bulgarian proof checklist or screening process established; ask AREF about the individual case. | Equivalent | Ready |
| `protection-dates` | BG-TP-01 | Draft / available | 2 | Bulgarian notice through 4 Mar 2027; EU scheme through 4 Mar 2028. | Neither date establishes an individual card's validity or later renewal process; one listed source is a 2022 procedural source rather than direct date evidence. | Equivalent | Minor correction |
| `residence-routes` | BG-TP-02 | Draft / available | 2 | Ordinary purpose-based residence routes are separate from TP; some normally require visa D. | No automatic conversion or universal route; route conditions differ. | Equivalent | Ready |
| `residence-conditions` | BG-TP-02 | Draft / available | 2 | In-country single-permit filing is limited; TP-card qualification is unconfirmed. | Confirm with Migration Directorate; Blue Card is not a TP route. | Equivalent | Ready |
| `residence-filing` | BG-TP-02 | Draft / available | 1 | A pending application is not a general TP stay/work bridge; EU long-term-resident scope excludes current TP beneficiaries. | Source list cites only the EU long-term-resident directive, not the Bulgarian stay/work rules stated in the same fact. | Equivalent | **Correction required** |
| `employment-vacancies` | BG-EMP-01 | Draft / available | 2 | Use the Employment Agency e-Labour Exchange and EURES to search vacancies. | Listings can change, omit jobs or already be filled; no referral or offer is promised. | Equivalent | Ready |
| `employment-institutions` | BG-EMP-01 | Draft / available | 3 | A TP holder may apply to register through the Labour Bureau serving their address/settlement. | Registration is not automatic and evidence/group are assessed. | Equivalent | Ready |
| `employment-assistance-conditions` | BG-EMP-01 | Draft / available | 3 | Mediation, counselling and referrals depend on assessment and available programmes. | Source list includes NSSI cash-benefit and qualification sources that do not support this comparison answer. | Equivalent | Minor correction |
| `work-permission` | BG-EMP-02 | Draft / available | 3 | Employee work is permit-exempt while Bulgarian TP is effective. | Does not establish post-TP work permission or business formalities. | Equivalent | Ready |
| `work-employer-duties` | BG-EMP-02 | Draft / available | 5 | Permit exemption does not remove employer notification, contract and register duties. | Status evidence and separate reporting deadlines remain applicable. | Equivalent | Ready |
| `work-minimum-pay` | BG-EMP-02 | Draft / available | 1 | 2026 gross floor: €620.20/month or €3.74/hour. | Effective 1 Jan 2026; full-time basis; not net pay and higher applicable occupation/agreement floors may apply. | Equivalent | Ready |
| `healthcare-access` | BG-HEALTH-01 | Draft / available | 2 | TP is within the compulsory-insurance category, subject to contribution rules. | This does not establish that state contributions cover every adult for all TP. | Equivalent | Ready |
| `healthcare-proof` | BG-HEALTH-01 | Draft / available | 3 | A TP card proves status, not current contribution/payment status. | Check insurance status and covered care separately; the card URL traceability issue is noted above. | Equivalent | Minor correction |
| `healthcare-urgent` | BG-HEALTH-01 | Draft / available | 2 | Call 112 or seek emergency care for a suspected medical emergency. | Emergency care does not make all urgent, follow-up or planned care free. | Equivalent | Ready |
| `housing-initial` | BG-HOUSING-01 | Draft / available | 4 | Necessary accommodation rights and provider-side Program 3 support do not guarantee an individual place. | No chosen location or capacity guarantee; Program 2 closure notice in source list is outside this fact's stated claim; Act URL traceability is unresolved. | Equivalent | Minor correction |
| `housing-private-rental` | BG-HOUSING-01 | Draft / available | 1 | Private renting follows an ordinary landlord-agreed lease and civil law. | A signed writing is advisable; no universal deposit cap is established. | Equivalent | Ready |
| `housing-financial-support` | BG-HOUSING-01 | Draft / available | 2 | Program 3 reimbursement goes to eligible providers, not displaced people as rent cash. | The located rate is not confirmed current; the social-assistance source in the fact list does not support this provider-payment answer. | Equivalent | Minor correction |
| `education-school-enrolment` | BG-EDU-01 | Draft / available | 4 | TP children can apply for public kindergarten/school through the RDE; general compulsory ages apply. | School preference, available seats, records and grade placement are assessed, not guaranteed or automatic. | Equivalent | Ready |
| `education-pupil-language` | BG-EDU-01 | Draft / available | 2 | Additional Bulgarian-language teaching/support may be arranged according to need. | Stage maximums are ceilings, not a guaranteed course or hours; Ukrainian online school is not an automatic substitute. | Equivalent | Ready |
| `education-higher-admission` | BG-EDU-01 | Draft / available | 6 | TP holders may apply through a limited state-university route; recognition and admission are separate. | Places and programme criteria are limited; no automatic admission or general tuition exemption. | Equivalent | Ready |
| `social-subsistence` | BG-SOC-01 | Draft / available | 3 | TP holders may apply for general assistance; award and amount are individually assessed. | Not a flat benefit; heating-allowance notice in source list concerns a separate scheme/window. | Equivalent | Minor correction |
| `social-application` | BG-SOC-01 | Draft / available | 2 | Ask the DSP for the current-address route and requirements for the specific benefit. | No universal checklist or one form for every scheme. | Equivalent | Ready |
| `social-child-benefits` | BG-SOC-01 | Draft / available | 6 | Family benefits have separate rules and uncertain TP interpretation; insurance-based maternity/childcare benefits are separate. | Do not assume entitlement; the People with Disabilities Act source is outside this fact's stated claim. | Equivalent | Minor correction |

**Fact results:** Ready 15; Minor correction 8; Correction required 1.

## 4. Source audit

| Draft | Source-set records | Owned by mapped article | HTTPS | Inline tokens resolve | UUID shared across Bulgaria drafts |
| --- | ---: | --- | --- | --- | --- |
| BG-TP-01 | 5 | Yes | Yes | Yes; all 5 cited | No |
| BG-TP-02 | 5 | Yes | Yes | Yes; all 5 cited | No |
| BG-EMP-01 | 8 | Yes | Yes | Yes; all 8 cited | No |
| BG-EMP-02 | 9 | Yes | Yes | Yes; all 9 cited | No |
| BG-HEALTH-01 | 9 | Yes | Yes | Yes; all 9 cited | No |
| BG-HOUSING-01 | 6 | Yes | Yes | Yes; all 6 cited | No |
| BG-EDU-01 | 12 | Yes | Yes | Yes; all 12 cited | No |
| BG-SOC-01 | 11 | Yes | Yes | Yes; all 11 cited | No |
| **Total** | **65** | **Yes** | **Yes** | **65/65** | **0 conflicts** |

No dangling citation tokens were found. Every comparison `OfficialSourceId` resolves to its linked article's source owner. Source-title and ownership mappings match the drafts. Two catalog URLs are not the URLs recorded for those sources in their corresponding research inventories, so exact resource/version equivalence could not be established from the permitted materials:

- `BG-HEALTH-01-S09` — AREF temporary-protection registration card.
- `BG-HOUSING-01-S01` — Asylum and Refugees Act.

Several fact source lists also include a source that is not relevant to the concise fact as written; these appear in the comparison matrix and correction list. This is a source-association precision issue, not evidence that the article has cross-article source ownership or that the underlying answer is false.

## 5. Consolidated corrections

| Item | Current issue | Research-supported correction | Severity |
| --- | --- | --- | --- |
| BG-TP-02 / `residence-filing` | Pending-stay/work assertions are supported only by the EU long-term-resident citation. It does not support the Bulgarian stay/termination and work-right statements. | Cite the Bulgarian Asylum and Refugees Act Arts. 39 and 82 at the stay/TP termination claim and the research-supported legal basis for the separate work-right boundary; keep Directive 2003/109 only on the EU long-term-resident sentence. | **blocker** |
| BG-HEALTH-01-S09 / `healthcare-proof` | Catalog source URL differs from the corresponding research inventory URL; exact equivalence was not established from the scoped files. | Reconcile the catalog URL with the researched AREF card page or document the authoritative equivalent during a source refresh. | minor |
| BG-HOUSING-01-S01 / `housing-initial` | Catalog Act URL differs from the corresponding research inventory URL; exact resource/version equivalence was not established from the scoped files. | Reconcile the catalog URL with the researched AREF consolidated Act or document the authoritative equivalent/version. | minor |
| `protection-dates` | Fact lists a 2022 procedure source alongside date evidence; it is not direct evidence for the current national/EU date boundary. | Keep direct date support on the 2026 AREF/EU sources; retain the older procedural source only if the fact makes a related procedural claim. | minor |
| `employment-assistance-conditions` | NSSI unemployment-benefit and professional-qualification sources do not support the comparison answer about Employment Agency mediation/programme services. | Limit the fact's source IDs to Employment Agency/jobseeker evidence that supports its wording. | minor |
| `housing-initial` | Program 2 closure source is outside the comparison fact's stated Program 3/accommodation claim. | Keep the Program 2 closure source on content that states that closure; limit this fact to sources supporting accommodation and Program 3. | minor |
| `housing-financial-support` | General social-assistance source is outside the stated provider reimbursement/current-rate claim. | Keep only Program 3/provider sources for this comparison answer, unless the fact adds a separate cited distinction. | minor |
| `social-subsistence` | Heating-allowance notice supports a separate scheme and is not cited in this concise general-assistance answer. | Keep the heating notice with the separate heating-window statement; use general-assistance sources for this fact. | minor |
| `social-child-benefits` | People with Disabilities Act source does not support the stated family-benefit and NSSI maternity/childcare answer. | Keep that source with disability-specific content; limit this fact to family-benefit, case-law and NSSI evidence. | minor |

**Blockers: 1. Minor corrections: 8.** No article or fact was edited during this review.

## 6. Owner decision tables

Decision cells are intentionally blank. The owner makes all acceptance decisions.

### Articles

| Draft | Owner decision | Owner notes |
| --- | --- | --- |
| BG-TP-01 |  |  |
| BG-TP-02 |  |  |
| BG-EMP-01 |  |  |
| BG-EMP-02 |  |  |
| BG-HEALTH-01 |  |  |
| BG-HOUSING-01 |  |  |
| BG-EDU-01 |  |  |
| BG-SOC-01 |  |  |

### Comparison facts

| Aspect ID | Owner decision | Owner notes |
| --- | --- | --- |
| `protection-scheme` |  |  |
| `protection-application` |  |  |
| `protection-dates` |  |  |
| `residence-routes` |  |  |
| `residence-conditions` |  |  |
| `residence-filing` |  |  |
| `employment-vacancies` |  |  |
| `employment-institutions` |  |  |
| `employment-assistance-conditions` |  |  |
| `work-permission` |  |  |
| `work-employer-duties` |  |  |
| `work-minimum-pay` |  |  |
| `healthcare-access` |  |  |
| `healthcare-proof` |  |  |
| `healthcare-urgent` |  |  |
| `housing-initial` |  |  |
| `housing-private-rental` |  |  |
| `housing-financial-support` |  |  |
| `education-school-enrolment` |  |  |
| `education-pupil-language` |  |  |
| `education-higher-admission` |  |  |
| `social-subsistence` |  |  |
| `social-application` |  |  |
| `social-child-benefits` |  |  |

## 7. Status and release boundary

- All 24 Bulgaria translations remain `NeedsReview`.
- All 24 Bulgaria comparison facts remain `Draft` and `IsAvailable=true`.
- Bulgaria remains Development-visible and Production-hidden under the existing generic visibility behavior.
- This audit is not owner acceptance and does not authorize editorial promotion.
- This audit does not authorize deployment.

## 8. Final topic verdict

**NEEDS CORRECTION BEFORE HUMAN ACCEPTANCE**

The BG-TP-02 pending-application stay/work claim has a material citation gap: the cited EU long-term-resident directive does not support the Bulgarian legal claims. Resolve that evidence link before human acceptance. The eight minor corrections concern source traceability or source-list precision; they do not change the bounded answer when corrected. The owner must still make the acceptance decision.
