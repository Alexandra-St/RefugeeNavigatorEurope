# Belgium Public Launch Red-Team Audit 01

**Audit date:** 21 November 2026  
**Starting current main:** `15a6bcbbd8dd36bb5bb4cd41581ea20a88a45b5b`  
**Scope:** Belgium production articles and Belgium Compare facts only. No production file was changed.

## Baseline and inventory

The current `main` branch was verified through GitHub's branch API at the starting SHA above. Its production blobs were checked at that exact commit: `information-entries.json` (`2bd71798d8b86913c5cbe74239afcc835ab74239`), `official-sources.json` (`b208eb988b0cbcdf0862be2f49c39cbef21e7781`) and `comparison-facts.json` (`2462e40b16e381f179d02222bfe187cd77a266d0`).

Eight approved Belgium articles have complete EN/UK-UA/RU content:

| Article | ArticleId |
|---|---|
| BE-TP-01 | `aef6ec65-f8ed-5054-bafb-c465e790dd57` |
| BE-TP-02 | `54c6f95f-589e-5901-beae-990bed994160` |
| BE-EMP-01 | `f2c1526a-a218-5bf0-ab2a-9167dbec0c26` |
| BE-EMP-02 | `55fa79e5-89a2-518b-90b2-85a2735f171b` |
| BE-HEALTH-01 | `673316a9-6ee9-50d6-9f66-e19659aeff0e` |
| BE-HOUSING-01 | `3646ae19-215c-5f0e-9075-85ef5bae88f7` |
| BE-EDU-01 | `ac80dcbd-4289-5c77-8340-f9e4226a47f6` |
| BE-SOC-01 | `20eef3c3-fcc7-5b5b-b672-695f29faad61` |

The finalized Belgium refresh report/review dated 16 November 2026 was used as the editorial baseline. Its 216 triage rows include 41 source-bundle rows; the 175 claim rows were matched against current production and reviewed for materiality. All 175 material/high-risk claim groups were audited (175/175), and their 525 localized instances were checked for EN/UK-UA/RU meaning. Thirty-three claim groups with explicit dates, amounts, ages, durations, deadlines or comparable time-sensitive conditions were checked (33/33).

Belgium has 24 current Compare facts across eight topic groups. All 24 were audited, including owner, availability/status, language parity, source ownership and Core alignment. Each is approved and available. The work-minimum-pay fact repeats the current Core error, so it is aligned with Core but not factually current.

## Material-topic delta search

The current official evidence was checked against the 16 November refresh through 21 November 2026 for:

1. Temporary-protection eligibility and the corrected EU military-obligation condition.
2. Registration, residence documents, validity and renewal.
3. Alternative residence routes and transition claims.
4. Work permission, regional employment services, employer duties and minimum pay.
5. Social assistance, CPAS procedure, statutory reference amounts and family benefits.
6. Healthcare affiliation, emergency care and assessed medical assistance.
7. Reception/housing capacity, regional schemes and tenancy rules.
8. School access, Community-specific support and higher-education tuition.

The targeted delta search found a material update to the Belgian RMMMG amount: FPS Employment and CNT/NAR show €2,233.61 gross per month from 1 July 2026, superseding the €2,189.81 April amount still shown in current Core and Compare. The official sources also clarify that CCT 50 is supplementary and applies to under-18 workers and student workers aged 18–20 only where the relevant sectoral agreement has not set a minimum for those groups. The current Core wording states that student workers aged 18–20 are covered by CCT 50 without this sectoral-fallback qualification.

Official evidence:
- FPS Employment, “Salaire” (RMMMG/CCT 43 and CCT 50 scope): https://emploi.belgique.be/fr/themes/remuneration/salaire?id=39004
- National Labour Council, CCT amount tables updated 1 July 2026: https://cnt-nar.be/fr/documents/montants-des-cct
- FPS Employment student remuneration table effective 1 July 2026: https://emploi.belgique.be/fr/themes/contrats-de-travail/contrats-de-travail-particuliers/contrat-doccupation-detudiants

No material delta was found in the other seven topic groups that changes the current qualified propositions. The reviewed evidence continues to indicate that regional services, benefit access, healthcare affiliation, housing and educational support remain status-, region-, assessment- or capacity-dependent as stated in Core. No recent rule was found that converts these pathways into a nationwide automatic entitlement.

## Temporary protection and residence

