# Poland Public-Launch Red-Team Audit

**Audit date:** 21 November 2026

**Starting production base:** `1fb160ebf1fc53fb5a8990449c210bac979e36e0`

**Final audited production base:** `5eadf9d27d9032435de37f71a4df78064bc73bea`

**Final status:** CLEAN

**CI gate at final production base:** CI success (run 44978460532); Netherlands visual evidence success (run 44978460567). Both exact-SHA push runs completed successfully.

## Article inventory

| Audit ID | Production ArticleId | Slug |
|---|---|---|
| PL-TP-01 | `218207bd-2a85-59e1-a945-eedb0604392e` | `temporary-protection-application` |
| PL-TP-02 | `c230d45f-9c76-5912-8c97-dc7d0689baa6` | `after-temporary-protection` |
| PL-EMP-01 | `a323701f-367e-52bb-9c33-45c4156a1350` | `finding-employment` |
| PL-EMP-02 | `7797d03c-6873-50cd-aef6-0d94a2f5fcab` | `work-authorization-rights` |
| PL-HEALTH-01 | `d7e0716b-154d-5db3-8790-f73684c07271` | `healthcare` |
| PL-HOUSING-01 | `1f310b3c-dceb-5e3a-90d0-d3a705ca5c53` | `housing` |
| PL-EDU-01 | `32ca34e9-22d9-5f59-88a9-bbd7ffd7b5cd` | `education-language` |
| PL-SOC-01 | `a5892591-d082-57ce-acf9-5c39ab4f9b84` | `social-assistance` |

Eight articles and 24 verified EN/UK/RU translations are in scope. All 24 translations were audited.

## Source inventory

- Poland-owned OfficialSources: **51 unique records**.
- Global OfficialSources: **1,560 records**.
- Poland Compare facts: **24**.

## Methodology

This is final consolidation of the completed Tasks 218A1, 218A2, 218B1, 218B2, 218C1, 218C2 and 218C3, plus the recorded repairs. No new legal research or legal audit was performed. Repository verification checked the current production base, source ownership and uniqueness, Compare bindings, and repair history. GitHub Actions was checked for the exact final production SHA. Local .NET tests were not required for this synthesis task.

## Phase coverage

| Phase | Scope | Articles | Compare | Material high-risk | Time-sensitive | Historical divergences | P0 | P1 | P2 | Watch candidates |
|---|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 218A1 | PL-TP-01 + protection Compare | 1/1 | 3/3 | 10/10 | 9/9 | 0 | 0 | 0 | 0 | 3 |
| 218A2 | PL-TP-02 + residence Compare | 1/1 | 3/3 | 14/14 | 9/9 | 1 | 0 | 1 | 0 | 1 |
| 218B1 | PL-EMP-01 + PL-EMP-02 + employment/work Compare | 2/2 | 6/6 | 14/14 | 8/8 | 0 | 0 | 0 | 1 | 1 |
| 218B2 | PL-HEALTH-01 + healthcare Compare | 1/1 | 3/3 | 13/13 | 6/6 | 0 | 0 | 0 | 1 | 0 |
| 218C1 | PL-HOUSING-01 + housing Compare | 1/1 | 3/3 | 14/14 | 7/7 | 0 | 0 | 0 | 0 | 0 |
| 218C2 | PL-EDU-01 + education Compare | 1/1 | 3/3 | 14/14 | 7/7 | 0 | 0 | 0 | 0 | 0 |
| 218C3 | PL-SOC-01 + social Compare | 1/1 | 3/3 | 20/20 | 8/8 | 0 | 0 | 0 | 1 | 0 |

**Aggregates:** ARTICLES_AUDITED=8/8; COMPARE_AUDITED=24/24; MATERIAL_HIGH_RISK_AUDITED=99/99; TIME_SENSITIVE_AUDITED=54/54. Historical divergence count was 1; it is repaired. Current SEMANTIC_DIVERGENCES=0. Four substantive findings were discovered, all resolved.

## Temporary protection

The audited EU temporary-protection horizon is through **4 March 2028**. The corrected military-obligation rule applies from **5 August 2026**. The same-Member-State continuous-protection exception was checked through **4 August 2026**. Article 106 has defined scope and does not apply universally to every Ukrainian national or PESEL UKR holder. The PESEL UKR central route and identity-confirmation deadlines were audited, as were the qualified consequences of absence exceeding 30 days and restoration. The old paper protection-certificate horizon is a separate document-validity matter from the overall temporary-protection horizon.

