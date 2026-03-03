# Project instructions for GitHub Copilot (VS Code / GitHub Agents)

When editing or generating code in this repo, follow these **STAF Playwright framework rules** so that UI, API, contract, and Excel tests stay consistent with the `STAF.Playwright.Tests` project.

---

## 1. Framework

- All **UI tests** must inherit from **BaseTest** from `STAF.Playwright.Framework`.
- All **API tests** must inherit from **TestBaseAPI** from `STAF.Playwright.Framework`.
- All **OpenAPI contract tests** must inherit from **OpenApiContractTestBase** from `STAF.Playwright.Framework.ContractTesting` and override `OpenApiSpecFolder` to point at the `OpenAPI` folder copied to the test output.
- Do **not** create your own Playwright browser/page lifecycle in tests; use the `BaseTest`-provided `Page` and configuration from `ConfigManager`.
- Assembly-level initialization/cleanup must delegate to `Framework.AssemblyInit` as in `AssemblyInit` so HTML reports (`ResultTemplate.html`, `ResultTemplateFinal.html`) are produced correctly.
- Use **runsettings parameters** (`BaseUrl`, `ApiBaseUrl`, `Browser`, `Headless`, `Environment`, etc.) via `ConfigManager.GetParameter(...)` instead of hardcoding values.

## 2. Tool usage (MCP / code generation)

- When **navigating** in UI tests, use `Page.GotoAsync(...)` with URLs from `ConfigManager`; do not spin up your own `IPlaywright`/`IBrowser` instances.
- When **interacting with elements** in page objects, rely on `BasePage` helpers:
  - `WaitForElementVisibleAsync(locator, timeoutMs)`
  - `EnterTextAsync(locator, text, testName, stepDescription)`
  - `PressAsync(locator, key, testName, stepDescription)`
- When **reporting UI steps**, use `ReportResult.ReportResultPass(...)`, `ReportResult.ReportResultFail(...)`, etc., passing `Page` and `TestContext`.
- For **API tests**, use `ApiClient` and `ReportResultAPI` from `TestBaseAPI` instead of creating new HTTP clients or custom logging.
- For **contract tests**, call `RunAllContractTestsAsync(...)` and `AssertAllContractTestsPassed(...)` from `OpenApiContractTestBase` rather than reimplementing contract validation.
- For **Excel operations**, use `ExcelDriver` from `STAF.Playwright.Framework.Excel` for creating, saving, opening, and comparing workbooks.
- Generated code must use the **framework abstraction layer** wherever possible. Do **not** bypass it with raw Playwright or .NET calls when an equivalent helper exists.

## 3. Test creation workflow

1. **Identify scenario and test type**
   - UI, API, contract, or Excel.
2. **Select the correct base class**
   - UI → `BaseTest`
   - API → `TestBaseAPI`
   - Contract → `OpenApiContractTestBase`
3. **Page Objects for UI tests**
   - Look in `Pages/` (e.g. `GooglePage`) before creating new pages.
   - New pages should inherit from `BasePage`, accept `IPage` and `TestContext`, and expose:
     - `ILocator` properties using `Page.Locator(...)` for elements.
     - High-level methods that use `WaitForElementVisibleAsync`, `EnterTextAsync`, `PressAsync`, and `ReportResult`.
4. **Centralize locators and actions**
   - Keep locators on page classes; avoid writing raw selectors inside tests.
   - Tests should call page methods (e.g. `VerifyGooglePageIsDisplayed`, `SearchFor(...)`), not direct Playwright calls.
5. **Use configuration and reporting**
   - Use `ConfigManager.GetParameter(...)` to read `BaseUrl`, `ApiBaseUrl`, and other parameters defined in `testsetting.runsettings`.
   - Use `ReportResult` / `ReportResultAPI` for all important steps, in addition to MSTest assertions.

## 4. Coding standards

- Prefer **stable selectors** (IDs, data attributes, semantic attributes) when defining `ILocator`s; avoid brittle, deeply nested CSS or XPath.
- Keep **page classes cohesive and focused**, grouping elements and actions per page/screen.
- Name tests clearly and follow **AAA (Arrange–Act–Assert)** in test bodies.
- Avoid `Thread.Sleep` and arbitrary `Task.Delay`; always use framework waits such as `WaitForElementVisibleAsync`.
- Do not duplicate existing page or helper methods; reuse and extend what the framework already provides.
- Keep files within the established structure (`Tests/`, `Pages/`, `Tests/Excel/`, `OpenAPI/`) so the project remains easy to navigate.

