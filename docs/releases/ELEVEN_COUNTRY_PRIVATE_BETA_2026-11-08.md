# Eleven-Country Private Beta — 2026-11-08

- **Status:** LIVE
- **Final verdict:** ELEVEN-COUNTRY PRIVATE BETA LIVE
- **Application/content accepted baseline:** `9f52e3dbd99d62b76cc2beab2a91f749bc27b8f5`
- **Live verifier/deployed main SHA:** `60c47a8a3d78023d550b9db131ef5a84482ab944`
- **Deployment:** The specified main SHA is deployed to the existing Render service; authenticated, read-only verification passed against the live service.
- **Release-record change:** documentation-only. It is not deployed; no further deployment will be performed.
- **Access:** restricted Private Beta over HTTPS and protected by Basic Auth; credentials are distributed separately.

## Production scope

Production exposes exactly these eleven countries:

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

Austria became Production-visible through the existing generic `CountryVisibility` rule following owner-approved promotion. No Austria-specific visibility logic was added.

## Austria accepted content

- 8 accepted articles.
- 24/24 EN/UK/RU translations are `Verified`.
- 24/24 comparison facts are `Approved`.
- 57 article-owned OfficialSources.
- Category distribution: temporary-protection (2), employment (2), healthcare (1), housing (1), education (1), and social-assistance (1).
- Austria country routes and all six category routes passed in EN, UK, and RU.
- Search returned 8/8 accepted results in EN, UK, and RU.
- Compare rendered 24/24 available aspects in EN, UK, and RU.
- The live verifier observed 57 HTTPS source links for the complete Austria comparison set in each language.

## Global catalog state

- Countries: 11
- Articles: 88
- Translations: 264 — 261 `Verified`, 3 `NeedsReview`
- OfficialSources: 723
- Comparison facts: 264 — 259 `Approved`, 5 `Draft`
- Available comparison facts: 257

## Preserved holds

### Netherlands

The accepted subset remains 21 `Verified` / 3 `NeedsReview` translations and 20 `Approved` / 4 `Draft` facts. Held items remain outside the accepted scope:

- Article: `NL-TP-01`
- Draft facts: `protection-scheme`, `protection-application`, `protection-dates`, and `residence-filing`

`residence-filing` remains unavailable and source-less. The live Netherlands hold verifier passed 48/48 checks.

### Sweden

`social-child-benefits` remains `Draft`, unavailable, and source-less. Live Compare shows its unresolved placeholder without source disclosure in EN, UK, and RU; the eleven-country live verifier confirmed this hold.

## Authenticated live verification

The owner-deployed application/content state was checked with authenticated, read-only verification against the existing live service.

| Verification | Result |
| --- | ---: |
| Deployment/security smoke | 162/162 PASS |
| Eleven-country live scope | 245/245 PASS |
| Netherlands live holds | 48/48 PASS |
| **Total live evidence** | **455/455 PASS** |

The deployment/security smoke checked HTTPS, Basic Auth, anonymous denial, protected-content leak protection, `noindex,nofollow`, CSS/assets, language switching, Search/Compare, and representative routes.

The eleven-country verifier confirmed the exact eleven-country Home/Search/Compare set and country/category routes across that scope in EN. Austria country and category routes, Search at 8/8, and Compare at 24/24 passed in EN/UK/RU with HTTPS source disclosures. Bulgaria, Slovakia, and Romania regression checks passed; Netherlands Search remained 7/7. Sweden `social-child-benefits` remained an unavailable placeholder without sources. The Netherlands verifier confirmed held content remains suppressed.

Observed source links in the Austria Compare set were 57 HTTPS links per language; the verifier checks availability and HTTPS disclosures rather than requiring the full article-source catalog to appear in Compare.

## CI and tests

For the accepted application/content promotion baseline SHA `9f52e3dbd99d62b76cc2beab2a91f749bc27b8f5`:

- CI: success ([run 42493817352](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42493817352)).
- Netherlands visual evidence: success ([run 42493817360](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42493817360)).
- Full `dotnet test`: 623/623 PASS.
- NU1900 warnings were caused by the unavailable local NuGet vulnerability-data cache and were not test failures.

For verifier/deployed main SHA `60c47a8a3d78023d550b9db131ef5a84482ab944`:

- CI: success ([run 42494719129](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42494719129)).
- Netherlands visual evidence: success ([run 42494719149](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42494719149)).
- Eleven-country verifier unit tests: 14/14 PASS.

## Limitations

- This is a restricted Private Beta, not a public release.
- Netherlands held items remain outside the accepted scope.
- Sweden `social-child-benefits` remains an evidence gap.
- Live checks record the state at verification time; they do not replace future content refresh or source monitoring.

No credentials, usernames, passwords, or Authorization headers are included. This release-record change is documentation-only; no further deployment will be performed.
