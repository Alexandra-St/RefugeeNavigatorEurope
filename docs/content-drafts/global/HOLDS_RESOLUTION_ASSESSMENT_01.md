# Global Holds Resolution Assessment 01

**Assessment date:** 15 November 2026
**Production baseline:** `a2b4b26c8d17db98d3663e1871fc03fdda535010` (`main` = `origin/main`)
**Scope:** the three Netherlands `NL-TP-01` translations at `NeedsReview`, five `Draft` Compare facts, and seven unavailable Compare facts. This is a read-only assessment; no production data or tests were changed.

## Core holds

### Netherlands — NL-TP-01

- **Article ID:** `244b1055-4d8c-55b2-820e-60ff018c3b1f`
- **Production translation states:** EN `NeedsReview`; UK `NeedsReview`; RU `NeedsReview`.
- **LastUpdatedAt:** `2026-10-21T02:12:48+00:00` for all three translations.
- **Production visibility:** the Netherlands country remains visible, but this article is filtered from production category content while its active-language translation is not `Verified`. The linked-article Compare gate also suppresses its three available facts. This is a status gate, not a missing record.
- **Source/citation inventory:** 8 official source records, all owned by this article and all HTTPS; the integration test expects 8. The research matrix lists nine research references, but its ninth is the metadata-only PUC listing for inaccessible bulletin IB 2026/34; the article explicitly says that listing is not evidence of the bulletin’s contents, so it is not an omitted production source. There are **0 inline `[[source:...]]` citation tokens** in each production translation (EN/UK/RU). Sources are attached in the article source list. All eight production records were last checked on 20 October 2026.
- **Structural and language state:** the draft has all six claim-coverage mappings, complete EN/UK/RU title, summary and content, and explicit editorial exclusions. The existing translation review found no material EN/UK/RU divergence. The three production translations are present; this is not a missing-translation or integration defect.

**Reason for hold — `A. INTENTIONAL — EVIDENCE INSUFFICIENT`.** The hold is deliberate, not stale. The 21 October selective publication review, owner decision and later integration test all exclude NL-TP-01 while admitting the other seven Netherlands Core entries. The owner decision explicitly withholds preliminary approval in all languages. It records unresolved evidence about the EU/IND military-obligation exception and its scope, an inaccessible IND bulletin (IB 2026/34) whose contents were not used as evidence, and the absence of a verified review/appeal route after negative municipal triage. The article also correctly preserves the difference between the EU scheme horizon and the validity of each person’s IND proof document. Existing freshness research checked the linked sources on 20 October and documents the live source wording conflicts; it does not close those blockers.

The article is structurally complete, the translations are materially aligned, the eight attached sources resolve to the article, and the text explicitly identifies the older Government.nl 2027 wording and the unverified IB contents. No source-owner, HTTPS, missing-source, or translation-parity defect was found. Those checks do not resolve the substantive eligibility/exception and negative-triage gaps.

**Decision:** **TARGETED RESEARCH REQUIRED; KEEP HIDDEN.** `NeedsReview` is current and should not be treated as stale.

**Required next action:** obtain readable authoritative text for IB 2026/34 or an equivalent; reconcile its military evidence/exception scope with the corrected EU Decision and current IND wording; establish the competent authority and steps/deadlines, if any, for review after negative triage. Then perform a bounded EN/UK/RU review of the affected passages and obtain explicit editorial acceptance before any status change. Until then, retain all three `NeedsReview` states and keep the article hidden.

## Compare holds

There are **5 Draft records**, **7 unavailable records**, and **10 unique affected facts**. The Netherlands `residence-filing` and Sweden `social-child-benefits` rows are each in both sets. Every affected fact has an existing owner entry and EN/UK/RU values. The seven unavailable facts have no Compare source IDs and use the localized “not established in the reviewed sources” value; this is the expected evidence-gap fallback.

