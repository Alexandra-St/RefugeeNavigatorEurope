# Four-Country Private Beta Preflight — 2026-10-25

**Repository:** `Alexandra-St/RefugeeNavigatorEurope`\
**Baseline:** `ecd2820a6d55e95214a5e2cfcdf5aee9e09e81a8`\
**Scope:** local production/release preflight for Denmark, Germany, Poland, and the accepted Netherlands content set. No live Render service was contacted.

## Accepted scope and Netherlands holds

| Country | Article translations | Comparison facts |
| --- | --- | --- |
| Denmark | 24 `Verified`; 0 `NeedsReview`; 0 other | 24 `Approved`; 0 `Draft`; 0 other |
| Germany | 24 `Verified`; 0 `NeedsReview`; 0 other | 24 `Approved`; 0 `Draft`; 0 other |
| Poland | 24 `Verified`; 0 `NeedsReview`; 0 other | 24 `Approved`; 0 `Draft`; 0 other |
| Netherlands | 21 `Verified`; 3 held `NeedsReview`; 0 other | 20 `Approved`; 4 held `Draft`; 0 other |

The accepted Netherlands scope is exactly 21 `Verified` translations and 20 `Approved` facts. Excluded intentional holds:

- NL-TP-01, EN/UK/RU, `NeedsReview`;
- `protection-scheme`, `protection-application`, and `protection-dates`, `Draft` and linked to NL-TP-01;
- `residence-filing`, `Draft`, unavailable, and source-less.

No temporary or synthetic status substitutions were used. Production catalogs were read as committed.

## Production gating

Checked against the production catalogs and `ASPNETCORE_ENVIRONMENT=Production` behavior:

- Netherlands Category shows the accepted NL-TP-02 article and omits held NL-TP-01.
- Opening the NL-TP-01 direct category fragment does not create its article anchor or expose its title/body.
- Search by the held NL-TP-01 title returns no Netherlands result; the accepted NL-EMP-01 article is searchable.
- Production Compare renders approved content. The four held Netherlands answers are suppressed and their source groups are absent; their aspect panels show the existing “not established” placeholder. `residence-filing` remains unavailable and does not render as an available answer.
- The expanded Compare full-article view shows accepted NL-TP-02 content. NL-TP-01 is represented only by the generic unavailable message; its title, summary, body, and sources are not exposed.
- No comparison-category failure or layout defect was observed.

The full test suite includes targeted Netherlands Category/Search gate tests and Compare tests that verify the 20 Approved / 4 Draft split, source ownership, and unavailable `residence-filing` invariants.

## Build, tests, Docker, and container smoke

- Release build: **passed**, 0 warnings and 0 errors.
- Full Release test suite: **589 passed, 0 failed, 0 skipped**.
- CI deployment-verification Python tests: **5 passed**.
- Local production Docker image build: **passed**.
- Local synthetic private-mode container checks: **passed** using fictitious CI-only credentials. Anonymous requests to the root, Search, Compare, Netherlands Category, all 3 discovered CSS/JS assets, and `/set-language` were denied. Incorrect credentials were denied; correct synthetic credentials were accepted. The language cookie was `Secure` and responses carried `X-Robots-Tag: noindex, nofollow`.
- The repository's `docker-smoke-test.sh` could not be invoked directly by the host's stock macOS Bash 3.2 because it uses `mapfile` (not available in that shell). Its assertions were reproduced locally against the same image and passed. The normal post-push GitHub Actions run will execute the unchanged repository script on its supported Ubuntu runner.

No real credentials were used. `render-free-edge` and the Render environment values above were only synthetic inputs to the isolated local auth test; they do not establish the configuration or compute plan of any hosted service.

## Four-country browser/runtime smoke

Used the locally built Production image and real production catalogs; no catalog fixture or status change was used.

- Home, Search, Compare, and country routes loaded for Denmark, Germany, Poland, and Netherlands. Each country page exposed its category navigation.
- Netherlands Category and Compare were exercised at desktop 1280px and narrow mobile 320px; Category was also checked at 390px. `scrollWidth` did not exceed the viewport width in the checked views.
- EN, UK, and RU language controls worked on Compare and retained its country/category query parameters.
- Search results showed an accepted Netherlands article and excluded NL-TP-01.
- Compare selection, comparison content, and the full-article disclosure worked with the accepted Netherlands content; held answers and sources stayed gated.
- Root HTML's two CSS assets and Blazor JavaScript asset returned HTTP 200 in local public packaging mode; the application initialized and rendered its pages.

No clipping, horizontal overflow, held-content leakage, or other runtime defect was observed in these smoke checks. This was a focused release smoke, not a full manual accessibility or visual acceptance audit.

## Scope integrity and source structure

The four-country status counts match `docs/qa/private-beta-content-readiness-2026-10-25.md`. Production catalogs have not changed since that baseline. Existing structural checks and the full test suite found no missing source references or wrong article/source ownership in the checked scope. No new `NeedsReview`, `Draft`, or other status regression was found. Source contents were not re-researched.

## Findings and remaining deployment gates

**Preflight defects:** none found for the accepted four-country Private Beta scope.

The Netherlands hold set remains intentionally excluded. Germany and Poland retain the time-sensitive source-maintenance notes recorded in their owner acceptance checklists; this preflight did not reverify those legal/source details. Before enabling private traffic, an operator must separately confirm the actual Render compute plan in the Dashboard and complete the deployment-time checks in `docs/RENDER_DEPLOYMENT.md`. This report neither checks a live deployment nor authorizes one.

## Verdict

**GO FOR OWNER DEPLOYMENT DECISION** — for the accepted four-country Private Beta scope and with the Netherlands hold set excluded. This is readiness for a separate owner decision only; no deployment was performed or authorized by this preflight.
