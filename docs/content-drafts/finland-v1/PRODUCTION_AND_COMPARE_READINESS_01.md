# Finland Production and Compare Readiness 01

**Assessment date:** 15 November 2026

**Scope:** Existing Finland drafts, their review and research packages, catalog/source records, and the current Compare schema. No external research was conducted and no production data was changed.

## Current Core state

- **Country:** Finland; ID `10000000-0000-0000-0000-000000000016`.
- **Articles:** 8/8 expected owners exist, with the expected categories and Article IDs.
- **Translations:** 24/24 EN/UK/RU records exist. All are `NeedsReview`, dated `2026-11-11`; none is Verified.
- **Sources:** 100/100 article-owned records, 100 unique source IDs and 90 unique HTTPS URLs. Counts by owner are shown below.
- **Visibility:** Production hidden; Development visible. Current Finland Compare facts: 0.
- **Current Compare:** 17 countries, 8 topics, 24 aspects and 408 facts. Finland is absent from the supported country map and from the facts.
- **Parity:** All 24 Title, Summary and Content fields match the eight drafts exactly. All 100 draft source entries match their catalog ID, owner, title and URL. All 318 inline citation tokens resolve to a source owned by the same article.

## Article review matrix

The post-correction review reports all eight drafts PASS, with no remaining correction finding. That is draft-level editorial clearance, not Production verification: all 24 catalog translations remain `NeedsReview`. The blocked individual permit/card-date issue is explicitly bounded in FI-TP-01 and must remain qualified.

| Article | Languages | Draft/catalog parity | Source records | Content status | Notes |
| --- | --- | --- | ---: | --- | --- |
| FI-TP-01 | EN/UK/RU | PASS | 10 | PASS | Individual permit/card validity and the military-condition date/proof details remain expressly qualified. |
| FI-TP-02 | EN/UK/RU | PASS | 15 | PASS | Ordinary routes remain conditional; no automatic conversion or universal pending rights. |
| FI-EMP-01 | EN/UK/RU | PASS | 11 | PASS | Current municipal service routing and assistance conditions remain local/individual. |
| FI-EMP-02 | EN/UK/RU | PASS | 9 | PASS | Work-right start, employer duties and collective-agreement pay boundaries remain distinct. |
| FI-HEALTH-01 | EN/UK/RU | PASS | 10 | PASS | Pending/granted status and post-reception practical handoff remain distinct. |
| FI-HOUSING-01 | EN/UK/RU | PASS | 14 | PASS | Capacity, local emergency housing and benefit eligibility are not guarantees. |
| FI-EDU-01 | EN/UK/RU | PASS | 15 | PASS | School access is separate from compulsory duty; local support and HEI decisions are qualified. |
| FI-SOC-01 | EN/UK/RU | PASS | 16 | PASS | Reception allowance, Kela support, unemployment and family benefits are separated. |
| **Total** | **24 translations** | **PASS** | **100** | **8 PASS** | **Production promotion still requires final Core acceptance and `Verified` status.** |

## Compare owner matrix

| Compare topic | Owner article | Article ID | Existing owner sources |
| --- | --- | --- | ---: |
| Temporary Protection | FI-TP-01 | `ffd32dca-56d8-5dcf-b995-7522a159010a` | 10 |
| Residence After Protection | FI-TP-02 | `344c5e31-45a9-5f22-8d4a-4a58d8fe8243` | 15 |
| Finding Employment | FI-EMP-01 | `53743a68-bacb-5344-9aee-559e146cab54` | 11 |
| Work Authorization and Rights | FI-EMP-02 | `0abffa8c-cd42-571e-97f3-e160afad880c` | 9 |
| Healthcare | FI-HEALTH-01 | `ea41d1e7-f921-50af-9886-2232d9fb06ef` | 10 |
| Housing | FI-HOUSING-01 | `61b331e0-76bb-58f4-91e7-e5944ba77e31` | 14 |
| Education and Language | FI-EDU-01 | `2a2094da-e321-50bd-8a45-bf3988be06c9` | 15 |
| Social Assistance | FI-SOC-01 | `51691974-1c20-5b25-9572-66624984a52f` | 16 |

All eight owner IDs exist and belong to Finland. The owner-specific production source keys below are the subset cross-referenced by the existing aspect research/readiness evidence; their records and citations are already present in the owning Core article. `FI-…-Sxx` refers to the production source key in that draft’s source set.

