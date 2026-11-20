# Task 158 — Evidence-gap Batch 2: employment and healthcare

As of 20 November 2026. The 25 targeted evidence gaps are closed; the healthcare decision-maker reopen is resolved. No production files, tests, other-country files, commits, pushes, CI, or Docker runs were touched.

## Resolution totals

| Resolution action | Claims |
|---|---:|
| Existing-source support | 2 |
| New-source support | 5 |
| Rewritten to supported claims | 13 |
| Temporal reclassification | 0 |
| Removed unsupported claims | 5 |
| Merged as redundant | 0 |
| Blocked | 0 |

All 25 targeted gap rows have a final disposition. 9 SE-EMP-01 claims, 9 SE-EMP-02 claims, and 7 healthcare claims were reviewed. Five claims were removed; 20 retained target claims are now covered.

The row-by-row record is [EVIDENCE_GAP_BATCH_02_EMPLOYMENT_HEALTHCARE.csv](EVIDENCE_GAP_BATCH_02_EMPLOYMENT_HEALTHCARE.csv).

## Healthcare C007 reopen

- **Original proposition:** A clinician assesses the individual situation.
- **Reopen reason:** “Clinician” may narrow or misstate the responsible decision-maker and the proposition was broader than source wording across individual cases.
- **Official evidence:** Socialstyrelsen’s Ukraine healthcare-rights FAQ attributes the case-by-case assessment to the responsible healthcare provider for care that cannot be postponed.
- **Action:** REWRITE_TO_SUPPORTED_CLAIM; the reopen is cleared.
- **Final proposition:** The responsible healthcare provider assesses case by case whether care cannot be postponed.
- **TemporalClass:** CURRENT. **Materiality:** SUPPORTING. **NumericDate:** NO.
- **SourceId:** `61666e4b-67f1-4ca3-894a-3717737eaf40`. **Coverage:** COVERED.

## New official source records

Three source additions are staged; all owner IDs match their articles and no same-owner canonical-URL duplicates were found. Two support claim-level Core tuples; the wage source is used for Compare only.

| SourceId | Owner | URL | Stable key |
|---|---|---|---|
| `43dfe833-392f-5c17-8a44-0b4afe18213c` | SE-EMP-01 (`a218013b-3a77-4ba2-829a-d724689837f2`) | https://arbetsformedlingen.se/other-languages/english-engelska/additional-support/support-a-z/introduction-programme | `refugeenavigator:official-source:a218013b-3a77-4ba2-829a-d724689837f2:https://arbetsformedlingen.se/other-languages/english-engelska/additional-support/support-a-z/introduction-programme` |
| `7bc8c19f-a8da-56a7-b243-ea0a4e6c528b` | SE-HEALTH-01 (`2234b91f-4021-40b2-b9d5-a029a6b533f2`) | https://www.1177.se/patientavgifter/ | `refugeenavigator:official-source:2234b91f-4021-40b2-b9d5-a029a6b533f2:https://www.1177.se/patientavgifter` |
| `9fa47f80-b71c-5bab-acf4-c776609f1ce0` | SE-EMP-02 (`55949af5-b4fe-404e-bcb7-6ebb37131bab`) | https://www.av.se/en/work-environment-work-and-inspections/foreign-labour-in-sweden/posting-foreign-labour-in-sweden/the-rights-of-posted-workers/ | `refugeenavigator:official-source:55949af5-b4fe-404e-bcb7-6ebb37131bab:https://www.av.se/en/work-environment-work-and-inspections/foreign-labour-in-sweden/posting-foreign-labour-in-sweden/the-rights-of-posted-workers` |

All three UUIDs recompute as UUIDv5 with `NAMESPACE_URL` and the stable keys shown. The introduction-programme page lists municipal adult education as a potential study route and lists validation, civic orientation, and job-search assistance. 1177’s national patient-fee guidance states that regions set many patient fees and that the applicable fee is the one for the region where care is provided.

## Employment evidence and minimum-pay wording

- Vacancy guidance now states what Arbetsförmedlingen actually recommends: Platsbanken, LinkedIn, or a jobseeker’s network. It no longer asserts that every listing is not an offer.
- Planning-meeting preparation is narrowed to bringing certificates and grades that show previous work and studies.
- UHR wording now describes its assessment of how a completed foreign qualification corresponds to Sweden’s education system; regulated-profession authorization is kept separate.
- The introduction-programme claims are tied to Arbetsförmedlingen’s official programme description and do not imply universal eligibility.
- Written-employment-information claims are limited to material terms and the Act’s collective-agreement variation provisions. The employer notification claim is limited to cases where the Tax Agency rules require notification.
- The Working Hours Act claim now states that qualifying collective agreements can replace the Act in whole or in part, supported by §3 of the Act itself. Annual-leave pay is tied to leave pay accrued during the qualifying year.
- **Minimum pay:** Arbetsmiljöverket states Sweden does not have a statutory minimum wage and that wages are often regulated by collective agreements. The Compare proposition is revised to those exact statements and directs users to the terms for their specific job. It makes no claim that every workplace is covered, and does not assert a universal statutory wage floor or any posted-worker rate.

