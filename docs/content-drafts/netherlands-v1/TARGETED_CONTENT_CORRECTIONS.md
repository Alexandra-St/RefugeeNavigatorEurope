# Netherlands targeted content corrections — Phase 18.4U

Baseline: `66b38615b539a03e0afb6371efa3756be838dc0a`.

## Records and corrections

- **NL-SOC-01 (`8d466e7d-93f7-596e-a44f-188e04396aa0`), EN/UK/RU Content:** replaced the categorical “adult works or receives specified benefits, therefore household leefgeld stops” wording. The corrected explanation distinguishes the personal municipal-reception contribution for adults aged 18+ in reception with a job or specified WW/long-term-illness/unable-to-work benefit; states that work alone does not automatically end leefgeld and that full withdrawal requires income above 115% of the applicable leefgeld; preserves partial withdrawal/collection and municipal discretion where collection would cause disproportionate harm; and separates the contribution from rent or rent benefit. The 2026 rates remain dated from 1 August 2026. The three edited translations have `LastUpdatedAt` `2026-10-21T14:44:40+00:00` and remain `NeedsReview`.
- **`social-subsistence`, EN/UK/RU Values:** now express the same household, age, income, 115% threshold, partial-withdrawal and discretion qualifications, without promising an individual payment.
- **`housing-initial`, EN/UK/RU Values:** now say that, in cases referred for IND triage, the municipality offers reception at least until the triage advice. This is conditional; no chosen region/facility, reception after the advice, or current availability is promised.
- **`healthcare-access`:** added existing NL-HEALTH-01-owned UWV source ID `e1edd80d-2530-57e4-8e55-637382f1dfd1`. Existing source references and answer wording are unchanged; the Zvw/RMO conflict and no-individual-insurance-instruction boundary remain.
- **NL-HEALTH-01 urgent-care source (`e59632da-eb67-5b56-840f-9749cdc9f171`):** replaced the inaccessible URL with the directly opened current Rijksoverheid answer, `https://www.rijksoverheid.nl/documenten/vraag-en-antwoord/eerstelijnszorg/wat-moet-ik-doen-als-iemand-dringend-medische-hulp-nodig-heeft`. The source ID, owner, title and fact relationships remain; `LastCheckedAt` records the direct check at `2026-10-21T14:39:08+00:00`. The source confirms 112 for life-threatening emergencies and GP / central doctors service for less urgent needs.
- **`education-higher-admission`, RU Values:** corrected only the case agreement from “и отсутствия права” to “и отсутствие права”; the nationality reason and all criteria remain.

## Translation and evidence checks

EN/UK/RU were compared for the changed leefgeld and housing facts: cohorts, conditions, 115% threshold, partial treatment, discretion and uncertainty are equivalent. The existing 18–31-under boundary, nationality-based reason, 2026–27 year and 31 July 2027 deadline remain in the DUO fact. The healthcare-access wording was not changed. The urgent-care source was directly opened on 21 October 2026; the IND handbook dated 1 October 2026 and the current contribution guidance were also opened to verify the limited wording used here.

## Focused validation

- JSON parsing and targeted record/source ownership checks: passed.
- Targeted Netherlands integration/comparison tests: **5 passed, 0 failed** (catalog/status invariants, article/draft parity, source ownership/date/URL, comparison relationships, and DUO conditions). The relevant regression expectations were narrowed to the one replaced healthcare URL/date, four sources for the Netherlands `healthcare-access` fact, and the corrected Russian case form.
- `git diff --check`: **passed**.
- Article and comparison statuses: unchanged; Netherlands translations remain `NeedsReview`, all 24 Netherlands facts remain `Draft`.
- NL-TP-01 and the four held/deferred facts: untouched.

## Remaining limitations and release status

Direct verification blockers remain for the previously inaccessible IND and Nuffic pages and the migration-decision memorandum. The UWV / RMO guidance conflict about ordinary insurance and the separate 2027/2028 government-guidance discrepancy remain unresolved. Source freshness is not fully passed. No source or editorial status was promoted. **No publication or deployment is authorized or performed.**
