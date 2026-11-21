# Task 188 — Romania Batch 1 blocker resolution

**As of:** 20 November 2026  
**Result:** **ROMANIA SOURCE AUDIT BATCH 1 COMPLETE — READY FOR BATCH 2**

## Blocker and retrieval result

Task 187 directly audited 9 of 11 source records but left two P0 legislative sources blocked: consolidated OUG 194/2002 (`501ff4ab-87d5-514d-b2a5-ac95fc628881`) and OUG 32/2026 (`ee3461f2-62c1-5515-aff2-4abf1b958a8a`). Both legal texts were recovered from official Romanian government representations. OUG 194 was opened through the Portal Legislativ alternate view `https://legislatie.just.ro/Public/DetaliiDocumentAfis/50236` and the IGI-hosted consolidated PDF; the current consolidation history ends 27 April 2026. OUG 32 was opened as the full official PDF linked by the IGI legislation page; it was published and entered into force on 27 April 2026.

OUG 194 Articles 45–73 set ordinary visa/residence routes and procedures. Article 51(4²) preserves lawful stay through an extension decision when the person was lawfully present on filing; that is distinct from work authorization. Article 70(2)(b) excludes a person from obtaining long-term residence while in temporary-protection applicant/beneficiary status. Article 71 does not expressly say whether TP time is credited after leaving TP. OUG 32 Articles 48–52 govern defined employment transitions; Article 51’s amended employer-change rules apply from 8 August 2026, while Article 52 is a specified cohort transition through 31 December 2026. It does not establish a general TP-to-study/family/Blue-Card path.

The failed Portal and Monitorul paths and complete legal-text findings are in `TASK_188_RESEARCH_TRACE_01.md`.

## Source inventory delta

Both recovered records are `CURRENT` and `UPDATE`; the URLs and canonical URLs need correction to the successful same-instrument official representations. UUIDv5 SourceId generation includes canonical URL, so both are flagged `SOURCE_ID_REGEN_REQUIRED_LATER`. No UUID was generated or altered; no OfficialSource row was added.

Final 11-source action totals: KEEP 6; UPDATE 4; REMOVE 0; RETAIN_HISTORICAL 0; REPLACE_CANDIDATE 1; REVIEW_BLOCKED 0. Currentness totals: CURRENT 9; LEGACY 2; SUPERSEDED 0; BROKEN 0; NOT_RELEVANT 0; UNCLEAR 0. Source action and currentness arithmetic reconcile to 11.

## Claim graph and coverage

The 10 target claim-source tuples were resolved: 3 `SUPPORTED` (C001, C002, C003) and 7 `NOT_SUPPORTED` with exact partial support recorded (C007-S01/S02/S03, C008 for each act, C009 for each act). No target tuple remains pending or blocked. C001–C003 are `SUPPORTED_AS_WRITTEN`. C007-S01 needs broader direct route sources or narrower wording; C007-S02/S03 and C008 need narrower evidence-boundary wording. C009 needs to be split: conditional right to remain is answered by Article 51(4²), pending work authorization remains separate, and prior TP-period credit toward long-term residence is not resolved by the cited current text.

The active TP/residence coverage set contains 30 claims: COVERED 15; COVERED_MULTIPLE 0; UNSUPPORTED 15; REQUIRES_SOURCE_REVIEW 0; NO_EXISTING_CANDIDATE 0. The 15 remaining gaps are:

- `RO-TP-01-C003-S01` — NEW_SOURCE_NEEDED (cohort detail)
- `RO-TP-01-C004-S01` — NARROWER_REWRITE_NEEDED (population qualifier)
- `RO-TP-01-C006-S01`, `RO-TP-01-C006-S02`, `RO-TP-01-C006-S03` — NEW_SOURCE_NEEDED (in-person/photo/fee)
- `RO-TP-01-C007` — NEW_SOURCE_NEEDED (appointment/checklist/time negative boundary)
- `RO-TP-01-C009`, `RO-TP-01-C010` — NEW_SOURCE_NEEDED (printed permit expiry/card exchange)
- `RO-TP-02-C006-S01` — NARROWER_REWRITE_NEEDED (TP-specific work route, not general conversion)
- `RO-TP-02-C006-S02` — NEW_SOURCE_NEEDED (filing effect/approval guarantee)
- `RO-TP-02-C007-S01` — NEW_SOURCE_NEEDED (ordinary procedures proposition under its current tuple/source)
- `RO-TP-02-C007-S02`, `RO-TP-02-C007-S03`, `RO-TP-02-C008` — NARROWER_REWRITE_NEEDED (source-set evidence boundaries)
- `RO-TP-02-C009` — NARROWER_REWRITE_NEEDED / NEW_SOURCE_NEEDED (split lawful-stay, work-right and TP-time-credit propositions)

## Compare, high-risk, and watch deltas

All four blocked Compare tuples are now `SUPPORTED`; the final 17 tuples are 16 supported, 1 not supported, 0 blocked/pending, with 0 ownership violations. `residence-routes` is REVISE because the IGI long-term residence explainer is dated 2022, while the current OUG, eVisa/family routes and TP work route are supported. `residence-conditions` is KEEP. `residence-filing` is KEEP with the explicit distinction between pending lawful stay and work authorization. The current support set is enumerated in `COMPARE_SOURCE_LINK_AUDIT_01.csv` and `COMPARE_DRAFT_01.md`.

All 22 Batch-1 high-risk rows were reviewed. Source-certified: 11; evidence gaps: 11; review-blocked: 0. The two recovered acts resolve three high-risk gaps (RO-TP-02-C001/002/003). Remaining high-risk gaps: RO-TP-01-C003-S01, RO-TP-01-C006-S03, RO-TP-01-C007, RO-TP-01-C009, RO-TP-01-C010, RO-TP-02-C006-S02, RO-TP-02-C007-S01, RO-TP-02-C007-S02, RO-TP-02-C007-S03, RO-TP-02-C008, RO-TP-02-C009.

RO-W-001 remains EDIT at 2027-03-05; RO-W-002 remains EDIT at 2028-03-04. Task 187’s extra-a `d22b2dda-6a8a-5a85-ae33-aa40b6a778fe` occurrence was a report typo; actual ID is `d22b2dda-6a08-5a85-ae33-aa40b6a778fe`. No phantom ID was introduced.

## Scope and readiness

Batch 1 blockers are closed; ordinary evidence gaps remain clearly assigned for later source/rewrite resolution. Ready for Batch 2. No Batch 2 sources were audited. No production article, production source, Core binding, or other country changed. No tests were run. No commit or push was made.
