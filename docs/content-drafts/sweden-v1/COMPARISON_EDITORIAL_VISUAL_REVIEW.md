# Sweden Comparison V2 Editorial & Visual Review

## Executive result

- **Baseline:** `78bfb112c5e730031afa4533cdcc58527a936881`
- **Facts reviewed:** 24 Sweden facts across all 8 comparison topics / 24 aspects; EN, UK and RU checked.
- **Findings:** 0 BLOCKER, 2 CORRECTION, 3 POLISH.
- **Recommendation:** **READY FOR CORRECTION PASS**.
- This is a review record, not editorial approval or authorization to publish.

## Fact verdicts

Each answer was checked against the candidate claim mapping in the [Sweden content proposal](../../features/SWEDEN_CONTENT_V1_PROPOSAL.md) and its linked article matrix. “Owned sources” means every listed production source exists and its `InformationEntryId` matches the fact’s mapped article. Available answers have at least one such source; the unavailable answer has none.

| # | Category / aspect | Article | Candidate claims | Production sources | EN / UK / RU verdict |
| ---: | --- | --- | --- | ---: | --- |
| 1 | temporary-protection / protection-scheme | SE-TP-01 | SE-TP01-01, 02, 14 | 2, owned | OK |
| 2 | temporary-protection / protection-application | SE-TP-01 | SE-TP01-08, 09 | 2, owned | OK |
| 3 | temporary-protection / protection-dates | SE-TP-01 | SE-TP01-01, 03, 10 | 3, owned | OK |
| 4 | temporary-protection / residence-routes | SE-TP-02 | SE-TP02-01, 02, 12 | 2, owned | OK |
| 5 | temporary-protection / residence-conditions | SE-TP-02 | SE-TP02-01–11 | 3, owned | OK |
| 6 | temporary-protection / residence-filing | SE-TP-02 | SE-TP02-01–05 | 3, owned | OK |
| 7 | employment / employment-vacancies | SE-EMP-01 | SE-EMP01-01 | 2, owned | OK |
| 8 | employment / employment-institutions | SE-EMP-01 | SE-EMP01-02, 03 | 2, owned | OK |
| 9 | employment / employment-assistance-conditions | SE-EMP-01 | SE-EMP01-04–07 | 1, owned | OK |
| 10 | employment / work-permission | SE-EMP-02 | SE-EMP02-01 | 1, owned | OK |
| 11 | employment / work-employer-duties | SE-EMP-02 | SE-EMP02-02, 03, 08 | 3, owned | OK |
| 12 | employment / work-minimum-pay | SE-EMP-02 | SE-EMP02-06 | 1, owned | OK |
| 13 | healthcare / healthcare-access | SE-HEALTH-01 | SE-HEALTH01-01–03 | 2, owned | POLISH-03 |
| 14 | healthcare / healthcare-proof | SE-HEALTH-01 | SE-HEALTH01-05, 06 | 2, owned | CORRECTION-01 |
| 15 | healthcare / healthcare-urgent | SE-HEALTH-01 | SE-HEALTH01-03, 04 | 3, owned | OK |
| 16 | housing / housing-initial | SE-HOUSING-01 | SE-HOUSING01-01–04 | 1, owned | OK |
| 17 | housing / housing-private-rental | SE-HOUSING-01 | SE-HOUSING01-05–07 | 3, owned | OK |
| 18 | housing / housing-financial-support | SE-HOUSING-01 | SE-HOUSING01-02, 03 | 1, owned | OK |
| 19 | education / education-school-enrolment | SE-EDU-01 | SE-EDU01-01–07 | 2, owned | POLISH-02 |
| 20 | education / education-pupil-language | SE-EDU-01 | SE-EDU01-08–11 | 3, owned | OK |
| 21 | education / education-higher-admission | SE-EDU-01 | SE-EDU01-12–14 | 3, owned | OK |
| 22 | social-assistance / social-subsistence | SE-SOC-01 | SE-SOC01-01–03, 06, 08–09, 13–16 | 3, owned | CORRECTION-02 |
| 23 | social-assistance / social-application | SE-SOC-01 | SE-SOC01-04–05, 10–12 | 3, owned | OK |
| 24 | social-assistance / social-child-benefits | SE-SOC-01 | SE-SOC01-13, 15–16 | 0; unavailable | OK — unresolved answer is explicit and has no sources |

