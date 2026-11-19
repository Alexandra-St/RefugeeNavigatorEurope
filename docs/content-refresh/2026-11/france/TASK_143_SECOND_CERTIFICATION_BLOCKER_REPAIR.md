# Task 143 — France second-certification blocker repair

As of 19 November 2026. Staging-only repair; Task 144 remains the independent certification step.

## Verdict

**FRANCE SECOND-CERTIFICATION BLOCKERS RESOLVED.** All three Task 142 blocker categories were repaired. No integration approval is granted here.

## TP eligibility — C021–C024

Task 142 found that the only latest SUPPORTED source was Ministry page `04f61765-f6e7-5f22-87c0-60f6382fa42c` (LEGACY / RETAIN_HISTORICAL); the current extension decision `44083afc-531a-52d2-be25-8f39a26a7087` was NOT_SUPPORTED for those exact clauses. The historical Ministry record remains historical. Targeted review of Article 2 of [Council Implementing Decision (EU) 2022/382](https://eur-lex.europa.eu/eli/dec_impl/2022/382/oj/eng) established the original displaced-person categories. The extension decision remains the source for the later 2026 extension and its additional conditions.

A new France-owned staging ADD record, `d0f6fb72-2f27-487d-8cc2-0cfdd32506f9`, was created under FR-TP-01. It supports all four exact current propositions. All four claims were rewritten to align with current Article 2 wording; none was reclassified or removed:

- C021 — rewritten to current displaced-person wording; supported by the new current official source: **REWRITE_TO_CURRENT_SUPPORTED_CLAIM**.
- C022 — rewritten to include the statutory status and displacement dates; supported by the new current official source: **REWRITE_TO_CURRENT_SUPPORTED_CLAIM**.
- C023 — narrowed to Article 2(2)’s Member-State choice of EU temporary protection or adequate national protection: **REWRITE_TO_CURRENT_SUPPORTED_CLAIM**.
- C024 — rewritten to Article 2(4)’s family-unit, dependency and pre-date residence scope: **REWRITE_TO_CURRENT_SUPPORTED_CLAIM**.

All four now have NumericDate=YES. The Article 2 clauses are linked by four SUPPORTED tuples. Prior Ministry and 2026-decision tuple decisions remain in the audit history. The EN/UK/RU first paragraphs and source markers were aligned. TP Compare aspects and approved Watch rows were not affected.

## Social administrator — C039

Original proposition: “OFII administers ADA for temporary-protection beneficiaries.” It remains a current SUPPORTING, non-NumericDate claim. Task 142 found its only surviving support in the 2024 OFII continuation form `aa0f80be-7a98-5b27-a6e2-38286081f4ed`, which remains LEGACY / RETAIN_HISTORICAL.

Current [CESEDA Article D. 581-7](https://www.legifrance.gouv.fr/codes/article_lc/LEGIARTI000042805990/2026-09-15), in force 15 September 2026, provides TP beneficiaries the L.553-1 allowance, applies the Title V Chapter III rules, and ends payment on OFII’s request when TP ends or a transfer occurs. C039 is **SUPPORTED_CURRENT_EXISTING_SOURCE** using `ba87b018-5dbb-5a3e-aeb9-c84e254ad507`; the claim was not temporally reclassified or removed. EN/UK/RU now state the current legal basis and scope. The historical form retains its role only for the continuation-form procedure.

Compare aspects social-subsistence and social-application were revalidated with the current D.581-7 source (two SUPPORTED aspect/source tuples added). No Watch row references C039.

## SMIC and Mayotte

The active wage-rate claims are C012 (metropolitan France) and C028 (Mayotte); related age and training-contract claims remain separate. The certified scope already held the exact metropolitan figures and a distinct Mayotte rate claim. Task 143 therefore expanded C028 and its copy/Compare wording to the exact Mayotte figures rather than altering metropolitan scope. The existing [22 May 2026 Légifrance order](https://www.legifrance.gouv.fr/jorf/id/JORFTEXT000054126589) supports both territory rates from 1 June 2026:

- Metropolitan France: **€12.31 gross/hour; €1,867.02 gross/month** at the 35-hour weekly reference.
- Mayotte: **€9.56 gross/hour; €1,449.93 gross/month** at the same reference.

C028’s exact widened proposition was re-adjudicated SUPPORTED. EN/UK/RU FR-EMP-02 copy and the work-minimum-pay Compare aspect now include both territory amounts, date, gross basis, and monthly reference. The Compare aspect remains source-certified. FR-W02 was updated to track both C012 and C028 and now cites the rate-setting order alongside Urssaf; watch count is unchanged.

## Reconciled gates and arithmetic

- Active claims: **172** (20 / 21 / 17 / 19 / 20 / 26 / 22 / 27 by article); MATERIAL **117**, SUPPORTING **55**; NumericDate **39**; CURRENT **165**, FUTURE **0**, HISTORICAL **0**, TRANSITION **7**. No claims were created, removed, or temporally reclassified.
- Core tuples added/retested: **6**, all SUPPORTED (4 TP new-source tuples, 1 Social current-source tuple, 1 Mayotte exact-scope retest); NOT_SUPPORTED **0**; unresolved **0**. Latest effective active Core graph: **423** (SUPPORTED **210**, NOT_SUPPORTED **213**, unresolved **0**).
- Coverage: COVERED **140** single-source and **32** multiple-source claims; NOT EXTERNALLY SOURCE-DEPENDENT **0**; UNSUPPORTED **0**; REQUIRES_SOURCE_REVIEW **0**. MATERIAL/NumericDate unsupported **0/0**. Open reopen candidates **0**.
- Source plan: production-existing France sources **104**; staging inventory **118**; ADD **14**, UPDATE **8**, RETAIN_HISTORICAL **2**, REMOVE **0**; planned France **118**, planned global **1,544**. Task 143 added only `d0f6fb72-2f27-487d-8cc2-0cfdd32506f9`. `aa0f80be-7a98-5b27-a6e2-38286081f4ed` remains historical. Owner conflicts, unintended same-owner/URL duplicates and dangling IDs remain **0**.
- Core binding delta: KEEP **82**, ADD **14**, REMOVE **22**. Compare: **24/24** source-certified; source review **0**; effective aspect/source tuples **88** (SUPPORTED **85**, NOT_SUPPORTED **3**, unresolved **0**). Compare binding delta: KEEP **59**, ADD **17**, REMOVE **12** (Task 143 adds two source/aspect bindings for the current OFII administrator evidence); dangling SourceIds **0**.
- Watch: **14** rows; APPROVE **11**, REMOVE **3**, gap-resolution **0**, stale/inactive approved ClaimIds **0**; W02 is the only watch row touched.
- Affected review copies checked: **3/3**. EN/UK/RU material, numeric/date, and missing/extra proposition divergences: **0**. TP eligibility, SMIC, and Social administrator/process regressions: **PASS**.
- No production information entries, production OfficialSources, production Compare facts, tests, or other-country files were changed. No commit or push was made.

## Next step

Task 144: independent third final certification of this staging state. Do not treat Task 143 as integration approval.
