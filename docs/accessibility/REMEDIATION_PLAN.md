# Accessibility Remediation Plan — Phase 3.6B

This plan is based on [AUDIT_3_6B.md](AUDIT_3_6B.md). Items are independent and should be reviewed before implementation. No fixes are included in this audit commit.

## P0 — Must fix before family testing

### P0-1 — Increase form-control boundary contrast

- **Problem addressed:** A11Y-01; the `#9aabc2` boundary is 2.34:1 against white, below WCAG 2.2 SC 1.4.11.
- **Affected components:** Search input and filters; comparison selects.
- **Affected files:** `Components/Pages/Search.razor.css`, `Components/Pages/Compare.razor.css`.
- **Proposed change:** Select a border color with at least 3:1 contrast against the adjacent control surface. Preserve the existing focus outline and layout.
- **Acceptance criteria:** Every affected control boundary reaches 3:1 or higher against its adjacent surface; focus remains visible; no control becomes less distinguishable in any supported culture.
- **Suggested automated tests:** Add a lightweight regression check using the existing Python CI runtime to calculate the sRGB contrast of the declared boundary and surface colors; no browser package is required.
- **Manual verification:** Inspect search and compare forms at desktop and 375px widths, including keyboard focus and high-contrast settings.

### P0-2 — Match accessible country-link names to visible text

- **Problem addressed:** A11Y-02; English and Ukrainian `aria-label` values replace the visible country link text instead of containing it.
- **Affected component:** Landing-page country cards.
- **Affected files:** `Components/Pages/Home.razor`, localization resources for `Home.CountryLinkLabel` and `Home.ExploreCountry` if retained.
- **Proposed change:** Remove the redundant `aria-label`, or revise the accessible name so that it starts with and contains the entire visible label for each country in Ukrainian, English, and Russian.
- **Acceptance criteria:** For every country and culture, the accessibility tree name contains the rendered visible link text in the same order; no raw localization key is exposed.
- **Suggested automated tests:** Add a parameterized test for all three cultures asserting that the visible label is a substring of the computed/localized accessible label; retain a browser accessibility-tree check where available.
- **Manual verification:** Use speech input or a screen reader's link list to activate each country link by its visible name in all three languages.

### P0-3 — Verify the private-alpha authentication prompt

- **Problem addressed:** The local audit did not exercise the production HTTP Basic Authentication challenge; browser-native prompt accessibility is not established by middleware tests.
- **Affected component:** Private Alpha access gate (`PrivateAlphaAuthenticationMiddleware`) and its browser challenge flow.
- **Proposed change:** Run a controlled private-alpha accessibility check with synthetic credentials on an authorized non-production environment; if the native challenge is not operable, review an accessible authentication flow before family access.
- **Acceptance criteria:** A keyboard-only user can reach, enter, submit, and dismiss the prompt; a screen reader identifies the prompt and result; the application remains protected.
- **Suggested automated tests:** Keep existing authentication tests for challenge status and protection behavior. Automated HTTP tests cannot establish native prompt accessibility.
- **Manual verification:** Test with VoiceOver/Safari and NVDA/Firefox or Chrome. Do not place credentials in screenshots or test output.

## P1 — Should fix before wider alpha

### P1-1 — Verify search and comparison feedback announcements

- **Problem addressed:** POT-01; result and selection feedback is rendered after full GET navigation, so actual screen-reader announcement and focus behavior need confirmation.
- **Affected components:** `Pages/Search.razor`, `Pages/Compare.razor`.
- **Proposed change:** First test the current `aria-live="polite"` and `role="status"` behavior. If feedback is not discoverable after navigation, add a visible, localized result/error summary near the form and an appropriate announcement/focus strategy.
- **Acceptance criteria:** After search submission or an invalid comparison selection, users can identify what changed, understand any error, and continue to the results or correct fields without relying on visual position alone.
- **Suggested automated tests:** Extend rendering tests to assert localized status/error content and its semantic role for each culture; add an automated browser check only if the project already has a supported browser-test harness.
- **Manual verification:** Test no-content, no-results, invalid-country, invalid-category, and same-country cases with VoiceOver and NVDA.

### P1-2 — Verify zoom and reflow beyond the 375px check

- **Problem addressed:** The audit found no horizontal overflow at 375px, but did not test 200% text resizing, 400% zoom/reflow, or text-spacing overrides.
- **Affected components:** Shared page layouts, search/comparison forms, language switcher, long localized content.
- **Proposed change:** Test at 200% text size and at 320 CSS px equivalent; fix only any concrete overflow, clipping, or obscured content found.
- **Acceptance criteria:** Content and controls remain available without two-dimensional scrolling for ordinary page content; translated labels and actions remain readable and operable.
- **Suggested automated tests:** Add browser smoke checks for horizontal overflow at a 320 CSS px viewport if a browser runner is already approved for CI.
- **Manual verification:** Inspect all three languages at the specified zoom/text spacing, including a populated article and forms.

## P2 — Can improve later

### P2-1 — Make external-link context predictable

- **Problem addressed:** POT-02; official source links open a new tab without telling users.
- **Affected component:** `Pages/Category.razor` source list.
- **Proposed change:** Decide whether new tabs are needed. If retained, include a concise localized indication that a new tab opens; otherwise remove the new-tab behavior.
- **Acceptance criteria:** The navigation context is predictable for sighted, keyboard, and screen-reader users in all three languages.
- **Suggested automated tests:** Assert that each external source link either stays in the current context or has the localized new-tab indication in its accessible name.
- **Manual verification:** Activate a source link by keyboard and screen reader and confirm the context change is announced or otherwise clear.
