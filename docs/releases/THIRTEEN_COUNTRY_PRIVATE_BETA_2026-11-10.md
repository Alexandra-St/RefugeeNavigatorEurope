# Thirteen-Country Private Beta — 2026-11-10

- Status: **LIVE**
- Final verdict: **THIRTEEN-COUNTRY PRIVATE BETA LIVE**
- Application/content accepted candidate: `b74bad1a470c2fef7b90c243a90eee7a3ee43176`
- Previous live SHA: `8c0d2b1b7330e56e1aae9a03f1dc2cc35cef44d1`
- Deployment: manually performed by owner before this verification
- Release-record change: documentation-only
- Access: restricted Private Beta over HTTPS / Basic Auth

The exact deployed SHA was not independently exposed by recognized live HTTP metadata. The owner manually deployed candidate `b74bad1a470c2fef7b90c243a90eee7a3ee43176`; authenticated live verification was then performed against that configured service. No SHA is inferred from page content.

## Production scope

The live Home cards, Search country filter, and both Compare selectors contain exactly these 13 countries, with no missing or extra country:

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

## Lithuania accepted content

- 8 accepted articles
- 24/24 translations Verified
- 24/24 comparison facts Approved and available
- 71 OfficialSources
- Production-visible
- EN/UK/RU country and category routes PASS
- Search: 8/8 results in EN/UK/RU
- Compare: 24/24 canonical aspects in EN/UK/RU
- Available answers and source disclosures present for all facts; source links HTTPS-only
- No Lithuania draft labels or draft-preview notices

## Global catalog state

| Measure | Live candidate state |
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

**Netherlands:** 21 Verified translations and 3 NeedsReview; 20 Approved facts and 4 Draft facts. The Draft aspects remain `protection-scheme`, `protection-application`, `protection-dates`, and `residence-filing`. Search returns 7 accepted articles. The `residence-filing` fact remains Draft, unavailable, and source-less. Held content suppression passed.

**Sweden:** `social-child-benefits` remains Draft, unavailable, and source-less; live Compare shows the unavailable placeholder without source disclosure. This remains an evidence gap outside the accepted scope.

## Authenticated live verification

| Verification | Result |
| --- | ---: |
| Deployment/security smoke | 162/162 PASS |
| Thirteen-country live scope | 338/338 PASS |
| Netherlands live holds | 48/48 PASS |
| **TOTAL** | **548/548 PASS** |

The thirteen-country live scope verifier also confirmed Lithuania's 8/8 category article distribution and Search results, 24/24 available comparison answers, HTTPS source disclosures, no draft preview, and the Sweden hold in EN/UK/RU.

## CI and tests

For candidate SHA `b74bad1a470c2fef7b90c243a90eee7a3ee43176`:

- CI: [42841777571 — success](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42841777571)
- Netherlands visual evidence: [42841777590 — success](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42841777590)

Candidate preflight documentation commit `b8439a0578804e2fe6015b627a2040248f84bb90`:

- CI: [42844419781 — success](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42844419781)
- Netherlands visual evidence: [42844419839 — success](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42844419839)

Local verification:

- `dotnet test`: 634/634 PASS
- Thirteen-country verifier unit tests: 31/31 PASS
- Historical twelve-country verifier tests: 24/24 PASS

## Limitations

- The deployment remains a restricted Private Beta.
- The Netherlands held subset remains outside accepted scope.
- Sweden `social-child-benefits` remains an evidence gap.
- Live verification represents the service state at verification time.
- Future content refresh and source monitoring remain required.

## Deployment state

Deployment had already been manually performed by the owner before these authenticated read-only checks. No deployment was performed as part of release closure. The documentation-only release-record commit must not be deployed. No further deployment is authorized or required by this record.
