# Content publishing workflow

This document describes the current manual process for adding and maintaining information entries. The application has no CMS, draft state, source monitor, or automatic verification. The production JSON catalogs are loaded and validated at application startup.

## Content lifecycle

Use this sequence for every information item:

1. **Research:** define one narrow question, country, and category. Separate factual claims so each can be checked against a source.
2. **Collect sources:** find the official page that directly supports each claim and record its exact URL. Read the page itself, including relevant dates and conditions; a homepage alone is not sufficient evidence.
3. **Draft:** write a concise title, summary, and content in the source language. Keep the wording within what the cited pages support. Do not infer eligibility or legal outcomes from incomplete information.
4. **Translate:** prepare Ukrainian (`uk`), English (`en`), and Russian (`ru`) versions of the same logical entry. Preserve official terminology where possible and compare all versions for equivalent meaning.
5. **Review and accept:** review every language version against the supporting pages, check links and dates, and record the translation's current status. Human acceptance is required before assigning `Verified` or deploying the entry.
6. **Integrate:** add the complete entry and its sources to the JSON catalogs in a reviewed change. Run catalog validation and automated tests, then have a human review the content and diff. The application renders `NeedsReview` entries, so a publication candidate in the catalogs must not be deployed until the owner accepts it.
7. **Review:** re-open the source pages when rules change, a link fails or redirects, a discrepancy is reported, or a translation is questioned. Also schedule manual review according to the subject's risk and how often its rules change.
8. **Update or withdraw:** update affected language versions and timestamps, or mark them `Outdated` while they are being corrected. If content must no longer be served, remove the entry and its source records from the production catalogs; the current application has no withdrawal field or editorial archive.

The rendered page shows all entries in the catalogs, including `NeedsReview` and `Outdated` entries, with their status labels. It does not suppress unreviewed or outdated content. Include only claims supported by the cited sources, and never describe a `NeedsReview` or `Outdated` item as confirmed or current guidance. Do not deploy a candidate until a human has accepted its claims, translations, sources, and disclosed limitations.

## Current JSON shape

[`information-entries.json`](../src/RefugeeNavigator.Web/Data/information-entries.json) is an array of logical entries. Each object has these exact property names:

- `Id`, `CountryId`, `CategoryId`
- `Translations`: an array of translation objects containing `InformationEntryId`, `LanguageCode`, `Title`, `Summary`, `Content`, `VerificationStatus`, and `LastUpdatedAt`

Each entry must have exactly one translation for each supported language: `uk`, `en`, and `ru`. Every translation's `InformationEntryId` must equal its parent entry's `Id`. `VerificationStatus` uses the enum values `NeedsReview`, `Verified`, and `Outdated`; with the current default `System.Text.Json` settings, enum values are serialized as their numeric values (`0`, `1`, and `2`). `LastUpdatedAt` is a `DateTimeOffset` serialized as an ISO 8601 timestamp.

[`official-sources.json`](../src/RefugeeNavigator.Web/Data/official-sources.json) is a separate array. Each object has `Id`, `InformationEntryId`, `Title`, `Url`, and `LastCheckedAt`. The source's `InformationEntryId` identifies the logical entry, so multiple source records can support one entry. `Url` is serialized as a URI string and must be an absolute HTTP or HTTPS URI; `Guid` values are serialized as strings, and timestamps are ISO 8601 `DateTimeOffset` values.

The current `OfficialSource` model has no separate organization field: include the issuing organization in the human-readable `Title` when it helps identify the exact page. Do not copy placeholder values from this document into the catalogs.

## Acceptable sources and conflicts

Prefer primary pages from government websites, official immigration authorities, official social protection agencies, public employment services, healthcare authorities, and official EU institutions. Use a source only when its page directly supports the claim being published. Record the page title in `Title`, its exact page URL in `Url`, the related logical entry ID in `InformationEntryId`, and the date that page was actually checked in `LastCheckedAt`.

Check the destination page, not just the domain or homepage. Confirm that the page loads, is relevant to the country and subject, and still contains the supporting detail. Do not invent source URLs or cite a search result as the source.

If official pages contradict one another or are unclear, do not choose one statement as settled law or infer which applies to an individual. When each competing statement is itself supported by an official source, a candidate may report the discrepancy transparently: attribute each statement to its source, state that the reviewed materials have not reconciled them, and link readers to the documents. Contacting an authority is optional, not a publication prerequisite. Human acceptance is still required before assigning `Verified` or deploying. If a statement lacks adequate support, exclude it; if the uncertainty cannot be explained clearly without misleading readers, hold that content back. Articles provide general information and source navigation, not individualized legal or immigration advice; do not tell readers which application they personally should submit. The current content model has no source-priority, conflict-note, or escalation fields.

