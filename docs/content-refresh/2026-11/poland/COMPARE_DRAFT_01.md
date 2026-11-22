# Poland Compare — Candidate staging draft

**Status:** Staging only; do not apply to `comparison-facts.json`.

**Ledger:** 24/24 current Poland facts. Final staging dispositions below reflect the 17 November 2026 evidence check; they are not production mutations.

## 01. Compare.Topic.TemporaryProtectionApplication / `protection-scheme`

- Production state: `Approved` / available `True`
- Final source bindings: e4aa407d-df53-58ab-b10a-6fc2b22d91d3, 7418e8ea-bc3d-561b-ba05-2f09ed29cdc0
- **EN — KEEP:** PESEL UKR records Polish temporary-protection status. It is distinct from an ordinary residence permit or a CUKR card; UdSC stopped issuing the former protection certificates on 5 March 2026.
- **UK — KEEP:** PESEL UKR фіксує статус тимчасового захисту в Польщі. Це не звичайний дозвіл на проживання і не картка CUKR; з 5 березня 2026 року UdSC припинило видавати колишні посвідчення захисту.
- **RU — KEEP:** PESEL UKR фиксирует статус временной защиты в Польше. Это не обычное разрешение на проживание и не карта CUKR; с 5 марта 2026 года UdSC прекратило выдавать прежние свидетельства защиты.
- **Disposition:** KEEP.

## 02. Compare.Topic.TemporaryProtectionApplication / `protection-application`

- Production state: `Approved` / available `True`
- Final source bindings: e4aa407d-df53-58ab-b10a-6fc2b22d91d3, 0dfc274b-f02d-5acb-913e-51dee3c7fa24
- **EN — REVISE:** Apply in person for PESEL UKR at any gmina office within 30 days after entering Poland. The service lists a form and 35 × 45 mm photograph; fingerprints are taken from applicants aged 12 or older. If no identity document is available, the service permits a declaration, subject to applicable identity-confirmation rules.
- **UK — REVISE:** Для отримання PESEL UKR подайте заяву особисто до будь-якого органу ґміни протягом 30 днів після в’їзду до Польщі. Сервіс указує форму й фотографію 35 × 45 мм; у заявників від 12 років беруть відбитки пальців. Якщо документа, що посвідчує особу, немає, послуга допускає декларацію з урахуванням чинних правил підтвердження особи.
- **RU — REVISE:** Чтобы получить PESEL UKR, подайте заявление лично в любой орган гмины в течение 30 дней после въезда в Польшу. Сервис указывает форму и фотографию 35 × 45 мм; у заявителей от 12 лет берут отпечатки пальцев. Если документа, удостоверяющего личность, нет, услуга допускает декларацию с учётом действующих правил подтверждения личности.
- **Disposition:** REVISE.

## 03. Compare.Topic.TemporaryProtectionApplication / `protection-dates`

- Production state: `Approved` / available `True`
- Final source bindings: e4aa407d-df53-58ab-b10a-6fc2b22d91d3, 7418e8ea-bc3d-561b-ba05-2f09ed29cdc0, 0a989122-f3d4-5afd-a7ce-e10c4566cebe, d0fc2583-ea0d-5289-b9c7-de3e97417529, e5d81209-42dd-5dd0-aabc-a8cfb338224c
- **EN — KEEP:** EU protection extends through 4 March 2028; Article 106 links lawful stay of its defined group to that decision, subject to exclusions and ending events. Older certificates are separately stated valid through 4 March 2027 unless an earlier ending event applies. Do not infer personal document validity.
- **UK — KEEP:** Захист ЄС продовжено до 4 березня 2028 року; стаття 106 пов’язує законність перебування визначеної групи з цим рішенням, з урахуванням виключень і підстав припинення. Для старих посвідчень окремо вказано строк до 4 березня 2027 року, якщо дія не припиниться раніше. Не робіть висновку про строк особистого документа.
- **RU — KEEP:** Защита ЕС продлена до 4 марта 2028 года; статья 106 связывает законность пребывания определённой группы с этим решением с учётом исключений и оснований прекращения. Для старых свидетельств отдельно указан срок до 4 марта 2027 года, если действие не прекратится раньше. Не делайте вывода о сроке личного документа.
- **Disposition:** KEEP.

