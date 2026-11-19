# Task 147 — Fourth-certification blocker repair

**As of:** 19 November 2026  
**Result: FRANCE FOURTH-CERTIFICATION BLOCKERS RESOLVED.** This is a staging-only repair; Task 148 must perform the fifth independent final certification. No production or canonical data was changed. No tests, CI, Docker, commit, or push were run.

## Four Task 146 blocker categories

1. **Compare `protection-scheme`: repaired.** The pre-repair EN proposition asserted that France applies EU temporary protection to every listed group, including the Article 2(2) cohort. The final EN/UK/RU text now identifies Article 2(1) groups separately and states that Member States apply either Decision (EU) 2022/382 or adequate protection under national law to the defined Article 2(2) group. The separate 5 August 2026 conditions are preserved. The exact current source is `fdf859e7-da78-52eb-88e9-0343805006d8`. Its aspect/source pair was retested against the final wording as SUPPORTED without adding a duplicate effective tuple. The aspect is REVISE and source-certified; Compare is 24/24. Primary dispositions: KEEP 11, REVISE 13, UNAVAILABLE 0, NEWLY_AVAILABLE 0. REVISE aspects: protection-scheme, healthcare-access, healthcare-proof, healthcare-urgent, housing-initial, housing-private-rental, housing-financial-support, education-school-enrolment, education-pupil-language, education-higher-admission, social-subsistence, social-application, social-child-benefits.

2. **Social C040 copy: repaired.** Final proposition: “The OFII temporary-protection ADA continuation form dated May 2024 instructed applicants seeking continuation to submit the form and supporting documents to the competent territorial directorate.” Temporal class remains HISTORICAL. EN/UK/RU state this is what the dated form instructed and does not establish the process in force today. Source remains `aa0f80be-7a98-5b27-a6e2-38286081f4ed` with role RETAIN_HISTORICAL. Missing active propositions: 0; material translation divergences: 0; numeric/date divergences: 0.

3. **Dangling TP candidate ID: repaired.** `2e094655-76bf-4800-91a5-7b6e62823d20` was a stale candidate reference, not an alias or source migration: it has no final inventory record or effective source tuple. Removed from CandidateSourceIds for FR-TP-01-C021 through C024 in both the claim ledger and coverage file (8 occurrences). Valid current `fdf859e7-da78-52eb-88e9-0343805006d8` support remains for all four. No source record was created. Core and Compare dangling SourceIds: 0; owner conflicts: 0; unintended same-owner/canonical-URL duplicates: 0.

4. **Compare binding delta: reconciled from the actual staging candidate lists.** Current production contains **71** pairs and staging contains **77** pairs. Set-based delta is **KEEP 59 / ADD 18 / REMOVE 12**; union: **89** pairs. This confirms Task 145’s arithmetic. Task 146’s 71/18/0 calculation treated the 89-pair union as the staging set. `COMPARE_BINDING_DELTA_FINAL_01.csv` contains one row per union pair with membership flags. Task 145’s historical report was not rewritten; a correction note was appended to Task 146 and the Compare repair register.

## Recomputed baseline and regressions

- Active claims: **172**; MATERIAL **117**; SUPPORTING **55**. NumericDate YES **42**. Temporal classes: CURRENT **159**, FUTURE **0**, HISTORICAL **6**, TRANSITION **7**.
- Coverage: **140 COVERED**, **32 COVERED — MULTIPLE SOURCES**, **0 UNSUPPORTED**, **0 REQUIRES_SOURCE_REVIEW**; material unsupported **0**; NumericDate unsupported **0**.
- Effective Core tuples: **423** (**210 SUPPORTED**, **213 NOT_SUPPORTED**, **0 unresolved**). Removing candidate-only references did not change effective tuples; Core binding delta remains KEEP **83** / ADD **14** / REMOVE **21**.
- Effective Compare tuples: **89** (**86 SUPPORTED**, **3 NOT_SUPPORTED**, **0 REQUIRES_SOURCE_REVIEW**).
- Source inventory: production **104**, staging **118**, ADD **14**, UPDATE **8**, RETAIN_HISTORICAL **2**, existing REMOVE **0**, planned France **118**, planned global **1,544**. EUR-Lex remains UUIDv5 `fdf859e7-da78-52eb-88e9-0343805006d8`; old UUIDv4 current references **0**.
- Watch: **14** candidates; APPROVE **11**, REMOVE **3**, REQUIRES_GAP_RESOLUTION **0**, approved stale/inactive ClaimIds **0**.
- Existing certified regression evidence: TP **PASS**; Social historical copy **PASS**; APS **PASS**; Employment/SMIC **PASS**; PUMa **PASS**; Housing **PASS**; Education **PASS**.

Task 146’s blocked verdict remains preserved as the accurate pre-repair result. Task 147 does not approve integration. No production/canonical data, other countries, tests, release CI, Docker, commit, or push were changed or run. **Ready for Task 148 fifth independent final certification: YES. Remaining Task 146 blocker categories: 0.**
