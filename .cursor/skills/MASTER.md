# STAF.Playwright.Tests — Cursor Skills Master Index

**Unified Cursor skills reference for STAF.Playwright automation.** Skills in `.cursor/skills/` are **stubs** that point at canonical content under **`AI/`** — do not duplicate rules in two places.

---

## Quick Navigation

| Skill | When to Use | Cursor stub | Canonical file |
|-------|---|---|---|
| **UI testing** | UI tests, page objects, POM | [staf-ui-testing/SKILL.md](./staf-ui-testing/SKILL.md) | [AI/skills/ui-testing.md](../../AI/skills/ui-testing.md) |
| **API testing** | REST / `TestBaseAPI` | [staf-api-testing/SKILL.md](./staf-api-testing/SKILL.md) | [AI/skills/api-testing.md](../../AI/skills/api-testing.md) |
| **Framework rules** | Layout, naming, parallel safety | [staf-framework-rules/SKILL.md](./staf-framework-rules/SKILL.md) | [AI/skills/framework-rules.md](../../AI/skills/framework-rules.md) |
| **Reporting** | `ReportResult`, HTML reports | [staf-reporting/SKILL.md](./staf-reporting/SKILL.md) | [AI/skills/reporting.md](../../AI/skills/reporting.md) |
| **Test data** | Runsettings, `testdata.json` | [staf-test-data/SKILL.md](./staf-test-data/SKILL.md) | [AI/skills/test-data.md](../../AI/skills/test-data.md) |
| **DB testing** | DB validation helpers | [staf-db-testing/SKILL.md](./staf-db-testing/SKILL.md) | [AI/skills/db-testing.md](../../AI/skills/db-testing.md) |
| **QA orchestrator** | PBI / work-item STLC | [staf-qa-orchestrator/SKILL.md](./staf-qa-orchestrator/SKILL.md) | [AI/skills/qa-orchestrator.md](../../AI/skills/qa-orchestrator.md) |
| **AI instructions** | Generation + debugging playbook | [staf-ai-instructions/SKILL.md](./staf-ai-instructions/SKILL.md) | [AI/instructions/](../../AI/instructions/) |
| **AI context** | Minimize tokens / `@` attachments | [staf-ai-context/SKILL.md](./staf-ai-context/SKILL.md) | [AI/instructions/ai-setup.md](../../AI/instructions/ai-setup.md) |

---

## Golden Files (by Workflow)

### UI test + page object

- **Test:** `STAF.Playwright.Tests/Tests/Test1.cs` — Google search flow
- **Page:** `STAF.Playwright.Tests/Pages/GooglePage.cs` — Locators + methods
- **Pattern:** Thin test calling page methods only; async `Task`

### API test

- **Test:** `STAF.Playwright.Tests/Tests/ApiTests.cs` — JSONPlaceholder GET
- **Pattern:** `TestBaseAPI` + `ApiClient` + `ReportResultAPI`

### Contract test

- **Test:** `STAF.Playwright.Tests/Tests/ContractTests.cs`
- **Spec:** `STAF.Playwright.Tests/OpenAPI/placeholder.json`
- **Pattern:** `OpenApiContractTestBase` + `RunAllContractTestsAsync`

### Excel

- **Test:** `STAF.Playwright.Tests/Tests/Excel/ExcelDriverSampleTests.cs`
- **Pattern:** `ExcelDriver` from framework

---

## Master Documentation

**Single source of truth:** [AI/instructions/system-prompt.md](../../AI/instructions/system-prompt.md) + [AI/skills/](../../AI/skills/)

**Quick start:** [AI/instructions/QUICK_START.md](../../AI/instructions/QUICK_START.md)

**Cross-tool entry:** [AGENTS.md](../../AGENTS.md)

---

## Workflow Decision Tree

```
Starting a new task?
│
├─ "Create a UI test method"
│  └─→ staf-ui-testing
│      └─ Check: Page already exists in Pages/?
│         ├─ Yes: Add [TestMethod] in BaseTest class
│         └─ No: Create *Page.cs first, then test
│
├─ "Create a new page / screen"
│  └─→ staf-ui-testing + golden GooglePage.cs
│
├─ "Test a REST API"
│  └─→ staf-api-testing
│      └─ TestBaseAPI + ApiClient + ReportResultAPI
│
├─ "Validate OpenAPI contract"
│  └─→ framework-rules + ContractTests.cs pattern
│      └─ Or use VS agent @staf-contract-automation
│
├─ "Full PBI / work-item QA cycle"
│  └─→ staf-qa-orchestrator
│      └─ Reports under QA/work-items/
│
└─ "Reduce context / pick @ files"
   └─→ staf-ai-context
```

---

## Key Constraints (All Skills)

- ❌ **No custom Playwright lifecycle** in tests — use `BaseTest` `Page`
- ❌ **No `Thread.Sleep(...)`** — use `WaitForElementVisibleAsync`
- ❌ **No raw locators in tests** — page object methods only
- ✅ **Config via `ConfigManager`** — `testsetting.runsettings` / `STAF_*` env
- ✅ **Every important step reports** — `ReportResult` or `ReportResultAPI`
- ✅ **Async UI tests** — `[TestMethod]` returning `async Task`

---

## Testing Commands

```powershell
# Run specific test
dotnet test --filter "FullyQualifiedName~STAF.Playwright.Test1.TestMethod1" `
    --settings STAF.Playwright.Tests/testsetting.runsettings

# Run test class
dotnet test --filter "ClassName~ApiTests" --settings STAF.Playwright.Tests/testsetting.runsettings

# Build
dotnet build STAF.Playwright.Tests/STAF.Playwright.Tests.csproj
```

---

## Cursor-Specific Tips

### Using Skills in Composer / Cmd+K

1. **Reference a skill explicitly:**
   - *"Using staf-ui-testing, create..."*
   - *"Based on staf-api-testing, add..."*

2. **Combine with golden files:**
   - *"Use the pattern from `GooglePage.cs` and `Test1.cs`"*

3. **Ask for checklist:**
   - *"Before finishing, verify against AI/skills/ui-testing.md"*

4. **Token discipline:**
   - Use **staf-ai-context** before large codegen

---

## Platform-Specific Notes

### Visual Studio

- `.github/copilot-instructions.md` for Copilot
- Custom agents: `.github/agents/staf-*.agent.md`
- MCP: `.mcp.json` → `MCPAgent/PlaywrightCSharpMcp.exe`

### VS Code

- `.vscode/README.md` + `.vscode/staf-ai/INDEX.md`
- Attach `AI/` files in Copilot Chat

### Cursor

- This folder (`.cursor/skills/`) + `.cursor/cursor.rules`
- Always-on: `.cursor/rules/staf-playwright-framework.mdc`

---

## Resource Links

| Resource | Path | Purpose |
|----------|------|---------|
| Agents entry | [AGENTS.md](../../AGENTS.md) | Cross-tool summary |
| AI setup | [AI/instructions/ai-setup.md](../../AI/instructions/ai-setup.md) | Editor-specific setup |
| Copilot instructions | [.github/copilot-instructions.md](../../.github/copilot-instructions.md) | VS / VS Code Copilot |
| Cursor rules | [.cursor/cursor.rules](../cursor.rules) | Cursor consistency |
| VS Code index | [.vscode/staf-ai/INDEX.md](../../.vscode/staf-ai/INDEX.md) | Attach list |
| NuGet | [STAF.Playwright](https://www.nuget.org/packages/STAF.Playwright) | Framework docs |

---

**Last Updated:** 2026-06-03  
**Applies To:** Cursor, VS Code, Visual Studio
