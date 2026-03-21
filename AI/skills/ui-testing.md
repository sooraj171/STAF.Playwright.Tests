# Skill: UI automation (STAF.Playwright + Playwright .NET)

**Scope:** Browser UI tests in this repository using **Microsoft Playwright** through **STAF.Playwright** (`BaseTest`, `BasePage`).

## Rules

- **Page Object Model (POM)**: all element locators and UI interactions belong in **`Pages/*`** classes inheriting **`BasePage`**.
- **Never use direct selectors in tests**: test methods must **not** call `Page.Locator(...)`, `ClickAsync`, `FillAsync`, etc., when a page object can encapsulate that behavior.
- **Reusable locators**: expose **`ILocator`** properties (or private locators used only inside cohesive methods) on the page class; do not duplicate the same selector string across classes—**compose** or inherit only when it stays maintainable.
- **Playwright best practices** (via framework + POM):
  - Rely on **auto-wait** for actions initiated through page methods.
  - Use **assertions** (`Assert.*`) after meaningful steps, paired with **`ReportResult`** outcomes.
  - **Browser/context reuse**: do not create browsers in tests; use **`BaseTest`**’s lifecycle and `Page`.

## Framework hooks (must use)

| Concern        | Use |
|----------------|-----|
| Test base      | `BaseTest` |
| Page base      | `BasePage` |
| Waits / input  | `WaitForElementVisibleAsync`, `EnterTextAsync`, `PressAsync`, … |
| Reporting      | `ReportResult.ReportResultPass` / `Fail` / … with `Page`, `TestContext` |
| URLs           | `ConfigManager.GetParameter("BaseUrl")` (+ relative paths as needed) |
| Navigation     | `Page.GotoAsync(...)` with configured URLs |

## Generate

1. **Page classes** — constructor `(IPage page, TestContext testContext) : base(page, testContext)`; region or grouped **`ILocator`** properties; **high-level methods** (`LoginAs(...)`, `SubmitOrder()`, `AssertErrorVisible()`).
2. **Test specs** — MSTest `[TestClass]` / `[TestMethod]`, **`async Task`**, inherit **`BaseTest`**, call **page methods only** for UI.
3. **Assertions** — MSTest `Assert.*` after actions; fail with clear messages.
4. **Navigation flows** — sequence of page method calls representing the journey; optional shared ** Arrange** helpers for data.

## Anti-patterns

- Raw Playwright in `[TestMethod]` bodies.
- Hardcoded full URLs or credentials.
- `Thread.Sleep` / blind `Task.Delay` instead of framework waits.
- Copy-pasted locator strings across multiple page files.

## Optional: contract / Excel

If the scenario is not classic UI, route to **`api-testing.md`**, OpenAPI contract skill patterns in **`framework-rules.md`**, or Excel samples under `Tests/Excel/` instead of forcing UI.
