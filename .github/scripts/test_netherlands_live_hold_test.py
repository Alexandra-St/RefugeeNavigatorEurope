import contextlib
import importlib.util
import io
from pathlib import Path
import sys
import unittest
from urllib.parse import parse_qs, urlsplit

SCRIPT = Path(__file__).with_name("netherlands_live_hold_test.py")
spec = importlib.util.spec_from_file_location("netherlands_live_hold_test", SCRIPT)
hold_test = importlib.util.module_from_spec(spec)
sys.modules[spec.name] = hold_test
spec.loader.exec_module(hold_test)


def article(entry_id, title, summary="", body="", sources=False):
    source_list = '<details class="source-details"><a href="https://held.example/source">Held source</a></details>' if sources else ""
    return (f'<article class="information-entry" id="entry-{entry_id.replace("-", "")}">'
            f'<h2>{title}</h2><p class="entry-summary">{summary}</p><p>{body}</p>{source_list}</article>')


def fact_aspect(key, language, *, marker=None, source=False):
    text = marker or hold_test.UNAVAILABLE_TEXT[language]
    source_group = '<details class="fact-sources"><a href="https://source.example/approved">Official source</a></details>' if source else ""
    return (f'<section class="structured-aspect" id="structured-aspect-temporary-protection-topic-{key}">'
            f'<article class="structured-country"><h5>Netherlands</h5><p>{text}</p>{source_group}</article>'
            '<article class="structured-country"><h5>Denmark</h5><p>Other country</p></article></section>')


class FakeService:
    def __init__(self, leak_held_article=False, leak_draft_fact=False):
        self.leak_held_article = leak_held_article
        self.leak_draft_fact = leak_draft_fact

    def __call__(self, path, cookie=None):
        parsed = urlsplit(path)
        route = parsed.path
        query = parse_qs(parsed.query)
        language = "en"
        if cookie:
            language = next((item for item in hold_test.LANGUAGES if f"c={item}" in cookie), "en")
        headers = {"X-Robots-Tag": "noindex, nofollow"}
        if route == "/set-language":
            selected = query.get("culture", ["en"])[0]
            return hold_test.Response(302, {**headers, "Location": "/", "Set-Cookie": f".AspNetCore.Culture=c={selected}|uic={selected}; path=/; secure"}, "")
        if route == "/countries/netherlands":
            return hold_test.Response(200, headers, '<html><h1>Netherlands</h1></html>')
        if route.startswith("/countries/netherlands/categories/"):
            content = article(hold_test.ACCEPTED_ENTRY_ID, hold_test.ACCEPTED_TITLES[language])
            if self.leak_held_article:
                content += article(hold_test.HELD_ENTRY_ID, hold_test.HELD_TITLES[language],
                                   hold_test.HELD_SUMMARIES[language], hold_test.HELD_BODY_MARKERS[language], sources=True)
            return hold_test.Response(200, headers, f"<html>{content}</html>")
        if route == "/search":
            value = query.get("q", [""])[0]
            title = ""
            if value == hold_test.ACCEPTED_TITLES[language]:
                title = value
            elif self.leak_held_article and value == hold_test.HELD_TITLES[language]:
                title = value
            return hold_test.Response(200, headers, f"<html><h2>{title}</h2></html>")
        if route == "/compare":
            sections = []
            for key in hold_test.HELD_FACT_MARKERS:
                marker = hold_test.HELD_FACT_MARKERS[key][language] if self.leak_draft_fact else None
                sections.append(fact_aspect(key, language, marker=marker, source=self.leak_draft_fact))
            sections.append(fact_aspect("residence-filing", language))
            sections.append(fact_aspect("residence-routes", language,
                                        marker=hold_test.ACCEPTED_FACT_MARKER[language], source=True))
            return hold_test.Response(200, headers, "<html>" + "".join(sections) + "</html>")
        return hold_test.Response(404, headers, "")


class NetherlandsLiveHoldTests(unittest.TestCase):
    def test_parser_extracts_held_article_and_specific_comparison_panel(self):
        body = article(hold_test.HELD_ENTRY_ID, hold_test.HELD_TITLES["en"], "summary", "body", sources=True)
        body += fact_aspect("protection-scheme", "en")
        root = hold_test.parse_document(body)
        self.assertIsNotNone(hold_test.find_article(root, hold_test.HELD_ENTRY_ID))
        panel = hold_test.fact_panel(root, "protection-scheme")
        self.assertIsNotNone(panel)
        self.assertEqual(len(list(panel.descendants("details", "fact-sources"))), 0)

    def test_synthetic_live_hold_verification_passes_without_logging_credentials(self):
        username, password = "synthetic-user", "Synthetic-Secret-098!"
        output = io.StringIO()
        with contextlib.redirect_stdout(output):
            passed = hold_test.run_checks("https://alpha.onrender.com", username, password,
                                          send=FakeService(), sleep=lambda _: None)
        self.assertTrue(passed)
        self.assertIn("Netherlands live hold verification:", output.getvalue())
        self.assertIn("PASS | EN Draft fact protection-scheme", output.getvalue())
        self.assertIn("PASS | RU Approved positive control residence-routes", output.getvalue())
        self.assertNotIn(username, output.getvalue())
        self.assertNotIn(password, output.getvalue())
        self.assertNotIn("Authorization", output.getvalue())

    def test_held_article_leak_fails_the_verifier(self):
        output = io.StringIO()
        with contextlib.redirect_stdout(output):
            passed = hold_test.run_checks("https://alpha.onrender.com", "user", "secret",
                                          send=FakeService(leak_held_article=True), sleep=lambda _: None)
        self.assertFalse(passed)
        self.assertIn("held article element present", output.getvalue())

    def test_draft_answer_and_source_group_leak_fails_the_verifier(self):
        output = io.StringIO()
        with contextlib.redirect_stdout(output):
            passed = hold_test.run_checks("https://alpha.onrender.com", "user", "secret",
                                          send=FakeService(leak_draft_fact=True), sleep=lambda _: None)
        self.assertFalse(passed)
        self.assertIn("FAIL | EN Draft fact protection-scheme", output.getvalue())


if __name__ == "__main__":
    unittest.main()
