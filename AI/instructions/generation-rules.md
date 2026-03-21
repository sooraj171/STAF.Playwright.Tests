# Generation rules — STAF.Playwright

Apply these rules whenever **creating or extending** automated tests, page objects, or supporting assets.

## 1. Understand the feature first

Before writing code:

1. Clarify **user-visible behavior** (UI) and/or **service behavior** (API).
2. Identify **preconditions** (auth, data, feature flags) and **postconditions** (DB, messages, redirects).
3. Check **existing** pages, tests, and helpers under `Pages/` and `Tests/` to **reuse** before adding new types.

## 2. Coverage map

For each feature, explicitly decide coverage for:

| Layer        | What to identify |
|-------------|-------------------|
| **UI flows** | Happy path, validation errors, empty states, role/permission variations |
| **API**      | Endpoints, methods, auth headers, expected status codes, error bodies |
| **DB**       | Tables/entities affected, integrity constraints, transactional behavior (use DB helpers only) |

## 3. Scenario categories to generate

1. **Happy path** — primary success criteria.
2. **Edge cases** — boundaries, optional fields, slow networks (handled via waits, not `Sleep`).
3. **Negative** — invalid input, unauthorized/forbidden, not found, conflict, timeouts where applicable.

Use a **data-driven** style when multiple inputs differ only by data (MSTest `[DataRow]` / dynamic data, or shared test data from `testdata.json` via `ConfigManager` patterns used in this repo).

## 4. Implementation constraints (STAF-specific)

- **UI tests**: class inherits `BaseTest`; **no** direct `Page.Locator(...)` in test methods if a page object should own that element.
- **API tests**: class inherits `TestBaseAPI`; use **`ApiClient`** for HTTP; chain responses by passing values into subsequent calls in the same test method (or helper), not via static globals.
- **Contract tests**: inherit `OpenApiContractTestBase`, set `OpenApiSpecFolder`, use framework `RunAllContractTestsAsync` / `AssertAllContractTestsPassed`.
- **Navigation**: `await Page.GotoAsync(ConfigManager.GetParameter("BaseUrl") + relativePath)` or equivalent—**never** hardcode full URLs unless the parameter is missing and you document the fallback as sample-only.

## 5. Optimization rules

- **Reuse login/session** when the framework provides storage state or documented patterns; do not invent parallel-unsafe global “logged in” flags without isolation.
- **Avoid duplicate locators**: one logical element → one `ILocator` on the page class (or shared component object if the project introduces that pattern).
- **Stable selectors**: `data-testid`, `id`, accessible roles/names over long XPath.
- **Async**: `async Task` tests, `await` framework methods, `ConfigureAwait(false)` where consistent with existing project style.
- **Flakiness**: rely on **auto-wait** and `WaitForElementVisibleAsync`; avoid fixed delays.

## 6. Output format (always)

When asked to implement automation, deliver **all** applicable artifacts:

1. **Test file(s)** — under `Tests/` (or `Tests/Excel/`), correct base class, clear test names.
2. **Page Object updates** — under `Pages/` when UI is involved; new methods and locators, not test-level selectors.
3. **Test data** — runsettings parameters, `testdata.json` entries, or data rows; no secrets in repo.
4. **Comments** — short, explaining **why** (business rule, workaround, data dependency), not narrating obvious code.

## 7. Framework architecture preferences

- **Atomic tests**: one clear scenario per test method; shared setup via init or helpers, not chained assertions across unrelated goals.
- **Avoid duplication**: extract repeated API or UI sequences into page methods or private helpers in the test project.
- **Composition over inheritance**: prefer small helpers or partial page responsibilities over deep base test hierarchies unless the framework already provides the base.
- **Naming**: test methods read as scenarios (`Login_WithInvalidPassword_ShowsError`); pages named after screens/features (`LoginPage`, `OrderDetailsPage`).

## 8. Parallel execution

- Do not rely on **execution order** or **shared mutable statics**.
- Use unique identifiers per run (GUIDs in created entities, temp file paths) when tests create data.
- Align with MSTest parallel settings for the solution; avoid static caches unless thread-safe and documented.
