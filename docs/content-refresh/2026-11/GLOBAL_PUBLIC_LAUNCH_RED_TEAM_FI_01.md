# Finland Public-Launch Red-Team Audit

- Audit date: 21 November 2026
- Audit completion: Task 216R
- Base SHA: `12954ab084a6bb7f9b57e5f2054e5b7e4608295f`
- Country: Finland (`finland`)
- Supersedes: incomplete placeholder Task 216 committed at `12954ab084a6bb7f9b57e5f2054e5b7e4608295f`
- Production changed: **No**
- Final status: **CLEAN**

## Executive result

Task 216's placeholder finding `FI-P2-001` was an audit-process limitation ("live primary-source inspection was not completed"), not a defect in Finland production content. Task 216R independently rechecked current primary sources and removes that placeholder finding.

The current Finland production set is launch-clear on the audited scope: **0 P0, 0 P1, 0 P2**. The remaining 2027/2028 uncertainty is already represented as qualified user-facing copy, not hidden as a categorical promise.

## Inventory

### Articles — 8

| Code | InformationEntryId |
|---|---|
| FI-TP-01 | `ffd32dca-56d8-5dcf-b995-7522a159010a` |
| FI-TP-02 | `344c5e31-45a9-5f22-8d4a-4a58d8fe8243` |
| FI-EMP-01 | `53743a68-bacb-5344-9aee-559e146cab54` |
| FI-EMP-02 | `0abffa8c-cd42-571e-97f3-e160afad880c` |
| FI-HEALTH-01 | `ea41d1e7-f921-50af-9886-2232d9fb06ef` |
| FI-HOUSING-01 | `61b331e0-76bb-58f4-91e7-e5944ba77e31` |
| FI-EDU-01 | `2a2094da-e321-50bd-8a45-bf3988be06c9` |
| FI-SOC-01 | `51691974-1c20-5b25-9572-66624984a52f` |

Translations: **24/24** (EN/UK/RU for all eight articles), all production translations Verified.

### Sources

- Finland-owned OfficialSource records: **101**
- Unique Finland source IDs: **101**
- Distinct Finland source URLs: **91**
- Expected owner counts: TP-01 10; TP-02 14; EMP-01 10; EMP-02 10; HEALTH-01 10; HOUSING-01 14; EDU-01 16; SOC-01 17.
- Global OfficialSource count at base: 1559.

### Compare

- Finland facts: **24**
- Localized Compare values: **72/72**
- Production status: 24 Approved / 24 available
- Compare source references: 102
- Unique Compare source IDs: 87
- Cross-owner Compare bindings: 0

## Methodology

The audit used the production-linked Finland drafts and integration tests to enumerate the exact production propositions and source IDs, then independently checked current official/public-authority material as of 21 November 2026. Repository review artifacts were used only for inventory/history, not as current evidence.

For high-risk propositions the live check prioritized Migri, Finlex, EUR-Lex, Kela, Finnish National Agency for Education, Job Market Finland, Work in Finland, EURES, Suomi.fi/116117 and current statutory text. Search snippets alone were not treated as sufficient where the underlying official page or statute was available.

## Primary authorities checked live

Material checks included, among others:

