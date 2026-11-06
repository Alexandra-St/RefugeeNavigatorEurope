# Slovakia Editorial Acceptance Preparation

**Implementation baseline:** `7d6cecf54733128bd6b1d9ef6722b6002ce578c0`  
**Review scope:** Slovakia v1 drafts, eight focused research packages, and Slovakia-owned catalog records  
**Overall verdict:** `NEEDS CORRECTION BEFORE HUMAN ACCEPTANCE`

## 1. Batch snapshot

| Item | Verified state |
| --- | --- |
| Articles | 8 |
| EN / UK / RU translations | 24; all `NeedsReview` |
| Comparison facts | 24; all `Draft`, all `IsAvailable=true` |
| Slovakia-owned article `OfficialSources` | 81 across the eight articles; all use HTTPS |
| Development visibility | Visible |
| Production visibility | Hidden |
| Research readiness | 8/8 topics Ready; 24/24 aspects Ready |

The eight catalog article GUIDs and their mapped comparison categories match `CountryComparisonTopicMap.cs`. Research readiness means that evidence supports bounded drafting; it is not editorial acceptance or owner authorization.

## 2. Article acceptance matrix

| Draft ID | Article GUID | Title | EN / UK / RU | Sources | Research | Caveats preserved | Comparison aspects | Editorial result |
| --- | --- | --- | --- | ---: | --- | --- | --- | --- |
| SK-TP-01 | `09d72fd2-1fa7-51a2-886b-9ebd93f4678d` | Temporary protection in Slovakia | NeedsReview / NeedsReview / NeedsReview | 6 | Ready | Category-based eligibility; no Slovak proof checklist, appointment, processing-time or universal card-expiry promise; TP booking separated from optional electronic declaration; 4 Mar 2028 scheme endpoint separated from individual document validity. | `protection-scheme`, `protection-application`, `protection-dates` | Ready |
| SK-TP-02 | `53272a98-60f0-507d-be15-eea36295bdf2` | Residence options after temporary refuge | NeedsReview / NeedsReview / NeedsReview | 6 | Ready | No automatic conversion; correct 15 Jul 2026 boundary and separate six-month tests; statutory ambiguity retained; business in-country route not generalized; pending stay not work authorization; Blue Card separate; `odídenec` exclusion retained and prior TP-time counting unresolved. | `residence-routes`, `residence-conditions`, `residence-filing` | Correction required |
| SK-EMP-01 | `c26f0cc5-6316-5051-8ea8-8aa53d5d8742` | Finding a job in Slovakia | NeedsReview / NeedsReview / NeedsReview | 7 | Ready | Vacancy search separate from registration; UoZ/ZoZ distinct; TP-holder UoZ eligibility unresolved; services do not promise placement; EURES and qualification recognition bounded. | `employment-vacancies`, `employment-institutions`, `employment-assistance-conditions` | Minor correction |
| SK-EMP-02 | `7e043916-46d8-5767-b00e-0ddc38c6c24e` | Work authorization and employee rights in Slovakia | NeedsReview / NeedsReview / NeedsReview | 14 | Ready | Employee exemption not extended to self-employment; §72bc separate; pending residence not general work permission; dated gross amounts and six grades; collective-agreement and job-grade caveat. | `work-permission`, `work-employer-duties`, `work-minimum-pay` | Correction required |
| SK-HEALTH-01 | `88047ea9-f8a9-5e74-9af7-9b8aeb684a24` | Healthcare in Slovakia | NeedsReview / NeedsReview / NeedsReview | 12 | Ready | Temporary refuge separated from insurance; §9h separated from membership; child/adult scope and employment conditions qualified; 4A legacy evidence not equated to an insurance card; medicines may involve copay; urgent/necessary/planned/full coverage distinguished. | `healthcare-access`, `healthcare-proof`, `healthcare-urgent` | Correction required |
| SK-HOUSING-01 | `47762263-5800-578e-964b-13806a059f7f` | Housing in Slovakia | NeedsReview / NeedsReview / NeedsReview | 7 | Ready | §68 facility and §69 host contribution separate; ordinary 60-day rule; capacity and no-placement guarantee; host-side rather than refugee cash; private lease regimes and deposit caveat separate. | `housing-initial`, `housing-private-rental`, `housing-financial-support` | Correction required |
| SK-EDU-01 | `c277d784-6c19-5edb-9137-a1289f9edc94` | Education in Slovakia | NeedsReview / NeedsReview / NeedsReview | 13 | Ready | Statutory cohort/age scope; chosen school not guaranteed; language support conditional; Ukrainian online study not an automatic substitute; public/state tuition exemption not generalized to private institutions or every fee. | `education-school-enrolment`, `education-pupil-language`, `education-higher-admission` | Correction required |
| SK-SOC-01 | `3e90b7d3-8cc3-5e78-a375-878d1b71b054` | Financial and social assistance in Slovakia | NeedsReview / NeedsReview / NeedsReview | 16 | Ready | Means test and earned-income disregard; agency/municipal/humanitarian routes separate; TP alone not ordinary child/parent entitlement; childcare route conditional; EU coordination case-specific. RU omits the explicit disability-compensation boundary and detailed childcare conditions stated in EN/UK. | `social-subsistence`, `social-application`, `social-child-benefits` | Minor correction |

