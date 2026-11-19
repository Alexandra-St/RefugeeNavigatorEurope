# Task 148 — Fifth independent final certification

**As of:** 19 November 2026  
**Verdict: FRANCE APPROVED FOR INTEGRATION.** This final independent review reconstructed the current France staging state from its ledgers, source audits, review copies, Compare pair sets, Watch file, and production records. No staging content was repaired. Only this certification report was created. Production and canonical records were not changed. No commit, push, tests, Docker, or release CI were run.

## Claim universe and coverage

The active ledger contains **172** claims: **117 MATERIAL** and **55 SUPPORTING**. All are substantive and atomic. Article counts are FR-TP-01 **20**, FR-TP-02 **21**, FR-EMP-01 **17**, FR-EMP-02 **19**, FR-HEALTH-01 **20**, FR-HOUSING-01 **26**, FR-EDU-01 **22**, and FR-SOC-01 **27**. The eight article UUIDs match the required IDs. Active ClaimIds are unique; there are no active empty propositions, split parents, removed claims, or orphan lineage rows.

NumericDate=YES totals **42**. The classifications match proposition text, including the operative 24 February 2022 cutoff in C023 and dated-artifact propositions C025, C026, and C040. Task 145’s NO→YES changes are C025, C026, and C040; C023 remains YES because its own proposition states the operative date. The six final HISTORICAL claims are C002, C025, C026, C040, C041, and C042 (article prefixes FR-TP-01 / FR-SOC-01 as applicable). The dated artifacts/events are explicitly framed as historical in the review copies; C040 now says that its submission-destination instruction came from the May 2024 form and does not establish the process in force today. Historical copy scope: **6/6 safe**.

Temporal classes total **CURRENT 159, FUTURE 0, HISTORICAL 6, TRANSITION 7**, reconciling to 172. Effective coverage yields **140 COVERED**, **32 COVERED — MULTIPLE SOURCES**, **0 NOT EXTERNALLY SOURCE-DEPENDENT**, **0 UNSUPPORTED**, and **0 REQUIRES_SOURCE_REVIEW**. Material unsupported and NumericDate unsupported are both zero. All four reopen-candidate rows are resolved or cleared. No active CURRENT actionable claim relies solely on RETAIN_HISTORICAL evidence.

## Effective evidence and source integrity

Resolving the latest audit result per exact active ClaimId/SourceId pair gives **423** effective Core tuples: **210 SUPPORTED**, **213 NOT_SUPPORTED**, **0 unresolved**, and **0 duplicate effective tuples**. Active/effective Core source references have **0 unknown IDs, 0 dangling IDs, and 0 wrong-owner bindings**. The Task 147 dangling candidate ID is absent from operative current references; the old UUIDv4 is absent from operative current references. Remaining mentions in certification history describe the repairs and are non-operative.

The final EUR-Lex Article 2 source is `fdf859e7-da78-52eb-88e9-0343805006d8`, UUID version 5, owned by FR-TP-01 (`d16ad009-3391-5e3b-80b2-5b0d348503d3`), canonical URL `https://eur-lex.europa.eu/eli/dec_impl/2022/382/oj/eng`. Recomputing UUIDv5 with `NAMESPACE_URL` and key `refugeenavigator:official-source:<owner-UUID>:<canonical-URL>` reproduces the ID; the convention is documented in the France source-add records and prior source-add documentation. C021–C024 remain atomic, retain current EUR-Lex support, and do not rely solely on legacy evidence. C023 states lawful residence before 24 February 2022, a valid Ukrainian-law permanent residence permit, and inability to return in safe and durable conditions; NumericDate=YES follows from that proposition text. The 2022 Ministry source `04f61765-f6e7-5f22-87c0-60f6382fa42c` remains RETAIN_HISTORICAL/supplementary. C039 remains CURRENT with current CESEDA source `ba87b018-5dbb-5a3e-aeb9-c84e254ad507`; the OFII form is supplementary only. C040 remains ACTIVE/HISTORICAL with source `aa0f80be-7a98-5b27-a6e2-38286081f4ed`.

## Review copies and translations

All eight EN/UK/RU review copies were checked. Each article UUID matches, each language section is present, and every active ClaimId is represented in the final atomic claim index. C040 is represented in all three Social language copies with historical date/scope wording and the existing OFII source citation. The seven Task 141 repaired claims remain aligned (**7/7**); FR-TP-02-C004/C005/C006 remain inactive. No missing active propositions, extra unsupported propositions, material semantic divergences, or numeric/date divergences were identified. Translation alignment: **PASS**.

## Source inventory and integration bindings

Current production France OfficialSources: **104**. Staging inventory: **118**. Existing source actions are **KEEP 94 + UPDATE 8 + RETAIN_HISTORICAL 2 + REMOVE 0 = 104**; ADD: **14**. Every UPDATE has an actionable successor/current URL. The ADD set is:

