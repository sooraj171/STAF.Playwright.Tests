# AI setup — Cursor & VS Code

Token-efficient guidance for **STAF.Playwright.Tests**. Full agent entry: [AGENTS.md](../../AGENTS.md).

## What loads automatically

| Editor | Always-on | On-demand |
|--------|-----------|-----------|
| **Cursor** | `.cursor/rules/staf-playwright-framework.mdc` | File rules when editing `Pages/` or `Tests/`; `@AI/instructions/*.md`; skills below |
| **VS Code / VS (Copilot)** | `.github/copilot-instructions.md` | Attach files from `AI/` via [`.vscode/staf-ai/INDEX.md`](../../.vscode/staf-ai/INDEX.md); golden `.cs` files |

Open the **repo root** as the workspace folder so paths resolve.

## Cursor skills (project)

Skills live in `.cursor/skills/`. Cursor discovers them from each `SKILL.md` `description`; you can also type `/` and the skill name. **Canonical text** is under **`AI/`** — stubs only point there.

| Skill | Use when |
|-------|----------|
| `staf-ui-testing` | UI tests, page objects |
| `staf-api-testing` | REST / `TestBaseAPI` |
| `staf-framework-rules` | Layout, naming, parallel |
| `staf-reporting` | HTML step reports |
| `staf-test-data` | Runsettings, `testdata.json` |
| `staf-db-testing` | DB validation |
| `staf-qa-orchestrator` | PBI / work-item STLC |
| `staf-ai-instructions` | Generation + debugging playbook |
| `staf-ai-context` | Choosing which files to attach (`@`) |

Master index: [`.cursor/skills/MASTER.md`](../../.cursor/skills/MASTER.md)

## File-scoped rules (Cursor only)

| Rule | Applies when editing |
|------|----------------------|
| `staf-pages.mdc` | `STAF.Playwright.Tests/Pages/**/*.cs` |
| `staf-tests.mdc` | `STAF.Playwright.Tests/Tests/**/*.cs` |

These add **deltas** on top of the always-on framework rule.

## Visual Studio custom agents

Pick from Copilot agent picker (VS 2026 18.4+) or `@staf-ui-automation`, `@staf-api-automation`, `@staf-contract-automation`, `@staf-qa-orchestrator`:

| Agent | File |
|-------|------|
| UI | [.github/agents/staf-ui-automation.agent.md](../../.github/agents/staf-ui-automation.agent.md) |
| API | [.github/agents/staf-api-automation.agent.md](../../.github/agents/staf-api-automation.agent.md) |
| Contract | [.github/agents/staf-contract-automation.agent.md](../../.github/agents/staf-contract-automation.agent.md) |
| QA orchestrator | [.github/agents/staf-qa-orchestrator.agent.md](../../.github/agents/staf-qa-orchestrator.agent.md) |

## MCP (browser + Azure DevOps)

1. Restart editor after clone.
2. Confirm **playwright-csharp** / **playwrightCsharp** in the MCP panel (`.cursor/mcp.json`, `.vscode/mcp.json`, or `.mcp.json` for VS).
3. For work-item fetch, configure **azure-devops** with org, project, team, and PAT (`ADO_MCP_AUTH_TOKEN`). See [README.md — Using MCP servers](../../README.md#using-mcp-servers).
4. Combine MCP with instructions: *"Use playwright tools to inspect the page, then generate a BaseTest test using GooglePage pattern."*

## VS Code Copilot tips

- Use **@workspace** for repo-wide questions; attach **one** golden file for codegen (e.g. `@STAF.Playwright.Tests/Pages/GooglePage.cs`).
- If output drifts to raw Playwright: *"Follow STAF: BaseTest, BasePage, ReportResult, no Thread.Sleep."*
- Repository instructions: `.github/copilot-instructions.md` (aligned with Cursor always-on rule).

## Copy-paste prompts

Ready-made prompts: [README.md — AI-assisted automation](../../README.md#ai-assisted-automation-copy-paste-prompts)

Quick task entry: [QUICK_START.md](./QUICK_START.md)
