# Nine-Country Private Beta — 2026-11-07

- **Status:** LIVE
- **Final verdict:** NINE-COUNTRY PRIVATE BETA LIVE
- **Application/content deployed baseline:** `bdcf300d30877efc7ab1c4c7c2e6f409d5d60606`
- **Verifier/testing SHA:** `27570c67bc3997b73676db0f067233d016c2fa9a` (contains only the nine-country live verifier and its tests; it was not deployed and must not be deployed).
- **Deployment:** owner manually deployed the application/content baseline through the existing Render service.
- **Release-record change:** this release record is documentation-only and is not deployed.
- **Access:** restricted Private Beta over HTTPS, protected by Basic Auth; credentials are distributed separately.

## Production scope

Production exposes exactly these nine countries:

1. Denmark
2. Germany
3. Poland
4. Czechia
5. Spain
6. Netherlands
7. Sweden
8. Romania
9. Slovakia

Slovakia became Production-visible through the existing generic `CountryVisibility` rule following owner-approved promotion. No Slovakia-specific visibility rule was added.

## Slovakia accepted content

- 8 accepted articles.
- 24/24 EN/UK/RU translations are `Verified`.
- 24/24 comparison facts are `Approved`.
- 81 article-owned OfficialSources.
- Search returned 8 accepted results in EN, UK, and RU.
- All six category routes are available.
- Compare rendered all 24 available aspects with HTTPS source disclosures in EN, UK, and RU.
- The live verifier observed 70 HTTPS source links for the complete Slovakia comparison set in each language.

## Preserved holds

### Netherlands

The accepted subset remains 21 `Verified` / 3 `NeedsReview` translations and 20 `Approved` / 4 `Draft` facts. Held items remain outside the accepted scope:

- Article: `NL-TP-01`
- Draft facts: `protection-scheme`, `protection-application`, `protection-dates`, and `residence-filing`

`residence-filing` remains unavailable and source-less.

### Sweden

`social-child-benefits` remains `Draft`, unavailable, and source-less. Live Compare shows its unresolved placeholder without source disclosure.

## Authenticated live verification

The owner manually deployed the accepted application/content state and ran authenticated, read-only checks against the existing live service.

| Verification | Result |
| --- | ---: |
| Deployment/security smoke | 162/162 PASS |
| Nine-country live scope | 166/166 PASS |
| Netherlands live holds | 48/48 PASS |
| **Total live evidence** | **376/376 PASS** |

The deployment/security smoke checked Basic Auth protection, anonymous denial, content-leak protection, `noindex,nofollow`, CSS, language switching, Search/Compare, and representative routes.

The nine-country verifier checked the exact nine-country Home/Search/Compare set, all country and category routes, Slovakia in EN/UK/RU, Slovakia Search at 8/8 in each language, Slovakia Compare at 24/24 in each language, and the Sweden hold regression. The separate Netherlands verifier confirmed held items remain suppressed.

## CI and tests

For the application/content deployed baseline SHA `bdcf300d30877efc7ab1c4c7c2e6f409d5d60606`:

- CI: success ([run 42294357018](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42294357018)).
- Netherlands visual evidence: success ([run 42294356979](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42294356979)).
- Targeted tests: 127/127.
- Full Release suite: 617/617.
- Release build: success, with NU1900 vulnerability-cache warnings because the vulnerability cache was unavailable.

For verifier/testing SHA `27570c67bc3997b73676db0f067233d016c2fa9a`:

- CI: success ([run 42302013640](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42302013640)).
- Netherlands visual evidence: success ([run 42302013642](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42302013642)).
- Nine-country verifier unit tests: 8/8.
- Deployment-smoke unit tests: 6/6.
- Netherlands-hold unit tests: 4/4.

## Limitations

- This is a restricted Private Beta, not a public release.
- Netherlands held items remain outside the accepted scope.
- Sweden `social-child-benefits` remains an evidence gap.
- Live checks record the state at verification time; they do not replace future content refresh or source monitoring.

No credentials, usernames, passwords, or Authorization headers are included. This release-record commit is docs-only; no deployment was performed for it.
