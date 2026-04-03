# Work item report templates (markdown)

Copy these skeletons into the files under **`QA/work-items/{Provider}-{WorkItemId}/`**. Replace placeholders. Keep third-person, factual tone suitable for attaching to ADO/Jira or a PR.

---

## `01-pbi-fetch.md`

```markdown
# PBI fetch — {Title}

| Field | Value |
|--------|--------|
| Provider | ADO / JIRA / PASTED |
| Work item ID | |
| URL | |
| State | |
| Fetched at (UTC) | |

## Source content

### Description
(preserve structure; redact secrets)

### Acceptance criteria
(bullet list as in source)

### Links
- Related items:
- PRs / branches (if known):

## Tooling / limitations
- MCP tools used:
- Gaps (e.g. no API access, missing fields):
```

---

## `02-pbi-analysis.md`

```markdown
# PBI analysis — {WorkItemId}

## Goal (one paragraph)

## In-scope behavior

## Out of scope / assumptions

## Testability assessment
| Area | Notes |
|------|--------|
| UI | |
| API | |
| Data / DB | |
| Events / async | |

## Risks and unknowns

## Open questions for PO/Dev

## Traceability seed (AC ids)
| AC # | Short name |
|------|------------|
| AC1 | |
```

---

## `03-test-design.md`

```markdown
# Test design — {WorkItemId}

## Test levels planned

| Level | Applies? | Rationale |
|-------|----------|-----------|
| Unit | Y/N | |
| Integration | Y/N | |
| API | Y/N | |
| UI (E2E) | Y/N | |
| Contract | Y/N | |

## Scope boundaries
- Must cover:
- Explicitly excluded:

## Test techniques
(e.g. equivalence partitioning, state transitions, negative paths, security basics)

## Test data strategy
- Sources (runsettings, testdata.json, API setup):
- PII / secrets handling:

## NFRs touched
(performance, accessibility, logging—if applicable)

## Environment / dependencies
```

---

## `04-test-cases.md`

```markdown
# Test cases — {WorkItemId}

## Review checklist (meta)
- [ ] Each AC mapped to at least one case (or justified gap)
- [ ] Negative and boundary cases included where relevant
- [ ] Preconditions and test data identified
- [ ] Stable expected results (observable, not flaky wording)

## Test cases

### TC-{n}: {short title}
| Field | Value |
|--------|--------|
| ID | TC-{n} |
| AC reference | ACx |
| Level | UI / API / … |
| Priority | P1–P3 |
| Preconditions | |
| Steps | 1. … 2. … |
| Expected result | |
| Automation candidate | Y/N — file/class name if known |

(repeat per case)
```

---

## `05-code-review.md`

```markdown
# Code review (vs PBI) — {WorkItemId}

## Applicability
- Product code present in workspace: Y/N
- Repo paths reviewed:

## Summary
- Implements AC: (yes/partial/no per AC)

## Findings
| Severity | Location | Finding | Suggested action |
|----------|----------|---------|------------------|

## Gaps vs acceptance criteria

## Test hooks / observability
(logging, feature flags, error codes for assertions)
```

---

## `06-test-execution-strategy.md`

```markdown
# Test execution strategy — {WorkItemId}

## Objectives
- What “done” means for this run:

## Environment
- BaseUrl / ApiBaseUrl parameters:
- Browsers / headless:
- Feature flags / config:

## Test selection
- Full suite / filtered:
- Filter expression or class names:

## Commands

Use a fenced `bash` block here, for example: `dotnet test --settings testsetting.runsettings` (adjust to your solution layout).

## Entry criteria
## Exit criteria
## CI pipeline notes (if any)

## Evidence to collect
- Screenshots / traces:
- Framework HTML report paths:
```

---

## `07-summary-report.md`

```markdown
# QA summary — {WorkItemId}

| Field | Value |
|--------|--------|
| Cycle date (UTC) | |
| Executed by | Human / Agent / CI |
| Build / commit | |

## Outcome
- Pass / Fail / Blocked — summary:

## Coverage vs AC
| AC | Cases executed | Result |
|----|------------------|--------|

## Defects found
| ID | Title | Severity | Linked case |
|----|-------|----------|-------------|

## Automation status
- New/updated tests:
- Not automated (with rationale):

## Residual risks

## Recommendations and next steps

## References
- Links to `01`–`06` reports in same folder
```
