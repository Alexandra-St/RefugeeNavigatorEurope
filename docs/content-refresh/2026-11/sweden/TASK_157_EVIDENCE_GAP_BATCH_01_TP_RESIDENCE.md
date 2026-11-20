# Task 157 — Evidence-gap Batch 1: temporary protection and residence

As of 20 November 2026. The batch resolved all 34 targeted unsupported claims, including the C008 claim reopen. No unresolved target claim remains. Production articles, OfficialSources, Compare bindings, tests, and other country files were not changed.

## Resolution summary

| Resolution action | Claims |
|---|---:|
| Existing-source support | 5 |
| New-source support | 2 |
| Rewritten to supported claims | 23 |
| Temporal reclassification as the sole action | 0 |
| Removed unsupported | 1 |
| Merged as redundant | 3 |
| Blocked | 0 |

The full row-by-row record, with original and final proposition, source IDs, coverage, and Watch impact, is in [`EVIDENCE_GAP_BATCH_01_TP_RESIDENCE.csv`](EVIDENCE_GAP_BATCH_01_TP_RESIDENCE.csv).

Claim IDs by action:

- **Existing-source support:** SE-TP-01-C005, C033, C053, C054; SE-TP-02-C002.
- **New-source support:** SE-TP-01-C055, C056.
- **Rewritten:** SE-TP-01-C001, C003, C004, C008, C015, C020, C021, C030, C031, C032, C034, C035, C036, C037, C042, C047, C048; SE-TP-02-C013, C015, C017, C021, C024, C038.
- **Merged:** SE-TP-01-C014 → C020; SE-TP-02-C018 → C024; SE-TP-02-C026 → C017.
- **Removed:** SE-TP-01-C022, an unsupported broad negative about repeat applications, interruptions, and all cases beyond the cited example.

### C008 claim reopen

- **Original:** “The reviewed Swedish public guidance does not settle every EU-law case involving third-country nationals or stateless people.”
- **Reopen reason:** a negative about the complete scope of EU law could not be established from the Swedish guidance reviewed.
- **Action:** REWRITE_TO_SUPPORTED_CLAIM; the reopen is cleared.
- **Final proposition:** Article 2(2) of Council Implementing Decision (EU) 2022/382 requires Member States to apply that Decision or adequate national protection to stateless persons and non-Ukrainian third-country nationals who held a valid Ukrainian-law permanent residence permit before 24 February 2022 and cannot return safely and durably to their origin country or region.
- **Coverage:** COVERED; source `2db46f66-3651-4889-81ae-0dd1dce97bbb`.

## New official source and deterministic ID

One staging ADD candidate was necessary to establish the formal transfer rules:

