# Hungary v1 — Editorial review

**Review date:** 12 November 2026  
**Baseline:** `2a2ad9adf6d6c0235d01226f2020798b2e2f3415`  
**Scope:** Exactly seven Hungary drafts listed below, their matching focused research packages, and Hungary aggregate readiness. Belgium and Finland reviews were used only for structure, severity and audit depth. No external research was used. No draft content was changed.

## Recommendation

**READY FOR CORRECTION PASS.** One bounded correction is required in HU-HOUSING-01: the shared editorial source entry for `HU-HOUSING-S03` must use the exact official URL in the owning research inventory. It is supported by existing research; no new research is needed. Do not integrate until the correction pass and post-correction review are complete. Hungary is not ready for Compare, is not Production-ready as a complete country, and all translations remain `NeedsReview`.

HU-TP-01 remains intentionally absent because `protection-scheme` and `protection-application` are blocked. The other blocked aspects, `residence-filing` and `housing-initial`, also remain blocked. This review makes no readiness change.

## Article review

| Draft | Review | Source UUIDs | Coverage and editorial boundaries |
|---|---:|---:|---|
| `HU-TP-02.md` | **PASS** | 18 | Separates ordinary residence-route eligibility from permission to file while active TP is held. No automatic conversion, post-expiry stay/work bridge, or confirmed active-TP filing procedure is promised; the `residence-filing` boundary remains explicit. |
| `HU-EMP-01.md` | **PASS** | 14 | Keeps VMP/EURES browsing, NFSZ services, recognized-beneficiary `szolgáltatást kérő`, ordinary `álláskereső`, work authorization and unemployment allowance distinct. Pending-applicant service registration and practical registration mechanics remain qualified. |
| `HU-EMP-02.md` | **PASS** | 14 | States permit-free work for recognized beneficiaries and formally filed applicants, identifies formal filing as the applicant trigger, and distinguishes employer notifications, NAV 08E, TAJ and work permission. Contract, wage, rest and leave claims are bounded; both 2026 wage schedules and qualification trigger are stated correctly. |
| `HU-HEALTH-01.md` | **PASS** | 14 | Keeps beneficiary and pending-applicant healthcare bases separate from ordinary employment insurance. Distinguishes TP, TAJ number and physical TAJ card; does not revive repealed Decree 171/2022 or claim that no TAJ means no care. Urgent-care numbers and service limits are qualified. |
| `HU-HOUSING-01.md` | **CORRECTION** | 11 | Clearly discloses that `housing-initial` remains blocked; BMSZKI is general homelessness support, not a verified TP arrival route. Rental, OIF address reporting, `lakcímkártya`, deposits and support recipients are distinct. The source URL for `HU-HOUSING-S03` differs from the matching research inventory and needs normalization. |
| `HU-EDU-01.md` | **PASS** | 12 | Preserves school access from formal TP filing, catchment routing, missing-record and local-support limits, meal application conditions, higher-education admission/funding distinctions, and the rule that study rights do not require a study residence permit. |
| `HU-SOC-01.md` | **PASS** | 14 | Limits regular subsistence support to the researched recognized-beneficiary route and amounts; keeps OIF decision/payment separate from employment-office cooperation. The 45-day condition is scheme-specific; application/payment details and family-benefit scope are qualified. |

## Required corrections

### 1. HU-HOUSING-01 — align the OIF source URL with its research inventory

- **Draft:** `HU-HOUSING-01.md`
- **Issue:** Editorial source `HU-HOUSING-01-S03` maps to research source `HU-HOUSING-S03`, but the draft lists `https://oif.gov.hu/menedekeseknek-nyujtott-ellatasok-es-tamogatasok`; the owning housing package lists `https://www.oif.gov.hu/menedekeseknek-nyujtott-ellatasok-es-tamogatasok`.
- **Why unsafe/incomplete:** The host difference may redirect to the same OIF material, and the authority/title/source identity correspond, but the requested source-ownership audit requires the official URL to match the inventory exactly. The record is therefore not an exact source-inventory match.
- **Exact supported correction boundary:** Change only the URL on the shared editorial source entry for `HU-HOUSING-S03` to the exact URL in the housing package. Keep the source UUID, research source ID, authority and title unchanged.
- **Languages affected:** The shared editorial source record used by EN / UK / RU; no language-specific claim or citation change is indicated.
- **Existing research source(s):** `docs/content-research/hungary-batch-2026-11-12/housing/SOURCES_AND_CLAIMS.md`, source `HU-HOUSING-S03`.
- **New research needed:** No.

