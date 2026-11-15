# Belgium Targeted Compare Research 01

**Research date:** 14 November 2026
**Scope:** only `protection-application`, `protection-dates`, and `residence-filing`. Sources were checked against the official-source registry. No Core text or Production data was changed.

## 1. `protection-application`

### Official sources

| Official title / authority | HTTPS URL | Scope; accessed | Claims supported | Limitations |
|---|---|---|---|---|
| **Temporary Protection** — Belgian Immigration Office (FPS Home Affairs) | https://dofi.ibz.be/en/themes/ukraine/temporary-protection | Federal; 14 Nov 2026 | A person requesting TP must appear in person at the Registration Centre; bring documents showing category eligibility and pre-24-February-2022 Ukrainian residence; registration records identity and fingerprints and copies available eligibility documents; if conditions are met, the Office issues a Certificate of Temporary Protection; holder then goes to the municipality of residence for the Temporary A residence card. If the centre cannot process everyone that day, it may invite people to return later. | The current page does not state that a prior appointment or online registration is required, nor does it say that no appointment is required. It does not provide a complete document checklist or a procedure for missing evidence. Do not reuse the 2022 online ticket/QR instructions. |
| **Registration Centre** — Belgian Immigration Office (FPS Home Affairs) | https://dofi.ibz.be/en/themes/ukraine/registration-centre | Federal; 14 Nov 2026 | Current centre address is 68 Rue Belliard, 1000 Brussels; open Monday–Friday from 08:30. | The page does not specify an appointment/pre-registration process or promise same-day processing. It notes that accompanying persons cannot enter. |

### Established facts

- The competent authority for the TP decision and registration is the federal Belgian Immigration Office; the official TP page directs applicants to one central Registration Centre at 68 Rue Belliard, Brussels. The page gives the same federal route without identifying a separate regional application route. **“Nationwide” here describes the federal route presented by the authority, not a separate express statutory declaration that every special case must use this address.**
- The current user-facing route is physical: the person requesting TP must appear in person. The page says to bring all documents in the applicant’s possession that show eligibility, including proof of residence in Ukraine before/on 24 February 2022. Registration includes identity data, fingerprints, and copies of available eligibility documents.
- The Federal Immigration Office issues the **Certificate of Temporary Protection** only if the eligibility conditions are met. This is a decision/certificate stage, not automatic approval.
- After receiving the federal certificate, the person presents it to the municipality where they reside. The municipality issues the **Temporary A residence card**. TP registration, the federal certificate, municipal residence registration, and the physical residence card are separate stages.
- Current official pages do not establish whether an appointment is required, whether walk-ins are guaranteed, or what to do if eligibility evidence is missing. The federal page says that capacity limits may result in an invitation to return later.
- The FPS Home Affairs notice about booking online with a QR code is dated 21 March 2022 and names the former Brussels Expo location. It is historical and is not evidence of the current booking process or address.

### Safe Compare assertion

**EN:** Apply in person at the federal Immigration Office’s Temporary Protection Registration Centre, 68 Rue Belliard, Brussels. Bring documents showing your identity and eligibility, including evidence of residence in Ukraine before 24 February 2022. If you are granted temporary protection, the Office issues a Certificate of Temporary Protection; then take it to the municipality where you live to obtain the Temporary A residence card. Check the current intake instructions before travelling: the official pages reviewed do not say whether an appointment is required, and capacity may require a return visit.

### Required qualification

Do not state that approval is automatic, that a complete document checklist has been published, that a person without documents will be accepted, or that an appointment is or is not required. Do not describe the municipality as the authority that grants TP: it handles the later residence-card step. The source page gives a centralized federal route but no alternative regional process; its capacity notice permits return on a later date.

### Production source reuse

- **Existing exact Production URL:** `https://dofi.ibz.be/en/themes/ukraine/registration-centre` — Source ID `2e8786e8-c502-5853-8ed7-ba5e9c1bc9f3`, owner `BE-TP-01` (`aef6ec65-f8ed-5054-bafb-c465e790dd57`).
- **New official URL:** `https://dofi.ibz.be/en/themes/ukraine/temporary-protection` — not found in `official-sources.json`; likely one new owner-specific Production source record for `BE-TP-01` if this wording is integrated.

### Status

**READY WITH QUALIFICATION** — official current steps are sufficient for a bounded Compare fact; appointment, complete evidence, and exceptional cases remain unspecified and must stay out of the claim.

## 2. `protection-dates`

### Official sources