- Migri, Temporary protection: https://migri.fi/en/temporary-protection
- Migri, Extension of temporary protection: https://migri.fi/en/extension-of-temporary-protection
- Migri, temporary-protection Information Bank: https://migri.fi/en/information-bank/temporary-protection
- Migri, residence permit card for temporary protection: https://migri.fi/en/residence-permit-card-for-temporary-protection
- Migri, Work and studies: https://migri.fi/work-and-studies
- Migri, For employers: https://migri.fi/en/for-employers1
- Migri, Employer announcements: https://migri.fi/en/employer-s-announcements-on-employment-relationships
- Migri, Other residence permits: https://migri.fi/en/what-type-of-permit-do-you-need
- Migri, First residence permit: https://migri.fi/en/first-residence-permit
- Migri, Extended permit: https://migri.fi/en/extended-permit
- Migri, Permanent/P-EU guidance: https://migri.fi/permanent-residence-permits and https://migri.fi/p-eu-residence-permit
- Migri, Duration and reduction of reception services: https://migri.fi/en/duration-and-reduction-of-reception-services
- Migri, Reception allowance: https://migri.fi/en/reception-allowance1
- Finlex, Government Decision 751/2026: https://www.finlex.fi/fi/lainsaadanto/saadoskokoelma/2026/751
- EUR-Lex, Council Implementing Decision (EU) 2026/1912 consolidated with corrigendum: https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02026D1912-20260804
- Finlex, Employment Contracts Act 55/2001
- Job Market Finland current registration/employment-service material
- EURES, current Finland living/working conditions
- Kela, Ukraine benefits: https://www.kela.fi/how-the-war-in-ukraine-affects-the-benefits-available-from-kela
- Kela, valid-residence-permit rule from 1 April 2026
- Kela, general social security benefit guidance
- Kela, child home care allowance
- EDUFI, Ukraine/basic-education guidance
- EDUFI, 1 August 2026 curriculum/preparatory-teaching change
- Finlex, Universities Act amendment 658/2024 and UAS Act amendment 659/2024
- 116117.fi and Suomi.fi emergency-service guidance.

## Live-source conclusions

### Temporary protection and dates

The EU decision extends the temporary-protection scheme to **4 March 2028**. Its corrected Article 2 applies the military-obligation condition from **5 August 2026**, with the corrected continuity exception for people who already enjoyed protection in the same Member State on or before **4 August 2026** and continuously retained it there.

Migri's current public guidance also applies the authorisation-to-leave/military-obligation condition from 5 August 2026 and states that permits granted before that date are unaffected. Finland's Government Decision 751/2026 was given and took effect on **13 August 2026**. Production correctly reports this national/EU timing boundary as unresolved rather than inventing a reconciliation.

Current Migri guidance genuinely contains two layers of wording:
- central/extension/card guidance says current Finnish TP permits/cards are valid through **4 March 2027** and further 2028 rollout instructions will follow;
- the Information Bank says Migri will automatically extend the permits to the EU 2028 horizon.

Production explicitly reports this conflict and does **not** claim that an individual permit/card is already proven valid through 2028. Old TP cards with printed 2023–2026 dates remain valid **in Finland** through 4 March 2027 only while the underlying permit remains valid; travel requires appropriate travel documents. This qualification is materially correct.

### Residence routes

Current Migri guidance confirms that TP is a temporary B-type basis; a holder who wants to remain on another basis may apply for an ordinary qualifying route such as work, family or study. Since 12 June 2026 only one valid residence permit/right can remain: a new other permit causes the TP permit and TP reception basis to expire. Current first/extended-permit guidance supports the production distinction between ordinary first-permit filing and an in-Finland extended-permit route. Permanent-residence and P-EU guidance confirm that qualifying residence periods depend on A/P-type residence; TP-only B residence is not treated as qualifying A-permit residence.

### Employment

Job Market Finland/EURES current material confirms the 2025 institutional transition from TE Offices to municipal employment areas and keeps Job Market Finland as a current channel. Registration as a jobseeker, the labour-policy statement and the Kela/fund payment decision remain separate steps.

Migri confirms that TP work/study rights start when police/Border Guard registers the application and that the pending-application certificate can evidence that work right. Current employer guidance confirms work-right verification and employee-announcement obligations. The separate early-ending notification applies to work-based residence permits and explicitly does **not** apply where the employee's permit is based on temporary protection. Occupational-health obligations remain distinct from broader voluntary/contractual medical care.

Finlex Employment Contracts Act §2:10 confirms the production pay rule: where no binding/applicable collective agreement and no agreed remuneration applies, usual and reasonable remuneration must be paid. No universal statutory national minimum wage is asserted.

### Healthcare

Current Migri guidance distinguishes:
- adults waiting for a TP decision: urgent and essential care;
- minors: healthcare on the same grounds as children with a municipality of residence;
- granted TP holders: access described at the level of permanent residents, with the reception centre responsible while reception services remain.

