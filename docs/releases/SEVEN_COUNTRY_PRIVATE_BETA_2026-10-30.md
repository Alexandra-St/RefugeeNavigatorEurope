# Seven-Country Private Beta Release — 30 October 2026

## Release

- **Status:** LIVE
- **Deployed commit:** `02774ae377e7bd3cde796adbe5b7b6661838f7b6`
- **Deployment evidence:** the owner confirmed the live release and supplied its deployed SHA from Render.
- **Deployment method:** manual deployment by the owner through Render Dashboard.
- **Hosting:** Render, Frankfurt.
- **Access:** Private Beta over HTTPS with Basic Auth; this is not a public release.
- **Deployment by Codex:** none. Codex did not start or repeat a deployment.

## Production country scope

Production exposes seven countries:

- Denmark
- Germany
- Poland
- Czechia
- Spain
- Netherlands
- Sweden

Sweden became Production-visible through the generic country-visibility rule after its accepted translations were promoted. No country-specific visibility rule was added.

| Country | Verified translations | Approved comparison facts |
| --- | ---: | ---: |
| Denmark | 24 | 24 |
| Germany | 24 | 24 |
| Poland | 24 | 24 |
| Czechia | 24 | 24 |
| Spain | 24 | 24 |
| Netherlands | 21; 3 held | 20; 4 held |
| Sweden | 24 | 23; 1 held |

### Sweden hold

`social-child-benefits` remains `Draft`, unavailable, and without source IDs. Live Compare presents it as unresolved evidence, without an approved answer or source group. This is an evidence gap, not a claim that the benefit does not exist.

### Netherlands holds

The accepted subset remains 21 Verified translations and 20 Approved facts. NL-TP-01 and the following facts remain held:

- `protection-scheme`
- `protection-application`
- `protection-dates`
- `residence-filing` (unavailable and source-less)

## Preflight and live verification

- Seven-country preflight: [SEVEN_COUNTRY_PRIVATE_BETA_PREFLIGHT_2026-10-28.md](SEVEN_COUNTRY_PRIVATE_BETA_PREFLIGHT_2026-10-28.md).
- Owner-reported authenticated deployment smoke: **162/162 checks passed**.
- Owner-reported seven-country live scope verification: **109/109 checks passed**.
- Sweden live checks confirmed its country route and all category routes, eight Verified articles, accepted Search results in EN/UK/RU, Compare availability, and rendering of Approved facts.
- Owner-reported Netherlands live hold verification: **48/48 checks passed**.
- **Total live checks:** **319/319 passed**.
- Post-deployment verifier-only commit: `b99691c191d7c9b4f283b76eb5c1eefd04992441`. It renamed and updated the seven-country live verifier and its synthetic tests; it was not deployed and contains no production application, catalog, or status changes.
- The owner reported no credential leakage. This record contains no credentials, passwords, usernames, or Authorization values.

## Limitations

- Access is limited to invited Private Beta users and remains protected by shared Basic Auth; credentials are distributed separately through a trusted channel.
- The held Sweden and Netherlands items above remain outside the accepted release scope.
- These checks confirm the tested routes and gates at verification time. They do not replace ongoing source monitoring or a full accessibility audit.

## Verdict

**SEVEN-COUNTRY PRIVATE BETA LIVE**
