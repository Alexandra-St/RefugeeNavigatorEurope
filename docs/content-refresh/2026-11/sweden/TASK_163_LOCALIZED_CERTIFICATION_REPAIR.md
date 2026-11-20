# Task 163 — localized certification repair

**As of:** 20 November 2026
**Result:** **SWEDEN LOCALIZED CERTIFICATION REPAIR COMPLETE — READY FOR INDEPENDENT RECERTIFICATION**

## Scope and preservation

Task 162’s blocked review and snapshot remain historical and unchanged. This task changed only staged review-copy text, translation-alignment notes, and repair records. It made no legal research and changed no claim-ledger proposition, source, temporal class, coverage, Core relationship, binding plan, Compare proposition/disposition, Watch row, or production file.

## Localized repairs

- Added the six omitted active SE-EMP-02 propositions (C018, C020, C021, C022, C027, C034) to both UK/UA and RU in the corresponding written-terms, employment-ending, and workplace-safety sections: 12 missing language instances repaired.
- Added SE-TP-01-C048 to the Russian application section. The extension appointment and first-time biometrics instructions now match the existing EN/UK/UA proposition.
- Removed the stale “each benefit has its own decision” sentence from SE-SOC-01 UK/UA and RU. The final municipality-specific individual-assessment proposition C072 remains exactly once in EN, UK/UA, and RU. C056 and C073 remain represented in all three copies.
- Removed review-process phrasing from the SE-EDU-01 school-reception, SFI, and komvux passages in EN, UK/UA, and RU, preserving the final claim meaning and qualifiers. Removed the corresponding “reviewed guidance” wording from the Socialstyrelsen child/housing benefit boundary across all three languages.
- Removed the internal “audit review copy” label from all eight staged article headings; it was staging metadata, not user-facing text.

## Regression review

Translation alignment covers all 272 active ClaimIds; every active row has EN, UK, and RU marked ALIGNED, with no active missing proposition, material divergence, or NumericDate divergence flagged. The repaired rows are SE-EMP-02-C018/C020/C021/C022/C027/C034, SE-TP-01-C048, SE-SOC-01-C072, and SE-EDU-01-C040/C044/C065. The full staged alignment file contains additional inactive/history rows; those were retained.

The eight copies were re-scanned for audit/meta language and known stale Social wording. No substantive audit/meta phrase or stale superseded statement remains. No newly orphaned review-copy proposition or active claim omission was found. Repair delta has 35 rows; every row records `SemanticChange=NO`.

Unchanged baseline: ACTIVE 272 (175 MATERIAL, 97 SUPPORTING); NumericDate 45; temporal counts CURRENT 255, FUTURE 3, HISTORICAL 5, TRANSITION 9; coverage 222 COVERED, 50 COVERED_MULTIPLE, zero unsupported and source-review-pending; Core 612 (325 SUPPORTED, 287 NOT_SUPPORTED, zero unresolved); Compare 24/24 certified and 65 supported tuples, dispositions 16/7/0/1; source inventory 69 production, 78 staged, 77 planned, actions 67/0/1/1/9; Watch 6 (5 APPROVE, 1 EDIT, zero REMOVE or unresolved); high-risk 118 (111 source-certified, zero active evidence gaps).

The blocked Task 162 certification is not replaced or upgraded by this repair. Independent re-certification is still required. Production/canonical files were not changed; no commit, push, CI, Docker, or tests were run.
