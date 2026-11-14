# France Final Content Review 01

**Review date:** 14 November 2026
**Scope:** The six existing France V1 drafts, their 18 EN / UK / RU versions, Production Readiness Gate 02, the post-correction editorial review, the partial integration audit, and France records in the current Development catalog. No new research or Production promotion was performed.

## Review method and shared results

- Confirmed all 6 expected Article IDs, their France country identity, and category mappings.
- Checked the 18 language sections for equivalent conditions, exclusions, figures, dates, and official-source reference IDs. Source reference order is the same across EN / UK / RU for every article; localized number punctuation is preserved.
- Confirmed all cited IDs resolve to official-source records owned by the same article. The 80 source records match the draft source-set URLs and article owners.
- Compared Production `Title`, `Summary`, and `Content` for all 18 translations against the current drafts. Five articles match exactly. `FR-TP-01` differs only because of the correction recorded below.
- All 18 Production translation statuses remain `NeedsReview`.
- Gate 02’s residence-filing boundary is preserved: the drafts do not claim a national or universal ordinary-permit filing procedure, a pending-application bridge, or an automatic switch from temporary protection.
- Gate 02’s housing exclusion is preserved: the six drafts do not claim a TP-specific national initial-accommodation route, guaranteed housing, or guaranteed placement through 115. The local emergency-help wording in `FR-SOC-01` does not promise accommodation.

## Article and translation results

| Article | EN | UK | RU | Production parity | Notes |
| --- | --- | --- | --- | --- | --- |
| `FR-TP-01` | REVISED | REVISED | REVISED | OUTDATED | Removed the unqualified claim that APS is “renewed automatically”; all three versions now say it may be renewed while temporary protection remains in force and retain the instruction to check the document expiry and prefecture procedure. Production still contains the earlier sentence and needs this one content-field update before status promotion. |
| `FR-EMP-01` | PASS | PASS | PASS | PASS | No job, course, funding, or residence-transition guarantee. Qualifications on registration, assessment, availability, and dated Ukraine-specific guidance are retained. |
| `FR-EMP-02` | PASS | PASS | PASS | PASS | Work authorization remains limited to a currently valid APS-BPT; employer duties, gross SMIC figures, effective date, and exceptions align across languages. |
| `FR-HEALTH-01` | PASS | PASS | PASS | PASS | Ukraine-specific health arrangements retain their scope and 12-month qualification; urgent access is not described as free care. |
| `FR-EDU-01` | PASS | PASS | PASS | PASS | School access, needs-based support, route-dependent admission, fees, and CROUS conditions remain qualified; no automatic admission, equivalence, credit transfer, or funding. |
| `FR-SOC-01` | PASS | PASS | PASS | PASS | ADA, RSA, Prime d’activité, ARE, family benefits, and local assistance remain separate and conditional; no automatic benefit award or housing promise. |

### `FR-TP-01` correction detail

The previous wording “is renewed automatically” could be read as a promise that a person need take no action. That degree of certainty was not appropriate alongside the same draft’s direction to check the APS expiry and prefecture instructions, and Gate 02’s official local procedure instructs applicants to renew APS during the ordinary-permit review.

The EN / UK / RU sentence was narrowed to “may be renewed / може поновлюватися / может продлеваться while temporary protection remains in force.” The six-month validity, work authorization, the separate EU end date, the individual-document caveat, and all source references are unchanged. This is a qualification correction; no new legal rule was added.

## Production parity

| Measure | Result |
| --- | --- |
| France articles / expected Article IDs | 6 / 6 — PASS |
| EN / UK / RU translations | 18 / 18 — PASS |
| Existing official source records | 80 — PASS; IDs, URLs, and article ownership match |
| Exact Title / Summary parity | 18 / 18 — PASS |
| Exact Content parity | 15 / 18 — PASS; the three `FR-TP-01` production contents contain the pre-correction renewal sentence |
| Translation status | 18 `NeedsReview`; unchanged |

No production JSON or source record was changed. To restore 18/18 parity, the next integration task must update only the three `FR-TP-01` production content fields to the reviewed draft wording before considering `NeedsReview` → `Verified` or Production visibility.

## Summary

- Articles PASS: **5**.
- Articles REVISED: **1** (`FR-TP-01`).
- Articles BLOCKED: **0**.
- Translation versions checked: **18**.
- Semantic equivalence: **PASS** after the matching EN / UK / RU draft correction.
- Production parity: **OUTDATED** for `FR-TP-01`; PASS for the other five articles.
- Unresolved legal/content issues: no new blocker within the six-article set. The existing `housing-initial` evidence gap remains excluded from this Core set; nationwide ordinary-permit filing and pending-rights claims remain prohibited.
- Housing exclusion preserved: **YES**.
- `residence-filing` qualification preserved: **YES**.
- Draft files changed: **1** (`FR-TP-01.md`).
- Production data/statuses changed: **0**.

## Verdict

**REVISION REQUIRED** — the reviewed drafts are ready for the corrected text to be synchronized, but Production parity is not yet complete. Do not promote France or change translation statuses until the three `FR-TP-01` Production content fields match this reviewed draft and the separate promotion task rechecks the full catalog.
