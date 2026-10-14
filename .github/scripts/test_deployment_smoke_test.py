import contextlib
import importlib.util
import io
import sys
from pathlib import Path
import unittest

SCRIPT = Path(__file__).with_name("deployment_smoke_test.py")
spec = importlib.util.spec_from_file_location("deployment_smoke_test", SCRIPT)
smoke = importlib.util.module_from_spec(spec)
sys.modules[spec.name] = smoke
spec.loader.exec_module(smoke)


class SmokeScriptTests(unittest.TestCase):
    def test_target_accepts_only_https_onrender_service_origin(self):
        self.assertEqual(smoke.validate_target("https://alpha-example.onrender.com"), "https://alpha-example.onrender.com")
        self.assertEqual(smoke.validate_target("https://alpha-example.onrender.com/"), "https://alpha-example.onrender.com")
        for target in (
            "http://alpha-example.onrender.com",
            "https://onrender.com",
            "https://alpha.onrender.com.attacker.invalid",
            "https://user:pass@alpha.onrender.com",
            "https://alpha.onrender.com/path",
            "https://alpha.onrender.com/?q=x",
            "https://alpha.onrender.com:444",
        ):
            with self.subTest(target=target), self.assertRaises(ValueError):
                smoke.validate_target(target)

    def test_html_parser_decodes_entities_inside_visible_headings(self):
        page = smoke.parse_page(smoke.Response(200, {}, '<html lang="uk"><h1>&#1057;&#1090;&#1086;&#1088;&#1110;&#1085;&#1082;&#1091; &#1085;&#1077; &#1079;&#1085;&#1072;&#1081;&#1076;&#1077;&#1085;&#1086;</h1></html>'))
        self.assertEqual(page.lang, "uk")
        self.assertEqual(page.headings, [("h1", smoke.NOT_FOUND_HEADINGS["uk"])])

    def test_redirect_handler_never_returns_a_redirect_request(self):
        self.assertIsNone(smoke.NoRedirectHandler().redirect_request(None, None, 302, "Found", {}, "https://other.invalid/"))

    def test_complete_synthetic_deployment_check_passes_without_logging_credentials(self):
        secret_user = "synthetic-user"
        secret_password = "Synthetic-Password-123!"

        def fake_request(path, authenticated=True, cookie=None, credentials=None):
            culture = "uk"
            if cookie:
                culture = next((lang for lang in ("uk", "en", "ru") if f"c={lang}" in cookie), "uk")
            headers = {"X-Robots-Tag": "noindex, nofollow"}
            if not authenticated or credentials is not None:
                return smoke.Response(401, headers, "")
            if path == "/" and authenticated:
                return smoke.Response(200, {**headers, "Content-Type": "text/html"}, '<html lang="uk"><link rel="stylesheet" href="/app.css"><h1>Home</h1></html>')
            if path.startswith("/set-language?"):
                query = path.split("?", 1)[1]
                selected = query.split("culture=", 1)[1][:2]
                return smoke.Response(302, {**headers, "Location": "/", "Set-Cookie": f".AspNetCore.Culture=c={selected}|uic={selected}; path=/; secure"}, "")
            if path.startswith("/app.css"):
                return smoke.Response(200 if authenticated else 401,
                                     {**headers, "Content-Type": "text/css"} if authenticated else headers,
                                     "body{}" if authenticated else "")
            if path.startswith("/countries/") and path.endswith("/categories/employment"):
                title = smoke.ARTICLE_TITLES[culture]
                sources = "".join(f'<a href="https://source{i}.gov.example">Source {i}</a>' for i in range(5))
                return smoke.Response(200, {**headers, "Content-Type": "text/html"},
                                      f'<html lang="{culture}"><h1>Employment</h1><article><h2>{title}</h2><p>Workindenmark and Jobnet.</p>{sources}</article></html>')
            if path.startswith("/countries/"):
                return smoke.Response(200, {**headers, "Content-Type": "text/html"}, f'<html lang="{culture}"><h1>Country</h1></html>')
            if path.startswith("/search?"):
                return smoke.Response(200, {**headers, "Content-Type": "text/html"},
                                      f'<html lang="{culture}"><h1>Search</h1><form><input name="q"><select name="country"></select><select name="category"></select><select name="status"></select></form><h2>{smoke.ARTICLE_TITLES[culture]}</h2></html>')
            if path.startswith("/compare?"):
                return smoke.Response(200, {**headers, "Content-Type": "text/html"},
                                      f'<html lang="{culture}"><h1>Compare</h1><h2>Denmark</h2><h2>Germany</h2></html>')
            if path == "/not-found":
                return smoke.Response(200, {**headers, "Content-Type": "text/html"}, f'<html lang="{culture}"><h1>{smoke.NOT_FOUND_HEADINGS[culture]}</h1></html>')
            if path == "/deployment-qa-unknown-route":
                return smoke.Response(404, {**headers, "Content-Type": "text/html"}, f'<html lang="{culture}"><h1>{smoke.NOT_FOUND_HEADINGS[culture]}</h1></html>')
            return smoke.Response(200, {**headers, "Content-Type": "text/html"}, '<html lang="uk"><h1>Page</h1></html>')

        output = io.StringIO()
        with contextlib.redirect_stdout(output):
            passed = smoke.run_checks("https://alpha.onrender.com", secret_user, secret_password,
                                      request=fake_request, sleep=lambda _: None)
        self.assertTrue(passed)
        self.assertIn("Deployment verification:", output.getvalue())
        self.assertIn("PASS | Localized unknown route (ru): 404 content", output.getvalue())
        self.assertNotIn(secret_user, output.getvalue())
        self.assertNotIn(secret_password, output.getvalue())
        self.assertNotIn("Authorization", output.getvalue())

    def test_failed_required_authentication_assertion_returns_failure(self):
        def failing_request(path, authenticated=True, cookie=None, credentials=None):
            headers = {"X-Robots-Tag": "noindex, nofollow"}
            if path == "/" and not authenticated:
                return smoke.Response(200, headers, "unexpected private content")
            return smoke.Response(404, headers, "")
        with contextlib.redirect_stdout(io.StringIO()):
            self.assertFalse(smoke.run_checks("https://alpha.onrender.com", "user", "secret",
                                               request=failing_request, sleep=lambda _: None, readiness_attempts=1))


if __name__ == "__main__":
    unittest.main()
