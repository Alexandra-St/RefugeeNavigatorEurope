# Lithuania v1 Editorial Acceptance Preparation

**Baseline:** `912f80dcf7ef4f622379f0d0e093c8354d168c69`
**Review date:** 2026-11-09
**Overall verdict:** **NEEDS CORRECTION BEFORE HUMAN ACCEPTANCE**

This report prepares the integrated Draft / NeedsReview batch for human review. It is not owner acceptance and does not promote status. No new external research was performed.

**Scope:** Eight accepted Lithuania drafts and their eight research packages; integrated country/articles/translations; all 71 owned sources; 24 comparison facts; topic mapping, localization and visibility; existing Netherlands and Sweden holds.

## 1. Batch snapshot

| Lithuania catalog item | Count / state |
|---|---:|
| Countries | 1 |
| Articles | 8 |
| Translations | 24; all `NeedsReview` |
| `OfficialSources` | 71; 71 unique article-owned UUIDs |
| Comparison facts | 24; all `Draft`, all `IsAvailable=true` |
| Canonical aspects | 24/24 exactly once |
| Development visibility | Visible |
| Production visibility | Hidden, expected while all translations need review |

| Global catalog item | Recounted state |
|---|---:|
| Countries | 13 |
| Articles | 104 |
| Translations | 312: 285 `Verified`, 27 `NeedsReview`, 0 `Outdated` |
| `OfficialSources` | 864 |
| Comparison facts | 312: 283 `Approved`, 29 `Draft` |
| `IsAvailable=true` / `false` | 305 / 7 |

The counts were recomputed from the checked-in catalogs. The integration tests confirm Development visibility at 13 countries and Production visibility at 12.

## 2. Article acceptance matrix

| Article | Article UUID | Category | Sources | Draft ↔ catalog EN / UK / RU parity | Claim support | Editorial result |
|---|---|---|---:|---|---|---|
| LT-TP-01 | `115cbf25-5ece-5cc9-8bde-c3dabcbda47f` | `temporary-protection` | 9 | Exact / Exact / Exact | Supported; accepted claim boundaries preserved | Ready |
| LT-TP-02 | `581150e2-3862-5a79-9e56-ad1f3fdd8424` | `temporary-protection` | 8 | Exact / Exact / Exact | Supported; accepted claim boundaries preserved | Ready |
| LT-EMP-01 | `26051216-75b3-5564-a2ec-9a0723bcfec6` | `employment` | 8 | Exact / Exact / Exact | Supported; accepted claim boundaries preserved | Ready |
| LT-EMP-02 | `5cdd5634-6d7a-5c02-aa2a-02f4758e6d2d` | `employment` | 8 | Exact / Exact / Exact | Supported; accepted claim boundaries preserved | Ready |
| LT-HEALTH-01 | `42863927-2979-5769-8092-c9393a0ea916` | `healthcare` | 8 | Exact / Exact / Exact | Supported; accepted claim boundaries preserved | Ready |
| LT-HOUSING-01 | `28169e82-6320-5f00-9b34-37e77f3c7d26` | `housing` | 8 | Exact / Exact / Exact | Supported; accepted claim boundaries preserved | Ready |
| LT-EDU-01 | `bacfcf91-7365-5c28-a240-b6db41c1aa0f` | `education` | 11 | Exact / Exact / Exact | Supported; accepted claim boundaries preserved | Ready |
| LT-SOC-01 | `c971e527-d7ca-5dff-b739-02e6df1f98e1` | `social-assistance` | 11 | Exact / Exact / Exact | Supported; accepted claim boundaries preserved | Ready |

**Article acceptance:** Ready 8/8 · Minor correction 0 · Correction required 0.

For all 24 translations, Title, Summary and Content match the corresponding EN/UK/RU draft exactly. Metadata is `NeedsReview` and `LastUpdatedAt=2026-11-09T00:00:00Z` for all 24. All 222 inline source tokens resolve within their own article.

## 3. Cross-language article review

EN/UK/RU article parity is exact against the integrated catalog and accepted draft versions. The post-correction review resolves LT-REV-01 and LT-REV-02 in LT-TP-01 and records 8/8 articles OK. Source re-keying changed IDs only; accepted boundaries remain intact:

- **LT-TP-01:** both pre-24-Feb-2022 third-country cohorts and statutory family scope; Lithuanian continuous-TP exception in any EU Member State versus the EU same-Member-State wording; scheme date and individual-document caveats.
- **LT-TP-02:** no automatic TP conversion; TP-only long-term-residence time, TP effect after another permit, and TP-only family sponsorship remain unresolved; filing examples stay route-specific.
- **LT-EMP-01:** vacancy browsing is separate from UŽT registration; assistance is conditional; Sodra insurance is separate; the Ukraine course intake is closed as of the review date.
- **LT-EMP-02:** work follows a qualifying TP application without waiting for the physical document; unrelated applications are excluded; employer-reporting edge cases remain qualified; gross pay amounts and 2027 effective date match the draft.
- **LT-HEALTH-01:** TP emergency/necessary care is not full CHI; a TP document is not proof of active CHI; 112/113 and fee boundaries remain separate.
- **LT-HOUSING-01:** no placement guarantee or invented deposit cap; lease, address declaration and separate support routes remain distinct.
- **LT-EDU-01:** preferred school and fixed language hours are not guaranteed; Ukrainian online schooling remains “not established as a substitute”; dated/closed support remains qualified.
- **LT-SOC-01:** TP/application does not guarantee payment; means tests, municipal decision, SPIS channel and Sodra roles remain distinct in the article text.

## 4. Article-specific findings

No unresolved article-specific finding. The integrated drafts match accepted text exactly. The findings below concern comparison facts, not article translations.

## 5. Comparison acceptance matrix

Each fact is `Draft` / available and linked to its mapped Lithuania article. Exact integrated EN/UK/RU values are reproduced below. All facts require the shared cleanup in LT-C01; LT-C02 and LT-C03 add aspect/content corrections.

