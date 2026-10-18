# Germany Temporary Protection Step-by-Step Guide V1 — Implementation

**Status: Approved for Private Beta use by the project owner on 18 October 2026.**

## Implemented

- A reusable JSON-backed guide catalog with guide, scenario, ordered step, translation, article, source, claim, action-kind, and editorial-status fields.
- The Germany temporary-protection guide at `/guides/germany/temporary-protection`.
- Four situation choices and 16 ordered steps: new arrival (5), application pending (3), existing §24 permit (4), and prior protection in another EU Member State (4).
- Ukrainian, English, and Russian guide content and interface text.
- A navigable, responsive step view with back, previous, next, change-situation, navigation-only progress, document examples, authority guidance, warnings, official-source links, and links to the existing article.
- A contextual, localized discovery card on the Germany temporary-protection category page. It appears only when the matching guide is available in the current environment.
- Startup validation for language completeness, unique and ordered IDs, valid country/article/source relationships, and valid editorial status.

## Editorial and publication restrictions

The project owner approved the guide on 18 October 2026 after implementation review and desktop and mobile visual review. The guide is now `Approved`; Production publication is permitted. Future `Draft` guides remain visible only in Development and retain the localized preview notice. No personal answers or progress are stored. The guide makes no automated eligibility decisions and does not decide what application someone should make. This approval does not independently certify legal accuracy.

Recommendations are visibly labelled as suggested next steps. The current guide does not label any recommendation as a legal requirement. Potentially relevant documents are examples, not universal checklists. The guide preserves distinctions between EU temporary protection and a §24 permit, the separate conditional 90-day title exemption, individual permit validity, pending-document work entries, the separate military-duty rules, the EU/Germany4Ukraine wording discrepancy, local procedures, and the Brandenburg service limitation.

## Content structure

`src/RefugeeNavigator.Web/Data/step-by-step-guides.json` contains a stable guide ID and slug, the existing Germany country ID, three localized guide texts, four stable scenario IDs, and 16 stable step IDs. Each step points to the existing article, one or more source IDs already linked to that article, and research claim IDs from the existing claim review. No source or article record is duplicated. The project owner approved the existing four scenarios and 16 steps in Ukrainian, English, and Russian; guide text, step order, claim IDs, source IDs, and article references were not changed by the approval.

## Test coverage

`StepByStepGuideTests` verifies all four scenarios and 16 stable ordered steps, three-language content completeness, Approved guide rendering in Development and Production without a Draft notice, synthetic Draft-only visibility, article/source relationships, and interface localization coverage. Category rendering tests verify that the localized guide callout appears on the Germany temporary-protection category page only when the matching guide is publishable for the current environment.

## Known limitations and ongoing review

- The UI uses an Interactive Server render mode for local step navigation; it adds no client-side data persistence.
- Automated component tests validate markup and catalog relationships. The project owner completed desktop and mobile visual review. Automated checks do not replace ongoing content and source review.
- Recheck official sources and local procedures at the publication gate. No new legal research was performed in this implementation.
- Existing legal and source limitations remain applicable. The guide does not make eligibility decisions, and it stores no personal answers.
- Production visibility is permitted by editorial status, but this task did not deploy the guide or change Render configuration.
