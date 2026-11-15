# Ireland Core and Compare Gap Assessment 01

**Assessment date:** 15 November 2026
**Research baseline:** Ireland-specific packages reviewed through 10 November 2026. No new research was performed.
**Scope:** Structural readiness, existing research/source coverage, owner gaps and Compare readiness. This is not a final editorial review.

## Current Core state

- Existing Ireland owner articles: **5/8**.
- Missing owner articles: **3/8** — `IE-TP-01`, `IE-TP-02`, `IE-HOUSING-01`.
- Existing translations: **15/15**, EN/UK/RU for each current article. All are `NeedsReview`, dated 10 November 2026. None is Verified for Production.
- Existing Ireland Production source records: **70**, all unique IDs and HTTPS. They cover the five existing article owners; URL deduplication yields **65 unique URLs**.
- Draft/catalog parity: **15/15 translations** have exact title, summary and content parity, including their language codes and current review status.
- Citation integrity: **216/216 citation tokens** resolve to an existing source owned by the cited article. Missing source IDs: **0**. Cross-owner citations: **0**.
- Visibility: Ireland is hidden in Production and visible in Development. Compare excludes Ireland.
- Current Compare baseline: **18 countries, 8 topics, 24 aspects and 432 facts**; Ireland facts: **0**. Older Ireland audit counts of 13 countries/312 facts predate the later country integrations and are superseded by the current Ireland integration tests.

The five current articles are structurally complete as drafts and catalog records. Their `NeedsReview` status means they are not Production-ready; this assessment does not replace editorial acceptance.

The 10 November research-readiness document's catalog snapshot predates Ireland's partial integration and is stale for entry counts/visibility. Its topic-level evidence findings are used here; current catalog and integration-test values above supersede its older catalog snapshot.

## Existing article matrix

| Article | Article ID | Category | Draft | EN/UK/RU | Catalog parity | Verification status | Production sources |
| --- | --- | --- | --- | --- | --- | --- | ---: |
| `IE-EMP-01` | `db600af0-bbf7-517b-ac82-3a7781105b8d` | employment | Yes | 3/3 | PASS | NeedsReview | 9 |
| `IE-EMP-02` | `e6b45fb5-4180-5e57-8e48-30c25ddad0f8` | employment | Yes | 3/3 | PASS | NeedsReview | 20 |
| `IE-HEALTH-01` | `864a1ff8-dc63-56bf-ae78-860a452a8f04` | healthcare | Yes | 3/3 | PASS | NeedsReview | 10 |
| `IE-EDU-01` | `8dc791b3-5002-59ab-b8fb-2c7ab1dbf43b` | education | Yes | 3/3 | PASS | NeedsReview | 15 |
| `IE-SOC-01` | `682561e1-29db-5332-974b-cf38327a859d` | social-assistance | Yes | 3/3 | PASS | NeedsReview | 16 |

Source counts total **70**. They match the Ireland integration test baseline. The 70 records resolve 216 inline citation uses; repeated citations are expected and do not imply extra source records.

## Missing owner matrix

| Article / topic | Existing draft / catalog owner | Research package and source coverage | Remaining evidence gap | Can drafting start now? | Later source registry work |
| --- | --- | --- | --- | --- | --- |
| `IE-TP-01` — Temporary Protection Application | No draft or information entry; no reserved article UUID found in the scoped Ireland files | Yes — `temporary-protection-application/SOURCES_AND_CLAIMS.md`; 14 source rows, 14 unique HTTPS URLs | Full current §237 application/grant workflow after border contact, proof format/timing and new-applicant evidence remain unresolved. Irish permission/certificate/IRP validity and holder action after 4 March 2027 also remain unresolved. | **Partial sections only.** Scheme scope can be drafted with qualifications; do not complete practical application/date instructions until targeted research closes or excludes the gaps. | New owner article ID and owner-bound `OfficialSource` records required. New official URLs likely for unresolved questions. |
| `IE-TP-02` — Residence After Protection | No draft or information entry; no reserved article UUID found in the scoped Ireland files | Yes — `after-temporary-protection/SOURCES_AND_CLAIMS.md`; 21 source rows, 21 unique HTTPS URLs | TP-specific in-country filing for ordinary work, study, family and STEP routes; application sequence; pending/bridging stay and work rights; and whether TP/TPTS time qualifies for Long Term Residency remain unestablished. | **Partial sections only.** TPTS and ordinary route conditions can be described with limits; do not give transition or filing instructions pending targeted evidence. | New owner article ID and owner-bound `OfficialSource` records required. New official URLs likely for unresolved questions. |
| `IE-HOUSING-01` — Housing | No draft or information entry; no reserved article UUID found in the scoped Ireland files | Yes — `housing/SOURCES_AND_CLAIMS.md`; 26 source rows, 26 unique HTTPS URLs | Current general initial intake and continued-accommodation procedure/eligibility beyond the closed Citywest cohort; route for people living independently; capacity/failure steps and accommodation-specific contribution rules remain incomplete. | **Partial sections only.** Private rental and support routes can be drafted with qualifications; do not present a complete initial or continued-accommodation procedure. | New owner article ID and owner-bound `OfficialSource` records required. New official URLs likely for unresolved questions. |

