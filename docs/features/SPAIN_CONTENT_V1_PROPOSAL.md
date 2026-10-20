# Spain content V1 proposal — research foundation

**Status: RESEARCH IN PROGRESS — TP / RESIDENCE FOUNDATION ONLY.** This is a partial architecture stub, not a final country proposal, content approval, translation approval, or implementation authorization. Research date: **20 October 2026 (UTC)**.

## Country identity

| Field | Proposed value |
| --- | --- |
| English / catalog name | Spain |
| Ukrainian | Іспанія |
| Russian | Испания |
| Slug | `spain` |
| Proposed CountryId | `10000000-0000-0000-0000-000000000005` |
| Country resource key | `Country.Spain` |
| Location resource key | `Country.Spain.In` |
| Location phrase (EN) | in Spain |
| Location phrase (UK) | в Іспанії |
| Location phrase (RU) | в Испании |

These are proposed documentation values only. Do not add them to runtime/catalog files in this phase.

## Eight-article architecture

Only the two temporary-protection articles below have research foundations in this phase. The six remaining topics are explicitly not yet researched.

| ID | Category | Working English title | Research status |
| --- | --- | --- | --- |
| ES-TP-01 | `temporary-protection` | Temporary Protection in Spain: Applying and Maintaining Your Status | Researched in this phase; publication blockers remain. See [source and claim matrix](../content-research/spain-batch-2026-10-20/temporary-protection-application/SOURCES_AND_CLAIMS.md). |
| ES-TP-02 | `temporary-protection` | Residence Options Beyond Temporary Protection in Spain | Researched in this phase; EU long-term-residence counting remains unresolved. See [source and claim matrix](../content-research/spain-batch-2026-10-20/after-temporary-protection/SOURCES_AND_CLAIMS.md). |
| ES-EMP-01 | `employment` | Finding a Job in Spain | **NOT YET RESEARCHED** |
| ES-EMP-02 | `employment` | Work Authorization and Employee Rights in Spain | **NOT YET RESEARCHED** |
| ES-HEALTH-01 | `healthcare` | Accessing Healthcare in Spain | **NOT YET RESEARCHED** |
| ES-HOUSING-01 | `housing` | Finding Accommodation in Spain | **NOT YET RESEARCHED** |
| ES-EDU-01 | `education` | Education and Language Learning in Spain | **NOT YET RESEARCHED** |
| ES-SOC-01 | `social-assistance` | Financial and Social Assistance in Spain | **NOT YET RESEARCHED** |

## Research inventory

| Article | Atomic claims | Unique official source URLs in its matrix | Current assessment |
| --- | ---: | ---: | --- |
| ES-TP-01 | 20 | 8 | Spain-specific cohorts and procedure are documented. EU protection runs to 4 March 2028, while the Spanish TIE extension instrument reviewed runs only through 4 March 2027. Detailed Spain-specific evidence rules for the August 2026 military condition and Spanish document handling after that date remain unresolved. |
| ES-TP-02 | 14 | 4 | A Spain-specific framework for applying to other in-country authorizations exists through RD 316/2026 and SEM 2/2026; there is no automatic blanket conversion. SEM 2/2026 expressly credits TP time for national long-term residence. The inspected sources do not establish TP-time counting for EU long-term residence. |
| **Unique sources across both matrices** | — | **12** | The TP matrix has 8 URLs and residence matrix 4 URLs. The two registers contain no duplicate URL, so the deduplicated total is 12. |

Claims marked Conditional preserve the relevant cohort, procedure, or exception; they are not individual eligibility findings. No production source IDs, `LastCheckedAt`, article timestamps, or verification statuses are assigned.

## Spain temporary-protection date and evidence boundaries

- **EU scheme:** Decision (EU) 2026/1912 sets 4 March 2028 for the EU-covered displaced-person categories. The corrigendum makes the military-obligation provision applicable from 5 August 2026 and preserves an exception for people holding TP in the same Member State before/on 4 August 2026 who retain it continuously.
- **Spanish card:** Orden INT/96/2026 extends covered Spanish TIE cards through 4 March 2027 and says a new card is not needed for the specified Article 209 obligation. The EU scheme date does not itself extend the Spanish card or decide a person's document validity. Recheck for a later Spanish measure and authoritative instructions before any publication.
- **Military evidence:** Spain’s Ministry overview says applicants from August 2026 must justify compliance with Ukrainian military obligations or exemption. It does not provide age bands, Spanish evidence formats, or detailed handling of continuous existing holders. Do not import Czech evidence requirements.
- **Official-source date conflict:** Ministry temporary-protection and FAQ pages still state 4 March 2027, reflecting earlier EU acts. Those historic statements conflict with the later EU act on the EU scheme endpoint and must not be reproduced as current. The status of Spanish TIE documents after 4 March 2027 is not established by the sources reviewed.
- **Transition to other residence:** Spain now has an administrative transition framework for current TP holders to seek applicable ordinary in-country authorizations. It is route-specific, not a separate automatic “conversion” permit.
- **Long-term residence:** TP time counts fully toward the five-year national long-term-residence period under SEM 2/2026. The inspected evidence does not establish TP time counting for the distinct EU long-term-residence route.

## Comparison V2 research candidates — temporary-protection aspects only

This is a candidate mapping, not final comparison answer text or approval. Readiness reflects evidence completeness, not eligibility or legal certification.

| Aspect ID | Candidate claim IDs | Readiness | Important limitation |
| --- | --- | --- | --- |
| `protection-scheme` | ES-TP01-01, ES-TP01-03–07, ES-TP01-20 | **Conditional** | EU-defined cohorts and Spain’s broader national scope must remain distinct; the Spanish cohort expansion has separate factual conditions. |
| `protection-application` | ES-TP01-08–11 | **Conditional** | Legal procedure is established, but current operational appointment/office instructions can change; evidence is cohort-specific. |
| `protection-dates` | ES-TP01-01–02, ES-TP01-14–16, ES-TP01-19 | **Unresolved for a single combined answer** | EU protection through 4 March 2028; Spanish TIE measure through 4 March 2027; outdated official pages still say 2027. Do not merge scheme and card dates. |
| `residence-routes` | ES-TP02-01–08, ES-TP02-14 | **Conditional** | A framework exists, but route accessibility and criteria vary; no automatic conversion or guaranteed approval. |
| `residence-conditions` | ES-TP02-03–08, ES-TP02-10–13 | **Conditional** | Only limited signposts researched; this is not a complete route-specific eligibility catalogue. |
| `residence-filing` | ES-TP02-02–09, ES-TP02-13 | **Conditional** | In-country filing is limited to procedures that can be initiated in Spain; current channels, steps, and sequence need route-specific verification. |

No comparison facts are drafted or added.

## Future guide note

ES-TP-01 may provide a foundation for a future `/guides/spain/temporary-protection` navigation guide after operational procedures and the military-evidence/document-validity publication gates are resolved. No scenarios, steps, branches, or guide data are designed here.

## Remaining research before a complete Spain proposal

Research the six employment, healthcare, housing, education, and social-assistance articles. Before integrating either TP article, resolve or transparently gate Spanish TIE handling after 4 March 2027, obtain authoritative Spain-specific military-evidence instructions for applicable applications, and clarify whether TP time counts for EU long-term residence. Recheck the Spanish pages that still display the earlier 4 March 2027 date. No runtime or production-catalog readiness is claimed.