Current Kela/reception guidance supports the separate post-reception/municipality pathways and does not make a Kela card or personal identity code a universal proof document. Current 116117 information supports the urgent non-life-threatening advice line and its Åland exception; 112 remains the emergency number.

### Reception, housing and subsistence

Migri's current reception guidance confirms that from **1 September 2026** ordinary TP reception services end three months after the person gains the right to apply for `kotikunta`, not three months after actual registration. Unaccompanied minors are outside that ordinary time limit, and continuation is possible only for exceptionally serious grounds.

Migri's current 2026 reception-allowance table supports the production household/meal categories. Current 2026 law/guidance also confirms that income/resources are assessed; production does not repeat the stale €150 earned-income threshold that remains visible on some older FAQ material.

Kela's current Ukraine guidance keeps housing allowance, rental-deposit/basic assistance and child/family benefits as separate applications and decisions. TP or `kotikunta` alone does not guarantee payment.

### Education

EDUFI current guidance confirms that a child can be treated as living in a municipality for basic/pre-primary education even without `kotikunta`; TP children without a municipality of residence have access but are not automatically within Finnish compulsory education. The 1 August 2026 EDUFI change allows providers to offer supplementary preparatory teaching after preparatory education, based on teacher assessment, for up to one year; provision is optional.

Current Universities/UAS statutory amendments support the production separation between admission, tuition fees and the application fee, including the temporary-protection permit exemptions and timing conditions. A pending TP application is not treated as a granted permit.

### Kela/social security

Kela confirms that from **1 April 2026** relevant non-EU/EEA/Swiss nationals require a valid residence permit for residence-based benefits, subject to the stated pending-extended-permit transition. Kela's Ukraine page confirms the one-year/permanent-residence framework, the child-benefit route for children under 17 living in Finland even without `kotikunta`, and the separate social-assistance application once reception coverage ends/home-municipality conditions are met.

The general social security benefit (`yleistuki`) replaced Kela's prior basic unemployment routes from **1 May 2026**. Current Kela guidance says TP/B-permit holders can receive it for up to 400 payment days when the work requirement is met. The proposed wider TP entitlement from **1 April 2027** is still presented by Kela as a Government proposal and is correctly marked future-only in production.

Kela now states that Parliament has approved the child-home-care changes taking effect **1 January 2027**: a three-year Finland/EU/EEA/Switzerland residence condition and self-care requirement, with transition protection for already-started periods. Production's statement that the three-year rule starts in 2027, not 2026, remains materially correct.

## Compare audit — 24/24

| Aspect | Result | Live evidence / red-team result |
|---|---|---|
| protection-scheme | PASS | Current Migri + Decision 751/2026 + EU decision support the cohorts and qualified military boundary. |
| protection-application | PASS | Police/Border Guard registration and Migri decision remain current; no invented initial online route. |
| protection-dates | PASS WITH QUALIFICATION | EU 2028 vs current Finnish 2027 permit/card wording conflict is real and is explicitly disclosed. |
| residence-routes | PASS | Ordinary job/family/study routes are separate; no automatic conversion is promised. |
| residence-conditions | PASS | Route-specific A/B/permanent/P-EU conditions remain qualified. |
| residence-filing | PASS | First/extended filing and pending-right distinctions remain accurate. |
| employment-vacancies | PASS | Job Market Finland, Work in Finland and EURES remain current official/public channels. |
| employment-institutions | PASS | Municipal employment areas are current; TE Offices are not presented as current. |
| employment-assistance-conditions | PASS | Registration, service assessment and benefit decision remain distinct. |
| work-permission | PASS | Work right begins at TP application registration; card is not a prerequisite. |
| work-employer-duties | PASS | Verification/notification/occupational-health distinctions remain current. |
| work-minimum-pay | PASS | TES rules and Employment Contracts Act usual/reasonable fallback are correctly qualified. |
| healthcare-access | PASS | Pending/granted/minor/reception stages remain distinct. |
| healthcare-proof | PASS | No single identity/Kela document is represented as universal proof. |
| healthcare-urgent | PASS | 112/116117 distinction and Åland qualification remain current. |
| housing-initial | PASS | Reception accommodation and post-1-Sep transition are correctly conditional. |
| housing-private-rental | PASS | Private contracting/deposit/lease rules remain qualified. |
| housing-financial-support | PASS | Kela housing/deposit support is a separate eligibility/decision route. |
| education-school-enrolment | PASS | Access without `kotikunta` and compulsory-education distinction are correct. |
| education-pupil-language | PASS | Optional supplementary preparatory teaching from 1 Aug 2026 is correctly framed. |
| education-higher-admission | PASS | Institution-led admission/recognition and separate fee exemptions remain accurate. |
| social-subsistence | PASS | Reception allowance, Kela basic and county support are correctly separated. |
| social-application | PASS | Application routes and 1-Apr-2026 valid-permit rule remain current. |
| social-child-benefits | PASS | One-year/under-17/lives-in-Finland route is qualified; no automatic award is promised. |

