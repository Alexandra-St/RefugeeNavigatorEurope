#!/usr/bin/env python3
"""Authenticated, read-only live check for the accepted eleven-country scope."""
from __future__ import annotations

import os
import sys
import time
from collections import Counter
from typing import Callable

import netherlands_live_hold_test as live

COUNTRIES = ("denmark", "germany", "poland", "czechia", "spain", "netherlands", "sweden", "romania", "slovakia", "bulgaria", "austria")
CATEGORIES = ("temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance")
SEARCH_RESULT_COUNTS = {"czechia": 8, "spain": 8, "netherlands": 7, "sweden": 8, "romania": 8, "slovakia": 8, "bulgaria": 8, "austria": 8}
LANGUAGES = ("uk", "en", "ru")
COUNTRY_NAMES = {"en": "Romania", "uk": "Румунія", "ru": "Румыния"}
SWEDEN_NAMES = {"en": "Sweden", "uk": "Швеція", "ru": "Швеция"}
SLOVAKIA_NAMES = {"en": "Slovakia", "uk": "Словаччина", "ru": "Словакия"}
BULGARIA_NAMES = {"en": "Bulgaria", "uk": "Болгарія", "ru": "Болгария"}
AUSTRIA_NAMES = {"en": "Austria", "uk": "Австрія", "ru": "Австрия"}
BULGARIA_CATEGORY_COUNTS = {
    "temporary-protection": 2,
    "employment": 2,
    "healthcare": 1,
    "housing": 1,
    "education": 1,
    "social-assistance": 1,
}
AUSTRIA_CATEGORY_COUNTS = {
    "temporary-protection": 2,
    "employment": 2,
    "healthcare": 1,
    "housing": 1,
    "education": 1,
    "social-assistance": 1,
}
SLOVAKIA_ASPECTS_BY_CATEGORY = {
    "temporary-protection": ("protection-scheme", "protection-application", "protection-dates",
                             "residence-routes", "residence-conditions", "residence-filing"),
    "employment": ("employment-vacancies", "employment-institutions", "employment-assistance-conditions",
                   "work-permission", "work-employer-duties", "work-minimum-pay"),
    "healthcare": ("healthcare-access", "healthcare-proof", "healthcare-urgent"),
    "housing": ("housing-initial", "housing-private-rental", "housing-financial-support"),
    "education": ("education-school-enrolment", "education-pupil-language", "education-higher-admission"),
    "social-assistance": ("social-subsistence", "social-application", "social-child-benefits"),
}
ROMANIA_ASPECTS_BY_CATEGORY = {
    "temporary-protection": ("protection-scheme", "protection-application", "protection-dates",
                             "residence-routes", "residence-conditions", "residence-filing"),
    "employment": ("employment-vacancies", "employment-institutions", "employment-assistance-conditions",
                   "work-permission", "work-employer-duties", "work-minimum-pay"),
    "healthcare": ("healthcare-access", "healthcare-proof", "healthcare-urgent"),
    "housing": ("housing-initial", "housing-private-rental", "housing-financial-support"),
    "education": ("education-school-enrolment", "education-pupil-language", "education-higher-admission"),
    "social-assistance": ("social-subsistence", "social-application", "social-child-benefits"),
}
BULGARIA_ASPECTS_BY_CATEGORY = {
    "temporary-protection": ("protection-scheme", "protection-application", "protection-dates",
                             "residence-routes", "residence-conditions", "residence-filing"),
    "employment": ("employment-vacancies", "employment-institutions", "employment-assistance-conditions",
                   "work-permission", "work-employer-duties", "work-minimum-pay"),
    "healthcare": ("healthcare-access", "healthcare-proof", "healthcare-urgent"),
    "housing": ("housing-initial", "housing-private-rental", "housing-financial-support"),
    "education": ("education-school-enrolment", "education-pupil-language", "education-higher-admission"),
    "social-assistance": ("social-subsistence", "social-application", "social-child-benefits"),
}
AUSTRIA_ASPECTS_BY_CATEGORY = {
    "temporary-protection": ("protection-scheme", "protection-application", "protection-dates",
                             "residence-routes", "residence-conditions", "residence-filing"),
    "employment": ("employment-vacancies", "employment-institutions", "employment-assistance-conditions",
                   "work-permission", "work-employer-duties", "work-minimum-pay"),
    "healthcare": ("healthcare-access", "healthcare-proof", "healthcare-urgent"),
    "housing": ("housing-initial", "housing-private-rental", "housing-financial-support"),
    "education": ("education-school-enrolment", "education-pupil-language", "education-higher-admission"),
    "social-assistance": ("social-subsistence", "social-application", "social-child-benefits"),
}
NOT_FOUND = {
    "uk": "Сторінку не знайдено",
    "en": "Page not found",
    "ru": "Страница не найдена",
}


