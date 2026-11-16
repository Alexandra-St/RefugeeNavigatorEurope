# Hungary Refresh Review 01

As of: **16 November 2026**

Review scope: final editorial, factual, translation, Compare and source-integration review of the eight Hungary refresh articles. This review did not edit production JSON, runtime, or tests and did not repeat the full research audit.

## Article decisions

| Article | Decision | Issues | Required edits |
|---|---|---|---|
| HU-TP-01 — temporary-protection application | APPROVE | Draft separates EU rule, current Hungarian published position and individual OIF decision; differentiates formal filing from online preparation; document-validity statement is distinct from printed expiry. Existing production text still makes an overbroad pending-residence claim. | No draft edit. Replace production from the reviewed draft; do not restore the old summary claim. |
| HU-TP-02 — residence options | APPROVE | Draft distinguishes eligibility from filing; Ukraine's National Card eligibility is not presented as a TP switch; Hungarian Card is limited to specified routes; no universal bridge is promised. | No draft edit; replace production from the reviewed draft. |
| HU-EMP-01 — finding a job | APPROVE | Recognized-beneficiary service-seeker eligibility is separate from general help and ordinary job-seeker registration; vacancies do not establish work authorization. | No draft edit; replace production from the reviewed draft. |
| HU-EMP-02 — employment rights | APPROVE | Formal TP applicants and recognized beneficiaries are distinguished; applicant reporting duty is not given the beneficiary-specific deadline; wage rates and guaranteed-minimum condition are correct. | No draft edit; add the cited 2027 negotiation notice to the source catalog during integration. |
| HU-HEALTH-01 — healthcare | APPROVE | TP package is separate from employment insurance; formal filing is required for applicant basis; TAJ card is not described as an entitlement prerequisite; 112 and 1830 roles are distinct. | No draft edit; replace production and retain the repealed procedure only as historical context. |
| HU-HOUSING-01 — housing | APPROVE | The draft safely describes the lack of a verified nationwide TP first-night route and labels Budapest shelter fallback as general homelessness support. Production Compare currently presents this fact as approved/available. | No draft edit; integration must set `housing-initial` unavailable using the schema rules below. |
| HU-EDU-01 — education | APPROVE | The draft correctly states school access from formal filing and conditional school-maintainer funding. The production language-support fact is bound to a legacy portal and meal-support material, not the current provider-funding authorities. | No draft edit; add three current education-support source records and revise the Compare bindings during integration. |
| HU-SOC-01 — social assistance | APPROVE | Draft separates recognized-beneficiary subsistence support from ordinary family benefits. The owner-specific catalog lacks the OIF allowance page used in the draft and needed for the allowance/application claims. | No draft edit; add the current OIF allowance page to this article's source records and bindings during integration. |

No additional draft edits were required by this review. The production-only conflicts noted above are addressed by the draft replacement and integration plan below; production files were left untouched as requested.

## Translation parity

**PASS.** EN/UK/RU drafts preserve the same material dates, HUF amounts, ages, statutory status categories, authorities, filing triggers, eligibility limits, local/national distinctions, and modal strength (`may`/`must`/not established). Numeric differences are formatting only (for example, thousands separators). The key terminology remains stable: recognized `menedékes` versus formally filed applicant; formal filing versus online preparation; and employer/provider funding versus individual cash entitlement.

## Compare decisions

Source-ID lists below are the retained production bindings. Additions/removals are explicit. New source-record IDs should be assigned during the later integration; no duplicate record with the same owner and URL should be created.

