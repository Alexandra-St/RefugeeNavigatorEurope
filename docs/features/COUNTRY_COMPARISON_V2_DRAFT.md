# Country Comparison V2 — Editorial Draft

V2 adds concise, structured extracts above the existing V1 article comparison. It does not replace or rewrite the verified country articles.

## Model and data

`comparison-facts.json` stores the eight existing topic mappings, ordered comparison aspects and per-country answers. Each answer links to the mapped `InformationEntry` and to existing `OfficialSource` IDs. Source titles, URLs and recorded `LastCheckedAt` values continue to come from the official-source catalog; this draft catalog stores no verification timestamps. Ukrainian, English and Russian answer text is explicit data, not generated at runtime.

The first aspect for each topic uses the approved article summary. The second uses the first full sentence of its opening factual passage, and is deliberately titled with a neutral prompt so it does not overstate what that sentence answers. The evidence audit maps each extract to the article passage and source records. These extracts are editorially new even where they reuse approved wording, so all 48 country facts remain `Draft` and require separate human approval.

## Topics and aspects

| Topic | Aspect 1 | Aspect 2 |
| --- | --- | --- |
| Temporary protection application | Scheme or status described | What key point is highlighted? |
| Residence after protection | Other residence routes | What key point is highlighted? |
| Finding employment | Job-search platforms | What key point is highlighted? |
| Work authorization and employee rights | Work-authorization basis | What key point is highlighted? |
| Healthcare | Basis for access | What key point is highlighted? |
| Housing | Where to start | What key point is highlighted? |
| Education and language learning | Education route | What key point is highlighted? |
| Social assistance | Support types | What key point is highlighted? |

The catalog contains 8 topics, 16 aspects and 48 populated country facts. All eight V1 topic-to-article mappings remain authoritative. No country ranking, personal eligibility determination or legal inference is computed by the application.

## Evidence and missing information

Each fact's source links are drawn from its mapped article's existing official-source relationships. The UI resolves their titles, URLs and historical check dates from the unchanged source catalog. See [COUNTRY_COMPARISON_FACT_AUDIT.md](COUNTRY_COMPARISON_FACT_AUDIT.md) for the exact supporting article passages, references and qualifications.

When a future aspect cannot be established from reviewed material, the catalog can mark it unavailable and provide a localized message: “Not established in the reviewed sources.” This is distinct from stating that a requirement does not exist. Unavailable items must not cite a source as if it proved absence.

## Editorial approval and environment separation

Development renders the structured draft with a localized preview notice and `Draft` labels. Production suppresses the structured layer while any fact remains Draft and continues to show the existing V1 comparison, including summaries, expandable full article content, source links, status, dates and original article links. A visual label is not the publication control; the environment and catalog status gate rendering.

Facts need separate evidence and translation review before status changes to `Approved`. Partial approval does not expose a mixed draft in Production. Startup validation checks all eight topics, three countries, article/category relationships, source ownership, unique aspect IDs, complete language coverage and supported editorial statuses. Localization coverage is checked at startup too.

## Known limitations

- Two aspects per topic are a deliberately small first structure; they are not a complete comparison of national systems.
- The second aspect presents one opening sentence and is intentionally generic; human editors can later replace it with a more discriminating question only when the existing evidence supports that question.
- Source records are reused from the article relationship and retain their existing historical check dates; this work does not re-verify sources.
- Facts are not individually marked as legally verified, and this feature does not change the 24 articles, their 72 Verified translations, source dates or publication state.
- No structured fact is approved for deployment by this implementation.
