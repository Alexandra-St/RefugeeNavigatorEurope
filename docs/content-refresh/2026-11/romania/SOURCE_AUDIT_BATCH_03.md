# Source Audit Batch 3 — Romania Housing and Education

Audit date: 21 November 2026. As-of date: 21 November 2026. Each assigned source was opened directly, or its exact official companion/alternate representation was inspected when the stored route returned a fetch error.

## 491030f2-f0c2-5494-a295-819a8ad82433
- Owner: RO-HOUSING-01; priority: P0
- Authority: Romanian Government
- Title: Romanian Government, Emergency Ordinance (OUG) 96/2024, current consolidated printable form
- URL: https://legislatie.just.ro/Public/FormaPrintabila/00000G0QBYKFR4FB89A28GQQ3M77FREN
- Fetch result: official page/PDF opened directly; relevant content reviewed
- Currentness: CURRENT; source action: KEEP
- Source summary: OUG 96/2024 official consolidated text opened. Arts. 1–3 define the new-beneficiary cohort, first-response temporary accommodation camps, lump-sum accommodation support for one three-month period from entry, designated local-authority locations, exclusions and responsible emergency authorities. Art. 26 is a separate transition rule for earlier beneficiaries and does not create an ongoing general housing entitlement. Owner-specific RO-HOUSING role is valid despite shared instrument. No metadata change.
- Candidate ClaimIds: RO-HOUSING-01-C001, RO-HOUSING-01-C002-S01, RO-HOUSING-01-C002-S02, RO-HOUSING-01-C002-S03, RO-HOUSING-01-C002-S04, RO-HOUSING-01-C005, RO-HOUSING-01-C006, RO-HOUSING-01-C007
- Tuple decisions: RO-HOUSING-01-C001 — SUPPORTED; OUG 96/2024 arts.2–3 cover first-response temporary camps and designated local-authority accommodation locations for the statutory beneficiary cohort; art.26 is a separate expired transition route for earlier beneficiaries.; RO-HOUSING-01-C002-S01 — NOT_SUPPORTED; OUG 96/2024 supports the applicable programme and locally designated accommodation route, but this tuple does not establish current capacity as a general condition. PARTIAL_SUPPORT: programme/local designation only.; RO-HOUSING-01-C002-S02 — NOT_SUPPORTED; OUG 96/2024 describes specific accommodation channels and eligibility but does not directly establish this negative guarantee assertion about municipality, housing type or stay length. Do not infer from statutory silence.; RO-HOUSING-01-C002-S03 — NOT_SUPPORTED; OUG 96/2024 describes specific accommodation channels and eligibility but does not directly establish this negative guarantee assertion about municipality, housing type or stay length. Do not infer from statutory silence.; RO-HOUSING-01-C002-S04 — NOT_SUPPORTED; OUG 96/2024 describes specific accommodation channels and eligibility but does not directly establish this negative guarantee assertion about municipality, housing type or stay length. Do not infer from statutory silence.; RO-HOUSING-01-C005 — SUPPORTED; OUG 96/2024 art.3 and HG 1178/2024 establish the statutory cohort, flat-rate accommodation amount (750 lei/month single; 2,000 lei/month family), one three-consecutive-month period, eligibility/application conditions and exclusions.; RO-HOUSING-01-C006 — SUPPORTED; OUG 96/2024 and HG 1178/2024 prescribe the local-authority application route and months-two/three conditions, including work-related and children’s education conditions with specified exceptions and deadlines.; RO-HOUSING-01-C007 — SUPPORTED; The instruments call this a flat-rate accommodation sum, require application/eligibility and limit it to one three-month period; this is not an open-ended general rent subsidy or actual-rent reimbursement.
- Compare roles: housing-initial — SUPPORTED; Source directly distinguishes first-response camps and designated local-authority accommodation, with legal eligibility and cohort boundaries.; housing-financial-support — SUPPORTED; OUG 96/2024 establishes qualifying cohort, period and authority, subject to separate implementation amounts/conditions.
- Translation roles: RO-HOUSING-01-C001 — NARROW_ALL_LANGUAGES; source baseline and exact EN/UK-UA/RU divergence recorded in TRANSLATION_ALIGNMENT_01.csv.
- Watch roles: RO-W-005 was not linked to this source; W-004 is higher-education annual admission and uses Law 199 source e47fa493…
- Metadata changes: KEEP; none required.
- SourceId stability: SOURCE_ID_STABLE
- Blockers: none.

