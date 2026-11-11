# France v1 — Editorial review

**Review date:** 11 November 2026
**Baseline:** `4aa31607b9f5365b045a1e0276aef0e161fde010`
**Scope:** Six France v1 drafts, their six corresponding research packages, and the France research-readiness audit. The Ireland review was used only as a structural reference. No external research was used.

## Recommendation

**READY FOR CORRECTION PASS.** Four articles need bounded corrections that can be made from the existing research packages. No article-level blocker was identified. After those corrections and a final language review, all six are suitable for later Development-only catalog integration. This verdict does not make France ready for Compare: `residence-filing` and `housing-initial` remain blocked.

## Article review

| Draft | Review | Source UUIDs | Coverage and editorial boundaries |
|---|---:|---:|---|
| `FR-TP-01.md` | **CORRECTION** | 7 | Correctly separates the six-month APS from the EU framework date, retains the military-rule exception and unresolved proof format, and limits local procedure guidance. Clarify the eligible non-Ukrainian cohorts and listed family members; the current shorthand could be read too broadly. |
| `FR-EMP-01.md` | **PASS** | 12 | Vacancy search, EURES limitations, conditional France Travail support and training, registration/ARE distinction, freshness caveat and regulated-profession boundary are grounded and appropriately qualified. No employment residence-transition advice is introduced. |
| `FR-EMP-02.md` | **PASS** | 12 | Work permission is tied to a valid APS-BPT; employer verification is separate from DPAE; contract and payslip wording is bounded. The metropolitan SMIC amount, effective date, gross basis and territory/age/training/collective-minimum qualifications are consistent across languages. Thirty-five hours is explicitly not an absolute maximum. |
| `FR-HEALTH-01.md` | **CORRECTION** | 12 | Correctly limits immediate PUMa/CSS and no-advance treatment to the researched Ukraine cohort and 12-month period; separates APS, CPAM rights, NIA/NIR and Carte Vitale; urgent access is not presented as free. Add practical clarification on using the attestation while waiting for the card. |
| `FR-EDU-01.md` | **CORRECTION** | 13 | School access, local enrolment, needs-based language support, institutional admission, 2026/27 fee qualification and conditional CROUS support are bounded. UK/RU narrow the 16–18 training obligation; the higher-education section also omits useful route and qualification-assessment boundaries documented in research. |
| `FR-SOC-01.md` | **CORRECTION** | 20 | ADA, RSA/Prime/ARE, CAF identifier, family-benefit conditions and local emergency-help limits are generally well qualified. Add the effect/reporting of work and income on ADA, and support the ARS exclusion with the national Cnaf guidance as well as the local CAF page. |

## Required corrections

### 1. FR-TP-01 — specify the eligible cohorts

“Specified non-Ukrainian residents or protection holders from Ukraine” is too compressed to make the eligibility boundary clear. The research package identifies non-Ukrainians granted international or equivalent national protection in Ukraine, and non-Ukrainians with valid permanent residence in Ukraine who cannot return safely and durably. It also names the family categories: spouse, unmarried minor children and dependent parents.

**Correction:** state those bounded groups (or an equally precise concise summary) in EN, UK and RU. Preserve the qualification that the French Ministry’s central cohort page dates from 2022 and should be read with the later EU condition. Do not expand family or other third-country categories.

### 2. FR-HEALTH-01 — care while waiting for a Carte Vitale

The draft correctly says the attestation de droits records Assurance Maladie rights and that the Carte Vitale is administrative, not the source of entitlement. It does not say what proof to use while waiting for the card.

**Correction:** add that a person whose rights are open can present the attestation de droits while waiting for the Carte Vitale, in all three languages. Keep the Ukraine-specific no-advance arrangement limited to its stated cohort and 12-month scope; do not imply that all TP holders receive free care.

### 3. FR-EDU-01 — correct the 16–18 translation and complete the bounded higher-education guidance

The EN “obligation to train” is broader than UK “обов’язок здобувати професійну підготовку” and RU “обязанность проходить профессиональное обучение,” which can sound like vocational training only. The research describes a separate 16–18 training obligation with multiple routes.

The higher-education paragraph says the route depends on programme and applicant but does not distinguish Parcoursup from DAP routes, explain that French-language evidence may be required on some routes, or include the TP-specific ENIC-NARIC priority/free handling and incomplete-document flexibility. It also omits the separate CVEC charge and previous-study assessment by the receiving institution.

**Correction:** use equivalent broad “training/education” wording in UK and RU. Add a concise, qualified explanation that some first-cycle and foreign-diploma applications use Parcoursup or DAP while the 2026 Ukraine guidance directs applicants to the chosen institution; language requirements depend on the route. Explain that ENIC-NARIC comparability is advisory, may have the researched TP-specific priority/free and incomplete-file handling, and does not decide admission or credit transfer; the institution assesses prior study. Keep tuition, CVEC and CROUS as separate decisions/charges, and retain that TP is not verified as automatic domestic-fee classification for 2026/27.

