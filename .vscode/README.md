# VS Code

## Test run settings

If you use the C# Dev Kit test explorer, point run settings at `STAF.Playwright.Tests/testsetting.runsettings` (see workspace `settings.json` if present).

## GitHub Copilot

Repository instructions load from [.github/copilot-instructions.md](../.github/copilot-instructions.md).

For STAF test patterns and attach lists, see:

- [AI/instructions/QUICK_START.md](../AI/instructions/QUICK_START.md) — quick tasks
- [AI/instructions/ai-setup.md](../AI/instructions/ai-setup.md) — editor setup
- [.vscode/staf-ai/INDEX.md](staf-ai/INDEX.md) — handbook table of contents (`AI/` paths to attach in chat)
- [AGENTS.md](../AGENTS.md) — cross-tool agents summary

### Visual Studio custom agents (same repo)

When using Visual Studio 2026+ Copilot, pick specialized agents from `.github/agents/` (`staf-ui-automation`, `staf-api-automation`, `staf-contract-automation`, `staf-qa-orchestrator`). VS Code uses attach + repo instructions instead of custom agent files.

## MCP

Configured in [.vscode/mcp.json](mcp.json):

| Server | Executable | Notes |
|--------|------------|--------|
| **playwrightCsharp** | `MCPAgent/PlaywrightCSharpMcp.exe` | Browser / codegen — no credentials |
| **azure-devops** | `MCPAgent/AzureDevOps/AzureDevOps.Mcp.Server.exe` | Prompts for org + PAT; set `ado_mcp_project` / `ado_mcp_team` in `mcp.json` |

Full setup (PAT scopes, auth modes, verification): [README.md — Using MCP servers](../README.md#using-mcp-servers).
