# Ireland Missing Core Review 01

**Review date:** 15 November 2026
**Scope:** editorial, evidence-boundary, source-map, UUID and Compare review of the three missing Ireland Core drafts. No new web research was conducted and no Production data was changed.

## IE-TP-01

**Result: PASS**

**Translations:** EN PASS; UK PASS; RU PASS.

**Source review:** 11 owner-specific records and 11 unique HTTPS URLs. All source IDs reproduce as UUIDv5 values using the Article ID as namespace and the local source key as name. Each source is cited in all three languages, resolves within this article, and supports the adjacent claim. The qualified contact route for a person already in Ireland is identified as a query contact, not as filing or grant. No cross-owner source token was found.

**Changes:** None.

**Evidence boundaries:** Preserved. The text does not invent an in-country §237 application form, office, appointment, checklist or walk-in route; it does not turn broad eligibility into automatic approval. The 4 March 2028 EU scheme date is kept separate from individual Irish permission and document validity. The cohort-specific ISD instruction to 4 March 2027 is not extended to 2028.

## IE-TP-02

**Result: PASS**

**Translations:** EN PASS; UK PASS; RU PASS.

**Source review:** 10 owner-specific records and 10 unique HTTPS URLs. All source IDs reproduce deterministically, resolve within this article and are used across EN / UK / RU. Employment, study and family routes are handled separately. The family-policy wording is qualified for the unresolved §60 / §237 interaction. TPTS is explicitly a separate route for eligible existing beneficiaries. No cross-owner source token was found.

**Changes:** None.

**Evidence boundaries:** Preserved. Ordinary TP-holder in-country filing, departure requirements and pending stay/work rights remain unconfirmed. The general ISD statement that filing does not extend current permission is not generalized into a TP-specific pending rule. TPTS criteria are described as core criteria, not a full checklist; filing is not described as conversion or as creating pending rights. No automatic permanent-residence entitlement or TP time credit is claimed.

## IE-HOUSING-01

**Result: REVISED**

**Translations:** EN REVISED; UK REVISED; RU REVISED.

**Source review:** 15 owner-specific records and 15 unique HTTPS URLs. All source IDs reproduce deterministically, are unique, resolve within this article and are cited in all three languages. Initial accommodation, RTB rental guidance, HAP, Rent Supplement and Additional Needs Payment claims are linked to sources that support the bounded statements. No cross-owner source token was found.

**Changes:** Clarified in all languages that only eligible new arrivals who register for TP from 10 November 2025 and need State accommodation may be offered up to 30 days, subject to current policy and availability. Reworded HAP as a housing-support scheme for eligible private renters, rather than describing it as separate from ordinary private rent. Softened the landlord wording to say that a tenancy offer is not guaranteed, without making an unsupported categorical statement about a landlord’s legal duty. The source mapping and citation tokens did not change.

**Evidence boundaries:** Preserved. No place, chosen centre, minimum stay, long-term housing, social housing, HAP, Rent Supplement, deposit payment or universal no-place fallback is promised. Homelessness services are identified as a separate information route, not an extension of TP accommodation.

## Translation review

| Result | Count | Languages / article status |
| --- | ---: | --- |
| PASS | 6 | EN / UK / RU for `IE-TP-01` and `IE-TP-02` |
| REVISED | 3 | EN / UK / RU for `IE-HOUSING-01` |
| BLOCKED | 0 | — |

Across all nine translations, the dates, amounts, conditions, responsible bodies, uncertainty and route distinctions match. The correction was applied symmetrically. Source-token sets match each article's source map in every language.

## Source gate

- **Planned owner-specific source records:** 36 — all 36 mappings valid.
- **Unique official URLs:** 35; all use HTTPS.
- **Exact URLs already present in the current Production registry:** 5.
- **Selected URLs not present in the current Production registry:** 30.
- **Cross-owner bindings in new drafts:** 0.
- **Source UUID conflicts with Production:** 0.
- **Orphan or unresolved citation tokens:** 0; every mapped source is cited in EN / UK / RU.

The five existing URL matches belong to other owners; the new drafts use separate owner-specific IDs for each source record:

