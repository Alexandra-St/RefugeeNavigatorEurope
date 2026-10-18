# Denmark Temporary Protection Guide — V1 Publication Record

**Status: Approved for Private Beta by the project owner on 18 October 2026.**

**Publication-time source check: 18 October 2026.**
**Deployment: Not performed.**

## Guide and acceptance

The approved guide is `denmark-temporary-protection-v1` at `/guides/denmark/temporary-protection`. It contains four scenarios and sixteen steps, each available in Ukrainian, English and Russian. The project owner completed editorial design and implementation review and accepted the desktop and narrow/mobile visual review. Editorial approval permits Production rendering; it is not independent legal certification and does not itself deploy the guide.

The publication-gate check confirmed the implemented wording remains supported by the current reviewed material. No guide wording, scenario, step, ID, ClaimId, source relationship or article relationship changed during this approval. Existing distinction between EU temporary protection and Denmark's Special Act; conditional routes; individual permit decisions; pending-work conditions; travel and re-entry; limited healthcare; and the section 57(5) exception remains intact. The guide makes no automated eligibility decision and stores no personal answers.

## Publication-time source recheck

The existing fifteen source records linked to the guide's primary article were reopened and checked. Their `LastCheckedAt` values were updated to `2026-10-18T20:36:29+00:00`; source IDs, titles, URLs and relationships were not changed.

| Research label | Rechecked source | Result relevant to this guide |
| --- | --- | --- |
| S01 | [Danish Immigration Service — application under the Special Act](https://www.nyidanmark.dk/en-GB/You-want-to-apply/Ukraine/Displaced-from-Ukraine) | Page still states 17 March 2027; 35 days is an average maximum target; current 23–59 military scope and application conditions remain stated. |
| S02 | [SL1 application form and instructions](https://www.nyidanmark.dk/-/media/Files/US/Application-forms/Diverse/sl1_en.pdf) | Reopened; current form issue is dated 4 September 2026. It contains form-specific military questions; the guide continues to defer to current agency criteria and does not broaden the affected group. |
| S03 | [Danish Immigration Service — family-member application](https://www.nyidanmark.dk/en-GB/You-want-to-apply/Ukraine/Family-to-displaced-from-ukraine) | Page still states 17 March 2027 and retains the conditional section 2 family categories and military scope. |
| S04 | [Danish Immigration Service — waiting for an answer](https://www.nyidanmark.dk/en-GB/You-are-waiting-for-an-answer/Fordreven-fra-Ukraine) | 35 days remains an average target, not a guarantee. Pending work remains conditional on application and biometrics, with the obvious-ineligibility caveat; travel/re-entry and healthcare remain limited as described. |
| S05 | [Danish Immigration Service — positive decision](https://www.nyidanmark.dk/en-GB/You-have-received-an-answer/Ukraine/Ja-Ukraine) | Reopened; decision-specific permit, travel and municipality guidance remains consistent with the guide's caution to read the individual decision. |
| S06 | [Danish Immigration Service — negative decision](https://www.nyidanmark.dk/en-GB/You-have-received-an-answer/Ukraine/Nej-Ukraine) | Reopened; appeal guidance remains decision-specific. The page is dated 2023; the guide gives no numeric deadline and directs readers to their decision/current instructions. |
| S07 | [Danish Immigration Service — military-age rules](https://www.nyidanmark.dk/en-GB/News-Front-Page/2026/09/New-rules-for-Ukrainian-men-of-military-age-(23-59-years)) | The page still describes Ukrainian men aged 23–59 subject to mobilisation rules, applications from 25 June 2026, the 3 September 2026 transition, the earlier-permit caveat, and the under-23 validity qualification. |
| S08 | [BEK no. 828/2026](https://www.retsinformation.dk/eli/lta/2026/828) | Direct browser review confirmed the regulation is in force from 1 October 2026 and provides a conditional possible extension through 17 March 2028, subject to section 10 grounds. |
| S09 | [Danish Immigration Service — information to people from Ukraine](https://nyidanmark.dk/en-GB/Words-and-concepts/F%C3%A6lles/Information-to-Ukrainian-citizens) | Reopened; extension information retains legacy date/process references. It does not resolve the conflict between BEK 828 and the current application/family pages. |
| S11 | [Life in Denmark — when you arrive](https://lifeindenmark.borger.dk/theme/when-you-arrive) | Reopened; general arrival and registration information remains a separate navigation resource, not a substitute for a Special Act permit. |
| S14 | [Act no. 725/2026](https://www.retsinformation.dk/eli/lta/2026/725) | Reopened enacted amendment. Its scope and transition provisions remain consistent with the agency's described military rule; the guide retains the agency's narrow affected group and caveats. |
| S15 | [Special Act, Act no. 324/2022](https://www.retsinformation.dk/eli/lta/2022/324) | Reopened. This is the original Act record, not a consolidated current text; the guide does not use it to state current numeric appeal deadlines. |
| S16 | [Council Directive 2001/55/EC](https://eur-lex.europa.eu/legal-content/EN/ALL/?uri=celex%3A32001L0055) | Reopened; no change affecting the guide's distinction between the EU framework and the national Danish Special Act was identified. |
| S17 | [Official PDF rendering of Act no. 324/2022](https://www.retsinformation.dk/api/pdf/229946) | Reopened and readable. It is the original 2022 enactment, not a current consolidated version; no numeric appeal deadline is reproduced in the guide. |
| S18 | [Integration Act, LBK no. 453/2026](https://www.retsinformation.dk/eli/lta/2026/453/pdf) | Current text of §57(5) still limits the section 12 accommodation exception to a permit under Special Act §2(1), with a very-special-personal-circumstances exception. |

### Open limitations and discrepancies

- **2027/2028 dates remain unresolved:** S01 and S03 still state 17 March 2027; BEK 828/2026 provides a possible extension through 17 March 2028 subject to its conditions. The guide retains both attributions, does not reconcile them, and tells readers to check their individual decision and current written communication. Neither general date is presented as an individual's expiry date.
- **L 43 remains a proposal:** the [Folketing status page](https://www.ft.dk/samling/20261/lovforslag/L43/index.htm) showed `Fremsat`, presented on 8 October 2026, with first reading scheduled for 13 November 2026. It is not treated as current law.
- S09 contains legacy extension references. S06 is an older decision page. S15 and S17 are original 2022 Act records rather than consolidated current text. The guide avoids deriving a current personal outcome or a universal numeric appeal deadline from these limitations.
- The public text extractor returns a JavaScript shell for S08; the official regulation was nevertheless reviewed directly in the browser. No inaccessible source was treated as verified solely from a search snippet.

## Visibility and validation

The guide's editorial status is `Approved`; the existing publication gate therefore permits it in both Development and Production. Its contextual discovery card is available on Denmark's temporary-protection category page. Approved content does not display the Draft preview notice. Germany remains Approved and unchanged; synthetic Draft guides remain Development-only and retain their preview notice. Poland and unrelated categories have no matching guide card.

The guide retains four scenarios, sixteen steps, UK/EN/RU translations, the existing ClaimIds and official-source mappings. Automated tests cover those relationships, production and development rendering, absence of the Denmark Draft notice, category discovery behavior, the date conflict, military scope and pending-application qualifications. The owner's desktop and narrow/mobile visual acceptance was completed before this publication gate.

No Render deployment or configuration change was performed.
