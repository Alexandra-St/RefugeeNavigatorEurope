# Denmark External QA Triage — 22 October 2026

Baseline: `60663bd51afc5a55bd23dcdeb9da6a6c26b6dfec`
Review date: 22 October 2026
Scope: the nine Denmark findings listed below. This is a source-check and correction plan, not editorial approval.

## Owner summary

The most significant current issue is an unresolved date mismatch in official immigration information: the current regulation permits qualifying permits to be extended until 17 March 2028, while several Danish Immigration Service pages still display 17 March 2027. The article, the `protection-dates` comparison fact, and the approved Denmark guide all attribute and explain the mismatch, and warn readers not to infer an individual expiry date. Keep that disclosure and require a fresh agency-page check and owner decision before a later release.

The housing article’s Section 57(5) explanation is consistent with the current Integration Act: the exclusion is specifically for permits under Ukraine Special Act §2(1), with a narrowly stated very-special-personal-circumstances exception. One housing-benefit sentence goes further than the sources establish for a person whose only basis is a pending Ukraine Special Act application; narrow that sentence before treating the article as fully source-supported. The employment-contract threshold is correct and has a direct statutory source; the older Life in Denmark page still shows an 8-hour/one-month criterion in its indexed preview, so it should not be used for that rule.

No production record, source record, status, date, test, application file, or deployment setting was changed in this review. The existing articles/facts remain in their current approved/verified lifecycle states; this triage does not itself authorize publication or deployment.

## Findings at a glance

| ID / priority | Affected records | Classification | Triage outcome |
|---|---|---|---|
| DK-EXT-01 / P1 | TP application `afc14824-4035-4ca7-9662-6d5352ba63cb`; after-protection `2d106fd3-e981-4ef9-9733-568b654dfca5`; fact `protection-dates`; guide `denmark-temporary-protection-v1` | Confirmed | Keep transparent 2027/2028 attribution; require fresh agency-source check and owner acceptance before release. |
| DK-EXT-02 / P1 | TP application; work-rights article `35da68f6-8880-40b2-9b3f-eb2b809b1caf`; fact `work-permission`; social article `1a19c1f9-1111-4d60-8490-da548f33083f`; guide step `pending-clarify-work-conditions` | Confirmed | Current post-biometric work rule is described accurately. L 43 remains a proposal; do not apply it as current law. Recheck before release. |
| DK-EXT-03 / P2 | Employment-rights article `35da68f6-8880-40b2-9b3f-eb2b809b1caf` | Confirmed, with access limitation | Article’s >3-hour/four-week rule matches Act no. 501/2023 and its stated exceptions. Do not rely on the older 8-hour Life in Denmark page for this rule. |
| DK-EXT-04 / P2 | Employment-search article `c2356631-3fa2-4553-8fda-50a4419262c0`; facts `employment-vacancies`, `employment-institutions` | Partially Confirmed | Article contains all three requested caveats. The comparison splits these points across two facts; a concise Danish-guide-language note in `employment-vacancies` is an optional clarity improvement, not a blocker. |
| BUG-008 / P1 | Housing article `19afdd19-e2cc-4da6-bde3-6dc3d64b7503`; fact `housing-initial` | Confirmed | Current wording correctly distinguishes §12 duty from §57(5)’s limited §2(1) exception and preserves the separate municipal duties. No correction indicated. |
| BUG-009 / P1 | Housing article; fact `housing-financial-support` | Needs Evidence | Sources establish the fixed-residence condition and general housing-benefit criteria, but do not directly decide whether a pending Ukraine Special Act application alone meets that condition. Narrow the article’s inference. |
| BUG-011 / P1 | Social article `1a19c1f9-1111-4d60-8490-da548f33083f`; facts `social-subsistence`, `social-application` | Confirmed | The current exclusion and L 43’s proposed qualified-subgroup changes are separated. Keep the date-qualified statement and refresh the proposal’s status before release. |
| DK-EXT-08 / P3 | After-protection article; facts `residence-routes`, `residence-conditions` | Not Reproduced | The Labour Market Attachment URL opens the named official page despite “Labout” in the path. Do not replace it based on spelling alone. |
| DK-EXT-09 / P3 | TP application source records for Act no. 324/2022 | Confirmed | The HTML and PDF are two presentations of the same Act no. 324/2022, not two independently consolidated versions. Keep amendments/current consolidation distinct. |

## Detailed review

### DK-EXT-01 — Temporary Protection extension dates (P1)

