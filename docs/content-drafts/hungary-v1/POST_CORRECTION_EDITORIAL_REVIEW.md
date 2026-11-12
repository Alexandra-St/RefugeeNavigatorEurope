# Hungary v1 — Post-correction editorial review

**Review date:** 12 November 2026  
**Baseline:** `ae374aa3a21bb86412316af3a4fe10d0cfd71ec0`  
**Content correction commit:** `ae374aa3a21bb86412316af3a4fe10d0cfd71ec0`  
**Scope:** Exactly the seven current Hungary drafts, their matching focused research packages, the initial Hungary editorial review and Hungary aggregate readiness. No external research was used. Belgium and Finland post-correction reviews were used only for structure and audit depth.

## Recommendation

**READY FOR DEVELOPMENT-ONLY CATALOG INTEGRATION.** All seven reviewed drafts pass after resolution of the single bounded source URL correction. This recommendation applies only to Development-only catalog integration for these seven bounded draft scopes. It does not authorize Compare, comparison facts/placeholders, or production deployment.

All translations remain `NeedsReview`. Hungary is **not Production-ready as a complete country**. HU-TP-01 remains intentionally absent. Four aggregate research blockers remain unchanged: `protection-scheme`, `protection-application`, `residence-filing`, and `housing-initial`. No research readiness was upgraded.

## Correction verification

The only finding in the initial review was the URL on `HU-HOUSING-01-S03` (UUID `c7daa0bb-96e5-5d55-a419-8a53613f0608`, research source `HU-HOUSING-S03`). It is **RESOLVED**:

- Current draft URL: `https://www.oif.gov.hu/menedekeseknek-nyujtott-ellatasok-es-tamogatasok`.
- Owning housing research inventory URL: `https://www.oif.gov.hu/menedekeseknek-nyujtott-ellatasok-es-tamogatasok`.
- Exact match: yes.
- Local key, source UUID, research source ID, authority, title and source count are unchanged.
- HU-HOUSING-01 has 11 source entries. EN / UK / RU article bodies and citation sets/counts were not changed by the URL-only correction.

## Article disposition

| Draft | Final disposition | Result |
|---|---|---|
| `HU-TP-02.md` | **PASS** | Qualified ordinary residence-route overview; no automatic conversion or transition, active-TP filing, or post-expiry stay/work bridge is promised. `residence-filing` remains blocked. |
| `HU-EMP-01.md` | **PASS** | VMP, EURES and NFSZ routes stay distinct; recognized age-16+ `menedékes` service-seeker registration is distinct from `álláskereső`, work authorization and unemployment allowance. Pending-applicant registration is not asserted. |
| `HU-EMP-02.md` | **PASS** | Formal TP filing is the applicant work-right trigger; employer-notification uncertainty stays qualified; NAV 08E, TAJ and work permission remain distinct. Contract and 2026 wage details match the reviewed research. |
| `HU-HEALTH-01.md` | **PASS** | Beneficiary and applicant care routes remain distinct from ordinary insurance. TAJ limitations, repealed decree, qualified TAJ-less process, emergency numbers and care limits are preserved. |
| `HU-HOUSING-01.md` | **PASS** | URL correction is resolved; first-arrival route remains explicitly unverified and blocked. General shelter, renting, address reporting, deposit rules and support recipients are correctly bounded. |
| `HU-EDU-01.md` | **PASS** | School access from filing, catchment, missing-record handling, local language support, meal conditions, higher-education boundaries and study-permit distinction are preserved. |
| `HU-SOC-01.md` | **PASS** | Recognized-beneficiary subsistence route, amounts, OIF/employment-office roles, scheme-specific cooperation and separate family/insurance benefits are correctly bounded. |

## Metadata, source ownership and citation audit