## ea337feb-2e12-5f3c-81ff-a32bf769e573
- Owner: RO-HOUSING-01; priority: P0
- Authority: Romanian Government
- Title: Romanian Government, HG 1178/2024, current consolidated implementation rules
- URL: https://legislatie.just.ro/Public/DetaliiDocument/295453
- Fetch result: official alternate/linked source inspected after stored Portal route returned an error
- Currentness: CURRENT; source action: KEEP
- Source summary: Portal record /295453 returned 502 in this access session; opened the official-gazette text of HG 1178/2024 and corroborating current IGSU programme information. HG sets 750 lei/month for a single person and 2,000 lei/month for a family, eligibility and application mechanisms, three-month limit, designated accommodation site capacity, monthly applications and conditions for months two/three. Amendments in 2025 are reflected in the available consolidated text. No blocker: underlying instrument and official administrative implementation were verified. Preserve Portal URL; no canonical change.
- Candidate ClaimIds: RO-HOUSING-01-C005, RO-HOUSING-01-C006, RO-HOUSING-01-C007
- Tuple decisions: RO-HOUSING-01-C005 — SUPPORTED; OUG 96/2024 art.3 and HG 1178/2024 establish the statutory cohort, flat-rate accommodation amount (750 lei/month single; 2,000 lei/month family), one three-consecutive-month period, eligibility/application conditions and exclusions.; RO-HOUSING-01-C006 — SUPPORTED; OUG 96/2024 and HG 1178/2024 prescribe the local-authority application route and months-two/three conditions, including work-related and children’s education conditions with specified exceptions and deadlines.; RO-HOUSING-01-C007 — SUPPORTED; The instruments call this a flat-rate accommodation sum, require application/eligibility and limit it to one three-month period; this is not an open-ended general rent subsidy or actual-rent reimbursement.
- Compare roles: housing-initial — SUPPORTED; Sets implementation conditions, available-place allocation, application route and operating mechanism for designated accommodation.; housing-financial-support — SUPPORTED; HG supplies current amount, eligibility, duration and application/condition detail.
- Translation roles: 
- Watch roles: RO-W-005 not source-linked.
- Metadata changes: KEEP; none required.
- SourceId stability: SOURCE_ID_STABLE
- Blockers: none.

## 0c3202ab-c7ce-52ac-88d4-d003597b4468
- Owner: RO-HOUSING-01; priority: P0
- Authority: Parliament of Romania
- Title: Parliament of Romania, Housing Law 114/1996, consolidated text
- URL: https://legislatie.just.ro/Public/DetaliiDocument/70658
- Fetch result: official alternate/linked source inspected after stored Portal route returned an error
- Currentness: CURRENT; source action: KEEP
- Source summary: Portal link /70658 returned 502; inspected current reproduced statutory text and official municipal-hosted law text. Housing Law 114/1996 art. 21 defines residential letting by written agreement between owner and tenant, contract content, term and obligations; arts. 22–25 address contract duration, party rights and termination. It does not create a TP-specific housing entitlement or residence-registration rule. Source remains relevant to private rental. No metadata change.
- Candidate ClaimIds: RO-HOUSING-01-C003
- Tuple decisions: RO-HOUSING-01-C003 — SUPPORTED; Housing Law 114/1996 art.21 treats residential letting as a written agreement between owner and tenant and specifies contract terms and party duties.
- Compare roles: housing-private-rental — SUPPORTED; Housing Law art.21 directly defines the written owner–tenant rental contract and its obligations.
- Translation roles: 
- Watch roles: No linked Watch row.
- Metadata changes: KEEP; none required.
- SourceId stability: SOURCE_ID_STABLE
- Blockers: none.