- **SourceId:** `1be057c2-a5e8-5ff6-bae6-4ca54d853fd9`
- **Owner:** SE-TP-01, article UUID `d2ab273d-21b7-44e4-b408-f4269a8753d8`
- **Title:** European Parliament and Council, Directive 2001/55/EC, Articles 26(1) and 26(4)
- **Authority / URL:** EUR-Lex; [Directive 2001/55/EC](https://eur-lex.europa.eu/eli/dir/2001/55/oj/eng)
- **Canonical URL:** `https://eur-lex.europa.eu/eli/dir/2001/55/oj/eng`
- **Stable key:** `refugeenavigator:official-source:d2ab273d-21b7-44e4-b408-f4269a8753d8:https://eur-lex.europa.eu/eli/dir/2001/55/oj/eng`
- **Convention:** UUIDv5 using `NAMESPACE_URL` and the repository’s stable-key convention. Recalculation yields the SourceId above.
- **Supported claims:** SE-TP-01-C001, C055, C056. C001 was classified as a rewrite; C055/C056 as new-source support.

No same-owner canonical URL duplicate was found. The inventory records one owner-validated ADD candidate. Existing source actions remain KEEP 67, RETAIN_HISTORICAL 1, REMOVE 1, UPDATE 0; no corrections. Planned staging Sweden source count is 69 − 1 existing REMOVE + 1 ADD = 69.

## Exact source findings and revised copy

- **Eligibility groups:** Swedish guidance lists Ukrainian nationals, people who previously had protection in Ukraine, and specified accompanying family members. Additional conditions and the Swedish first-time military-obligation rule are stated for defined cohorts. Article 2(2)’s third-country/stateless group is not broadened beyond its exact EU criteria. [Migration Agency application guidance](https://www.migrationsverket.se/en/you-want-to-apply/protection-under-the-temporary-protection-directive/apply-for-protection-under-the-temporary-protection-directive.html), [Council Decision 2022/382](https://eur-lex.europa.eu/eli/dec_impl/2022/382/oj/eng).
- **Application and decision:** the agency describes first-time and extension procedures, evidence, biometrics, and the separate decision that grants or rejects an application. A receipt remains distinct from a granted permit. The 23–65 military-obligation condition and examples of evidence were narrowed to the exact Sweden and first-time cohort in current agency guidance.
- **Permit dates:** the EU protection horizon is 4 March 2028; the agency states that Swedish permit decisions made before 5 August 2026 are valid until 4 March 2027. The revised copy keeps EU horizon and Swedish permit validity distinct. The agency’s 10 September 2026 notice says the pre-5 August cohort will have an opportunity to apply for extension in early 2027. [Migration Agency guidance](https://www.migrationsverket.se/en/you-want-to-apply/protection-under-the-temporary-protection-directive/apply-for-protection-under-the-temporary-protection-directive.html), [extension notice](https://www.migrationsverket.se/en/newsarchive/stricterruleswhenthetemporaryprotectiondirectiveisextended.5.15b2309e1a04e5ed207368.html), [EU Decision 2026/1912](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A32026D1912).
- **Population registration:** the Tax Agency decides registration, the 12-month rule applies to the stated permit cohort subject to exceptions, the one-year route uses its further-stay and other conditions, and registration is not automatic. The revised numeric wording uses the agency’s “18 months from grant” formulation. Coordination numbers and personal identity numbers are described as identifiers, not as immigration permits. [Tax Agency guidance](https://www.skatteverket.se/servicelankar/otherlanguages/englishengelska/individualsandemployees/movingtosweden/comingtoswedenfromukraine.4.1657ce2817f5a993c3a25d.html), [Act 2024:691](https://www.riksdagen.se/sv/dokument-och-lagar/dokument/svensk-forfattningssamling/lag-2024691-om-folkbokforing-av-utlanningar-med_sfs-2024-691/).
- **Work and transfer:** a granted Swedish temporary-protection permit gives work rights from age 16; an application is decided separately. A permit from another Member State does not automatically authorize residence in Sweden. Directive 2001/55/EC Article 26 requires the person’s consent for a formal transfer and states that the departure-state permit expires and its obligations end after transfer.
- **Alternative residence routes:** Act 2026:456 and Migration Agency guidance support specified in-country applications from 11 June 2026, route-specific requirements, qualifying family applications, the pre-expiry grant condition, and distinct support consequences for registered and unregistered people. The broad statement about the filing deadline not guaranteeing continued residence was replaced by the Act’s exact condition for a grant after the current permit expires. [Act 2026:456](https://www.riksdagen.se/sv/dokument-och-lagar/dokument/svensk-forfattningssamling/lag-2026456-om-uppehallstillstand-i-vissa-fall_sfs-2026-456/), [Migration Agency route guidance](https://www.migrationsverket.se/en/you-have-a-permit-in-sweden/protection-under-the-temporary-protection-directive/you-want-to-apply-for-a-different-type-of-permit.html).
- **Long-term resident status:** the revised claims use the Migration Agency’s published five-year conditions and the EU Directive’s express exclusion of people currently authorized under temporary protection or awaiting that status. The unresolved claim about how past TP time is counted after a later status change was merged into the directly supportable Directive-scope claim and is not stated as established fact. [Migration Agency long-term resident guidance](https://www.migrationsverket.se/en/you-want-to-apply/long-term-residents-in-sweden.html), [Directive 2003/109/EC](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A32003L0109).

Both TP review copies were updated in EN/UK/RU. Changed claims remain atomic; three redundant/unsupported claim rows are explicitly inactive with merge lineage, and C022 is removed rather than preserved as guidance. The alignment register records no material, numeric/date, or missing-proposition divergence.

## Coverage and effective Core graph

There are 279 active claims: SE-TP-01 41, SE-TP-02 28, SE-EMP-01 27, SE-EMP-02 28, SE-HEALTH-01 22, SE-HOUSING-01 25, SE-EDU-01 51, SE-SOC-01 57. MATERIAL 177; SUPPORTING 102; NumericDate 46; TemporalClass CURRENT 262, FUTURE 3, HISTORICAL 5, TRANSITION 9. Active compound and non-propositional claims remain 0; duplicate active ClaimIds and orphan lineage rows are 0.

Coverage: COVERED 190, COVERED_MULTIPLE 50, UNSUPPORTED 39, REQUIRES_SOURCE_REVIEW 0. MATERIAL unsupported 15; NumericDate unsupported 2. All 30 retained targeted claims are covered (29 single-source, 1 multiple-source). Four targeted claims are inactive: three merges and one removal.

The original 614 Core pairs were retained as source-audit history. Task 157 appended 79 exact claim/source retest rows. In the effective graph, 29 new pairs were added and 10 pairs for inactivated claims were retired, for a net increase of 19: **633 total, 293 SUPPORTED, 340 NOT_SUPPORTED, 0 unresolved**. Two existing effective candidate pairs changed from NOT_SUPPORTED to SUPPORTED on retest. Core dangling SourceIds 0; wrong-owner bindings 0. The ledger’s effective state follows the latest proposition-level retest; earlier source-audit findings remain visible as history.

## Six TP/residence Compare aspects

All six are source-certified in staging; the certified total rises from 11/24 to 17/24. Two dispositions are REVISE and four KEEP:

| Aspect | Disposition | Final scope |
|---|---|---|
| protection-scheme | REVISE | EU mechanism and Article 2(2) cohort with statutory qualifications |
| protection-application | KEEP | Current Swedish application route, listed eligibility/evidence, separate decision |
| protection-dates | KEEP | EU 4 March 2028 horizon vs Swedish permit cohort date |
| residence-routes | KEEP | Limited in-country routes from 11 June 2026 and route-specific requirements |
| residence-conditions | REVISE | Route requirements, permit-status distinction, long-term status limits |
| residence-filing | KEEP | Act 2026:456 pre-expiry condition and narrow route conditions |

The staged Compare graph now has 59 tuples: 59 SUPPORTED, 0 NOT_SUPPORTED, 0 unresolved; dangling Compare SourceIds 0. Production Compare remains 24 aspects / 52 bindings.

## Watch, high risk, and remaining work

W-SE-04 is the only affected Watch row; it remains EDIT, now also names C042, and tracks the published early-2027 extension expectation. No date correction was needed. The earliest certified future legal/effective trigger remains 1 January 2027 (SFI); earliest maintenance pre-check remains 1 December 2026.

There are 24 targeted high-risk register rows: 20 retained rows are source-certified; 4 merged/removed rows are marked inactive; blocked high-risk gaps 0. The unrelated cumulative health reopen SE-HEALTH-01-C007 remains open; C008 is cleared. No temporal or translation reopen was created. No active claim relies solely on LEGACY or RETAIN_HISTORICAL evidence.

The remaining evidence-gap queue has 39 rows: P0 16, P1 23, P2 0. These are outside this TP/residence batch. Seven Compare aspects remain uncertified. The TP/residence target itself has no substantive blocker.

## Verdict

**SWEDEN EVIDENCE GAP BATCH 1 COMPLETE**
