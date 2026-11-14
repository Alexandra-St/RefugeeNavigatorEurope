# France Production Readiness Gate 02

**Review date:** 14 November 2026
**Scope:** Targeted re-check of `residence-filing` and `housing-initial`, plus the existing France V1 six-article catalog. No drafts, catalog records, statuses, Compare facts, code, or tests were changed.

## Previous state

- Development-only.
- 6 France articles.
- 18 EN / UK / RU translations, all `NeedsReview`.
- 0 France Compare facts.
- Previous blockers: `residence-filing`, `housing-initial`.
- Prior disposition: **PARTIAL INTEGRATION ACCEPTED — KEEP DEVELOPMENT-ONLY**.

## Blocker 1 — `residence-filing`

**Status: RESOLVED for the existing six-article Core scope, with the qualification below.** This does not establish a France-wide right or a universal procedure for changing from temporary protection to an ordinary permit.

### Confirmed claim and safe wording

Ordinary residence permits have route-specific eligibility requirements. An official Seine-Maritime prefecture page, updated 16 January 2026, describes a France-in-country change-of-status procedure for Ukrainian temporary-protection holders residing in that department, for listed work, study, business, and family grounds. It directs applicants to a route based on their residence area and provides local filing channels. The page says APS holders must renew their APS under the current procedure while the ordinary application is examined. An ordinary permit is issued only if its separate conditions are met; temporary protection does not convert automatically.

**Safe publication wording:** “Some prefectures publish local procedures for Ukrainian temporary-protection holders who want to apply for an ordinary residence permit from France. The available grounds, documents, filing channel, and treatment of the temporary-protection document depend on the permit and the prefecture responsible for your residence. Check that prefecture’s current instructions before applying. A pending application does not, by itself, establish a nationwide extension of your stay or work rights, and no automatic change of status is guaranteed.”

For a practical example only: Seine-Maritime’s page gives local postal / Démarches Numériques / APS-renewal-appointment options for the listed grounds, says that this specific change-of-status procedure cannot be filed through ANEF, and instructs applicants to continue renewing the APS during review. Do not transfer those channels or instructions to another department or to another permit category.

### Official sources checked 14 November 2026

