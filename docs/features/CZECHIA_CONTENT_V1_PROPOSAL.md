# Czechia content V1 proposal

Status: **RESEARCH READY FOR HUMAN EDITORIAL REVIEW — NOT IMPLEMENTED**.

Research date: **19 October 2026 (UTC)**. Baseline inspected: `eba9b2fcbcb6abeb74139fbff9387741ad004071`. This document and the eight linked matrices are research, not production content, translations, legal certification or publication approval. Follow the [content workflow](../CONTENT_WORKFLOW.md); its historical UI descriptions are not a substitute for inspecting the current implementation.

## Country identity

| Field | Proposal |
| --- | --- |
| English / catalog Name | Czechia |
| Ukrainian | Чехія |
| Russian | Чехия |
| Slug | `czechia` |
| Proposed CountryId | `10000000-0000-0000-0000-000000000004` |
| Country resource key | `Country.Czechia` |
| Location-phrase resources | EN `in Czechia`; UK `у Чехії`; RU `в Чехии` under `Country.Czechia.In` |

The checked-in country catalog contains only Denmark/Germany/Poland, with IDs ending 001/002/003. Neither proposed ID nor slug conflicts. Six existing categories remain shared; no new category, schema, country page component or language is proposed.

## Eight-article architecture

Confirm the existing 2 temporary-protection / 2 employment / 1 healthcare / 1 housing / 1 education / 1 social-assistance distribution. Two English working titles are narrowed to avoid implying automatic eligibility or that the optional residence route begins only after TP ends. These are editorial proposals, not final multilingual articles.

| Article | Category | Working EN title / research matrix | User need | Primary authority | Major caveat |
| --- | --- | --- | --- | --- | --- |
| CZ-TP-01 | `temporary-protection` | [Temporary Protection in Czechia: Applying and Extending Your Status](../content-research/czechia-batch-2026-10-19/temporary-protection-application/SOURCES_AND_CLAIMS.md) | Application, evidence and extension stages | MV / EU / Act 65 | Age-specific evidence versus overbroad summary; EU/Czech dates differ. |
| CZ-TP-02 | `temporary-protection` | [Residence Options Beyond Temporary Protection in Czechia](../content-research/czechia-batch-2026-10-19/after-temporary-protection/SOURCES_AND_CLAIMS.md) | Optional alternative residence and current registration stage | MV / regulation 86/2025 | Cumulative household/income/reference-date tests; no guaranteed conversion. |
| CZ-EMP-01 | `employment` | [Finding a Job in Czechia](../content-research/czechia-batch-2026-10-19/finding-a-job/SOURCES_AND_CLAIMS.md) | Official vacancies, counselling and language/qualification help | Labour Office | Service access is not guaranteed funding, benefits or a job. |
| CZ-EMP-02 | `employment` | [Work Authorization and Employee Rights in Czechia](../content-research/czechia-batch-2026-10-19/employment-rights/SOURCES_AND_CLAIMS.md) | Work permission and basic worker protections | MPSV / SÚIP / Act 66 | 2026 wage and post-June-2025 termination rules are dated. |
| CZ-HEALTH-01 | `healthcare` | [Accessing Healthcare in Czechia](../content-research/czechia-batch-2026-10-19/healthcare/SOURCES_AND_CLAIMS.md) | Insurance payer, proof and access to care | Act 65 / public insurers / Ministry of Health | 90/150-day cohorts and payer transitions must remain distinct. |
| CZ-HOUSING-01 | `housing` | [Finding Accommodation in Czechia](../content-research/czechia-batch-2026-10-19/housing/SOURCES_AND_CLAIMS.md) | Initial accommodation, tenancy and support navigation | MV / MMR / Act 66 | 90-day accommodation limit; no universal municipal placement. |
| CZ-EDU-01 | `education` | [Education and Czech-Language Learning in Czechia](../content-research/czechia-batch-2026-10-19/education/SOURCES_AND_CLAIMS.md) | School enrolment, adaptation and later learning | MŠMT / DZS / Labour Office | Annual admission measures and individual school conditions. |
| CZ-SOC-01 | `social-assistance` | [Financial and Social Assistance in Czechia](../content-research/czechia-batch-2026-10-19/social-assistance/SOURCES_AND_CLAIMS.md) | HuD and urgent-assistance assessment | Act 66 / Act 111 / MPSV / Labour Office | Means testing, disability reform and mixed-age guidance; no blanket general benefit eligibility. |

