# Bulgaria content refresh — independent final review

**Review date / cutoff:** 17 November 2026

**Scope:** Final Bulgaria staging copies, 111-row claim and source ledger, 65 owned OfficialSource records, 24 Compare aspects, source/binding plans, and 8 final Date/Event Watch candidates. Production and canonical content were read only and remain unchanged.

## A. Scope and method

This review independently checked the staging package and targeted Bulgaria production baselines and bindings. Official-source checks focused on the high-risk dates, wages, euro transition, health contribution rules, heating assistance, employer duties, education rules, and the five proposed source additions. It did not repeat broad country research or release checks. No restore, build, test suite, CI, Docker, commit, or push was run.

## B–D. Claims, coverage, and numeric/date reconciliation

The ledger contains 111 claims: 95 existing and 16 new-important. Final classifications reconcile to 76 CONFIRMED, 3 UPDATED, 0 INCORRECT, 0 NO LONGER SUPPORTED, 16 QUALIFICATION REQUIRED, and 16 NEW IMPORTANT INFORMATION. The independent review accepted all 111 factual propositions and their classifications after the staging-copy completeness edits recorded below.

The coverage matrix has 111/111 rows: 97 MATERIAL and 14 SUPPORTING; 44 COVERED, 67 COVERED — MULTIPLE SOURCES, 0 NOT EXTERNALLY SOURCE-DEPENDENT, 0 UNSUPPORTED, and 0 unsupported MATERIAL. Every listed SourceId resolves to an existing production record or one of the five deterministic proposed ADD records. No dangling claim-source IDs were found.

The correct row-level numeric/date baseline is **38/38**: 24 existing claim rows plus 14 new-important rows, each uniquely identified in the CSV. Task 97’s 35 count is not supported by the row-level ledger and is not restored. This is a count-definition correction, not a claim-status change; no row is double-counted in the 38-row CSV tally.

## E–F. Temporary protection and current horizon

The copies keep EU temporary protection, Bulgaria’s national implementation, individual card validity, and administrative re-registration distinct. Temporary protection is also distinguished from ordinary residence routes and international protection/asylum. The exact category, cross-Member-State issues, and pre-grant rights are qualified; no universal rights bundle is inferred.