## 24-aspect matrix

| Aspect | Owner | Research package | Safe assertion for a future fact | Qualification to retain | Usable production source keys | Status |
| --- | --- | --- | --- | --- | --- | --- |
| `protection-scheme` | FI-TP-01 | Temporary Protection | Finland applies its bounded Migri eligibility cohorts; eligibility is assessed on individual circumstances. | Keep the separate Finnish cohort limits and military-condition dates (5 Aug EU/Migri; 13 Aug Finnish decision) and the missing nationwide proof checklist. | FI-TP-01-S01, S02, S05–S07 | READY WITH QUALIFICATION |
| `protection-application` | FI-TP-01 | Temporary Protection | Apply in person through police or Border Guard registration; Migri decides. | Local intake/appointment rules vary; no initial online Enter Finland route or exhaustive document checklist is established. | FI-TP-01-S01, S03–S04 | READY WITH QUALIFICATION |
| `protection-dates` | FI-TP-01 | Temporary Protection | The EU temporary-protection scheme endpoint is 4 March 2028; this alone does not establish an individual Finnish permit or card expiry. | Migri materials conflict on 2027/2028 permit/card validity and holder action. Do not claim universal extension, card validity or no action. Obtain current authoritative clarification before using this as a full comparison fact. | FI-TP-01-S01–S02, S07–S10 | RESEARCH REQUIRED |
| `residence-routes` | FI-TP-02 | Residence Options | A current TP holder may seek an ordinary extended permit on another qualifying basis such as work, study or family. | Conditions are route-specific; admission/job/family relationship alone does not grant a permit. No automatic conversion, settled Blue Card interaction or announced post-2028 national route. | FI-TP-02-S01, S07–S09, S15 | READY WITH QUALIFICATION |
| `residence-conditions` | FI-TP-02 | Residence Options | Each ordinary residence route has its own statutory and evidentiary conditions. | Date-stamp 2026 thresholds; TP time does not count where the route requires qualifying continuous A-permit time. Blue Card interaction remains unresolved. | FI-TP-02-S07–S09, S13–S15 | READY WITH QUALIFICATION |
| `residence-filing` | FI-TP-02 | Residence Options | First permits and extended permits have different filing rules; only documented exceptions support in-country first applications. | Do not imply universal in-country filing, automatic pending rights after TP expiry, or that extended-permit timing rules apply to every first permit. | FI-TP-02-S02–S06, S09 | READY WITH QUALIFICATION |
| `employment-vacancies` | FI-EMP-01 | Finding a Job | Job Market Finland, Work in Finland and EURES are official vacancy channels. | Listings are not exhaustive; language and application route depend on each vacancy. | FI-EMP-01-S04, S07–S09 | READY WITH QUALIFICATION |
| `employment-institutions` | FI-EMP-01 | Finding a Job | TE Offices closed on 1 Jan 2025; municipal employment areas provide current local services, while Job Market Finland and KEHA have distinct national roles. | Responsibility for a person without `kotikunta` is not universal; direct them to the local authority for their address. | FI-EMP-01-S01–S03 | READY |
| `employment-assistance-conditions` | FI-EMP-01 | Finding a Job | Employment assistance is assessed through the responsible employment authority; contact is possible without e-ID through the described route. | Job-search service access is not eligibility for every programme or payment. Pending TP work rights alone do not establish jobseeker-registration or benefit eligibility; local service/interpreter availability is not guaranteed. | FI-EMP-01-S02–S03, S05–S06, S10–S11 | READY WITH QUALIFICATION |
| `work-permission` | FI-EMP-02 | Employment Rights | For a TP application, the work right starts when police/Border Guard registers the application; a permit card is not a prerequisite to that start. | The current certificate is evidence, not a residence permit. Do not assure individual card validity after the unresolved 2027/2028 extension conflict. | FI-EMP-02-S01–S02 | READY WITH QUALIFICATION |
| `work-employer-duties` | FI-EMP-02 | Employment Rights | Employers must verify and retain the required evidence and perform applicable notification duties. | Keep employer duties separate from the worker’s work right; exact early-ending notice application to TP workers is not established. | FI-EMP-02-S03–S05, S08–S09 | READY WITH QUALIFICATION |
| `work-minimum-pay` | FI-EMP-02 | Employment Rights | The reviewed evidence establishes no universal statutory national minimum wage; applicable collective-agreement terms may set the floor. | Where no agreement applies and no pay was agreed, the reviewed fallback is usual and reasonable remuneration. Do not give one national euro minimum. | FI-EMP-02-S05–S07 | READY WITH QUALIFICATION |
| `healthcare-access` | FI-HEALTH-01 | Healthcare | Access differs by pending/granted status and age; granted TP has the Reception Act §26 basis while reception services remain active. | A pending adult does not have the same scope as a child or granted TP holder. After reception exit without `kotikunta`, §26 remains but the practical provider/referral route is not uniform. | FI-HEALTH-01-S01–S04 | READY WITH QUALIFICATION |
| `healthcare-proof` | FI-HEALTH-01 | Healthcare | Use the document relevant to the entitlement: TP certificate/permit, municipality registration or a Kela certificate where Kela determines eligibility. | Personal identity code and Kela card are not universal healthcare entitlements. No nationwide post-reception/no-`kotikunta` proof or referral list is established. | FI-HEALTH-01-S04, S07–S08 | READY WITH QUALIFICATION |
| `healthcare-urgent` | FI-HEALTH-01 | Healthcare | Urgent care is available regardless of residence; use 112 for emergencies and 116117 for urgent non-life-threatening advice where available. | 116117 is unavailable in Åland; urgent care may be billed. Access does not mean free care. | FI-HEALTH-01-S03, S07, S09–S10 | READY |
| `housing-initial` | FI-HOUSING-01 | Housing | Reception accommodation is arranged within available capacity; reception entitlement ends under the eligibility-based transition rule. | No chosen location or replacement housing is guaranteed. State the maximum three-month transition from eligibility to apply for `kotikunta`, its unaccompanied-minor exception and limited discretionary extension; individual end date must be checked. | FI-HOUSING-01-S01–S06 | READY WITH QUALIFICATION |
| `housing-private-rental` | FI-HOUSING-01 | Housing | A person may seek a private rental under the lease and landlord’s lawful application process. | Do not promise acceptance or an exhaustive document list. A personal identity code is not a universal statutory prerequisite. Apply the 1 Oct 2026 lease amendment by the relevant lease date; distinguish deposit from advance rent. | FI-HOUSING-01-S10–S14 | READY WITH QUALIFICATION |
| `housing-financial-support` | FI-HOUSING-01 | Housing | Kela housing allowance or deposit assistance may be applied for where its separate criteria are met. | TP, reception allowance or `kotikunta` alone does not guarantee support; Kela decides eligibility and amount. Reception allowance does not reimburse private rent. A housing application is not an allocation. | FI-HOUSING-01-S03, S07–S09 | READY WITH QUALIFICATION |
| `education-school-enrolment` | FI-EDU-01 | Education | A TP child living in a municipality can access pre-primary/basic education without `kotikunta`; contact the municipality’s education department. | Access is not the same as compulsory-education duty. No national document checklist, preferred-school right or immediate placement is established. | FI-EDU-01-S01–S05 | READY WITH QUALIFICATION |
| `education-pupil-language` | FI-EDU-01 | Education | Preparatory education and S2/R2 language support may be arranged according to need and provider decisions. | Preparatory or Ukrainian-language teaching is not guaranteed in every municipality; Ukrainian online schooling is not established as an equivalent substitute. | FI-EDU-01-S02, S06–S07 | READY WITH QUALIFICATION |
| `education-higher-admission` | FI-EDU-01 | Education | Studyinfo is an application/search channel; each HEI decides admission and recognition. | Admission is not guaranteed. Keep granted-TP tuition exemption distinct from the application-fee test, scholarship and Kela student aid; apply the separate statutory conditions. | FI-EDU-01-S08–S15 | READY WITH QUALIFICATION |
| `social-subsistence` | FI-SOC-01 | Social Assistance | Reception allowance is assessed by the reception centre while a person is entitled to reception services; a person with `kotikunta` may separately apply to Kela for basic assistance. | Allowance is not automatic or rent reimbursement. Preserve the eligibility-based transition/exception rule; ordinary recurring post-reception support without `kotikunta` is not established. | FI-SOC-01-S01–S05, S07–S10 | READY WITH QUALIFICATION |
| `social-application` | FI-SOC-01 | Social Assistance | Apply to the reception centre for reception allowance and to Kela for Kela benefits; Kela accepts non-online applications. | No universal process is established for an applicant without both personal code and usable e-ID, or for Kela payment without a bank account. Jobseeker registration is not a benefit decision. | FI-SOC-01-S01–S03, S06–S11, S13–S16 | READY WITH QUALIFICATION |
| `social-child-benefits` | FI-SOC-01 | Social Assistance | Child/family benefits require separate Kela eligibility decisions; a specific TP child-benefit route is described for qualifying one-year cases. | No automatic award. Child-abroad and family coverage need individual assessment; dated form guidance and future 2027 changes must not be presented as universal current rules. | FI-SOC-01-S04, S13–S16 | READY WITH QUALIFICATION |

