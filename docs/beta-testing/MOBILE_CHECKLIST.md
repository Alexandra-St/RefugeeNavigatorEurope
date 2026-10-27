# Mobile testing checklist

Test at least one portrait phone and a narrow viewport of approximately 375–390 CSS pixels. These are manual usability checks; CI does not verify visual usability.

- [ ] Basic Authentication prompt is understandable and does not expose credentials in screenshots or notes.
- [ ] Home, country, and category navigation is usable and orientation remains clear.
- [ ] Search field, filters, and result cards fit and remain usable with the virtual keyboard open.
- [ ] Comparison country selectors, category selector, cards, and country labels are readable.
- [ ] Spot-check a Category page from Czechia or Spain and a Netherlands Category or Country Comparison page at a narrow viewport; confirm that “information unavailable” is understandable as an evidence gap, not proof that a right or service does not exist.
- [ ] Full-article comparison disclosure is discoverable and can be expanded/collapsed.
- [ ] Guide scenario cards remain one-column where appropriate, with readable text and usable touch targets.
- [ ] Guide step text and long warning blocks wrap without clipping.
- [ ] Previous, Next, and Change situation controls are understandable and usable.
- [ ] Official-source links wrap, remain distinguishable, and are easy to activate.
- [ ] Language switcher is visible and operable.
- [ ] No horizontal overflow or clipped content appears on tested pages.
- [ ] Touch targets are practical without reducing normal body-text readability.
- [ ] Virtual keyboard does not hide the active search field or prevent access to results and filters.
- [ ] Try 200% text zoom where the browser supports it; check narrow reflow and visible focus.
