# Task 145 — Third-certification blocker repair

**Verdict: FRANCE THIRD-CERTIFICATION BLOCKERS RESOLVED.** This is a staging repair only and is not an approval for production integration. Task 146 is the fourth independent final-certification gate. As-of: 19 November 2026.

## Deterministic SourceId migration

Recomputed UUIDv5 with UUID namespace URL and stable key:

`refugeenavigator:official-source:d16ad009-3391-5e3b-80b2-5b0d348503d3:https://eur-lex.europa.eu/eli/dec_impl/2022/382/oj/eng`

Computed SourceId: `fdf859e7-da78-52eb-88e9-0343805006d8` (UUIDv5), matching Task 145’s expected value. Migrated the staging inventory and all active source references from `d0f6fb72-2f27-487d-8cc2-0cfdd32506f9` (UUIDv4). Owner FR-TP-01; canonical URL as above. Affected core claims C021–C024; Compare aspect protection-scheme; affected Watch IDs: none. Migration complete: yes. Historical Task 143/144 certification reports retain the former ID as audit history.

## C023 exact scope and NumericDate

C023 is now: “Member States shall apply either Decision (EU) 2022/382 or adequate protection under national law to stateless persons and nationals of third countries other than Ukraine who can prove that they were legally residing in Ukraine before 24 February 2022 on the basis of a valid permanent residence permit issued in accordance with Ukrainian law and are unable to return in safe and durable conditions to their country or region of origin.” This tracks Article 2(2) of the official EUR-Lex decision. Current source tuple is SUPPORTED; NumericDate=YES is justified by the explicit cutoff. C021, C022 and C024 remain unchanged in scope and current-source supported.

## Six historical-only claims

| ClaimId | Final treatment | Materiality | NumericDate | Temporal class | Sole supporting source / status |
|---|---|---|---|---|---|
| FR-TP-01-C002 | Page publication date fact | MATERIAL | YES | HISTORICAL | 04f61765-f6e7-5f22-87c0-60f6382fa42c; LEGACY / RETAIN_HISTORICAL |
| FR-SOC-01-C025 | May 2024 form content fact | MATERIAL | YES | HISTORICAL | aa0f80be-7a98-5b27-a6e2-38286081f4ed; LEGACY / RETAIN_HISTORICAL |
| FR-SOC-01-C026 | May 2024 form content fact | MATERIAL | YES | HISTORICAL | aa0f80be-7a98-5b27-a6e2-38286081f4ed; LEGACY / RETAIN_HISTORICAL |
| FR-SOC-01-C040 | May 2024 form instruction fact | SUPPORTING | YES | HISTORICAL | aa0f80be-7a98-5b27-a6e2-38286081f4ed; LEGACY / RETAIN_HISTORICAL |
| FR-SOC-01-C041 | May 2024 form date fact | SUPPORTING | YES | HISTORICAL | aa0f80be-7a98-5b27-a6e2-38286081f4ed; LEGACY / RETAIN_HISTORICAL |
| FR-SOC-01-C042 | May 2024 form scope fact | SUPPORTING | YES | HISTORICAL | aa0f80be-7a98-5b27-a6e2-38286081f4ed; LEGACY / RETAIN_HISTORICAL |

All six now explicitly describe dated artifacts in past tense, rather than current actionable guidance. The taxonomy distinguishes historical artifacts from presently operative advice, so HISTORICAL is appropriate. Existing current Social guidance and C039 were not changed.

## Compare and integration arithmetic

`protection-scheme` now names the Article 2(2) eligibility cohort and includes the deterministic current source in candidate bindings. The exact aspect/source tuple is SUPPORTED. Compare certification: 24/24; unresolved tuples: 0; dangling: 0. Effective Compare tuple count rises by one from 88 to 89: 86 SUPPORTED / 3 NOT_SUPPORTED / 0 unresolved. Compare binding delta: KEEP 59 / ADD 18 / REMOVE 12. Core effective tuples remain 423: 210 SUPPORTED / 213 NOT_SUPPORTED / 0 unresolved; the SourceId substitution preserves the existing C021–C024 pairs. Core binding delta remains KEEP 83 / ADD 14 / REMOVE 21.

Source plan remains production 104, staging 118, ADD 14, UPDATE 8, RETAIN_HISTORICAL 2, REMOVE existing 0; planned France 118 and global 1,544. The UUID correction replaces the one staging ADD and does not add another source.

## Reconciled claim counts and regressions

Active substantive claims: 172 (117 MATERIAL, 55 SUPPORTING). NumericDate=YES: 42 (39 baseline + three Social artifact claims); C023 remains YES. Temporal classes: CURRENT 159, HISTORICAL 6, TRANSITION 7, FUTURE 0. Only the six historical-only claims changed temporal class; NumericDate changed NO→YES for C025, C026 and C040.

Coverage: 140 single-source and 32 multiple-source covered; UNSUPPORTED 0; REQUIRES_SOURCE_REVIEW 0; material unsupported 0; NumericDate unsupported 0. High-risk TP C021–C024: PASS. No actionable CURRENT Social claim relies solely on RETAIN_HISTORICAL evidence; C039 support retained. SMIC regression unchanged: metropolitan €12.31/hour and €1,867.02/month; Mayotte €9.56/hour and €1,449.93/month, effective 1 June 2026. Watch: 14 total, 11 APPROVE / 3 REMOVE; stale approved ClaimIds 0; REQUIRES_GAP_RESOLUTION 0.

Modified TP and Social EN/UK/RU copies were reconciled for the changed scope and dates; no material translation or numeric/date divergence was identified. No production entries, OfficialSources, comparison facts, tests, other countries, commits, or pushes were changed.

## Resolved blockers and next gate

Task 144’s three explicit blockers are resolved: deterministic UUIDv5 migration, Article 2(2) C023 correction, and current `protection-scheme` evidence binding. All six historical-only CURRENT claims now accurately describe dated artifacts and are classified HISTORICAL. No Task 145 blocker remains. This report does not self-approve integration; Task 146 must independently certify the repaired package.