## 39adf310-6ef9-5678-b19c-5daa873b2209
- Owner: RO-HOUSING-01; priority: P1
- Authority: National Agency for Fiscal Administration (ANAF)
- Title: National Agency for Fiscal Administration (ANAF), Order 161/2025 amending the rental-contract registration procedure
- URL: https://static.anaf.ro/static/10/Anaf/legislatie/OPANAF_161_2025.pdf
- Fetch result: official page/PDF opened directly; relevant content reviewed
- Currentness: CURRENT; source action: KEEP
- Source summary: Opened official ANAF Order 161/2025 PDF, published 6 February 2025. It amends the registration procedure for lease contracts: taxpayers receiving income from personal-property letting (subject to stated exceptions) register the contract and modifications within 30 days with the competent tax body; where co-owned, the designated owner/usufructuary/legal holder performs registration. This is a tax filing duty of qualifying lessor, not tenant residence registration. URL and owner metadata verified.
- Candidate ClaimIds: RO-HOUSING-01-C003, RO-HOUSING-01-C004-S01, RO-HOUSING-01-C004-S02, RO-HOUSING-01-C004-S03
- Tuple decisions: RO-HOUSING-01-C003 — SUPPORTED; ANAF Order 161/2025 prescribes registration of a lease agreement between parties and identifies the lessor responsible for the tax filing; it is relevant to the separate landlord–tenant arrangement.; RO-HOUSING-01-C004-S01 — SUPPORTED; ANAF Order 161/2025 states the 30-day tax registration duty for qualifying personal-property lessors, assigns it to the lessor/designated owner rather than tenant, and defines exclusions/co-ownership handling.; RO-HOUSING-01-C004-S02 — SUPPORTED; ANAF Order 161/2025 states the 30-day tax registration duty for qualifying personal-property lessors, assigns it to the lessor/designated owner rather than tenant, and defines exclusions/co-ownership handling.; RO-HOUSING-01-C004-S03 — SUPPORTED; ANAF Order 161/2025 states the 30-day tax registration duty for qualifying personal-property lessors, assigns it to the lessor/designated owner rather than tenant, and defines exclusions/co-ownership handling.
- Compare roles: housing-private-rental — SUPPORTED; ANAF lease registration rule is directly relevant to landlord filing duties in a private rental.
- Translation roles: 
- Watch roles: No linked Watch row.
- Metadata changes: KEEP; none required.
- SourceId stability: SOURCE_ID_STABLE
- Blockers: none.

