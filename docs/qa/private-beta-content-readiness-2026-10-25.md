# Private Beta Content Readiness — 2026-10-25

## 1. Overall status

This is a status and structural-integrity audit of Denmark, Germany, Poland, and the Netherlands for the next private-beta release phase. It does not re-evaluate legal claims, source contents, or publication/deployment authorization.

| Country | Articles / translations | Translation status totals | Comparison facts | Comparison status totals |
| --- | ---: | --- | ---: | --- |
| Denmark | 8 / 24 | 24 `Verified`; 0 `NeedsReview`; 0 other | 24 | 24 `Approved`; 0 `Draft`; 0 other |
| Germany | 8 / 24 | 24 `Verified`; 0 `NeedsReview`; 0 other | 24 | 24 `Approved`; 0 `Draft`; 0 other |
| Poland | 8 / 24 | 24 `Verified`; 0 `NeedsReview`; 0 other | 24 | 24 `Approved`; 0 `Draft`; 0 other |
| Netherlands | 8 / 24 | 21 `Verified`; 3 `NeedsReview`; 0 other | 24 | 20 `Approved`; 4 `Draft`; 0 other |

The catalog integrity check found no orphaned official-source records, missing referenced source IDs, wrong-article source references, or non-HTTPS owned source URLs in the four-country scope. There are no available facts without official source IDs. The one source-less fact is the intentionally unavailable Netherlands `residence-filing` fact described below. This is structural validation only; no official pages were reopened.

**Assessment:** the accepted Denmark, Germany, Poland, and selected Netherlands content has no status-based blocker for proceeding to the next private-beta release phase. Keep the Netherlands hold set excluded. This verdict is not public-release approval and does not authorize deployment.

## 2. Denmark

- **Translations:** 24 `Verified`; 0 `NeedsReview`; 0 other.
- **Comparison:** 24 `Approved`; 0 `Draft`; 0 other.
- **Review items:** none in the current status catalogs.
- **Recorded completion:** the Denmark Phase 18.7 inline-source-link rollout was completed and owner-accepted in the preceding phases. This audit checked catalog statuses only and did not repeat that editorial or visual review.

## 3. Germany

- **Translations:** 24 `Verified`; 0 `NeedsReview`; 0 other.
- **Comparison:** 24 `Approved`; 0 `Draft`; 0 other.
- **Acceptance record:** `docs/content-research/germany-batch-2026-10-15/HUMAN_ACCEPTANCE_CHECKLIST.md` records the owner's approval of all eight articles and 24 translations for Private Beta preparation on 16 October 2026.
- **Carry-forward notes, not status blockers:** that record calls out sources and time-sensitive qualifications to maintain or recheck, including a Labour Law page that was inaccessible directly and therefore is not evidence, and preserving distinctions between EU and German permit-date scopes. No legal/source recheck was performed here. These are maintenance items for the appropriate later release/publication gate; they do not change the recorded Private Beta acceptance or current statuses.
- **Unaccepted content:** none indicated by the current catalogs or cited acceptance record.

## 4. Poland

- **Translations:** 24 `Verified`; 0 `NeedsReview`; 0 other.
- **Comparison:** 24 `Approved`; 0 `Draft`; 0 other.
- **Acceptance record:** `docs/content-research/poland-batch-2026-10-16/HUMAN_ACCEPTANCE_CHECKLIST.md` records the owner's approval of all eight articles and 24 translations for Private Beta preparation on 16 October 2026, with no outstanding editorial questions in the acceptance summary.
- **Carry-forward notes, not status blockers:** the acceptance record requires maintenance of time-sensitive information, including official guidance that displays superseded dates, and preserving program-specific conditions and individual-assessment caveats. This audit did not recheck those sources.
- **Unaccepted content:** none indicated by the current catalogs or cited acceptance record.

## 5. Netherlands

