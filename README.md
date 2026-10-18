# Refugee Navigator Europe

An open-source application helping Ukrainians under temporary protection navigate practical information about living in Europe.

## Private Beta

The restricted Private Beta is live. The current content covers Denmark, Germany, and Poland: **24 articles with 72 translations marked Verified** in Ukrainian, English, and Russian. The application includes search and the structured Country Comparison V2, alongside expandable full-article comparisons.

“Verified” means an article translation has passed the project's editorial review against its cited sources; it is not a guarantee of permanent legal accuracy. Access remains limited to Private Beta users. This repository does not contain access credentials.

## Current scope

- Countries: Denmark, Germany, and Poland.
- Categories: temporary protection, employment, healthcare, housing, education, and social assistance.
- Supported interface and content languages: Ukrainian (`uk`), English (`en`), and Russian (`ru`).
- Search supports article content and country/category filtering.
- Country Comparison V2 presents structured, source-linked facts first and keeps the full article comparison available in a collapsed section.
- Approved interactive Temporary Protection guides are available for Germany, Denmark, and Poland.
- Articles, translations, official sources, and comparison facts are stored in local JSON catalogs and validated at startup.

## Technology

- .NET 10, C#, ASP.NET Core Blazor Web App
- JSON catalogs with startup validation
- Built-in dependency injection
- xUnit automated tests
- A local console tool for source freshness scheduling and content dependency reports

## Local development

Install the .NET 10 SDK. From the repository root:

```sh
dotnet restore RefugeeNavigatorEurope.slnx
dotnet build RefugeeNavigatorEurope.slnx --configuration Release --no-restore
dotnet test RefugeeNavigatorEurope.slnx --configuration Release --no-build
```

Run the web application locally:

```sh
dotnet run --project src/RefugeeNavigator.Web
```

The console maintenance audit is read-only and never checks official pages or changes catalog statuses or timestamps. See [Content maintenance audit](docs/features/CONTENT_MAINTENANCE_AUDIT.md) for usage and limitations.

Catalogs are in `src/RefugeeNavigator.Web/Data/`. Invalid catalog data or missing required translations prevents the web application from starting.

## Repository structure

```text
RefugeeNavigatorEurope/
├── src/
│   ├── RefugeeNavigator.Domain/  # Domain models and validation
│   ├── RefugeeNavigator.Data/    # JSON catalog loading and validation
│   └── RefugeeNavigator.Web/     # Blazor application, localization, and catalogs
├── tools/
│   └── RefugeeNavigator.ContentAudit/  # Read-only editorial maintenance report
├── tests/                        # xUnit tests
├── docs/                         # Editorial, feature, and contribution guidance
├── .github/workflows/            # GitHub Actions CI
└── RefugeeNavigatorEurope.slnx
```

## Contributing

See [contribution guidelines](docs/CONTRIBUTING.md). Research, source verification, translation, and publication follow the [content publishing workflow](docs/CONTENT_WORKFLOW.md).

## License

[MIT](LICENSE).
