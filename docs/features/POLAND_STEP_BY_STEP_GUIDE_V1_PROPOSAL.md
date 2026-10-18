# Poland Temporary Protection Step-by-Step Guide — V1 Proposal

**Status: AWAITING HUMAN EDITORIAL ACCEPTANCE**

**Phase:** 12.1 — design only

**Baseline:** `72b6374b7f0e3ef8c97384fd9fb8c0d75a33c7ad`

**Canonical design language:** English. Any later implementation requires reviewed `uk`, `en`, and `ru` content.

## A. Product objective and editorial baseline

Design a non-personalized navigation aid for people seeking to understand or check Polish temporary-protection and PESEL UKR procedures. Choosing a situation selects explanatory steps; it does not assess eligibility, determine a person's status, or advise which application to file.

The current production catalog is the source of truth for article identity and approval. Primary article **P** is `218207bd-2a85-59e1-a945-eedb0604392e`, **“Applying for Temporary Protection in Poland”**; its `uk`, `en`, and `ru` translations are currently `Verified`. Secondary article **R** is `c230d45f-9c76-5912-8c97-dc7d0689baa6`, **“Residence Options After Temporary Protection”**; its three translations are also currently `Verified`. Historical research-matrix labels such as “unpublished” or “not Verified” are historical and do not override the production catalog. This proposal does not change article or source status.

Research basis only: `docs/content-research/poland-batch-2026-10-16/temporary-protection-application/SOURCES_AND_CLAIMS.md` and `docs/content-research/poland-batch-2026-10-16/after-temporary-protection/SOURCES_AND_CLAIMS.md`. Their recorded check date is 16 October 2026. No source was reopened and no new legal research was performed for this design; those dates are not a freshness guarantee.

Proposed metadata, not production data: guide ID `poland-temporary-protection-v1`; slug `temporary-protection`; country ID `10000000-0000-0000-0000-000000000003`; route `/guides/poland/temporary-protection`; initial implementation status **Draft**.

## B. Four user situations

| Situation / proposed stable ID | Purpose | Proposed steps |
| --- | --- | --- |
| A — New arrival seeking PESEL UKR / `new-arrival-pesel-ukr` | Navigate the current application route without deciding whether the person qualifies. | A1–A4 |
| B — Existing PESEL UKR record / `check-existing-pesel-ukr` | Understand the narrowly defined 2026 identity-confirmation rule and request an actual record check. | B1–B4 |
| C — Older paper certificate / `older-protection-certificate` | Explain the certificate-specific transition and distinguish it from EU protection duration. | C1–C4 |
| D — Longer-term residence and CUKR / `explore-cukr-and-residence` | Navigate CUKR and separate ordinary residence options using the approved secondary article. | D1–D4 |

**Total: 16 proposed steps (four per situation).** Steps are a reading sequence, not a mandatory legal process. All proposed actions are recommendations to consult current instructions or the competent authority. They do not score, classify, or store a user's circumstances.

## C. Proposed steps and claim-level traceability

In each step, “potential documents” means examples to keep available if relevant, not a universal checklist. Article passages are exact English excerpts from the current production articles. Research ClaimIds and research labels map to existing production source GUIDs in section E. GUIDs are grouped by article; no step links to a source record belonging to the other article.

### Situation A — New arrival seeking PESEL UKR

#### A1 — Separate PESEL from PESEL UKR

- **Stable step ID:** `new-arrival-distinguish-pesel-ukr`; **kind:** Recommendation.
- **Action:** Read the current PESEL UKR procedure and distinguish a PESEL number from the UKR status marker.
- **Explanation:** A PESEL number alone is not the same as a record marked UKR or a decision that a person meets the statutory conditions for Polish temporary protection.
- **Potential documents:** Any existing PESEL confirmation or status record, identity document, and authority correspondence, if available.
- **Authority:** The gmina (municipal office) for record/application questions; Office for Foreigners (UdSC) for published protection information.
- **Supporting article:** P (`218207bd-2a85-59e1-a945-eedb0604392e`).
- **Exact article passage:** “EU temporary protection, Polish temporary-protection status, a PESEL number marked UKR, an older protection certificate and a CUKR residence card are different legal concepts.”
- **Claims / research sources / production GUIDs:** TP01 / LAW2026 / `e4aa407d-df53-58ab-b10a-6fc2b22d91d3`; TP02 / PESEL / `0dfc274b-f02d-5acb-913e-51dee3c7fa24`.
- **Limit:** Do not infer status or eligibility from possession of a PESEL number.
- **Optional link:** Full article P.