### Cross-language review

The three translations are substantively aligned for TP-01, TP-02, EMP-01, EMP-02, HEALTH-01, HOUSING-01 and EDU-01. Names and typography vary naturally by language; no material date or amount divergence was found. Two cross-language precision findings remain (both are reported again in the consolidated corrections table):

- **SK-SOC-01, RU vs EN/UK:** RU says only that childcare has “additional conditions” and omits the researched work/study, child-age, provider and non-duplication conditions; it also omits the explicit statement that temporary refuge alone does not establish ordinary disability cash compensation. Add the same boundaries already stated in EN/UK. This is incomplete, not an affirmative RU entitlement claim (**minor**).
- **SK-EMP-02, EN/UK/RU:** all versions call €915, €1,031, €1,147, €1,263, €1,379 and €1,495 “monthly grade floors” but omit the matching six hourly floors €5.259, €5.925, €6.592, €7.259, €7.925 and €8.592. The current 2026 base values are otherwise correct and language formats are equivalent. Add hourly values or scope the statement clearly to monthly floors (**minor**).

All other reviewed modality and number statements are equivalent in substance. In particular, the 4 August continuity exception, 5 August 2026 rule, 21-day TP booking horizon, 15 July §131o boundary, 2026 monthly pay values, 60-day housing statement, and the education age bands are not translated differently between EN/UK/RU.

## 3. Article-specific findings