| Aspect | Linked article UUID | State | Source ownership/relevance and language parity | Classification |
|---|---|---|---|---|
| `protection-scheme` | `115cbf25-5ece-5cc9-8bde-c3dabcbda47f` | `Draft` / available | Owned sources resolve; Cohort conditions and family scope are present; accepted boundaries retained. | Correction required |
| `protection-application` | `115cbf25-5ece-5cc9-8bde-c3dabcbda47f` | `Draft` / available | Owned sources resolve; Authority/document distinction and missing first-application checklist are accurately qualified. | Correction required |
| `protection-dates` | `115cbf25-5ece-5cc9-8bde-c3dabcbda47f` | `Draft` / available | Owned sources resolve; 4 March 2028 and national any-EU / EU same-Member-State exceptions are distinguished. | Correction required |
| `residence-routes` | `581150e2-3862-5a79-9e56-ad1f3fdd8424` | `Draft` / available | Owned sources resolve; Ordinary routes remain application-based; no automatic conversion claimed. | Correction required |
| `residence-conditions` | `581150e2-3862-5a79-9e56-ad1f3fdd8424` | `Draft` / available | Owned sources resolve; TP-only time and effect of another permit / TP-only family sponsorship remain not established. | Correction required |
| `residence-filing` | `581150e2-3862-5a79-9e56-ad1f3fdd8424` | `Draft` / available | Owned sources resolve; The 1 November 2026 filing examples are limited; no universal in-country right stated. | Correction required |
| `employment-vacancies` | `26051216-75b3-5564-a2ec-9a0723bcfec6` | `Draft` / available | Owned sources resolve; UŽT and EURES are identified without claiming completeness; browsing is separate from registration. | Correction required |
| `employment-institutions` | `26051216-75b3-5564-a2ec-9a0723bcfec6` | `Draft` / available | Owned sources resolve; UŽT registration and conditional counseling/training access are covered. | Correction required |
| `employment-assistance-conditions` | `26051216-75b3-5564-a2ec-9a0723bcfec6` | `Draft` / available | Owned sources resolve; Sodra unemployment insurance is distinct; conditions and closed programme intake remain qualified. | Correction required |
| `work-permission` | `5cdd5634-6d7a-5c02-aa2a-02f4758e6d2d` | `Draft` / available | Owned sources resolve; Qualifying TP application is the trigger; physical document is not required; unrelated applications excluded. | Correction required |
| `work-employer-duties` | `5cdd5634-6d7a-5c02-aa2a-02f4758e6d2d` | `Draft` / available | Owned sources resolve; Ordinary employer duties remain; reporting edge cases are not generalized. | Correction required |
| `work-minimum-pay` | `5cdd5634-6d7a-5c02-aa2a-02f4758e6d2d` | `Draft` / available | Owned sources resolve; 2026 and 2027 gross amounts/dates match; monthly minimum is bounded to unqualified work. | Correction required |
| `healthcare-access` | `42863927-2979-5769-8092-c9393a0ea916` | `Draft` / available | Owned sources resolve; TP necessary/emergency care is distinguished from full CHI and routine care. | Correction required |
| `healthcare-proof` | `42863927-2979-5769-8092-c9393a0ea916` | `Draft` / available | Owned sources resolve; TP residence document is not active-CHI proof; separate eligibility/record check stated. | Correction required |
| `healthcare-urgent` | `42863927-2979-5769-8092-c9393a0ea916` | `Draft` / available | Owned sources resolve; 112 / 113 distinction and no blanket free-care promise are preserved. | Correction required |
| `housing-initial` | `28169e82-6320-5f00-9b34-37e77f3c7d26` | `Draft` / available | Owned sources resolve; The six-month route is limited to eligible pending applicants; no placement guarantee. | Correction required |
| `housing-private-rental` | `28169e82-6320-5f00-9b34-37e77f3c7d26` | `Draft` / available | Owned sources resolve; Written lease/advance rent is qualified; no general deposit cap invented; declaration is separate. | Correction required |
| `housing-financial-support` | `28169e82-6320-5f00-9b34-37e77f3c7d26` | `Draft` / available | Owned sources resolve; Support routes/local criteria are distinct; application is not award or immediate housing. | Correction required |
| `education-school-enrolment` | `bacfcf91-7365-5c28-a240-b6db41c1aa0f` | `Draft` / available | Owned sources resolve; Individual placement and no preferred-school guarantee are present. | Correction required |
| `education-pupil-language` | `bacfcf91-7365-5c28-a240-b6db41c1aa0f` | `Draft` / available | Owned sources resolve; No fixed hours; eight-school snapshot is dated; online study is not established as substitute. | Correction required |
| `education-higher-admission` | `bacfcf91-7365-5c28-a240-b6db41c1aa0f` | `Draft` / available | Owned sources resolve; Admission, recognition, fees/funding are institution/call-specific; closed call/VSF uncertainty retained. | Correction required |
| `social-subsistence` | `c971e527-d7ca-5dff-b739-02e6df1f98e1` | `Draft` / available | Owned sources resolve; Means testing and no guaranteed payment stated; application detail is better aligned to social-application. | Correction required |
| `social-application` | `c971e527-d7ca-5dff-b739-02e6df1f98e1` | `Draft` / available | Owned sources resolve; Value describes family/child payments and Sodra, not municipal/SPIS application roles; LT-C02. | Correction required |
| `social-child-benefits` | `c971e527-d7ca-5dff-b739-02e6df1f98e1` | `Draft` / available | Owned sources resolve; Value covers Ukraine-specific support but omits general child benefit/supplement distinctions; LT-C03. | Correction required |

**Comparison acceptance:** checked 24/24 · Ready 0 · Minor correction 0 · Correction required 24. LT-C01 affects all facts; two facts also have aspect-specific corrections.

**Fact language parity:** 24/24 are materially equivalent across EN/UK/RU for eligibility, negation, dates, amounts and conditions. The source-label tails are translated labels, not factual differences.

### `protection-scheme`

- **Article UUID:** `115cbf25-5ece-5cc9-8bde-c3dabcbda47f` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `9a75824b-8c6e-5438-9d58-b0a814551034`, `a3325749-2fe0-5a7d-ac14-d925c2bb7c15`, `b05d1e85-2309-5af9-ac16-3dd996a5e2e7`
- **Support / language parity:** Cohort conditions and family scope are present; accepted boundaries retained.

**EN**

Lithuania’s temporary-protection (TP) rules cover defined groups, not every Ukrainian national or former resident. They include Ukrainian citizens who lived in Ukraine before 24 February 2022 and were displaced from that date by Russia’s aggression under the national rules, and their family members only within the specified statutory family scope. They also include two distinct groups: (1) stateless persons and third-country nationals who had international protection or equivalent national protection in Ukraine before 24 February 2022; and (2) stateless persons and third-country nationals who held a valid Ukrainian permanent-residence permit and were lawfully permanently resident in Ukraine before 24 February 2022, and who cannot safely and durably return to their country of origin or another receiving state. Check the exact category and any military-obligation condition; nationality alone does not decide an individual case. Government Resolution No. 224 Foreigners Law EU Decision 2022/382

**UK**

Правила Литви про тимчасовий захист (TP) охоплюють визначені групи, а не кожного громадянина України чи колишнього мешканця країни. До них належать громадяни України, які проживали в Україні до 24 лютого 2022 року та були переміщені з цієї дати внаслідок агресії Росії відповідно до національних правил, а також члени їхніх сімей лише в межах визначеного законом кола. Окремо охоплено дві різні групи: (1) особи без громадянства та громадяни третіх країн, які мали міжнародний захист або рівнозначний національний захист в Україні до 24 лютого 2022 року; та (2) особи без громадянства та громадяни третіх країн, які мали чинний дозвіл на постійне проживання в Україні, законно й постійно проживали там до 24 лютого 2022 року та не можуть безпечно й тривало повернутися до країни походження або іншої держави, що приймає. Перевіряйте точну категорію та застосовну умову щодо військового обов’язку: саме громадянство не визначає індивідуальну справу. Постанова Уряду № 224 Закон про правове становище іноземців Рішення ЄС 2022/382

**RU**

Правила Литвы о временной защите (TP) охватывают определённые группы, а не каждого гражданина Украины или бывшего жителя страны. К ним относятся граждане Украины, проживавшие в Украине до 24 февраля 2022 года и перемещённые с этой даты в результате агрессии России согласно национальным правилам, а также члены их семей только в пределах установленного законом круга. Отдельно предусмотрены две разные группы: (1) лица без гражданства и граждане третьих стран, имевшие международную защиту или эквивалентную национальную защиту в Украине до 24 февраля 2022 года; и (2) лица без гражданства и граждане третьих стран, имевшие действительный украинский вид на постоянное проживание, законно и постоянно проживавшие там до 24 февраля 2022 года и не способные безопасно и устойчиво вернуться в страну происхождения или другое принимающее государство. Проверяйте точную категорию и применимое условие о военной обязанности: одно лишь гражданство не решает индивидуальный вопрос. Постановление Правительства № 224 Закон о правовом положении иностранцев Решение ЕС 2022/382

### `protection-application`

- **Article UUID:** `115cbf25-5ece-5cc9-8bde-c3dabcbda47f` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `72d5717d-6d90-521e-be36-dedf852d5863`, `9a75824b-8c6e-5438-9d58-b0a814551034`, `c734830c-c6ba-59cb-a015-05abcc5149bf`
- **Support / language parity:** Authority/document distinction and missing first-application checklist are accurately qualified.

**EN**

The Migration Department registers eligible people and issues the relevant registration certificate and digital residence document. The document confirms status but is not the temporary-protection scheme itself. Current guidance describes replacement of existing digital permits through MIGRIS; a complete, refreshed nationwide checklist, initial biometrics, fee, appointment or processing time for a first application was not established in the reviewed sources. Confirm current first-application steps with the Migration Department. Government Resolution No. 224 2026 permit-replacement guidance Interior Ministry notice

**UK**

Департамент міграції реєструє осіб, які відповідають умовам, і видає відповідне свідоцтво про реєстрацію та цифровий документ на проживання. Документ підтверджує статус, але не є самою схемою тимчасового захисту. Чинні вказівки описують заміну наявних цифрових посвідок через MIGRIS; у переглянутих джерелах не встановлено повного оновленого загальнонаціонального переліку документів, первинної біометричної процедури, збору, запису чи строку розгляду першої заяви. Уточнюйте чинні кроки первинного подання в Департаменті міграції. Постанова Уряду № 224 Вказівки 2026 року щодо заміни посвідок Повідомлення МВС

**RU**