## Research gaps

1. **`protection-dates` is the sole material gap.** Official Migri pages in the existing research disagree on 2027 versus 2028 individual permit/card validity and whether holder action is needed. Existing sources support the EU endpoint and a warning to verify the personal record, but do not resolve individual permit/card validity or a universal no-action instruction. Targeted official clarification is required before a complete 24-aspect Compare set can pass review.
2. The other open questions recorded in the eight packages are bounded local or individual uncertainties. They do not prevent a safe qualified fact when the conditions above are retained. Do not turn them into national guarantees or invent a uniform procedure.

## Production source gaps

- Existing Finland source records: **100**, all owner-scoped and HTTPS; unique IDs: **100**; unique URLs: **90**.
- Each of the 23 non-blocked aspects has owner-specific records cross-referenced in its research package and draft source set.
- No additional production source record is currently required to draft those 23 aspects or to state the narrow qualified `protection-dates` boundary. Recheck the existing Migri URLs first. If the authoritative clarification is published at a distinct URL, add an owner-specific FI-TP-01 source record during a later authorized integration; that URL is not known now.

## Core issues

- **Factual/translation correction required:** none identified in the post-correction editorial review. All 8 drafts are PASS there; no correction is proposed by this assessment.
- **Qualification to preserve:** FI-TP-01 must not convert the EU 4 March 2028 endpoint into an individual Finnish permit/card expiry or universal “no action” instruction. The article already states this boundary in EN/UK/RU.
- **Release status:** all 24 production translations remain `NeedsReview`. This report does not promote them to `Verified` or Production-visible status. A final Core review/promotion step remains separate.