#### A2 — Check the current application conditions

- **Stable step ID:** `new-arrival-check-current-conditions`; **kind:** Recommendation.
- **Action:** Check the statutory conditions and current official instructions before applying.
- **Explanation:** The guide can point to the application route but cannot decide whether the statutory definition or an exception applies to a particular person.
- **Potential documents:** Identity, entry/travel, status, or other records only as potentially relevant to the person's circumstances and requested by the authority.
- **Authority:** Gmina for the application and record; consult UdSC's current guidance for general information.
- **Supporting article:** P.
- **Exact article passage:** “Under Article 106 of Poland’s 2026 Act, the stay of a covered beneficiary who entered Poland legally during the period specified in the Council decision for the purpose of temporary protection is lawful while that decision remains in force, unless the Act provides otherwise; statutory exclusions and ending events still apply.”
- **Claims / research sources / production GUIDs:** TP01 / LAW2026 / `e4aa407d-df53-58ab-b10a-6fc2b22d91d3`; TP02 / LAW2026 / same GUID.
- **Limit:** Article 106 concerns its defined statutory group and remains subject to exclusions and ending events. Do not classify the reader as a covered beneficiary.
- **Optional link:** Full article P.

#### A3 — Note the 30-day rule and application location

- **Stable step ID:** `new-arrival-check-deadline-and-gmina`; **kind:** Recommendation.
- **Action:** If the person is pursuing PESEL UKR, check the current deadline and submit through the instructed gmina procedure.
- **Explanation:** The approved article states an application at any gmina within 30 days after crossing the Polish border. The applicable statutory conditions and current service instructions still need to be checked.
- **Potential documents:** Application form, photograph, identity document, and any additional records requested for the person's circumstances.
- **Authority:** Any gmina office, subject to the current official procedure.
- **Supporting article:** P.
- **Exact article passage:** “To obtain a new PESEL UKR record, apply at any gmina (municipality) office within 30 days after crossing the Polish border.”
- **Claims / research sources / production GUIDs:** TP02 / LAW2026 / `e4aa407d-df53-58ab-b10a-6fc2b22d91d3`; TP02 / PESEL / `0dfc274b-f02d-5acb-913e-51dee3c7fa24`.
- **Limit:** Do not imply that the 30-day rule makes a person eligible or that a late application has a particular outcome. Current instructions control operational details.
- **Optional link:** Full article P.

#### A4 — Confirm in-person and document instructions

- **Stable step ID:** `new-arrival-confirm-local-instructions`; **kind:** Recommendation.
- **Action:** Confirm the in-person procedure and the documents needed for the specific application, especially for a child or missing-document situation.
- **Explanation:** The official service describes an in-person application and the article gives a form, photograph, and identity document as preparation examples. It directs people to check the current gmina instructions for children, missing documents, or special circumstances.
- **Potential documents:** Form, photograph, identity document; other evidence only if current instructions or the authority request it.
- **Authority:** The gmina accepting the application.
- **Supporting article:** P.
- **Exact article passage:** “The official service describes an in-person application; prepare the form, a photograph and identity document. Check the current gmina instructions for children, missing documents or other special circumstances.”
- **Claims / research sources / production GUIDs:** TP02 / PESEL / `0dfc274b-f02d-5acb-913e-51dee3c7fa24`; TP02 / LAW2026 / `e4aa407d-df53-58ab-b10a-6fc2b22d91d3`.
- **Limit:** These examples are not a universal checklist; no special procedure is invented for children or absent documents.
- **Optional link:** Full article P.

### Situation B — Existing PESEL UKR record

#### B1 — Check whether the historic rule may concern the record

- **Stable step ID:** `existing-record-check-affected-cohort`; **kind:** Recommendation.
- **Action:** Check whether the record was issued only on the basis of a declaration without presentation of a valid passport.
- **Explanation:** The identity-confirmation requirement applied to that defined cohort, not to all people with PESEL UKR.
- **Potential documents:** The original application/record information and identity documents, if available; do not upload them to this guide.
- **Authority:** Gmina, which can check the actual record.
- **Supporting article:** P.
- **Exact article passage:** “By 31 August 2026, people whose PESEL UKR had been issued only on the basis of a declaration, without showing a valid passport, had to confirm their identity at a gmina office.”
- **Claims / research sources / production GUIDs:** TP04 / IDENTITY / `433faca8-dcb6-59a1-8d32-5e38d16049b0`.
- **Limit:** Do not broaden the affected cohort or ask the user to enter passport/status information.
- **Optional link:** Full article P.

