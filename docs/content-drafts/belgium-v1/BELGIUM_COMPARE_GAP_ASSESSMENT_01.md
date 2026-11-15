# Belgium Compare Gap Assessment 01

**Assessment date:** 14 November 2026
**Scope:** existing Belgium Core drafts, research packages, Belgium-only catalog/source/fact records, and the Belgium integration test. No new web research was performed.

> **Premise correction:** Belgium has eight Core article records in the development catalog, but it is **not Production-visible**. The integration test explicitly requires Belgium to be hidden in Production and all 24 language records to remain `NeedsReview`. This is therefore a readiness assessment, not a claim that Belgium Core has been released.

## Current Production Core

| Item | Observed state |
|---|---|
| Country ID | `10000000-0000-0000-0000-000000000017` |
| Production visibility | **Hidden / not released.** Production home, search, country routes and Compare exclude Belgium; it is available in Development. |
| Core article records | 8, all present in catalog and Belgium drafts; 8/8 are development-only. |
| EN / UK / RU translations | 24 total (3 per article); all 24 `NeedsReview` (0 `Verified`, 0 `Outdated`); last updated 2026-11-11. |
| Official source records | 146 Belgium-owned records, all attached to the eight existing article IDs. |
| Source URLs | 132 distinct URLs (14 repeated URL occurrences across records). The per-article source bindings and direct HTTPS links are in the corresponding Core drafts; production UUIDs are indexed below. |
| Categories | temporary-protection (2); employment (2); healthcare (1); housing (1); education (1); social-assistance (1). |
| Compare | Belgium absent from `CountryComparisonTopicMap`; Belgium facts: **0**. Current Compare remains **16 countries / 8 topics / 24 aspects / 384 facts**. |

### Article inventory

| Article ID | Production information-entry ID | Category | Production state |
|---|---|---|---|
| `BE-TP-01` | `aef6ec65-f8ed-5054-bafb-c465e790dd57` | temporary-protection | Development-only; translations `NeedsReview` |
| `BE-TP-02` | `54c6f95f-589e-5901-beae-990bed994160` | temporary-protection | Development-only; translations `NeedsReview` |
| `BE-EMP-01` | `f2c1526a-a218-5bf0-ab2a-9167dbec0c26` | employment | Development-only; translations `NeedsReview` |
| `BE-EMP-02` | `55fa79e5-89a2-518b-90b2-85a2735f171b` | employment | Development-only; translations `NeedsReview` |
| `BE-HEALTH-01` | `673316a9-6ee9-50d6-9f66-e19659aeff0e` | healthcare | Development-only; translations `NeedsReview` |
| `BE-HOUSING-01` | `3646ae19-215c-5f0e-9075-85ef5bae88f7` | housing | Development-only; translations `NeedsReview` |
| `BE-EDU-01` | `ac80dcbd-4289-5c77-8340-f9e4226a47f6` | education | Development-only; translations `NeedsReview` |
| `BE-SOC-01` | `20eef3c3-fcc7-5b5b-b672-695f29faad61` | social-assistance | Development-only; translations `NeedsReview` |

## Topic owner matrix

| Topic | Existing Belgium article | Article ID | Category | Production status | Can own Compare topic? |
|---|---|---|---|---|---|
| Temporary Protection Application | Applying for temporary protection in Belgium | `BE-TP-01` | temporary-protection | Dev-only; translations `NeedsReview` | YES — record exists; release gate remains |
| Residence After Protection | Residence options during or after temporary protection in Belgium | `BE-TP-02` | temporary-protection | Dev-only; translations `NeedsReview` | YES — record exists; aspect `residence-filing` still blocked |
| Finding Employment | Finding a job in Belgium | `BE-EMP-01` | employment | Dev-only; translations `NeedsReview` | YES — record exists; release gate remains |
| Work Authorization and Rights | Employment rights in Belgium under temporary protection | `BE-EMP-02` | employment | Dev-only; translations `NeedsReview` | YES — record exists; release gate remains |
| Healthcare | Healthcare in Belgium under temporary protection | `BE-HEALTH-01` | healthcare | Dev-only; translations `NeedsReview` | YES — record exists; release gate remains |
| Housing | Housing in Belgium under temporary protection | `BE-HOUSING-01` | housing | Dev-only; translations `NeedsReview` | YES — record exists; release gate remains |
| Education and Language | Education in Belgium under temporary protection | `BE-EDU-01` | education | Dev-only; translations `NeedsReview` | YES — record exists; release gate remains |
| Social Assistance | Social assistance in Belgium under temporary protection | `BE-SOC-01` | social-assistance | Dev-only; translations `NeedsReview` | YES — record exists; release gate remains |