## Research inventory and confidence

Only primary official sources support the proposed claims. Relevant page text/PDFs were actually opened and read; search snippets were used for discovery only. e-Sbírka legal texts and the dynamic vacancy service required browser inspection where extraction was insufficient. Dates below are actual research dates; exact individual times were not retained and must not be fabricated later as production `LastCheckedAt` times.

Counts include the source registers supporting/contextualising these eight matrices, deduplicated by canonical URL. They exclude rejected historical pages, inaccessible attachments and search-only results. Reuse makes the sum of per-article references larger than the unique URL count. No official-source GUIDs are assigned in research.

| Article | Official URLs referenced | Atomic claims | Confidence |
| --- | ---: | ---: | --- |
| CZ-TP-01 | 5 | 16 | 8 Confirmed; 8 Conditional |
| CZ-TP-02 | 10 | 19 | 3 Confirmed; 16 Conditional |
| CZ-EMP-01 | 7 | 8 | 4 Confirmed; 4 Conditional |
| CZ-EMP-02 | 9 | 11 | 7 Confirmed; 4 Conditional |
| CZ-HEALTH-01 | 10 | 11 | 5 Confirmed; 6 Conditional |
| CZ-HOUSING-01 | 6 | 8 | 2 Confirmed; 6 Conditional |
| CZ-EDU-01 | 4 | 9 | 1 Confirmed; 8 Conditional |
| CZ-SOC-01 | 6 | 11 | 1 Confirmed; 10 Conditional |

**42 unique official source URLs; 57 article/source references; 93 atomic claims.** No unresolved material contradiction is written as settled fact. Conditional confidence is not individual eligibility or a production status. Deferred unsupported details below are excluded, not silently declared confirmed.

## Critical dates and transitions