class Reporter:
    def __init__(self) -> None:
        self.count = 0
        self.failed = False

    def check(self, name: str, expected: str, ok: bool, actual: str) -> None:
        self.count += 1
        self.failed |= not ok
        print(f"{'PASS' if ok else 'FAIL'} | {name} | expected: {expected} | actual: {actual[:120]}")


def nodes(root: live.Element, tag: str, class_name: str) -> list[live.Element]:
    return list(root.descendants(tag, class_name))


def country_card_slugs(body: str) -> list[str]:
    root = live.parse_document(body)
    slugs = []
    for card in nodes(root, "article", "country-card"):
        for anchor in card.descendants("a"):
            href = anchor.attrs.get("href", "")
            if href.startswith("/countries/") and href.count("/") == 2:
                slugs.append(href.rsplit("/", 1)[-1])
    return slugs


def select_values(body: str, name: str) -> list[str]:
    root = live.parse_document(body)
    select = next((item for item in root.descendants("select") if item.attrs.get("name") == name), None)
    return [option.attrs.get("value", "") for option in select.descendants("option")] if select else []


def fact_panel(root: live.Element, aspect_id: str, country_name: str) -> live.Element | None:
    for aspect in root.descendants("section", "structured-aspect"):
        heading_id = aspect.attrs.get("aria-labelledby", "")
        if not heading_id.endswith("-" + aspect_id):
            continue
        if not any(node.attrs.get("id") == heading_id for node in aspect.descendants()):
            continue
        for panel in aspect.descendants("article", "structured-country"):
            heading = next(panel.descendants("h5"), None)
            if heading and live.normalized(heading.text()) == live.normalized(country_name):
                return panel
    return None


def set_language(client: live.HttpClient, language: str, reporter: Reporter) -> str | None:
    response = client.request(f"/set-language?culture={language}&returnUrl=%2F")
    cookie = live.secure_culture_cookie(response, client.base_url)
    reporter.check(f"{language.upper()} language setup", "same-origin redirect and Secure cookie", cookie is not None,
                   "cookie set" if cookie else f"HTTP {response.status}; cookie/redirect missing")
    return cookie


