# Seven-Country Private Beta Preflight — 2026-10-28

## Verdict

**GO FOR OWNER DEPLOYMENT DECISION**

This preflight establishes that the current release candidate is ready for the owner to decide whether to deploy. It does not authorize or perform deployment. The current live service remains on the previous six-country release; Sweden is **not** represented as live.

## Release candidate and scope

- **Release candidate:** `5a458c106c1e2d8d3d3c6b8904b03d996895692b`
- **Promotion commit:** `5a458c106c1e2d8d3d3c6b8904b03d996895692b`
- **Expected Production countries after a separate deployment:** Denmark, Germany, Poland, Czechia, Spain, Netherlands, and Sweden.
- **Visibility rule:** the existing generic `CountryVisibility` rule includes countries with at least one Verified translation. No country-specific Sweden rule was added.

## Sweden content and visibility

The catalog contains 8 Sweden articles and 24/24 EN/UK/RU translations are `Verified`. Its 24 comparison facts comprise 23 `Approved` available facts and one held fact:

| Aspect | Status | Available | Sources | Production behavior |
| --- | --- | --- | --- | --- |
| `social-child-benefits` | `Draft` | false | none | Shows the localized unresolved/unavailable state; no answer or source list is exposed as approved. |

Production component tests confirm Sweden appears on Home, its country and category routes, Search filters/results, and Compare selectors. Its verified category content renders, and its 23 available facts render under the normal Approved/source gates. Development navigation and preview regression tests also pass.

## Netherlands regression

The accepted subset remains 21 Verified translations and 20 Approved facts. The remaining 3 translations and 4 facts are still held. Tests confirm NL-TP-01 and the Draft facts `protection-scheme`, `protection-application`, `protection-dates`, and unavailable `residence-filing` remain suppressed from accepted Production content; the unavailable fact stays source-less and the accepted comparison control remains available.

## Validation

- **Targeted status, Sweden, CountryVisibility, Search, Compare, and Netherlands regression tests:** 158/158 passed.
- **Full Release test suite:** 605/605 passed.
- **Release build:** succeeded.
- **JSON validation:** passed through the catalog and comparison-fact provider validation exercised by the test suite. The promotion diff changed only 24 translation statuses and 23 available-fact statuses; wording, answers, sources, timestamps, and the held fact are unchanged.
- **Docker image build and container smoke:** passed in [CI run 40760253383](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/40760253383) for this exact candidate SHA.
- **CI:** success in the same run, including build, full tests, deployment-verifier tests, Docker image build, and Docker integration smoke tests.
- **Existing visual workflow:** [Netherlands visual evidence run 40760253079](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/40760253079) succeeded. It covers Netherlands only and is not counted as Sweden visual evidence.

## Sweden mobile visual gate

The prior Sweden review recorded the 390px mobile inspection as incomplete. The successful Netherlands workflow did not close that Sweden-specific gap, so a focused local Production visual check was performed against this release candidate without changing repository files or contacting the live service.

- At **390×844**, all six Sweden category routes were checked in EN, UK, and RU (18 route/language combinations); all eight articles rendered in each language.
- Compare rendered all 24 aspects in EN, UK, and RU. The held child-benefits fact displayed the localized unresolved state without a source disclosure or approved answer.
- Category TOC/source disclosures, Compare article/source disclosures, and keyboard toggling were checked. Layout width stayed at 390px with no horizontal overflow on the checked pages or expanded states.
- A desktop 1280px smoke check of Sweden Category and Compare also passed. Representative screenshots were saved outside the repository at `/tmp/sweden-preflight-evidence/` and visually inspected.
- **Result:** the outstanding 390px visual verification is complete; no clipping, overlap, or horizontal overflow was found. This is visual verification evidence, not an owner deployment decision.

## Deployment state

No deployment was performed. No live endpoint was queried. The owner must make a separate deployment decision; until then, the existing live release remains six-country and does not include Sweden.
