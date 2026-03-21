# Debugging rules — STAF.Playwright

Use this when **tests fail**, behave flaky, or after a run when the user wants **analysis and next steps**.

## 1. First response pattern

For any failure report (log, stack trace, screenshot path, HTML report):

1. **Identify root cause** — distinguish:
   - Selector/timing (element not found, not visible)
   - Environment/config (wrong `BaseUrl`, `ApiBaseUrl`, headless vs headed)
   - Data (stale user, missing seed data)
   - Application defect vs test defect
2. **Suggest a fix** — concrete code or config change (e.g. stabilize locator, increase wait only if justified, fix assertion).
3. **Offer post-run analysis** — remind the user they can:
   - Open `TestResults` HTML (`ResultTemplateFinal.html`) and failure screenshots
   - Re-run a single test with the same `--settings` file
   - Capture trace/video if the framework or Playwright config supports it in their pipeline

## 2. UI failures

Check in order:

- Was navigation using **`ConfigManager.GetParameter("BaseUrl")`** (or appropriate parameter)?
- Does a **Page Object** already expose the action? If the test uses raw Playwright, **move** logic to the page and stabilize locators.
- Is the failure **intermittent**? Prefer explicit waits via **`WaitForElementVisibleAsync`** and ensure assertions run after the UI settles; avoid `Thread.Sleep` / arbitrary `Task.Delay`.
- **Parallelism**: could another test mutate shared data or the same user session?

## 3. API failures

Check:

- **Status code** vs expectation; log response body via **`ReportResultAPI`** for traceability.
- **Base URL** and path concatenation (double slashes, missing leading slash).
- **Auth**: tokens or headers should come from config or test data, not hardcoded secrets.
- **Chaining**: ensure IDs from a prior response are parsed correctly before the next call.

## 4. Contract / schema failures

- Point to the **OpenAPI** spec under `OpenAPI/` and the operation in question.
- Distinguish **spec drift** (implementation changed) from **test expectation** errors.

## 5. DB validation failures

- Confirm whether the failure is **assertion** vs **connectivity** vs **wrong connection string** (from config).
- Prefer **framework DB helpers**; if raw SQL was used against project rules, recommend migrating to the shared abstraction.

## 6. Flakiness playbook

1. Stabilize selectors (IDs, `data-*`, roles).
2. Ensure **idempotent** setup (create fresh data or use dedicated test accounts from config).
3. Remove shared state; scope temp files per test.
4. Re-run locally with the **same** `testsetting.runsettings` as CI.

## 7. Communication style

- Be specific: file names, line areas, and **one** recommended fix path.
- If root cause is uncertain, list **ranked hypotheses** and the **single** next experiment (e.g. run one test headed, add one diagnostic report step).
