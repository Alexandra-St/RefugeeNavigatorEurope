# Italy v1 Editorial Acceptance Preparation

**Baseline:** `4864fa811b4c0bfb6f746018844f6c87bb21eb55`  
**Review date:** 2026-11-09  
**Overall verdict:** **NEEDS CORRECTION BEFORE HUMAN ACCEPTANCE**

This report is not owner acceptance.

**Scope:** Eight Italy drafts and their research packages; integrated Italy catalog records; 24 Italy comparison facts; 70 Italy `OfficialSources`; comparison mapping; visibility; and existing Netherlands and Sweden hold invariants.

## 1. Batch snapshot

| Italy catalog item | Count / state |
|---|---:|
| Countries | 1 |
| Articles | 8 |
| Translations | 24; all 24 `NeedsReview` |
| `OfficialSources` | 70 |
| Comparison facts | 24; all 24 `Draft` and `IsAvailable=true` |
| Canonical aspects represented | 24/24 |
| Development visibility | Visible |
| Production visibility | Hidden |

Global catalog state recorded in Phase 23.12:

| Global catalog item | Count / state |
|---|---:|
| Countries | 12 |
| Articles | 96 |
| Translations | 288: 261 `Verified`, 27 `NeedsReview` |
| `OfficialSources` | 793 |
| Comparison facts | 288: 259 `Approved`, 29 `Draft` |
| `IsAvailable=true` | 281 |

## 2. Article acceptance matrix

| Article | Article ID | Category | Sources | Translation/catalog parity | Claim support | Editorial result |
|---|---|---|---:|---|---|---|
| IT-TP-01 | `c8c06066-d3cd-55ce-88b0-3499384eece7` | temporary-protection | 5 | Pass | Supported | Ready |
| IT-TP-02 | `28aee635-27ff-525c-9cd3-8673b5ba2be3` | temporary-protection | 7 | Pass | Supported | Ready |
| IT-EMP-01 | `39091eb7-04c7-5b45-83b7-585f5f1ce719` | employment | 8 | Pass | Supported | Ready |
| IT-EMP-02 | `d988941a-e709-53ba-a2bf-47f1ac2f9956` | employment | 10 | Pass | Supported | Ready |
| IT-HEALTH-01 | `462ace7d-8deb-5cd7-a530-548c79a8e144` | healthcare | 9 | Pass | Supported | Ready |
| IT-HOUSING-01 | `5aafddcb-e302-52b7-9365-f95e7261d3a4` | housing | 10 | Pass | Supported | Ready |
| IT-EDU-01 | `ba3d3110-730d-5340-a89a-2b6722430bdd` | education | 9 | Pass | Supported | Ready |
| IT-SOC-01 | `55696570-3337-5e2f-8a24-71e256e869e9` | social-assistance | 12 | Pass | Supported | Ready |

**Article totals:** Ready 8 · Minor correction 0 · Correction required 0.  
**Category follow-up:** IT-HEALTH-01 → `healthcare`: PASS.

## 3. Cross-language review

- Article translations: 24/24 materially equivalent to the English draft and in parity with the catalog.
- Material article translation issues: 0.
- B-C02 below concerns a comparison-fact translation, not an article translation.

## 4. Article-specific findings

There are no article-level findings. All eight articles are Ready based on Phase 23.13A.

## 5. Comparison acceptance matrix

Every fact is in `Draft` state and available. Source/language results below refer to fact-level support and EN/UK/RU meaning.

