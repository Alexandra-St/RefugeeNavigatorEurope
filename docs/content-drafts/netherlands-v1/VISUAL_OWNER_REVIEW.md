# Netherlands Visual Owner Review — Phase 18.4R

## Source

- GitHub Actions run: [39607279744 — Netherlands visual evidence](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/39607279744)
- Source commit: `4edd2019d58d7309995ff56e814ca2fed16571f0`
- Artifact: [netherlands-publication-gate-screenshots (ID 12348886647)](https://github.com/Alexandra-St/RefugeeNavigatorEurope/actions/runs/39607279744/artifacts/12348886647)
- Artifact inspection: downloaded and opened locally; contains 18 PNG files and the run-specific `CI_VISUAL_EVIDENCE.md`.
- Capture environment: headless Chromium / Playwright against an isolated local Production-mode app and synthetic catalogs. No hosted Production was accessed.

## Inventory

All 18 PNGs were present, non-empty, readable as PNG images, and opened for review. Pixel dimensions equal the requested viewport widths; full-page heights are listed below.

| Page | Language | Viewport | PNG file | Image dimensions |
| --- | --- | --- | --- | --- |
| Core Category | EN | 1280×720 | `core-category-en-1280x720.png` | 1280×1716 |
| Core Category | EN | 390×844 | `core-category-en-390x844.png` | 390×2309 |
| Core Category | EN | 320×700 | `core-category-en-320x700.png` | 320×2799 |
| Comparison V2 | EN | 1280×720 | `comparison-v2-en-1280x720.png` | 1280×9133 |
| Comparison V2 | EN | 390×844 | `comparison-v2-en-390x844.png` | 390×18255 |
| Comparison V2 | EN | 320×700 | `comparison-v2-en-320x700.png` | 320×22722 |
| Core Category | UK | 1280×720 | `core-category-uk-1280x720.png` | 1280×1716 |
| Core Category | UK | 390×844 | `core-category-uk-390x844.png` | 390×2560 |
| Core Category | UK | 320×700 | `core-category-uk-320x700.png` | 320×2892 |
| Comparison V2 | UK | 1280×720 | `comparison-v2-uk-1280x720.png` | 1280×9307 |
| Comparison V2 | UK | 390×844 | `comparison-v2-uk-390x844.png` | 390×19989 |
| Comparison V2 | UK | 320×700 | `comparison-v2-uk-320x700.png` | 320×24809 |
| Core Category | RU | 1280×720 | `core-category-ru-1280x720.png` | 1280×1744 |
| Core Category | RU | 390×844 | `core-category-ru-390x844.png` | 390×2426 |
| Core Category | RU | 320×700 | `core-category-ru-320x700.png` | 320×2980 |
| Comparison V2 | RU | 1280×720 | `comparison-v2-ru-1280x720.png` | 1280×9778 |
| Comparison V2 | RU | 390×844 | `comparison-v2-ru-390x844.png` | 390×21573 |
| Comparison V2 | RU | 320×700 | `comparison-v2-ru-320x700.png` | 320×26292 |

## Visual observations

- Desktop Comparison V2 keeps the two country panels side by side, with topic and aspect headings visibly grouping the answers. Core Category has a clear article card and separate source list.
- At 390px and 320px, navigation and selectors wrap into a readable vertical flow; comparison country panels stack vertically. Text and long source names wrap within their cards. No visible clipping, overlap, or horizontal overflow was found in the reviewed views. The CI overflow assertions also passed at all 18 captures.
- Source links are visually distinct and paired with checked-date labels on the Core Category page. Comparison source groups are presented as expandable “Sources for this item” controls; the controls wrap cleanly at narrow widths.
- The Netherlands answers and sample article deliberately contain `SYNTHETIC-CI-FIXTURE` / “synthetic” preview wording. This is test-fixture content, not a production-content defect. The held article and Draft-only answer/source sentinels were confirmed absent by the CI assertions.
- Unavailable answers display an explicit “Not established in the reviewed sources” message rather than a blank panel. These are intentional evidence-gap placeholders in the fixture, not missing UI content.
- Comparison full-page captures are very tall (9,133–26,292 px) because they show all categories and aspects. This is expected for full-page evidence; it is not clipping. The owner may wish to confirm that this dense all-category view is acceptable for review.

## Issues requiring correction

None identified from the available screenshots. No page/language/viewport/file-specific visual defect requires a code correction.

## Awaiting owner confirmation

- Confirm the long, dense full-page Comparison V2 presentation is suitable for the all-categories view.
- Confirm that the expandable source groups and wrapped mobile navigation/selectors are clear at 320px and 390px.
- Treat synthetic fixture labels and sample text only as layout evidence; they are not proposed public content.

## Proposed verdict

**READY FOR OWNER VISUAL ACCEPTANCE**

This is a preparation recommendation only. It does not record owner acceptance or publication approval. The artifact remains available for direct inspection from the linked GitHub Actions run.

## Publication

**Not published. No content or status changes were made, and no deployment was performed.**
