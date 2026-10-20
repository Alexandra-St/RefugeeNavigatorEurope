# Spain Comparison V2 Review

Baseline:
8d9758856aac0ffaa670b669812697b0f26a76c9

## Executive result

- facts reviewed: 24
- BLOCKER: 0
- CORRECTION: 0 open (the earlier clipping report was not reproduced with exact viewport control)
- POLISH: 0
- visual checkpoints completed: all six EN / UK / RU checks at 1440 × 900 and 390 × 844; 900 × 720 was also retested in all three languages.
- recommendation: SPAIN COMPARISON V2 READY FOR PUBLICATION GATE

## Fact review

| Aspect | Availability | Verdict | Finding |
| --- | --- | --- | --- |
| protection-scheme | Available | OK | EU and Spain-specific groups remain distinct; nationality alone is not presented as sufficient. |
| protection-application | Available | OK | Police/CREADE route is conditional; no universal checklist or unstable appointment detail. |
| protection-dates | Unavailable | OK | Intentional fallback; EU and Spanish TIE dates are not combined. |
| residence-routes | Available | OK | Route-specific options; no automatic conversion or guaranteed approval. |
| residence-conditions | Available | OK | Conditions and authority assessment are retained; not a complete eligibility catalogue. |
| residence-filing | Available | OK | Filing is limited to applicable in-country routes; transition sequence is retained. |
| employment-vacancies | Available | OK | SEPE, regional vacancy services and EURES are distinguished; no job guarantee. |
| employment-institutions | Available | OK | Address-competent regional services and the Ceuta/Melilla SEPE exception are retained. |
| employment-assistance-conditions | Available | OK | Registration is distinct from benefits; no job, course place or free training is guaranteed. |
| work-permission | Available | OK | Authorization is tied to a granted TP decision; a pending application, receipt or NIE alone is insufficient. |
| work-employer-duties | Available | OK | Spain-specific duties remain qualified and are not presented as a complete checklist. |
| work-minimum-pay | Available | OK | €1,221/month, €40.70/day and €17,094/year are explicitly 2026 gross figures; no net-pay inference. |
| healthcare-access | Available | OK | Applicable national framework and regional administration are both retained. |
| healthcare-proof | Available | OK | Entitlement and health-system record/card are distinguished; no rejected receipt-to-card workflow. |
| healthcare-urgent | Available | OK | 112 is stated nationwide; 061 is not generalized. |
| housing-initial | Available | OK | Reception is conditional and assessed; no particular place, location, duration or cash is guaranteed. |
| housing-private-rental | Available | OK | LAU habitual-residence scope, separate additional guarantee and current-law caveat are preserved. |
| housing-financial-support | Unavailable | OK | Intentional fallback; no unsupported housing-support claim. |
| education-school-enrolment | Available | OK | Regional implementation is clear; no chosen-school or place guarantee. |
| education-pupil-language | Available | OK | Support and hours vary regionally; no universal hours or course guarantee. |
| education-higher-admission | Available | OK | Vocational, university admission and qualification recognition remain distinct. |
| social-subsistence | Available | OK | Reception support is conditional and assessed; no universal fixed TP payment. |
| social-application | Available | OK | IMV is separate and means-tested; TP alone does not establish eligibility. |
| social-child-benefits | Unavailable | OK | Intentional fallback; no unsupported child-benefit claim. |

## Cross-language consistency

EN / UK / RU retain the same material scope, dates, amounts, conditions and degree of certainty across the 24 facts. No material semantic divergence or stronger translated entitlement was found. Unavailable fallbacks are exact in all three languages. No Czech military terms or age cohorts appear in the Spain facts.

## Source ownership

All 21 available facts have at least one source. Every referenced source exists, belongs to the linked article's `InformationEntryId`, and supports the fact's specific comparison statement. The three unavailable facts have no source IDs. No source ownership or availability-semantics issue was found.

## Visual review

- EN, nearest available desktop capture: 900 × 720; route `/compare?country1=spain&country2=germany&category=all`; Spain/Germany and All categories selected. Spain is available in the selector, the Draft preview notice and all six category groups render, and the country panels are correctly ordered. **Correction:** the third selector and right-side comparison content are clipped beyond the available viewport. Screenshot: browser capture output emitted in this review session; no persistent file path was provided.
- EN, requested mobile checkpoint (~390 px): **incomplete**. Browser viewport could not be resized or emulated; the 900 × 720 capture does not verify mobile reflow. Screenshot: same EN browser capture output; not evidence of a mobile viewport.
- UK, requested mobile checkpoint (~390 px): **incomplete** for the same viewport limitation. UK labels, Draft notice and fallback text were inspected at 900 × 720. Screenshot: UK browser capture output emitted in this review session; no persistent file path was provided.
- RU, requested mobile checkpoint (~390 px): **incomplete** for the same viewport limitation. RU labels, Draft notice and fallback text were inspected at 900 × 720. Screenshot: RU browser capture output emitted in this review session; no persistent file path was provided.

The source disclosure opened successfully. Source titles, links and checked dates were readable at the tested viewport; the external-link marker wrapped onto its own line without obscuring the title. The unavailable fallback is visible rather than blank, and the full-article comparison control remains collapsed and usable. No raw localization keys appeared. A mobile overflow/reflow conclusion cannot be made from the available viewport.

## Required corrections before publication

- **CORRECTION:** Review and correct the selector/comparison clipping observed at 900 × 720, then verify the requested ~390 px mobile reflow and ~1440 px desktop layout in a resizable browser.

## Optional polish

None.

## Publication recommendation

SPAIN COMPARISON V2 READY FOR PUBLICATION GATE

## Correction pass — 20 October 2026

The prior clipping finding was retested in the local Development application using Brave 155.1.97.57 with exact CSS viewport dimensions. It did not reproduce, so no CSS or test change was justified.

- At 900 × 720, the page and document widths were 900 px in EN, UK and RU. The three selectors fit their grid cells, and the right comparison panels ended at approximately 853 px.
- At 1440 × 900, the page and document widths were 1440 px in all three languages. All six category groups and both country columns were visible without elements crossing the viewport edge.
- At 390 × 844, the page and document widths were 390 px in all three languages. The comparison columns used one column, all six category groups remained readable, and no horizontal overflow or clipped element was found.
- On the 390 px EN view, the full-article comparison, a category, an article detail and a source disclosure were expanded. Their content remained within the viewport; the article detail and source links rendered successfully.
- Language, selected countries (Spain / Germany), selected category (All), localized headings, Draft notice, unavailable answers and HTTPS source links were verified in the rendered page. The full-article comparison remains collapsed by default.

Full-page screenshots from the controlled browser checks were saved outside the repository under `/private/tmp/SpainCompareReview/` (`en-1440.png`, `uk-1440.png`, `ru-1440.png`, `en-390.png`, `uk-390.png`, `ru-390.png`). They are review artifacts only and are not committed.

The earlier screenshot-based clipping report is retained above as historical evidence. The present exact-viewport checks show no reproducible clipping defect. No facts, article content, statuses or source data were changed.
