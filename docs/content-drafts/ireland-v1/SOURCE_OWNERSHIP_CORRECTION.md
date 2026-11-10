# Ireland v1 Editorial Source Ownership Correction

This correction normalizes ownership for all seven affected editorial source entries. Each replacement is a deterministic UUID v5 generated with the owning Article UUID as the namespace and the entry's local editorial source key as the name. Every affected entry was re-keyed symmetrically; no shared UUID remains as a preferred owner.

Baseline: `f45a7150e738eeffccd8e45ff8a61cbe7c7dc004`

## Counts

Before:

- 70 source-set entries
- 65 unique UUIDs
- 2 shared UUIDs
- 7 affected entries
- 5 excess cross-article UUID uses

After:

- 70 source-set entries
- 70 unique UUIDs
- 0 shared UUIDs

## Mapping

| Article | Editorial source key | Old UUID | New UUID |
|---|---|---|---|
| IE-EMP-01 | `IE-EMP-01-S09` | `2ccf09e9-c753-5561-8342-fbe129665bdb` | `8268bba5-e3a7-5a5a-a172-6996291243ff` |
| IE-EMP-02 | `IE-EMP-02-S01` | `c2717e6b-0405-552a-b200-58e9acdcc909` | `8f91c5cd-e52f-51f0-a704-25590ee12f33` |
| IE-EMP-02 | `IE-EMP-02-S19` | `2ccf09e9-c753-5561-8342-fbe129665bdb` | `a818f974-269f-5775-9951-795f665754d3` |
| IE-HEALTH-01 | `IE-HEALTH-01-S01` | `c2717e6b-0405-552a-b200-58e9acdcc909` | `c53c975b-f313-572b-b8bc-0e0969e7e2c0` |
| IE-EDU-01 | `IE-EDU-01-S01` | `c2717e6b-0405-552a-b200-58e9acdcc909` | `4f1a222d-934c-52a3-ad9d-66f64870bf2d` |
| IE-EDU-01 | `IE-EDU-01-S09` | `2ccf09e9-c753-5561-8342-fbe129665bdb` | `d8a478cc-111b-5701-ba5b-7356a5685b7d` |
| IE-SOC-01 | `IE-SOC-01-S01` | `c2717e6b-0405-552a-b200-58e9acdcc909` | `af9a8fa6-f874-59f2-a681-b3dca9263aea` |

## Scope and invariants

Only source UUIDs in the seven affected Editorial source set entries and their corresponding inline citation tokens in EN, UK and RU were changed. Inline replacements are symmetric for each article-owned entry. Content wording is byte-equivalent apart from those UUID tokens; language semantics are unchanged. Source URLs and titles are unchanged. Article IDs and all other metadata, including comparison aspects, are unchanged. Source-set entry counts remain unchanged for every article. Catalog integration was not performed.
