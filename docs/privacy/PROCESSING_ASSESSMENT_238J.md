# Internal processing and legitimate-interest assessment — 238J

**Status: preliminary working assessment for operator review.** This is not legal advice, a completed Data Protection Impact Assessment, or a legal-compliance certification. The operator has not yet confirmed the legitimate-interest assessment or several provider/legal facts below. Do not treat the proposed basis as finally established until that review is complete.

## Processing and purposes

| Activity and data | Purpose | Proposed basis to review | Necessity and limits |
|---|---|---|---|
| Contact submission: required topic and free-text message; optional name and reply email. Message is limited to 5,000 characters. | Handle and reply to enquiries, correct errors, maintain the project. | Intended legitimate interests under GDPR Article 6(1)(f), subject to the operator's documented purpose, necessity and balancing assessment. Article 6(1)(b) may apply only to a specific request for pre-contractual steps; Article 6(1)(c) only where a concrete legal obligation applies. | Topic and message support a response. Name and email are optional. The form has no attachment field. Do not solicit identity documents, medical records or other sensitive details. |
| Language preference cookie; selected theme in browser `localStorage`. | Remember a visitor-selected language and theme. | The GDPR basis and any separate cookie/e-privacy requirements depend on the actual deployment and must be confirmed. Do not use consent as a blanket basis. | Language cookie is set for one year. Theme remains client-side under `theme` until changed or cleared. No visitor account is created. |
| Network and service operation data, potentially IP address, path, timestamp and user-agent; application logs. | Deliver pages and operate/secure the service. | Intended legitimate interests where applicable, pending operator assessment of actual fields, access and necessity. Any legal-obligation basis is limited to a specific applicable obligation. | Render Hobby application-log retention is confirmed as 7 days. This does not establish retention/access for every provider-side technical record; the workspace has no Pro-level HTTP request logs. |
| Message processing by Formspree and mailbox handling by Proton Mail. | Transmit and receive contact correspondence. | Provider processing role and agreement applicable to these accounts are unresolved. | Formspree Submission Archive is disabled per operator; Formshield is enabled, CAPTCHA and the Formspree dashboard “HTTP API” option are disabled per operator. The app sends JSON by HTTPS to the configured Formspree form endpoint; compatibility with that dashboard setting and actual production delivery are unverified. |
| Ko-fi, only if the visitor follows the external support link. | Optional support transaction on Ko-fi. | Ko-fi's independent processing and any project-specific implications need review. | No Ko-fi widget or embedded payment flow is present in the app. Access to site information does not depend on support. |

## Necessity, user impact and balancing

The project provides information to refugees and displaced people. Some visitors may be in vulnerable circumstances and may include health, immigration, family, identity or location details in an unrestricted message despite the warning. Accidental disclosure or unauthorized access could have disproportionate consequences for them. The free-text field accepts arbitrary text; the current application has no sensitive-data detector, redaction step or technical submission block.

The contact purpose is limited to website-related enquiries, corrections and maintenance. A topic and concise message appear proportionate; mandatory identity details, identity documents and detailed personal narratives are not needed. Name and reply email remain optional. The current warnings reduce surprise but cannot ensure that users comply. Formspree's published Privacy Policy says its service must not be used to collect sensitive personal data; therefore the form copy must clearly prohibit such submissions, and the operator should decide whether additional proportionate safeguards are needed before public launch.

Potential risks include overcollection in free text, exposure in a provider or mailbox account, provider-side security logs or backups, cross-border processing, mistaken assumptions about deletion, and delivery failure. Existing safeguards include HTTPS to Formspree, no project database for submissions, no intentional application logging of form fields, optional name/email, a 12-month post-resolution mailbox retention rule (subject to justified legal exceptions), disabled Formspree Submission Archive, and an explicit sensitive-data warning. The 12-month deletion action and provider-side deletion are not technically verified. Actual Formspree-to-Proton delivery is not verified.

**Preliminary balance:** there is a plausible project interest in answering limited site-related messages, but the operator has not documented/confirmed the required balancing assessment. Given the audience and unrestricted message field, the residual risk is material even though the form asks users not to submit sensitive data. This document does not conclude that Article 6(1)(f) is satisfied. Operator approval and the open reviews below are required before relying on that basis.

## Data minimization, retention and safeguards

- Keep topic and message limited to what is needed; name and reply email remain optional. Do not request attachments or supporting identity/medical documents.
- Contact UI and Privacy text must say not to submit sensitive personal data, identity documents, or medical information/records. The warning is advisory; there is currently no technical detection or blocking safeguard.
- Keep message contents out of application logs and project databases. Verify this against the deployed service logs during the synthetic production check.
- Operator retention rule: Proton Mail correspondence for 12 months after enquiry resolution, subject to justified legal exceptions. Confirm the operational deletion process and exception handling. Formspree archiving is disabled, but this does not prove no temporary processing, security logs or backups.
- Render application logs: 7 days in the configured Hobby workspace. Do not infer that every technical/provider record has the same retention period.
- Formspree states that it uses AWS infrastructure in the United States and relies on Standard Contractual Clauses. Confirm the agreement and transfer mechanism applicable to this account; do not claim an executed customer DPA without evidence. Proton terms/DPA applicability is conditional and must be checked for this operator/account.
- With Basic Auth enabled, submit one synthetic message after deployment; verify Formspree processing, operator-confirmed Proton receipt, localized success feedback, and absence of message text in application logs. Do not send real or sensitive data.

## Unresolved operator/legal questions

1. Confirm the legitimate-interest purpose, necessity and balancing assessment for each activity; identify any processing that instead relies on a specific contract step or legal obligation. This confirmation is pending.
2. Determine whether Portuguese Decreto-Lei n.º 7/2004, Articles 3 and 10, apply to the project's actual activities and what operator identification/contact disclosures follow.
3. Confirm whether a localized Contact form alone is sufficient as the operator's public contact arrangement.
4. Confirm the Formspree DPA and SCC/transfer safeguards applicable to the actual account, plus compatibility of the “HTTP API disabled” setting with the app's HTTPS JSON form endpoint.
5. Review Ko-fi implications for the project's existing optional external support link.
6. Confirm provider-side retention/deletion, access and technical-log details where relevant; keep unknown facts expressly unknown.

## Implementation facts and references

- Contact implementation: `Contact.razor`, `ContactSubmissionService.cs`, `ContactFormInput.cs`; the app sends a JSON POST to `https://formspree.io/f/{configured-id}` and stores no message in a project database.
- Operator-confirmed account/deployment facts are recorded in the 238J release checklist; the repository cannot independently establish external account settings or successful production mail delivery.
- Formspree Privacy Policy: <https://formspree.io/legal/privacy-policy/> (prohibits using its service to collect sensitive personal data).
- Formspree Security: <https://formspree.io/security/>.
- Render logging and DPA: <https://render.com/docs/logging>, <https://render.com/dpa>.
- Proton Terms and DPA: <https://proton.me/legal/terms>, <https://proton.me/legal/dpa>.
- Portuguese Decreto-Lei n.º 7/2004: <https://diariodarepublica.pt/dr/legislacao-consolidada/decreto-lei/2004-73199154-73197486>.
- GDPR: <https://eur-lex.europa.eu/eli/reg/2016/679/oj/eng/>.
