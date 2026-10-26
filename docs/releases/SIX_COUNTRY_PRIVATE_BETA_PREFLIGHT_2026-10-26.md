# Six-Country Private Beta Preflight — 26 October 2026

## Release candidate and scope

- **Release candidate:** `190b44b5137f3224c04961e8caa226329d1c9e54`
- **Accepted Production countries:** Denmark, Germany, Poland, Czechia, Spain, and Netherlands.
- **Sweden:** remains in the repository for Development/editorial preview; the visibility gate hides it from Production navigation and direct country/category routes.
- **Deployment:** not performed. This report records readiness for the owner's separate deployment decision.

## Content scope

| Country | Verified translations | Approved comparison facts | Production content result |
| --- | ---: | ---: | --- |
| Denmark | 24 | 24 | Accepted |
| Germany | 24 | 24 | Accepted |
| Poland | 24 | 24 | Accepted |
| Czechia | 24 | 24 | Accepted |
| Spain | 24 | 24 | Accepted |
| Netherlands | 21; 3 NeedsReview | 20; 4 Draft | Accepted subset only; holds stay hidden |
| Sweden | 0; 24 NeedsReview | 0; 24 Draft | Hidden in Production; available in Development |

The Netherlands holds remain NL-TP-01 in EN/UK/RU and the four Draft facts: `protection-scheme`, `protection-application`, `protection-dates`, and `residence-filing`. The visibility gate does not change catalogs or statuses. Production article and fact rendering continues to apply the existing Verified/Approved gates, so held Netherlands and Sweden content is not exposed as accepted content.

## Visibility validation

The reusable country visibility rule includes a catalog country in Production when at least one of its translations is Verified. It is applied consistently to Home, country and category routes, the Search country filter, and both Compare selectors. A manually supplied Sweden Compare parameter resolves through the existing invalid-country state. Development continues to expose all seven catalog countries and its Draft preview.

Targeted tests confirmed:

- Home renders exactly six Production country cards, including Czechia and Spain, and omits Sweden.
- Production country and category routes return the existing NotFound state for Sweden; accepted country routes remain available.
- Search filters expose six countries and retain the Verified-only results gate.
- Compare selectors expose six supported, accepted countries; manually selecting Sweden is invalid.
- Development exposes all seven countries, including Sweden's review content and Draft comparison preview.

## Build, tests, and CI

- Local Release build: **passed**.
- Local targeted tests: **218/218 passed**.
- Local full test suite: **605/605 passed**.
- `git diff --check`: **passed**.
- GitHub Actions CI for the release candidate: **success** — [run 40338878539](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/40338878539). Build, full .NET tests, deployment-verifier tests, production Docker image build, and Docker integration smoke tests all succeeded.
- Netherlands visual evidence: **success** — [run 40338878522](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/40338878522). The isolated Production run prepared synthetic Netherlands catalogs, passed visibility assertions, captured screenshots, and uploaded the evidence artifact.

## Known limitations

The Netherlands visual workflow validates Production visibility with its isolated synthetic fixture. It does not constitute a new authenticated live deployment check. This is a preflight for the stated candidate; deployment and post-deployment verification remain separate owner actions.

## Verdict

**GO FOR OWNER DEPLOYMENT DECISION**
