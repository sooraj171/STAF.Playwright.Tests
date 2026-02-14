# STAF.Playwright.Tests

**Sample implementation** of the [STAF.Playwright](https://www.nuget.org/packages/STAF.Playwright) framework — a .NET test automation framework for web and API testing using **Microsoft Playwright** and **MSTest**. This repository provides a ready-to-run project so you can clone, restore, and execute tests with minimal setup.

---

## STAF.Playwright framework

This project implements and demonstrates the **[STAF.Playwright](https://www.nuget.org/packages/STAF.Playwright)** NuGet package:

- **NuGet:** [https://www.nuget.org/packages/STAF.Playwright](https://www.nuget.org/packages/STAF.Playwright)
- **Install:** `dotnet add package STAF.Playwright`

STAF (Simple Test Automation Framework) provides base classes, page object support, HTML reporting with screenshots, and configuration for multi-environment test runs. It targets **.NET 8** and uses **MSTest** and **Microsoft Playwright**.

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

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Playwright browsers](https://playwright.dev/dotnet/docs/browsers) installed once (from repo root after `dotnet build`):

  ```powershell
  pwsh STAF.Playwright.Tests/bin/Debug/net8.0/playwright.ps1 install
  ```

  Or from the test project directory: `dotnet build` then `pwsh bin/Debug/net8.0/playwright.ps1 install`.

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
   HTML reports (including `ResultTemplateFinal.html`) and screenshots are written under `TestResults` in the test project output (e.g. `STAF.Playwright.Tests/bin/Debug/net8.0/TestResults`).

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
| `MCPAgent/PlaywrightCSharpMcp.exe` | Playwright C# MCP server for code generation (see MCP section below) |
| `.cursor/mcp.json` | Cursor MCP config |
| `.vscode/mcp.json` | VS Code MCP config |
| `.mcp.json` | Visual Studio MCP config (solution root) |

---

## Configuration

- **Run settings** – Edit `testsetting.runsettings`: `BaseUrl`, `ApiBaseUrl`, `Browser` (Chrome, Firefox, Edge, Webkit), `Headless`, `Environment`, and other parameters.
- **Environment variables** – Override any parameter with the `STAF_` prefix (e.g. `STAF_BaseUrl`, `STAF_Headless=true`).
- **Test data** – Use `testdata.json` and optional `testdata.{Environment}.json`; access via `ConfigManager.GetTestData(environment, section, key)`.

For full options and CI/CD usage, see the [STAF.Playwright NuGet page](https://www.nuget.org/packages/STAF.Playwright) and the framework’s configuration documentation.

---

## MCP server (Playwright C# code generation)

This project includes **MCP (Model Context Protocol) server** configuration so you can use the **Playwright C# MCP server** to generate STAF.Playwright tests and page objects from **Cursor**, **VS Code**, or **Visual Studio** (Community/Professional).

The MCP server is checked in under **`MCPAgent/`** so the same setup works for everyone after a clone—no environment variables or path edits required.

### What you need in `MCPAgent/`

The MCP server is a .NET 8 application. To run it, **the entire build output** must be in `MCPAgent/`, not just the exe. Copy everything from your MCP project’s `bin/Debug/net8.0/` (or `bin/Release/net8.0/`) into `MCPAgent/`:

| Required | Purpose |
|----------|--------|
| **PlaywrightCSharpMcp.exe** | Entry point. |
| **PlaywrightCSharpMcp.dll** | Main assembly. |
| **PlaywrightCSharpMcp.deps.json** | Dependency manifest (tells .NET which assemblies to load). |
| **PlaywrightCSharpMcp.runtimeconfig.json** | Runtime config (e.g. .NET 8). |
| **All `.dll` files** | Dependencies (e.g. `ModelContextProtocol.dll`, `Microsoft.Extensions.*.dll`, etc.). |
| **runtimes/** folder | Platform-specific assemblies (e.g. `win/`, `browser/`). |

Optional: `PlaywrightCSharpMcp.pdb` for debugging. If the server fails to start (e.g. in Visual Studio), ensure the full build output has been copied into `MCPAgent/`.

### What you need (editor)

- One of: **Cursor**, **VS Code** (with Copilot), or **Visual Studio 2022** (Community or Professional, version 17.14 or later with GitHub Copilot).
- The repo opened as the workspace/solution (the folder that contains `MCPAgent` and the solution file).

### Setup by editor

#### Cursor

1. **Clone the repo** and open this repository as the workspace (**File → Open Folder** → select the repo root).
2. **Enable the project MCP**  
   Go to **Cursor Settings → Features → MCP** (or **Tools & MCP**) and ensure the **project** MCP configuration is enabled so the `playwright-csharp` server is used.
3. **Use the MCP tools**  
   The Playwright C# MCP server will expose tools in the AI/composer. Use them to generate or refine tests and page objects.

Config: `.cursor/mcp.json` (uses `${workspaceFolder}/MCPAgent/PlaywrightCSharpMcp.exe`).

#### VS Code

1. **Clone the repo** and open this repository as the workspace (**File → Open Folder** → select the repo root).
2. **Install/use Copilot**  
   MCP in VS Code works with [GitHub Copilot](https://code.visualstudio.com/docs/copilot/setup). Ensure Copilot is set up for your workspace.
3. **Trust the MCP server (first time)**  
   When you use the MCP server for the first time, VS Code may prompt you to trust it. Confirm so the Playwright C# tools can run.
4. **Use the MCP tools**  
   In the Chat view, open the tool picker and enable the **playwrightCsharp** server’s tools. Use Copilot chat (e.g. Agent mode) to generate or refine STAF.Playwright tests and page objects.

Config: `.vscode/mcp.json` (uses `${workspaceFolder}/MCPAgent/PlaywrightCSharpMcp.exe`). You can also run **MCP: Open Workspace Folder Configuration** from the Command Palette to open it.

#### Visual Studio (Community or Professional)

1. **Prerequisites**  
   Visual Studio 2022 version **17.14** or later and [GitHub Copilot](https://learn.microsoft.com/en-us/visualstudio/ide/visual-studio-github-copilot-chat) for Visual Studio.
2. **Clone the repo** and open the solution (**File → Open → Project/Solution** → select `STAF.Playwright.Tests.sln`).
3. **Use the project MCP config**  
   Visual Studio discovers MCP config from the solution directory. This repo includes **`.mcp.json`** at the solution root with the `playwrightCsharp` server pointing at `MCPAgent/PlaywrightCSharpMcp.exe`. If the server does not start, edit `.mcp.json` and set `"command"` to the **full path** to `PlaywrightCSharpMcp.exe` (e.g. `C:/path/to/STAF.Playwright.Tests/MCPAgent/PlaywrightCSharpMcp.exe`).
4. **Switch to Agent mode and enable tools**  
   In the **GitHub Copilot** chat window, open the mode dropdown and select **Agent**. In the tool picker, enable the **playwrightCsharp** tools.
5. **Use the MCP tools**  
   Ask Copilot to generate or refine STAF.Playwright tests or page objects; when it uses a tool, approve the request if prompted.

Config: **`.mcp.json`** at the solution root (relative path `MCPAgent/PlaywrightCSharpMcp.exe`). You can add this file to **Solution Items** in Solution Explorer so it’s easy to find. See [Use MCP servers in Visual Studio](https://learn.microsoft.com/en-us/visualstudio/ide/mcp-servers) for more.

### Updating the MCP server

To update to a newer build of the Playwright C# MCP server, **copy the entire contents** of your MCP project’s `bin/Debug/net8.0/` (or `bin/Release/net8.0/`) into **`MCPAgent/`**—all `.exe`, `.dll`, `.json`, and the `runtimes/` folder. Do not copy only the exe; the app needs its dependencies to run. Then commit the changes. The config files point at `MCPAgent/PlaywrightCSharpMcp.exe`, so no config edits are needed.

### Project layout (MCP)

| Path | Purpose |
|------|--------|
| `MCPAgent/` | Full build output of Playwright C# MCP server (exe, dlls, deps.json, runtimeconfig.json, runtimes/). |
| `.cursor/mcp.json` | Cursor: project MCP config. |
| `.vscode/mcp.json` | VS Code: workspace MCP config. |
| `.mcp.json` | Visual Studio: solution MCP config (solution root). |

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