## Residence / CUKR

CUKR is a separate three-year permit with defined UKR eligibility, including the audited **4 June 2025** and **365-day** conditions; children born in Poland have separate treatment. The MOS filing window runs **4 May 2026 through 4 March 2027**. Qualifying timely filing has a pending lawful-stay effect, but filing alone does not grant CUKR. Card collection triggers the UKR-to-CUKR transition; the audited 60-day non-collection consequence is represented. Benefit eligibility remains separately qualified. Ordinary residence uses the MOS framework, with the post-**5 March 2026** distinction audited.

## Employment

The audit distinguishes ePraca vacancy listings from PUP registration and services, and registered unemployed status from jobseeker registration, including the different health-insurance consequence. KBI coverage is stated with its qualified scope. Temporary-protection labour-market access is qualified by status and route. For the applicable route, employer notification to PUP is due within seven days after work starts. Agreement requirements include written form and a language understandable to the worker. The audited 2026 minimums are **PLN 4,806/month** and **PLN 31.40/hour**; enacted 2027 rates are **PLN 4,950/month** and **PLN 32.30/hour** from **1 January 2027**. Contract-scope qualifications are retained.

## Healthcare

The post-**5 March 2026** framework does not make PESEL UKR alone equivalent to universal full insurance. The limited category route includes people under 18, pregnancy, childbirth and the postnatal period, survivors of torture or rape, and the specified collective-accommodation certificate category. Limited care is distinguished from insurance. POZ, emergency numbers **112/999**, and NiŚOZ are covered; employment, unemployment and business-insurance routes remain separate.

## Housing

The **5 March 2026** framework change and the end of the former Ukraine-specific transition on **30 June 2026** were audited. The interventional stay-and-meal support’s statutory 12-month limit tied to the 2022 mass-influx event was checked. Residual funded accommodation is limited to qualifying UKR vulnerable groups unable to self-house; no universal place guarantee is implied. Private-rental advice is qualified. Housing allowance national eligibility criteria are distinguished from local administration.

## Education

The audited framework distinguishes annual preschool preparation for children turning six, the school obligation from the school year in which a child turns seven, and the broader learning obligation through age 18. The Ukraine rule from September 2024 was checked; Ukrainian remote learning alone does not satisfy the current Polish obligation, and the historical 2024/2025 exception has expired. Ordinary school admission does not require nostrification of prior school records; missing-record and grade-placement procedures are addressed. Free Polish lessons may last up to 24 months; a language-of-origin teacher assistant may support up to 12 months, separately from an intercultural assistant. NAWA recognition does not itself equal admission.

## Social assistance

There is no universal Ukraine-only payment, and PESEL UKR alone does not establish general social-assistance entitlement. OPS/MOPS assesses general assistance. 800+ is **PLN 800/month** for an eligible child subject to residence/status conditions and the applicable Polish education obligations: annual preschool preparation, compulsory schooling or the learning obligation, as applicable. The UKR work-activity rule applies from **1 February 2026**, with statutory exceptions. A qualifying registered-unemployed parent may be eligible for three months, or six months when raising at least three children. ZUS assesses 800+ cases. Dobry Start is **PLN 300 once per school year**; conditions change in 2026/2027, and its UKR education condition remains school-specific. Other family benefits remain programme-specific.

## Compare audit

Post-repair disposition for all 24 Poland aspects:

| # | Aspect | Final disposition |
|---:|---|---|
| 1 | protection-scheme |
| 2 | protection-application |
| 3 | protection-dates |
| 4 | residence-routes |
| 5 | residence-conditions |
| 6 | residence-filing |
| 7 | employment-vacancies |
| 8 | employment-institutions |
| 9 | employment-assistance-conditions |
| 10 | work-permission |
| 11 | work-employer-duties |
| 12 | work-minimum-pay |
| 13 | healthcare-access |
| 14 | healthcare-proof |
| 15 | healthcare-urgent |
| 16 | housing-initial |
| 17 | housing-private-rental |
| 18 | housing-financial-support |
| 19 | education-school-enrolment |
| 20 | education-pupil-language |
| 21 | education-higher-admission |
| 22 | social-subsistence |
| 23 | social-application |
| 24 | social-child-benefits |

