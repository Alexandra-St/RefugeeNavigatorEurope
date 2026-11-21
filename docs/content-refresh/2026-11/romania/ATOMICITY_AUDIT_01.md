# Romania atomicity audit — Task 185

Reviewed from the Task 184 sentence-level claim baseline. No production text or sources were changed.

- Compound candidates reviewed: 26/26.
- SPLIT: 22.
- ATOMIC_AFTER_REVIEW: 4.
- Final staged ACTIVE claims at this pass: 90.

Each row below records the original parent and, where split, deterministic `-S##` child IDs and child propositions. See `CLAIM_LEDGER_01.csv` for full fields and lineage.

## RO-EDU-01-C001 — SPLIT

Original proposition: A child may attend as an audient while prior studies are assessed; this does not guarantee a chosen school, class or immediate place.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-EDU-01-C001-S01` — A child may attend as an audient while prior studies are assessed.
- `RO-EDU-01-C001-S02` — This does not guarantee a chosen school, class or immediate place.

## RO-EDU-01-C002 — SPLIT

Original proposition: Adult initiation courses follow different rules and fee categories; the sources do not establish that every temporary-protection holder can attend a course for free.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-EDU-01-C002-S01` — Adult initiation courses follow different rules and fee categories.
- `RO-EDU-01-C002-S02` — The cited sources do not establish that every temporary-protection holder can attend a course for free.

## RO-EDU-01-C004 — SPLIT

Original proposition: Romania’s annual plan also provides for a number of state-funded higher-education places for people from Ukraine under temporary protection who wish to continue university studies; that plan-level measure does not guarantee an individual admission or a funded place.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-EDU-01-C004-S01` — Romania’s annual plan provides a number of state-funded higher-education places for people from Ukraine under temporary protection who wish to continue university studies.
- `RO-EDU-01-C004-S02` — The plan-level measure does not guarantee an individual admission.
- `RO-EDU-01-C004-S03` — The plan-level measure does not guarantee an individual funded place.

## RO-EMP-01-C001 — SPLIT

Original proposition: Listings change, and applying does not guarantee an interview or job.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-EMP-01-C001-S01` — Job listings change over time.
- `RO-EMP-01-C001-S02` — Applying for a listed vacancy does not guarantee an interview.
- `RO-EMP-01-C001-S03` — Applying for a listed vacancy does not guarantee a job.

## RO-EMP-01-C002 — ATOMIC_AFTER_REVIEW

Original proposition: EURES is a separate European channel for jobseekers to search vacancies and create a profile.
Reason: One channel proposition with two coordinated functions; the trailing “see” was citation navigation, not a claim.

## RO-EMP-02-C002 — SPLIT

Original proposition: This work exception does not itself grant or replace a residence permit, and it does not establish a right to work after the relevant period ends.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-EMP-02-C002-S01` — The work exception does not itself grant or replace a residence permit.
- `RO-EMP-02-C002-S02` — The work exception does not establish a right to work after the relevant period ends.

## RO-EMP-02-C003 — SPLIT

Original proposition: Romanian employment rules cover written employment terms, pay, working time and leave under the applicable Labour Code; workplace safety duties are set separately by occupational-safety law.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-EMP-02-C003-S01` — The Labour Code covers written employment terms, pay, working time and leave.
- `RO-EMP-02-C003-S02` — Occupational-safety law separately sets workplace safety duties.

## RO-EMP-02-C004 — SPLIT

Original proposition: A collective agreement may apply where the legal and workplace conditions are met, but it does not cover every worker automatically.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-EMP-02-C004-S01` — A collective agreement may apply when legal and workplace conditions are met.
- `RO-EMP-02-C004-S02` — A collective agreement does not automatically cover every worker.

## RO-EMP-02-C007 — SPLIT

Original proposition: Gross pay is before deductions, not take-home pay; sector scope and rates are date-sensitive, so check the current rules for the job.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-EMP-02-C007-S01` — Gross pay is before deductions.
- `RO-EMP-02-C007-S02` — Gross pay is not take-home pay.
- `RO-EMP-02-C007-S03` — Sector scope and rates are date-sensitive.

