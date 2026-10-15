# Germany Content Batch Integration — 16 October 2026

## Result

Eight Germany information entries have been integrated as publication candidates. Each entry has complete Ukrainian, English, and Russian translations. All 24 translations remain `NeedsReview (0)`. No application behavior or deployment settings were changed. The articles are not approved for publication and no deployment was performed.

| Topic | Category | InformationEntry ID | Official-source records |
|---|---|---|---:|
| Temporary protection application | Temporary protection | `1a6b97c4-eb9e-5590-8715-c95f5f95677e` | 10 |
| Residence options after protection | Temporary protection | `33106e71-115f-5809-9066-7e3d3e5c57a3` | 10 |
| Finding a job | Employment | `283d4519-21f5-5be2-a534-1bd2606d95fc` | 11 |
| Work authorization and employee rights | Employment | `c4895b5a-eacd-53d7-a86f-f504dd615c76` | 10 |
| Healthcare | Healthcare | `895b79b4-ce0d-5bee-9919-42ce3d76ecdb` | 12 |
| Housing | Housing | `fd041e53-7794-5c1f-9ae4-131930fe69ee` | 9 |
| Education and German-language learning | Education | `f5e0f499-0f60-5465-85f8-de6345029b59` | 10 |
| Financial and social assistance | Social assistance | `31f5802b-26a0-5cd0-bb24-0a9a4afa706a` | 15 |

The entries contain 87 source relationships across 65 distinct official URLs. All source records link to the intended entry and retain their exact checked timestamps. Translation `LastUpdatedAt` is `2026-10-15T23:43:55+00:00` for this edit.

## Editorial and source handling

Article bodies were adapted from the reviewed language drafts into the application’s supported plain-text format: paragraphs, `##` section headings, and unordered `-` lists. Research-only source lists, editorial notes, raw URLs, Markdown links, and unsupported inline emphasis were kept out of article content. Each draft retains four sections, with blank-line separation that the current formatter recognizes.

The temporary-protection articles distinguish the EU extension to 4 March 2028 from the published German ordinance’s 4 March 2027 date for the specified group of permits. They attribute the conflicting 2028 statements to Germany4Ukraine and leave the conflict unresolved. They do not infer an individual permit’s validity or work permission from either date. The EU military-obligation rule and Germany’s separate UkraineAufenthÜV §2(3) condition are described separately. The social-assistance bill is identified as a proposal, not current law. Healthcare routes, housing support, education/childcare conditions, and local variation remain scheme- or situation-specific.

Primary sources were rechecked on 15 October 2026 UTC (the checks occurred late that evening; the local Europe/Lisbon date was 16 October). The review covered 69 unique candidate URLs. Two EUR-Lex text endpoints returned HTTP 202; the corresponding official Official Journal PDFs were opened and read, and those direct PDF links are used in the source register. Three matrix rows covering two Make it in Germany URLs were excluded because the pages presented a browser-verification challenge. Four rows pointing to one Bundestag DIP procedure page were excluded because the page returned only its site shell without substantive record data. One Bundestag press item was omitted because none of the article claims relies on it. The official printed government bill and BMAS tracker support only the statement that the benefit reform remains a proposal; no proposed change is presented as enacted law.

## Validation

- Catalog loading and startup data validation: passed through `JsonCatalogProvider.LoadAsync()` in the regression suite.
- Release build: passed with 0 warnings and 0 errors.
- Automated tests: 352 passed, 0 failed, 0 skipped. This includes the new Germany integration coverage for catalog relationships, all three languages, article rendering, source links, search, comparison, and retained legal qualifications.
- Existing Denmark content remains present (8 entries / 101 linked source records); Poland remains without information entries.
- Browser/manual visual review and legal editorial acceptance remain outstanding.
- GitHub Actions result: to be recorded after the integration commit is pushed.

## Review gate

All Germany translations require human editorial and translation review. In particular, reviewers should recheck the extension-date discrepancy and its attribution, current military-rule wording, the legislative status and effective date of the benefit proposal, and source freshness immediately before any publication decision. This integration does not mark content `Verified`, authorize a release, or change the project’s deployment state.
