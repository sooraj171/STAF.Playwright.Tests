---
name: staf-qa-orchestrator
description: >-
  End-to-end QA lifecycle for a work item (PBI/User Story): ADO or Jira fetch
  via MCP when available, analysis, test design, test cases, optional code
  review, execution strategy, and phase markdown reports under QA/work-items/.
  Use when the user wants PBI review, STLC-aligned QA, or orchestrated reports
  before or alongside STAF.Playwright automation.
---

# STAF QA Orchestrator (work-item lifecycle)

**Canonical content:** read and follow, in order:

1. **`AI/instructions/qa-orchestrator-lifecycle.md`** — STLC phases, MCP (Azure DevOps / Jira), artifact paths, gaps  
2. **`AI/instructions/work-item-report-templates.md`** — markdown skeletons for each report file  
3. **`AI/skills/qa-orchestrator.md`** — triggers, agent behavior, STAF reminders  

For **implementing** tests after cases exist, use **`AI/instructions/generation-rules.md`** and the existing UI/API/framework skills.

If anything conflicts with framework coding rules, **`.cursor/rules/staf-playwright-framework.mdc`** and **`AI/instructions/generation-rules.md`** win for code; orchestrator docs win for **report structure and lifecycle steps**.
