# QA Orchestrator — work-item lifecycle (STLC-aligned)

Use this playbook when the goal is **end-to-end QA** for a **PBI, User Story, Bug, or Feature** linked to STAF.Playwright automation—**not** only when writing a single test file.

## Scope and role

The agent acts as an **AI QA Orchestrator**: it progresses a work item through **fetch → analysis → design → cases → (optional) code review → execution strategy → reporting**, producing **one markdown report per phase** (see paths below).

Automation code in this repo must still follow **`generation-rules.md`**, **`system-prompt.md`**, and **`.cursor/rules/staf-playwright-framework.mdc`**.

## Software Test Life Cycle (STLC) mapping

| STLC phase | Orchestrator step | Primary output |
|------------|-------------------|----------------|
| Requirement analysis | PBI fetch + PBI analysis | `01-pbi-fetch.md`, `02-pbi-analysis.md` |
| Test planning | Test design + execution strategy (outline) | `03-test-design.md`, `06-test-execution-strategy.md` |
| Test case development | Test case generation / review | `04-test-cases.md` |
| Environment setup | (Config/runsettings alignment) | Documented inside `06-test-execution-strategy.md` |
| Test execution | Run `dotnet test` (or CI) per strategy | Framework HTML reports + `07-summary-report.md` |
| Test cycle closure | Summary, defects, traceability | `07-summary-report.md` |

**Test implementation** (writing C# tests and page objects) is a **sub-step** after `04-test-cases.md`; use **`AI/skills/ui-testing.md`**, **`api-testing.md`**, etc., when generating code.

## External systems: Azure DevOps vs Jira (MCP)

**Principle:** Prefer **MCP tools** when the user has configured them. Do **not** assume a specific MCP server name; discover available tools in-session (work item query, get issue, comments, links).

### Azure DevOps (ADO)

When an **Azure DevOps** MCP (or equivalent) is available:

1. Resolve **organization, project, and work item id** from the user or from a URL they paste.
2. **Fetch** the work item (title, description, acceptance criteria, state, area/iteration, tags).
3. Pull **child tasks**, **related PBIs**, and **linked PRs/commits** if the tool supports it.
4. Record **source metadata** (ids, URLs, revision) in `01-pbi-fetch.md`.

### Jira

When a **Jira** MCP (or equivalent) is available:

1. Resolve **issue key** (e.g. `PROJ-123`) and **site/base URL** if needed.
2. **Fetch** summary, description, acceptance criteria (custom fields vary by project—map what is returned).
3. Pull **subtasks**, **linked issues**, and **epic/theme** context when exposed by the tool.
4. Record **source metadata** in `01-pbi-fetch.md`.

### If no work-item MCP is available

- Use **pasted** work item content (description, AC, mocks) as the source of truth.
- State the **gap** explicitly in `01-pbi-fetch.md` under **Tooling / limitations**.

## Local application code (optional)

If an **application repository** is available **inside or alongside** this framework workspace (monorepo, git submodule, or multi-root workspace):

1. Treat **that code** as the **implementation under test** for **static review** and **traceability** (files ↔ acceptance criteria).
2. Prefer **read-only** inspection (search, open files) unless the user asked to change product code.
3. Record reviewed paths, components, and risk calls in `05-code-review.md`.

If **no** application code is present, **skip** deep code review; note the gap in `02-pbi-analysis.md` and `05-code-review.md` as **N/A — product repo not in workspace**.

## Artifact layout (mandatory markdown reports)

For each work item run, create a **dedicated folder**:

`QA/work-items/{Provider}-{WorkItemId}/`

**Provider** examples: `ADO`, `JIRA`, or `PASTED` when the item was not fetched via API.

**Files (one per orchestrator step):**

| File | Step |
|------|------|
| `01-pbi-fetch.md` | Raw/source-aligned fetch log and metadata |
| `02-pbi-analysis.md` | Requirements understanding, risks, open questions |
| `03-test-design.md` | Scope, levels (unit/integration/E2E), techniques, data |
| `04-test-cases.md` | Cases with traceability to AC; review checklist |
| `05-code-review.md` | Code-level review vs PBI (or N/A) |
| `06-test-execution-strategy.md` | Environments, filters, commands, CI, entry/exit |
| `07-summary-report.md` | Consolidated outcome, coverage, defects, next steps |

Use the **section templates** in **`AI/instructions/work-item-report-templates.md`** for headings and tables so outputs stay consistent.

## End-to-end workflow (agent checklist)

Copy and track:

```text
[ ] 01 Fetch work item (MCP or paste) → 01-pbi-fetch.md
[ ] 02 Analyze requirements, risks, testability → 02-pbi-analysis.md
[ ] 03 Test design (levels, scope, NFRs) → 03-test-design.md
[ ] 04 Test cases + AC traceability + review → 04-test-cases.md
[ ] 05 Code review vs PBI if product code in workspace → 05-code-review.md
[ ] 06 Execution strategy (local + CI, data, env) → 06-test-execution-strategy.md
[ ] 07 Implement or schedule automation (optional; use framework skills)
[ ] 08 Execute tests if requested; capture results → 07-summary-report.md
```

**Order:** strictly **01 → 06** for planning artifacts; **07 summary** is filled after execution or when closing the cycle without a run.

## Integration with STAF.Playwright.Tests

- **Implementing** automation: follow **`generation-rules.md`**; use **`ConfigManager`** and correct base classes.
- **Running** tests: `dotnet test` from solution root; respect **`testsetting.runsettings`** parameters documented in repo README.
- **Reporting**: framework HTML reports are separate from these **markdown** orchestrator reports; the summary should **reference** HTML output paths when a run was performed.

## Known gaps and redesign notes

| Gap | Mitigation |
|-----|------------|
| MCP not configured | Use pasted requirements; document in `01-pbi-fetch.md`. |
| Jira/ADO field schemas differ | Map available fields; avoid hardcoding custom field ids in skills—use what MCP returns. |
| Product code absent | Code review step is N/A; analysis may be specification-only. |
| Secrets / prod data | Never store secrets in `QA/work-items/**`; use placeholders and runsettings/env references. |
| Sign-off / compliance | Orchestrator outputs are **drafts**; human QA/PO approval may be required before execution in regulated contexts. |
| Non-functional requirements | If not in the work item, infer only with **assumptions** section and user confirmation. |

**Redesign philosophy:** This playbook is **tool-agnostic** (MCP discovery) and **repo-local** (reports under `QA/work-items/`). Add **project-specific** checklists in a team wiki or a single `QA/team-overrides.md` if needed—do not fork the core STLC mapping for small variations.

## Related files

- **`AI/instructions/work-item-report-templates.md`** — markdown skeletons for `01`–`07`
- **`AI/skills/qa-orchestrator.md`** — skill entry point and triggers
- **`AI/instructions/generation-rules.md`** — automation implementation rules
