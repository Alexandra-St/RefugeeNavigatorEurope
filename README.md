# Refugee Navigator Europe

An open-source web application helping Ukrainians under temporary protection navigate essential information about living in European countries.

## Status

**Active development.** Country and category navigation is data-driven, and the interface is available in Ukrainian, English, and Russian. Information content and official source references are loaded from validated JSON catalogs. The application currently includes one published Denmark employment article in all three languages, with its supporting sources and translation review status.

Search and country comparison are under development. Their current pages are previews only; neither feature is functional yet. The private alpha has not been deployed. A public beta is planned for a later phase.

## Current scope

- Countries: Denmark, Germany, and Poland.
- Categories: temporary protection, employment, healthcare, housing, education, and social assistance.
- Published content is organized by country and category and includes language-specific review status and official source check dates.
- Supported interface and content languages: Ukrainian (`uk`), English (`en`), and Russian (`ru`).
- Search and country comparison are marked as previews and do not provide results.

## Technology

- .NET 10 and C#
- ASP.NET Core Blazor Web App
- JSON catalogs with startup validation
- Built-in dependency injection
- xUnit tests

## Local development

Install the .NET 10 SDK. From the repository root, restore, build, and run the tests:

```sh
dotnet restore RefugeeNavigatorEurope.slnx
dotnet build RefugeeNavigatorEurope.slnx --configuration Release --no-restore
dotnet test RefugeeNavigatorEurope.slnx --configuration Release --no-build
```

Run the web application with:

```sh
dotnet run --project src/RefugeeNavigator.Web
```

Open the local address printed by the command. Catalog data is in `src/RefugeeNavigator.Web/Data/`; invalid data or missing required translations prevents startup.

## Repository structure

```text
RefugeeNavigatorEurope/
├── src/
│   ├── RefugeeNavigator.Domain/  # Domain models and validation
│   ├── RefugeeNavigator.Data/    # JSON catalog loading and validation
│   └── RefugeeNavigator.Web/     # Blazor application, localization, and JSON catalogs
├── tests/                        # xUnit tests
├── docs/                         # Contribution and content publishing guidance
├── .github/workflows/            # GitHub Actions CI
└── RefugeeNavigatorEurope.slnx
```

## Contributing

See [contribution guidelines](docs/CONTRIBUTING.md). For research, source verification, translation, and publication requirements, see the [content publishing workflow](docs/CONTENT_WORKFLOW.md).

## License

[MIT](LICENSE).
