# Refugee Navigator Europe

An open-source web application intended to help Ukrainians under temporary protection navigate essential information about living in European countries.

## Status

**Initial development — Phase 0: repository initialization.** Application functionality has not yet been implemented. The repository contains a minimal .NET solution, an empty Blazor placeholder page, and one infrastructure smoke test.

## Project goals

- Make essential information easier to find and navigate.
- Start with Denmark, Germany, and Poland; country content is not yet available.
- Plan coverage of temporary protection, employment, healthcare, housing, social assistance, and education.
- Build a maintainable, portfolio-quality open-source application.

## Planned technology stack

- .NET 10 LTS and C#
- ASP.NET Core and Blazor Web App
- xUnit
- Built-in dependency injection

Future infrastructure will be selected when needed.

## Local development

Install the .NET 10 SDK, then run from the repository root:

```sh
dotnet restore RefugeeNavigatorEurope.slnx
dotnet build RefugeeNavigatorEurope.slnx --configuration Release --no-restore
dotnet test RefugeeNavigatorEurope.slnx --configuration Release --no-build
```

To run the empty web placeholder:

```sh
dotnet run --project src/RefugeeNavigator.Web
```

## Repository structure

```text
RefugeeNavigatorEurope/
├── src/
│   └── RefugeeNavigator.Web/
├── tests/
│   └── RefugeeNavigator.Tests/
├── docs/
│   └── CONTRIBUTING.md
├── .github/
│   └── workflows/
│       └── ci.yml
├── .editorconfig
├── .gitignore
├── LICENSE
├── README.md
└── RefugeeNavigatorEurope.slnx
```

## Contributing

See [contribution guidelines](docs/CONTRIBUTING.md). Discuss new functionality in an issue before implementation.

## License

[MIT](LICENSE).
