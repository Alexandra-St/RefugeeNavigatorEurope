#!/usr/bin/env python3
"""Bounded authenticated live checks for the Netherlands private-beta hold set."""
from __future__ import annotations

import base64
from html.parser import HTMLParser
import os
import sys
import time
from dataclasses import dataclass, field
from typing import Callable, Iterator
from urllib.error import HTTPError, URLError
from urllib.parse import parse_qs, quote_plus, urljoin, urlsplit
from urllib.request import HTTPRedirectHandler, Request, build_opener

TIMEOUT_SECONDS = 15
READY_ATTEMPTS = 12
READY_DELAY_SECONDS = 5
BASE_CATEGORIES = (
    "temporary-protection", "employment", "healthcare", "housing", "education", "social-assistance",
)
LANGUAGES = ("en", "uk", "ru")
HELD_ENTRY_ID = "244b1055-4d8c-55b2-820e-60ff018c3b1f"
ACCEPTED_ENTRY_ID = "c5f5f833-0ab7-5e1a-9325-87a92b21d2ad"
HELD_TITLES = {
    "en": "Applying for Temporary Protection in the Netherlands",
    "uk": "Подання заяви на тимчасовий захист у Нідерландах",
    "ru": "Подача заявления на временную защиту в Нидерландах",
}
HELD_SUMMARIES = {
    "en": "Dutch temporary protection has different applicant routes and document rules.",
    "uk": "У Нідерландах діють різні маршрути для заявників і правила щодо документів.",
    "ru": "В Нидерландах предусмотрены разные маршруты для заявителей и правила оформления документов.",
}
HELD_BODY_MARKERS = {
    "en": "IND guidance describes separate routes for Ukrainian nationals",
    "uk": "IND описує окремі маршрути для громадян України",
    "ru": "IND описывает отдельные маршруты для граждан Украины",
}
ACCEPTED_TITLES = {
    "en": "Residence Options Beyond Temporary Protection in the Netherlands",
    "uk": "Варіанти проживання за межами тимчасового захисту в Нідерландах",
    "ru": "Варианты проживания за пределами временной защиты в Нидерландах",
}
HELD_FACT_MARKERS = {
    "protection-scheme": {
        "en": "The Netherlands applies the EU Temporary Protection Directive through the IND.",
        "uk": "У Нідерландах тимчасовий захист ЄС оформлюється через IND.",
        "ru": "В Нидерландах временная защита ЕС оформляется через IND.",
    },
    "protection-application": {
        "en": "In a clear case, the usual first step is municipal registration in the BRP.",
        "uk": "У зрозумілому випадку зазвичай спершу звертаються до муніципалітету для реєстрації в BRP.",
        "ru": "В понятном случае обычный первый шаг — обратиться в муниципалитет для регистрации в BRP.",
    },
    "protection-dates": {
        "en": "The EU temporary-protection scheme runs through 4 March 2028;",
        "uk": "Схема тимчасового захисту ЄС діє до 4 березня 2028 року;",
        "ru": "Схема временной защиты ЕС действует до 4 марта 2028 года;",
    },
}
UNAVAILABLE_TEXT = {
    "en": "Not established in the reviewed sources.",
    "uk": "Не встановлено за перевіреними джерелами.",
    "ru": "Не установлено по проверенным источникам.",
}
ACCEPTED_FACT_MARKER = {
    "en": "Accessible IND, Government.nl and ministerial guidance describes prospective work",
    "uk": "Доступні рекомендації IND, Government.nl та міністерства описують перспективну роботу",
    "ru": "Доступные рекомендации IND, Government.nl и министерства описывают перспективную работу",
}


@dataclass
class Response:
    status: int
    headers: dict[str, str]
    body: str


@dataclass
class Element:
    tag: str
    attrs: dict[str, str]
    children: list[Element | str] = field(default_factory=list)

    def text(self) -> str:
        return " ".join(" ".join(
            child if isinstance(child, str) else child.text()
            for child in self.children
        ).split())

    def descendants(self, tag: str | None = None, class_name: str | None = None) -> Iterator[Element]:
        for child in self.children:
            if not isinstance(child, Element):
                continue
            if (tag is None or child.tag == tag) and (
                class_name is None or class_name in child.attrs.get("class", "").split()
            ):
                yield child
            yield from child.descendants(tag, class_name)


