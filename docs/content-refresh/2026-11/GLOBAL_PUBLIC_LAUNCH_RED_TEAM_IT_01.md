# Task 214 — Italy final public-launch red-team audit

Date: 21 November 2026

Audited repository SHA: `2ad3477e97f35a3c74959a2a1315486e873c5573`

Production changed by this task: **NO**

## Result

**Italy final status: `P2_ONLY`.**

Severity counts:

- P0: **0**
- P1: **0**
- P2: **2**
- unresolved high-risk candidates: **0**

No factual P0/P1 public-launch blocker was found. Two bounded source-binding defects remain in the temporary-protection evidence chain. The underlying current propositions are supportable by current official/authoritative evidence; the defects concern which sources are actively bound to those propositions.

## Inventory

Current Italy production contains:

- **8** Italy InformationEntry articles;
- **24** localized article instances: EN / UK-UA / RU for all eight articles;
- **76** current Italy-owned OfficialSource records;
- **24** Italy Compare facts;
- all 24 Compare facts are `Approved` and available;
- **26/26** current material/high-risk Core proposition groups audited;
- **12/12** identified time-sensitive proposition groups audited;
- **24/24** Italy Compare facts audited;
- **78** localized material proposition instances reviewed for semantic parity;
- **0** material EN / UK-UA / RU semantic divergences.

The eight current articles are:

- `c8c06066-d3cd-55ce-88b0-3499384eece7` — Temporary protection in Italy
- `28aee635-27ff-525c-9cd3-8673b5ba2be3` — Residence options during or after temporary protection in Italy
- `39091eb7-04c7-5b45-83b7-585f5f1ce719` — Finding a job in Italy
- `d988941a-e709-53ba-a2bf-47f1ac2f9956` — Work rights and employee conditions in Italy
- `462ace7d-8deb-5cd7-a530-548c79a8e144` — Healthcare access in Italy
- `5aafddcb-e302-52b7-9365-f95e7261d3a4` — Housing in Italy
- `ba3d3110-730d-5340-a89a-2b6722430bdd` — Education in Italy
- `55696570-3337-5e2f-8a24-71e256e869e9` — Social assistance and family benefits in Italy

## Prior baseline

The audit used the existing Italy research/review artifacts as historical context but independently audited current production:

- `docs/content-refresh/2026-11/italy/ITALY_REFRESH_RESEARCH_01.md`
- `docs/content-refresh/2026-11/italy/ITALY_REFRESH_REVIEW_01.md`

The prior review had already identified and integrated substantial Italy updates. This Task 214 audit treats the resulting production JSON, not the earlier draft recommendations, as the object under review.

## Current official-evidence delta search

A targeted current-source recheck through 21 November 2026 found no material post-refresh rule change that makes current Italy production factually unsafe.

### Temporary protection

Council Implementing Decision (EU) 2026/1912, as corrected, continues to support:

- EU temporary protection through **4 March 2028**;
- the military-obligation condition for relevant new grants from **5 August 2026**;
- the corrected exception for people enjoying temporary protection in the same Member State on or before **4 August 2026** and continuously thereafter.

Current Italy operational guidance from Polizia di Stato Questure and current UNHCR Italy guidance describe temporary-protection residence permits as renewable on request through **4 March 2027**.

The national primary-law source currently bound in production requires qualification: the literal text of Decree-Law 201/2025 as converted by Law 27/2026 expressly discusses renewal of `permessi di soggiorno per protezione speciale` held by Ukrainians present before 24 February 2022 and separately preserves the EU temporary-protection extension. It does not, by itself, state the complete operational TP-permit renewal proposition used in production.

### Employment and pay

Current official evidence continues to support:

- TP work access from the TP application stage;
- ordinary employer notification/payroll/contribution/safety duties;
- no single statutory euro minimum wage applying to every Italian job;
- the 2026 fair-pay framework using representative CCNL economic treatment rather than one universal hourly amount.

No conflicting current statutory national minimum amount was found.

### Healthcare

The Italian Ministry of Health currently states that compulsory SSN registration for Ukrainian TP permit holders and people who have filed the TP request is assured through **31 December 2026**.

Current production correctly does not assert continuation after that date.

The X22 exemption is also treated as time-limited rather than permanent.

### Housing / reception

Civil Protection OCDPC 1175/2025 continues specified Ukraine-related CAS/SAI reception arrangements through **4 March 2027**, subject to the covered arrangements and available programme capacity.

Current production correctly avoids presenting this as a guaranteed nationwide bed.

