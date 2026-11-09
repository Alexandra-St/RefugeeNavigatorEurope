# Twelve-Country Private Beta — 2026-11-09

- **Status:** LIVE
- **Final verdict:** TWELVE-COUNTRY PRIVATE BETA LIVE
- **Application/content accepted baseline:** `44d430dcca8c7907513f65ffcd69c2228943c385`
- **Live verifier/deployed application SHA:** `8c0d2b1b7330e56e1aae9a03f1dc2cc35cef44d1`
- **Deployment:** The specified deployed SHA was checked against the existing Render service with authenticated, read-only live verification; all three live verifiers passed.
- **Release-record change:** documentation-only. The release-record commit is not a deployed SHA; no further deployment will be performed.
- **Access:** restricted Private Beta over HTTPS and protected by Basic Auth; credentials are distributed separately.

## Production scope

Production exposes exactly these twelve countries:

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

Italy became Production-visible through the existing generic `CountryVisibility` rule following owner-approved promotion. No Italy-specific visibility logic was added.

## Italy accepted content

- 8 accepted articles.
- 24/24 EN/UK/RU translations are `Verified`.
- 24/24 comparison facts are `Approved` and available.
- 70 OfficialSources.
- Category distribution: temporary-protection (2), employment (2), healthcare (1), housing (1), education (1), and social-assistance (1).
- Italy country and category routes passed in EN, UK, and RU.
- Search returned 8/8 accepted results in EN, UK, and RU.
- Compare rendered 24/24 available aspects in EN, UK, and RU with source disclosures and HTTPS links.

## Global catalog state

- Countries: 12
- Articles: 96
- Translations: 288 — 285 `Verified`, 3 `NeedsReview`
- OfficialSources: 793
- Comparison facts: 288 — 283 `Approved`, 5 `Draft`
- Available comparison facts: 281

## Preserved holds

### Netherlands

The accepted subset remains 21 `Verified` / 3 `NeedsReview` translations and 20 `Approved` / 4 `Draft` facts. Held items remain outside the accepted scope:

- Article: `NL-TP-01`
- Draft facts: `protection-scheme`, `protection-application`, `protection-dates`, and `residence-filing`

`residence-filing` remains unavailable and source-less. The live Netherlands hold verifier passed 48/48 checks.

### Sweden

`social-child-benefits` remains `Draft`, unavailable, and source-less. Live Compare shows its unresolved placeholder without source disclosure in EN, UK, and RU; the twelve-country live verifier confirmed this hold.

## Authenticated live verification

The deployed application was checked with authenticated, read-only verification against the existing live service.

| Verification | Result |
| --- | ---: |
| Deployment/security smoke | 162/162 PASS |
| Twelve-country live scope | 287/287 PASS |
| Netherlands live holds | 48/48 PASS |
| **Total live evidence** | **497/497 PASS** |

The deployment/security smoke checked HTTPS, Basic Auth, anonymous denial, protected-content leak protection, `noindex,nofollow`, CSS/assets, language switching, Search/Compare, and representative routes.

The twelve-country verifier confirmed the exact twelve-country Home/Search/Compare set and country/category routes across the scope in EN. Italy country and category routes, Search at 8/8, and Compare at 24/24 passed in EN/UK/RU with HTTPS source disclosures. Existing Austria, Bulgaria, Slovakia, Romania, and Netherlands behavior passed; Netherlands Search remained 7/7. Sweden `social-child-benefits` remained an unavailable placeholder without sources. The Netherlands verifier confirmed held content remains suppressed.

## CI and tests

For the Italy application/content promotion baseline SHA `44d430dcca8c7907513f65ffcd69c2228943c385`:

- CI: success ([run 42611022428](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42611022428)).
- Netherlands visual evidence: success ([run 42611022587](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42611022587)).
- Full `dotnet test`: 627/627 PASS.

For the original twelve-country verifier SHA `709bfafa9b5edfa8684042a6aa8faa4d3b5b633e`:

- CI: success ([run 42612124504](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42612124504)).
- Netherlands visual evidence: success ([run 42612124553](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42612124553)).
- Twelve-country verifier unit tests: 21/21 PASS.

For the verifier false-positive fix and deployed application SHA `8c0d2b1b7330e56e1aae9a03f1dc2cc35cef44d1`:

- CI: success ([run 42616540862](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42616540862)).
- Netherlands visual evidence: success ([run 42616540920](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/42616540920)).
- Twelve-country verifier unit tests: 24/24 PASS.

## Limitations

- This is a restricted Private Beta, not a public release.
- Netherlands held items remain outside the accepted scope.
- Sweden `social-child-benefits` remains an evidence gap.
- Live checks record the state at verification time; they do not replace future content refresh or source monitoring.

No credentials, usernames, passwords, or Authorization headers are included. The release-record commit is documentation-only and is not deployed; no further deployment will be performed.
