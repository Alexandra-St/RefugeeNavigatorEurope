# Spain content V1 proposal — research foundation

**Status: RESEARCH IN PROGRESS.** This remains a partial research proposal, not final country content approval, translation approval, or implementation authorization. Six of the eight proposed articles now have source/claim matrices; Education and Social Assistance remain unresearched. Research date: **20 October 2026 (UTC)**.

## Country identity

| Field | Proposed value |
| --- | --- |
| English / catalog name | Spain |
| Ukrainian | Іспанія |
| Russian | Испания |
| Slug | `spain` |
| Proposed CountryId | `10000000-0000-0000-0000-000000000005` |
| Country resource key | `Country.Spain` |
| Location resource key | `Country.Spain.In` |
| Location phrase (EN) | in Spain |
| Location phrase (UK) | в Іспанії |
| Location phrase (RU) | в Испании |

These are proposed documentation values only. Do not add them to runtime/catalog files in this phase.

## Eight-article architecture

Research status and matrix links for all eight proposed articles are listed below; only Education and Social Assistance remain unresearched.

| ID | Category | Working English title | Research status |
| --- | --- | --- | --- |
| ES-TP-01 | `temporary-protection` | Temporary Protection in Spain: Applying and Maintaining Your Status | Researched; publication blockers remain. See [source and claim matrix](../content-research/spain-batch-2026-10-20/temporary-protection-application/SOURCES_AND_CLAIMS.md). |
| ES-TP-02 | `temporary-protection` | Residence Options Beyond Temporary Protection in Spain | Researched; EU long-term-residence counting remains unresolved. See [source and claim matrix](../content-research/spain-batch-2026-10-20/after-temporary-protection/SOURCES_AND_CLAIMS.md). |
| ES-EMP-01 | `employment` | Finding a Job in Spain | Researched; regional registration and support conditions remain. See [source and claim matrix](../content-research/spain-batch-2026-10-20/finding-a-job/SOURCES_AND_CLAIMS.md). |
| ES-EMP-02 | `employment` | Work Authorization and Employee Rights in Spain | Researched; individual document effects and time-sensitive wage rules require recheck. See [source and claim matrix](../content-research/spain-batch-2026-10-20/employment-rights/SOURCES_AND_CLAIMS.md). |
| ES-HEALTH-01 | `healthcare` | Accessing Healthcare in Spain | Researched; the Ministry Q&A publication status and regional procedures remain publication gates. See [source and claim matrix](../content-research/spain-batch-2026-10-20/healthcare/SOURCES_AND_CLAIMS.md). |
| ES-HOUSING-01 | `housing` | Finding Accommodation in Spain | Researched; reception access and recent rental-law amendments require case-specific/current checks. See [source and claim matrix](../content-research/spain-batch-2026-10-20/housing/SOURCES_AND_CLAIMS.md). |
| ES-EDU-01 | `education` | Education and Language Learning in Spain | **NOT YET RESEARCHED** |
| ES-SOC-01 | `social-assistance` | Financial and Social Assistance in Spain | **NOT YET RESEARCHED** |

## Research inventory

| Article | Atomic claims | Unique official source URLs in its matrix | Current assessment |
| --- | ---: | ---: | --- |
| ES-TP-01 | 20 | 8 | Spain-specific cohorts and procedure are documented. EU protection runs to 4 March 2028, while the Spanish TIE extension instrument reviewed runs only through 4 March 2027. Detailed Spain-specific evidence rules for the August 2026 military condition and Spanish document handling after that date remain unresolved. |
| ES-TP-02 | 14 | 4 | A Spain-specific framework for applying to other in-country authorizations exists through RD 316/2026 and SEM 2/2026; there is no automatic blanket conversion. SEM 2/2026 expressly credits TP time for national long-term residence. The inspected sources do not establish TP-time counting for EU long-term residence. |
| ES-EMP-01 | 9 | 7 | Official job-search channels, public employment-service roles, jobseeker registration navigation, and a limited professional-recognition signpost are supported. Regional procedures and individual access remain conditional. |
| ES-EMP-02 | 15 | 8 | TP grant decisions include residence/work authorization; selected employer duties and 2026 employment baselines are supported. Pending-document effects remain case-specific. |
| ES-HEALTH-01 | 6 | 5 | National entitlement framework, health-card role, regional process boundary and 112 emergency navigation are mapped. Practical receipt/card steps depend on a Ministry PDF whose final publication status is unclear. |
| ES-HOUSING-01 | 13 | 5 | State reception is conditional and distinct from private rentals. LAU deposit/guarantee, fees and Padrón boundaries are covered with current-text and local-procedure gates. CREADE is a conditional navigation signpost requiring an operational recheck. |
| **All six researched matrices** | **77** | **37 source entries / 33 unique URLs** | Source entries across matrices are counted per article; shared URLs are deduplicated in the total. The four new matrices add 25 source entries, 21 of which are new URLs beyond the 12 URLs in the TP/residence foundation. |