| Existing URL | Existing owner or owners in the current registry |
| --- | --- |
| [ISD Ukraine FAQ](https://www.irishimmigration.ie/faqs-for-ukraine-nationals-and-residents-of-ukraine/) | `IE-EMP-02` |
| [International Protection Act 2026, §237](https://www.irishstatutebook.ie/eli/2026/act/9/section/237/enacted/en/index.html) | `IE-EMP-02`, `IE-HEALTH-01`, `IE-EDU-01`, `IE-SOC-01` |
| [Consolidated EU Decision 2026/1912](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02026D1912-20260804) | Five non-target Article IDs: `115cbf25-5ece-5cc9-8bde-c3dabcbda47f`, `1bb2a2b8-cfe0-5595-8155-138443fade17`, `4a1e4c9b-a09c-5c92-bc37-2c9a15b459bc`, `c8c06066-d3cd-55ce-88b0-3499384eece7`, `ffd32dca-56d8-5dcf-b995-7522a159010a` |
| [Designated Accommodation Centres](https://www.gov.ie/en/department-of-justice-home-affairs-and-migration/publications/designated-accommodation-centres/) | `IE-SOC-01` |
| [Additional Needs Payment](https://www.gov.ie/en/service/80b24c-exceptional-needs-payments-swa/) | `IE-SOC-01` |

The previous Source Gate counted four exact matches. The current registry check also found the Additional Needs Payment URL under `IE-SOC-01`, making five. This is an inventory-count discrepancy only; the new `IE-HOUSING-01` record has its own source ID and no existing owner ID is reused.

## Article IDs

| Article | Article ID | UUID validation |
| --- | --- | --- |
| `IE-TP-01` | `51fc8ed8-123d-5a6f-b4fa-16cd2e41f3bf` | UUIDv5, URL namespace name `refugeenavigatoreurope:ireland:IE-TP-01`; unique and absent from current Production Article IDs |
| `IE-TP-02` | `6bca20f8-b06d-5068-bd45-c78155111354` | UUIDv5, URL namespace name `refugeenavigatoreurope:ireland:IE-TP-02`; unique and absent from current Production Article IDs |
| `IE-HOUSING-01` | `d0d74e2e-9a2a-50bc-970f-605997565456` | UUIDv5, URL namespace name `refugeenavigatoreurope:ireland:IE-HOUSING-01`; unique and absent from current Production Article IDs |

All 36 source UUIDs are UUIDv5 values generated with their owning Article ID as namespace and the exact local key (`<Article Code>-Snn`) as name. The 36 IDs are unique, owner-specific, absent from the current Production source registry, and do not conflict with each other. The new Article IDs do not conflict with existing catalog IDs.

## Evidence ceilings

| Evidence ceiling | Review result | Publication limit retained |
| --- | --- | --- |
| New §237 first application from inside Ireland | **PRESERVED** | No universal filing route, form, office, appointment or checklist asserted; official mailbox is presented as a query contact only. |
| Irish permission/document validity after 4 March 2027 | **PRESERVED** | EU date is not used as an individual Irish permission or document extension. |
| Ordinary-route pending stay/work rights | **PRESERVED** | No general pending or bridging right is inferred; route-specific uncertainty remains explicit. |
| Housing fallback when no place is available | **PRESERVED** | No hotel, local-authority placement or universal referral is promised; homelessness contacts remain separate. |

## Compare readiness

**Owner coverage: 8/8.** All 24 aspects were reclassified from the scoped Ireland assessment and the existing evidence. The three existing READY findings remain READY; the other findings are qualified to preserve their documented limits.

### READY — 3

- `employment-vacancies` — `IE-EMP-01`
- `employment-institutions` — `IE-EMP-01`
- `work-minimum-pay` — `IE-EMP-02`

### READY WITH QUALIFICATION — 21

- `employment-assistance-conditions` — `IE-EMP-01`
- `work-permission`, `work-employer-duties` — `IE-EMP-02`
- `healthcare-access`, `healthcare-proof`, `healthcare-urgent` — `IE-HEALTH-01`
- `education-school-enrolment`, `education-pupil-language`, `education-higher-admission` — `IE-EDU-01`
- `social-subsistence`, `social-application`, `social-child-benefits` — `IE-SOC-01`
- `protection-scheme`, `protection-application`, `protection-dates` — `IE-TP-01`
- `residence-routes`, `residence-conditions`, `residence-filing` — `IE-TP-02`
- `housing-initial`, `housing-private-rental`, `housing-financial-support` — `IE-HOUSING-01`

### RESEARCH REQUIRED — 0

The open procedures remain evidence ceilings, and the drafts do not make claims beyond those ceilings. They do not block the qualified Compare statements.

## Remaining issues

- The four evidence ceilings above remain unresolved at the level of detailed individual procedure. They are preserved as limitations and do not block the bounded Core drafts or qualified Compare facts.
- The Source Gate's earlier count of four existing URLs is reconciled to five in this review; the additional match is Additional Needs Payment under `IE-SOC-01`.
- The drafts remain `NeedsReview`. This review clears their content and source mapping for the next integration step; it does not change Production status, visibility or catalog data.

## Verdict

**READY FOR MISSING CORE PRODUCTION INTEGRATION**

All three articles are safe for integration with the qualifications recorded here. The housing wording was tightened in EN / UK / RU. Article and source IDs, citations, translation parity, evidence ceilings and 8/8 Compare ownership pass review. No Production data was changed.