class DocumentParser(HTMLParser):
    VOID_TAGS = {"area", "base", "br", "col", "embed", "hr", "img", "input", "link", "meta", "param", "source", "track", "wbr"}

    def __init__(self) -> None:
        super().__init__(convert_charrefs=True)
        self.root = Element("#document", {})
        self.stack = [self.root]

    def handle_starttag(self, tag: str, attrs: list[tuple[str, str | None]]) -> None:
        node = Element(tag, {key: value or "" for key, value in attrs})
        self.stack[-1].children.append(node)
        if tag not in self.VOID_TAGS:
            self.stack.append(node)

    def handle_startendtag(self, tag: str, attrs: list[tuple[str, str | None]]) -> None:
        self.stack[-1].children.append(Element(tag, {key: value or "" for key, value in attrs}))

    def handle_endtag(self, tag: str) -> None:
        for index in range(len(self.stack) - 1, 0, -1):
            if self.stack[index].tag == tag:
                del self.stack[index:]
                break

    def handle_data(self, data: str) -> None:
        if data:
            self.stack[-1].children.append(data)


def parse_document(body: str) -> Element:
    parser = DocumentParser()
    parser.feed(body)
    return parser.root


def normalized(value: str) -> str:
    return " ".join(value.split()).casefold()


class NoRedirectHandler(HTTPRedirectHandler):
    """Prevent Basic credentials from being forwarded to another origin."""

    def redirect_request(self, req, fp, code, msg, headers, newurl):
        return None


def validate_target(target: str) -> str:
    try:
        parts = urlsplit(target)
        host, port = parts.hostname or "", parts.port
    except ValueError as exc:
        raise ValueError("target URL is malformed") from exc
    if (
        parts.scheme.lower() != "https" or not host or host.lower() == "onrender.com"
        or not host.lower().endswith(".onrender.com") or parts.username is not None
        or parts.password is not None or parts.query or parts.fragment
        or parts.path not in ("", "/") or port not in (None, 443)
    ):
        raise ValueError("target must be an HTTPS onrender.com service origin without path, query, or credentials")
    return f"https://{host.lower()}"


class HttpClient:
    def __init__(self, base_url: str, username: str, password: str) -> None:
        self.base_url = base_url
        self.origin = urlsplit(base_url).netloc.lower()
        token = base64.b64encode(f"{username}:{password}".encode("utf-8")).decode("ascii")
        self.authorization = f"Basic {token}"
        self.opener = build_opener(NoRedirectHandler())

    def request(self, path: str, cookie: str | None = None) -> Response:
        url = urljoin(self.base_url + "/", path.lstrip("/"))
        parts = urlsplit(url)
        if parts.scheme.lower() != "https" or parts.netloc.lower() != self.origin:
            raise ValueError("refusing request outside configured HTTPS origin")
        headers = {
            "User-Agent": "RefugeeNavigatorEurope-NetherlandsHoldQA/1.0",
            "Accept": "text/html,*/*;q=0.8",
            "Authorization": self.authorization,
        }
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
        self.failed = False
        self.count = 0

    def check(self, name: str, expected: str, ok: bool, actual: str) -> None:
        self.count += 1
        self.failed |= not ok
        print(f"{'PASS' if ok else 'FAIL'} | {name} | expected: {expected} | actual: {actual[:160]}")


def header(response: Response, name: str) -> str:
    return next((value for key, value in response.headers.items() if key.lower() == name.lower()), "")


def secure_culture_cookie(response: Response, origin: str) -> str | None:
    location = header(response, "Location")
    target = urlsplit(urljoin(origin + "/", location)) if location else None
    set_cookie = header(response, "Set-Cookie")
    if (
        response.status != 302 or target is None or target.scheme != "https"
        or target.netloc.lower() != urlsplit(origin).netloc.lower()
        or not any(part.strip().lower() == "secure" for part in set_cookie.split(";"))
    ):
        return None
    return set_cookie.split(";", 1)[0]


def article_nodes(root: Element) -> list[Element]:
    return [node for node in root.descendants("article", "information-entry")]


def find_article(root: Element, entry_id: str) -> Element | None:
    wanted = "entry-" + entry_id.replace("-", "")
    return next((node for node in article_nodes(root) if node.attrs.get("id") == wanted), None)


def article_title(article: Element) -> str:
    heading = next(article.descendants("h2"), None)
    return heading.text() if heading else ""