#### B2 — Understand the passed deadline and stated effect

- **Stable step ID:** `existing-record-understand-passed-deadline`; **kind:** Recommendation.
- **Action:** Read the historical deadline and, if potentially affected, ask the gmina to verify the present record.
- **Explanation:** The article says the deadline passed and describes the reported effect for people covered by the requirement. The guide does not determine whether a particular record was affected.
- **Potential documents:** Relevant gmina correspondence or record confirmation if already held.
- **Authority:** Gmina for the actual PESEL UKR record; obtain individual advice where needed.
- **Supporting article:** P.
- **Exact article passage:** “That deadline has passed. UdSC says that for people covered by the requirement, failure to confirm led to loss of UKR status and Polish temporary protection from 1 September 2026.”
- **Claims / research sources / production GUIDs:** TP04 / IDENTITY / `433faca8-dcb6-59a1-8d32-5e38d16049b0`; TP04 / LAW2026 / `e4aa407d-df53-58ab-b10a-6fc2b22d91d3`.
- **Limit:** 31 August / 1 September 2026 is a historical, cohort-specific rule. Never infer an individual outcome.
- **Optional link:** Full article P.

#### B3 — Ask the gmina to verify the current record

- **Stable step ID:** `existing-record-request-gmina-check`; **kind:** Recommendation.
- **Action:** Ask the gmina to check the person's current PESEL UKR record and explain any available next step.
- **Explanation:** The article explicitly recommends a record check if the historical requirement may apply. The guide cannot query government systems or establish a person's status.
- **Potential documents:** Case/record reference or correspondence, if available and requested by the gmina; do not disclose these in the guide.
- **Authority:** Gmina.
- **Supporting article:** P.
- **Exact article passage:** “If this may apply to you, ask the gmina urgently to check your record and obtain individual advice; do not infer your status from the PESEL number alone.”
- **Claims / research sources / production GUIDs:** TP04 / IDENTITY / `433faca8-dcb6-59a1-8d32-5e38d16049b0`.
- **Limit:** No automated status checker, questionnaire, passport collection, or guarantee that a record can be corrected.
- **Optional link:** Full article P.

#### B4 — Keep EU protection dates separate from personal records

- **Stable step ID:** `existing-record-separate-eu-and-record`; **kind:** Recommendation.
- **Action:** Check the actual Polish record with the gmina rather than treating the EU extension date as proof of an individual record's validity.
- **Explanation:** The EU-level duration and Article 106's rule for a defined group do not establish whether an individual PESEL UKR record or document is valid.
- **Potential documents:** Current gmina record confirmation and any individual authority decision or notice.
- **Authority:** Gmina for the record; UdSC for published guidance.
- **Supporting article:** P.
- **Exact article passage:** “Do not infer an individual record or document’s validity; ask the competent authority to check it.”
- **Claims / research sources / production GUIDs:** TP01 / LAW2026 / `e4aa407d-df53-58ab-b10a-6fc2b22d91d3`; TP05 / EUTPACT / `0a989122-f3d4-5afd-a7ce-e10c4566cebe`; TP05 / UDSC2028 / `d0fc2583-ea0d-5289-b9c7-de3e97417529`.
- **Limit:** No inference about EU protection eligibility, individual status, or permit validity from a date alone.
- **Optional link:** Full article P.

### Situation C — Older paper temporary-protection certificate

#### C1 — Identify the document as an older certificate

- **Stable step ID:** `old-certificate-identify-document`; **kind:** Recommendation.
- **Action:** Check whether the document is an older paper certificate issued by UdSC confirming temporary protection.
- **Explanation:** UdSC stopped issuing new former certificates from 5 March 2026; the certificate is a distinct document and is not the PESEL UKR record or a CUKR permit.
- **Potential documents:** The certificate and any later PESEL UKR or authority record, if available.
- **Authority:** UdSC for certificate guidance; gmina for a PESEL UKR record.
- **Supporting article:** P.
- **Exact article passage:** “Since 5 March, UdSC no longer issues the former certificates.”
- **Claims / research sources / production GUIDs:** TP03 / CERT / `7418e8ea-bc3d-561b-ba05-2f09ed29cdc0`; TP07 / LAW2026 / `e4aa407d-df53-58ab-b10a-6fc2b22d91d3`.
- **Limit:** The cessation date concerns issuance of this certificate type, not the end of EU temporary protection.
- **Optional link:** Full article P.