## Metadata audit

All seven drafts specify Country `Hungary`, the expected category, the exact matching focused package path and the expected comparison aspects. EN / UK / RU sections and `NeedsReview` status are present in every draft. Each language contains a `Claim coverage and editorial exclusions` section. All seven Article IDs are valid UUIDv5 values and unique within the reviewed set. No mismatch was found in the requested metadata fields. The Article IDs were validated for UUIDv5 form and uniqueness; the repository does not define a separate article-ID name-generation recipe in the reviewed scope.

## Source UUID audit

- **Editorial source entries:** 97 total: HU-TP-02 18; HU-EMP-01 14; HU-EMP-02 14; HU-HEALTH-01 14; HU-HOUSING-01 11; HU-EDU-01 12; HU-SOC-01 14.
- **Deterministic UUIDv5:** 97/97 reproduced using each owning Article ID as the UUID namespace and the exact local editorial source key as the name. All keys use the owning article prefix.
- **Version and uniqueness:** 97/97 are UUIDv5 and globally unique.
- **Collisions:** 0.
- **Cross-article ownership violations:** 0; no source UUID is shared across articles.

## Research-source ownership audit

All 97 editorial entries refer to a source ID in that draft's matching focused package. Authority and source description materially correspond, all listed URLs are HTTPS, and no unofficial or cross-topic borrowed source was found. The sole inventory mismatch is HU-HOUSING-01-S03's `www` host difference documented above; it is an exact-URL correction, not a source-identity or claim-evidence blocker. Other audited URL, title and source-ID mappings match their owning inventories.

## Citation audit

The citation UUIDs resolve only to editorial entries in the same article. Across the three language bodies there are **861 inline citation tokens** (287 per language across the seven drafts). Citation UUID sets are equivalent in EN / UK / RU for every article.

| Draft | Sources | Citation tokens across EN / UK / RU |
|---|---:|---:|
| HU-TP-02 | 18 | 96 |
| HU-EMP-01 | 14 | 108 |
| HU-EMP-02 | 14 | 99 |
| HU-HEALTH-01 | 14 | 132 |
| HU-HOUSING-01 | 11 | 108 |
| HU-EDU-01 | 12 | 162 |
| HU-SOC-01 | 14 | 156 |
| **Total** | **97** | **861** |

**Orphan citations:** 0. **Unused editorial source entries:** 0. No raw research source IDs are used as citations in the public EN / UK / RU article bodies; research IDs appear only in the editorial source mapping. No citation label materially overstates the owning source based on this review. Citation labels are language-appropriate and citation coverage is parallel.

## Translation parity

EN / UK / RU material guidance is parallel across status and eligibility, dates, amounts, ages, authorities, filing boundaries, negative statements and practical instructions. In particular, formal TP filing is not reduced to online preparation; pending applicants are not conflated with recognized beneficiaries; uncertainty and blocked-area qualifications are retained; and numeric rules appear consistently. Citation UUID sets match across all three languages for each article. No material translation parity correction was found. The source URL correction is in the shared editorial source metadata and does not alter translated claims.

## HU-TP-02 review