`56c5f996-f13c-5df1-9cd2-ca2858889e6d`, `fecb425a-243b-5233-871d-2ce26b742a66`, `3a55eb20-dea4-5b00-bdef-7d932cf1fa31`, `d277a456-4e10-5a6c-849a-e23ba7474991`, `bf5e5f83-b27d-5b60-aa2d-9e5a924b342e`, `fd583be7-629f-5004-a38d-136e1a85b2d4`, `06d80225-6dde-5d61-94c8-48feac8d8e67`, `6ed80718-151e-52d3-bebd-dd8f9eb07d38`, `9003daa2-1315-58ef-8237-e8147d3a5592`, `77766134-f03b-5576-89df-6f38a08ed93c`, `ea7b16e1-7abc-5ebc-9521-2d45289a040d`, `a8de9820-9b53-5b98-b1e7-b89cb427f71d`, `c42ac694-fe38-52e6-b619-3f6b18542d89`, `fdf859e7-da78-52eb-88e9-0343805006d8`. Owner validation is **14/14**; conflicts and unintended same-owner/canonical-URL duplicates are zero. The two RETAIN_HISTORICAL records are the 2022 Ministry page (`04f61765-f6e7-5f22-87c0-60f6382fa42c`) and May 2024 OFII form (`aa0f80be-7a98-5b27-a6e2-38286081f4ed`). Planned France source count: **118**. Current global sources: **1,530**; planned global sources: **1,544**.

Core binding pairs were independently reconstructed by grouping the latest effective SUPPORTED active claim/source mappings by article and comparing them with production OfficialSources. Production **104**, staging **97**, union **118**; KEEP **83**, ADD **14**, REMOVE **21**. Set equations hold: 104=83+21; 97=83+14; 118=83+14+21. No source binding changes were applied.

## Compare review and delta

The 24 final Compare aspects were reviewed; all 24 are source-certified. `protection-scheme` now distinguishes Article 2(1) groups from the Article 2(2) group and preserves the alternative: Member States apply either Decision (EU) 2022/382 or adequate protection under national law to the defined Article 2(2) cohort. The exact current Article 2 source tuple was retested and is SUPPORTED. No overstatement remains.

Effective Compare graph: **89 tuples — 86 SUPPORTED, 3 NOT_SUPPORTED, 0 REQUIRES_SOURCE_REVIEW**. Primary dispositions, one per aspect: **KEEP 11, REVISE 13, UNAVAILABLE 0, NEWLY_AVAILABLE 0**. The REVISE aspects are protection-scheme, healthcare-access, healthcare-proof, healthcare-urgent, housing-initial, housing-private-rental, housing-financial-support, education-school-enrolment, education-pupil-language, education-higher-admission, social-subsistence, social-application, and social-child-benefits. Secondary NEWLY_AVAILABLE markers: **0**.

The raw production pair set from `comparison-facts.json` has **71** pairs. The 24 staging Candidate SourceIds lists have **77** pairs. Their union has **89**. Set delta: **KEEP 59, ADD 18, REMOVE 12**; equations hold: 71=59+12; 77=59+18; 89=59+18+12. `COMPARE_BINDING_DELTA_FINAL_01.csv` matches the raw sets: one row per union pair, no duplicate pair rows, correct membership flags, and correct DeltaAction. Compare dangling SourceIds: **0**. This independently confirms Task 145’s delta and corrects Task 146’s misclassification of the 89-pair union as the staging set.

## Watch and high-risk checks

The Watch has **14** candidates: final APPROVE **11**, REMOVE **3**, REQUIRES_GAP_RESOLUTION **0**; historical audit actions APPROVE/EDIT/REMOVE are **9/2/3**. Duplicate WatchIds, approved stale/inactive ClaimIds, and approved unknown SourceIds: **0**. FR-W02 approves monitoring metropolitan and Mayotte SMIC claims C012/C028. FR-W14 is APPROVE / DECISION_EXPECTED for the PUMa implementing decree, with trigger date not established. Earliest future legal/effective trigger is **5 March 2027**; earliest maintenance pre-check is **1 December 2026**.

High-risk regression review: EU TP **PASS**; APS **PASS**; 5 August 2026 rule **PASS**; Employment **PASS**; SMIC **PASS** (Metropolitan France €12.31 gross/hour and €1,867.02 gross/month; Mayotte €9.56/hour and €1,449.93/month; effective 1 June 2026); PUMa **PASS**; Housing **PASS**; Education **PASS**; Social **PASS**. C039 retains current evidence, C040 is dated historical copy, and no CURRENT actionable Social claim uses RETAIN_HISTORICAL evidence alone.

## Exact production integration delta

The independently derivable integration plan is:

- Update the eight France information-entry contents with their reviewed EN/UK/RU copy.
- OfficialSources: KEEP **94**, UPDATE **8**, ADD **14**, REMOVE **0**; planned France **118**, planned global **1,544**.
- Core bindings: KEEP **83**, ADD **14**, REMOVE **21**.
- Compare content dispositions: KEEP **11**, REVISE **13**, UNAVAILABLE **0**, NEWLY_AVAILABLE **0**.
- Compare bindings: KEEP **59**, ADD **18**, REMOVE **12**.

This is a certification only; the integration plan was not applied. No substantive or integrity blockers remain. **Final verdict: FRANCE APPROVED FOR INTEGRATION.**
