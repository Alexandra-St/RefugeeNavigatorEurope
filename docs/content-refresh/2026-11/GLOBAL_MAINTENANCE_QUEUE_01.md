# Global country maintenance queue — 21 November 2026

This is an input for future maintenance tasks, not an automation or scheduled job. Country-specific event details and source references are in [the Watch index](GLOBAL_DATE_EVENT_WATCH_INDEX_01.csv); interval and country dates are in [the maintenance baseline](GLOBAL_COUNTRY_MAINTENANCE_BASELINE_01.csv).

## Monthly delta checks

Run a material-change scan across all 19 countries. Focus on laws, rates, programmes, dates, and administrative processes. If nothing material changed, record the check without repeating certification. For each check record country, checked date, changed sources, affected claims, Compare changes, Watch changes, and release required yes/no.

Countries in sequence: Netherlands, Germany, Hungary, Belgium, Italy, Finland, Lithuania, Poland, Slovakia, Denmark, Czechia, Bulgaria, Spain, Norway, Ireland, France, Sweden, Austria, Romania.

## Upcoming exact dates

| Date | Country | Watch | Action |
|---|---|---|---|
| 2027-01-01 | Bulgaria (BG) | LEGACY-BG-04 | Recheck the named authority before/at the event. |
| 2027-01-01 | Bulgaria (BG) | LEGACY-BG-08 | Recheck the named authority before/at the event. |
| 2027-01-01 | Spain (ES) | LEGACY-ES-01 | Approved: this is an effective date; current 2026 rates end 31 Dec 2026. Do not call 2027 amount currently effective before date or infer hourly rate. |
| 2027-01-01 | Ireland (IE) | LEGACY-IE-01 | S.I. No. 533/2026 was made on 15 October 2026 and sets €14.94 for age 20+ from 1 January 2027; recheck the commencement and WRC publication. |
| 2027-01-01 | Sweden (SE) | W-SE-05 | Course plans entered into force 2026-08-15 and apply to courses beginning after 2026-12-31. The 2027-01-01 implementation boundary and 2026-12-01 precheck are supported by Skolverket guidance. |
| 2027-01-02 | Sweden (SE) | W-SE-06 | SFS 2026:1552 confirms this amendment takes effect on 2027-01-02. The 2026-12-01 precheck remains appropriate. |
| 2027-03-04 | Bulgaria (BG) | LEGACY-BG-05 | Recheck the named authority before/at the event. |
| 2027-03-04 | Spain (ES) | LEGACY-ES-02 | Check for later Spanish Interior/BOE measure and affected card cohort; do not infer TIE validity through 2028. |
| 2027-03-04 | Ireland (IE) | LEGACY-IE-02 | Check for any new Irish extension, current IRP renewal instructions, and validity of the exact yellow-certificate cohort described by ISD. |
| 2027-03-04 | Sweden (SE) | W-SE-02 | Current Swedish guidance supports the 4 March 2027 permit cohort endpoint; precheck is an operational reminder. |
| 2027-03-05 | Bulgaria (BG) | LEGACY-BG-01 | Recheck the named authority before/at the event. |
| 2027-03-05 | Spain (ES) | LEGACY-ES-03 | EU legal trigger; does not itself extend the Spanish TIE validity date. |
| 2027-03-05 | Ireland (IE) | LEGACY-IE-03 | Check for later EU decisions/corrigenda and Irish implementation instructions. |
| 2027-03-05 | Romania (RO) | RO-W-001 | Retained as an event/rate update trigger; exact dates remain only where explicitly supported, and IDs are reconciled to active claims and final sources. |
| 2027-03-31 | Bulgaria (BG) | LEGACY-BG-07 | Recheck the named authority before/at the event. |
| 2027-03-31 | Ireland (IE) | LEGACY-IE-07 | Check extension, rate, eligibility and wind-down notices. |
| 2027-04-30 | Bulgaria (BG) | LEGACY-BG-02 | Recheck the named authority before/at the event. |
| 2027-09-15 | Bulgaria (BG) | LEGACY-BG-06 | Recheck the named authority before/at the event. |
| 2028-03-04 | Austria (AT) | AT-W-001 | Review on endpoint or successor school-year guidance. |
| 2028-03-04 | Bulgaria (BG) | LEGACY-BG-03 | Recheck the named authority before/at the event. |
| 2028-03-04 | Spain (ES) | LEGACY-ES-04 | Review for later EU decision and Spanish implementation; do not present as an individual document expiry. |
| 2028-03-04 | France (FR) | FR-W01 | Recheck EU acts before the extension end; continue checking APS validity separately. |
| 2028-03-04 | Ireland (IE) | LEGACY-IE-05 | Verify later EU decisions and national documentation guidance. |
| 2028-03-04 | Romania (RO) | RO-W-002 | Retained as an event/rate update trigger; exact dates remain only where explicitly supported, and IDs are reconciled to active claims and final sources. |
| 2028-03-04 | Sweden (SE) | W-SE-01 | Current EU Decision and Swedish guidance support the 4 March 2028 horizon; precheck precedes expiry. |
| 2028-03-04 | Sweden (SE) | W-SE-03 | Current Swedish guidance supports the 4 March 2028 permit cohort endpoint; precheck is an operational reminder. |
| 2028-07-01 | Norway (NO) | LEGACY-NO-09 | Current consolidated §2-1, replacement/transitional provisions, and whether the scheduled change is extended or amended. The expired 1 Jul 2026 text is historical only. |

