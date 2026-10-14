#!/usr/bin/env python3
"""Read-only authenticated post-deployment verification for the private alpha."""
from __future__ import annotations

import base64
import html
from html.parser import HTMLParser
import os
import re
import sys
import time
from dataclasses import dataclass
from typing import Callable
from urllib.error import HTTPError, URLError
from urllib.parse import urljoin, urlsplit
from urllib.request import HTTPRedirectHandler, Request, build_opener

TIMEOUT_SECONDS = 15
READY_ATTEMPTS = 24
READY_DELAY_SECONDS = 5
COUNTRIES = ("denmark", "germany", "poland")
CATEGORIES = ("temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance")
ARTICLE_TITLES = {
    "uk": "Де шукати роботу та інформацію про пошук роботи в Данії",
    "en": "Find jobs and job-search guidance in Denmark",
    "ru": "Где искать работу и информацию о поиске работы в Дании",
}
NOT_FOUND_HEADINGS = {"uk": "Сторінку не знайдено", "en": "Page not found", "ru": "Страница не найдена"}


@dataclass
class Response:
    status: int
    headers: dict[str, str]
    body: str


class PageParser(HTMLParser):
    def __init__(self) -> None:
        super().__init__(convert_charrefs=True)
        self.lang = ""
        self.headings: list[tuple[str, str]] = []
        self.links: list[tuple[str, str]] = []
        self.stylesheets: list[str] = []
        self.form_names: list[str] = []
        self._heading_tag: str | None = None
        self._heading_text: list[str] = []
        self._anchor_href: str | None = None
        self._anchor_text: list[str] = []
        self._in_form = False

    def handle_starttag(self, tag: str, attrs: list[tuple[str, str | None]]) -> None:
        values = dict(attrs)
        if tag == "html":
            self.lang = values.get("lang") or ""
        if tag == "link" and "stylesheet" in (values.get("rel") or "").lower().split():
            href = values.get("href")
            if href:
                self.stylesheets.append(href)
        if tag in ("h1", "h2", "h3"):
            self._heading_tag, self._heading_text = tag, []
        if tag == "a":
            self._anchor_href, self._anchor_text = values.get("href") or "", []
        if tag == "form":
            self._in_form = True
        if self._in_form and tag in ("input", "select", "button") and values.get("name"):
            self.form_names.append(values["name"])

    def handle_endtag(self, tag: str) -> None:
        if self._heading_tag == tag:
            self.headings.append((tag, normalize("".join(self._heading_text))))
            self._heading_tag = None
        if tag == "a" and self._anchor_href is not None:
            self.links.append((self._anchor_href, normalize("".join(self._anchor_text))))
            self._anchor_href = None
        if tag == "form":
            self._in_form = False

    def handle_data(self, data: str) -> None:
        if self._heading_tag:
            self._heading_text.append(data)
        if self._anchor_href is not None:
            self._anchor_text.append(data)


def normalize(value: str) -> str:
    return " ".join(html.unescape(value).split())


class NoRedirectHandler(HTTPRedirectHandler):
    """Do not follow redirects, so Basic credentials cannot reach another origin."""
    def redirect_request(self, req, fp, code, msg, headers, newurl):
        return None


def validate_target(target: str) -> str:
    try:
        parts = urlsplit(target)
        host, port = parts.hostname or "", parts.port
    except ValueError as exc:
        raise ValueError("target URL is malformed") from exc
    if (parts.scheme.lower() != "https" or not host or host.lower() == "onrender.com"
            or not host.lower().endswith(".onrender.com") or parts.username is not None
            or parts.password is not None or parts.query or parts.fragment
            or parts.path not in ("", "/") or port not in (None, 443)):
        raise ValueError("target must be an HTTPS onrender.com service origin without path, query, or credentials")
    return f"https://{host.lower()}"