Across the three missing-owner packages there are **61 inventory rows and 57 unique URLs**. Four of those URLs already occur in the current Ireland source registry; 52 of the 57 are not currently in the global Production source catalog. These package sources do not close the remaining evidence gaps. A later drafting/integration pass will need owner-specific source records even when reusing an existing URL. No Production records or UUIDs are created by this assessment.

## Compare owner matrix

| Topic | Expected owner | Existing? | Article ID / draft | Research? | Production sources | Compare readiness |
| --- | --- | --- | --- | --- | ---: | --- |
| Temporary Protection | `IE-TP-01` | No | No | Yes — 14 source rows | 0 | Partial; two aspects require research |
| Residence After Protection | `IE-TP-02` | No | No | Yes — 21 source rows | 0 | Partial; one aspect requires research |
| Finding Employment | `IE-EMP-01` | Yes | Yes | Yes — 26 research rows | 9 | Ready for Compare with two READY and one qualified aspect |
| Work Rights | `IE-EMP-02` | Yes | Yes | Yes — 33 research rows | 20 | Ready with qualifications |
| Healthcare | `IE-HEALTH-01` | Yes | Yes | Yes — 13 research rows | 10 | Ready with qualifications |
| Housing | `IE-HOUSING-01` | No | No | Yes — 26 source rows | 0 | Partial; one aspect requires research |
| Education | `IE-EDU-01` | Yes | Yes | Yes — 21 research rows | 15 | Ready with qualifications |
| Social Assistance | `IE-SOC-01` | Yes | Yes | Yes — 20 research rows | 16 | Ready with qualifications |

The eight topic packages contain **174 inventory rows and 153 unique official HTTPS URLs**. Their 174 research source IDs are unique according to the Ireland research readiness audit. Research inventory rows are not Production source records.

## 24-aspect matrix

`CONTENT READY — OWNER MISSING` means the research supports a qualified fact, but no owner article exists for integration. `RESEARCH REQUIRED` means the existing package itself marks a material evidence gap; a missing owner also applies where noted in the owner matrix.

