# Netherlands Comparison V2 Per-Fact Production Gate

**Implementation date:** 21 October 2026; **Baseline:** `2052831a87fc33f1b2bc974a79748ba241a9ec7a`

## Change summary

The previous Production gate treated the selected comparison set as a whole: one Draft fact suppressed the structured comparison for the selection. Production now evaluates each fact independently, so an eligible Approved answer can render even when another selected fact is Draft or unavailable.

A fact answer and its fact-specific sources render only when all of the following hold:

- the fact is `Approved` and `IsAvailable`;
- its linked information entry exists;
- it has at least one source ID, and every linked source exists and belongs to that same information entry;
- the active-language article translation is `Verified`.

If any condition fails, the answer is replaced by the existing “not established in reviewed sources” state, and fact-specific source details are omitted. These checks occur in the existing server-rendered component; no CSS hiding or client-side gate is used.

## Mixed statuses and language behavior

In Production, Draft answer text, Draft labels, and fact-source details are not rendered. They do not prevent eligible Approved facts in the same selection from rendering. The selected language is checked independently against its linked article translation, so a Verified translation in one language does not make a `NeedsReview` or `Outdated` translation visible in another.

Development retains Draft fact previews, source details, Draft labels, and the preview notice. No catalog status was changed to exercise these cases; tests use in-memory fixtures.

## Netherlands and full-article behavior

The three Netherlands facts linked to held article NL-TP-01 (`protection-scheme`, `protection-application`, and `protection-dates`) remain suppressed in Production while that language's NL-TP-01 translation is not Verified, even when synthetic test facts are marked Approved. The full-article panel also omits that article's title, summary, body, and source details while its translation is not Verified.

`residence-filing` remains Draft, unavailable, and source-less. It displays the existing unavailable state and does not block other eligible Approved facts.

The full-article comparison fallback remains available as before. In Production it may display article text only for a Verified active-language translation; source details for an unverified article translation are now suppressed. In Development, existing preview behavior is retained.

## Validation

- Focused Comparison, Netherlands content integration, and category rendering tests: **127 passed**.
- Full Release test suite: **566 passed**.
- Release solution build: **passed, 0 warnings, 0 errors**.
- `git diff --check`: recorded with the final change review.
- Visual/browser review and local Docker were not performed.

## Remaining limits and publication status

This is a rendering safeguard, not content approval or legal verification. It does not change any checked-in fact, article, translation, or source status. No publication or deployment was performed.