Council Implementing Decision (EU) 2026/1912 was checked in its corrected consolidated form. It extends the EU temporary-protection scheme through 4 March 2028. Its military-obligation condition applies from 5 August 2026; the corrigendum excludes people who already enjoyed protection in the same Member State on or before 4 August 2026 and continuously retain it there. Current Belgium Core reflects the corrected dates and same-state continuity exception, distinguishes the EU scheme endpoint from an individual Belgian card expiry, and discloses that Belgian public guidance does not provide a complete national evidence checklist. The old pre-corrigendum dates are not adopted as controlling.

Official source: https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX:02026D1912-20260804

The Belgian Immigration Office's current temporary-protection page still contains inconsistent legacy card text. Current Core identifies that inconsistency, states the dated 2027 renewal guidance separately, and tells readers to check the expiry printed on their individual card. Registration intake wording does not invent appointment/walk-in rules or a guaranteed same-day process. No defect was confirmed for these points.

## Employment, proposal status and source findings

One confirmed P1 finding was recorded as `BE-P1-001` for `BE-EMP-02`, ArticleId `55fa79e5-89a2-518b-90b2-85a2735f171b`, Compare aspect `work-minimum-pay`.

- Core and Compare state €2,189.81 per month from 1 April 2026 as the present RMMMG. The official current amount is €2,233.61 from 1 July 2026.
- Core says student workers aged 18–20 are covered by CCT 50 without stating that CCT 50 is only the supplementary fallback where sectoral rules do not set a youth/student minimum.
- Effective sources include the March 2026 rate announcement, CCT 43/18, CCT 50, CNT/NAR and the sectoral wage database. The April amount announcement is stale for the current rate. The valid CCT 50 source supports the instrument but only partially supports the unqualified scope wording.
- Suggested correction: update the rate/date in EN/UK-UA/RU and the affected Compare fact; state the sectoral-fallback condition for CCT 50 and add the current FPS Employment/CNT evidence in the follow-up production repair.

This is a materially incorrect current remuneration amount/legal condition, severity P1. It is non-blocking to an individual's status but materially affects employment/pay guidance. Exact current language, sources, evidence and suggested correction are in `GLOBAL_PUBLIC_LAUNCH_RED_TEAM_BE_FINDINGS_01.csv`.

One current proposal-status statement was checked: the federal 70%-to-100% integration-linked amount remains a proposal and is not described as enacted law. No proposal/current-law misstatement was found. No P0, P2 or unverified high-risk candidate was confirmed.

## Sources, translations, structure and Compare

There are 142 unique effective source IDs across Belgium Core and Compare. All resolve in `official-sources.json`; all Core citations resolve to an owner record, and all Compare owners/source owners match the relevant article. Duplicate SourceIds: 0. Dangling OfficialSources: 0. Dangling Core refs: 0. Dangling Compare refs: 0. Wrong-owner Compare refs: 0.

The effective-source review found 140 supported bindings, one partial binding (CCT 50 scope in the current wage proposition), and one stale-for-current-rate binding (the March rate announcement). No unsupported or wrong-scope binding was confirmed. The pay source weakness is included in BE-P1-001. This binding-level classification does not count repeated inline citations in the three language strings as separate source records.

Translation parity: 175/175 material Core proposition groups; 525 localized instances; zero semantic divergences. The 24 Compare fact values were also checked across EN/UK-UA/RU. All 24 owners are correct and source ownership is valid; the wage fact shares the Core finding and does not contradict it.

## Watch

The starting Watch index contains 0 Belgium rows. Four exact, authoritative future triggers are recorded in the findings CSV as new Watch candidates: the A-card renewal window opening on 4 January 2027; the card-validity/fallback date on 4 March 2027; the corrected EU extension beginning on 5 March 2027; and the EU scheme endpoint on 4 March 2028. The global Watch index was not changed in this Belgium-only audit record. No speculative wage-rate date was added.

## Structural verification and disposition

| Check | Result |
|---|---:|
| Belgium articles with EN/UK-UA/RU | 8/8 |
| Belgium Compare facts audited | 24/24 |
| Unique effective source IDs inspected | 142 |
| Duplicate SourceIds | 0 |
| Dangling OfficialSources | 0 |
| Dangling Core refs | 0 |
| Dangling Compare refs | 0 |
| Wrong-owner Compare refs | 0 |
| Translation divergences | 0 |
| P0 / P1 / P2 | 0 / 1 / 0 |
| Unverified high-risk candidates | 0 |

**Belgium factual status: P1_BLOCKED.** The audit is complete, but the public-launch completion gate is not passed until BE-P1-001 is repaired and recertified. No production file was changed in this task. The only production follow-up needed from this finding is the updated rate/scope wording and aligned `work-minimum-pay` Compare fact.

