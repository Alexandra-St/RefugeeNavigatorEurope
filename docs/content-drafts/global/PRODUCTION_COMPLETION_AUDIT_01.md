# Production Completion Audit 01

## Release snapshot

- Branch: `main`
- HEAD: `a2b4b26c8d17db98d3663e1871fc03fdda535010`
- `origin/main`: `a2b4b26c8d17db98d3663e1871fc03fdda535010`
- Working tree at audit start: clean (`## main...origin/main`).
- Expected commit `a2b4b26c8d17db98d3663e1871fc03fdda535010`: exact match.
- No production checks, builds, tests, Docker, network research, or changes were performed.

## Global counts

| Measure | Result |
|---|---:|
| Catalog countries | 19 |
| Core articles | 152 |
| Core translations | 456 |
| Verified | 453 |
| NeedsReview | 3 |
| Outdated | 0 |
| Official source records / unique IDs | 1462 / 1462 |
| Unique official URLs | 1270 |
| Compare countries / topics / aspects / facts | 19 / 8 / 24 / 456 |
| Compare Approved / Draft | 451 / 5 |
| Compare available / unavailable | 449 / 7 |

## Country coverage

All 19 countries have 8 Core entries, 24 translations and 24 Compare fact records. Production country visibility follows `CountryVisibility.GetVisibleCountries`: a country is accepted when at least one article has a Verified translation. The Category page filters each article by Verified status for the selected language. Development returns every catalog country.

| Country | Slug | Country ID | Core articles | Translations | Verified | NeedsReview | Outdated | Production visible | Compare supported | Compare facts |
|---|---|---|---:|---:|---:|---:|---:|---|---|---:|
| Denmark | `denmark` | `10000000-0000-0000-0000-000000000001` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Germany | `germany` | `10000000-0000-0000-0000-000000000002` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Poland | `poland` | `10000000-0000-0000-0000-000000000003` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Czechia | `czechia` | `10000000-0000-0000-0000-000000000004` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Spain | `spain` | `10000000-0000-0000-0000-000000000005` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Sweden | `sweden` | `10000000-0000-0000-0000-000000000006` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Netherlands | `netherlands` | `10000000-0000-0000-0000-000000000007` | 8 | 24 | 21 | 3 | 0 | Yes | Yes | 24 |
| Romania | `romania` | `10000000-0000-0000-0000-000000000008` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Slovakia | `slovakia` | `10000000-0000-0000-0000-000000000009` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Bulgaria | `bulgaria` | `10000000-0000-0000-0000-000000000010` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Austria | `austria` | `10000000-0000-0000-0000-000000000011` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Italy | `italy` | `10000000-0000-0000-0000-000000000012` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Lithuania | `lithuania` | `10000000-0000-0000-0000-000000000013` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Ireland | `ireland` | `10000000-0000-0000-0000-000000000014` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| France | `france` | `10000000-0000-0000-0000-000000000015` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Finland | `finland` | `10000000-0000-0000-0000-000000000016` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Belgium | `belgium` | `10000000-0000-0000-0000-000000000017` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Hungary | `hungary` | `10000000-0000-0000-0000-000000000018` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |
| Norway | `norway` | `10000000-0000-0000-0000-000000000019` | 8 | 24 | 24 | 0 | 0 | Yes | Yes | 24 |

- Catalog countries: 19/19.
- Production-visible countries: 19/19.
- Development-visible countries: 19/19.
- Compare-supported countries: 19/19.
- Production-visible Core articles per language: 151/152 (the same held article is excluded for EN, UK and RU).

## Core completeness

- Owner/category shape: 152/152 articles in 8-owner country sets; expected category distribution per country is 2 temporary-protection, 2 employment, and one each healthcare, housing, education and social assistance.
- Missing owners/articles: 0. Extra or duplicate owners beyond the expected category distribution: 0.
- Translation rows: 456/456; every entry has exactly one each of `en`, `uk`, `ru`. Duplicate or missing language rows: 0.
- Empty title, summary or content fields: 0.
- Country ID and category links are structurally valid for all entries.
- Status totals: 453 Verified, 3 NeedsReview, 0 Outdated.