Департамент миграции регистрирует лиц, отвечающих условиям, и выдаёт соответствующее свидетельство о регистрации и цифровой документ на проживание. Документ подтверждает статус, но не является самой схемой временной защиты. Действующие указания описывают замену имеющихся цифровых разрешений через MIGRIS; в изученных источниках не установлены полный обновлённый общенациональный список документов, первоначальная биометрическая процедура, сбор, запись или срок рассмотрения первого заявления. Уточняйте текущие шаги первичной подачи в Департаменте миграции. Постановление Правительства № 224 Указания 2026 года о замене разрешений Сообщение МВД

### `protection-dates`

- **Article UUID:** `115cbf25-5ece-5cc9-8bde-c3dabcbda47f` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `008e4736-27b8-5634-a85b-0cec942ebbf8`, `3292c5a4-4e15-5322-9840-f74616cd009a`, `3abe9ec8-adb7-526d-974e-b243cac76c81`, `ba657acd-83d2-5368-adf1-510fb3585467`
- **Support / language parity:** 4 March 2028 and national any-EU / EU same-Member-State exceptions are distinguished.

**EN**

Lithuania has implemented the national protection period through 4 March 2028. This scheme date does not establish the expiry printed on every person’s document or a universal automatic reissue process. From 5 August 2026, a military-obligation condition applies where relevant. Under the Lithuanian national transition rule, the exception covers people who had continuous TP in any EU Member State across the 4 August 2026 cutoff; separately, it covers people whose Lithuanian TP application was already pending at that cutoff. The EU-level continuity exception is narrower and refers to continuous protection in the same Member State. Check the current legal wording for an individual case. Resolution No. 742 EU extension decision EU corrigendum Migration Department guidance

**UK**

Литва запровадила національний строк захисту до 4 березня 2028 року. Ця дата не встановлює строк дії документа кожної людини чи універсальну автоматичну заміну. З 5 серпня 2026 року умова щодо військового обов’язку застосовується, якщо вона стосується людини. За національним перехідним правилом Литви виняток поширюється на людей, чий безперервний період TP у будь-якій державі-члені ЄС охоплював граничну дату 4 серпня 2026 року; окремо він поширюється на людей, чиї заяви на TP у Литві вже розглядалися на цю дату. Виняток ЄС щодо безперервності вужчий і стосується безперервного захисту в тій самій державі-члені. Для індивідуальної справи перевіряйте чинне формулювання закону. Постанова № 742 Рішення ЄС про продовження Виправлення ЄС Вказівки Департаменту міграції

**RU**

Литва установила национальный срок защиты до 4 марта 2028 года. Эта дата не определяет срок действия документа каждого человека и не подтверждает универсальную автоматическую замену. С 5 августа 2026 года условие о военной обязанности применяется, если оно относится к человеку. По национальному переходному правилу Литвы исключение распространяется на людей, чей непрерывный период TP в любом государстве-члене ЕС охватывал граничную дату 4 августа 2026 года; отдельно оно распространяется на людей, чьи заявления на TP в Литве уже находились на рассмотрении на эту дату. Исключение ЕС в отношении непрерывности уже и касается непрерывной защиты в том же государстве-члене. Для индивидуального случая проверяйте действующую формулировку закона. Постановление № 742 Решение ЕС о продлении Исправление ЕС Указания Департамента миграции

### `residence-routes`

- **Article UUID:** `581150e2-3862-5a79-9e56-ad1f3fdd8424` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `2b6fe8b6-bb8c-5d71-9416-0d8323cbae40`, `7f4b45f7-5394-5a1d-9b43-ea96489ff47f`
- **Support / language parity:** Ordinary routes remain application-based; no automatic conversion claimed.

**EN**

Lithuania has ordinary residence routes for work, study, family, business and other grounds. Each has its own eligibility conditions and application process. Temporary protection does not automatically convert into a national residence permit, and the reviewed sources did not establish a special, generally available TP-to-permit transition route. Foreigners Law EU transition recommendation

**UK**

У Литві існують звичайні підстави для проживання з метою роботи, навчання, возз’єднання сім’ї, підприємництва та з інших причин. Для кожної діють окремі умови й процедура подання. Тимчасовий захист не переходить автоматично в національний дозвіл на проживання; у переглянутих джерелах не встановлено спеціального загальнодоступного маршруту переходу з TP на дозвіл. Закон про правове становище іноземців Рекомендація ЄС щодо переходу

**RU**

В Литве существуют обычные основания для проживания с целью работы, учёбы, воссоединения семьи, предпринимательства и по другим причинам. Для каждого действуют отдельные условия и процедура подачи. Временная защита автоматически не переходит в национальное разрешение на проживание; в изученных источниках не установлен специальный общедоступный порядок перехода с TP на разрешение. Закон о правовом положении иностранцев Рекомендация ЕС о переходе

### `residence-conditions`

- **Article UUID:** `581150e2-3862-5a79-9e56-ad1f3fdd8424` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `2b6fe8b6-bb8c-5d71-9416-0d8323cbae40`, `50f6da55-be11-586a-8167-7f21b4bead7f`, `abd2a75c-4043-5b1d-bde0-f854d31c9032`, `b613cf04-cfae-59ff-b511-244f47807fa1`
- **Support / language parity:** TP-only time and effect of another permit / TP-only family sponsorship remain not established.

**EN**

Whether time spent only under TP counts toward the five-year long-term-residence calculation is not established in the reviewed Lithuanian guidance. The Department expressly counts some time on a qualifying pending-application registration certificate, but that is a distinct rule. The sources also do not settle how TP is affected when another permit is granted or whether TP alone qualifies someone to sponsor family. Check the exact route and sponsor category before applying. Long-term-residents directive Foreigners Law residence rules Pending-application certificate guidance National TP resolution

**UK**

У переглянутих литовських вказівках не встановлено, чи зараховується період лише під тимчасовим захистом до п’ятирічного строку для довгострокового проживання. Департамент прямо зараховує певний час за свідоцтвом, виданим під час розгляду кваліфікованої заяви, але це окреме правило. Джерела також не визначають, як TP змінюється після видачі іншого дозволу і чи може сам статус TP бути підставою для спонсорства сім’ї. Перед поданням перевіряйте конкретну підставу й категорію спонсора. Директива про довгострокових резидентів Правила проживання в Законі про іноземців Вказівки щодо свідоцтва на час розгляду заяви Національна постанова про TP

**RU**

В изученных литовских указаниях не установлено, засчитывается ли период только под временной защитой в пятилетний срок для долгосрочного проживания. Департамент прямо засчитывает отдельный период по свидетельству, выданному во время рассмотрения подходящего заявления, но это самостоятельное правило. Источники также не определяют, как TP меняется после выдачи другого разрешения и может ли один лишь статус TP служить основанием для спонсорства семьи. Перед подачей проверьте конкретное основание и категорию спонсора. Директива о долгосрочных резидентах Правила проживания в Законе об иностранцах Указания о свидетельстве на время рассмотрения заявления Национальное постановление о TP

### `residence-filing`

- **Article UUID:** `581150e2-3862-5a79-9e56-ad1f3fdd8424` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `3f8561fd-f947-5893-82dc-70dc3a3de269`, `40713c70-a104-5274-b3b5-603429d7f724`, `ab4c13da-bdd1-5ee2-8ff2-a672e093cf26`
- **Support / language parity:** The 1 November 2026 filing examples are limited; no universal in-country right stated.

**EN**

Some in-country applications are permitted only for defined categories. Since 1 November 2026, a legally present person holding a valid Lithuanian temporary residence permit may apply in Lithuania for ordinary employment or first-cycle bachelor studies under the specified rule; highly qualified employment has a separate ordinary process. These provisions do not establish a universal right to file in-country for every permit ground or an automatic approval. Migration Department filing guidance TP employment facilitation Family-reunification guidance

**UK**

Подання заяви в Литві дозволено лише для визначених категорій. З 1 листопада 2026 року особа, яка законно перебуває в країні та має чинний литовський тимчасовий дозвіл на проживання, може подати в Литві заяву на звичайний дозвіл для роботи або навчання на бакалаврському рівні за встановленим правилом; для висококваліфікованої роботи діє окрема звичайна процедура. Це не встановлює загального права подаватися в країні на будь-яку підставу чи гарантії схвалення. Вказівки Департаменту міграції щодо подання Пільга для працевлаштування після TP Вказівки щодо возз’єднання сім’ї

