# Netherlands Refresh Review 01

As of: 16 November 2026

## Article decisions

| Article | Decision | Issues | Required edits |
| --- | --- | --- | --- |
| NL-TP-01 | APPROVE | EU scheme horizon, personal proof cohorts and January 2027 letter are separated; pending challenge rights remain qualified. | None. |
| NL-TP-02 | APPROVE | No universal post-TPD filing, pending-stay/work, or TPD time-credit rule is asserted. | None. |
| NL-EMP-01 | APPROVE | UWV support, local provision and EU Talent Pool access are bounded; no placement or funded-service guarantee. | None. |
| NL-EMP-02 | APPROVE | Employee route is cohort-specific; notice, proof and current wage date are stated; 2027 rates are not presented as current. | None. |
| NL-HEALTH-01 | APPROVE WITH EDITS | UK/RU service-coverage and patient-cost paragraphs were less complete than EN. | UK/RU aligned to EN on covered basic/additional services, contracted-provider limits, no patient contribution/deductible for covered RMO care, and first-contact GP/HAP routing. |
| NL-HOUSING-01 | APPROVE | National reception responsibility is distinct from eligibility, local assignment and capacity. Rent-benefit/guest change and contribution amounts are dated and bounded. | None. |
| NL-EDU-01 | APPROVE | School registration, local placement, 2026–27 amounts and 2027 deadline preserve eligibility and local-service limits. | None. |
| NL-SOC-01 | APPROVE WITH EDITS | Opening leefgeld wording previously risked narrowing the eligible reception setting; host-family residents are included when they meet scheme conditions. | EN/UK/RU opening now includes qualifying municipal reception arrangements, including qualifying host-family stays. |

## Translation parity

**PASS after the two draft corrections above.** EN/UK/RU retain the same dates, amounts, age/household criteria, eligibility boundaries, exceptions, future-only status and legal uncertainty. The reviewed education 2026–27 tuition and DUO deadline, protection document cohorts, and social-assistance values match across languages. No translation strengthens a right beyond EN.

## Compare decisions

Thirteen KEEP, ten REVISE and one UNAVAILABLE. Every available fact remains owned by the Netherlands article specified in production. Revised text is in the Task 51 batch ledgers in EN/UK/RU; bindings below describe the integration target. Existing production status remains Approved unless specified.

