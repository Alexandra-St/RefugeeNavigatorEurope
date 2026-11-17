# Denmark refresh — independent final review

**As of:** 17 November 2026
**Scope:** Independent review of the staged Denmark refresh package: the 372-row claim ledger and coverage map, 62 numeric/date claims, eight EN/UK/RU review copies, 24 Compare facts, 102 existing Denmark source records, proposed source actions, bindings, and reconciliation. No production or canonical content was changed. No tests, build, Docker, CI, commit, or push were run.

## A. Scope and method

Reviewed `DENMARK_REFRESH_RESEARCH_01.md`, all eight article review copies, and `COMPARE_DRAFT_01.md`; checked the supplied article UUID mapping and used targeted reads of source metadata/bindings only. Recomputed ledger, coverage, source, and Compare totals from the staged rows. Independently checked high-risk current legal/status points against Danish Immigration Service, Retsinformation, UVM, and Folketing materials as of the cutoff. The review did not repeat broad research for every low-risk KEEP source.

## B. 372-claim review

The ledger has 372 unique claim IDs: 357 CONFIRMED, 9 UPDATED, and 6 NEW IMPORTANT INFORMATION; no INCORRECT, NO LONGER SUPPORTED, or QUALIFICATION REQUIRED rows. Materiality totals are 254 MATERIAL and 118 SUPPORTING. Review result: 371 ACCEPT; one bounded EDIT REQUIRED for DK-HEALTH-01-C028, corrected below. No BLOCK remains at claim level.

## C. Coverage-map review

There are 372 unique map rows and the reported pre-correction distribution reconciles: COVERED 132, COVERED — MULTIPLE SOURCES 197, NOT EXTERNALLY SOURCE-DEPENDENT 43, UNSUPPORTED 0. The pre-correction C028 row pointed to general post-permit health-card guidance, which did not substantiate the pending-applicant proposition. I corrected both C028 wording and its map to the current Immigration Service pending-applicant page. The row now states only that applicants who can show proof of application have access to “several health services,” and distinguishes that from permit-holder Health Act access. The post-correction map still has 372 rows, zero unsupported rows, and all 254 material claims externally mapped; one row has a documented review edit.

## D. 62 numeric/date claims

All 62 numeric/date ledger entries were reviewed. High-risk dates and values checked include 17 March 2028, 17 March 2027, 25 June 2026, 3 September 2026, ages 23–59, 24 February 2022, four weeks, 35 days, 17 March 2026, 26 weeks, 1 July 2025, and 4 November 2026. Result: 62 ACCEPT, 0 EDIT REQUIRED, 0 BLOCK. Four weeks remains attached to the relevant application/notification context; 35 days is a processing target, not a guaranteed deadline.

## E. Article verdicts

| Article | Verdict | Review note |
|---|---|---|
| DK-TP-01 | APPROVE | National Special Act route, filing, pending stage, permit and extension distinctions are maintained. |
| DK-TP-02 | APPROVE | Ordinary-residence routes are separate; no automatic conversion is promised. |
| DK-EMP-01 | APPROVE | Job-search services and responsible institutions are presented as guidance, not eligibility guarantees. |
| DK-EMP-02 | APPROVE | Permission to work is separated from employment-law and tax duties; no nationwide statutory minimum wage is claimed. |
| DK-HEALTH-01 | APPROVE | Article is scoped to permit holders and clearly separates CPR/card access; it does not make the defective pending-stage claim from the research map. |
| DK-HOUSING-01 | APPROVE | Municipal placement/support, private rental, and discretionary/local support are distinguished. |
| DK-EDU-01 | APPROVE | Schooling, Ukraine-specific expiry, remote/home education, VET, higher education, tuition and SU routes are qualified. |
| DK-SOC-01 | APPROVE | Cash assistance, family benefits, housing/childcare support and insurance schemes remain distinct. |

All eight copies include EN, UK and RU versions. No material wording edit to an article copy was necessary.

## F. Translation review

Reviewed all 24 locale versions for the material legal distinctions, conditions, negations, military scope, dates, pending rights, minimum-pay meaning, benefits, and municipal discretion. EN/UK/RU are semantically aligned for publication. TP-02-C010 correctly distinguishes the current card date (17 March 2027), extension date (17 March 2028), and same-rights processing period. No unresolved translation conflict found.

## G. Existing-source plan — 102/102

The inventory contains 102 existing Denmark-owned records: CURRENT 100, LEGACY 2, BROKEN 0, UNCLEAR 0. Actions reconcile to KEEP 98, UPDATE 2, RETAIN HISTORICAL 2, REMOVE 0. Final reconciled active/evidence set is 76 (69 prior active + six guide-bound sources + one claim-map dependency); 26 are truly unreferenced. The earlier “69 active” figure is a historical stage count, not the final inventory total. All 102 rows have an explicit status/action; no source-record removal is proposed.

## H. UPDATE reviews

- **Act no. 501/2023 — APPROVE UPDATE.** Replace the PDF input with the current consolidated Retsinformation page at `https://www.retsinformation.dk/eli/lta/2023/501`; retain the same legislative identity and update checked metadata as planned.
- **UCPH tuition page — APPROVE UPDATE.** Use `https://www.ku.dk/studies/admission/application-and-tuition-fees`, the verified current destination for the same admissions/tuition role. Update title/URL metadata and checked date as planned.

## I. LEGACY reviews