The corresponding accepted matrices are [temporary protection](../../content-research/sweden-batch-2026-10-20/temporary-protection-application/SOURCES_AND_CLAIMS.md), [residence after protection](../../content-research/sweden-batch-2026-10-20/after-temporary-protection/SOURCES_AND_CLAIMS.md), [finding a job](../../content-research/sweden-batch-2026-10-20/finding-a-job/SOURCES_AND_CLAIMS.md), [employment rights](../../content-research/sweden-batch-2026-10-20/employment-rights/SOURCES_AND_CLAIMS.md), [healthcare](../../content-research/sweden-batch-2026-10-20/healthcare/SOURCES_AND_CLAIMS.md), [housing](../../content-research/sweden-batch-2026-10-20/housing/SOURCES_AND_CLAIMS.md), [education](../../content-research/sweden-batch-2026-10-20/education/SOURCES_AND_CLAIMS.md), and [social assistance](../../content-research/sweden-batch-2026-10-20/social-assistance/SOURCES_AND_CLAIMS.md).

## Findings

### CORRECTION-01 — Healthcare registration cutoff is incomplete

- **Aspect:** `healthcare-proof`; EN / UK / RU.
- **Observed:** The answer gives the first-12-month bar and the possible further-six-month residence test, but omits the matrix’s current cohort qualification: for first permits granted on or after 5 September 2026, Skatteverket says registration is not normally possible under the current 4 March 2028 endpoint, subject to stated exceptions.
- **Accepted boundary:** SE-HEALTH01-05; the individual decision belongs to Skatteverket. The matrix maps this fact to Act 2024:691 and Skatteverket guidance (production source IDs `da69130e-5b7e-4087-b061-f4bea7c3d384` and `4f474221-b476-49e3-a66b-7651d6ae9a2f`).
- **Required correction:** State the 5 September cohort limitation and its exception-qualified nature consistently in all three languages. Do not imply that registration becomes available after the generic 12-month period for every cohort.

### CORRECTION-02 — Current and future municipal-assistance rules need to be explicit

- **Aspect:** `social-subsistence`; EN / UK / RU.
- **Observed:** The answer says that municipal assistance is a separate needs-based assessment and that current rules must be distinguished from the 2 January 2027 change, but does not state the key current boundary: while a person remains covered by the Reception Act, current law bars Chapter 12 §1 economic assistance. The sentence leaves the practical distinction unclear.
- **Accepted boundary:** SE-SOC01-08/09; after Reception Act coverage ends, a registered TPD holder may apply under ordinary needs-based rules, which remain individually assessed. The amendment starts on 2 January 2027 and must not be described as current. Relevant linked production source IDs include `c1ba3594-7cfc-4c92-abcb-c1819fdf63cd` and `e0c6e5e6-c491-4394-90fc-729ee9d05c3d`.
- **Required correction:** State the present exclusion, the separate post-coverage application route, and the amendment’s future effective date without implying that registration guarantees assistance.

### POLISH-01 — Development description conflicts with Draft preview

- **Aspect:** Comparison-page introduction; EN / UK / RU.
- **Observed:** The generic description says users compare “reviewed information,” while Development displays Sweden Draft facts. A local-preview notice and Draft labels clarify that state, so this is not a Production exposure issue.
- **Accepted boundary:** Draft facts are previewable in Development only; only approved structured facts are available in Production.
- **Required correction:** Consider neutral wording or a Development-specific description that does not imply the preview facts have completed review.

### POLISH-02 — “Home municipality” is overly literal in UK/RU

- **Aspect:** `education-school-enrolment`; UK / RU.
- **Observed:** The translations use literal equivalents of “home municipality” (“домашній муніципалітет” / “домашний муниципалитет”), which sound unnatural in this context.
- **Accepted boundary:** The responsible municipality arranges education for eligible children; the wording must not imply a guaranteed chosen-school place.
- **Required correction:** Use a natural equivalent such as “the municipality responsible for the child’s place of residence,” preserving the same responsibility and no-guarantee qualification.

### POLISH-03 — Awkward RU healthcare wording

- **Aspect:** `healthcare-access`; RU.
- **Observed:** “владелец охватываемого разрешения” is an awkward literal construction for a holder of a permit covered by the rule.
- **Accepted boundary:** The rule applies only to a covered temporary-protection permit holder who is population-registered; patient fees and regional arrangements may apply.
- **Required correction:** Use natural wording such as “человек с разрешением на временное проживание, на которое распространяется это правило,” without broadening the covered permit scope.

## Cross-language consistency

- All 24 facts contain EN, UK and RU values; no raw resource keys appeared in the rendered page.
- Numeric/date tokens match across the three versions. Manual review found the main conditions, exceptions, uncertainty and modal strength aligned; UK/RU are not more categorical than EN.
- The Swedish permit-date distinction (EU scheme through 4 March 2028; Swedish permit cohorts through 4 March 2027 / 4 March 2028) is kept separate. No individual permit expiry is inferred.
- The 5 August 2026 military-rule boundary is not asserted as a Swedish age-band rule in these comparison answers. No Czech age split or unsupported Swedish cohort was introduced. The article-level first-time-applicant guidance remains subject to its own freshness and scope review.
- Conditional programme access, work authorization, regional healthcare, rent arrangements, school placement and benefit decisions remain qualified. No minimum-wage amount, guaranteed placement, accommodation or benefit is asserted.
- `social-child-benefits` says only that the answer was not established, in all three languages, and has no source references.

