# Task 152 — Sweden Source Audit Batch 1: TP / Residence

As of 19 November 2026. This is an evidence audit only; production material was not edited.

## Scope and source outcomes

The exact Batch 1 set from `SOURCE_AUDIT_INPUT_LIST_01.md` contains 21 P0 sources (P1 0, P2 0), comprising the 11 `SE-TP-01` and 10 `SE-TP-02` sources. All 21 official pages/legal texts were inspected. Final status: CURRENT 20, LEGACY 1, UNCLEAR 0, BROKEN 0. Final action: KEEP 20, UPDATE 0, RETAIN_HISTORICAL 1, REMOVE 0. No successor URL was needed. The legacy record is `78f08f69-cbf2-4715-8a6c-faedcec8be27`, a dated 13 March 2025 notice retained only for the historical abolition of change-of-track route; its URL remains available and it does not establish C032.

`SOURCE_AUDIT_BATCH_01.csv` records each source, exact URL, owner, status, action and reviewed tuple counts. Existing owner identities were preserved. The four duplicate URL groups (Skatteverket Ukraine guidance, SFS 2024:691, Aliens Ordinance 2006:97 and Aliens Act 2005:716) remain separate owner records; no deduplication occurred.

## Core claim-source tuples and coverage

Batch tuples adjudicated: 160 (47 SUPPORTED; 113 NOT_SUPPORTED; 0 pending). Added one candidate tuple using already-inventoried work-permit source `af0b44aa-8339-402f-a993-bb91ff578fe4` for C032; this is the sole tuple-count delta. Cumulative graph: 519 total, 47 SUPPORTED, 113 NOT_SUPPORTED, 359 pending; arithmetic reconciles.

Coverage is rebuilt for all 283 active claims. COVERED 29, COVERED_MULTIPLE 9, UNSUPPORTED 35, REQUIRES_SOURCE_REVIEW 210, NOT_EXTERNALLY_SOURCE_DEPENDENT 0. Material: covered 25, multiple 5, review 123, unsupported 27. NumericDate: covered 9, multiple 6, review 23, unsupported 7.

## C032 mandatory resolution

Exact proposition: “One work-based route requires qualifying work within a seven-year period.” The 2025 track-change notice is LEGACY and does not contain this requirement; the tuple to it is NOT_SUPPORTED. Current Migration Agency work-permit extension guidance says the applicant must have held qualifying permits and worked in Sweden for four of the past seven years. C032 therefore has effective current support from existing source `af0b44aa-8339-402f-a993-bb91ff578fe4`; outcome: SUPPORTED_CURRENT (the separate current source record already existed in the inventory, so the legacy notice remains historical and requires no replacement URL). C032 remains CURRENT. No current claim relies solely on legacy evidence.

## Official-source audit notes

The current EU implementing decision 2026/1912 extends EU protection through 4 March 2028; the corrigendum sets the military-obligation rule’s continuous-status exception and its effective date. Swedish Migration Agency guidance distinguishes that EU horizon from individual permit cohorts and describes first-time application requirements. Travel guidance supports passport/card advice, notice on departure, and possible accommodation/support effects. Skatteverket’s current Ukraine page states the 12-month permit period, expectation of six further months, four-week notification window and the special rule for first permits from 5 September 2026. SFS 2024:691 and the Aliens Act/Ordinance were inspected in their current amended forms but do not establish the exact claims assigned to them in this candidate graph. EUR-Lex Directive 2003/109 Article 3(2)(b) directly excludes temporary-protection holders and pending applicants from that Directive’s scope, but no active claim-source candidate was mapped to that source; the existing graph was not expanded for this unsupported mapping. The July 2026 Migration Agency news page is current but does not certify the TP-specific propositions linked to it.

Zero-active-claim-candidate batch sources: `6fd70e1b-fb73-46f1-aa8e-176ec5c280a3`, `67b35a29-610f-49e3-b439-c2521683a619`, `17fa9717-a2fb-467e-815a-f0628c3f2645`. The Aliens Act record remains a legal-context production source; Directive 2003/109 remains a Compare/context source; the July news source has no exact active TP claim support and remains in the production inventory pending later source inventory decisions. No automatic removal was made.

## Compare and Watch

Batch Compare tuples reviewed: 15 (15 source-component supports; 0 not supported; 0 pending). Cumulative: 15 supported, 0 not supported, 37 pending. No compound Compare aspect is source-certified from this batch alone; 0/24 certified, 0 newly certified. Candidate dispositions remain 23 KEEP_CANDIDATE and 1 UNAVAILABLE_CANDIDATE; no source-backed final disposition was forced.

Affected Watch rows: W-SE-01 APPROVE, W-SE-02 APPROVE, W-SE-03 APPROVE, W-SE-04 EDIT. They correspond to EU scheme endpoint, Swedish permit cohort endpoints, and the expected early-2027 extension opportunity. W-SE-04 keeps an unknown exact date and uses a review reminder, not a fixed expiry.

## Gaps, reopen, integrity

Reopen register: `CLAIM_REOPEN_CANDIDATES_01.csv` contains C008. The negative proposition (“guidance does not settle every EU-law case…”) cannot be established merely from examples on Swedish pages. No translation reopen candidate was identified. High-risk claims with Batch 1 candidate tuples were marked reviewed and their certification/gap/reopen flags recorded. Unsupported claims are allowed at this stage; remaining pending claims await later batches.

Active claim universe remains 283; production Core binding baseline remains 69. No owner conflicts were introduced. Core dangling SourceIds: none; wrong-owner bindings: none. No production articles, sources, Compare facts, tests, other countries, commit, push, CI or Docker action was performed.

## Readiness

Batch 1 source audit is complete and ready for Batch 2. Remaining substantive gaps include C008 wording review, C022/C036 application-vs-permit distinctions, C031 registration-law date details, and broader claim candidates assigned to later source batches. These gaps do not leave a Batch 1 tuple pending.


## Post-audit state correction recorded in Task 153

The Batch 1 conclusion already found C032 SUPPORTED_CURRENT from `af0b44aa-8339-402f-a993-bb91ff578fe4`. Its persisted tuple, coverage and high-risk metadata had drifted afterward. Task 153 repaired that existing tuple to SUPPORTED, restored C032 coverage to COVERED and cleared the stale EvidenceGap flag. This correction does not reopen C032 or alter the substantive conclusion.
