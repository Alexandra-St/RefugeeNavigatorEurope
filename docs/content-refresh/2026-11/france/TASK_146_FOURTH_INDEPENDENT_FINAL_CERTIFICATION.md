# Task 146 — Fourth independent final certification

**As of:** 19 November 2026  
**Verdict: FRANCE FOURTH FINAL CERTIFICATION BLOCKED.** This review independently reconstructed the France staging package after Task 145. No production or staging content was repaired. The report itself is the only file created for this certification. No tests, release CI, commit, or push were run.

## Decision and release blockers

The staging package is not approved for integration. These issues remain:

1. **Compare `protection-scheme` is overbroad for Article 2(2).** The English proposition says France applies EU temporary protection to the named eligibility categories, including the Article 2(2) cohort. Article 2(2), as represented by the certified C023 proposition and source tuple, says Member States apply either Decision 2022/382 *or adequate protection under national law* to that cohort. The Compare proposition does not say which alternative France applies and asserts EU temporary protection. A SUPPORTED tuple for the source cannot certify this proposition as written. UK and RU translations carry the same scope issue. This leaves independently source-certified Compare at **23/24**.
2. **C040 is absent from the Social review-copy prose.** The ledger says the May 2024 OFII form instructed continuation applicants to send the form and supporting documents to the competent territorial directorate. That instruction appears in the claim index, but the EN/UK/RU copy bodies do not state the submission destination. The 8/8 copy indexes include their active ClaimIds, but this substantive proposition is not represented in the Social prose.
3. **Four active TP claim candidate lists retain a dangling SourceId.** `2e094655-76bf-4800-91a5-7b6e62823d20` appears in CandidateSourceIds for C021–C024 but has no final inventory record. It is not among the final review-copy citations or effective audited source tuples, so the accepted evidence graph remains 423 with zero dangling effective tuples. The active staging candidate/coverage references nevertheless do not pass a zero-dangling-reference gate without resolving whether those candidates should be removed or restored.
4. **Task 145’s Compare binding-delta arithmetic does not match the production and staging records.** Recomputing exact aspect/SourceId pairs from current `comparison-facts.json` and the 24 staging Candidate SourceIds gives **KEEP 71 / ADD 18 / REMOVE 0**, not the Task 145 report’s 59 / 18 / 12. The production delta is independently derivable, but the discrepancy is an unresolved certification-record integrity issue.

## Claim ledger and coverage reconstruction

All eight article IDs in the review copies match the specified UUIDs. Active substantive claims total **172**, by article:

| Article | Active claims |
|---|---:|
| FR-TP-01 | 20 |
| FR-TP-02 | 21 |
| FR-EMP-01 | 17 |
| FR-EMP-02 | 19 |
| FR-HEALTH-01 | 20 |
| FR-HOUSING-01 | 26 |
| FR-EDU-01 | 22 |
| FR-SOC-01 | 27 |

Materiality: **117 MATERIAL**, **55 SUPPORTING**. NumericDate=YES: **42**. Task 145 NumericDate changes were **FR-SOC-01-C025 NO→YES**, **C026 NO→YES**, and **C040 NO→YES**; each revised proposition explicitly refers to the May 2024 form. C023 was already YES in the Task 144 baseline; Task 145 retained YES and added the operative 24 February 2022 cutoff to the proposition. NumericDate count and classifications are internally consistent with the final text.

Temporal classes: **CURRENT 159, FUTURE 0, HISTORICAL 6, TRANSITION 7**; total 172. Active compound claims: **0**; active non-propositional rows: **0**; duplicate active ClaimIds: **0**; orphan lineage rows: **0**; active removed claims: **0**; active split parents: **0**.

The six Task 145 historical claims are:

| ClaimId | Final proposition | Class | Supported SourceIds | Historical rationale |
|---|---|---|---|---|
| FR-TP-01-C002 | The Ministry’s central eligibility page was published in 2022. | HISTORICAL | 04f61765-f6e7-5f22-87c0-60f6382fa42c | Dated 2022 publication fact. |
| FR-SOC-01-C025 | The OFII temporary-protection ADA continuation form dated May 2024 asked applicants to report resources and provide supporting documents. | HISTORICAL | aa0f80be-7a98-5b27-a6e2-38286081f4ed | Dated form content, past tense. |
| FR-SOC-01-C026 | The OFII temporary-protection ADA continuation form dated May 2024 requested applicant and spouse/partner resource information, including supporting evidence. | HISTORICAL | aa0f80be-7a98-5b27-a6e2-38286081f4ed | Dated form content, past tense. |
| FR-SOC-01-C040 | The May 2024 form instructed continuation applicants to submit it and supporting documents to the competent territorial directorate. | HISTORICAL | aa0f80be-7a98-5b27-a6e2-38286081f4ed | Dated form instruction, past tense; missing from copy prose. |
| FR-SOC-01-C041 | The available OFII temporary-protection ADA continuation form is dated May 2024. | HISTORICAL | aa0f80be-7a98-5b27-a6e2-38286081f4ed | Historical artifact date fact. |
| FR-SOC-01-C042 | The May 2024 OFII form concerns TP holders already receiving ADA who seek continuation; it is not a universal first-claim checklist. | HISTORICAL | aa0f80be-7a98-5b27-a6e2-38286081f4ed | Historical form scope; explicitly excludes current first-claim instruction. |

The copies state the dated character of these claims, but C040’s submission instruction is not present in the copy prose. Current actionable claims supported solely by RETAIN_HISTORICAL sources: **0**. C039 remains CURRENT and supported by current CESEDA source `ba87b018-5dbb-5a3e-aeb9-c84e254ad507`; its other candidate `aa0f80be...` is historical and supplementary.

Coverage reconstruction from latest exact active ClaimId/SourceId decisions: **140 COVERED (single source), 32 COVERED — MULTIPLE SOURCES, 0 NOT EXTERNALLY SOURCE-DEPENDENT, 0 UNSUPPORTED, 0 REQUIRES_SOURCE_REVIEW**. Material unsupported: **0**. NumericDate unsupported: **0**. Open claim reopen candidates: **0** (all four recorded rows are resolved/cleared).

Effective active Core graph: **423 tuples — 210 SUPPORTED, 213 NOT_SUPPORTED, 0 unresolved, 0 duplicate effective tuples**. Superseded historical decisions were not added as parallel effective tuples. The four absent candidate-only IDs identified above are outside this effective graph but remain in active claim candidate fields.

## Source ID and source plan

Recomputed the documented construction: UUIDv5 using the URL namespace and stable key `refugeenavigator:official-source:<owner-UUID>:<canonical-URL>`. For owner `d16ad009-3391-5e3b-80b2-5b0d348503d3` and URL `https://eur-lex.europa.eu/eli/dec_impl/2022/382/oj/eng`, the result is `fdf859e7-da78-52eb-88e9-0343805006d8`, UUID version 5. The repository’s deterministic convention is evidenced in Task 144’s review of the France and other-country ADD records; this recomputation matches the staging record. Active references to former UUIDv4 `d0f6fb72-2f27-487d-8cc2-0cfdd32506f9`: **0**. The EUR-Lex record is owned by FR-TP-01 (`d16ad009-3391-5e3b-80b2-5b0d348503d3`), Authority Council of the European Union, Article 2 of Decision 2022/382; same-owner duplicate URL: **0**.

C021, C022 and C024 retain their exact category propositions and have SUPPORTED tuples to current staging source `fdf859e7-da78-52eb-88e9-0343805006d8`; C023 is supported by the same source after the Article 2(2) rewrite. None relies solely on legacy evidence. The legacy Ministry source `04f61765-f6e7-5f22-87c0-60f6382fa42c` is RETAIN_HISTORICAL and supplementary for C021–C024. C023 states the before-24-February-2022 legal-residence condition, valid Ukrainian-law permanent residence permit, and inability to return safely and durably; exact scope PASS. The 2026 extension remains separate from original eligibility.