## Status and review policy

`VerificationStatus` belongs to each language-specific translation:

- **`NeedsReview` (0):** the translation has not completed manual review, or needs review again. Treat it as unconfirmed; do not publish it as settled guidance.
- **`Verified` (1):** a reviewer manually checked that translation against its supporting sources and confirmed that the wording is supported. This records a review at a point in time; it does not guarantee ongoing legal accuracy.
- **`Outdated` (2):** the translation is known or suspected to be out of date. Do not present it as current guidance. Correct it and review it again, or remove the entry from production if it should no longer be shown.

Set a translation to `Verified` only after a human has reviewed that language version against the supporting pages. Change it to `NeedsReview` when the translation or a relevant source changes and review is incomplete, or when a discrepancy is reported. Set it to `Outdated` when a material change or reliable evidence means the content may no longer be current. Return it to `Verified` only after the corrected translation has itself been checked.

Having no source records does not make an entry verified. The current UI displays the status text but does not enforce publication policy; reviewers must ensure that production content is appropriate to serve.

## Translation and dates

Keep one stable `InformationEntry.Id` for the logical item. Put its three language versions in `Translations`, each with the same `InformationEntryId` and one of `uk`, `en`, or `ru`. Country and category IDs are shared by the logical entry, not repeated per translation. Do not translate URL slugs or create separate logical entries just to represent language versions.

Review translations independently. Checking the Ukrainian wording does not verify the English or Russian wording. Keep titles, summaries, conditions, exceptions, and steps consistent in meaning; use the source authority's official terminology where available. Update a translation's `VerificationStatus` and `LastUpdatedAt` independently of the other languages.

- `LastUpdatedAt` is when that language-specific title, summary, or content was most recently edited. Update it when that translation changes.
- `LastCheckedAt` is when a reviewer last opened and checked that particular official source page. Update it only after rechecking that page.

Neither timestamp alone proves that the information is still accurate. Use explicit offsets in ISO 8601 timestamps. The current validator rejects default timestamps and timestamps in the future.

## Publication checklist

- [ ] The entry has the correct country and category IDs.
- [ ] Each material claim is supported by one or more reliable official pages.
- [ ] A reviewer checked the exact source pages, not just their homepages.
- [ ] Every source has a valid absolute HTTP(S) URL, title, related entry ID, and correct `LastCheckedAt`.
- [ ] The logical entry has exactly one complete `uk`, `en`, and `ru` translation.
- [ ] All translations preserve the same meaning and official terminology.
- [ ] Each translation has an appropriate review status; none is presented as confirmed while `NeedsReview` or as current while `Outdated`.
- [ ] Each translation's `LastUpdatedAt` reflects its latest edit.
- [ ] The catalog passes the application's startup validation.
- [ ] Restore, Release build, and automated tests pass.
- [ ] A human reviewer has checked the claims, translations, source pages, and complete diff before publication.

## Manual maintenance

Recheck sources when an authority announces a relevant rule change, a source URL is reported broken or redirects, content receives a challenge, or a translation discrepancy is found. The appropriate review interval depends on the subject: rapidly changing eligibility or temporary measures need closer attention than stable service-navigation details. Set a practical review reminder for the topic, but do not describe it as real-time monitoring.

When a source changes, determine which specific claims and language versions are affected. Update the source's `LastCheckedAt` after checking the new page. Edit only translations that need changes and update their `LastUpdatedAt`; set each affected translation's status to `NeedsReview` until its own review is complete, or `Outdated` when it should not be treated as current. Re-verify all affected language versions before marking them `Verified`.

If guidance should no longer be shown, remove its logical entry and associated source records from the production JSON arrays. Git history retains prior catalog revisions, but the running application has no withdrawal state, audit screen, or automatic expiry. Do not rely on `Outdated` as a mechanism that hides an item.

## Editorial example

The Denmark × Employment entry demonstrates a narrow service-navigation topic. A later Denmark × Temporary protection candidate addresses residence options after protection. For legal or policy topics, attribute each claim to an official source, preserve relevant conditions and exceptions, and distinguish an unresolved official-source discrepancy from settled guidance. A candidate is not a publication: the owner must review the claims, translations, and sources before deployment.
