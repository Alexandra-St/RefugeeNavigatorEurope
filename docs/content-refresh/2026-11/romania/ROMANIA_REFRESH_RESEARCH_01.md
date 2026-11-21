# Romania content-refresh initial research / baseline

As of 2026-11-20. This is repository baseline work only; no direct source research was performed.

## Article universe

- RO-EDU-01 — `bfd554ff-a41a-5338-ab3e-25bb3c827a73`; `education`; EN: Education and Romanian-Language Learning in Romania; languages en;ru;uk; sources/Core 10/10.
- RO-EMP-01 — `54197f49-594b-51d2-a40c-2fcded05f791`; `employment`; EN: Finding a Job in Romania; languages en;ru;uk; sources/Core 9/9.
- RO-EMP-02 — `61d13a69-6704-50ee-8b7b-e673a2828e65`; `employment`; EN: Work Authorization and Employee Rights in Romania; languages en;ru;uk; sources/Core 9/9.
- RO-HEALTH-01 — `be12596b-8657-5b23-a17c-dead174859f3`; `healthcare`; EN: Accessing Healthcare in Romania; languages en;ru;uk; sources/Core 6/6.
- RO-HOUSING-01 — `35bb6fe6-b0b9-5517-af04-e022c7105ec7`; `housing`; EN: Finding Accommodation in Romania; languages en;ru;uk; sources/Core 4/4.
- RO-SOC-01 — `97f02ab3-3352-597b-ad48-11ebf19993c2`; `social-assistance`; EN: Financial and Social Assistance in Romania; languages en;ru;uk; sources/Core 7/7.
- RO-TP-01 — `24d0ab3d-935c-50b7-8385-c1c54583ec51`; `temporary-protection`; EN: Temporary Protection in Romania: Applying and Maintaining Your Status; languages en;ru;uk; sources/Core 3/3.
- RO-TP-02 — `6b966a1f-335e-596a-9825-005d30ab23d4`; `temporary-protection`; EN: Residence Options Beyond Temporary Protection in Romania; languages en;ru;uk; sources/Core 8/8.

Articles: 8. Languages: 24/24 locale payloads. Production OfficialSources/Core owner pairs: 56/56.

## Claim inventory

Factual sentence-level claim candidates: 54; material 47; supporting 7; NumericDate YES 10. Temporal current/future/historical/transition: 50/1/0/3. Compound candidates: 26. Duplicate ClaimIds: 0. Existing machine-readable inactive claim rows: 0 found.

## Translation baseline

Material scope divergences flagged: 1. NumericDate divergences: 0. Missing proposition components flagged: 1. Extra propositions: 0. Temporal mismatches: 0. These are initial human-review flags, not source/content adjudication.

## Source / Core baseline

Production Romania sources/Core pairs: 56/56. Priority P0/P1/P2: 39/11/6. Currentness candidates: 53 current, 3 legacy, 0 broken/superseded/not-relevant/unclear. Same-owner canonical duplicates: 0. Cross-owner canonical URL groups: 2 (structural, not presumed errors). Core defects: 0 identified.

## Claim-source / coverage baseline

Candidate tuples: 108; all are PENDING_SOURCE_REVIEW. Claims with 0/1/multiple candidate sources: 0/16/38. Coverage REQUIRES_SOURCE_REVIEW: 54; NO_EXISTING_CANDIDATE: 0.

## Compare baseline

Aspects 24; effective production tuples 67; ownership-valid 67; invalid 0. Provisional dispositions: REVIEW_REQUIRED 11, REVISE_CANDIDATE 7, KEEP_CANDIDATE 6. All tuple support pending direct source review.

## Watch / high-risk

Watch candidates 5: RO-W-001, RO-W-002, RO-W-003, RO-W-004, RO-W-005. High-risk rows {len(high)}; NumericDate {sum(x["NumericDate"]=="YES" for x in high)}; source-review-required {len(high)}; zero-candidate high-risk {sum(not x["CandidateSourceIds"] for x in high)}.

## Source audit batches

- BATCH-01: Temporary protection / residence; sources 11; P0/P1/P2 7/2/2.
- BATCH-02: Employment / work / healthcare; sources 24; P0/P1/P2 14/6/4.
- BATCH-03: Housing / education; sources 14; P0/P1/P2 12/2/0.
- BATCH-04: Social support; sources 7; P0/P1/P2 6/1/0.

Assigned 56/56 sources; duplicate assignments 0; unassigned 0.

## Initial reopen candidates

