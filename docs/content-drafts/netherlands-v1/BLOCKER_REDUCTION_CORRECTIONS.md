# Phase 18.5B — Netherlands blocker-reduction corrections

**Baseline:** `abaa19439c213691ebb1bb2323cddbfcfe730ad9`
**Translation edit time:** 2026-10-21 22:01:22 UTC

## Core article corrections

- **NL-TP-02:** EN / UK / RU now describe prospective transition planning only from accessible official guidance. The September memorandum's proposed design and further-legal-work history were removed. The article says the reviewed guidance does not establish automatic conversion, individual entitlement or a universal filing procedure; it directs readers to current, route-specific IND instructions. The filing-route limitation is attributed to the reviewed evidence. Ordinary work and partner/family routes remain examples with separate conditions. The permanent-residence overview and uncertainty about whether temporary-protection time counts remain explicit.
- **NL-EMP-01:** EN / UK / RU retain vacancy search, the specified UWV registration route, Werkcentrum navigation and existing no-guarantee language. The credential-evaluation section and its Nuffic/SBB workflow, non-binding-effect and processing-time claims were removed. Summaries and claim mappings now match the retained content.
- **NL-EDU-01:** EN / UK / RU now use “MBO and higher education” and retain institution-controlled admission. Nuffic/SBB evaluation claims were removed. The complete conditional DUO 2026–27 paragraph remains, including the nationality reason, age 18 to under 31 on 1 August, inability-to-pay/no-sponsor condition, and 31 July 2027 deadline.

All nine translations match their corresponding draft sections. Every target translation remains **NeedsReview (0)**; each `LastUpdatedAt` records the edit time above.

## Comparison fact corrections

- **residence-routes:** EN / UK / RU describe prospective transition planning from accessible IND, Government.nl and ministerial guidance. They state that reviewed guidance does not establish automatic conversion, individual entitlement or a universal filing procedure and direct readers to current IND guidance. The unavailable September memorandum is not linked.
- **employment-assistance-conditions:** EN / UK / RU retain Werkcentrum navigation and its service/job non-guarantees. The credential-evaluation claim was removed.
- **education-higher-admission:** EN / UK / RU retain institution-controlled admission and the conditional DUO route. The Nuffic/SBB claim was removed; the DUO criteria were preserved.

The other five reviewed facts—`residence-conditions`, `employment-vacancies`, `employment-institutions`, `education-school-enrolment` and `education-pupil-language`—are content- and source-identical to baseline. All eight facts remain **Draft**.

## Source relationships

Two fact source links were removed: Nuffic `368fd458-a574-550b-a15a-48183cb3ba0e` from `employment-assistance-conditions`, and Nuffic `fa10f3f5-53eb-5e4d-a962-5bbf49529aee` from `education-higher-admission`. `residence-routes` now links only to `979a64e2-7a45-5c2b-8151-9ad7f57d61d8`, `fda6d123-58d8-5858-9e91-311f5266f5ee` and `249d9618-2212-5251-8f6e-8de0766e0cf3`, the approved accessible transition sources. Its previous high-skilled-migrant and partner source links were removed from that fact; those source records remain because NL-TP-02 still uses them for its separate ordinary-route examples.

Three source records became genuinely unused and were deleted:

- `84524c35-1a0f-553d-a06c-4ca02ebb4bb3` — the inaccessible September memorandum supported only the removed proposal-history detail; no retained article claim or comparison fact depends on it.
- `368fd458-a574-550b-a15a-48183cb3ba0e` — the NL-EMP-01 Nuffic evaluation record supported only the deleted evaluation section and fact clause.
- `fa10f3f5-53eb-5e4d-a962-5bbf49529aee` — the NL-EDU-01 Nuffic evaluation record supported only the deleted evaluation sentence and fact clause.

All remaining source records are unchanged, including their `LastCheckedAt` values. Target articles retain 8, 3 and 9 source records respectively. No comparison fact has a dangling source ID.

## Validation and remaining evidence limits

- Targeted Netherlands regression checks: **15 passed**.
- Release build: **0 warnings, 0 errors**.
- Full Release test suite: **566 passed**.
- `git diff --check`: passed.
- JSON parsing, target statuses, draft/catalog parity, five protected facts, source ownership and out-of-scope catalog invariants: passed.

The September memorandum remains unavailable as evidence; its specific proposal details are omitted. The older English Government.nl transition wording remains a documented source limitation and is explicitly not presented as current filing guidance. Whether temporary-protection time counts toward permanent residence remains unresolved and is not inferred. No source-freshness research was conducted in this correction pass.

**Status boundary:** No translation or comparison status was promoted. No publication or deployment occurred.