**COMPARE_AUDITED=24/24**

## Material high-risk coverage

The audit enumerated and independently checked **44/44** material high-risk proposition groups:

1. TP applicant cohorts.
2. 5-Aug-2026 military/authorisation condition.
3. 4-Aug-2026 continuity exception.
4. Finland Decision 751/2026 national timing.
5. In-person police/Border Guard application.
6. Registration versus Migri decision.
7. TP work/study right start.
8. Pending certificate versus permit card.
9. Personal identity code issuance/limitations.
10. Separate `kotikunta` eligibility/application.
11. EU TP horizon to 4-Mar-2028.
12. Current Finnish permit validity to 4-Mar-2027.
13. Old-card validity in Finland.
14. 2027/2028 Migri rollout conflict.
15. Travel-document/card limitation.
16. Ordinary alternative residence routes/no automatic conversion.
17. One-valid-permit rule and TP expiry on another permit.
18. Extended versus first-permit filing.
19. Permanent-residence A-permit accrual boundary.
20. P-EU qualifying residence boundary.
21. Current employment-service institution routing.
22. Jobseeker registration versus benefit decision.
23. Jobseeker/contact routing and e-service limits.
24. Employer work-right verification.
25. Employer hiring/employee announcement.
26. Early-ending notice exemption for TP-based permit.
27. Preventive occupational-health obligation.
28. Minimum-pay/TES/usual-and-reasonable fallback.
29. Pending-adult healthcare.
30. Granted-TP healthcare.
31. Minor healthcare.
32. Post-reception/`kotikunta` healthcare routing.
33. Urgent 112/116117 route.
34. Reception accommodation/capacity.
35. Three-month reception transition.
36. Private rental versus housing/deposit support.
37. School access without `kotikunta` and compulsory-education distinction.
38. Supplementary preparatory teaching from August 2026.
39. Higher-education admission/tuition/application-fee distinctions.
40. Reception allowance current rates/means assessment.
41. Kela basic assistance and valid-permit boundary.
42. `yleistuki` current TP work-condition boundary.
43. Child/family benefit eligibility and non-automatic award.
44. Wellbeing-services-county supplementary/preventive/urgent support.

**MATERIAL_HIGH_RISK_AUDITED=44/44**

## Time-sensitive coverage

The audit enumerated and checked **18/18** material time-sensitive propositions/events:

1. 4-Aug-2026 continuity cutoff.
2. 5-Aug-2026 military condition effective date.
3. 13-Aug-2026 Finland Decision 751/2026.
4. EU TP endpoint 4-Mar-2028.
5. Current Finnish TP permit horizon 4-Mar-2027.
6. Old-card Finland validity through 4-Mar-2027.
7. One-valid-permit rule from 12-Jun-2026.
8. 12-Aug-2026 legacy dual-permit expiry case.
9. TE-office transition effective 1-Jan-2025.
10. 2026 reception-allowance means-assessment change.
11. Valid-permit Kela rule from 1-Apr-2026.
12. `yleistuki` replacement from 1-May-2026.
13. Reception transition applied from 1-Sep-2026.
14. Supplementary preparatory teaching from 1-Aug-2026.
15. 2026 reception-allowance amounts.
16. Child-home-care rule effective 1-Jan-2027.
17. Proposed wider TP `yleistuki` eligibility from 1-Apr-2027.
18. 2026 higher-education application/tuition-fee rule timing.