- **Scope and metadata:** Exactly seven drafts reviewed; HU-TP-01 is absent as intended. Each draft has the expected Hungary country/category, matching package path and comparison aspects; EN / UK / RU sections and `NeedsReview` status are present. Each language has a `Claim coverage and editorial exclusions` section.
- **Source counts:** HU-TP-02 18; HU-EMP-01 14; HU-EMP-02 14; HU-HEALTH-01 14; HU-HOUSING-01 11; HU-EDU-01 12; HU-SOC-01 14. **Total: 97.**
- **Source UUIDs:** All 97 reproduce as UUIDv5 using the owning Article ID as namespace and exact local source key as name. All keys use their owning article prefix. All UUIDs are version 5 and globally unique; collisions: **0**; cross-article ownership violations: **0**.
- **Research ownership:** All 97 entries map to source IDs in their matching focused package, with materially matching authority/title and HTTPS inventory URLs. No unofficial or unauthorized cross-topic source borrowing was found. The prior HU-HOUSING-S03 URL mismatch is gone.
- **Citations:** 861 inline citation tokens total, 287 per language. Every citation resolves within its owning article; orphan citations: **0**; unused editorial source entries: **0**. No raw `HU-…-Sxx` source IDs are used as citations in public article bodies. Citation labels materially match source identity and language citation sets are equivalent.
- **Article IDs:** All seven are valid UUIDv5 values and unique. The reviewed repository scope does not define a separate Article ID name-generation recipe.

## Translation parity audit

EN / UK / RU were rechecked for status, eligibility, amounts, dates, ages, authorities, document requirements, negative and “not established” statements, practical instructions and blocked-area qualifications. No language adds an entitlement, removes a material condition, turns a possibility into a guarantee, introduces an unsupported mandatory document, changes a material number/date/age or hides uncertainty. Citation sets are parallel. The URL correction affects shared source metadata only. Translation status remains `NeedsReview` in all drafts.

## Article-specific findings

### HU-TP-02

No automatic TP-to-ordinary-residence conversion or automatic post-4-March-2028 transition is claimed. Active-TP filing and any lawful-stay/work bridge after expiry remain unestablished. Active TP exclusions for EU Blue Card and study permit are retained. National Card and Hungarian Card routes remain conditional, and National Residence Card versus EU Residence Card time-counting is distinguished. `residence-filing` remains blocked.

### HU-EMP-01

VMP / EURES / NFSZ roles remain distinct. The recognized beneficiary age-16+ route is `szolgáltatást kérő`; this is not `álláskereső`. Pending-applicant service registration is not established. Registration does not grant work authorization or unemployment allowance. No TP-specific document checklist or guaranteed walk-in/online procedure is invented.

### HU-EMP-02

Recognized beneficiaries and formally filed applicants may work permit-free under the researched rule; online preparation is not formal filing. Recognized-beneficiary employer notification is stated, while the pending-applicant channel/deadline gap remains qualified. NAV 08E is used. TAJ does not equal TP or work permission.

2026 minimum wage values are correct: HUF 322,800/month, 74,210/week, 14,850/day, 1,856/hour. Guaranteed wage minimum values are correct: HUF 373,200/month, 85,800/week, 17,160/day, 2,145/hour. The higher floor depends on the job's qualification requirement, not merely the worker's diploma.

### HU-HEALTH-01

Beneficiary and applicant health-care routes remain distinct; TP healthcare is not ordinary insurance. TAJ is not TP, and the physical card is not the source of TP entitlement. Decree 171/2022 remains identified as repealed/legacy, and the TAJ-less workflow remains qualified. 112 and 1830 are distinguished; 1830 is described as weekdays 16:00–08:00 and weekends/holidays 24 hours. No universal free-care, interpreter or EHIC claim is made.

### HU-HOUSING-01

Correction is resolved. `housing-initial` remains visibly blocked, with no TP first-night intake procedure invented. BMSZKI is general homelessness support, 18:00–08:00, free but with no guaranteed bed; no national hotline is invented. Lease, OIF address reporting and `lakcímkártya` are distinct. No hard statutory three-month deposit cap is claimed; deposit and advance rent are distinguished. No national direct rent/deposit benefit is asserted. Employer housing subsidy is paid to the employer. Legacy BOK/railway/transit routes are not revived.

### HU-EDU-01

Pending children's school access begins on formal TP filing. Catchment and accommodation routing are stated without a universal `lakcímkártya` prerequisite. Missing Ukrainian records do not mean automatic refusal. No current fixed five-hour language entitlement is claimed; Ukrainian online school is not a substitute for Hungarian compulsory education. Six-month meal assistance requires an application. Admission, credential recognition and university assessment/OH-MEIK recognition are distinct. Recognized TP funding-category eligibility is not a guaranteed funded seat or free tuition; pending-applicant funding is not inferred. Study rights do not require a study residence permit.

### HU-SOC-01