**RU**

Подать заявление в Литве можно только для определённых категорий. С 1 ноября 2026 года законно находящийся в стране человек с действующим литовским временным разрешением на проживание может подать в Литве заявление на обычное разрешение для работы или обучения на бакалавриате по установленному правилу; для высококвалифицированной работы действует отдельная обычная процедура. Это не устанавливает общего права подаваться внутри страны по любому основанию и не гарантирует одобрения. Указания Департамента миграции о подаче Послабление для трудоустройства после TP Указания о воссоединении семьи

### `employment-vacancies`

- **Article UUID:** `26051216-75b3-5564-a2ec-9a0723bcfec6` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `2d60007d-d131-53fc-8921-afb83fb58b06`, `96a25879-f035-5fa1-9a6f-e55f82515ec4`, `d977c0b6-5579-5399-9f0e-32eb9a5121ea`
- **Support / language parity:** UŽT and EURES are identified without claiming completeness; browsing is separate from registration.

**EN**

You can browse public vacancies through the Lithuanian Employment Service (UŽT) without first registering as a jobseeker. UŽT also provides a Ukrainian-language portal, but this does not mean every vacancy or service is translated. EURES offers cross-border job-search and mobility information; compare listings and confirm details with the employer. UŽT vacancy search Ukrainian-language UŽT portal EURES Lithuania

**UK**

Переглядати відкриті вакансії можна через Литовську службу зайнятості (UŽT), не реєструючись попередньо як шукач роботи. UŽT також має україномовний портал, але це не означає, що перекладено кожну вакансію чи послугу. EURES надає транскордонну інформацію про пошук роботи та мобільність; перевіряйте оголошення й деталі безпосередньо в роботодавця. Пошук вакансій UŽT Україномовний портал UŽT EURES у Литві

**RU**

Просматривать открытые вакансии можно через Литовскую службу занятости (UŽT), не регистрируясь заранее как соискатель. У UŽT также есть украиноязычный портал, но это не означает, что переведены все вакансии или услуги. EURES предоставляет трансграничную информацию о поиске работы и мобильности; сверяйте объявления и условия у работодателя. Поиск вакансий UŽT Украиноязычный портал UŽT EURES в Литве

### `employment-institutions`

- **Article UUID:** `26051216-75b3-5564-a2ec-9a0723bcfec6` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `39b5100d-6133-5d73-97c5-a5337c1b3e02`, `6124aeba-ca07-597c-b8b0-64376b741d4b`
- **Support / language parity:** UŽT registration and conditional counseling/training access are covered.

**EN**

Formal UŽT registration, jobseeker status, and access to counselling, training or other measures are separate steps. Registration and services depend on the person’s documents and status; training or support may require an assessment, an individual plan and available places or funding. A Ukrainian-language page or registration does not itself establish eligibility for every service. UŽT registration and documents UŽT status and service guidance

**UK**

Офіційна реєстрація в UŽT, статус шукача роботи та доступ до консультацій, навчання чи інших заходів — окремі етапи. Реєстрація й послуги залежать від документів і статусу людини; навчання чи підтримка можуть потребувати оцінювання, індивідуального плану та наявних місць або фінансування. Україномовна сторінка чи сама реєстрація не підтверджує право на кожну послугу. Реєстрація та документи UŽT Статуси й послуги UŽT

**RU**

Официальная регистрация в UŽT, статус соискателя и доступ к консультациям, обучению или другим мерам — отдельные этапы. Регистрация и услуги зависят от документов и статуса человека; обучение или поддержка могут требовать оценки, индивидуального плана и наличия мест или финансирования. Страница на украинском языке или сама регистрация не подтверждают право на каждую услугу. Регистрация и документы UŽT Статусы и услуги UŽT

### `employment-assistance-conditions`

- **Article UUID:** `26051216-75b3-5564-a2ec-9a0723bcfec6` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `762e80ed-e9ed-58d8-865c-5e7c3520932a`, `a37162b7-95f9-58e2-8fb3-18a05f6db7b0`, `eed01710-a0b1-5fa3-af0f-7e6b1d87f095`
- **Support / language parity:** Sodra unemployment insurance is distinct; conditions and closed programme intake remain qualified.

**EN**

Unemployment insurance is a separate Sodra benefit, not a payment from browsing vacancies or registering with UŽT. For unemployed status granted from 1 July 2026, the general rule includes at least 12 months of unemployment insurance in the previous 24 months, subject to exceptions and other conditions. Whether Ukrainian insurance periods count is not established in the reviewed sources. The reviewed JOINT-UA-EMPLOY course page showed participant intake closed as of 9 November 2026; do not treat it as an open application. Sodra unemployment benefit rules Lithuania–Ukraine agreement information JOINT-UA-EMPLOY course notice

**UK**

Страхування на випадок безробіття — окрема виплата Sodra, а не результат перегляду вакансій чи реєстрації в UŽT. Для статусу безробітного, наданого з 1 липня 2026 року, загальне правило передбачає щонайменше 12 місяців страхування від безробіття протягом попередніх 24 місяців, з урахуванням винятків та інших умов. У переглянутих джерелах не встановлено, чи зараховується український страховий стаж. На 9 листопада 2026 року на перевіреній сторінці курсу JOINT-UA-EMPLOY прийом учасників був закритий; не вважайте його відкритою заявкою. Правила допомоги Sodra з безробіття Інформація про угоду Литви й України Оголошення курсу JOINT-UA-EMPLOY

**RU**

Страхование по безработице — отдельная выплата Sodra, а не результат просмотра вакансий или регистрации в UŽT. Для статуса безработного, предоставленного с 1 июля 2026 года, общее правило предусматривает не менее 12 месяцев страхования по безработице за предыдущие 24 месяца с учётом исключений и других условий. В изученных источниках не установлено, засчитывается ли украинский страховой стаж. На 9 ноября 2026 года на проверенной странице курса JOINT-UA-EMPLOY набор участников был закрыт; не считайте это открытой заявкой. Правила пособия Sodra по безработице Информация о соглашении Литвы и Украины Объявление курса JOINT-UA-EMPLOY

### `work-permission`

- **Article UUID:** `5cdd5634-6d7a-5c02-aa2a-02f4758e6d2d` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `5ba0aad9-3de1-543c-a5b7-ca64cf8926c4`, `820ce6f5-0a59-5343-a33c-fd83c96d5495`
- **Support / language parity:** Qualifying TP application is the trigger; physical document is not required; unrelated applications excluded.

**EN**

A person entitled to temporary protection (TP) in Lithuania may work after applying for TP; the physical residence document need not first be issued. The covered TP route does not require a separate work permit. This rule applies to the qualifying TP group and period, not to every Ukrainian national or every pending residence-permit application. Foreigners Law Migration Department work guidance

**UK**

Особа, яка має право на тимчасовий захист (TP) у Литві, може працювати після подання заяви на TP; чекати на видачу фізичного документа на проживання не потрібно. Для охопленого маршруту TP окремий дозвіл на роботу не потрібен. Це правило стосується визначеної категорії та періоду TP, а не кожного громадянина України чи будь-якої заяви на дозвіл на проживання, що розглядається. Закон про правове становище іноземців Вказівки Департаменту міграції щодо роботи

**RU**

Человек, имеющий право на временную защиту (TP) в Литве, может работать после подачи заявления на TP; ждать выдачи физического документа на проживание не требуется. Для охватываемого маршрута TP отдельное разрешение на работу не нужно. Это правило относится к определённой категории и периоду TP, а не к каждому гражданину Украины или любому рассматриваемому заявлению на разрешение на проживание. Закон о правовом положении иностранцев Указания Департамента миграции о работе

### `work-employer-duties`

- **Article UUID:** `5cdd5634-6d7a-5c02-aa2a-02f4758e6d2d` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `a2d34570-a095-53c8-9c1c-d97227db1a55`, `d854fb9b-8a44-58b2-b581-b24d76f48b4d`, `e1e0d5c2-2757-57e2-ba97-7240431cc4c4`, `f1d9f456-cbf7-5299-8cdb-95b408b0e92e`
- **Support / language parity:** Ordinary employer duties remain; reporting edge cases are not generalized.

**EN**