#### C2 — Check the certificate-specific transition

- **Stable step ID:** `old-certificate-check-transition`; **kind:** Recommendation.
- **Action:** Read UdSC's current explanation of the validity of an already-issued certificate and its exceptions.
- **Explanation:** UdSC says older certificates remain valid until 4 March 2027 subject to earlier-ending events or status changes.
- **Potential documents:** The certificate and any later record/decision that may affect it.
- **Authority:** UdSC; ask the competent authority to check an individual case.
- **Supporting article:** P.
- **Exact article passage:** “Separately, UdSC says older paper certificates remain valid until 4 March 2027 subject to earlier-ending events.”
- **Claims / research sources / production GUIDs:** TP03 / CERT / `7418e8ea-bc3d-561b-ba05-2f09ed29cdc0`; TP07 / LAW2026 / `e4aa407d-df53-58ab-b10a-6fc2b22d91d3`.
- **Limit:** This is a transitional rule for the older certificate, not an individual validity determination.
- **Optional link:** Full article P.

#### C3 — Ask the authority to verify current status

- **Stable step ID:** `old-certificate-verify-current-record`; **kind:** Recommendation.
- **Action:** Ask UdSC or the competent office to confirm how the certificate and any later record relate in the person's case.
- **Explanation:** The article warns that an earlier-ending event or PESEL UKR assignment can affect the old certificate. The user should not decide this from the certificate alone.
- **Potential documents:** Certificate, PESEL UKR confirmation if held, and official correspondence requested by the authority.
- **Authority:** UdSC for certificate guidance; gmina for the PESEL UKR record.
- **Supporting article:** P.
- **Exact article passage:** “UdSC says certificates issued earlier remain valid until 4 March 2027 unless an earlier invalidating event applies or PESEL UKR is assigned to the holder.”
- **Claims / research sources / production GUIDs:** TP03 / CERT / `7418e8ea-bc3d-561b-ba05-2f09ed29cdc0`; TP07 / LAW2026 / `e4aa407d-df53-58ab-b10a-6fc2b22d91d3`.
- **Limit:** Do not state that every old certificate ended upon PESEL UKR assignment without checking the exact statutory and record context.
- **Optional link:** Full article P.

#### C4 — Keep certificate and EU-level dates distinct

- **Stable step ID:** `old-certificate-separate-eu-duration`; **kind:** Recommendation.
- **Action:** When checking dates, identify whether the question concerns the paper certificate, Polish record/status, or EU temporary-protection decision.
- **Explanation:** The old-certificate transition through 4 March 2027 is document-specific. The Council decision extends EU temporary protection through 4 March 2028, applying from 5 March 2027; Article 106 concerns a defined statutory group and has exclusions/events.
- **Potential documents:** The certificate, current status record, and any individual authority decision.
- **Authority:** UdSC / gmina according to the record or document being checked.
- **Supporting article:** P.
- **Exact article passage:** “Council Implementing Decision (EU) 2026/1912 extends EU temporary protection to 4 March 2028, with that extension applying from 5 March 2027.”
- **Claims / research sources / production GUIDs:** TP07 / CERT / `7418e8ea-bc3d-561b-ba05-2f09ed29cdc0`; TP05 / EUTPACT / `0a989122-f3d4-5afd-a7ce-e10c4566cebe`; TP05 / LAW2026 / `e4aa407d-df53-58ab-b10a-6fc2b22d91d3`; TP01 / LAW2026 / same GUID.
- **Limit:** Never describe 4 March 2027 as the end of EU protection or 4 March 2028 as a guarantee of an individual certificate, record, or permit.
- **Optional link:** Full article P.

### Situation D — Longer-term residence and CUKR

#### D1 — Distinguish PESEL UKR and CUKR