## Healthcare evidence

The fee claim is narrowed to the regional patient-fee rule. A vague regional-arrangements caveat is removed as redundant with certified healthcare-access guidance. The new 1177 source supports regional variation and fees charged in the region where care is provided. The 1177 review copy now describes advice/information and region-organized presentation. The official posted-worker rights page states there is no statutory minimum wage and wages are often regulated by collective agreements. Registration claim C015 retains its numeric 12-month scope and says later registration depends on statutory conditions and an individual decision; it does not imply automatic registration or a change in immigration status.

The healthcare-urgent Compare wording retains the adult/unregistered scope, the specified additional services, and 1177/112 routes. It assigns the case-by-case assessment to the responsible healthcare provider and removes the unsupported generalization about regional language-service availability.

## Claims, coverage, and Core graph

Active claims after the batch: 274; by article: SE-TP-01 41, SE-TP-02 28, SE-EMP-01 26, SE-EMP-02 25, SE-HEALTH-01 21, SE-HOUSING-01 25, SE-EDU-01 51, SE-SOC-01 57. Material 176; supporting 98; NumericDate YES 45; TemporalClass CURRENT 257, FUTURE 3, HISTORICAL 5, TRANSITION 9.

Active coverage: COVERED 210, COVERED_MULTIPLE 50, UNSUPPORTED 14, REQUIRES_SOURCE_REVIEW 0. Targeted retained claims: 20 covered (all single-source), 0 covered-multiple, 0 unsupported. The 5 removed claims are inactive and omitted from active coverage totals.

Task 157’s effective Core graph was 633 tuples (293 SUPPORTED, 340 NOT_SUPPORTED, 0 unresolved). Task 158 adds 20 exact supported claim/source tuples and retires 33 superseded proposition/source tuples: **620 effective tuples (313 SUPPORTED, 307 NOT_SUPPORTED, 0 unresolved)**. Net delta −13. The 33 historical NOT_SUPPORTED decisions remain in the source-link audit with retirement notes. The earlier NOT_SUPPORTED audit tuples remain historical; they are not silently overwritten. Core dangling source IDs and wrong-owner bindings remain 0.

## Compare certification

The five requested aspects are all certified in staging:

| Aspect | Disposition | Source-certified | Effective SourceIds |
|---|---|---|---|
| employment-vacancies | KEEP | Yes | 4801dab3-29e2-446e-934f-3970ffa5f772; 6f9b2ed9-0476-4981-8f5a-1762613fe963 |
| employment-institutions | KEEP | Yes | 3afb974c-1420-4905-aafb-c2b83c2fcfaf; 3b4700be-7f2b-402d-ba62-0848efd05c18; 43dfe833-392f-5c17-8a44-0b4afe18213c |
| work-employer-duties | REVISE | Yes | 78f75cc3-a5d4-497f-9d4a-de6999d8000c; 3d4d281b-b267-4e22-a282-8d736b00ebd2; 0e6cf194-dcfa-48d2-b91e-1c3d0334c782 |
| work-minimum-pay | REVISE | Yes | 20cb6a67-b3ac-4505-8736-2a4c1edfb742; 9fa47f80-b71c-5bab-acf4-c776609f1ce0 |
| healthcare-urgent | REVISE | Yes | 61666e4b-67f1-4ca3-894a-3717737eaf40; d9f29509-b6d8-4df9-ae03-15e8b176c741; 275c1c14-3caa-4343-aa7e-67689edfa9bb |

Final revised minimum-pay proposition: “Sweden does not have a statutory minimum wage. Wages are often regulated by collective agreements. Check the agreement and employment terms that apply to the particular job.”

Final revised healthcare-urgent proposition: “An adult with temporary protection who is not population-registered can access care that cannot be postponed and specified other services. The responsible healthcare provider assesses case by case whether care can be postponed. Use 1177 for healthcare advice and call 112 for urgent emergencies.”

The Compare graph grows from 59 to 61 tuples (61 SUPPORTED, 0 NOT_SUPPORTED, 0 unresolved); dangling Compare SourceIds 0. Cumulative certified aspects rise from 17/24 to 22/24. The remaining uncertified aspects are housing-financial-support and education-higher-admission.

## Watch, high risk, translations, and remaining gaps

No existing Watch row was affected and no new rate, implementation date, or scheduled change was added. W-SE-01/02/03/05/06 remain APPROVE; W-SE-04 remains EDIT. The earliest future legal/effective trigger remains 2027-01-01; the earliest maintenance pre-check remains 2026-12-01.

High-risk target rows in the register: 3; source-certified retained rows 3, removed/merged rows 0, blocked 0. Target translation-alignment rows: 25; all rows are marked EN/UK/RU aligned with no material or numeric/date divergence and no missing proposition. The review copies for SE-EMP-01, SE-EMP-02, and SE-HEALTH-01 were updated in all three languages.

