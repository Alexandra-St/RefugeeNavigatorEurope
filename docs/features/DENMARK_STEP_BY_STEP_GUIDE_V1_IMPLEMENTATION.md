# Denmark Temporary Protection Guide — V1 Development Preview

**Status:** Draft; awaiting visual and editorial acceptance.  
**Primary article:** `afc14824-4035-4ca7-9662-6d5352ba63cb` (“Temporary protection in Denmark: how to apply and extend your permit”).

## Implementation

The Denmark guide is added to `step-by-step-guides.json` with four situations and sixteen ordered steps. Its guide-level `EditorialStatus` remains `Draft`, which keeps it available only in Development. All step `ArticleId` values refer to the primary article; ClaimIds and OfficialSource GUIDs follow the Phase 11.1 proposal. UK, EN and RU text is provided for the guide, each scenario and each step. No source record or article content was changed.

The existing guide model was reused without schema changes. The shared route now resolves `/guides/{countrySlug}/{guideSlug}` using the country slug, guide slug and catalog country ID. Germany keeps `/guides/germany/temporary-protection`; Denmark uses `/guides/denmark/temporary-protection`. Full article links resolve the article, country and category from existing catalog relationships, then use the existing entry anchor.

The category discovery card searches publishable guides associated through their linked articles with the current country and category. Germany’s Approved guide remains visible in Production. Denmark’s Draft guide is visible in Development and hidden in Production. Poland and unrelated category pages receive no matching card. The Draft preview notice remains exclusive to Draft guides.

The optional secondary links to Denmark healthcare, housing and residence-options articles are deferred. V1 retains the primary full-article link and source list; no fields were added for secondary links.

## Editorial and legal boundaries

The guide does not calculate eligibility, request or store personal answers, persist navigation progress, accept uploads, or decide an application. Sections 1 and 2 remain distinct. The national Special Act is not conflated with EU temporary protection. Pending stay, conditional work, travel/re-entry, limited healthcare, approved permits, municipal assignment, housing, CPR and residence documents remain distinct concepts.

The limited military-status rule retains its affected group, application dates and transition qualifications. The 35-day processing figure remains a target, not a guarantee. The BEK 828/2026 / Immigration Service 2027–2028 date conflict remains attributed and unresolved; neither date is presented as an individual expiry date. L 43 is not treated as current law. Appeal guidance remains decision-specific and non-numeric. The section 57(5) housing limitation remains limited to the supported Special Act §2(1) basis. These statements reproduce the approved article and Phase 11.1 design; this implementation is not new legal research or a source recheck.

## Acceptance before publication

- Review all four scenarios, sixteen steps and UK/EN/RU meaning against the Phase 11.1 proposal and current article.
- Review the visible 2027/2028 conflict, military warning, pending-work conditions, limited healthcare scope and §57(5) scope.
- Confirm sources, article links, language switching and the Development-only Draft gate.
- Visually review desktop and narrow/mobile layouts, including long warnings, scenario cards, focus and touch targets, source links, text wrapping and horizontal overflow.
- Confirm the guide remains Draft until separate editorial approval. This change does not authorize deployment.

**No deployment was performed.**
