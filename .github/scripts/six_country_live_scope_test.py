#!/usr/bin/env python3
"""Authenticated, read-only live check for the accepted six-country scope."""
from __future__ import annotations

import os
import sys
import time
from collections import Counter
from typing import Callable
from urllib.parse import quote_plus

import netherlands_live_hold_test as live

COUNTRIES = ("denmark", "germany", "poland", "czechia", "spain", "netherlands")
CATEGORIES = ("temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance")
SEARCH_RESULT_COUNTS = {"czechia": 8, "spain": 8, "netherlands": 7}
LANGUAGES = ("uk", "en", "ru")
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
        print(f"Six-country live scope verification: {reporter.count} checks; FAIL")
        return False

    slugs = country_card_slugs(home_response.body)
    reporter.check("Home country cards", "exactly the accepted six countries", Counter(slugs) == Counter(COUNTRIES),
                   f"{len(slugs)} country card(s); Sweden {'present' if 'sweden' in slugs else 'absent'}")

    en_cookie = set_language(client, "en", reporter) if client else None
    if client is None:
        language_response = send("/set-language?culture=en&returnUrl=%2F", None)
        en_cookie = live.secure_culture_cookie(language_response, base_url)
        reporter.check("EN language setup", "same-origin redirect and Secure cookie", en_cookie is not None,
                       "cookie set" if en_cookie else f"HTTP {language_response.status}; cookie/redirect missing")
    if not en_cookie:
        print(f"Six-country live scope verification: {reporter.count} checks; FAIL")
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

    sweden_page = send("/countries/sweden", en_cookie)
    reporter.check("Sweden country route", "NotFound and no country page",
                   visible_not_found(sweden_page, "en", "country-page"),
                   "NotFound" if visible_not_found(sweden_page, "en", "country-page") else f"HTTP {sweden_page.status}; page content present")
    for category in CATEGORIES:
        page = send(f"/countries/sweden/categories/{category}", en_cookie)
        hidden = visible_not_found(page, "en", "category-page")
        reporter.check(f"Sweden category route {category}", "NotFound and no category page", hidden,
                       "NotFound" if hidden else f"HTTP {page.status}; category page present")

    search_form = send("/search", en_cookie)
    search_values = [value for value in select_values(search_form.body, "country") if value]
    reporter.check("Search country filter", "exactly six accepted countries", Counter(search_values) == Counter(COUNTRIES),
                   f"{len(search_values)} country option(s); Sweden {'present' if 'sweden' in search_values else 'absent'}")
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

    compare_form = send("/compare", en_cookie)
    for name in ("country1", "country2"):
        values = [value for value in select_values(compare_form.body, name) if value]
        reporter.check(f"Compare selector {name}", "exactly six accepted countries", Counter(values) == Counter(COUNTRIES),
                       f"{len(values)} country option(s); Sweden {'present' if 'sweden' in values else 'absent'}")
    for parameter in ("country1=sweden&country2=denmark", "country1=denmark&country2=sweden"):
        response = send(f"/compare?{parameter}&category=employment", en_cookie)
        root = live.parse_document(response.body)
        invalid = bool(nodes(root, "p", "selection-message")) and not nodes(root, "article", "structured-country")
        reporter.check(f"Compare rejects {parameter.split('&')[0] if parameter.startswith('country1') else parameter.split('&')[1]}",
                       "invalid selection; no country fact panels", response.status == 200 and invalid,
                       f"HTTP {response.status}; {'invalid selection' if invalid else 'country content rendered'}")
    for slug in ("czechia", "spain", "netherlands"):
        response = send(f"/compare?country1={slug}&country2=denmark&category=employment", en_cookie)
        root = live.parse_document(response.body)
        selectable = response.status == 200 and not nodes(root, "p", "selection-message") and bool(nodes(root, "article", "structured-country"))
        reporter.check(f"Compare accepted country {slug}", "country selectable and comparison rendered", selectable,
                       f"HTTP {response.status}; {'comparison rendered' if selectable else 'selection unavailable'}")

    print(f"Six-country live scope verification: {reporter.count} checks; {'FAIL' if reporter.failed else 'PASS'}")
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
