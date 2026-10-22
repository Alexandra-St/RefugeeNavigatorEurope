# Phase 18.7.1 — UX/UI Audit & Improvement Plan

Baseline reviewed: `2a4cd604cfed922e1acfebb2017bcf64acefeb68`

Scope: article/category pages and country comparison only. This is an analysis plan; no UI, CSS, localization, or production content was changed.

## Summary

The three reported issues have concrete implementation causes:

1. Article prose is parsed into headings, paragraphs, and unordered lists only. Official sources are rendered as a separate article-level list, so readers cannot follow a source directly from the relevant sentence.
2. The global header, top home link, page top padding, and page-specific navigation/header margins stack vertically, delaying the page title and first content.
3. The article container is capped at `68rem`, while comparison is capped at `72rem`. Article paragraphs already have a readable `72ch` measure; comparison answer text does not have an equivalent explicit measure.

Recommended changes are incremental and preserve the separate official-source lists, comfortable article line length, and existing responsive breakpoints.

## Current implementation

### Article rendering and sources

`Category.razor` renders each localized translation through `ArticleContentFormatter.Parse`. The formatter recognizes only `## ` headings, blocks of `- ` list items, and plain paragraphs. Razor emits the returned text as normal text nodes, so markup-like content is encoded rather than treated as HTML. The category page separately renders `item.Sources` under “Sources”.

`OfficialSource` has an ID, an owning `InformationEntryId`, one title, URL, and `LastCheckedAt`. It is associated with an article, not with an individual sentence, paragraph, or claim. Consequently, selecting a source merely because it belongs to the same article would not establish that it supports the nearby statement. The comparison page similarly renders the structured fact's own source references separately, and its full-article view uses the same article formatter.

Relevant files:

- `src/RefugeeNavigator.Web/Components/Pages/Category.razor`
- `src/RefugeeNavigator.Web/Components/Pages/Compare.razor`
- `src/RefugeeNavigator.Web/Content/ArticleContentFormatter.cs`
- `src/RefugeeNavigator.Domain/OfficialSource.cs`
- `tests/RefugeeNavigator.Tests/DenmarkHousingArticleTests.cs`
- `tests/RefugeeNavigator.Tests/CountryComparisonTests.cs`

### Top spacing

`App.razor` places `SiteHeader` before the routed page. The header has vertical padding of `0.5rem` above and below. Category and comparison pages then render `HomeLink` with `Placement="top"`; its top variant has a `0.25rem` bottom margin. Each page's main container adds `2.5rem` top padding.

The category page adds a further `2rem` below its back link and `2.5rem` below its category header. On mobile (`max-width: 540px`), the page top padding is `1.75rem`, the back-link gap is `1.5rem`, and the header gap is `2rem`. The comparison page uses the same desktop/mobile main padding and adds a `1.5rem` bottom margin after its heading. These are separate, cumulative spaces rather than one oversized margin.

### Content width and responsive layout

The category `.page-content` uses `width: min(100%, 68rem)`. Article prose, headings, and lists are capped at `72ch`, so increasing the outer container does not require stretching long paragraphs. The comparison `.page-content` uses `width: min(100%, 72rem)`; its structured answers are in two `minmax(0, 1fr)` columns, with `overflow-wrap: anywhere` on answer paragraphs. The article comparison also uses two columns. Both comparison grids collapse to one column at `800px`; page side padding drops to `1rem` at `540px`.

`SiteHeader` and the top home link currently use a `72rem` max width. Neither page has a sidebar; there is therefore no sidebar column to reclaim or risk compressing.

## Recommended improvements

### 1. Contextual article source links

**Recommendation:** add a small, explicit source-reference token to the existing article text format and resolve it to an `OfficialSource` owned by that same article. A candidate syntax is `[[source:<official-source-guid>|<localized link label>]]`. The token should produce a typed inline-link block or inline segment; it must never permit arbitrary HTML or arbitrary URL values. The renderer should obtain the actual URL and title/metadata only from the matching `OfficialSource` record.

This avoids duplicating URLs in content and allows distinct natural link labels in EN/UK/RU. It also keeps the existing full source list unchanged as a complete reference section. Because the current model has no sentence-to-source relationship, the source ID must be deliberately selected by an editor for the precise statement; article ownership alone is insufficient. Record that editorial mapping in the existing internal claim/source review workflow, and validate that each token resolves to a source ID belonging to the current article. Never automatically attach the “closest” source.

Render link labels as Razor-encoded text. Accept only resolved official-source records with valid HTTPS URLs; emit external-link protections (`target="_blank"`, `rel="noopener noreferrer"`) consistently with the existing source list. Unknown, malformed, or cross-article IDs should fail validation or remain harmless plain text, never become a link. Keep EN/UK/RU link labels in each translation, preserve the existing list of sources, and do not add a general-purpose Markdown or HTML renderer.

**Alternative:** use Markdown-style `[label](source:<id>)` syntax with a custom parser. This is slightly more familiar to editors but increases the chance of confusion with unsupported Markdown or ordinary links. A source-specific token is less ambiguous. Storing structured blocks instead of text would offer stronger typing but requires a larger data-model and authoring migration; it is not justified for this first improvement.

**Risk:** medium. Parser boundaries, entity encoding, source ownership, fallback behavior, and translated labels need direct tests. The principal content risk is a source being linked to a statement it does not support; validation can verify ownership, but an editor must verify claim-level support.

**Likely files:** `ArticleContentFormatter.cs` or a focused replacement parser/model; `Category.razor`; `Compare.razor` full-article rendering; relevant content-formatting and integration tests; later content updates only after editorial approval. `OfficialSource.cs` and catalog schema should remain unchanged if the token can resolve through the already loaded article source list.

