# Ireland Missing Core Drafting 01

**Date:** 15 November 2026
**Scope:** Draft three missing Ireland Core owners in EN / UK / RU using the existing Ireland research packages and targeted evidence boundaries. No Production catalog or other runtime data was changed.

## Created articles

| Code | Article ID | Category | Languages | Planned owner source records | Unique URLs |
| --- | --- | --- | --- | ---: | ---: |
| `IE-TP-01` | `51fc8ed8-123d-5a6f-b4fa-16cd2e41f3bf` | `temporary-protection` | EN / UK / RU | 11 | 11 |
| `IE-TP-02` | `6bca20f8-b06d-5068-bd45-c78155111354` | `temporary-protection` | EN / UK / RU | 10 | 10 |
| `IE-HOUSING-01` | `d0d74e2e-9a2a-50bc-970f-605997565456` | `housing` | EN / UK / RU | 15 | 15 |

## Source inventory

The article source sets contain **36 owner-specific source records** and **35 unique official HTTPS URLs**. The only URL shared across the new owners is International Protection Act 2026 §237, which has separate deterministic source IDs under `IE-TP-01` and `IE-TP-02`. The current Production registry already contains records for five of the selected exact URLs: §237, the ISD Ukraine FAQ, the consolidated EU decision, Designated Accommodation Centres, and Additional Needs Payment. Each new article still has its own deterministic owner-bound source ID; no existing ID is reused. The previous Targeted Research 02 summary listed four existing URL matches, so the Additional Needs Payment URL is recorded here as the fifth match from the current registry check. Production `official-sources.json` was not changed.

### `IE-TP-01` — 11 records / 11 URLs

ISD Temporary Protection information; ISD Ukraine FAQs; International Protection Act 2026 §237; S.I. No. 244/2026 commencement order; ISD required documents; Government of Ireland legacy Temporary Protection page (explicitly identified as unconfirmed for the new process); Department of Justice Temporary Protection and Ukraine collection; IPO help/contact page; ISD TP registration renewal; corrected consolidated EU Decision 2026/1912; Dáil statement of 30 September 2026.

### `IE-TP-02` — 10 records / 10 URLs

International Protection Act 2026 §237; ISD changing-permission guidance; DETE immigration and employment permits; ISD student-permission guidance; revised non-EEA family policy; ISD family-route guidance; ISD TPTS opening notice; Government of Ireland “A New Permission” outline; Oireachtas PQ 430 answer; Oireachtas PQ 195 answer.

### `IE-HOUSING-01` — 15 records / 15 URLs

Current Ukraine accommodation policy; designated accommodation centres; accommodation refusals policy; UCTAT contact page; homelessness services page; RTB rental-law changes; RTB rent-setting guidance; RTB enforceable-breaches/deposit guidance; RTB tenancy-agreement guidance; RTB tenant checklist; HAP service; social-housing eligibility policy page; Rent Supplement service; Rent Supplement operational guidelines; Additional Needs Payment service.

## Evidence boundaries

- **New §237 application from inside Ireland:** current public sources do not publish a complete universal procedure or checklist. The draft describes the supported border contact and gives the official Ukraine query mailbox as a contact route only.
- **Irish TP documents after 4 March 2027:** the EU framework date is stated separately from individual Irish permission/certificate/IRP validity. The draft retains only ISD’s cohort-specific 4 March 2027 instruction and does not promise automatic validity to 2028.
- **Ordinary residence application pending rights:** route-specific conditions are separated. The draft does not infer universal in-country filing, departure, bridging residence, continued work, or TP continuation from filing. TPTS is described separately with its published core conditions.
- **Initial accommodation if no place is available:** the maximum-30-day model is qualified by availability. UCTAT and homelessness contacts are not represented as an application process or guaranteed placement; no universal fallback is promised.
- **Housing support:** HAP, Rent Supplement and Additional Needs Payment are not treated as automatic benefits or as interchangeable schemes.

## Translation validation

| Article | EN / UK / RU | Review result |
| --- | --- | --- |
| `IE-TP-01` | Present | Matched dates, categories, qualification language, procedural limits and citations across languages. |
| `IE-TP-02` | Present | Matched route-specific limits, TPTS thresholds, pending-right caveats and citations across languages. |
| `IE-HOUSING-01` | Present | Matched availability, rental limits, scheme conditions and non-guarantee wording across languages. |

All nine translations are drafts marked `NeedsReview`; a separate editorial and translation review remains necessary before production integration.

## Compare readiness after drafting

All 24 aspects now have conceptual owners across the eight Core articles. The three previously `READY` existing-owner aspects remain `READY`; the other 21 aspects are classified `READY WITH QUALIFICATION` because their approved wording has conditions, availability limits, or bounded evidence ceilings. No aspect is classified `RESEARCH REQUIRED` for draft-level Compare wording.

### `READY` — 3

- `employment-vacancies` — `IE-EMP-01`
- `employment-institutions` — `IE-EMP-01`
- `work-minimum-pay` — `IE-EMP-02`

### `READY WITH QUALIFICATION` — 21

- `employment-assistance-conditions` — `IE-EMP-01`
- `work-permission`, `work-employer-duties` — `IE-EMP-02`
- `healthcare-access`, `healthcare-proof`, `healthcare-urgent` — `IE-HEALTH-01`
- `education-school-enrolment`, `education-pupil-language`, `education-higher-admission` — `IE-EDU-01`
- `social-subsistence`, `social-application`, `social-child-benefits` — `IE-SOC-01`
- `protection-scheme`, `protection-application`, `protection-dates` — `IE-TP-01`
- `residence-routes`, `residence-conditions`, `residence-filing` — `IE-TP-02`
- `housing-initial`, `housing-private-rental`, `housing-financial-support` — `IE-HOUSING-01`

### `RESEARCH REQUIRED` — 0

The four previously open research areas are bounded by explicit safe wording: do not assert an unpublished procedure, individual-document extension, ordinary-route pending right, or no-place housing fallback. A later fact must stay within those boundaries; a broader claim would require new evidence.

**Conceptual owner coverage: 8/8.** This is draft-level coverage only. Production remains at five Ireland articles / 15 translations, with Ireland hidden in Production; this task does not integrate or publish the new drafts.

## Validation

- Article IDs use the established URL-namespace UUIDv5 naming convention and are distinct from existing Ireland Article IDs.
- Source IDs use UUIDv5 with the owning Article ID as namespace and the exact local source key as name; records are unique and owner-specific.
- Every listed source uses an official HTTPS URL; every inline citation token resolves to a source in the same article’s source set; every source record is cited.
- EN / UK / RU sections and all nine language versions are present; the three expected aspect triples and 24/24 total aspects are preserved.
- No Production JSON, existing article, test, runtime, status or visibility file was changed.

## Verdict

**READY FOR MISSING CORE REVIEW**

The three missing Core owners are drafted in all requested languages with owner-bound source mappings. The four residual evidence gaps remain explicit publication limits rather than inferred permissions, document extensions or housing guarantees. Drafts are ready for review, not production integration.
