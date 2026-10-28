from __future__ import annotations

import contextlib
import io
import unittest
from urllib.parse import urlsplit

import seven_country_live_scope_test as scope
import netherlands_live_hold_test as live


def fixture_response(path: str, include_sweden: bool = True, cookie: str | None = None) -> live.Response:
    countries = list(scope.COUNTRIES) if include_sweden else [slug for slug in scope.COUNTRIES if slug != "sweden"]
    cards = "".join(f'<article class="country-card"><a href="/countries/{slug}">{slug}</a></article>' for slug in countries)
    values = "".join(f'<option value="{slug}">{slug}</option>' for slug in countries)
    if path == "/set-language?culture=en&returnUrl=%2F" or path.startswith("/set-language?"):
        culture = path.split("culture=", 1)[1][:2]
        return live.Response(302, {"Location": "/", "Set-Cookie": f".AspNetCore.Culture=c={culture}|uic={culture}; path=/; secure"}, "")
    if path == "/":
        return live.Response(200, {}, f'<html><main>{cards}</main></html>')
    if path == "/search":
        return live.Response(200, {}, f'<form><select name="country">{values}</select></form>')
    if path.startswith("/search?"):
        slug = urlsplit(path).query.split("country=", 1)[1]
        count = scope.SEARCH_RESULT_COUNTS.get(slug, 0)
        results = "".join('<article class="result-card">Accepted result</article>' for _ in range(count))
        return live.Response(200, {}, f'<section>{results}</section>')
    if path == "/compare":
        return live.Response(200, {}, f'<form><select name="country1">{values}</select><select name="country2">{values}</select></form>')
    if path.startswith("/compare?"):
        if "country1=sweden" in path:
            if "social-assistance" in path:
                language = (cookie or "c=en").split("c=", 1)[1][:2]
                unavailable = {"en": "Not established in the reviewed sources.", "uk": "Не встановлено за перевіреними джерелами.", "ru": "Не установлено по проверенным источникам."}[language]
                sweden_name = {"en": "Sweden", "uk": "Швеція", "ru": "Швеция"}[language]
                return live.Response(200, {}, f'<section class="structured-aspect" aria-labelledby="structured-aspect-social-child-benefits"><h4 id="structured-aspect-social-child-benefits">Child benefits</h4><div class="structured-columns"><article class="structured-country"><h5>{sweden_name}</h5><p>{unavailable}</p></article><article class="structured-country"><h5>Denmark</h5><p>Fact</p></article></div></section>')
            return live.Response(200, {}, '<article class="structured-country"><h5>Sweden</h5><p>Approved comparison</p><details class="fact-sources"><a href="https://example.gov/source">Official source</a></details></article><article class="structured-country"><h5>Denmark</h5><p>Fact</p></article>')
        return live.Response(200, {}, '<article class="structured-country">Fact</article>')
    if path == "/countries/sweden" and not include_sweden:
        return live.Response(404, {}, "")
    if path.startswith("/countries/sweden/categories/") and not include_sweden:
        return live.Response(404, {}, "")
    if path.startswith("/countries/") and "/categories/" not in path:
        slug = path.rsplit("/", 1)[-1]
        return live.Response(200, {}, f'<main class="country-page"><h1>{slug}</h1></main>')
    if "/categories/" in path:
        slug, category = path.split("/countries/", 1)[1].split("/categories/")
        count = 2 if category in ("temporary-protection", "employment") else 1
        if slug == "netherlands" and category == "temporary-protection": count = 1
        if slug == "sweden": count = {"temporary-protection": 2, "employment": 2, "healthcare": 1, "housing": 1, "education": 1, "social-assistance": 1}.get(category, 0)
        entries = "".join('<article class="information-entry">Article</article>' for _ in range(count))
        return live.Response(200, {}, f'<main class="category-page"><h1>{category}</h1>{entries}</main>')
    return live.Response(404, {}, "")


class SevenCountryLiveScopeTests(unittest.TestCase):
    def run_fixture(self, include_sweden: bool = True):
        output = io.StringIO()
        request = lambda path, cookie=None: fixture_response(path, include_sweden, cookie)
        with contextlib.redirect_stdout(output):
            passed = scope.run_checks("https://private.onrender.com", "test-user", "test-password",
                                     request=request, sleep=lambda _: None)
        return passed, output.getvalue()

    def test_seven_country_live_scope_verifier_passes_expected_production_fixture(self):
        passed, output = self.run_fixture()
        self.assertTrue(passed)
        self.assertIn("Seven-country live scope verification:", output)
        self.assertIn("Home country cards", output)
        self.assertIn("Sweden country route", output)
        self.assertIn("Sweden category route temporary-protection", output)
        self.assertIn("Search accepted articles czechia (UK)", output)
        self.assertIn("Compare accepts Sweden (EN)", output)
        self.assertIn("PASS | EN Sweden social-child-benefits hold", output)
        self.assertNotIn("test-user", output)
        self.assertNotIn("test-password", output)
        self.assertNotIn("Authorization", output)

    def test_sweden_missing_from_home_fails_the_seven_country_gate(self):
        passed, output = self.run_fixture(include_sweden=False)
        self.assertFalse(passed)
        self.assertIn("FAIL | Home country cards", output)
        self.assertNotIn("test-password", output)

    def test_social_child_benefits_must_remain_unavailable_and_source_less(self):
        passed, output = self.run_fixture()
        self.assertTrue(passed)
        self.assertIn("PASS | EN Sweden social-child-benefits hold", output)
        self.assertNotIn("test-password", output)


if __name__ == "__main__":
    unittest.main()