| Aspect | Owner | Evidence / source basis | Qualification or unresolved issue | Status |
| --- | --- | --- | --- | --- |
| `protection-scheme` | `IE-TP-01` | TP research package; 14 sources | Keep §237 and legacy §60 cohorts, eligibility groups, family scope and military-leave evidence distinct. | CONTENT READY — OWNER MISSING |
| `protection-application` | `IE-TP-01` | TP research package; 14 sources | Border contact is not a complete current §237 workflow. Grant/proof format, timing and new-applicant checklist are unestablished. | RESEARCH REQUIRED |
| `protection-dates` | `IE-TP-01` | TP research package; 14 sources | Underlying TP is confirmed through March 2028 for non-TPTS beneficiaries; this does not establish Irish permission, certificate/letter or IRP validity/action after 4 March 2027. | RESEARCH REQUIRED |
| `residence-routes` | `IE-TP-02` | Residence research package; 21 sources | TPTS is a specific route for eligible beneficiaries. Do not imply automatic conversion or that work, study or family circumstances alone grant a permission. | CONTENT READY — OWNER MISSING |
| `residence-conditions` | `IE-TP-02` | Residence research package; 21 sources | TPTS public criteria are not a full checklist; ordinary route criteria do not establish TP-holder eligibility. | CONTENT READY — OWNER MISSING |
| `residence-filing` | `IE-TP-02` | Residence research package; 21 sources | TP-specific in-country filing, sequencing and pending/bridging stay or work effects remain unestablished. | RESEARCH REQUIRED |
| `employment-vacancies` | `IE-EMP-01` | 9 owner sources | JobsIreland/EURES are not exhaustive; browsing and registration differ. | READY |
| `employment-institutions` | `IE-EMP-01` | 9 owner sources | Separate DSP/Intreo, DETE, EURES, SOLAS/ETBs and QQI/NARIC roles; services do not guarantee placement or licensing. | READY |
| `employment-assistance-conditions` | `IE-EMP-01` | 9 owner sources | No universal Intreo checklist or open Ukraine-specific intake; scheme/payment eligibility is separate from work rights. | READY WITH QUALIFICATION |
| `work-permission` | `IE-EMP-02` | 20 owner sources | Limit TP work right to a valid permission; distinguish it from pending initial requests, ordinary permits, PPSN and IRP. | READY WITH QUALIFICATION |
| `work-employer-duties` | `IE-EMP-02` | 20 owner sources | Ordinary employment duties are supported; no universal TP-specific document retention/notification process is established. | READY WITH QUALIFICATION |
| `work-minimum-pay` | `IE-EMP-02` | 20 owner sources | Use €14.15/hour adult rate as of the research date; announced €14.94 for 2027 was not found enacted. Retain age, exception and sectoral-rate limits. | READY |
| `healthcare-access` | `IE-HEALTH-01` | 10 owner sources | Valid TP permission gives equal-conditions medical entitlement, not automatic GP registration, capacity or universally free care. | READY WITH QUALIFICATION |
| `healthcare-proof` | `IE-HEALTH-01` | 10 owner sources | PPSN is relevant to the UKR Medical Card route, not a universal care prerequisite; no complete universal proof checklist. | READY WITH QUALIFICATION |
| `healthcare-urgent` | `IE-HEALTH-01` | 10 owner sources | 112/999 calls and treatment charges are distinct; retain specific exemption limits. | READY WITH QUALIFICATION |
| `housing-initial` | `IE-HOUSING-01` | Housing research package; 26 sources | High-level 30-day offer and closed Citywest process do not establish a complete current general intake or continued-accommodation procedure. | RESEARCH REQUIRED |
| `housing-private-rental` | `IE-HOUSING-01` | Housing research package; 26 sources | Apply ordinary tenancy/RTB rules with type/date exceptions; TP is not a tenancy, and universal immigration-document requirements are not established. | CONTENT READY — OWNER MISSING |
| `housing-financial-support` | `IE-HOUSING-01` | Housing research package; 26 sources | Separate designated-centre BOTP payment, Rent Supplement, HAP and ARP. ARP rate pages conflict; no automatic rent/deposit payment. | CONTENT READY — OWNER MISSING |
| `education-school-enrolment` | `IE-EDU-01` | 15 owner sources | Access and compulsory-education rules do not guarantee a preferred or immediate place; local documentation/placement conditions apply. | READY WITH QUALIFICATION |
| `education-pupil-language` | `IE-EDU-01` | 15 owner sources | 2026/27 EAL is school-level support, not guaranteed individual hours or a dedicated teacher. | READY WITH QUALIFICATION |
| `education-higher-admission` | `IE-EDU-01` | 15 owner sources | Institutions decide admission/recognition; tuition support is limited to a specified continuing §60 cohort, separate from SUSI/stipends. | READY WITH QUALIFICATION |
| `social-subsistence` | `IE-SOC-01` | 16 owner sources | BOTP Weekly Payment is income-tested and tied to designated accommodation; ordinary payments have separate conditions. | READY WITH QUALIFICATION |
| `social-application` | `IE-SOC-01` | 16 owner sources | Evidence, channels and payment vary; PPSN or application is not an award. | READY WITH QUALIFICATION |
| `social-child-benefits` | `IE-SOC-01` | 16 owner sources | Child Benefit and WFP are distinct; HRC and EU coordination are payment-specific, with new §237 treatment unresolved. | READY WITH QUALIFICATION |

