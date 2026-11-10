# Lithuania v1 Comparison Post-Correction Review

**Baseline:** `a3dea84b49bdc3dbf8bcf5c0b0ace643f1a53c1b`  
**Previous acceptance audit:** `docs/content-drafts/lithuania-v1/EDITORIAL_ACCEPTANCE_PREPARATION.md`  
**Correction comparison:** `d20b106a1339849de3c094833d51b3ace431f122` → `a3dea84b49bdc3dbf8bcf5c0b0ace643f1a53c1b`  
**Review scope:** LT-C01 / LT-C02 / LT-C03; comparison facts, their source ownership, correction change scope, and carried-forward Lithuania/global invariants. No new research was performed. No catalog integration or status change was made.

## LT-C01

**Result: Resolved.** All 24 Lithuania facts were checked in EN / UK / RU (72/72 values). None has a trailing plain-text source-label tail; no URL or `[[source:...]]` token is embedded in a value. `OfficialSourceIds` remain the separate attribution mechanism.

For the 22 facts outside LT-C02 and LT-C03, each correction-commit value is byte-for-byte the prior factual wording after removing its terminal source labels. The fact source arrays and metadata for those 22 facts are unchanged. Contaminated values: **0/72**.

## LT-C02

**Result: Resolved — `social-application`.** EN / UK / RU now describe the application route: applicants use the relevant municipality and, where applicable, SPIS or direct municipal filing; the municipality assesses scheme-specific evidence and eligibility and decides; SPIS is only an application channel and creates no entitlement. Requirements vary by scheme. Sodra insurance claims are separate, and applying does not guarantee an award. The fact does not revert to a child-benefit category list.

The three language versions convey the same authority, channel, decision, qualification, insurance separation, and non-guarantee boundaries.

## LT-C03

**Result: Resolved — `social-child-benefits`.** EN / UK / RU cover the general child benefit; conditional supplements; separate birth, pregnancy and childcare benefits with their own rules; and Sodra insured maternity, paternity and childcare benefits as a separate insurance system. The limited Ukraine-related scope covers conditional one-time accommodation/settlement support, a separate TP rent-compensation route, and preschool-cost compensation up to six months. It retains status, lease, child, income and municipal qualifications, local variation, no guaranteed award, and the warning not to assume historical emergency programmes remain open. No exhaustive benefit catalogue is implied.

The three language versions preserve the same benefit distinctions, limited programme scope, conditions, and application-versus-entitlement boundary.

## Correction change-scope verification

The diff from the correction parent `d20b106a1339849de3c094833d51b3ace431f122` to correction commit `a3dea84b49bdc3dbf8bcf5c0b0ace643f1a53c1b` contains only:

- `src/RefugeeNavigator.Web/Data/comparison-facts.json`

No country, article, translation, OfficialSources catalog, topic map, localization, draft, research, test, code, or release-record file changed. No comparison fact status or availability changed anywhere in that correction diff. The only source-array changes are the intended arrays for `social-application` and `social-child-benefits`.

## Lithuania final fact state

- Lithuania facts: **24**; all `Draft`: **24**; `Approved`: **0**; `IsAvailable=true`: **24**.
- Canonical mapped aspects: **24/24 exactly once**.
- EN / UK / RU values: **72/72 non-empty**; terminal source-label contamination: **0/72**.
- Fact-to-article source ownership: **PASS 24/24**. Every listed source resolves in `official-sources.json` and its `InformationEntryId` matches the fact’s owning article.
- Language parity: **PASS 24/24**. LT-C02 and LT-C03 were checked for semantic parity across all three values; the previous acceptance audit’s parity for the other facts remains intact after mechanical tail removal.

## Source relevance and ownership

**LT-C02 `social-application`: PASS, 4/4.** The exact set is `c57b0f26-f706-510a-b22c-b2255a79bc99`, `ad9628c2-0f84-52c4-9b58-48f6783233f8`, `a050f8a7-09ce-5af0-9c34-826da705905c`, and `9e961b2a-50fa-5dc8-9374-a49dee9fbff4`. All four exist and belong to LT-SOC-01. SADM’s low-income guidance and detailed cash-assistance rules support the municipal/social-assistance context; the SPIS information supports its use as an application channel; Sodra’s Ukraine benefits guidance supports the separate insurance-claim route. The combined set supports the complete bounded wording.

**LT-C03 `social-child-benefits`: PASS, 8/8.** The exact set is `69321373-30b6-50fc-92d6-6b5f94b0de32`, `e179bb12-cd2b-5264-892a-36eef1a4a71b`, `9e961b2a-50fa-5dc8-9374-a49dee9fbff4`, `ab65a176-c2e3-5fc1-81d1-0be540993a94`, `61117a42-3c5d-5ad3-9064-44ee0cda1edd`, `f4d568e0-18a5-5988-8e7d-ce4d5984d0c5`, `f19d66e3-a034-5493-befc-d739a06d390c`, and `ed936571-5140-5579-9c3f-f6143ebd8c23`. All eight exist and belong to LT-SOC-01. SADM family-benefit guidance supports general and conditional benefits; Sodra supports the insurance distinction; SADM’s Ukraine guidance and 2026 status guide support limited Ukraine measures and conditions; the Foreigners Law and housing guidance support the separate TP rent route; the municipal directory supports local variation/contact qualification. Each source is relevant to at least one included claim; the set jointly supports the complete bounded fact.

No irrelevant or foreign-owned UUIDs were found in either set.

## Cross-language verification

EN / UK / RU were checked for equivalent application/decision roles, benefit categories, conditions, separate Sodra insurance, Ukraine-specific limits, and non-guarantee language. Source IDs are stored separately; none of the 72 values contains an inline citation token, URL, or source-label tail.

## Existing hold verification

**Netherlands: PASS.** Facts remain 20 `Approved` / 4 `Draft`. `residence-filing` remains `Draft`, `IsAvailable=false`, `OfficialSourceIds=[]`.

**Sweden: PASS.** `social-child-benefits` remains `Draft`, `IsAvailable=false`, `OfficialSourceIds=[]`.

## Carried-forward article and global state

The previous acceptance audit’s article assessment carries forward: **8/8 Articles Ready**, **24 translations NeedsReview**, **71 article-owned OfficialSources**, and **222/222 inline references resolved**. Correction commit changed none of those records. No full article audit was repeated.

Global invariants, confirmed from the current checked-in structured data and previous audit:

- Countries: **13**
- Articles: **104**
- Translations: **312** — **285 Verified**, **27 NeedsReview**, **0 Outdated**
- OfficialSources: **864**
- Comparison facts: **312** — **283 Approved**, **29 Draft**, **305 available**, **7 unavailable**

Correction commit test evidence: `dotnet test` **634/634 PASS**; CI **PASS**. A full test rerun was not needed for this documentation-only review after the targeted structural checks passed.

## Final acceptance state

- Articles Ready: **8/8**
- Facts Ready: **24/24**
- Unresolved corrections: **0**
- Unresolved blockers: **0**

## Verdict

**READY FOR OWNER ACCEPTANCE**

This review is not owner acceptance. It does not promote statuses or authorize deployment.
