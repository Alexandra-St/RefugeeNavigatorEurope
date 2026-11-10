# Thirteen-Country Private Beta — Deployment Candidate

Status: **READY FOR MANUAL DEPLOYMENT**

This is a preflight result, not deployment authorization. Deploy only the exact candidate SHA recorded below after the separately required manual deployment decision.

Candidate SHA: `b74bad1a470c2fef7b90c243a90eee7a3ee43176`

Previous live SHA: `8c0d2b1b7330e56e1aae9a03f1dc2cc35cef44d1`

Preflight date: 2026-11-10

## Candidate scope

The candidate exposes exactly these 13 countries, with no 14th country:

1. Denmark
2. Germany
3. Poland
4. Czechia
5. Spain
6. Netherlands
7. Sweden
8. Romania
9. Slovakia
10. Bulgaria
11. Austria
12. Italy
13. Lithuania

The existing generic visibility behavior exposes all 13 countries in Production and Development. The CountryVisibility tests verify the exact accepted production set; the candidate's thirteen-country verifier unit fixture verifies the exact Home cards, Search filter, and both Compare selectors.

## Lithuania accepted content

- Country ID: `10000000-0000-0000-0000-000000000013`
- 8 articles
- 24 Verified translations; 0 NeedsReview
- 71 owned sources
- 24 Approved comparison facts; all 24 available
- Production-visible and Development-visible
- Article IDs, EN/UK/RU titles, summaries, content, and timestamps match the owner-accepted baseline.
- Source UUID, title, URL, and entry ownership match for 71/71 sources.
- All 24 comparison Values and OfficialSourceIds match the accepted baseline; all 24 canonical aspects occur exactly once.

## Global catalog state

| Measure | Candidate state |
| --- | ---: |
| Countries | 13 |
| Articles | 104 |
| Translations | 312 |
| Verified | 309 |
| NeedsReview | 3 |
| Outdated | 0 |
| OfficialSources | 864 |
| Comparison facts | 312 |
| Approved | 307 |
| Draft | 5 |
| Available | 305 |
| Unavailable | 7 |

## Preserved holds

**Netherlands:** 21 Verified translations and 3 NeedsReview; 20 Approved facts and 4 Draft facts. The held aspects remain `protection-scheme`, `protection-application`, `protection-dates`, and `residence-filing`. `residence-filing` remains Draft, unavailable, and source-less.

**Sweden:** `social-child-benefits` remains Draft, unavailable, and source-less.

Both holds passed the candidate data audit and the verifier fixture coverage.

## Candidate diff classification

Compared with previous live SHA `8c0d2b1b7330e56e1aae9a03f1dc2cc35cef44d1`, all 8 changed runtime paths are the expected catalog/topic-map/localization content needed for Lithuania:

- `src/RefugeeNavigator.Web/Comparison/CountryComparisonTopicMap.cs`
- `src/RefugeeNavigator.Web/Data/countries.json`
- `src/RefugeeNavigator.Web/Data/information-entries.json`
- `src/RefugeeNavigator.Web/Data/official-sources.json`
- `src/RefugeeNavigator.Web/Data/comparison-facts.json`
- `src/RefugeeNavigator.Web/Resources/Localization.SharedResource.en.resx`
- `src/RefugeeNavigator.Web/Resources/Localization.SharedResource.uk.resx`
- `src/RefugeeNavigator.Web/Resources/Localization.SharedResource.ru.resx`

Unexpected runtime or application-code changes: **0**. No changes were found in authentication, middleware, controllers, Razor/UI/components, Program/startup, Render configuration, environment-variable handling, domain models, or HTTP/security code. The remaining candidate changes are tests/verifiers and documentation/research/release records.

## Verification evidence

- `dotnet test --no-restore`: **634/634 PASS**.
- `python3 .github/scripts/test_thirteen_country_live_scope_test.py`: **31/31 PASS**. Coverage includes Lithuania EN/UK/RU country and category routes, exact category distribution, 8/8 search, 24/24 comparison answers, source disclosure, HTTPS-only links, no draft labels/previews, and seven injected failure cases.
- `python3 .github/scripts/test_twelve_country_live_scope_test.py`: **24/24 PASS**. Historical verifier unchanged.
- `git diff --check`: **PASS**.
- CI: **success**, run [42841777571](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42841777571).
- Netherlands visual evidence: **success**, run [42841777590](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42841777590).

The verifier was validated only with unit-test fixtures. It was not run against the current service.

## Deployment state

**NO DEPLOYMENT WAS PERFORMED DURING THIS PREFLIGHT.**

**THE CURRENT LIVE SERVICE WAS NOT USED TO VALIDATE THIRTEEN-COUNTRY SCOPE.**

Live verification must occur only after the exact candidate SHA is deployed. The deployment candidate remains `b74bad1a470c2fef7b90c243a90eee7a3ee43176`; this later documentation-only commit is not the deployment candidate.
