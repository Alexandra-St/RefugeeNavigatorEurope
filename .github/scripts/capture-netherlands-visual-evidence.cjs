const assert = require('node:assert/strict');
const fs = require('node:fs');
const path = require('node:path');
const { chromium } = require(process.env.PLAYWRIGHT_PACKAGE);

const baseUrl = process.env.VISUAL_BASE_URL;
const outputDirectory = process.env.VISUAL_OUTPUT_DIRECTORY;
const manifest = JSON.parse(fs.readFileSync(process.env.VISUAL_FIXTURE_MANIFEST, 'utf8'));
const languages = ['en', 'uk', 'ru'];
const viewports = [
  { width: 1280, height: 720 },
  { width: 390, height: 844 },
  { width: 320, height: 700 },
];
const unavailableText = {
  en: 'Not established in the reviewed sources.',
  uk: 'Не встановлено за перевіреними джерелами.',
  ru: 'Не установлено по проверенным источникам.',
};

function pngSize(buffer) {
  assert.equal(buffer.subarray(0, 8).toString('hex'), '89504e470d0a1a0a', 'Screenshot is not a PNG');
  return { width: buffer.readUInt32BE(16), height: buffer.readUInt32BE(20) };
}

async function assertLanguage(page, language) {
  const actual = await page.locator('html').getAttribute('lang');
  assert.equal(actual, language, `Expected document language ${language}, got ${actual}`);
}

async function assertNoOverflow(page, viewport, routeName) {
  const widths = await page.evaluate(() => ({
    document: document.documentElement.scrollWidth,
    body: document.body.scrollWidth,
    viewport: window.innerWidth,
  }));
  assert.ok(widths.document <= viewport.width && widths.body <= viewport.width,
    `${routeName} overflows at ${viewport.width}px: ${JSON.stringify(widths)}`);
}

async function saveFullPage(page, name, viewport) {
  await page.evaluate(() => document.fonts.ready);
  const filePath = path.join(outputDirectory, name);
  const bytes = await page.screenshot({ path: filePath, fullPage: true, type: 'png' });
  assert.ok(bytes.length > 10_000, `${name} is unexpectedly small (${bytes.length} bytes)`);
  const dimensions = pngSize(bytes);
  assert.equal(dimensions.width, viewport.width, `${name} has the wrong viewport width`);
  assert.ok(dimensions.height >= viewport.height, `${name} is not a full-page capture`);
  return { file: name, bytes: bytes.length, png: dimensions, viewport: `${viewport.width}x${viewport.height}` };
}