## 04. Compare.Topic.ResidenceAfterProtection / `residence-routes`

- Production state: `Approved` / available `True`
- Final source bindings: 41dea3fa-8ffc-5d2e-a476-aaf2cf07b109
- **EN — KEEP:** CUKR is a separate three-year temporary residence permit for a defined group with UKR status. It is not PESEL UKR itself and is not granted automatically.
- **UK — KEEP:** CUKR — окремий трирічний дозвіл на тимчасове проживання для визначеної групи зі статусом UKR. Це не сам PESEL UKR; його не надають автоматично.
- **RU — KEEP:** CUKR — отдельное трёхлетнее разрешение на временное проживание для определённой группы со статусом UKR. Это не сам PESEL UKR; его не предоставляют автоматически.
- **Disposition:** KEEP.

## 05. Compare.Topic.ResidenceAfterProtection / `residence-conditions`

- Production state: `Approved` / available `True`
- Final source bindings: 41dea3fa-8ffc-5d2e-a476-aaf2cf07b109
- **EN — KEEP:** CUKR requires UKR status when applying and when the card is issued, UKR status on 4 June 2025, and at least 365 uninterrupted days with UKR status. Children born in Poland have separate conditions.
- **UK — KEEP:** CUKR вимагає статусу UKR під час подання й видачі картки, статусу UKR на 4 червня 2025 року та щонайменше 365 безперервних днів зі статусом UKR. Для дітей, народжених у Польщі, діють окремі умови.
- **RU — KEEP:** CUKR требует статуса UKR при подаче и выдаче карты, статуса UKR на 4 июня 2025 года и не менее 365 непрерывных дней со статусом UKR. Для детей, родившихся в Польше, действуют отдельные условия.
- **Disposition:** KEEP.

## 06. Compare.Topic.ResidenceAfterProtection / `residence-filing`

- Production state: `Approved` / available `True`
- Final source bindings: 41dea3fa-8ffc-5d2e-a476-aaf2cf07b109, 8097a634-318b-5375-930b-c82f4f72df26
- **EN — REVISE:** CUKR applications have been accepted online through MOS since 4 May 2026 and may be filed through 4 March 2027. A timely application can keep the applicant’s stay lawful until card collection or a final refusal/discontinuance; it does not itself grant a residence permit. This special procedure concerns CUKR, not every residence route.
- **UK — REVISE:** Заяви на CUKR приймають онлайн через MOS з 4 травня 2026 року до 4 березня 2027 року. Своєчасна заява може забезпечити законність перебування до отримання картки або остаточного рішення про відмову/припинення; сама подача не надає дозволу на перебування. Ця спеціальна процедура стосується CUKR, а не всіх підстав проживання.
- **RU — REVISE:** Заявления на CUKR принимают онлайн через MOS с 4 мая 2026 года по 4 марта 2027 года. Своевременное заявление может обеспечивать законность пребывания до получения карты или окончательного решения об отказе/прекращении; сама подача не предоставляет разрешение на пребывание. Эта специальная процедура относится к CUKR, а не ко всем основаниям проживания.
- **Disposition:** REVISE.

## 07. Compare.Topic.FindingEmployment / `employment-vacancies`

- Production state: `Approved` / available `True`
- Final source bindings: 657597f7-ecb6-5b19-a33d-0ddb81b5eb2f
- **EN — KEEP:** Use ePraca to search for vacancies, internships, work placements and job-related events or training.
- **UK — KEEP:** Використовуйте ePraca для пошуку вакансій, стажувань, практики та заходів чи навчання, пов’язаних із роботою.
- **RU — KEEP:** Используйте ePraca для поиска вакансий, стажировок, практики и связанных с работой мероприятий или обучения.
- **Disposition:** KEEP.

## 08. Compare.Topic.FindingEmployment / `employment-institutions`

