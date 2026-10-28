#!/usr/bin/env python3
"""Authenticated, read-only live check for the accepted seven-country scope."""
from __future__ import annotations

import os
import sys
import time
from collections import Counter
from typing import Callable
from urllib.parse import quote_plus

import netherlands_live_hold_test as live

COUNTRIES = ("denmark", "germany", "poland", "czechia", "spain", "netherlands", "sweden")
CATEGORIES = ("temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance")
SEARCH_RESULT_COUNTS = {"czechia": 8, "spain": 8, "netherlands": 7, "sweden": 8}
LANGUAGES = ("uk", "en", "ru")
SWEDEN_NAMES = {"en": "Sweden", "uk": "Швеція", "ru": "Швеция"}
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


def visible_not_found(response: live.Response, language: str, forbidden_class: str) -> bool:
    root = live.parse_document(response.body)
    has_not_found = any(NOT_FOUND[language].casefold() in heading.text().casefold()
                        for heading in root.descendants("h1"))
    page_available = bool(nodes(root, "main", forbidden_class))
    return response.status == 404 or (response.status == 200 and has_not_found and not page_available)


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
        print(f"Seven-country live scope verification: {reporter.count} checks; FAIL")
        return False

    slugs = country_card_slugs(home_response.body)
    reporter.check("Home country cards", "exactly seven accepted countries", Counter(slugs) == Counter(COUNTRIES),
                   f"{len(slugs)} country card(s); Sweden {'present' if 'sweden' in slugs else 'absent'}")

    en_cookie = set_language(client, "en", reporter) if client else None
    if client is None:
        language_response = send("/set-language?culture=en&returnUrl=%2F", None)
        en_cookie = live.secure_culture_cookie(language_response, base_url)
        reporter.check("EN language setup", "same-origin redirect and Secure cookie", en_cookie is not None,
                       "cookie set" if en_cookie else f"HTTP {language_response.status}; cookie/redirect missing")
    if not en_cookie:
        print(f"Seven-country live scope verification: {reporter.count} checks; FAIL")
        return False

    for slug in COUNTRIES:
        response = send(f"/countries/{slug}", en_cookie)
        root = live.parse_document(response.body)
        has_heading = any(heading.text().strip() for heading in root.descendants("h1"))
        reporter.check(f"Country route {slug}", "HTTP 200 with heading", response.status == 200 and has_heading,
                       f"HTTP {response.status}; {'heading present' if has_heading else 'heading missing'}")
        article_counts: Counter[str] = Counter()
        for category in CATEGORIES:
            page = send(f"/countries/{slug}/categories/{category}", en_cookie)
            page_root = live.parse_document(page.body)
            category_ok = page.status == 200 and any(h.text().strip() for h in page_root.descendants("h1"))
            reporter.check(f"Category route {slug}/{category}", "HTTP 200 with heading", category_ok,
                           f"HTTP {page.status}; {'heading present' if category_ok else 'page unavailable'}")
            article_counts[category] += len(nodes(page_root, "article", "information-entry"))
        expected_articles = SEARCH_RESULT_COUNTS.get(slug, 8)
        reporter.check(f"Category verified articles {slug} (EN)", str(expected_articles),
                       sum(article_counts.values()) == expected_articles,
                       f"{sum(article_counts.values())} article(s)")

    search_form = send("/search", en_cookie)
    search_values = [value for value in select_values(search_form.body, "country") if value]
    reporter.check("Search country filter", "exactly seven accepted countries", Counter(search_values) == Counter(COUNTRIES),
                   f"{len(search_values)} country option(s); Sweden {'present' if 'sweden' in search_values else 'absent'}")
    compare_form = send("/compare", en_cookie)
    for name in ("country1", "country2"):
        values = [value for value in select_values(compare_form.body, name) if value]
        reporter.check(f"Compare selector {name}", "exactly seven accepted countries", Counter(values) == Counter(COUNTRIES),
                       f"{len(values)} country option(s); Sweden {'present' if 'sweden' in values else 'absent'}")

    for language in LANGUAGES:
        cookie = en_cookie if language == "en" else (set_language(client, language, reporter) if client else None)
        if client is None and language != "en":
            response = send(f"/set-language?culture={language}&returnUrl=%2F", None)
            cookie = live.secure_culture_cookie(response, base_url)
            reporter.check(f"{language.upper()} language setup", "same-origin redirect and Secure cookie", cookie is not None,
                           "cookie set" if cookie else f"HTTP {response.status}; cookie/redirect missing")
        for slug, expected in SEARCH_RESULT_COUNTS.items():
            response = send(f"/search?country={slug}", cookie)
            count = len(nodes(live.parse_document(response.body), "article", "result-card"))
            reporter.check(f"Search accepted articles {slug} ({language.upper()})", str(expected),
                           response.status == 200 and count == expected,
                           f"HTTP {response.status}; {count} result(s)")
        if not cookie:
            continue

        sweden_page = send("/countries/sweden", cookie)
        sweden_root = live.parse_document(sweden_page.body)
        reporter.check(f"Sweden country route ({language.upper()})", "HTTP 200 with country page",
                       sweden_page.status == 200 and bool(nodes(sweden_root, "main", "country-page")),
                       f"HTTP {sweden_page.status}; {'country page present' if nodes(sweden_root, 'main', 'country-page') else 'country page missing'}")
        sweden_article_total = 0
        for category in CATEGORIES:
            page = send(f"/countries/sweden/categories/{category}", cookie)
            page_root = live.parse_document(page.body)
            category_ok = page.status == 200 and bool(nodes(page_root, "main", "category-page"))
            count = len(nodes(page_root, "article", "information-entry"))
            sweden_article_total += count
            reporter.check(f"Sweden category route {category} ({language.upper()})", "HTTP 200 with category page",
                           category_ok, f"HTTP {page.status}; {count} verified article(s)")
        reporter.check(f"Sweden verified category articles ({language.upper()})", "8 accepted articles across categories",
                       sweden_article_total == 8, f"{sweden_article_total} article(s)")

        sweden_compare = send("/compare?country1=sweden&country2=denmark&category=temporary-protection", cookie)
        sweden_compare_root = live.parse_document(sweden_compare.body)
        sweden_panels = nodes(sweden_compare_root, "article", "structured-country")
        sweden_heading = any("Sweden" in h.text() or "Швеція" in h.text() or "Швеция" in h.text()
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
    for slug in ("czechia", "spain", "netherlands"):
        response = send(f"/compare?country1={slug}&country2=denmark&category=employment", en_cookie)
        root = live.parse_document(response.body)
        selectable = response.status == 200 and not nodes(root, "p", "selection-message") and bool(nodes(root, "article", "structured-country"))
        reporter.check(f"Compare accepted country {slug}", "country selectable and comparison rendered", selectable,
                       f"HTTP {response.status}; {'comparison rendered' if selectable else 'selection unavailable'}")

    print(f"Seven-country live scope verification: {reporter.count} checks; {'FAIL' if reporter.failed else 'PASS'}")
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
