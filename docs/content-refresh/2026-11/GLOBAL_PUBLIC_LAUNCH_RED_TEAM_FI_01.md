# Finland Public-Launch Red-Team Audit

- Audit date: 21 November 2026
- Base SHA: `f572341a50a61f374b4903ba27d53fbeed2041fc`
- Country: Finland (`finland`)

## Article inventory

Eight production articles, all present in EN/UK/RU (24/24 localized entries): FI-TP-01 `ffd32dca-56d8-5dcf-b995-7522a159010a`; FI-TP-02 `344c5e31-45a9-5f22-8d4a-4a58d8fe8243`; FI-EMP-01 `53743a68-bacb-5344-9aee-559e146cab54`; FI-EMP-02 `0abffa8c-cd42-571e-97f3-e160afad880c`; FI-HEALTH-01 `ea41d1e7-f921-50af-9886-2232d9fb06ef`; FI-HOUSING-01 `61b331e0-76bb-58f4-91e7-e5944ba77e31`; FI-EDU-01 `2a2094da-e321-50bd-8a45-bf3988be06c9`; FI-SOC-01 `51691974-1c20-5b25-9572-66624984a52f`.

## Compare inventory

24 Finland aspect facts: protection-scheme, protection-application, protection-dates, residence-routes, residence-conditions, residence-filing, employment-vacancies, employment-institutions, employment-assistance-conditions, work-permission, work-employer-duties, work-minimum-pay, healthcare-access, healthcare-proof, healthcare-urgent, housing-initial, housing-private-rental, housing-financial-support, education-school-enrolment, education-pupil-language, education-higher-admission, social-subsistence, social-application, social-child-benefits.

## Source inventory

101 Finland-owned OfficialSource records (ownership-linked to the eight article IDs). Source binding identifiers and fact ownership were inspected in repository JSON. All three translation languages exist for all eight entries.

## Audit methodology and evidence limits

Read-only inspection of production `information-entries.json`, `official-sources.json`, `comparison-facts.json`, plus the Finland refresh research and review artifacts dated November 2026. The review artifact records that it checked 24 aspects, eight articles, current source bindings, and staged drafts, but explicitly says it undertook no full new research. This audit did not independently open every linked primary authority; therefore it cannot certify the requested complete current-source audit or treat the staged review as a substitute for current primary evidence. Network availability, repository base and origin/main were confirmed. Primary-authority families cited in production/review include Migri, Government of Finland, EU Council, Finlex, Job Market Finland, Kela, wellbeing services counties and Finnish National Agency for Education; substantive page-by-page validation remains unresolved for this audit.

## Coverage and findings

- Core audit coverage: 8/8 entries inventoried; proposition-by-proposition primary-source verification incomplete.
- High-risk proposition coverage: high-risk claims inventoried in the eight articles; current independent primary-source verification incomplete.
- Time-sensitive coverage: dated 2025–2027 transition and expiry statements inventoried; current independent primary-source verification incomplete.
- Translation consistency: all languages exist; the 17 November review reports 24/24 parity after its edits. This audit did not independently diff every translated proposition.
- Source-binding integrity: 101 owned source records and 24 fact bindings inventoried; independent scope and live-content validation incomplete.

### High-risk areas requiring source revalidation

The production and review materials themselves identify unresolved or time-sensitive propositions: 5 August 2026 EU/Migri eligibility change versus Finnish Government Decision effective 13 August; the EU TP horizon of 4 March 2028 versus Migri permit/card guidance through 4 March 2027 and conflicting automatic-extension wording; 1 September 2026 reception-service transition; 1 April 2026 Kela valid-permit condition; 1 May 2026 unemployment benefit replacement; and the proposed 2027 benefit change. The existing review records qualified copy and binding corrections, but this audit lacks fresh primary-source inspection sufficient to clear these issues.

## Findings table

| ID | Severity | Affected scope | Finding |
|---|---|---|---|
| FI-P2-001 | P2 | All Finland articles and 24 Compare aspects | The requested independent, current page-level evidence check was not completed. Existing review explicitly disclaims full new research. Do not interpret this audit as legal/current-source certification; re-open the primary sources for the date-sensitive and high-risk propositions before launch clearance. |

Current proposition/effective source IDs: propositions and bindings remain as recorded in production; the scope and currentness of linked sources were not independently verified in this run. Authoritative evidence checked: repository research/review artifacts only, not live authority page contents. Minimal correction: perform and document primary-source verification before marking the country cleared. Confidence: High that the verification gap exists; no conclusion that a proposition is false.

## Watch candidates

- FI-W-001 — Temporary protection/individual Finnish permit extension and card arrangements approaching 4 March 2027 and the EU scheme horizon 4 March 2028; trigger: official Migri extension/card instructions or expiry approach.
- FI-W-002 — Reception-service three-month transition rule effective 1 September 2026; trigger: amendment, implementation guidance or approaching individual transition date.
- FI-W-003 — Kela unemployment-benefit eligibility proposal for 1 April 2027; trigger: enactment or official Kela guidance.

## Structural validation

Inventory confirms 8 Finland entries, 101 owner-linked Finland sources, and 24 Finland facts. Localization presence is 8/8 in each of EN, UK and RU. This report does not claim that URL uniqueness, source-title scope, live-source status, or every evidence-to-proposition edge passed semantic review. No production data was edited.

## Final launch assessment

P2_ONLY — audit evidence is insufficient to issue a clean public-launch clearance. This is an audit limitation and sourcing-verification finding, not a determination that the Finland content is materially false. Counters: `COMPARE_AUDITED=0/24` fully independently verified; `MATERIAL_HIGH_RISK_AUDITED=0/0` independently cleared (denominator not reliably enumerated from claims); `TIME_SENSITIVE_AUDITED=0/0` independently cleared (denominator not reliably enumerated); `SEMANTIC_DIVERGENCES=0` independently established; `P0=0`; `P1=0`; `P2=1`; `UNRESOLVED_HIGH_RISK=1`; `WATCH_CANDIDATES=3`.
