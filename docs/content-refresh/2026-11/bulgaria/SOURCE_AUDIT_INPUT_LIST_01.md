# Bulgaria official source audit input list — final staging (17 November 2026)

This file is finalized. The complete row-level owned-source ledger, including current URL result, authority, LastCheckedAt, claim/aspect mapping, source status, action, and metadata plan is [`OFFICIALSOURCE_INVENTORY_01.csv`](OFFICIALSOURCE_INVENTORY_01.csv). Proposed new source records and deterministic IDs are in [`SOURCE_ADD_PLAN_01.csv`](SOURCE_ADD_PLAN_01.csv). Full inventory audit: 65/65.

## Reconciled counts

- Bulgaria-owned existing OfficialSource records: 65; unique URLs: 63.
- Core-referenced: 65; Compare-referenced: 56; shared: 56; Core-only: 9; Compare-only: 0; combined unique: 65.
- Owned but unreferenced: 0; referenced non-owned: 0; dangling IDs: 0.
- Source statuses: CURRENT 55; LEGACY 8; BROKEN 2; UNCLEAR 0.
- Source actions: KEEP 55; UPDATE 2; RETAIN HISTORICAL 8; REMOVE 0.
- Proposed ADD: 5.

The two repeated URLs across records are intentional owner-local copies: the Ministry of Interior Law on Asylum and Refugees is owned by BG-TP-02 and BG-EMP-02; the Labour Migration and Labour Mobility Act is also owned by BG-TP-02 and BG-EMP-02. There are no duplicate SourceIds or owner+URL duplicates.

## ADD records

| Proposed SourceId | Owner UUID | Title / authority | Evidence scope | Binding |
|---|---|---|---|---|
| `1b7d23f4-0c08-581b-b156-ba4c119f92bc` | `4a1e4c9b-a09c-5c92-bc37-2c9a15b459bc` | CoM notice, 4 Nov 2026 | Bulgarian protection extension and 2027 re-registration dates | BG-TP-01 Core + Compare protection-dates |
| `399a3a4e-75e4-59d3-bdb1-315585052ea8` | `26a17913-a1f2-5710-b11b-df406dab09cc` | Decree 235/2026, MLSP | Enacted 2027 future minimum wage | BG-EMP-02 Core |
| `8b439e25-9430-5ab2-92b9-0df6343fa660` | `7fede68e-bccb-53f8-9648-984d7ac6fb68` | BNB euro Q&A | Adoption, fixed conversion, cash and price-display dates | BG-SOC-01 Core |
| `44c74fe5-d729-5690-b037-fd1d96bf502d` | `26a17913-a1f2-5710-b11b-df406dab09cc` | NRA free-professions guidance | Separate independent-activity registration, tax, and insurance steps | BG-EMP-02 Core |
| `50f0a447-2fb8-532c-920e-8f24e69a637a` | `26a17913-a1f2-5710-b11b-df406dab09cc` | Registry Agency company-registration guidance | Separate company registration and licensing route | BG-EMP-02 Core |

IDs use UUIDv5 with the URL namespace and the canonical URL. Planned LastCheckedAt for CURRENT KEEP, UPDATE, and ADD is `2026-11-17T00:00:00+00:00`; historical records retain the project’s historical-date convention. This is staging only; no production record or binding was changed.