def fact_panel(root: Element, aspect_id: str) -> Element | None:
    aspect = next(
        (node for node in root.descendants("section", "structured-aspect")
         if node.attrs.get("id", "").endswith("-" + aspect_id)),
        None,
    )
    return next(aspect.descendants("article", "structured-country"), None) if aspect else None


def request(send: Callable[[str, str | None], Response] | None, client: HttpClient | None,
            path: str, cookie: str | None = None) -> Response:
    return send(path, cookie) if send else client.request(path, cookie)  # type: ignore[union-attr]


def run_checks(base_url: str, username: str, password: str,
               send: Callable[[str, str | None], Response] | None = None,
               sleep: Callable[[float], None] = time.sleep) -> bool:
    base_url = validate_target(base_url)
    client = None if send else HttpClient(base_url, username, password)
    reporter = Reporter()

    ready = None
    for attempt in range(READY_ATTEMPTS):
        candidate = request(send, client, "/countries/netherlands")
        if candidate.status not in (0, 502, 503, 504):
            ready = candidate
            break
        if attempt + 1 < READY_ATTEMPTS:
            sleep(READY_DELAY_SECONDS)
    reporter.check("Authenticated endpoint readiness", "HTTPS route responds", ready is not None,
                   f"HTTP {ready.status}" if ready else f"no response after {READY_ATTEMPTS} attempts")
    if ready is None:
        print(f"Netherlands live hold verification: {reporter.count} checks; FAIL")
        return False

    country_root = parse_document(ready.body)
    reporter.check("Netherlands country page", "HTTP 200", ready.status == 200, f"HTTP {ready.status}")
    reporter.check("Netherlands country page content", "country page rendered",
                   ready.status == 200 and bool(country_root.text()),
                   "page content present" if country_root.text() else "page content missing")

    en_cookie_response = request(send, client, "/set-language?culture=en&returnUrl=%2F")
    en_cookie = secure_culture_cookie(en_cookie_response, base_url)
    reporter.check("English language setup", "same-origin redirect with Secure culture cookie",
                   en_cookie is not None,
                   "Secure cookie set" if en_cookie else f"HTTP {en_cookie_response.status}; cookie/redirect missing")
    if not en_cookie:
        print(f"Netherlands live hold verification: {reporter.count} checks; FAIL")
        return False

    for category in BASE_CATEGORIES:
        response = request(send, client, f"/countries/netherlands/categories/{category}", en_cookie)
        reporter.check(f"Netherlands category route {category}", "HTTP 200", response.status == 200,
                       f"HTTP {response.status}")

    for language in LANGUAGES:
        if language == "en":
            cookie = en_cookie
        else:
            culture_response = request(send, client, f"/set-language?culture={language}&returnUrl=%2F")
            cookie = secure_culture_cookie(culture_response, base_url)
            reporter.check(f"{language.upper()} language setup", "same-origin redirect with Secure culture cookie",
                           cookie is not None,
                           "Secure cookie set" if cookie else f"HTTP {culture_response.status}; cookie/redirect missing")
            if not cookie:
                continue

        category_path = "/countries/netherlands/categories/temporary-protection"
        category_response = request(send, client, category_path, cookie)
        category_root = parse_document(category_response.body)
        accepted = find_article(category_root, ACCEPTED_ENTRY_ID)
        held = find_article(category_root, HELD_ENTRY_ID)
        reporter.check(f"{language.upper()} Temporary Protection category", "HTTP 200", category_response.status == 200,
                       f"HTTP {category_response.status}")
        reporter.check(f"{language.upper()} accepted NL-TP-02", ACCEPTED_TITLES[language],
                       accepted is not None and article_title(accepted) == ACCEPTED_TITLES[language],
                       article_title(accepted) if accepted else "accepted article not rendered")
        reporter.check(f"{language.upper()} held NL-TP-01 suppressed", "no held article element or text",
                       held is None and all(normalized(marker) not in normalized(category_root.text()) for marker in (
                           HELD_TITLES[language], HELD_SUMMARIES[language], HELD_BODY_MARKERS[language])),
                       "held article absent" if held is None else "held article element present")

        fragment_path = category_path + "#entry-" + HELD_ENTRY_ID.replace("-", "")
        fragment_response = request(send, client, fragment_path, cookie)
        fragment_root = parse_document(fragment_response.body)
        fragment_held = find_article(fragment_root, HELD_ENTRY_ID)
        fragment_text = normalized(fragment_root.text())
        reporter.check(f"{language.upper()} direct held-article fragment", "no held title, summary, body or source section",
                       fragment_response.status == 200 and fragment_held is None and all(
                           normalized(marker) not in fragment_text for marker in (
                               HELD_TITLES[language], HELD_SUMMARIES[language], HELD_BODY_MARKERS[language])),
                       "held article and its nested sources absent" if fragment_held is None else "held article element present")

        for entry_id, title, should_appear in (
            (HELD_ENTRY_ID, HELD_TITLES[language], False),
            (ACCEPTED_ENTRY_ID, ACCEPTED_TITLES[language], True),
        ):
            search_path = f"/search?q={quote_plus(title)}&country=netherlands"
            search_response = request(send, client, search_path, cookie)
            search_text = normalized(parse_document(search_response.body).text())
            visible = normalized(title) in search_text
            expected = "accepted article appears" if should_appear else "held article is absent"
            reporter.check(f"{language.upper()} search {'accepted' if should_appear else 'held'} article",
                           expected, search_response.status == 200 and visible == should_appear,
                           f"HTTP {search_response.status}; {'title present' if visible else 'title absent'}")

        compare_path = "/compare?country1=netherlands&country2=denmark&category=temporary-protection"
        compare_response = request(send, client, compare_path, cookie)
        compare_root = parse_document(compare_response.body)
        reporter.check(f"{language.upper()} Netherlands comparison", "HTTP 200", compare_response.status == 200,
                       f"HTTP {compare_response.status}")

        for fact_key, markers in HELD_FACT_MARKERS.items():
            panel = fact_panel(compare_root, fact_key)
            panel_text = normalized(panel.text()) if panel else ""
            fact_sources = list(panel.descendants("details", "fact-sources")) if panel else []
            suppressed = panel is not None and normalized(UNAVAILABLE_TEXT[language]) in panel_text \
                and normalized(markers[language]) not in panel_text and not fact_sources
            reporter.check(f"{language.upper()} Draft fact {fact_key}", "not established placeholder; no answer or source group",
                           compare_response.status == 200 and suppressed,
                           "Draft answer and sources suppressed" if suppressed else "suppression check failed")

        unavailable_panel = fact_panel(compare_root, "residence-filing")
        unavailable_text = normalized(unavailable_panel.text()) if unavailable_panel else ""
        unavailable_sources = list(unavailable_panel.descendants("details", "fact-sources")) if unavailable_panel else []
        unavailable_ok = unavailable_panel is not None and normalized(UNAVAILABLE_TEXT[language]) in unavailable_text \
            and not unavailable_sources
        reporter.check(f"{language.upper()} unavailable residence-filing", "unavailable placeholder and no source group",
                       compare_response.status == 200 and unavailable_ok,
                       "unavailable state retained" if unavailable_ok else "unavailable-state check failed")

        positive_panel = fact_panel(compare_root, "residence-routes")
        positive_text = normalized(positive_panel.text()) if positive_panel else ""
        positive_sources = list(positive_panel.descendants("details", "fact-sources")) if positive_panel else []
        positive_links = [a for source_group in positive_sources for a in source_group.descendants("a")
                          if urlsplit(a.attrs.get("href", "")).scheme.lower() == "https"]
        positive_ok = positive_panel is not None and normalized(ACCEPTED_FACT_MARKER[language]) in positive_text \
            and bool(positive_links)
        reporter.check(f"{language.upper()} Approved positive control residence-routes",
                       "approved answer and HTTPS source group visible",
                       compare_response.status == 200 and positive_ok,
                       f"answer visible; {len(positive_links)} HTTPS source link(s)" if positive_ok else "positive control missing")

    print(f"Netherlands live hold verification: {reporter.count} checks; {'FAIL' if reporter.failed else 'PASS'}")
    return not reporter.failed


def main() -> int:
    try:
        base_url = validate_target(os.environ.get("RENDER_TARGET_URL", ""))
    except ValueError as exc:
        print(f"FAIL | Configuration | expected: HTTPS onrender.com URL | actual: {exc}")
        return 2
    username = os.environ.get("PRIVATE_ALPHA_USERNAME", "")
    password = os.environ.get("PRIVATE_ALPHA_PASSWORD", "")
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
