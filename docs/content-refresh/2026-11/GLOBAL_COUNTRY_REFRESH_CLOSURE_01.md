# Global country refresh closure — 21 November 2026

## Scope and result

This documentation-only closure pass reconstructs the ordered production release chain for the 19-country refresh sequence, verifies each production commit against `main` ancestry and its exact-SHA GitHub Actions runs, reconciles later docs-only release commits, and records the post-refresh maintenance baseline. No country content, sources, Compare facts, production code, or tests were changed.

**Verdict: GLOBAL COUNTRY REFRESH CLOSED — 19/19 COUNTRIES RELEASE-VALIDATED.** The final country set is 19/19; 4 releases use the dedicated docs-only validation-report convention (`CLOSED`), 15 predate it (`LEGACY_FORMAT_CLOSED`), and 0 are blocked. All production SHAs are ancestors of current main and all required exact-SHA push workflows succeeded.

The source of truth for per-country metadata is [the release matrix](GLOBAL_COUNTRY_REFRESH_RELEASE_MATRIX_01.csv). It contains 19 rows, actual commit subjects/dates, exact production workflow names/run IDs/conclusions, and artifact pointers. For the first 15 countries, release validation predates the separate docs-report convention. Their country review/production records are retained as legacy evidence; exact commit identity, production diffs, branch ancestry, and GitHub run records independently verify closure. Ireland's Task 128 record includes the initial blocked attempt and the later Task 129 correction/release outcome; the production commit is `95ffd2344c62ddc1b924542e3f282cd46c8d4462`.

## Country sequence and release chain

