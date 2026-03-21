# Skill: API testing (STAF.Playwright)

**Scope:** HTTP/API tests using **`TestBaseAPI`**, **`ApiClient`**, and **`ReportResultAPI`** from `STAF.Playwright.Framework`.

## Rules

- **Always** use the framework **`ApiClient`** (from `TestBaseAPI`) for requests—do **not** spin up ad-hoc `HttpClient` instances for test code unless the framework truly lacks a capability (then document why).
- **Validate**:
  - **Status codes** — assert expected codes for happy/negative paths.
  - **Response shape** — JSON/XML parsing; for contract-level guarantees prefer **`OpenApiContractTestBase`** and specs under `OpenAPI/`.
  - **Business logic** — assert domain fields (IDs, state transitions), not only HTTP success.
- **Chaining**: use values from **response N** as inputs to **request N+1** inside the same test (local variables), or via a small private helper—**not** static mutable state.

## Configuration

- Base URL: **`ConfigManager.GetParameter("ApiBaseUrl")`** (from `testsetting.runsettings` or `STAF_ApiBaseUrl`).
- Paths: lead with `/` for absolute paths on the host (e.g. `GetAsync("/posts/1")`).

## Reporting and logging

- After important calls, use **`ReportResultAPI.ReportResultPass`** (and fail variants as provided by the framework) with **`TestContext`**, including status code and concise context—**never** log secrets or full auth tokens.

## Generate

1. **API test methods** — `[TestMethod]`, `async Task`, inherit **`TestBaseAPI`**.
2. **Request builders** — if the API is complex, add **small builder/helper methods** in the test class or a dedicated `Clients/` / `Builders/` type in the test project—still delegating transport to **`ApiClient`**.
3. **Validation logic** — parse body with `System.Text.Json` or project conventions; assert with **`Assert.*`**.

## Data-driven API tests

- Prefer **`[DataRow]`** / dynamic data for parameter variations.
- For environment-specific payloads, use **`testdata.json`** patterns via **`ConfigManager`** (see **`test-data.md`**).

## Anti-patterns

- Hardcoded production URLs.
- Assertions that only check “response is not null” on success paths.
- Shared static last-response caches across tests (breaks parallel isolation).
