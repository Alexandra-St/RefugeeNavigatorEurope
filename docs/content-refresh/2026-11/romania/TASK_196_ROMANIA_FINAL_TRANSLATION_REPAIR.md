# Task 196 — Romania final translation repair

**Status:** ROMANIA FINAL TRANSLATION REPAIR COMPLETE — READY FOR FINAL RE-CERTIFICATION  
**As of:** 21 November 2026

## Blocker and repair

Task 195 identified an unsupported extra proposition in both localized spans for `RO-EMP-01-C001-S01`: that applying for a vacancy does not guarantee an interview or employment. The active English proposition remains unchanged: “Job listings change over time.”

| Locale | Previous span | Final span |
|---|---|---|
| UK-UA | Оголошення змінюються, а подання заявки не гарантує співбесіди чи працевлаштування. | Оголошення про вакансії змінюються з часом. |
| RU | Объявления меняются, а отклик не гарантирует собеседование или трудоустройство. | Объявления о вакансиях со временем меняются. |

The interview/employment guarantee disclaimer was removed. The review copy retains its existing vacancy-search instructions and source citations, with the corrected proposition represented in the localized content. No English wording, claim status, claim ID, source evidence, source citation, or other article content was changed.

## Validation

- `TRANSLATION_ALIGNMENT_01.csv`: target row updated; EN remains “Job listings change over time.”; UK-UA and RU spans express the same proposition; divergence flags are `NO`.
- All active translation rows: 81 active claims and 81 unique alignment rows; no missing or duplicate active rows. Active rows have no flagged material, NumericDate, missing-proposition, extra-proposition, or temporal divergences; no placeholder or compound alignment rows were introduced.
- RO-EMP-01 review copy: all three active claim locators remain; no inactive claim locator was added; no unsupported interview/employment disclaimer remains.
- RO-EMP-01 citation check: body citations remain the same three final source IDs; all three effective article source IDs are cited; unknown source IDs 0; citations without a supported tuple 0; source citations changed 0.
- Effective claim-source graph remains 104 tuples, all 104 `SUPPORTED`; coverage remains 61 `COVERED` and 20 `COVERED_MULTIPLE` across 81 active claims.
- Other certified staging counts remain as previously reconciled: 59/59 active high-risk claims source-certified; 24 inactive split parents; 15 removed non-split claims; 57 final sources; Core 50 KEEP / 7 ADD / 6 REMOVE / 57 final; Compare 24 aspects / 66 supported tuples; 5 Watch rows; 39/39 gap dispositions.
- `ROMANIA_RECONCILED_FINAL_STATE_01.md` was not changed because it does not record the Task 195 blocker and its instruction calls for an update only if it does.
- No Romania-specific staging test target was identified. CI and tests were not run.

## Change boundary

Only `TRANSLATION_ALIGNMENT_01.csv`, the UK-UA and RU target spans in `RO-EMP-01_REVIEW_COPY_01.md`, and this repair report were changed. No certification or certified production plan was issued. Production/canonical files were not changed; no commit or push was made.

**Remaining Task 196 blockers:** none identified. Independent final re-certification remains for Task 197.