- **Stable step ID:** `residence-distinguish-ukr-and-cukr`; **kind:** Recommendation.
- **Action:** Read the CUKR procedure as a separate residence-permit route, not as an update to the PESEL UKR number.
- **Explanation:** CUKR is a separate three-year temporary residence permit for a defined group with UKR status. PESEL UKR records Polish temporary-protection status; neither this guide nor a general description determines whether an individual meets CUKR conditions.
- **Potential documents:** Existing PESEL UKR record and residence/decision documents, if available and requested in the procedure.
- **Authority:** UdSC for CUKR procedure; MOS for the filing service.
- **Supporting article:** R (`c230d45f-9c76-5912-8c97-dc7d0689baa6`).
- **Exact article passage:** “PESEL UKR records temporary-protection status in Poland; it is not itself a temporary or permanent residence permit. A CUKR card is a separate three-year temporary residence permit for a defined group of people with UKR status.”
- **Claims / research sources / production GUIDs:** AT01 / CUKR / `41dea3fa-8ffc-5d2e-a476-aaf2cf07b109`.
- **Limit:** Do not present CUKR as equivalent to PESEL UKR or as automatically available to every UKR holder.
- **Optional link:** Full article R.

#### D2 — Check cumulative and child-specific conditions

- **Stable step ID:** `residence-check-cukr-conditions`; **kind:** Recommendation.
- **Action:** Compare the current official CUKR requirements with the person's records, and ask UdSC about unclear or child-specific conditions.
- **Explanation:** The article lists four cumulative conditions: valid PESEL UKR at application, UKR on 4 June 2025, UKR at card issuance, and at least 365 uninterrupted days with UKR status. Children born in Poland have separate conditions.
- **Potential documents:** Records relevant to each stated condition and any child-specific documents requested by UdSC; not a guide-generated eligibility checklist.
- **Authority:** UdSC.
- **Supporting article:** R.
- **Exact article passage:** “UdSC lists four cumulative CUKR conditions: valid PESEL UKR when applying; valid UKR status on 4 June 2025; valid UKR status when the card is issued; and at least 365 uninterrupted days with UKR status. Children born in Poland have separate conditions.”
- **Claims / research sources / production GUIDs:** AT02 / CUKR / `41dea3fa-8ffc-5d2e-a476-aaf2cf07b109`.
- **Limit:** The guide must not evaluate records or say that a reader qualifies; confirm the current conditions and exceptions with UdSC.
- **Optional link:** Full article R.

#### D3 — Check the MOS procedure and deadline

- **Stable step ID:** `residence-check-mos-and-cukr-deadline`; **kind:** Recommendation.
- **Action:** Check the live UdSC procedure and MOS instructions before deciding whether or how to file.
- **Explanation:** The recorded article says CUKR applications are online through MOS and may be filed until 4 March 2027. Procedures and deadlines are time-sensitive and must be rechecked before implementation/publication.
- **Potential documents:** MOS account/application materials only as required by the current procedure; this guide does not collect them.
- **Authority:** UdSC and MOS.
- **Supporting article:** R.
- **Exact article passage:** “A CUKR application is filed only online through MOS (Moduł Obsługi Spraw).” and “Applications may be filed until 4 March 2027.”
- **Claims / research sources / production GUIDs:** AT03 / CUKR / `41dea3fa-8ffc-5d2e-a476-aaf2cf07b109`; AT03 / MOS / `50dddaa7-ff1f-5c77-b4f7-2cf1254adda6`.
- **Limit:** The 4 March 2027 deadline is not a guarantee that a filed application will be granted. The live service must be checked.
- **Optional link:** Full article R.

#### D4 — Separate other permits, filing, and grant

- **Stable step ID:** `residence-clarify-other-permit-effects`; **kind:** Recommendation.
- **Action:** Before filing or withdrawing any residence application, ask UdSC or the competent voivode how that specific procedure and case stage interact with the current record.
- **Explanation:** Ordinary residence routes have their own criteria. Polish temporary protection ends on specified legal events, including grant of certain permits or international protection; filing alone is not the same event. The effect depends on the procedure and case.
- **Potential documents:** Individual decisions, application receipts, and official correspondence if requested by the authority.
- **Authority:** UdSC / MOS / competent voivode, depending on the procedure.
- **Supporting article:** R.
- **Exact article passages:** “From 5 March 2026, the special basis for certain simplified permits under the Ukraine assistance law ended; UdSC says the general Foreigners Act requirements apply again.” “Under the 2026 Act, Polish temporary protection ends on specified events, including the grant of certain residence permits or international protection. Filing an application alone is not the same event; check the effect of your specific procedure before applying.”
- **Claims / research sources / production GUIDs:** AT04 / UDSCCHANGE / `ddffc1ab-c2e0-5f7a-b89f-9fafb794e4b5`; AT06 / LAW2026 / `599f4d31-e4d8-5ebf-82ed-149d5d677dd9`.
- **Limit:** Do not say that filing necessarily ends protection, that a permit will be granted, or that a particular event has a definite effect on an individual case.
- **Optional link:** Full article R.

