# GitHub Copilot — STAF.Playwright.Tests

**Master reference:** [AI/instructions/system-prompt.md](../AI/instructions/system-prompt.md) and [AI/skills/](../AI/skills/) — single source of truth for all platforms (Visual Studio, VS Code, Cursor).

---

## Platform-Specific Entry Points

| Tool | Where Instructions Load | What to Read |
|------|---|---|
| **Visual Studio** | `.github/copilot-instructions.md` (this file) | **Quick Rules** below + attach `AI/skills/*.md` as needed |
| **VS Code** | `.vscode/README.md` + `.github/copilot-instructions.md` | [Quick Rules](#quick-rules) + [`.vscode/staf-ai/INDEX.md`](../.vscode/staf-ai/INDEX.md) |
| **Cursor** | `.cursor/skills/` + `.cursor/cursor.rules` | [MASTER.md](../.cursor/skills/MASTER.md) + always-on `.cursor/rules/staf-playwright-framework.mdc` |

---

## Quick Rules

### Framework Basics

- **UI Tests:** inherit `BaseTest` (has `Page`, `TestContext`, framework lifecycle)
- **API Tests:** inherit `TestBaseAPI` (no browser; use `ApiClient`)
- **Contract Tests:** inherit `OpenApiContractTestBase`; override `OpenApiSpecFolder`
- **Pages:** inherit `BasePage`; use `ILocator` properties and framework helpers only

### Critical Constraints

- **No custom Playwright lifecycle** in tests — use `BaseTest` `Page` and `ConfigManager`
- **No `Thread.Sleep`** — use `WaitForElementVisibleAsync` and framework waits
- **No raw locators in tests** — call page object methods only
- **Assertions + reporting** in page methods and tests via `ReportResult` / `ReportResultAPI`

### Core Methods

| Action | UI Pattern | API Pattern |
|--------|-----------|-------------|
| **Navigate** | `Page.GotoAsync(ConfigManager.GetParameter("BaseUrl"))` | N/A |
| **Find / interact** | `WaitForElementVisibleAsync`, `EnterTextAsync`, `PressAsync` on page | N/A |
| **Report Step** | `ReportResult.ReportResultPass/Fail(...)` | `ReportResultAPI.ReportResultPass/Fail(...)` |
| **HTTP** | N/A | `ApiClient.GetAsync(...)` etc. |
| **Contract** | N/A | `RunAllContractTestsAsync` + `AssertAllContractTestsPassed` |

### File Naming = Class Naming

- File: `GooglePage.cs` → Class: `GooglePage` (inherits `BasePage`)
- File: `Test1.cs` → Class: `Test1` (inherits `BaseTest`)
- File: `ApiTests.cs` → Class: `ApiTests` (inherits `TestBaseAPI`)

---

## Three Core Workflows

### 1️⃣ Create UI Test

**Where:** `STAF.Playwright.Tests/Tests/{TestClass}.cs`  
**What:** New `[TestMethod]` in class inheriting `BaseTest`

```csharp
[TestMethod]
public async Task GoogleSearch_Playwright_ReturnsResults()
{
    await Page.GotoAsync(ConfigManager.GetParameter("BaseUrl") ?? "https://www.google.com");
    var googlePage = new GooglePage(Page, TestContext);
    await googlePage.VerifyGooglePageIsDisplayed();
    await googlePage.SearchFor("Playwright");
}
```

**Golden files:** `STAF.Playwright.Tests/Pages/GooglePage.cs`, `STAF.Playwright.Tests/Tests/Test1.cs`  
**Full details:** [AI/skills/ui-testing.md](../AI/skills/ui-testing.md)

---

### 2️⃣ Create Page Object

**Where:** `STAF.Playwright.Tests/Pages/{Screen}Page.cs`  
**What:** Locators + high-level methods (this repo uses POM without a separate Actions layer)

**Page template:**

```csharp
public class MyScreenPage : BasePage
{
    public MyScreenPage(IPage page, TestContext testContext) : base(page, testContext) { }

    public ILocator SubmitButton => Page.Locator("[data-testid='submit']");

    public async Task VerifyPageLoadedAsync()
    {
        if (await WaitForElementVisibleAsync(SubmitButton, 5000))
            await ReportResult.ReportResultPass(Page, TestContext, nameof(VerifyPageLoadedAsync), "Page loaded");
        else
        {
            await ReportResult.ReportResultFail(Page, TestContext, nameof(VerifyPageLoadedAsync), "Page not loaded");
            Assert.Fail("Page not loaded");
        }
    }
}
```

**Golden file:** `STAF.Playwright.Tests/Pages/GooglePage.cs`  
**Full details:** [AI/skills/ui-testing.md](../AI/skills/ui-testing.md)

---

### 3️⃣ Create API Test

**Where:** `STAF.Playwright.Tests/Tests/{Name}Tests.cs`  
**What:** Class inheriting `TestBaseAPI`

```csharp
[TestMethod]
public async Task Api_GetPost_ReturnsOkAndBody()
{
    var response = await ApiClient.GetAsync("/posts/1").ConfigureAwait(false);
    var body = await ApiClient.GetResponseAsStringAsync(response).ConfigureAwait(false);

    await ReportResultAPI.ReportResultPass(TestContext, nameof(Api_GetPost_ReturnsOkAndBody),
        $"GET /posts/1 - Status: {(int)response.StatusCode}").ConfigureAwait(false);

    Assert.IsTrue(response.IsSuccessStatusCode);
    Assert.IsFalse(string.IsNullOrWhiteSpace(body));
}
```

**Golden file:** `STAF.Playwright.Tests/Tests/ApiTests.cs`  
**Full details:** [AI/skills/api-testing.md](../AI/skills/api-testing.md)

---

## Testing & Running

```powershell
# Run specific test
dotnet test --filter "FullyQualifiedName~STAF.Playwright.Test1.TestMethod1" `
    --settings STAF.Playwright.Tests/testsetting.runsettings

# Run test class
dotnet test --filter "ClassName~ApiTests" --settings STAF.Playwright.Tests/testsetting.runsettings

# Run all tests
dotnet test --settings STAF.Playwright.Tests/testsetting.runsettings
```

---

## Resource Map

| Need | Open | Notes |
|------|------|-------|
| **Persona & guardrails** | [AI/instructions/system-prompt.md](../AI/instructions/system-prompt.md) | Default QA-architect behavior |
| **Generation rules** | [AI/instructions/generation-rules.md](../AI/instructions/generation-rules.md) | Output format, coverage |
| **Debugging** | [AI/instructions/debugging-rules.md](../AI/instructions/debugging-rules.md) | Failures, flakes |
| **Quick start** | [AI/instructions/QUICK_START.md](../AI/instructions/QUICK_START.md) | Platform navigation |
| **Cursor skills index** | [.cursor/skills/MASTER.md](../.cursor/skills/MASTER.md) | Cursor skill picker |
| **VS Code handbook index** | [.vscode/staf-ai/INDEX.md](../.vscode/staf-ai/INDEX.md) | Attach list for Copilot |
| **VS custom agents** | [agents/](agents/) | UI, API, contract, QA orchestrator |
| **Repo agents entry** | [AGENTS.md](../AGENTS.md) | Cross-tool summary |
| **Golden examples** | `GooglePage.cs`, `Test1.cs`, `ApiTests.cs`, `ContractTests.cs` | Under `STAF.Playwright.Tests/` |

---

## IDE-Specific Tips

### Visual Studio (GitHub Copilot)

- **This file** (`.github/copilot-instructions.md`) is auto-loaded by VS GitHub Copilot
- **Custom agents** (VS 2026 18.4+): `.github/agents/staf-ui-automation.agent.md`, `staf-api-automation.agent.md`, `staf-contract-automation.agent.md`, `staf-qa-orchestrator.agent.md` — pick from the agent picker or `@staf-ui-automation` etc.
- Attach `AI/skills/ui-testing.md` or `api-testing.md` for deep codegen
- MCP: `.mcp.json` → `MCPAgent/PlaywrightCSharpMcp.exe`

### VS Code (GitHub Copilot)

- Check [.vscode/README.md](../.vscode/README.md) for Copilot setup
- Use [.vscode/staf-ai/INDEX.md](../.vscode/staf-ai/INDEX.md) to attach `AI/` files
- Copilot Chat (`Ctrl+Shift+I`) for code generation

### Cursor (AI Editor)

- Cursor reads `.cursor/skills/MASTER.md` and `.cursor/cursor.rules`
- Always-on: `.cursor/rules/staf-playwright-framework.mdc`
- Skills under `.cursor/skills/` point at canonical `AI/` files

---

## Checklists

### Before Creating New Code

- [ ] Identify workflow: UI test, page object, API test, contract, or Excel
- [ ] Check existing `Pages/` and `Tests/` for reuse
- [ ] Reference golden file(s) from this document
- [ ] Use `ConfigManager.GetParameter(...)` — no hardcoded URLs
- [ ] Plan `ReportResult` / `ReportResultAPI` for important steps

### After Creating New Code

- [ ] Inherits correct base class (`BaseTest`, `BasePage`, `TestBaseAPI`, `OpenApiContractTestBase`)
- [ ] Test passes locally with `testsetting.runsettings`
- [ ] No build errors: `dotnet build STAF.Playwright.Tests/STAF.Playwright.Tests.csproj`

---

**Last Updated:** 2026-06-03  
**Framework:** [STAF.Playwright](https://www.nuget.org/packages/STAF.Playwright)  
**Target Framework:** .NET 10  
**Applies To:** Visual Studio, VS Code, Cursor
