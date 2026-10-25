# Netherlands Final Readiness Audit — 25 October 2026

**Baseline:** `07c8241df90a121d3c1ee89f6300417e5dc9b1e9`

## Verdict

**READY FOR FINAL ACCEPTANCE**, with the documented caveats and one bounded source-list decision below. The Phase 18.5F correction is present in the production records. No further content correction is identified as a prerequisite to the owner review.

This is a readiness audit, not owner acceptance, legal certification, status promotion, publication approval, or deployment authorization.

## Current Netherlands status

The catalog contains 24 Netherlands translations: **12 `Verified`, 12 `NeedsReview`, 0 `Outdated`**.

| Articles | Current translation status (EN / UK / RU) | Scope |
| --- | --- | --- |
| NL-TP-01 | `NeedsReview` | Held; exclude from this acceptance. |
| NL-TP-02 | `NeedsReview` | Candidate for final owner acceptance. |
| NL-EMP-01 | `NeedsReview` | Candidate for final owner acceptance. |
| NL-EDU-01 | `NeedsReview` | Candidate for final owner acceptance. |
| NL-EMP-02, NL-HEALTH-01, NL-HOUSING-01, NL-SOC-01 | `Verified` | Already promoted; no new acceptance decision is needed for this pass. |

The catalog contains 24 Netherlands Comparison facts: **12 `Approved`, 12 `Draft`**. The eight facts in the Phase 18.5E review remain `Draft`: `residence-routes`, `residence-conditions`, `employment-vacancies`, `employment-institutions`, `employment-assistance-conditions`, `education-school-enrolment`, `education-pupil-language`, and `education-higher-admission`. The other four Draft facts remain held/deferred: three protection facts linked to NL-TP-01, and unavailable, source-less `residence-filing`.

## Phase 18.5F correction and continuity

The current NL-EMP-01 production translations no longer describe Work.nl as a vacancy-search service. EN/UK/RU attribute EURES vacancy search and Europass profile sharing to UWV and retain the no-job/no-placement guarantee. The current `employment-vacancies` fact uses equivalent “jobseekers covered by temporary protection” wording in all three languages and has only the UWV source ID `1690f9d6-0721-5005-b9de-fda56864ed70`. This matches the Phase 18.5F source alignment; no regression was found in the inspected records or directly related tests.

NL-EMP-01's separate article source list still contains the general Rijksoverheid work/income source (`98966301-dedb-5b6c-907c-adc84b07dfbb`). The previous editorial review asks whether the owner wants to retain it as general context. It is no longer attached to `employment-vacancies` and is not used to support the EURES/Europass statement. Decide whether to keep or remove this article-level reference during acceptance; it does not require reopening the corrected comparison fact.

The earlier source-recheck concerns about the unavailable September memorandum and Nuffic/IDW are addressed in the current bounded content: NL-TP-02 keeps transition material prospective and does not rely on the memo's unverified design or legal effect; NL-EMP-01 and NL-EDU-01 omit the Nuffic-dependent evaluation/legal-effect and ten-week claims. Existing caveats remain: the older Government.nl 2027 wording is attributed as outdated, permanent-residence time-counting remains unknown, local availability is not promised, and DUO relief remains conditional for 2026–27 with the 31 July 2027 deadline. This audit did not recheck any source or legal rule.

## Remaining owner decisions

1. Review EN, UK and RU separately for NL-TP-02, NL-EMP-01 and NL-EDU-01, accepting or rejecting each translation with the qualifications above.
2. Review the eight Draft facts and their links to the candidate articles. Decide whether to accept their bounded wording; keep the four held/deferred facts unchanged.
3. Decide whether the Rijksoverheid source remains useful in the NL-EMP-01 article source list.
4. If visual acceptance is part of the final owner decision, review real-content evidence for **NL-TP-02, NL-EMP-01 and NL-EDU-01** in Category and their linked Comparison facts. Existing Netherlands owner visual acceptance covers the synthetic layout scenario only, not these real article texts. No new browser review was run in this audit.
5. Any later status changes require separate explicit authorization. Do not alter the already `Verified` / `Approved` records or promote the held material as part of this audit.

Optional language polish previously noted for the UK/RU rendering of “newcomer education” is non-blocking and does not change the reviewed legal scope.

## Validation and change boundary

Targeted `NetherlandsContentIntegrationTests` and `CountryComparisonTests`: **86 passed, 0 failed**. `git diff --check`: passed. No production content, statuses, sources, or timestamps were changed. No visual/browser run, publication, or deployment was performed.
