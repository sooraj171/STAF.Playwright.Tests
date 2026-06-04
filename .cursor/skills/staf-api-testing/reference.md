# API test templates

**Canonical rules:** [AI/skills/api-testing.md](../../../AI/skills/api-testing.md)

```csharp
[TestMethod]
public async Task Api_GetResource_ReturnsOk()
{
    var testName = nameof(Api_GetResource_ReturnsOk);
    var response = await ApiClient.GetAsync("/posts/1").ConfigureAwait(false);

    await ReportResultAPI.ReportResultPass(TestContext, testName,
        $"Status: {(int)response.StatusCode}").ConfigureAwait(false);

    Assert.IsTrue(response.IsSuccessStatusCode);
}
```

Golden: `STAF.Playwright.Tests/Tests/ApiTests.cs`