## D. Evidence coverage and source-ID crosswalk

Each step above identifies its article, claim IDs, research labels, and exact production GUIDs. These existing source records were checked against `official-sources.json` for existence, article ownership, and URL correspondence with the research registers. This is a catalog-integrity check, not a new check of the live legal sources or independent confirmation of each claim.

### Primary article P — `218207bd-2a85-59e1-a945-eedb0604392e`

| Research label | Existing OfficialSource GUID | URL from source register |
| --- | --- | --- |
| LAW2026 | `e4aa407d-df53-58ab-b10a-6fc2b22d91d3` | https://dziennikustaw.gov.pl/DU/2026/203/D2026000020301.pdf |
| PESEL | `0dfc274b-f02d-5acb-913e-51dee3c7fa24` | https://www.gov.pl/web/gov/uzyskaj-numer-pesel-oraz-profil-zaufany--usluga-dla-obywateli-ukrainy |
| IDENTITY | `433faca8-dcb6-59a1-8d32-5e38d16049b0` | https://www.gov.pl/web/udsc/wazna-informacja-dla-obywateli-ukrainy-posiadajacych-numer-pesel-ze-statusem-ukr |
| CERT | `7418e8ea-bc3d-561b-ba05-2f09ed29cdc0` | https://www.gov.pl/web/udsc/zaprzestanie-wydawania-zaswiadczen-potwierdzajacych-korzystanie-z-ochrony-czasowej |
| UDSCSTAY | `4eba2e3a-fa21-5803-929c-c39d0e19a7a2` | https://www.gov.pl/web/udsc/zmiany-w-szczegolnych-zasadach-dotyczacych-legalnosci-pobytu-obywateli-ukrainy-na-terytorium-rzeczypospolitej-polskiej |
| EUTPACT | `0a989122-f3d4-5afd-a7ce-e10c4566cebe` | https://eur-lex.europa.eu/eli/dec_impl/2026/1912/oj/eng/pdf |
| UDSC2028 | `d0fc2583-ea0d-5289-b9c7-de3e97417529` | https://www.gov.pl/web/udsc/przedluzenie-ochrony-czasowej-do-4-marca-2028-r |
| EUTPCORR | `6a544d55-b307-5747-bddd-df1c57f678a6` | https://eur-lex.europa.eu/eli/dec_impl/2026/1912/corrigendum/2026-08-05/oj/eng/pdf |
| KSI2028 | `5259d868-4cfb-5b58-97a5-a26975b7c283` | https://www.gov.pl/web/krajowa-siec-integracji/przedluzenie-ochrony-czasowej-do-4-marca-2028-r |
| MRPiPS2026 | `66d98c8a-b567-5864-806b-120408bb3a93` | https://www.gov.pl/web/rodzina/co-sie-zmienia-w-pomocy-dla-obywateli-ukrainy-w-2026-r |

### Secondary article R — `c230d45f-9c76-5912-8c97-dc7d0689baa6`

| Research label | Existing OfficialSource GUID | URL from source register |
| --- | --- | --- |
| CUKR | `41dea3fa-8ffc-5d2e-a476-aaf2cf07b109` | https://www.gov.pl/web/udsc/CUKR-procedura |
| MOS | `50dddaa7-ff1f-5c77-b4f7-2cf1254adda6` | https://www.gov.pl/web/udsc/info-mos |
| LAW2026 | `599f4d31-e4d8-5ebf-82ed-149d5d677dd9` | https://dziennikustaw.gov.pl/DU/2026/203/D2026000020301.pdf |
| UDSCCHANGE | `ddffc1ab-c2e0-5f7a-b89f-9fafb794e4b5` | https://www.gov.pl/web/udsc/zmiany-w-szczegolnych-zasadach-dotyczacych-legalnosci-pobytu-obywateli-ukrainy-na-terytorium-rzeczypospolitej-polskiej |
| CERT | `1cc08213-ec62-5f77-b688-89e3c7474a28` | https://www.gov.pl/web/udsc/zaprzestanie-wydawania-zaswiadczen-potwierdzajacych-korzystanie-z-ochrony-czasowej |

