# Skill: Logging & HTML reporting (STAF.Playwright)

**Scope:** Step visibility in **HTML reports**, screenshots on failure, and consistent **`TestContext`** usage.

## Rules

- **UI steps**: use **`ReportResult`** helpers (`ReportResultPass`, `ReportResultFail`, …) with **`Page`** and **`TestContext`** for meaningful steps—not `Console.WriteLine` as the primary trace.
- **API steps**: use **`ReportResultAPI`** with **`TestContext`** after important requests/responses (status, short summary).
- **Assertions**: keep **`Assert.*`** for pass/fail; reporting describes **what** was attempted and **observed** before the assertion where useful.
- **Assembly lifecycle**: **`AssemblyInit`** must continue to delegate to framework **`AssemblyInit`** so **`ResultTemplate.html`** / **`ResultTemplateFinal.html`** aggregate correctly—do not bypass for custom report hacks.

## What to report

| Event | Guidance |
|-------|----------|
| Navigation | After `GotoAsync`, report destination (parameterized URL or relative path). |
| Critical UI state | After verification waits, report success/fail with step name. |
| API call | Method, path, status code; truncate large bodies. |
| Cleanup | Optional pass/fail for teardown only if it affects diagnosis. |

## Failure artifacts

- On failure, framework/reporting typically captures **screenshots** under **`TestResults`**—tell users where to look when debugging (see **`debugging-rules.md`**).

## Anti-patterns

- Logging PII (full credit card, government IDs).
- Reporting **every** line of code—noise hides real failures.
- Swallowing exceptions without a **Fail** report + assertion.
