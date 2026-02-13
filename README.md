# STAF.Playwright.Tests – Sample implementation

This repository is a **ready-to-run sample** for [STAF.Playwright](https://www.nuget.org/packages/STAF.Playwright) on NuGet. It shows how to use the framework with **MSTest** so you can clone, restore, and run tests with minimal setup.

## What you get

- **STAF.Playwright 2.0.0** – .NET test automation framework using Microsoft Playwright and MSTest  
- **BaseTest** – Browser/context/page setup and teardown, config-driven BaseUrl  
- **BasePage** – Page object base with automatic step reporting (`ClickAsync`, `EnterTextAsync`, `PressAsync`)  
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
| `STAF.Playwright.Tests/Tests/Test1.cs` | Sample test inheriting `BaseTest` |
| `STAF.Playwright.Tests/Pages/GooglePage.cs` | Sample page object inheriting `BasePage` |
| `STAF.Playwright.Tests/testsetting.runsettings` | BaseUrl, Browser, Headless, Environment, etc. |
| `STAF.Playwright.Tests/testdata.json` | Optional test data by environment (QA, UAT, …) |
| `MCPAgent/PlaywrightCSharpMcp.exe` | Playwright C# MCP server for code generation (see MCP section below) |
| `.cursor/mcp.json` | Cursor MCP config |
| `.vscode/mcp.json` | VS Code MCP config |
| `.mcp.json` | Visual Studio MCP config (solution root) |

## Configuration

- **Run settings** – Edit `testsetting.runsettings`: `BaseUrl`, `Browser` (Chrome, Firefox, Edge, Webkit), `Headless`, `Environment`, and other parameters.  
- **Environment variables** – Override any parameter with `STAF_` prefix (e.g. `STAF_BaseUrl`, `STAF_Headless=true`).  
- **Test data** – Use `testdata.json` and optional `testdata.{Environment}.json`; access via `ConfigManager.GetTestData(environment, section, key)`.

For full options and CI/CD usage, see the [STAF.Playwright NuGet README](https://www.nuget.org/packages/STAF.Playwright) and the framework’s CONFIGURATION documentation.

## MCP server (Playwright C# code generation)

This project includes **MCP (Model Context Protocol) server** configuration so you can use the **Playwright C# MCP server** to generate STAF.Playwright tests and page objects from **Cursor**, **VS Code**, or **Visual Studio** (Community/Professional).

The MCP executable is checked in under **`MCPAgent/`** so the same setup works for everyone after a clone—no environment variables or path edits required.

### What you need

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

### Updating the MCP executable

To update to a newer build of the Playwright C# MCP server, replace **`MCPAgent/PlaywrightCSharpMcp.exe`** with the new executable (e.g. from your MCP project’s `bin/Debug/net8.0/` or `bin/Release/net8.0/` output) and commit the change. The config files point at the exe under `MCPAgent/`, so no config edits are needed unless you use a different path.

### Project layout (MCP)

| Path | Purpose |
|------|--------|
| `MCPAgent/PlaywrightCSharpMcp.exe` | Checked-in Playwright C# MCP server executable. |
| `.cursor/mcp.json` | Cursor: project MCP config. |
| `.vscode/mcp.json` | VS Code: workspace MCP config. |
| `.mcp.json` | Visual Studio: solution MCP config (solution root). |

## Sample test and page

- **Test** – `Test1` inherits `BaseTest`, uses `Page` and `ConfigManager`, and creates a `GooglePage` to verify the page and run a search.  
- **Page** – `GooglePage` inherits `BasePage`, uses locators and `EnterTextAsync` / `PressAsync` (with automatic reporting), and uses `HtmlResult.TC_ResultCreation` for a custom verification step.

You can add more tests and page objects following the same pattern.

## License

Same as STAF.Playwright (MIT). See [LICENSE.txt](LICENSE.txt).