The Council of Ministers’ 4 November 2026 decision summary states the Bulgarian horizon through 4 March 2028 and the 5 March–30 April 2027 re-registration period. It concerns the national protection/card framework and sets a re-registration period; it does not supply every practical instruction or establish each holder’s card validity throughout that period. The staging text appropriately directs the reader to AREF for the individual document and procedure. The 30 April 2026 card deadline is historical and not presented as a current action. [Council of Ministers, 4 November 2026](https://www.gov.bg/bg/prestsentar/zasedaniya-na-ms/dneven-red-na-zasedanie-na-ministerskiya-savet-na-04-11-2026-g)

## G. Employment and employer duties

Temporary-protection employee work access is separated from self-employment, licensing, tax, and registration requirements. The three employer obligations remain distinct: contract and NRA-register steps before work begins; Labour Inspectorate notice within seven days after the actual start; and the specified Employment Agency notice within three days for premature termination. The claim qualifications prevent the last rule from being generalized to every scheduled end.

The 2026 wage source states €620.20 monthly / €3.74 hourly from 1 January 2026 for an eight-hour day and five-day week. State Gazette publication verifies Decree No. 235, adopted 29 October 2026, setting €660 / €3.98 from 1 January 2027. The future amounts are clearly future as of the review cutoff. [State Gazette, Decree No. 235](https://dv.parliament.bg/DVWeb/showMaterialDV.jsp?idMat=247238), [MLSP 2026 wage decree](https://www.mlsp.government.bg/uploads/67/zhrdpsi/acrobat-document.pdf)

## H. Euro and monetary framing

Official BNB information supports adoption on 1 January 2026, the fixed rate €1 = BGN 1.95583, and sole euro legal tender from 1 February 2026. The euro legislation and BNB guidance support dual-price display through 8 August 2026. Current benefit and wage values are stated in euros; lev amounts are used only as explicit historical equivalents where present. The stale BGN 28.40 self-paid health-contribution amount does not appear as current guidance. [BNB euro Q&A](https://www.bnb.bg/AboutUs/AUEurosystem/AUAccessionToTheEuroArea/AUAEFIQuestionsAndAnswers/index.htm), [Bulgarian Euro Introduction Act](https://www.bnb.bg/bnbweb/groups/public/documents/bnb_law/laws_euro_bg.pdf)

## I–J. Healthcare

The copies distinguish compulsory insurance, covered care, contribution payer, provider rules, and co-payments. They do not equate insurance with free care. Decree No. 69/2022 supports state-paid contributions through the protection period for children under 18, women aged 63 or older, and men aged 65 or older, absent another insurance basis. For adults aged 18 to under 63 (women) or under 65 (men), the state-paid period is 90 days from grant, subject to the same other-basis qualification. This is a payer rule, not a statement that healthcare entitlement ends after 90 days. [Decree No. 69/2022, State Gazette](https://dv.parliament.bg/DVWeb/showMaterialDV.jsp?idMat=173089)

No current self-paid contribution amount was established from primary evidence, so the copies safely omit a number.

## K–L. Housing and social benefits

Program 3 is described as a provider reimbursement arrangement, through 4 March 2027, without a guarantee of placement, location, fixed stay, or direct rent payment. Program 2 and older accommodation sources are historical. General social assistance is an individual application route. The €390.63 poverty line and €117.19 calculation base are distinguished; €117.19 is not described as a flat monthly benefit. The 2026/27 heating support is €64.39 monthly for five months; its 31 October 2026 application deadline is closed, with the period running through 31 March 2027. Family assistance remains separate from insurance-based maternity, childcare, and unemployment benefits. [ASP 2026/27 heating assistance](https://asp.government.bg/bg/deynosti/sotsialno-podpomagane/otpuskane-na-celeva-pomosh-za-otoplenie-za-otoplitelen-sezon-2026-2027-g/)

## M. Education

The copies distinguish application, placement, enrolment, language support, and higher-education admission. The school-age statement is properly scoped: preschool from the calendar year of age four; ordinary school start in the school year the child turns seven; possible start at six with parent choice and readiness certification; compulsory schooling through 16. School preference is not a placement guarantee. Higher-education routes are limited by year, programme, and institution; diploma recognition and admission remain separate decisions.

The school-year watch entry’s RDE URL contained a malformed character. It is corrected to the official RDE URL in the finalized candidate file. The annual-benefit watch entry now points to ASP general-assistance and family-benefit guidance as well as the budget review.

## N–O. Article and translation verdicts

Four copies are approved as staged: BG-TP-02, BG-EMP-01, BG-HOUSING-01, and BG-EDU-01. Four are approved with the concrete content/source-reference edits below: BG-TP-01, BG-EMP-02, BG-HEALTH-01, and BG-SOC-01. No article is blocked.

All 24 Compare aspects contain EN/UK/RU final text and were checked. The six REVISE aspects are `protection-dates`, `work-employer-duties`, `work-minimum-pay`, `healthcare-access`, `housing-financial-support`, and `social-child-benefits`. Current/future wage timing, protection dates, healthcare payer wording, benefit amounts, and housing mechanism remain aligned across languages. No unresolved material cross-article conflict was found.

## P–T. Source inventory, broken records, updates, legacy, and additions

All 65 Bulgaria-owned source records and 63 distinct URLs were reviewed. Statuses: CURRENT 55, LEGACY 8, BROKEN 2, UNCLEAR 0. Actions: KEEP 55, UPDATE 2, RETAIN HISTORICAL 8, REMOVE 0. Both broken records have official replacements and are approved for same-record URL updates:

- `92763c9a-e086-5432-be40-770681f92d95` — old MVR Migration Directorate service URL returned 404; update to the official current MVR residence-services page at `https://www.mvr.bg/migration/%D0%B0%D0%B4%D0%BC%D0%B8%D0%BD%D0%B8%D1%81%D1%82%D1%80%D0%B0%D1%82%D0%B8%D0%B2%D0%BD%D0%B8-%D1%83%D1%81%D0%BB%D1%83%D0%B3%D0%B8-%D0%B7%D0%B0-%D0%B3%D1%80%D0%B0%D0%B6%D0%B4%D0%B0%D0%BD%D0%B8/rpr`.
- `b76ea3f3-4ee1-5b97-b724-5cbf35d67f34` — old RDE Ordinance No. 3 PDF returned 404; update to the official current RDE regulation page at `https://ruo.mon.bg/sofia-grad/regulation/naredba-%E2%84%96-3-ot-6-april-2017-g-za-usloviyata-i-reda-za-priemane-i-obuchenie-na-liczata-tarsesthi-ili-poluchili-mezhdunarodna-zakrila/`.

Both updates preserve the SourceId, owner, and claim mapping; refresh `LastCheckedAt` to `2026-11-17T00:00:00+00:00` on integration. All eight LEGACY records are approved for historical retention because each supports a dated claim, prior procedure, court-specific history, or program closure and is not used as current operational evidence: `6dd90ade-3354-5160-97dc-fe4d66eed6cb`, `62ec9f9f-0d3a-51ba-88aa-6c5aade59a71`, `c233cdfc-267b-58d8-9fd5-887650066d8a`, `d584362c-3990-5055-8833-158f2b594773`, `bb443266-cbc9-55a9-bcbf-eb01ea44d1fc`, `f5d17e23-dfba-5cd0-bde0-b3f0b8dbface`, `989cc461-fa87-5e26-8058-a8404fbe6d52`, and `a2981f25-2a47-5ef4-ad62-349dee64690b`.

All five additions are approved. Their IDs are UUIDv5(NAMESPACE_URL, canonical URL), are unique, and do not collide with production: `1b7d23f4-0c08-581b-b156-ba4c119f92bc` (Council of Ministers, current protection decision; Core TP-01 and Compare protection dates); `399a3a4e-75e4-59d3-bdb1-315585052ea8` (2027 wage decree; Core EMP-02); `8b439e25-9430-5ab2-92b9-0df6343fa660` (BNB euro Q&A; Core SOC-01); `44c74fe5-d729-5690-b037-fd1d96bf502d` (NRA free-profession guidance; Core EMP-02); `50f0a447-2fb8-532c-920e-8f24e69a637a` (Registry Agency company registration; Core EMP-02). Each addition has an explicit scope and claim mapping. The two self-employment sources supplement, rather than replace, the EU source for access during temporary protection.

## U–W. Core and Compare bindings

The production baseline independently reconciles to 65 unique Core SourceIds and 56 unique Compare SourceIds, with 56 shared, 9 Core-only, 0 Compare-only, 65 combined, and no owned-but-unreferenced or referenced non-owned sources. Core delta is ADD 5 / REMOVE 0. Compare delta is ADD 1 / REMOVE 1: add the 4 November Council source to `protection-dates` and remove the superseded 2026 AREF date notice from that Compare aspect; retain that historical source where Core claims need it. All 24 final Compare binding lists resolve to owned existing or planned sources. Planned totals: Core 70, Compare 56, shared 56, Core-only 14, Compare-only 0.

## X. Date/Event Watch

All nine Task 97 candidates were reviewed; one obsolete 4 March 2027 protection-horizon trigger was removed, leaving eight candidates. All eight final candidates are accepted after the two source-pointer corrections. They cover 5 March 2027 and 30 April 2027 re-registration, 4 March 2028 protection horizon, 1 January 2027 wage change, 4 March 2027 Program 3 end, 15 September 2027 school-year transition (next working day if a holiday), 31 March 2027 heating period relevance, and 1 January 2027 annual benefit review. The listed lead times are 30 or 60 days, appropriate to the event risk. No project-wide watchlist was created.

## Y. Source, count, and conflict integrity

Existing Bulgaria sources 65 + ADD 5 − REMOVE 0 = 70 planned. Global production source count 1,498 + 5 − 0 = 1,503 planned. Duplicate SourceIds: 0; duplicate owner+URL pairs: 0; unintended repeated cross-owner URLs: 0; two repeated act URLs are legitimate owner-local copies. Owner conflicts: 0. Dangling IDs: 0. Material cross-article conflicts: 0. `CLAIM REOPEN REQUIRED`: 0. Material evidence gaps and blocking findings: 0.

## Z. Staging edits made during review

1. BG-EMP-02: added the already-audited employee permit exemption, employer reporting distinctions (including NRA contract and termination timing), and self-employment boundary to EN/UK/RU, with mapped source references; retained the wage facts and future/current distinction. Replaced index-only wage links with the 2026 and planned 2027 source records.
2. BG-HEALTH-01: added the already-audited age groups and 90-day state-paid contribution rule to EN/UK/RU, explicitly separating payer rules from healthcare entitlement.
3. BG-SOC-01: made unemployment explicit in the insurance-based benefit distinction in EN/UK/RU and added its mapped NSSI source reference.
4. BG-TP-01 and BG-SOC-01: replaced loose web links with the planned Council of Ministers and BNB SourceId references, and labeled dated AREF guidance as historical/subject to confirmation.
5. DATE_EVENT_WATCH_CANDIDATES_01.md: fixed the malformed RDE URL and added sources specific to annual social/family benefit review.
6. SOURCE_ADD_PLAN_01.csv: replaced repeated generic live-result text with source-specific result and evidence-scope notes.
7. BULGARIA_REFRESH_RESEARCH_01.md: recorded the review edits and linked this report.

No production JSON, canonical draft, test, or other-country file was changed.

## AA. Integration recommendation

**BULGARIA APPROVED FOR INTEGRATION.** Integrate the five approved ADD records, the two same-ID URL updates, and the documented Core/Compare binding deltas as a later release task. The staging package has no remaining blocking findings.

## Integration reconciliation

During the production binding check, the final TP-01 and EMP-02 review copies initially omitted two already-audited historical/current-card evidence references present in the 111-row claim ledger and approved source inventory. The approved claim context has been restored in those staging copies across EN/UK/RU, with the historic AREF notice explicitly labeled as 2026 history and current-card guidance scoped to status evidence. No new claims or SourceIds were introduced. The language labels on the TP-01 historical AREF citation were checked per locale. The resulting production bindings reconcile to Core 70 (65 existing + 5 additions, no removals) and Compare 56 (+1/−1).

The integration check also found a cross-article citation in BG-EMP-02 to NACID SourceId `0fb4e6bd-2a0e-5668-b340-be46f83ce44f`, which is owned by BG-EMP-01. That reference was removed from BG-EMP-02’s staged prose while the underlying audited source and BG-EMP-01 binding remain intact; BG-EMP-02 continues to cite its own labor-migration source for the reviewed qualification boundary. This keeps the approved +5/−0 unique Core SourceId delta and avoids a dangling or wrong-owner citation.

A parallel ownership check found BG-SOC-01 cited NSSI unemployment SourceId `dc44b7fb-fa68-5658-a89a-37c3d65ae67f`, which is owned by BG-EMP-01. The social-assistance copy retains the reviewed distinction between means-tested family support and insurance-based benefits, while the unemployment source remains bound to BG-EMP-01. No production SourceId or inventory row was removed.

Task 100's integration checklist further required the already-reviewed school-entry qualification to name parent choice and readiness assessment, and the euro status to state sole legal tender from 1 February 2026. Those clarifications are reflected in the EN/UK/RU staging and canonical copies; they preserve the approved ages, dates, and source set.
