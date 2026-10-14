# Accessibility Audit — Phase 3.6B

**Audit date:** 2026-10-14
**Baseline:** `20901101ce3489e7d1adaa97cd7b8b94761b0f7f`
**Standard:** WCAG 2.2 Level AA (including Level A criteria)
**Status:** Audit complete; remediation review required

## A. Executive summary

The source and local browser review found **2 confirmed issues**, both medium severity: form-control boundaries do not meet the non-text contrast minimum, and English/Ukrainian country-card links have accessible names that do not contain their visible labels. No critical or high-severity issue was confirmed.

The application has a strong semantic baseline: sampled pages have one `h1` and one `main`, forms have associated labels, keyboard focus is visible, language metadata follows the selected culture, and representative pages reflowed without horizontal scrolling at 375 CSS px. A release should not be described as WCAG 2.2 AA conformant until the confirmed issues are corrected and the manual checks below are completed.

## B. Scope and methodology

Reviewed `App.razor`, `Routes.razor`, `Home.razor`, `CountryOverview.razor`, `Category.razor`, `Search.razor`, `Compare.razor`, `NotFound.razor`, `LanguageSwitcher.razor`, their page/component stylesheets, `wwwroot/app.css`, `Program.cs`, the private-alpha authentication middleware, and the Ukrainian, English, and Russian resource files.

Built the Release solution with zero warnings/errors and ran the existing .NET suite: **269 passed, 0 failed**. Started the current build locally in Development mode in an isolated checkout; no deployed service or Render was accessed. Used the local browser accessibility tree and rendered DOM, keyboard Tab/Shift+Tab, and viewport checks. Tested eight representative routes in each supported culture at 375 CSS px; additionally checked the employment article at 768 and 1440 CSS px. All 109 resource entries exist and are nonempty in each culture; all 90 literal localization keys referenced by application code resolve in all three cultures.

Contrast ratios below use the declared sRGB CSS colors and the WCAG relative-luminance formula. No automated WCAG scanner or screen reader was used. The local app ran in Development mode, so the production Private Alpha authentication challenge was not exercised.

## C. Confirmed accessibility issues

### A11Y-01 — Form-control boundary contrast

