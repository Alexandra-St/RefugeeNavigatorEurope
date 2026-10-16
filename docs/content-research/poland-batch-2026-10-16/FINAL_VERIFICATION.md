# Poland first batch — final focused verification

**Verification date:** 16 October 2026
**Baseline:** `32b7042780721db3e11977633d6f9d5b668a7c1c`
**Outcome:** `POLAND FOCUSED VERIFICATION COMPLETE — AWAITING INTEGRATION DECISION`

## Batch completeness

- Eight article folders, 24 language drafts (UK/EN/RU) and eight source/claim matrices are present.
- All 24 drafts remain research-only and unpublished. No translation was marked `Verified`.
- Source check dates record when pages were opened; they are not represented as source publication dates.
- Only Poland research and editorial documents in this batch are in scope. Production catalogs, application code, tests, Denmark, Germany and deployment settings were not changed.

## Article-by-article decision

| # | Topic | Assessment | Evidence boundary / publication-time gate |
|---|---|---|---|
| 1 | Applying for Temporary Protection in Poland | **READY FOR INTEGRATION** | Corrigendum directly confirms 5/4 Aug dates and same-Member-State continuity. A KSI page still shows superseded 31/30 Jul; the draft attributes it as stale guidance. Check the page again before release and never infer a person’s record validity. |
| 2 | Residence Options After Temporary Protection | **READY FOR INTEGRATION** | CUKR conditions, child distinction, deadline and MOS filing scope are supported. Effects of an individual application/permit remain outside the general article. |
| 3 | Finding a Job in Poland | **READY FOR INTEGRATION** | ePraca and qualified PUP navigation are supported; local service access is conditional. |
| 4 | Work Authorization and Employee Rights | **READY FOR INTEGRATION** | PIP notification/contract guidance is supported. Refresh the date-sensitive 2026 wage amount before publication. |
| 5 | Accessing Healthcare in Poland | **READY FOR INTEGRATION** | NFZ’s limited statutory groups, exclusions and insurance route are distinguished. Coverage and provider availability require individual confirmation. |
| 6 | Finding Accommodation in Poland | **READY FOR INTEGRATION** | Act 203 art. 112 and consolidated tenant law support the narrow claims. Funding, vulnerable-group conditions and local implementation are explicit; no national placement is promised. |
| 7 | Education and Polish-Language Learning | **READY FOR INTEGRATION** | Current 2026 education-law codification supports age rules and bounded Polish-language support. Local capacity and universal free adult courses are not promised. |
| 8 | Financial and Social Assistance | **READY FOR INTEGRATION** | Current ZUS and statutory rules support the qualified 800+ and assistance navigation. No benefit is inferred from PESEL UKR alone. |

Readiness means suitable for the next integration-review step, not approved content or publication permission. Human editorial review is still required.

## High-impact corrections and findings

1. **EU corrigendum:** The official EUR-Lex Official Journal PDF for the 5 August 2026 corrigendum was directly opened and read. It changes the Article 2 application date to **5 August 2026** and changes the exception’s reference date to **4 August 2026**. The exception requires uninterrupted temporary protection in the same Member State. The corrected dates and condition are consistent in all three temporary-protection drafts.
2. **EU / Polish stay / certificates:** Decision (EU) 2026/1912 extends EU protection through **4 March 2028**, applying from **5 March 2027**. Under Polish Act 203 Article 106, the stay of a defined beneficiary group is lawful while that Council decision remains in force, unless the Act provides otherwise and subject to Articles 109a–109b. The MRPiPS page dated 26 March 2026 stated 4 March 2027 before the later Council decision; it is dated administrative guidance, not a different current statutory expiry for that group. Older paper certificates have a separate stated validity through 4 March 2027, subject to earlier-ending events. Individual records and permits are not inferred.
3. **Residual official-page discrepancy:** UdSC’s August page gives the corrected military dates; KSI’s directly opened gov.pl page still displays 31/30 July. The drafts name this as superseded KSI page content, not a conflict in the corrected legal text. Recheck whether KSI updates it before publication.
4. **CUKR/MOS:** Four cumulative CUKR conditions, separate child rules, online CUKR filing and the 4 March 2027 deadline are recorded. The broader MOS guide includes specified paper-filing exceptions for ordinary permit routes. Filing, permit grant and protection status remain separate; case-specific consequences are not claimed.
5. **Healthcare:** NFZ identifies limited groups for the statutory scope; insurance-based access is separate. Emergency treatment is not described as comprehensive routine coverage.
6. **Housing:** Act 203 art. 112 permits short-term accommodation/meals within activated funds and statutory limits, plus a separate resource-dependent route for defined vulnerable groups unable to self-accommodate. Regional Mazowieckie information is not generalized. Housing allowances and private tenancies have separate rules.
7. **Education:** ELI’s in-force Education Law codified text prepared 16 September 2026, incorporating the later listed 2026 amendments, was opened; relevant age duties and Article 165 language-support provisions were checked. The drafts specify up to 24 months of free additional Polish for pupils in statutory scope and up to 12 months for a possible language-of-origin teaching assistant. Classes/assistants and adult courses are not guaranteed nationwide.
8. **Social assistance:** Act 203 Article 112a says assistance may be granted under Social Assistance Act rules and procedure. The drafts do not claim automatic entitlement. ZUS’s foreign-beneficiary guidance was opened for residence/status, schooling, work-activity and suspension checks.
9. **Employment:** The two employment drafts were preserved. PIP’s 7-day employer notification and contract guidance remain supported; PLN 4,806 is the dated 2026 monthly minimum wage, to refresh before release. Local PUP services remain conditional.

## Claim and source audit

- Eight matrices contain **45** claim rows: `SUPPORTED` 33, `PARTIALLY SUPPORTED` 4, `CONFLICTED` 0, and `UNVERIFIED` 8.
- **36** unique official URLs are listed. Access limits and superseded source text are explicitly noted.
- All eight unverified propositions remain excluded from article copy. Four partial claims are constrained by their stated local, programme or individual-condition caveats.
- Translation review confirms equivalent legal qualifications in UK/EN/RU for the corrected protection dates, same-state continuity, Article 106 cohort, CUKR/MOS process, health coverage, education ages/language support, housing and benefit conditions.

## Remaining publication gates

- Recheck whether the KSI page has been corrected; if not, retain the explicit attribution to its superseded wording.
- Recheck legislation and agency pages immediately before publication, including current consolidated acts, CUKR/MOS instructions and the benefit period.
- Confirm personal legal-status records with the competent authority; general articles cannot determine an individual file.
- Confirm current local PUP, healthcare-provider, housing, school and OPS/MOPS availability for any location-specific instructions.
- Refresh the 2026 minimum-wage figure and complete final human editorial/translation review of all 24 drafts.

## Validation performed

- Inventory: eight folders, 24 drafts, eight matrices and two batch-level documents.
- Source IDs and exact URLs cross-checked against the article source lists; newly relied-on official materials were added to the affected drafts.
- All material corrections aligned across UK/EN/RU; no unsupported claim was added.
- `git diff --check`: passed.
- `dotnet restore RefugeeNavigatorEurope.slnx`: passed.
- `dotnet build RefugeeNavigatorEurope.slnx -c Release`: passed with 0 warnings and 0 errors.
- `dotnet test RefugeeNavigatorEurope.slnx -c Release`: passed, 352/352 tests.