| Topic | Review result |
| --- | --- |
| SK-TP-01 | The stated eligibility cohorts remain generic; the draft does not broaden them or assert that departure alone suffices. The military condition is conditional and the narrow same-state continuous-protection exception is retained. Reservation, electronic declaration, 21-day TP horizon, and individual document validation are distinguished. Owned markers support the adjacent claims. The concise cohort summary is a useful refinement but not a false eligibility statement. |
| SK-TP-02 | Correct dates, six-month tests, cross-reference ambiguity, no automatic conversion, Blue Card separation, business-filing boundary, work-right boundary, and long-term residence boundary are present. The §131o reservation notice is overgeneralized to “Apply” without limiting scope. Pending-stay sentence is qualified but lacks an inline Act 404 citation for §31(4); add the legal source at that claim and distinguish the ordinary complete-application rule from §131o’s own pending-status rule. |
| SK-EMP-01 | Vacancy browsing, EURES, ÚPSVaR, UoZ, ZoZ and regulated-recognition channels are materially distinguished. The final sentence groups counselling, mediation, training and qualification recognition, while its markers point to Employment Services and regulated-profession sources; the source for the particular training/counselling offer is not clearly tied to each claim. Improve marker placement or narrow the sentence (**minor**). |
| SK-EMP-02 | The 2026 monthly figures and six grades match research and are gross/date-qualified. The employer duty is described only as an information card within seven working days. Research specifies notice at both employment start and termination, electronic filing with qualified signature, and required supporting/retained records. State those events and requirements so an employer is not left with an incomplete deadline action (**blocker**). Add the six hourly grade figures or limit the fact/article wording to monthly values (**minor**). The wage source association includes the minimum-wage act and current ministry grade source; no unsupported collective-agreement guarantee is made. |
| SK-HEALTH-01 | TP does not equal public insurance; §9h is distinct and VšZP is not described as membership. The first paragraph says “uninsured” without stating the §9h exclusion for a person insured under another EU Member State’s public system. Add that qualification and keep cross-border coverage separate (**blocker**). Also identify current `DOČASNÉ ÚTOČISKO` proof (the article only names legacy 4A), and clarify emergency roles (155 ambulance / 112 integrated emergency line); both are useful, non-reversing precision (**minor**). Current proof and emergency claims cite owned operational/statutory sources. |
| SK-HOUSING-01 | §68 and §69 are separated, private rental is distinguished, no universal deposit cap is stated, and historical sources S08–S10 are excluded from current authority. However, §68 omits the exact vulnerability categories and the written extension procedure: vulnerable applicants may request up to six months, repeat requests subject to capacity, and a person already housed must apply at least 15 days before expiry. The omitted deadline can affect accommodation continuity (**blocker**). §69 says categories, rates and reporting depend on law but gives no concrete beneficiary notification schedule or host reporting duties: beneficiary notice within three working days after accommodation begins and twice monthly at least 14 days apart while it continues (with the under-14-day exception); host monthly report within five working days after month end; capacity/advance agreement requirements for specified hosts. Add these action deadlines (**blocker**). Current rate schedule is €5/night for owner-provided residential accommodation with room caps €390/€540/€720/€900 monthly, and €6/night for specified qualifying legal-person/public-host cases; naming the dated host rates/caps is useful but nonessential while the article makes no amount claim (**minor**). |
| SK-EDU-01 | The September 2025 statutory Ukrainian cohort, age bands, school-capacity caveat and current Act 300/2025 boundary are present. The phrase “subject to statutory dates” omits the §161r cohort transition deadlines: three months for proceedings initiated after 31 Aug 2025; three months after 1 Sep 2025 for Jan–Aug 2025 proceedings; and required steps by 1 Sep 2025 for pre-2025 proceedings/status. These dates define a compulsory action and are material (**blocker**). Grade placement and missing-document bounds are not discussed; include that prior records affect placement but missing records do not prevent an application, without promising a grade (**minor**). No funded-place or scholarship entitlement is asserted. |
| SK-SOC-01 | EN and UK state the disability boundary and childcare conditions; RU omits both details, though it cites the associated owned laws. Align RU to EN/UK (**minor**). Other scheme boundaries and amounts are not overstated; no universal application checklist appears. |

## 4. Comparison acceptance matrix

The 24 Slovakia facts are present once each, linked to the corresponding mapped article, `IsAvailable=true`, and `Draft`, with article-owned source IDs. Counts below reflect the source-ID arrays in the integrated record. EN/UK/RU values were checked separately; the language note names each fact’s result.

