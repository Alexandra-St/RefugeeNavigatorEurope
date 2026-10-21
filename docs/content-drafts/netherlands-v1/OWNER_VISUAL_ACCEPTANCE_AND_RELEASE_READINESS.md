# Netherlands Owner Visual Acceptance and Release Readiness

## 1. Decision date and baseline

**Decision date:** 21 October 2026
**Baseline:** `c06ffe6f241818390a3071f0b1f4b99f832ef7be`

The project owner grants visual acceptance for the reviewed synthetic-data scenario described below. This records a visual gate decision only; overall release, publication, status promotion, and deployment are not authorized.

## 2. Visual acceptance scope

**Accepted for the reviewed scenario:**

- Current Core Category presentation.
- Current Country Comparison V2 presentation, including the long all-categories page.
- Mobile navigation and selectors.
- Expandable comparison source groups, within the documented review scope.

The owner accepts the current long Comparison page without immediate redesign. The acceptance covers the synthetic-data visual scenario only; it does not certify production content, real-user interaction behavior, or legal correctness.

## 3. Evidence and limitations

- GitHub Actions: [run 39607279744 — Netherlands visual evidence](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/39607279744).
- Commit: `4edd2019d58d7309995ff56e814ca2fed16571f0`.
- Artifact: [netherlands-publication-gate-screenshots, ID 12348886647](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/39607279744/artifacts/12348886647).
- 18 PNGs: two pages × EN/UK/RU × 1280×720, 390×844 and 320×700.
- Automated assertions: **PASS**. Screenshot capture and artifact upload: **PASS**. Screenshot review: completed; owner visual acceptance: granted for this scenario.
- Hosted Production was not accessed or visually tested. The captures use isolated synthetic catalogs; fixture copy is not Netherlands publication content.

**Visual gate: ACCEPTED FOR REVIEWED SCENARIO.** This is distinct from overall release authorization.

## 4. Non-blocking Comparison UX backlog

**BACKLOG — NON-BLOCKING:** consider category collapsing and/or section navigation for long all-categories Comparison pages. The owner accepted the current layout; do not implement this improvement as a release prerequisite.

## 5. Previously approved editorial scope

The existing [owner editorial decision](OWNER_EDITORIAL_DECISION.md) preliminarily approves seven bounded Core articles in EN/UK/RU (21 candidate translations) and 20 bounded Comparison facts. This is editorial approval only, not status promotion or production authorization.

- Held article: **NL-TP-01**, all three translations remain `NeedsReview`.
- Held/deferred facts: `protection-scheme`, `protection-application`, and `protection-dates` remain deferred because they link to NL-TP-01; `residence-filing` remains unavailable, `Draft`, and source-less.
- Catalog count check: eight Netherlands articles / 24 translations, all `NeedsReview` (21 candidate and three held-article translations). All 24 Netherlands Comparison facts remain `Draft` (23 available, one unavailable).

The existing [editorial preparation matrix](EDITORIAL_APPROVAL_PREPARATION.md) records the evidence limitations and article-specific scope. No preliminary approval resolves those limitations.

## 6. Release readiness matrix

| Area | State | Evidence / remaining condition |
| --- | --- | --- |
| Editorial owner decision | **PASS — LIMITED** | Seven Core articles and 20 facts are preliminarily approved only within their documented wording and qualifications; held material is excluded. |
| Production visibility gates | **PASS — IMPLEMENTED** | Existing active-language and linked-article/source gates are documented and tested. A selective-production smoke check after any authorized status promotion remains pending. |
| Translation review | **PENDING** | Candidate wording has preliminary editorial approval, but all 21 candidate translation statuses remain `NeedsReview`; semantic equivalence must be confirmed before promotion. |
| Comparison source integrity | **PENDING** | Before promotion, confirm each of the 20 selected facts still has valid source records owned by its linked article and that the linked translation is eligible for every renderable language. No new source audit was performed here. |
| Source freshness | **PENDING** | Recheck the approved candidates' official sources and current service instructions at release time. |
| Date-sensitive legal / agency information | **PENDING** | Recheck TPD/residence guidance, UWV processes, Zvw/RMO guidance, DUO 2026/27 conditions and 31 July 2027 deadline, 2026 wage figures, benefits, and local services. Preserve all documented conflicts and limitations. |
| Visual owner acceptance | **PASS — REVIEWED SCENARIO ONLY** | Owner accepted the synthetic-data screenshots in run 39607279744; no hosted Production review or interaction certification. |
| Held records and dependencies | **BLOCKED** | NL-TP-01 and its three linked facts remain held/deferred; `residence-filing` remains unavailable and source-less. Exclude these from any selective release unless separately resolved and approved. |
| Production status authorization | **NOT AUTHORIZED** | A separate explicit authorization is required before changing 21 translation statuses `NeedsReview` → `Verified` and 20 fact statuses `Draft` → `Approved`. |
| Deployment authorization | **NOT AUTHORIZED** | This decision grants no publication or deployment authorization. Obtain a separate explicit authorization after the release gates pass. |

**Overall release:** **NOT YET AUTHORIZED.** Visual acceptance does not make the release ready.

## 7. Required actions before status promotion

1. Perform the targeted freshness recheck for the seven candidate Core articles and 20 selected facts, particularly TPD/residence, UWV employment procedures, Zvw/RMO healthcare, DUO 2026/27 conditions and deadline, 2026 wage figures, social benefits, and local services.
2. Confirm EN/UK/RU semantic equivalence, retaining all documented qualifications, date scopes, uncertainty, and source attribution.
3. Confirm each selected Comparison fact still references existing, valid official sources owned by its linked article, and that linked-article publication requirements are met.
4. Obtain separate explicit authorization before promoting exactly the 21 candidate translations from `NeedsReview` to `Verified` and the 20 selected facts from `Draft` to `Approved`. Do not promote NL-TP-01 or the four held/deferred facts under this decision.
5. After any authorized promotion, run a selective-Production smoke check for active-language visibility, source rendering, and held-content suppression.

Do not perform these actions under this visual acceptance record.

## 8. Required actions before deployment

- Complete a release audit after authorized status promotion, checking source freshness, status counts, active-language visibility, linked-article/source gates, and all unresolved limitations.
- Confirm the four held/deferred facts remain suppressed and `residence-filing` remains unavailable unless separately resolved.
- Obtain separate explicit authorization for publication and deployment. No deployment is authorized here.

## 9. Held material and unresolved limitations

NL-TP-01 remains held for the EU/IND exception discrepancy, military-screening evidence, and incomplete negative-triage procedure. The three facts linked to it remain deferred. `residence-filing` remains an evidence gap. Other recorded limitations include the Zvw/RMO guidance conflict, route-specific residence uncertainty, local service availability, and time-sensitive DUO, wage, and benefit conditions. Preserve the attribution and qualifications documented in the existing decision and preparation matrix; this report does not resolve them.

## 10. Explicit non-authorization

This document records **owner visual acceptance for the reviewed synthetic-data scenario only**. It does not authorize catalog status changes, publication, or deployment. Production catalogs and statuses remain unchanged: 21 candidate translations remain `NeedsReview`, all 24 Netherlands Comparison facts remain `Draft`, and held content remains held. No publication or deployment occurred.