Ordinary employment duties still apply. The employer must use a written employment contract and complete the applicable Sodra start notice before work begins, alongside payroll, social-insurance and workplace-safety obligations. The exact foreign-worker reporting steps for every TP document or cohort are not settled by the reviewed guidance; the work-permit exemption should not be read as removing other employer duties. Self-employment requires separate State Tax Inspectorate (VMI) registration and tax steps, and regulated professions may need recognition or a licence. Sodra employment notice Sodra reporting and contributions Regulated-profession guidance VMI individual-activity registration

**UK**

Звичайні трудові обов’язки залишаються чинними. Роботодавець має укласти письмовий трудовий договір і до початку роботи подати відповідне повідомлення до Sodra, а також виконувати обов’язки щодо зарплати, соціального страхування й безпеки праці. У переглянутих вказівках не визначено точний порядок повідомлення про іноземного працівника для кожного документа чи категорії TP; звільнення від дозволу на роботу не слід вважати скасуванням інших обов’язків роботодавця. Самозайнятість потребує окремої реєстрації у Державній податковій інспекції (VMI) та податкових кроків, а регульовані професії можуть вимагати визнання кваліфікації чи ліцензії. Повідомлення Sodra про працевлаштування Звітність і внески Sodra Вказівки щодо регульованих професій Реєстрація індивідуальної діяльності у VMI

**RU**

Обычные трудовые обязанности сохраняются. Работодатель должен заключить письменный трудовой договор и до начала работы подать соответствующее уведомление в Sodra, а также выполнять обязанности по зарплате, социальному страхованию и охране труда. В изученных указаниях не определён точный порядок уведомления о найме иностранца для каждого документа или категории TP; освобождение от разрешения на работу не следует считать отменой других обязанностей работодателя. Самозанятость требует отдельной регистрации в Государственной налоговой инспекции (VMI) и налоговых действий, а регулируемые профессии могут требовать признания квалификации или лицензии. Уведомление Sodra о трудоустройстве Отчётность и взносы Sodra Указания о регулируемых профессиях Регистрация индивидуальной деятельности в VMI

### `work-minimum-pay`

- **Article UUID:** `5cdd5634-6d7a-5c02-aa2a-02f4758e6d2d` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `6707afc9-e920-5f14-953a-adc07f61aec0`, `c103e4f9-f67c-5fcd-a762-4191050c61cf`
- **Support / language parity:** 2026 and 2027 gross amounts/dates match; monthly minimum is bounded to unqualified work.

**EN**

As of 2026, the minimum is €1,153 gross per month and €7.05 per hour. The monthly minimum may be paid only for unqualified work; it is not a universal salary for every skilled full-time role. An enacted rate takes effect on 1 January 2027: €1,245 gross per month and €7.61 per hour. These are gross minimums, not take-home pay. 2026 minimum-pay guidance 2027 minimum-pay resolution

**UK**

У 2026 році мінімальна оплата становить €1 153 брутто на місяць і €7,05 за годину. Місячний мінімум дозволено платити лише за некваліфіковану роботу; це не універсальна зарплата для кожної кваліфікованої роботи на повний день. Затверджена ставка набуде чинності 1 січня 2027 року: €1 245 брутто на місяць і €7,61 за годину. Це суми до оподаткування, а не виплата на руки. Вказівки щодо мінімальної оплати 2026 року Постанова про мінімальну оплату 2027 року

**RU**

В 2026 году минимальная оплата составляет €1 153 брутто в месяц и €7,05 в час. Месячный минимум допускается только для неквалифицированной работы; это не универсальная зарплата для каждой квалифицированной должности с полной занятостью. Утверждённая ставка вступит в силу 1 января 2027 года: €1 245 брутто в месяц и €7,61 в час. Это суммы до налогов, а не на руки. Указания о минимальной оплате 2026 года Постановление о минимальной оплате 2027 года

### `healthcare-access`

- **Article UUID:** `42863927-2979-5769-8092-c9393a0ea916` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `7d4447b5-e6d4-5d59-b73d-14d5cedcb22d`, `928b6d64-fcd6-5f98-9aa0-36e91eda9fc8`, `99a10eee-33b4-59ef-8ef0-8e53cedc02a8`
- **Support / language parity:** TP necessary/emergency care is distinguished from full CHI and routine care.

**EN**

People entitled to or granted temporary protection (TP) can access state-funded emergency and other necessary healthcare under the specific TP rules. “Necessary” care is not the same as all routine treatment, and this entitlement is not full compulsory health insurance (CHI). A clinician assesses whether a service falls within the necessary-care boundary. Health Insurance Law Health System Law VLK temporary-protection FAQ

**UK**

Особи, які мають право на тимчасовий захист (TP) або отримали його, можуть користуватися екстреною та іншою необхідною медичною допомогою, що фінансується державою за спеціальними правилами TP. «Необхідна» допомога — це не всі планові послуги, а таке право не дорівнює повному обов’язковому медичному страхуванню (CHI). Лікар оцінює, чи належить послуга до необхідної допомоги. Закон про медичне страхування Закон про систему охорони здоров’я FAQ VLK про тимчасовий захист

**RU**

Лица, имеющие право на временную защиту (TP) или получившие её, могут пользоваться экстренной и другой необходимой медицинской помощью, финансируемой государством по специальным правилам TP. «Необходимая» помощь — это не все плановые услуги, и такое право не равно полному обязательному медицинскому страхованию (CHI). Врач оценивает, относится ли услуга к необходимой помощи. Закон о медицинском страховании Закон о системе здравоохранения FAQ VLK о временной защите

### `healthcare-proof`

- **Article UUID:** `42863927-2979-5769-8092-c9393a0ea916` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `1df94c0e-ba86-541a-b308-fac43473560e`, `ae0b438d-063a-5fb2-bf68-ecf2fb3903d7`, `bd32ad02-527a-5a00-bc9a-c5e183f3a44e`
- **Support / language parity:** TP residence document is not active-CHI proof; separate eligibility/record check stated.

**EN**

Full CHI requires a separate qualifying insurance basis or category, such as eligible employment with the required contributions or a listed state-insured group. A TP residence document alone does not prove active CHI. The State Health Insurance Fund (VLK) maintains the relevant coverage record; check it with VLK or a healthcare provider. For planned insured care, register with a primary-care/family-doctor institution under the applicable rules. VLK CHI categories When CHI takes effect Choosing a family doctor

**UK**

Для повного CHI потрібна окрема підстава страхування чи категорія, наприклад відповідна робота зі сплатою необхідних внесків або включення до визначеної державою застрахованої групи. Сам документ на проживання за TP не підтверджує активне CHI. Фонд державного медичного страхування (VLK) веде відповідний запис про покриття; перевірте його у VLK або медичного закладу. Для планової допомоги за страхуванням зареєструйтеся у закладі первинної допомоги/сімейного лікаря за чинними правилами. Категорії CHI у VLK Коли починає діяти CHI Вибір сімейного лікаря

**RU**

Для полного CHI требуется отдельное страховое основание или категория, например подходящая работа с необходимыми взносами либо включение в установленную государством застрахованную группу. Сам документ на проживание по TP не подтверждает активное CHI. Фонд государственного медицинского страхования (VLK) ведёт соответствующую запись о покрытии; проверьте её в VLK или медицинском учреждении. Для плановой застрахованной помощи зарегистрируйтесь в учреждении первичной помощи/семейного врача по применимым правилам. Категории CHI в VLK Когда начинает действовать CHI Выбор семейного врача

### `healthcare-urgent`

- **Article UUID:** `42863927-2979-5769-8092-c9393a0ea916` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `405da7d0-16b4-56b6-9f70-c76458be17ea`, `4fe60258-62fa-5671-84cc-e30fe72f3fc3`
- **Support / language parity:** 112 / 113 distinction and no blanket free-care promise are preserved.

**EN**

For an emergency, call 112. Number 113 is for non-emergency medical advice. These routes do not mean every visit, ambulance service, routine maternity care, medicine or follow-up service is automatically free; pregnancy, ESI and individual CHI status have specific boundaries. Do not delay emergency care while checking insurance. VLK urgent-care guidance Emergency number 112

**UK**

У невідкладній ситуації телефонуйте 112. Номер 113 призначений для консультації щодо неекстрених медичних питань. Це не означає, що кожен візит, швидка допомога, планова допомога під час вагітності, ліки чи подальше лікування автоматично безкоштовні; для вагітності, ESI та індивідуального статусу CHI діють окремі межі. Не відкладайте невідкладну допомогу, з’ясовуючи страхування. Вказівки VLK щодо термінової допомоги Номер екстреної допомоги 112

