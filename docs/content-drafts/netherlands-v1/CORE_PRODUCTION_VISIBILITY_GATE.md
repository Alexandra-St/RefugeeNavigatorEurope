# Core Production Visibility Gate

**Phase baseline:** `9276c9723379e8c603a4b5007e3053ecafb0aa71`

## Defect and rule

The country/category page previously rendered every matching Core entry regardless of its verification status. Because the page is directly addressable, an unverified translation could be reached without using Search; the page also emitted the article anchor and its source list.

In Production, the category page now includes an entry only when the translation for the currently selected language has `VerificationStatus.Verified`. Filtering occurs before article markup and source links are built. An excluded translation therefore contributes no title, summary, body, metadata, source links, or entry anchor. The rule applies equally when the category URL is opened directly.

In Development, the existing editorial preview remains unchanged: entries in `NeedsReview`, `Outdated`, and other statuses remain available for inspection.

## Language isolation and Netherlands regression

The gate evaluates the active `uk`, `en`, or `ru` translation independently. A Verified English translation cannot cause a NeedsReview Ukrainian translation or Outdated Russian translation to render in Production.

Focused regression coverage uses synthetic mixed-status translations and the checked-in Netherlands catalog loaded without mutation. It verifies that NL-TP-01 remains hidden when NL-TP-02 alone is marked Verified in an in-memory test copy. The checked-in article statuses and content are unchanged.

## Other Core access paths

Search already filters results to the active-language Verified translation; no Search code change was needed. Existing Search tests and Netherlands integration tests are included in validation. No additional Core visibility path was identified within the authorized inspection scope.

Country Comparison V2 and step-by-step guides have their own publication gates. Comparison V2 is outside this phase and was not changed.

## Validation

- Category rendering, ArticleSearch, and Netherlands integration tests: **68 passed, 0 failed** in Release. Coverage includes Production filtering for EN/UK/RU, exclusion of complete entries and anchors, mixed translation statuses, Development preview, and Netherlands NL-TP-01/NL-TP-02 isolation.
- `git diff --check`: pending final pre-commit check.

## Publication and deployment

No article status or catalog data was changed. This technical gate does not approve content. No publication action or deployment was performed.