- `RO-EDU-01-C002` (RO-EDU-01): Adult initiation courses follow different rules and fee categories; the sources do not establish that every temporary-protection holder can attend a course for free.
- `RO-EDU-01-C004` (RO-EDU-01): Romania’s annual plan also provides for a number of state-funded higher-education places for people from Ukraine under temporary protection who wish to continue university studies; that plan-level measure does not guarantee an individual admission or a funded place.
- `RO-EMP-02-C005` (RO-EMP-02): From 1 July 2026, the general minimum wage is 4,325 lei gross per month under HG 146/2026.
- `RO-EMP-02-C006` (RO-EMP-02): A separate statutory construction-sector minimum is 4,582 lei gross per month under OUG 156/2024, Article LXIX.
- `RO-HEALTH-01-C003` (RO-HEALTH-01): CASS may be due on income that is subject to the health contribution, including applicable employment income, under the Tax Code.
- `RO-HOUSING-01-C004` (RO-HOUSING-01): Under the applicable tax procedure, a qualifying lessor registers the rental contract with ANAF within 30 days; this is not a tenant filing duty, and the rule has defined scope.
- `RO-HOUSING-01-C005` (RO-HOUSING-01): For the statutory cohort covered by the Ukraine support programme, accommodation support is limited to one period of three consecutive months: 750 lei per month for a single person or 2,000 lei per month for a family.
- `RO-TP-01-C004` (RO-TP-01): The EU decision’s military-obligation condition for covered men applies from 5 August 2026, with a narrow exception for people continuously protected in the same Member State on 4 August 2026; see.
- `RO-TP-01-C007` (RO-TP-01): Published guidance does not establish a universal appointment requirement, complete document checklist, or processing time.
- `RO-TP-02-C001` (RO-TP-02): Romania has purpose-specific residence routes, including work, study, family reunification and long-term residence, each with its own eligibility and filing rules.
- `RO-TP-02-C009` (RO-TP-02): The effect of a pending application on work rights, and whether prior TP time counts toward long-term residence, remain unresolved.

## Temporal reopen candidates

- `RO-EDU-01-C004` (RO-EDU-01): Romania’s annual plan also provides for a number of state-funded higher-education places for people from Ukraine under temporary protection who wish to continue university studies; that plan-level measure does not guarantee an individual admission or a funded place.
- `RO-EMP-02-C001` (RO-EMP-02): People in covered current or former temporary-protection categories may work under the specific statutory exception during the applicable temporary-protection period.
- `RO-EMP-02-C005` (RO-EMP-02): From 1 July 2026, the general minimum wage is 4,325 lei gross per month under HG 146/2026.
- `RO-EMP-02-C006` (RO-EMP-02): A separate statutory construction-sector minimum is 4,582 lei gross per month under OUG 156/2024, Article LXIX.
- `RO-HEALTH-01-C005` (RO-HEALTH-01): Call 112 for an immediate threat to life.
- `RO-HOUSING-01-C004` (RO-HOUSING-01): Under the applicable tax procedure, a qualifying lessor registers the rental contract with ANAF within 30 days; this is not a tenant filing duty, and the rule has defined scope.
- `RO-HOUSING-01-C005` (RO-HOUSING-01): For the statutory cohort covered by the Ukraine support programme, accommodation support is limited to one period of three consecutive months: 750 lei per month for a single person or 2,000 lei per month for a family.
- `RO-HOUSING-01-C006` (RO-HOUSING-01): Application follows the prescribed local-authority route, and conditions apply in months two and three, including rules for work-capable adults and children’s education subject to exceptions.
- `RO-TP-01-C004` (RO-TP-01): The EU decision’s military-obligation condition for covered men applies from 5 August 2026, with a narrow exception for people continuously protected in the same Member State on 4 August 2026; see.
- `RO-TP-01-C008` (RO-TP-01): The EU temporary-protection scheme runs through 4 March 2028; the extension applies from 5 March 2027.
- `RO-TP-02-C004` (RO-TP-02): A specific work-purpose residence route is established for current and former temporary-protection beneficiaries during the applicable statutory temporary-protection period.

## Known blockers / next phase

- Source support is not adjudicated; every claim-source tuple remains pending source review.
- Currentness is structural and provisional; no URLs were fetched.
- Initial translation review flagged housing eligibility/cohort scope and the explicit temporary-protection child cohort in education for manual comparison.
- Exact rates, statutory cohort/conditions, negative legal boundaries, emergency-care and residence-transition assertions require direct source audit.
- Structural certification should validate sentence-level atomicity and split the flagged compound candidates before source conclusions.

## Task 185 structural staging

- Atomic staged claims: 90. Split parents: 22. Compound candidates individually reviewed: 26/26.
- Source audit status: NOT YET PERFORMED. Candidate tuples are pending only.
- Translation child alignment remains open for human verification; see `TRANSLATION_ALIGNMENT_01.csv` and `STRUCTURAL_CERTIFICATION_01.md`.

## Task 185 certification status

- Structural stage: BLOCKED; see `STRUCTURAL_CERTIFICATION_01.md`. The child-level translation rows remain review-required.
- A completeness scan found three factual sentences not mapped to active claims: RO-EDU-01 paragraph 1 sentences 1–2 and RO-EMP-02 paragraph 1 sentence 3.
- Source adjudication: NOT YET PERFORMED. Existing tuples remain candidate-only and pending.

## Task 186 structural repair

- Task 185 blocked status is preserved historically in `STRUCTURAL_CERTIFICATION_01.md`; the Task 186 recertification is complete.
- Final ACTIVE claims: 94. Candidate tuples: 130, all pending source review.
- Two translation-scope questions remain mapped to active ClaimIds: `RO-EDU-01-C006` and `RO-HOUSING-01-C001`.
- Source adjudication: NOT YET PERFORMED.