### 4. FR-SOC-01 — qualify ADA work effects and cite national ARS guidance

The ADA paragraph mentions household resources but omits that work and income should be reported to OFII and may reduce ADA or affect continued eligibility; the research does not support saying every job automatically ends ADA. In the child-benefits paragraph, the ARS exclusion is cited only to the Yvelines CAF page, although the package also records national Cnaf TP guidance.

**Correction:** state in EN/UK/RU that work and income changes should be reported to OFII and can affect the individually assessed amount/eligibility, without an automatic-termination claim. Add the relevant national Cnaf source citation for the APS-BPT ARS exclusion alongside the local Yvelines citation. Keep child-entry derogations benefit-specific and all family benefits conditional.

## Metadata, source ownership and citation audit

- All six drafts have `Country: France`, the expected category, the specified Article ID, the exact research-package path, `NeedsReview` for EN/UK/RU, the expected comparison aspects, EN/UK/RU sections, claim-coverage sections and an editorial source set.
- All six Article IDs match the requested UUIDv5 values.
- Source entries: FR-TP-01 **7**; FR-EMP-01 **12**; FR-EMP-02 **12**; FR-HEALTH-01 **12**; FR-EDU-01 **13**; FR-SOC-01 **20**; **76 total**.
- All 76 source UUIDs were mathematically reproduced as UUIDv5 using the owning Article ID as namespace and the exact local editorial source key as name. The local keys are contiguous and use the owning article prefix.
- **76/76 source UUIDs are unique.** No editorial source UUID is shared across articles.
- Every inline citation maps to exactly one source-set entry; there are no unused entries. Every research source ID exists in the owning package, and each editorial URL matches that package’s URL for the source. No source crosses into an article owned by another research package.
- EN/UK/RU use the same citation/source coverage in every article. No numerical/date mismatch was found in SMIC, TP duration, school ages or healthcare scope. The required correction list records the identified translation narrowing in FR-EDU-01.

## Translation parity audit

The three language versions otherwise preserve the same article scope, figures, dates, conditions, exclusions and source coverage. In particular, the 2026 military-obligation start date and continuous same-Member-State exception, APS and EU dates, SMIC amount/effective date/gross basis, Ukraine-only healthcare scope, and conditional benefit/admission wording align. FR-EDU-01’s UK/RU phrasing of the 16–18 obligation is narrower than EN and must be corrected. The four correction items above should be applied symmetrically in all three languages where applicable.

## Cross-article and blocked-area audit

- APS-BPT naming and work authorization are consistent: temporary-protection work permission is tied to the relevant valid APS; the employment-rights draft does not extend this to an expired/pending APS or ordinary residence status.
- FR-EMP-01 and FR-SOC-01 both distinguish France Travail registration from an ARE award.
- FR-HEALTH-01 distinguishes health-insurance identifiers and rights from APS; FR-SOC-01 treats a CAF temporary identifier as account access, not entitlement. These identifiers are not conflated.
- FR-EDU-01 keeps CROUS support separate from CAF family benefits.
- No draft asserts a nationwide TP-to-ordinary-residence filing route, automatic bridging of stay/work after APS expiry, or automatic post-TP conversion. `residence-filing` leakage: **none found**.
- No draft asserts a national TP accommodation intake route, guaranteed State placement, a guaranteed 115 bed or a current nationwide Ukraine hosting programme. `housing-initial` leakage: **none found**.

## Freshness checks before publication

Refresh these time-sensitive points against the applicable official sources before publication; this review did not refresh them:

- EU TP end date and military-obligation condition, APS validity and renewal wording.
- SMIC amount and effective date, including territorial and special-contract qualifications.
- France Travail Ukraine-specific information and contact form availability.
- Ukraine-specific PUMa/CSS/no-advance arrangement, and current FPU and hospital charges/exemptions.
- School/university application routes, 2026/27 differentiated fees, CVEC and CROUS rules.
- ADA rate/transition treatment and current initial TP application procedure; CAF/Cnaf TP child-benefit and ARS guidance.

## Final disposition

- **PASS:** FR-EMP-01, FR-EMP-02.
- **CORRECTION:** FR-TP-01, FR-HEALTH-01, FR-EDU-01, FR-SOC-01.
- **BLOCKER:** none at article level; the identified corrections are supported by the scoped research packages.
- After the correction pass and final editorial/translation review, all six drafts are suitable for later **Development-only** catalog integration. No Production or Compare readiness is implied.
- France remains excluded from Compare while `residence-filing` and `housing-initial` are blocked.
