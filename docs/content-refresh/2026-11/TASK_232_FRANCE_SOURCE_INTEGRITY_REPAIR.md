# Task 232 — France temporary-protection source repair

Task 231 stopped because active source `44c2a53e-f0ab-5eb3-b167-016b7e89c86d` for France `FR-TP-01` and Compare `protection-application` redirected to an `/introuvable/` page despite returning HTTP 200.

Updated that source record in place to the current official Seine-Saint-Denis procedure: https://demarche.numerique.gouv.fr/commencer/prefecture-seine-saint-denis-aps-protection-temporaire-ukraine. The page covers first applications and renewals of APS under temporary protection for displaced Ukrainians and limits use to Seine-Saint-Denis residents. The source ID and article/Compare bindings remain unchanged; article text and Compare values were not changed.

Validation: old route redirects to `/introuvable/`; replacement resolves directly to the current procedure. Static source/reference checks and exact-SHA CI are required before closing this repair. Task 231 must be rerun independently from the new production SHA. Password/access gate unchanged; site not published.
