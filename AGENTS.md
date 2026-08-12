# AI agents — STAF.Playwright.Tests

C# / MSTest UI, API, contract, and Excel automation on **[STAF.Playwright](https://www.nuget.org/packages/STAF.Playwright)**. Use this file in **Cursor**, **GitHub Copilot (VS Code / Visual Studio)**, and other agents that read repo-root instructions.

## Non-negotiables

| Rule | UI | API / contract / Excel |
|------|----|------------------------|
| Test base | `BaseTest` | `TestBaseAPI` / `OpenApiContractTestBase` |
| Browser / page | Use base `Page` only — never `new Playwright()` in tests | N/A |
| Waits | `WaitForElementVisibleAsync`, framework helpers — **no** `Thread.Sleep` | N/A |
| Pages | `BasePage` + `ILocator` properties + high-level methods | N/A |
| Navigation / input | `Page.GotoAsync` + `EnterTextAsync`, `PressAsync` via page objects | N/A |
| Reporting | `ReportResult` | `ReportResultAPI` |
| Tests | Thin methods — call **page methods**, not raw locators | `ApiClient` / contract helpers |

## Layout

| Folder | Role |
|--------|------|
| `STAF.Playwright.Tests/Pages/` | Locators + UI flows (`*Page`, inherits `BasePage`) |
| `STAF.Playwright.Tests/Tests/` | `[TestMethod]` UI, API, contract tests |
| `STAF.Playwright.Tests/Tests/Excel/` | ExcelDriver samples |
| `STAF.Playwright.Tests/OpenAPI/` | OpenAPI specs for contract tests |

Config: `ConfigManager.GetParameter(...)` ↔ `STAF.Playwright.Tests/testsetting.runsettings` (`BaseUrl`, `ApiBaseUrl`, `Browser`, `Headless`, `Environment`, …).

Run: `dotnet test --settings STAF.Playwright.Tests/testsetting.runsettings`

## Golden examples (open these, not the whole solution)

| Task | Files |
|------|--------|
| Page + UI test | `Pages/GooglePage.cs`, `Tests/Test1.cs` |
| API test | `Tests/ApiTests.cs` |
| Contract test | `Tests/ContractTests.cs`, `OpenAPI/placeholder.json` |
| Excel | `Tests/Excel/ExcelDriverSampleTests.cs` |

## Token discipline

1. **Default:** rely on `.cursor/rules/staf-playwright-framework.mdc` or `.github/copilot-instructions.md` — do not paste all of `AI/instructions/` unless generating new types.
2. **New page/test:** attach **one** golden file from the table above + the relevant skill under `AI/skills/`.
3. **Deep detail:** `AI/instructions/generation-rules.md` or `AI/instructions/debugging-rules.md` — attach **one** file only.
4. **Cursor skills** (optional, on-demand): `.cursor/skills/staf-*` — index [`.cursor/skills/MASTER.md`](.cursor/skills/MASTER.md); see [AI/instructions/ai-setup.md](AI/instructions/ai-setup.md).

## Tool-specific setup

| Tool | Instructions |
|------|----------------|
| **Cursor** | `.cursor/rules/*.mdc` (always-on + file-scoped); skills in `.cursor/skills/` (stubs → `AI/`); MCP: `.cursor/mcp.json` |
| **VS Code Copilot** | `.github/copilot-instructions.md`; handbook index: `.vscode/staf-ai/INDEX.md`; MCP: `.vscode/mcp.json` |
| **Visual Studio Copilot** | `.github/copilot-instructions.md`; **custom agents** in `.github/agents/*.agent.md`; MCP: `.mcp.json` |

### Visual Studio custom agents

Pick an agent from the Copilot agent picker (VS 2026 18.4+) or type `@staf-ui-automation`, `@staf-api-automation`, `@staf-contract-automation`, or `@staf-qa-orchestrator`:

| Agent | File | Use for |
|-------|------|---------|
| **STAF UI Automation** | [.github/agents/staf-ui-automation.agent.md](.github/agents/staf-ui-automation.agent.md) | UI tests, page objects |
| **STAF API Automation** | [.github/agents/staf-api-automation.agent.md](.github/agents/staf-api-automation.agent.md) | REST tests, ApiClient |
| **STAF Contract Automation** | [.github/agents/staf-contract-automation.agent.md](.github/agents/staf-contract-automation.agent.md) | OpenAPI contract tests |
| **STAF QA Orchestrator** | [.github/agents/staf-qa-orchestrator.agent.md](.github/agents/staf-qa-orchestrator.agent.md) | PBI / work-item STLC reports |

Documentation: [README.md](README.md) · Quick start: [AI/instructions/QUICK_START.md](AI/instructions/QUICK_START.md)

MCP servers: `MCPAgent/PlaywrightCSharpMcp.exe` and `MCPAgent/AzureDevOps/` — see [README.md — Using MCP servers](README.md#using-mcp-servers).

## Prompt shortcuts

Copy-paste prompts and `@` attach bundles: [README.md — AI-assisted automation](README.md#ai-assisted-automation-copy-paste-prompts).
