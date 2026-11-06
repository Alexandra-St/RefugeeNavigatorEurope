# Slovakia Research Readiness — 6 November 2026

**Repository baseline:** `fc3b21ec80c050e884b497f779bacbf7dc337312`  
**Research date:** 6 November 2026  
**Status:** Readiness audit only. No Slovakia catalog content, facts, sources, visibility, or production state was added or changed.

## 1. Current Slovakia state

Counts below were mechanically checked in the four requested JSON catalogs. Slovakia matching used country slug/name, entry ownership, OfficialSource title/URL/metadata, and comparison-fact CountrySlug; exact Slovakia-specific references were also searched in the scoped tests and research documents.

| Inventory | Slovakia state at baseline |
| --- | ---: |
| Country definition in `countries.json` | 0 |
| Articles / information entries | 0 |
| Translations | 0 |
| Comparison facts | 0 |
| Slovakia-owned OfficialSources | 0 |
| Existing Slovakia research/draft files under `docs/content-research` | 0 |
| Slovakia-specific tests | 0 |
| Slovakia-specific hardcodes in scoped application/catalog/test files | 0 |
| Production visibility | Not visible; absent from the country catalog |

Repository-wide catalog context: 8 countries, 64 articles, 192 translations, 520 OfficialSources, and 24 shared comparison aspects. `CountryComparisonTopicMap` maps only the eight existing country slugs for its eight article topics. The generic visibility tests assert exactly the accepted eight countries and derive visibility from catalog content; no Slovakia exception or country-specific visibility rule exists in the scoped tests. This audit does not change the catalog or visibility.

## 2. Existing comparison contract

The shared catalog has eight topics and exactly 24 aspect IDs. The map below assigns every current aspect once; no new IDs are proposed.

| Research topic | Exact existing aspect IDs |
| --- | --- |
| Temporary Protection | `protection-scheme`, `protection-application`, `protection-dates` |
| Residence Options | `residence-routes`, `residence-conditions`, `residence-filing` |
| Finding a Job | `employment-vacancies`, `employment-institutions`, `employment-assistance-conditions` |
| Work Authorization and Employee Rights | `work-permission`, `work-employer-duties`, `work-minimum-pay` |
| Healthcare | `healthcare-access`, `healthcare-proof`, `healthcare-urgent` |
| Housing | `housing-initial`, `housing-private-rental`, `housing-financial-support` |
| Education | `education-school-enrolment`, `education-pupil-language`, `education-higher-admission` |
| Financial and Social Assistance | `social-subsistence`, `social-application`, `social-child-benefits` |

**Mapping check: 24/24 unique; 8/8 topics; no missing or extra aspect IDs.**

## 3. Slovakia research plan and readiness matrix

`Ready for focused research` means a bounded research task can begin; it does not mean claims are sufficiently verified for content drafting. Official Slovak law and competent agencies take priority. IOM’s Migration Information Centre is a useful navigation aid, but should not replace current statutory text or agency instructions where these are available.