| Date / interval | Meaning and cohort | Evidence |
| --- | --- | --- |
| 24 February 2022 | Pre-war Ukraine residence/protection reference for relevant TP cohorts; not a universal nationality-only test. | TP-S01 |
| 4 August 2026 | Corrected existing-holder cut-off; continuous same-Member-State protection matters. | TP-S04, TP-S02 |
| 5 August 2026 | New military-obligation condition for covered first/repeat/family TP applications; Czech 18–22 / 23–60 evidence split. Not routine extension. | TP-S02, TP-S04 |
| 15 March / 30 September 2026 | Completed Czech online/sticker extension stages. Missing stages must not be described as still open. | TP-S01, TP-S05 |
| 31 March 2027 | Validity reached by the completed 2026 Czech cycle; new 2026 grants described by MV also use this date. | TP-S01 |
| 4 March 2028 | EU temporary-protection extension endpoint; not the expiry printed on every Czech document. | TP-S03, TP-S04 |
| 15 March / 30 September 2027 | Annual statutory registration/sticker cut-offs for the next applicable cycle; portal opening/booking details still require publication-time confirmation. | TP-S05 §§7b–7c; TP-S02 |
| 31 March 2028 | Czech endpoint after completing the next extension process; not already granted automatically to every holder. | TP-S02, TP-S05 |
| 1–30 April 2026 | ZDP expression-of-interest stage, now closed. | RES-S04, RES-S07 |
| 1 April 2026 / 31 March 2026 | ZDP TP-continuity reference / insurance and relevant school-condition reference respectively. | RES-S02, RES-S07 |
| Tax year 2025; filing by 4 May 2026 | ZDP income reference and tax-return filing condition; not a newly open filing route in October. | RES-S07 |
| 1 October 2025–31 March 2026 | No-HuD reference period for the 2026 ZDP round. | RES-S02, RES-S07 |
| 1 October–31 December 2026 | ZDP registration for the assessed expressed-interest cohort; launch at 08:00 on 1 October. | RES-S06, RES-S07 |
| Residence-card collection | Acquisition of five-year ZDP; expression of interest is not this event. | TP-S05 §7o |
| First TP through 10 February 2025 / from 11 February 2025 | Initial state-paid insurance lasts 150 / 90 days respectively; repeat grants do not reset it. | HEALTH-S01 |
| After the initial insurance period | Payer depends on age, employment and other statutory category; under-18/65+ and ZDP have distinct rules. | HEALTH-S01/02/04, TP-S05 |
| Within eight days | Report relevant insurance-payer/status change to insurer. | HEALTH-S01/04 |
| From 1 September 2024 | Free state humanitarian accommodation capped at 90 days from first TP, including vulnerable people. | HOUSE-S01 |
| Month after the month in which 150 days elapse | HuD adult reduction, unless an exception applies; not the insurance/accommodation clock. | SOC-S01 §6b |
| 1 July 2025 assessment reform / Act 66 consolidation from 1 October 2025 | Degree-based self-sufficiency assessment; old flat disability uplifts must not be copied as current. | SOC-S01, SOC-S02 |
| 1 April 2026 | REGZEC employer reporting route replaces prior forms. | WORK-S01 |
| 1 January–31 December 2026 | CZK 22,400 monthly / 134.40 hourly gross minimum for the stated 40-hour reference week. | WORK-S03 |
| From 1 June 2025 | Amended notice timing; older leaflet timing not current for new notices. | WORK-S06 |
| 2026/2027 school year; 90 days after TP | Year-specific school cohorts; representative’s admission request satisfies the stated §2b duty, attendance may follow later. | EDU-S01 |

## Source conflicts and publication boundaries

1. **Military evidence — explicitly resolved for research by owner ruling, 19 October 2026.** The undated main-page eligibility summary overstates full exemption for all 18–60. Prefer the dedicated 4 August article plus the main page’s detailed evidence section: 18–22 Reserv+ registration/paper eVOD; 23–60 paper eVOD demonstrating full removal/exemption. Applies to persons subject to Ukrainian military obligations, not “all men”. EU Decision/corrigendum provide the EU rule/date/existing-holder exception, not the Czech age-specific evidence distinction. Preserve disclosure of the summary inconsistency.
2. **Extension dates are different legal/administrative stages, not a silently resolved conflict.** EU 4 March 2028; Czech completed cycle 31 March 2027; next successful Czech process 31 March 2028. Current statutes establish an annual mechanism, while opening/appointment details still need the next operational announcement. No individual permit-validity inference.
3. **Insurance and accommodation historical pages.** Old 150-day accommodation/vulnerable exemptions and broad early-war state-insurance statements are not current new-arrival rules. Current legislation/dated guidance/insurers control; keep historical scope visible in notes.
4. **Benefits guidance mixes periods.** Current degree-based rules in Act 66 take priority over old flat disability uplift text still present on MPSV’s HuD page. General Czech subsistence/DSSP reform is not automatically a HuD reform. No imported general child/housing benefit entitlement.
5. **Old worker leaflet.** Use only its stable contract/working-time/inspection guidance. Current minimum-wage and termination pages replace its older time-sensitive details.
6. **Education attachment access.** No readable verification was obtained for MŠMT attachment 65634_1_1; older secondary-language attachment 63306_1_1/download returned not found. Exclude exact annual examination concessions/secondary language-support hours until the applicable readable measure is checked. The accessible 23-page methodology supports the narrower school-navigation claims. No inaccessible material source is needed to establish the core claims retained here.