All 24 final Compare aspects are PASS or PASS_WITH_QUALIFICATION. None has a final P0, P1 or P2 finding.

## High-risk coverage

MATERIAL_HIGH_RISK_AUDITED=99/99.

| Phase | Audited |
|---|---:|
| 218A1 | 10/10 |
| 218A2 | 14/14 |
| 218B1 | 14/14 |
| 218B2 | 13/13 |
| 218C1 | 14/14 |
| 218C2 | 14/14 |
| 218C3 | 20/20 |

## Time-sensitive coverage

TIME_SENSITIVE_AUDITED=54/54.

| Phase | Audited |
|---|---:|
| 218A1 | 9/9 |
| 218A2 | 9/9 |
| 218B1 | 8/8 |
| 218B2 | 6/6 |
| 218C1 | 7/7 |
| 218C2 | 7/7 |
| 218C3 | 8/8 |

## Translation consistency

TRANSLATIONS_AUDITED=24/24. One historical semantic divergence was found in PL-TP-02: EN contained pending-CUKR material that UK/RU omitted. After repair, all eight articles have EN/UK/RU material parity within audited scope. Historical divergence=1; final SEMANTIC_DIVERGENCES=0.

## Findings and repair history

Four substantive findings were discovered: historical severity totals P0=0, P1=1, P2=3. All four are RESOLVED.

| Finding | Severity | Article | Repair commit | Final disposition |
|---|---|---|---|---|
| PL_P1_001 | P1 | PL-TP-02 | `8c651cfeff9c47a1ff0b4cf1f2506945f00753da` | RESOLVED |
| PL_P2_001 | P2 | PL-EMP-01 | `80c48aea3edbc580a11c312848bf43d3cce74a35` | RESOLVED |
| PL_P2_002 | P2 | PL-HEALTH-01 | `a408357efa4bed3772bb554f6efe25c880a6e0e0` (content); `8656fbae288fbb0e84922d3c414bbdfe304d1654` (test-only follow-up) | RESOLVED |
| PL_P2_003 | P2 | PL-SOC-01 | `5eadf9d27d9032435de37f71a4df78064bc73bea` | RESOLVED |

The PL_P2_002 follow-up fixed a regression-test compile defect and was not a new legal/content finding.

## Source-binding integrity

Structural repository verification found 51 unique Poland-owned source IDs out of 1,560 global source records. Poland Compare has 24 facts; every Compare source ID resolves in `official-sources.json`, each source owner matches that fact's Poland InformationEntryId, and all eight article owner bindings resolve. No duplicate source IDs or dangling Compare source IDs were found. No new dangling IDs were introduced by the repairs.

## Watch candidates

WATCH_CANDIDATES=5. The two 4 March 2027 watches remain separate because certificate validity and the CUKR filing deadline are distinct operational triggers.

| Watch | Topic | Exact date | Trigger |
|---|---|---|---|
| PL-W-001 | work-minimum-pay | 2027-01-01 | Enacted 2027 rates take effect; recheck rates and production wording. |
| PL-W-002 | protection-document / old certificate validity | 2027-03-04 | Recheck current UdSC/document-replacement instructions before expiry. |
| PL-W-003 | residence-filing / CUKR | 2027-03-04 | Recheck filing window, transition rules or successor procedure. |
| PL-W-004 | protection-dates / Article 106 transition | 2027-03-05 | Recheck controlling EU instrument and current Polish implementation/guidance. |
| PL-W-005 | temporary-protection endpoint / post-TP residence | 2028-03-04 | Recheck EU successor/transition legislation and Polish post-TP residence guidance. |

## Production change history

AUDIT_DISCOVERED_PRODUCTION_CHANGES=YES: the completed Poland audit led to the five recorded repair commits above. TASK_218D_PRODUCTION_CHANGED=NO: this task creates only this report and its findings/watch CSV.

## Structural validation and final launch assessment

Historical findings: P0=0, P1=1, P2=3. Resolved findings=4/4. UNRESOLVED_P0=0; UNRESOLVED_P1=0; UNRESOLVED_P2=0; UNRESOLVED_HIGH_RISK=0. Current semantic divergences=0. CI and Netherlands visual evidence both succeeded for the exact final audited production SHA. Final status: **CLEAN**.
