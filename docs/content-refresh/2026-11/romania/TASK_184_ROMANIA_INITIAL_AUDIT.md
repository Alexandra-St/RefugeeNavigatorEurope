# Task 184 — Romania initial audit

As of 2026-11-20. Phase: repository-only initial audit. No web/source adjudication, production change, test run, commit, or push.

## Repository baseline

- Branch: main. Local HEAD: `10fd3834dca682162a1881b5c8c751d0cf2b0cb4` (the required Task 183 docs-only release head). Worktree had pre-existing untracked Austria audit artifacts; no tracked changes were present before this task.
- No reset or cleanup of existing user work was performed.

## Romania article universe

- RO-EDU-01 — `bfd554ff-a41a-5338-ab3e-25bb3c827a73`; `education`; EN: Education and Romanian-Language Learning in Romania; languages en;ru;uk; sources/Core 10/10.
- RO-EMP-01 — `54197f49-594b-51d2-a40c-2fcded05f791`; `employment`; EN: Finding a Job in Romania; languages en;ru;uk; sources/Core 9/9.
- RO-EMP-02 — `61d13a69-6704-50ee-8b7b-e673a2828e65`; `employment`; EN: Work Authorization and Employee Rights in Romania; languages en;ru;uk; sources/Core 9/9.
- RO-HEALTH-01 — `be12596b-8657-5b23-a17c-dead174859f3`; `healthcare`; EN: Accessing Healthcare in Romania; languages en;ru;uk; sources/Core 6/6.
- RO-HOUSING-01 — `35bb6fe6-b0b9-5517-af04-e022c7105ec7`; `housing`; EN: Finding Accommodation in Romania; languages en;ru;uk; sources/Core 4/4.
- RO-SOC-01 — `97f02ab3-3352-597b-ad48-11ebf19993c2`; `social-assistance`; EN: Financial and Social Assistance in Romania; languages en;ru;uk; sources/Core 7/7.
- RO-TP-01 — `24d0ab3d-935c-50b7-8385-c1c54583ec51`; `temporary-protection`; EN: Temporary Protection in Romania: Applying and Maintaining Your Status; languages en;ru;uk; sources/Core 3/3.
- RO-TP-02 — `6b966a1f-335e-596a-9825-005d30ab23d4`; `temporary-protection`; EN: Residence Options Beyond Temporary Protection in Romania; languages en;ru;uk; sources/Core 8/8.

- Article count: 8. Duplicate ArticleCodes: 0. Duplicate ArticleIds: 0. Missing locale variants: 0. Romania orphan content records: 0 found. Unexpected extra records: 0. Translation payloads: 24/24 EN/UK-UA/RU complete. Review copies created: 8/8.

## Claim / translation counts

- Extracted factual proposition candidates: 54; active candidates: 54; inactive structured claim rows found in production: 0. Material/supporting: 47/7; arithmetic: 54 = 54.

- NumericDate YES: 10. Temporal CURRENT/FUTURE/HISTORICAL/TRANSITION: 50/1/0/3; arithmetic: 54 = 54.

- Compound candidates: 26 (not split); non-propositional rows retained in ledger: 0; duplicate ClaimIds: 0. Exact normalized duplicate claim groups: 0; contextual/near-duplicate groups flagged: 3.

## Translation baseline

- Material divergences: 1. NumericDate divergences: 0. Missing proposition components: 1. Extra propositions: 0. Temporal divergences: 0.
- Initial scope review flags: housing support eligibility/designated-setting scope in RU; covered temporary-protection minors are not expressly named in UK-UA/RU education wording. Human review required; no translation repair made.

## Sources / Core

- Romania OfficialSources: 56; provisional priority P0/P1/P2 39/11/6. Candidate currentness: CURRENT 53; LEGACY 3; BROKEN 0; SUPERSEDED 0; NOT_RELEVANT 0; UNCLEAR 0.
- Same-owner canonical duplicate groups: 0. Cross-owner canonical duplicate groups: 2. These shared legal URLs are recorded as structural duplicates across article owners and are not presumed invalid.
- Current Core/source-owner pairs: 56. Dangling sources, unknown owners, wrong-owner pairs, duplicate pairs, and owned sources without expected owner-level Core binding: 0 found.

## Claim-source graph / coverage

- Existing-citation candidate tuples: 108, all PENDING_SOURCE_REVIEW. Claims with zero/one/multiple candidate sources: 0/16/38. Coverage REQUIRES_SOURCE_REVIEW 54; NO_EXISTING_CANDIDATE 0.
- Zero-candidate ClaimIds: none.

## Compare / Watch / risk

- Compare aspects: 24; production source tuples 67; owner-valid 67; violations 0. Candidate dispositions: KEEP 6, REVISE 7, UNAVAILABLE 0, NEWLY_AVAILABLE 0, REVIEW_REQUIRED 11. All valid tuples pending source review.
- Watch candidates: RO-W-001, RO-W-002, RO-W-003, RO-W-004, RO-W-005. Explicit future dates: 2027-03-05 and 2028-03-04; proposed prechecks are three months before each date. Other rate/school-year events have no exact date in current production.
- High-risk claims: 47; NumericDate 8; source-review-required 47; zero-candidate 0.

## Batch arithmetic

- BATCH-01 — Temporary protection / residence: 11 sources (P0/P1/P2 7/2/2).
- BATCH-02 — Employment / work / healthcare: 24 sources (P0/P1/P2 14/6/4).
- BATCH-03 — Housing / education: 14 sources (P0/P1/P2 12/2/0).
- BATCH-04 — Social support: 7 sources (P0/P1/P2 6/1/0).
- Assigned: 56/56; duplicate assignments: 0; unassigned: 0.

## Artifacts

- 8 `RO-* _REVIEW_COPY_01.md` review copies (actual filenames omit the space).
- `CLAIM_LEDGER_01.csv`, `COMPOUND_CLAIM_AUDIT_01.csv`, `DUPLICATE_CLAIM_AUDIT_01.csv`, `TRANSLATION_ALIGNMENT_01.csv`.
- `OFFICIALSOURCE_INVENTORY_AUDIT_01.csv`, `SOURCE_CLAIM_LINK_AUDIT_01.csv`, `CLAIM_SOURCE_COVERAGE_01.csv`.
- `COMPARE_DRAFT_01.md`, `COMPARE_ASPECT_INVENTORY_01.csv`, `COMPARE_SOURCE_LINK_AUDIT_01.csv`.
- `DATE_EVENT_WATCH_CANDIDATES_01.md`, `WATCH_CANDIDATES_01.csv`, `HIGH_RISK_CLAIMS_01.csv`.
- `SOURCE_AUDIT_BATCH_PLAN_01.csv`, `SOURCE_AUDIT_INPUT_LIST_01.md`, `ROMANIA_ARTICLE_INVENTORY_01.csv`, `ROMANIA_REFRESH_RESEARCH_01.md`.
- This report: `TASK_184_ROMANIA_INITIAL_AUDIT.md`.

## Readiness

Baseline inventory and arithmetic are complete. No Romania production data was changed. The audit is ready to proceed to structural certification after human review of compound claim candidates and translation scope flags; all source tuples remain pending direct source audit.