| Aspect | Linked article | State | Source / language result | Classification |
|---|---|---|---|---|
| protection-scheme | IT-TP-01 | Draft / available | Sources resolve; eligibility qualification omitted in EN/UK/RU | Correction required |
| protection-application | IT-TP-01 | Draft / available | Relevant sources; language parity pass | Ready |
| protection-dates | IT-TP-01 | Draft / available | Relevant sources; language parity pass | Ready |
| residence-routes | IT-TP-02 | Draft / available | Relevant sources; language parity pass | Ready |
| residence-conditions | IT-TP-02 | Draft / available | Relevant sources; language parity pass | Ready |
| residence-filing | IT-TP-02 | Draft / available | Relevant sources; language parity pass | Ready |
| employment-vacancies | IT-EMP-01 | Draft / available | Relevant sources; language parity pass | Ready |
| employment-institutions | IT-EMP-01 | Draft / available | Relevant sources; language parity pass | Ready |
| employment-assistance-conditions | IT-EMP-01 | Draft / available | Relevant sources; language parity pass | Ready |
| work-permission | IT-EMP-02 | Draft / available | Relevant sources; language parity pass | Ready |
| work-employer-duties | IT-EMP-02 | Draft / available | Relevant sources; language parity pass | Ready |
| work-minimum-pay | IT-EMP-02 | Draft / available | Relevant sources; language parity pass | Ready |
| healthcare-access | IT-HEALTH-01 | Draft / available | Relevant sources; language parity pass | Ready |
| healthcare-proof | IT-HEALTH-01 | Draft / available | Relevant sources; language parity pass | Ready |
| healthcare-urgent | IT-HEALTH-01 | Draft / available | Relevant sources; language parity pass | Ready |
| housing-initial | IT-HOUSING-01 | Draft / available | Relevant sources; language parity pass | Ready |
| housing-private-rental | IT-HOUSING-01 | Draft / available | Tenancy sources do not support landlord-acceptance sentence; translations align | Minor correction |
| housing-financial-support | IT-HOUSING-01 | Draft / available | Relevant sources; language parity pass | Ready |
| education-school-enrolment | IT-EDU-01 | Draft / available | Relevant sources; language parity pass | Ready |
| education-pupil-language | IT-EDU-01 | Draft / available | UK/RU strengthen the English qualification | Correction required |
| education-higher-admission | IT-EDU-01 | Draft / available | Relevant sources; language parity pass | Ready |
| social-subsistence | IT-SOC-01 | Draft / available | Relevant sources; language parity pass | Ready |
| social-application | IT-SOC-01 | Draft / available | Relevant sources; language parity pass | Ready |
| social-child-benefits | IT-SOC-01 | Draft / available | Relevant sources; older/current INPS distinction preserved | Ready |

**Comparison totals:** checked 24/24 · Ready 21 · Minor correction 1 · Correction required 2.

## 6. Source ownership and relevance

The Italy catalog-source audit is clean: 70/70 checked; ownership mismatches 0; UUID collisions 0; URL/provenance mismatches 0; title/source-identity mismatches 0; `LastCheckedAt` mismatches 0; non-HTTPS URLs 0.

Fact-level source relevance identified B-C03 below. It is not a source ownership or provenance failure.

## 7. Cross-topic consistency

**Blocking cross-topic conflicts: 0.** The review checked:

- TP status, application, and physical permit distinctions;
- the 4 March 2028 TP endpoint versus the SSN special-framework date of 31 December 2026;
- TP work access versus ordinary residence routes;
- the specific TP application `ricevuta` versus other immigration receipts;
- CPI/SIISL assistance versus NASpI;
- reception and housing support versus cash welfare;
- compulsory schooling boundaries;
- AUU wording and the difference between current and older INPS guidance.

## 8. Existing hold verification

**Netherlands: PASS.**

- Translations: 21 `Verified`, 3 `NeedsReview`.
- Comparison facts: 20 `Approved`, 4 `Draft`.
- `protection-scheme`, `protection-application`, `protection-dates`, and `residence-filing` remain `Draft`.
- `residence-filing`: `IsAvailable=false`; `OfficialSourceIds=[]`.

**Sweden: PASS.**

- `social-child-benefits`: `Draft`; `IsAvailable=false`; `OfficialSourceIds=[]`.

## 9. Required corrections

### B-C01 — Correction required

**Aspect:** `protection-scheme` · **Article:** IT-TP-01 · **Languages:** EN/UK/RU

The fact refers broadly to “Ukrainian-residence-permit cases” and omits the narrower conditions. The relevant group requires a permanent residence permit in Ukraine and inability to safely return. Add both conditions in EN/UK/RU.

### B-C02 — Correction required

**Aspect:** `education-pupil-language` · **Article:** IT-EDU-01 · **Languages:** UK/RU

The English fact says Ukrainian online schooling alone is “not established as a substitute”; UK/RU make the stronger categorical claim that it “does not replace” Italian compulsory education. Research establishes only that substitution is not established. Restore that qualified meaning in UK/RU.

### B-C03 — Minor correction

**Aspect:** `housing-private-rental` · **Article:** IT-HOUSING-01 · **Languages:** EN/UK/RU

The fact says TP does not guarantee landlord acceptance, but the cited Laws 431/1998 and 392/1978 support tenancy rules rather than that statement. Remove the landlord-acceptance sentence.

## 10. Human acceptance readiness

Italy v1 is **NOT** ready for human/owner acceptance until B-C01 and B-C02 are corrected and B-C03 is resolved. Article content itself passed editorial audit. Required changes are limited to these three comparison facts. After correction, a focused post-correction review is required before owner acceptance.

**Final verdict: NEEDS CORRECTION BEFORE HUMAN ACCEPTANCE**