Claims marked Conditional preserve the relevant cohort, procedure, region, or exception; they are not individual eligibility findings. No production source IDs, `LastCheckedAt`, article timestamps, or verification statuses are assigned.

## Spain temporary-protection date and evidence boundaries

- **EU scheme:** Decision (EU) 2026/1912 sets 4 March 2028 for the EU-covered displaced-person categories. The corrigendum makes the military-obligation provision applicable from 5 August 2026 and preserves an exception for people holding TP in the same Member State before/on 4 August 2026 who retain it continuously.
- **Spanish card:** Orden INT/96/2026 extends covered Spanish TIE cards through 4 March 2027 and says a new card is not needed for the specified Article 209 obligation. The EU scheme date does not itself extend the Spanish card or decide a person's document validity. Recheck for a later Spanish measure and authoritative instructions before any publication.
- **Military evidence:** Spain’s Ministry overview says applicants from August 2026 must justify compliance with Ukrainian military obligations or exemption. It does not provide age bands, Spanish evidence formats, or detailed handling of continuous existing holders. Do not import Czech evidence requirements.
- **Official-source date conflict:** Ministry temporary-protection and FAQ pages still state 4 March 2027, reflecting earlier EU acts. Those historic statements conflict with the later EU act on the EU scheme endpoint and must not be reproduced as current. The status of Spanish TIE documents after 4 March 2027 is not established by the sources reviewed.
- **Transition to other residence:** Spain now has an administrative transition framework for current TP holders to seek applicable ordinary in-country authorizations. It is route-specific, not a separate automatic “conversion” permit.
- **Long-term residence:** TP time counts fully toward the five-year national long-term-residence period under SEM 2/2026. The inspected evidence does not establish TP time counting for the distinct EU long-term-residence route.

## Comparison V2 research candidates — temporary-protection aspects only

This is a candidate mapping, not final comparison answer text or approval. Readiness reflects evidence completeness, not eligibility or legal certification.

| Aspect ID | Candidate claim IDs | Readiness | Important limitation |
| --- | --- | --- | --- |
| `protection-scheme` | ES-TP01-01, ES-TP01-03–07, ES-TP01-20 | **Conditional** | EU-defined cohorts and Spain’s broader national scope must remain distinct; the Spanish cohort expansion has separate factual conditions. |
| `protection-application` | ES-TP01-08–11 | **Conditional** | Legal procedure is established, but current operational appointment/office instructions can change; evidence is cohort-specific. |
| `protection-dates` | ES-TP01-01–02, ES-TP01-14–16, ES-TP01-19 | **Unresolved for a single combined answer** | EU protection through 4 March 2028; Spanish TIE measure through 4 March 2027; outdated official pages still say 2027. Do not merge scheme and card dates. |
| `residence-routes` | ES-TP02-01–08, ES-TP02-14 | **Conditional** | A framework exists, but route accessibility and criteria vary; no automatic conversion or guaranteed approval. |
| `residence-conditions` | ES-TP02-03–08, ES-TP02-10–13 | **Conditional** | Only limited signposts researched; this is not a complete route-specific eligibility catalogue. |
| `residence-filing` | ES-TP02-02–09, ES-TP02-13 | **Conditional** | In-country filing is limited to procedures that can be initiated in Spain; current channels, steps, and sequence need route-specific verification. |

