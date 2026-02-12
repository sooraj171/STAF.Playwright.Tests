# STAF.Playwright.Tests – Sample implementation

This repository is a **ready-to-run sample** for [STAF.Playwright](https://www.nuget.org/packages/STAF.Playwright) on NuGet. It shows how to use the framework with **MSTest** so you can clone, restore, and run tests with minimal setup.

## What you get

- **STAF.Playwright 2.0.0** – .NET test automation framework using Microsoft Playwright and MSTest  
- **UI tests** – `BaseTest` and `BasePage` (browser setup, page objects, step reporting)  
- **API tests** – `TestBaseAPI` and `ApiClient` with a free GET API sample (JSONPlaceholder)  
- **Contract tests** – OpenAPI contract validation via `OpenApiContractTestBase` and specs in `OpenAPI/`  
- **Excel** – `ExcelDriver` sample (create, read, write, compare workbooks)  
- **HTML reports** – In `TestResults` with screenshots on failure  
- **Configuration** – `testsetting.runsettings` and optional `testdata.json` with environment support  

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
- [Playwright browsers](https://playwright.dev/dotnet/docs/browsers) installed once (from repo root after `dotnet build`):

  ```powershell
  pwsh STAF.Playwright.Tests/bin/Debug/net8.0/playwright.ps1 install
  ```

  Or from the test project directory: `dotnet build` then `pwsh bin/Debug/net8.0/playwright.ps1 install`.

## Quick start

1. **Clone and restore**

   ```bash
   git clone https://github.com/sooraj171/STAF.Playwright.Tests.git
   cd STAF.Playwright.Tests
   dotnet restore
   ```

2. **Run tests**

   ```bash
   dotnet test --settings STAF.Playwright.Tests/testsetting.runsettings
   ```

   Or run from the test project:

   ```bash
   cd STAF.Playwright.Tests
   dotnet test --settings testsetting.runsettings
   ```

3. **View results**  
   HTML reports and screenshots are written under `TestResults` (e.g. in the project or output directory).

## Project layout

| Path | Purpose |
|------|--------|
| `STAF.Playwright.Tests/` | Test project |
| `STAF.Playwright.Tests/Tests/Test1.cs` | UI sample test inheriting `BaseTest` |
| `STAF.Playwright.Tests/Tests/ApiTests.cs` | API sample (TestBaseAPI, ApiClient) – JSONPlaceholder GET |
| `STAF.Playwright.Tests/Tests/ContractTests.cs` | OpenAPI contract tests (OpenApiContractTestBase) |
| `STAF.Playwright.Tests/Tests/Excel/ExcelDriverSampleTests.cs` | ExcelDriver create/read/compare samples |
| `STAF.Playwright.Tests/Pages/GooglePage.cs` | Sample page object inheriting `BasePage` |
| `STAF.Playwright.Tests/OpenAPI/placeholder.json` | OpenAPI spec for contract tests (JSONPlaceholder) |
| `STAF.Playwright.Tests/testsetting.runsettings` | BaseUrl, ApiBaseUrl, Browser, Headless, Environment, etc. |
| `STAF.Playwright.Tests/testdata.json` | Optional test data by environment (QA, UAT, …) |

## Configuration

- **Run settings** – Edit `testsetting.runsettings`: `BaseUrl`, `Browser` (Chrome, Firefox, Edge, Webkit), `Headless`, `Environment`, and other parameters.  
- **Environment variables** – Override any parameter with `STAF_` prefix (e.g. `STAF_BaseUrl`, `STAF_Headless=true`).  
- **Test data** – Use `testdata.json` and optional `testdata.{Environment}.json`; access via `ConfigManager.GetTestData(environment, section, key)`.

For full options and CI/CD usage, see the [STAF.Playwright NuGet README](https://www.nuget.org/packages/STAF.Playwright) and the framework’s CONFIGURATION documentation.

## Sample tests (covers all STAF.Playwright features)

- **UI** – `Test1` inherits `BaseTest`, uses `Page` and `ConfigManager`, and `GooglePage` (BasePage) for search.  
- **API** – `ApiTests` inherits `TestBaseAPI`, uses `ApiClient.GetAsync` against [JSONPlaceholder](https://jsonplaceholder.typicode.com); set `ApiBaseUrl` in runsettings.  
- **Contract** – `ContractTests` inherits `OpenApiContractTestBase`, validates endpoints against `OpenAPI/placeholder.json` (status code and optional schema).  
- **Excel** – `ExcelDriverSampleTests` uses `ExcelDriver` to create workbooks, set/get cells, save, open, and compare files (temp directory, no external files).

You can add more tests and page objects following the same patterns.

## License

Same as STAF.Playwright (MIT). See [LICENSE.txt](LICENSE.txt).
