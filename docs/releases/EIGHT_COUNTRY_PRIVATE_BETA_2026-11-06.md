# Eight-Country Private Beta — 2026-11-06

- **Status:** LIVE
- **Final verdict:** EIGHT-COUNTRY PRIVATE BETA LIVE
- **Deployed application SHA:** `2f0a95bd70817b33a2c8d48c4631bb9a0c39e261`
- **Deployment:** owner manually deployed through the existing Render service.
- **Access:** Private Beta over HTTPS, protected by Basic Auth; credentials are distributed separately.
- **Verifier/testing/documentation SHA:** `fc02bc8e2e7d626bed4195182971626b401d7140` (not required to be deployed; no application or catalog changes).

## Production scope

Production exposes exactly eight countries:

1. Denmark
2. Germany
3. Poland
4. Czechia
5. Spain
6. Netherlands
7. Sweden
8. Romania

Romania became Production-visible through the existing generic `CountryVisibility` rule after its owner-approved promotion. No Romania-specific visibility rule was added.

## Romania accepted content

- 8 articles.
- 24/24 EN/UK/RU translations are `Verified`.
- 24/24 comparison facts are `Approved`.
- 56 article-owned OfficialSources.
- Search returned 8 accepted results in EN, UK, and RU.
- All six Romania category routes were available.
- Compare rendered all 24 Romania aspects with available answers and source disclosures in EN, UK, and RU.

## Preserved holds

### Netherlands

The accepted subset remains 21 `Verified` translations and 20 `Approved` facts. Three translations and four facts remain held outside the accepted subset:

- Article: `NL-TP-01`
- Facts: `protection-scheme`, `protection-application`, `protection-dates`, and `residence-filing`

`residence-filing` remains unavailable and source-less.

### Sweden

`social-child-benefits` remains `Draft`, unavailable, and without source IDs. Live Compare showed the unresolved evidence-gap state in EN/UK/RU rather than an approved answer.

## Authenticated live verification

The owner ran authenticated, read-only checks against the existing live deployment:

| Verification | Result |
| --- | ---: |
| Deployment/security smoke | 162/162 PASS |
| Eight-country live scope | 124/124 PASS |
| Netherlands live holds | 48/48 PASS |
| **Total** | **334/334 PASS** |

The deployment/security smoke included Basic Auth protection, anonymous denial, content-leak checks, noindex/nofollow, CSS, language switching, and representative country, category, search, compare, and 404 behavior.

The eight-country scope checks covered the exact Home/Search/Compare country sets, all eight country routes, accepted category counts, Romania routes and Search in EN/UK/RU, all 24 Romania Compare aspects, and the Sweden hold regression. The Netherlands verifier confirmed the held article/facts remain suppressed and accepted content remains available.

## CI and visual evidence

For the deployed application SHA `2f0a95bd70817b33a2c8d48c4631bb9a0c39e261`:

- CI: success.
- Netherlands visual evidence: success.
- Targeted tests: 165/165.
- Full Release suite: 612/612.
- Release build: success.

For verifier/testing/documentation SHA `fc02bc8e2e7d626bed4195182971626b401d7140`:

- CI: success.
- Netherlands visual evidence: success.
- Eight-country verifier tests: 6/6.
- Deployment-smoke tests: 6/6.
- Netherlands-hold synthetic tests: 4/4.
- Targeted .NET tests: 165/165.
- Full Release suite: 612/612.
- Release build: success.

## Limitations

- This is a restricted Private Beta, not a public release.
- Netherlands held items remain outside the accepted scope.
- Sweden `social-child-benefits` remains an evidence gap.
- Live checks cover the tested routes and gates at verification time; they do not replace ongoing source monitoring, accessibility work, or future content refresh.

No credentials or Authorization values are included in this record. The owner performed the deployment; Codex did not deploy or repeat it.
