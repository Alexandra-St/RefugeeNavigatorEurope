from __future__ import annotations

import contextlib
import io
import unittest
from urllib.parse import urlsplit

import twelve_country_live_scope_test as scope
import netherlands_live_hold_test as live


SLOVAKIA_CATEGORY_COUNTS = {
    "temporary-protection": 2, "employment": 2, "healthcare": 1,
    "housing": 1, "education": 1, "social-assistance": 1,
}

ROMANIA_CATEGORY_COUNTS = {
    "temporary-protection": 2,
    "employment": 2,
    "healthcare": 1,
    "housing": 1,
    "education": 1,
    "social-assistance": 1,
}
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


def localized_name(slug: str, language: str) -> str:
    if slug == "italy":
        return scope.ITALY_NAMES[language]
    if slug == "romania":
        return scope.COUNTRY_NAMES[language]
    if slug == "sweden":
        return scope.SWEDEN_NAMES[language]
    if slug == "slovakia":
        return scope.SLOVAKIA_NAMES[language]
    if slug == "bulgaria":
        return scope.BULGARIA_NAMES[language]
    if slug == "austria":
        return scope.AUSTRIA_NAMES[language]
    return slug.title()


def fixture_response(path: str, countries: tuple[str, ...] | None = None, cookie: str | None = None,
                     bad_romania_fact: str | None = None, bad_slovakia_fact: str | None = None,
                     bad_bulgaria_fact: str | None = None,
                     bad_bulgaria_fact_mode: str = "unavailable",
                     bad_austria_fact: str | None = None,
                     bad_austria_fact_mode: str = "unavailable",
                     bad_italy_fact: str | None = None,
                     bad_italy_fact_mode: str = "unavailable",
                     bad_italy_category: str | None = None,
                     bad_italy_heading: bool = False,
                     bad_italy_search: bool = False) -> live.Response:
    countries = countries or scope.COUNTRIES
    cards = "".join(f'<article class="country-card"><a href="/countries/{slug}">{localized_name(slug, "en")}</a></article>' for slug in countries)
    values = "".join(f'<option value="{slug}">{localized_name(slug, "en")}</option>' for slug in countries)
    language = (cookie or "c=en").split("c=", 1)[1][:2]
    if path.startswith("/set-language?"):
        culture = path.split("culture=", 1)[1][:2]
        return live.Response(302, {"Location": "/", "Set-Cookie": f".AspNetCore.Culture=c={culture}|uic={culture}; path=/; secure"}, "")
    if path == "/":
        return live.Response(200, {}, f'<html><main>{cards}</main></html>')
    if path == "/search":
        return live.Response(200, {}, f'<form><select name="country">{values}</select></form>')
    if path.startswith("/search?"):
        query = urlsplit(path).query
        slug = next((value.split("=", 1)[1] for value in query.split("&") if value.startswith("country=")), "")
        count = scope.SEARCH_RESULT_COUNTS.get(slug, 0)
        if slug == "italy" and bad_italy_search:
            count = 7
        results = "".join('<article class="result-card">Accepted result</article>' for _ in range(count))
        return live.Response(200, {}, f'<section>{results}</section>')
    if path == "/compare":
        return live.Response(200, {}, f'<form><select name="country1">{values}</select><select name="country2">{values}</select></form>')
    if path.startswith("/compare?"):
        query = urlsplit(path).query
        params = dict(part.split("=", 1) for part in query.split("&") if "=" in part)
        slug = params.get("country1", "")
        category = params.get("category", "")
        if slug == "sweden" and category == "social-assistance":
            unavailable = live.UNAVAILABLE_TEXT[language]
            name = scope.SWEDEN_NAMES[language]
            section = ('<section class="structured-aspect" aria-labelledby="structured-aspect-social-child-benefits">'
                       '<h4 id="structured-aspect-social-child-benefits">Child benefits</h4><div class="structured-columns">'
                       f'<article class="structured-country"><h5>{name}</h5><p>{unavailable}</p></article>'
                       '<article class="structured-country"><h5>Denmark</h5><p>Fact</p></article></div></section>')
            return live.Response(200, {}, section)
        if slug == "sweden":
            return live.Response(200, {}, f'<section class="structured-aspect" aria-labelledby="structured-aspect-protection-scheme"><h4 id="structured-aspect-protection-scheme">Protection</h4><div class="structured-columns"><article class="structured-country"><h5>{scope.SWEDEN_NAMES[language]}</h5><p>Approved comparison</p><details class="fact-sources"><a href="https://example.gov/source">Official source</a></details></article><article class="structured-country"><h5>Denmark</h5><p>Fact</p><details class="fact-sources"><a href="https://example.gov/denmark">Official source</a></details></article></div></section>')
        if slug == "slovakia":
            aspects = scope.SLOVAKIA_ASPECTS_BY_CATEGORY.get(category, ())
            sections = []
            for aspect_id in aspects:
                if bad_slovakia_fact == aspect_id:
                    answer = live.UNAVAILABLE_TEXT[language]
                    sources = ""
                else:
                    answer = f"Approved Slovakia answer for {aspect_id}"
                    sources = '<details class="fact-sources"><a href="https://example.gov/slovakia">Official source</a></details>'
                sections.append(
                    f'<section class="structured-aspect" aria-labelledby="structured-aspect-slovakia-{aspect_id}">'
                    f'<h4 id="structured-aspect-slovakia-{aspect_id}">{aspect_id}</h4><div class="structured-columns">'
                    f'<article class="structured-country"><h5>{scope.SLOVAKIA_NAMES[language]}</h5><p>{answer}</p>{sources}</article>'
                    '<article class="structured-country"><h5>Denmark</h5><p>Fact</p>'
                    '<details class="fact-sources"><a href="https://example.gov/denmark">Official source</a></details>'
                    '</article></div></section>')
            return live.Response(200, {}, "".join(sections))
        if slug == "romania":
            aspects = scope.ROMANIA_ASPECTS_BY_CATEGORY.get(category, ())
            sections = []
            for aspect_id in aspects:
                if bad_romania_fact == aspect_id:
                    answer = live.UNAVAILABLE_TEXT[language]
                    sources = ""
                else:
                    answer = f"Approved answer for {aspect_id}"
                    sources = '<details class="fact-sources"><a href="https://example.gov/romania">Official source</a></details>'
                sections.append(
                    f'<section class="structured-aspect" aria-labelledby="structured-aspect-romania-{aspect_id}">'
                    f'<h4 id="structured-aspect-romania-{aspect_id}">{aspect_id}</h4><div class="structured-columns">'
                    f'<article class="structured-country"><h5>{scope.COUNTRY_NAMES[language]}</h5><p>{answer}</p>{sources}</article>'
                    '<article class="structured-country"><h5>Denmark</h5><p>Fact</p>'
                    '<details class="fact-sources"><a href="https://example.gov/denmark">Official source</a></details>'
                    '</article></div></section>')
            return live.Response(200, {}, "".join(sections))
        if slug == "bulgaria":
            aspects = scope.BULGARIA_ASPECTS_BY_CATEGORY.get(category, ())
            sections = []
            for aspect_id in aspects:
                if bad_bulgaria_fact == aspect_id and bad_bulgaria_fact_mode == "unavailable":
                    answer = live.UNAVAILABLE_TEXT[language]
                    sources = ""
                elif bad_bulgaria_fact == aspect_id and bad_bulgaria_fact_mode == "http-source":
                    answer = f"Accepted Bulgaria answer for {aspect_id}"
                    sources = '<details class="fact-sources"><a href="http://example.gov/bulgaria">Official source</a></details>'
                else:
                    answer = f"Accepted Bulgaria answer for {aspect_id}"
                    sources = '<details class="fact-sources"><a href="https://example.gov/bulgaria">Official source</a></details>'
                sections.append(
                    f'<section class="structured-aspect" aria-labelledby="structured-aspect-bulgaria-{aspect_id}">'
                    f'<h4 id="structured-aspect-bulgaria-{aspect_id}">{aspect_id}</h4><div class="structured-columns">'
                    f'<article class="structured-country"><h5>{scope.BULGARIA_NAMES[language]}</h5><p>{answer}</p>{sources}</article>'
                    '<article class="structured-country"><h5>Denmark</h5><p>Fact</p>'
                    '<details class="fact-sources"><a href="https://example.gov/denmark">Official source</a></details>'
                    '</article></div></section>')
            return live.Response(200, {}, "".join(sections))
        if slug == "austria":
            aspects = scope.AUSTRIA_ASPECTS_BY_CATEGORY.get(category, ())
            sections = []
            for aspect_id in aspects:
                if bad_austria_fact == aspect_id and bad_austria_fact_mode == "unavailable":
                    answer = live.UNAVAILABLE_TEXT[language]
                    sources = ""
                elif bad_austria_fact == aspect_id and bad_austria_fact_mode == "http-source":
                    answer = f"Accepted Austria answer for {aspect_id}"
                    sources = '<details class="fact-sources"><a href="http://example.gov/austria">Official source</a></details>'
                else:
                    answer = f"Accepted Austria answer for {aspect_id}"
                    sources = '<details class="fact-sources"><a href="https://example.gov/austria">Official source</a></details>'
                sections.append(
                    f'<section class="structured-aspect" aria-labelledby="structured-aspect-austria-{aspect_id}">'
                    f'<h4 id="structured-aspect-austria-{aspect_id}">{aspect_id}</h4><div class="structured-columns">'
                    f'<article class="structured-country"><h5>{scope.AUSTRIA_NAMES[language]}</h5><p>{answer}</p>{sources}</article>'
                    '<article class="structured-country"><h5>Denmark</h5><p>Fact</p>'
                    '<details class="fact-sources"><a href="https://example.gov/denmark">Official source</a></details>'
                    '</article></div></section>')
            return live.Response(200, {}, "".join(sections))
        if slug == "italy":
            sections = []
            for aspect_id in scope.ITALY_ASPECTS_BY_CATEGORY.get(category, ()):
                if bad_italy_fact == aspect_id and bad_italy_fact_mode == "unavailable":
                    answer = live.UNAVAILABLE_TEXT[language]
                    sources = ""
                elif bad_italy_fact == aspect_id and bad_italy_fact_mode == "missing-source":
                    answer = f"Approved Italy answer for {aspect_id}"
                    sources = ""
                elif bad_italy_fact == aspect_id and bad_italy_fact_mode == "http-source":
                    answer = f"Approved Italy answer for {aspect_id}"
                    sources = '<details class="fact-sources"><a href="http://example.gov/italy">Official source</a></details>'
                else:
                    answer = f"Approved Italy answer for {aspect_id}"
                    sources = '<details class="fact-sources"><a href="https://example.gov/italy">Official source</a></details>'
                sections.append(
                    f'<section class="structured-aspect" aria-labelledby="structured-aspect-italy-{aspect_id}">'
                    f'<h4 id="structured-aspect-italy-{aspect_id}">{aspect_id}</h4><div class="structured-columns">'
                    f'<article class="structured-country"><h5>{scope.ITALY_NAMES[language]}</h5><p>{answer}</p>{sources}</article>'
                    '<article class="structured-country"><h5>Denmark</h5><p>Fact</p>'
                    '<details class="fact-sources"><a href="https://example.gov/denmark">Official source</a></details>'
                    '</article></div></section>')
            return live.Response(200, {}, "".join(sections))
        return live.Response(200, {}, '<article class="structured-country">Fact</article>')
    if path.startswith("/countries/"):
        route = path.split("?", 1)[0]
        if "/categories/" in route:
            slug, category = route.split("/countries/", 1)[1].split("/categories/", 1)
            if slug not in countries or category not in scope.CATEGORIES:
                return live.Response(404, {}, "")
            if slug == "romania":
                count = ROMANIA_CATEGORY_COUNTS[category]
            elif slug == "bulgaria":
                count = BULGARIA_CATEGORY_COUNTS[category]
            elif slug == "austria":
                count = AUSTRIA_CATEGORY_COUNTS[category]
            elif slug == "italy":
                count = scope.ITALY_CATEGORY_COUNTS[category]
                if bad_italy_category == category:
                    count -= 1
            elif slug == "netherlands":
                count = {"temporary-protection": 1, "employment": 2, "healthcare": 1,
                         "housing": 1, "education": 1, "social-assistance": 1}[category]
            elif slug == "slovakia":
                count = SLOVAKIA_CATEGORY_COUNTS[category]
            elif slug == "sweden":
                count = {"temporary-protection": 2, "employment": 2, "healthcare": 1,
                         "housing": 1, "education": 1, "social-assistance": 1}[category]
            else:
                count = 2 if category in ("temporary-protection", "employment") else 1
            entries = "".join('<article class="information-entry">Article</article>' for _ in range(count))
            return live.Response(200, {}, f'<main class="category-page"><h1>{category}</h1>{entries}</main>')
        slug = route.rsplit("/", 1)[-1]
        if slug not in countries:
            return live.Response(404, {}, "")
        heading = "Italia errata" if slug == "italy" and bad_italy_heading else localized_name(slug, language)
        return live.Response(200, {}, f'<main class="country-page"><h1>{heading}</h1></main>')
    return live.Response(404, {}, "")


