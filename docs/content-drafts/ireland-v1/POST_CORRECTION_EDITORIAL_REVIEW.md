# Ireland v1 Post-Correction Editorial Review

- **Review baseline:** `21812b3037a462f07529dbc423b475daa33dfe5b`
- **Previous review:** `EDITORIAL_REVIEW.md`
- **Correction commit reviewed:** `21812b3037a462f07529dbc423b475daa33dfe5b`
- **Review date:** 10 November 2026
- **Scope:** only the previous correction findings and the correction commit diff
- **External research:** none

## Findings resolution

### IE-REV-01 — IE-EMP-02 PPSN

**Result: Resolved**

The Ukrainian sentence now says PPSN is used for administrative and tax procedures, and that it does not create work authorization. It no longer presents PPSN as a universal prerequisite for starting work. The nearby employer-payroll qualification allowing specified alternative employee details when PPSN is unavailable remains intact. This matches the English and Russian meaning.

### IE-REV-02 — IE-EDU-01 QQI/NARIC

**Result: Resolved**

- **UK:** The conclusion is expressly described as advisory and as not obliging the institution to make an admission decision. Credit transfer is assessed separately. The wording describes the effect of the advice, not whether the applicant needs to obtain it.
- **RU:** The conclusion is expressly described as advisory and as not obliging the university to admit the applicant. Credit transfer is considered separately.

Both translations now preserve the non-binding nature of QQI/NARIC comparability advice while leaving admission and credit-transfer decisions with the institution.

### IE-REV-03 — IE-SOC-01 WFP

**Result: Resolved**

The Russian paragraph now states the usual 38 hours of insurable employee work per fortnight, that eligible partner hours may count, and that self-employed hours do not count. It also retains the qualifying-child and income-limit conditions and says that TP status or employment alone does not establish entitlement. The material conditions align with EN and UK.

## Carried-forward article status

The prior review's PASS results for IE-EMP-01 and IE-HEALTH-01 are carried forward without re-review.

| Article | Current status |
|---|---|
| IE-EMP-01 | **PASS** — carried forward |
| IE-EMP-02 | **PASS** — IE-REV-01 resolved |
| IE-HEALTH-01 | **PASS** — carried forward |
| IE-EDU-01 | **PASS** — IE-REV-02 resolved in UK and RU |
| IE-SOC-01 | **PASS** — IE-REV-03 resolved |

## Change-scope verification

The correction commit changes only these three files:

- `IE-EMP-02.md`
- `IE-EDU-01.md`
- `IE-SOC-01.md`

The diff is limited to the specified language paragraphs: the PPSN sentence in UK, the QQI/NARIC wording in UK and RU, and the WFP eligibility wording in RU. The English wording is unchanged. No changes were made to IE-EMP-01, IE-HEALTH-01, `EDITORIAL_REVIEW.md`, research packages or catalogs.

## Source / metadata integrity

Compared with the correction commit's parent, all three changed drafts retain their Article IDs, metadata, `NeedsReview` translation status, comparison aspects, inline source UUIDs and editorial source sets. No citations were added, removed or changed.

## Final recommendation

**READY FOR CATALOG INTEGRATION**

All three correction findings are resolved, and the correction commit is limited to the approved translation paragraphs. This is an editorial review result, not human acceptance; translation status remains `NeedsReview`.
