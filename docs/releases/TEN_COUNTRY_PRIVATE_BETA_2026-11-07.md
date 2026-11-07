# Ten-Country Private Beta — 2026-11-07

- **Status:** LIVE
- **Final verdict:** TEN-COUNTRY PRIVATE BETA LIVE
- **Application/content accepted baseline:** `1b3c273452a56dd78034bb34bb0611cf2210413e`
- **Verifier/testing/deployed main SHA:** `ff1ff94ca763855e6b1478181ccfa886f5425dc6` (adds only the ten-country live verifier and its tests on top of the application/content baseline).
- **Deployment:** owner manually deployed current `main` through the existing Render service.
- **Release-record change:** documentation-only; it is not deployed. No further deployment was performed.
- **Access:** restricted Private Beta over HTTPS and protected by Basic Auth; credentials are distributed separately.

## Production scope

Production exposes exactly these ten countries:

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

Bulgaria became Production-visible through the existing generic `CountryVisibility` rule following owner-approved promotion. No Bulgaria-specific visibility logic was added.

## Bulgaria accepted content

- 8 accepted articles.
- 24/24 EN/UK/RU translations are `Verified`.
- 24/24 comparison facts are `Approved`.
- 65 article-owned OfficialSources.
- All six category routes are available.
- Search returned 8/8 accepted results in EN, UK, and RU.
- Compare rendered 24/24 available aspects in EN, UK, and RU.
- The live verifier observed 65 HTTPS source links for the complete Bulgaria comparison set in each language.
- Bulgaria country routes passed in EN, UK, and RU.

Category distribution: temporary-protection (2), employment (2), healthcare (1), housing (1), education (1), and social-assistance (1).

## Preserved holds

### Netherlands

The accepted subset remains 21 `Verified` / 3 `NeedsReview` translations and 20 `Approved` / 4 `Draft` facts. Held items remain outside the accepted scope:

- Article: `NL-TP-01`
- Draft facts: `protection-scheme`, `protection-application`, `protection-dates`, and `residence-filing`

`residence-filing` remains unavailable and source-less. The live Netherlands hold verifier passed 48/48 checks.

### Sweden

`social-child-benefits` remains `Draft`, unavailable, and source-less. Live Compare shows its unresolved placeholder without source disclosure in EN, UK, and RU; the ten-country live verifier confirmed this hold.

## Authenticated live verification

The owner manually deployed the accepted application/content state and completed authenticated, read-only checks against the existing live service.

| Verification | Result |
| --- | ---: |
| Deployment/security smoke | 162/162 PASS |
| Ten-country live scope | 206/206 PASS |
| Netherlands live holds | 48/48 PASS |
| **Total live evidence** | **416/416 PASS** |

The deployment/security smoke checked HTTPS, Basic Auth, anonymous denial, protected-content leak protection, `noindex,nofollow`, CSS/assets, language switching, Search/Compare, and representative country/category/article routes.

The ten-country verifier confirmed the exact ten-country Home/Search/Compare set, Bulgaria routes and accepted content in EN/UK/RU, all six Bulgaria category routes, Search at 8/8 and Compare at 24/24 in each language with HTTPS source disclosures, plus Romania and Slovakia regression checks and the Sweden hold. The Netherlands verifier confirmed the held content remains suppressed.

## CI and tests

For the application/content promotion baseline SHA `1b3c273452a56dd78034bb34bb0611cf2210413e`:

- CI: success ([run 42411699564](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42411699564)).
- Netherlands visual evidence: success ([run 42411699501](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42411699501)).
- Bulgaria targeted tests: 3/3 PASS.
- Full suite: 620/620 PASS.
- CI Release build: success; no local Release build was run.
- NU1900 warnings were caused by the unavailable NuGet vulnerability cache.

For verifier/deployed main SHA `ff1ff94ca763855e6b1478181ccfa886f5425dc6`:

- CI: success ([run 42412479581](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42412479581)).
- Netherlands visual evidence: success ([run 42412479570](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42412479570)).
- Verifier unit tests: 35/35 PASS.
- `git diff --check`: PASS.

## Limitations

- This is a restricted Private Beta, not a public release.
- Netherlands held items remain outside the accepted scope.
- Sweden `social-child-benefits` remains an evidence gap.
- Live checks record the state at verification time; they do not replace future content refresh or source monitoring.

No credentials, usernames, passwords, or Authorization headers are included. This release-record commit is documentation-only; no further deployment was performed.