Regular subsistence support is limited to recognized `menedékes`; pending applicants are not included. Amounts are HUF 22,800 adult / HUF 13,700 minor; OIF decides and pays by the 15th, while the employment office has a separate cooperation role. No form/channel or no-bank payment method is invented. Age-6+ appearance and age-16+ service-seeker conditions remain. The 45-day suitable-job rule is scheme-specific; employment and pension effects are stated without inventing a comprehensive resource test. No payment guarantee through 4 March 2028. TP alone is not used to infer family allowance, maternity support, GYES or GYET; CSED/GYED remain insurance-based.

## Cross-article consistency

- **Status and documents:** Pending applicant, recognized `menedékes`, formal filing, interim applicant evidence, beneficiary card, accommodation evidence, `lakcímkártya`, TAJ number/card, `szolgáltatást kérő` and `álláskereső` remain distinct.
- **Address:** Accommodation address is not treated as a private lease, `lakcímkártya` or automatic benefit eligibility.
- **Work:** HU-EMP-01 / HU-EMP-02 / HU-SOC-01 agree that service registration is not work authorization; pending work rights do not establish pending service-seeker registration; the 45-day condition belongs only to TP subsistence support; unemployment allowance is separate.
- **TAJ:** HU-EMP-02 / HU-HEALTH-01 agree that employer registration may arrange ordinary insurance administration but does not mean TP automatically creates TAJ. A physical card is neither work authorization nor TP healthcare entitlement.
- **Education/residence:** HU-TP-02 / HU-EDU-01 agree that active recognized TP permits study under the researched rule, study rights do not equal a study residence permit, and active TP excludes grant of the ordinary study-purpose permit.
- **Housing/support:** HU-HOUSING-01 / HU-EMP-01 / HU-SOC-01 agree that employer subsidy is not tenant cash, municipal/provider funding is not TP subsistence cash, and no national direct rent/deposit benefit is established.

## Blocked-area audit

The aggregate readiness remains **3 Ready, 17 Ready with qualification, 4 Blocked**. No aspect was upgraded.

- **`protection-scheme`:** No other draft resolves the military-obligation eligibility conflict. HU-TP-01 remains intentionally absent. No leakage; still Blocked.
- **`protection-application`:** No other article provides an end-to-end TP application/no-document/walk-in/border procedure. HU-TP-01 remains intentionally absent. No leakage; still Blocked.
- **`residence-filing`:** HU-TP-02 keeps active-TP filing and pending-after-expiry rights unresolved. No leakage; still Blocked.
- **`housing-initial`:** HU-HOUSING-01 discloses the missing TP first-arrival intake route and describes only qualified general shelter options. No leakage; still Blocked.

## Remaining research blockers

Exactly these four remain blocked: `protection-scheme`, `protection-application`, `residence-filing`, `housing-initial`. Readiness counts are unchanged at 3 Ready / 17 Ready with qualification / 4 Blocked. HU-TP-01 remains intentionally absent. Hungary remains excluded from Compare; no comparison facts or placeholders are authorized.

## Final disposition

- **PASS:** HU-TP-02, HU-EMP-01, HU-EMP-02, HU-HEALTH-01, HU-HOUSING-01, HU-EDU-01, HU-SOC-01.
- **CORRECTION:** none.
- **BLOCKER:** none at article level.
- **Exact counts:** 7 PASS, 0 CORRECTION, 0 BLOCKER.
- **Correction:** HU-HOUSING-S03 URL mismatch is resolved; 97 source entries remain.
- **Overall:** **READY FOR DEVELOPMENT-ONLY CATALOG INTEGRATION.** This does not authorize Compare or production deployment and does not claim complete-country Production readiness.
- All translations remain `NeedsReview`; HU-TP-01 remains absent; four aggregate research blockers remain blocked.

## Checks

- This review covers exactly seven drafts; the disposition table has exactly seven rows.
- Source counts sum to 97; UUIDv5 ownership, source package membership, exact URLs, citation resolution/use and EN / UK / RU parity were rechecked.
- Corrected HU-HOUSING-S03 URL exactly matches its research inventory entry; `housing-initial` remains blocked.
- Four aggregate blockers and the 3 / 17 / 4 readiness counts are unchanged; HU-TP-01 is absent.
- No external research was used. No draft, initial review, research, catalog, runtime, test or localization file was changed.
- `git diff --check` must pass before commit. No `dotnet test` was required.