class HttpClient:
    def __init__(self, base_url: str, username: str, password: str) -> None:
        self.base_url, self.username, self.password = base_url, username, password
        self.origin = urlsplit(base_url).netloc.lower()
        self.opener = build_opener(NoRedirectHandler())

    def request(self, path: str, authenticated: bool = True, cookie: str | None = None,
                credentials: tuple[str, str] | None = None) -> Response:
        url = urljoin(self.base_url + "/", path.lstrip("/"))
        parsed = urlsplit(url)
        if parsed.scheme != "https" or parsed.netloc.lower() != self.origin:
            raise ValueError("refusing request outside configured HTTPS origin")
        headers = {"User-Agent": "RefugeeNavigatorEurope-DeploymentQA/1.0", "Accept": "text/html,*/*;q=0.8"}
        if authenticated:
            user, password = credentials or (self.username, self.password)
            token = base64.b64encode(f"{user}:{password}".encode("utf-8")).decode("ascii")
            headers["Authorization"] = f"Basic {token}"
        if cookie:
            headers["Cookie"] = cookie
        try:
            with self.opener.open(Request(url, headers=headers), timeout=TIMEOUT_SECONDS) as response:
                return Response(response.status, dict(response.headers.items()), response.read().decode("utf-8", "replace"))
        except HTTPError as response:
            return Response(response.code, dict(response.headers.items()), response.read().decode("utf-8", "replace"))
        except (URLError, TimeoutError, OSError) as exc:
            return Response(0, {}, f"transport error: {type(exc).__name__}")


class Reporter:
    def __init__(self) -> None:
        self.failed, self.count = False, 0

    def check(self, name: str, expected: str, ok: bool, actual: str) -> None:
        self.count += 1
        self.failed |= not ok
        print(f"{'PASS' if ok else 'FAIL'} | {name} | expected: {expected} | actual: {actual[:180]}")

    def skip(self, name: str, expected: str, reason: str) -> None:
        self.count += 1
        print(f"SKIP | {name} | expected: {expected} | {reason}")


def response_header(response: Response, name: str) -> str:
    return next((v for k, v in response.headers.items() if k.lower() == name.lower()), "")


def has_robots(response: Response) -> bool:
    value = response_header(response, "X-Robots-Tag").lower()
    return "noindex" in value and "nofollow" in value


def parse_page(response: Response) -> PageParser:
    page = PageParser()
    page.feed(response.body)
    return page


def record_response(reporter: Reporter, name: str, response: Response, expected: int,
                    expected_lang: str | None = None) -> PageParser:
    reporter.check(name, f"HTTP {expected}", response.status == expected, f"HTTP {response.status}")
    reporter.check(f"{name}: X-Robots-Tag", "contains noindex, nofollow", has_robots(response),
                   "present" if has_robots(response) else "missing or incorrect")
    page = parse_page(response)
    if expected_lang:
        reporter.check(f"{name}: document language", expected_lang, page.lang.lower() == expected_lang,
                       page.lang or "missing")
    return page


def same_origin_location(base_url: str, response: Response) -> bool:
    location = response_header(response, "Location")
    if not location:
        return False
    target, origin = urlsplit(urljoin(base_url + "/", location)), urlsplit(base_url)
    return target.scheme == "https" and target.netloc.lower() == origin.netloc.lower()


def set_culture(client, reporter: Reporter, culture: str) -> str | None:
    response = client.request(f"/set-language?culture={culture}&returnUrl=%2F")
    safe_location = same_origin_location(client.base_url, response)
    reporter.check(f"Language switch to {culture}", "HTTP 302 with same-origin HTTPS Location",
                   response.status == 302 and safe_location,
                   f"HTTP {response.status}; {'same-origin Location' if safe_location else 'missing or unsafe Location'}")
    reporter.check(f"Language switch to {culture}: X-Robots-Tag", "contains noindex, nofollow", has_robots(response),
                   "present" if has_robots(response) else "missing or incorrect")
    set_cookie = response_header(response, "Set-Cookie")
    secure = bool(re.search(r";\s*secure(?:;|$)", set_cookie, re.IGNORECASE))
    reporter.check(f"Language switch to {culture}: secure culture cookie", "Set-Cookie includes Secure", secure,
                   "Secure attribute present" if secure else "missing Secure attribute")
    return set_cookie.split(";", 1)[0] if set_cookie else None


