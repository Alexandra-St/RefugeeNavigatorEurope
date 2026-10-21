# Netherlands remaining primary-source recheck — Phase 18.4V

Baseline: `e32533c10ac732a25b7c0f609f007755e4cd4b54`.

Direct checks: **21 October 2026, 15:34–15:37 UTC**. The source register below uses actual observation windows, not invented per-request timestamps. Explicit HTTP-client attempt times are given where available. These are report observations, not catalog `LastCheckedAt` updates. Scope is the remaining blockers in [the targeted review](TARGETED_FRESHNESS_AND_TRANSLATION_REVIEW.md), after [Phase 18.4U](TARGETED_CONTENT_CORRECTIONS.md); previously passed content was not re-audited.

## Direct-access register and primary states

All URLs below were requested directly. Search indexing was used only to locate/check possible routes and is **not** fresh verification. No official redirect from an unavailable source to a replacement was observed; IDW is supplementary evidence, not a silently substituted Nuffic source.

| Source / exact URL | Access and identity; displayed date | Actual check time (UTC, 21 October 2026) | Primary state / impact |
| --- | --- | --- | --- |
| [IND — Highly skilled migrant](https://ind.nl/en/residence-permits/work/highly-skilled-migrant) | Opened; correct page, last update 11 September 2026. Requirements and application sections read. | 15:34–15:37 | **RESOLVED — SOURCE CURRENT**. Supports route-specific work/sponsor/income requirements; permits bounded review, not universal TPD eligibility. |
| [IND — Residence permit for partner](https://ind.nl/en/residence-permits/family-and-partner/residence-permit-for-partner) | Web reader returned 503; direct HTTP client subsequently returned 200 at the same URL. Correct page, last update 24 July 2026. Retrieved HTML confirms a partner-permit route and presents a nationality/current-permit questionnaire; detailed conditional results were not retrieved. | HTTP attempt began 15:36:18; retrieval completed before 15:36:53 | **RESOLVED — SOURCE CURRENT**, limited to identity/route existence. Does not newly certify every sponsor, income or relationship condition. No personalized questionnaire answers were supplied. |
| [IND — Permanent residence permit](https://ind.nl/en/replace-extend-renew-and-change/permanent-residency/permanent-residence-permit) | Opened; correct page, last update 20 May 2026. Requirements and filing sections read. | 15:34–15:35 | **RESOLVED — SOURCE CURRENT**. Supports the bounded five-year/other-conditions statement; preserve the TPD-time uncertainty. |
| [IND — Temporary Protection Directive Ukraine](https://ind.nl/en/ukraine/temporary-protection-directive-ukraine) | Opened; correct page, displayed last update 6 August 2026. Working/studying and post-4-March-2028 sections read. | 15:34–15:35 | **RESOLVED — SOURCE CURRENT**. Supports employee/other-work distinction and a prospective transition document; does not establish a universal ordinary-permit filing route. |
| [Nuffic — Applying for a credential evaluation](https://www.nuffic.nl/en/study-and-work-in-the-netherlands/applying-for-a-credential-evaluation) | Direct web opens and HTTP client returned 503. Current body/page identity and displayed dates could not be independently read. Indexed title/text alone were not accepted. | 15:34–15:36; HTTP attempt 15:35:28 | **SOURCE UNAVAILABLE**. Blocks full recheck of the linked non-binding-evaluation and ten-working-week statements. |
| [September memorandum — exact parliamentary download](https://www.tweedekamer.nl/downloads/document?id=2026D46524) | Web fetch and direct HTTP client timed out. No document body, signature, decision, publication date or commencement provision retrieved. Catalog describes it as “Oplegger verkenning reguliere vergunningen / decision memorandum attached to the 25 September package”; that identity was not freshly confirmed. | 15:34–15:36; HTTP attempt 15:35:28 | **SOURCE UNAVAILABLE**. Blocks current verification of the article's specific September-proposal account and its possible policy implications. |
| [Government.nl — Long term policy: return and stay](https://www.government.nl/themes/migration-and-travel/reception-of-refugees-from-ukraine/long-term-policy-return-and-stay) | Opened; correct page. Still gives 4 March 2027 for protection and the subsequent transition. No page update/effective date shown. | 15:34–15:35 | **STALE OFFICIAL GUIDANCE** relative to the current 2028 material below. Retain explicit attribution/caveat; do not use as an operative filing instruction. |
| [Rijksoverheid — Opvang en verblijf van ontheemden uit Oekraïne](https://www.rijksoverheid.nl/themas/migratie-en-reizen/opvang-vluchtelingen-uit-oekraine/opvang-en-verblijf-van-ontheemden-uit-oekraine) | Opened; correct page. Says EU protection runs through 4 March 2028; transition-document section describes Cabinet intentions and links plans dated 26 June 2026. No page update date shown. | 15:34–15:35 | **RESOLVED — SOURCE CURRENT** for the stated horizon and prospective-policy distinction, not proof of enacted implementation. |
| [UWV — Working in the Netherlands as a Ukrainian refugee](https://www.uwv.nl/en/individuals/working-in-the-netherlands/ukrainian-refugee) | Opened; substantive English guidance readable despite a JavaScript banner. Worker requirements still instruct taking out Dutch health insurance. No update/effective date shown. | 15:34–15:35 | **OFFICIAL CONFLICT PRESERVED** with the government RMO guidance below; permits only bounded presentation of the conflict. |
| [Rijksoverheid — Als zorgverlener kosten declareren voor zorg aan vluchtelingen uit Oekraïne](https://www.rijksoverheid.nl/themas/familie-zorg-en-gezondheid/gezondheidszorg-vluchtelingen-oekraine/als-zorgverlener-zorgkosten-declareren) | Opened; correct page. Own-insurance section says working refugees can buy insurance but need not do so; identifies the BSN/code-46 RMO cohort. No page update date shown. | 15:34–15:35 | **OFFICIAL CONFLICT PRESERVED**. Contradicts UWV's general instruction for the overlapping described cohort; no individual insurance ruling inferred. |
| [RMO/DSW — Verzekeringsvoorwaarden 2026](https://content.rmoekraine.nl/file/asset/v3/assets/blt7fdf1e71acb1b4cb/blt57e9de7968b039e6/6a5deecbcdd6bc799fbfc0fe/RMO_verzekeringsvoorwaarden_2026.pdf?branch=productie) | Opened, 35-page PDF, cover dated March 2026. Section 1.1 identifies BRP/code 46; section 1.3 limits contracted-provider reimbursement. No separate commencement date established from the cover. | 15:35–15:37 | **RESOLVED — SOURCE CURRENT** for these scheme conditions; does not reconcile UWV's insurance instruction. |
| [IDW — International credential evaluation](https://www.idw.nl/en/) | Opened; correct service page. Defines comparison with Dutch education levels. Warns My IDW is temporarily offline: login, submissions and viewing applications unavailable. No dated notice/repair deadline shown. | 15:35–15:36 | **RESOLVED — SOURCE CURRENT** for purpose and the observed service warning only; not proof Nuffic moved. |
| [IDW — Longer processing times](https://idw.nl/en/longer-processing-times/) | Followed IDW's official link. Initial document check approximately six working weeks; evaluation approximately eight working weeks after sufficient documentation; incomplete submissions can restart the initial wait. No update/effective date shown. | 15:35–15:36 | **RESOLVED — SOURCE CURRENT** for current approximate stages. Does not directly reverify Nuffic's general ten-week formulation or guarantee any deadline. |
| [IDW — Frequently asked questions](https://www.idw.nl/en/frequently-asked-questions/) | Opened; rendered extraction gave section headings, not substantive answers about legal effect. No update date shown. | 15:35–15:36 | **SOURCE UNAVAILABLE** for the specific non-binding-evaluation answer; page accessibility alone is insufficient. |
| [IDW — Disclaimer and Privacy policy](https://www.idw.nl/en/disclaimer-and-privacy-policy/) | Opened through IDW's official link. Identifies Nuffic/SBB and ministry mandates; website disclaimer is not evidence that an individual credential evaluation is non-binding. No update date shown. | 15:37 | **RESOLVED — SOURCE CURRENT** only for organisational identity; does not close the non-binding-evaluation blocker. |

## IND findings and limits

The highly skilled migrant page requires a recognised employer/sponsor, an employment contract and applicable income requirements. It distinguishes application variants and contains in-country employer-filing text for a particular route; this must not be generalized to every TPD holder or every ordinary permit. The partner page's retrieved questionnaire confirms that circumstances matter, but did not yield all conditional requirements. None of these retrieved pages demonstrates **general automatic conversion** or **universal in-country filing from TPD**. This is a limit of this evidence, not proof that every in-country route is prohibited.

IND's permanent-residence requirements include five consecutive years with a valid Dutch residence permit (with exceptions), main residence, timely extensions, continuing conditions, a current non-temporary residence purpose, income, BRP registration and integration requirements/exemptions. The retrieved general page does not explicitly settle how TPD time counts for national permanent residence or EU long-term residence. Preserve that uncertainty; do not derive an answer from the generic five-year rule.

IND's working/studying section supports the employee TWV exemption with employment contract and IND proof, and separately describes volunteer/trainee conditions and a TWV requirement for self-employment. The current article/fact's qualified distinction remains supported. No unrelated employment-law recheck was performed.

## Nuffic/IDW and September memorandum

IDW directly confirms evaluation purpose, but its accessible pages do not newly establish the candidate's non-binding legal effect. The Nuffic text found through indexing is **indirect evidence only**. No official redirect or move was proven. The ten-week lower-bound wording is not disproved simply because IDW now describes longer approximate stages, but it is not newly certified either. The existing article already sends readers to current IDW processing information and does not promise submission availability. The temporary service outage is a publication-time navigation concern; no fabricated reopening date or guaranteed processing time should be added.

Because the exact memorandum could not be retrieved, this pass cannot determine whether its contents are a **POLICY PROPOSAL**, **ADOPTED POLICY**, **ENACTED RULE** or **OPERATIONAL PROCEDURE**, nor whether it changes transition design, commencement, ordinary-route filing or asylum processing. The existing catalog's description of a proposal is not substituted for current primary verification. Accessible IND material describes investigation and a possible future three-year transition document; that supports a prospective distinction but cannot prove that the unavailable memorandum has no later effect. NL-TP-02 therefore remains blocked for a status decision.

## Two official-guidance discrepancies

**2027/2028 — STALE OFFICIAL GUIDANCE.** The English long-term-policy page still describes the same TPD horizon as 2027, while current IND and Dutch government pages explicitly give 2028. There is no distinct cohort/context on the English page that makes those horizon statements compatible. “Stale” is this review's evidence-based assessment, not a claim that the government has formally withdrawn the page. Its transition timetable must not be used as current operational guidance. Preserve the attribution and the difference between the EU horizon and an individual's document/entitlement; do not silently reconcile the dates.

**UWV/RMO — OFFICIAL CONFLICT PRESERVED on 21 October 2026.** UWV's instruction and the government RMO own-insurance guidance remain different for overlapping described workers. RMO terms confirm scheme scope rather than resolve that legal/administrative distinction. The current candidate wording accurately exposes the conflict and avoids individual insurance instructions. Bounded promotion with that caveat may be considered; this report neither resolves a person's obligation nor certifies current legal accuracy generally.

## Seven Core candidate recommendations

“READY” below is limited to these remaining-source findings and carries forward earlier passed checks; it is not a new whole-article legal audit or an approval.

| Core candidate | Readiness | Reason |
| --- | --- | --- |
| NL-TP-02 | NOT READY — SOURCE BLOCKER | Exact September memorandum unavailable; partner details only partially retrieved. Preserve the stale-English-guidance attribution and TPD-time uncertainty. |
| NL-EMP-01 | NOT READY — SOURCE BLOCKER | Nuffic non-binding-evaluation/ten-week statement not directly reverified; IDW outage and longer stages need attention before release. |
| NL-EMP-02 | READY FOR STATUS DECISION | IND employee/other-work distinction directly rechecked; retain current qualifications and dated wages. |
| NL-HEALTH-01 | READY WITH PRESERVED CAVEAT | UWV/RMO conflict still explicit; Phase 18.4U source correction remains the baseline. |
| NL-HOUSING-01 | READY FOR STATUS DECISION | No new blocker in this pass; previous bounded triage correction retained. |
| NL-EDU-01 | NOT READY — SOURCE BLOCKER | Nuffic's non-binding-evaluation claim still lacks direct current recheck; existing DUO correction not reopened. |
| NL-SOC-01 | READY FOR STATUS DECISION | No new blocker in this pass; Phase 18.4U qualified contribution/leefgeld wording retained. |

Core totals: **3 READY FOR STATUS DECISION; 1 READY WITH PRESERVED CAVEAT; 3 NOT READY — SOURCE BLOCKER; 0 NOT READY — CORRECTION REQUIRED**.

## Twenty selected Comparison fact recommendations

| Aspect | Readiness | Reason |
| --- | --- | --- |
| residence-routes | NOT READY — SOURCE BLOCKER | Memorandum unavailable; partner detailed requirements only partially retrieved. |
| residence-conditions | READY WITH PRESERVED CAVEAT | General IND conditions current; explicit TPD-time uncertainty must remain. |
| employment-vacancies | READY FOR STATUS DECISION | Earlier passed scope retained. |
| employment-institutions | READY FOR STATUS DECISION | Earlier passed scope retained. |
| employment-assistance-conditions | NOT READY — SOURCE BLOCKER | Linked Nuffic legal-effect statement not freshly verified. |
| work-permission | READY FOR STATUS DECISION | Current IND section supports the bounded distinction. |
| work-employer-duties | READY FOR STATUS DECISION | Earlier passed scope retained. |
| work-minimum-pay | READY FOR STATUS DECISION | Earlier passed dated 2026 scope retained. |
| healthcare-access | READY WITH PRESERVED CAVEAT | Direct UWV/RMO conflict persists; baseline already adds UWV relationship. |
| healthcare-proof | READY FOR STATUS DECISION | Earlier passed provider-route scope retained. |
| healthcare-urgent | READY FOR STATUS DECISION | Phase 18.4U link correction retained; no new emergency review needed. |
| housing-initial | READY FOR STATUS DECISION | Phase 18.4U interim-triage wording retained. |
| housing-private-rental | READY FOR STATUS DECISION | Earlier passed scope retained. |
| housing-financial-support | READY FOR STATUS DECISION | Earlier passed separate-schemes scope retained. |
| education-school-enrolment | READY FOR STATUS DECISION | Earlier passed scope retained. |
| education-pupil-language | READY FOR STATUS DECISION | Earlier passed scope retained. |
| education-higher-admission | NOT READY — SOURCE BLOCKER | Nuffic evidence gap remains; DUO wording unchanged. |
| social-subsistence | READY FOR STATUS DECISION | Phase 18.4U qualified wording retained. |
| social-application | READY FOR STATUS DECISION | Earlier passed bounded municipal navigation retained. |
| social-child-benefits | READY FOR STATUS DECISION | Earlier passed conditional routes retained. |

Fact totals: **15 READY FOR STATUS DECISION; 2 READY WITH PRESERVED CAVEAT; 3 NOT READY — SOURCE BLOCKER; 0 NOT READY — CORRECTION REQUIRED**.

## Remaining gates and explicit non-authorization

- Reopen the exact Nuffic and September memorandum sources, or obtain a separately reviewed evidence/correction decision. Do not call either source current based on indexing or catalog metadata.
- Do not represent the partner questionnaire as fresh verification of every conditional requirement. Preserve all bounded route/TPD-time caveats.
- Reassess the IDW operational warning at publication time; this pass does not certify a working application service. No definite incompatible production claim was established and no content correction was made.
- Owner/editorial status decisions remain separate. Preserved conflicts are not independent legal certification.
- Held **NL-TP-01**, the four held/deferred facts and their status decisions remain outside scope. No new assessment or approval of them is implied.
- Exactly seven Core candidates and twenty selected facts are classified above. All source access is distinguished from indirect indexing; no conflict is silently reconciled. No build, unit-test run or browser workflow rerun was performed locally. `git diff --check`, relative-link checks, 7/20 classification counts and allowed-file validation passed.

**No content, catalog, source timestamp or status change; no publication; no deployment. This report authorizes none of those actions.**