## 4b7f9f52-e26c-5d39-b274-8f0fedf97c8e
- Owner: RO-EDU-01; priority: P0
- Authority: Government of Romania
- Title: Government of Romania / Portal Legislativ, Emergency Ordinance no. 96/2024, consolidated text
- URL: https://legislatie.just.ro/Public/FormaPrintabila/00000G0QBYKFR4FB89A28GQQ3M77FREN
- Fetch result: official page/PDF opened directly; relevant content reviewed
- Currentness: CURRENT; source action: KEEP
- Source summary: Opened current consolidated OUG 96/2024 official text. Arts. 7–11 cover a defined group of minors from Ukraine, including minors with temporary-protection permits under the EU decision and those receiving Romanian humanitarian support; art. 7 excludes asylum/international-protection applicants/beneficiaries in its stated scope. Arts. 8–11 provide enrolment, school integration and inspectorate-level coordination/placement, with distinct decision role for unaccompanied minors. Owner-specific education role is valid despite shared URL.
- Candidate ClaimIds: RO-EDU-01-C001-S01, RO-EDU-01-C001-S02, RO-EDU-01-C006, RO-EDU-01-C007
- Tuple decisions: RO-EDU-01-C001-S01 — SUPPORTED; OUG 96/2024 art.7 and Order 5679/2024 audient procedure support school access for the covered minors from Ukraine while their study status is handled under the applicable process.; RO-EDU-01-C001-S02 — NOT_SUPPORTED; The source sets a defined access and placement procedure but does not establish the broad negative that a chosen school/class or immediate place is never guaranteed. PARTIAL_SUPPORT: it supports that placement is processed through the statutory route; do not state a broader guarantee rule without direct evidence.; RO-EDU-01-C006 — SUPPORTED; OUG 96/2024 art.7 expressly includes the specified minors from Ukraine with temporary-protection permits or receiving humanitarian support; scope is narrower and more precise than a generic “certain children” group.; RO-EDU-01-C007 — SUPPORTED; OUG 96/2024 arts.8–11 establish inspectorate-level coordination and placement decisions, especially the express decision role for unaccompanied minors; phrase the general role within the covered Ukraine-minor procedure.
- Compare roles: education-school-enrolment — SUPPORTED; OUG 96/2024 arts.7–11 establish access and inspectorate placement for covered minors.
- Translation roles: RO-EDU-01-C006 — NARROW_ALL_LANGUAGES; source cohort and exact EN/UK-UA/RU divergence recorded in TRANSLATION_ALIGNMENT_01.csv.
- Watch roles: No linked Watch row.
- Metadata changes: KEEP; none required.
- SourceId stability: SOURCE_ID_STABLE
- Blockers: none.

## b44d2e00-999a-521f-ad11-2d2d9f68b590
- Owner: RO-EDU-01; priority: P0
- Authority: Ministry of Education
- Title: Ministry of Education / Portal Legislativ, Ministerial Order no. 5679/2024 and annexed procedures
- URL: https://legislatie.just.ro/Public/FormaPrintabila/00000G04UCO6K29RV9L2UIMB92LZ8UVC
- Fetch result: official page/PDF opened directly; relevant content reviewed
- Currentness: CURRENT; source action: KEEP
- Source summary: Opened Order 5679/2024 and its linked official Portal procedure /286548. The inspectorate-level committee receives audient/enrolment requests, reviews them within up to five working days, decides placement institutions, communicates its decisions and monitors integration. It is a placement decision/coordination body, not merely a contact point. Procedure is for preschoolers/pupils from Ukraine in specified exceptional circumstances; forms may be submitted by parent/legal representative, adult pupil, or competent representative for unaccompanied minors. No universal admissions checklist.
- Candidate ClaimIds: none
- Tuple decisions: none
- Compare roles: education-school-enrolment — SUPPORTED; Order 5679/2024 procedure directly establishes application, audient enrolment, placement decisions and authority roles.
- Translation roles: 
- Watch roles: No linked Watch row.
- Metadata changes: KEEP; none required.
- SourceId stability: SOURCE_ID_STABLE
- Blockers: none.

## 0e8b831f-7fb1-583a-a330-d17999da2cd7
- Owner: RO-EDU-01; priority: P0
- Authority: Ministry of Education
- Title: Ministry of Education, Order no. 5726/2024 — Pre-university School Regulation (ROFUIP)
- URL: https://www.edu.ro/sites/default/files/_fi%C8%99iere/Legislatie/2024/OME_5726_2024_ROFUIP.pdf
- Fetch result: official page/PDF opened directly; relevant content reviewed
- Currentness: CURRENT; source action: UPDATE
- Source summary: Official Ministry PDF of Order 5726/2024 (ROFUIP) opened. It is the original framework regulation, effective from school year 2024–25; subsequent amendments include 2025 changes to foreign-pupil recognition/placement provisions. Retain the original act for provisions unchanged in the relevant scope, but update metadata/use to flag that current school enrolment and Romanian-language rules must be read with the current consolidated ROFUIP source 8ae492b9… and later amendments. No URL changed.
- Candidate ClaimIds: none
- Tuple decisions: none
- Compare roles: education-school-enrolment — SUPPORTED; ROFUIP base regulation addresses foreign-pupil enrolment and study recognition; use with the current consolidated version for later amendments.; education-pupil-language — SUPPORTED; The framework regulates Romanian initiation courses for foreign pupils who do not know Romanian; scope and later amendments must be respected.
- Translation roles: 
- Watch roles: RO-W-005 remains unlinked; school-year rules suggest event-driven monitoring if later tied to a claim.
- Metadata changes: UPDATE; scope/use note updated; no URL/canonical change.
- SourceId stability: SOURCE_ID_STABLE
- Blockers: none.