| Aspect | Decision | Review result | Source plan |
|---|---|---|---|
| protection-scheme | REVISE | EN/UK/RU separate the corrected EU military-obligation rule, current OIF position, Decree 145/2026 legislative-history statement, and individual outcome; do not imply the conflict is resolved. | Retain `e6b30e67-8181-532b-bee0-bae30e65bd62`, `ac550c02-2fdb-5e01-91e5-945170662589`, `e3a5bbad-8e12-555f-8623-ae16d9b02c76`, `3e2865ba-34e0-5ac8-887e-2bb66c904564`; add owner HU-TP-01 record for Decree 145/2026 as historical/context only, not current operative law. |
| protection-application | REVISE | Formal in-person filing is distinguished from Enter Hungary preparation; the all-document-missing procedure remains unestablished. | Retain `e6b30e67-8181-532b-bee0-bae30e65bd62`, `ac550c02-2fdb-5e01-91e5-945170662589`. |
| protection-dates | REVISE | EU period and Hungarian beneficiary-document extension both run through 4 March 2028; earlier printed card expiry alone does not require replacement under current OIF guidance. | Retain `3e2865ba-34e0-5ac8-887e-2bb66c904564`, `e3a5bbad-8e12-555f-8623-ae16d9b02c76`; add owner HU-TP-01 record for current consolidated Decree 196/2025 (`https://njt.jog.gov.hu/jogszabaly/2025-196-20-22.2`). Do not use the expired 145/2026 provisions as operative authority. |
| residence-conditions | REVISE | National Card, Hungarian Card, ordinary employment-purpose permit, study, family, long-term residence, and active-TP filing conditions are kept route-specific. Ukraine is not on the researched ordinary employment-purpose nationality list. | Retain `c9223718-edaf-5674-9834-dbdb33ad59c1`, `9948bf57-bc75-5015-b045-149ea0070121`, `627dd813-58fd-5f95-8f51-a21f6c7164cf`, `5891ba29-363f-5fdc-86b6-130584026f3f`, `b2bcbafc-9a8b-5255-8e6f-d9c131f8a854`, `ad6bb10c-c503-5f29-977a-95bdf059e4f7`, `1893750c-8ed3-578d-af9d-1f3b55a0223d`; ensure owner HU-TP-02 records `48554f60-77d5-5d5e-86c1-992db2bf312a`, `dd7ff4c1-dbbb-5285-86fe-91eff50e8810`, `d244fc9f-a449-5d66-8fe1-b818ed2b4a5b`, `516458e9-91fa-5f95-a179-01bd4a205473`, `8f317b2b-3b86-566f-95c8-00c89c27f6e6`, `30a9abc4-f823-5b3d-83fd-dc2a2c866cd6` as required by the complete wording. |
| residence-filing | REVISE | No universal active-TP domestic filing or post-expiry stay/work bridge is claimed. Under Asylum Act §1(3), an existing title is withdrawn when another permit is granted, not merely on filing; route-specific filing remains separate. | Retain `76ed8a96-5871-51b9-904c-860602adf0e9`, `6d959a58-99a9-56ab-af89-6796db4a1767`, `94b90de6-7250-5e47-89d7-22eb085d0ee2`; add owner HU-TP-02 residence-law record `c9223718-edaf-5674-9834-dbdb33ad59c1` if absent from this fact's binding list. Keep Commission/UNHCR transition documents as non-binding context only if the value refers to their recommendations. |
| residence-routes | KEEP | Existing text correctly says no automatic conversion and keeps routes conditional. | Retain `c9223718-edaf-5674-9834-dbdb33ad59c1`, `48554f60-77d5-5d5e-86c1-992db2bf312a`, `dd7ff4c1-dbbb-5285-86fe-91eff50e8810`, `8f317b2b-3b86-566f-95c8-00c89c27f6e6`, `30a9abc4-f823-5b3d-83fd-dc2a2c866cd6`, `e4f84444-6542-5bf0-8598-0ce6c22a4bf2`. |
| employment-assistance-conditions | REVISE | Service-seeker route is for recognized TP beneficiaries aged 16+; pending applicants are not established in that special route; general help is distinct. | Retain `b4700f42-9959-55a9-95f4-b01b0ee59408`, `1d6d1d6c-b85e-5f4d-bf69-abfe320d02c5`, `2bd8abfe-ddab-5f25-bac9-b8626c5fe85c`, `6f77399d-6e4c-526e-bb29-a7e867f87b88`, `ee3cc613-a716-5ee7-8ae7-cb5994fc564e`. |
| employment-institutions | KEEP | NFSZ, district offices and EURES roles remain distinct. | Retain `1d6d1d6c-b85e-5f4d-bf69-abfe320d02c5`, `7c9ad51d-8770-5a1b-b19e-00d3da495ba4`, `98cd5457-4e24-5c7b-81fe-d831583098d4`, `6f77399d-6e4c-526e-bb29-a7e867f87b88`. |
| employment-vacancies | KEEP | VMP/EURES vacancy access is not presented as proof of work authorization or suitability. | Retain `3a0dcaf4-0bec-503b-857f-1f50941faece`, `7c9ad51d-8770-5a1b-b19e-00d3da495ba4`, `1e94be95-6339-52c2-b1e0-7440b3e6a626`, `50867616-8fa0-504b-89d7-d6de4aa64014`. |
| work-permission | KEEP | Recognized beneficiaries and formally filed applicants may work without separate permit; self-employment and licensing are not inferred. | Retain `07e41672-bca1-5dba-89b4-6e02b917d088`, `c346f78b-f199-5c74-b548-ce093d65fccf`, `7135ea05-c72e-5f8c-8d7a-b991dbc37a87`. |
| work-employer-duties | REVISE | Beneficiary notification deadlines are not copied to applicants; applicant reporting duty remains, while its adapted public procedure is unspecified. | Retain `67c33d29-b3e4-5fb4-a5ce-629bd2ae88c7`, `9bfdd18a-02c0-5fd2-a3f6-5e4a8f60819a`, `ce072706-2256-570a-8ebe-3057ff86feb6`, `9f6cfad3-2fcd-5ddf-b760-b041af8a8ec4`, `07e41672-bca1-5dba-89b4-6e02b917d088`, `7135ea05-c72e-5f8c-8d7a-b991dbc37a87`. |
| work-minimum-pay | REVISE | 2026 full-time rates are correct; guaranteed minimum applies to jobs requiring at least secondary education or secondary vocational qualification; no adopted 2027 rate identified. | Retain `40ccdf60-cf64-59ea-9bfd-85bbfa292a2a`, `9f6cfad3-2fcd-5ddf-b760-b041af8a8ec4`; add owner HU-EMP-02 record for 11 November 2026 government negotiation notice: `https://kormany.hu/en/government/szocialis-es-csaladugyi-miniszterium/news/kozeledtek-az-allaspontok-a-2027-es-minimalber-es-garantalt-berminimum-ugyeben`. |
| healthcare-access | REVISE | Defined TP package covers recognized beneficiaries and formally filed applicants under the statutory conditions; ordinary insurance and service limits are separate. | Retain `7066c5ba-84f7-5ba1-aad1-2a751af8ffd5`, `249d667d-fc43-59e0-bc7c-8a2f7c4628cb`, `9c992f27-6c8c-5a47-87dd-f3ba6d550c4d`, `e8a58413-bbc2-5358-8904-397ba755a890`, `0f170c46-d260-50a4-acc1-477d875404b9`. |
| healthcare-proof | REVISE | Status and accommodation evidence are advised; TAJ is not an entitlement prerequisite; TAJ-less workflow remains unclear. | Retain `e8a58413-bbc2-5358-8904-397ba755a890`, `0f170c46-d260-50a4-acc1-477d875404b9`, `83e9d8a9-d9af-59be-b11d-27e2e5fa4132`, `4aa814e4-85f6-578a-b189-926a0313fc41`; retain `49fd38fd-30a3-5de4-8f2d-ce28223c71da` only to identify the repealed predecessor procedure, not as current procedure. |
| healthcare-urgent | REVISE | 112 is for life-threatening emergencies; 1830 is urgent non-life-threatening out-of-hours primary care. | Retain `7c2917ca-0e60-5d5e-b0de-6dc7caaaa52f`, `071864ee-106b-5b39-8086-6b6177873fac`, `84212082-dff3-5c33-81d8-b98156c1bc12`. |
| housing-initial | UNAVAILABLE | No verified nationwide TP-specific first-night route; neutral local fallback is not a guaranteed intake route. | Schema requires `Status: Draft`, `IsAvailable: false`, `OfficialSourceIds: []`, and exact localized fallback values: EN “Not established in the reviewed sources.” / UK “Не встановлено за перевіреними джерелами.” / RU “Не установлено по проверенным источникам.” Remove this fact’s three current source bindings (`5614950d-6ab4-5686-a53a-8c632bc3d488`, `c0234d97-e7fa-50a6-b038-b988c0ee1907`, `c3fbfdbe-37d5-588b-9ed4-5b515d35e97a`); keep the source records for Core article evidence. |
| housing-financial-support | REVISE | Provider/employer subsidy is conditional and paid to provider/employer; municipal aid is local. | Retain `5614950d-6ab4-5686-a53a-8c632bc3d488`, `b38e7899-76a5-5106-ba0c-1a32127ccb5e`, `c0234d97-e7fa-50a6-b038-b988c0ee1907`. |
| housing-private-rental | REVISE | Lease and OIF accommodation-address reporting are separate; no universal reporting deadline is added. | Retain `de0e0250-5328-59d8-89cc-40aad4839160`, `845ed1c2-ac14-5316-837c-5baa93ac8354`, `cfe06190-a9ff-53c0-be45-15bbf60509d9`, `c7daa0bb-96e5-5d55-a419-8a53613f0608`. |
| education-school-enrolment | REVISE | Public education access begins at formal TP filing; no universal address-card/documents checklist is implied. | Retain `1ad2baec-8bff-5786-af4a-0f79a7a2893e`, `0338ca9e-0bc5-5a1c-bc3c-099be181b65e`, `d9cdf1bd-7792-535f-9cf2-bcadad67762b`. |
| education-pupil-language | REVISE | General support is school-specific; HUF 130,000 is conditional maintainer funding tied to five extra weekly hours, not student/family cash. | Retain `1ad2baec-8bff-5786-af4a-0f79a7a2893e`, `0338ca9e-0bc5-5a1c-bc3c-099be181b65e`; remove bindings `a7acb298-2bfc-540b-9561-5dce6ff792e6` (legacy Safe in Hungary page) and `bcfb5db8-70ad-5f7e-aac6-24ae1605062d` (meal support, not language subsidy); add HU-EDU-01 records for NJT Decree 229/2012 §37/T (`https://njt.jog.gov.hu/jogszabaly/2012-229-20-22.60`), Decree 12/2020 §341/A (`https://njt.jog.gov.hu/jogszabaly/2020-12-20-22.29`), and Decision 1083/2026 (`https://njt.jog.gov.hu/jogszabaly/2026-1083-30-22`). |
| education-higher-admission | REVISE | Recognized-beneficiary funding categories are not a guarantee of admission or free tuition; pending-applicant tuition treatment is not established; scholarships are separate. | Retain `0338ca9e-0bc5-5a1c-bc3c-099be181b65e`, `bae91a42-eeea-58ba-995b-b417c5fa328b`, `6ec7f895-31f4-56c0-8373-592192f80099`, `2c1a4d2e-9db3-53fc-8519-7dc9dcc4a64b`. |
| social-application | REVISE | OIF/NDGAP decides the recognized-beneficiary allowance; district employment office handles separate appearance and employment-service duties. | Retain `5763a821-b36f-5049-b47e-0df43cf1d4de`, `30cf615c-4141-53b4-b123-e01c225bfb30`, `822d8a19-7e19-545b-8068-a814eab2c03b`; add HU-SOC-01 OIF allowance record `https://oif.gov.hu/menedekeseknek-nyujtott-ellatasok-es-tamogatasok`. |
| social-child-benefits | REVISE | TP status alone is not sufficient for ordinary family benefits; each benefit has separate status/residence/household/insurance criteria. | Retain `5d022371-5bc2-5752-8437-af703d7de480`, `01857427-6878-54fc-8dae-fcd56dee7695`, `98c88ab4-5bb2-511a-a49d-8d06bac231f8`, `6d3b26a8-3c7a-5e1f-ba31-25ddbdde9819`, `4537851a-67d7-53fd-ae36-d67ae9abdab2`. |
| social-subsistence | REVISE | Recognized beneficiaries only; 2026 per-person rates and employment/appearance/income/pension conditions are stated; pending applicants excluded. | Retain `5763a821-b36f-5049-b47e-0df43cf1d4de`, `30cf615c-4141-53b4-b123-e01c225bfb30`, `822d8a19-7e19-545b-8068-a814eab2c03b`, `1205ed96-21f4-5d7e-8f77-cb5bb35a8c6d`; add and bind the HU-SOC-01 OIF allowance record above. |