## RO-HEALTH-01-C002 — SPLIT

Original proposition: This special package is conceptually separate from ordinary insured status and contribution duties; the applicable scope depends on the legal route.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-HEALTH-01-C002-S01` — The special Ukraine-related healthcare package is separate from ordinary insured status and contribution duties.
- `RO-HEALTH-01-C002-S02` — The applicable package scope depends on the legal route.

## RO-HEALTH-01-C004 — SPLIT

Original proposition: That liability alone does not automatically end the special Ukraine-related healthcare package or establish ordinary insured classification; employment by itself does not prove that classification.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-HEALTH-01-C004-S01` — CASS liability alone does not automatically end the special Ukraine-related healthcare package.
- `RO-HEALTH-01-C004-S02` — CASS liability alone does not establish ordinary insured classification.
- `RO-HEALTH-01-C004-S03` — Employment by itself does not prove ordinary insured classification.

## RO-HEALTH-01-C006 — ATOMIC_AFTER_REVIEW

Original proposition: Routine care follows separate rules; ask the insurer or provider which package, documents and payment rules apply.
Reason: One factual proposition (“routine care follows separate rules”); remainder is non-factual user advice.

## RO-HOUSING-01-C002 — SPLIT

Original proposition: Placement depends on the applicable programme, current capacity and local arrangements; it does not guarantee a particular municipality, housing type or length of stay.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-HOUSING-01-C002-S01` — Placement depends on the applicable programme, current capacity and local arrangements.
- `RO-HOUSING-01-C002-S02` — Placement does not guarantee a particular municipality.
- `RO-HOUSING-01-C002-S03` — Placement does not guarantee a particular housing type.
- `RO-HOUSING-01-C002-S04` — Placement does not guarantee a particular length of stay.

## RO-HOUSING-01-C004 — SPLIT

Original proposition: Under the applicable tax procedure, a qualifying lessor registers the rental contract with ANAF within 30 days; this is not a tenant filing duty, and the rule has defined scope.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-HOUSING-01-C004-S01` — A qualifying lessor must register the rental contract with ANAF within 30 days under the applicable tax procedure.
- `RO-HOUSING-01-C004-S02` — The registration duty is not a tenant filing duty.
- `RO-HOUSING-01-C004-S03` — The registration rule has a defined scope.

## RO-SOC-01-C002 — SPLIT

Original proposition: Each has separate conditions; temporary protection alone does not automatically establish eligibility or payment.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-SOC-01-C002-S01` — Each listed benefit has separate conditions.
- `RO-SOC-01-C002-S02` — Temporary-protection status alone does not automatically establish eligibility for a listed benefit.
- `RO-SOC-01-C002-S03` — Temporary-protection status alone does not automatically establish payment of a listed benefit.

## RO-SOC-01-C003 — SPLIT

Original proposition: VMI is means-tested and requires an application and assessment through the responsible local social-assistance authority.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-SOC-01-C003-S01` — VMI is means-tested.
- `RO-SOC-01-C003-S02` — A VMI applicant must apply.
- `RO-SOC-01-C003-S03` — The responsible local social-assistance authority assesses VMI applications.

## RO-SOC-01-C006 — ATOMIC_AFTER_REVIEW

Original proposition: Child and family benefits have separate conditions.
Reason: One proposition: child and family benefits have separate conditions.

## RO-SOC-01-C007 — SPLIT

Original proposition: School attendance alone does not establish eligibility, and neither an application nor temporary-protection status guarantees payment.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-SOC-01-C007-S01` — School attendance alone does not establish eligibility for child or family benefits.
- `RO-SOC-01-C007-S02` — An application alone does not guarantee payment.
- `RO-SOC-01-C007-S03` — Temporary-protection status alone does not guarantee payment.

## RO-TP-01-C003 — SPLIT

Original proposition: A separate cohort includes third-country nationals or stateless people who held valid permanent residence in Ukraine and cannot return safely and durably; Romania also provides for additional national categories.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-TP-01-C003-S01` — A separate temporary-protection cohort includes third-country nationals or stateless people who held valid permanent residence in Ukraine and cannot return safely and durably.
- `RO-TP-01-C003-S02` — Romania provides for additional national temporary-protection categories.