- **2022 Ministry healthcare notice, `d3520d2c-2fc2-4cdf-89dc-eb192aa9b64a` — APPROVE RETAIN HISTORICAL.** Useful for the 2022 rule-change history; do not treat its earlier acute/continuing-care categories as the current pending-applicant rule or bind it as current broad healthcare evidence.
- **Act no. 1655/2024, `c1f2c424-b1e0-4cfe-8d3a-2c5573f2f59a` — APPROVE RETAIN HISTORICAL.** Preserves the provenance and effective date of the cash-assistance reform; current operative text is carried by the later consolidation and current guidance.

Neither record is the sole current support for an operative proposition.

## J. ADD reviews

All five proposed ADDs are approved as staged: (1) `e1d08534-e55b-5025-ac23-75a2d7cdc5dd`, DIS extension notice, DK-TP-01; (2) `cc72237b-6472-539c-94d9-ff73e3cbea02`, UVM remote teaching, DK-EDU-01; (3) `6ea2a976-8834-55e3-8817-ef3d769c7349`, UVM home education, DK-EDU-01; (4) `5b063a3e-9643-5c83-9a57-a61b24138abc`, DIS permanent residence, DK-TP-02; (5) `f4e25072-b328-5469-ab1c-30169b92abfe`, UVM VET admission, DK-EDU-01. Targeted identity, owner, URL, authority, date, scope and collision checks found no existing same-owner source that should instead be updated. The C028 correction uses an already-existing current DIS source and does not add a sixth source.

## K. Repeated-URL reconciliation

The 102-record inventory has 12 exact-URL groups and 14 repeated occurrences (88 distinct exact URLs). Each group was reviewed; all are owner-local across distinct articles. Same-owner + exact-URL duplicates: 0. The Act 324 HTML/PDF pair is a same-document format redundancy, not an exact-URL duplicate; the PDF remains KEEP because a step-by-step guide is bound to it.

## L. Core bindings

The 21 planned Core ADD binding instances were checked for valid owner, claim purpose, surviving source, and unnecessary duplication. Approve 21 ADD / 0 REMOVE. All planned IDs resolve to existing retained/updated records or one of the five proposed ADDs.

## M. Compare 24/24

All 24 facts and EN/UK/RU copies were reviewed. Dispositions reconcile to KEEP 23, REVISE 1, UNAVAILABLE 0, NEWLY AVAILABLE 0; all 24 are source-audited. The protection-dates wording is the one REVISE and accurately separates 17 March 2027 card validity from the 17 March 2028 extension. The minimum-pay fact correctly says there is no statutory nationwide minimum wage and does not provide a universal amount.

## N. Compare bindings

Approve 1 ADD and 3 aspect-level REMOVE actions. The three removals are binding changes only: two obsolete date-page bindings on protection-dates and the historical 2022 healthcare notice on healthcare-access. They are not source-record removals.

## O. Source-count review

Submitted plan: Denmark 102 existing + 5 ADD − 0 source removals = 107; global 1,489 + 5 = 1,494. These totals reconcile to the approved source actions. The C028 evidence correction relies on an existing source and leaves the count unchanged.

## P. Owner and ID integrity

All eight article UUIDs match the provided mapping. Existing source IDs are unique; all five proposed IDs are unique against the stated global baseline; owners resolve to the intended Denmark articles; no foreign-country source is introduced; no unintended owner+URL duplicate or dangling planned Core/Compare SourceId was found. Owner conflicts: 0; dangling SourceIds: 0; ADD collisions: 0.

## Q. Cross-article consistency

Checked TP against employment, healthcare, housing, education, social assistance and ordinary-residence content, plus employment against healthcare/social/self-employment, housing against social, and education against social. No unresolved material cross-article conflict remains. Pending healthcare is now stated narrowly in the research claim/map; permit-holder coverage remains separate in the article copy.

## R. Required edits made

One minimal staging-only correction was made in `DENMARK_REFRESH_RESEARCH_01.md`: DK-HEALTH-01-C028 was narrowed to current DIS wording and its coverage SourceId changed from general post-permit Borger.dk coverage to the current DIS pending-applicant page (`141d8418-08af-4315-b5c2-ff34900b3369`). The earlier 2022 Ministry description is retained as historical context. The corrected source directly supports the limited statement; no article or Compare copy changed.

## S. Blocking findings

No unresolved blocking finding remains. The initial coverage mismatch is corrected and the new wording was rechecked against current Immigration Service guidance. No material unsupported claims, dangling IDs, owner conflicts, or material cross-article conflicts remain.

## T. Final integration recommendation

**DENMARK APPROVED FOR INTEGRATION.** Proceed with the staged source actions and bindings after ordinary integration review. This approval covers the reviewed staging package as of 17 November 2026; production and canonical data remain untouched in this review.

## Official checks

- [Danish Immigration Service — You are waiting for an answer](https://www.nyidanmark.dk/en-GB/You-are-waiting-for-an-answer/Fordreven-fra-Ukraine) (current pending application guidance, including proof and access to several health services).
- [Retsinformation — BEK no. 856 of 9 June 2022](https://www.retsinformation.dk/eli/lta/2022/856) (current regulation on necessary health services for applicants without a residence permit; supersedes BEK 442/2022).
- [Danish Immigration Service — 29 October 2026 extension notice](https://www.nyidanmark.dk/en-GB/News-Front-Page/2026/10/Extension-of-residence-permit-under-the-Ukraine-Special-Act).
- [Retsinformation — BEK no. 828/2026](https://www.retsinformation.dk/eli/lta/2026/828).