| Aspect ID | Linked article | Available / status | Sources | Answer summary | Qualification to preserve | EN / UK / RU | Result |
| --- | --- | --- | ---: | --- | --- | --- | --- |
| `protection-scheme` | SK-TP-01 | Yes / Draft | 2 | EU-based Slovak temporary refuge for defined cohorts; departure alone is insufficient. | Narrow military rule and same-state continuity exception; no invented proof list. | Equivalent | Ready |
| `protection-application` | SK-TP-01 | Yes / Draft | 2 | Use DOČASNÉ ÚTOČISKO; 21-day TP booking horizon; electronic declaration separate. | Horizon is TP-specific; no slot or outcome guarantee. | Equivalent | Ready |
| `protection-dates` | SK-TP-01 | Yes / Draft | 2 | Scheme/status through 4 Mar 2028; individual printed document validity separate. | Use ministry validation for individual record. | Equivalent | Ready |
| `residence-routes` | SK-TP-02 | Yes / Draft | 2 | Purpose-specific residence and Blue Card routes; §131o limited transition. | No automatic conversion. | Equivalent | Ready |
| `residence-conditions` | SK-TP-02 | Yes / Draft | 2 | Six months before filing for pre-end route; six months before end plus continuity after end. | Preserve statutory cross-reference ambiguity. | Equivalent | Ready |
| `residence-filing` | SK-TP-02 | Yes / Draft | 2 | Pre-end filing since 15 Jul 2026; pending stay is not general work authorization; business in-country route not established. | Cite §31(4)/applicable law and distinguish §131o from ordinary routes; reservation notice is transition-specific. | Equivalent | Correction required |
| `employment-vacancies` | SK-EMP-01 | Yes / Draft | 2 | Public vacancy search without UoZ registration; listings dynamic. | No job guarantee. | Equivalent | Ready |
| `employment-institutions` | SK-EMP-01 | Yes / Draft | 2 | ÚPSVaR and EURES have distinct roles. | Neither decides immigration status or guarantees a service. | Equivalent | Ready |
| `employment-assistance-conditions` | SK-EMP-01 | Yes / Draft | 2 | UoZ/ZoZ differ; TP-holder UoZ eligibility unresolved. | Confirm with ÚPSVaR; no guaranteed training/placement. | Equivalent | Ready |
| `work-permission` | SK-EMP-02 | Yes / Draft | 2 | Covered TP employees exempt; §72bc is separate after refuge. | No extension to self-employment or pending residence. | Equivalent | Ready |
| `work-employer-duties` | SK-EMP-02 | Yes / Draft | 2 | Employer information card due within seven working days. | State start and termination events, electronic form/signature and required evidence/retention. | Equivalent | Correction required |
| `work-minimum-pay` | SK-EMP-02 | Yes / Draft | 2 | Six 2026 gross monthly grade floors from €915 to €1,495. | Grade depends on job complexity and applicable collective agreement; figures are dated. | Equivalent; hourly grade values absent in all three | Minor correction |
| `healthcare-access` | SK-HEALTH-01 | Yes / Draft | 2 | TP does not create insurance; separate §9h reimbursement with adult/child scope. | Exclude persons insured in another EU Member State from the “uninsured” route. | Equivalent | Correction required |
| `healthcare-proof` | SK-HEALTH-01 | Yes / Draft | 2 | Refugee proof/VšZP reimbursement record is not insurance membership. | Distinguish current `DOČASNÉ ÚTOČISKO` proof, legacy 4A, and insurance card. | Equivalent; current document label absent in all three | Minor correction |
| `healthcare-urgent` | SK-HEALTH-01 | Yes / Draft | 2 | Urgent, necessary, planned and full insured care differ; medicines may have copays. | Emergency roles: 155 medical ambulance; 112 integrated emergency line. | Equivalent; number roles omitted in all three | Minor correction |
| `housing-initial` | SK-HOUSING-01 | Yes / Draft | 2 | §68 facility accommodation ordinarily 60 days from first grant. | Exact vulnerable group, up-to-six-month written extension, repeat/capacity rule and 15-day request deadline. | Equivalent | Correction required |
| `housing-private-rental` | SK-HOUSING-01 | Yes / Draft | 2 | Civil Code ordinary lease differs from Act 98/2014 short-term lease; deposit cap only for latter. | No ordinary-lease universal cap; address-reporting rule remains qualified. | Equivalent | Ready |
| `housing-financial-support` | SK-HOUSING-01 | Yes / Draft | 2 | §69 goes through host/municipality rather than refugee rent cash. | Vulnerability/proof, 60-day/rate/cap schedule and beneficiary/host deadlines. | Equivalent | Correction required |
| `education-school-enrolment` | SK-EDU-01 | Yes / Draft | 2 | September 2025 compulsory-school rules apply to specified Ukrainian/status/age cohorts. | Give three §161r transition deadlines; chosen school not guaranteed. | Equivalent | Correction required |
| `education-pupil-language` | SK-EDU-01 | Yes / Draft | 2 | Language/adaptation depend on needs, consent, capacity and availability. | Ukrainian online study is not automatic substitute. | Equivalent | Ready |
| `education-higher-admission` | SK-EDU-01 | Yes / Draft | 2 | Admission/recognition are institution-specific; tuition exemption limited to public/state institutions. | No universal admission, credit, private-university exemption or fee waiver. | Equivalent | Ready |
| `social-subsistence` | SK-SOC-01 | Yes / Draft | 2 | Material-need assistance is household-, income- and asset-tested; work does not automatically end it. | Current amounts/date and scheme assessment apply. | Equivalent | Ready |
| `social-application` | SK-SOC-01 | Yes / Draft | 2 | ÚPSVaR, municipality and MPSVR routes have separate assessments. | No single checklist or guaranteed award. | Equivalent | Ready |
| `social-child-benefits` | SK-SOC-01 | Yes / Draft | 2 | TP alone does not establish ordinary child/parent allowance; childcare route separate; EU coordination case-specific. | Add disability-compensation boundary and childcare work/study/age/provider/non-duplication conditions to RU for parity. | EN/UK more qualified than RU | Minor correction |