## Source integrity

- 23 available facts have 51 source references across 44 distinct source IDs; every reference resolves to an existing production source owned by the fact’s mapped article.
- The sole unavailable aspect, `social-child-benefits`, has `IsAvailable = false` and an empty source list.
- The eight Sweden article entries each have exactly three translations (`en`, `uk`, `ru`), all `NeedsReview` (numeric value `0`).
- All source IDs and ArticleIds match the proposal’s topic/article mapping. No source records, facts, article translations or statuses were changed in this review.
- The 44 referenced production sources show `LastCheckedAt` on 20 October 2026. This records the current catalog check date; it is not a guarantee of continued legal accuracy.

## Visual review

### Desktop

- **Status:** Complete for desktop visual observation at approximately 1280 × 720 CSS pixels in the local Development application.
- Sweden was selected against Denmark with all categories visible. The page exposed all eight topics and 24 aspects; Swedish Draft badges and the local-preview notice appeared, source disclosures opened to titled external links with check dates, and the unavailable child-benefit answer rendered without a source control.
- EN, UK and RU were viewed. Country/category selectors and page strings localized, and the fact text matched the selected language. No raw keys, clipped text, or horizontal overflow were observed in the inspected desktop view. Long answer text wrapped inside the two-column cards.
- A visible copy inconsistency remains: the generic page description says “Compare reviewed information” (localized equivalents) while Development also shows explicitly labelled Draft facts. The prominent local-preview notice and per-fact Draft labels clarify the state; this is polish, not a production visibility defect.

### Mobile

- **Status:** Incomplete. This browser session does not expose a controllable viewport override, so a true mobile-width render was not inspected.
- CSS has a one-column layout breakpoint at 800px and reduced page padding at 540px; card containers use `min-width: 0` and long text/source URLs use wrapping rules. These source-level checks do not substitute for a 390px visual check.
- **Manual checklist:** At 390px, load `/compare?country1=sweden&country2=denmark&category=all` in EN, UK and RU; check that selector labels and options fit, all 24 aspects remain readable in a single column, long dates/statute names and source links wrap without horizontal scrolling, `social-child-benefits` remains visibly unresolved with no Sweden source links, source disclosures can be opened by keyboard/touch, and the “Read full country articles” disclosure remains clear and usable.
- No persistent screenshots were saved. Desktop observations were made live in the local browser.

## Development / Production gating

- **Development:** The local app started successfully in Development. Sweden was selectable and all 24 Draft facts rendered with the draft notice and Draft labels in EN / UK / RU.
- **Production:** Existing component tests rendered the same Sweden comparison under `Environments.Production` in all three languages. They confirmed the Development notice, Draft labels and Sweden Draft answer text were absent. No production deployment or live service was accessed.
- Focused validation: 4 existing Sweden comparison tests passed, covering 24 facts, source ownership, one unavailable aspect, localized selector/preview output, and Development-versus-Production gating.
- Article translations remain `NeedsReview`; no status or publication logic was changed.

## Freshness gates

Recheck official material immediately before any publication gate, especially:

1. Sweden Migration Agency extension instructions and opening/booking state for the expected early-2027 cycle; it was not open in the reviewed guidance.
2. Current permit date cohorts, the 5 August 2026 military-obligation rule and its exact first-time-applicant scope; do not generalize to repeat/interrupted cases or borrow another country’s age rule.
3. Skatteverket registration guidance, including the 5 September 2026 cohort and stated exceptions.
4. SFI/komvux requirements and the changes expected from 1 January 2027.
5. The Social Services Act amendment effective 2 January 2027 and current Reception Act interaction.
6. Child/housing-allowance cohort guidance and individual statutory conditions.
7. Higher-education fee rules already effective from 1 October 2026, including current exceptions and proof requirements.

These are freshness checks, not findings that the underlying rules have changed.

## Required corrections

- Resolve CORRECTION-01 and CORRECTION-02 in the affected structured answers in EN / UK / RU, then repeat the targeted evidence and translation review.
- Complete the 390px visual review in EN / UK / RU.
- Do not promote any Sweden fact or translation to an approved status as part of this review.

## Optional polish

- POLISH-01 through POLISH-03 are optional wording improvements; none changes the legal scope or publication gate.

## Publication-gate recommendation

**READY FOR CORRECTION PASS**

There are two factual-scope corrections, and mobile visual review is incomplete. No blocker was found in the reviewed production visibility gate, but this report does not approve or publish Sweden content.