**Owners present: 8/8. Missing owner topics: none.** The article IDs above are existing catalog IDs; this assessment creates no new IDs.

## 24-aspect matrix

Research package readiness is taken from `docs/content-research/belgium-batch-2026-11-11/RESEARCH_READINESS.md` and the corresponding focused `SOURCES_AND_CLAIMS.md`. Existing Production source counts below are per owner article; source IDs and direct official URLs are already bound in that article's draft and production JSON. Research-package source aliases (such as `BE-TP-S01`) are not production UUIDs.

| Aspect | Owner | Existing research / source coverage | Safe assertion for a future fact | Required qualification / regional scope | Status |
|---|---|---|---|---|---|
| `protection-scheme` | `BE-TP-01` | `temporary-protection-application`; 10 production records / 10 URLs | Belgium applies the EU temporary-protection framework to defined eligible groups; nationality, prior residence/protection, family link and evidence conditions matter. | Do not imply all Ukrainians or all third-country nationals qualify. Keep the applicable cohort and post-5-August-2026 evidence/family qualifications. No automatic ordinary residence conversion. | READY WITH QUALIFICATION |
| `protection-application` | `BE-TP-01` | same package; 10 / 10 | The Immigration Office registration centre is at Rue Belliard 68, Brussels; current source says working days at 08:30. A grant is followed by municipal residence processing. | Appointment/walk-in/pre-registration, full accepted document list, lost-document route, children/interpreters and no-address route are unresolved. EU-wide guidance does not establish Belgian intake practice. | RESEARCH REQUIRED |
| `protection-dates` | `BE-TP-01` | same package; 10 / 10 | The EU scheme is extended through 4 March 2028. | That date is not evidence of an individual Belgian A-card expiry or automatic extension. Belgian A-card renewal/replacement, holder action and interim proof for 2027–28 remain unresolved. | RESEARCH REQUIRED |
| `residence-routes` | `BE-TP-02` | `after-temporary-protection`; 16 production records / 14 URLs | Work/single permit, study, family reunification and self-employment/professional-card routes exist subject to their own conditions. | These are separate residence routes, not automatic conversions from TP; retain route-specific Blue Card and eligibility boundaries. | READY WITH QUALIFICATION |
| `residence-conditions` | `BE-TP-02` | same package; 16 / 14 | Each ordinary route has distinct substantive and residence-duration conditions; TP time is excluded from EU long-term-resident qualifying time. | Do not imply any residence period automatically qualifies for permanent/long-term status. National establishment-counting during TP remains unresolved. | READY WITH QUALIFICATION |
| `residence-filing` | `BE-TP-02` | same package; 16 / 14 | Ordinary routes have route-specific filing procedures; those procedures can be described only within their documented eligibility and filing conditions. | No universal in-country filing instruction for TP holders; pending lawful-stay/work rights at TP end, TP cessation/reactivation and ordinary-grant effects are unresolved by route. | RESEARCH REQUIRED |
| `employment-vacancies` | `BE-EMP-01` | `finding-a-job`; 17 production records / 16 URLs | Public vacancy channels include VDAB, Actiris, Le Forem, ADG/Arbeitsamt and EURES. | Non-exhaustive channels; regional residence/language and each vacancy's own application requirements apply. | READY WITH QUALIFICATION |
| `employment-institutions` | `BE-EMP-01` | same package; 17 / 16 | The four regional employment services are VDAB, Actiris, Le Forem and Arbeitsamt/ADG. | Service coverage and documents vary by region; distinguish a portal/account from formal jobseeker registration and work authorization. | READY WITH QUALIFICATION |
| `employment-assistance-conditions` | `BE-EMP-01` | same package; 17 / 16 | Regional services provide job-search or integration support through their own processes. | Eligibility, coaching, language support, training and funding are not automatic; do not promise a particular service or benefit. | READY WITH QUALIFICATION |
| `work-permission` | `BE-EMP-02` | `employment-rights`; 22 production records / 22 URLs | A TP holder may work as an employee after grant when holding the A card or a qualifying TP-related Annex 15 pending the card. | Pending TP application and federal attestation alone are not established as sufficient work proof. Self-employment/professional card is a separate question. | READY WITH QUALIFICATION |
| `work-employer-duties` | `BE-EMP-02` | same package; 22 / 22 | Employers have verification, notification, contract and payroll obligations when hiring. | Preserve federal/regional and sector-specific duties; do not generalize an employee rule to self-employment or every profession. | READY WITH QUALIFICATION |
| `work-minimum-pay` | `BE-EMP-02` | same package; 22 / 22 | The research supports the RMMMG of €2,189.81 gross/month from 1 April 2026 under the covered CCT 43 framework. | This is not a universal hourly minimum; scope is covered adult workers and applicable CCT rules, with sectoral minima potentially higher. | READY WITH QUALIFICATION |
| `healthcare-access` | `BE-HEALTH-01` | `healthcare`; 13 production records / 13 URLs | TP status does not itself complete health-insurance affiliation; insurance-based care, CPAS medical aid and urgent care are distinct routes. | Do not equate eligibility with completed registration, reimbursement or free treatment. Individual affiliation and cost-sharing conditions apply. | READY WITH QUALIFICATION |
| `healthcare-proof` | `BE-HEALTH-01` | same package; 13 / 13 | INAMI guidance supports using TP-attestation evidence for resident affiliation before the A card. | The CAAMI checklist and operational workflow are not fully aligned in the research; say to confirm accepted documents with the mutuality/CAAMI. | READY WITH QUALIFICATION |
| `healthcare-urgent` | `BE-HEALTH-01` | same package; 13 / 13 | 112 is the emergency number; 1733 is the out-of-hours GP route where available. | Do not equate emergency access with free care or ordinary insured reimbursement. Keep Belgian AMU/DMH eligibility and payment boundaries distinct. | READY WITH QUALIFICATION |
| `housing-initial` | `BE-HOUSING-01` | `housing`; 23 production records / 23 URLs | Fedasil/referral and regional Ukraine accommodation channels exist. | Availability and placement depend on assessment, capacity and local/regional arrangements; no bed, duration or permanent home is guaranteed. | READY WITH QUALIFICATION |
| `housing-private-rental` | `BE-HOUSING-01` | same package; 23 / 23 | Private renting is governed regionally; the research documents region-specific lease/deposit rules. | State the relevant region only: Flanders, Brussels, Wallonia and DG rules differ. A deposit rule is not an advance-rent cap; landlord acceptance is not assured. | READY WITH QUALIFICATION |
| `housing-financial-support` | `BE-HOUSING-01` | same package; 23 / 23 | CPAS support, regional rent aid and social housing are separate possible application routes. | Assessment, residence, income and regional conditions apply; neither social housing nor rent support is automatic or guaranteed. | READY WITH QUALIFICATION |
| `education-school-enrolment` | `BE-EDU-01` | `education`; 25 production records / 24 URLs | School access is governed by the relevant Community system; school enrolment generally need not wait for an A card. | Rules and administration differ across Flemish Community, French Community/FWB and German-speaking Community. A place, preferred school or timing is not guaranteed. | READY WITH QUALIFICATION |
| `education-pupil-language` | `BE-EDU-01` | same package; 25 / 24 | Newcomer language/support routes include Flemish newcomer provision, FWB DASPA and DG language classes. | Eligibility, names, placement and availability are Community/local. Do not generalize one pathway to all Belgium or treat Ukrainian online schooling as an automatic substitute. | READY WITH QUALIFICATION |
| `education-higher-admission` | `BE-EDU-01` | same package; 25 / 24 | Higher-education admission, credential recognition and fees are assessed by the relevant Community/institution. | TP does not create a Belgium-wide tuition waiver; admission, recognition, language and aid are distinct. Student aid is separate and is not automatic. | READY WITH QUALIFICATION |
| `social-subsistence` | `BE-SOC-01` | `social-assistance`; 20 production records / 20 URLs | TP holders can request assessment for equivalent financial social assistance through the competent CPAS/OCMW. | Not automatic statutory RI/leefloon; means, household and individual eligibility apply. Reference rates are not guaranteed awards; do not present the proposed 70–100% measure as current law. | READY WITH QUALIFICATION |
| `social-application` | `BE-SOC-01` | same package; 20 / 20 | A TP attestation and evidence of municipal presentation may support an application before the A card. | Competence depends on registration/residence and exceptions; evidence, decision and payment are individual. Pending TP alone is not a general cash-assistance route. | READY WITH QUALIFICATION |
| `social-child-benefits` | `BE-SOC-01` | same package; 20 / 20 | Family benefits are separate from CPAS and administered in regional systems. | Keep Flanders, Brussels, Wallonia and DG eligibility/registration/residence rules distinct; do not promise an award or generalize regional child-benefit rules. | READY WITH QUALIFICATION |

