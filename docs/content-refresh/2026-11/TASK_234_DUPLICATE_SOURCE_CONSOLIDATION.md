# Task 234 — Duplicate source consolidation

Starting snapshot: 094ed7d82d24b538b1181cff6612f47b7a318522.

Consolidated two redundant owner-local records. Finland retained 7ab4c5c4-4363-5083-9734-4a16e6d72e74 and removed e0b8e4ac-3707-5842-8fc1-63f81bbb2532. Hungary retained d244fc9f-a449-5d66-8fe1-b818ed2b4a5b and removed 516458e9-91fa-5f95-a179-01bd4a205473. Active citations and reviewed fixtures use retained IDs; no substantive article wording or Compare text changed.

## Owner/URL multi-record classification

Broad scan: same owner + lower-case scheme/host + path with trailing slash removed, ignoring query and fragment. It yielded 18 groups in the pre-consolidation catalog. The 16 remaining groups are canonicalization false positives: distinct BOE legal-document IDs, statute routes, EUR-Lex CELEX/version identifiers, RIS law identifiers, or distinct downloadable ÖGK documents share a path while query parameters identify different resources. The two exact-resource duplicates (Finland and Hungary) were consolidated. Current grouping by owner + normalized scheme/host/path + query + fragment yields zero multi-record groups.

OWNER_URL_MULTI_RECORD_GROUPS=18
REDUNDANT_SOURCE_GROUPS=2
JUSTIFIED_SCOPED_SAME_RESOURCE_GROUPS=0
REDIRECT_COLLISION_GROUPS=0
CANONICALIZATION_FALSE_POSITIVE_GROUPS=16
GENUINE_SOURCE_INTEGRITY_DEFECT_GROUPS=0
NETWORK_INCONCLUSIVE_GROUPS=0
UNJUSTIFIED_OWNER_RESOURCE_DUPLICATES=0

Counts: official sources 1562 to 1560; Finland 101 to 100 (FI-EMP-02 10 to 9); Hungary 113 to 112 (HU-TP-02 19 to 18). Articles 152, translations 456, Compare facts 456. Task 231 must be rerun independently from the final validated SHA. Password protection unchanged; site unpublished.
