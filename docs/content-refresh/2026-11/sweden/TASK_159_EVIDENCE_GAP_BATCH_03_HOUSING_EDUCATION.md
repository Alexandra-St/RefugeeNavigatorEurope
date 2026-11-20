# TASK 159 — Evidence-gap batch 3: Housing + Education

AS-OF: 20 November 2026
Scope: Sweden staging only. Production content, canonical sources, tests, and other country files were not touched.

## Resolution

All 11 targeted gaps are cleared from the active gap input. Nine propositions were narrowed to exact official guidance and supported; two unsupported general statements were removed: population-registration status as a general immigration-status distinction (C003), and the universal statement that a course place is not guaranteed (C027). C005 now describes the Migration Agency daily allowance only. Education school-placement claims now refer specifically to compulsory-school choices and municipal placement rules. UHR claims distinguish recognition statements, professional authorization and credit transfer.

## Housing findings

The Migration Agency states that temporary-protection daily allowance depends on whether a person lives in own housing or agency accommodation and whether food is included. Once population-registered, a person no longer receives Migration Agency financial aid and may instead qualify for support from Försäkringskassan, the Pensions Agency or the municipality. Försäkringskassan describes a separate housing allowance for families with children, with application and housing documentation requirements and exceptions to registration at the residence address. These programs are not interchangeable.

The two additional housing sources are staging ADD candidates. The agency sources do not establish eligibility for every individual or every form of municipal assistance; applicant-specific decisions remain with the responsible body.

## Education findings

Skolverket says guardians may request preferred compulsory schools, but local placement rules govern allocation; an unplaced child is placed at a school close to home under those rules. UHR says its recognition statement compares completed foreign qualifications, does not award a Swedish qualification, and is not a professional licence or an admission decision. The UHR FAQ also directs incomplete-study credit-transfer questions to the higher education institution. Higher-education fee rules distinguish temporary permits granted for a reason other than studies; the University Admissions service says these holders are exempt from application and tuition fees, with documentation requirements where requested.

## New source IDs and stable keys

- `1c223817-2971-5ce2-bac3-2d2baa41011e` — `refugeenavigator:official-source:e0f307e9-7991-4350-89f4-2f84a9047393:https://www.migrationsverket.se/en/you-have-a-permit-in-sweden/protection-under-the-temporary-protection-directive/financial-aid.html`
- `dfb4d353-b6be-57ae-b238-9e142907e55a` — `refugeenavigator:official-source:e0f307e9-7991-4350-89f4-2f84a9047393:https://www.forsakringskassan.se/english/parents/housing-allowance-for-families-with-children`
- `ce3256c3-3694-5a83-90c5-b0edc3784fa0` — `refugeenavigator:official-source:cac58c0c-ce69-41ac-877e-4fd124105b89:https://utbildningsguiden.skolverket.se/grundskolan/valja-grundskola/ansokan-och-antagning-till-grundskolan/application-and-admission-to-compulsory-school`
- `be9c8da3-450b-599a-afcb-b6f26de3958e` — `refugeenavigator:official-source:cac58c0c-ce69-41ac-877e-4fd124105b89:https://uhr.se/en/start/recognition-of-foreign-qualifications/how-to-apply-for-an-evaluation/faq/`
- `320c1cb2-afa2-5a5c-a009-2ae8cbba2046` — `refugeenavigator:official-source:cac58c0c-ce69-41ac-877e-4fd124105b89:https://www.uhr.se/en/start/laws-and-regulations/Laws-and-regulations/The-Higher-Education-Ordinance`

No same-owner canonical URL duplicates were found before adding these five sources.

## Graph, compare and remaining gaps

The two target Compare propositions have been revised and their source-link rows added. The cumulative Compare tuple arithmetic still requires a complete downstream recomputation before this batch can be represented as fully certified. No dates, numeric rates or future implementation triggers were introduced by the claim rewrites. The existing SFI 1 January 2027 watch and all unrelated watch states remain unchanged.

The active evidence-gap input now contains only the three Social claims. EN/UK/RU review copies were updated for the changed claims.

## Completion limitation

This staging pass does not claim full task completion: the 620-row effective Core graph and all 24 Compare aspect disposition counts were not rebuilt and independently reconciled after edits. Final verdict remains BLOCKED until those aggregate ledgers and translation-alignment counts are reconciled.
