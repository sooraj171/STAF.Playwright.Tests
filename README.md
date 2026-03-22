# STAF.Playwright.Tests

**Sample implementation** of the [STAF.Playwright](https://www.nuget.org/packages/STAF.Playwright) framework — a .NET test automation framework for web and API testing using **Microsoft Playwright** and **MSTest**. This repository provides a ready-to-run project so you can clone, restore, and execute tests with minimal setup.

**Using AI to write tests?** See [AI-assisted automation (copy-paste prompts)](#ai-assisted-automation-copy-paste-prompts) for `@`/`attach` bundles and ready-made prompts.

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
- **AI QA Orchestrator (work items)** – STLC-aligned playbook and phase markdown reports under `QA/work-items/` (Azure DevOps / Jira via MCP when configured, or pasted requirements). See [Work-item / PBI QA (orchestrator)](#work-item--pbi-qa-orchestrator).

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

## AI-assisted automation (copy-paste prompts)

Use this when you want **Cursor**, **VS Code (Copilot)**, or **Visual Studio (Copilot)** to generate **STAF.Playwright** tests and page objects without hunting for file paths.

### 1. Open the right folder

Open the **repository root** (the folder that contains `STAF.Playwright.Tests.sln`, `AI/`, and `.cursor/`) as your workspace. Paths below are relative to that root.

### 2. Give the model the handbook (recommended)

| Editor | What to do |
|--------|------------|
| **Cursor** | In **Chat** or **Composer**, type **`@`** and add **`AI/instructions/system-prompt.md`**, **`AI/instructions/generation-rules.md`**, plus the skill(s) you need (e.g. **`@AI/skills/ui-testing.md`**). For PBIs/User Stories end-to-end, add **`@.cursor/skills/staf-qa-orchestrator/SKILL.md`** or **`AI/instructions/qa-orchestrator-lifecycle.md`**. Optional: enable **project Skills** under `.cursor/skills/` — they only *point* at the same `AI/` files (no duplicate rules). |
| **VS Code** | In **Copilot Chat**, use **Add context** / attach files. Start from **[`.vscode/staf-ai/INDEX.md`](.vscode/staf-ai/INDEX.md)** to see the full list, then attach the same `AI/instructions/*.md` and `AI/skills/*.md` files. Repo-wide behavior is also in **[`.github/copilot-instructions.md`](.github/copilot-instructions.md)**. |
| **Visual Studio** | Use Copilot **Agent** mode with the Playwright MCP tools if enabled; attach or paste paths to the `AI/` files when the chat supports context. |

**Suggested bundles**

- **UI automation:** `AI/instructions/system-prompt.md`, `AI/instructions/generation-rules.md`, `AI/skills/ui-testing.md`, `AI/skills/reporting.md`, `AI/skills/test-data.md`, `AI/skills/framework-rules.md`
- **API automation:** same instructions + `AI/skills/api-testing.md`, `AI/skills/test-data.md`, `AI/skills/framework-rules.md`
- **After a failure:** `AI/instructions/debugging-rules.md` + the skill for the layer that failed (e.g. `AI/skills/ui-testing.md`)
- **Work item / PBI (STLC reports):** `AI/instructions/qa-orchestrator-lifecycle.md`, `AI/instructions/work-item-report-templates.md`, `AI/skills/qa-orchestrator.md` — Cursor: `@.cursor/skills/staf-qa-orchestrator/SKILL.md`. Optionally configure an **Azure DevOps** or **Jira** MCP server in Cursor/VS Code so the agent can fetch the item; if not available, paste the description and acceptance criteria.

### 3. Copy a sample prompt (fill in the brackets)

**New UI flow (Page Object + test)**

```text
You are working in the STAF.Playwright.Tests repository. Follow the attached AI/instructions and AI/skills files exactly.

Feature / user story:
[Describe the screen, role, and what the user should achieve—e.g. login, submit form, verify message.]

Requirements:
- UI test class: inherit BaseTest from STAF.Playwright.Framework; use ConfigManager for URLs and parameters (testsetting.runsettings), no hardcoded environments.
- Page Object: under Pages/, inherit BasePage; all ILocator and interactions live there; use WaitForElementVisibleAsync, EnterTextAsync, PressAsync, and ReportResult for steps.
- The test method must NOT use raw Playwright (no Page.Locator / Click / Fill in the test class)—only call page object methods.
- Add MSTest assertions and reporting for important steps.
- If you add new URLs, keys, or test data, update testsetting.runsettings and/or testdata.json and show the code that reads them via ConfigManager.

Deliver: (1) Page class(es) as needed (2) Test class under Tests/ (3) config/test data updates (4) short comments only where logic is non-obvious.
```

**New API tests**

```text
Repository: STAF.Playwright.Tests. Follow the attached AI handbook files.

API scenario:
[Describe endpoints, methods, auth, expected status codes, and what to assert in the body.]

Requirements:
- Test class inherits TestBaseAPI; use ApiClient and ReportResultAPI from the framework—no ad-hoc HttpClient.
- Base URL from ConfigManager GetParameter("ApiBaseUrl") / runsettings.
- Include happy path and at least one negative or edge case if applicable.
- Use async/await consistently with existing samples.

Deliver: test class(es) under Tests/, and any runsettings/testdata updates.
```

**OpenAPI / contract coverage**

```text
STAF.Playwright.Tests repo. Follow attached AI/skills/framework-rules.md and framework conventions.

Contract goal:
[Describe which API surface or spec to validate.]

Requirements:
- Use OpenApiContractTestBase; OpenApiSpecFolder points at specs under OpenAPI/ copied to output (match existing ContractTests pattern).
- Use framework helpers RunAllContractTestsAsync / AssertAllContractTestsPassed—do not reimplement contract validation from scratch.

Deliver: test class and any new/updated OpenAPI JSON under OpenAPI/ as needed.
```

**Debug a failed run**

```text
STAF.Playwright.Tests. Follow AI/instructions/debugging-rules.md.

Failure summary:
[Paste assertion message, stack trace, or describe flaky behavior.]

Task: identify likely root cause (config, selector, timing, data, parallel conflict), suggest a concrete fix (files/methods to change), and say what to re-run (single test command with testsetting.runsettings).
```

**Excel / workbook scenario**

```text
STAF.Playwright.Tests. Follow AI/skills/framework-rules.md and existing Excel samples under Tests/Excel/.

Scenario:
[Describe create/read/compare or data setup with Excel files.]

Requirements:
- Use ExcelDriver from STAF.Playwright.Framework.Excel (CreateWorkbook, Save, Open, CompareFiles, etc.)—do not manipulate OpenXML or COM Excel directly unless the framework cannot cover the case.
- Keep tests under Tests/Excel/ or follow the existing namespace/folder pattern in this repo.
- Use temp paths or isolated files so parallel runs do not clash.

Deliver: test class changes and any notes on required input files (prefer generating files in-test).
```

**Work item / PBI — full QA cycle (markdown reports)**

```text
STAF.Playwright.Tests repo. Follow the QA Orchestrator skill / AI/instructions/qa-orchestrator-lifecycle.md and work-item-report-templates.md.

Work item:
[ADO/Jira ID or URL, or paste title + description + acceptance criteria here.]

Task: Run the lifecycle — fetch (or use paste), analysis, test design, test cases with AC traceability, code review if application code is in this workspace, test execution strategy, then summary. Write one markdown file per phase under QA/work-items/{Provider}-{WorkItemId}/ (01-pbi-fetch.md through 07-summary-report.md) using the templates.

When implementing automation afterward, follow generation-rules.md and the UI/API skills as usual.
```

### 4. Run what was generated

From the **repository root**:

```bash
dotnet test --settings STAF.Playwright.Tests/testsetting.runsettings
```

From **`STAF.Playwright.Tests/`** (test project folder):

```bash
dotnet test --settings testsetting.runsettings
```

Run a **single** test (example — replace with your test class and method):

```bash
dotnet test --settings STAF.Playwright.Tests/testsetting.runsettings --filter "FullyQualifiedName~YourTestClass.YourTestMethod"
```

Then open **`TestResults`** under the test project output folder for HTML and screenshots (see [Quick start](#quick-start) step 3).

---

## Work-item / PBI QA (orchestrator)

For **end-to-end QA** from a **PBI, User Story, or Bug** (not only “write one test”), the repo includes an **STLC-aligned** playbook:

| Doc | Purpose |
|-----|--------|
| [`AI/instructions/qa-orchestrator-lifecycle.md`](AI/instructions/qa-orchestrator-lifecycle.md) | Phases: fetch → analysis → design → cases → optional code review → execution strategy → summary; Azure DevOps / Jira via **MCP** when available, or pasted requirements |
| [`AI/instructions/work-item-report-templates.md`](AI/instructions/work-item-report-templates.md) | Markdown skeletons for each report file |
| [`AI/skills/qa-orchestrator.md`](AI/skills/qa-orchestrator.md) | Skill entry: when to use, checklist, STAF reminders |
| [`.cursor/skills/staf-qa-orchestrator/SKILL.md`](.cursor/skills/staf-qa-orchestrator/SKILL.md) | Cursor stub pointing at the files above |

**Outputs:** phase reports under **`QA/work-items/{Provider}-{WorkItemId}/`** (`01-pbi-fetch.md` … `07-summary-report.md`). See [`QA/README.md`](QA/README.md). These are separate from framework **HTML** test reports under `TestResults`.

**MCP:** This repo ships the **Playwright C#** MCP under `MCPAgent/` for test authoring. **ADO** or **Jira** work-item fetch is optional — add those MCP servers in your editor if you want live work-item retrieval; otherwise paste the item into chat (documented as a gap in `01-pbi-fetch.md`).

A ready-made prompt is in [AI-assisted automation](#ai-assisted-automation-copy-paste-prompts) under **Work item / PBI — full QA cycle**.

---

## Project layout

| Path | Purpose |
|------|--------|
| `STAF.Playwright.Tests.sln` | Visual Studio / `dotnet` solution (open this or the repo folder) |
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
| `AI/instructions/` | Canonical AI playbook: persona, generation rules, debugging rules, **qa-orchestrator-lifecycle**, **work-item-report-templates** |
| `AI/skills/` | Canonical per-topic skills (UI, API, DB, test data, reporting, framework, **qa-orchestrator**) |
| `QA/` | Orchestrator outputs: **`work-items/{Provider}-{Id}/`** phase `.md` reports (see `QA/README.md`) |
| `.cursor/skills/` | Cursor **project skills** (stubs only — each points at a file under `AI/`) |
| `.vscode/staf-ai/INDEX.md` | Table of contents for the same `AI/` files (VS Code / Copilot attach list) |
| `MCPAgent/` | Playwright C# MCP server (included for use with Cursor, VS Code, or Visual Studio — see [Using the MCP agent](#using-the-mcp-agent)) |
| `.cursor/mcp.json` | Cursor MCP config |
| `.cursor/rules/staf-playwright-framework.mdc` | Cursor rules for STAF Playwright (base classes, page objects, tool usage) |
| `.github/copilot-instructions.md` | GitHub Copilot / agent instructions; references `AI/` as single source of truth |
| `.vscode/mcp.json` | VS Code MCP config |
| `.mcp.json` | Visual Studio MCP config (solution root) |

---

## Configuration

- **Run settings** – Edit `STAF.Playwright.Tests/testsetting.runsettings`: `BaseUrl`, `ApiBaseUrl`, `Browser` (Chrome, Firefox, Edge, Webkit), `Headless`, `Environment`, and other parameters.
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

- **Cursor:** [.cursor/rules/staf-playwright-framework.mdc](.cursor/rules/staf-playwright-framework.mdc) — applied automatically for this workspace.
- **VS Code / GitHub Copilot:** [.github/copilot-instructions.md](.github/copilot-instructions.md) — loaded for the repository; attach **`AI/`** files in chat for the strongest alignment (see [AI-assisted automation](#ai-assisted-automation-copy-paste-prompts)).
- **Canonical handbook:** [`AI/instructions/`](AI/instructions/) and [`AI/skills/`](AI/skills/) — **only** place the full rule text is maintained.
- **Cursor project skills:** [`.cursor/skills/`](.cursor/skills/) — stubs that point at `AI/` (no duplicated rules).
- **VS Code index:** [`.vscode/staf-ai/INDEX.md`](.vscode/staf-ai/INDEX.md) — quick list of `AI/` paths to attach.

**Copy-paste prompts and @-attach bundles:** [AI-assisted automation](#ai-assisted-automation-copy-paste-prompts) (above).

MCP tools (browser inspection, etc.) complement the handbook; they do not replace the `BaseTest` / `BasePage` / `ApiClient` patterns in this project.

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