class TwelveCountryLiveScopeTests(unittest.TestCase):
    def run_fixture(self, countries: tuple[str, ...] | None = None, bad_romania_fact: str | None = None,
                    bad_slovakia_fact: str | None = None, bad_bulgaria_fact: str | None = None,
                    bad_bulgaria_fact_mode: str = "unavailable", username: str = "", password: str = "",
                    bad_austria_fact: str | None = None, bad_austria_fact_mode: str = "unavailable",
                    bad_italy_fact: str | None = None, bad_italy_fact_mode: str = "unavailable",
                    bad_italy_category: str | None = None, bad_italy_heading: bool = False,
                    bad_italy_search: bool = False):
        output = io.StringIO()
        request = lambda path, cookie=None: fixture_response(path, countries, cookie, bad_romania_fact,
                                                               bad_slovakia_fact, bad_bulgaria_fact,
                                                               bad_bulgaria_fact_mode, bad_austria_fact,
                                                               bad_austria_fact_mode, bad_italy_fact,
                                                               bad_italy_fact_mode, bad_italy_category,
                                                               bad_italy_heading, bad_italy_search)
        with contextlib.redirect_stdout(output):
            passed = scope.run_checks("https://private.onrender.com", username, password,
                                      request=request, sleep=lambda _: None)
        return passed, output.getvalue()

    def test_exact_twelve_country_production_fixture_and_austria_acceptance_pass(self):
        passed, output = self.run_fixture()
        self.assertTrue(passed)
        self.assertEqual(12, len(scope.COUNTRIES))
        self.assertEqual(12, len(set(scope.COUNTRIES)))
        self.assertEqual(24, sum(len(aspects) for aspects in scope.AUSTRIA_ASPECTS_BY_CATEGORY.values()))
        self.assertEqual(24, len({aspect for aspects in scope.AUSTRIA_ASPECTS_BY_CATEGORY.values() for aspect in aspects}))
        self.assertEqual(scope.COUNTRIES, (
            "denmark", "germany", "poland", "czechia", "spain", "netherlands", "sweden", "romania", "slovakia", "bulgaria", "austria", "italy",
        ))
        self.assertIn("Twelve-country live scope verification:", output)
        self.assertIn("expected: exactly twelve accepted countries", output)
        self.assertIn("PASS | Italy country route (EN) | expected: HTTP 200 and Italy", output)
        self.assertIn("PASS | Italy accepted articles in Category (RU) | expected: exactly eight", output)
        self.assertIn("PASS | Search accepted articles italy (UK) | expected: 8", output)
        for language in ("EN", "UK", "RU"):
            self.assertIn(f"PASS | Italy Compare facts ({language}) | expected: 24 available answers with HTTPS source disclosures | actual: 24/24", output)
        self.assertIn("Romania country route (EN)", output)
        self.assertIn("Romania accepted articles in Category (UK)", output)
        self.assertIn("Search accepted articles romania (RU)", output)
        self.assertIn("Romania Compare facts (EN) | expected: 24 available answers with source disclosures | actual: 24/24", output)
        self.assertIn("Compare accepted country romania", output)
        self.assertIn("PASS | EN Sweden social-child-benefits hold", output)
        self.assertIn("Slovakia country route (EN)", output)
        self.assertIn("Slovakia accepted articles in Category (UK)", output)
        self.assertIn("Search accepted articles slovakia (RU)", output)
        for language in ("EN", "UK", "RU"):
            self.assertIn(f"PASS | Slovakia Compare facts ({language}) | expected: 24 available answers with HTTPS source disclosures | actual: 24/24", output)
            self.assertIn(f"PASS | Bulgaria country route ({language})", output)
            self.assertIn(f"PASS | Bulgaria accepted articles in Category ({language}) | expected: exactly eight", output)
            self.assertIn(f"PASS | Search accepted articles bulgaria ({language}) | expected: 8", output)
            self.assertIn(f"PASS | Bulgaria Compare facts ({language}) | expected: 24 available answers with HTTPS source disclosures | actual: 24/24", output)
            self.assertIn(f"PASS | Austria country route ({language})", output)
            self.assertIn(f"PASS | Austria accepted articles in Category ({language}) | expected: exactly eight", output)
            self.assertIn(f"PASS | Search accepted articles austria ({language}) | expected: 8", output)
            self.assertIn(f"PASS | Austria Compare facts ({language}) | expected: 24 available answers with HTTPS source disclosures | actual: 24/24", output)
            for category, count in AUSTRIA_CATEGORY_COUNTS.items():
                self.assertIn(f"PASS | Austria category {category} ({language}) | expected: HTTP 200 with {count} accepted article(s)", output)
        self.assertIn("Bulgaria category temporary-protection (EN) | expected: HTTP 200 with 2 accepted article(s)", output)
        self.assertIn("Bulgaria category social-assistance (RU) | expected: HTTP 200 with 1 accepted article(s)", output)
        self.assertIn("PASS | Category accepted articles bulgaria (EN) | expected: 8", output)
        self.assertIn("PASS | Austria category temporary-protection (EN) | expected: HTTP 200 with 2 accepted article(s)", output)
        self.assertIn("PASS | Austria category social-assistance (RU) | expected: HTTP 200 with 1 accepted article(s)", output)
        self.assertIn("PASS | Category accepted articles austria (EN) | expected: 8", output)
        for language, name in scope.BULGARIA_NAMES.items():
            self.assertIn(f"PASS | Bulgaria country route ({language.upper()}) | expected: HTTP 200 and {name}", output)
        for language, name in scope.AUSTRIA_NAMES.items():
            self.assertIn(f"PASS | Austria country route ({language.upper()}) | expected: HTTP 200 and {name}", output)
        self.assertNotIn("Authorization", output)

    def test_missing_slovakia_fails(self):
        countries = tuple(country for country in scope.COUNTRIES if country != "slovakia")
        passed, output = self.run_fixture(countries=countries)
        self.assertFalse(passed)
        self.assertIn("FAIL | Home country cards", output)
        self.assertIn("FAIL | Search country filter", output)
        self.assertIn("FAIL | Compare selector country1", output)

    def test_missing_italy_fails_exact_scope(self):
        countries = tuple(country for country in scope.COUNTRIES if country != "italy")
        passed, output = self.run_fixture(countries=countries)
        self.assertFalse(passed)
        self.assertIn("FAIL | Home country cards", output)
        self.assertIn("FAIL | Search country filter", output)
        self.assertIn("FAIL | Compare selector country1", output)

    def test_italy_category_count_mismatch_fails(self):
        passed, output = self.run_fixture(bad_italy_category="education")
        self.assertFalse(passed)
        self.assertIn("FAIL | Italy category education (EN)", output)
        self.assertIn("FAIL | Italy category education (UK)", output)

    def test_italy_search_count_mismatch_fails(self):
        passed, output = self.run_fixture(bad_italy_search=True)
        self.assertFalse(passed)
        for language in ("EN", "UK", "RU"):
            self.assertIn(f"FAIL | Search accepted articles italy ({language})", output)

    def test_italy_wrong_localized_heading_fails(self):
        passed, output = self.run_fixture(bad_italy_heading=True)
        self.assertFalse(passed)
        for language in ("EN", "UK", "RU"):
            self.assertIn(f"FAIL | Italy country route ({language})", output)

    def test_italy_unavailable_fact_fails(self):
        passed, output = self.run_fixture(bad_italy_fact="education-pupil-language")
        self.assertFalse(passed)
        for language in ("EN", "UK", "RU"):
            self.assertIn(f"FAIL | Italy Compare facts ({language})", output)

    def test_italy_fact_without_source_disclosure_fails(self):
        passed, output = self.run_fixture(bad_italy_fact="education-pupil-language",
                                          bad_italy_fact_mode="missing-source")
        self.assertFalse(passed)
        for language in ("EN", "UK", "RU"):
            self.assertIn(f"FAIL | Italy Compare facts ({language})", output)

    def test_italy_http_source_fails(self):
        passed, output = self.run_fixture(bad_italy_fact="education-pupil-language",
                                          bad_italy_fact_mode="http-source")
        self.assertFalse(passed)
        for language in ("EN", "UK", "RU"):
            self.assertIn(f"FAIL | Italy Compare facts ({language})", output)

    def test_missing_bulgaria_fails_all_exact_scope_selectors(self):
        countries = tuple(country for country in scope.COUNTRIES if country != "bulgaria")
        passed, output = self.run_fixture(countries=countries)
        self.assertFalse(passed)
        self.assertIn("FAIL | Home country cards", output)
        self.assertIn("FAIL | Search country filter", output)
        self.assertIn("FAIL | Compare selector country1", output)
        self.assertIn("FAIL | Compare selector country2", output)

    def test_missing_austria_fails_all_exact_scope_selectors(self):
        countries = tuple(country for country in scope.COUNTRIES if country != "austria")
        passed, output = self.run_fixture(countries=countries)
        self.assertFalse(passed)
        self.assertIn("FAIL | Home country cards", output)
        self.assertIn("FAIL | Search country filter", output)
        self.assertIn("FAIL | Compare selector country1", output)
        self.assertIn("FAIL | Compare selector country2", output)

    def test_extra_country_fails_exact_scope(self):
        passed, output = self.run_fixture(countries=scope.COUNTRIES + ("switzerland",))
        self.assertFalse(passed)
        self.assertIn("FAIL | Home country cards", output)
        self.assertIn("FAIL | Search country filter", output)
        self.assertIn("FAIL | Compare selector country1", output)
        self.assertIn("FAIL | Compare selector country2", output)

    def test_romania_unavailable_or_unsourced_fact_fails(self):
        passed, output = self.run_fixture(bad_romania_fact="social-child-benefits")
        self.assertFalse(passed)
        self.assertIn("FAIL | Romania Compare facts (EN)", output)
        self.assertIn("FAIL | Romania Compare facts (UK)", output)
        self.assertIn("FAIL | Romania Compare facts (RU)", output)

    def test_unavailable_slovakia_fact_fails_in_all_languages(self):
        passed, output = self.run_fixture(bad_slovakia_fact="social-child-benefits")
        self.assertFalse(passed)
        for language in ("EN", "UK", "RU"):
            self.assertIn(f"FAIL | Slovakia Compare facts ({language})", output)

    def test_unavailable_bulgaria_fact_fails_in_all_languages(self):
        passed, output = self.run_fixture(bad_bulgaria_fact="social-child-benefits")
        self.assertFalse(passed)
        for language in ("EN", "UK", "RU"):
            self.assertIn(f"FAIL | Bulgaria Compare facts ({language})", output)

    def test_unavailable_austria_fact_fails_in_all_languages(self):
        passed, output = self.run_fixture(bad_austria_fact="social-child-benefits")
        self.assertFalse(passed)
        for language in ("EN", "UK", "RU"):
            self.assertIn(f"FAIL | Austria Compare facts ({language})", output)

    def test_austria_fact_without_https_source_fails_in_all_languages(self):
        passed, output = self.run_fixture(bad_austria_fact="social-child-benefits",
                                          bad_austria_fact_mode="http-source")
        self.assertFalse(passed)
        for language in ("EN", "UK", "RU"):
            self.assertIn(f"FAIL | Austria Compare facts ({language})", output)

    def test_bulgaria_fact_without_https_source_fails_in_all_languages(self):
        passed, output = self.run_fixture(bad_bulgaria_fact="social-child-benefits",
                                          bad_bulgaria_fact_mode="http-source")
        self.assertFalse(passed)
        for language in ("EN", "UK", "RU"):
            self.assertIn(f"FAIL | Bulgaria Compare facts ({language})", output)

    def test_credentials_are_never_emitted(self):
        username = "PRIVATE_ALPHA_USERNAME_SENTINEL"
        password = "PRIVATE_ALPHA_PASSWORD_SENTINEL"
        passed, output = self.run_fixture(username=username, password=password)
        self.assertTrue(passed)
        self.assertNotIn(username, output)
        self.assertNotIn(password, output)
        self.assertNotIn("Authorization", output)

    def test_netherlands_search_count_remains_seven(self):
        passed, output = self.run_fixture()
        self.assertTrue(passed)
        for language in ("EN", "UK", "RU"):
            self.assertIn(f"PASS | Search accepted articles netherlands ({language}) | expected: 7", output)

    def test_sweden_held_fact_remains_unavailable_and_source_less(self):
        passed, output = self.run_fixture()
        self.assertTrue(passed)
        for language in ("EN", "UK", "RU"):
            self.assertIn(f"PASS | {language} Sweden social-child-benefits hold", output)
        self.assertNotIn("Authorization", output)


if __name__ == "__main__":
    unittest.main()
