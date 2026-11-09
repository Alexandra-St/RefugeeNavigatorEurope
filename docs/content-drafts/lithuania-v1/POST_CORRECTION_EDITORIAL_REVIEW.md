# Lithuania v1 Post-Correction Editorial Review

**Review baseline:** `3c9e64329d47efd6c7150be2472ac7707569844b`
**Previous review:** [`EDITORIAL_REVIEW.md`](EDITORIAL_REVIEW.md), baseline `33a4065e41defa3d64b40a136fe406527fff5842`
**Correction commit reviewed:** `3c9e64329d47efd6c7150be2472ac7707569844b`
**Review scope:** LT-REV-01 and LT-REV-02 in `LT-TP-01.md`, their existing research qualifications, and the correction commit diff. No new research was performed. The other seven drafts were not reread; their previous **7/7 OK** findings are carried forward.

## LT-REV-01

**Result: Resolved.**

EN, UK and RU now distinguish the two stateless / third-country cohorts:

- The protection cohort had international protection or equivalent national protection in Ukraine **before 24 February 2022**.
- The permanent-residence cohort held a **valid Ukrainian permanent-residence permit**, was **lawfully permanently resident in Ukraine before 24 February 2022**, and cannot **safely and durably return** to the country of origin or another receiving state.

The safe-return test is attached to the permanent-residence cohort. The draft limits family coverage to the specified statutory scope and continues to exclude general coverage for every former Ukrainian resident. The wording follows the existing research package; citations and source UUIDs were not changed.

## LT-REV-02

**Result: Resolved.**

EN, UK and RU state the two Lithuanian national transition exceptions separately: continuous temporary protection in **any EU Member State across the 4 August 2026 cutoff**, and a **Lithuanian TP application already pending at that cutoff**. Each language then distinguishes the EU-level continuity exception, which refers to continuous protection in the **same Member State**. The national and EU formulations are not conflated.

## Change-scope verification

The diff from `a297b8112e2410d886449c643abeb3d753475d71` to correction commit `3c9e64329d47efd6c7150be2472ac7707569844b` changes only:

- `docs/content-drafts/lithuania-v1/LT-TP-01.md`

That change is limited to the three language versions of the two reviewed paragraphs. The article UUID (`115cbf25-5ece-5cc9-8bde-c3dabcbda47f`), `temporary-protection` category, `NeedsReview` translation status, and all three aspect mappings (`protection-scheme`, `protection-application`, `protection-dates`) are unchanged. Inline source UUIDs and the nine-entry editorial source set are unchanged. Unrelated LT-TP-01 claims and the other seven drafts were not changed by the correction commit. Research, catalogs, tests, code and verifiers were not changed. No catalog integration has occurred.

## Cross-language verification

- LT-REV-01: equivalent cohort boundaries in EN / UK / RU; no language broadens eligibility to former residents generally or detaches the return test from the permanent-residence cohort.
- LT-REV-02: equivalent cutoff and separate pending-application exception in EN / UK / RU.
- The 24 February 2022 and 4 August 2026 dates match across the three versions.
- Each language preserves the distinction between **any EU Member State** for the Lithuanian national exception and the **same Member State** for the EU-level continuity wording.
- No stronger or weaker eligibility condition was identified across the three versions.

## Final editorial state

Carrying forward the previous review’s unchanged findings for the other seven drafts and rechecking LT-TP-01 after correction:

- Articles OK: **8/8**
- Unresolved BLOCKER: **0**
- Unresolved CORRECTION: **0**
- Unresolved POLISH: **0**
- Canonical aspects: **24/24**, with no duplicates, omissions or extras
- Editorial source UUIDs: **62 unique**, unchanged
- Inline source references: **222**, unchanged
- Cross-article conflicts: **0**, carried forward from the previous review
- Source-set changes: **none**
- Catalog integration: **not performed**

The prior review’s source identity audit is carried forward. It is not repeated because the correction changed no citations or source-set entries.

## Integration recommendation

**READY FOR CORE INTEGRATION**

This recommendation permits only core integration with article translations remaining **NeedsReview** and comparison facts remaining **Draft**. It is not owner acceptance, status promotion, publication approval or deployment authorization. No catalog integration or deployment was performed in this review.