Source inventory: production France **104**, staging France **118**. Existing actions: **KEEP 94 + UPDATE 8 + RETAIN_HISTORICAL 2 + REMOVE 0 = 104**. ADD **14**, planned France **118**, current global **1,530**, planned global **1,544**. UPDATE records without successor/current URL: **0**. Exact ADD IDs:

`56c5f996-f13c-5df1-9cd2-ca2858889e6d`, `fecb425a-243b-5233-871d-2ce26b742a66`, `3a55eb20-dea4-5b00-bdef-7d932cf1fa31`, `d277a456-4e10-5a6c-849a-e23ba7474991`, `bf5e5f83-b27d-5b60-aa2d-9e5a924b342e`, `fd583be7-629f-5004-a38d-136e1a85b2d4`, `06d80225-6dde-5d61-94c8-48feac8d8e67`, `6ed80718-151e-52d3-bebd-dd8f9eb07d38`, `9003daa2-1315-58ef-8237-e8147d3a5592`, `77766134-f03b-5576-89df-6f38a08ed93c`, `ea7b16e1-7abc-5ebc-9521-2d45289a040d`, `a8de9820-9b53-5b98-b1e7-b89cb427f71d`, `c42ac694-fe38-52e6-b619-3f6b18542d89`, `fdf859e7-da78-52eb-88e9-0343805006d8`.

ADD owners valid: **14/14**. `fecb425a...` owner is FR-TP-02 (`78be24cd-82f5-5f29-ab54-0013aa15ab93`); `6ed80718...` is owned by FR-EMP-02 and cited for C027. Owner conflicts: **0**. Unintended same-owner/canonical-URL duplicates: **0**. Final copy citations: 97 unique IDs; all 97 exist in the inventory and have the correct article owner. Core binding delta from production OfficialSources: **KEEP 83 / ADD 14 / REMOVE 21**. Final integration bindings are derivable.

Both RETAIN_HISTORICAL sources are `04f61765-f6e7-5f22-87c0-60f6382fa42c` (2022 Ministry eligibility/context page; used by HISTORICAL C002 and supplementary CURRENT TP claims) and `aa0f80be-7a98-5b27-a6e2-38286081f4ed` (May 2024 OFII continuation form; used by HISTORICAL C025/C026/C040/C041/C042 and supplementary C039). Neither is sole support for a CURRENT claim.

## Compare reconstruction

All **24/24** aspects are present and marked source-certified in the staging draft, with 24/24 aspect sections reviewed. The 24 aspects are protection-scheme, protection-application, protection-dates, residence-routes, residence-conditions, residence-filing, employment-vacancies, employment-institutions, employment-assistance-conditions, work-permission, work-employer-duties, work-minimum-pay, healthcare-access, healthcare-proof, healthcare-urgent, housing-initial, housing-private-rental, housing-financial-support, education-school-enrolment, education-pupil-language, education-higher-admission, social-subsistence, social-application, social-child-benefits. Independent source certification is **23/24** because `protection-scheme` wording overstates the Article 2(2) outcome. Current Article 2(2) SourceId is `fdf859e7-da78-52eb-88e9-0343805006d8`; explicit aspect/source tuple is stored SUPPORTED. Effective Compare tuples: **89 — 86 SUPPORTED / 3 NOT_SUPPORTED / 0 REQUIRES_SOURCE_REVIEW**. Stored unresolved count: 0; independently unresolved semantic component: protection-scheme. Dangling Compare SourceIds: **0**.

Primary dispositions: **KEEP 12 / REVISE 12 / UNAVAILABLE 0 / NEWLY_AVAILABLE 0**. Secondary NEWLY_AVAILABLE markers: **0**. REVISE aspects: healthcare-access, healthcare-proof, healthcare-urgent, housing-initial, housing-private-rental, housing-financial-support, education-school-enrolment, education-pupil-language, education-higher-admission, social-subsistence, social-application, social-child-benefits. Independently derived Compare binding delta: **KEEP 71 / ADD 18 / REMOVE 0** from 71 current production aspect/SourceId pairs and 89 staged pairs. This differs from Task 145’s 59/18/12 and must be reconciled in a later repair before certification.

## Review copies and regressions