No comparison facts are drafted or added.

## Employment, healthcare and housing research findings

### Finding a job and public employment services

- SEPE provides Empléate and links to autonomous-community vacancy services. EURES is a separate EU vacancy and mobility information route, not the authority for regional jobseeker registration or Spanish benefits.
- In autonomous communities, jobseeker registration is routed through the service competent for the person's address; Ceuta and Melilla use SEPE. Registration is distinct from a decision on unemployment-benefit eligibility.
- Public employment services provide intermediation, counselling and training within applicable national/regional service portfolios. No job placement, benefit, course place, free course or diploma recognition is guaranteed.
- For regulated professions, the narrow official signpost is REGPROF and the National Assistance Centre for professional qualifications. Academic homologación/equivalence is not researched in this article.

### Work authorization and employee rights

- The Spanish temporary-protection grant decision reviewed includes residence and work authorization. A receipt/NIE reference is not a substitute for checking the person's current status and operative document; the effect of a pending application is not generalized.
- The evidence supports selected employer duties: Social Security registration and contributions, contract communication to SEPE, and obtaining work authorization where the general foreign-national rule requires it. Special regimes and exceptions matter; this is not a complete employer checklist.
- The official 2026 SMI is €1,221 per month or €40.70 per day according to the wage basis, with an annual comparison floor of €17,094. The annual figure corresponds to 14 × €1,221; two extra payments are provided for under the Workers' Statute and may be prorated across 12 months by collective agreement. Shorter working time is prorated. Recheck after 2026.
- Selected national baselines include an average 40-hour ordinary workweek over the year and at least 30 calendar days' paid annual leave, subject to applicable law, contract and collective agreement. The official Punto de Acceso General page describes ITSS complaint routes; filing does not establish a violation or guarantee an outcome.

### Healthcare

- The 2026 national decree places temporary-protection applicants in a specially protected group whose healthcare rights follow applicable specific rules. Health entitlement, its administrative recognition, and the regional individual health card are separate steps/concepts.
- Ministry Q&A material describes presenting a TP application receipt or grant resolution to a region-enabled health centre/unit, but the PDF viewer title says “Draft Concept Note” and has no clear final-publication date. Do not treat the practical route as final until its status is confirmed and regional instructions are checked.
- 112 is the nationwide general emergency number; autonomous communities operate response services. This research does not assert a uniform nationwide 061 role.
- A dated 2022 Ministry page's broad “free healthcare” statement is not used as a current universal coverage or copayment rule. Medicines, copayments, interpretation and region-specific primary/specialist routes remain outside the supported claims.

### Housing and accommodation

- The state reception system may include temporary-protection applicants/beneficiaries with insufficient resources, subject to assessment and access/permanence rules. The Ministry's Ukraine page lists CREADE as information/appointment points and a route to reception, but its contact details and operational availability must be checked before publication. Reception is distinct from ordinary private rental; no placement, facility, region or fixed duration is guaranteed.
- For habitual-residence leases within the LAU, the checked consolidated text requires a one-month cash deposit; a separate additional guarantee may be agreed and, for contracts up to five years (seven where the landlord is a legal person), is capped at two months' rent. The currently checked text places named real-estate management and contract-formalization fees on the landlord. The law's October 2026 amendment/repeal history makes a publication-time recheck essential.
- Padrón registration records actual residence rather than private title or immigration status. Municipalities administer evidence and verification; one national instruction describes a particular authorization route when other persons are already registered at the address. Do not convert it into a universal landlord-consent requirement.
- No nationwide entitlement to rent reimbursement, municipal housing, or indefinite state accommodation is established. Local and regional aid criteria remain unresearched.

## Conflicts and publication boundaries