- Production state: `Approved` / available `True`
- Final source bindings: 178079f9-2746-5728-a86c-dc1877b2d3b8
- **EN — KEEP:** Contact the local powiat employment office (PUP) about counselling, training or job matching. The available service and access conditions must be confirmed with that PUP.
- **UK — KEEP:** Зверніться до місцевого повітового управління праці (PUP) щодо консультацій, навчання чи підбору роботи. Доступну послугу та умови доступу слід уточнити в цьому PUP.
- **RU — KEEP:** Обратитесь в местное повятовое управление труда (PUP) по вопросам консультаций, обучения или подбора работы. Доступную услугу и условия доступа нужно уточнить в этом PUP.
- **Disposition:** KEEP.

## 09. Compare.Topic.FindingEmployment / `employment-assistance-conditions`

- Production state: `Approved` / available `True`
- Final source bindings: 3736b420-8ed5-512c-bea4-db7096cbd752, 178079f9-2746-5728-a86c-dc1877b2d3b8
- **EN — KEEP:** PUP registration as unemployed or a jobseeker and access to each service depend on current rules and personal circumstances. The 2026 changes affected registration categories; ask the competent PUP for the applicable documents and conditions.
- **UK — KEEP:** Реєстрація в PUP як безробітного чи шукача роботи та доступ до кожної послуги залежать від чинних правил і обставин. Зміни 2026 року торкнулися категорій реєстрації; уточніть документи й умови у відповідному PUP.
- **RU — KEEP:** Регистрация в PUP как безработного или соискателя и доступ к каждой услуге зависят от действующих правил и обстоятельств. Изменения 2026 года затронули категории регистрации; уточните документы и условия в соответствующем PUP.
- **Disposition:** KEEP.

## 10. Compare.Topic.WorkAuthorizationRights / `work-permission`

- Production state: `Approved` / available `True`
- Final source bindings: cb8bb4e3-f65d-5a00-8b20-a541475a3050
- **EN — KEEP:** PIP says Ukrainian citizens with temporary protection have labour-market access. Lawful stay and the applicable status must be checked; other Ukrainian citizens require a separate check of their labour-market route.
- **UK — KEEP:** За інформацією PIP, громадяни України з тимчасовим захистом мають доступ до ринку праці. Потрібно перевірити законність перебування й відповідний статус; для інших громадян України підставу доступу до праці перевіряють окремо.
- **RU — KEEP:** По информации PIP, граждане Украины с временной защитой имеют доступ к рынку труда. Нужно проверить законность пребывания и соответствующий статус; для других граждан Украины основание доступа к труду проверяют отдельно.
- **Disposition:** KEEP.

## 11. Compare.Topic.WorkAuthorizationRights / `work-employer-duties`

- Production state: `Approved` / available `True`
- Final source bindings: cb8bb4e3-f65d-5a00-8b20-a541475a3050
- **EN — KEEP:** For the temporary-protection route, the employer must notify PUP through praca.gov.pl within 7 days after work starts. The agreement must be written before work, with a copy for the worker and an understandable written version before signature if the worker does not know Polish.
- **UK — KEEP:** За шляхом тимчасового захисту роботодавець має повідомити PUP через praca.gov.pl протягом 7 днів після початку роботи. Договір має бути письмовим до початку роботи, з копією для працівника та зрозумілою письмовою версією до підписання, якщо працівник не знає польської.
- **RU — KEEP:** По пути временной защиты работодатель должен уведомить PUP через praca.gov.pl в течение 7 дней после начала работы. Договор должен быть письменным до начала работы, с копией для работника и понятной письменной версией до подписания, если работник не знает польского.
- **Disposition:** KEEP.

## 12. Compare.Topic.WorkAuthorizationRights / `work-minimum-pay`