- [Seine-Maritime Prefecture — Ukrainian temporary-protection holders applying for an ordinary residence permit](https://www.seine-maritime.gouv.fr/index.php/layout/set/print/Demarches/Etrangers-en-Seine-Maritime/Je-demande-un-titre-de-sejour-ou-un-document-de-voyage-en-Seine-Maritime/Ukraine-Protection-temporaire-Ukrayina-timchasovu-ohoronu-Ukraine-Temporary-protection/Je-souhaite-basculer-vers-le-droit-commun/Je-souhaite-demander-un-titre-de-sejour-de-droit-commun) — page states updated 16 January 2026; applies to Ukrainian residents of Seine-Maritime and the grounds/channels it lists.
- [Seine-Maritime Prefecture — general renewal and change-of-status navigation](https://www.seine-maritime.gouv.fr/Demarches/Etrangers-en-Seine-Maritime/Je-demande-un-titre-de-sejour-ou-un-document-de-voyage-en-Seine-Maritime/Je-souhaite-demander-un-titre-de-sejour/Je-souhaite-renouveler-mon-titre-de-sejour-ou-changer-de-statut) — checked 14 November 2026. Its generic ANEF list appears broader; the Ukraine-specific change page expressly excludes ANEF for that local change procedure. Use the procedure-specific page for that route and do not infer a general ANEF rule.

### What remains unknown / prohibited

- No nationwide APS-BPT-to-ordinary-permit filing procedure was confirmed.
- The Seine-Maritime example does not establish that every temporary-protection beneficiary or every permit category can file in France.
- Filing place, channel, deadline, document list, eligibility, and any visa exception must be checked for the chosen route and responsible prefecture.
- The nationwide effect of a pending ordinary application after APS expiry, including stay and work rights, is not established. A generic ANEF deposit receipt must not be described as a residence document or a bridge.
- Do not promise approval, an automatic switch, or permanent residence.

## Blocker 2 — `housing-initial`

**Status: EXCLUDED from the existing six-article Core set.** The national TP-specific initial-accommodation intake gap remains open for any future housing article or Compare fact. No France-wide TP placement route or entitlement is asserted in this Core set.

### Confirmed claim and safe wording

The national Service-Public page, verified 6 August 2026, says emergency accommodation is available for people in precarious circumstances, identifies the 115 as the free, 24-hour Samu social contact, and directs people to the mairie or Samu social for local contacts. This is the general homelessness emergency route; it is not a TP-specific application channel and does not guarantee a bed.

A Haute-Garonne Prefecture Ukraine page, updated 9 September 2026, says its local welcome process can orient people who need accommodation toward emergency accommodation and then temporary housing. It describes local association-run collective places, citizen-solidarity accommodation with association support, and social support toward/within housing. It also says the prefecture gathers offers through local authorities, especially Toulouse Métropole. The page does not publish a complete request or assessment procedure, guarantee availability, or set a nationwide model.

**Safe publication wording:** “If you have nowhere safe to sleep, call 115 for the general emergency-housing service; it can assess and orient you, but accommodation is not guaranteed. For Ukraine-specific accommodation support, check the current official prefecture or departmental Ukraine page for where you are. Any described offer is local and depends on the arrangements and places available there.”

### Official sources checked 14 November 2026

- [Service-Public — Emergency and social accommodation options (F2003)](https://www.service-public.gouv.fr/particuliers/vosdroits/F2003) — verified 6 August 2026; lists the general emergency route, 115, and varying accommodation duration.
- [Haute-Garonne Prefecture — Ukraine: State services in the department](https://www.haute-garonne.gouv.fr/index.php/Actualites/Ukraine-mobilisation-des-services-de-l-Etat-dans-le-departement) — updated 9 September 2026; local orientation description. This page also retains older crisis-era material; only the dated 2026 update is used here. Its older 2022 hosting and “sas” material is not current-rule evidence.

### What remains unknown / prohibited

- The reviewed current official publications do not establish one national TP-specific intake channel for a new beneficiary needing accommodation, a nationwide allocator, or uniform eligibility/assessment criteria.
- Availability, timing, location choice, placement duration, contributions, continuation, and re-entry are not established nationally.
- The Haute-Garonne description cannot be generalized to other departments. It does not itself provide a complete practical application route or promise a place.
- Do not conflate general 115/emergency homelessness services with asylum reception accommodation, Ukraine-specific temporary-protection support, or ordinary social housing.
- Do not promise State accommodation, a bed, a chosen location, automatic social housing, or continuation of a placement.

## Content audit

The six Article IDs and categories were checked against the drafts and production catalog. All six drafts remain the latest post-correction versions identified by `POST_CORRECTION_EDITORIAL_REVIEW.md`. Catalog titles, summaries, and content for EN / UK / RU match those drafts exactly; all citations resolve to source records owned by the respective France article. The six article-specific post-correction statuses remain PASS.

| Article ID | Article | Content audit |
| --- | --- | --- |
| `d16ad009-3391-5e3b-80b2-5b0d348503d3` | FR-TP-01 — temporary protection application | PASS — covers protection eligibility/application/document dates; excludes ordinary-residence filing advice. |
| `82179b12-c3bd-5bf1-be0a-374502fc0386` | FR-EMP-01 — finding a job | PASS — no promise of a job, training, funding, or ordinary residence transition. |
| `084c5497-7b96-59a2-9d9a-4b0be9b5a2ff` | FR-EMP-02 — work rights | PASS — work permission remains tied to a currently valid APS-BPT; no ordinary-permit filing claim. |
| `ffc21bf8-901c-5ec3-9f54-fce1dc797974` | FR-HEALTH-01 — healthcare | PASS — no asylum accommodation rule is transferred to temporary protection. |
| `0a1c276b-9a49-5bf5-8be8-104974dd0e5e` | FR-EDU-01 — education | PASS — the school-access statement is separate from a residence-permit application or filing route. |
| `8d7dd894-f0c5-53f4-831c-4c936f27fdff` | FR-SOC-01 — social assistance | PASS — emergency-help wording does not promise housing or a placement. |

Neither `residence-filing` nor `housing-initial` is among the 18 aspects covered by these six Core articles. The drafts do not introduce a nationwide ordinary-permit filing route, pending-application bridge, TP-specific accommodation intake, or guaranteed housing claim. No additional substantive blocker was found in this scoped audit.

## Translation audit

All 18 production translations exist: EN / UK / RU for each of the six Article IDs. Exact production-to-draft comparison passed for every title, summary, and content field. The 18 translations remain `NeedsReview`; no status was changed.

The prior post-correction review records all six articles as PASS and finds the correction claims equivalent across EN / UK / RU, including the dates, figures, eligibility conditions, and legal qualifications reviewed there. This gate’s targeted read found no added or strengthened guarantee concerning residence filing or initial housing. The new safe wording above is research-gate guidance and has not been inserted into any article or production translation.

## Remaining blockers

- No blocker remains for the **existing six-article Core set to proceed to final human/editorial review**.
- `housing-initial` remains excluded from this set and blocked for any future France housing article or Compare fact that needs a TP-specific national intake, placement, or entitlement claim.
- Any future article that gives practical ordinary-permit filing instructions must be scoped to a named route and current prefecture instructions. The national filing/bridging questions listed above remain unestablished.
- The final human/editorial review must retain `NeedsReview` until accepted and refresh time-sensitive legal/service details before any separate Production promotion. This gate itself does not authorize promotion.

## Verdict

**READY FOR FINAL CONTENT REVIEW** — limited to the existing six France Core articles and their 18 EN / UK / RU translations. This is not Production approval, does not close the future `housing-initial` evidence gap, and does not approve France Compare integration.

## Checks

- Both previous blockers have explicit dispositions and safe publication boundaries.
- Unconfirmed nationwide filing, pending-application, and housing-entitlement claims are excluded.
- Temporary protection, ordinary residence, asylum reception, emergency homelessness help, and social housing are kept distinct.
- 6 Article IDs / 18 translations / `NeedsReview` statuses / draft-to-catalog parity / citation ownership: PASS.
- New official HTTPS URLs: **4 unique**.
- Only this readiness gate was created; no draft, production record, runtime, test, status, commit, or push was changed.