All eight final review copies were checked; every active ClaimId appears in its article’s final atomic-claim index. EN/UK/RU are present in all eight. TP C021–C024 appear in all three language bodies with the 24 February 2022 conditions, including the C023 cutoff, permit and safe/durable-return conditions. C040 is the identified missing body proposition in all three Social language copies. The six historical claims are not stated as current instructions; the five form-content/scope claims and Ministry date are phrased as dated historical facts. Prior Task 141 claims are present in their intended article copies: **7/7** (EMP-01-C020/C022/C033, EMP-02-C027, SOC-C017/C026/C034). FR-TP-02-C004/C005/C006 remain inactive.

Material translation divergence identified: **1 proposition (C040 missing from EN/UK/RU body)**. Numeric/date divergence identified: **0**. Missing active propositions: **1 (C040)**; extra unsupported propositions identified: **0**. The candidate indexes contain the active sets plus inactive parent/removed audit entries; no active ID is missing from an index.

## Watch and high-risk checks

Watch candidates: **14**; final APPROVE **11**; final REMOVE **3**; historical actions APPROVE/EDIT/REMOVE **9/2/3**; REQUIRES_GAP_RESOLUTION **0**. Stale/inactive approved ClaimIds **0**; duplicate WatchIds **0**; unknown approved SourceIds **0**. FR-W02 APPROVE tracks metropolitan and Mayotte rate claims (EMP-02-C012/C028). FR-W14 remains APPROVE / DECISION_EXPECTED, trigger date unknown, for the PUMa implementing decree. Earliest future legal/effective trigger: **5 March 2027**. Earliest maintenance precheck: **1 December 2026**.

High-risk regressions: EU TP **FAIL** at the Compare scope; APS **PASS**; 5-August-2026 cohort/proof **PASS**; Employment **PASS**; SMIC **PASS** (metropolitan €12.31/hour and €1,867.02/month; Mayotte €9.56/hour and €1,449.93/month; effective 1 June 2026); PUMa **PASS**; Housing **PASS**; Education **PASS**; Social **FAIL** for copy alignment on C040, though C039 has current CESEDA support and no CURRENT actionable Social claim relies solely on historical sources.

## Final integration gate

Production France OfficialSources: 104; staging source plan: 118. Current production Compare bindings: 71 aspect/source pairs; staging: 89. Core delta is derivable. Production information entries, OfficialSources and comparison facts remain unchanged. Commit/push: none. Release CI: not run.

**Substantive/integrity blockers: 4** — protection-scheme Article 2(2) overstatement; C040 absent from three-language Social prose; four active candidate lists include absent SourceId `2e094655-76bf-4800-91a5-7b6e62823d20`; and Task 145 Compare binding-delta arithmetic mismatch. **Final verdict: FRANCE FOURTH FINAL CERTIFICATION BLOCKED.**


## POST-FOURTH-CERTIFICATION REPAIR NOTE — Task 147

Task 147 resolves the four localized Task 146 blocker categories in staging. `protection-scheme` now distinguishes Article 2(1) groups from the Article 2(2) group and states the Decision-or-national-law alternative; its EUR-Lex exact tuple was retested as SUPPORTED. The primary disposition is REVISE. FR-SOC-01-C040 now appears in the EN/UK/RU Social copy as a dated May 2024 form instruction, explicitly not current process guidance. The unrecognized SourceId `2e094655-76bf-4800-91a5-7b6e62823d20` was removed from the four active claim candidate lists and four coverage candidate lists; valid current EUR-Lex support remains.

The exact production/staging candidate-set comparison is production 71 pairs and staging 77 pairs: KEEP 59 / ADD 18 / REMOVE 12. This confirms the Task 145 59/18/12 arithmetic. Task 146’s 71/18/0 calculation mistakenly treated the 89-pair production/staging union as the staging set. The union table is `COMPARE_BINDING_DELTA_FINAL_01.csv`, with one row per pair and explicit membership columns. Task 145’s historical report was not rewritten. Task 146’s blocked verdict remains the accurate pre-repair certification verdict; its delta calculation is corrected by this note.

Task 147 does not self-approve integration; the next gate is Task 148, the fifth independent final certification.
