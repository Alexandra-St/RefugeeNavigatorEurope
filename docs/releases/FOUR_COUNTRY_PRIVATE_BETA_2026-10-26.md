# Four-Country Private Beta Release — 26 October 2026

## Release

- **Status:** Live
- **Deployed commit:** `623fbb39a936298d45d1d6ae5f70d7c1501cdabb`
- **Deployment evidence:** Render Dashboard showed `Deploy succeeded · Live`, source `623fbb3`; deployment was performed manually by the owner.
- **Hosting:** Render, Frankfurt
- **Service:** [https://refugeenavigatoreurope.onrender.com](https://refugeenavigatoreurope.onrender.com)
- **Access:** Private Beta over HTTPS with Basic Auth; this is not a public release.
- **Compute plan:** Free Web Service, confirmed by the owner in Render Dashboard.

## Country and content scope

The Private Beta includes Denmark, Germany, Poland, and the accepted Netherlands content set.

The accepted Netherlands scope is **21 Verified translations** and **20 Approved comparison facts**. Intentional holds remain excluded:

- NL-TP-01 translations in EN/UK/RU remain `NeedsReview` (3 translations).
- `protection-scheme`, `protection-application`, and `protection-dates` remain `Draft`.
- `residence-filing` remains `Draft`, unavailable, and source-less.

These holds are intentional and are not presented as accepted content.

## Preflight and live verification

- Four-country preflight: [Phase 18.9.2 report](FOUR_COUNTRY_PRIVATE_BETA_PREFLIGHT_2026-10-25.md) — `GO FOR OWNER DEPLOYMENT DECISION` for the accepted scope, with the Netherlands hold set excluded.
- Release-candidate GitHub Actions for deployed SHA `623fbb39a936298d45d1d6ae5f70d7c1501cdabb`: [CI](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/40246485940) and [Netherlands visual evidence](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/40246485971) — both succeeded.
- Owner-reported authenticated live smoke: **162/162 checks passed**. HTTPS, Basic Auth, localized routes, Search, Compare, and protected resources were included.
- Owner-reported Netherlands live hold verifier: **48/48 checks passed**. This covered NL-TP-01 suppression in Category, direct-fragment and Search checks, suppression of the three Draft protection facts, unavailable `residence-filing`, and the Approved `residence-routes` positive control.
- The owner confirmed the Free compute plan before deployment.
- No credentials or Authorization header values/tokens are included in this record or committed data. The verification scripts use credentials only in memory and do not print or persist them; their tests check that output contains no credentials or Authorization header.

The deployed application SHA remains `623fbb39a936298d45d1d6ae5f70d7c1501cdabb`. Commits added to `main` after that deployment changed only deployment-verification scripts and their synthetic tests; no production application, content, or status files changed.

## Limitations

- Access is limited to invited Private Beta users and remains protected by shared Basic Auth; credentials must be shared separately through a trusted channel.
- Render Free services can spin down when idle, have cold-start delays and ephemeral filesystems, and are not intended for public production workloads.
- Render terminates public HTTPS at its edge and forwards requests to the container over HTTP; credentials are not protected by end-to-end TLS inside Render-managed infrastructure. This deployment relies on the confirmed Free-plan ingress boundary and a unique shared password.
- The Netherlands holds listed above remain outside the accepted release scope.
- A passing smoke check confirms the tested routes and gates at the time of verification; it does not replace ongoing legal-source monitoring or a full accessibility audit.

## Verdict

**FOUR-COUNTRY PRIVATE BETA LIVE**