### Remaining non-Verified content

| Country | Article | Article ID | Topic/category | Language | Status | LastUpdatedAt |
|---|---|---|---|---|---|---|
| Netherlands (`netherlands`) | `NL-TP-01` | `244b1055-4d8c-55b2-820e-60ff018c3b1f` | Temporary Protection Application (NL-TP-01) | `en` | NeedsReview | `2026-10-21T02:12:48+00:00` |
| Netherlands (`netherlands`) | `NL-TP-01` | `244b1055-4d8c-55b2-820e-60ff018c3b1f` | Temporary Protection Application (NL-TP-01) | `uk` | NeedsReview | `2026-10-21T02:12:48+00:00` |
| Netherlands (`netherlands`) | `NL-TP-01` | `244b1055-4d8c-55b2-820e-60ff018c3b1f` | Temporary Protection Application (NL-TP-01) | `ru` | NeedsReview | `2026-10-21T02:12:48+00:00` |

These three NeedsReview rows are the EN/UK/RU translations of Netherlands `NL-TP-01` (Temporary Protection Application), ID `244b1055-4d8c-55b2-820e-60ff018c3b1f`. The existing Netherlands integration test explicitly treats this as the one non-promoted article; the other seven Netherlands articles are promoted. It is an intentional production hold, not a missing translation, but the article is not visible as verified Production content.

## Compare completeness

- Countries: 19/19; exact slug set matches `countries.json`: PASS. Missing slugs: NONE. Extra slugs: NONE.
- Topics: 8/8. Aspects: 24 unique IDs. Facts: 456/456. Every country/aspect pair occurs exactly once; duplicates: 0, missing pairs: 0, extra pairs: 0.
- Topic mappings: TemporaryProtectionApplication 19/19, ResidenceAfterProtection 19/19, FindingEmployment 19/19, WorkAuthorizationRights 19/19, Healthcare 19/19, Housing 19/19, EducationLanguage 19/19, SocialAssistance 19/19.
- Mapped entry IDs are unique: 152/152.
- Fact values have non-empty `en`, `uk`, `ru`: 456/456. Missing/empty language values: 0.
- InformationEntryId exists and matches the fact country: 456/456; missing entries: 0, cross-country: 0.
- Fact states: 451 Approved, 5 Draft; 449 available, 7 unavailable.

### Draft and unavailable facts

| Country | Aspect | Status | IsAvailable | Production behavior |
|---|---|---|---|---|
| `netherlands` | `protection-scheme` | Draft | true | Draft: fact not rendered as a factual answer in Production |
| `netherlands` | `protection-application` | Draft | true | Draft: fact not rendered as a factual answer in Production |
| `spain` | `protection-dates` | Approved | false | Unavailable: localized “not established” answer; no sources |
| `netherlands` | `protection-dates` | Draft | true | Draft: fact not rendered as a factual answer in Production |
| `netherlands` | `residence-filing` | Draft | false | Draft and unavailable: not established answer in Production; no sources |
| `denmark` | `employment-assistance-conditions` | Approved | false | Unavailable: localized “not established” answer; no sources |
| `spain` | `housing-financial-support` | Approved | false | Unavailable: localized “not established” answer; no sources |
| `germany` | `education-pupil-language` | Approved | false | Unavailable: localized “not established” answer; no sources |
| `spain` | `social-child-benefits` | Approved | false | Unavailable: localized “not established” answer; no sources |
| `sweden` | `social-child-benefits` | Draft | false | Draft and unavailable: not established answer in Production; no sources |

The five Draft records are four Netherlands aspects whose owner is the held `NL-TP-01` entry (`protection-scheme`, `protection-application`, `protection-dates`, `residence-filing`) and Sweden `social-child-benefits`. The seven unavailable records are those two with unavailable entries plus five Approved-but-unavailable facts: Denmark `employment-assistance-conditions`, Germany `education-pupil-language`, and Spain `protection-dates`, `housing-financial-support`, `social-child-benefits`. Existing catalog invariants explicitly preserve the Sweden unavailable hold and the listed unavailable answers. No fact pair is structurally missing.

## Source integrity

