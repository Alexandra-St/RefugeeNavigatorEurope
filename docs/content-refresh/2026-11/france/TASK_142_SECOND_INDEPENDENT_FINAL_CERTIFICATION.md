# Task 142 — France second independent final certification

As-of: 19 November 2026

## Verdict

**FRANCE SECOND FINAL CERTIFICATION BLOCKED.** The counts below were reconstructed from the staging rows and current production JSON. No production or staging content was changed. Task 141’s summaries were treated as audit history, not arithmetic inputs.

## Independent recomputation

- Article identities: **8/8 pass**. All eight draft ArticleIds match Task 142’s expected UUIDs.
- Active ledger: **172** rows; by article: FR-TP-01 20; FR-TP-02 21; FR-EMP-01 17; FR-EMP-02 19; FR-HEALTH-01 20; FR-HOUSING-01 26; FR-EDU-01 22; FR-SOC-01 27. MATERIAL **117**, SUPPORTING **55**; NumericDate YES **36**; CURRENT **165**, FUTURE **0**, HISTORICAL **0**, TRANSITION **7**.
- Active compound claims **0**; active non-propositional rows **0**; duplicate active ClaimIds **0**; orphan lineage rows **0**; active split parents **0**; active removed claims **0**.
- FR-TP-02-C004/C005/C006 remain INACTIVE and absent from active totals, coverage, Compare and approved Watch rows: **PASS**.
- Coverage reconstructed from the latest exact ClaimId/SourceId adjudication and final staging sources: COVERED **145**, COVERED — MULTIPLE SOURCES **27**, NOT EXTERNALLY SOURCE-DEPENDENT **0**, UNSUPPORTED **0**, REQUIRES_SOURCE_REVIEW **0**. Coverage artifact mismatches: **0**. Unsupported MATERIAL/NumericDate claims: **0/0**.
- Effective Core graph: **418** tuples; SUPPORTED **205**, NOT_SUPPORTED **213**, unresolved **0**, duplicate effective tuples **0**. Historical repeated/superseded rows preserved: **20**; latest-pair resolution produces no ambiguity.
- Open reopen candidates: **0**. The four rows in the reopen file have resolved/cleared statuses. No active unresolved `PENDING_RESEARCH`, `BLOCKED_MATERIAL_CLAIM` or equivalent marker remains.

## Review copies and source gates

