# Eight-Country Private Beta — Live Verification Preparation

- Owner-deployed application SHA: `2f0a95bd70817b33a2c8d48c4631bb9a0c39e261`
- Deployment method: owner manually deployed through the existing Render service.
- Access: Private Beta protected by Basic Auth.
- Authenticated live verification: **pending**.

## Expected Production scope

The deployed application is expected to expose exactly these eight countries:

- Denmark
- Germany
- Poland
- Czechia
- Spain
- Netherlands
- Sweden
- Romania

Romania entered Production through the existing generic country-visibility rule after its owner-approved promotion. Its accepted content is 24 Verified translations across 8 articles and 24 Approved comparison facts.

## Preserved holds

- Netherlands: the three held translations and four held comparison facts remain outside the accepted subset; `residence-filing` remains unavailable and source-less.
- Sweden: `social-child-benefits` remains an unresolved evidence gap, unavailable and without source IDs.

## Pre-verification workflows

- CI for deployed application SHA `2f0a95bd70817b33a2c8d48c4631bb9a0c39e261`: success.
- Netherlands visual evidence for that SHA: success.

## Next step and verdict

The owner should run the authenticated, read-only deployment smoke, eight-country scope, and Netherlands hold verifiers against the existing live deployment. No credentials are recorded in this document.

**READY FOR OWNER LIVE VERIFICATION**

This preparation record does not claim that authenticated live verification has passed and is not a final Live release record.