| Country / aspect | Status | Available | Owner article (ID) | EN/UK/RU | Fact source IDs | Reason and evidence state | Decision / next action |
|---|---|---:|---|---|---|---|---|
| Netherlands / `protection-scheme` | Draft | Yes | NL-TP-01 (`244b1055-4d8c-55b2-820e-60ff018c3b1f`) | Yes | 3; all exist, HTTPS and owner-matched | **Reason A — intentional, evidence insufficient.** Deliberately deferred with NL-TP-01. Existing source matrix records a material unresolved military-obligation scope and IND/EU exception wording conflict. The fact’s general cohort wording does not resolve that condition. | **TARGETED RESEARCH REQUIRED.** Establish the precise scope/exception and accessible IND evidence as listed under Core holds; then review wording and all three languages before any promotion. |
| Netherlands / `protection-application` | Draft | Yes | NL-TP-01 (`244b1055-4d8c-55b2-820e-60ff018c3b1f`) | Yes | 2; all exist, HTTPS and owner-matched | **Reason A — intentional, evidence insufficient.** Municipal-to-IND triage steps are described, but an authoritative negative-triage review route is not established; military-screening boundaries also remain incomplete. | **TARGETED RESEARCH REQUIRED.** Establish the negative-triage route and resolve the material military scope before approval. |
| Spain / `protection-dates` | Approved | No | ES-TP-01 (`b278c1d6-1add-4e65-8af1-e16431eaffb3`) | Yes | 0 on fact; relevant owner records exist | **Reason E — stale availability; fact is now supported.** The Spain review deliberately used an unavailable fallback to avoid combining the EU scheme horizon with Spain’s separate TIE validity. Existing reviewed primary-law evidence distinguishes the two: Decision (EU) 2026/1912 and its corrigendum set the EU scheme horizon at 4 March 2028; Order INT/96/2026 extends covered Spanish TIE cards through 4 March 2027. | **READY FOR APPROVAL + AVAILABILITY**, with a strictly attributed, cohort-limited statement distinguishing the scheme date from covered TIE validity and directing readers to check their own document. In a separately authorized update, attach the existing owner-matched EU Decision, corrigendum and Order records, then review the three localized values. No new source is indicated by this assessment. |
| Netherlands / `protection-dates` | Draft | Yes | NL-TP-01 (`244b1055-4d8c-55b2-820e-60ff018c3b1f`) | Yes | 3; all exist, HTTPS and owner-matched | **Reason G — reason for this individual fact’s deferred state is unclear.** The fact itself already separates the 4 March 2028 EU scheme horizon, the stale English Government.nl 2027 wording, and individual IND-document validity. Existing research supports this qualified distinction. The owner decision deferred it with the unapproved parent article, so no independent approval is documented. | **TARGETED REVIEW REQUIRED.** Review this fact independently against its three existing sources and accept its language variants. It may then proceed to approval; its production rendering will still remain gated by NL-TP-01’s `NeedsReview` status. |
| Netherlands / `residence-filing` | Draft | No | NL-TP-02 (`c5f5f833-0ab7-5e1a-9325-87a92b21d2ad`) | Yes | 0 | **Reason A — intentional, evidence insufficient.** The reviewed route-specific sources do not establish a general in-country filing process for a temporary-protection holder. Owner decision and publication-gate review say to keep it unavailable and source-less. | **KEEP UNAVAILABLE — INTENTIONAL.** Do not infer a general route from unrelated permit pages. Reopen only if an authoritative source establishes a general filing process. |
| Denmark / `employment-assistance-conditions` | Approved | No | DK finding-jobs article (`c2356631-3fa2-4553-8fda-50a4419262c0`) | Yes | 0 | **Reason unclear.** The Compare fact has no sources and no fact-specific explanation was found. The linked Core article does say a municipality may offer job-search help, but the existing citations support Workindenmark and Jobnet navigation, not clearly the municipal assistance conditions. Do not infer that the Core statement supports every service or eligibility condition. | **TARGETED REVIEW REQUIRED.** Crosswalk the existing owner article and its five official records against a narrowly worded municipal-help statement. If one directly supports it, bind that existing owner source and retain “may”/local-availability limits; otherwise leave unavailable and identify the exact missing condition for later research. |
| Spain / `housing-financial-support` | Approved | No | ES-HOUSING-01 (`bd08644f-dd27-412c-9bfd-888de1fe304d`) | Yes | 0 | **Reason A — intentional, evidence insufficient.** Existing housing research says no national rent-aid entitlement was established and local housing aid/criteria were not researched. | **TARGETED RESEARCH REQUIRED.** Check whether an official national or applicable regional/local rent-support route exists for this cohort, and its residence, income and application conditions. Until then, do not imply that aid is absent or available. |
| Germany / `education-pupil-language` | Approved | No | DE-EDU-01 (`f5e0f499-0f60-5465-85f8-de6345029b59`) | Yes | 0 | **Reason A — intentional, evidence insufficient.** Existing claim review shows: Land-level school procedures are distinct from adult BAMF integration/vocational-language courses; no uniform school-enrolment or pupil-language entitlement was established. The available owner sources do not justify importing adult course access into a child/pupil fact. | **TARGETED RESEARCH REQUIRED.** Check official Länder/KMK sources for school-age German-language support routes and conditions, then state the variation by Land. Keep unavailable until a source-backed pupil-specific statement is established. |
| Spain / `social-child-benefits` | Approved | No | ES-SOC-01 (`d33628da-cb49-44b5-a7aa-ae6c6ac8f389`) | Yes | 0 | **Reason A — intentional, evidence insufficient.** Existing social-assistance research documents the general IMV child supplement but explicitly says temporary-protection-specific access was not established. | **TARGETED RESEARCH REQUIRED.** Establish, from official Social Security/BOE sources, whether and under what residence/status/household conditions a temporary-protection holder can qualify for the IMV child supplement or another relevant child benefit. Do not imply automatic entitlement. |
| Sweden / `social-child-benefits` | Draft | No | SE-SOC-01 (`8b5cc53a-9933-4366-8421-d795966267ec`) | Yes | 0 | **Reason A — intentional, evidence insufficient.** The Sweden owner decision and focused research readiness record confirm: reviewed evidence did not establish a sufficiently bounded child-benefit answer. Those documents explicitly say this is not proof that no benefit exists. | **KEEP UNAVAILABLE — INTENTIONAL.** Leave Draft, unavailable and source-less. Reopen only with a bounded official-source answer; do not turn the evidence gap into a negative entitlement claim. |

