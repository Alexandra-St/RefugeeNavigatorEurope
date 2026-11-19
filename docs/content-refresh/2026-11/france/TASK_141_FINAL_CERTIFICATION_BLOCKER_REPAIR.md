# Task 141 — France final certification blocker repair

As-of: 19 November 2026

## Result

**FRANCE CERTIFICATION BLOCKERS RESOLVED** — all four Task 140 blocker categories are closed in staging. The package is ready for Task 142, a second independent certification. This is not approval for production integration.

## Blocker repairs

1. **Compare certification (12/24):** audited the 12 previously uncertified aspects against exact tuples already present in the official-source audit and exact Core claim mappings where applicable. All twelve are now certified. Added four explicit tuples: `social-subsistence` × `d9b295aa-1f88-5757-8f43-2ac5380c3323` (Core C028/C032), `social-application` × `0e30784a-4434-5fd1-8ff8-6d09c8bbc9f1` (Core C010/C046), `education-school-enrolment` × `8e80f891-0c43-5ad7-a308-c54c4c55d097` (Core C005), and `education-higher-admission` × `ed6beedb-b5a5-5853-9c02-70596459860c` (Core C029/C031/C033/C040). All four are SUPPORTED. No new Compare-only source record was needed. See `COMPARE_CERTIFICATION_REPAIR_01.md` for all 24 propositions and tuple results.
2. **Malformed FR-TP-02 fragments:** inactivated `FR-TP-02-C004` (`CESEDA Article L.`), `FR-TP-02-C005` (`412-1 CESEDA Article L.`), and `FR-TP-02-C006` (`412-2 CESEDA Article L.`) as non-propositions. Original text and graph rows remain for audit; all draft handle indexes now mark them inactive. No merge was appropriate.
3. **Seven ledger/copy mismatches:** added the ledger propositions to EN/UK/RU review copy with their audited citations: C020 adviser job-search steps; C022 Ukraine-specific France Travail information/form; C033 EURES source-language listings; EMP-02-C027 regulated-profession requirements; SOC-01-C017 allocations familiales general child threshold; SOC-01-C026 applicant and spouse/partner resource disclosure; SOC-01-C034 PAJE component-specific conditions. Translations preserve the proposition scope and qualifiers. No divergence remains.
4. **Missing FR-EMP-02 source binding:** added `6ed80718-151e-52d3-bebd-dd8f9eb07d38` to the FR-EMP-02 copy and active ledger candidate list for `FR-EMP-02-C027`; source is retained and inventory CoreMappingTotals is now 1.

## Recalculated claim and evidence state

- Active claims: 172; by article: FR-TP-01 20, FR-TP-02 21, FR-EMP-01 17, FR-EMP-02 19, FR-HEALTH-01 20, FR-HOUSING-01 26, FR-EDU-01 22, FR-SOC-01 27.
- Materiality: MATERIAL 117; SUPPORTING 55. NumericDate YES 36; NO 136.
- Temporal class: CURRENT 165; FUTURE 0; HISTORICAL 0; TRANSITION 7.
- Active compound claims: 0; active non-propositional claims: 0; duplicate active ClaimIds: 0; orphan lineage rows: 0.
- Active evidence coverage: COVERED 145; COVERED — MULTIPLE SOURCES 27; unsupported 0; requires source review 0.
- Effective active Core evidence graph: 418 unique tuples after selecting the latest adjudication per ClaimId/SourceId pair; SUPPORTED 205; NOT_SUPPORTED 213; unresolved 0. The source audit retains its original historical 561 rows; inactive-claim rows and superseded repeat decisions are excluded from the effective graph. There are 20 historical repeated rows for active pairs; 0 duplicate effective tuples.
- Unsupported MATERIAL claims: 0; unsupported NumericDate claims: 0; open reopens: 0; Core dangling SourceIds: 0; Compare dangling SourceIds: 0; owner conflicts: 0.
- FR-TP-02 NumericDate cleanup: C005 and C006 removed from active NumericDate count; `FR-EMP-01-C033` reclassified NumericDate NO because its proposition is linguistic, not numeric/date.

## Compare, sources, bindings and Watch

- Compare: source-certified 24/24; blocked 0; `REQUIRES_SOURCE_REVIEW` 0. Newly certified aspects: protection-scheme, protection-application, residence-routes, residence-conditions, residence-filing, employment-vacancies, employment-institutions, employment-assistance-conditions, work-permission, work-employer-duties, social-subsistence, social-application.
- Primary Compare dispositions: KEEP 12; REVISE 12; UNAVAILABLE 0; NEWLY_AVAILABLE 0. Secondary NEWLY_AVAILABLE marker: 1 (protection-dates). Compare binding delta: KEEP 59; ADD 15; REMOVE 12. New tuples 4, SUPPORTED 4, NOT_SUPPORTED 0; new Compare-only source ADDs 0.
- France OfficialSources: production 104; staged 117; KEEP 94; ADD 13; UPDATE 8; RETAIN_HISTORICAL 2; REMOVE 0. Planned France total 117; global total 1,543. ADD SourceIds: 56c5f996-f13c-5df1-9cd2-ca2858889e6d, fecb425a-243b-5233-871d-2ce26b742a66, 3a55eb20-dea4-5b00-bdef-7d932cf1fa31, d277a456-4e10-5a6c-849a-e23ba7474991, bf5e5f83-b27d-5b60-aa2d-9e5a924b342e, fd583be7-629f-5004-a38d-136e1a85b2d4, 06d80225-6dde-5d61-94c8-48feac8d8e67, 6ed80718-151e-52d3-bebd-dd8f9eb07d38, 9003daa2-1315-58ef-8237-e8147d3a5592, 77766134-f03b-5576-89df-6f38a08ed93c, ea7b16e1-7abc-5ebc-9521-2d45289a040d, a8de9820-9b53-5b98-b1e7-b89cb427f71d, c42ac694-fe38-52e6-b619-3f6b18542d89.
- Core binding delta after citation repair: KEEP 79; ADD 13; REMOVE 25.
- Watch unchanged: 14 total; final APPROVE 11; REMOVE 3; historical actions APPROVE/EDIT/REMOVE 9/2/3; gap resolution 0; stale or inactive approved ClaimIds 0.

## Review and regression checks

All 8 article drafts were rechecked for EN/UK/RU material proposition alignment. Missing/extra active proposition divergences: 0; material divergences: 0; numeric/date divergences: 0. The seven targeted propositions now appear in all three language copies with source links.

High-risk regression checks pass: EU temporary-protection extension (5 March 2027–4 March 2028); APS six-month validity and renewal; 5 August 2026 scoped rule; work permission distinct from France Travail and ARE; SMIC figures and geographic scope; PUMa decree dependency; emergency numbers; housing non-guarantees; education age distinctions; Social scheme scoping.

No production or canonical JSON, tests, other countries, commits, pushes, CI, Docker or releases were changed or run.

## Remaining blockers and next step

Remaining Task 140 substantive blockers: 0. Task 142 should independently verify this repaired staging package and decide whether it is ready for integration.
