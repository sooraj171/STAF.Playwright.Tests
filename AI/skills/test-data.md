# Skill: Test data & configuration

**Scope:** **Configuration-driven** automation—no hardcoded environments, accounts, or endpoints in test source.

## Configuration sources (this repo)

1. **`testsetting.runsettings`** — `BaseUrl`, `ApiBaseUrl`, `Browser`, `Headless`, `Environment`, and other parameters consumed by **`ConfigManager.GetParameter(...)`**.
2. **Environment variables** — override with **`STAF_`** prefix (e.g. `STAF_BaseUrl`, `STAF_Headless=true`) for CI/CD.
3. **`testdata.json`** — optional structured data by environment; access via **`ConfigManager.GetTestData(environment, section, key)`** (see README).

## Rules

- **No hardcoding** of URLs, API hosts, or secrets in C# tests or page objects. Use parameters + test data files.
- **Secrets**: use pipeline secret variables, user secrets, or KeyVault patterns **outside** committed JSON—never commit tokens/passwords.
- **Data-driven tests**:
  - **Small matrices** → MSTest **`[DataRow]`** / **`DynamicData`**.
  - **Larger or environment-specific sets** → **`testdata.json`** / `testdata.{Environment}.json`.

## Generate / update

When adding scenarios, deliver:

1. **Runsettings** entries (or document new keys) for any new base URL, feature flag, or timeout parameter.
2. **`testdata.json`** sections for personas, SKUs, or message expectations—**keys** stable, **values** per environment.
3. **Test code** that reads config once per test or in init—**no** duplicated `GetParameter` magic strings scattered widely (use named constants in a static `TestConfig` class if the team adopts that pattern).

## Parallelism & data

- Each parallel worker should use **non-colliding** data (separate users, unique order IDs) via parameterized test data or runtime-generated IDs.

## Anti-patterns

- Commented-out “old URL” blocks in tests.
- Same user password in every test method.
- Global JSON blob with no environment split when QA vs UAT differ.