**RU**

В экстренной ситуации звоните 112. Номер 113 предназначен для консультаций по неэкстренным медицинским вопросам. Это не означает, что каждый визит, скорая помощь, плановая помощь при беременности, лекарства или последующее лечение автоматически бесплатны; для беременности, ESI и индивидуального статуса CHI есть отдельные ограничения. Не откладывайте экстренную помощь, выясняя вопрос страхования. Указания VLK по срочной помощи Экстренный номер 112

### `housing-initial`

- **Article UUID:** `28169e82-6320-5f00-9b34-37e77f3c7d26` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `002ab8f8-f86b-50d2-86f3-0192fa6ec11d`, `edd48b6b-339f-59c0-858c-89c6b3a1a5fb`
- **Support / language parity:** The six-month route is limited to eligible pending applicants; no placement guarantee.

**EN**

Eligible people whose temporary-protection (TP) residence-permit application is pending may use a state or municipal accommodation route for up to six months from registration of the application. This is a limited statutory route, not a guarantee that a place is available, where it will be, or that free accommodation continues beyond the period. Confirm current capacity with the responsible authority. Foreigners Law Government Resolution No. 224

**UK**

Особи, які відповідають умовам і чия заява на дозвіл на проживання за тимчасовим захистом (TP) ще розглядається, можуть скористатися державним або муніципальним розміщенням на строк до шести місяців від реєстрації заяви. Це обмежений законом маршрут, а не гарантія наявності місця, його розташування чи безкоштовного житла після завершення строку. Перевіряйте поточну наявність місць у відповідального органу. Закон про правове становище іноземців Постанова Уряду № 224

**RU**

Лица, отвечающие условиям и ожидающие решения по заявлению на разрешение на проживание на основании временной защиты (TP), могут воспользоваться государственным или муниципальным размещением на срок до шести месяцев с момента регистрации заявления. Это ограниченный законом маршрут, а не гарантия наличия места, его расположения или бесплатного жилья после окончания срока. Уточняйте текущую доступность у ответственного органа. Закон о правовом положении иностранцев Постановление Правительства № 224

### `housing-private-rental`

- **Article UUID:** `28169e82-6320-5f00-9b34-37e77f3c7d26` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `36ff51ff-ff51-529a-9424-ce4b1c325a6e`, `c8dedc7c-e90b-5d75-8d9a-1099a67d17a1`
- **Support / language parity:** Written lease/advance rent is qualified; no general deposit cap invented; declaration is separate.

**EN**

Private renting is separate from reception. A fixed-term residential lease must be in writing. The Civil Code also limits advance rent demands to no more than the first month in the described residential-lease context; the reviewed sources do not establish a general statutory deposit cap. Declaring a place of residence is a separate procedure from signing a lease or registering it in the Real Estate Register, and local evidence requirements may vary. Civil Code rental rules Residence-declaration guidance

**UK**

Приватна оренда не є частиною системи прийому. Строковий договір оренди житла має бути письмовим. Цивільний кодекс також обмежує вимогу авансової орендної плати сумою не більше першого місяця у відповідному контексті житлової оренди; у переглянутих джерелах не встановлено загального законодавчого ліміту застави. Декларування місця проживання — окрема процедура від підписання договору чи його реєстрації в Реєстрі нерухомості; місцеві вимоги до підтверджень можуть відрізнятися. Правила оренди в Цивільному кодексі Вказівки щодо декларування місця проживання

**RU**

Частная аренда не относится к системе приёма. Срочный договор аренды жилья должен быть письменным. Гражданский кодекс также ограничивает требование авансовой арендной платы суммой не более первого месяца в описанном контексте аренды жилья; в изученных источниках не установлен общий законодательный предел залога. Декларирование места жительства — отдельная процедура от подписания договора или его регистрации в Реестре недвижимости; местные требования к подтверждениям могут различаться. Правила аренды в Гражданском кодексе Указания о декларировании места жительства

### `housing-financial-support`

- **Article UUID:** `28169e82-6320-5f00-9b34-37e77f3c7d26` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `279afd3e-c06d-5723-810c-79b49b0b6c86`, `6e630bfe-7cba-510f-90c9-847f1b421d79`, `789a25a4-129e-5c64-9450-364e81486f29`, `fb747279-3f8b-5001-99cb-3478c6e17f5c`
- **Support / language parity:** Support routes/local criteria are distinct; application is not award or immediate housing.

**EN**

Housing assistance may include ordinary means-tested support and a separate TP-related rent-compensation route. Social housing, utilities support and one-time settlement assistance have their own eligibility rules, application steps and funding. Applying does not mean approval, a particular amount, or immediate housing; municipalities administer or confirm relevant details locally. Ask the municipality about the current route for your circumstances. SADM housing support Low-income and utility support Support for Ukrainians One-time accommodation benefit guidance

**UK**

Житлова допомога може включати звичайну підтримку з перевіркою доходів і окремий маршрут компенсації оренди для осіб із TP. Соціальне житло, допомога на комунальні послуги та одноразова допомога на облаштування мають власні умови, порядок подання й фінансування. Подання заяви не означає схвалення, визначеної суми чи негайного надання житла; відповідні деталі адмініструють або підтверджують на місцевому рівні. Уточнюйте в муніципалітеті чинний маршрут для вашої ситуації. Житлова підтримка SADM Допомога малозабезпеченим і на комунальні послуги Підтримка українців Вказівки щодо одноразової допомоги на облаштування

**RU**

Жилищная поддержка может включать обычную помощь с проверкой доходов и отдельный маршрут компенсации аренды для лиц с TP. Социальное жильё, помощь на коммунальные услуги и единовременная помощь на обустройство имеют собственные условия, порядок подачи и финансирование. Подача заявления не означает одобрения, определённой суммы или немедленного предоставления жилья; соответствующие детали администрируются или подтверждаются на местном уровне. Уточните в муниципалитете действующий маршрут для вашей ситуации. Жилищная поддержка SADM Помощь малоимущим и на коммунальные услуги Поддержка украинцев Указания о единовременной помощи на обустройство

### `education-school-enrolment`

- **Article UUID:** `bacfcf91-7365-5c28-a240-b6db41c1aa0f` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `088fd097-e7d9-57a6-b725-1ff3e9e587dd`, `634a26ac-c608-5b64-a86d-447265a873de`, `94a47f71-e7c4-5295-80ed-8ef93f04ccda`, `c47e1d25-76d9-5159-9bae-21b19793b1c8`
- **Support / language parity:** Individual placement and no preferred-school guarantee are present.

**EN**

Children with the relevant right to reside, including children covered by temporary protection (TP), can access Lithuanian education. Compulsory education continues through age 16. Contact the municipality’s education division or a school about enrolment; available records, an interview and other information can inform an individual class-placement decision. A preferred school place is not guaranteed. Law on Education Ministry Ukraine-pupil guidance Municipal school contacts Class-placement procedure

**UK**

Діти, які мають відповідне право на проживання, зокрема діти під тимчасовим захистом (TP), можуть навчатися в Литві. Обов’язкова освіта триває до 16 років. Зверніться до відділу освіти муніципалітету або школи щодо зарахування; наявні документи, співбесіда та інша інформація можуть враховуватися під час індивідуального визначення класу. Місце саме у вибраній школі не гарантується. Закон про освіту Вказівки міністерства щодо учнів з України Контакти муніципалітетів щодо шкіл Процедура визначення класу

**RU**

Дети, имеющие соответствующее право на проживание, включая детей под временной защитой (TP), могут получать образование в Литве. Обязательное обучение продолжается до 16 лет. По вопросу зачисления обратитесь в отдел образования муниципалитета или школу; имеющиеся документы, собеседование и другая информация могут учитываться при индивидуальном определении класса. Место именно в выбранной школе не гарантируется. Закон об образовании Указания министерства об учениках из Украины Муниципальные контакты по школам Процедура определения класса

### `education-pupil-language`

- **Article UUID:** `bacfcf91-7365-5c28-a240-b6db41c1aa0f` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `336c304a-c672-5f36-9008-fed3a6adefe6`, `c47e1d25-76d9-5159-9bae-21b19793b1c8`
- **Support / language parity:** No fixed hours; eight-school snapshot is dated; online study is not established as substitute.