| Ordinal | Country | Production SHA | Production commit | Previous refresh | Docs-only SHA | Next refresh |
|---:|---|---|---|---|---|---|
| 1 | Netherlands (NL) | `7a3130b600f87271c95f6029899ed15324094959` | feat: refresh Netherlands content (2026-11-16) | `Initial refresh in sequence` | NONE | `7607bb1447a23ab3bc334ebbbfeb2bf30b5d9589` |
| 2 | Germany (DE) | `7607bb1447a23ab3bc334ebbbfeb2bf30b5d9589` | feat: refresh Germany content (2026-11-16) | `7a3130b600f87271c95f6029899ed15324094959` | NONE | `9b9b8a68c468f5d1f400dbe3efecd68ac4696e7d` |
| 3 | Hungary (HU) | `9b9b8a68c468f5d1f400dbe3efecd68ac4696e7d` | feat: refresh Hungary content (2026-11-16) | `7607bb1447a23ab3bc334ebbbfeb2bf30b5d9589` | NONE | `c2c65f7ffd0c4c19872409d3c4adc7971ae7c787` |
| 4 | Belgium (BE) | `c2c65f7ffd0c4c19872409d3c4adc7971ae7c787` | feat: refresh Belgium content (2026-11-16) | `9b9b8a68c468f5d1f400dbe3efecd68ac4696e7d` | NONE | `cffceecdae85fbe763b27f606a07028ca8c5d6b5` |
| 5 | Italy (IT) | `cffceecdae85fbe763b27f606a07028ca8c5d6b5` | feat: refresh Italy content (2026-11-17) | `c2c65f7ffd0c4c19872409d3c4adc7971ae7c787` | NONE | `eefe4618b55a45d9be3eb89aa741f3ee1472a1e1` |
| 6 | Finland (FI) | `eefe4618b55a45d9be3eb89aa741f3ee1472a1e1` | feat: refresh Finland content (2026-11-17) | `cffceecdae85fbe763b27f606a07028ca8c5d6b5` | NONE | `8dbabdf2540ba8e87e77a9848275beb07fc1560f` |
| 7 | Lithuania (LT) | `8dbabdf2540ba8e87e77a9848275beb07fc1560f` | feat: refresh Lithuania content (2026-11-17) | `eefe4618b55a45d9be3eb89aa741f3ee1472a1e1` | NONE | `dce20367d16f1d44765871b25759d5c99ded81f8` |
| 8 | Poland (PL) | `dce20367d16f1d44765871b25759d5c99ded81f8` | feat: refresh Poland content (2026-11-17) | `8dbabdf2540ba8e87e77a9848275beb07fc1560f` | NONE | `f4722c6e7fbf73b2b5f6cf2c875112a22a1c4346` |
| 9 | Slovakia (SK) | `f4722c6e7fbf73b2b5f6cf2c875112a22a1c4346` | feat: refresh Slovakia content (2026-11-17) | `dce20367d16f1d44765871b25759d5c99ded81f8` | NONE | `9f5d44a8bdc4e23427bb3e2c270939cf44849e87` |
| 10 | Denmark (DK) | `9f5d44a8bdc4e23427bb3e2c270939cf44849e87` | feat: refresh Denmark content (2026-11-17) | `f4722c6e7fbf73b2b5f6cf2c875112a22a1c4346` | NONE | `f2dcb4da3de25945488941ab920ed787ff22e1f2` |
| 11 | Czechia (CZ) | `f2dcb4da3de25945488941ab920ed787ff22e1f2` | feat: refresh Czechia content (2026-11-17) | `9f5d44a8bdc4e23427bb3e2c270939cf44849e87` | NONE | `f54bf4b80d5c5d13c4dae1fab5e2d54a8c5894f4` |
| 12 | Bulgaria (BG) | `f54bf4b80d5c5d13c4dae1fab5e2d54a8c5894f4` | feat: refresh Bulgaria content (2026-11-18) | `f2dcb4da3de25945488941ab920ed787ff22e1f2` | NONE | `6b849ccf8d75d17e33c0d36ace28101faa2ab4a6` |
| 13 | Spain (ES) | `6b849ccf8d75d17e33c0d36ace28101faa2ab4a6` | feat: refresh Spain content (2026-11-18) | `f54bf4b80d5c5d13c4dae1fab5e2d54a8c5894f4` | NONE | `a20c6be7dcbed46c97152b6f4f439505d3c39335` |
| 14 | Norway (NO) | `a20c6be7dcbed46c97152b6f4f439505d3c39335` | feat: refresh Norway content (2026-11-18) | `6b849ccf8d75d17e33c0d36ace28101faa2ab4a6` | NONE | `95ffd2344c62ddc1b924542e3f282cd46c8d4462` |
| 15 | Ireland (IE) | `95ffd2344c62ddc1b924542e3f282cd46c8d4462` | feat: refresh Ireland content (2026-11-19) | `a20c6be7dcbed46c97152b6f4f439505d3c39335` | NONE | `b3a9cd74c4b3ae2e4ed0c4e1a03c1e471786376d` |
| 16 | France (FR) | `b3a9cd74c4b3ae2e4ed0c4e1a03c1e471786376d` | feat: refresh France content (2026-11-19) | `95ffd2344c62ddc1b924542e3f282cd46c8d4462` | `684c224461e8688c5753c9ec8c64c738104cb6d8` | `9e41f090d62d0b580978f4c5db1cf5da3c8d41ce` |
| 17 | Sweden (SE) | `9e41f090d62d0b580978f4c5db1cf5da3c8d41ce` | feat: refresh Sweden content (2026-11-20) | `b3a9cd74c4b3ae2e4ed0c4e1a03c1e471786376d` | `f9e78dbff6a7cd28133d18958f2beee848176336` | `cb41da55a280251f1ece9fafefc0faf3664c7197` |
| 18 | Austria (AT) | `cb41da55a280251f1ece9fafefc0faf3664c7197` | feat: refresh Austria content (2026-11-20) | `9e41f090d62d0b580978f4c5db1cf5da3c8d41ce` | `10fd3834dca682162a1881b5c8c751d0cf2b0cb4` | `fde6e6a18b29c73bdcb2c77f3f1a74b988c3dfde` |
| 19 | Romania (RO) | `fde6e6a18b29c73bdcb2c77f3f1a74b988c3dfde` | feat: refresh Romania content (2026-11-21) | `cb41da55a280251f1ece9fafefc0faf3664c7197` | `4d2faba583970c08741aa6ac1641a51deda45f0b` | `—` |

