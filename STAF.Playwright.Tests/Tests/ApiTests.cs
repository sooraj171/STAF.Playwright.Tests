using STAF.Playwright.Framework;

namespace STAF.Playwright.Tests
{
    /// <summary>
    /// Sample API tests using a free public API (JSONPlaceholder).
    /// ApiBaseUrl is set in testsetting.runsettings (https://jsonplaceholder.typicode.com).
    /// Inherits TestBaseAPI for ConfigManager and ApiClient with automatic request reporting.
    /// </summary>
    [TestClass]
    public class ApiTests : TestBaseAPI
    {
        [TestMethod]
        public async Task Api_GetPost_ReturnsOkAndBody()
        {
            var response = await ApiClient.GetAsync("/posts/1").ConfigureAwait(false);
            var body = await ApiClient.GetResponseAsStringAsync(response).ConfigureAwait(false);

            await ReportResultAPI.ReportResultPass(TestContext, "API/GetPost",
                $"GET /posts/1 - Status: {(int)response.StatusCode}, Body length: {body?.Length ?? 0} chars").ConfigureAwait(false);

            Assert.IsTrue(response.IsSuccessStatusCode, "GET /posts/1 should succeed");
            Assert.IsFalse(string.IsNullOrWhiteSpace(body), "Response body should not be empty");
        }

        [TestMethod]
        public async Task Api_GetUser_ReturnsOk()
        {
            var response = await ApiClient.GetAsync("/users/1").ConfigureAwait(false);

            await ReportResultAPI.ReportResultPass(TestContext, "API/GetUser",
                $"GET /users/1 - Status: {(int)response.StatusCode} {response.StatusCode}").ConfigureAwait(false);

            Assert.IsTrue(response.IsSuccessStatusCode, "GET /users/1 should succeed");
        }
    }
}
