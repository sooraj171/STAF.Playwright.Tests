# STAF Playwright — AI handbook index (VS Code / GitHub Copilot)

**There is only one copy of the rules:** the markdown under **`AI/`** at the **repository root**.

- **Cursor** also loads **project skills** from **`.cursor/skills/*/SKILL.md`**. Those files are **stubs**: they tell the agent to read the matching file under **`AI/`**. They do not re-state the rules—so nothing is duplicated.

## Canonical files (attach in chat or open side-by-side)

### Instructions

| File | Use when |
|------|-----------|
| `AI/instructions/system-prompt.md` | Default QA-architect behavior and non-negotiables |
| `AI/instructions/generation-rules.md` | What to generate (tests, pages, data) and how |
| `AI/instructions/debugging-rules.md` | Failed runs, flakes, root-cause analysis |
| `AI/instructions/qa-orchestrator-lifecycle.md` | Work-item / PBI lifecycle, STLC, ADO/Jira MCP, report layout |
| `AI/instructions/work-item-report-templates.md` | Markdown templates for `QA/work-items/.../01`–`07` reports |

### Skills

| File | Use when |
|------|-----------|
| `AI/skills/ui-testing.md` | UI tests, `Pages/*`, POM, `BaseTest` |
| `AI/skills/api-testing.md` | API tests, `ApiClient`, `TestBaseAPI` |
| `AI/skills/db-testing.md` | DB validation and helpers |
| `AI/skills/test-data.md` | Runsettings, `testdata.json`, env overrides |
| `AI/skills/reporting.md` | `ReportResult` / `ReportResultAPI`, HTML reports |
| `AI/skills/framework-rules.md` | Layout, naming, atomic tests, parallel safety |
| `AI/skills/qa-orchestrator.md` | PBI/User Story orchestration, phase reports, MCP fetch |

## Cursor ↔ VS Code parity

| Editor | How rules are discovered |
|--------|---------------------------|
| **Cursor** | `.cursor/rules/staf-playwright-framework.mdc` (always) + optional **Skills** from `.cursor/skills/` (stubs → `AI/`) |
| **VS Code (Copilot)** | `.github/copilot-instructions.md` + **you attach** or open files from **`AI/`** (use this index for paths) |

## Repo-level Copilot instructions

See **`.github/copilot-instructions.md`** — it references the **`AI/`** handbook and stays aligned with the same canonical files.

**Copy-paste prompts and editor-specific steps:** [README.md — AI-assisted automation (copy-paste prompts)](../../README.md#ai-assisted-automation-copy-paste-prompts) (repository root).