- **Severity:** MEDIUM
- **WCAG:** [1.4.11 Non-text Contrast (AA)](https://www.w3.org/WAI/WCAG22/Understanding/non-text-contrast.html)
- **Affected components:** Search text input and filters; comparison selects.
- **Files/evidence:** `src/RefugeeNavigator.Web/Components/Pages/Search.razor.css:83-93` and `src/RefugeeNavigator.Web/Components/Pages/Compare.razor.css:92-101` set a `#9aabc2` border on white (`#ffffff`) controls. The measured contrast is **2.34:1**, below the 3:1 minimum for visual information needed to identify a control.
- **User impact:** People with low vision or reduced contrast sensitivity may have difficulty locating the input and select boundaries.
- **Recommended correction:** Use a control boundary color that reaches at least 3:1 against its adjacent surface, including every affected control style. Keep the visible focus indicator distinct.

### A11Y-02 — Country-card accessible names omit visible labels

- **Severity:** MEDIUM
- **WCAG:** [2.5.3 Label in Name (A)](https://www.w3.org/WAI/WCAG22/Understanding/label-in-name.html)
- **Affected component:** Country links on the landing page.
- **File/evidence:** `src/RefugeeNavigator.Web/Components/Pages/Home.razor:46-48` overrides each link's name with `Home.CountryLinkLabel`, while its visible text comes from `Home.ExploreCountry`. In English the visible label is “Explore Denmark” and the accessible name is “Explore practical information for Denmark”; the visible phrase is not contained in the accessible name. The Ukrainian labels also differ (“Докладніше про країну …” versus “Переглянути практичну інформацію про країну: …”). The Russian accessible name does contain its visible label. The local accessibility tree confirmed the English accessible name override.
- **User impact:** Speech-input users who activate a control by speaking its visible label may not be able to match the country link reliably.
- **Recommended correction:** Remove the redundant `aria-label`, or make the accessible name contain the full visible link text in the same order in every supported language.

## D. Potential issues requiring verification

These are not counted as confirmed WCAG failures.

- **POT-01 — Search and comparison feedback after GET navigation.** `Search.razor` marks empty-result sections `aria-live="polite"`; `Compare.razor` uses `role="status"` for invalid selections. The forms perform full GET navigation, so source inspection cannot establish what VoiceOver or NVDA announces after submission or whether users discover the updated state promptly. Verify on real screen readers before wider alpha.
- **POT-02 — External links open a new tab.** Source links in `Category.razor:64` use `target="_blank"` without an explicit new-tab notice in their link text. This is not, by itself, a confirmed WCAG failure, but may surprise some users. Decide whether to disclose the context change or open sources in the same tab.

## E. Checks that passed

- Sampled home, country, category, search, compare, empty-category, not-found, and unknown-country pages rendered one `h1` and one `main` in Ukrainian, English, and Russian. Their document `lang` and page titles matched the selected culture.
- The language selector is a named `nav`; each language link has a corresponding `lang` attribute and the current language is marked with `aria-current`.
- Search labels reference their input/select IDs; comparison labels reference all three native select controls. Submit actions use buttons; reset and navigation actions use links.
- The home page keyboard sequence proceeded from skip link to language links, preview links, then country links. The skip link became visible with keyboard focus and targeted `#main-content`. Visible `:focus-visible` rules use a 3px outline; the declared blue outline is 5.41:1 against white and 5.08:1 against the page background.
- Main text pairs checked exceeded 4.5:1: for example, `#425675` on white is 7.45:1 and `#155eef` on white is 5.41:1. Button text and visible link text meet the same minimum in the checked combinations.
- The sampled links and controls met the 24×24 CSS-pixel minimum: home country links measured 24px high; search/compare controls measured at least 44px high; language links measured 36px high.
- At 375 CSS px, the representative route matrix showed no document-level horizontal overflow in all three cultures. The populated employment article also had no overflow at 768 or 1440 CSS px. Long source titles wrap within the article card.
- Article metadata uses a definition list and `<time>` elements; article sources use a list; decorative arrows are hidden from assistive technology. Status is conveyed in text, not color alone.

## F. Multilingual considerations

All three resource files have the same 109 nonempty keys, and every literal localization key found in the application resolves in Ukrainian, English, and Russian. Browser checks confirmed localized document titles, headings, labels, and error/empty-state content on the sampled routes. Language names remain in their own scripts and each link declares its language. The landing-page country link-name mismatch is present in English and Ukrainian; Russian currently retains the visible phrase within its accessible name.

## G. Mobile accessibility considerations

At 375 CSS px the tested pages and all three article translations reflowed without horizontal document scrolling. The 375px check is not a substitute for 200% text resizing, 400% zoom/reflow at a 320 CSS px viewport, or WCAG text-spacing overrides. Those checks remain open. No exact 200% browser text zoom test was performed.

## H. Recommended fixes

1. Correct `#9aabc2` control-boundary contrast on search and comparison forms (A11Y-01).
2. Align country-card accessible names with the visible localized link text (A11Y-02).
3. Verify GET-result and comparison-message announcements with actual screen readers (POT-01).
4. Decide and document the new-tab behavior for official source links (POT-02).

## I. Manual verification checklist

- [ ] Test the private-alpha HTTP Basic Authentication prompt with keyboard-only navigation and a screen reader in supported browsers. This audit ran only in local Development mode.
- [ ] With VoiceOver/Safari and NVDA/Firefox or Chrome, navigate by landmarks and headings, use the language switcher, submit search and comparison forms, and confirm error/no-result messages are discoverable and understandable.
- [ ] Verify every country-card link by its visible spoken label in English and Ukrainian after remediation.
- [ ] Test 200% text resizing, 400% zoom/reflow at 320 CSS px, and WCAG text-spacing overrides in all three languages.
- [ ] Confirm keyboard focus remains visible and unobscured on all pages, including long article/source lists and at narrow widths.
- [ ] Confirm whether external source links announce their new-tab behavior, or revise the behavior accordingly.
- [ ] Recalculate all changed text and control-boundary color pairs after remediation.

## J. Release-readiness assessment

**Not ready to claim WCAG 2.2 AA conformance.** Two confirmed issues should be addressed before family testing. Screen-reader behavior, zoom/reflow, and the production authentication challenge still require manual verification. The audit did not access a deployed service or make application changes.

### WCAG references

- [WCAG 2.2 Understanding 1.4.11 — Non-text Contrast](https://www.w3.org/WAI/WCAG22/Understanding/non-text-contrast.html)
- [WCAG 2.2 Understanding 2.5.3 — Label in Name](https://www.w3.org/WAI/WCAG22/Understanding/label-in-name.html)
- [WCAG 2.2 Understanding 2.4.7 — Focus Visible](https://www.w3.org/WAI/WCAG22/Understanding/focus-visible.html)
- [WCAG 2.2 Understanding 1.4.10 — Reflow](https://www.w3.org/WAI/WCAG22/Understanding/reflow.html)
