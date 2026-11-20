# Task 153 — Sweden Source Audit Batch 2: Employment + Healthcare

As of 19 November 2026. Official-source audit and audit-register updates only; production was left untouched.

## Batch and source outcomes

The exact owner-based Batch 2 set contains 22 sources: P0 13, P1 9, P2 0. Every URL was opened and reviewed against its claim scope. Status: CURRENT 22, LEGACY 0, UNCLEAR 0, BROKEN 0. Actions: KEEP 21, UPDATE 0, RETAIN_HISTORICAL 0, REMOVE 1. No successor URL was needed.

The one staged REMOVE is `f241d32f-d41c-46f1-90c2-8d61eb4b2aea` (Arbetsförmedlingen EURES job-search overview). It is current and accessible but no active claim tuple, Compare tuple, or review-copy proposition uses it. All other sources remain useful for a current claim or Compare component. No source action changed production bindings.

## C032 prior-state repair

C032 now has a SUPPORTED tuple from current source `af0b44aa-8339-402f-a993-bb91ff578fe4`, a COVERED state and a cleared `EvidenceGap=NO` high-risk record. The persisted 2025 notice tuple remains NOT_SUPPORTED and the notice remains historical. After removing resolved C032, the corrected cumulative Batch-1 high-risk evidence-gap count is 12: SE-TP-01-C003, C004, C005, C008, C014, C015, C020, C021, C022, C030, C031 and C033. C032 is not reopened.

## Core tuples and claim coverage

Batch 2 reviewed 152 tuples: 65 SUPPORTED, 87 NOT_SUPPORTED, 0 pending. Added 13 exact source/claim pairs using existing inventory records: Socialstyrelsen (`61666e4b-67f1-4ca3-894a-3717737eaf40`) to 11 directly matching healthcare claims; Arbetsförmedlingen Ukraine guidance (`b4ecaf9b-6860-4896-a766-0adcc2c5ad4c`) to C034 and C035 for programme participation/activity compensation conditions. No new OfficialSource record was added.

Cumulative Core graph: 532 tuples = 113 SUPPORTED + 199 NOT_SUPPORTED + 220 PENDING_SOURCE_REVIEW. C032 repair reclassified one prior NOT_SUPPORTED tuple as SUPPORTED (no tuple-count change); the 13 new mapping tuples account for the only count delta from 519.

Coverage across 283 active claims: COVERED 70, COVERED_MULTIPLE 21, NOT_EXTERNALLY_SOURCE_DEPENDENT 0, UNSUPPORTED 59, REQUIRES_SOURCE_REVIEW 133. Material: covered 48, multiple 14, review 86, unsupported 32 (total 180). NumericDate: covered 16, multiple 7, review 14, unsupported 8 (total 45).

Newly UNSUPPORTED since corrected pre-Batch-2 state: 25 — SE-EMP-01-C002, C009, C010, C018, C020, C027, C028, C029, C031; SE-EMP-02-C008, C009, C010, C030, C032, C033, C036, C037, C039; SE-HEALTH-01-C005, C007, C010, C015, C023, C024, C028. Cumulative UNSUPPORTED claims: 59 — SE-EMP-01-C002, SE-EMP-01-C009, SE-EMP-01-C010, SE-EMP-01-C018, SE-EMP-01-C020, SE-EMP-01-C027, SE-EMP-01-C028, SE-EMP-01-C029, SE-EMP-01-C031, SE-EMP-02-C008, SE-EMP-02-C009, SE-EMP-02-C010, SE-EMP-02-C030, SE-EMP-02-C032, SE-EMP-02-C033, SE-EMP-02-C036, SE-EMP-02-C037, SE-EMP-02-C039, SE-HEALTH-01-C005, SE-HEALTH-01-C007, SE-HEALTH-01-C010, SE-HEALTH-01-C015, SE-HEALTH-01-C023, SE-HEALTH-01-C024, SE-HEALTH-01-C028, SE-TP-01-C001, SE-TP-01-C003, SE-TP-01-C004, SE-TP-01-C005, SE-TP-01-C008, SE-TP-01-C014, SE-TP-01-C015, SE-TP-01-C020, SE-TP-01-C021, SE-TP-01-C022, SE-TP-01-C030, SE-TP-01-C031, SE-TP-01-C032, SE-TP-01-C033, SE-TP-01-C034, SE-TP-01-C035, SE-TP-01-C036, SE-TP-01-C037, SE-TP-01-C042, SE-TP-01-C047, SE-TP-01-C048, SE-TP-01-C053, SE-TP-01-C054, SE-TP-01-C055, SE-TP-01-C056, SE-TP-02-C002, SE-TP-02-C013, SE-TP-02-C015, SE-TP-02-C017, SE-TP-02-C018, SE-TP-02-C021, SE-TP-02-C024, SE-TP-02-C026, SE-TP-02-C038.

## Employment findings

Employment claims with Batch-2 tuples reviewed: 55; source-certified through at least one exact supported tuple: 37; evidence gaps in this batch: 18 — SE-EMP-01-C002, SE-EMP-01-C009, SE-EMP-01-C010, SE-EMP-01-C018, SE-EMP-01-C020, SE-EMP-01-C027, SE-EMP-01-C028, SE-EMP-01-C029, SE-EMP-01-C031, SE-EMP-02-C008, SE-EMP-02-C009, SE-EMP-02-C010, SE-EMP-02-C030, SE-EMP-02-C032, SE-EMP-02-C033, SE-EMP-02-C036, SE-EMP-02-C037, SE-EMP-02-C039. The Migration Agency states that work rights for people aged 16+ begin once the permit decision has been received. Skatteverket distinguishes legal work authorization from A-tax/coordination-number processing; administrative tax registration can be pending after work begins when legal work authorization exists. Employer reporting duties are separate from immigration permission.