**Affected records:** TP application `afc14824-4035-4ca7-9662-6d5352ba63cb`; Residence After Protection `2d106fd3-e981-4ef9-9733-568b654dfca5`; Denmark comparison aspect `protection-dates`; guide `denmark-temporary-protection-v1`, including `permit-check-extension-communication`.

**Current project statement:** BEK no. 828/2026 allows a qualifying permit to be extended until 17 March 2028 unless revocation grounds under Special Act §10 apply. The Immigration Service application/family guidance still states 17 March 2027. The article, comparison fact, and guide attribute both dates and explicitly say neither general date determines an individual permit’s expiry.

**Source check (22 October 2026):** BEK no. 828/2026 is marked current; §1 says permits may be extended until 17 March 2028 unless §10 revocation grounds apply, §1(2) provides for agency decisions on its own initiative with suspensive effect, and §4 took effect on 1 October 2026. The official application and family pages continue to state 17 March 2027. The indexed Immigration Service “Information to people from Ukraine” page also shows 2027; direct retrieval of the legacy URL timed out, so that particular page was checked through the official indexed result, not directly opened.

**Classification:** Confirmed official-public-information mismatch. The regulation’s operative date is clear; the remaining issue is stale agency-facing guidance and user confusion, not an unresolved text within BEK.

