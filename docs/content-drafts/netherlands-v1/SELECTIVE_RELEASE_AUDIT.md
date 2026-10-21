# Phase 18.4X — Netherlands Selective Release Audit

Audit baseline: `a7af52e871a583089b34a5b72c0a16782b757c54`
Pre-promotion comparison baseline: `cd9fb989f3dd3c3312f0d76c19c6f4ed5a5ca794`

## Audited release subset

Four articles are eligible in EN / UK / RU: NL-EMP-02, NL-HEALTH-01, NL-HOUSING-01, and NL-SOC-01 (12 `Verified` translations). The 12 `Approved` comparison facts are `work-permission`, `work-employer-duties`, `work-minimum-pay`, `healthcare-access`, `healthcare-proof`, `healthcare-urgent`, `housing-initial`, `housing-private-rental`, `housing-financial-support`, `social-subsistence`, `social-application`, and `social-child-benefits`.

## Catalog and source integrity

Current counts match the selective release boundary:

| Scope | Verified / Approved | NeedsReview / Draft |
| --- | ---: | ---: |
| Netherlands translations | 12 | 12 |
| Netherlands comparison facts | 12 | 12 |
| Global translations | 132 | 36 |
| Global comparison facts | 132 | 36 |

No `Outdated` translations are present. Against the pre-promotion baseline, IDs, article text, summaries, dates, fact wording, source relationships, ordering, and availability are unchanged; only the 12 authorized translation statuses and 12 authorized fact statuses differ. All 12 approved facts link to one of the four verified articles, have sources, and every source resolves to that linked article.

The `healthcare-access` fact retains its UWV source relationship. `healthcare-urgent` retains the corrected current Rijksoverheid URL. `residence-filing` remains `Draft`, unavailable, and source-less.

## Excluded and pending records

NL-TP-01, NL-TP-02, NL-EMP-01, and NL-EDU-01 remain `NeedsReview` in all three languages (12 translations). The 12 facts `protection-scheme`, `protection-application`, `protection-dates`, `residence-routes`, `residence-conditions`, `residence-filing`, `employment-vacancies`, `employment-institutions`, `employment-assistance-conditions`, `education-school-enrolment`, `education-pupil-language`, and `education-higher-admission` remain `Draft`. These records are pending or held, not rejected. NL-TP-01 remains held separately.

The documented source blockers for NL-TP-02, NL-EMP-01, and NL-EDU-01 do not block this bounded subset because those articles and their linked facts remain suppressed. The source recheck's non-binding-evaluation and memorandum findings remain applicable to those excluded records.

## Production gate — real catalogs

Existing focused tests rendered the actual current catalogs in EN / UK / RU:

- Core category pages show NL-EMP-02, NL-HEALTH-01, NL-HOUSING-01, and NL-SOC-01 with their article sources. NL-TP-01, NL-TP-02, NL-EMP-01, and NL-EDU-01 are suppressed; their article panels, titles, summaries, representative body text, anchors, and article sources do not leak.
- Comparison shows the 12 `Approved` facts and their owned source groups. The 12 `Draft` facts remain suppressed behind the unavailable-information response and expose no Draft source groups. `residence-filing` remains unavailable and source-less.
- Focused real-catalog audit tests: **9 passed, 0 failed**.

## Preserved healthcare caveat

NL-HEALTH-01 and `healthcare-access` retain the documented conflict between UWV and RMO/Rijksoverheid guidance about ordinary insurance for overlapping groups. The wording exposes that conflict and avoids giving an individual insurance instruction. Promotion and this audit do not resolve it or certify general legal accuracy. It is a non-blocking caveat for presenting this selective subset to the owner.

## Existing validation and visual evidence

Phase 18.4W targeted tests passed **82/82** and the Release suite passed **566/566**. GitHub CI run [39635984898](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/39635984898) succeeded for the audit baseline SHA. Netherlands visual evidence run [39635984939](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/39635984939) also succeeded for that SHA. The owner granted visual acceptance for the reviewed synthetic-data layout scenario; this is not hosted Production visual testing.

## Readiness

**READY WITH NON-BLOCKING CAVEAT** — the current selective subset can be presented to the project owner for an explicit deployment decision. This verdict covers only the four promoted articles and 12 facts, with the healthcare conflict preserved. Held records remain excluded under their existing gates.

No production status or catalog content was changed during this audit. No publication or deployment occurred. This audit does not authorize deployment.
