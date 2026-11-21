# Task 186 — Romania structural blocker repair

As of 2026-11-20. This task repaired Task-185 structural gaps only. No production change, source adjudication, test, commit, or push was performed.

## Blockers repaired

- Added active `RO-EDU-01-C006` for the covered-minor school-access cohort. The temporary-protection phrase is an eligibility qualifier within this atomic access proposition, not a separate proposition.
- Added active `RO-EDU-01-C007` for the local inspectorate's placement-coordination role.
- Added inactive `RO-EMP-02-C008` and active children `RO-EMP-02-C008-S01` (employer-side formalities) and `RO-EMP-02-C008-S02` (job-specific conditions). The children use distinct candidate sources.
- All three orphan source propositions now have active ClaimIds and review-copy cards. The additional compound was recorded in the atomicity audit as discovered during Task 185.

## Translation alignment

- `TRANSLATION_ALIGNMENT_01.csv` contains one row for each of 94 active ClaimIds and sentence/claim spans in EN, UK-UA and RU. Review copies contain the same claim-specific alignments.
- `RO-EDU-01-C006`: UK-UA/RU describe defined/certain children from Ukraine but do not expressly name children under temporary protection. Source audit must confirm the cohort scope.
- `RO-HOUSING-01-C001`: RU describes special-program availability/capacity but omits EN/UK-UA eligibility and designated-setting scope. Source audit must confirm the legally covered population and setting scope.
- The RU school-placement limitation and RU employment-service registration omission are supplied as direct translated wording in review copies only. Canonical translations remain unchanged.

## Rebuilt mappings and counts

- ACTIVE claims 94; MATERIAL/SUPPORTING 74/20; NumericDate 12; temporal 88/2/0/4.
- Candidate source tuples 130, PENDING_SOURCE_REVIEW 130; coverage 94; zero-candidate 0.
- Compare aspects/tuples 24/67; ownership violations 0; all valid tuples pending. Watch candidates 5; high-risk claims 74.
- Four source batches remain 56/56 assigned. OfficialSources/Core unchanged at 56/56.

## Certification

No duplicate ACTIVE ClaimIds, orphan lineage, active split parents, active compounds, duplicate active locators, missing active review-copy cards, orphan factual propositions within the three repaired passages, stale Watch references, or Compare ownership violations were found. Two precisely mapped translation-scope reopens remain, which Task 186 allows to proceed to source audit.

**ROMANIA STRUCTURAL CERTIFICATION COMPLETE — READY FOR SOURCE AUDIT**
