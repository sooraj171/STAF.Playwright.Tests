# Skill: Database validation (enterprise)

**Scope:** Validating **backend state** after UI or API actions, or asserting **data integrity** in test automation for this solution.

## Rules

- **Use DB helper utilities** provided by your organization’s extension of STAF or shared test libraries—**never** embed **raw ADO.NET/SQL strings** in tests when an abstraction already exists (repository, query helper, seeded data API).
- **Never embed SQL directly** in test code if:
  - A **framework or team DB helper** exists for that query pattern, or
  - The application exposes a **supported read API** for verification (often preferable for black-box tests).
- **Validate**:
  - **Data integrity** — expected rows, key fields, soft-delete flags, audit columns where relevant.
  - **Transactions** — commit/rollback behavior when the scenario requires it (often via API + DB read, not UI).
  - **Backend impact of UI/API** — same test creates via UI/API, asserts via DB helper with **clear isolation** (unique keys per run).

## Parallel execution

- Use **unique** business keys (GUID suffix, timestamp from test context) when inserting rows.
- Avoid **global cleanup** that deletes data other tests rely on; scope cleanup to rows created in that test or use transactions if your helper supports rollback.

## Generate

1. **Queries** — via approved helpers (e.g. `ExecuteScalarAsync`, `QuerySingleAsync`, LINQ against test DB context—whatever your stack standardizes).
2. **Validation assertions** — `Assert.*` with messages that show **expected vs actual** summaries (not full table dumps in CI logs unless needed).

## When DB checks are inappropriate

- Prefer **API contract tests** for schema stability.
- Prefer **idempotent APIs** + GET verification when DB access is restricted in CI.

## Anti-patterns

- Storing **production** connection strings in source.
- Tests that **order-depend** on seeded data without recreating it.
- Direct SQL concatenation with unparameterized user input.

> **Note:** This sample repo focuses on UI/API/contract/Excel. If no DB package is referenced, treat this skill as the **standard** for when your enterprise adds `STAF` DB helpers or a shared data access test project.