All 19 commits are distinct and appear in the intended ordinal order on the first-parent `main` history. Each country refresh changes the three country catalog files (`information-entries.json`, `official-sources.json`, `comparison-facts.json`); Germany also updates the German step-by-step guide in the same country integration. No wrong-country integration or unrelated feature work was found in these country production diffs. The `main` history was initially shallow in the checkout; its full history was fetched before completing ancestry checks.

## Workflow verification

For every production SHA, GitHub Actions returned a push-triggered `CI` run and `Netherlands visual evidence` run whose `head_sha` exactly matched that country's production SHA. All 38 required runs completed with conclusion `success`; there are no failed, cancelled, timed-out, or incomplete required runs. Germany also has a successful `Content maintenance audit` run (`43930382213`), but it was triggered by `schedule` and is not a required push workflow.

The four later docs-only SHAs are France `684c224461e8688c5753c9ec8c64c738104cb6d8`, Sweden `f9e78dbff6a7cd28133d18958f2beee848176336`, Austria `10fd3834dca682162a1881b5c8c751d0cf2b0cb4`, and Romania `4d2faba583970c08741aa6ac1641a51deda45f0b`. For each, production is an ancestor, the docs commit is an ancestor of current main, and its sole changed file is that country's release-validation report. These docs SHAs are not used as production workflow evidence.

**Current remote `main` before this Task 199 commit:** `4d2faba583970c08741aa6ac1641a51deda45f0b`. All 19 production SHAs are ancestors of it.

## Maintenance model

Four lanes apply to all countries:

1. **MONTHLY_DELTA_REFRESH** — review official source pages for material changes to laws, rates, programmes, dates, and processes. Do not repeat the full country audit when no material delta exists. Record country, checked date, source changes, claim changes, Compare changes, Watch changes, and whether a release is required.
2. **DATE_EVENT_WATCH** — use the existing country Watch candidate/final plan as the baseline. Recheck exact dates and event triggers at their supported trigger or when the competent authority publishes a change. A pre-check date is an operational reminder, not the legal event date. The global index preserves 60 retained rows from eight country artifacts; the 11 countries without a supplied Watch artifact have zero indexed rows rather than invented watches.
3. **RISK_STALENESS_REVIEW** — no existing written project-wide staleness intervals were found. Use the requested suggested intervals as review windows: VERY_HIGH 3 months, HIGH 6 months, MEDIUM 9 months, LOW 12 months. The 16 November global triage screen identifies HIGH material in all 19 countries; those classifications are queueing aids, not legal risk scores. Review `LastCheckedAt` and open a delta check; metadata age alone does not prove source staleness.
4. **FULL_RECERTIFICATION** — trigger after a major legal/system reform, temporary-protection framework change, large evidence/source migration, or structural claim drift. Use an approximate 6–12 month window from each full refresh depending on country risk; no exact deadline is asserted here. For the current HIGH triage class, use about six months as the planning window.

The repository has no persisted runtime Watch service; Watch rows are documentation-based maintenance inputs. No automation or external schedule was created. The global content-maintenance audit ran as of 21 November 2026: 152 articles, 456 translations, 1,559 source records, 456 Compare facts, 81 sources suggested for review based on age, and zero relationship issues. Its age signal is editorial scheduling only and did not inspect official websites.

## Upcoming exact-date events

The exact-date index contains **29** rows with an explicit date, including historical dates retained in source Watch records; **27** are upcoming after the 21 November 2026 baseline. Pre-check dates and non-day-specific planning markers are excluded.