**Final Compare counts: 4 KEEP / 19 REVISE / 1 UNAVAILABLE / 0 NEWLY AVAILABLE = 24.**

## Critical legal checks

### Military-obligation conflict

The corrected Council Decision (EU) 2026/1912 applies Article 2 from 5 August 2026 and excepts people who held TP in the same Member State before or on 4 August 2026 and retained it continuously there. OIF's current notice states that Hungary retains its prior national approach. Decree 145/2026 contains the same position in its explanatory text, but the decree's relevant provisions ceased to be in force on 7 September 2026; use it only as legislative-history context. These positions conflict for new grants. Drafts appropriately distinguish EU law, Hungary's current published position, and an individual's OIF decision; do not present the conflict as legally resolved.

### TP/document validity

EU temporary protection and Hungary's amended beneficiary-document validity extend to 4 March 2028. Current OIF guidance says existing beneficiary cards remain valid to that date despite earlier printed expiry, with no replacement required solely because of the extension. This is legal validity, not a promise about card reissue/renewal procedures for other situations. Older OIF 2027 wording is stale and must not be used as current evidence.

### Residence filing

No automatic TP-to-residence conversion is claimed. Permit eligibility is separate from a right to file in Hungary during active TP. No universal active-TP switch or post-TP pending-stay/work bridge is stated. Act LXXX of 2007 §1(3) supports that an existing Asylum-Act title is withdrawn when another residence permit is granted, not simply upon filing; the exact route and filing eligibility still require OIF confirmation. National Card eligibility (including Ukraine on the nationality list), Hungarian Card qualifying activities/professions, and ordinary employment-purpose permits remain separate routes. Ukraine is excluded from the researched ordinary employment-purpose nationality list.