**EN**

Schools may provide Lithuanian-language learning and other support based on the pupil’s needs and local organisation, but the reviewed sources do not establish guaranteed weekly hours or a uniform package. A June 2026 ministry update reported eight schools running Ukrainian education programmes; this is a dated snapshot, not a permanent nationwide placement guarantee. Ukrainian online schooling alone is not established as a substitute for Lithuanian compulsory schooling for a child living in Lithuania. Ministry Ukraine-pupil guidance June 2026 education update

**UK**

Школи можуть організовувати вивчення литовської мови та іншу підтримку з огляду на потреби учня й місцеву організацію, але в переглянутих джерелах не встановлено гарантованої кількості годин на тиждень чи єдиного пакета послуг. У повідомленні міністерства за червень 2026 року йшлося про вісім шкіл з українськими освітніми програмами; це датований стан, а не постійна загальнонаціональна гарантія місця. Саме лише онлайн-навчання в Україні у переглянутих джерелах не встановлено як заміну обов’язковому навчанню в Литві для дитини, яка тут живе. Вказівки міністерства щодо учнів з України Оновлення про освіту за червень 2026 року

**RU**

Школы могут организовать изучение литовского языка и другую поддержку с учётом потребностей ученика и местной организации, но в изученных источниках не установлены гарантированное число часов в неделю или единый пакет. В сообщении министерства за июнь 2026 года говорилось о восьми школах с украинскими образовательными программами; это датированный срез, а не постоянная общенациональная гарантия места. Одно лишь онлайн-обучение в Украине в изученных источниках не установлено как замена обязательному обучению в Литве для ребёнка, живущего здесь. Указания министерства об учениках из Украины Обновление об образовании за июнь 2026 года

### `education-higher-admission`

- **Article UUID:** `bacfcf91-7365-5c28-a240-b6db41c1aa0f` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `506595b4-de02-5577-a54a-6ab24950e074`, `81aceb9f-bc8f-5686-b9fc-62b932f34349`, `9452a8a3-75ef-5525-a1a2-1bd90161abe8`, `eb817f8d-7915-5ea7-a712-47a72e02dc98`, `eea9fa77-bcae-5ffc-8221-2168938ed5ad`, `f72a1f19-ba27-5c65-8f36-bf4e165e395b`
- **Support / language parity:** Admission, recognition, fees/funding are institution/call-specific; closed call/VSF uncertainty retained.

**EN**

Higher-education admission, recognition of prior qualifications, credit transfer, language requirements and fees are decided under the rules of the chosen institution and programme. The Centre for Quality Assessment in Higher Education (SKVC) can advise on recognition, including cases with incomplete records, while the institution makes the admission decision. State-funded places and scholarships have separate eligibility and calls: a 2026 scholarship call reviewed here was closed, and State Studies Foundation (VSF) guidance had unresolved cohort wording. Confirm current terms directly before relying on funding. SKVC Ukraine study guidance Recognition documentation Higher-education institution guidance 2026 state-funded eligibility 2026 scholarship call VSF Ukraine support

**UK**

Вступ до закладу вищої освіти, визнання попередньої кваліфікації, перенесення кредитів, мовні вимоги й оплата визначаються правилами обраного закладу та програми. Центр оцінювання якості вищої освіти (SKVC) може консультувати щодо визнання, зокрема за неповних документів, а рішення про вступ ухвалює заклад. Державне фінансування й стипендії мають окремі умови та конкурси: перевірений конкурс 2026 року вже був закритий, а у вказівках Фонду державних досліджень (VSF) залишалися невирішені формулювання щодо категорій вступу. Перед розрахунком на фінансування уточнюйте чинні умови безпосередньо. Вказівки SKVC для вступників з України Документи для визнання Вказівки для закладів вищої освіти Умови державного фінансування 2026 року Конкурс стипендій 2026 року Підтримка VSF для українців

**RU**

Поступление в вуз, признание прежней квалификации, перенос зачётных единиц, языковые требования и оплата определяются правилами выбранного учреждения и программы. Центр оценки качества высшего образования (SKVC) консультирует по признанию, в том числе при неполных документах, а решение о приёме принимает учреждение. Государственное финансирование и стипендии имеют отдельные условия и конкурсы: проверенный конкурс 2026 года был закрыт, а в указаниях Фонда государственных исследований (VSF) оставались неразрешённые формулировки о категориях поступающих. Перед расчётом на финансирование уточняйте актуальные условия напрямую. Указания SKVC для поступающих из Украины Документы для признания Указания для вузов Условия государственного финансирования 2026 года Конкурс стипендий 2026 года Поддержка VSF для украинцев

### `social-subsistence`

- **Article UUID:** `c971e527-d7ca-5dff-b739-02e6df1f98e1` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `a050f8a7-09ce-5af0-9c34-826da705905c`, `ad9628c2-0f84-52c4-9b58-48f6783233f8`, `c57b0f26-f706-510a-b22c-b2255a79bc99`
- **Support / language parity:** Means testing and no guaranteed payment stated; application detail is better aligned to social-application.

**EN**

People granted temporary protection (TP) are included in specified Lithuanian social-assistance and child-benefit categories, but TP does not guarantee a payment. Means-tested cash support depends on household, income, property and activity rules. The municipality generally receives applications and decides relevant cash assistance; SPIS, the Social Support for the Family Information System, is an electronic application channel, not an independent decision-maker or entitlement. SADM low-income support SADM detailed cash-assistance rules SPIS information

**UK**

Особи, які отримали тимчасовий захист (TP), включені до визначених литовських категорій соціальної допомоги та виплат на дітей, але сам TP не гарантує виплату. Грошова допомога з оцінкою потреб залежить від складу домогосподарства, доходів, майна й виконання інших умов. Заяви на відповідну грошову допомогу зазвичай приймає та розглядає муніципалітет; SPIS (Інформаційна система соціальної підтримки сім’ї) є електронним каналом подання, а не органом, що самостійно ухвалює рішення чи надає право на виплату. Допомога малозабезпеченим від SADM Детальні правила грошової допомоги SADM Інформація про SPIS

**RU**

Лица, получившие временную защиту (TP), входят в определённые литовские категории социальной помощи и выплат на детей, однако сам TP не гарантирует выплату. Денежная помощь с проверкой нуждаемости зависит от состава домохозяйства, доходов, имущества и выполнения других условий. Заявления на соответствующую денежную помощь обычно принимает и рассматривает муниципалитет; SPIS (Информационная система социальной поддержки семьи) — электронный канал подачи, а не самостоятельный орган, который принимает решение или создаёт право на выплату. Помощь малоимущим от SADM Подробные правила денежной помощи SADM Информация о SPIS

### `social-application`

- **Article UUID:** `c971e527-d7ca-5dff-b739-02e6df1f98e1` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `69321373-30b6-50fc-92d6-6b5f94b0de32`, `9e961b2a-50fa-5dc8-9374-a49dee9fbff4`, `e179bb12-cd2b-5264-892a-36eef1a4a71b`
- **Additional finding:** LT-C02.
- **Support / language parity:** Value describes family/child payments and Sodra, not municipal/SPIS application roles; LT-C02.

**EN**

Child and family payments include the general child benefit and, where conditions are met, additional supplements and separate birth, pregnancy and childcare benefits. Each has its own eligibility, amount and application rules. Sodra (the Lithuanian social-insurance fund) insurance benefits are different: they depend on insured status and contribution conditions. A social-assistance or child-benefit application is not the same as an insurance claim. SADM family-benefit guidance Child and family benefit rules Sodra benefits for Ukrainians

**UK**

Сімейні виплати включають загальну допомогу на дитину та, за виконання умов, додаткові надбавки й окремі виплати у зв’язку з народженням дитини, вагітністю та доглядом за дитиною. Для кожної діють власні критерії, суми й порядок подання. Страхові виплати Sodra (литовського фонду соціального страхування) — окрема система: вони залежать від застрахованого статусу та страхового стажу. Заява на соціальну допомогу чи виплату на дитину не є заявою на страхову виплату. Вказівки SADM щодо сімейних виплат Правила виплат на дітей і сім’ю Виплати Sodra українцям

**RU**