The article makes no automatic TP-to-ordinary-permit conversion or enacted automatic post-4 March 2028 transition claim. National Card nationality eligibility is separated from an established active-TP filing route; Hungarian Card remains conditional. It correctly states that Ukraine is absent from the researched current employment-purpose nationality list, new guest-worker applications have been unavailable since 6 June 2026, and active TP excludes grant of an EU Blue Card and study residence permit. Family reunification is distinguished from TP family unity; guest self-employment is conditional. National Residence Card TP-time treatment remains unresolved while EU Residence Card counting is distinguished. In-country active-TP filing, pending-after-expiry stay/work, and coexistence/termination/reactivation mechanics remain explicitly unestablished. The practical text does not promise that filing before 4 March 2028 allows the person to stay or work during processing. **`residence-filing` remains blocked; no leakage found.**

## HU-EMP-01 review

VMP, EURES, NFSZ and district employment offices are covered as separate public channels. The recognized `menedékes` age-16+ special route is `szolgáltatást kérő`, not ordinary `álláskereső`. The pending-applicant route to that registration is not established; formal TP filing's work right is not imported as a job-service registration right. Registration is not work authorization or unemployment allowance. The article invents no TP document checklist and promises no guaranteed online or walk-in procedure. Training and language programmes are conditional, and employer housing support is not jobseeker cash. Terminology aligns with HU-SOC-01.

## HU-EMP-02 review

The article correctly distinguishes recognized-beneficiary work from the applicant's right after formal TP filing; online preparation or intent is not enough. It states no general sector restriction for the TP work route and does not treat protection granted by another EU country alone as Hungarian authorization. Recognized-beneficiary employer notification timing is given; the applicant-specific channel/deadline remains qualified. NAV 08E is current and T1041 is not presented as current. TAJ is not immigration/work permission, and a physical TAJ card is not a precondition established for contracting or work. The written-contract, wage/payment/rest/leave statements are bounded.

The 2026 full-time minimum wage is accurately stated as HUF 322,800/month, 74,210/week, 14,850/day and 1,856/hour. The guaranteed wage minimum is HUF 373,200/month, 85,800/week, 17,160/day and 2,145/hour. The higher floor is tied to a qualification required by the job, not the worker's diploma alone. Gross pay is distinguished from take-home pay and part-time qualification is included. No obsolete Ukraine sector-limited rule is presented as current. No correction required.

## HU-HEALTH-01 review

Recognized-beneficiary and pending-applicant statutory packages remain distinct; work is not made a condition of TP healthcare, and TP care is not described as full ordinary insurance. TAJ number, physical card and TP entitlement are distinct, with no automatic TAJ claim and no “no TAJ = no healthcare” rule. Repealed Decree 171/2022 is identified as repealed rather than a current process; the TAJ-less provider workflow stays qualified. Status/application and accommodation evidence, planned versus urgent care, medicine/dental/chronic limits, pregnancy/child status evidence, and absence of guaranteed interpreter/language service are bounded. 112 is for life-threatening emergencies; 1830 is for urgent non-life-threatening out-of-hours care, weekdays 16:00–08:00 and continuously on weekends/holidays. No automatic EHIC claim is made. Cross-article TAJ wording agrees with HU-EMP-02.

## HU-HOUSING-01 review

The article explicitly keeps `housing-initial` unresolved. It gives no verified TP first-night intake route, describes BMSZKI only as general homelessness support (18:00–08:00, free, bed not guaranteed), invents no national hotline, and does not turn applicant accommodation/funding into guaranteed placement. It distinguishes the recognized beneficiary's ordinary reception-centre exclusion, conditional vulnerable-group funding and private rental. Written lease, OIF address reporting and `lakcímkártya` are separate; the card is not a universal rental prerequisite. The three-month deposit threshold is not described as a hard cap; `óvadék` may be reduced by a court if grossly excessive, and is distinguished from advance rent. No national direct rent/deposit/utility cash is claimed. Employer subsidy goes to the employer; municipal aid remains local and conditional. No legacy BOK/railway/transit route is revived. The article safely discloses the evidence gap, so the known research blocker alone does not make it a draft-level BLOCKER. One bounded source URL correction is recorded above.

## HU-EDU-01 review