### Source integrity

- All 10 owner entries exist; all 10 have non-empty EN/UK/RU fact values.
- The 3 available NL-TP-01 facts cite 8 fact-to-source bindings in total; all 8 source IDs exist, belong to NL-TP-01, and use HTTPS.
- The other 7 facts are unavailable and source-less by design. No missing, cross-owner, or non-HTTPS fact binding was found.
- NL-TP-01 has 8 owner-matched HTTPS source records, with 0 inline citation tokens in each language. The count of linked source records and inline citations is therefore explicitly distinguished.
- No data or integration error was found. The test suite asserts all seven unavailable country/aspect pairs and the generic localized fallback. For NL, tests also encode that the three TP-01 facts are gated by the linked article’s Verified status.

## Counts

| Inventory / disposition | Count |
|---|---:|
| Core NeedsReview translations | 3/3, classified |
| Compare Draft records | 5/5, classified |
| Compare unavailable records | 7/7, classified |
| Unique affected Compare facts | 10 |
| Compare facts ready for approval + availability | 1 |
| Compare facts requiring targeted review | 2 |
| Compare facts requiring targeted research | 5 |
| Compare facts to keep Draft for insufficient evidence without a defined research task | 0 |
| Compare facts to keep unavailable intentionally | 2 |
| Compare data/integration errors | 0 |
| Core article requiring targeted research | 1 (all three translations remain held) |

**Exact unique affected set:** Netherlands `protection-scheme`, `protection-application`, `protection-dates`, `residence-filing`; Spain `protection-dates`, `housing-financial-support`, `social-child-benefits`; Denmark `employment-assistance-conditions`; Germany `education-pupil-language`; Sweden `social-child-benefits`.

## Holds ready to remove

- **Compare:** Spain `protection-dates` is evidence-ready for a narrowly qualified fact. A separately authorized update still needs to supply the reviewed wording and attach the existing owner-matched legal sources before availability can be changed. No production change is made here.
- **NL-TP-01-linked Compare:** `protection-dates` has adequate existing evidence for the scheme/document distinction, but requires a focused editorial review and remains hidden behind NL-TP-01. It is not counted as ready to remove yet.
- **Core:** none. The NL-TP-01 hold is not stale.

## Holds requiring targeted review

1. Netherlands `protection-dates`: accept the existing qualified scheme/date distinction and verify the three language variants.
2. Denmark `employment-assistance-conditions`: determine whether existing owner sources directly support a municipal-help statement and its limits; source history does not explain the unavailable decision.

## Holds requiring targeted research

1. **Netherlands NL-TP-01, `protection-scheme` and `protection-application`:** accessible authoritative military-obligation proof/exception scope and how current IND wording aligns with the corrected EU provision; the competent route and any review deadlines after negative municipal triage.
2. **Spain `housing-financial-support`:** whether a national or relevant local/regional rent-support programme applies to this group and its actual eligibility criteria.
3. **Germany `education-pupil-language`:** official school-age pupil German-language support routes and conditions across Länder, distinct from adult language courses.
4. **Spain `social-child-benefits`:** temporary-protection holders’ eligibility, if any, for the IMV child supplement or another relevant child benefit.

NL `residence-filing` and Sweden `social-child-benefits` stay intentionally unavailable; their existing evidence reviews do not justify a general affirmative answer.

## Intentional holds to keep

- **Core:** NL-TP-01 remains hidden in all three languages pending its identified evidence blockers and explicit editorial acceptance.
- **Compare:** Netherlands `residence-filing` and Sweden `social-child-benefits` remain Draft/unavailable/source-less. Their tests and review documents preserve the “not established” fallback; neither is a claim that a route or benefit does not exist.

The Spain and Germany unavailable fallbacks also remain in force until the targeted research questions above are resolved. Their current `Approved` status approves the stored fallback state; it does not establish the underlying benefit or service.

## Data/integration errors

**None found.** Fact owners, language values and existing bindings are structurally valid. The absence of source IDs on unavailable facts matches their unavailable behavior. The Netherlands NL-TP-01 facts have existing owner-matched sources but remain Draft due the documented parent/article review gate and claim-specific evidence limits.

## Freeze impact

The holds are not all ready for cleanup. The existing evidence supports one currently unavailable fact (Spain `protection-dates`) for a qualified approval path; two facts need focused editorial review; five Compare facts and the NL-TP-01 article require targeted research; two other facts should remain intentionally unavailable. The two NL protection facts stay Draft while that research is pending. There are no data defects to fix. A strict Content Freeze that requires every translation Verified and every fact Approved/available is **not ready**. Freeze can proceed only if the documented exceptions and remaining research/review items are explicitly accepted as excluded from frozen publishable content.

## Verdict

**TARGETED RESEARCH REQUIRED**

Production holds were not modified. Apply the bounded research and reviews above, then run a separately authorized status/availability cleanup. Until then, retain all current statuses and availability values.
