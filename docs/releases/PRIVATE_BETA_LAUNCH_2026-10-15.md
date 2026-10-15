# Private Beta launch — 15 October 2026

## Launch

The first password-protected Private Beta launched on 15 October 2026.

- **Hosting:** Render, Frankfurt
- **URL:** [https://refugeenavigatoreurope.onrender.com](https://refugeenavigatoreurope.onrender.com)
- **Access:** Private Beta protected by HTTP Basic Authentication. This is not a public release.
- **Initial country:** Denmark
- **Published content:** 8 articles, each available in Ukrainian (`uk`), English (`en`) and Russian (`ru`); all 24 translations are marked `Verified`.
- **Release readiness:** Phase 5.24 recommended GO for a separate owner deployment decision. Phase 5.25 preflight was completed before deployment.

## Validation

The release CI completed successfully for commit `7e98d5a36bbe90212cb874610433606637c47e72` (347 .NET tests passed, 5 deployment-script tests passed, and Docker build and smoke tests passed). This identifies the CI-tested repository commit; it does not independently identify the commit deployed to Render. [GitHub Actions run](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/38811504134).

The project owner confirmed that the Render deployment completed, the application started in Production, HTTPS opened, the browser requested authentication, valid credentials granted access, and all eight Denmark articles were reviewed and displayed correctly.

## Checks not confirmed

There is no evidence in this launch record that every language, mobile layout, search flow, comparison flow, or every authentication and security edge case was manually tested. The automated CI smoke tests are not a substitute for those live checks.

## Deployment identification

- **Latest GitHub `main` at documentation time:** `7e98d5a36bbe90212cb874610433606637c47e72`.
- **Commit actually deployed to Render:** Unverified. The Render deployment record was not independently inspected, so the `main` SHA is not used as a substitute.

## Known content and legal limitations

- Denmark is currently the only content-complete country.
- The official-source discrepancy between 17 March 2027 and 17 March 2028 remains transparently attributed in the relevant article; this launch does not resolve it or establish an individual permit's validity.
- L 43 remains subject to monitoring and a fresh legislative check before relying on any proposed changes.
- Verified status reflects editorial review at a point in time and does not guarantee continuing legal accuracy.

## Next development priority

Develop Germany content next. Poland follows Germany.
