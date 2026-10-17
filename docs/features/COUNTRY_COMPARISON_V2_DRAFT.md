# Country Comparison V2 — editorial draft

V2 presents three concise, side-by-side questions for each of the eight existing comparison topics. It retains the verified V1 article comparison and makes that full article content a keyboard-accessible disclosure in the Development preview.

## Structured facts

`comparison-facts.json` contains 24 aspects across eight topics and three countries. It has 72 country answers: 70 populated, source-linked facts and two unavailable answers. Each populated fact is linked to the mapped article and only the existing official-source records selected for that claim. The catalog contains no source titles, URLs, or verification dates; the page resolves those from the existing official-source records, preserving their recorded `LastCheckedAt` values.

All 72 answers are `Draft`. Questions and answers are localized explicitly in Ukrainian, English, and Russian. Two unavailable answers use the localized “Not established in the reviewed sources” message and have no source IDs. Their absence is a documentation gap, not a claim that a right, duty, or service does not exist.

The internal [claim-level evidence audit](COUNTRY_COMPARISON_FACT_AUDIT.md) maps every country answer to the exact supporting passage in the approved article, relevant claim IDs where a research matrix exists, selected source records and their historical check dates, and the applicable limits. Source records’ historical check dates were not reset. The §4a(5) text was checked through the official page’s indexed content after a direct page open timed out; see the audit’s source-access note. The Denmark employment pilot has no dedicated matrix; its bounded portal/service statements are mapped to the approved article passage and the matching official page, without inferring access to employment assistance.

## Display and release boundary

Development shows the structured facts with a preview notice and Draft labels. Beneath them, the V1 comparison is available under the localized “Read full country articles” disclosure, collapsed by default. It retains the existing article summaries, status and update dates, full formatted content, source links, and links to the country/category pages.

Production continues to suppress the structured layer while any fact remains Draft. It renders the existing V1 comparison directly and expanded, without the new disclosure. Structured content appears in Production only after every fact is explicitly approved. Approval is an editorial action; startup validation checks structure, relationships, language coverage and valid statuses, not legal accuracy.

## Editorial review

The 24 questions distinguish practical topics such as eligibility conditions, application steps, documents, services, and dates. Facts are not rankings and do not decide personal eligibility. The two unavailable items are:

- Denmark: conditions for access to public employment assistance.
- Germany: school-pupil-specific language support.

Human reviewers should check that each question is useful, the short answers preserve qualifications and exceptions, the three translations have equivalent meaning, and the selected sources directly support the displayed claim.

## Existing publication gates

- Denmark’s attributed 2027/2028 extension-date discrepancy remains unresolved; no individual permit validity is inferred.
- German EU protection and individual permit dates remain separate legal scopes.
- Existing legislative, source-freshness, translation and release checks remain required.
- This work does not change the 24 production articles, their 72 Verified translations, official-source records, or release state.
- No structured fact is approved for publication by this implementation.