- Review copies rechecked: **8/8**. Every active ClaimId appears in its article copy/claim index; missing ClaimIds: **0**. Seven Task 141 claims are present in EN/UK/RU copy with associated sources: **7/7**. Material, numeric/date, and missing/extra proposition divergences found in the targeted recheck: **0**.
- Production France OfficialSources: **104**; current global source records: **1530**.
- Staging inventory: **117** records: existing KEEP **94**, UPDATE **8**, RETAIN_HISTORICAL **2**, existing REMOVE **0**, ADD **13**. Existing source action arithmetic: 94 + 8 + 2 + 0 = 104 of 104. Updates without a successor/current URL: 0. UPDATE SourceIds: bec70186-5af2-54e6-8ba3-2a4f8ce770c1, d9e7aa4b-7888-5f10-9781-05dadd3ca055, f923322c-4336-5c6a-a971-f23901bb4ce2, fe6b9f9e-ea2b-5864-bd31-e4f19b566e7b, 8e80f891-0c43-5ad7-a308-c54c4c55d097, e7915cbf-568f-5224-9872-9331f849e885, 6ab4e1c0-eea2-5c1a-b231-50aff97e701e, d8411755-c986-5660-83c1-cbfb80100896. RETAIN_HISTORICAL SourceIds: 04f61765-f6e7-5f22-87c0-60f6382fa42c, aa0f80be-7a98-5b27-a6e2-38286081f4ed.
- Exact ADD set (13): 06d80225-6dde-5d61-94c8-48feac8d8e67, 3a55eb20-dea4-5b00-bdef-7d932cf1fa31, 56c5f996-f13c-5df1-9cd2-ca2858889e6d, 6ed80718-151e-52d3-bebd-dd8f9eb07d38, 77766134-f03b-5576-89df-6f38a08ed93c, 9003daa2-1315-58ef-8237-e8147d3a5592, a8de9820-9b53-5b98-b1e7-b89cb427f71d, bf5e5f83-b27d-5b60-aa2d-9e5a924b342e, c42ac694-fe38-52e6-b619-3f6b18542d89, d277a456-4e10-5a6c-849a-e23ba7474991, ea7b16e1-7abc-5ebc-9521-2d45289a040d, fd583be7-629f-5004-a38d-136e1a85b2d4, fecb425a-243b-5233-871d-2ce26b742a66. Owner validation: **13/13**. `fecb425a-243b-5233-871d-2ce26b742a66` owner is FR-TP-02 UUID `78be24cd-82f5-5f29-ab54-0013aa15ab93`; erroneous owner `b43097d3-2524-5e94-8ab4-9a384efeb995` references: **0**. Source `6ed80718-151e-52d3-bebd-dd8f9eb07d38`: FR-EMP-02 owner, active claim C027, and copy citation all **PASS**.
- Unintended same-owner+URL duplicates: **0**. Five cross-owner same-URL groups are article-owned records with distinct owner scopes in the inventory; no ownership conflicts or cross-owner active copy bindings were found. Core/Compare dangling SourceIds: **0/0**.
- Planned France source records: **117** (104 current + 13 ADD, 0 REMOVE); planned global: **1,543** (1,530 + 13).
- Independently derived Core binding delta from current France source IDs and the 95 unique SourceIds cited in final article bodies: **KEEP 82; ADD 13; REMOVE 22**. This differs from Task 141’s **79/13/25** summary: body citations add three existing production sources (`1b5d164b-dda3-57f8-ab22-09eb8424d283`, `d67ebe35-199e-526d-ac69-ec317fd6452b`, `1bcae388-5a74-5027-87a3-0a18751ce5fb`) to the final binding set, so the source set is **82/13/22**. All 95 target source IDs exist in staging and have the correct article owner. The delta is mechanically derivable; the Task 141 count discrepancy is recorded, not silently carried forward.

## Compare and Watch

- Compare exact aspect set: **24/24**; every candidate source pair was checked against the final effective tuple row. Final aspects fully source-certified: **24/24**; uncertified: **0**; missing/unsupported candidate pairs: **0**. Effective aspect/source tuples: **86** unique; SUPPORTED **83**, NOT_SUPPORTED **3**, REQUIRES_SOURCE_REVIEW **0**, unresolved **0**. The three NOT_SUPPORTED pairs are rejected non-final candidates, not relied-on final sources.
- Primary dispositions: KEEP **12**, REVISE **12**, UNAVAILABLE **0**, NEWLY_AVAILABLE **0**. Secondary marker: protection-dates / NEWLY_AVAILABLE. REVISE set: healthcare-access, healthcare-proof, healthcare-urgent, housing-initial, housing-private-rental, housing-financial-support, education-school-enrolment, education-pupil-language, education-higher-admission, social-subsistence, social-application, social-child-benefits.
- Compare binding delta from production `comparison-facts.json` vs final candidate sets: **KEEP 59; ADD 15; REMOVE 12**. Compare dangling source IDs: **0**.
- Watch rows: **14**; final APPROVE **11**, REMOVE **3**; historical actions APPROVE/EDIT/REMOVE **9/2/3**; gap-resolution rows **0**; duplicate WatchIds **0**; stale/inactive approved ClaimIds **0**; unknown approved SourceIds **0**. Removed: FR-W07 (EDU-C034, removed year-specific automatic fee assertion); FR-W08 (EDU-C021, malformed composite removed); FR-W09 (SOC-C005, volatile universal ADA rate removed). FR-W14: APPROVE; DECISION_EXPECTED; date unknown; tracks implementing decree for PUMa contribution amount/modalities.
- Earliest future legal/effective trigger: **5 March 2027**, EU TP extension start. Earliest maintenance pre-check: **1 December 2026**.

## High-risk copy regressions

