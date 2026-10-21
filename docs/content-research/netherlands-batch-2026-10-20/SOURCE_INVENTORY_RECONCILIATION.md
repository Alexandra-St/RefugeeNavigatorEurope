# Netherlands Source Inventory Reconciliation

**Baseline:** `3cfd485f91880134de9372c2f64e0b415130675d`
**Reconciliation date:** 21 October 2026
**Scope:** Exact URL coverage between the shared official-source inventory, eight article research matrices and Netherlands production sources. Documentation only; no new legal research or production changes.

## 1. URL normalization and method

The inventory table's URL targets were compared with every exact URL in the eight `SOURCES_AND_CLAIMS.md` source registers. Normalization only removes Markdown link wrappers and terminal punctuation introduced by surrounding prose/table syntax. It does **not** change host or path case, remove `www`, alter query strings, collapse trailing slashes, follow redirects, infer canonical URLs, or treat similar titles as the same source. Therefore a different path remains a separate URL unless the records establish otherwise.

Production provenance was checked only for Netherlands-owned records in `official-sources.json`. Production source GUIDs, URLs and `LastCheckedAt` values were not changed.

## 2. Six discrepancy decisions

| Exact URL / authority and title | Matrix ID(s) | Production use | Evidence role and direct-review record | Decision |
| --- | --- | --- | --- | --- |
| [UWV — Working in the Netherlands as a Ukrainian refugee](https://www.uwv.nl/en/individuals/working-in-the-netherlands/ukrainian-refugee) | EMP01-S02; HEALTH-S04 | Yes: Finding a Job, source `1690f9d6-0721-5005-b9de-fda56864ed70`; Healthcare, source `e1edd80d-2530-57e4-8e55-637382f1dfd1`. Both catalog records retain `LastCheckedAt` `2026-10-20T00:00:00+00:00`. | Directly opened/reopened in the matrices on 20 Oct 2026; Phase 18.4A reopened it on 21 Oct 2026. No page update date was visible. Supports bounded UWV/EURES/jobseeker navigation; its generic “must take out” insurance wording is conflicting guidance, not a resolution of Zvw/RMO. | **Add** as `NL-EMP-07 / NL-HEALTH-11`, with both matrix labels and the conflict warning. Inventory review date is date-only; do not interpret the production timestamp as an exact time the page was checked. |
| [Government.nl — Civic integration in the Netherlands](https://www.government.nl/themes/migration-and-travel/integration-in-the-netherlands/civic-integration/civic-integration-in-the-netherlands) | EDU-S05 | No Netherlands production source record. | The education source register records that this exact URL could not be fetched in the 20 Oct 2026 review. No substantive passage was read; it is not evidence. No update date could be confirmed. | **Add** as `NL-EDU-10` solely to preserve the matrix source-register provenance, visibly marked inaccessible / not evidence. |
| [Rijksoverheid — “Wat moet ik doen als iemand dringend medische hulp nodig heeft?”](https://www.rijksoverheid.nl/themas/familie-en-zorg/zorgverzekering/vraag-en-antwoord/eerstelijnszorg/wat-moet-ik-doen-als-iemand-dringend-medische-hulp-nodig-heeft) | HEALTH-S07 | No Netherlands production source record for this exact URL. | Healthcare source register says it was opened on 20 Oct 2026. It supports 112 for life-threatening situations and GP, out-of-hours GP or central-doctors navigation for less urgent needs; no update date displayed. | **Already matched; no change.** The exact source-register URL is already recorded as NL-HEALTH-06. The `/familie-zorg-en-gezondheid/…` path described in the prior audit is not the URL in this source register; it was not added or treated as equivalent. |
| [Government.nl — Arrival, registration and stay of refugees from Ukraine](https://www.government.nl/themes/migration-and-travel/reception-of-refugees-from-ukraine/arrival-registration-and-stay) | None for this exact URL | No Netherlands production source record. | The earlier inventory's source table describes general arrival/municipality/BRP navigation and records no update date. Its inventory-level check date is 20 Oct 2026; no per-URL timestamp is recorded, and this exact URL was not reopened in this phase. The inventory notes that its sequence predates later triage guidance. | **Retain as historical/contextual** inventory-only reference. It is not evidence for current intake order and is not mapped to a current matrix claim. |
| [Rijksoverheid — municipal triage-process instruction landing page](https://www.rijksoverheid.nl/documenten/2024/09/05/instructie-triageproces) | None for this exact URL; matrices cite the linked handbook instead | No Netherlands production source record. | The earlier inventory records that this landing page identifies the 17 Sep 2026 circular and links the 1 Oct 2026 handbook; inventory-level check date 20 Oct 2026, exact time not recorded. The landing-page description was directly inspected for that inventory entry; it was not reopened in this phase. Its role is an administrative index/context pointer, not a substitute for the handbook. | **Retain as contextual index** only. Keep it distinct from the substantive handbook URL used by TP-01; it does not establish an individual result or appeal right. |
| [Government.nl — Civic integration overview, legacy URL](https://www.government.nl/themes/migration-and-travel/integration-in-the-netherlands/civic-integration-in-the-netherlands) | None for this exact URL. EDU-S05 is the different longer path in row 2. | No Netherlands production source record. | The earlier inventory describes a general, status-dependent civic-integration overview and no displayed update date; no separate per-URL check date/time or direct-access outcome was recorded, and the exact legacy URL was not reopened in this phase. The current education matrix says its different EDU-S05 URL could not be fetched on 20 Oct 2026. | **Retain as legacy URL/reference, not evidence.** Do not treat it as the same page as EDU-S05 or infer that it redirects. Reopen the exact legacy URL before using it substantively. |

The UWV page's two production source records are article-owned instances of one shared URL. Both GUIDs and their source check timestamps are preserved. UWV remains relevant to employment navigation and is an explicit source in the healthcare conflict; adding it to the inventory does not settle the conflict or authorize individual insurance advice.

## 3. Historical and contextual handling

The three URLs already present in the inventory but absent from current matrix registers were not deleted:

- The arrival/registration overview remains as historical context only because the inventory itself notes that its public sequence is older than the later triage instruction.
- The triage landing page remains as an index to the circular and handbook. The handbook remains the substantive matrix source; the landing page is not promoted to equivalent claim evidence.
- The older civic-integration path remains a legacy locator with no independent direct-review claim. It is not merged with the longer, inaccessible EDU-S05 path.

All three records are marked so that an auditor can distinguish inventory history from current matrix evidence. Their retention does not imply current applicability, an exact source check time, or production use.

## 4. Final URL accounting

| Set | Unique exact URLs | Result |
| --- | ---: | --- |
| Eight source registers (union) | 51 | All 51 exact source-register URLs are represented in the reconciled inventory. |
| Reconciled inventory | 54 | 51 source-register URLs plus 3 explicitly retained inventory-only historical/contextual URLs. |
| Source-register URLs missing from inventory | 0 | No remaining matrix source-register coverage discrepancy. |
| Inventory-only historical/contextual URLs | 3 | Retained and labelled above; none is treated as current matrix evidence. |
| Duplicate exact URL records in inventory | 0 | Each exact URL appears once in the inventory source table. |

The earlier count of 52 URLs across the matrices is not reproducible when the requested comparison is limited to the eight `Official sources reviewed` registers: those registers contain 66 source rows and 51 unique exact URLs. The pre-correction inventory had 52 URLs, with three inventory-only entries and two actual source-register-only entries; one URL previously described as matrix-only (the urgent-care Q&A) was already an exact match. Adding only the two documented missing register URLs produces the 54-URL inventory. The inventory is intentionally larger than the register union; no valid history was deleted to force equal totals.

## 5. Production provenance

The Netherlands production catalog has 59 article-owned official-source records across 44 exact URLs. Each production URL remains traceable to one or more of the approved matrices. The UWV URL is the production-linked URL that was absent from the prior shared inventory; it is now represented under the two relevant matrix IDs. No other Netherlands production source URL lacked matrix provenance in the audited set.

The inventory uses research labels and is not the production source catalog. Repeated production records for a shared URL remain separately owned by their articles; the inventory records the URL once. Existing production `LastCheckedAt` values remain unchanged, including the two UWV records dated `2026-10-20T00:00:00+00:00`.

## 6. Remaining discrepancies and limitations

- There are no remaining exact-URL coverage gaps between the eight source registers and the inventory.
- Three inventory-only links remain by design as historical/contextual references; they do not have current matrix claims. Their exact paths must not be silently substituted for similar paths.
- EDU-S05 remains inaccessible and is recorded only to preserve matrix provenance; it supplies no substantive evidence.
- The inventory reconciliation does not resolve the UWV-versus-TPD/RMO insurance conflict, verify individual coverage, or establish legal conclusions. The conflict and limits remain exactly as recorded in the healthcare matrix and publication-readiness audit.
- Source update dates that were not displayed remain unknown. Matrix check dates remain 20 October 2026 where recorded, without exact times. The UWV page was also reopened during the 21 October audit; no production timestamp was changed.

## 7. Publication implications

This reconciliation improves source traceability only. The UWV source remains conflicting evidence for healthcare and does not make insurance advice publishable. The inaccessible civic-integration page is not evidence. The urgent-care Q&A supports only its bounded emergency-routing statement. Historical and contextual inventory-only entries must not be used to fill claim gaps. Existing publication-time freshness checks, evidence limitations, and all `NeedsReview` / `Draft` statuses remain in force.

No production content, source record, comparison fact, status, application behavior or deployment setting was changed. This document does not approve publication or authorize deployment.