| Date | Country | WatchId | Claim | Required maintenance action |
|---|---|---|---|---|
| 2027-01-01 | Bulgaria (BG) | LEGACY-BG-04 | BG-EMP-02-N005 | Recheck the named authority before/at the event. |
| 2027-01-01 | Bulgaria (BG) | LEGACY-BG-08 | BG-SOC-01-N009; BG-SOC-01-N011 | Recheck the named authority before/at the event. |
| 2027-01-01 | Spain (ES) | LEGACY-ES-01 | ES-EMP-02-C016; ES-EMP-02-C015 | Approved: this is an effective date; current 2026 rates end 31 Dec 2026. Do not call 2027 amount currently effective before date or infer hourly rate. |
| 2027-01-01 | Ireland (IE) | LEGACY-IE-01 | IE-EMP-02-C022; IE-EMP-02-C039; IE-EMP-02-C040; IE-EMP-02-C041; IE-EMP-02-C027; IE-EMP-02-C042; IE-EMP-02-C028 | S.I. No. 533/2026 was made on 15 October 2026 and sets €14.94 for age 20+ from 1 January 2027; recheck the commencement and WRC publication. |
| 2027-01-01 | Sweden (SE) | W-SE-05 | SE-EDU-01-C046 | Course plans entered into force 2026-08-15 and apply to courses beginning after 2026-12-31. The 2027-01-01 implementation boundary and 2026-12-01 precheck are supported by Skolverket guidance. |
| 2027-01-02 | Sweden (SE) | W-SE-06 | SE-SOC-01-C055 | SFS 2026:1552 confirms this amendment takes effect on 2027-01-02. The 2026-12-01 precheck remains appropriate. |
| 2027-03-04 | Bulgaria (BG) | LEGACY-BG-05 | BG-HOUSING-01-N014 | Recheck the named authority before/at the event. |
| 2027-03-04 | Spain (ES) | LEGACY-ES-02 | ES-TP-01-C014, C015, C019 | Check for later Spanish Interior/BOE measure and affected card cohort; do not infer TIE validity through 2028. |
| 2027-03-04 | Ireland (IE) | LEGACY-IE-02 | IE-TP-01-C026; IE-TP-01-C045; IE-TP-01-C057; IE-TP-01-C061 | Check for any new Irish extension, current IRP renewal instructions, and validity of the exact yellow-certificate cohort described by ISD. |
| 2027-03-04 | Sweden (SE) | W-SE-02 | SE-TP-01-C025 | Current Swedish guidance supports the 4 March 2027 permit cohort endpoint; precheck is an operational reminder. |
| 2027-03-05 | Bulgaria (BG) | LEGACY-BG-01 | BG-TP-01-N002 | Recheck the named authority before/at the event. |
| 2027-03-05 | Spain (ES) | LEGACY-ES-03 | ES-TP-01-C001, C016–C019 | EU legal trigger; does not itself extend the Spanish TIE validity date. |
| 2027-03-05 | Ireland (IE) | LEGACY-IE-03 | IE-TP-01-C024 | Check for later EU decisions/corrigenda and Irish implementation instructions. |
| 2027-03-05 | Romania (RO) | RO-W-001 | RO-TP-01-C008-S02 | Retained as an event/rate update trigger; exact dates remain only where explicitly supported, and IDs are reconciled to active claims and final sources. |
| 2027-03-31 | Bulgaria (BG) | LEGACY-BG-07 | BG-SOC-01-N010 | Recheck the named authority before/at the event. |
| 2027-03-31 | Ireland (IE) | LEGACY-IE-07 | IE-HOUSING-01-C080; IE-HOUSING-01-C081; IE-HOUSING-01-C082; IE-HOUSING-01-C083; IE-HOUSING-01-C084 | Check extension, rate, eligibility and wind-down notices. |
| 2027-04-30 | Bulgaria (BG) | LEGACY-BG-02 | BG-TP-01-N002 | Recheck the named authority before/at the event. |
| 2027-09-15 | Bulgaria (BG) | LEGACY-BG-06 | BG-EDU-01-N015 | Recheck the named authority before/at the event. |
| 2028-03-04 | Austria (AT) | AT-W-001 | AT-TP-01-C005 | Review on endpoint or successor school-year guidance. |
| 2028-03-04 | Bulgaria (BG) | LEGACY-BG-03 | BG-TP-01-N001 | Recheck the named authority before/at the event. |
| 2028-03-04 | Spain (ES) | LEGACY-ES-04 | ES-TP-01-C001, C019 | Review for later EU decision and Spanish implementation; do not present as an individual document expiry. |
| 2028-03-04 | France (FR) | FR-W01 | FR-TP-01-C025 | Recheck EU acts before the extension end; continue checking APS validity separately. |
| 2028-03-04 | Ireland (IE) | LEGACY-IE-05 | IE-TP-01-C024; IE-TP-01-C044 | Verify later EU decisions and national documentation guidance. |
| 2028-03-04 | Romania (RO) | RO-W-002 | RO-TP-01-C008-S01 | Retained as an event/rate update trigger; exact dates remain only where explicitly supported, and IDs are reconciled to active claims and final sources. |
| 2028-03-04 | Sweden (SE) | W-SE-01 | SE-TP-01-C002, SE-TP-01-C024 | Current EU Decision and Swedish guidance support the 4 March 2028 horizon; precheck precedes expiry. |
| 2028-03-04 | Sweden (SE) | W-SE-03 | SE-TP-01-C026 | Current Swedish guidance supports the 4 March 2028 permit cohort endpoint; precheck is an operational reminder. |
| 2028-07-01 | Norway (NO) | LEGACY-NO-09 | NO-EDU-01-C003; CNEW012 | Current consolidated §2-1, replacement/transitional provisions, and whether the scheduled change is extended or amended. The expired 1 Jul 2026 text is historical only. |