def run_checks(base_url: str, username: str, password: str,
               request: Callable[[str, str | None], live.Response] | None = None,
               sleep: Callable[[float], None] = time.sleep, readiness_attempts: int = 12) -> bool:
    base_url = live.validate_target(base_url)
    client = None if request else live.HttpClient(base_url, username, password)
    send = request or (lambda path, cookie=None: client.request(path, cookie))  # type: ignore[union-attr]
    reporter = Reporter()

    home_response = None
    for attempt in range(readiness_attempts):
        candidate = send("/", None)
        if candidate.status not in (0, 502, 503, 504):
            home_response = candidate
            break
        if attempt + 1 < readiness_attempts:
            sleep(5)
    reporter.check("Authenticated Home", "HTTP 200", home_response is not None and home_response.status == 200,
                   f"HTTP {home_response.status}" if home_response else "no response")
    if home_response is None or home_response.status != 200:
        print(f"Eleven-country live scope verification: {reporter.count} checks; FAIL")
        return False

    slugs = country_card_slugs(home_response.body)
    reporter.check("Home country cards", "exactly eleven accepted countries", Counter(slugs) == Counter(COUNTRIES),
                   f"{len(slugs)} country card(s); Austria {'present' if 'austria' in slugs else 'absent'}")

    en_cookie = set_language(client, "en", reporter) if client else None
    if client is None:
        language_response = send("/set-language?culture=en&returnUrl=%2F", None)
        en_cookie = live.secure_culture_cookie(language_response, base_url)
        reporter.check("EN language setup", "same-origin redirect and Secure cookie", en_cookie is not None,
                       "cookie set" if en_cookie else f"HTTP {language_response.status}; cookie/redirect missing")
    if not en_cookie:
        print(f"Eleven-country live scope verification: {reporter.count} checks; FAIL")
        return False

    for slug in COUNTRIES:
        response = send(f"/countries/{slug}", en_cookie)
        root = live.parse_document(response.body)
        has_heading = any(heading.text().strip() for heading in root.descendants("h1"))
        page_ok = response.status == 200 and has_heading and bool(nodes(root, "main", "country-page"))
        reporter.check(f"Country route {slug}", "HTTP 200 with country page", page_ok,
                       f"HTTP {response.status}; {'country page present' if page_ok else 'country page missing'}")
        article_total = 0
        for category in CATEGORIES:
            page = send(f"/countries/{slug}/categories/{category}", en_cookie)
            page_root = live.parse_document(page.body)
            category_ok = page.status == 200 and bool(nodes(page_root, "main", "category-page"))
            count = len(nodes(page_root, "article", "information-entry"))
            expected_counts = {"bulgaria": BULGARIA_CATEGORY_COUNTS, "austria": AUSTRIA_CATEGORY_COUNTS}
            if slug in expected_counts:
                category_ok &= count == expected_counts[slug][category]
            article_total += count
            expected_category = (f"HTTP 200 with {expected_counts[slug][category]} accepted article(s)"
                                 if slug in expected_counts else "HTTP 200 with category page")
            reporter.check(f"Category route {slug}/{category}", expected_category, category_ok,
                           f"HTTP {page.status}; {count} accepted article(s)")
        expected_articles = SEARCH_RESULT_COUNTS.get(slug, 8)
        reporter.check(f"Category accepted articles {slug} (EN)", str(expected_articles),
                       article_total == expected_articles, f"{article_total} article(s)")

    bulgaria_country = send("/countries/bulgaria", en_cookie)
    bulgaria_root = live.parse_document(bulgaria_country.body)
    bulgaria_headings = [live.normalized(h.text()) for h in bulgaria_root.descendants("h1")]
    reporter.check("Bulgaria country route (EN)", f"HTTP 200 and {BULGARIA_NAMES['en']}",
                   bulgaria_country.status == 200 and bool(nodes(bulgaria_root, "main", "country-page"))
                   and live.normalized(BULGARIA_NAMES["en"]) in bulgaria_headings,
                   f"HTTP {bulgaria_country.status}; localized heading {'present' if live.normalized(BULGARIA_NAMES['en']) in bulgaria_headings else 'missing'}")

    search_form = send("/search", en_cookie)
    search_values = [value for value in select_values(search_form.body, "country") if value]
    reporter.check("Search country filter", "exactly eleven accepted countries",
                   search_form.status == 200 and Counter(search_values) == Counter(COUNTRIES),
                   f"HTTP {search_form.status}; {len(search_values)} country option(s)")
    compare_form = send("/compare", en_cookie)
    for name in ("country1", "country2"):
        values = [value for value in select_values(compare_form.body, name) if value]
        reporter.check(f"Compare selector {name}", "exactly eleven accepted countries",
                       compare_form.status == 200 and Counter(values) == Counter(COUNTRIES),
                       f"HTTP {compare_form.status}; {len(values)} country option(s)")

    for language in LANGUAGES:
        cookie = en_cookie if language == "en" else (set_language(client, language, reporter) if client else None)
        if client is None and language != "en":
            response = send(f"/set-language?culture={language}&returnUrl=%2F", None)
            cookie = live.secure_culture_cookie(response, base_url)
            reporter.check(f"{language.upper()} language setup", "same-origin redirect and Secure cookie", cookie is not None,
                           "cookie set" if cookie else f"HTTP {response.status}; cookie/redirect missing")
        if not cookie:
            continue

        # Romania: localized country route, all six category pages, and exactly eight articles.
        country = send("/countries/romania", cookie)
        country_root = live.parse_document(country.body)
        headings = [live.normalized(h.text()) for h in country_root.descendants("h1")]
        country_ok = country.status == 200 and bool(nodes(country_root, "main", "country-page")) \
            and live.normalized(COUNTRY_NAMES[language]) in headings
        reporter.check(f"Romania country route ({language.upper()})", f"HTTP 200 and {COUNTRY_NAMES[language]}",
                       country_ok, f"HTTP {country.status}; localized heading {'present' if country_ok else 'missing'}")
        romania_article_total = 0
        for category in CATEGORIES:
            page = send(f"/countries/romania/categories/{category}", cookie)
            page_root = live.parse_document(page.body)
            page_articles = nodes(page_root, "article", "information-entry")
            romania_article_total += len(page_articles)
            category_ok = page.status == 200 and bool(nodes(page_root, "main", "category-page"))
            reporter.check(f"Romania category {category} ({language.upper()})", "HTTP 200 with category page",
                           category_ok, f"HTTP {page.status}; {len(page_articles)} accepted article(s)")
        reporter.check(f"Romania accepted articles in Category ({language.upper()})", "exactly eight",
                       romania_article_total == 8, f"{romania_article_total} article(s)")

        # Bulgaria: localized country route and all six category counts.
        country = send("/countries/bulgaria", cookie)
        country_root = live.parse_document(country.body)
        headings = [live.normalized(h.text()) for h in country_root.descendants("h1")]
        country_ok = country.status == 200 and bool(nodes(country_root, "main", "country-page")) \
            and live.normalized(BULGARIA_NAMES[language]) in headings
        reporter.check(f"Bulgaria country route ({language.upper()})",
                       f"HTTP 200 and {BULGARIA_NAMES[language]}", country_ok,
                       f"HTTP {country.status}; localized heading {'present' if country_ok else 'missing'}")
        bulgaria_article_total = 0
        for category in CATEGORIES:
            page = send(f"/countries/bulgaria/categories/{category}", cookie)
            page_root = live.parse_document(page.body)
            page_articles = nodes(page_root, "article", "information-entry")
            bulgaria_article_total += len(page_articles)
            category_ok = page.status == 200 and bool(nodes(page_root, "main", "category-page")) \
                and len(page_articles) == BULGARIA_CATEGORY_COUNTS[category]
            reporter.check(f"Bulgaria category {category} ({language.upper()})",
                           f"HTTP 200 with {BULGARIA_CATEGORY_COUNTS[category]} accepted article(s)",
                           category_ok, f"HTTP {page.status}; {len(page_articles)} accepted article(s)")
        reporter.check(f"Bulgaria accepted articles in Category ({language.upper()})", "exactly eight",
                       bulgaria_article_total == 8, f"{bulgaria_article_total} article(s)")

        # Austria: localized country route and all six exact accepted-article counts.
        country = send("/countries/austria", cookie)
        country_root = live.parse_document(country.body)
        headings = [live.normalized(h.text()) for h in country_root.descendants("h1")]
        austria_name = live.normalized(AUSTRIA_NAMES[language])
        country_ok = country.status == 200 and bool(nodes(country_root, "main", "country-page")) \
            and austria_name in headings
        reporter.check(f"Austria country route ({language.upper()})",
                       f"HTTP 200 and {AUSTRIA_NAMES[language]}", country_ok,
                       f"HTTP {country.status}; localized heading {'present' if country_ok else 'missing'}")
        austria_article_total = 0
        for category in CATEGORIES:
            page = send(f"/countries/austria/categories/{category}", cookie)
            page_root = live.parse_document(page.body)
            page_articles = nodes(page_root, "article", "information-entry")
            austria_article_total += len(page_articles)
            expected_count = AUSTRIA_CATEGORY_COUNTS[category]
            category_ok = page.status == 200 and bool(nodes(page_root, "main", "category-page")) \
                and len(page_articles) == expected_count
            reporter.check(f"Austria category {category} ({language.upper()})",
                           f"HTTP 200 with {expected_count} accepted article(s)", category_ok,
                           f"HTTP {page.status}; {len(page_articles)} accepted article(s)")
        reporter.check(f"Austria accepted articles in Category ({language.upper()})", "exactly eight",
                       austria_article_total == 8, f"{austria_article_total} article(s)")

        for slug, expected in SEARCH_RESULT_COUNTS.items():
            response = send(f"/search?country={slug}", cookie)
            count = len(nodes(live.parse_document(response.body), "article", "result-card"))
            reporter.check(f"Search accepted articles {slug} ({language.upper()})", str(expected),
                           response.status == 200 and count == expected,
                           f"HTTP {response.status}; {count} result(s)")

        # Slovakia: localized country route, all six categories, exactly eight articles and search results.
        country = send("/countries/slovakia", cookie)
        country_root = live.parse_document(country.body)
        headings = [live.normalized(h.text()) for h in country_root.descendants("h1")]
        country_ok = country.status == 200 and bool(nodes(country_root, "main", "country-page")) \
            and live.normalized(SLOVAKIA_NAMES[language]) in headings
        reporter.check(f"Slovakia country route ({language.upper()})", f"HTTP 200 and {SLOVAKIA_NAMES[language]}",
                       country_ok, f"HTTP {country.status}; localized heading {'present' if country_ok else 'missing'}")
        slovakia_article_total = 0
        for category in CATEGORIES:
            page = send(f"/countries/slovakia/categories/{category}", cookie)
            page_root = live.parse_document(page.body)
            page_articles = nodes(page_root, "article", "information-entry")
            slovakia_article_total += len(page_articles)
            category_ok = page.status == 200 and bool(nodes(page_root, "main", "category-page"))
            reporter.check(f"Slovakia category {category} ({language.upper()})", "HTTP 200 with category page",
                           category_ok, f"HTTP {page.status}; {len(page_articles)} accepted article(s)")
        reporter.check(f"Slovakia accepted articles in Category ({language.upper()})", "exactly eight",
                       slovakia_article_total == 8, f"{slovakia_article_total} article(s)")
        slovakia_search = send("/search?country=slovakia", cookie)
        slovakia_search_count = len(nodes(live.parse_document(slovakia_search.body), "article", "result-card"))
        reporter.check(f"Search accepted articles slovakia ({language.upper()})", "8",
                       slovakia_search.status == 200 and slovakia_search_count == 8,
                       f"HTTP {slovakia_search.status}; {slovakia_search_count} result(s)")

        # All 24 Slovakia facts must render as available sourced answers in the selected language.
        rendered_aspects = 0
        slovakia_compare_ok = True
        slovakia_source_links = 0
        unavailable_text = live.normalized(live.UNAVAILABLE_TEXT[language])
        for category, aspect_ids in SLOVAKIA_ASPECTS_BY_CATEGORY.items():
            response = send(f"/compare?country1=slovakia&country2=denmark&category={category}", cookie)
            root = live.parse_document(response.body)
            valid_compare = response.status == 200 and not nodes(root, "p", "selection-message")
            for aspect_id in aspect_ids:
                panel = fact_panel(root, aspect_id, SLOVAKIA_NAMES[language])
                panel_text = live.normalized(panel.text()) if panel else ""
                groups = list(panel.descendants("details", "fact-sources")) if panel else []
                links = [a.attrs.get("href", "") for group in groups for a in group.descendants("a")] \
                    if panel else []
                has_answer = panel is not None and bool(panel_text) and unavailable_text not in panel_text
                has_sources = bool(groups) and bool(links) and all(url.startswith("https://") for url in links)
                slovakia_compare_ok &= valid_compare and has_answer and has_sources
                slovakia_source_links += len(links)
                rendered_aspects += int(panel is not None)
        reporter.check(f"Slovakia Compare facts ({language.upper()})", "24 available answers with HTTPS source disclosures",
                       slovakia_compare_ok and rendered_aspects == 24,
                       f"{rendered_aspects}/24 aspect panels; {slovakia_source_links} HTTPS source link(s)")

        # All 24 accepted Bulgaria facts must have a non-placeholder answer and HTTPS sources.
        rendered_aspects = 0
        bulgaria_compare_ok = True
        bulgaria_source_links = 0
        unavailable_text = live.normalized(live.UNAVAILABLE_TEXT[language])
        for category, aspect_ids in BULGARIA_ASPECTS_BY_CATEGORY.items():
            response = send(f"/compare?country1=bulgaria&country2=denmark&category={category}", cookie)
            root = live.parse_document(response.body)
            valid_compare = response.status == 200 and not nodes(root, "p", "selection-message")
            for aspect_id in aspect_ids:
                panel = fact_panel(root, aspect_id, BULGARIA_NAMES[language])
                panel_text = live.normalized(panel.text()) if panel else ""
                paragraphs = [live.normalized(node.text()) for node in panel.descendants("p")] if panel else []
                groups = list(panel.descendants("details", "fact-sources")) if panel else []
                links = [a.attrs.get("href", "") for group in groups for a in group.descendants("a")] \
                    if panel else []
                has_answer = bool(paragraphs) and any(text and unavailable_text not in text for text in paragraphs) \
                    and unavailable_text not in panel_text
                has_sources = bool(groups) and bool(links) and all(url.startswith("https://") for url in links)
                bulgaria_compare_ok &= valid_compare and has_answer and has_sources
                bulgaria_source_links += sum(url.startswith("https://") for url in links)
                rendered_aspects += int(panel is not None)
        reporter.check(f"Bulgaria Compare facts ({language.upper()})",
                       "24 available answers with HTTPS source disclosures",
                       bulgaria_compare_ok and rendered_aspects == 24,
                       f"{rendered_aspects}/24 aspect panels; {bulgaria_source_links} HTTPS source link(s)")

        # All 24 accepted Austria facts must render with a substantive answer and HTTPS sources.
        rendered_aspects = 0
        austria_compare_ok = True
        austria_source_links = 0
        unavailable_text = live.normalized(live.UNAVAILABLE_TEXT[language])
        for category, aspect_ids in AUSTRIA_ASPECTS_BY_CATEGORY.items():
            response = send(f"/compare?country1=austria&country2=denmark&category={category}", cookie)
            root = live.parse_document(response.body)
            valid_compare = response.status == 200 and not nodes(root, "p", "selection-message")
            for aspect_id in aspect_ids:
                panel = fact_panel(root, aspect_id, AUSTRIA_NAMES[language])
                panel_text = live.normalized(panel.text()) if panel else ""
                paragraphs = [live.normalized(node.text()) for node in panel.descendants("p")] if panel else []
                groups = list(panel.descendants("details", "fact-sources")) if panel else []
                links = [a.attrs.get("href", "") for group in groups for a in group.descendants("a")] \
                    if panel else []
                has_answer = bool(paragraphs) and any(text and unavailable_text not in text for text in paragraphs) \
                    and unavailable_text not in panel_text
                has_sources = bool(groups) and bool(links) and all(url.startswith("https://") for url in links)
                austria_compare_ok &= valid_compare and has_answer and has_sources
                austria_source_links += sum(url.startswith("https://") for url in links)
                rendered_aspects += int(panel is not None)
        reporter.check(f"Austria Compare facts ({language.upper()})",
                       "24 available answers with HTTPS source disclosures",
                       austria_compare_ok and rendered_aspects == 24,
                       f"{rendered_aspects}/24 aspect panels; {austria_source_links} HTTPS source link(s)")

        # Sweden positive comparison and its intentionally unresolved social-child-benefits fact.
        sweden_compare = send("/compare?country1=sweden&country2=denmark&category=temporary-protection", cookie)
        sweden_compare_root = live.parse_document(sweden_compare.body)
        sweden_panels = nodes(sweden_compare_root, "article", "structured-country")
        sweden_heading = any(live.normalized(h.text()) == live.normalized(SWEDEN_NAMES[language])
                             for panel in sweden_panels for h in panel.descendants("h5"))
        sweden_source_links = [a for panel in sweden_panels for group in panel.descendants("details", "fact-sources")
                               for a in group.descendants("a") if a.attrs.get("href", "").startswith("https://")]
        reporter.check(f"Compare accepts Sweden ({language.upper()})", "Sweden panel and approved comparison rendered",
                       sweden_compare.status == 200 and sweden_heading and len(sweden_panels) >= 2 and bool(sweden_source_links),
                       f"HTTP {sweden_compare.status}; {len(sweden_panels)} panel(s); Sweden heading {'present' if sweden_heading else 'missing'}; {len(sweden_source_links)} HTTPS source(s)")

        compare = send("/compare?country1=sweden&country2=denmark&category=social-assistance", cookie)
        compare_root = live.parse_document(compare.body)
        panel = fact_panel(compare_root, "social-child-benefits", SWEDEN_NAMES[language])
        panel_text = live.normalized(panel.text()) if panel else ""
        source_groups = list(panel.descendants("details", "fact-sources")) if panel else []
        unavailable = live.UNAVAILABLE_TEXT[language]
        suppressed = panel is not None and live.normalized(unavailable) in panel_text and not source_groups
        reporter.check(f"{language.upper()} Sweden social-child-benefits hold",
                       "unavailable evidence-gap placeholder with no source group",
                       compare.status == 200 and suppressed,
                       "unresolved placeholder; no sources" if suppressed else "held fact leaked or panel missing")

        # All 24 Romania facts must render as available approved answers with HTTPS source disclosure.
        rendered_aspects = 0
        romania_compare_ok = True
        romania_source_links = 0
        unavailable_text = live.normalized(live.UNAVAILABLE_TEXT[language])
        for category, aspect_ids in ROMANIA_ASPECTS_BY_CATEGORY.items():
            response = send(f"/compare?country1=romania&country2=denmark&category={category}", cookie)
            root = live.parse_document(response.body)
            valid_compare = response.status == 200 and not nodes(root, "p", "selection-message")
            for aspect_id in aspect_ids:
                panel = fact_panel(root, aspect_id, COUNTRY_NAMES[language])
                panel_text = live.normalized(panel.text()) if panel else ""
                groups = list(panel.descendants("details", "fact-sources")) if panel else []
                links = [a.attrs.get("href", "") for group in groups for a in group.descendants("a")] \
                    if panel else []
                has_approved_answer = panel is not None and bool(panel_text) and unavailable_text not in panel_text
                has_sources = bool(groups) and bool(links) and all(url.startswith("https://") for url in links)
                romania_compare_ok &= valid_compare and has_approved_answer and has_sources
                romania_source_links += len(links)
                rendered_aspects += int(panel is not None)
        reporter.check(f"Romania Compare facts ({language.upper()})", "24 available answers with source disclosures",
                       romania_compare_ok and rendered_aspects == 24,
                       f"{rendered_aspects}/24 aspect panels; {romania_source_links} HTTPS source link(s)")

    for slug in ("czechia", "spain", "netherlands", "romania", "slovakia", "bulgaria", "austria"):
        response = send(f"/compare?country1={slug}&country2=denmark&category=employment", en_cookie)
        root = live.parse_document(response.body)
        selectable = response.status == 200 and not nodes(root, "p", "selection-message") \
            and bool(nodes(root, "article", "structured-country"))
        reporter.check(f"Compare accepted country {slug}", "country selectable and comparison rendered", selectable,
                       f"HTTP {response.status}; {'comparison rendered' if selectable else 'selection unavailable'}")

    print(f"Eleven-country live scope verification: {reporter.count} checks; {'FAIL' if reporter.failed else 'PASS'}")
    return not reporter.failed


def main() -> int:
    try:
        base_url = live.validate_target(os.environ.get("RENDER_TARGET_URL", ""))
    except ValueError as exc:
        print(f"FAIL | Configuration | expected: HTTPS onrender.com URL | actual: {exc}")
        return 2
    username, password = os.environ.get("PRIVATE_ALPHA_USERNAME", ""), os.environ.get("PRIVATE_ALPHA_PASSWORD", "")
    if not username or not password:
        print("FAIL | Configuration | expected: credentials provided via environment | actual: required variables missing")
        return 2
    try:
        return 0 if run_checks(base_url, username, password) else 1
    except Exception as exc:
        print(f"FAIL | Verification run | expected: checks complete | actual: {type(exc).__name__}")
        return 1


if __name__ == "__main__":
    sys.exit(main())