## Event-driven checks

- **Spain (ES)**: LEGACY-ES-06 (REVIEW_ON (event-based)), LEGACY-ES-07 (SCHOOL_YEAR_CHANGE), LEGACY-ES-08 (REVIEW_ON).
- **Norway (NO)**: LEGACY-NO-01 (DECISION_EXPECTED), LEGACY-NO-02 (IMPLEMENTATION_DATE), LEGACY-NO-03 (REVIEW_ON), LEGACY-NO-04 (DECISION_EXPECTED), LEGACY-NO-05 (OFFICIAL_LIST_CHANGE), LEGACY-NO-08 (RATE_CHANGE / REVIEW_ON), LEGACY-NO-10 (OFFICIAL_RULE_CHANGE), LEGACY-NO-11 (OFFICIAL_GUIDANCE_OR_LAW_CHANGE).
- **Ireland (IE)**: LEGACY-IE-04 (REVIEW_ON monthly while scheme remains live; first review 1 December 2026), LEGACY-IE-06 (PROGRAM_TARGET), LEGACY-IE-08 (RATE_CHANGE), LEGACY-IE-09 (SCHOOL_YEAR_CHANGE).
- **France (FR)**: FR-W02 (RATE_CHANGE), FR-W03 (SCHOOL_YEAR_CHANGE), FR-W04 (SCHOOL_YEAR_CHANGE), FR-W05 (SCHOOL_YEAR_CHANGE), FR-W06 (SCHOOL_YEAR_CHANGE), FR-W10 (REVIEW_ON), FR-W11 (REVIEW_ON), FR-W12 (RATE_CHANGE), FR-W13 (RATE_CHANGE), FR-W14 (DECISION_EXPECTED).
- **Sweden (SE)**: W-SE-04 (DECISION_EXPECTED).
- **Austria (AT)**: AT-W-002 (SCHOOL_YEAR_CHANGE), AT-W-003 (SCHOOL_YEAR_CHANGE).
- **Romania (RO)**: RO-W-003 (RATE_UPDATE), RO-W-004 (ANNUAL_ADMISSION_PLAN_CHANGE), RO-W-005 (SCHOOL_YEAR_CHANGE).

## Trigger types, including dated triggers

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

## Risk/staleness and recertification queue

All countries currently have HIGH material under the global triage screen. Use a suggested six-month risk review window from their last full refresh (approximately 16–21 May 2027); this is a planning window, not a fixed legal deadline. Reassess the risk class during monthly checks. Suggested risk windows where reassessed: VERY_HIGH 3 months; HIGH 6 months; MEDIUM 9 months; LOW 12 months. Start a full recertification sooner after major legal/system reform, a major temporary-protection change, large source/evidence migration, or structural claim drift.

## Watch gaps / legacy handling

No retained Watch artifact was supplied for Netherlands, Germany, Hungary, Belgium, Italy, Finland, Lithuania, Poland, Slovakia, Denmark, or Czechia. The index records zero rows for those countries and does not infer that no future event exists. Legacy rows without native WatchIds use source-row locators only. Do not create an exact date from a pre-check or a planning marker.