**Sources:** [BEK no. 828/2026](https://www.retsinformation.dk/eli/lta/2026/828); [Immigration Service — Ukraine information](https://nyidanmark.dk/en-GB/Words-and-concepts/F%C3%A6lles/Information-to-Ukrainian-citizens); [application page](https://www.nyidanmark.dk/en-GB/You-want-to-apply/Ukraine/Displaced-from-ukraine); [family application page](https://www.nyidanmark.dk/en-GB/You-want-to-apply/Ukraine/Family-to-displaced-from-ukraine).

**Minimum recommendation / risk:** Keep the existing explicit attribution and warning; do not resolve the 2027 agency date by silently replacing it or imply guaranteed personal extension. Before any future release, check whether the Immigration Service has updated the pages and have the owner decide whether continued publication of the attributed mismatch is acceptable. Without the disclosure, readers could mistake a general date for their own expiry date or follow stale official instructions.

### DK-EXT-02 — Work while a Special Act application is pending and L 43 (P1)

**Affected records:** TP application; work-rights article `35da68f6-8880-40b2-9b3f-eb2b809b1caf`; comparison aspect `work-permission`; social article; guide step `pending-clarify-work-conditions`.

**Current project statement:** Work may begin after fingerprints and facial image are recorded, subject to the stated obvious-ineligibility exception under §1 or §2; if application of that exception is unclear, the Immigration Service advises not starting before a decision. The social-assistance article says that, as of 15 October 2026, permit holders are not covered by the separate `arbejdspligt` regime, while other individual programme/benefit requirements remain.

**Source check (22 October 2026):** The Immigration Service page “You are waiting for an answer” (last updated 11 September 2026) confirms the biometric condition, the §1/§2 limitation, and its advice where eligibility is unclear. L 43 was introduced on 8 October 2026 and the official Folketing overview says **Status: Fremsat**; Retsinformation lists it as ongoing with the bill introduced and committee work pending. Its proposed work-duty changes cover a defined group of Special Act permit holders who receive `kontanthjælp`, do not meet the applicable residence/work conditions, and are covered by the relevant integration or employment-service programme/intervention. The bill proposes the relevant provisions to take effect on 1 January 2027 and a transition deadline no later than 1 May 2027. These are proposed future rules, not current requirements on the review date. The bill’s proposed change to `arbejdspligt` does not itself amend the separate pending-application work-permit exemption.

**Classification:** Confirmed current rule and confirmed proposed change; no present-tense legal error found in the reviewed statements.

**Sources:** [Immigration Service — waiting for an answer](https://www.nyidanmark.dk/en-GB/You-are-waiting-for-an-answer/Fordreven-fra-Ukraine); [L 43 on Retsinformation](https://www.retsinformation.dk/eli/ft/202612L00043); [L 43 bill overview and status](https://www.ft.dk/samling/20261/lovforslag/L43/bilag.htm).

**Minimum recommendation / risk:** Preserve the distinction between permission to work while a case is pending and later work-duty obligations for a defined benefit/program group. Refresh L 43 and implementing rules before release, especially before the proposed 1 January 2027 effective date. Treating the proposal as enacted would wrongly state current obligations; ignoring a later enactment would make the dated social-assistance statement stale.

### DK-EXT-03 — Employment-contract threshold (P2)

**Affected record:** Employment rights article `35da68f6-8880-40b2-9b3f-eb2b809b1caf`; linked direct statute source ID `2acf95dd-1fea-45bc-9478-fda84be4496d`.

**Current project statement:** Written-information rules generally cover employees whose predetermined or actual working time averages more than 3 hours a week over 4 consecutive weeks. The text also records aggregation within the same company/group/entity, work without a guaranteed paid-hours amount, separate seafarer legislation, and collective-agreement exceptions.

**Source check (22 October 2026):** Act no. 501/2023 §1 contains that threshold and four-week period. It aggregates work within the same company, group or entity; §1(2) excludes seafarers covered by separate legislation; §1(3) includes employment without guaranteed paid hours; §1(4) and (5) provide collective-agreement exceptions with statutory equivalence/coverage conditions. The direct PDF was opened and is linked in the article. Life in Denmark’s employment-contracts page still appears in official search indexing to state the older “at least one month” and “more than 8 hours per week” test; direct page retrieval timed out, so the page itself was not independently opened during this check.

**Classification:** Confirmed that the project’s statutory rule is accurate; partially confirmed that the older official summary remains stale because only its indexed result was accessible. The direct statutory source is sufficient and already linked.

**Sources:** [Act no. 501/2023 PDF](https://www.retsinformation.dk/eli/lta/2023/501/pdf); [Life in Denmark — employment contracts](https://lifeindenmark.borger.dk/working/work-rights/working-conditions/employment-contracts).

**Minimum recommendation / risk:** Keep the article threshold and its direct Act no. 501/2023 source. Do not cite the older Life in Denmark page as authority for the coverage threshold unless that page is corrected. The old page could mislead a reader about which workers receive written employment information; no article correction is indicated by this finding.

### DK-EXT-04 — Job search language and local services (P2)

**Affected records:** Employment search article `c2356631-3fa2-4553-8fda-50a4419262c0`; comparison aspects `employment-vacancies` and `employment-institutions`.

**Current project statement:** The article says Workindenmark listings are in English but English alone is not enough for every job; readers should follow each employer’s own posting. It says the Jobnet Find Job guide is in Danish and can be used without login, and that municipal assistance varies locally. The comparison splits vacancy-search and institutional-support information into separate facts; the municipal availability caveat is in `employment-institutions`, not `employment-vacancies`.

**Source check (22 October 2026):** Workindenmark’s job portal says all portal jobs are in English, directs readers to each employer’s own posting, and says application instructions differ. Its jobseeker-service page describes English-language vacancies and guidance. The Jobnet help page is a Danish-language guide and confirms that “Find Job” does not require login. Workindenmark’s official guidance does not itself state that every employer requires Danish; it directs readers to the employer-specific advert. The project article’s more cautious “English alone is not sufficient for every job” does not promise English-only eligibility. The municipality caveat is present and appropriately local.

**Classification:** Partially Confirmed. The article adequately includes the reviewer’s three practical limits. No missing municipal caveat was found. The comparison’s vacancy fact does not say the Jobnet guide is Danish, but it also does not claim Jobnet is English-language; this is a clarity opportunity, not a factual error.

**Sources:** [Workindenmark — Job Portal](https://www.workindenmark.dk/getting-started/finding-vacancies-in-denmark/use-our-job-portal); [Workindenmark — services for jobseekers](https://www.workindenmark.dk/for-job-seekers/); [Jobnet — Find Job guide](https://jobnet.dk/hjaelp-og-support/vejledninger-og-guides/saadan-soeger-du-job-paa-jobnet).

**Minimum recommendation / risk:** No article change is required. If the owner wants the comparison to carry the same language nuance, add a short statement that Jobnet’s Find Job guide is in Danish, retaining the no-login fact. Do not state that all Danish jobs require Danish or that municipal help is universal. Omitting the optional comparison note creates limited usability risk, not a misleading entitlement or legal claim.

### BUG-008 — Municipal housing duty and §57(5) exception (P1)

**Affected records:** Housing article `19afdd19-e2cc-4da6-bde3-6dc3d64b7503`; comparison aspect `housing-initial`.

**Current project statement:** Where Integration Act §12 applies, the municipality must designate temporary accommodation but may instead designate housing. Integration Act §57(5) excludes a person granted a permit under Ukraine Special Act §2(1) from ordinary §12 placement, unless the Danish Immigration Service decides otherwise because of very special personal circumstances. The text says other municipal assignment/integration responsibilities remain separate and that no particular dwelling is guaranteed.

**Source check (22 October 2026):** Current Integration Act LBK no. 453/2026 §12(1) states the mandatory temporary-accommodation duty and discretionary housing alternative. §57(5) names only permits under Special Act §2(1), and allows the Immigration Service to decide otherwise in the specified very-special-personal-circumstances case. It does not support extending that exclusion to every §2 category or to §1 permit holders. The SIRI FAQ’s broader wording omits the statutory exception; the project text transparently notes the FAQ discrepancy.

**Classification:** Confirmed; the implementation text follows the current statute rather than relying on the broader FAQ.

**Sources:** [Integration Act LBK no. 453/2026 PDF](https://www.retsinformation.dk/eli/lta/2026/453/pdf); [SIRI Ukraine FAQ](https://integrationogjob.dk/nyankomne/faq-ukraine/).

**Minimum recommendation / risk:** No correction indicated. Preserve the exact §2(1) scope, the exceptional decision-maker/threshold, and the separation between accommodation placement and other municipal duties. Generalising the exception could wrongly suggest that all permit holders are excluded or that accommodation is guaranteed.

### BUG-009 — Housing benefit and pending Special Act application (P1)

**Affected records:** Housing article `19afdd19-e2cc-4da6-bde3-6dc3d64b7503`; comparison aspect `housing-financial-support`.

**Current project statement:** The article says a person with a Special Act residence permit may apply if they have fixed residence in Denmark and meet other conditions; it then says the reviewed guidance does not treat an application alone as fixed residence. The comparison fact limits itself to permit holder, fixed residence, and other eligibility conditions.

**Source check (22 October 2026):** The current Housing Benefits Act LBK no. 995/2025 §5 and Udbetaling Danmark’s guidance support fixed/permanent residence, an appropriate home (including a kitchen in the general guidance), and case-specific financial/household assessment. The official law and guidance reviewed do not state how the specific status of a person whose only basis is a pending Ukraine Special Act application is treated under the fixed-residence test. General guidance and older administrative materials about other residence procedures do not directly settle that distinct Special Act question.

**Classification:** Needs Evidence for the pending-application conclusion. The underlying general fixed-residence requirement is confirmed; the Special Act application-specific inference is not.

**Sources:** [Housing Benefits Act LBK no. 995/2025 PDF](https://www.retsinformation.dk/eli/lta/2025/995/pdf); [Udbetaling Danmark — Housing benefits](https://lifeindenmark.borger.dk/housing-and-moving/housing-benefits).

**Minimum recommendation / risk:** Before publication, remove or narrow the sentence “The guidance reviewed does not treat an application alone as fixed residence.” A safer formulation is that an application alone does not guarantee the fixed-residence condition and that Udbetaling Danmark must assess the individual situation. Keep the supported conditions and avoid promising an award. Leaving the categorical inference could incorrectly discourage a pending applicant from asking the competent authority.

### BUG-011 — Current `arbejdspligt` exclusion and L 43 (P1)

**Affected records:** Social assistance article `1a19c1f9-1111-4d60-8490-da548f33083f`; comparison aspects `social-subsistence` and `social-application`.

**Current project statement:** The article dates the current Ukraine Special Act permit-holder exclusion from the separate `arbejdspligt` regime as of 15 October 2026 and says this does not cancel other integration-programme or individual benefit requirements. It does not name L 43 in user-facing content or present the proposal as current law.

**Source check (22 October 2026):** Official Retsinformation describes L 43 as an introduced bill with committee work pending; the Folketing overview reports “Fremsat.” The proposal would remove specified exclusions so a defined subgroup of permit holders receiving `kontanthjælp`, not meeting the stay/work requirements, and covered by the relevant integration programme or employment-service intervention could be subject to a 37-hour weekly work-duty effort. The bill proposes 1 January 2027 commencement for the relevant provisions and transition by 1 May 2027. Those dates remain proposed. The bill’s own current-law description and official guidance confirm the existing exclusion at the review date.

**Classification:** Confirmed current rule and proposed change; no present-tense error found. High freshness risk because the proposed dates are near and the rule may change before the next content release.

**Sources:** [L 43 bill](https://www.retsinformation.dk/eli/ft/202612L00043); [Folketing L 43 status](https://www.ft.dk/samling/20261/lovforslag/L43/bilag.htm); [Retsinformation guidance on the work-duty scheme](https://www.retsinformation.dk/eli/retsinfo/2024/10228).

**Minimum recommendation / risk:** Keep the current-law statement dated; do not add L 43 as if enacted. At the next publication gate, check the bill’s final status, enacted text, commencement and any implementing rules, then revisit the subgroup and transition wording. Without that check, readers may receive stale information about work obligations or municipalities may be described as applying a proposal prematurely.

### DK-EXT-08 — Labour Market Attachment URL (P3)

**Affected records:** Residence After Protection article `2d106fd3-e981-4ef9-9733-568b654dfca5`; comparison aspects `residence-routes` and `residence-conditions`.

**Current project statement:** The official source URL contains `Labout-Market-Attachment`.

**Source check (22 October 2026):** The exact URL opens a New to Denmark page titled “Labour Market Attachment,” covering the route and its conditions. It is not a dead link. The path’s spelling is unusual but works.

**Classification:** Not Reproduced.

**Source:** [New to Denmark — Labour Market Attachment](https://nyidanmark.dk/en-GB/You-want-to-apply/Work/Labout-Market-Attachment).

**Minimum recommendation / risk:** Do not change the URL solely because of the apparent typo. Recheck it during normal link maintenance. Current user risk is low.

### DK-EXT-09 — HTML/PDF pair for Act no. 324/2022 (P3)

**Affected records:** TP application article `afc14824-4035-4ca7-9662-6d5352ba63cb`; source IDs `441b7bd9-54cf-47a8-80a1-e89a0e410952` (HTML) and `f360d680-d215-4fdf-b419-d75a63ad68a8` (PDF rendering).

**Current project statement:** Two source records name Act no. 324 of 16 March 2022: one points to the Retsinformation HTML record and one to its PDF rendering.

**Source check (22 October 2026):** Both URLs identify the same Act no. 324/2022. The PDF is a print rendering of that act and displays a 21 October 2026 print date plus a list of later amendments, including Acts no. 725/2026 and no. 766/2026. It is not a separately consolidated version of the amended act. The later amending acts are separately linked in the article’s source set.

**Classification:** Confirmed duplicate formats of the same underlying act, not evidence of two conflicting legal versions.

**Sources:** [Act no. 324/2022 HTML](https://www.retsinformation.dk/eli/lta/2022/324); [Act no. 324/2022 PDF rendering](https://www.retsinformation.dk/api/pdf/229946); [Act no. 725/2026](https://www.retsinformation.dk/eli/lta/2026/725); [Act no. 766/2026](https://www.retsinformation.dk/eli/lta/2026/766).

**Minimum recommendation / risk:** Optionally consolidate the two source records in a later editorial/source-maintenance pass if one format is sufficient. Do not call either Act no. 324 record the current consolidated text; preserve links to subsequent amendments/current legal instruments. The duplicate itself is a low-risk navigation issue, but mislabelling the historical act as consolidated would be material.

## Recommended correction order

1. Resolve or obtain explicit owner acceptance of the 2027/2028 official-guidance mismatch, keeping its attribution visible (DK-EXT-01).
2. Narrow the housing-benefit sentence about a pending Special Act application unless an authority confirms the specific fixed-residence treatment (BUG-009).
3. Before the next release, recheck L 43’s enactment/status, exact final scope, commencement, and implementing rules; keep current law and proposal separate meanwhile (DK-EXT-02 / BUG-011).
4. Keep the employment-contract threshold tied to Act no. 501/2023, not the older Life in Denmark summary (DK-EXT-03).
5. Optionally add a Jobnet-language note to the comparison fact and simplify the duplicate Act source presentation in a later maintenance change (DK-EXT-04 / DK-EXT-09).
6. No correction is currently indicated for the §57(5) housing explanation or the Labour Market Attachment URL (BUG-008 / DK-EXT-08).

## Validation and change-control record

- The review considered only the six requested Denmark article records, directly related Denmark comparison facts, the Denmark guide, their linked official sources, and directly related Denmark tests.
- Source pages rechecked for this report were checked on 22 October 2026. Where a page timed out, the report says whether only its official indexed result was available; it does not claim a direct page review.
- No `LastCheckedAt` value was changed.
- No production JSON, article text, comparison fact, guide, test, application code, status, or deployment configuration was changed.
- Full Release build/test suite was not run because this change is documentation-only.
- `git diff --check` was run for this report.
- No publication or deployment was performed.