| Official title / authority | HTTPS URL | Scope; accessed | Claims supported | Limitations |
|---|---|---|---|---|
| **Council Implementing Decision (EU) 2026/1912 of 30 July 2026** — Council of the European Union / EUR-Lex | https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX:02026D1912-20260804 | EU framework; in force from 5 Aug 2026; accessed 14 Nov 2026 | Article 1 extends the EU temporary-protection period until 4 March 2028. | This is the EU scheme end date, not by itself the expiry date printed on or legal validity of an individual Belgian document. |
| **Temporary Protection** — Belgian Immigration Office (FPS Home Affairs) | https://dofi.ibz.be/en/themes/ukraine/temporary-protection | Federal Belgian implementation; accessed 14 Nov 2026 | The Immigration Office states that TP is extended until 4 March 2028; from 4 January 2027, the person may request A-card renewal at their municipality, and the new A card will be valid until 4 March 2028. The page says people receiving a TP certificate after 4 January 2027 will receive an A card valid until 4 March 2028; it also describes an interim registration certificate if the new card cannot be issued before 4 March 2027. | The same page contains a stale/inconsistent sentence describing an A card “valid until 4 March 2025” while giving a future renewal window beginning 4 January 2027. Do not use that 2025 date as a current individual-card expiry or infer that every existing card is already valid through 2028. Ask the municipality to confirm a person’s document and renewal steps. |

### Established facts

- The EU scheme is extended through **4 March 2028** by Decision (EU) 2026/1912. The decision entered into force on 5 August 2026; its extension applies from 5 March 2027.
- The Belgian Immigration Office’s current Ukraine TP page separately describes Belgian document handling: a holder may request renewal of the Temporary A card at their municipality from **4 January 2027**; the new card will be valid to **4 March 2028**. It also says a TP certificate issued from 4 January 2027 results in an A card valid to 4 March 2028. Where a new card cannot be issued before 4 March 2027, the page describes issuance of a registration certificate as interim proof.
- The EU end date and the Belgian card/document validity are distinct. The UDI page’s reference to a card “valid until 4 March 2025” conflicts with its future 2027 renewal instructions and is not safe as a general description of current holders’ cards.

### Safe Compare assertion

**EN:** The EU temporary-protection scheme has been extended until 4 March 2028. Belgian Immigration Office guidance says A-card renewal is handled through the municipality: renewal requests may be made from 4 January 2027, and the new card will be valid until 4 March 2028. The EU scheme date does not, by itself, establish the expiry date on an individual Belgian card; check the card and confirm the applicable renewal steps with the municipality.

### Required qualification

Keep “EU scheme through 4 March 2028” separate from the validity of an individual Belgian A card or certificate. Do not say all current cards are automatically valid until 2028, that no holder action is needed, or that every card has the same expiry. Because the federal page retains a stale 2025 card date, individual card cohort and timing should be confirmed locally. This qualification is not a claim that the scheme extension itself is uncertain.

### Production source reuse

- **Existing exact Production URL:** `https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX:02026D1912-20260804` — Source ID `01044ed1-42f4-5d33-bbcd-931f2b1006b7`, owner `BE-TP-01`; the same URL is also bound as Source ID `14577aa6-0af5-5c93-9f97-44dd9cf1cd55` to `BE-TP-02`.
- **New official URL:** `https://dofi.ibz.be/en/themes/ukraine/temporary-protection` — not found in `official-sources.json`; likely one new owner-specific Production source record for `BE-TP-01` if this wording is integrated.

### Status

**READY WITH QUALIFICATION** — both the EU extension and a Belgian renewal path are published by their competent official sources. The source page’s stale 2025 wording prevents a blanket statement about an individual holder’s current card expiry; the safe fact avoids that inference.

## 3. `residence-filing`

### Official sources

| Official title / authority | HTTPS URL | Scope; accessed | Claims supported | Limitations |
|---|---|---|---|---|
| **Long stay [more than 90 days]** — Belgian Immigration Office (FPS Home Affairs) | https://dofi.ibz.be/en/themes/ukraine/long-stay-more-90-days | Federal Ukraine guidance; accessed 14 Nov 2026 | For Ukrainian citizens who have left Ukraine and are already in Belgium, it says a residence-permit application may be made directly at the municipal administration of the place of residence, for study, work, or family reunification; the route’s conditions remain applicable. If usual official documents cannot be presented, the Immigration Office says it will look for alternatives with the applicant. | It is general guidance for Ukrainians already in Belgium; it does not expressly confirm that every TP holder qualifies for every in-country route. It does not establish a universal filing right, pending lawful-stay/work rights, or an automatic TP-to-ordinary-permit transition. The page does not display a current revision date. |
| **Law of 15 December 1980 on access to the territory, residence, establishment and removal of foreigners (consolidated)** — Belgian Official Gazette / Justel (FPS Justice) | https://www.ejustice.just.fgov.be/eli/loi/1980/12/15/1980121550/justel | Federal legislation; accessed 14 Nov 2026 | Provides the statutory basis for distinct ordinary stay routes and route-specific filing rules, including student residence and defined family-reunification procedures. | The law does not create one universal procedure for all TP holders or establish that filing preserves residence/work rights after TP ends. Route eligibility and filing location must be checked under the applicable provision. |

