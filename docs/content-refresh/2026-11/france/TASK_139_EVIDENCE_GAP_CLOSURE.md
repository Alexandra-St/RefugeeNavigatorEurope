# Task 139 — France evidence-gap closure

As of 19 November 2026. This is a staging result for Task 140 independent certification; it is not approval for production integration.

## Cumulative batches

- **Batch 1 — Temporary protection and residence routes:** processed 23 rows (12 removed, 6 rewritten, 4 supported by new official sources, 1 reopen cleared with no proposition change).
- **Batch 2 — Employment:** processed 23 rows (11 removed, 7 supported by new official sources, 2 rewritten, 2 supported by existing sources, 1 reopen cleared with no proposition change), covering employment rights, employer duties, pay and leave.
- **Batch 3 — Health and housing:** processed 20 rows (8 removed, 8 rewritten, 3 supported by existing-source reevaluation, 1 supported by a new official source); kept healthcare coverage and housing eligibility distinct.
- **Batch 4 — Education:** processed 12 rows (7 removed, 3 rewritten, 1 supported by a new official source, 1 supported by existing-source reevaluation); retained assessed enrollment, language and admission routes and removed unsupported blanket claims.
- **Batch 5 — Social (Task 139):** processed all 22 remaining unsupported Social claims: 12 MATERIAL, 10 SUPPORTING, and 5 NumericDate. Outcomes: 3 SUPPORTED_EXISTING_REEVALUATED, 1 SUPPORTED_NEW_OFFICIAL_SOURCE, 12 REWRITE_TO_SUPPORTED_SAFE_CLAIM, 6 REMOVE_UNSUPPORTED_CLAIM, 0 blocked. FR-SOC-01-C035 was narrowed to CAF des Yvelines’ local guidance; its reopen item is resolved.

The starting cumulative unsupported universe was 97. Across the five batches, all 97 input gaps are resolved or removed; no active claim remains unsupported or under source review.

## Final claim and evidence state

- Active Core claims: **175** — **117 MATERIAL**, **58 SUPPORTING**, **39 NumericDate**.
- Coverage: **148 COVERED**, **27 COVERED — MULTIPLE SOURCES**, **0 NOT EXTERNALLY SOURCE-DEPENDENT**, **0 REQUIRES_SOURCE_REVIEW**, **0 UNSUPPORTED** (175 active claims total).
- Open claim-reopen candidates: **0**. FR-SOC-01-C035 is `RESOLVED_TASK_139`.
- Task 139 appended **23 exact SUPPORTED tuples**; no new NOT_SUPPORTED decisions were added and no historical NOT_SUPPORTED decision was flipped. Prior source-audit history is retained.

## Social evidence decisions

- **ADA:** CESEDA L. 581-9 supports the TP-specific, time-limited route subject to age and resources. OFII’s 2024 form supports continuation-only process and resource-documentation wording. The free-accommodation rule is limited to the additional accommodation amount. The unstable universal 2026 amount caveat and unsupported work-termination wording were removed; asylum-specific rules were not applied to TP.
- **RSA / Prime d’activité / ARE:** retained as distinct schemes. CNAF’s TP guidance supports the five-year work-authorized residence condition for RSA and Prime d’activité, with specific exceptions; France Travail’s own guidance supports the employment-history and other-condition statement for ARE.
- **CAF and family benefits:** CAF temporary identifier wording is limited to account access after starting a claim. CAF/MSA application routes are scheme- and regime-specific. CNAF IT-2025-005 supports a limited, benefit-specific child documentation exception; it does not waive all family conditions.
- **ARS:** C035 now states only the local CAF des Yvelines guidance and no longer implies a nationwide categorical rule.
- **Housing crossover:** no Social wording changes the already certified Housing financial-support proposition. ADA accommodation rules remain distinct from APL/ALF/ALS.
- **Emergency/local aid:** CCAS/CIAS wording remains local and non-guaranteed; no statutory entitlement is inferred.

## Staging sources and integrity

- New official-source ADD: **1**, owned by FR-SOC-01: `c42ac694-fe38-52e6-b619-3f6b18542d89`, CNAF IT-2025-005, [official CNAF PDF](https://caf.fr/sites/default/files/medias/cnaf/Nous_connaitre/qui%20sommes%20nous/Textes%20de%20r%C3%A9f%C3%A9rence/Circulaires/2025/IT-2025-005.pdf).
- France staging inventory: **117** records — **13 ADD**, **8 UPDATE**, **2 RETAIN_HISTORICAL**, **94 KEEP**, **0 REMOVE**. Production-existing source baseline remains **104**.
- Owner conflicts: **0**. Unintended same-owner/URL duplicates: **0**. Core dangling SourceIds: **0**. Compare dangling SourceIds: **0**.

## Compare and Watch

- Compare dispositions: **KEEP 12**, **REVISE 12**, **UNAVAILABLE 0**; the single NEWLY_AVAILABLE marker remains overlapping metadata. No broad recertification was performed.
- Task 139 revised and source-certified the exact propositions for `social-subsistence`, `social-application`, and `social-child-benefits`; all three have exact supported source links. Task 139 marked 14/24 source-certified; Task 140 independently found two missing aspect/source tuples and corrected the current staging status to 12/24. See the independent certification report.
- Watch candidates: **14 total** — candidate statuses: **11 APPROVE**, **0 EDIT**, **3 REMOVE**, **0 REQUIRES_GAP_RESOLUTION**; the separate audit-outcome column has 9 APPROVE, 2 EDIT and 3 REMOVE, with both EDIT outcomes pre-existing and outside Social scope.. FR-W09 was removed because the volatile ADA-rate claim was removed. No new watch was added.

## Readiness

The five evidence-gap batches close the active unsupported universe and leave zero open reopen candidates. EN/UK/RU Social review copy is aligned to the same scheme, temporal, and territorial limits. This staging set is ready for **Task 140 independent France certification**. Production/canonical content is unchanged; no commit, push, release CI, Docker, or tests were run.