| Aspect | Decision | Review result | Sources |
| --- | --- | --- | --- |
| protection-scheme | KEEP | Scheme scope/horizon retained with cohort and military-screening qualifications. | `e1f80033`, `4febe797`, `59f03221` |
| protection-application | KEEP | Referral/triage and appeal route remain conditional; no guaranteed outcome. | `e1f80033`, `be301190`, `185885fd` |
| protection-dates | REVISE | Replace Spain/TIE answer with Netherlands scheme horizon and IND document cohorts; see critical checks. | `e1f80033`, `5da562ac`, `9422da1c`, `59f03221` |
| residence-routes | KEEP | Policy transition remains prospective; ordinary routes remain permit-specific. | `979a64e2`, `fda6d123`, `249d9618` |
| residence-conditions | KEEP | TPD-time credit remains unresolved, not promised. | `9c54de3d` |
| residence-filing | UNAVAILABLE | Keep Draft, unavailable, empty source list and neutral fallback. | None; evidence ceiling requires no binding. |
| employment-assistance-conditions | KEEP | Local services vary; no universal eligibility or job/training guarantee. | `6cbd665c` |
| employment-institutions | REVISE | Add UWV registration certificate and three-month renewal detail with municipal-registration condition. | `1690f9d6` |
| employment-vacancies | REVISE | Bound EURES/Europass EU Talent Pool pilot access; no job guarantee. | `1690f9d6` |
| work-employer-duties | KEEP | Covered route duties and two-working-day timing remain bounded. | `583fa01b`, `0842cba6` |
| work-minimum-pay | KEEP | July 2026 rates remain in force as of date; January 2027 values are future-only. | `1f249289` |
| work-permission | KEEP | Covered employee exemption; self-employment and cohort limits retained. | `ba44d821`, `583fa01b` |
| healthcare-access | REVISE | Use current RMO practical rule and qualified employee Zvw distinction; remove superseded UWV source from this binding. | `41f41389`, `bba4ed15`, `98234e00`, `35ba0b19`, `5f8e832a` |
| healthcare-proof | KEEP | COV check and provider-side SOV distinction remain accurate. | `bba4ed15`, `32810604`, `9b608d08` |
| healthcare-urgent | KEEP | 112/GP/HAP instructions remain local-route qualified. | `e59632da`, `acc9e0c2` |
| housing-initial | REVISE | State national municipal responsibility, interim triage reception and local capacity limits; add Housing-owned Act source. | Add Housing-owned Temporary Act record; `39581114`, `f61d03f2` |
| housing-private-rental | KEEP | Tenant protections do not imply access or special TPD housing entitlement. | `af04791b` |
| housing-financial-support | REVISE | Separate rent benefit from reception; add verified 2026 cap and 2027 co-resident pages as owner records. | `870388b4`, `6290793b`, `d53fa054`; add two Dienst Toeslagen records |
| education-school-enrolment | REVISE | State school registration regardless of BRP/status proof; local placement varies. | `82c68617` |
| education-pupil-language | KEEP | Newcomer language provision is possible, not guaranteed. | `6dae1849`, `82c68617` |
| education-higher-admission | REVISE | Preserve institution decisions, 2026–27 tuition, TPD student-finance exclusion and conditional DUO remission/deadline. | `400b5488`, `c917869f` |
| social-application | KEEP | Municipal leefgeld enquiry route does not promise ordinary bijstand/MOP. | `b4d671c9` |
| social-child-benefits | REVISE | Distinguish kinderbijslag and kindgebonden budget; add asset-limit owner source for exact 2026 caps. | `b4d671c9`, `9cec202a`, `69aa017b`; add Dienst Toeslagen asset-limit record |
| social-subsistence | REVISE | Preserve 1 Aug 2026 values, household scope, overlapping income rules and municipal calculation caveat. | `b4d671c9`, `e5c28f7f` |

## Critical checks

- **protection-dates:** Production’s Netherlands fact currently contains Spain-specific Order INT/96/2026/TIE content. Replace it completely. The safe replacement describes the EU horizon through 4 March 2028 separately from personal proof; covered non-Ukrainian holders’ O-document date is 4 March 2027, with a letter IND says it will send in January 2027. It does not claim the letter was sent. Bind Netherlands IND/EU sources and remove the old Spanish-owned binding `1b41070c-31a5-50d2-8ef9-a2c2becbbc3d`.
- **residence-filing:** Correctly unavailable. Keep `Status: Draft`, `IsAvailable: false`, empty `OfficialSourceIds`, and the fallback “Not established in the reviewed sources.” Do not infer a universal in-country filing route or pending rights.
- **work-minimum-pay:** KEEP. July 1 2026 gross hourly rates are current on November 16; announced January 1 2027 rates are not yet effective.
- **RMO/Zvw:** The practical guidance is that a person actually covered by RMO/code 46 need not buy an ordinary policy to receive covered care, including if working. Formal employee Zvw liability and income-related contributions are distinct; individual/self-employed/status-transition cases remain qualified. RMO does not mean all care/providers are covered or that all care is free. Keep this distinction; exclude superseded UWV insurance source `e1edd80d-2530-57e4-8e55-637382f1dfd1` from `healthcare-access` bindings.
- **housing/reception:** National municipal responsibility and qualifying access do not establish a chosen placement, capacity, duration or local transfer. Interim reception in specified pre-registration triage cases is qualified. Keep rent benefit, reception and personal contribution separate.
- **education:** School enrolment does not depend on BRP/status proof in the stated Ukraine-specific guidance; placement is local. Tuition/statutory-rate and DUO remission claims are limited to their stated student/course criteria and academic year.
- **social assistance:** Leefgeld and contribution provisions overlap in the official descriptions. Drafts disclose the overlap and direct household-specific calculation to the municipality; do not convert this into ordinary bijstand or universal MOP eligibility.