**Next exact-date event:** 1 January 2027 — Bulgaria (`LEGACY-BG-04`, employment/wage) and `LEGACY-BG-08` (social/benefits), Spain (`LEGACY-ES-01`), Ireland (`LEGACY-IE-01`), and Sweden (`W-SE-05`). Check the current official rate/rule at its effective date. **Latest exact-date event in the index:** 1 July 2028 — Norway (`LEGACY-NO-09`), statutory education change.

## Event-driven watch index

The index contains **31 watches with no day-specific trigger**; use the event type, source change, or annual publication as the trigger.

- **ANNUAL_ADMISSION_PLAN_CHANGE** (1): RO:RO-W-004.
- **DECISION_EXPECTED** (4): FR:FR-W14, SE:W-SE-04, NO:LEGACY-NO-01, NO:LEGACY-NO-04.
- **EFFECTIVE_FROM** (2): IE:LEGACY-IE-03 [2027-03-05], BG:LEGACY-BG-04 [2027-01-01].
- **EXPIRY / REVIEW_ON** (2): ES:LEGACY-ES-02 [2027-03-04], ES:LEGACY-ES-04 [2028-03-04].
- **IMPLEMENTATION_DATE** (3): SE:W-SE-05 [2027-01-01], SE:W-SE-06 [2027-01-02], NO:LEGACY-NO-02.
- **LEGAL_EFFECTIVE_FROM** (1): ES:LEGACY-ES-03 [2027-03-05].
- **OFFICIAL_GUIDANCE_OR_LAW_CHANGE** (1): NO:LEGACY-NO-11.
- **OFFICIAL_LIST_CHANGE** (1): NO:LEGACY-NO-05.
- **OFFICIAL_RATE_CHANGE** (1): NO:LEGACY-NO-07 [2026-11-06].
- **OFFICIAL_RULE_CHANGE** (2): NO:LEGACY-NO-06 [2026-05-04], NO:LEGACY-NO-10.
- **PROGRAM_END** (2): BG:LEGACY-BG-05 [2027-03-04], BG:LEGACY-BG-07 [2027-03-31].
- **PROGRAM_TARGET** (1): IE:LEGACY-IE-06.
- **RATE_CHANGE** (6): IE:LEGACY-IE-01 [2027-01-01], IE:LEGACY-IE-08, FR:FR-W02, FR:FR-W12, FR:FR-W13, BG:LEGACY-BG-08 [2027-01-01].
- **RATE_CHANGE / EFFECTIVE_FROM** (1): ES:LEGACY-ES-01 [2027-01-01].
- **RATE_CHANGE / REVIEW_ON** (1): NO:LEGACY-NO-08.
- **RATE_UPDATE** (1): RO:RO-W-003.
- **REGISTRATION_CLOSES** (1): BG:LEGACY-BG-02 [2027-04-30].
- **REGISTRATION_OPENS** (1): BG:LEGACY-BG-01 [2027-03-05].
- **REVIEW_ON** (6): IE:LEGACY-IE-02 [2027-03-04], IE:LEGACY-IE-07 [2027-03-31], FR:FR-W10, FR:FR-W11, ES:LEGACY-ES-08, NO:LEGACY-NO-03.
- **REVIEW_ON (event-based)** (1): ES:LEGACY-ES-06.
- **REVIEW_ON monthly while scheme remains live; first review 1 December 2026** (1): IE:LEGACY-IE-04.
- **SCHOOL_YEAR_CHANGE** (10): RO:RO-W-005, AT:AT-W-002, AT:AT-W-003, IE:LEGACY-IE-09, FR:FR-W03, FR:FR-W04, FR:FR-W05, FR:FR-W06, ES:LEGACY-ES-07, BG:LEGACY-BG-06 [2027-09-15].