## 3ef0a209-7ace-5cc7-927b-65932e22cea5
- Owner: RO-EDU-01; priority: P0
- Authority: Ministry of Education
- Title: Ministry of Education / Portal Legislativ, Order no. 6129/2022 — adult Romanian initiation courses
- URL: https://www.edu.ro/sites/default/files/_fi%C8%99iere/Legislatie/2022/OM_6129_2022.pdf
- Fetch result: official page/PDF opened directly; relevant content reviewed
- Currentness: CURRENT; source action: UPDATE
- Source summary: Official Ministry Order 6129/2022 PDF opened. It establishes adult Romanian initiation-course methodology and fee handling for adults with international protection or residence rights and EU/EEA/Swiss citizens; free manuals for specified protection beneficiaries do not establish universally free tuition for all temporary-protection holders. The course methodology remains relevant, while course/evaluation amounts were separately set in 2026 by Order 4649/2026 (source 1244a27d…). Update metadata/use to state this instrument is read with current fee order; URL unchanged. Task-184 legacy candidate resolved as CURRENT/UPDATE.
- Candidate ClaimIds: RO-EDU-01-C002-S01, RO-EDU-01-C002-S02
- Tuple decisions: RO-EDU-01-C002-S01 — SUPPORTED; Order 6129/2022 distinguishes adult-course access and fee arrangements by legal category; Order 4649/2026 supplies current fee amounts for its defined adult residence-right cohort.; RO-EDU-01-C002-S02 — SUPPORTED; The cited course rules do not create a universal free-course entitlement for every TP holder; free manuals for a specified protection group are distinct from course tuition, and the fee order has its own defined cohort.
- Compare roles: education-pupil-language — NOT_SUPPORTED; Order 6129/2022 concerns adult Romanian initiation courses, not school-pupil language support.
- Translation roles: 
- Watch roles: No linked Watch row.
- Metadata changes: UPDATE; scope/use note updated; no URL/canonical change.
- SourceId stability: SOURCE_ID_STABLE
- Blockers: none.

## 1244a27d-7d6c-502f-b8f2-4cf68a5e7671
- Owner: RO-EDU-01; priority: P0
- Authority: Ministry of Education
- Title: Ministry of Education and Research, Order no. 4649/2026 on fees for adult Romanian-language initiation/evaluation
- URL: https://edu.ro/sites/default/files/2026-09/OM_4649_2026.pdf
- Fetch result: official page/PDF opened directly; relevant content reviewed
- Currentness: CURRENT; source action: KEEP
- Source summary: Opened official Ministry Order 4649/2026 PDF, dated 12 August 2026. It sets 5,217 lei for course plus language evaluation and 522 lei for evaluation alone for adults with residence rights and EU/EEA/Swiss citizens; it is not a pupil-language support instrument and the fee cohort must not be generalized to all TP beneficiaries. Current for 2026 fee propositions; URL and metadata verified.
- Candidate ClaimIds: none
- Tuple decisions: none
- Compare roles: education-pupil-language — NOT_SUPPORTED; Order 4649/2026 fixes adult course/evaluation fees and is not evidence of pupil language-support models, hours or duration.
- Translation roles: 
- Watch roles: No linked Watch row.
- Metadata changes: KEEP; none required.
- SourceId stability: SOURCE_ID_STABLE
- Blockers: none.

