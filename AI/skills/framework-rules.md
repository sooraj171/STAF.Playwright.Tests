# Skill: STAF.Playwright framework rules & architecture

**Scope:** Solution-wide conventions for **STAF.Playwright.Tests** and enterprise clones of this pattern.

## Core framework mapping

| Scenario | Base / type | Package area |
|----------|-------------|--------------|
| UI | `BaseTest` | `STAF.Playwright.Framework` |
| API | `TestBaseAPI` | `STAF.Playwright.Framework` |
| OpenAPI contract | `OpenApiContractTestBase` | `STAF.Playwright.Framework.ContractTesting` |
| Excel | `ExcelDriver` | `STAF.Playwright.Framework.Excel` |
| Pages | `BasePage` | `STAF.Playwright.Framework` |

## Architectural rules

- **Keep tests atomic**: one scenario per `[TestMethod]`; multiple asserts are fine when they validate **one** outcome (e.g. same API response).
- **Avoid duplication**: extract repeated flows to **page methods** or **private static/async helpers** in the test project.
- **Prefer composition over inheritance**: use small helper classes or partial responsibilities instead of deep `BaseTest` subclasses—unless extending the framework’s own bases.
- **Maintain naming conventions**:
  - Tests: `Feature_Condition_ExpectedOutcome` or readable sentence style consistent with existing files.
  - Pages: `{ScreenName}Page`.
  - Locators: property names reflect **user intent** (`SubmitButton`, `ErrorBanner`), not DOM structure (`Div2`).

## Folder structure (do not deviate without team agreement)

- `Tests/` — UI, API, contract test classes.
- `Tests/Excel/` — ExcelDriver samples/tests.
- `Pages/` — page objects only.
- `OpenAPI/` — contract specs copied to output for contract tests.

## MCP / AI generation

- Generated code must use **framework abstractions** first; see `.cursor/rules/staf-playwright-framework.mdc`.
- For detailed behaviors, combine:
  - `AI/skills/ui-testing.md`
  - `AI/skills/api-testing.md`
  - `AI/skills/db-testing.md`
  - `AI/skills/test-data.md`
  - `AI/skills/reporting.md`

## Parallel execution (MSTest)

- Avoid **static mutable** shared state.
- Use **unique** entities/files per test when creating data.
- Do not depend on **`TestInitialize`** ordering across classes beyond what MSTest guarantees.

## Quality checklist (before merge)

- [ ] Correct base class for test type  
- [ ] No raw Playwright in tests when POM exists  
- [ ] Config via `ConfigManager` / test data files  
- [ ] `ReportResult` / `ReportResultAPI` on key steps  
- [ ] No arbitrary sleeps  
- [ ] Parallel-safe data and files  
