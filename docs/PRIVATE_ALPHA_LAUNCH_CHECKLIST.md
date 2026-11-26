# Private Alpha launch checklist

Use this checklist only after a human approves creating the Render service. The verification script does not create or deploy a service.

## Render configuration

- [ ] **MANUAL — NOT YET VERIFIED:** Connect `Alexandra-St/RefugeeNavigatorEurope`, branch `main`.
- [ ] **MANUAL — NOT YET VERIFIED:** Select Docker, root `Dockerfile`, and the **Free** compute plan; independently confirm the actual plan in the dashboard.
- [ ] **MANUAL — NOT YET VERIFIED:** Keep the default TCP health check and confirm port `10000`.
- [ ] **MANUAL — NOT YET VERIFIED:** Set `PRIVATE_ALPHA_ACCESS_MODE=private` and `PRIVATE_ALPHA_TLS_MODE=render-free-edge`.
- [ ] **MANUAL — NOT YET VERIFIED:** Create a unique username and strong password (16+ characters, at least three character types) in Render's secret settings. Never put them in source control, a URL, screenshots, or logs.
- [ ] **MANUAL — NOT YET VERIFIED:** Leave `PRIVATE_ALPHA_TRUSTED_PROXY_IPS` unset. Let Render provide its runtime indicators and external URL.
- [ ] **MANUAL — NOT YET VERIFIED:** Review Auto-Deploy settings; turn it off if deployments should require a separate manual review.
- [ ] **MANUAL — NOT YET VERIFIED:** Record the deployed HTTPS `*.onrender.com` URL.

## Security verification

- [ ] **MANUAL — NOT YET VERIFIED:** Confirm Free compute in the Render dashboard before relying on `render-free-edge` mode.
- [ ] **AUTOMATED — READY TO RUN:** With the deployed URL and credentials supplied as environment variables, run `.github/scripts/deployment_smoke_test.py`. It checks HTTPS, anonymous and authenticated access, incorrect credentials, protected static assets, private response headers, the Secure culture cookie, authenticated routes and the localized 404. It follows no redirects and refuses requests outside the selected HTTPS `onrender.com` origin.
- [ ] **MANUAL — NOT YET VERIFIED:** Review Render logs for accidental secret disclosure. Do not copy credentials or Authorization headers into notes or reports.
- [ ] **NOT YET VERIFIED:** Real Render ingress behavior and the actual selected service plan are not established by local tests or CI.

Example: enter values interactively so the password is not part of a command or file, then unset them after the run:

```sh
read -r -p 'Render HTTPS URL: ' RENDER_TARGET_URL; export RENDER_TARGET_URL
read -r -p 'Private Alpha username: ' PRIVATE_ALPHA_USERNAME; export PRIVATE_ALPHA_USERNAME
read -r -s -p 'Private Alpha password: ' PRIVATE_ALPHA_PASSWORD; export PRIVATE_ALPHA_PASSWORD; printf '\n'
python3 .github/scripts/deployment_smoke_test.py
unset RENDER_TARGET_URL PRIVATE_ALPHA_USERNAME PRIVATE_ALPHA_PASSWORD
```

## Manual acceptance

- [ ] **MANUAL — NOT YET VERIFIED:** Open the site in a desktop browser; confirm the Basic Auth prompt and private access.
- [ ] **MANUAL — NOT YET VERIFIED:** Repeat on a mobile browser and inspect responsive layout.
- [ ] **MANUAL — NOT YET VERIFIED:** Switch between Ukrainian, English, and Russian; confirm the choice persists.
- [ ] **MANUAL — NOT YET VERIFIED:** Try search and its country/category/status filters.
- [ ] **MANUAL — NOT YET VERIFIED:** Compare two countries for a category.
- [ ] **MANUAL — NOT YET VERIFIED:** Review the published Denmark employment article and its source links.
- [ ] **MANUAL — NOT YET VERIFIED:** Check page layout, text wrapping, and source list visually.

## Public launch privacy and contact checks

- [ ] **LEGAL REVIEW — NOT YET RESOLVED:** Determine whether Decreto-Lei n.º 7/2004, Articles 3 and 10, apply to the project's actual activities and what operator identification/contact information must be published. Do not infer an answer from the operator being an individual or from this checklist.
- [ ] **LEGAL REVIEW — NOT YET RESOLVED:** Confirm that the localized Contact form alone is a sufficient public operator contact arrangement; do not publish a personal email or home address without explicit operator approval.
- [ ] **LEGAL REVIEW — NOT YET RESOLVED:** Confirm the lawful basis for each processing purpose and any required balancing assessment; do not use consent as a blanket basis.
- [ ] **PROVIDER REVIEW — NOT YET RESOLVED:** Confirm the Formspree DPA and international-transfer safeguards applicable to this account. Provider policy statements or a linked DPA do not establish an executed or applicable customer agreement.
- [ ] **PROVIDER CONFIGURATION — NOT YET VERIFIED:** Confirm that the Formspree dashboard setting labelled “HTTP API disabled” is compatible with the application’s HTTPS JSON POST to the configured Formspree form endpoint; verify this during the synthetic production submission.
- [ ] **PROVIDER REVIEW — NOT YET RESOLVED:** Review the implications of the existing Ko-fi support link for the project's processing and disclosures.
- [ ] **MANDATORY BEFORE PUBLIC LAUNCH — NOT YET VERIFIED:** With Basic Auth still enabled, submit one synthetic message through the deployed Contact form; confirm Formspree accepts/processes it and the operator receives it in Proton Mail; verify localized success feedback; inspect application logs to ensure message contents are not exposed. Do not use real personal data in the synthetic submission.

The Privacy page records the operator-confirmed 12-month correspondence-retention period, Render Hobby application-log period, and current provider configuration. Actual production delivery from Contact through Formspree to Proton Mail remains unverified until the check above passes.

## Family testing

- [ ] **MANUAL — NOT YET VERIFIED:** Share the URL only after approval.
- [ ] **MANUAL — NOT YET VERIFIED:** Share credentials separately through a trusted channel, never in the same message as the URL.
- [ ] **MANUAL — NOT YET VERIFIED:** Collect feedback and record issues without requesting sensitive personal or immigration details.