- **Stale Ukraine guidance:** the Ministry temporary-protection page used in TP and employment research still shows an earlier 4 March 2027 date; that text is not used as current scheme duration or permit validity. EU scheme dates and Spanish TIE/document validity remain distinct and need a publication-time check.
- **Healthcare document status:** the Ministry-hosted Q&A PDF's viewer title is “Draft Concept Note.” It is conditional evidence only; no universal current card process or free-care/copayment conclusion should be published from it alone.
- **Housing law freshness:** the current consolidated LAU displays September/October 2026 amendments and repeal history. Deposit, guarantee and Article 20 fee terms need rechecking against the law applicable to the actual lease date/type.
- **Regional procedures:** jobseeker registration/services, training portfolios, health-card issuance and care pathways, reception intake/routing, and Padrón evidence are administered regionally or locally. This proposal does not generalize one regional process nationally.
- **Current-program limitation:** no current Ukraine-specific employment program was established in this focused review. Do not revive historical 2022–2024 programs as current.
- **Workplace reporting:** the ITSS page about anonymous mailbox communications could not be opened directly; no anonymous-reporting claim is made. A separate directly opened government page supports formal complaint navigation only.

## Comparison V2 research candidates — employment, healthcare and housing

This mapping supplements the temporary-protection candidates above. It is a research candidate map, not final comparison answer text or approval.

| Aspect ID | Candidate claim IDs | Readiness | Important limitation |
| --- | --- | --- | --- |
| `employment-vacancies` | ES-EMP01-01–04 | Conditional | Official channels exist; vacancies and regional coverage are not exhaustive or guarantees. |
| `employment-institutions` | ES-EMP01-03, 05, 07 | Conditional | Registration/service route depends on autonomous community or Ceuta/Melilla. |
| `employment-assistance-conditions` | ES-EMP01-06–09 | Conditional | Registration documents, benefit eligibility, course access and professional-recognition route are individual/route-specific. |
| `work-permission` | ES-EMP02-01–03 | Conditional | Grant decision carries work authorization; pending receipt and individual document effects require case-specific checking. |
| `work-employer-duties` | ES-EMP02-04–06, 13–15 | Conditional | Selected registration, contribution, contract/reporting, conditional authorization and ITSS signposts only; not a complete checklist. |
| `work-minimum-pay` | ES-EMP02-07–11 | Conditional | 2026 SMI and selected statutory baseline only; contract, collective agreement, work time and payroll determine application. |
| `healthcare-access` | ES-HEALTH01-01–04 | Conditional | Practical receipt/resolution pathway depends on uncertain-status PDF and regional process. |
| `healthcare-proof` | ES-HEALTH01-02–04 | Conditional | TP receipt/resolution and individual health card have distinct roles; confirm current regional process. |
| `healthcare-urgent` | ES-HEALTH01-05 | Supported | 112 is nationwide; do not generalize 061. |
| `housing-initial` | ES-HOUSING01-01–03, 13 | Conditional | Reception is assessed and program-based; CREADE details are undated and need an operational recheck; no placement, duration or location guarantee. |
| `housing-private-rental` | ES-HOUSING01-04–11 | Conditional | LAU scope and recent amendments matter; Padrón procedure is municipal and fact-specific. |
| `housing-financial-support` | ES-HOUSING01-12 | Unresolved | No national rent-aid entitlement established; local housing aid and criteria were not researched. |

No comparison facts are drafted or added.

## Future guide note

ES-TP-01 may provide a foundation for a future `/guides/spain/temporary-protection` navigation guide after operational procedures and the military-evidence/document-validity publication gates are resolved. No scenarios, steps, branches, or guide data are designed here.

## Remaining research before a complete Spain proposal

- Research ES-EDU-01 Education and Language Learning and ES-SOC-01 Financial and Social Assistance. These are the only two articles in the eight-article architecture not yet researched.
- Before article drafting/publication, resolve or clearly gate Spanish TIE/document handling after 4 March 2027, obtain authoritative Spain-specific military-evidence instructions for applicable applications, and clarify whether TP time counts for EU long-term residence. Recheck Spanish pages still showing the earlier 4 March 2027 date.
- Confirm the final/operative status of the Ministry healthcare Q&A PDF and replace it with current authoritative guidance if possible.
- Recheck the 2026 SMI and October 2026 LAU text immediately before publication, along with region-specific operational procedures for the intended audience.
- No runtime, translation, production-catalog, guide, or comparison-fact readiness is claimed.
