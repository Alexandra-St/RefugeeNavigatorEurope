# Poland content integration report

**Integration date:** 16 October 2026  
**Baseline:** `426342482e04ce439f82ead3531d88d7bccc23a5`  
**Status:** Integrated for human editorial review. Publication has **not** been authorized.

## Integrated articles

Each article has one UK, EN and RU translation. All 24 translations remain `NeedsReview` (0); none is marked `Verified`.

| Article | Category | InformationEntry ID | Official-source records |
| --- | --- | --- | ---: |
| Applying for Temporary Protection in Poland | Temporary protection | `218207bd-2a85-59e1-a945-eedb0604392e` | 10 |
| Residence Options After Temporary Protection | Temporary protection | `c230d45f-9c76-5912-8c97-dc7d0689baa6` | 5 |
| Finding a Job in Poland | Employment | `a323701f-367e-52bb-9c33-45c4156a1350` | 5 |
| Work Authorization and Employee Rights | Employment | `7797d03c-6873-50cd-aef6-0d94a2f5fcab` | 5 |
| Accessing Healthcare in Poland | Healthcare | `d7e0716b-154d-5db3-8790-f73684c07271` | 4 |
| Finding Accommodation in Poland | Housing | `1f310b3c-dceb-5e3a-90d0-d3a705ca5c53` | 5 |
| Education and Polish-Language Learning | Education | `32ca34e9-22d9-5f59-88a9-bbd7ffd7b5cd` | 3 |
| Financial and Social Assistance | Social assistance | `a5892591-d082-57ce-acf9-5c39ab4f9b84` | 8 |

The catalog now contains 24 articles: eight each for Denmark, Germany and Poland. Denmark and Germany remain unchanged and retain their existing Verified translations.

## Sources

Added 45 source records linked to the eight articles, representing 34 distinct official URLs. The research matrices list 36 unique URLs; two were excluded because the integrated text does not rely on them:

- The Council press release about the July 2026 agreement was superseded for the current rule by the adopted Council decision and its official corrigendum.
- The UdSC education page contains embedded material identified in the research review as potentially stale; the integrated age and support rules rely on the in-force Education Law text instead.

The KSI page that still displays the superseded 31/30 July military-obligation dates is retained only as an explicitly attributed example of outdated official guidance. Its source title says that the page shows superseded dates, and the article states that the corrigendum governs. The Mazowieckie accommodation source is explicitly labelled regional; no nationwide availability is inferred.

Source matrices record the actual check date, 16 October 2026, without a clock time. Because `LastCheckedAt` requires a timestamp, the catalog stores that date at `2026-10-16T00:00:00+00:00` as a date-only UTC representation; it does not claim that a source was checked at midnight. Translation `LastUpdatedAt` values use the actual integration edit timestamp.

## Formatting and translation checks

The three language drafts were converted to the formatter’s supported headings and plain paragraphs. Research-only notices and the separate source lists were omitted from article bodies; Markdown links and emphasis syntax were removed. Section headings are localized for each language. The long first Temporary Protection rules section was split into four logical paragraphs covering the EU/Polish stay distinction, paper-certificate validity, corrected EU military-obligation dates and superseded KSI guidance. The legal qualifications and source relationships were retained.

UK, EN and RU versions retain the same material dates, eligibility conditions, exceptions, route limitations and warnings. No individual legal outcome is inferred. All 24 translation statuses are `NeedsReview`.

## Automated validation

- Release build: passed, zero warnings and zero errors.
- Automated tests: **360 passed**.
- Catalog loading and relationship validation: passed.
- Poland: 8 entries, 24 translations, 24 `NeedsReview`, 0 `Verified`.
- Denmark/Germany: 8 entries each; their existing Verified content remains intact.
- Poland sources: 45 records, 34 distinct URLs; all records use HTTPS and resolve to a Poland entry.
- `git diff --check`: passed.

The integration tests cover language coverage, status labels, article rendering, headings, source links, search, comparison, critical dates and qualifications, and preservation of Denmark/Germany counts and statuses.

## Local visual preview

The application ran in Development mode at localhost. The browser review covered all six categories in UK, EN and RU at 1440px, and representative Temporary Protection, Employment and Social Assistance pages at 390px. Search results and Poland/Denmark comparison were checked for all six categories in all three languages. The 42 browser checks found no missing articles or source links, incorrect status labels, language mismatch or horizontal overflow. The Temporary Protection text wraps into four readable rules paragraphs; date and source-check labels display as 16 October 2026.

Representative screenshots were saved outside the repository for human review: six English desktop category pages; Ukrainian and Russian desktop Temporary Protection pages; three 390px mobile category pages; and desktop search and Employment comparison views. They are temporary preview artifacts, not production assets.

**Minor existing UI copy issue:** the search introduction still says “published information” and the comparison panel says “Available published articles,” although NeedsReview material is visible. This is outside the integration file scope and was not changed. It should be corrected before a future release that could be read as implying approval.

## Human review and publication gate

Human editorial and translation review is still required for all 24 translations. Before any publication decision, recheck legislation and source freshness, the outdated KSI page, the expired identity-confirmation deadline’s application to the defined cohort, local service availability, CUKR/MOS instructions, and the 2026 minimum-wage amount. Individual status, coverage, services or benefit eligibility cannot be determined by these general articles. These articles remain visible in the current application despite `NeedsReview`; this integration does not authorize deployment.