## e47fa493-a454-5f69-ade6-789e193c4d6f
- Owner: RO-EDU-01; priority: P0
- Authority: Parliament of Romania
- Title: Parliament of Romania / Portal Legislativ, Higher Education Law no. 199/2023, current text
- URL: https://legislatie.just.ro/Public/DetaliiDocument/271898
- Fetch result: official alternate/linked source inspected after stored Portal route returned an error
- Currentness: CURRENT; source action: KEEP
- Source summary: Portal Law 199/2023 link timed out in this session; official text was checked via the Ministry admission framework and official Portal source family. Higher Education Law governs university autonomy, recognition and admission frameworks; it does not itself prove a TP-specific free tuition guarantee. Current legal framework; annual admission rules and places need year-specific materials. No metadata change.
- Candidate ClaimIds: RO-EDU-01-C003, RO-EDU-01-C005
- Tuple decisions: RO-EDU-01-C003 — SUPPORTED; Law 199/2023 and the current admission framework require institutions to publish institution-level admission information and rules; the general statement is supported.; RO-EDU-01-C005 — SUPPORTED; Higher Education Law treats recognition/admission and institutional tuition arrangements as distinct legal questions; this supports keeping recognition, preparation and fees separate.
- Compare roles: education-higher-admission — SUPPORTED; Higher Education Law supplies general admission/autonomy and recognition framework, not TP-specific fee entitlement.
- Translation roles: 
- Watch roles: RO-W-004 annual admission update event; no fixed date.
- Metadata changes: KEEP; none required.
- SourceId stability: SOURCE_ID_STABLE
- Blockers: none.

## 69556406-3d64-5fb2-9485-1c2b0dfdea3b
- Owner: RO-EDU-01; priority: P0
- Authority: Ministry of Education
- Title: Ministry of Education, Order no. 3693/2024 — national higher-education admission framework
- URL: https://edu.ro/sites/default/files/fisiere%20articole/OM_3693_2024.pdf
- Fetch result: official page/PDF opened directly; relevant content reviewed
- Currentness: CURRENT; source action: KEEP
- Source summary: Opened official Ministry Order 3693/2024 PDF. It governs the national higher-education admission framework: universities establish and publish institution-specific admission rules, offer, criteria, application documents and calendars; institutions conduct admission competitions within approved capacity. It is a general framework, not a TP-specific funding entitlement, and annual/academic-year capacity details change.
- Candidate ClaimIds: none
- Tuple decisions: none
- Compare roles: education-higher-admission — SUPPORTED; Order 3693/2024 sets current general admission procedures and institution-level criteria/capacity.
- Translation roles: 
- Watch roles: No linked Watch row.
- Metadata changes: KEEP; none required.
- SourceId stability: SOURCE_ID_STABLE
- Blockers: none.

## 8ad9e863-a228-57e6-890a-3ac183eca778
- Owner: RO-EDU-01; priority: P0
- Authority: Ministry of Education
- Title: Ministry of Education / Portal Legislativ, Order no. 5655/2024 — admission of foreign citizens on fee-paying places
- URL: https://legislatie.just.ro/public/DetaliiDocument/286720
- Fetch result: official page/PDF opened directly; relevant content reviewed
- Currentness: CURRENT; source action: KEEP
- Source summary: Opened official Portal methodology under Order 5655/2024. It governs admission of foreign citizens to fee-paying places in foreign currency, sets eligibility and institution-specific selection rules, and is limited to its stated foreign-citizen/fee-paying track; it is not direct evidence of the status or tuition treatment of temporary-protection beneficiaries. Current for its defined route.
- Candidate ClaimIds: none
- Tuple decisions: none
- Compare roles: education-higher-admission — SUPPORTED; Order 5655/2024 applies to the separate foreign-citizen fee-paying route; relevant as a bounded comparator, not a TP fee rule.
- Translation roles: 
- Watch roles: No linked Watch row.
- Metadata changes: KEEP; none required.
- SourceId stability: SOURCE_ID_STABLE
- Blockers: none.

