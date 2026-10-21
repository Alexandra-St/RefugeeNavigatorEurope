# Netherlands Selective Publication Gate Feasibility

**Review date:** 21 October 2026
**Baseline:** `d7cdcc3d02f4374ac608c73275012177265da3f1`
**Purpose:** assess whether the recommended Netherlands records can be exposed selectively under the current application gates. This is a technical/editorial feasibility review only. It does not approve content, change a status, publish, or authorize deployment.

## 1. Decision summary

The current application does **not** safely support selective publication of the seven Core candidates. A production category page renders every entry in that country/category regardless of verification status. The held NL-TP-01 entry shares the temporary-protection category with candidate NL-TP-02, so marking NL-TP-02 Verified would still expose NL-TP-01 through the category page and its direct URL/anchor.

Comparison V2 has a publication gate, but it is selection-wide: every fact in the selected country/category selection must be Approved before structured comparison appears in Production. Keeping the unavailable `residence-filing` fact Draft therefore prevents the structured comparison for the entire temporary-protection category and for `category=all`. In addition, the three protection facts linked to held NL-TP-01 only show their answer text when the selected-language NL-TP-01 translation is Verified.

At this baseline all 24 Netherlands article translations are `NeedsReview`, all 24 Netherlands comparison facts are `Draft`, and `residence-filing` is unavailable with no source IDs. The candidate designations below are recommendations for separate human decisions; none is approved.

## 2. Candidate and hold matrix

### Core articles

| Record | Recommendation | Current EN / UK / RU status | Category and publication consequence |
| --- | --- | --- | --- |
| NL-TP-02 | Candidate for separate approval per the Phase 18.4F evidence resolution | NeedsReview / NeedsReview / NeedsReview | Temporary protection; shares a category page with held NL-TP-01. |
| NL-EMP-01 | Candidate | NeedsReview / NeedsReview / NeedsReview | Employment. |
| NL-EMP-02 | Candidate | NeedsReview / NeedsReview / NeedsReview | Employment. |
| NL-HEALTH-01 | Candidate, within the bounded scope and explicit insurance conflict described in the evidence resolution | NeedsReview / NeedsReview / NeedsReview | Healthcare. |
| NL-HOUSING-01 | Candidate | NeedsReview / NeedsReview / NeedsReview | Housing. |
| NL-EDU-01 | Candidate | NeedsReview / NeedsReview / NeedsReview | Education. |
| NL-SOC-01 | Candidate | NeedsReview / NeedsReview / NeedsReview | Social assistance. |
| NL-TP-01 | **HOLD — evidence conflict** | NeedsReview / NeedsReview / NeedsReview | Temporary protection. Its military/exception and negative-triage boundaries remain unresolved. |

