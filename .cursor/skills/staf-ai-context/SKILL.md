---
name: staf-ai-context
description: >-
  Minimizes tokens when working in STAF.Playwright.Tests by choosing which AI/
  docs and source files to attach. Use before large codegen, repo exploration, or
  when the user asks to reduce context or @-mentions.
---

# STAF context loading

## Default (most chats)

- **No extra files** — always-on rule + user message is enough for small edits.
- Copilot: `.github/copilot-instructions.md` | Cursor: `.cursor/rules/staf-playwright-framework.mdc`

## By task

| Task | Attach (`@`) |
|------|----------------|
| New UI test (page exists) | `STAF.Playwright.Tests/Tests/Test1.cs` OR `Pages/GooglePage.cs` + `AI/skills/ui-testing.md` |
| New page object | `Pages/GooglePage.cs` + `AI/skills/ui-testing.md` |
| New API test | `Tests/ApiTests.cs` + `AI/skills/api-testing.md` |
| Contract test | `Tests/ContractTests.cs` + `AI/skills/framework-rules.md` |
| Work item / PBI QA | `AI/instructions/qa-orchestrator-lifecycle.md` + `AI/skills/qa-orchestrator.md` |
| Framework depth (output format, parallel) | `AI/instructions/generation-rules.md` |
| Failure analysis | `AI/instructions/debugging-rules.md` + skill for failed layer |
| Full persona | `AI/instructions/system-prompt.md` (one file only) |

## Do not

- Paste entire `STAF.Playwright.Tests/` or solution into context
- Load all of `AI/instructions/` + all golden files together
- Read `MCPAgent/` binaries for C# patterns

## Editor setup

Human onboarding: [AI/instructions/ai-setup.md](../../AI/instructions/ai-setup.md) | Index: [.cursor/skills/MASTER.md](../MASTER.md) | VS Code: [.vscode/staf-ai/INDEX.md](../../.vscode/staf-ai/INDEX.md)