The compulsory school-year age rule and kindergarten age are stated with exceptions. School access begins at formal TP application filing for eligible children; recognized and pending categories, address/catchment routing and the catchment school's duty are preserved. No universal `lakcímkártya` prerequisite is asserted. Missing Ukrainian records are not an automatic refusal; school/director placement authority and the absence of automatic grade mapping are stated. Language support is local and conditional; no universal fixed-hours programme, Ukrainian class/teacher/interpreter or legacy five-hour entitlement is promised. Ukrainian online school is not treated as a substitute for compulsory Hungarian schooling. Free public instruction is distinguished from every related cost; six-month meal support requires application. University admission, qualification recognition and funded status remain separate; OH/MEIK is not conflated with university assessment, no universal certified-translation rule is imposed, and scholarships/calls are dated rather than presented as open guarantees. Active recognized TP study rights are not made subject to obtaining a study residence permit, consistently with HU-TP-02. No correction required.

## HU-SOC-01 review

`Rendszeres létfenntartási támogatás` is limited to the recognized `menedékes` route; no pending-applicant route is inferred. Adult/minor amounts (HUF 22,800 / 13,700), OIF decision/payment, payment by the 15th, age-6 appearance condition and age-16+ `szolgáltatást kérő` route are retained. No form, channel, document list or bank-free payment method is invented. `Szolgáltatást kérő` remains distinct from `álláskereső`; the 45-day suitable-job condition is scoped to this support, with income/pension consequences and no comprehensive means test invented. There is no guarantee through 4 March 2028. Local municipal assistance is conditional; no same-day cash guarantee is made. TP alone is not turned into family allowance, maternity support, GYES or GYET eligibility; ordinary rates are not presented as TP entitlements. CSED/GYED remain insurance-based, school meals and housing programmes are separate, newborn recognition requires its separate TP procedure, and the §37/A 15-day report is not transferred to this allowance. EEA coordination is not generalized to Ukraine. Terminology and conditions align with HU-EMP-01. No correction required.

## Cross-article status and document consistency

Across all seven drafts, pending applicant, recognized `menedékes`, formal TP filing, beneficiary card, applicant humanitarian/interim document, accommodation evidence, `lakcímkártya`, TAJ number, physical TAJ card, `szolgáltatást kérő` and `álláskereső` retain distinct meanings. No article turns one status or document into another.

## Cross-article address consistency

HU-TP-02, HU-EMP-01, HU-HEALTH-01, HU-HOUSING-01, HU-EDU-01 and HU-SOC-01 do not conflate accommodation address, private lease, `lakcímkártya` or automatic benefit eligibility. The accommodation record is used for the specific OIF, employment-service, health or school-routing purpose supported by its package. No contradictory address instruction was found.

## Cross-article work consistency

HU-EMP-01, HU-EMP-02 and HU-SOC-01 preserve that job-service registration is not work authorization. Pending-applicant work after formal filing does not imply pending-applicant `szolgáltatást kérő` registration. The recognized-beneficiary age-16+ route is retained. Benefit cooperation is not generalized into a universal job-search duty; the 45-day suitable-job rule belongs only to the TP subsistence scheme. Unemployment allowance remains separate.

## Cross-article TAJ consistency

HU-EMP-02 and HU-HEALTH-01 agree that an employer may request/arrange TAJ registration for an insured foreign worker, while this does not mean every TP holder automatically receives TAJ. TAJ is not TP; a physical TAJ card is neither work authorization nor the legal source of TP healthcare entitlement.

## Cross-article education/residence consistency

HU-TP-02 and HU-EDU-01 agree that a recognized active TP beneficiary can study, that study rights do not equal a study residence permit, and that active TP is excluded from the ordinary study-purpose permit under the researched residence rule. HU-EDU-01 does not say a study permit is required for enrolment.

## Cross-article housing/support consistency

HU-HOUSING-01, HU-EMP-01 and HU-SOC-01 keep employer housing subsidy payable to the employer, municipal/provider funding separate from direct household cash, national direct rent/deposit benefit unestablished, TP subsistence cash distinct from housing subsidy, and municipal assistance local and conditional.

