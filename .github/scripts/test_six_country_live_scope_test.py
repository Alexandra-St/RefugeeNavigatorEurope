import contextlib
import io
import unittest
from urllib.parse import urlsplit

import six_country_live_scope_test as scope
import netherlands_live_hold_test as live


def fixture_response(path: str, include_sweden: bool = False) -> live.Response:
    countries = list(scope.COUNTRIES) + (["sweden"] if include_sweden else [])
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
        if "sweden" in path:
            return live.Response(200, {}, '<p class="selection-message">Invalid country</p>')
        return live.Response(200, {}, '<article class="structured-country">Fact</article>')
    if path == "/countries/sweden" or path.startswith("/countries/sweden/categories/"):
        return live.Response(404, {}, "")
    if path.startswith("/countries/") and "/categories/" not in path:
        slug = path.rsplit("/", 1)[-1]
        return live.Response(200, {}, f'<main class="country-page"><h1>{slug}</h1></main>')
    if "/categories/" in path:
        slug, category = path.split("/countries/", 1)[1].split("/categories/")
        count = 2 if category in ("temporary-protection", "employment") else 1
        if slug == "netherlands" and category == "temporary-protection": count = 1
        entries = "".join('<article class="information-entry">Article</article>' for _ in range(count))
        return live.Response(200, {}, f'<main class="category-page"><h1>{category}</h1>{entries}</main>')
    return live.Response(404, {}, "")


class SixCountryLiveScopeTests(unittest.TestCase):
    def run_fixture(self, include_sweden: bool = False):
        output = io.StringIO()
        request = lambda path, cookie=None: fixture_response(path, include_sweden)
        with contextlib.redirect_stdout(output):
            passed = scope.run_checks("https://private.onrender.com", "test-user", "test-password",
                                     request=request, sleep=lambda _: None)
        return passed, output.getvalue()

    def test_six_country_live_scope_verifier_passes_expected_production_fixture(self):
        passed, output = self.run_fixture()
        self.assertTrue(passed)
        self.assertIn("Six-country live scope verification:", output)
        self.assertIn("Home country cards", output)
        self.assertIn("Sweden country route", output)
        self.assertIn("Sweden category route temporary-protection", output)
        self.assertIn("Search accepted articles czechia (UK)", output)
        self.assertIn("Compare rejects country1=sweden", output)
        self.assertNotIn("test-user", output)
        self.assertNotIn("test-password", output)
        self.assertNotIn("Authorization", output)

    def test_home_exposure_of_sweden_fails_the_six_country_gate(self):
        passed, output = self.run_fixture(include_sweden=True)
        self.assertFalse(passed)
        self.assertIn("FAIL | Home country cards", output)
        self.assertNotIn("test-password", output)


if __name__ == "__main__":
    unittest.main()
