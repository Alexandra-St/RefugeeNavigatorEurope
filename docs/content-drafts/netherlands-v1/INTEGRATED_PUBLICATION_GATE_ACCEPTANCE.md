# Netherlands Integrated Publication Gate Acceptance

**Baseline:** `94c61ef6839e014e53ab48d86f437174ed99c723`
**Review date:** 21 October 2026
**Verdict:** **TECHNICAL GATE PARTIALLY VERIFIED**

This review checks how the existing Core article gate and Comparison V2 per-fact gate interact. It is a technical acceptance record only; it does not approve Netherlands content, change editorial statuses, or authorize publication or deployment.

Related gate records: [Core production visibility gate](CORE_PRODUCTION_VISIBILITY_GATE.md) and [Comparison per-fact production gate](COMPARISON_PER_FACT_PRODUCTION_GATE.md).

## Core article visibility

The Production category component filters information entries to translations whose status is `Verified` in the active language before rendering article markup, source details, or article anchors. Direct category URLs use this same component and gate. Development retains its editorial preview behavior.

Focused regression coverage passed for English, Ukrainian, and Russian. It verifies that `NeedsReview` and `Outdated` article titles, body content, sources, and anchors are omitted while `Verified` content remains visible. Mixed translation statuses are evaluated independently by language.

The Netherlands-specific regression fixture confirms that TP-01 remains hidden when its selected-language translation is not `Verified`, while TP-02 can be independently visible when its selected-language translation is `Verified`. The currently checked-in Netherlands translations remain held, so the live local Production category route returned HTTP 200 with the expected empty-state placeholder and did not expose the held articles or their sources.

## Comparison V2 visibility

Comparison facts are gated independently in Production. A fact is exposed only when it is approved, available, has a valid linked article, has nonempty source IDs owned by that article, and the linked article translation is `Verified` in the selected language. A blocked fact uses the generic unavailable presentation; its draft answer and source details are not rendered. Draft preview remains available in Development.

Focused tests cover independent approved/draft facts, source ownership, active-language article status, and the Netherlands TP-01/TP-02 relationship. They also confirm that the source-less, unavailable `residence-filing` fact remains unavailable without blocking other eligible facts. These tests use synthetic fixtures for eligible Netherlands cases; those fixtures do not change the checked-in catalog or constitute content approval.

In the local Production browser check, Netherlands answers remained generic and showed no source details in English, Ukrainian, and Russian. Eligible comparison content for the other selected country remained visible. The Netherlands result therefore matched its current held catalog state.

## Combined-gate assessment

The Core and Comparison routes apply separate publication checks to their respective content types. No path was found that causes a held Netherlands article to appear through the category page or through a comparison answer when its active-language translation is not `Verified`. The test suite exercises each gate and their Netherlands-specific cross-references with synthetic status combinations; this was not a single end-to-end HTTP test that promotes a Netherlands article through both surfaces at once.

Direct category navigation and the comparison route both returned nonempty HTTP 200 responses in the local Production instance. The category showed the expected empty state; comparison showed generic unavailable content for held Netherlands facts. No hidden Netherlands article body or source details were observed in those browser checks.

## Localization and URL checks

The focused regression suite checks the status gate independently for EN, UK, and RU. Local browser checks also confirmed localized Netherlands category and comparison presentations in all three languages. Direct category navigation did not expose an article anchor for held content. Automated tests assert the corresponding article anchors and source details are absent for blocked content.

## Browser and viewport review

The local application was run from its Release-published output in Production mode, using loopback HTTP and public access mode solely for isolated packaging/visibility verification. The category and comparison routes, framework JavaScript, and application CSS returned HTTP 200 with nonempty responses.

Browser rendering was inspected at the available default viewport, but its exact dimensions were not measurable. The requested viewport checks at **1280×720**, **390×844**, and **320×700** were **not verified**. No screenshots were captured. Consequently, this review makes no claim about overflow, clipping, or layout at those specific sizes.

## Validation

The focused `CategoryRenderingTests` and `CountryComparisonTests` run passed: **113 tests passed, 0 failed**. This includes the language-specific and Netherlands-specific synthetic gate cases. The full 566-test suite and local Docker were not rerun for this acceptance pass.

## Remaining limitations and publication prerequisites

- Exact desktop and mobile viewport verification remains outstanding at 1280×720, 390×844, and 320×700.
- The checked-in Netherlands translations and comparison facts remain under their existing review statuses; synthetic eligible test fixtures are not publication approval.
- Editorial review and any required status decisions remain separate from this technical assessment.
- A separate release decision is required before any publication or deployment.

No content status, publication status, production catalog, application code, or deployment configuration was changed as part of this acceptance review.