No authority or tester has been contacted. No discrepancy is used to decide an individual case. Remaining non-core evidence gaps are exclusions from proposed copy, not a blanket disclaimer concealing unsupported claims.

## Architecture impact — inspected baseline

| Area | Existing support | Later work required; no change made now |
| --- | --- | --- |
| Country/catalog and models | Country/InformationEntry/Translation/OfficialSource relations already use stable GUIDs; generic JSON loading and relationship/uniqueness validation. | Add the proposed country, eight entries and 24 reviewed translations in a separately authorized integration. No schema change needed. |
| Country/category routes | `CountryOverview.razor` uses `/countries/{CountrySlug}`; `Category.razor` uses `/countries/{CountrySlug}/categories/{CategorySlug}` with catalog lookup and 404 handling. | Existing six categories suffice. Add no duplicate Czech country page. Verify unknown slugs and empty states in later integration. |
| Home/localization | Country cards loop over catalog; slug-based resolver yields `Country.Czechia`; location phrases use `.In`. | Add name AND `.In` resources in uk/en/ru, plus Czech flag in Home’s existing slug switch. Startup name validation alone does not currently validate `.In`; test explicitly. |
| Search | Existing ArticleSearch and country filters use catalog relationships/translations. | Add content/resources and focused fourth-country tests; do not assume availability equals editorial approval. No search schema change identified. |
| V1 comparison | `CountryComparisonTopicMap` has eight topics with explicit Denmark/Germany/Poland article mappings and an exact-three count check. | **Not data-only today:** extend mappings/helper and validation to the fourth country; verify selectors cannot select an unmapped article. |
| V2 comparison | `JsonComparisonFactProvider` contains a three-country array and exact coverage check; validates topic/article/category/source ownership. | Extend country coverage safely, add 24 Czech answers in all three languages with real article-owned source IDs. Preserve Draft/Approved gate and unavailable-answer rules. No new fact schema. |
| Language switching | Existing uk/en/ru cookie/return URL and full navigation for interactive guides. | Reuse unchanged; test Czech page names and return paths. No language-dependent URL slugs. |
| Future guide | Generic `/guides/{CountrySlug}/{GuideSlug}` route and guide catalog/source/article validation. | A later Czech guide can reuse schema and Draft visibility; no guide content or new guide catalog record now. |
| Source ownership | OfficialSource has one `InformationEntryId`; comparison/guide validators require the cited source to belong to a linked article. | Reused URLs need deliberate article-owned source records where cited by multiple articles; identical URL does not imply duplicate evidence or a new check. Preserve provenance and actual check date. |

No runtime readiness claim is made: comparison’s hardcoded country assumptions must be removed/extended in a separate implementation phase before exposing Czechia in comparison. Future tests should find count assumptions without weakening existing legal/content assertions.

## Proposed source ownership and deliberate reuse

Each unique page has a primary editorial owner below. “Other matrices” records reuse. Production currently cannot attach one OfficialSource record to multiple entries. If each reused article cites that page, create a separate source record owned by that article; do not reuse an unrelated article’s source GUID. No source GUIDs are created here. These are planned URL references, not catalog insertions or newly performed checks.