This is the requested seven-candidate / one-hold Core scope. It combines the six Core candidates from the earlier selective review with NL-TP-02, whose recommendation changed from Hold to Candidate in Phase 18.4F. The seven-record Phase 18.4F impact matrix separately assessed NL-TP-01, NL-TP-02 and five comparison aspects; it does not mean all seven Core articles received fresh evidence review. See [the evidence resolution, sections 9–13](TP_RESIDENCE_EVIDENCE_RESOLUTION.md#9-seven-record-publication-impact-matrix) and [the earlier selective review](SELECTIVE_PUBLICATION_GATE_REVIEW.md).

### Comparison V2

The 23 available Netherlands facts are the requested candidate set for separate editorial decisions; all currently remain `Draft`. This set retains the earlier 19 candidate aspects and adds the four aspects whose recommendations changed in Phase 18.4F (`protection-scheme`, `protection-application`, `protection-dates` and `residence-routes`). `residence-filing` is the sole hold: it remains unavailable, `Draft`, and has no official source IDs.

| Candidate aspects | Linked article | Current fact status | Technical dependency |
| --- | --- | --- | --- |
| `protection-scheme`, `protection-application`, `protection-dates` | NL-TP-01 (held) | Draft | Answer text is suppressed in Production while the selected-language linked article translation is not Verified. Keep these facts Draft until NL-TP-01 is resolved or a separately reviewed safe relationship/rendering approach is authorized. |
| `residence-routes`, `residence-conditions` | NL-TP-02 | Draft | Candidate scope is the bounded navigation described in the evidence resolution. |
| `employment-vacancies`, `employment-institutions`, `employment-assistance-conditions` | NL-EMP-01 | Draft | Candidate scope remains conditional and navigational. |
| `work-permission`, `work-employer-duties`, `work-minimum-pay` | NL-EMP-02 | Draft | Cohort, work-form, notice and dated-rate limits remain part of any approval. |
| `healthcare-access`, `healthcare-proof`, `healthcare-urgent` | NL-HEALTH-01 | Draft | Keep the documented conflict and conditional coverage boundaries. |
| `housing-initial`, `housing-private-rental`, `housing-financial-support` | NL-HOUSING-01 | Draft | Preserve placement, locality, scheme and eligibility limits. |
| `education-school-enrolment`, `education-pupil-language`, `education-higher-admission` | NL-EDU-01 | Draft | Preserve conditional placement and all DUO criteria and dates. |
| `social-subsistence`, `social-application`, `social-child-benefits` | NL-SOC-01 | Draft | Keep schemes and eligibility conditions distinct. |
| `residence-filing` | NL-TP-02 | **Draft; unavailable; no sources** | **HOLD — evidence gap.** Keep unavailable and source-less. |

The set is 23 candidate aspects plus one held aspect (24 Netherlands facts total). “Candidate” does not imply Approved, and the three facts linked to NL-TP-01 have the additional runtime dependency shown above.

## 3. Core article visibility

**Code and tests inspected:** [Category.razor](../../../src/RefugeeNavigator.Web/Components/Pages/Category.razor), [Search.razor](../../../src/RefugeeNavigator.Web/Components/Pages/Search.razor), [ArticleSearch.cs](../../../src/RefugeeNavigator.Web/Search/ArticleSearch.cs), [CategoryRenderingTests.cs](../../../tests/RefugeeNavigator.Tests/CategoryRenderingTests.cs) and the Netherlands cases in [CountryComparisonTests.cs](../../../tests/RefugeeNavigator.Tests/CountryComparisonTests.cs).

**Verified from code:** `Category.razor` selects records only by country and category, then renders the current-language translation without checking `VerificationStatus` or the hosting environment. The category page therefore renders `NeedsReview` and `Outdated` content in Production as well as Development. The UI does not show a verification badge, but the article text, title, summary, dates and source links are still rendered.

**Impact:** `/countries/netherlands/categories/temporary-protection` returns both NL-TP-01 and NL-TP-02. A link with `#entry-<article-id>` addresses an anchor on that same category page; it is not a separately gated article endpoint. The category route is directly requestable, so hiding an item from Search alone cannot protect a held article. Current Search filters results to `Verified` translations, but that is only a discovery filter, not an access gate.

The category route chooses the current UI-language translation but does not gate by that translation's status. Thus a language-specific approval decision would not protect an unapproved translation on this route.

## 4. Comparison visibility

**Verified from code:**

- Development can preview Draft facts. Fact text is shown only when it is available, has a valid linked article and owned sources, and either the selected-language article translation is `Verified`, or Development is rendering a Draft fact linked to a `NeedsReview` translation.
- In Production, structured comparison appears only when **all** facts selected by the country pair and category are Approved. The gate is an aggregate condition, not a per-fact filter.
- `FactText` additionally requires the selected language's linked article translation to be `Verified`; unavailable facts, missing relationships or unverified linked translations fall back to the generic “not established” text.
- Full-article comparison reveals title, summary, body and article link only for a `Verified` translation. Official-source details are rendered separately when source records exist, even if that translation is not Verified.
- When a category selection fails the structured gate, Production falls back to the full-article comparison. This still exposes only Verified article translations in that comparison component; it does not make the category page safe.

**Impact:** promoting 23 facts while leaving `residence-filing` Draft will not produce a selective structured panel for `category=all`, because the selected set still contains a Draft fact. The temporary-protection category also contains the held `residence-filing` aspect, so its selected structured panel remains hidden as a whole. Other category selections can render only when every selected fact for both countries is Approved and its linked language translations pass the article gate.

The three protection facts linked to held NL-TP-01 cannot display their answer text in Production while its relevant translation remains NeedsReview. Their fact status alone is insufficient. Do not promote them as independently visible answers unless a separately approved implementation removes that dependency safely or the parent article is resolved and approved.

## 5. Direct URL and linked-article risk

There are no per-article routes in this flow. The navigable article destination is the country/category route with an optional entry anchor. Because that page renders every category entry, a person who knows or receives the category URL can access held article text directly. This is confirmed by the current component logic; the production category URL is not a publication barrier.

In Comparison V2, an unverified linked article does not get a title/body/article link in the full-article panel. However, the source-details block can still render the linked article's source records. More importantly, direct navigation to the Netherlands temporary-protection category exposes NL-TP-01. The existing comparison-to-article gate therefore does not mitigate the category-page exposure.

## 6. Development and Production behavior by record type

| Surface | Development | Production | Selective today? |
| --- | --- | --- | --- |
| Country/category article page | All matching translations render, regardless of status | Same behavior | **No.** Verification status is not checked. |
| Search | Current-language translation is filtered to Verified | Same behavior | Yes, for search results only; direct category URLs remain accessible. |
| Structured comparison | Draft preview can render with a preview notice and Draft labels | Shown only if every fact in the selected set is Approved; answer also needs a Verified linked translation and valid owned sources | **Not per fact.** One Draft fact blocks the selected panel. |
| Full-article comparison | Full comparison available; unverified articles show unavailable state | Verified linked translations render; unverified ones do not | Per linked translation, but only inside this comparison component. |

## 7. EN / UK / RU gating

Core article verification status is stored per translation, and Search checks the selected language's translation. The category page does not check status for any language. Comparison fact status is one status per fact, shared by its EN/UK/RU values; rendering then checks the linked article translation for the active language. This means Comparison has language-specific content suppression through the linked article status, but no independent per-language fact approval.

For safe publication, every Core candidate needs an explicit decision for all three translations. The runtime must suppress each non-Verified language separately on category pages, Search and article expansion. Comparison facts need a clear editorial status decision and the selected-language article dependency must remain enforced unless a different safe policy is reviewed and tested.

## 8. Required technical safeguards

These are recommendations for a later implementation phase, not changes made here:

1. Filter category-page entries in Production to translations with `VerificationStatus.Verified` for the active language. Keep Development preview behavior explicit. Ensure hidden entries contribute no title, summary, body, anchor target, source block or Search link.
2. Change Comparison V2's Production selection-wide gate to a per-fact gate so Approved facts can render independently while Draft facts remain absent or use an explicitly safe unavailable state. Keep unavailable facts unavailable and source-less.
3. Preserve the linked-article safeguard for the three NL-TP-01 facts. Keep those facts Draft while the parent remains held, unless a reviewed architecture allows independently verified facts without exposing or implying approval of the full article.
4. Add tests that render actual Netherlands routes in Production for EN/UK/RU: verified records visible; NeedsReview/Outdated absent from category HTML, anchors, sources and Search; Draft comparison values absent; Approved values visible only when their own linked language translation is Verified; `residence-filing` remains unavailable and has no sources.
5. Test known category and anchor URLs directly, not only search discovery. Verify Development preview remains available and does not change Production behavior.

## 9. Required approval decisions

- Project owner must separately approve each of the seven Core candidates and each translation's readiness; the candidate list is not publication authorization.
- Project owner must separately approve Comparison facts. Keep `residence-filing` unavailable, source-less and Draft.
- Keep NL-TP-01 and its three linked protection facts held until its evidence blocker is resolved or a narrower replacement article and fact mapping are separately reviewed.
- Approval must preserve the candidate scope, dated/conditional claims, evidence conflicts and remaining limitations in the Phase 18.4F resolution.
- Deployment requires a separate release decision after code safeguards, tests, visual checks and current source freshness checks pass.

## 10. Recommended implementation order

1. Resolve the publication policy for NL-TP-01 and its three linked facts; do not infer their release from NL-TP-02 or the fact-candidate list.
2. Implement and test per-language Production filtering on country/category routes, including direct URL and anchor requests.
3. Implement and test per-fact Comparison Production filtering, preserving unavailable and linked-article constraints.
4. Record human approvals for the selected articles and facts; leave held records unchanged.
5. Run the focused and full release checks, visual verification, and the separate release audit before asking for deployment authorization.

## 11. Remaining blockers and limits

- **Existing visual evidence:** the earlier review records nine Netherlands Comparison scenarios passing at 1280×720, 390×844 and 320×700 for EN/UK/RU. No visual review of the Core category exposure issue or this feasibility phase was performed. The prior result does not verify the proposed Production safeguards.
- **Existing test evidence:** `NetherlandsDraftFactsPreviewInDevelopmentAndRemainGatedInProduction` covers the current Comparison Draft preview and Production suppression for the present all-Draft catalog. Category rendering tests verify content and that status labels are absent; they do not assert a Production gate for `NeedsReview` articles.

- **Core publication blocker:** category pages expose all matching article translations regardless of status, including held NL-TP-01.
- **Comparison selectivity blocker:** the Production structured panel is all-or-nothing for the current selected set; one Draft fact blocks other candidates in that selection.
- **Linked-record blocker:** three candidate facts depend on the language-specific Verified status of held NL-TP-01 before their answer text can render.
- **Evidence holds:** NL-TP-01 remains held for E2/M2 and T1 boundaries; `residence-filing` remains unavailable for an evidence gap. NL-TP-02 is a separate candidate under the later Phase 18.4F matrix, but its candidate status is not approval.
- **Validation limit:** source and test inspection establishes the above code paths. A targeted local test run was attempted but the test host could not bind its local VSTest communication socket in this environment (`SocketException: Permission denied`), so no new runtime test result is claimed. Existing Netherlands Comparison tests cover the Draft Development/Production gate; existing Category tests do not establish a Production verification-status gate.
- This review does not assess the remaining 21 comparison records or repeat the full source review.

## 12. Next recommended phase

Create a separately scoped technical implementation phase for the two visibility safeguards (category per-language Production filtering and per-fact Comparison Production filtering), with explicit handling for facts linked to held NL-TP-01. Add focused tests for direct category/anchor access and all three languages. After that, request separate human content approvals and perform the required visual/release checks.

## 13. Explicit non-authorization

This feasibility report changes no production records or statuses. It is not editorial approval, publication, an individual eligibility determination, or deployment authorization. No content was published and no deployment was performed.