## 5. Source audit

- The eight article-owned source groups contain 6, 6, 7, 14, 12, 7, 13 and 16 records respectively: **81 total**. All 81 IDs are owned by the linked article, all URLs use HTTPS, and inline source tokens inspected resolve to records owned by that same article. No cross-article or dangling token was found.
- Historical housing sources in the focused research set (old MV SR registration page/manual/FAQ, research IDs S08–S10) are not in the seven-record housing editorial source set and are not cited as current. The retained current housing law and Regulation 151/2024 / amendment 36/2025 are consistent with the research package’s transitional-rule explanation. No other clearly stale source was observed being represented as current in the reviewed marker sets.
- No clearly unrelated catalog source was identified. The catalog is an editorial subset of research evidence; not every research reference needs to be catalogued. The current Slovakia research says each article-owned source group is bounded to that article’s topic.
- Claim-marker gaps requiring correction: **TP-02** pending lawful stay needs Act 404 §31(4) tied to the proposition; the §131o reservation source must be limited to that transition route. **EMP-01** should attach the relevant service source to the specific counselling/training claims. **EMP-02** should associate the seven-day duty with the current electronic-service source and Act 5 requirements, and retain the minimum-wage ministry/notice sources for grade amounts. Other reviewed claims have adjacent, article-owned statutory or operational support. No source ID points to a different article’s authority.
- All 24 fact source arrays contain two IDs, each belonging to its linked article. Fact-level support issues are recorded in Section 4; in particular, source presence does not supply the omitted qualification or required deadline.

## 6. Consolidated corrections

