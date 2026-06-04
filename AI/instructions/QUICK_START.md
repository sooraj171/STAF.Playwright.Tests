# Quick Start — STAF.Playwright.Tests

Get started in 5 minutes. Choose your platform and task.

---

## What Do You Want to Do?

| Task | Time | Link |
|------|------|------|
| **Add a new UI test** | 2 min | [Create UI Test](#create-ui-test) |
| **Create a page object** | 5 min | [Create Page Object](#create-page-object) |
| **Create an API test** | 5 min | [Create API Test](#create-api-test) |
| **Run PBI / work-item QA** | varies | [QA Orchestrator](#qa-orchestrator) |
| **Pick editor setup** | 2 min | [Platform Setup](#platform-setup) |

---

## Create UI Test

### Scenario

You have an existing page object (`GooglePage`). You want to add a new test method.

### Files

- Create in: `STAF.Playwright.Tests/Tests/MyTests.cs`
- Reference: `STAF.Playwright.Tests/Tests/Test1.cs` (golden example)

### Code

```csharp
[TestMethod]
public async Task GoogleSearch_MyTerm_Succeeds()
{
    await Page.GotoAsync(ConfigManager.GetParameter("BaseUrl") ?? "https://www.google.com");
    var googlePage = new GooglePage(Page, TestContext);
    await googlePage.VerifyGooglePageIsDisplayed();
    await googlePage.SearchFor("My search term");
}
```

### Checklist

- [ ] Class inherits `BaseTest`
- [ ] Method decorated with `[TestMethod]`, returns `async Task`
- [ ] Calls page methods only — no `Page.Locator` in test
- [ ] Test method name: `{Action}_{Scenario}_{Expected}`

### Run It

```powershell
dotnet test --filter "FullyQualifiedName~STAF.Playwright.MyTests.GoogleSearch_MyTerm_Succeeds" `
    --settings STAF.Playwright.Tests/testsetting.runsettings
```

---

## Create Page Object

### Files

- Create: `STAF.Playwright.Tests/Pages/{Screen}Page.cs`
- Reference: `STAF.Playwright.Tests/Pages/GooglePage.cs`

### Template

```csharp
public class MyScreenPage : BasePage
{
    public MyScreenPage(IPage page, TestContext testContext) : base(page, testContext) { }

    public ILocator SubmitButton => Page.Locator("[data-testid='submit']");

    public async Task VerifyPageLoadedAsync()
    {
        if (await WaitForElementVisibleAsync(SubmitButton, 5000))
            await ReportResult.ReportResultPass(Page, TestContext, nameof(VerifyPageLoadedAsync), "Loaded");
        else
        {
            await ReportResult.ReportResultFail(Page, TestContext, nameof(VerifyPageLoadedAsync), "Not loaded");
            Assert.Fail("Page not loaded");
        }
    }
}
```

---

## Create API Test

### Files

- Create or extend: `STAF.Playwright.Tests/Tests/ApiTests.cs`
- Reference: same file (golden example)

### Code

```csharp
[TestMethod]
public async Task Api_GetPost_ReturnsOk()
{
    var response = await ApiClient.GetAsync("/posts/1").ConfigureAwait(false);
    await ReportResultAPI.ReportResultPass(TestContext, nameof(Api_GetPost_ReturnsOk),
        $"Status: {(int)response.StatusCode}").ConfigureAwait(false);
    Assert.IsTrue(response.IsSuccessStatusCode);
}
```

### Run It

```powershell
dotnet test --filter "ClassName~ApiTests" --settings STAF.Playwright.Tests/testsetting.runsettings
```

---

## QA Orchestrator

For **PBI / User Story** end-to-end QA (not only one test):

1. Attach `AI/instructions/qa-orchestrator-lifecycle.md` and `AI/skills/qa-orchestrator.md`
2. Cursor: use skill `staf-qa-orchestrator` or VS agent `@staf-qa-orchestrator`
3. Outputs go to `QA/work-items/{Provider}-{Id}/01` … `07` markdown files

Copy-paste prompt: [README.md — Work item / PBI](../../README.md#ai-assisted-automation-copy-paste-prompts)

---

## Platform Setup

| Platform | Start here |
|----------|------------|
| **Cursor** | [AGENTS.md](../../AGENTS.md) · [`.cursor/skills/MASTER.md`](../../.cursor/skills/MASTER.md) · [ai-setup.md](./ai-setup.md) |
| **VS Code Copilot** | [`.vscode/README.md`](../../.vscode/README.md) · [`.vscode/staf-ai/INDEX.md`](../../.vscode/staf-ai/INDEX.md) |
| **Visual Studio Copilot** | [`.github/copilot-instructions.md`](../../.github/copilot-instructions.md) · [`.github/agents/`](../../.github/agents/) |

---

## Resource Map

| Need | File |
|------|------|
| Cross-tool agents summary | [AGENTS.md](../../AGENTS.md) |
| Copilot quick rules | [.github/copilot-instructions.md](../../.github/copilot-instructions.md) |
| UI skill (full) | [AI/skills/ui-testing.md](../skills/ui-testing.md) |
| API skill (full) | [AI/skills/api-testing.md](../skills/api-testing.md) |
| Generation rules | [generation-rules.md](./generation-rules.md) |
| Debugging | [debugging-rules.md](./debugging-rules.md) |