- Production state: `Approved` / available `True`
- Final source bindings: b93d9eab-8905-5847-9c80-2a3cbc5eca73, 451eb915-ff9b-5b88-8b12-ca173399213d, 3b1d42d4-1a47-5b88-80ae-09aae7cfe912
- **EN — REVISE:** The 2026 national minimum is PLN 4,806 per month and PLN 31.40 per hour for covered civil-law contracts. Enacted FUTURE rates from 1 January 2027 are PLN 4,950 per month and PLN 32.30 per hour for covered contracts. Part-month work and some contract types follow separate rules; compare gross pay and applicable hours, not only take-home pay. Source bindings: existing `b93d9eab-8905-5847-9c80-2a3cbc5eca73`; proposed 2026 regulation `451eb915-ff9b-5b88-8b12-ca173399213d`; proposed 2027 regulation `3b1d42d4-1a47-5b88-80ae-09aae7cfe912`.
- **UK — REVISE:** Загальнодержавна мінімальна зарплата у 2026 році — 4 806 злотих на місяць і 31,40 злотого за годину для охоплених цивільно-правових договорів. Ухвалені МАЙБУТНІ ставки з 1 січня 2027 року — 4 950 злотих на місяць і 32,30 злотого за годину для охоплених договорів. Для неповного місяця та деяких договорів діють окремі правила. Джерела: чинне `b93d9eab-8905-5847-9c80-2a3cbc5eca73`; запропоновані акти: 2026 рік `451eb915-ff9b-5b88-8b12-ca173399213d`, 2027 рік `3b1d42d4-1a47-5b88-80ae-09aae7cfe912`.
- **RU — REVISE:** Общегосударственная минимальная зарплата в 2026 году — 4 806 злотых в месяц и 31,40 злотого в час для охватываемых гражданско-правовых договоров. Принятые БУДУЩИЕ ставки с 1 января 2027 года — 4 950 злотых в месяц и 32,30 злотого в час для охватываемых договоров. Для неполного месяца и некоторых договоров действуют отдельные правила. Источники: действующее `b93d9eab-8905-5847-9c80-2a3cbc5eca73`; предлагаемые акты: 2026 год — `451eb915-ff9b-5b88-8b12-ca173399213d`, 2027 год — `3b1d42d4-1a47-5b88-80ae-09aae7cfe912`.
- **Disposition:** REVISE.

## 13. Compare.Topic.Healthcare / `healthcare-access`

- Production state: `Approved` / available `True`
- Final source bindings: 6dbd3c8d-ac7f-5211-8609-eea9edfecbce, 10831751-821e-5615-993d-d0b29f693361
- **EN — KEEP:** From 5 March 2026, limited free care under the PESEL UKR protection route covers specified groups: under-18s, pregnancy, childbirth or postnatal cases, torture or rape survivors, or holders of a collective-accommodation certificate. It does not cover every public service; insurance may provide another route.
- **UK — KEEP:** З 5 березня 2026 року обмежена безоплатна допомога за шляхом захисту з PESEL UKR охоплює визначені групи: осіб до 18 років, вагітних, осіб під час пологів або в післяпологовому періоді, постраждалих від катувань чи зґвалтування або власників довідки про колективне розміщення. Це не всі публічні послуги; інший шлях може давати страхування.
- **RU — KEEP:** С 5 марта 2026 года ограниченная бесплатная помощь по пути защиты с PESEL UKR охватывает определённые группы: лиц до 18 лет, беременных, лиц в период родов или в послеродовом периоде, пострадавших от пыток или изнасилования либо обладателей справки о коллективном размещении. Это не все публичные услуги; другой путь может давать страхование.
- **Disposition:** KEEP.

## 14. Compare.Topic.Healthcare / `healthcare-proof`

- Production state: `Approved` / available `True`
- Final source bindings: 6dbd3c8d-ac7f-5211-8609-eea9edfecbce
- **EN — KEEP:** Full insurance coverage may follow a qualifying employment contract, registered unemployment or lawful business activity. Other bases and exceptions depend on status and law; PESEL UKR alone does not establish the same coverage for everyone.
- **UK — KEEP:** Повне страхове покриття може випливати з відповідного трудового договору, реєстрації безробітним чи законної підприємницької діяльності. Інші підстави й винятки залежать від статусу та закону; сам PESEL UKR не встановлює однакового покриття для всіх.
- **RU — KEEP:** Полное страховое покрытие может следовать из соответствующего трудового договора, регистрации безработным или законной предпринимательской деятельности. Другие основания и исключения зависят от статуса и закона; сам PESEL UKR не устанавливает одинакового покрытия для всех.
- **Disposition:** KEEP.