| Source | Primary owner | Other matrices reusing it |
| --- | --- | --- |
| [TP-S01](https://ipc.gov.cz/docasna-ochrana/) | CZ-TP-01 | CZ-TP-02, CZ-HOUSING-01 |
| [TP-S02](https://ipc.gov.cz/prodlouzeni-docasne-ochrany-do-roku-2028-a-uprava-podminek-pro-jeji-udeleni/) | CZ-TP-01 | None |
| [TP-S03](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX:32026D1912) | CZ-TP-01 | None |
| [TP-S04](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX:32026D1912R(01)) | CZ-TP-01 | None |
| [TP-S05](https://e-sbirka.gov.cz/sb/2022/65) | CZ-TP-01 | CZ-TP-02, CZ-HEALTH-01 |
| [RES-S01](https://ipc.gov.cz/zvlastni-dlouhodoby-pobyt/) | CZ-TP-02 | None |
| [RES-S02](https://ipc.gov.cz/zvlastni-dlouhodoby-pobyt/kdo-muze-ziskat-zvlastni-dlouhodoby-pobyt/) | CZ-TP-02 | None |
| [RES-S03](https://ipc.gov.cz/zvlastni-dlouhodoby-pobyt/obecne-informace-ke-zvlastnimu-dlouhodobemu-pobytu/) | CZ-TP-02 | CZ-EMP-02, CZ-HEALTH-01, CZ-SOC-01 |
| [RES-S04](https://ipc.gov.cz/zvlastni-dlouhodoby-pobyt/vyjadreni-zajmu-o-zvlastni-dlouhodoby-pobyt-v-ramci-spolecne-domacnosti/) | CZ-TP-02 | None |
| [RES-S05](https://ipc.gov.cz/zvlastni-dlouhodoby-pobyt/registrace-k-ziskani-zvlastniho-dlouhodobeho-pobytu-navsteva-pracoviste-ministerstva-vnitra/) | CZ-TP-02 | None |
| [RES-S06](https://ipc.gov.cz/spusteni-registrace-ke-zvlastnimu-dlouhodobemu-pobytu/) | CZ-TP-02 | None |
| [RES-S07](https://e-sbirka.gov.cz/sb/2025/86) | CZ-TP-02 | None |
| [EMP-S01](https://up.gov.cz/pomoc-pro-obcany-ukrajiny-a-jejich-zamestnavate-1) | CZ-EMP-01 | CZ-EMP-02, CZ-HOUSING-01, CZ-SOC-01 |
| [EMP-S02](https://up.gov.cz/sluzby-pro-cizince-ze-tretich-zemi) | CZ-EMP-01 | None |
| [EMP-S03](https://up.gov.cz/zmeny-v-poskytovani-rekvalifikacnich-kurzu-ceskeho-jazyka-pro-cizince-od-roku-2026) | CZ-EMP-01 | CZ-EDU-01 |
| [EMP-S04](https://up.gov.cz/czechup-jazyk-kvalifikace-a-zamestnani-pro-cizince) | CZ-EMP-01 | None |
| [EMP-S05](https://up.gov.cz/menite-praci) | CZ-EMP-01 | None |
| [WORK-S01](https://mpsv.gov.cz/informace-o-nastupu-u-obcana-eu-ehp-a-svycarska-nebo-jeho-rodinneho-prislusnika-nebo-cizince-ktery-nepotrebuje-pracovni-opravneni-do-zamestnani-k-vysl?inheritRedirect=true) | CZ-EMP-02 | None |
| [WORK-S02](https://mpsv.gov.cz/evidence-cizincu-a-eu) | CZ-EMP-02 | None |
| [WORK-S03](https://mpsv.gov.cz/minimalni-mzda) | CZ-EMP-02 | None |
| [WORK-S04](https://suip.gov.cz/documents/20142/43720/pracovni%2Bpodminky_cizinci_cs-2023_01.pdf/efc26efc-baf1-a51c-ce96-7b15ef018f77) | CZ-EMP-02 | None |
| [WORK-S05](https://suip.gov.cz/a3b-minimalni-delka-dovolene-za-kalendarni-rok-nebo-jeji-pomerna-cast) | CZ-EMP-02 | None |
| [WORK-S06](https://ppropo.mpsv.cz/VI21Obecnepodminkyplatnostivypov) | CZ-EMP-02 | None |
| [HEALTH-S01](https://cz.pomocukrajine.vzp.cz/oznameni-platce-pojistneho/) | CZ-HEALTH-01 | None |
| [HEALTH-S02](https://www.ozp.cz/ua-cz) | CZ-HEALTH-01 | None |
| [HEALTH-S03](https://cz.pomocukrajine.vzp.cz/registrace/) | CZ-HEALTH-01 | None |
| [HEALTH-S04](https://cz.pomocukrajine.vzp.cz/prechod-osob-z-viza-docasne-ochrany-na-dlouhodoby-pobyt-na-uzemi-cr/) | CZ-HEALTH-01 | None |
| [HEALTH-S05](https://cz.pomocukrajine.vzp.cz/prodlouzeni-zdravotniho-pojisteni-po-prodlouzeni-docasne-ochrany/) | CZ-HEALTH-01 | None |
| [HEALTH-S06](https://www.nzip.cz/clanek/205-zdravotnicka-zachranna-sluzba) | CZ-HEALTH-01 | None |
| [HEALTH-S07](https://www.nzip.cz/clanek/1073-poskytovatele-zdravotnich-sluzeb) | CZ-HEALTH-01 | None |
| [HEALTH-S08](https://mzd.gov.cz/tiskove-centrum-mz/za-co-u-lekare-platit-muzete-a-za-co-ne-ministerstvo-zdravotnictvi-vyjasnuje-pravidla-pro-pacienty/) | CZ-HEALTH-01 | None |
| [HOUSE-S01](https://ipc.gov.cz/docasna-ochrana/statni-humanitarni-ubytovani-2/) | CZ-HOUSING-01 | None |
| [HOUSE-S02](https://mmr.gov.cz/cs/ministerstvo/bytova-politika/najemni-vztahy/najemni-smlouva) | CZ-HOUSING-01 | None |
| [HOUSE-S03](https://mmr.gov.cz/cs/ministerstvo/bytova-politika/najemni-vztahy/platby-spojene-s-najmem-bytu) | CZ-HOUSING-01 | None |
| [EDU-S01](https://edu.gov.cz/wp-content/uploads/2025/12/Metodika-lex_UKR_12_2025.pdf) | CZ-EDU-01 | None |
| [EDU-S02](https://msmt.gov.cz/vzdelavani/uznavani-zahranicniho-a-ceskeho-studia/nostrifikace) | CZ-EDU-01 | CZ-EMP-01 |
| [EDU-S03](https://studyin.gov.cz/plan-your-studies/find-your-programme) | CZ-EDU-01 | None |
| [SOC-S01](https://e-sbirka.gov.cz/sb/2022/66) | CZ-SOC-01 | CZ-TP-02, CZ-EMP-02, CZ-HOUSING-01 |
| [SOC-S02](https://mpsv.gov.cz/humanitarni-davka) | CZ-SOC-01 | None |
| [SOC-S03](https://e-sbirka.gov.cz/sb/2006/111) | CZ-SOC-01 | None |
| [SOC-S04](https://mpsv.gov.cz/up-cz/mimoradna-okamzita-pomoc) | CZ-SOC-01 | None |
| [EMP-S06](https://up.gov.cz/volna-mista-v-cr) | CZ-EMP-01 | None |

Metadata, exact title, authority, language read, check date, claim IDs and access limitations are in each owning matrix. Preserve an actual date-only research check as date-only; a future integrator must re-open sources to obtain genuine production check timestamps if required, rather than invent midnight/time-of-day records.

## Terminology glossary

| Czech term | Editorial treatment |
| --- | --- |
| dočasná ochrana | Temporary protection; UK тимчасовий захист; RU временная защита. Distinguish status from a visa sticker. |
| zvláštní dlouhodobý pobyt (ZDP) | Special long-term residence; retain official Czech name on first use. Not permanent residence, not automatic replacement for TP. |
| vyjádření zájmu / registrace | Expression of interest / registration: separate dated stages. |
| Úřad práce ČR (ÚP ČR) | Labour Office of the Czech Republic; use the official institutional name alongside translation. |
| uchazeč o zaměstnání / zájemce o zaměstnání | Two different registration categories; do not translate both as identical benefit-entitled “unemployed”. |
| KACPU | Krajské asistenční centrum pomoci Ukrajině; application/accommodation routing depends on the case. |
| humanitární dávka (HuD) | Humanitarian benefit; assessed financial support, not a universal refugee allowance. |
| mimořádná okamžitá pomoc (MOP) | Extraordinary immediate assistance; individual statutory situation/person assessment. |
| omezení soběstačnosti | Assessed limitation of self-sufficiency, not simply any self-declared disability. |
| veřejné zdravotní pojištění | Public health insurance; distinguish participation, payer and covered services. |
| nostrifikace | Recognition of foreign education; not automatic professional licensing. |
| Reserv+ / eVOD | Preserve official names; paper military-record evidence under the specified Czech administrative rule. Never request it through the application. |

## Future guide candidate — no guide designed

CZ-TP-01 is the primary basis for a possible `/guides/czechia/temporary-protection`. CZ-TP-02 can provide tightly bounded signposts about optional ZDP, separate from TP extension; Housing/Healthcare/Education can support ancillary service links. A later proposal must choose supported scenarios/claim IDs and retain the military cohort/continuous-holder distinctions, next-cycle opening caveat and individual-authority assessment. No scenarios, steps, article/source GUIDs or eligibility branches are approved in this phase.

## Implementation sequence and open questions

1. Human review of these matrices and selected source passages; retain the explicit military-summary inconsistency. No publication approval is implied.
2. Draft eight articles, then translate uk/en/ru with matched legal meaning, dated thresholds, six existing categories and stable logical IDs. Recheck time-sensitive source pages before integration. Do not collect personal evidence.
3. Separately authorize the small fourth-country comparison mapping/validation extension and its regression tests; no need for a new architecture or database.
4. Add catalog/resources/flag/content/source records with a reviewed diff and actual check/edit timestamps. Reuse existing formatting (headings, paragraphs, unordered lists; no inline Markdown emphasis). Keep candidates unapproved until human acceptance; never deploy based on startup validation alone.
5. Prepare 24 narrowly supported Czech comparison answers, article-owned source links and matched translations; unavailable answers must describe an evidence gap, not an unsupported assertion of no entitlement.
6. Later visual/localization/accessibility review, source-freshness check, editorial approvals and separately authorized release. Future guide design is another task.

Before operational article wording: confirm opening/booking instructions for the extension to 31 March 2028; confirm ZDP deadlines still apply at publication; check 2027 wage changes if publishing after 2026; obtain readable annual secondary-admissions measures only if adding exact exam concessions. Optional topics not supported here (universal pregnancy payer category, detailed medicine reimbursements, ordinary family-benefit eligibility, fixed housing allowance ceilings) must remain excluded until specifically evidenced. These are scoped follow-ups, not unresolved contradictions in retained core claims. No request to an authority is required or authorized here.

## Research completeness gate

- Eight matrices and this architecture proposal prepared; every retained material claim cites an opened official source and a passage/section.
- 2026–2028 dates and applicable cohorts are explicit; EU extension is not an individual permit extension.
- Military evidence uses the owner-resolved hierarchy, with the overbroad main summary disclosed.
- ZDP interest, registration and card acquisition are separate; reference years and household conditions retained.
- Insurance participation/payment, housing limits and HuD clocks are separate; historical/mixed-age guidance identified.
- No unresolved contradiction or inaccessible-source-only claim is presented as settled copy.
- All six comparison categories covered below; future guide candidate identified without designing it.
- No full translations, runtime code, catalogs, statuses or deployment changes.
- Documentation validation: relative links and `git diff --check`; no local .NET build required for this research-only phase. CI outcome is reported separately after commit.

## Comparison V2 candidate coverage

Research candidates for the **existing 24 aspect IDs**, not approved answers. “Supported narrowly” means the conditions in the cited claims must remain in the eventual answer. No general entitlement is inferred from an official portal’s menu.

| Category / topic | Existing aspect ID | Candidate claim IDs | Evidence readiness / limitation |
| --- | --- | --- | --- |
| Temporary protection / application | `protection-scheme` | CZ-TP01-01–03 | Supported narrowly: Czech/EU scheme and defined cohorts. |
| Temporary protection / application | `protection-application` | CZ-TP01-04–10 | Supported narrowly: application route and conditional military evidence; summary discrepancy disclosed. |
| Temporary protection / application | `protection-dates` | CZ-TP01-01, 11–14 | Supported with stage dates; next registration opening not yet confirmed. |
| Temporary protection / residence | `residence-routes` | CZ-TP02-01–02, 14, 19 | Supported narrowly: optional ZDP; ordinary-route exceptions only as signposts. |
| Temporary protection / residence | `residence-conditions` | CZ-TP02-05–12, 17–18 | Supported with income year/household/reference dates; not a calculator. |
| Temporary protection / residence | `residence-filing` | CZ-TP02-03–04, 13–14 | Supported for 2026 round; April interest is closed. |
| Employment / finding work | `employment-vacancies` | CZ-EMP01-04 | Supported: actual official search service loaded; no job outcome promised. |
| Employment / finding work | `employment-institutions` | CZ-EMP01-01–02 | Supported: ÚP, third-country specialists and EURES. |
| Employment / finding work | `employment-assistance-conditions` | CZ-EMP01-01, 03, 05–06 | Supported narrowly: registration/approval/funding conditions. |
| Employment / rights | `work-permission` | CZ-EMP02-01–02 | Supported: valid Czech TP or acquired ZDP, not nationality alone. |
| Employment / rights | `work-employer-duties` | CZ-EMP02-03–04 | Supported: REGZEC and document retention; no unsupported deadline extension. |
| Employment / rights | `work-minimum-pay` | CZ-EMP02-07 | Supported for calendar 2026 only. |
| Healthcare | `healthcare-access` | CZ-HEALTH01-01–04, 10–11 | Supported narrowly: cohort/payer/status distinctions. |
| Healthcare | `healthcare-proof` | CZ-HEALTH01-05–06 | Supported: insurer registration/evidence, reporting; sticker not insurance card. |
| Healthcare | `healthcare-urgent` | CZ-HEALTH01-07–08 | Supported: 155/112 for serious emergencies; ordinary local services otherwise. |
| Housing | `housing-initial` | CZ-HOUSING01-01–03 | Supported: 90-day first-grant limit, centralized routing. |
| Housing | `housing-private-rental` | CZ-HOUSING01-04–05 | Supported within residential tenancy; not every accommodation contract. |
| Housing | `housing-financial-support` | CZ-HOUSING01-07–08; CZ-SOC01-01–05 | Supported narrowly: HuD component/assessment, not universal housing allowance. |
| Education | `education-school-enrolment` | CZ-EDU01-01–03 | Supported: cohort, 90-day request and capacity escalation. |
| Education | `education-pupil-language` | CZ-EDU01-04, 08 | Supported: temporary adaptation plus ordinary preparation; exact secondary hours not evidenced. |
| Education | `education-higher-admission` | CZ-EDU01-06–07 | Supported: university-specific admission/recognition and fee caveats. |
| Social assistance | `social-subsistence` | CZ-SOC01-01–06, 09 | Supported: means/household tests and dated HuD bases. |
| Social assistance | `social-application` | CZ-SOC01-07–08, 10 | Supported: official route and conditional urgent-assistance assessment. |
| Social assistance | `social-child-benefits` | CZ-SOC01-02–03, 11 | Supported narrowly: HuD child component and status change; ordinary child/family benefits not established here. |

All 24 aspects have a supported, bounded candidate response. Evidence gaps remain for broader propositions: universal general child benefit, guaranteed secondary exam waiver, uniform school language hours, fixed future wages, or an already-open 2028 extension portal. Do not fill those gaps by inference. Human editorial acceptance and fresh publication-time checks remain mandatory.
