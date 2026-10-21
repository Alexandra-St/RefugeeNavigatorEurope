# Phase 18.4K — Targeted Browser Acceptance

Date: 21 October 2026
Baseline: `00de68c98b0fff11e97f3add17e55dbe6e67c701`
Environment: local Production-mode application, loopback only, using an isolated synthetic fixture under `/private/tmp/RefugeeNavigatorEurope-phase184k-fixture`.

## Browser and scope

The ChatGPT in-app browser inspected the Netherlands Temporary Protection category page and the Netherlands/Germany Temporary Protection comparison at 1280×720, 390×844 and 320×700. Each page was checked in English, Ukrainian and Russian (18 page/viewport/language combinations). The application reported the matching `html lang` value in all combinations.

The fixture kept production files untouched. Netherlands TP-01 used synthetic `NeedsReview` / `Outdated` translations; TP-02 used synthetic `Verified` translations. Comparison TP-01 facts were approved but linked to the unverified article; TP-02 included approved available facts, a draft fact and the unavailable, source-less `residence-filing` aspect.

## Results

- **Visibility and status gate: PASS.** The category rendered the verified TP-02 synthetic article in all three languages. The unverified TP-01 article was absent. In comparison, TP-02 approved facts appeared, while TP-01 facts linked to the unverified article and the TP-02 draft fact remained absent. The unavailable filing aspect remained unavailable. These checks used synthetic markers only.
- **Localization: PASS.** Both routes rendered English, Ukrainian and Russian page language and localized headings.
- **Comparison responsive width: PASS.** At 1280, 390 and 320 pixels, the comparison document width matched the viewport. No horizontal overflow was measured.
- **Category responsive width: INCONCLUSIVE.** At desktop, the document width matched the viewport. At 390 and 320 pixels the document measured 458 pixels (English), 476 pixels (Ukrainian), and 463 pixels (Russian). The overflow was traced to the intentionally unbroken synthetic TP-02 heading (`SYNTHETIC_VERIFIED_TP02_…`) in the fixture. This does not establish overflow for real article copy; the fixture was not altered or replaced during this pass, so narrow-width category layout remains to be rechecked with realistic, breakable synthetic copy.
- **Screenshot evidence: NOT SAVED.** Browser screenshots were available in memory, but could not be written to the requested repository evidence directory through the available browser tooling. A local file receiver was also blocked by the execution environment. No screenshot files are included or claimed.

The browser check was read-only against the local synthetic environment. No production catalogs, code, tests, statuses, sources, deployment settings, or hosted service were changed. No full test suite was rerun; the previously completed 113-test validation was not repeated.

## Gate recommendation

**VISUAL GATE UNVERIFIED — do not treat this pass as acceptance.** The source-visibility behavior and comparison sizing passed the targeted checks. Before accepting the visual gate, capture and retain screenshots for the requested viewport/language combinations and repeat the category narrow-width check with realistic synthetic text so the marker itself does not force overflow.

No publication or deployment was performed.