Arbetsförmedlingen Ukraine guidance supports how people register, service-centre access, PIN/BankID online sign-up, interpreter access, planning meetings, programme eligibility distinctions, included introduction-programme activities and programme-linked compensation. UHR separates its assessment of completed qualifications from regulated-profession licensing, where another competent authority may have to authorize practice. The Employment Protection Act supports written information about material terms and deadlines; Working Hours Act states the 40-hour ordinary baseline and scope/exceptions; Annual Leave Act states 25 days, with accrual and short-employment rules. The labour-model page covers collective-agreement arrangements but does not establish a general national minimum-wage rule or universal coverage of workers; those claims remain unsupported by this batch’s candidates.

## Healthcare findings

Healthcare claims with Batch-2 tuples reviewed: 22; source-certified through at least one exact supported tuple: 15; evidence gaps in this batch: 7 — SE-HEALTH-01-C005, SE-HEALTH-01-C007, SE-HEALTH-01-C010, SE-HEALTH-01-C015, SE-HEALTH-01-C023, SE-HEALTH-01-C024, SE-HEALTH-01-C028. Socialstyrelsen directly distinguishes registered permit holders (same healthcare and dental scope as residents) from unregistered adults and children under 18, and lists adult non-deferrable care, maternity, abortion, contraceptive advice, health examination, immediate care and child healthcare/dental entitlements. Migration Agency guidance corroborates adult care, permit-card reduced fees and free health-exam offer after grant. 1177 is a care-advice/navigation service; its Stockholm page says English/other-language help is available in some regions, supporting the regional-language limitation. SOS Alarm confirms 112 is for urgent risk to life, property or environment.

Skatteverket’s Ukraine guidance and Act 2024:691 support the one-year qualifying period, further six-month expected-stay requirement and case-by-case registration process. The Act/Skatteverket distinction does not turn registration into a permit or make registration automatic.

Audit outcomes by topic: work-permission timing is tied to receipt of a granted permit decision; no separate permit is needed after that status grant. Sweden’s no-general-national-minimum-wage statement is not established by the candidate sources in this batch, and collective-agreement coverage is not universal or proven for every workplace here. Adult and child healthcare entitlements are independently supported. Dental care is supported for registered holders and unregistered children; immediate/non-deferrable dental care for adults is also stated in Socialstyrelsen. Health examination offer is supported as a free post-grant offer on the Migration Agency page; the unregistered adult entitlement is separately supported by Socialstyrelsen. Patient-fee evidence establishes a reduced fee when a temporary-protection permit card is shown; the Compare aspect states only that fees and regional arrangements may apply and gives no national fee amount.

## Compare and Watch

Batch Compare tuples reviewed: 17 (17 SUPPORTED; 0 NOT_SUPPORTED; 0 pending). Cumulative Compare graph: 52 tuples = 32 SUPPORTED + 0 NOT_SUPPORTED + 20 pending. No new Compare tuples were added. Source-certified aspects: 4/24; newly source-certified: employment-assistance-conditions, work-permission, healthcare-access, healthcare-proof. Candidate dispositions: KEEP_CANDIDATE 23, REVISE_CANDIDATE 0, UNAVAILABLE_CANDIDATE 1, NEWLY_AVAILABLE_CANDIDATE 0.

No Watch candidate intersects a Batch-2 source or employment/health claim; 0 affected rows and no disposition changes.

## High-risk, reopen and integrity

High-risk Batch-2 claims reviewed: 24 unique; source-certified: 15; gaps: 9 — SE-EMP-02-C010, SE-EMP-02-C030, SE-EMP-02-C036, SE-EMP-02-C037, SE-EMP-02-C039, SE-HEALTH-01-C010, SE-HEALTH-01-C015, SE-HEALTH-01-C023, SE-HEALTH-01-C024. C032 stale evidence-gap state is corrected as above. Reopen register now contains 2 claims: SE-TP-01-C008, SE-HEALTH-01-C007. C007 is flagged because official sources assign urgency decisions to the healthcare service/responsible care provider, not necessarily a “clinician” in every case; safer wording should use the responsible healthcare provider. No translation reopen was identified.

Zero-active-Core-candidate sources after mapping repairs: one, `f241d32f-d41c-46f1-90c2-8d61eb4b2aea` (staged REMOVE; source has no Compare/review-copy use). Socialstyrelsen was a mapping omission and now has 11 supported claim tuples. Cross-owner duplicates were preserved. No owner conflict, dangling Core SourceId, wrong-owner binding, or same-owner canonical-URL duplicate was introduced. Production Core remains 69 pairs; production Compare remains 24 aspects / 52 pairs; active universe remains 283.

## Readiness

Batch 2 is complete and ready for Batch 3. Remaining gaps include the national minimum-wage/collective-agreement coverage claims, exact regional fee/healthcare-variation scope, the C007 decision-maker wording, and claims still awaiting Batch 3/4 evidence. No Batch-2 tuple remains pending.