## Blocked-area leakage audit

Aggregate readiness remains **3 Ready, 17 Ready with qualification, 4 Blocked**. This review does not alter research readiness.

### `protection-scheme`

HU-TP-01 is intentionally absent. The other six drafts do not give a definitive answer to the unresolved military-obligation eligibility conflict for the affected Ukrainian applicants. **No leakage; remains Blocked.**

### `protection-application`

HU-TP-01 is intentionally absent. No other article gives an end-to-end TP application, no-document, walk-in, border or remote procedure. Mention of formal filing in employment, education or health is limited to its specific supported legal effect and does not become application instructions. **No leakage; remains Blocked.**

### `residence-filing`

HU-TP-02 keeps active-TP filing, pending-after-expiry stay/work bridge, and coexistence/termination mechanics unresolved. It does not advise that filing before 4 March 2028 ensures continued stay or work. **No leakage; remains Blocked.**

### `housing-initial`

HU-HOUSING-01 explicitly states the first-arrival intake gap and presents only general homelessness/night-shelter services with their limits. **No leakage; remains Blocked.**

## Freshness checks before publication

No external refresh was performed. Recheck only the time-sensitive issues already identified in the Hungary research: the TP military-obligation eligibility conflict; OIF's stale 2027/2028 validity page; TP appointment, no-document and walk-in procedure; TP-to-ordinary-residence transition and any post-4 March 2028 rules; employment-purpose nationality list and guest-worker suspension; EU Blue Card thresholds; minimum and guaranteed wage rates; NFSZ vacancies/programmes; TAJ-less provider workflow; 112/1830 operational guidance; first-arrival accommodation route; employer housing-support programme and end date; education language-support schemes; Felvi and scholarship cycles; TP subsistence amount/form/channel; and Family Support Act rates/status scope.

## Final disposition

- **PASS:** HU-TP-02, HU-EMP-01, HU-EMP-02, HU-HEALTH-01, HU-EDU-01, HU-SOC-01.
- **CORRECTION:** HU-HOUSING-01.
- **BLOCKER:** none at draft level.
- **Exact correction count:** 1.
- **Exact blocker count:** 0.
- **Overall recommendation:** **READY FOR CORRECTION PASS.** Do not integrate until the correction pass and post-correction review are complete.
- **Draft scope:** exactly 7 reviewed drafts; 7/7 present. HU-TP-01 is intentionally absent and is not a defect.
- **Source entries:** 97 total; counts by article are given above.
- **Article UUID audit:** all seven IDs are UUIDv5 and unique; no derivation recipe is defined in the scoped repository files.
- **Source UUID audit:** 97/97 deterministic UUIDv5, unique; 0 collisions and 0 cross-article ownership violations.
- **Research-source ownership:** 97 entries map to their owning package; one exact URL mismatch, HU-HOUSING-01-S03, is the correction.
- **Citation audit:** 861 inline citation tokens across languages; 0 orphan citations and 0 unused editorial sources.
- **Translation parity:** material claims and citation sets are parallel across EN / UK / RU; all remain `NeedsReview`.
- **Research blockers:** `protection-scheme`, `protection-application`, `residence-filing` and `housing-initial` remain blocked; aggregate readiness remains 3 Ready / 17 Ready with qualification / 4 Blocked.
- **Compare:** not authorized. Development-only integration is not authorized until correction and post-correction review. Production completeness is not claimed.

## Validation

- Created only `docs/content-drafts/hungary-v1/EDITORIAL_REVIEW.md`.
- No draft or research file was modified; no catalog, runtime, test or localization file was changed.
- The seven article-review table has exactly seven rows; the reviewed set contains exactly seven Hungary drafts.
- Source counts sum to 97. Deterministic source UUID, citation ownership, source inventory, translation citation parity and blocked-area audits are recorded above.
- No external sources were added. Aggregate blocked aspects remain unchanged.
- `git diff --check` is required before commit. No `dotnet test` was required.