Remaining evidence-gap queue: 14; P0 9, P1 5, P2 0. These are the out-of-scope housing, education, and social claims; Task 158 introduces no new blocker.

Source actions: KEEP 67, UPDATE 0, RETAIN_HISTORICAL 1, REMOVE 1, ADD 4. Planned Sweden sources: 69 production − 1 REMOVE + 4 ADD = 72. Preliminary Core binding plan: KEEP_CANDIDATE 68, ADD_CANDIDATE 3, REMOVE_CANDIDATE 1. The additional Compare-only wage source is not a Core binding candidate. Existing action corrections: 0. New source owner validation: 3/3. Same-owner canonical URL duplicates among additions: 0.

Structural counts from the active ledger: active compounds 0; active non-propositional 0; duplicate active ClaimIds 0; orphan lineage rows 0. Open claim reopens 0; temporal reopen candidates 0; no active claim relies solely on LEGACY or RETAIN_HISTORICAL evidence. Materiality totals sum to active claims and temporal totals sum to active claims.

## Verdict

**SWEDEN EVIDENCE GAP BATCH 2 COMPLETE**


### Retired effective Core pairs

The following 33 old proposition/source pairs were retired from the effective graph because their claim wording was rewritten or removed. Their NOT_SUPPORTED decisions remain in `SOURCE_CLAIM_LINK_AUDIT_01.csv` as historical audit evidence.

- `SE-EMP-01-C002` — `4801dab3-29e2-446e-934f-3970ffa5f772`
- `SE-EMP-01-C009` — `3afb974c-1420-4905-aafb-c2b83c2fcfaf`
- `SE-EMP-01-C010` — `3b4700be-7f2b-402d-ba62-0848efd05c18`
- `SE-EMP-01-C027` — `b6f2e260-476a-48af-8d0e-7bb24b3d76cd`
- `SE-EMP-01-C028` — `3afb974c-1420-4905-aafb-c2b83c2fcfaf`
- `SE-EMP-01-C029` — `3afb974c-1420-4905-aafb-c2b83c2fcfaf`
- `SE-EMP-01-C031` — `6f9b2ed9-0476-4981-8f5a-1762613fe963`
- `SE-EMP-01-C018` — `4801dab3-29e2-446e-934f-3970ffa5f772`
- `SE-EMP-01-C020` — `b6f2e260-476a-48af-8d0e-7bb24b3d76cd`
- `SE-EMP-02-C008` — `1809031a-2cc5-49ae-b6ed-595c9a675b83`
- `SE-EMP-02-C009` — `3d4d281b-b267-4e22-a282-8d736b00ebd2`
- `SE-EMP-02-C010` — `20cb6a67-b3ac-4505-8736-2a4c1edfb742`
- `SE-EMP-02-C030` — `20cb6a67-b3ac-4505-8736-2a4c1edfb742`
- `SE-EMP-02-C032` — `0d4b84f3-690b-4dc4-a899-69068c7b2c28`
- `SE-EMP-02-C033` — `3d4d281b-b267-4e22-a282-8d736b00ebd2`
- `SE-HEALTH-01-C005` — `275c1c14-3caa-4343-aa7e-67689edfa9bb`
- `SE-HEALTH-01-C007` — `275c1c14-3caa-4343-aa7e-67689edfa9bb`
- `SE-HEALTH-01-C010` — `3b0a97d1-884f-49ac-b3a2-5571780a342d`
- `SE-HEALTH-01-C010` — `da69130e-5b7e-4087-b061-f4bea7c3d384`
- `SE-HEALTH-01-C010` — `4f474221-b476-49e3-a66b-7651d6ae9a2f`
- `SE-HEALTH-01-C015` — `da69130e-5b7e-4087-b061-f4bea7c3d384`
- `SE-EMP-02-C036` — `1809031a-2cc5-49ae-b6ed-595c9a675b83`
- `SE-EMP-02-C036` — `0e6cf194-dcfa-48d2-b91e-1c3d0334c782`
- `SE-EMP-02-C037` — `0e6cf194-dcfa-48d2-b91e-1c3d0334c782`
- `SE-EMP-02-C037` — `1809031a-2cc5-49ae-b6ed-595c9a675b83`
- `SE-EMP-02-C039` — `20cb6a67-b3ac-4505-8736-2a4c1edfb742`
- `SE-EMP-02-C039` — `1809031a-2cc5-49ae-b6ed-595c9a675b83`
- `SE-HEALTH-01-C023` — `da69130e-5b7e-4087-b061-f4bea7c3d384`
- `SE-HEALTH-01-C023` — `4f474221-b476-49e3-a66b-7651d6ae9a2f`
- `SE-HEALTH-01-C024` — `3b0a97d1-884f-49ac-b3a2-5571780a342d`
- `SE-HEALTH-01-C024` — `da69130e-5b7e-4087-b061-f4bea7c3d384`
- `SE-HEALTH-01-C028` — `3b0a97d1-884f-49ac-b3a2-5571780a342d`
- `SE-HEALTH-01-C028` — `d9f29509-b6d8-4df9-ae03-15e8b176c741`