## RO-TP-01-C004 — SPLIT

Original proposition: The EU decision’s military-obligation condition for covered men applies from 5 August 2026, with a narrow exception for people continuously protected in the same Member State on 4 August 2026; see.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-TP-01-C004-S01` — The EU decision’s military-obligation condition applies from 5 August 2026 to covered men.
- `RO-TP-01-C004-S02` — A narrow exception applies to people continuously protected in the same Member State on 4 August 2026.

## RO-TP-01-C006 — SPLIT

Original proposition: The process includes in-person steps and a photograph; IGI issues the residence permit free of charge.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-TP-01-C006-S01` — The temporary-protection process includes in-person steps.
- `RO-TP-01-C006-S02` — The temporary-protection process includes a photograph.
- `RO-TP-01-C006-S03` — IGI issues the residence permit free of charge.

## RO-TP-01-C008 — SPLIT

Original proposition: The EU temporary-protection scheme runs through 4 March 2028; the extension applies from 5 March 2027.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-TP-01-C008-S01` — The EU temporary-protection scheme runs through 4 March 2028.
- `RO-TP-01-C008-S02` — The extension applies from 5 March 2027.

## RO-TP-02-C002 — ATOMIC_AFTER_REVIEW

Original proposition: Ordinary visa and residence procedures are described in the cited official sources.
Reason: One proposition: ordinary visa/residence procedures are described by the cited authorities; the source list is not a separate proposition.

## RO-TP-02-C005 — SPLIT

Original proposition: For this route, the law provides an exemption from the long-stay employment visa; relevant residence, employer and substantive work conditions still apply.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-TP-02-C005-S01` — The law exempts this work-purpose residence route from the long-stay employment visa.
- `RO-TP-02-C005-S02` — Relevant residence conditions still apply to this route.
- `RO-TP-02-C005-S03` — Relevant employer conditions still apply to this route.
- `RO-TP-02-C005-S04` — Substantive work conditions still apply to this route.

## RO-TP-02-C006 — SPLIT

Original proposition: This is not a general conversion mechanism, and filing does not guarantee approval.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-TP-02-C006-S01` — This route is not a general conversion mechanism.
- `RO-TP-02-C006-S02` — Filing under the route does not guarantee approval.

## RO-TP-02-C007 — SPLIT

Original proposition: The reviewed official sources establish ordinary study, family and other residence procedures but do not establish a general direct in-country transition from temporary protection for those purposes; this evidence boundary does not mean such a transition is prohibited.
Reason: Contains separately verifiable rules, limits, conditions, or effects; split to isolate each evidence unit.

Children:
- `RO-TP-02-C007-S01` — The reviewed official sources establish ordinary study, family and other residence procedures.
- `RO-TP-02-C007-S02` — The reviewed official sources do not establish a general direct in-country transition from temporary protection for those purposes.
- `RO-TP-02-C007-S03` — The evidence boundary does not establish that such a transition is prohibited.

## Certification note

Atomicity staging is recorded, but the structural certification remains blocked: three factual source propositions are not represented by active claim rows, original source-sentence ordinals need reconciliation, and child-level UK-UA/RU proposition spans remain unverified.

## Task 186 — compound discovered in Task 185

### RO-EMP-02-C008 — SPLIT

Original: Employer-side formalities and the conditions for the particular job still apply.

- `RO-EMP-02-C008-S01` — Employer-side formalities still apply to the work route. Candidate source: `49ae3d06-a9a3-5319-bf00-520c542d44c2`.
- `RO-EMP-02-C008-S02` — Conditions specific to the particular job still apply. Candidate source: `643a3def-a38f-59f5-b83e-c09284188c61`.

Recorded as `DISCOVERED_IN_TASK_185`; parent is inactive and children are active.

## Task 186 final status

The additional Task-185 discovery `RO-EMP-02-C008` is split into `RO-EMP-02-C008-S01` and `RO-EMP-02-C008-S02`. All 27 compound candidates across Tasks 185–186 were reviewed (23 split, 4 atomic after review). Structural certification passed for source-audit handoff; source support is pending.