## Missing owner articles

**NONE.** All 8 topics have a suitable existing Belgium Core owner article and Article ID (see matrix above). No new Core article is proposed or created. Owner presence alone does not clear the Production translation review or the three blocked Compare aspects.

## Research gaps

Three genuine gaps are already identified in the Belgium research readiness record:

1. **`protection-application`:** current Belgian federal intake details needed to safely give a complete application guide (appointment/pre-registration, accepted evidence and no-document/family cases, among other listed operational points).
2. **`protection-dates`:** current Belgian authority instruction reconciling the EU 4 March 2028 scheme endpoint with individual A-card validity, renewal/replacement, holder action and interim proof.
3. **`residence-filing`:** route-specific authoritative treatment of TP-holder in-country filing, pending stay/work rights at TP end and the effect of an ordinary permit application/grant.

**Safe publication boundary:** omit unsupported operational detail; for a Compare fact, do not disguise these gaps as a generic “rules vary” answer. Existing sources establish limited safe statements but do not support a complete, reliable aspect fact. No additional official URLs were researched in this task, so the exact number and URLs of new official evidence pages cannot be responsibly specified here.

## Production source gaps

The current owners already have **146 owner-bound Production source records** (132 distinct HTTPS URLs):

| Owner | Production source records | Distinct URLs | Existing source inventory |
|---|---:|---:|---|
| `BE-TP-01` | 10 | 10 | `BE-TP-01.md` source bindings; TP application package |
| `BE-TP-02` | 16 | 14 | `BE-TP-02.md` source bindings; residence package |
| `BE-EMP-01` | 17 | 16 | `BE-EMP-01.md` source bindings; finding-a-job package |
| `BE-EMP-02` | 22 | 22 | `BE-EMP-02.md` source bindings; employment-rights package |
| `BE-HEALTH-01` | 13 | 13 | `BE-HEALTH-01.md` source bindings; healthcare package |
| `BE-HOUSING-01` | 23 | 23 | `BE-HOUSING-01.md` source bindings; housing package |
| `BE-EDU-01` | 25 | 24 | `BE-EDU-01.md` source bindings; education package |
| `BE-SOC-01` | 20 | 20 | `BE-SOC-01.md` source bindings; social-assistance package |
| **Total** | **146** | **132** | All records are attached to the listed existing entry IDs. |