**Per-step association:** A1–C4 use article P and only P-owned source GUIDs. D1–D4 use article R and only R-owned source GUIDs. `ArticleId` is resolved per step in the existing guide model; the secondary article is not represented as evidence owned by P. No source is inferred from its title or invented. TP06 uses the corrected corrigendum record and may mention KSI only as stale guidance; KSI's superseded dates are not treated as governing law.

## E. Legal and concept distinctions

Keep these concepts distinct in every step and future translation:

- EU temporary protection under Council Decision (EU) 2026/1912.
- Polish temporary-protection status and the defined Article 106 lawful-stay rule.
- A PESEL number and a PESEL record marked UKR.
- An older paper temporary-protection certificate.
- CUKR as a separate temporary residence permit/card.
- Other temporary, permanent, or EU long-term-resident permits.
- Filing an application and a competent authority granting a permit.

The guide must not conflate a document, registry status, EU-level protection duration, Polish statutory stay rule, or individual permit validity. It must not decide eligibility or the effect of a particular filing for a person.

## F. PESEL UKR identity-confirmation treatment

The identity-confirmation step is limited to records issued solely on a declaration without presentation of a valid passport. The recorded deadline was **31 August 2026**; the article states that the deadline has passed and that failure for the affected cohort led to loss of UKR status and Polish temporary protection from **1 September 2026**. The action is to ask the gmina to verify the actual current record. The guide must not infer loss or continuation of status from a PESEL number and must never ask for passport or status data.

## G. 2027 / 2028 date distinction

- Council Implementing Decision (EU) 2026/1912: EU temporary protection through **4 March 2028**, with the extension applying from **5 March 2027**.
- Article 106 of the Polish 2026 Act: lawful stay is linked to the Council decision for its defined statutory group, subject to statutory exclusions and ending events; this is not an individual-record or permit-validity determination.
- Ministry page dated 26 March 2026: its 4 March 2027 statement predates the later Council decision and is not presented as a competing current statutory end date for the covered group.
- Older UdSC paper certificates: a separate document-specific transition through **4 March 2027**, subject to earlier-ending events/status changes.

No date in this section establishes an individual person's current record or permit validity.

## H. Older paper-certificate transition

UdSC stopped issuing new former certificates from **5 March 2026**. UdSC says previously issued paper certificates remain valid until **4 March 2027**, subject to earlier-ending events; the production article also notes PESEL UKR assignment as a relevant event. This is a certificate transition, not the end of EU temporary protection. A user should ask the competent authority to check how the certificate and any later record relate in their case.

## I. Corrigendum and stale KSI dates

The approved article records the 5 August 2026 Official Journal corrigendum: Article 2 applies from **5 August 2026**; the same-Member-State continuous-protection exception refers to **4 August 2026**. KSI's separate gov.pl page was recorded as still showing superseded **31/30 July** dates. The guide may attribute and flag that stale page and direct readers to the corrected instrument/current authority guidance. It must not treat those stale dates as governing law or add a standalone eligibility calculator for military obligations.

## J. CUKR and onward residence route

Situation D uses article R because its evidence belongs to the Residence Options entry. It must preserve that CUKR is a separate three-year temporary residence permit, distinct from PESEL UKR; its listed conditions are cumulative, with separate rules for children born in Poland; the recorded filing route is MOS; and the recorded application deadline is **4 March 2027**. Ordinary residence routes have distinct criteria. Filing is not grant, and the 2026 Act lists specified events that may end Polish temporary protection, including grant of certain permits or international protection. The guide must not decide what filing, grant, or another event means in an individual's case.

## K. Interaction design

Reuse the existing Germany / Denmark guide pattern:

1. **Situation selection:** “What do you need help with?” and four cards for A–D.
2. **Step view:** selected situation, navigation-only step number/progress, “Recommendation” label, practical action, explanation, potentially relevant documents, responsible authority, limitation, official source links, and a link to the supporting full article.
3. **Controls:** Back, Previous, Next, and Change situation.

The progress indicator is navigation only and does not indicate eligibility, completion of legal requirements, or a successful application. Do not persist answers, request personal facts/passport data, upload documents, calculate status, or store progress. A guide choice is not an eligibility determination.

