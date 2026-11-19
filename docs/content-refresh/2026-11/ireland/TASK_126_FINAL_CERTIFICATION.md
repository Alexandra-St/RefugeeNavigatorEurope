# Task 126 — Ireland final certification

**As of:** 18 November 2026
**Purpose:** deterministic synthesis for independent review; this is not the independent review.

## Active Core claim universe

- Active claims: **265** (289 → 279 → 274 → 265; 24 removals/inactivations in Tasks 123–125).
- MATERIAL: **242**; SUPPORTING: **23**; NumericDate: **109**.
- Article counts (active / material / supporting / NumericDate): IE-EDU-01: 18/12/6/8; IE-EMP-01: 25/15/10/3; IE-EMP-02: 32/32/0/17; IE-HEALTH-01: 26/23/3/8; IE-HOUSING-01: 49/49/0/20; IE-SOC-01: 37/35/2/15; IE-TP-01: 36/34/2/18; IE-TP-02: 42/42/0/20.
- Coverage: 182 COVERED; 83 COVERED — MULTIPLE SOURCES; 0 NOT EXTERNALLY SOURCE-DEPENDENT; 0 UNSUPPORTED. NumericDate certified **109/109** by explicit current SUPPORTED tuples. Material gaps: 0; blocked claims: 0; claim reopens: 0.

## Removed/inactivated claims

Tasks 123–125 removed/inactivated **24** claims: `IE-EDU-01-C023`, `IE-EDU-01-C028`, `IE-EMP-01-C019`, `IE-EMP-01-C026`, `IE-EMP-01-C038`, `IE-EMP-02-C026`, `IE-HEALTH-01-C004`, `IE-HOUSING-01-C003`, `IE-HOUSING-01-C048`, `IE-HOUSING-01-C051`, `IE-HOUSING-01-C061`, `IE-HOUSING-01-C069`, `IE-HOUSING-01-C077`, `IE-SOC-01-C029`, `IE-TP-01-C055`, `IE-TP-02-C009`, `IE-TP-02-C034`, `IE-TP-02-C046`, `IE-TP-02-C060`, `IE-TP-02-C066`, `IE-TP-02-C075`, `IE-TP-02-C078`, `IE-TP-02-C083`, `IE-TP-02-C086`. These IDs are absent from final active coverage and are marked in ledger lineage.

## Evidence graph and source plan

- Evidence tuples: **1321** total; 368 SUPPORTED; 953 NOT_SUPPORTED; 0 unresolved.
- Source records: 106 production-existing; 119 final staging inventory; **13 ADD**, 3 UPDATE, 3 RETAIN HISTORICAL, 0 REMOVE. No planned ADD was dropped; each has an active Core role or final Compare role.
- ADD SourceIds: `2f5d4e13-b73e-5412-8de4-7f45823239f6`, `3a69b9ba-4d88-52ed-ba45-f1c64837097d`, `44437212-81e7-50e3-97ad-48237d7f0a3d`, `59a09766-a133-5f45-ab2b-3a156c3afe2e`, `7305cf75-847f-5b66-a1e4-c599c518e25c`, `79ccdfbe-5be6-5248-844d-ca744c6de269`, `840503b5-08cc-52a9-bfe8-51cf855abd2f`, `9eda791f-b545-5437-b5c8-62213de2dde5`, `ab9a6f06-b596-5dbe-921e-72740bafd774`, `b6c41dff-e2af-526d-abfe-28d48dd7f848`, `d2b33cd9-87e8-56ef-8804-3b23895f4e2d`, `ee0f776b-faab-551e-9f7e-6a294803d300`, `f70b0ad2-ae43-5301-bcb7-9d6ac00b6bab`.
- Ireland planned total: **119** (106 + 13); global planned total: **1,529** (1,516 + 13).
- Existing source actions reconcile: 100 KEEP, 3 UPDATE, 3 RETAIN HISTORICAL. UPDATEs: e65655f3-ee2f-51d7-9cb4-a222c19d92db — `https://www.neh.gov.ie/sustainability/understand-sustainability/skills-to-advance-boost-skills-boost-business` → `https://www.gov.ie/en/department-of-further-and-higher-education-research-innovation-and-science/publications/give-your-staff-training-to-develop-new-skills/` (current employee upskilling/reskilling page); 4cef8942-deb1-5e1a-9e5b-4e8c95458887 — `https://www.irishimmigration.ie/wp-content/uploads/2026/11/FINAL-PDF-Revised-Family-Reunification-Policy.pdf` → `https://www.irishimmigration.ie/wp-content/uploads/2026/06/Family-Reunification-Policy-12-June-2026.pdf` (current policy for applications from 12 June; limited section-60 scope); dd7f4201-5ecf-5603-99af-0b9bc304ee92 — `https://www.irishimmigration.ie/my-situation-has-changed-since-i-arrived-in-ireland/student-permission/` → `https://www.irishimmigration.ie/coming-to-study-in-ireland/` (current Student Pathway). RETAIN HISTORICAL: c26f454e-e5ab-5d3b-b5ba-dd57886fc201; dde56465-3a0b-5c73-988b-090e25867d23; e3d51e0a-6f8c-5951-91f7-4e1ad5807875.
- Core article/source binding pairs: **100 KEEP / 14 ADD / 6 REMOVE**. Exact rows are in `CORE_SOURCE_BINDING_PLAN_01.csv`.

