# Finland Production Parity Sync 01

**Date:** 15 November 2026
**Scope:** Exact FI-TP-01 draft-to-catalog sync and Finland integrity checks. No source registry, status, visibility, Compare or runtime data was changed.

## Translation sync

**Article:** FI-TP-01 (`ffd32dca-56d8-5dcf-b995-7522a159010a`)

| Language | Changed fields | Result |
| --- | --- | --- |
| EN | Content | Catalog Content copied exactly from the reviewed draft. Title and Summary already matched. |
| UK | Content | Catalog Content copied exactly from the reviewed draft. Title and Summary already matched. |
| RU | Content | Catalog Content copied exactly from the reviewed draft. Title and Summary already matched. |

No wording was edited during transfer. Verification status and `LastUpdatedAt` were preserved.

## Protection dates

All three language versions now state the same qualified points:

- The EU Temporary Protection scheme runs through **4 March 2028**; this is not an individual Finnish permit or card expiry date.
- Migri says current Finnish temporary-protection residence permits are valid until **4 March 2027**. Separate expiry situations may apply, including where another Finnish residence permit has been issued.
- A separate extension application is currently not required; Migri says it will extend the permit automatically.
- Cards printed with expiry dates in **2023–2026** remain valid **in Finland** until **4 March 2027** if the underlying permit remains valid. A replacement card is not needed solely because Migri extends the permit if card details are current.
- Migri will publish further instructions for the 2028 rollout. The drafts do not assert travel validity or old-card validity after 4 March 2027; holders are directed to follow Migri instructions and check their permit record.

## Source integrity

- Finland source records: **100**.
- Unique source IDs: **100**.
- Unique HTTPS URLs: **90**.
- New source records required: **0**.
- Production citation tokens across Finland: **318**; all resolve to existing sources owned by the citing article. No draft-local keys or cross-owner citations were found.
- `official-sources.json` was not changed.

## Full parity

- Articles reviewed: **8/8**.
- Translations: **24/24**.
- Exact draft/catalog parity: **24/24**. The other 21 translations remained exact matches; the three FI-TP-01 Content fields now match the reviewed drafts.
- Comparison aspect IDs in the drafts: **24/24 unique**.

## Status / visibility hold

- `NeedsReview`: **24/24**, unchanged.
- `LastUpdatedAt`: **2026-11-11T00:00:00+00:00** for all Finland translations, unchanged.
- Production: Finland remains hidden.
- Development: Finland remains visible.
- Compare: Finland remains excluded; **17 countries / 408 facts**, with **0 Finland facts**.
- No visibility or Compare configuration was changed.

## Validation

- Information-entry and official-source JSON parse: **PASS**.
- Global official-source ID uniqueness and Finland source relationships: **PASS**.
- Citation resolution and ownership: **318/318 PASS**.
- Finland targeted `.NET` integration tests: **7/7 PASS**. Restore emitted NU1900 warnings because vulnerability metadata could not be written to the local NuGet cache; the targeted test run completed successfully.
- `git diff --check`: **PASS**.
- Changed-file whitespace check: **PASS**.
- No test expectation needed changes.

## Verdict

**READY FOR PRODUCTION PROMOTION**
