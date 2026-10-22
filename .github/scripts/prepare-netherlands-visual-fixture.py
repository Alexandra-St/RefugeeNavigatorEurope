#!/usr/bin/env python3
"""Create an isolated, synthetic Netherlands publication-gate fixture."""

import json
import sys
from pathlib import Path


APPROVED_ASPECTS = {
    "residence-routes",
    "residence-conditions",
    "employment-vacancies",
    "employment-institutions",
    "employment-assistance-conditions",
    "work-permission",
    "work-employer-duties",
    "work-minimum-pay",
    "healthcare-access",
    "healthcare-proof",
    "healthcare-urgent",
    "housing-initial",
    "housing-private-rental",
    "housing-financial-support",
    "education-school-enrolment",
    "education-pupil-language",
    "education-higher-admission",
    "social-subsistence",
    "social-application",
    "social-child-benefits",
}
LANGUAGES = ("en", "uk", "ru")
SYNTHETIC_CORE = {
    "en": (
        "Netherlands overview — synthetic CI example",
        "Synthetic CI example for layout and production-visibility checks.",
        "## Synthetic preview only\n\nThis sample confirms that a reviewed article can be displayed in the Production rendering path. Contact the responsible service to check the current process for your circumstances; local steps can vary. This deliberately breakable text exercises paragraph wrapping at narrow viewport widths.\n\n- Example document one\n- Example document two",
    ),
    "uk": (
        "Огляд Нідерландів — синтетичний приклад CI",
        "Синтетичний приклад CI для перевірки верстки та Production-відображення.",
        "## Лише синтетичний приклад\n\nЦей зразок перевіряє, що попередньо переглянуту статтю можна показати у Production. Уточніть чинну процедуру у відповідної служби з огляду на вашу ситуацію; місцеві кроки можуть відрізнятися. Текст навмисно містить довгі звичайні фрази для перевірки перенесення рядків на вузькому екрані.\n\n- Приклад документа один\n- Приклад документа два",
    ),
    "ru": (
        "Обзор Нидерландов — синтетический пример CI",
        "Синтетический пример CI для проверки вёрстки и Production-отображения.",
        "## Только синтетический пример\n\nЭтот образец проверяет, что предварительно просмотренную статью можно показать в Production. Уточните действующую процедуру в компетентной службе с учётом вашей ситуации; местные шаги могут различаться. В тексте намеренно использованы длинные обычные фразы для проверки переноса строк на узком экране.\n\n- Пример документа один\n- Пример документа два",
    ),
}
SYNTHETIC_FACT = {
    "en": "SYNTHETIC-CI-FIXTURE — Contact the responsible service to confirm the current process for your circumstances; local steps and individual outcomes can vary. This representative text is intentionally long enough to test ordinary wrapping on narrow screens.",
    "uk": "SYNTHETIC-CI-FIXTURE — зверніться до відповідної служби, щоб уточнити чинну процедуру для вашої ситуації; місцеві кроки та індивідуальні результати можуть відрізнятися. Цей репрезентативний текст навмисно достатньо довгий для перевірки перенесення рядків на вузькому екрані.",
    "ru": "SYNTHETIC-CI-FIXTURE — обратитесь в компетентную службу, чтобы уточнить действующую процедуру для вашей ситуации; местные шаги и индивидуальные результаты могут различаться. Этот образец намеренно достаточно длинный для проверки переноса строк на узком экране.",
}
SYNTHETIC_INLINE_SOURCE_LABEL = {
    "en": "synthetic official-source example",
    "uk": "синтетичний приклад офіційного джерела",
    "ru": "синтетический пример официального источника",
}


def read_json(path: Path):
    return json.loads(path.read_text(encoding="utf-8"))