### National Card / Hungarian Card

National Card: Ukraine is on the nationality list, but qualifying work/business and other conditions apply; no active-TP filing right is inferred. Hungarian Card: limited to qualifying activities/professions and associated requirements; ordinary instructions do not establish active-TP filing. Route eligibility is not a guaranteed domestic filing route.

### Work permission

Act L of 2025 §20 allows recognized beneficiaries and formally filed applicants to work without a separate work permit. Online preparation or intent is not formal filing. This does not automatically authorize self-employment; regulated professions may require separate licensing/recognition. Cross-article wording is consistent.

## Employment review

Beneficiary employer notice: start by the start day and end by the following day under the cited rule. Formal applicants also have a statutory reporting duty under Act L §20(2), applying Act XC/2023 subtitle 88, but there is no bespoke public deadline/channel checklist for the permit-free applicant category. The draft correctly keeps these rules separate. 2026 minimum wage: HUF 322,800/month, 74,210/week, 14,850/day, 1,856/hour. Guaranteed minimum: HUF 373,200/month, 85,800/week, 17,160/day, 2,145/hour, only for a job requiring at least secondary education or secondary vocational qualification. No 2027 replacement was adopted in the reviewed research; negotiation notice is context only.

## Healthcare review

Recognized beneficiary and formally filed applicant packages are distinct statutory bases. Formal application filing is required; employment insurance is separate; a physical TAJ card is not established as an entitlement prerequisite. Provider procedure without TAJ remains evidence-limited. Not every treatment/medicine is free. 112 is life-threatening emergency dispatch; 1830 is nationwide urgent out-of-hours primary-care triage/service for non-life-threatening needs.

