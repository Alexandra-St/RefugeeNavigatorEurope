# Italy v1 Post-Correction Editorial Review

**Review baseline:** `b4c31389d7675d559a306087d07e943c24ebf3d4`  
**Previous audit:** `docs/content-drafts/italy-v1/EDITORIAL_ACCEPTANCE_PREPARATION.md`  
**Review scope:** Only findings B-C01, B-C02, and B-C03; correction commit scope; Italy/global catalog counts; and Netherlands/Sweden hold invariants.

## B-C01 — `protection-scheme`

**Result: Resolved.**

EN, UK, and RU now preserve both conditions for the relevant third-country national group: a permanent residence permit in Ukraine and inability to return safely to the country or region of origin. Their eligibility meaning is materially aligned, and the prior qualification is restored.

The fact remains mapped to article `c8c06066-d3cd-55ce-88b0-3499384eece7`, with `Status=Draft`, `IsAvailable=true`, and the original `OfficialSourceIds` unchanged.

## B-C02 — `education-pupil-language`

**Result: Resolved.**

EN retains the qualified statement that Ukrainian online schooling alone is not established as a substitute for Italian compulsory education. UK and RU express the same evidentiary qualification; neither states a categorical prohibition. The original first sentence about Italian-language support is unchanged in both translations.

The fact remains mapped to article `ba3d3110-730d-5340-a89a-2b6722430bdd`, with `Status=Draft`, `IsAvailable=true`, and the original `OfficialSourceIds` unchanged.

## B-C03 — `housing-private-rental`

**Result: Resolved.**

The unsupported landlord-acceptance sentence has been removed from EN, UK, and RU. The remaining text is limited to contract-dependent tenancy rules and the Law 392/1978 deposit ceiling for residential leases within that law’s scope. It makes no claim of guaranteed landlord acceptance or rejection.

The fact remains mapped to article `5aafddcb-e302-52b7-9365-f95e7261d3a4`, with `Status=Draft`, `IsAvailable=true`, and the original `OfficialSourceIds` unchanged.

## Change-scope verification

The diff for correction commit `b4c31389d7675d559a306087d07e943c24ebf3d4` changes only `src/RefugeeNavigator.Web/Data/comparison-facts.json`. Exactly three Italy fact `Values` objects changed: `protection-scheme`, `education-pupil-language`, and `housing-private-rental`.

No article drafts, catalog translations, `OfficialSources`, fact source arrays, statuses, availability values, article mappings, or application code changed in that commit.

## Italy state

| Item | Verified state |
|---|---:|
| Italy countries | 1 |
| Articles | 8 |
| Translations | 24; all `NeedsReview` |
| `OfficialSources` | 70 |
| Comparison facts | 24; all `Draft` |
| Available facts | 24 |
| Canonical aspects | 24/24 |
| Development visibility | Visible |
| Production visibility | Hidden |
| Articles Ready | 8/8, per the previous editorial audit |
| Facts Ready after this review | 24/24 |

## Global invariants

| Item | Verified state |
|---|---:|
| Countries | 12 |
| Articles | 96 |
| Translations | 288 |
| Sources | 793 |
| Comparison facts | 288 |
| Approved facts | 259 |
| Draft facts | 29 |
| Available facts | 281 |

**Netherlands hold: PASS.** `residence-filing` remains Draft, `IsAvailable=false`, and `OfficialSourceIds=[]`.

**Sweden hold: PASS.** `social-child-benefits` remains Draft, `IsAvailable=false`, and `OfficialSourceIds=[]`.

## Validation evidence

The correction baseline recorded 627/627 tests passing. This full test suite was not rerun for this focused review. The post-correction structural comparison confirms that only the three specified Italy fact values differ from the correction baseline, with mappings and source arrays preserved.

## Verdict

**READY FOR OWNER ACCEPTANCE**

- Articles Ready: 8/8
- Facts Ready: 24/24
- Unresolved blockers: 0
- Unresolved corrections: 0
- Unresolved minor corrections: 0

This review is not owner acceptance. No statuses were promoted.
