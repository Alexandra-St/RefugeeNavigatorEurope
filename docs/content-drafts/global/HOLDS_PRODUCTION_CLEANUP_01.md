# Global Holds Production Cleanup 01

**Date:** 2026-11-16
**Scope:** Complete production promotion for the eight Task 46 approved Compare facts and the reviewed Netherlands `NL-TP-01` article. This cleanup preserves the two explicitly intentional Compare holds.

## Production changes

- Promoted `NL-TP-01` EN/UK/RU from NeedsReview to Verified and made the article Production-visible. The catalog content is synchronized with `docs/content-drafts/netherlands-v1/NL-TP-01.md`.
- Added six owner-specific HTTPS source records: two for Netherlands `NL-TP-01`, and one each for Denmark `DK-TP-01`, Spain `ES-HOUSING-01`, Germany `DE-EDU-01`, and Spain `ES-SOC-01`.
- Promoted the eight Task 46 approved facts to Approved and available, retaining owner-correct source bindings. The Netherlands `residence-filing` and Sweden `social-child-benefits` facts remain untouched, Draft, unavailable, and source-less.

## Expected final inventory

| Area | Result |
|---|---:|
| Countries | 19 |
| Articles | 152 |
| Translations | 456 |
| Verified / NeedsReview / Outdated | 456 / 0 / 0 |
| Compare facts | 456 |
| Approved / Draft | 454 / 2 |
| Available / unavailable | 454 / 2 |
| Intentional holds | Netherlands `residence-filing`; Sweden `social-child-benefits` |

## Validation record

The mandatory release gate completed on 2026-11-16:

- Restore: PASS; NuGet vulnerability metadata emitted NU1900 because it could not be accessed.
- Release build: PASS (0 errors; NU1900 warning only).
- .NET tests: PASS, 703/703.
- Python tests: PASS, 104/104.
- `git diff --check`: PASS.
- JSON validity, UUID uniqueness, HTTPS source records, source ownership/bindings, 19/152/456 Core inventory, 456 Verified translations, 454/2 Compare status/availability counts, and exact hold identities: PASS.
- NL-TP-01 draft/catalog parity and Production visibility: PASS in the .NET integration suite.
- Docker production image and integration smoke: **NOT RUN — daemon unavailable** (`/Users/alexandra/.docker/run/docker.sock` is absent).
- Commit `985ebc662af3352405c6579edb819130c820d5bf` (`chore: resolve final production content holds`) was pushed to `main`.
- Exact-SHA GitHub Actions CI for that commit: PASS, including build, tests, deployment verification, production Docker image build, and Docker integration smoke.
- Netherlands visual evidence workflow for that commit: PASS.
- After recording this report update, repeat the commit/push and branch-tip CI check for the final report revision.
