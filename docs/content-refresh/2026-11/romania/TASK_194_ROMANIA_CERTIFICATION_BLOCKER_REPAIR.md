# Task 194 — Romania certification-blocker repair

**Status:** Complete — ready for localized re-certification  
**As of:** 21 November 2026  
**Scope:** Staging artifacts only; no production integration.

## Task 193 blockers resolved

- **Core delta:** Reclassified the seven same-ID pairs that had been listed as both ADD and REMOVE to KEEP. The corrected Core plan is 50 KEEP, 7 ADD and 6 REMOVE, with 57 final pairs. Recomputed set difference against the 56 production pairs gives exactly 7 additions and 6 removals.
- **Translation alignment:** Revalidated all 81 ACTIVE claims against their EN, UK-UA and RU review-copy propositions. Repaired the 10 specifically reported rows, 42 additional stale/compound/placeholder rows, and retained 16 inactive rows as historical alignment records. Active rows have distinct child-level spans and no placeholder text or flagged divergence.
- **Review-copy citation graph:** For the adult course/evaluation fee sentence in RO-EDU-01, added the audited SUPPORTED Order 4649/2026 tuples to RO-EDU-01-C002-S01 and RO-EDU-01-C002-S02. Removed stale/out-of-scope citations and the unsupported occupational-recognition paragraph from the applicable localized copies. Added the existing RO-TP-02-C009-S01 proposition to all three RO-TP-02 content blocks with its audited source citation.
- **Source-action history:** Preserved the historical `REPLACE_CANDIDATE` audit action for source `2d6c45be-df4c-5064-ad7b-423a9d7bcea4`; added `FinalReconciledAction=REPLACE` to the source inventory and final source plan. Its replacement is `d3a3d139-2774-5024-8cb7-02d3404dac48`.

## Citation dispositions

| Review copy citation | Final disposition |
|---|---|
| RO-EDU-01 — `1244a27d-7d6c-502f-b8f2-4cf68a5e7671` | Added two audited SUPPORTED tuples: C002-S01 and C002-S02 |
| RO-EMP-01 — `0320ee26-8918-5e10-8022-a64bd60d708c` | Removed the occupational-recognition paragraph; it was outside the active claim set and the audited source role was academic-study recognition |
| RO-EMP-01 — `7ec35359-18dd-5720-87b7-b537bb7c90c5` | Removed from the combined agency-services sentence; the audited tuple for C003 was NOT_SUPPORTED |
| RO-SOC-01 — `f90cc1cb-0654-5539-ba74-c5e8d099ff30` | Removed from broad application-channel text; source audit found no supported claim tuple and rejected the Compare role |
| RO-TP-02 — `643e0f53-5acd-5ad8-9371-b4cf42fe851e` | Removed from the TP-specific work-route sentence; audited OUG 32/2026 scope is not the OUG 96/2024 TP work exemption |
| RO-TP-02 — `c75ac1cb-32c1-5bab-85e3-09eeb4c4cd72` | Removed with the extra amendment-detail clause; current OUG 194/2002 source remains cited for the active routes proposition |
| RO-TP-02 — `d22b2dda-6a08-5a85-ae33-aa40b6a778fe` | Removed from the long-term-residence paragraph; the IGI page concerns family reunification, not the cited Article 71 proposition |

## Post-repair state

- ACTIVE claims: 81; MATERIAL 62; SUPPORTING 19; NumericDate 12.
- Temporal classes: CURRENT 74; FUTURE 2; HISTORICAL 0; TRANSITION 5.
- Inactive split parents: 24 total (23 pre-existing plus RO-TP-02-C009); non-split removals: 15.
- Effective claim-source tuples: 104/104 SUPPORTED; NOT_SUPPORTED, PENDING and REVIEW_BLOCKED effective tuples: 0.
- Coverage: COVERED 61; COVERED_MULTIPLE 20; UNSUPPORTED 0; every ACTIVE claim remains covered.
- Active high-risk claims: 59/59 source-certified; active high-risk evidence gaps: 0.
- Final sources: 57; final source set and six old-absent/seven final-added ID arithmetic unchanged at 56 − 6 + 7 = 57.
- Core: 50 KEEP / 7 ADD / 6 REMOVE; final 57.
- Compare: 24 aspects, 66/66 SUPPORTED tuples, ownership violations 0.
- Watch: 5 linked rows; invalid claims, dangling sources, duplicate IDs and unlinked rows: 0.
- Evidence-gap dispositions: 39/39 resolved, no duplicate dispositions.
- Review copies: 8/8. Citation-to-source/claim graph mismatches: 0. Final-active translation rows: 81/81; stale, placeholder, repeated compound-span and flagged divergence rows: 0.

## Validation performed

Read-only staging checks recomputed claim, lineage, source, coverage, Core, Compare, Watch and gap counts; compared production-to-final Core pair sets; checked final SourceIds and owners; checked every review-copy citation against the final-present source set and active supported claim-source graph; checked every active tuple source is cited in its article copy; and checked all 81 active alignment rows for final English-claim synchronization, populated localized spans, placeholders, duplicate locale spans and divergence flags. No production tests were run because the repository’s Romania integration tests validate production catalog data rather than these staging artifacts.

Production/canonical files were not changed. No commit or push was made. This report does not issue final certification or a certified production plan; Task 195 must independently re-certify the localized state.
