# Task 155 — Sweden Batch 3 Housing + Education source audit

As-of 2026-11-20. Task 154 expected 17; repository and production integration source counts both confirm 17. Task 155 reconciled its mistaken 18-source premise and completed the audit on all 17 owner records. Production articles, OfficialSources, Compare records, and tests were not changed.

## Scope reconciliation

The owner inventory contains 7 Housing and 10 Education records (6 P0, 11 P1, 0 P2). Task 155 incorrectly stated the discovered count as 18; the source inventory and production integration source counts confirm 17. No source moved to Batch 4 and no source ownership changed. `f8308a50-caa9-4628-9a6e-9b8311020dc5` is cited in the Housing review copy as practical tenant-rights guidance. It had no active Core tuple, Compare binding, or Watch reference; Task 155 repairs the mapping by linking it to active C030/C031.

## Source outcomes

17/17 official sources reviewed; CURRENT 17, LEGACY 0, UNCLEAR 0, BROKEN 0. KEEP 17, UPDATE 0, RETAIN_HISTORICAL 0, REMOVE 0. All URLs remain actionable; no replacement was needed. Batch decisions and counts are in `SOURCE_AUDIT_BATCH_03.csv`.

## Housing

Migration Agency accommodation guidance distinguishes self-arranged housing, accommodation offered by the Agency, self-payment, and address notification. Population registration assigns responsibility for arranging housing; address notification to the Agency is a distinct procedure. The new Reception Act (2026:999) took effect 1 October 2026 and repealed the former LMA Act; transitional provisions preserve the repealed law for appeals of pre-effective decisions and treat earlier accommodation-allocation decisions as decisions under the new law. The 1 October Agency notice confirms that temporary-protection beneficiaries from Ukraine are exempt from the new geographic restriction and attendance checks described for other reception-system groups. This does not establish exemption from other Act provisions.

Rental sources distinguish second-hand apartment letting from rental of a home by a private individual. Specific rent-review and dispute remedies depend on letting type and circumstances. The Court source is current but its page serves procedural/routing context; it does not establish universal rights. The Consumer Agency tenant-rights page is general guidance and does not provide individualized legal advice. No general rent reimbursement is established by the housing guidance; separate benefit rules remain distinct.

## Education

Skolverket guidance confirms the registration-status distinction for education rights and compulsory schooling, including that actual registration/personal number is not always required for the registered-or-required-to-register group; children not registered and not required to register may have education rights without compulsory attendance. The home municipality responsibility, school reception, assessment, and placement rules are separate. Upper-secondary access is age- and status-dependent. Mother-tongue instruction is distinct from study guidance. SFI is a distinct municipal adult-education route and temporary protection by itself does not create entitlement; municipalities may permit participation only under specified conditions. Higher-education admission, documents, language and programme requirements, fee exemptions, and UHR foreign-qualification assessment remain separate processes.

## C046 and W-SE-05

Ledger proposition: “A new SFI course structure is scheduled to begin on 1 January 2027.” Current TemporalClass: FUTURE. Skolverket’s SFI right page states that new course structure/course plans apply from 1 January 2027. The dedicated implementation page clarifies the legal nuance: new course plans entered into force 15 August 2026 but must be used for courses beginning after 31 December 2026. Therefore 2027-01-01 is a valid implementation boundary, not the regulation’s entry-into-force date and not a school-year change. Outcome: SUPPORTED_FUTURE; TemporalClass remains confirmed. Watch W-SE-05: APPROVE. Precheck 2026-12-01 remains appropriate, one month before the implementation boundary. Earliest certified future trigger is 2027-01-01 (the separate Social watch remains 2027-01-02).

## Tuple and coverage results

Batch 3 adjudicated 179 Core tuples (129 original candidate tuples plus 50 mapping repairs, including f8308a50/C030 and C031 and exact missing Housing/Skolverket/University Admissions mappings); no Batch-3 pending rows remain. Batch outcome: 88 SUPPORTED and 91 NOT_SUPPORTED. Four non-exact candidate edges from the Ukraine-specific Skolverket page were removed because the separate newly-arrived pupil page directly establishes those placement rules. Cumulative Core: 582 total (201 SUPPORTED, 290 NOT_SUPPORTED, 91 PENDING). Coverage was rebuilt for all active Housing/Education claims.

Affected Compare source links reviewed: 14 SUPPORTED, 0 NOT_SUPPORTED, 0 pending. Cumulative Compare: 52 tuples (46 SUPPORTED, 0 NOT_SUPPORTED, 6 pending). Source-certified aspects: 8/24 (four new: housing-initial, housing-private-rental, education-school-enrolment, education-pupil-language). housing-financial-support and education-higher-admission remain uncertified because at least one material component lacks direct mapped evidence. All affected candidate dispositions remain KEEP_CANDIDATE. W-SE-05 is the only Watch row intersecting Batch 3 and is APPROVE.

## High risk and open issues

High-risk Housing/Education rows reviewed: 21; source-certified 15; evidence gap 6. Reopen candidates remain SE-TP-01-C008 and SE-HEALTH-01-C007; no new claim reopen or temporal reopen was required. No translation reopen was identified. Newly unsupported active Housing/Education claims (11): SE-HOUSING-01-C003, SE-HOUSING-01-C005, SE-EDU-01-C007, SE-EDU-01-C008, SE-EDU-01-C010, SE-EDU-01-C013, SE-EDU-01-C027, SE-EDU-01-C061, SE-EDU-01-C063, SE-EDU-01-C064, SE-EDU-01-C034. No housing/education claim has a remaining Batch-4 candidate in the reconciled graph; other article claims with unresolved evidence remain in Batch 4 scope. No new OfficialSource was added.

## Cumulative status

Cumulative coverage (active universe 283): COVERED 114, COVERED_MULTIPLE 42, NOT_EXTERNALLY_SOURCE_DEPENDENT 0, UNSUPPORTED 70, REQUIRES_SOURCE_REVIEW 57. MATERIAL: covered 74, multiple 23, review 45, unsupported 38. NumericDate: covered 20, multiple 10, review 7, unsupported 8. Housing claims reviewed 25, supported by at least one source 23, unsupported 2; Education reviewed 51, supported by at least one source 42, unsupported 9. Cumulative unsupported IDs are in `CLAIM_SOURCE_COVERAGE_01.csv`; Housing/Education subset: SE-HOUSING-01-C003, SE-HOUSING-01-C005, SE-EDU-01-C007, SE-EDU-01-C008, SE-EDU-01-C010, SE-EDU-01-C013, SE-EDU-01-C027, SE-EDU-01-C061, SE-EDU-01-C063, SE-EDU-01-C064, SE-EDU-01-C034. Production baselines remain 69 source bindings and 24 Compare aspects/52 pairs; active claim universe remains 283. Task 154 blocked history is preserved. Ready for Batch 4 after this closure.