### Education

No material current contradiction was found in the school-access, compulsory-education, language-support or higher-education propositions. Current production appropriately distinguishes school entitlement from guaranteed placement/support and university admission from automatic tuition relief.

### Social assistance

The former national Ukraine `contributo di sostentamento` remains closed. The official contribution platform states that the last instalment request closed on **20 April 2025**.

Current INPS guidance continues to support variable 2026 AUU treatment and Bonus nido of up to **€3,600** only where the applicable eligibility and documented-cost conditions are met.

## Time-sensitive audit

The following 12 material time-sensitive proposition groups were checked:

1. EU TP endpoint — 4 March 2028 / Italian permit horizon — 4 March 2027.
2. Military-obligation rule — 5 August 2026 / continuity cutoff — 4 August 2026.
3. Residence-route TP renewal and post-2028 distinction.
4. Historical 4 March 2026 renewal deadline versus continuing work-conversion rule.
5. SIISL mandatory-communications alternative from 1 April 2026.
6. Current 2026 fair-pay / CCNL framework.
7. SSN compulsory enrollment through 31 December 2026.
8. X22 exemption through 31 December 2026.
9. Covered CAS/SAI reception through 4 March 2027.
10. Closed subsistence deadline 20 April 2025 and closed local 2026 housing example.
11. Compulsory-school age/duration: generally 6–16 with completion obligation through 18 as applicable.
12. 2026 AUU / Bonus nido amount and eligibility framework.

`TIME_SENSITIVE_AUDITED=12/12`

## Source-binding structural audit

Current Italy source relationships were structurally checked across Core and Compare.

Results:

- Italy-owned OfficialSource records: **76**
- Core source-token references: **244**
- unique Core source IDs used: **76**
- Compare source bindings: **61**
- unique Compare source IDs: **58**
- dangling Core source IDs: **0**
- wrong-owner Core source IDs: **0**
- dangling Compare source IDs: **0**
- wrong-owner Compare source IDs: **0**
- duplicate source IDs inside one Compare fact: **0**
- duplicate Italy OfficialSource IDs: **0**
- duplicate Italy owner+URL source records: **0**

The structural source graph is therefore intact. The two findings below concern substantive source scope rather than broken IDs or ownership.

## Finding IT-P2-001 — TP permit renewal proposition uses an incomplete / wrong-scope primary binding

Severity: **P2**

Affected Core articles:

- `c8c06066-d3cd-55ce-88b0-3499384eece7`
- `28aee635-27ff-525c-9cd3-8673b5ba2be3`

Affected Compare aspect:

- `protection-dates`

Relevant current source IDs:

- `a73b7d5b-720e-5d02-8014-32cc416c5b5d`
- `1bad6a10-abe2-5b2f-976c-bb4b3e4ad274`
- `9e67a296-0fac-5005-98b9-11e079bd56e5`

Current production proposition:

> Existing Italian TP permits can be renewed on request through 4 March 2027, while the EU scheme runs through 4 March 2028.

Disposition:

**PARTIAL / WRONG_SCOPE active binding.**

The proposition itself is consistent with current Polizia di Stato Questura guidance and current UNHCR Italy guidance.

However, the active `a73...` / `1bad...` records point to Decree-Law 201/2025 as converted by Law 27/2026 and label that source as an Italian TP renewal limit. The literal primary-law text at that location expressly governs renewal of `permessi di soggiorno per protezione speciale` for the specified pre-24-February-2022 Ukrainian cohort and separately refers to the temporary-protection extension. It does not, standing alone, state the complete request-based TP-permit renewal proposition.

In addition, the current `protection-dates` Compare fact binds only `a73...`, although the fact also asserts the EU 4 March 2028 endpoint.

Current corroborating evidence:

- Polizia di Stato, Questura di Roma — current TP permit renewal guidance through 4 March 2027:
  `https://questure.poliziadistato.it/it/Roma/articolo/199867b85c08de1f5642927550`
- Polizia di Stato, Questura di Udine — current TP permit renewal guidance through 4 March 2027:
  `https://questure.poliziadistato.it/it/Udine/articolo/33996996c5977e133806420427`
- UNHCR Italy — current temporary-protection guidance:
  `https://help.unhcr.org/italy/forms-of-protection-in-italy/temporary-protection/`
- EUR-Lex, Decision 2026/1912 — EU scheme through 4 March 2028.

Minimal repair:

1. Add or bind a directly supporting current official operational source for the Italian TP-permit renewal proposition, preferably a current Polizia di Stato source.
2. Keep the Normattiva source only for the proposition it directly supports / as legal context, or rename/re-scope the source record so it is not presented as direct proof of the full TP renewal proposition.
3. Add `9e67a296-0fac-5005-98b9-11e079bd56e5` to `protection-dates` for the EU 4 March 2028 component.
4. No substantive Core wording change is required unless the repair chooses to further qualify the national legal basis.

Confidence: **HIGH**

This is P2 rather than P1 because current official operational guidance supports the user-facing rule; the defect is in the active evidence binding.

## Finding IT-P2-002 — `protection-scheme` Compare fact is under-bound

Severity: **P2**

Affected Core article:

- `c8c06066-d3cd-55ce-88b0-3499384eece7`

Affected Compare aspect:

- `protection-scheme`

Current Compare source binding:

- `dde016b4-2f2d-5322-9d21-0269318f8456`

Current Compare proposition combines:

- foundational eligible displaced groups and family-member categories;
- the 5 August 2026 military-obligation rule;
- the corrected continuous-protection exception.

Disposition:

**PARTIAL.**

The corrigendum source is appropriate for the corrected military-rule exception but is not the full evidentiary basis for the original eligible-category proposition.

Core already carries the appropriate foundational eligibility source:

- `21a3a70f-2515-5c72-97a8-4ca8cd80e064`

Minimal repair:

Bind the `protection-scheme` Compare fact to the complete existing evidence set, at minimum:

- `21a3a70f-2515-5c72-97a8-4ca8cd80e064` — foundational eligibility;
- `dde016b4-2f2d-5322-9d21-0269318f8456` — corrected military-rule exception.

`9e67a296-0fac-5005-98b9-11e079bd56e5` may also be retained where useful for the consolidated 2026 rule.

No user-facing wording change is required.

Confidence: **HIGH**

## Translation parity

All 26 material Core proposition groups were checked across EN / UK-UA / RU production.

`MATERIAL_PROPOSITION_GROUPS=26`

`LOCALIZED_INSTANCES=78`

`SEMANTIC_DIVERGENCES=0`

No material production-language divergence affecting eligibility, rights, amount, date, duration, authority, obligation, exception or procedure was found.

## Compare audit

`COMPARE_AUDITED=24/24`

All 24 Italy Compare facts are present, Approved and available.

No wrong owner, missing source ID, duplicated source binding or material Core/Compare factual contradiction was found.

`CORE_COMPARE_MATERIAL_CONTRADICTIONS=0`

The two P2 findings are evidence-scope deficiencies, not contradictions between Core and Compare values.

## Current law vs future / historical rules

Current production correctly distinguishes:

- current EU protection through 4 March 2028;
- current Italian operational TP-permit horizon through 4 March 2027;
- the expired 4 March 2026 renewal deadline from the continuing work-conversion provision;
- closed historical subsistence support;
- current 2026 healthcare and benefit rules;
- no enacted general Italian post-TP route after 4 March 2028.

No proposal or future rule was found being presented as already effective.

## Watch result

The current global Watch index contains **0 Italy rows**.

Task 214 does not modify the global Watch index, but records these candidates for a later maintenance task.

### IT-W-001

Trigger: **2026-12-31**

Type: `EXPIRY / REVIEW_ON`

Affected topic:

- healthcare / SSN compulsory enrollment;
- X22 exemption.

Action:

Recheck Ministry of Health and relevant national/regional implementing guidance before or at 31 December 2026 for continuation into 2027.

### IT-W-002

Trigger: **2027-03-04**

Type: `EXPIRY / REVIEW_ON`

Affected topics:

- practical Italian TP permit renewal horizon;
- specified Ukraine-related CAS/SAI reception continuation.

Action:

Recheck Interior/Polizia/Civil Protection measures for permit renewal/reissue and reception continuation beyond 4 March 2027.

### IT-W-003

Trigger: **2028-03-04**

Type: `EXPIRY / REVIEW_ON`

Affected topics:

- EU temporary-protection endpoint;
- Italian post-TP residence/transition rules.

Action:

Recheck EUR-Lex and Italian implementing legislation/guidance before the EU temporary-protection endpoint.

## Final disposition

No P0 or P1 public-launch blocker was found.

Residual severity:

**P0 0 / P1 0 / P2 2**

Unresolved high-risk candidates:

**0**

Final status:

**`P2_ONLY`**

A narrow follow-up repair should correct the two temporary-protection source-binding defects without reopening the full Italy audit.