async function main() {
  fs.mkdirSync(outputDirectory, { recursive: true });
  const browser = await chromium.launch({ headless: true });
  const records = [];
  try {
    for (const language of languages) {
      const context = await browser.newContext({ extraHTTPHeaders: { 'Accept-Language': language } });
      try {
        const page = await context.newPage();
        page.setDefaultTimeout(20_000);
        const categoryResponse = await page.goto(`${baseUrl}/countries/netherlands/categories/temporary-protection`, { waitUntil: 'networkidle' });
        assert.ok(categoryResponse?.ok(), `Category page returned ${categoryResponse?.status()}`);
        await assertLanguage(page, language);
        const article = page.locator(`#entry-${manifest.reviewedArticleId.replaceAll('-', '')}`);
        assert.equal(await page.locator('.information-entry').count(), 1, 'Only the verified NL-TP-02 sample should render in the category');
        assert.equal(await article.count(), 1, 'The verified NL-TP-02 sample should be the visible category article');
        assert.match(await article.locator('h2').innerText(), /Netherlands|Нідерланд|Нидерланд/,
          'The reviewed sample article title is missing');
        assert.ok((await article.innerText()).includes('synthetic') || (await article.innerText()).includes('синтетич'),
          'Synthetic reviewed article content did not render');
        assert.equal(await page.getByText('HELD-ARTICLE-TEXT-SENTINEL', { exact: false }).count(), 0,
          'Held NL-TP-01 text leaked into the category');
        assert.equal(await page.locator(`#entry-${manifest.heldArticleId.replaceAll('-', '')}`).count(), 0,
          'Held NL-TP-01 anchor leaked into the category');

        for (const viewport of viewports) {
          await page.setViewportSize(viewport);
          await assertNoOverflow(page, viewport, 'Core Category');
          records.push(await saveFullPage(page, `core-category-${language}-${viewport.width}x${viewport.height}.png`, viewport));
        }

        const compareResponse = await page.goto(`${baseUrl}/compare?country1=netherlands&country2=denmark&category=all`, { waitUntil: 'networkidle' });
        assert.ok(compareResponse?.ok(), `Comparison page returned ${compareResponse?.status()}`);
        await assertLanguage(page, language);
        assert.ok(await page.locator('.structured-comparison').count(), 'Structured comparison did not render in Production');
        for (const aspectId of manifest.approvedAspectIds) {
          const aspect = page.locator(`#${manifest.aspectDomIds[aspectId]}`);
          assert.equal(await aspect.count(), 1, `Approved aspect ${aspectId} is missing`);
          const netherlandsAnswer = aspect.locator('.structured-country').first();
          assert.ok((await netherlandsAnswer.innerText()).includes('SYNTHETIC-CI-FIXTURE'),
            `Approved Netherlands answer ${aspectId} did not render`);
          assert.ok(await netherlandsAnswer.locator('.fact-sources a[href^="http"]').count() > 0,
            `Approved Netherlands answer ${aspectId} has no source link`);
        }
        for (const aspectId of manifest.draftAspectIds) {
          const aspect = page.locator(`#${manifest.aspectDomIds[aspectId]}`);
          const netherlandsAnswer = aspect.locator('.structured-country').first();
          assert.ok((await netherlandsAnswer.innerText()).includes(unavailableText[language]),
            `Draft Netherlands answer ${aspectId} did not use the unavailable placeholder`);
          assert.equal(await netherlandsAnswer.locator('.fact-sources a').count(), 0,
            `Draft Netherlands source links leaked for ${aspectId}`);
        }
        const filing = page.locator(`#${manifest.aspectDomIds[manifest.unavailableAspectId]} .structured-country`).first();
        assert.ok((await filing.innerText()).includes(unavailableText[language]), 'residence-filing must remain unavailable');
        const comparisonHtml = await page.locator('main').innerHTML();
        assert.ok(!comparisonHtml.includes('DRAFT-ONLY-CONTENT-SENTINEL'), 'Draft answer text leaked into Production HTML');
        assert.ok(!comparisonHtml.includes('DRAFT-ONLY-SOURCE-SENTINEL'), 'Draft-only source title leaked into Production HTML');

        for (const viewport of viewports) {
          await page.setViewportSize(viewport);
          await assertNoOverflow(page, viewport, 'Comparison V2');
          records.push(await saveFullPage(page, `comparison-v2-${language}-${viewport.width}x${viewport.height}.png`, viewport));
        }
      } finally {
        await context.close();
      }
    }
  } finally {
    await browser.close();
  }

  assert.equal(records.length, 18, `Expected 18 screenshots, got ${records.length}`);
  const report = [
    '# Netherlands CI visual evidence',
    '',
    `- Workflow run: ${process.env.GITHUB_RUN_URL || 'local execution'}`,
    `- Commit: ${process.env.GITHUB_SHA || 'local working tree'}`,
    '- Environment: local ASP.NET Core Production process with an isolated synthetic catalog fixture; no hosted service was accessed.',
    '- Browser: headless Chromium through Playwright.',
    '- Pages: Netherlands Temporary Protection category and Country Comparison V2 (Netherlands / Denmark, all categories).',
    '- Languages: EN, UK, RU.',
    '- Viewports: 1280×720, 390×844, 320×700.',
    `- Captured PNG files: ${records.length}.`,
    '- Assertions: document language, held article absence, approved fact visibility and source links, Draft/unavailable content and source suppression, no horizontal overflow, valid full-page PNG dimensions.',
    '- Visual gate: REVIEW PENDING. Automated capture and visibility assertions do not constitute human visual acceptance.',
    '',
    '## Captures',
    '',
    '| File | Viewport | PNG dimensions | Bytes |',
    '| --- | --- | --- | ---: |',
    ...records.map(item => `| ${item.file} | ${item.viewport} | ${item.png.width}×${item.png.height} | ${item.bytes} |`),
    '',
  ].join('\n');
  fs.writeFileSync(path.join(outputDirectory, 'CI_VISUAL_EVIDENCE.md'), report, 'utf8');
  console.log(`Captured ${records.length} full-page PNG screenshots with all assertions passing.`);
}

main().catch(error => {
  console.error(error);
  process.exitCode = 1;
});