## Housing review

`housing-initial` remains **UNAVAILABLE** as a verified nationwide TP first-night route. The neutral fallback to local social/homelessness services is not guaranteed TP reception and does not invent an address or phone number. Employer/provider accommodation support is conditional and paid to the eligible employer/provider; municipal support varies by locality. Private lease and OIF accommodation-address notification are separate; no unsupported universal address-change deadline is added.

## Education review

Public education access for a child begins on formal TP application filing; recognition need not come first. Catchment school duties and placement remain statutory/local, and preferred-school placement is not guaranteed. Compulsory-school/kindergarten ages and statutory exceptions are stated. Language support is not promised nationwide. The five-extra-hour/HUF 130,000 route is current conditional provider/maintainer funding under Decree 229/2012 §37/T, Decree 12/2020 §341/A and Decision 1083/2026; it is not a family payment. University admission is institution-specific; credential recognition, admission, scholarship and tuition decisions are separate; no universal free-university claim is made.

## Social-assistance review

The special TP subsistence allowance is for recognized `menedékes`, not a pending applicant: 2026 rates are HUF 22,800 per adult/month and HUF 13,700 per minor/month. Appearance, employment-service, income and pension requirements and consequences are kept separate. Ordinary family allowance, maternity/childcare benefits, insurance-based CSED/GYED, municipal aid and tax relief are not inferred from TP status alone. OIF/NDGAP is the allowance decision/payment authority; MÁK/competent government office handles ordinary benefits as applicable; local aid remains municipal.

