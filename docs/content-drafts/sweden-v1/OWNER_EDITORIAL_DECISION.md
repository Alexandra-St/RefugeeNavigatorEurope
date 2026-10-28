# Sweden Owner Editorial Decision

## Decision

- **Decision type:** `OWNER EDITORIAL ACCEPTANCE`
- **Decision authority:** Project owner
- **Acceptance preparation baseline:** `c21101a9b9572bd2d305474691e8d97a34a5deb7`
- **Source readiness:** `READY FOR HUMAN ACCEPTANCE`

The owner accepts all 8 Sweden articles and all 24 EN/UK/RU translations in their current wording, retaining the caveats recorded below and in the [editorial acceptance preparation](EDITORIAL_ACCEPTANCE_PREPARATION.md).

The owner also accepts 23 of the 24 comparison facts. The `social-child-benefits` fact is held because the reviewed evidence does not establish an answer.

## Article decisions

| Article | Article ID | Owner decision | Caveats accepted with the current wording |
| --- | --- | --- | --- |
| Temporary Protection | `d2ab273d-21b7-44e4-b408-f4269a8753d8` | **ACCEPT** | Keep EU scheme dates separate from each Swedish permit’s validity. Filing does not grant a permit or work authorization. The next extension window’s exact dates and booking process, some military-document cases, and some third-country/stateless cases remain unresolved. |
| Residence Options | `befa7777-af70-4269-883c-8918961554dc` | **ACCEPT** | Routes are limited and route-specific; filing does not guarantee a permit or permanent residence. Treatment of every transition cohort and counting prior TPD time for EU long-term resident status remain unresolved. |
| Finding a Job | `a218013b-3a77-4ba2-829a-d724689837f2` | **ACCEPT** | Listings are not job offers. Registration, support, training, compensation, qualification recognition, and placement are conditional and not guaranteed. This item was Ready in the original audit and did not receive a separate focused source refresh on 27–28 October 2026. |
| Work Authorization and Employee Rights | `55949af5-b4fe-404e-bcb7-6ebb37131bab` | **ACCEPT** | Work permission follows a granted qualifying permit and remains subject to its scope. Employer reporting is separate. Sweden has no general statutory national minimum-wage amount; collective-agreement coverage and terms vary. |
| Healthcare | `2234b91f-4021-40b2-b9d5-a029a6b533f2` | **ACCEPT** | Keep routes distinct by registration and age. Care that cannot be postponed is not emergency-only; clinical decisions, fees, and regional delivery vary. Population registration does not establish social-insurance eligibility. Recheck the cohort cutoff if the EU protection endpoint changes. |
| Housing | `e0f307e9-7991-4350-89f4-2f84a9047393` | **ACCEPT** | Reception-accommodation rights depend on statutory coverage. Location, type, and timing are not guaranteed. Rental rules vary by tenancy type; no general rent reimbursement is promised. |
| Education | `cac58c0c-ce69-41ac-877e-4fd124105b89` | **ACCEPT** | Municipal delivery, course availability, placement, and admission are not guaranteed. Keep SFI eligibility separate from TPD status, preserve the 2027 curriculum transition, and retain permit-specific fee and proof conditions. |
| Social Assistance | `8b5cc53a-9933-4366-8421-d795966267ec` | **ACCEPT** | Assess benefits individually; TPD does not create blanket eligibility. Keep the current municipal-assistance exclusion separate from the narrow change effective 2 January 2027. The additional-payment boundary remains unresolved. |

**Article decision count:** 8/8 accepted. All 24 localized translations (EN/UK/RU) are editorially accepted in their current wording.

## Comparison fact decisions

| Category | Aspect / fact ID | Linked article | IsAvailable | Current status | Owner decision |
| --- | --- | --- | --- | --- | --- |
| temporary-protection | `protection-scheme` | Temporary Protection | true | Draft | **ACCEPT** |
| temporary-protection | `protection-application` | Temporary Protection | true | Draft | **ACCEPT** |
| temporary-protection | `protection-dates` | Temporary Protection | true | Draft | **ACCEPT** |
| temporary-protection | `residence-routes` | Residence Options | true | Draft | **ACCEPT** |
| temporary-protection | `residence-conditions` | Residence Options | true | Draft | **ACCEPT** |
| temporary-protection | `residence-filing` | Residence Options | true | Draft | **ACCEPT** |
| employment | `employment-vacancies` | Finding a Job | true | Draft | **ACCEPT** |
| employment | `employment-institutions` | Finding a Job | true | Draft | **ACCEPT** |
| employment | `employment-assistance-conditions` | Finding a Job | true | Draft | **ACCEPT** |
| employment | `work-permission` | Work Authorization and Employee Rights | true | Draft | **ACCEPT** |
| employment | `work-employer-duties` | Work Authorization and Employee Rights | true | Draft | **ACCEPT** |
| employment | `work-minimum-pay` | Work Authorization and Employee Rights | true | Draft | **ACCEPT** |
| healthcare | `healthcare-access` | Healthcare | true | Draft | **ACCEPT** |
| healthcare | `healthcare-proof` | Healthcare | true | Draft | **ACCEPT** |
| healthcare | `healthcare-urgent` | Healthcare | true | Draft | **ACCEPT** |
| housing | `housing-initial` | Housing | true | Draft | **ACCEPT** |
| housing | `housing-private-rental` | Housing | true | Draft | **ACCEPT** |
| housing | `housing-financial-support` | Housing | true | Draft | **ACCEPT** |
| education | `education-school-enrolment` | Education | true | Draft | **ACCEPT** |
| education | `education-pupil-language` | Education | true | Draft | **ACCEPT** |
| education | `education-higher-admission` | Education | true | Draft | **ACCEPT** |
| social-assistance | `social-subsistence` | Social Assistance | true | Draft | **ACCEPT** |
| social-assistance | `social-application` | Social Assistance | true | Draft | **ACCEPT** |
| social-assistance | `social-child-benefits` | Social Assistance | false | Draft | **HOLD — EVIDENCE GAP** |

`social-child-benefits` remains held because `IsAvailable=false` and `OfficialSourceIds=[]`. The answer was not established in the reviewed sources. This hold is **not** a claim that the benefit does not exist.

**Comparison fact decision count:** 23/24 accepted; 1/24 held.

## Scope and limitations

This editorial acceptance confirms the owner’s acceptance of the current wording and its documented limits. It is not individual legal advice, a guarantee of eligibility or outcome, a resolution of the listed uncertainties, or authorization to deploy.

## No promotion in this phase

No catalogs, article text, facts, source records, or visibility rules are changed by this decision record. Until a separate status-promotion phase, the catalog remains:

- 24 translations: `NeedsReview`;
- 24 comparison facts: `Draft`;
- Sweden: hidden in Production.

## Promotion authorization boundary

This explicit owner acceptance authorizes a **separate future promotion phase** to prepare the following status changes:

- 24 accepted translations: `NeedsReview → Verified`;
- only the 23 accepted, available comparison facts: `Draft → Approved`.

That later phase must leave `social-child-benefits` unchanged as `Draft`, `IsAvailable=false`, with no source IDs. No status promotion is performed or implied by this document or its commit.