**Aspect totals:** READY **3**; READY WITH QUALIFICATION **12**; RESEARCH REQUIRED **4**; CONTENT READY — OWNER MISSING **5**; BLOCKED BY MISSING OWNER **0**. Total assessed: **24/24**. The five owner-missing facts remain unavailable for Compare integration until the relevant owner articles and their records exist.

## Research gaps

Only four material gaps remain in the current research inventory:

1. **`protection-application`:** official steps after border interaction, new §237 grant/proof format and timing, and essential evidence for new applicants.
2. **`protection-dates`:** current Irish permission/certificate/letter/IRP validity and holder action after 4 March 2027, kept distinct from the EU temporary-protection end date of 4 March 2028.
3. **`residence-filing`:** whether/how TP holders can file ordinary employment, study, family or STEP routes in-country; application sequence; pending/bridging stay and work effects; TP/TPTS time treatment under Long Term Residency.
4. **`housing-initial`:** current general initial request channel, eligibility/assessment and capacity steps; whether a general continued-accommodation intake is open outside the closed Citywest cohort; procedure for people living independently and Rapid Build/commercial residents; applicable contribution rules by accommodation type.

## Drafting gaps

- Create `IE-TP-01`, `IE-TP-02` and `IE-HOUSING-01` drafts in EN/UK/RU after targeted research resolves or explicitly excludes the four gaps.
- Five qualified aspects can be drafted from current packages with their stated limits; they do not justify completing the blocked application, date, filing or initial-housing instructions.
- Expected additions after drafting: **3 article drafts and 9 translations**. Current five articles and 15 translations remain unchanged.
- Do not generate article UUIDs in this assessment. No reserved deterministic article UUID was found in the scoped Ireland draft/research files or current information-entry catalog; IDs are **required at drafting/integration**.

## Source gaps

- The three missing-owner packages already list **61 source rows / 57 unique HTTPS URLs**. They do not resolve the four gaps. A targeted official-source refresh is likely to require additional URLs for those questions; exact URLs are not established without that research.
- Current catalog has no source records owned by the missing article IDs because those owners do not exist. New owner-specific source records **will be required** at integration, even when a URL is reused.
- Current packages include 57 URLs, 5 of which appear somewhere in the global Production source catalog and 4 in the existing Ireland owner source set. Research inventory presence is not Production ownership or a valid citation binding.

## Production and Compare hold

- Existing owners: **5/8**; missing owners: **3/8**.
- Ireland is absent from `CountryComparisonTopicMap` and has **0 Compare facts**.
- Current global Compare remains **18 countries / 8 topics / 24 aspects / 432 facts**.
- Ireland remains Development-visible and Production-hidden. This assessment changes no production, visibility or Compare state.

## Recommended next batch

**Targeted official-source research only for the four aspects above**, using the existing Ireland packages as the evidence baseline. Do not repeat the other twenty aspect reviews. Then draft the three missing owners in the order `IE-TP-01` → `IE-TP-02` → `IE-HOUSING-01`, with translations, article/source IDs and owner-bound source records handled in the drafting/integration tasks. If an official procedure cannot be confirmed, keep that claim out of the draft and retain the limitation.

## Counts

- Existing Core articles: **5**
- Missing Core articles: **3**
- Existing translations: **15** (all NeedsReview)
- Missing translations for the three owners: **9**
- Existing Ireland source records: **70**
- Existing unique Ireland HTTPS URLs: **65**
- Existing citation tokens / valid: **216 / 216**
- Existing draft/catalog parity: **15/15**
- READY: **3**
- READY WITH QUALIFICATION: **12**
- RESEARCH REQUIRED: **4**
- CONTENT READY — OWNER MISSING: **5**
- BLOCKED BY MISSING OWNER: **0**

## Verdict

**TARGETED RESEARCH REQUIRED**

The evidence packages cover all three missing owners and support qualified drafting for five aspects, but four material gaps affect practical application, document validity, route filing and initial/continued housing guidance. The three missing articles cannot be completed for safe Core use until those targeted gaps are resolved or the unsupported claims are explicitly excluded.