Семейные выплаты включают общую выплату на ребёнка и, при соблюдении условий, дополнительные надбавки и отдельные выплаты в связи с рождением ребёнка, беременностью и уходом за ребёнком. Для каждой действуют свои критерии, суммы и порядок подачи. Страховые выплаты Sodra (литовского фонда социального страхования) — отдельная система: они зависят от застрахованного статуса и страхового стажа. Заявление на социальную помощь или выплату на ребёнка не является заявлением на страховое пособие. Указания SADM о семейных выплатах Правила выплат на детей и семью Выплаты Sodra украинцам

### `social-child-benefits`

- **Article UUID:** `c971e527-d7ca-5dff-b739-02e6df1f98e1` · **State:** `Draft` / `IsAvailable=true` · **Classification:** Correction required (LT-C01).
- **OfficialSourceIds:** `61117a42-3c5d-5ad3-9064-44ee0cda1edd`, `ab65a176-c2e3-5fc1-81d1-0be540993a94`, `ed936571-5140-5579-9c3f-f6143ebd8c23`, `f19d66e3-a034-5493-befc-d739a06d390c`, `f4d568e0-18a5-5988-8e7d-ce4d5984d0c5`
- **Additional finding:** LT-C03.
- **Support / language parity:** Value covers Ukraine-specific support but omits general child benefit/supplement distinctions; LT-C03.

**EN**

Current support described for people from Ukraine may include a limited one-time settlement/accommodation payment, a separate TP rent-compensation route and compensation for up to six months of preschool education, subject to the relevant status, lease, child and municipal conditions. These are not unconditional recurring payments, and applying does not guarantee approval. Local emergency help varies by municipality. Confirm current details locally; older emergency schemes should not be described as open. SADM support for Ukrainians 2026 status-based support guide Foreigners Law housing route Housing support guidance Municipal social-support contacts

**UK**

Чинна підтримка для людей з України може включати обмежену одноразову допомогу на облаштування/проживання, окремий маршрут компенсації оренди для осіб із TP та компенсацію за дошкільну освіту на строк до шести місяців — за відповідних умов щодо статусу, договору оренди, дитини й муніципалітету. Це не безумовні регулярні виплати, а подання заяви не гарантує схвалення. Місцева екстрена допомога різниться між муніципалітетами. Уточнюйте чинні деталі на місці; старі надзвичайні програми не слід описувати як відкриті. Підтримка українців від SADM Посібник 2026 року за статусом Житловий маршрут у Законі про іноземців Вказівки щодо житлової підтримки Контакти муніципальної соціальної допомоги

**RU**

Текущая поддержка для людей из Украины может включать ограниченную единовременную помощь на обустройство/проживание, отдельный маршрут компенсации аренды для лиц с TP и компенсацию дошкольного образования сроком до шести месяцев — при соблюдении условий о статусе, договоре аренды, ребёнке и муниципалитете. Это не безусловные регулярные выплаты, а подача заявления не гарантирует одобрения. Местная экстренная помощь различается по муниципалитетам. Уточняйте актуальные детали на месте; старые чрезвычайные программы не следует описывать как открытые. Поддержка украинцев от SADM Руководство 2026 года по статусу Жилищный маршрут в Законе об иностранцах Указания о жилищной поддержке Контакты муниципальной социальной помощи

## 6. Source ownership and relevance

- Lithuania OfficialSources: **71/71**; unique IDs: **71/71**; UUID collisions against existing catalog IDs: **0**.
- Each source belongs to one of the eight Lithuania InformationEntryIds. Exact UUID/title parity with its owning draft: **71/71**.
- Research source-ID plus URL check: **71/71** match a source row in the corresponding research package; URL mismatches: **0**.
- `LastCheckedAt`: **71/71** equal `2026-11-09T00:00:00Z`; HTTPS-only: **71/71**; duplicate `(article, URL)` pairs: **0**.
- Inline references: **222**; dangling or wrong-owner refs: **0**.
- Comparison `OfficialSourceIds`: non-empty for 24/24; every ID exists and belongs to the linked article. The IDs match the sources cited by the corresponding article paragraph and relevant research rows. Shared official documents across articles correctly have distinct article-owned UUIDs.
- Source relevance supports the present factual text. LT-C02/C03 concern fact-to-aspect alignment/coverage, not UUID ownership. LT-C01 concerns text values; source IDs are already provided separately.

## 7. Cross-topic consistency

**Blocking cross-topic conflicts: 0.** The article statements are mutually consistent on TP application/work triggers and ordinary permits; protection scheme dates and personal documents; UŽT versus Sodra; TP healthcare versus full CHI; housing versus social support; schooling; and general family payments versus Ukraine-specific support. LT-C02/C03 are aspect alignment issues, not article contradictions.

## 8. Topic-map / localization / visibility

- **Topic map:** Lithuania appears in all 8 topics; each has 13 country entries; all Lithuania entries target the specified article IDs; 104 distinct mapped articles globally. `CountryComparisonTopicMap.Validate` is covered by the integration test.
- **Localization:** EN `Lithuania` / `in Lithuania`; UK `Литва` / `у Литві`; RU `Литва` / `в Литве` — exact.
- **Visibility:** Development visible (13 countries); Production hidden (12 countries). Hidden Production state is expected while all 24 translations are `NeedsReview`, not a finding.

## 9. Existing hold verification

**Netherlands: PASS.** Translations remain 21 `Verified` / 3 `NeedsReview`; facts remain 20 `Approved` / 4 `Draft`. `protection-scheme`, `protection-application`, `protection-dates`, and `residence-filing` remain Draft. `residence-filing` remains `IsAvailable=false` with `OfficialSourceIds=[]`. Netherlands comparison records match baseline.

**Sweden: PASS.** `social-child-benefits` remains `Draft`, `IsAvailable=false`, `OfficialSourceIds=[]`. Sweden comparison records match baseline.

## 10. Required corrections

### LT-C01 — Correction required — all 24 Lithuania facts, EN / UK / RU
**Current wording issue:** each of the 72 fact-value strings ends with plain source labels left by replacing inline citation tokens (for example: `Government Resolution No. 224 Foreigners Law EU Decision 2022/382`). They are not part of the fact sentence and are redundant with `OfficialSourceIds`.
**Why:** the labels appear as unseparated text after the concluding sentence and can be mistaken for part of the answer.
**Exact correction required:** remove only the trailing source-label text from every fact value in EN/UK/RU; retain the factual sentences and the correct `OfficialSourceIds`.

### LT-C02 — Correction required — `social-application` — EN / UK / RU
**Current wording:** this fact starts “Child and family payments include the general child benefit…” and then distinguishes Sodra insurance benefits.
**Why:** that content describes child/family benefits, not municipal applications, SPIS as the channel, and who decides. The municipality/SPIS wording currently appears under `social-subsistence`.
**Exact correction required:** make `social-application` describe the municipal application/decision role, SPIS as an application channel rather than decision-maker, and separate Sodra insurance claims. Keep family-benefit categories in `social-child-benefits`; use only article-owned relevant sources.

### LT-C03 — Correction required — `social-child-benefits` — EN / UK / RU
**Current wording:** this fact covers Ukraine-specific one-time support, TP rent compensation and preschool compensation, with local variation.
**Why:** it omits the general child benefit and the distinction between conditional supplements/separate birth, pregnancy and childcare payments and insurance benefits.
**Exact correction required:** state the general child benefit and distinguish conditional supplements/separate family payments and insurance benefits; retain Ukraine-specific programmes as a limited conditional sub-scope. Preserve application-versus-entitlement and municipality qualifications.

**Minor corrections: 0. Blockers / source-refresh requirements: 0.** These findings can be resolved from the accepted drafts and existing source packages; new research is not required.

## 11. Human acceptance readiness

Article content and source integration are ready: **8/8 articles Ready**. Comparison facts are not ready: 24/24 need the shared label cleanup, and `social-application` / `social-child-benefits` need aspect-specific corrections. Resolve LT-C01–LT-C03 and rerun wording, source, and language checks before a focused post-correction review.

**Final verdict: NEEDS CORRECTION BEFORE HUMAN ACCEPTANCE**

This is an acceptance-preparation verdict only. It is not owner acceptance, status promotion, publication approval or deployment authorization. No catalog, draft, research, test, application or status was changed in this audit; this report is the only intended change.