- Official source records: 1462; unique IDs: 1462; duplicate IDs: 0.
- Unique URLs: 1270; HTTPS: 1462; non-HTTPS: 0.
- Duplicate exact `(InformationEntryId, Title, URL)` triples: 0.
- Source records with missing owner entries: 0.
- Source records not referenced by any Core citation or Compare fact: 194 (all still have valid owner entries; this is unused registry inventory, not a broken owner reference).
- Compare source bindings: 1193; unique Compare source IDs: 1010; missing IDs / owner mismatches / non-HTTPS Compare URLs: 0 / 0 / 0.
- Cross-owner Compare source bindings: 0.

## Citation integrity

- Core citation tokens: 3690.
- Well-formed tokens: 3690/3690.
- Resolvable tokens: 3690/3690.
- Missing source IDs: 0.
- Cross-owner tokens: 0.
- Malformed tokens: 0.
- Unique source IDs referenced by Core translations: 996.
- Core citation structural integrity: PASS. No semantic claim review was performed.

## Core ↔ Compare alignment

- Topic-map entries: 152/152; distinct mapped owner IDs: 152/152.
- Core ↔ Compare owner alignments (mapped Core owner plus all three aspects per topic): 152/152.
- Missing owners, wrong country/category mappings, or fact-owner mismatches: 0.
- Topic map has exactly 8 topics with 19 country mappings each; duplicate IDs within a topic: 0.

## Remaining non-Verified content

See the three Netherlands `NL-TP-01` translation rows in the Core completeness table above. No Outdated translations remain.

## Remaining hidden Production content

- Netherlands Core article `NL-TP-01` (`244b1055-4d8c-55b2-820e-60ff018c3b1f`) has NeedsReview status in EN/UK/RU and is filtered out by the Category production renderer. The Netherlands country remains visible because its other seven articles include Verified translations.
- No country is hidden from Production country navigation; all 19 countries pass the existing visibility rule. Development shows all 19 catalog countries.
- Five Draft Compare facts are not rendered as factual answers in Production. Seven unavailable facts render the localized “not established” response rather than a sourced factual claim.

## Test coverage inventory

- Dedicated `*ContentIntegrationTests.cs` country suites: 18/19. Present for: austria, belgium, bulgaria, czechia, finland, france, germany, hungary, ireland, italy, lithuania, netherlands, norway, poland, romania, slovakia, spain, sweden.
- Missing full dedicated country suite: Denmark (`DenmarkContentIntegrationTests.cs` absent). `DenmarkAfterProtectionArticleTests.cs` covers a specific Denmark article; shared `CountryComparisonTests` and `CountryVisibilityTests` cover global mappings/visibility.
- Stale global country-count expectations: none found in the reviewed relevant tests. Remaining `18` expectation in `DenmarkAfterProtectionArticleTests.cs` counts list items in one article, not countries. `432` expectations in Compare tests intentionally count all facts excluding Netherlands or preserve the 432 pre-Ireland facts.

## Issues

- **FIX BEFORE FREEZE —** If the freeze requires every Core article to be Production-visible and every Compare record Approved and available, the current catalog does not meet that strict state: Netherlands `NL-TP-01` is held at NeedsReview; Compare has 5 Draft and 7 unavailable facts. These are intentional evidence/editorial holds, not structural integrity failures. Explicitly accept these holds in the freeze criteria or resolve them in a separately authorized content update.
- **NON-BLOCKING —** Denmark lacks a dedicated full-country integration test file; the specific article test and shared global coverage exist.
- **NON-BLOCKING —** 194 owner-valid source records are not referenced by Core citations or Compare facts; no missing-owner/orphan records were found.
- Broken countries, owners, mappings, fact pairs, source references, or citations: NONE.

## Verdict

**MINOR CLEANUP REQUIRED**

The 19-country Core and Compare matrices are structurally complete and all ownership/source/citation relationships resolve. Do not describe the current production payload as an unqualified 152/152 published Core articles or 456/456 Approved-and-available Compare facts until the Netherlands hold and the existing Compare Draft/unavailable exceptions are explicitly accepted for freeze or resolved.