| Topic | Aspects | Existing evidence | Missing research | Priority | Status |
| --- | --- | --- | --- | ---: | --- |
| Temporary Protection | `protection-scheme`; `protection-application`; `protection-dates` | Ministry of Interior (MV SR) registration page describes in-person registration, documents, locations, and travel/termination; Council Implementing Decision (EU) 2026/1912 extends protection to 4 March 2028 and has a corrected military-obligation rule. | Reconcile statutory eligibility cohorts under the Asylum Act with EU Decision 2026/1912; identify Slovakia’s application process and current locations/booking; required documents and biometrics; document validity/online confirmation; travel, absence, address-reporting and termination. Confirm the corrected exception for people already protected continuously on/before 4 Aug 2026, and exactly when the new condition applies to later grants. | 1 | Ready for focused research |
| Residence Options | `residence-routes`; `residence-conditions`; `residence-filing` | Slov-Lex Act 128/2026 inserts §131o into Act 404/2011; MV SR announced the booking workflow effective 15 July 2026. Ordinary purpose-based temporary residence is separately described by MV SR. | Read the complete current §131o and related amendments, not just the news notice. Establish eligible cohorts and filing window; exact residence purpose/type; six-month activity condition; documents, criminal-record exception for under-14s, appointment rules, decision/document timeline, validity, refusal grounds, TP status while pending and after grant, work rights, exclusions, and relation to one-year continued displaced-person treatment after EU protection ends. Separately map ordinary employment, study, family routes, in-country filing rules, long-term residence, and whether TP time counts. | 2 | Ready for focused research |
| Finding a Job | `employment-vacancies`; `employment-institutions`; `employment-assistance-conditions` | ÚPSVaR publishes employment services and EURES navigation; the Labour Ministry maintains foreign-employment information. | Identify official vacancy search channels and local ÚPSVaR roles; eligibility and steps for jobseeker registration, mediation, counselling, retraining and courses; EURES scope; qualification recognition authority and procedures. Keep job search services distinct from work authorization and benefit eligibility. | 4 | Ready for focused research |
| Work Authorization and Employee Rights | `work-permission`; `work-employer-duties`; `work-minimum-pay` | Act 5/2004 and ÚPSVaR materials identify an exemption from work permits for temporary-refuge beneficiaries; the Ministry publishes minimum-wage tables, including 2026 values. | Verify current consolidated §§23a and employer notice deadlines/form (including July 2026 electronic services); contract and agreement forms; rights on pay, working time, rest, leave, safety, complaints and collective agreements; 2026 general and graded minimum-wage amounts and sector rules; transitions under §131o and post-TP work rights. | 3 | Ready for focused research |
| Healthcare | `healthcare-access`; `healthcare-proof`; `healthcare-urgent` | Health Insurance Act 580/2004 §9h addresses reimbursed care for specified uninsured groups; public insurer and Health Ministry have temporary-refuge guidance. | Verify the current statutory package for a temporary-refuge holder who is not publicly insured, and how employment/public-insurance enrollment changes entitlement; urgent versus routine care; registration/proof documents; provider billing, medicines and copayments; children, pregnancy and vulnerable groups. Resolve differences between public summaries and current law. | 5 | Ready for focused research |
| Housing | `housing-initial`; `housing-private-rental`; `housing-financial-support` | Asylum Act amendment 69/2026 sets current reception-accommodation limits for displaced persons; MV SR provides reporting guidance; IOM MIC has an updated accommodation-support summary. | Verify current emergency and asylum-facility access, vulnerability categories, duration and extensions; whether the former accommodation allowance remains in force in 2026, exact eligibility, amount, duration, payee and employer/host reporting; private lease/deposit/basic rights; municipality and social-housing boundaries. Do not infer a national housing benefit from older materials. | 6 | Ready for focused research |
| Education | `education-school-enrolment`; `education-pupil-language`; `education-higher-admission` | Ministry of Education’s Ukraine portal documents enrolment, compulsory education changes effective 2025, adaptation classes and language support. | Verify current age/cohort and deadline rules under Education Act §161r; school placement if capacity is unavailable; language and adaptation supports; recognition/equivalence authority; secondary and university admissions, tuition and funded-place rules. Establish which rules apply to all foreign children versus Ukrainian displaced children specifically. | 7 | Ready for focused research |
| Financial and Social Assistance | `social-subsistence`; `social-application`; `social-child-benefits` | Ministry of Labour publishes material-need assistance, child allowance, parental and childcare support; Act 69/2026 changes accommodation and displaced-person rules. | Establish displaced-person eligibility for material-need assistance and each child/family/disability scheme separately; interactions with work, residence and insurance; application authority/documents; residence conditions and household assessment. Reconfirm amounts and effective dates from current law; distinguish accommodation support from cash benefits. | 8 | Ready for focused research |

## 4. Mandatory research questions and boundaries

### Temporary protection

- Establish exact current EU and Slovak eligibility cohorts, including family members and non-Ukrainian residents of Ukraine, from Decision 2022/382 and current Slovak asylum legislation.
- Record that Council Implementing Decision (EU) 2026/1912 extends the EU scheme through **4 March 2028**. Its Article 2 military-obligation condition is subject to the corrigendum’s exception for people already protected in a Member State on/before 4 August 2026 who continuously retain that status; verify applicability dates and Slovak administration before describing individual cohorts.
- Confirm Slovakia’s registration offices, appointment/booking needs, personal attendance/biometrics, documents, electronic confirmation and permit validity. Current Ministry pages may be operationally stale in their hours or locations.
- Verify travel within Schengen, return to Ukraine, address reporting, and how status ends—including written notification when permanently departing or taking protection in another Member State.
- Determine whether any 2026 military-obligation change applies to a new applicant; do not project it onto continuously protected people.

