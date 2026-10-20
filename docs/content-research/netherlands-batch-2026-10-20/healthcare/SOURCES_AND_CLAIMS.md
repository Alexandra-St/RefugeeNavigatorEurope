# NL-HEALTH-01 — Sources and Claims

Research check: **20 October 2026 (UTC; exact check time not recorded)**. Research only, not medical advice or a determination of coverage. See the [shared official source inventory](../OFFICIAL_SOURCE_INVENTORY.md).

## Scope

RMO, registration/proof, defined coverage and exclusions, patient cost limits, and urgent-care navigation. Distinguish the reimbursement route used by providers from an individual’s eligibility and clinical access.

## Official sources reviewed

| ID | Authority / title | Exact URL | Date / limitations |
| --- | --- | --- | --- |
| HEALTH-S01 | Rijksoverheid, Medische zorg en hulpmiddelen voor vluchtelingen Oekraïne | https://www.rijksoverheid.nl/themas/familie-zorg-en-gezondheid/gezondheidszorg-vluchtelingen-oekraine/medische-zorg-en-hulpmiddelen-vluchtelingen-oekraine | No update date displayed; RMO package and examples/exclusions. |
| HEALTH-S02 | Rijksoverheid, Als zorgverlener kosten declareren voor zorg aan vluchtelingen uit Oekraïne | https://www.rijksoverheid.nl/themas/familie-zorg-en-gezondheid/gezondheidszorg-vluchtelingen-oekraine/als-zorgverlener-zorgkosten-declareren | Provider-facing; states RMO cohort code and no-BSN route. |
| HEALTH-S03 | CAK, Vragen en antwoorden over zorg verleend aan vluchtelingen uit Oekraïne | https://www.hetcak.nl/zakelijk/regeling-onverzekerden/vragen-antwoorden-zorg-verleend/ | Provider-facing FAQ; update date not displayed. |
| HEALTH-S04 | UWV, Working in the Netherlands as a Ukrainian refugee | https://www.uwv.nl/en/individuals/working-in-the-netherlands/ukrainian-refugee | Notes employed TPD holders may need Dutch health insurance; not a complete insurance advice source. |

## Atomic claims

| Claim ID | Proposed factual statement | Source ID / URL | Evidence location / note | Applicable cohort | Conditions | Exceptions | Effective date | LastCheckedAt | Evidence status | Comparison aspect | Drafting boundary |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| NL-HEALTH01-01 | Provider guidance describes RMO reimbursement for displaced people from Ukraine with a BSN and BRP title code 46, associated with TPD; workers may instead take out Dutch health insurance and pay its premium/cost-sharing. | HEALTH-S02 — https://www.rijksoverheid.nl/themas/familie-zorg-en-gezondheid/gezondheidszorg-vluchtelingen-oekraine/als-zorgverlener-zorgkosten-declareren; HEALTH-S04 — https://www.uwv.nl/en/individuals/working-in-the-netherlands/ukrainian-refugee | RMO eligibility/registration section is addressed to care providers; UWV says working Ukrainians can take out insurance. | People recorded with code 46; separately, employed Ukrainian people choosing private statutory insurance | Code/status and applicable insurance basis matter. | Other status cohorts may use a different route. | Government page says RMO runs at least to March 2028 | 2026-10-20 (time not recorded) | CONDITIONAL | healthcare-access | Do not infer that BSN alone or Ukrainian nationality automatically establishes a patient's coverage. |
| NL-HEALTH01-02 | For providers, Rijksoverheid describes billing the CAK under the uninsured-care subsidy route where a person is not in COV or has no BSN; it separately identifies uninsured-foreigner arrangements for third-country nationals. | HEALTH-S02 — https://www.rijksoverheid.nl/themas/familie-zorg-en-gezondheid/gezondheidszorg-vluchtelingen-oekraine/als-zorgverlener-zorgkosten-declareren; HEALTH-S03 — https://www.hetcak.nl/zakelijk/regeling-onverzekerden/vragen-antwoorden-zorg-verleend/ | Provider billing section; not patient instructions. | Provider treating an unregistered/no-BSN person or separate third-country cohort | The provider reimbursement category depends on registration/status. | Does not itself establish that every provider/service is reimbursed or how a patient should arrange treatment. | As displayed in current pages | 2026-10-20 (time not recorded) | CONDITIONAL | healthcare-proof | Describe as provider-side funding/navigation; do not promise an individual has no costs. |
| NL-HEALTH01-03 | The RMO description covers basic-package care and lists some additional categories, while specific benefits have limits (for example adult acute dental care up to €250, orthodontics excluded for children, and glasses only after eight weeks registered). | HEALTH-S01 — https://www.rijksoverheid.nl/themas/familie-zorg-en-gezondheid/gezondheidszorg-vluchtelingen-oekraine/medische-zorg-en-hulpmiddelen-vluchtelingen-oekraine | Sections on oral care, glasses, contraception, maternity and long-term care. Page points to RMOekraine for current exact terms. | People eligible under RMO | Coverage depends on service-specific criteria, referral, provider arrangements and time registered where stated. | The page does not say all care is free; some categories have exclusions or caps. | Current page; some examples date from scheme terms | 2026-10-20 (time not recorded) | CONDITIONAL | healthcare-access | Do not generalize examples into universal coverage or zero patient cost. Recheck RMO terms before publication. |
| NL-HEALTH01-04 | The reviewed sources do not establish one patient-facing urgent/out-of-hours pathway or a complete emergency-routing rule for all regions and status cohorts. | HEALTH-S01–S04 — URLs listed above | Public pages focus on reimbursement/provider navigation; no complete regional urgent-care route was verified. | People needing urgent or emergency care | Local routes and clinical urgency must be verified separately. | None established by this review. | Not established | 2026-10-20 (time not recorded) | UNRESOLVED | healthcare-urgent | Do not give unverified emergency numbers, costs or out-of-hours instructions as universal rules. |

## Comparison readiness

| Aspect | Verdict | Claim mapping | Reason |
| --- | --- | --- | --- |
| healthcare-access | CONDITIONAL | NL-HEALTH01-01, 03 | Defined RMO route and examples, with registration/service-specific limits. |
| healthcare-proof | CONDITIONAL | NL-HEALTH01-01, 02 | Provider billing categories are not a patient’s individualized coverage proof. |
| healthcare-urgent | UNRESOLVED | NL-HEALTH01-04 | Regional patient-facing urgent-care route not verified. |

## Research gaps / drafting gate

- Read current RMO terms and patient-facing information at RMOekraine.nl, plus current 2026 referral, contracted-provider and cost rules.
- Confirm status transitions, service access for people without BSN, patient contribution rules, and how statutory insurance applies after a status change.
- Verify regional primary-care/out-of-hours and emergency routes from responsible public health authorities before any user-facing instruction.