## 15. Compare.Topic.Healthcare / `healthcare-urgent`

- Production state: `Approved` / available `True`
- Final source bindings: 7813be17-01bf-5f2a-86aa-ac013ea36b81
- **EN — KEEP:** Call 112 or 999 for an immediate threat to life. For an urgent problem outside POZ hours, contact the nearest night and holiday healthcare point (NiŚOZ); emergency and routine care routes are different.
- **UK — KEEP:** За безпосередньої загрози життю телефонуйте 112 або 999. Для термінової проблеми поза годинами POZ зверніться до найближчого пункту нічної та святкової допомоги (NiŚOZ); екстрена й планова допомога мають різні шляхи.
- **RU — KEEP:** При непосредственной угрозе жизни звоните 112 или 999. При срочной проблеме вне часов POZ обратитесь в ближайший пункт ночной и праздничной помощи (NiŚOZ); экстренная и плановая помощь имеют разные пути.
- **Disposition:** KEEP.

## 16. Compare.Topic.Housing / `housing-initial`

- Production state: `Approved` / available `True`
- Final source bindings: 80348417-6bc3-5c3b-90ec-f5a75b9092cd
- **EN — REVISE:** The transitional extension of the former Ukraine-specific collective-accommodation scheme ended on 30 June 2026. The new interventional stay-and-meal measure is no longer available for the Ukraine displacement because its 12-month statutory period from the 2022 EU mass-influx event has expired. Funded collective accommodation with meals may still be provided to people with UKR status who belong to a statutory vulnerable group and cannot house themselves. Eligibility and funding apply; PESEL UKR alone does not guarantee a place.
- **UK — REVISE:** Перехідне продовження колишньої спеціальної програми колективного проживання для людей з України завершилося 30 червня 2026 року. Нова інтервенційна допомога з проживанням і харчуванням для переміщених з України вже недоступна, оскільки законодавчий строк у 12 місяців від події масового напливу ЄС у 2022 році минув. За наявності фінансування колективне проживання з харчуванням може надаватися людям зі статусом UKR, які належать до законодавчо визначеної вразливої групи та не можуть самостійно забезпечити собі житло. Сам PESEL UKR не гарантує місце.
- **RU — REVISE:** Переходное продление прежней специальной программы коллективного проживания для людей из Украины завершилось 30 июня 2026 года. Новая интервенционная помощь с проживанием и питанием для перемещённых из Украины уже недоступна, поскольку установленный законом срок в 12 месяцев от события массового притока ЕС в 2022 году истёк. При наличии финансирования коллективное проживание с питанием может предоставляться людям со статусом UKR, которые относятся к установленной законом уязвимой группе и не могут самостоятельно обеспечить жильё. Сам PESEL UKR не гарантирует место.
- **Disposition:** REVISE.

## 17. Compare.Topic.Housing / `housing-private-rental`

- Production state: `Approved` / available `True`
- Final source bindings: 09cc8513-104c-5ced-b017-b64cf0e3ee9c, a5ebb19b-f5d4-5e72-9107-5e4f84445286
- **EN — KEEP:** Ask for a written agreement in Polish or a translation you understand. Check the landlord, rent, utilities, term, termination and deposit return; do not transfer money based only on an advertisement.
- **UK — KEEP:** Попросіть письмовий договір польською або зрозумілий вам переклад. Перевірте орендодавця, плату, комунальні витрати, строк, розірвання й повернення депозиту; не переказуйте гроші лише на підставі оголошення.
- **RU — KEEP:** Попросите письменный договор на польском или понятный вам перевод. Проверьте арендодателя, плату, коммунальные расходы, срок, расторжение и возврат депозита; не переводите деньги только на основании объявления.
- **Disposition:** KEEP.

## 18. Compare.Topic.Housing / `housing-financial-support`

