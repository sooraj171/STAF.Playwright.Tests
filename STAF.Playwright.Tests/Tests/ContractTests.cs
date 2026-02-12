using STAF.Playwright.Framework.ContractTesting;

namespace STAF.Playwright.Tests
{
    /// <summary>
    /// OpenAPI contract tests. Validates API responses against OpenAPI specs in the OpenAPI folder.
    /// Uses ApiBaseUrl from testsetting.runsettings (e.g. https://jsonplaceholder.typicode.com).
    /// </summary>
    [TestClass]
    public class ContractTests : OpenApiContractTestBase
    {
        protected override string OpenApiSpecFolder
        {
            get
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
                return Path.Combine(baseDir, "OpenAPI");
            }
        }

        [TestMethod]
        public async Task Contract_AllOperations_StatusCodeOnly_ShouldMatchSpec()
        {
            var results = await RunAllContractTestsAsync(validateSchema: false).ConfigureAwait(false);

            Assert.IsNotEmpty(results,
                "No operations found in OpenAPI specs. Ensure OpenAPI folder contains .json or .yaml and is copied to output.");

            AssertAllContractTestsPassed(results);
        }

        [TestMethod]
        public async Task Contract_AllOperations_WithSchema_ShouldMatchSpec()
        {
            var results = await RunAllContractTestsAsync(validateSchema: true).ConfigureAwait(false);

            Assert.IsNotEmpty(results,
                "No operations found in OpenAPI specs.");

            AssertAllContractTestsPassed(results);
        }
    }
}
