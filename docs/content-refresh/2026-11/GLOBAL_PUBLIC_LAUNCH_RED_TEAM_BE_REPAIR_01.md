# Task 213 — Belgium P1 repair and final recertification

Date: 21 November 2026

Starting main SHA: `b10ddf373f6c8898727c262eb345f6e19548ee38`

Original audit commit: `58ecd3a1b660a33daa2ac2d8c315826bc732d343`

Production SHA: `fd47f6447683caaf0889499ec0a33e0a7a76481e`

Production commit: `fix: resolve Belgium launch audit finding`

Source audit record: `GLOBAL_PUBLIC_LAUNCH_RED_TEAM_BE_01.md`

Finding record: `GLOBAL_PUBLIC_LAUNCH_RED_TEAM_BE_FINDINGS_01.csv`

## Result

**Belgium final status: `RECHECKED_NO_BLOCKER`.**

The single P1 finding from the Belgium public-launch red-team audit, `BE-P1-001`, is resolved.

Final counts:

- P0: **0**
- P1: **0**
- P2: **0**
- unresolved high-risk candidates: **0**

The original Task 212 audit report and findings CSV remain immutable. This repair did not repeat the full 175-claim Belgium audit; it performed a targeted recertification of the affected minimum-pay cluster, translations, Compare alignment, source bindings, reviewed fixtures, tests and release chain.

## Resolved finding — BE-P1-001

Task 212 found two defects in the Belgium `BE-EMP-02` minimum-pay proposition and the aligned `work-minimum-pay` Compare fact:

1. Production still presented **€2,189.81 gross/month from 1 April 2026** as the operative RMMMG amount after it had been superseded.
2. Production described CCT 50 as applying to student workers aged 18–20 without the necessary sectoral-fallback qualification.

The repaired EN, UK-UA and RU wording now states that:

- from **1 July 2026**, the interprofessional RMMMG under CCT 43 is **€2,233.61 gross per month** for covered workers aged 18 and over;
- the RMMMG is an average monthly minimum-income guarantee, not a universal Belgian hourly minimum and not necessarily the simple base wage paid each month;
- CCT 43 does not apply to student workers aged 18–20;
- CCT 50 is a **supplementary fallback only where the applicable sectoral agreement does not set a minimum for workers under 21**;
- a sectoral collective agreement may set a higher minimum.

The `work-minimum-pay` Compare fact is aligned with the same corrected rate, effective date and CCT 50 qualification.

## Source disposition

The stale March 2026 current-rate source:

`56f4c722-2026-56c5-bbe5-27e7440694d8`

is no longer cited in the repaired Core wage proposition and is no longer bound to the Belgium `work-minimum-pay` Compare fact.

It remains an owned source record in the project source catalog, but it is no longer used as active evidence for the current RMMMG amount.

The repaired Compare source set is:

- `5b9b6430-79ba-5aee-9ef6-f6d8f21e47db`
- `3ebe4be7-0c7e-50bc-8222-183c6d151ede`
- `f0b4fabc-1e39-554f-a429-07d32e27265a`
- `0e853ef8-2b3b-5592-833b-6d1ce463c196`

No new OfficialSource record was required.

## Production and reviewed-fixture delta

Production repair commit `fd47f6447683caaf0889499ec0a33e0a7a76481e` changes five files:

- `src/RefugeeNavigator.Web/Data/information-entries.json`
- `src/RefugeeNavigator.Web/Data/comparison-facts.json`
- `docs/content-drafts/belgium-v1/BE-EMP-02.md`
- `docs/content-drafts/belgium-v1/COMPARE_DRAFT_01.md`
- `tests/RefugeeNavigator.Tests/BelgiumContentIntegrationTests.cs`

The two production JSON files contain the factual repair. The reviewed Belgium draft fixtures and integration-test expectations were updated to preserve the repository's existing invariant that production Belgium content matches its reviewed drafts exactly.

No unrelated country content was changed.

## Validation

Local validation on the repaired production state:

- Release build: **passed**
- Targeted Belgium integration tests: **9/9 passed**
- Full .NET test suite: **733/733 passed**
- Failed: **0**
- Skipped: **0**
- `git diff --check`: **passed**

Before the reviewed fixtures were synchronized, three Belgium integration tests failed because they intentionally asserted the previous reviewed draft values (`1 April 2026` / `€2,189.81`). The production repair itself was not reverted. The affected Belgium reviewed drafts and their exact-match assertions were updated to the corrected audited proposition, after which both the targeted Belgium suite and the complete 733-test suite passed.

Exact production-SHA GitHub Actions for:

`fd47f6447683caaf0889499ec0a33e0a7a76481e`

- `CI` run **#555**, run ID `44945743175` — **success**
- `Netherlands visual evidence` run **#357**, run ID `44945743063` — **success**

Both workflows completed successfully on the exact production SHA.

## Final launch disposition

`BE-P1-001` is **RESOLVED**.

The Task 212 conclusions outside the repaired minimum-pay cluster were not reopened. No broad re-audit of the 175 material/high-risk Belgium proposition groups was performed or required for this targeted recertification.

Residual severity counts:

**P0 0 / P1 0 / P2 0**

**Launch gate: passed. Final status: `RECHECKED_NO_BLOCKER`.**

This recertification commit is documentation-only and does not modify production.
