# Germany Temporary Protection Step-by-Step Guide V1 — Implementation

**Status: Development preview only — awaiting human visual and editorial acceptance.**

## Implemented

- A reusable JSON-backed guide catalog with guide, scenario, ordered step, translation, article, source, claim, action-kind, and editorial-status fields.
- The Germany temporary-protection guide at `/guides/germany/temporary-protection`.
- Four situation choices and 16 ordered steps: new arrival (5), application pending (3), existing §24 permit (4), and prior protection in another EU Member State (4).
- Ukrainian, English, and Russian guide content and interface text.
- A navigable, responsive step view with back, previous, next, change-situation, navigation-only progress, document examples, authority guidance, warnings, official-source links, and links to the existing article.
- Startup validation for language completeness, unique and ordered IDs, valid country/article/source relationships, and Draft-only status.

## Editorial and publication restrictions

All guide content is `Draft`. Development shows a localized preview notice. A Production request for this Draft guide is not rendered as guide content and is marked not found; there is no public navigation link to it. No user answers or progress are persisted. The guide does not collect personal information, calculate eligibility, or decide what application someone should make. The existing verified articles, source records, and comparison facts are not changed.

Recommendations are visibly labelled as suggested next steps. The current guide does not label any recommendation as a legal requirement. Potentially relevant documents are examples, not universal checklists. The guide preserves distinctions between EU temporary protection and a §24 permit, the separate conditional 90-day title exemption, individual permit validity, pending-document work entries, the separate military-duty rules, the EU/Germany4Ukraine wording discrepancy, local procedures, and the Brandenburg service limitation.

## Content structure

`src/RefugeeNavigator.Web/Data/step-by-step-guides.json` contains a stable guide ID and slug, the existing Germany country ID, three localized guide texts, four stable scenario IDs, and 16 stable step IDs. Each step points to the existing article, one or more source IDs already linked to that article, and research claim IDs from the existing claim review. No source or article record is duplicated.

## Test coverage

`StepByStepGuideTests` verifies all four scenarios and 16 stable ordered steps, three-language content completeness, Draft-only enforcement, article/source relationships, localized Development preview rendering, Production suppression of Draft content, and interface localization coverage. The existing test suite remains in place.

## Known limitations and human acceptance required

- The UI uses an Interactive Server render mode for local step navigation; it adds no client-side data persistence.
- Automated component tests validate markup and catalog relationships. Manual browser review is still required for mobile/desktop appearance, keyboard interaction, all three languages, and actual next/previous/change-situation behavior.
- The draft has not received human editorial approval. Build, tests, and source-link validation are not publication approval.
- Recheck official sources and local procedures at the publication gate. No new legal research was performed in this implementation.
- Do not deploy or expose this guide in Production until a separate explicit editorial approval and release decision.
