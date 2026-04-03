---
name: staf-ai-instructions
description: >-
  STAF.Playwright AI playbook—persona, test generation output format, coverage
  strategy, and debugging. Use when generating new tests from requirements,
  defining output bundles, or analyzing failures and flakiness.
---

# STAF AI instructions (generation and debugging)

**Canonical content (single source of truth):** read and follow these files at the repository root, in order when relevant:

1. **`AI/instructions/system-prompt.md`** — persona and guardrails  
2. **`AI/instructions/generation-rules.md`** — coverage, output format, optimization  
3. **`AI/instructions/debugging-rules.md`** — failure analysis and stabilization  
4. **`AI/instructions/qa-orchestrator-lifecycle.md`** — when driving **PBIs / work items** end-to-end (STLC phase reports under `QA/work-items/`); templates in **`work-item-report-templates.md`**

Do not duplicate those instructions here. If anything conflicts, the **`AI/instructions/*.md`** files win. For orchestrator-only workflows, the **`staf-qa-orchestrator`** skill is the primary entry point.