### 2. Reduce excess top spacing

**Recommendation:** first reduce repeated page-level gaps while leaving the 44px navigation and touch targets intact. Suggested first-pass values:

| Selector / value | Current | Proposed |
|---|---:|---:|
| `.category-page`, `.compare-page` desktop top padding | `2.5rem` | `1.75rem` |
| `.category-page`, `.compare-page` mobile top padding | `1.75rem` | `1.25rem` |
| `.back-link` desktop bottom margin | `2rem` | `1.25rem` |
| `.back-link` mobile bottom margin | `1.5rem` | `1rem` |
| `.category-header` desktop bottom margin | `2.5rem` | `2rem` |
| `.category-header` mobile bottom margin | `2rem` | `1.5rem` |
| `.page-heading` bottom margin on comparison | `1.5rem` | `1.25rem` |

Keep the top home-link area at its existing `0.25rem` bottom margin and preserve the header's current padding. This targets the repeated spaces after navigation rather than shrinking controls. The main reductions apply equally to desktop and mobile, with a slightly tighter mobile rhythm. Tablet inherits the desktop values until the existing `540px` rules; review at a tablet width before accepting.

**Alternative:** reduce only main top padding and leave the internal gaps unchanged. This is the lowest-risk option but does not address the category page's `2rem` + `2.5rem` sequence between back navigation, title, and first article. Prefer the tabled, modest adjustments as one visual iteration.

**Risk:** low. Main risk is making the title feel crowded against the navigation or compressing the category introduction. Verify focus rings, top home link, title/subtitle, guide callout, and first card at desktop, tablet, and narrow mobile sizes.

**Likely files:** `Category.razor.css`, `Compare.razor.css`; no component markup change is necessary.

### 3. Use more wide-screen space without widening prose

**Recommendation:** widen only the outer page containers and constrain text within its own reading surface:

| Selector / value | Current | Proposed |
|---|---:|---:|
| Category `.page-content` max width | `68rem` | `80rem` |
| Comparison `.page-content` max width | `72rem` | `84rem` |
| `.entry-content`, `.entry-section-heading`, `.entry-list` | `72ch` | keep `72ch` |
| Comparison answer prose (`.structured-country > p`, `.article-body`) | no explicit max width | cap text blocks at `65ch`–`72ch` |

At wide desktop widths this gives article cards and comparison panels more room while keeping article paragraph measure unchanged. An `84rem` comparison container leaves room for two country panels and their gap without turning either answer into an extremely long line; a `65ch`–`72ch` cap within answer text keeps reading comfortable. Keep current `minmax(0, 1fr)` columns, `min-width: 0`, `overflow-wrap: anywhere`, and the existing `800px` collapse point. At tablet and mobile, the larger max widths have no effect because the container remains bounded by the viewport and side padding.

The header and top home link are currently capped at `72rem`; they may remain so for a restrained header alignment. If visual review makes the header appear detached from the wider page, consider increasing both to `80rem` in the same later iteration, but do not widen navigation independently without a screenshot review.

**Alternative:** widen article cards only while keeping `.page-content` at `68rem`. That cannot use the extra viewport width. Do not set prose to `width: 100%` or remove the `ch` constraints.

**Risk:** low-to-medium. Wider cards change visual rhythm and can make source lists or two-column comparison panels feel sparse. Text caps may create uneven blank space inside comparison panels; test long and short answers, sources open/closed, and full-article details. Confirm there is no horizontal scrolling at `390px` and `320px`.

**Likely files:** `Category.razor.css`, `Compare.razor.css`; optionally `SiteHeader.razor.css` and `HomeLink.razor.css` only if the later visual pass calls for alignment.

## Suggested implementation subphases

1. **18.7.2 — Source-reference format:** finalize the token convention, parser output, source ownership validation, encoded rendering, and focused unit/integration tests. Keep source lists and article text unchanged until the renderer is accepted.
2. **18.7.3 — Vertical rhythm:** apply the proposed spacing changes and review category and comparison pages at desktop, tablet, and mobile widths.
3. **18.7.4 — Wide-screen layout:** widen outer containers, retain/cap reading measure, then inspect short and long content at desktop and check for overflow at tablet/mobile.

Each subphase should have its own scoped diff and visual acceptance. No legal/source content or publication statuses are part of this audit.

## Acceptance criteria

### Functional and content safety

- Inline source links resolve only to existing `OfficialSource` records belonging to the current article; no URL is authored in article prose.
- Every link is deliberately mapped to the statement it supports; no automatic source matching is used.
- Malformed or unknown references cannot emit arbitrary HTML or URLs; visible labels are HTML-encoded.
- EN/UK/RU links have equivalent meaning, and the complete article source list remains present.
- Article headings, paragraphs, and lists render as before; existing search, category navigation, comparison, and source panels continue to work.

### Visual and responsive

- At desktop, titles and first content begin visibly higher without crowding navigation or reducing touch target size.
- At tablet, the page remains balanced around existing layout breakpoints; comparison columns collapse according to the current `800px` rule.
- At `390px` and `320px`, no horizontal overflow, clipped links, or overlapping controls occur; comparison remains a single column.
- Article prose remains at most `72ch`; comparison prose remains at most the accepted `65ch`–`72ch` measure.
- Long and short titles, summaries, body text, and source titles remain readable in EN/UK/RU, including when source details are expanded.

## Audit conclusion

The three requests are implementable with small changes to the existing parser/renderers and page-scoped CSS. The inline-link item needs an explicit, editor-authored source reference because current source records do not identify the claim or paragraph they support. The proposed changes do not imply publication readiness or change any article or source evidence.