- Production state: `Approved` / available `True`
- Final source bindings: 768aa9b7-06de-5926-9235-69fb3e537b8b
- **EN — KEEP:** Apply for housing allowance through OPS or the local authority. The office checks eligibility, income, floor area and housing expenses; the legal title to the dwelling and other conditions matter. An award and amount are not guaranteed.
- **UK — KEEP:** За житловою допомогою звертаються через OPS або місцеву владу. Орган перевіряє відповідність умовам, доходи, площу й житлові витрати; важливі правова підстава користування житлом та інші умови. Призначення й суму не гарантовано.
- **RU — KEEP:** За жилищной помощью обращаются через OPS или местную власть. Орган проверяет соответствие условиям, доходы, площадь и жилищные расходы; важны правовое основание пользования жильём и другие условия. Назначение и сумма не гарантированы.
- **Disposition:** KEEP.

## 19. Compare.Topic.EducationLanguage / `education-school-enrolment`

- Production state: `Approved` / available `True`
- Final source bindings: a646d0ee-81ba-5b10-b179-e09ca4ac3453, 1cf8737c-cef9-56ac-8c57-8984169ba13d
- **EN — KEEP:** Contact a public school near home with prior-school records if available. Ordinary admission does not require nostrification of those records; the headteacher determines the grade. Ask about missing records; some school types apply additional criteria.
- **UK — KEEP:** Зверніться до державної школи поблизу дому з документами попереднього навчання, якщо вони є. Для звичайного вступу їх нострифікація не потрібна; клас визначає директор. Уточніть порядок за відсутності документів; деякі типи шкіл мають додаткові критерії.
- **RU — KEEP:** Обратитесь в государственную школу рядом с домом с документами предыдущего обучения, если они есть. Для обычного поступления их нострификация не нужна; класс определяет директор. Уточните порядок при отсутствии документов; некоторые типы школ имеют дополнительные критерии.
- **Disposition:** KEEP.

## 20. Compare.Topic.EducationLanguage / `education-pupil-language`

- Production state: `Approved` / available `True`
- Final source bindings: 1cf8737c-cef9-56ac-8c57-8984169ba13d
- **EN — KEEP:** Pupils subject to the learning obligation with insufficient Polish are entitled to free additional Polish lessons for up to 24 months. A preparatory class may be organised; a teacher’s assistant knowing the pupil’s language may be employed for up to 12 months. Ask the school about implementation.
- **UK — KEEP:** Учні, на яких поширюється обов’язок навчання й які недостатньо знають польську, мають право на безоплатні додаткові уроки польської до 24 місяців. Можна організувати підготовчий клас; помічника вчителя, який знає мову учня, можна залучити до 12 місяців. Уточніть реалізацію в школі.
- **RU — KEEP:** Ученики, на которых распространяется обязанность обучения и которые недостаточно знают польский, имеют право на бесплатные дополнительные уроки польского до 24 месяцев. Можно организовать подготовительный класс; помощника учителя, знающего язык ученика, можно привлечь до 12 месяцев. Уточните реализацию в школе.
- **Disposition:** KEEP.

## 21. Compare.Topic.EducationLanguage / `education-higher-admission`

- Production state: `Approved` / available `True`
- Final source bindings: 7b72e7d2-6359-5eae-b2c9-68f2e9da7c80
- **EN — KEEP:** Check the university’s programme and language requirements and NAWA guidance on recognition of the school-leaving certificate. Recognition for admission does not guarantee acceptance; competition and other programme conditions can apply.
- **UK — KEEP:** Перевірте вимоги програми й мови в університеті та вказівки NAWA щодо визнання шкільного атестата. Визнання для вступу не гарантує зарахування; можливі конкурс та інші умови програми.
- **RU — KEEP:** Проверьте требования программы и языка в университете и указания NAWA по признанию школьного аттестата. Признание для поступления не гарантирует зачисления; возможны конкурс и другие условия программы.
- **Disposition:** KEEP.

## 22. Compare.Topic.SocialAssistance / `social-subsistence`