## Compare

All **24/24** aspects are source-certified. Dispositions: **KEEP 17 / REVISE 7 / UNAVAILABLE 0 / NEWLY AVAILABLE 0**. Final REVISE aspects: residence-routes, residence-conditions, residence-filing, work-minimum-pay, housing-initial, housing-private-rental, education-higher-admission.

- `housing-initial`: 30-day maximum for the post-10 November 2025 cohort and 90-day maximum for earlier TP grants remain distinct. No automatic day-31 eviction, universal guarantee, or preferred-centre guarantee is asserted.
- `housing-private-rental`: HAP/Rent Supplement remain distinct from RTB tenancy rules; the one-month rules and student-specific advance-rent exception are stated precisely. No guaranteed landlord/property or universal subsidy claim.
- `work-minimum-pay`: €14.15 is current at cutoff; S.I. No. 533/2026 was made 15 October 2026 and establishes €14.94 from 1 January 2027. The future rate is enacted but not yet in force at cutoff.
- Compare source bindings: 77 KEEP / 2 ADD / 0 REMOVE / 1 NOT_BOUND. No PENDING rows.

## Watch

Nine final candidates: **7 APPROVE / 2 EDIT**. Two superseded candidates were removed: the separate wage-enactment check was merged into the 1 January 2027 rate-change watch; the duplicate TPTS amendment watch was merged into the monthly TPTS review. Earliest future legal/effective trigger: **1 January 2027**. Earliest maintenance pre-check: **1 December 2026**. TPTS has no certified hard close date. The commercial accommodation March 2027 date remains a programme target. TP dates remain distinct: 4 January 2027 pre-check, 4 March 2027 proof horizon for the specified Irish yellow-certificate cohort, 5 March 2027 EU extension start, and 4 March 2028 EU horizon. ARP remains €400/month effective 1 September 2026, first reduced payment October 2026, paid in arrears, with 31 March 2027 horizon. Education support is rechecked before 2027/28.

## Integrity and readiness

- Source owner conflicts: 0. Same-owner/same-URL duplicates: 0.
- Core dangling SourceIds: 0. Compare dangling SourceIds: 0. Watch dangling ClaimIds: 0. Removed Task 123–125 ClaimIds in active coverage/review-copy prose: 0.
- All eight review copies contain EN/UK/RU and their substantive high-risk rules align.
- Source inventory and final source input manifest each represent 106 production records plus 13 planned ADD records.
- Production and canonical content unchanged. No tests/build/Docker/CI/commit/push performed.
- **Independent-review readiness: YES.** Known limitations: final evidence synthesis depends on the audited official-source tuples and was not independently re-audited in this task.


## Mechanical integration correction (Task 129)

During production integration, the IE-TP-02 family-route paragraph was found citing `185a8f60-39cf-5be9-8d05-29441e49dfc5`, which is correctly owned by IE-TP-01. Production retains the IE-TP-01 record and adds deterministic owner-local duplicate `7c79b996-e285-5c86-971c-8676540d1226` for IE-TP-02 (UUIDv5 / NAMESPACE_URL / key `refugeenavigator:official-source:6bca20f8-b06d-5068-bd45-c78155111354:https://www.irishimmigration.ie/faqs-for-ukraine-nationals-and-residents-of-ukraine/`). The same approved ISD FAQ URL and evidence are used; no EN/UK/RU wording or evidence meaning changed. IE-TP-02-C016 now includes the owner-local tuple; the active claim count and claim coverage disposition counts are unchanged. Final Ireland sources: **120** (106 existing + **14 ADD**); global sources: **1,530**. Core binding deltas remain **100 KEEP / 14 ADD / 6 REMOVE**; Compare remains **24/24 certified** and **77 KEEP / 2 ADD / 0 REMOVE**, unchanged. Evidence tuples: **1,322** total (**369 SUPPORTED / 953 NOT_SUPPORTED / 0 unresolved**). The cross-owner same-URL pair is intentional; same-owner+URL duplicates remain **0**.
