# Content Maintenance Audit

## Purpose

The local console tool helps editors schedule source reviews and trace the articles and structured comparison facts that reference each source. It uses catalog IDs and recorded `LastCheckedAt` values; it does not assess legal validity, verify content, or contact official websites.

The report is read-only. It never changes `VerificationStatus`, comparison editorial status, `LastCheckedAt`, or `LastUpdatedAt`. A recorded source date means only that the catalog contains that date; the tool does not claim the page was checked or is still available.

## Run

From the repository root, with the .NET 10 SDK:

```sh
dotnet run --project tools/RefugeeNavigator.ContentAudit
```

This prints a console summary and a detailed Markdown report. To use a fixed report date and save the Markdown outside production content directories:

```sh
dotnet run --project tools/RefugeeNavigator.ContentAudit -- \
  --as-of 2026-10-17 \
  --output /tmp/content-maintenance-audit.md
```

Options:

- `--data-dir PATH`: directory containing `countries.json`, `categories.json`, `information-entries.json`, `official-sources.json`, and `comparison-facts.json`. The default locates `src/RefugeeNavigator.Web/Data` from the current directory or its parents.
- `--as-of YYYY-MM-DD`: explicit UTC calendar date for deterministic day calculations. Defaults to the current UTC date.
- `--recent-days N`: last age still described as recently checked. Default: `30`.
- `--review-days N`: last age still described as review suggested. Default: `90`.
- `--output PATH`: write the Markdown report to the specified path. When omitted, print it after the summary. The tool does not create a report in a production content directory automatically.
- `--help`: show usage.

Thresholds must be non-negative and the review-suggested threshold must not be lower than the recently-checked threshold.

## Report fields

The report includes total articles, translations, official source records, and structured comparison answers; the source age distribution; each source ID, title, URL, related country/article/category, stored check date, age, priority, and linked comparison facts; review candidates; affected articles and comparison facts; duplicate URLs; and invalid or missing relationships.

Source records sharing an exact URL are grouped in the duplicate section, but their IDs and check dates remain separate. The report never merges records or assumes that checking one record updates another.

## Review thresholds

- `0–30` days by default: **Recently checked**.
- `31–90` days: **Review suggested**.
- More than `90` days: **Review priority**.

These are editorial scheduling rules only. Age alone does not mean that information is outdated, inaccurate, or legally invalid. The `--as-of` date and configurable thresholds make reports reproducible.

## Source dependency tracking

Official source records link to articles through `InformationEntryId`. Comparison facts link to articles through `InformationEntryId` and to official sources through `OfficialSourceIds`; country and category context is joined through existing country/category IDs and slugs. The audit does not infer relationships from similar titles, keywords, or subject matter.

The loader reads the existing JSON schemas through the existing catalog providers for countries, categories, entries/translations, and sources. It reads only the comparison relationship fields needed to map topic, aspect, country, article, and source IDs.

## Limitations

- No HTTP requests, scraping, or external services are used.
- No legal or editorial verification is automated.
- A source's stored date does not prove the source page was checked successfully, is accessible now, or remains accurate.
- The tool identifies dependencies, not which exact sentence or legal claim each source supports.
- A source age does not automatically alter article or comparison statuses.
- Invalid or missing IDs are reported where the relevant records can be loaded; malformed JSON or malformed typed values stop the run with an error.

## Manual editorial workflow

1. Review the age report and choose a source for human rechecking.
2. Open the official source directly and verify its current content, date, conditions, and scope.
3. Review every linked article and structured comparison fact identified by ID. Confirm the source still supports each claim; do not infer support from the title or topic.
4. Record research evidence and editorial decisions using the normal content workflow. If content changes, update its timestamps through that workflow; do not use the audit tool for edits.
5. Change verification or approval status only after the required human review and publication process.

The report itself is a work queue, not publication approval.
