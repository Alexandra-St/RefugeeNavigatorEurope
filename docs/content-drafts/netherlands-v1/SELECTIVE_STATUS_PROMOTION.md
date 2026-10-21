# Phase 18.4W — Selective Status Promotion

Date: 21 October 2026

Baseline: `cd9fb989f3dd3c3312f0d76c19c6f4ed5a5ca794`

The project owner authorized status promotion for the bounded Netherlands subset in this phase. No article or comparison wording was changed.

## Core translations promoted

All EN / UK / RU translations are now `Verified` for:

- NL-EMP-02 — Work Authorization and Employee Rights
- NL-HEALTH-01 — Healthcare in the Netherlands: RMO and Urgent Care
- NL-HOUSING-01 — Finding Housing in the Netherlands
- NL-SOC-01 — Social Assistance in the Netherlands

Total promoted: 12 translations.

## Comparison facts promoted

The following 12 Netherlands facts are now `Approved`:

- `work-permission`, `work-employer-duties`, `work-minimum-pay`
- `healthcare-access`, `healthcare-proof`, `healthcare-urgent`
- `housing-initial`, `housing-private-rental`, `housing-financial-support`
- `social-subsistence`, `social-application`, `social-child-benefits`

Their values, availability, article links, source links, IDs, and order are unchanged. The UWV/RMO conflict in NL-HEALTH-01 and `healthcare-access` remains as documented; this promotion does not resolve or restate it.

## Content intentionally left pending

The following four articles remain `NeedsReview` in EN / UK / RU: NL-TP-01, NL-TP-02, NL-EMP-01, and NL-EDU-01. They are pending review, not rejected.

The corresponding 12 facts remain `Draft`: `protection-scheme`, `protection-application`, `protection-dates`, `residence-routes`, `residence-conditions`, `residence-filing`, `employment-vacancies`, `employment-institutions`, `employment-assistance-conditions`, `education-school-enrolment`, `education-pupil-language`, and `education-higher-admission`. `residence-filing` remains unavailable and has no official sources.

## Resulting status counts

| Scope | Verified / Approved | NeedsReview / Draft |
|---|---:|---:|
| All article translations | 132 Verified | 36 NeedsReview |
| Netherlands article translations | 12 Verified | 12 NeedsReview |
| All comparison facts | 132 Approved | 36 Draft |
| Netherlands comparison facts | 12 Approved | 12 Draft |

## Validation

- Targeted Netherlands content and comparison tests: 82 passed.
- Full Release test suite: 566 passed, 0 failed.
- Production category rendering checked in EN / UK / RU: the four promoted articles and their sources render; the four held articles, their content, and their article panels do not.
- Production comparison rendering checked in EN / UK / RU: the 12 approved facts and their owned sources render; draft facts and source panels remain suppressed. The unavailable, source-less `residence-filing` answer remains unavailable.
- JSON parsing, exact status counts, baseline comparisons for all non-status fields, source ownership, and `git diff --check` passed.

## Release boundary

This status promotion does not authorize deployment. No hosted production change or deployment was performed. The held content and documented source limitations remain subject to their existing review gates.