- Production state: `Approved` / available `True`
- Final source bindings: 83e84de8-10ef-5176-b863-50a2fe6780b8, 131a629b-e3ba-54a3-932b-2cddd661ff40
- **EN — KEEP:** General social assistance has statutory residence/status and need conditions. PESEL UKR alone does not establish entitlement to a payment; the local OPS/MOPS assesses the household’s circumstances under the applicable rules.
- **UK — KEEP:** Загальна соціальна допомога має законодавчі умови щодо проживання, статусу й потреби. Сам PESEL UKR не встановлює права на виплату; місцевий OPS/MOPS оцінює обставини домогосподарства за відповідними правилами.
- **RU — KEEP:** Общая социальная помощь имеет законодательные условия проживания, статуса и нуждаемости. Сам PESEL UKR не устанавливает права на выплату; местный OPS/MOPS оценивает обстоятельства домохозяйства по соответствующим правилам.
- **Disposition:** KEEP.

## 23. Compare.Topic.SocialAssistance / `social-application`

- Production state: `Approved` / available `True`
- Final source bindings: 83e84de8-10ef-5176-b863-50a2fe6780b8
- **EN — KEEP:** General social-assistance requests go to local OPS/MOPS. Prepare identity, residence, income, household and need-related documents and request an assessment; requirements depend on the case.
- **UK — KEEP:** За загальною соціальною допомогою звертаються до місцевого OPS/MOPS. Підготуйте документи особи, проживання, доходів, домогосподарства й потреби та попросіть оцінку; вимоги залежать від справи.
- **RU — KEEP:** За общей социальной помощью обращаются в местный OPS/MOPS. Подготовьте документы личности, проживания, доходов, домохозяйства и нуждаемости и попросите оценку; требования зависят от дела.
- **Disposition:** KEEP.

## 24. Compare.Topic.SocialAssistance / `social-child-benefits`

- Production state: `Approved` / available `True`
- Final source bindings: ac033b42-ecb0-5eb4-b08d-9af621e98693, 33354082-99fb-5d3a-97af-92c825f960e3
- **EN — REVISE:** 800+ is PLN 800 per eligible child per month and requires the applicable residence/status conditions and the child living with a parent in Poland; the child must fulfil the applicable Polish education obligation: annual preschool preparation, compulsory schooling or the learning obligation, as applicable, subject to statutory exceptions. For Ukrainian citizens with UKR status, a work-activity condition applies from 1 February 2026 with statutory exceptions. A registered unemployed parent with health insurance through registration may qualify for 3 months, or 6 months when raising at least three children. ZUS checks the individual case.
- **UK — REVISE:** 800+ становить 800 злотих на місяць на дитину, яка відповідає умовам, і вимагає відповідних умов проживання/статусу та проживання дитини з одним із батьків у Польщі; дитина має виконувати відповідний обов’язок у польській системі освіти: річну дошкільну підготовку, шкільний обов’язок або обов’язок навчання, залежно від віку та обставин, з урахуванням передбачених законом винятків. З 1 лютого 2026 року для громадян України зі статусом UKR діє умова професійної активності з винятками, встановленими законом. Зареєстрований безробітний з батьків із медичним страхуванням через реєстрацію може отримувати виплату протягом 3 місяців або 6 місяців, якщо виховує щонайменше трьох дітей. Справу перевіряє ZUS.
- **RU — REVISE:** 800+ составляет 800 злотых в месяц на ребёнка, отвечающего условиям, и требует соответствующих условий проживания/статуса и проживания ребёнка с одним из родителей в Польше; ребёнок должен выполнять применимую обязанность в польской системе образования: годичную дошкольную подготовку, школьную обязанность или обязанность обучения, в зависимости от возраста и обстоятельств, с учётом предусмотренных законом исключений. С 1 февраля 2026 года для граждан Украины со статусом UKR действует условие профессиональной активности с установленными законом исключениями. Зарегистрированный безработный родитель с медицинской страховкой через регистрацию может получать выплату 3 месяца или 6 месяцев, если воспитывает не менее трёх детей. Дело проверяет ZUS.
- **Disposition:** REVISE.