**Owner-source records needed solely because of a missing owner: 0.** For claims already supported by these inventories, no additional source URL or binding is identified as necessary by this assessment. The three blocked claims require targeted research first; any new evidence must then be added as an owner-bound Production source record. Its count is **not yet knowable** without the research and should not be fabricated. Thus: new URLs now = **not determined / targeted research required**; new production records now = **0 for covered claims, TBD for the 3 blocked claims**.

The exact existing Production source UUIDs (which all exist in `official-sources.json` and are attached to the owner Article ID) are listed below. The corresponding Core drafts provide each source title and full HTTPS URL; research packages provide the claim-to-source alias mapping.

Representative direct official URLs already present in the owner-bound records (not an exhaustive link list):

| Owner | Existing official URL coverage (examples) |
|---|---|
| `BE-TP-01` | [Belgian Immigration Office — Registration Centre](https://dofi.ibz.be/en/themes/ukraine/registration-centre); [EU Council Decision (consolidated)](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX:02026D1912-20260804); [Belgian residence law](https://www.ejustice.just.fgov.be/eli/loi/1980/12/15/1980121550/justel) |
| `BE-TP-02` | [Belgian Immigration Office — Work](https://dofi.ibz.be/en/themes/third-country-nationals/work); [Belgian residence law](https://www.ejustice.just.fgov.be/eli/loi/1980/12/15/1980121550/justel); [Flanders — employing a foreigner](https://www.vlaanderen.be/en/working-enterprise-and-investment/working/employing-a-foreigner-in-flanders/categories/others) |
| `BE-EMP-01` | [VDAB — vacancies](https://www.vdab.be/vindeenjob); [Actiris — job offers](https://www.actiris.brussels/fr/citoyens/mes-outils-pour-postuler/postuler-a-des-offres-demploi/); [Le Forem — job offers](https://www.leforem.be/citoyens/offres-emploi.html) |
| `BE-EMP-02` | [FPS Employment — work under temporary protection](https://employment.belgium.be/sites/default/files/content/news/Brochure_travail_protect_temp_EN.pdf); [National Labour Council — minimum wage](https://cnt-nar.be/fr/dossiers-thematiques/salaire-minimum); [FPS Employment — sectoral minimum wages](https://emploi.belgique.be/fr/themes/remuneration/salaires-minimums-par-sous-commission-paritaire) |
| `BE-HEALTH-01` | [INAMI — joining a mutuality](https://www.inami.fgov.be/fr/themes/soins-de-sante-cout-et-remboursement/assurabilite/s-affilier-a-une-mutualite); [CAAMI — Ukrainian refugees](https://www.caami-hziv.fgov.be/fr/pro/refugies-ukrainiens); [FPS Public Health — 112](https://www.health.belgium.be/fr/themes/sante/aide-medicale-urgente/acteurs-laide-medicale-urgente/centrale-durgence-112) |
| `BE-HOUSING-01` | [Flanders — housing for displaced people](https://www.vlaanderen.be/vlaanderen-helpt-oekraine/huisvesting-voor-tijdelijk-ontheemden); [Wallonia — collective accommodation](https://www.wallonie.be/fr/ukraine/hebergements-collectifs); [Brussels — finding accommodation](https://www.helpukraine.brussels/fr/lookingfor/chercher-un-hebergement) |
| `BE-EDU-01` | [Belgian compulsory-education law](https://www.ejustice.just.fgov.be/eli/wet/1983/06/29/1983000482/justel); [Flanders — compulsory education](https://www.vlaanderen.be/onderwijs-en-vorming/leerplicht/leerplicht-van-5-tot-18-jaar); [FWB — newcomer learner support](https://www.enseignement.be/public/docs/daspa-flyer.pdf) |
| `BE-SOC-01` | [FPS Social Integration — TP assistance FAQ](https://www.mi-is.be/sites/default/files/documents/FAQ%27s%20version%20du%2027%20novembre%202025_0.pdf); [Primabook — CPAS competence and TP](https://primabook.mi-is.be/fr/competence-territoriale/les-regles-de-competence-territoriale-des-cpas/les-demandeurs-dasile-et-les); [Famiris — Ukrainian refugees](https://famiris.brussels/fr/faq/autres/refugies-dukraine/) |


### Production source UUID index

**BE-TP-01 (10 records):** `58179a1a-d1b1-5b23-8948-5fe40b5e7552`, `07f2aed9-f526-519f-b3d8-a77fbd702beb`, `b6faac1b-d28a-56cd-b5d2-f421a69a3e93`, `01044ed1-42f4-5d33-bbcd-931f2b1006b7`, `fc4518bf-9ef4-5f9d-8699-74c9201f59b3`, `2e8786e8-c502-5853-8ed7-ba5e9c1bc9f3`, `bfba6cf3-9235-5373-9475-663dbf309800`, `e99de5f6-c988-5957-8b20-5e2540d0ca6e`, `921c7f23-b71c-5904-9d3c-83e49bee0935`, `7739ee90-7172-5a62-b729-be68b81561bf`

**BE-TP-02 (16 records):** `f46e562b-6077-5f41-82c0-3339cbbe4f0a`, `9e3a433f-f25e-5a8f-8139-22df26c3cb45`, `3815efea-e59a-510a-8ee1-9f75add885e6`, `d428624e-ada0-5508-bea6-dafb76f70f60`, `1f7fbec4-2dfe-524e-84ff-3d532de47b26`, `a53fc39d-261e-5e59-b897-1a5cb8c1fdba`, `c826f285-132d-51a2-9088-3085059438e0`, `004e2e07-df4a-5dd2-8b08-6615118b61d1`, `4bb0cb04-562b-5f14-a5d0-c4f982d5e2db`, `629624c0-97bf-5b3d-b37a-3f36e36d6fa9`, `361dd9ab-9289-50b9-8787-bc53d99d1e6e`, `71d39cfb-146a-54b3-9be8-9f7a23c1c093`, `7c5eaee9-f716-5bd4-89f1-ef8ad9ccfdf3`, `518183d0-d7ab-5355-a6d4-c89e6858c43e`, `36887fc1-a87d-55f5-9748-d32f0385ba4a`, `14577aa6-0af5-5c93-9f97-44dd9cf1cd55`

**BE-EMP-01 (17 records):** `e89b64e7-9783-51a9-adaf-fb8cc5f6d9e8`, `05ea266d-60b3-5b6a-85ef-f5a76a3d661b`, `f59d27e0-2534-5e9c-863c-98844912ce8b`, `0a4683e0-29c4-54a2-b3ab-4cd370a7a508`, `95002c1d-f7c7-5b23-9ed5-4097eba69bcf`, `58f26033-4c71-5630-b81d-dce3b254ae81`, `ed653592-de9c-53f1-9fe8-c15b92eadd32`, `9bcc9e44-c601-5096-aeca-d6d3eb770e65`, `9273af9a-276b-5704-8952-f8059d8342d6`, `5aa4a523-2d90-5436-8891-397b32598575`, `f5b125d3-c27b-58bd-90b0-3de7805270e8`, `d61cbb1a-8eed-55c0-a38c-db91a0342fd7`, `aebe0542-9efb-50c9-99b8-1df53ce7e4aa`, `e26c4de9-358d-5edf-b2fc-93b8e946deec`, `c93d8de4-5cf6-5a14-932d-00e9509d9609`, `eb4470ee-3120-57ac-a084-f6a19975ac14`, `61c78b4d-5a7c-5674-9d6a-92990469fe73`

**BE-EMP-02 (22 records):** `be9f9561-6140-5ca7-9c43-7ec3036d151d`, `cd2fcc40-e951-5a5e-8db7-ff1e7f0cf315`, `3913778e-1ce5-5c0f-8ff4-ad97a1a351d9`, `c2c5d6f0-2d87-5638-9798-052fe251ff03`, `b446a9df-fc94-5e24-b8be-17fdbfff6805`, `3acabcbe-71f2-5ac5-9ac5-70089a4e0986`, `e04b3f9c-0696-5737-9fe6-66454122ed34`, `0f6c24cc-542d-5f4b-84ab-1f7de551c727`, `5a621d4b-af6f-5b46-9233-ad0476931e2f`, `f657f2ad-4376-556b-9515-5803bf17d9c8`, `913863ff-1aa1-5c40-9d6f-d19cc9e93be6`, `ccb59b22-9c58-5148-ab40-64a16261ad6a`, `3459ff16-cad7-5b90-a588-41cb59940c36`, `9de1e89c-79be-5f99-977d-5741b09d35fa`, `eb9ffc31-008b-5f46-b85e-0e2d7b2a441b`, `dc53fcba-c37d-5591-99c7-39325362526b`, `c0794ee2-3a4a-56e7-97b8-89e51b9a54fe`, `8a61e3c3-d1ea-5e1a-b606-b6969fac6184`, `5b9b6430-79ba-5aee-9ef6-f6d8f21e47db`, `56f4c722-2026-56c5-bbe5-27e7440694d8`, `3ebe4be7-0c7e-50bc-8222-183c6d151ede`, `f0b4fabc-1e39-554f-a429-07d32e27265a`

**BE-HEALTH-01 (13 records):** `81197d61-1969-5585-93be-a50216ee3216`, `f71c6433-3ecd-5b13-abfb-50e1899a902d`, `4f6016c8-56d4-5bea-9ba7-94f967bbfe69`, `88880ea5-61b6-58d6-8846-f1d58e96e487`, `f922c941-8ff1-535c-8d69-d8d10f83b779`, `8a1c3017-928a-5d48-b0dc-4e3d610c565e`, `4da6e640-68e6-580f-9a93-06f634ea617a`, `ba436257-02ee-5bcd-b719-284c3674be1d`, `1eabafd9-bd75-59a8-bd40-b3255ec935d7`, `2cd11cd4-3041-5a37-8348-ab27ba897605`, `88b723ba-fc98-5afa-a2f0-5fbefdd90171`, `71fcf3b7-1729-56d5-91ce-820a89f28ca8`, `de59da4a-a6fd-5271-8e38-48dfd1340eaa`

**BE-HOUSING-01 (23 records):** `38c8799b-801e-5cfe-85a3-db17c1c4ed88`, `7979026e-f481-5fa8-a83a-c1cacfd3492a`, `0717f7f4-ab78-58b3-a22c-872a5be2a045`, `65e9de22-190a-5655-9b3b-fdeeff11ed5a`, `4aeda6da-7b0c-5e75-ae89-e1dde5202fa8`, `1371f734-17f8-5d91-8b7e-c470905ad35c`, `cd12674f-4aa0-5fd9-bfce-64669f6c5efa`, `3ffc736b-e9b7-5124-b9fc-99dfb78a74c0`, `896a52f0-1572-530f-a3b7-22c0f6a6d91e`, `df234032-d270-54d6-962b-067fd06bd678`, `3f00a643-854f-50bd-8684-86abff1fc96f`, `2b6262d0-4b0d-543a-89cc-a19047f983d0`, `36256a4b-1038-5a9f-bed2-4ba10ab638f8`, `a2f8a1c5-27ef-545c-9fdc-305365fe56b8`, `4a65d4b6-7e4a-581f-bec6-3a663177d5ab`, `6db7d19a-37aa-551e-9b8a-0fb1e823a80c`, `86df2aeb-4505-52db-a869-735b2d22e2c9`, `e145e499-8372-5569-8670-3955186fa7fa`, `c72f8669-a2f9-5169-aa0c-028743ff7c80`, `8d504f88-5263-5624-a146-58247453e9ab`, `9cbfd3be-7f75-5f10-9506-fac985c29579`, `8f25d6ec-1878-589f-a63d-e6ce02da48fa`, `93082b4e-5949-5f85-ade6-e84a8d427b86`

**BE-EDU-01 (25 records):** `4c1dbd7c-191e-5fe8-b4e7-4068e7e2a412`, `81b42fb9-92b5-58a6-9175-839b1f51b3bf`, `374087c8-f1ef-5605-a95f-56bd147cea56`, `966a48b9-5806-5fd9-a75b-91f3bc9a7871`, `9dc5e4af-2b45-5706-97af-a52dc8e29104`, `759e9eb4-8170-56fc-9d2b-81e0d071708a`, `a12e3080-07a0-5320-99de-631048c72889`, `6524c420-aac2-50ca-9e54-97dd0392deaa`, `8aaf36d9-17d9-5ae3-a85e-ea44ea493465`, `914453bb-89bf-5ff2-96e8-0174c35a06a5`, `33ad0f2a-4e8d-5dfd-9f12-b3af21e08074`, `b87e0d8e-68cc-5a9e-8f60-4cd69f35341c`, `04bfeaa1-e1f5-55d4-8414-83576cf21102`, `24388ad9-f1f9-50ed-9457-ac1effe64c3c`, `55527e9d-3d6e-5ba7-8673-017b5c85184f`, `536ee853-3420-5e84-900a-c4e773ace826`, `9934a200-35bc-531f-9718-7826071b4cd6`, `b1beb623-f98a-5a9e-8f23-5d0ee0464480`, `35499662-2299-58d2-b78d-ac8d49edaa9c`, `35d94f29-2a00-592f-9f6d-ce34e5021102`, `4ae5b359-e5ab-5490-a662-b4dcedce7f79`, `757cd1e2-91e3-521a-9677-1bd011ca04f4`, `af739589-319e-5f1a-a4c4-8ab7cdb494e1`, `9edceacc-c1b6-547f-9e41-c831aeec577d`, `6ab58a9c-b6cb-5923-bd21-8dbcb44101d6`

**BE-SOC-01 (20 records):** `d62396b7-dd77-5b71-9d02-f98d791dbfb8`, `7c957ee3-fa6d-5ef4-b673-50d7640dd737`, `449be759-c789-5d11-b901-a8c4b3f892d3`, `6e21b16b-0405-5c4d-ad00-565185b66fa5`, `53454d1a-d42e-55b1-b428-f5d35390988b`, `085020dc-fd7d-58a1-ab3f-faf160153e45`, `4cb43553-e363-54d4-967b-4da42e2f8dd6`, `fe1b60e9-1d79-5bd2-846e-9fe834645037`, `d12c3b88-ba4a-55b4-9a41-5be3f35a97ab`, `6121f97a-fd76-584e-b0fc-0103a0f29914`, `a4c52d48-6591-5e0a-aefa-51182569d5a2`, `965e61d9-6505-5bfd-a9e2-ac4994c69791`, `cab92bae-0b76-56b2-9581-7381ad653d3b`, `da7ce59f-9468-5455-b548-6a879c954b28`, `7e9a47b2-6a01-57e3-ae65-9f64425869a2`, `3ec5a565-96aa-57f8-88d9-dcb0bd951387`, `1e087799-5007-5780-9f82-299c88f491b5`, `6b97ac43-6e68-5d54-912a-6e8aa2f97f46`, `61baf97b-858c-5365-a0b6-deff47b476e2`, `864bb2ea-e879-5d6c-9893-c86f28ca1050`

## Regional-scope risks

- **Flanders:** distinct regional systems for employment service, housing/tenancy and social housing, education/language support, and Groeipakket. A Flemish rule is not a Belgium-wide rule.
- **Wallonia:** Le Forem and Walloon tenancy, education/FWB-facing, housing and family-benefit procedures have their own requirements. Keep Walloon and French Community competences distinct where they differ.
- **Brussels:** Actiris and Brussels housing/family-benefit systems are separate. Education can follow either Dutch-language Flemish or French-language FWB systems; a City of Brussels procedure is local, not national.
- **German-speaking Community (DG):** it has separate employment, education, housing and family-benefit institutions. Do not silently omit DG when making a country-wide claim or infer its rules from Wallonia.
- For higher education and school/language support, name the responsible Community/institution. For rental rules, name the Region. For child benefits, state the administering regional system and scope.

## Final counts

- **READY:** 0
- **READY WITH QUALIFICATION:** 21
- **RESEARCH REQUIRED:** 3
- **Topics with owner:** 8/8
- **Missing owner topics:** 0
- **Existing usable Production source records:** 146 across 8 owner articles / 132 distinct URLs; all 146 UUIDs indexed above
- **New Production source records required:** 0 for already supported claims; additional bindings for the 3 research gaps remain TBD
- **New official research URLs required:** targeted evidence is needed for 3 gaps; exact number/URLs cannot be known without the separately authorized research
- **Belgium Compare facts currently:** 0
- **Current Compare:** 16 countries / 8 topics / 24 aspects / 384 facts

## Verdict

**TARGETED RESEARCH REQUIRED**

Belgium has all eight topic owners and existing source bindings, but not Production-released Core: all translations are `NeedsReview` and the country is hidden in Production. Separately, Compare drafting must wait for evidence on `protection-application`, `protection-dates`, and `residence-filing`. This assessment creates no article, Compare fact, source record, or production change.