## Source disposition review

Task 51’s four batch ledgers reconcile to **52 CURRENT, 3 SUPERSEDED, 3 LEGACY, 0 BROKEN, 0 UNCLEAR (58 total)**. A prior summary incorrectly counted 2 LEGACY and 1 UNCLEAR; the final report now records the corrected total. Superseded/legacy material must not support current claims. Retain the superseded UWV source as registry history if the schema has no disposition field, but remove it from the current healthcare Compare binding. Retain historical education sources only for historical context.

Source owner and binding issues to resolve during integration:

- The existing Temporary Ukraine Displaced Reception Act record `0770873e-4eb2-5d87-ae01-ce29e6a7cf02` is owned by HEALTH; do not bind it to Housing. Add a Housing-owned record for the Act.
- Add Housing-owned records for the reviewed Dienst Toeslagen 2026 rent-benefit change (`https://www.belastingdienst.nl/wps/wcm/connect/nl/huurtoeslag/content/huurtoeslag-verandert-vanaf-2026`) and co-resident rule (`https://www.belastingdienst.nl/wps/wcm/connect/nl/huurtoeslag/content/wie-telt-als-medebewoner`).
- Add a Social-owned Dienst Toeslagen source for 2026 child-budget asset limits (`https://www.belastingdienst.nl/wps/wcm/connect/bldcontentnl/belastingdienst/prive/toeslagen/kindgebonden-budget/maximaal-vermogen-kindgebonden-budget/`). The exact caps are €146,011 without a toeslagpartner and €184,633 with one.
- No source is needed for `residence-filing`; an empty list is the correct representation of the evidence ceiling.

Do not fabricate IDs for the four new records; assign IDs when creating the records. The `official-sources.json` schema has no status field, so current/superseded/legacy disposition must remain documented in the audit and must not be implied by changing unsupported schema fields.

## Cross-article consistency

- **TP ↔ Employment:** IND proof cohorts are separate from the UWV employee notification route; employer checks and the two-working-day timing do not create a general work right for every cohort.
- **TP ↔ Healthcare:** code 46/RMO eligibility is a defined healthcare cohort, not a synonym for all temporary-protection status; the drafts do not imply ordinary Zvw policy purchase for covered RMO care.
- **TP ↔ Housing:** BRP registration is part of ordinary reception and several proof routes, while specified triage cases can receive interim reception before registration; neither article promises an individual municipal placement.
- **Housing ↔ Social:** leefgeld, reception contributions and huurtoeslag are described as separate rules. Both social and housing drafts preserve the August 2026 contribution caps and distinguish the income threshold from household leefgeld calculations.
- **Education ↔ Social:** education text does not imply DUO student finance for TPD holders; child/family benefits remain separately conditional.

No unresolved cross-article contradiction was found after the draft corrections.

## LastUpdatedAt integration plan

Set `LastUpdatedAt` to `2026-11-16` for these seven substantively refreshed entries only:

- `244b1055-4d8c-55b2-820e-60ff018c3b1f` — NL-TP-01
- `757c1db6-b66b-54b9-ac14-d8a3da958ef5` — NL-EMP-01
- `6a077f79-89c2-55d6-81f3-9ba7279979c9` — NL-EMP-02
- `d5c4d5fa-3b0f-5cc2-865f-841bd40911d1` — NL-HEALTH-01
- `19dde97c-cb42-5a90-97f7-e344f2139d2e` — NL-HOUSING-01
- `7633a085-4555-5a8f-a9a8-747a0f6e0545` — NL-EDU-01
- `8d466e7d-93f7-596e-a44f-188e04396aa0` — NL-SOC-01

