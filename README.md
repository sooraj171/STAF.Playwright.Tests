# STAF.Playwright.Tests

**Sample implementation** of the [STAF.Playwright](https://www.nuget.org/packages/STAF.Playwright) framework — a .NET test automation framework for web and API testing using **Microsoft Playwright** and **MSTest**. This repository provides a ready-to-run project so you can clone, restore, and execute tests with minimal setup.

---

## STAF.Playwright framework

This project implements and demonstrates the **[STAF.Playwright](https://www.nuget.org/packages/STAF.Playwright)** NuGet package:

- **NuGet:** [https://www.nuget.org/packages/STAF.Playwright](https://www.nuget.org/packages/STAF.Playwright)
- **Install:** `dotnet add package STAF.Playwright`

STAF (Simple Test Automation Framework) provides base classes, page object support, HTML reporting with screenshots, and configuration for multi-environment test runs. It targets **.NET 10** and uses **MSTest** and **Microsoft Playwright**.

---

## What this sample includes

- **UI tests** – `BaseTest` and `BasePage` (browser setup, page objects, step reporting)
- **API tests** – `TestBaseAPI` and `ApiClient` with a free GET API sample (JSONPlaceholder)
- **Contract tests** – OpenAPI contract validation via `OpenApiContractTestBase` and specs in `OpenAPI/`
- **Excel** – `ExcelDriver` sample (create, read, write, compare workbooks)
- **HTML reports** – Per-test and final aggregated report (`ResultTemplateFinal.html`) in `TestResults`, with screenshots on failure
- **Configuration** – `testsetting.runsettings` and optional `testdata.json` with environment support

---

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- **Chrome or Edge** (or another Chromium-based browser) installed on your machine. This project uses the **browser already on your system** (via Playwright’s channel support); you do **not** need to run `playwright.ps1 install`.  
  If you later switch configuration to use Playwright’s bundled Chromium, Firefox, or WebKit, see [Playwright browsers](https://playwright.dev/dotnet/docs/browsers) for installation.

---

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

   Or from the test project:

   ```bash
   cd STAF.Playwright.Tests
   dotnet test --settings testsetting.runsettings
   ```

3. **View results**  
   HTML reports (including `ResultTemplateFinal.html`) and screenshots are written under `TestResults` in the test project output (e.g. `STAF.Playwright.Tests/bin/Debug/net10.0/TestResults`).

---

## Project layout

| Path | Purpose |
|------|--------|
| `STAF.Playwright.Tests/` | Test project |
| `STAF.Playwright.Tests/AssemblyInit.cs` | Assembly init/cleanup; delegates to framework for final HTML report |
| `STAF.Playwright.Tests/Tests/Test1.cs` | UI sample test inheriting `BaseTest` |
| `STAF.Playwright.Tests/Tests/ApiTests.cs` | API sample (TestBaseAPI, ApiClient) – JSONPlaceholder GET |
| `STAF.Playwright.Tests/Tests/ContractTests.cs` | OpenAPI contract tests (OpenApiContractTestBase) |
| `STAF.Playwright.Tests/Tests/Excel/ExcelDriverSampleTests.cs` | ExcelDriver create/read/compare samples |
| `STAF.Playwright.Tests/Pages/GooglePage.cs` | Sample page object inheriting `BasePage` |
| `STAF.Playwright.Tests/OpenAPI/placeholder.json` | OpenAPI spec for contract tests (JSONPlaceholder) |
| `STAF.Playwright.Tests/testsetting.runsettings` | BaseUrl, ApiBaseUrl, Browser, Headless, Environment, etc. |
| `STAF.Playwright.Tests/testdata.json` | Optional test data by environment (QA, UAT, …) |
| `MCPAgent/` | Playwright C# MCP server (included for use with Cursor, VS Code, or Visual Studio — see [Using the MCP agent](#using-the-mcp-agent)) |
| `.cursor/mcp.json` | Cursor MCP config |
| `.cursor/rules/staf-playwright-framework.mdc` | Cursor rules for STAF Playwright (base classes, page objects, tool usage) |
| `.github/copilot-instructions.md` | GitHub Copilot / agent instructions for this repo |
| `.vscode/mcp.json` | VS Code MCP config |
| `.mcp.json` | Visual Studio MCP config (solution root) |

---

## Configuration

- **Run settings** – Edit `testsetting.runsettings`: `BaseUrl`, `ApiBaseUrl`, `Browser` (Chrome, Firefox, Edge, Webkit), `Headless`, `Environment`, and other parameters.
- **Environment variables** – Override any parameter with the `STAF_` prefix (e.g. `STAF_BaseUrl`, `STAF_Headless=true`).
- **Test data** – Use `testdata.json` and optional `testdata.{Environment}.json`; access via `ConfigManager.GetTestData(environment, section, key)`.

For full options and CI/CD usage, see the [STAF.Playwright NuGet page](https://www.nuget.org/packages/STAF.Playwright) and the framework’s configuration documentation.

---

## Using the MCP agent

This repo is configured for the **Playwright C# MCP server**, so you can use AI-assisted development in **Cursor**, **VS Code**, or **Visual Studio** to generate and refine STAF.Playwright tests and page objects. The MCP server is included under **`MCPAgent/`**—clone the repo and open it in your editor; no extra setup is required.

### What you need

- **.NET 10 SDK** and one of: **Cursor**, **VS Code** (with [GitHub Copilot](https://code.visualstudio.com/docs/copilot/setup)), or **Visual Studio 2022** (17.14+ with [GitHub Copilot](https://learn.microsoft.com/en-us/visualstudio/ide/visual-studio-github-copilot-chat)).
- Open this repository as the workspace (Cursor/VS Code: **File → Open Folder** → repo root; Visual Studio: **File → Open → Project/Solution** → `STAF.Playwright.Tests.sln`).

### How to use the MCP agent

- **Cursor**  
  Open the repo as the workspace, then go to **Cursor Settings → Features → MCP** and ensure the project MCP is enabled. The Playwright C# tools appear in the AI/composer; use them to generate or refine tests and page objects.

- **VS Code**  
  Open the repo as the workspace and ensure [GitHub Copilot](https://code.visualstudio.com/docs/copilot/setup) is set up. First time you use the MCP server, trust it when prompted. In Chat, enable the **playwrightCsharp** tools and use Copilot (e.g. Agent mode) to generate or refine STAF.Playwright tests and page objects.

- **Visual Studio**  
  Open `STAF.Playwright.Tests.sln`. Visual Studio picks up `.mcp.json` at the solution root. In the **GitHub Copilot** chat, switch to **Agent** mode, enable the **playwrightCsharp** tools, then ask Copilot to generate or refine tests or page objects (approve tool use when prompted). If the server does not start, set the `"command"` in `.mcp.json` to the full path to `MCPAgent/PlaywrightCSharpMcp.exe`. See [Use MCP servers in Visual Studio](https://learn.microsoft.com/en-us/visualstudio/ide/mcp-servers).

### AI instructions and rules

So that generated code follows STAF.Playwright patterns (base classes, page objects, reporting), the repo includes:

- **Cursor:** [.cursor/rules/staf-playwright-framework.mdc](.cursor/rules/staf-playwright-framework.mdc) — applied automatically.
- **VS Code (GitHub Copilot):** [.github/copilot-instructions.md](.github/copilot-instructions.md) — repo-level guidance.

No extra configuration is needed; the AI and MCP agent use these when working in this project.

---

## Sample tests (all STAF.Playwright features)

- **UI** – `Test1` inherits `BaseTest`, uses `Page` and `ConfigManager`, and `GooglePage` (BasePage) for search.
- **API** – `ApiTests` inherits `TestBaseAPI`, uses `ApiClient.GetAsync` against [JSONPlaceholder](https://jsonplaceholder.typicode.com); set `ApiBaseUrl` in runsettings.
- **Contract** – `ContractTests` inherits `OpenApiContractTestBase`, validates endpoints against `OpenAPI/placeholder.json` (status code and optional schema).
- **Excel** – `ExcelDriverSampleTests` uses `ExcelDriver` to create workbooks, set/get cells, save, open, and compare files (temp directory, no external files).

You can add more tests and page objects following the same patterns.

---

## License

This project is licensed under the **MIT License**.

See [LICENSE.txt](LICENSE.txt) for the full text.

---

## Author and repository

- **Author:** Sooraj Ramachandran  
- **Repository:** [GitHub – STAF.Playwright.Tests](https://github.com/sooraj171/STAF.Playwright.Tests)  
- **Framework (NuGet):** [STAF.Playwright](https://www.nuget.org/packages/STAF.Playwright)

Contributions and issues are welcome.