### Residence Options and §131o — mandatory focused investigation

Treat the route as a distinct statutory transitional measure connected with the termination of temporary refuge. Read current §131o in Act 404/2011 as amended by Act 128/2026, along with relevant Employment Services Act changes. Determine:

- who can use the route and the exact filing window;
- the eligible temporary-residence purpose/type and how it is granted;
- the six-month purpose/activity test and evidence required;
- Foreign Police filing process, reservation category, required documents, criminal-record and age exceptions, deadlines and actual operational practice;
- whether pending filing confers continued residence or work rights;
- what happens to temporary refuge on filing, notification, refusal and grant;
- duration of the residence permit and interaction with §131o’s one-year continued displaced-person treatment after scheme termination;
- exclusions, rejection grounds, and relation to ordinary employment, study, family, business and other routes.

MV SR’s 13 August notice gives the booking category and says reservations open 21 days ahead; it does not establish all eligibility or legal effects. Do not infer those from the news notice. Use the Slov-Lex statutory text as the primary basis and MV SR for current procedure.

### Employment and job search

- Verify the work-without-permit rule for holders of temporary refuge, employer information-card duty and deadlines, acceptable contracts, and all employee rights under current Labor Code and Employment Services Act.
- Capture 2026 statutory minimum wage by monthly/hourly floor and graded minimum wage levels, with effective date and sector/role boundaries. These figures are time-sensitive and must be refreshed at drafting and publication.
- Check current official vacancies, ÚPSVaR registration eligibility, mediation and retraining conditions, EURES role, and professional-qualification recognition authority. None promises a vacancy, registration, course, credential recognition or job.
- Do not infer that a right to work while under temporary refuge settles work authorization while a §131o application is pending or after temporary refuge ends.

### Healthcare

- Separate public health-insurance membership from statutory reimbursement of care for uninsured temporary-refuge holders. Verify the applicable service scope in Acts 580/2004 and 577/2004, including urgent and routine care.
- Confirm proof accepted by providers/insurer, employment-triggered insurance, registration steps, payer, medicines, copayments and special groups with the Ministry, insurer and current legislation.
- Do not claim either blanket full public-insurance status or emergency-only entitlement without resolving the exact statutory provision and cohort.

### Housing

- Read the current Asylum Act, especially amendments effective in 2026, for reception accommodation and vulnerability-based extension, capacity, application and deadlines.
- Explicitly determine whether an accommodation allowance remains available in 2026, who is eligible, how long it runs, who receives it, and what host/municipal obligations apply. Treat older allowance guidance as stale until checked against current law and MV SR instructions.
- Separate temporary reception, any time-limited state contribution, municipal/local support, social housing and private renting. Do not promise availability, a location or continuing rent payment.

### Education

- Verify the new compulsory-education and application deadlines for Ukrainian children with temporary refuge under §161r, and distinguish those from general foreign-student admission rules.
- Research ordinary and adaptation-class placement, the fallback if a school has no place, language assistance and local capacity.
- Check credential recognition with the competent recognition authority, secondary-school and university admission rules, annual funded places and tuition status. Do not transfer Romanian or other-country school rules by analogy.

### Financial and Social Assistance

- Determine eligibility independently for material-need assistance, child allowance, parental allowance, childcare allowance and disability support. Check any residence-duration, household, employment, insurance, child-residence or coordination requirements.
- Identify the competent ÚPSVaR office or municipality, evidence and application channels for each scheme.
- Use current official amounts only when supported by an effective-date source; amounts, subsistence minimums and conditions are time-sensitive. Eligibility to apply is not an award.
- Do not assume temporary refuge alone creates eligibility for every residence-based or contribution-based benefit.

Across all topics, Slovak statutory text and competent Slovak agencies are primary. EU law governs EU temporary protection scope and duration. Do not transfer eligibility, deadlines, work rights, insurance, accommodation payments, benefit conditions, education pathways or residence transitions from another country by analogy.

## 5. Priority official-source families