## 8ae492b9-7875-50c0-b7a2-4d6574e1afc8
- Owner: RO-EDU-01; priority: P1
- Authority: Romanian Ministry
- Title: Romanian Ministry of Education / Portal Legislativ, current consolidated Pre-university School Regulation (ROFUIP)
- URL: https://legislatie.just.ro/Public/FormaPrintabila/00000G1XW01S6XCJ1IP08ADVQDNKT2OX
- Fetch result: official page/PDF opened directly; relevant content reviewed
- Currentness: CURRENT; source action: KEEP
- Source summary: Opened official consolidated ROFUIP Portal text (current through amendments displayed in 2025). Art. 125 covers foreign pupils without documents, audient placement, study equivalence, and Romanian initiation courses for foreign pupils who do not know Romanian; it gives no universal exact lesson-hour guarantee. Relevant consolidated companion to the 2024 Ministry PDF and the 2025 amendment.
- Candidate ClaimIds: none
- Tuple decisions: none
- Compare roles: none
- Translation roles: 
- Watch roles: School-year/current-regulation changes may be monitored in future; no new row/date added.
- Metadata changes: KEEP; none required.
- SourceId stability: SOURCE_ID_STABLE
- Blockers: none.

## c2bee662-3b3f-5b94-803d-1e1f49184180
- Owner: RO-EDU-01; priority: P0
- Authority: Romanian Legislative Portal
- Title: Romanian Legislative Portal, OUG 100/2022 — annual state-funded higher-education places measure
- URL: https://legislatie.just.ro/Public/DetaliiDocumentAfis/256915
- Fetch result: official page/PDF opened directly; relevant content reviewed
- Currentness: CURRENT; source action: KEEP
- Source summary: Opened official Portal OUG 100/2022 and its annexed National Measures Plan. Art. 1(3) applies the plan during temporary protection and extensions; annex measure 13.2.2 directs annual state-funded university places for displaced persons from Ukraine who are beneficiaries of temporary protection and wish to continue studies. Separate measure 13.2.1’s 20% additional capacity is expressly limited to academic years 2021–22 and 2022–23 and must not be carried forward. Annual allocation does not state a personal admission or funded-seat guarantee. No metadata change.
- Candidate ClaimIds: RO-EDU-01-C003, RO-EDU-01-C004-S01, RO-EDU-01-C004-S02, RO-EDU-01-C004-S03, RO-EDU-01-C005
- Tuple decisions: RO-EDU-01-C003 — NOT_SUPPORTED; OUG 100/2022 is a Ukraine inclusion plan and does not establish the general proposition that universities publish annual admission requirements.; RO-EDU-01-C004-S01 — SUPPORTED; OUG 100/2022 annex measure 13.2.2 provides annual state-funded university places for people displaced from Ukraine who are beneficiaries of temporary protection and wish to continue studies; the plan applies during the TP period.; RO-EDU-01-C004-S02 — NOT_SUPPORTED; The plan identifies an annual measure/places but does not directly state that an individual admission or funded place is not guaranteed. A plan-level allocation is not sufficient evidence for this negative boundary.; RO-EDU-01-C004-S03 — NOT_SUPPORTED; The plan identifies an annual measure/places but does not directly state that an individual admission or funded place is not guaranteed. A plan-level allocation is not sufficient evidence for this negative boundary.; RO-EDU-01-C005 — SUPPORTED; OUG 100/2022 separately provides a plan-level state-funded-place measure; it does not merge recognition, language preparation or fee status into admission.
- Compare roles: none
- Translation roles: 
- Watch roles: No linked Watch row.
- Metadata changes: KEEP; none required.
- SourceId stability: SOURCE_ID_STABLE
- Blockers: none.