- EU TP extension: **PASS**; 5 March 2027 through 4 March 2028, distinct from APS validity.
- APS validity/renewal and local procedure: **PASS**; six-month validity, renewal during TP subject to remaining period, and Seine-Maritime procedure are stated.
- 5 August 2026 cohort and continuity exception: **PASS**.
- Employment separation (TP work permission, employer duties, France Travail registration, ARE, training, regulated-profession rules): **PASS**.
- SMIC: metropolitan figures **€12.31/hour and €1,867.02/month from 1 June 2026** appear; exact Mayotte figures **€9.56/hour and €1,449.93/month do not appear in the final review copy** (copy says only that Mayotte may have a different rate). No later rate is asserted. Required exact-figure regression: **FAIL**.
- PUMa participation group and decree-dependent amount/collection: **PASS**.
- Emergency health access: **PASS**; 15/112, scoped 114 accessibility route, and no blanket free-treatment claim.
- Housing non-guarantees: **PASS**; no guaranteed emergency place, automatic APL/HLM, universal Visale, or automatic APS substitution for rental identity-document rules.
- Education: **PASS**; compulsory instruction 3–16, separate formation obligation 16–18, scoped CASNAV, no unsupported fixed language-support hours, and annual higher-education rules scoped.
- Social: schemes/TP vs asylum, RSA five-year rule and exceptions, local Yvelines scope, and non-guaranteed CCAS support are stated. **Historical-source freshness issue below prevents this gate from passing.**

## Substantive certification blockers

1. **Current temporary-protection eligibility facts depend only on historical evidence.** Active `FR-TP-01-C021`, C022, C023 and C024 are classified CURRENT and assert who the current France route covers. Their only latest SUPPORTED source is `04f61765-f6e7-5f22-87c0-60f6382fa42c`, whose inventory marks `CurrencyStatus=LEGACY`, `SourceAction=RETAIN_HISTORICAL`, and limits it to historical/category context. The current EU source `44083afc-531a-52d2-be25-8f39a26a7087` is explicitly NOT_SUPPORTED for each exact category tuple. This does not meet the Task 142 gate against a current claim improperly depending on a historical-only source.
2. **Current OFII administrator statement depends only on a historical form.** Active `FR-SOC-01-C039` states that OFII currently administers ADA for TP beneficiaries. Its only latest SUPPORTED source is `aa0f80be-7a98-5b27-a6e2-38286081f4ed`, marked LEGACY/RETAIN_HISTORICAL and scoped to a May 2024 continuation form as historical/transition procedure evidence. Other candidate tuples are NOT_SUPPORTED. The staging graph’s COVERED classification does not resolve this currency mismatch.
3. **Mayotte rate figures are missing from the final review copy.** The Task 142 high-risk check requires the staged figures €9.56/hour and €1,449.93/month; neither is present in EN/UK/RU. The copy’s narrower statement that the rate may differ does not satisfy the specified exact-figure regression.

Task 141’s Core binding count discrepancy is a report arithmetic issue; the final integration delta is derivable from the actual source IDs. It is not counted above as a substantive content blocker.

## Production and final decision

Production `information-entries.json`, `official-sources.json`, and `comparison-facts.json` are unchanged. No tests, commits, pushes, Docker, CI or other-country files were changed. No staging content was repaired in this certification pass.

**Substantive blockers: 3. Final verdict: FRANCE SECOND FINAL CERTIFICATION BLOCKED.**


## POST-SECOND-CERTIFICATION REPAIR NOTE

Task 143 repaired the three blockers recorded above: TP claims C021–C024 were narrowed to Article 2 of EU Decision 2022/382 and supported by a new France-owned current-source ADD; C039 gained current CESEDA D.581-7 support while the 2024 OFII form remains RETAIN_HISTORICAL; and the exact Mayotte SMIC figures were added to aligned EN/UK/RU review copy and the work-minimum-pay Compare aspect. Effective active Core graph after repair: 423 (210 SUPPORTED / 213 NOT_SUPPORTED / 0 unresolved); Compare remains 24/24 certified; Watch remains 14 rows, with FR-W02 updated for both metropolitan and Mayotte rates. Task 143 report: `TASK_143_SECOND_CERTIFICATION_BLOCKER_REPAIR.md`. Its verdict is not integration approval; Task 144 independent final certification is next.
