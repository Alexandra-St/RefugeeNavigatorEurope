# Six-Country Private Beta Release — 27 October 2026

> **Historical / superseded release record.** This six-country release was replaced by the Seven-Country Private Beta after Sweden was accepted and promoted. See the [Seven-Country Private Beta release record](SEVEN_COUNTRY_PRIVATE_BETA_2026-10-30.md) for the current live scope. The historical details below are retained as recorded at the time.

## Release

- **Status:** Live
- **Deployed commit:** `f474426e49139e681bd60cccf718fc8efd5556f5`
- **Deployment evidence:** Render Dashboard showed `Deploy succeeded · Live`, source `f474426`; deployment was performed manually by the owner.
- **Hosting:** Render, Frankfurt
- **Access:** Private Beta over HTTPS with Basic Auth; this is not a public release.
- **Deployment by Codex:** none. No deployment was started or repeated during release verification or documentation.

## Production country scope

Production exposes six countries:

- Denmark
- Germany
- Poland
- Czechia
- Spain
- Netherlands

Sweden remains available in Development/editorial preview. It is hidden from Production Home, country and category routes, Search filters, and Compare selectors; manual Sweden Compare selections are invalid.

Czechia and Spain are fully accepted, with 24 Verified translations and 24 Approved comparison facts each. The accepted Netherlands scope is **21 Verified translations** and **20 Approved comparison facts**. Intentional holds remain excluded:

- NL-TP-01 translations in EN/UK/RU remain `NeedsReview` (3 translations).
- `protection-scheme`, `protection-application`, and `protection-dates` remain `Draft`.
- `residence-filing` remains `Draft`, unavailable, and source-less.

The complete country counts are:

| Country | Verified translations | Approved comparison facts |
| --- | ---: | ---: |
| Denmark | 24 | 24 |
| Germany | 24 | 24 |
| Poland | 24 | 24 |
| Czechia | 24 | 24 |
| Spain | 24 | 24 |
| Netherlands | 21; 3 held | 20; 4 held |

## Preflight and live verification

- Six-country preflight: [SIX_COUNTRY_PRIVATE_BETA_PREFLIGHT_2026-10-26.md](SIX_COUNTRY_PRIVATE_BETA_PREFLIGHT_2026-10-26.md).
- Owner-reported authenticated live smoke: **162/162 checks passed**.
- Owner-reported six-country live scope verification: **77/77 checks passed**. This verified the six-country Home, route, Search, and Compare scope, including Sweden suppression and Czechia, Spain, and Netherlands availability.
- Owner-reported Netherlands live hold verification: **48/48 checks passed**. This covered NL-TP-01 suppression, the three Draft protection facts, unavailable `residence-filing`, and the Approved `residence-routes` positive control.
- Post-deployment verifier commit: `3c9d1a5ccffb59a5257f45fa56b0095b43e3eb9`; it contains only `.github/scripts/six_country_live_scope_test.py` and `.github/scripts/test_six_country_live_scope_test.py`.
- The deployed SHA is an ancestor of the verifier commit. Changes after deployment were limited to these verification scripts and synthetic tests; production application, content, and statuses were unchanged.
- No credentials, passwords, or Authorization values are included in this record. The owner ran authenticated checks locally using environment variables; credentials were not provided to or logged by Codex.

## Limitations

- Access is limited to invited Private Beta users and remains protected by shared Basic Auth; credentials must be shared separately through a trusted channel.
- The Netherlands holds listed above remain outside the accepted release scope.
- A passing smoke check confirms the tested routes and gates at the time of verification; it does not replace ongoing legal-source monitoring or a full accessibility audit.

## Verdict

**SIX-COUNTRY PRIVATE BETA LIVE**