## Legacy source remediation

Three catalog records were classified LEGACY:

| Record | Owner / claim | Treatment | Current replacement |
|---|---|---|---|
| `a7acb298-2bfc-540b-9561-5dce6ff792e6` — Safe in Hungary education page | HU-EDU-01; formerly bound to `education-pupil-language` | Keep the record as historical context; remove it from current language-support Compare bindings and do not rely on it for an active benefit. | Current Decree 229/2012 §37/T, Decree 12/2020 §341/A and Decision 1083/2026 (add records). |
| `49fd38fd-30a3-5de4-8f2d-ce28223c71da` — Decree 171/2022, repealed 20 July 2025 | HU-HEALTH-01; TAJ-less provider handling | Retain as historical-only evidence explaining why the prior workflow is not the current workflow; never use as sole authority for a current provider process. | Act L of 2025 §19, Decree 196/2025 §3 and current OIF pages already catalogued. |
| `22daea53-997d-5502-8fe0-c4733bfe0c02` — Decree 104/2022 predecessor | HU-HOUSING-01; historical/current-status audit | Retain as history only; do not bind as current housing entitlement. | Decree 196/2025, Act L of 2025, current OIF and NFSZ sources already catalogued. |

**Existing legacy remediation count: 3/3 assessed.** Remove one current-Compare binding (`a7ac...`). The other two remain historical-only records for explaining repeals/predecessors; no current claim relies on them alone. The proposed Decree 145/2026 source record is an additional historical record, not part of the 106-source baseline.

## Source integration plan

### Add source records

Add these owner-specific records, after checking for an existing exact owner+URL match:

1. HU-TP-01 — Government Decree 145/2026: `https://njt.jog.gov.hu/jogszabaly/2026-145-20-22`. Its relevant provisions ceased to be in force on 7 September 2026; retain it as historical context for the published Hungarian position, not as current operative law.
2. HU-TP-01 — current consolidated Government Decree 196/2025 §2(4): `https://njt.jog.gov.hu/jogszabaly/2025-196-20-22.2`.
3. HU-EMP-02 — Government's 11 November 2026 2027 wage-negotiation notice: `https://kormany.hu/en/government/szocialis-es-csaladugyi-miniszterium/news/kozeledtek-az-allaspontok-a-2027-es-minimalber-es-garantalt-berminimum-ugyeben`.
4. HU-EDU-01 — Decree 229/2012 §37/T: `https://njt.jog.gov.hu/jogszabaly/2012-229-20-22.60`.
5. HU-EDU-01 — Decree 12/2020 §341/A: `https://njt.jog.gov.hu/jogszabaly/2020-12-20-22.29`.
6. HU-EDU-01 — Government Decision 1083/2026: `https://njt.jog.gov.hu/jogszabaly/2026-1083-30-22`.
7. HU-SOC-01 — OIF/NDGAP allowance page: `https://oif.gov.hu/menedekeseknek-nyujtott-ellatasok-es-tamogatasok` (same URL exists under another owner; add one HU-SOC-01-owned record because ownership is article-specific; do not add a second HU-SOC-01 record for that URL).

### Update or remove source records

- Update source records: **none planned**; keep existing metadata and IDs unless an integration-time URL check finds a broken/moved record.
- Remove source records: **none**; retain the three legacy records with historical-only use as described above.
- Remove bindings: `a7acb298-2bfc-540b-9561-5dce6ff792e6` from `education-pupil-language`; `bcfb5db8-70ad-5f7e-aac6-24ae1605062d` from that fact because it concerns meal support, not language/provider funding; and the three listed housing sources from `housing-initial` because unavailable facts must carry no source IDs. Keep those source records for Core article citations.
- Add bindings: Decree 145/2026 as historical context to `protection-scheme`; current Decree 196/2025 to `protection-dates`; the 2027 negotiation notice to `work-minimum-pay`; the three education authorities to `education-pupil-language`; the HU-SOC-01-owned OIF allowance source to `social-application` and `social-subsistence`; add `c9223718-edaf-5674-9834-dbdb33ad59c1` to `residence-filing` if not already bound there.
- Retain all other bindings listed in the 24-row Compare plan; add no duplicate owner+URL records.

## Cross-article consistency

Reviewed TP ↔ Employment, TP ↔ Healthcare, TP ↔ Housing, TP ↔ Education, TP ↔ Social, Employment ↔ Healthcare, Employment ↔ Social, Housing ↔ Social, and Education ↔ Social. **No unresolved draft-to-draft material conflict found.** Formal TP filing, applicant/recognized-beneficiary scope, work rights, housing availability, address reporting, school access and benefit authorities are consistently qualified. Current production data does have two integration mismatches: TP-01's old summary overstates the pending residence-permit effect compared with the reviewed residence article, and Compare `housing-initial` remains approved/available. Full draft replacement and the explicit unavailable-state change resolve them.

## LastUpdatedAt integration plan

The eight `information-entries.json` records currently have no `LastUpdatedAt` property. On integration, set `LastUpdatedAt` to **2026-11-16** for all eight IDs because each receives a substantive factual refresh/replacement:

- HU-TP-01 `1bb2a2b8-cfe0-5595-8155-138443fade17` — military-rule/document validity, formal filing and route caveats.
- HU-TP-02 `914418c6-15f8-527c-85dd-be5bda5594fc` — active-TP filing limits, route eligibility and current nationality restrictions.
- HU-EMP-01 `0e65e68f-5c42-58d5-b7b3-e941896bb532` — applicant/beneficiary service access and current employment-service limits.
- HU-EMP-02 `4dde28d5-8f4c-5503-926b-eac8789b4e9f` — formal-applicant reporting duty, 2026 wages and reporting distinction.
- HU-HEALTH-01 `8cb405ab-c562-5c22-81ad-0fd93ef50202` — current applicant/beneficiary package and TAJ workflow caveat.
- HU-HOUSING-01 `bbdf7f5c-da1d-59ad-9515-5bd8499e2332` — first-night evidence ceiling and conditional provider funding.
- HU-EDU-01 `e0fd251b-cf52-5a45-9a8d-b07e91580985` — formal-filing school right and current conditional school funding.
- HU-SOC-01 `f34f7c15-d879-5ba8-81cf-65f7e9ca416f` — current TP allowance, ordinary-family-benefit distinction and eligibility conditions.

## Remaining evidence ceilings

- EU military-obligation rule versus Hungary's published position; individual OIF decisions and evidence checklist.
- No universal active-TP residence-switch procedure or route-independent pending bridge.
- Applicant employer-reporting channel/deadline adaptation.
- TAJ-less healthcare provider workflow.
- First-night accommodation and variable local housing/social aid.
- School/provider availability and placement; institution-specific university admissions, recognition and future scholarships.
- Individual allowance and ordinary family-benefit determinations; OIF filing/payment details.

None is a blocker because drafts use safe qualified wording.

## Production integration plan

- Replace core records/translations for all eight IDs listed in the LastUpdatedAt plan with the matching EN/UK/RU reviewed drafts.
- Revise 19 Compare facts to the reviewed wording and source bindings in the table; leave four KEEP facts as reviewed.
- Set `housing-initial` to schema-valid `Status: Draft`, `IsAvailable: false`, `OfficialSourceIds: []`, and the exact EN/UK/RU reviewed-source fallback strings; the Core housing draft retains its sourced neutral fallback guidance.
- Add the six owner-specific source records listed above; remove the two education language-support bindings; retain the three legacy source records as historical-only; no source records are otherwise removed or updated.
- Set `LastUpdatedAt` to 2026-11-16 for all eight substantively revised articles.
- No status changes to the 23 available Compare facts; no other article/source status changes.

## Verdict

**READY FOR HUNGARY PRODUCTION INTEGRATION.** All material draft claims have safe qualified wording; translation parity passes; the military-rule conflict remains explicit; `housing-initial` remains unavailable; the source and Compare corrections are enumerated above. Production integration remains a separate step.

## Task 59 production integration execution

- Core: 8/8 article records replaced from the final EN/UK/RU article drafts; all 24 translations remain Verified and have `LastUpdatedAt` set to 2026-11-16.
- TP-01: Decree 145/2026 is identified in all three languages as historical context only; the relevant provisions expired on 7 September 2026. Current OIF position remains explicitly in conflict with the EU rule. Updated the final draft wording to reflect this Task 59 safety requirement.
- Compare: 4 KEEP, 19 REVISE, 1 UNAVAILABLE, 0 newly available. `housing-initial` uses the exact unavailable fallback, Draft status, and no source bindings. Retained only reviewed owner-specific bindings; removed the two obsolete education bindings and three housing bindings.
- Sources: 1,479 before; added 7; updated 0; removed 0; 1,486 after. Hungary has 113 owner-specific source records. No dangling or wrong-owner Core or Compare citation IDs were found. All seven new records are used by their reviewed facts/Core content.
- Integration regression: updated catalog totals and targeted Hungary assertions for the timestamp, reviewed content, Decree 145/2026 expiry, wages, Compare availability and source ownership.
- Local release checks: `dotnet restore RefugeeNavigatorEurope.slnx` passed (NU1900 vulnerability-cache access warnings); Release build passed (0 errors); Release .NET tests passed, 706/706; Python unittest discovery passed, 104/104; `git diff --check` passed. Local Docker daemon was unavailable, so Docker build and smoke test were not run locally.
- Push and exact-SHA CI are tracked after commit; this report is updated with their results after they finish.