## Current state checks

- Finland owners: **8/8** present with expected IDs and categories.
- Compare aspects: **24/24 unique IDs** in the eight expected groups; the current schema has each aspect once.
- Translation records assessed: **24/24**; languages and timestamps match the drafts/review.
- Catalog/source integrity: **8/8 owners**, 100/100 source records matched, 318/318 inline citation tokens resolved.
- Finland Compare: **0 facts**; map excludes Finland. Current total remains **17 countries / 408 facts**.
- No production JSON, source registry, test, visibility or runtime changes were made.

## Final counts

- Articles: **8**
- Translations: **24** — all `NeedsReview`
- Owner source records / unique URLs: **100 / 90**
- READY aspects: **2**
- READY WITH QUALIFICATION aspects: **21**
- RESEARCH REQUIRED aspects: **1** (`protection-dates`)
- Core PASS: **8** at post-correction draft review
- Core NEEDS FINAL REVIEW: **0 correction findings; formal final acceptance/promotion still pending**
- Core REVISION REQUIRED: **0**
- Core BLOCKED: **0 article drafts**; full Compare is blocked by the separate `protection-dates` evidence gap
- New research required: **YES**, targeted authoritative clarification for `protection-dates`
- New production source records required now: **NO**; possible later only if new clarification uses a new official URL

## Verdict

**READY FOR FINAL CORE REVIEW; TARGETED COMPARE RESEARCH REQUIRED**

The eight Core drafts may proceed to final Core review without a correction pass. Production promotion remains pending final Core acceptance and translation verification. Compare drafting can use the 23 ready/qualified aspects, but the complete 24-aspect Compare package must wait for targeted authoritative clarification of `protection-dates`; Finland remains hidden from Production and absent from Compare in the current state.
