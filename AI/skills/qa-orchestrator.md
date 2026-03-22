# QA Orchestrator skill (work-item / PBI lifecycle)

## When to use

- User mentions **PBI**, **User Story**, **work item**, **Jira**, **Azure DevOps**, **acceptance criteria**, or **end-to-end QA** from requirements through execution.
- User wants **markdown reports** per phase under **`QA/work-items/`**.
- User wants **test design / test cases** **before** or **alongside** STAF.Playwright test code.

## Canonical instructions

1. **`AI/instructions/qa-orchestrator-lifecycle.md`** — STLC mapping, MCP usage, folder layout, gaps.
2. **`AI/instructions/work-item-report-templates.md`** — templates for `01`–`07` markdown files.

## Agent behavior (concise)

1. **Discover tools:** If ADO or Jira MCP tools exist, use them to fetch the item; otherwise require paste and set Provider to `PASTED`.
2. **Create folder** `QA/work-items/{Provider}-{WorkItemId}/` (sanitize id for filesystem safety: replace `/\:*?"<>|`).
3. **Write one file per phase** using templates; do not merge phases into a single file unless the user explicitly asks for a combined report.
4. **Traceability:** Every acceptance criterion should appear in `04-test-cases.md` or be called out as intentionally uncovered in `02` or `03`.
5. **Code review:** Only if application source is in the workspace; otherwise `05-code-review.md` states N/A with reason.
6. **Implementation:** When user asks to automate, switch to **`AI/instructions/generation-rules.md`** and **`AI/skills/ui-testing.md`** / **`api-testing.md`** as appropriate; keep orchestrator reports updated in `07-summary-report.md` after runs.

## STAF-specific reminders

- Execution strategy must reference **`ConfigManager`** parameters and **`testsetting.runsettings`**, not hardcoded URLs.
- Prefer **atomic tests** and **page objects** per **`AI/skills/framework-rules.md`**.

## Anti-patterns

- Skipping `02-pbi-analysis.md` and jumping straight to code (unless user explicitly requests code-only).
- Storing credentials, tokens, or production PII inside `QA/work-items/**`.
- Assuming an MCP server name; always use **actually available** MCP tools in the session.