| Item | Current issue | Research-supported correction | Severity |
| --- | --- | --- | --- |
| SK-TP-02, articles and `residence-filing` | “Apply through the designated Foreign Police reservation” is attached broadly to the filing paragraph, but the owned notice is specifically for §131o. The pending lawful-stay proposition has no inline Act 404 §31(4) citation. | Limit that reservation instruction to §131o and direct ordinary-purpose routes to their own booking requirements. Cite §31(4) at the applicable complete ordinary-application stay statement and distinguish §131o pending status; neither creates work permission. | blocker |
| SK-EMP-02, article and `work-employer-duties` | Seven-working-day card duty omits the triggering start/end notifications and prescribed electronic signature/evidence requirements. | State that the employer files the prescribed electronic information card for commencement and termination within seven working days, with qualified electronic signature and required employment/status documents and retention; keep §72bc card updates separate. | blocker |
| SK-HEALTH-01, article and `healthcare-access` | “Uninsured” §9h route does not exclude coverage under another EU Member State’s public system. | Specify that §9h is for a temporary-refuge holder not covered by Slovak or another EU Member State public health insurance; keep any cross-border entitlement route separate. | blocker |
| SK-HOUSING-01, article and `housing-initial` | §68 statement omits defined vulnerable categories and extension deadline/procedure. | State the exact §68 groups (age 65+, a single parent with a child under five, or court-appointed caregiver of a child under five); a vulnerable person may request up to six months in writing, repeat requests are subject to capacity, and an already accommodated person must request at least 15 days before expiry. | blocker |
| SK-HOUSING-01, article and `housing-financial-support` | §69 reporting is deferred to “confirm with municipality,” omitting required beneficiary/host actions and deadlines. | State beneficiary notice within three working days after accommodation begins and twice monthly at least 14 days apart while it continues (except stays under 14 days); state eligible host monthly report within five working days after month end and applicable advance agreement/capacity requirements. Keep notices distinct from address-change reporting. | blocker |
| SK-EDU-01, article and `education-school-enrolment` | “Subject to statutory dates” hides the three §161r transition deadlines for a compulsory-school cohort. | Specify: post-31 Aug 2025 proceedings—within three months; proceedings begun Jan–Aug 2025—within three months after 1 Sep 2025; pre-2025 proceedings/status—required steps by 1 Sep 2025. Preserve the separate age/cohort conditions and school-capacity caveat. | blocker |
| SK-EMP-02, article and `work-minimum-pay` | Monthly grade figures are correct, but the six statutory hourly gross floors are omitted from the wage overview. | Either add €5.259, €5.925, €6.592, €7.259, €7.925 and €8.592 per hour for grades 1–6 respectively (2026 values), or explicitly scope the statement to monthly grades; retain gross/date and grading/collective-agreement caveats. | minor |
| SK-HEALTH-01, article and `healthcare-proof` | Current status proof is not named; only legacy 4A is called out. | Name current `DOČASNÉ ÚTOČISKO` proof and supported legacy model 4A; do not describe either as an insurance card. | minor |
| SK-HEALTH-01, article and `healthcare-urgent` | 155 and 112 are given as alternatives without describing their distinct roles. | Identify 155 as emergency medical service and 112 as the integrated emergency line; preserve urgency and coverage distinctions. | minor |
| SK-EDU-01, article | School-placement paragraph lacks research-supported grade/document boundary. | Say placement uses available prior education/records and grade is not guaranteed; missing records do not prevent an application, subject to the authority’s assessment. | minor |
| SK-HOUSING-01, article | §69 rates and caps are not stated, though described as conditional; this is safe but less actionable for hosts. | If retaining amount guidance, state date-qualified host amounts: €5/night owner-provided residential accommodation with monthly room caps €390/€540/€720/€900; €6/night for specified qualifying non-residential accommodation by legal-person/public hosts. Do not present as refugee cash and recheck schedule. | minor |
| SK-EMP-01, article | The combined services sentence groups counselling/training with broad Employment Services markers; the individual training offer is not clearly supported by its immediately adjacent source. | Place the ÚPSVaR information/advisory-centre and relevant employment-services authority markers next to the service claims, or narrow to what those linked sources expressly establish; keep no-course/no-funding guarantee caveat. | minor |
| SK-EMP-02, EN/UK/RU | All three list monthly grade minima but omit the corresponding hourly minima without saying scope is monthly-only. | Add the six hourly figures with EN dot and UK/RU decimal-comma formatting, or clarify the monthly-only scope consistently in all three languages. | minor |
| SK-SOC-01, RU | RU omits EN/UK’s explicit disability-compensation boundary and childcare work/study/age/provider/non-duplication conditions. | Translate those existing EN/UK qualifications into RU; retain case-specific EU coordination. | minor |

**Blockers: 6. Minor corrections: 7.**

## 7. Owner decision tables

No decision has been entered. These cells are intentionally blank for the owner.

### Articles

| Item | Decision |
| --- | --- |
| SK-TP-01 |  |
| SK-TP-02 |  |
| SK-EMP-01 |  |
| SK-EMP-02 |  |
| SK-HEALTH-01 |  |
| SK-HOUSING-01 |  |
| SK-EDU-01 |  |
| SK-SOC-01 |  |

### Comparison facts

| Item | Decision |
| --- | --- |
| `protection-scheme` |  |
| `protection-application` |  |
| `protection-dates` |  |
| `residence-routes` |  |
| `residence-conditions` |  |
| `residence-filing` |  |
| `employment-vacancies` |  |
| `employment-institutions` |  |
| `employment-assistance-conditions` |  |
| `work-permission` |  |
| `work-employer-duties` |  |
| `work-minimum-pay` |  |
| `healthcare-access` |  |
| `healthcare-proof` |  |
| `healthcare-urgent` |  |
| `housing-initial` |  |
| `housing-private-rental` |  |
| `housing-financial-support` |  |
| `education-school-enrolment` |  |
| `education-pupil-language` |  |
| `education-higher-admission` |  |
| `social-subsistence` |  |
| `social-application` |  |
| `social-child-benefits` |  |

## 8. Status and release boundary

All Slovakia translations remain `NeedsReview`; all 24 comparison facts remain `Draft`. Development remains visible and Production remains hidden. No owner decision has been made. This review authorizes no promotion or deployment and changes no content or status record.
