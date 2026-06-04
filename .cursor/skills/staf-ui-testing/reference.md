# UI test templates

**Canonical rules:** [AI/skills/ui-testing.md](../../../AI/skills/ui-testing.md)

**Test (minimal):**

```csharp
[TestMethod]
public async Task MyFlow_Scenario_Succeeds()
{
    await Page.GotoAsync(ConfigManager.GetParameter("BaseUrl")!);
    var page = new MyScreenPage(Page, TestContext);
    await page.VerifyPageLoadedAsync();
    await page.CompleteActionAsync();
}
```

**Page (minimal):**

```csharp
public class MyScreenPage : BasePage
{
    public MyScreenPage(IPage page, TestContext testContext) : base(page, testContext) { }
    public ILocator SubmitButton => Page.Locator("[data-testid='submit']");
}
```

Golden: `STAF.Playwright.Tests/Pages/GooglePage.cs`, `STAF.Playwright.Tests/Tests/Test1.cs`