Leave NL-TP-02 `c5f5f833-0ab7-5e1a-9325-87a92b21d2ad` unchanged; its draft was complete and not substantively revised.

## Remaining evidence ceilings

Individual TPD eligibility, IND proof cohort, negative decision and appeal rights remain case-specific. The post-TPD transition is prospective; no general filing route, pending stay/work right, or TPD residence-time credit is established. Local reception capacity/placement and local course/service availability are not guaranteed. Benefit amounts depend on household circumstances and municipal/SVB/Toeslagen determinations. The social pages describe overlapping income rules that require municipal calculation. Housing 2027 co-resident treatment and minimum-wage figures must remain explicitly future-dated as of this review. These are safely qualified and do not block integration once the listed owner records and bindings are prepared.

## Production integration plan

Production JSON was inspected and **not changed** in this review.

- **Core articles to replace:** NL-TP-01, NL-EMP-01, NL-EMP-02, NL-HEALTH-01, NL-HOUSING-01, NL-EDU-01, NL-SOC-01. Keep NL-TP-02 as-is.
- **Compare facts to revise (10):** `protection-dates`, `employment-institutions`, `employment-vacancies`, `healthcare-access`, `housing-initial`, `housing-financial-support`, `education-school-enrolment`, `education-higher-admission`, `social-child-benefits`, `social-subsistence`.
- **Statuses:** leave all revised available facts `Approved`/available. Keep `residence-filing` `Draft`/unavailable/source-less. No other status change.
- **Source records to add:** four owner-specific records: one Housing Act record (https://zoek.officielebekendmakingen.nl/stb-2024-190.html), two Housing Toeslagen records (URLs above), and one Social child-budget asset-limit record. Allocate new IDs during integration.
- **Source records to update/remove/retain:** no URL/title/record mutation is required by this review. Remove Spain source `1b41070c-31a5-50d2-8ef9-a2c2becbbc3d` from `protection-dates` binding. Remove superseded UWV source `e1edd80d-2530-57e4-8e55-637382f1dfd1` from `healthcare-access` binding; retain the registry record for audit/history. Retain existing records and bindings for unchanged facts. Keep `residence-filing` empty.
- **Source bindings:** set `protection-dates` to `e1f80033-9e6d-5474-916f-f8fc1eb493b5`, `5da562ac-2956-51c5-8d0e-c2f553979d93`, `9422da1c-8127-5191-ae4c-7e0a783af1a7`, and corrected EU record `59f03221-ac26-5602-8173-e880102d695b`; use the five current healthcare-access IDs listed in the Compare table; bind housing-initial to a Housing-owned Act source plus existing IND/triage records, never the HEALTH-owned ID; bind housing-financial-support to its three current records plus the two new records; bind social-child-benefits to existing SVB/Toeslagen sources plus the new asset-limit record. Employment and education bindings are listed in their rows above.
- **LastUpdatedAt:** set the seven article IDs in the preceding section to `2026-11-16`; leave TP-02 unchanged.

## Verdict

**READY FOR NETHERLANDS PRODUCTION INTEGRATION**

The drafts pass after the specified copy edits. Production integration must follow the listed Netherlands-owned source bindings and add four source records before publishing claims that depend on those supplemental pages. Keep the unavailable filing fact unavailable. No production file was edited here.

## Task 53 integration addendum

Five owner-specific records were added during integration. Four are the new Housing/Social records listed in the plan above. A fifth was added for the Raad van State 1 October 2026 ruling because the approved NL-TP-01 draft explicitly cites that official page and labels it as requiring an owner-specific production record; it was already reviewed in Task 51. This closes the draft-to-registry gap without changing Compare bindings. Its source ID is `6924cd87-635c-5b58-9b67-79f14fe0ddfd` (owner NL-TP-01). Production therefore has 63 Netherlands-owned source records and 1,473 records overall.
