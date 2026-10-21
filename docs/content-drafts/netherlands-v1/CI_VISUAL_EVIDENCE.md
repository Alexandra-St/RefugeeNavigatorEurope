# Netherlands CI Visual Evidence

This workflow captures reproducible browser evidence for the Core Category and Country Comparison V2 publication gates without using hosted Production or modifying checked-in catalogs.

## Capture workflow

The dedicated `Netherlands visual evidence` GitHub Actions workflow runs on pushes to `main` and can also be started manually with `workflow_dispatch`. It publishes the existing web application, creates a temporary synthetic catalog fixture under the runner's temporary directory, and starts the app with `ASPNETCORE_ENVIRONMENT=Production`. The packaging-only `PRIVATE_ALPHA_ACCESS_MODE=public` setting is confined to this isolated CI process; it does not change deployment configuration.

Playwright runs headless Chromium with filesystem screenshot support. Each capture uses `page.screenshot({ fullPage: true })`. Nothing connects to the hosted service, and no screenshot binary or generated fixture is committed.

## Coverage and assertions

The workflow captures both the Netherlands Temporary Protection category and the Netherlands/Denmark Comparison V2 page for EN, UK and RU at 1280×720, 390×844 and 320×700: 18 full-page PNG files total.

The synthetic fixture marks the seven preliminary Core candidates Verified for Production visibility and gives them breakable sample copy, while held NL-TP-01 remains NeedsReview with a unique hidden-text sentinel. It marks the 20 selected Netherlands comparison aspects Approved and the remaining four Draft; `residence-filing` stays unavailable and source-less. Synthetic sample answers are localized, and source records remain isolated in the runner's temporary published output.

The capture fails if the language attribute is wrong, the held article or Draft-only answer/source leaks, an approved answer or its source link is missing, the unavailable filing fact is not shown as unavailable, the document overflows horizontally, or a screenshot is not a nonempty full-page PNG with the requested viewport width. The run-specific Markdown report is included with the screenshots and GitHub Actions job summary.

## Artifact and acceptance state

Successful runs upload the actual PNG files and run report as the artifact **`netherlands-publication-gate-screenshots`**, retained for 30 days. Open the specific workflow run in GitHub Actions and download that artifact to review the screenshots.

Automated capture and assertions do not equal human visual acceptance. The visual release gate remains **REVIEW PENDING** until the saved screenshots are inspected and accepted. This workflow does not promote content statuses, publish content, or deploy the application.