**TIME_SENSITIVE_AUDITED=18/18**

## EN / UK / RU semantic consistency

All eight production articles were compared across EN/UK/RU. Source-token sets are identical per locale for each article; the material dates/numbers and paragraph structure align across the translations. No material divergence was found in eligibility, dates, amounts, obligations, process, caveats or may/must/can meaning.

**SEMANTIC_DIVERGENCES=0**

## Source-binding integrity

Structural and semantic checks found:

- no dangling Finland source IDs;
- no duplicate source IDs;
- no cross-owner Core or Compare source IDs;
- no removed broken/legacy IDs still active;
- removed IDs `a8f043b1-b76c-532c-8d62-16a988efb1cd`, `0d535e37-f80b-59c8-9a35-57038c1d876b`, and `d5bed9b1-7eb7-5223-a1d1-c56b705c712e` remain absent;
- one intentional same-owner/same-URL pair remains for the Employment Contracts Act (`7ab4c5c4-4363-5083-9734-4a16e6d72e74` and `e0b8e4ac-3707-5842-8fc1-63f81bbb2532`), representing separate editorial scopes; it is not duplicated inside one Compare binding;
- `fa6d5f24-dab9-50d5-8c4a-500931079914` remains deliberately active only as qualified evidence of Migri's Information-Bank 2028 wording and separate `kotikunta` guidance. It is not used as sole proof of individual 2028 permit/card validity.

Some official pages themselves contain internally stale material outside the proposition for which they are bound (for example an older FAQ income-threshold sentence). No audited production proposition relies on that stale fragment.

## Findings

No substantive production finding was established.

| Severity | Count |
|---|---:|
| P0 | 0 |
| P1 | 0 |
| P2 | 0 |

The placeholder `FI-P2-001` from incomplete Task 216 is removed; inability to complete an audit is not a content finding.

## New Watch candidates

| ID | Trigger | Scope | Reason |
|---|---|---|---|
| FI-W-001 | 2027-03-04 / earlier official update | TP permit/card rollout | Migri must reconcile/publish operational 2028 permit/card instructions before the current 2027 individual horizon becomes material. |
| FI-W-002 | 2027-01-01 | Child home care allowance | Parliament-approved three-year residence/self-care rule takes effect; recheck final Kela operational wording at effective date. |
| FI-W-003 | 2027-04-01 / enactment | `yleistuki` for TP holders | Current wider eligibility remains a Government proposal; recheck enactment and final Kela guidance. |
| FI-W-004 | 2028-03-04 | EU TP endpoint / post-TP transition | Recheck EU acts and Finnish transition/residence guidance before scheme expiry. |

The former placeholder FI-W-002 with trigger 2026-09-01 is not retained as a future Watch candidate because that date had already passed by the 21-Nov-2026 audit; the reception transition is instead audited as a current rule.

## Structural validation

- Articles: 8
- Translations: 24/24
- Finland-owned sources: 101
- Compare facts: 24/24 Approved and available
- Compare audited: 24/24
- High-risk audited: 44/44
- Time-sensitive audited: 18/18
- Semantic divergences: 0
- P0/P1/P2: 0/0/0
- Unresolved high-risk: 0
- Watch candidates: 4
- Production changes: none

## Final launch assessment

**CLEAN.**

Finland's current user-facing production content is materially consistent with the current primary authorities checked on 21 November 2026. The important unresolved administrative uncertainty — Finland's operational permit/card rollout from the current 4-March-2027 horizon to the EU 4-March-2028 scheme horizon — is already disclosed as uncertainty rather than represented as settled individual validity. No production repair is required from this audit.

Final counters:

```text
COMPARE_AUDITED=24/24
MATERIAL_HIGH_RISK_AUDITED=44/44
TIME_SENSITIVE_AUDITED=18/18
SEMANTIC_DIVERGENCES=0
P0=0
P1=0
P2=0
UNRESOLVED_HIGH_RISK=0
WATCH_CANDIDATES=4
FINAL_STATUS=CLEAN
PRODUCTION_CHANGED=NO
```