def write_json(path: Path, value):
    path.write_text(json.dumps(value, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


def main():
    if len(sys.argv) != 3:
        raise SystemExit("Usage: prepare-netherlands-visual-fixture.py <Data-directory> <manifest-path>")

    data = Path(sys.argv[1])
    manifest_path = Path(sys.argv[2])
    countries = read_json(data / "countries.json")
    country = next(item for item in countries if item["Slug"] == "netherlands")
    entries = read_json(data / "information-entries.json")
    comparison = read_json(data / "comparison-facts.json")
    sources = read_json(data / "official-sources.json")

    nl_entries = [item for item in entries if item["CountryId"] == country["Id"]]
    if len(nl_entries) != 8:
        raise ValueError(f"Expected 8 Netherlands entries, found {len(nl_entries)}")

    nl_facts = {}
    for topic in comparison["Topics"]:
        for aspect in topic["Aspects"]:
            fact = next(item for item in aspect["Facts"] if item["CountrySlug"] == "netherlands")
            nl_facts[aspect["Id"]] = (topic, aspect, fact)
    if len(nl_facts) != 24:
        raise ValueError(f"Expected 24 Netherlands facts, found {len(nl_facts)}")
    if not APPROVED_ASPECTS.issubset(nl_facts) or len(APPROVED_ASPECTS) != 20:
        raise ValueError("The expected 20 approved comparison aspect IDs do not match the catalog")

    held_entry_id = nl_facts["protection-scheme"][2]["InformationEntryId"]
    tp02_entry_id = nl_facts["residence-routes"][2]["InformationEntryId"]
    entry_ids = {item["Id"] for item in nl_entries}
    if held_entry_id not in entry_ids or tp02_entry_id not in entry_ids or held_entry_id == tp02_entry_id:
        raise ValueError("Could not resolve distinct held NL-TP-01 and reviewed NL-TP-02 entries")

    for entry in nl_entries:
        is_held = entry["Id"] == held_entry_id
        if len(entry["Translations"]) != 3 or {item["LanguageCode"] for item in entry["Translations"]} != set(LANGUAGES):
            raise ValueError(f"Netherlands entry {entry['Id']} does not have exactly EN/UK/RU translations")
        for translation in entry["Translations"]:
            language = translation["LanguageCode"]
            if is_held:
                translation["VerificationStatus"] = 0
                translation["Title"] = "HELD-ARTICLE-TEXT-SENTINEL"
                translation["Summary"] = "HELD-ARTICLE-TEXT-SENTINEL"
                translation["Content"] = "HELD-ARTICLE-TEXT-SENTINEL"
            else:
                translation["VerificationStatus"] = 1
                title, summary, content = SYNTHETIC_CORE[language]
                if entry["Id"] == tp02_entry_id:
                    source = next(
                        (item for item in sources
                         if item["InformationEntryId"] == tp02_entry_id
                         and item["Url"].lower().startswith("https://")),
                        None,
                    )
                    if source is None:
                        raise ValueError("Could not find an HTTPS source for the synthetic inline-link example")
                    content += (
                        "\n\nSynthetic inline link example: "
                        f"[[source:{source['Id']}|{SYNTHETIC_INLINE_SOURCE_LABEL[language]}]]."
                    )
                translation["Title"] = title
                translation["Summary"] = summary
                translation["Content"] = content

    draft_source_ids = set()
    approved_source_ids = set()
    for aspect_id, (_, _, fact) in nl_facts.items():
        if aspect_id in APPROVED_ASPECTS:
            fact["Status"] = "Approved"
            approved_source_ids.update(fact["OfficialSourceIds"])
            for language in LANGUAGES:
                fact["Values"][language] = SYNTHETIC_FACT[language]
        else:
            fact["Status"] = "Draft"
            draft_source_ids.update(fact["OfficialSourceIds"])
            for language in LANGUAGES:
                fact["Values"][language] = f"DRAFT-ONLY-CONTENT-SENTINEL-{aspect_id}"
    unavailable = nl_facts["residence-filing"][2]
    unavailable["IsAvailable"] = False
    unavailable["OfficialSourceIds"] = []
    unavailable["Values"] = {
        "en": "Not established in the reviewed sources.",
        "uk": "Не встановлено за перевіреними джерелами.",
        "ru": "Не установлено по проверенным источникам.",
    }
    draft_only_source_ids = draft_source_ids - approved_source_ids
    source_by_id = {item["Id"]: item for item in sources}
    for source_id in draft_only_source_ids:
        source_by_id[source_id]["Title"] = "DRAFT-ONLY-SOURCE-SENTINEL"

    write_json(data / "information-entries.json", entries)
    write_json(data / "comparison-facts.json", comparison)
    write_json(data / "official-sources.json", sources)

    aspect_dom_ids = {}
    for aspect_id, (topic, _, _) in nl_facts.items():
        aspect_dom_ids[aspect_id] = (
            f"structured-aspect-{topic['CategorySlug']}-"
            f"{topic['TopicResourceKey'].replace('.', '-')}-{aspect_id}"
        )
    manifest = {
        "heldArticleId": held_entry_id,
        "reviewedArticleId": tp02_entry_id,
        "approvedAspectIds": sorted(APPROVED_ASPECTS),
        "draftAspectIds": sorted(set(nl_facts) - APPROVED_ASPECTS),
        "unavailableAspectId": "residence-filing",
        "aspectDomIds": aspect_dom_ids,
    }
    manifest_path.write_text(json.dumps(manifest, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    print("Prepared isolated synthetic Netherlands fixture: 8 entries, 24 translations, 20 Approved facts, 4 Draft facts (including unavailable residence-filing).")


if __name__ == "__main__":
    main()