def run_checks(base_url: str, username: str, password: str,
               request: Callable[[str, bool, str | None, tuple[str, str] | None], Response] | None = None,
               sleep: Callable[[float], None] = time.sleep, readiness_attempts: int = READY_ATTEMPTS) -> bool:
    reporter = Reporter()
    client = HttpClient(base_url, username, password) if request is None else None
    send = request or (lambda path, auth=True, cookie=None, credentials=None:
                       client.request(path, auth, cookie, credentials))

    ready = None
    for attempt in range(readiness_attempts):
        candidate = send("/", False, None, None)
        if candidate.status not in (0, 502, 503, 504):
            ready = candidate
            break
        if attempt + 1 < readiness_attempts:
            sleep(READY_DELAY_SECONDS)
    if ready is None:
        reporter.check("HTTPS endpoint readiness", "private app responds within bounded retry window", False,
                       f"HTTP {candidate.status} after {readiness_attempts} attempts")
        print(f"Deployment verification: {reporter.count} checks; FAIL")
        return False
    reporter.check("HTTPS endpoint readiness", "private app responds over HTTPS", ready.status in (200, 401),
                   f"HTTP {ready.status}")
    reporter.check("Anonymous homepage", "HTTP 401", ready.status == 401, f"HTTP {ready.status}")
    reporter.check("Anonymous homepage: no content leak", "empty protected response body", not ready.body.strip(),
                   "response body empty" if not ready.body.strip() else "protected response had a body")
    reporter.check("Anonymous homepage: X-Robots-Tag", "contains noindex, nofollow", has_robots(ready),
                   "present" if has_robots(ready) else "missing or incorrect")

    incorrect = send("/", True, None, (username, password + "-incorrect"))
    reporter.check("Incorrect credentials", "HTTP 401", incorrect.status == 401, f"HTTP {incorrect.status}")
    reporter.check("Incorrect credentials: no content leak", "empty protected response body", not incorrect.body.strip(),
                   "response body empty" if not incorrect.body.strip() else "protected response had a body")
    reporter.check("Incorrect credentials: X-Robots-Tag", "contains noindex, nofollow", has_robots(incorrect),
                   "present" if has_robots(incorrect) else "missing or incorrect")

    anon_language = send("/set-language?culture=en&returnUrl=%2F", False, None, None)
    reporter.check("Anonymous language switching", "HTTP 401", anon_language.status == 401,
                   f"HTTP {anon_language.status}")
    reporter.check("Anonymous language switching: no content leak", "empty protected response body",
                   not anon_language.body.strip(),
                   "response body empty" if not anon_language.body.strip() else "protected response had a body")
    reporter.check("Anonymous language switching: X-Robots-Tag", "contains noindex, nofollow", has_robots(anon_language),
                   "present" if has_robots(anon_language) else "missing or incorrect")

    home = send("/", True, None, None)
    home_page = record_response(reporter, "Authenticated homepage", home, 200, "uk")
    home_heading = any(tag == "h1" and text for tag, text in home_page.headings)
    reporter.check("Authenticated homepage: rendered heading", "non-empty h1", home_heading,
                   "h1 present" if home_heading else "h1 missing")
    styles = home_page.stylesheets or [href for href, _ in home_page.links if href.split("?", 1)[0].lower().endswith(".css")]
    reporter.check("CSS asset discovery", "at least one stylesheet reference", bool(styles),
                   f"discovered {len(styles)} stylesheet link(s)")
    asset_path = None
    if styles:
        asset = urlsplit(urljoin(base_url + "/", styles[0]))
        origin = urlsplit(base_url)
        same_origin = asset.scheme == "https" and asset.netloc.lower() == origin.netloc.lower()
        reporter.check("CSS asset origin", "same HTTPS service origin", same_origin,
                       "same origin" if same_origin else "external or insecure origin")
        if same_origin:
            asset_path = asset.path + (("?" + asset.query) if asset.query else "")
    if asset_path:
        anon_css = send(asset_path, False, None, None)
        record_response(reporter, "Anonymous CSS asset", anon_css, 401)
        reporter.check("Anonymous CSS asset: no content leak", "empty protected response body", not anon_css.body.strip(),
                       "response body empty" if not anon_css.body.strip() else "protected response had a body")
        css = send(asset_path, True, None, None)
        css_ok = css.status == 200 and "text/css" in response_header(css, "Content-Type").lower()
        reporter.check("Authenticated CSS asset", "HTTP 200 with CSS content type", css_ok,
                       f"HTTP {css.status}; content type {response_header(css, 'Content-Type') or 'missing'}")
        reporter.check("Authenticated CSS asset: X-Robots-Tag", "contains noindex, nofollow", has_robots(css),
                       "present" if has_robots(css) else "missing or incorrect")
    else:
        reporter.skip("CSS asset access", "anonymous 401 and authenticated 200", "no safe same-origin CSS reference")

    for country in COUNTRIES:
        country_page = record_response(reporter, f"Country page {country}", send(f"/countries/{country}", True), 200)
        has_heading = any(tag == "h1" and text for tag, text in country_page.headings)
        reporter.check(f"Country page {country}: rendered heading", "non-empty h1", has_heading,
                       "h1 present" if has_heading else "h1 missing")
        for category in CATEGORIES:
            page = record_response(reporter, f"Category page {country}/{category}",
                                   send(f"/countries/{country}/categories/{category}", True), 200)
            has_heading = any(tag == "h1" and text for tag, text in page.headings)
            reporter.check(f"Category page {country}/{category}: rendered heading", "non-empty h1", has_heading,
                           "h1 present" if has_heading else "h1 missing")

    for culture in ("uk", "en", "ru"):
        cookie = set_culture(client or type("Client", (), {"base_url": base_url, "request": lambda self, path: send(path, True, None, None)})(), reporter, culture)
        article = send("/countries/denmark/categories/employment", True, cookie, None)
        article_page = record_response(reporter, f"Denmark employment article ({culture})", article, 200, culture)
        article_title_ok = any(tag == "h2" and text == ARTICLE_TITLES[culture] for tag, text in article_page.headings)
        reporter.check(f"Denmark employment article ({culture}): translated title", ARTICLE_TITLES[culture], article_title_ok,
                       "expected article heading rendered" if article_title_ok else "expected article heading missing")
        visible_text = normalize(article.body)
        brands_ok = "Workindenmark" in visible_text and "Jobnet" in visible_text
        reporter.check(f"Denmark employment article ({culture}): content", "Workindenmark and Jobnet visible", brands_ok,
                       "both service names rendered" if brands_ok else "one or more expected names missing")
        source_links = [href for href, _ in article_page.links if urlsplit(urljoin(base_url + "/", href)).netloc.lower() != urlsplit(base_url).netloc.lower()]
        reporter.check(f"Denmark employment article ({culture}): official sources", "five external source links", len(source_links) == 5,
                       f"found {len(source_links)} external source link(s)")

        search = send("/search?q=Workindenmark&country=denmark&category=employment&status=Verified", True, cookie, None)
        search_page = record_response(reporter, f"Search and filters ({culture})", search, 200, culture)
        title_visible = ARTICLE_TITLES[culture] in normalize(search.body)
        reporter.check(f"Search and filters ({culture}): matching article", "published article appears", title_visible,
                       "matching article rendered" if title_visible else "matching article missing")
        filters = {"q", "country", "category", "status"}
        filters_present = filters.issubset(set(search_page.form_names))
        reporter.check(f"Search and filters ({culture}): filter controls", "query, country, category, and status controls",
                       filters_present, "all filter controls present" if filters_present else "one or more controls missing")

        comparison = send("/compare?country1=denmark&country2=germany&category=employment", True, cookie, None)
        compare_page = record_response(reporter, f"Country comparison ({culture})", comparison, 200, culture)
        comparison_ok = sum(1 for tag, _ in compare_page.headings if tag == "h2") >= 2
        reporter.check(f"Country comparison ({culture}): comparison rendered", "two country panels", comparison_ok,
                       "both panels rendered" if comparison_ok else "expected panels missing")

        direct_not_found = send("/not-found", True, cookie, None)
        not_found_page = record_response(reporter, f"Not-found route ({culture})", direct_not_found, 200, culture)
        direct_heading = any(tag == "h1" and text == NOT_FOUND_HEADINGS[culture] for tag, text in not_found_page.headings)
        reporter.check(f"Not-found route ({culture}): localized heading", NOT_FOUND_HEADINGS[culture], direct_heading,
                       "expected heading rendered" if direct_heading else "expected heading missing")

        unknown = send("/deployment-qa-unknown-route", True, cookie, None)
        unknown_page = record_response(reporter, f"Localized unknown route ({culture})", unknown, 404, culture)
        unknown_heading = any(tag == "h1" and text == NOT_FOUND_HEADINGS[culture] for tag, text in unknown_page.headings)
        reporter.check(f"Localized unknown route ({culture}): 404 content", NOT_FOUND_HEADINGS[culture], unknown_heading,
                       "localized heading in 404 response" if unknown_heading else "expected heading missing")

    print(f"Deployment verification: {reporter.count} checks; {'FAIL' if reporter.failed else 'PASS'}")
    return not reporter.failed


def main() -> int:
    try:
        base_url = validate_target(os.environ.get("RENDER_TARGET_URL", ""))
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