## L. Architecture reuse

The current model already supports country-aware guide identity, slug, editorial status, UK/EN/RU translations, scenarios, ordered steps, `ArticleId` per step, `ClaimIds`, and `OfficialSourceIds`. Scenario D can point its steps to R while A–C point to P; the existing per-step `ArticleId` is sufficient. The guide view resolves sources and article links from each step's IDs. No schema change or new routing framework is indicated by this proposal.

The current page/route integration may still need country-aware extension before Poland can render. That is implementation work for a later authorized phase, not a reason to add a model field here. Phase 12.2 must verify route resolution, source/article ownership validation, and full-article links for both article IDs while retaining existing Germany and Denmark behavior.

## M. Localization approach

English is the canonical design language, not final publication copy. Later UK/EN/RU versions require equivalent human-reviewed meaning. Preserve these terms and dates consistently: **PESEL UKR**, **CUKR**, **UdSC**, **gmina**, **MOS**, **31 August 2026**, **1 September 2026**, **4 March 2027**, **5 March 2027**, **4 March 2028**, **4 August 2026**, and **5 August 2026**. Use natural explanatory translations without merging legal concepts, changing route slugs, or turning examples into mandatory documents. Translation review must confirm all conditions and date relationships.

## N. Publication-time rechecks

No fresh legal research is part of Phase 12.1. Before any later integration/publication, recheck:

- Whether KSI has corrected the superseded 31/30 July dates; use the corrigendum as the controlling date reference.
- Current PESEL UKR service instructions, 30-day rule, application location, in-person procedure, and local/child/missing-document instructions.
- Identity-confirmation cohort, consequences, and any later law or official clarification.
- Council Decision (EU) 2026/1912 and its corrigendum, including 4/5 August dates.
- Article 106 and current Polish amendments, exclusions, and ending events.
- UdSC's old-certificate transition and earlier-ending events.
- CUKR cumulative and child conditions, 4 March 2027 deadline, and current MOS procedure.
- Article 109b and the scope of events ending Polish temporary protection; distinguish filing from grant.
- Source freshness, all article passages, source ownership, and UK/EN/RU translation consistency.

## O. Scope and limitations

This is an editorial/architecture proposal based on the existing approved articles and recorded research matrices, not an independent legal opinion, source re-verification, eligibility assessment, or publication approval. Operational instructions and legal rules can change. The production articles' `Verified` status does not approve this guide. The future guide must remain `Draft` until a separate human review and publication gate; no deployment is authorized by this document.

## P. Phase 12.2 acceptance criteria

- [ ] Human editorial acceptance of all four scenarios and each proposed step.
- [ ] Every step's exact article passage, ClaimId, research label, source GUID, URL, and owning article checked; no cross-article source relationship or invented source.
- [ ] P remains the supporting article for A–C; R remains the supporting article for D; per-step `ArticleId` handles both without a schema change.
- [ ] PESEL vs PESEL UKR, EU protection vs Polish status, old certificate, CUKR, other permits, and filing vs grant remain distinct.
- [ ] The 31 August / 1 September 2026 cohort-specific identity rule does not become an individual status inference.
- [ ] 4 March 2027 certificate transition, 4 March 2028 EU protection, and 5 March 2027 application date remain correctly scoped.
- [ ] 4 August / 5 August 2026 corrigendum dates are retained; stale KSI 31/30 July wording is not presented as governing law.
- [ ] CUKR conditions, separate child rules, MOS procedure, deadline, Article 109b events, and filing/grant distinction receive publication-time recheck.
- [ ] No personal data collection, passport entry, upload, eligibility engine, or stored answers.
- [ ] UK/EN/RU translation review and accessible desktop/mobile implementation tests pass.
- [ ] Guide starts Draft and remains hidden in Production until separately approved; no article/source status is changed.
- [ ] Existing Germany and Denmark guide routes, source links, article links, and Draft production gate remain intact.

**Catalog validation for this proposal:** P and R exist under Poland and Temporary Protection; both currently have `uk`, `en`, `ru` translations marked `Verified`. All 15 listed source GUIDs exist, the 10 primary GUIDs belong to P and the five secondary GUIDs belong to R, and their URLs match the two research source registers. The proposal contains 16 stable step IDs and no production changes.

**AWAITING HUMAN EDITORIAL ACCEPTANCE.**
