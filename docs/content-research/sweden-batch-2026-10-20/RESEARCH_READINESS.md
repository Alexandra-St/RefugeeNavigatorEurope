# Sweden Content Readiness — 27 October 2026

## Overall verdict

**NOT READY FOR HUMAN ACCEPTANCE** as a complete eight-topic batch. The source-and-claim packages are sufficiently detailed to support a human review, and the catalog has no structural source-binding failures found in this audit. Seven topics have explicit, material source-refresh items in their research notes, including current-law changes and future effective dates. `Finding a Job in Sweden` is ready for human acceptance with the caveats below. Do not promote statuses or comparison facts based on this structural audit.

This was a repository-only readiness review. Official pages were not reopened; the research matrices record checks dated 20 October 2026. No new legal conclusions are made here.

## Catalog and integrity checks

- The Sweden catalog has **8 articles** and **24 translations**: EN, UK, and RU for each article. All 24 translations are `NeedsReview`.
- The eight article IDs and stable identities match the Sweden integration tests. The tests also check nonempty localized title, summary, and content, and match the translations to the Sweden draft articles.
- There are **68 Sweden-owned OfficialSource records** across the eight articles (**60 distinct URLs**). The Sweden integration test verifies ownership, unique source IDs, HTTPS URLs, expected per-article source counts, and correspondence to URLs in the topic research packages. The catalog records have `LastCheckedAt` of 20 October 2026.
- No inline `[[source:...]]` tokens were found in the 24 Sweden translations, so there are no inline source tokens with missing or foreign ownership. Sources remain available through each article’s official-source list.
- There are **24 Sweden comparison facts**, all `Draft`: 23 available facts have 1–3 existing source IDs owned by the mapped Sweden article; the one unavailable fact, `social-child-benefits`, has no source IDs. All fact-to-article mappings are checked in the Sweden comparison integration test.
- `CountryVisibilityTests` and Sweden-specific comparison tests cover Production suppression and Development preview. They assert that Sweden is excluded in Production and that Draft facts are not rendered as accepted answers; Development retains Sweden preview. No Draft exposure defect was identified in the inspected tests.

## Topic decisions

| Topic / article | Article ID | Official sources | Research package assessment and remaining caveat | Readiness |
| --- | --- | ---: | --- | --- |
| [Temporary Protection: Applying and Maintaining Your Status](temporary-protection-application/SOURCES_AND_CLAIMS.md) | `d2ab273d-21b7-44e4-b408-f4269a8753d8` | 11 | Strong EU, Swedish agency, and statutory source set. The 2027 extension opportunity is not yet open in the reviewed evidence; military-document edge cases, registration guidance, and some third-country/stateless cohorts remain unsettled. Recheck current operational instructions before acceptance. | **Needs source refresh** |
| [Residence Options Beyond Temporary Protection](after-temporary-protection/SOURCES_AND_CLAIMS.md) | `befa7777-af70-4269-883c-8918961554dc` | 10 | Statute and Migration Agency guidance cover the listed routes. Recheck current permit requirements and the 2026 permanent-residence changes. The matrix explicitly leaves prior TPD-time counting for EU long-term-resident status unresolved; retain that boundary and do not infer an answer. | **Needs source refresh** |
| [Finding a Job](finding-a-job/SOURCES_AND_CLAIMS.md) | `a218013b-3a77-4ba2-829a-d724689837f2` | 7 | Official employment-service, qualification-evaluation, and regulated-profession sources cover the claims. No unresolved claim or dated legal change is identified in the matrix. Search tools and program conditions can change; preserve the no-job/no-placement/no-support guarantee caveat. | **Ready for human acceptance** |
| [Work Authorization and Employee Rights](employment-rights/SOURCES_AND_CLAIMS.md) | `55949af5-b4fe-404e-bcb7-6ebb37131bab` | 9 | Statutes and agency guidance are identified. The matrix explicitly calls for rechecking permit-specific work wording and current statutory texts; one employer work-environment page was last updated in 2024. Avoid asserting a minimum wage or universal collective-agreement coverage. | **Needs source refresh** |
| [Accessing Healthcare](healthcare/SOURCES_AND_CLAIMS.md) | `2234b91f-4021-40b2-b9d5-a029a6b533f2` | 6 | Official healthcare, registration, and legal sources are documented, including a conflicting stale English social-insurance summary. Registration guidance is date-sensitive: it includes a 12-month plus further-six-month test, a four-week process window, and a cutoff for certain permits issued from 5 September 2026. Reopen the current Skatteverket guidance before acceptance. | **Needs source refresh** |
| [Finding Accommodation](housing/SOURCES_AND_CLAIMS.md) | `e0f307e9-7991-4350-89f4-2f84a9047393` | 6 | Official accommodation, Reception Act, tenancy, and court sources are identified. The new Reception Act took effect on 1 October 2026. The matrix requires a current check of Migration Agency accommodation procedures and private-rental rules; do not promise placement, location, duration, or rent reimbursement. | **Needs source refresh** |
| [Education and Swedish-Language Learning](education/SOURCES_AND_CLAIMS.md) | `cac58c0c-ce69-41ac-877e-4fd124105b89` | 10 | The matrix has broad Skolverket, statute, University Admissions, and UHR coverage. SFI course structure changes on 1 January 2027, and the fee ordinance changed on 1 October 2026. Recheck current transition, permit-proof, and exception handling before acceptance; preserve municipal and admission caveats. | **Needs source refresh** |
| [Financial and Social Assistance](social-assistance/SOURCES_AND_CLAIMS.md) | `8b5cc53a-9933-4366-8421-d795966267ec` | 9 | Statutes and agency sources are documented, but the Reception Act took effect on 1 October 2026 and a Social Services Act amendment takes effect on 2 January 2027. Recheck the exact current rules and guidance before acceptance. Keep benefit-by-benefit limits, TPD exceptions, and the unresolved additional-payment boundary explicit. | **Needs source refresh** |

The source counts above reflect the current article-owned catalog records; the research files may list more distinct page entries because repeated URLs are consolidated in the catalog.

## Comparison facts and production gating

All 24 Sweden facts remain `Draft`; 23 are available and source-linked, while `social-child-benefits` is unavailable and source-less. An unavailable fact means the reviewed evidence did not establish the answer; it must not be restated as proof that the benefit does not exist. The Sweden-specific tests verify source ownership and the unavailable fact’s localized evidence-gap wording. Production hides Sweden through the country-visibility rule, while Development retains review preview. No status or catalog changes are part of this report.

## Blockers and next steps

Before accepting the full batch, refresh the seven topics marked **Needs source refresh**, focusing only on the dated or explicitly unresolved points named in the table. Human reviewers can proceed with the `Finding a Job` article and its three Draft facts as a separate editorial unit, while retaining its eligibility and no-guarantee caveats. Keep all other Sweden content out of production until its focused source review and human acceptance are complete.

No content correction is asserted by this audit: the identified issues are source freshness or explicitly bounded uncertainty, not a demonstrated false statement. No Sweden-specific test gap was found for catalog structure, ownership, HTTPS, Draft status, or production suppression. Those tests validate structure and gating, not legal correctness or editorial acceptance.
