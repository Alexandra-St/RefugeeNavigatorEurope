# Slovakia Final Five-Fact Source Audit

**Audit baseline:** `a6485dc880888186c4e64dd8ca49b63798d9a38a`  
**Verdict:** `READY FOR OWNER ACCEPTANCE`

## Summary

| Check | Result |
| --- | ---: |
| Phase 20.17 write boundary | Pass: only `comparison-facts.json` and `SlovakiaContentIntegrationTests.cs` |
| Last five source-association findings resolved | 5/5 |
| Slovakia articles ready for owner decision | 8/8 (previous article audit; article files unchanged) |
| Slovakia facts ready for owner decision | 24/24 |
| EN/UK/RU values present and equivalent | 24/24 (prior semantic review; five corrected facts rechecked below) |
| Remaining blockers | 0 |
| Remaining minors | 0 |
| Slovakia `OfficialSources` | 81 |

This is an independent final check of the five Phase 20.17 source-association corrections. It does not repeat the semantic review of the other 19 facts. The preceding `FINAL_FACT_SOURCE_AUDIT.md` records their earlier review; Phase 20.17 did not alter their wording or source arrays.

## Change-boundary verification

Compared with the parent of `a6485dc`, Phase 20.17 changed exactly:

- `src/RefugeeNavigator.Web/Data/comparison-facts.json`
- `tests/RefugeeNavigator.Tests/SlovakiaContentIntegrationTests.cs`

The JSON diff changes only `OfficialSourceIds` for the five findings listed below. No fact values or wording changed. No article Markdown or translation wording changed. `official-sources.json` was not changed, so no `OfficialSource` record was added or deleted. The test changes mirror the corrected expected associations and assert the housing regulation's removal from `housing-initial` and continued association with `housing-financial-support`.

## Five corrected facts

| Aspect | Verification | Result |
| --- | --- | --- |
| `protection-scheme` | Now associates `38918d42-3122-587a-b06a-a70fd99520bc` (Government Resolution 185/2022 as amended), alongside the Slovak temporary-protection framework and EU Decision 2026/1912. Together these cover the Slovak activation/cohort boundary, the EU military-obligation rule, and the narrow same-state continuity exception stated in the unchanged fact. | Ready for owner decision |
| `healthcare-access` | Now associates VšZP provider guidance `5604f5aa-f855-56a1-a76a-05e0bc267711` and Act 577/2004 `84ac063a-2b96-5676-b97c-ab372f94fd75`, alongside existing Act 580/2004 §9h and temporary-refuge authorities. The source set covers non-membership by TP alone, the separate §9h reimbursement route, the Slovakia/other-EU insurance exclusions, and the different adult/child service scope. | Ready for owner decision |
| `social-child-benefits` | Now associates Government Regulation 93/2022, `11f5ef69-4ca5-5306-b447-7be40502d021`, with the ordinary allowance laws, childcare allowance law, and EU coordination sources. This covers the distinction between ordinary child/parental allowance and the conditional special-protection childcare route, including the parent/child conditions and possible cross-border coordination. The EN/UK/RU values retain equivalent scope following the earlier RU narrowing. | Ready for owner decision |
| `healthcare-proof` | Now associates Act 580/2004, `88261d23-1430-5fda-bd4c-666ba5bc0ac9`, alongside current VšZP proof/billing guidance and the legacy A4 source. The unchanged fact distinguishes current `DOČASNÉ ÚTOČISKO` proof and supported legacy A4 from an insurance card, separates provider billing, and identifies employment-based enrolment as a separate route. | Ready for owner decision |
| `housing-initial` | Regulation 151/2024 (`6422bbd8-e3d1-51c7-9487-a3680b24a07d`) is absent. Act 69/2026 (`de4856af-6e9b-5754-9a0a-6beb8e2e3727`) remains and supports the §68 facility claims. Regulation 151/2024 remains associated with `housing-financial-support`, where it supports the §69 host contribution. | Ready for owner decision |

All 24 Slovakia facts remain present, `Draft`, and available. Their wording and source arrays outside these five corrections are unchanged from the prior audit. The current records contain EN, UK, and RU values for all 24 facts; the five corrected facts were checked for equivalent scope and qualifications. Source associations remain article-owned as established by the prior mechanical audit and the current test expectations.

The previous article review remains **8/8 articles Ready for owner decision**; Phase 20.17 changed no article drafts. Slovakia remains at 24 translations, all `NeedsReview`, with 0 `Verified`; 24 comparison facts, all `Draft`, with 0 `Approved`; all 24 facts available; and 81 `OfficialSources`. Development visibility remains enabled and Production visibility remains hidden.

Netherlands holds are unchanged. Sweden `social-child-benefits` remains `Draft`, unavailable, and source-less.

## Verification provenance

The latest commit's change listing and diff were inspected. Counts and the five source-ID associations were checked against the current JSON and the integration-test expectations. The preceding article audit and the prior audit's checks for the unaffected facts, ownership, statuses, translations, holds, and visibility remain applicable because Phase 20.17 changed only five source-ID arrays and their integration-test expectations.

This audit does **not** claim that the previous broader checks were rerun in this phase. No new deployment or live-site verification is claimed here.

## Release boundary

**`READY FOR OWNER ACCEPTANCE`** means the five source-association findings are resolved and the bounded Slovakia package is ready for the owner's decision. This audit is not owner acceptance. No translation was marked `Verified`; no fact was marked `Approved`; visibility was not changed; Slovakia was not promoted; and no deployment was performed.