### Established facts

- Belgian official materials describe ordinary residence routes based on work, study, and family reunification, each subject to its own legal conditions.
- Federal Ukraine guidance says a Ukrainian citizen already in Belgium may apply for a residence permit at the municipality of their place of residence for study, work, or family reunification. This is a route-specific filing statement, not automatic conversion or approval.
- The source does not say that every TP holder qualifies under every route. Existing research also identifies specific limits: work/single-permit applications involve the employer and competent region; the reviewed Flemish in-country condition does not expressly confirm that every TP A card satisfies its lawful-residence test; student in-country filing requires the statutory admission/enrolment and timing conditions; family applications can be filed in Belgium only in defined cases.
- No general rule was established that a pending ordinary residence application preserves lawful stay or work after TP ends. No automatic conversion, concurrent status, or nationwide pending-status bridge is confirmed.

### Safe Compare assertion

**EN:** A Ukrainian citizen already in Belgium may be able to apply for an ordinary residence permit through the municipality of their place of residence for work, study, or family reunification, subject to the conditions and filing rules for that specific route. The Belgian guidance does not establish that every temporary-protection holder can use every in-country route, or that a pending application automatically extends residence or work rights. Confirm the route with the Immigration Office, municipality, and—where work authorization is involved—the competent regional authority before relying on it.

### Required qualification

Do not state that TP automatically converts to an ordinary permit, that every permit can be requested in-country, that a pending application preserves stay or work, or that all procedures are federal/uniform. The municipality is the receiving/local administration for the stated Ukrainian route; substantive eligibility and decisions may involve the Immigration Office or regional authority according to permit type. This research supports a qualified route overview, not case-specific filing advice.

### Production source reuse

- **Existing exact Production URL:** `https://www.ejustice.just.fgov.be/eli/loi/1980/12/15/1980121550/justel` — Source ID `9e3a433f-f25e-5a8f-8139-22df26c3cb45`, owner `BE-TP-02` (`54c6f95f-589e-5901-beae-990bed994160`). The same URL also exists under `BE-TP-01` as Source ID `921c7f23-b71c-5904-9d3c-83e49bee0935`.
- **New official URL:** `https://dofi.ibz.be/en/themes/ukraine/long-stay-more-90-days` — not found in `official-sources.json`; likely one new owner-specific Production source record for `BE-TP-02` if this wording is integrated.

### Status

**READY WITH QUALIFICATION** — a federal filing route is documented for Ukrainians already in Belgium, and distinct statutory routes are evidenced. The claim must not be broadened to a universal TP-holder filing right or pending-status guarantee.

## Cross-gap consistency

- **TP registration ≠ municipality registration:** the federal Immigration Office conducts TP registration and decides eligibility; municipality registration/card issuance follows the TP certificate.
- **TP certificate ≠ residence card:** certificate is issued by the federal Immigration Office after the conditions are met; the municipality later issues the Temporary A card.
- **EU scheme end date ≠ individual card expiry:** the EU decision sets the scheme period to 4 March 2028. Individual Belgian card renewal/document rules are separately described by the Immigration Office and should be confirmed for the holder’s document.
- **TP ≠ ordinary residence permit:** ordinary work, study, and family routes have their own conditions and filing arrangements.
- **Filing ≠ automatic transition:** no automatic TP conversion, universal pending residence/work rights, or common route for all permit types is established.

## Source summary

- **Official sources used:** 5 distinct URLs (all official Belgian federal/justice sources or the Council of the EU’s official EUR-Lex legislation).
- **Existing exact Production URLs reused:** 3 — Registration Centre, Council Decision (EU) 2026/1912, and the consolidated Belgian residence law.
- **New official URLs:** 2 — Belgian Immigration Office Temporary Protection page and Long Stay page.
- **New Production source records likely required:** 2 owner-specific records if integrating the new UDI pages (`BE-TP-01` for the TP guidance; `BE-TP-02` for the Long Stay guidance). No source records or UUIDs were created in this research task. Existing URL reuse and new URL count are distinct from source-record count.

## Final verdict

**ALL 3 GAPS RESOLVED** — each has a safe, expressly qualified Compare assertion based on official sources. Qualification remains required for appointment/document exceptions, individual A-card expiry, and the application-specific scope and pending rights of ordinary residence routes. These limitations do not require asserting a universal rule or introducing an unsupported claim.