- **Temporary Protection / Residence:** MV SR and Foreign Police booking/registration pages; Slov-Lex (Acts 404/2011, 480/2002 and 128/2026); EUR-Lex Council Implementing Decision (EU) 2026/1912 and Decision 2022/382.
- **Employment / Job search:** Ministry of Labour, Social Affairs and Family; ÚPSVaR (including official vacancies, EURES, registration and employer notices); Slov-Lex (Acts 5/2004 and 311/2001); the relevant qualification recognition authority.
- **Healthcare:** Ministry of Health; Slov-Lex (Acts 580/2004, 577/2004 and 576/2004); official public health insurers and provider instructions.
- **Housing:** MV SR / Migration Office; Slov-Lex (Asylum Act 480/2002, including 2026 amendments); current government accommodation-support rules; municipality or higher territorial unit rules where national entitlement is absent.
- **Education:** Ministry of Education’s Ukraine portal; Slov-Lex (Education Act 245/2008 and applicable financing/recognition legislation); competent credential-recognition body and the relevant school/university authority.
- **Social assistance:** Ministry of Labour and ÚPSVaR; Slov-Lex (Acts 417/2013 on assistance in material need, 600/2003 on child allowance, 571/2009 on parental allowance, and applicable disability/childcare statutes).

## 6. Recommended research order

1. Temporary Protection
2. Residence Options, including statutory §131o
3. Work Authorization and Employee Rights
4. Finding a Job
5. Healthcare
6. Housing
7. Education
8. Financial and Social Assistance

This order preserves the requested sequence and follows key dependencies: protection status defines the cohort; §131o and ordinary residence routes affect later work and service entitlements; employment is then researched before its insurance and benefit interactions. Housing and education have their own urgent time-sensitive 2026 legislation and should follow without importing other countries’ rules. Social-assistance amounts and eligibility are last because they depend on the preceding status, employment and household findings.

## 7. Source discovery references

Initial discovery only; each focused phase must retrieve the current Slovak-language text and check amendment and effective dates.

- [Slov-Lex Act 128/2026 amending Act 404/2011, including §131o](https://www.slov-lex.sk/ezbierky-fe/pravne-predpisy/SK/ZZ/2026/128/20261001)
- [Current Slov-Lex Act 404/2011 text](https://static.slov-lex.sk/static/SK/ZZ/2011/404/20261001.print.html)
- [MV SR: §131o transition reservation instructions (13 August 2026)](https://pes.minv.sk/wps/wcm/connect/sk/site/main/spravy/Policia-upozornuje-odidencov)
- [MV SR: temporary-refuge registration](https://pes.minv.sk/wps/wcm/connect/sk/site/main/zivotne-situacie/Cudzinci/registracia-docasneho-utociska/)
- [EUR-Lex: Council Implementing Decision (EU) 2026/1912, current corrected text](https://eur-lex.europa.eu/eli/dec_impl/2026/1912/2026-08-04/eng)
- [ÚPSVaR: employment services information for Ukraine arrivals](https://www.upsvr.gov.sk/media/medialne-spravy/uzitocne-informacie-pre-obcanov-ukrajiny-korisna-informacia-dla-gromadan-ukraini-useful-information-for-citizens-of-ukraine.html?lang=sk&page_id=1159972)
- [Labour Ministry: 2026 minimum-wage levels](https://employment.gov.sk/sk/praca-zamestnanost/vztah-zamestnanca-zamestnavatela/odmenovanie/minimalne-mzdove-naroky/)
- [Slov-Lex: Act 69/2026 affecting asylum/reception arrangements](https://static.slov-lex.sk/static/SK/ZZ/2026/69/20260612.print.html)
- [Ministry of Education: enrolment and education for children from Ukraine](https://ukrajina.minedu.sk/42188-sk/proces-prijimania-deti-a-ziakov-z-ukrajiny/)
- [Ministry of Labour: material-need assistance](https://employment.gov.sk/sk/rodina-socialna-pomoc/hmotna-nudza/)
- [Slov-Lex: Health Insurance Act 580/2004](https://www.slov-lex.sk/ezbierky/pravne-predpisy/SK/ZZ/2004/580/)

## 8. Verdict

**READY FOR FOCUSED RESEARCH**

All eight topics can proceed to bounded source and claim investigations. This is not readiness for content drafting, a legal conclusion, human acceptance, catalog integration, Production visibility, or deployment. No blocking issue prevents the first focused research phases; their findings may still gate particular claims or later drafting.