- **Translations:** 21 `Verified`; 3 `NeedsReview`; 0 other.
- **Comparison:** 20 `Approved`; 4 `Draft`; 0 other.
- **Verified and Approved set:** the 21 accepted translations and 20 accepted facts remain in their production statuses. `employment-vacancies` retains only UWV source `1690f9d6-0721-5005-b9de-fda56864ed70`; the general Rijksoverheid source `98966301-dedb-5b6c-907c-adc84b07dfbb` remains an article-level source for NL-EMP-01.

### Non-Verified translations

| Article ID | EN title | Languages affected | Existing documented reason |
| --- | --- | --- | --- |
| `244b1055-4d8c-55b2-820e-60ff018c3b1f` (NL-TP-01) | Applying for Temporary Protection in the Netherlands | EN, UK, RU (`NeedsReview` in all three) | Explicit owner hold: unresolved EU/IND exception and military-screening evidence boundaries, plus an incomplete negative-triage review procedure. See `docs/content-drafts/netherlands-v1/OWNER_EDITORIAL_DECISION.md` and `TP_RESIDENCE_EVIDENCE_RESOLUTION.md`. |

### Draft facts

| Fact key | Related article | `IsAvailable` | OfficialSourceIds | Existing documented reason |
| --- | --- | --- | --- | --- |
| `protection-scheme` | NL-TP-01 — Applying for Temporary Protection in the Netherlands | `true` | Present | Intentional defer: depends on held NL-TP-01; keep suppressed until its hold is resolved or a separately reviewed safe mapping is authorized. |
| `protection-application` | NL-TP-01 — Applying for Temporary Protection in the Netherlands | `true` | Present | Intentional defer: linked to held NL-TP-01; its negative-triage remedy remains incomplete. |
| `protection-dates` | NL-TP-01 — Applying for Temporary Protection in the Netherlands | `true` | Present | Intentional defer: linked to held NL-TP-01; preserve the distinction between scheme horizon and individual document validity and recheck the noted evidence before any later release decision. |
| `residence-filing` | NL-TP-02 — Residence Options Beyond Temporary Protection in the Netherlands | `false` | None (`[]`) | Intentional evidence-gap hold: no general in-country filing route has been established. Keep unavailable, Draft, and source-less. |

These are the four requested intentional holds, not newly discovered defects. Do not promote or broaden them as part of the next phase. The seven affected NL-TP-01 records above comprise one held article across three languages and its three dependent facts; `residence-filing` is a separate unavailable fact.

## 6. Intentional holds

1. NL-TP-01 translations EN/UK/RU remain `NeedsReview` under the owner's explicit hold.
2. `protection-scheme`, `protection-application`, and `protection-dates` remain `Draft` and deferred because they depend on NL-TP-01.
3. `residence-filing` remains `Draft`, unavailable, and source-less because a general process has not been established.

All other Netherlands translations and facts remain in their accepted `Verified` / `Approved` statuses. These intentional holds are excluded from the accepted beta scope and are not blockers to advancing that bounded scope.

## 7. Actual blockers

- **No catalog-status or structural-integrity blocker was found for the accepted four-country private-beta scope.** Targeted status/integrity tests passed (183 passed, 0 failed).
- **The Netherlands package as a whole is not cleared for unrestricted release.** Keep the NL-TP-01 and four-fact hold set outside the accepted scope; its recorded evidence/procedure gaps remain unresolved by design.
- Germany and Poland acceptance records contain time-sensitive/source-maintenance notes. Those notes should be handled at the applicable release/publication maintenance gate; this audit did not revalidate them and does not represent them as newly resolved.

## 8. Recommended next phase

Proceed with the next private-beta release-phase checks for the accepted set: Denmark, Germany, Poland, and the 21 Verified Netherlands translations plus 20 Approved Netherlands facts. Preserve the NL-TP-01/four-fact hold set and the existing source relationship for `employment-vacancies`. At the later publication gate, schedule the Germany and Poland maintenance checks recorded in their owner acceptance documents. Do not interpret this report as public-release approval or deployment authorization.

**Verdict: READY FOR NEXT RELEASE PHASE** — for the bounded accepted scope above. The exact excluded Netherlands holds are listed in section 6; no other content blocker was identified in this status and structural audit.