Rows with an exact date may also be event-driven (for example, a wage rate effective date or school-year start); they are listed above by their exact date and are shown here under their trigger type as well. A pre-check date is not a legal event date. No date has been inferred from a review reminder or planning marker.

The index keeps native WatchIds where the source artifact provides them. For older candidate tables without a formal WatchId, `LEGACY-CC-NN` is a locator derived from the retained source row number, not a newly invented Watch item. Some legacy candidate artifacts identify authorities rather than SourceIds; those cells say `NOT_LISTED_IN_CANDIDATE_ARTIFACT`. All explicit SourceId UUIDs in the index resolve in production; dangling explicit SourceIds: 0. Watch claim references were checked against the final country audit/review artifacts where available; dangling ClaimIds found: 0.

## Artifact coverage and final checks

The 19-row release matrix points to one country release/review artifact for every row. Four countries have later docs-only release records; the earlier release records use legacy artifact formats and are marked `LEGACY_FORMAT_CLOSED`, not treated as missing blockers. Watch candidate/final artifacts are present for BG, ES, NO, IE, FR, SE, AT, and RO. No such artifact is present for NL, DE, HU, BE, IT, FI, LT, PL, SK, DK, or CZ; these are recorded with zero Watch rows. Older source/audit documentation formats are mapped to their country review/research artifacts rather than recreated.

Current production state: 19 countries represented; 152 articles; 456 localized translations; 1,559 OfficialSources; 456 Compare facts. The global content audit reports **0 relationship issues**. Release build passed with 0 errors (two NU1900 vulnerability-cache access warnings). Full .NET tests passed **733/733**; Python CI passed **104/104**. The initial concurrent build/test attempt collided on generated localization files; sequential reruns passed. Local Docker smoke was not run for this documentation-only closure task.

Task 199 changes only these global documentation files: `GLOBAL_COUNTRY_REFRESH_RELEASE_MATRIX_01.csv`, `GLOBAL_COUNTRY_REFRESH_CLOSURE_01.md`, `GLOBAL_DATE_EVENT_WATCH_INDEX_01.csv`, `GLOBAL_COUNTRY_MAINTENANCE_BASELINE_01.csv`, and `GLOBAL_MAINTENANCE_QUEUE_01.md`. No production content or country production tests were changed.
