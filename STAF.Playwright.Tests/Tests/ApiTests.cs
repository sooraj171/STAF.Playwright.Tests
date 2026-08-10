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

        [TestMethod]
        public async Task Api_RelativeEndpointWithoutLeadingSlash_PreservesBasePath()
        {
            // STAF 3.0 ApiClient trims leading '/' so BaseAddress path is preserved.
            var response = await ApiClient.GetAsync("posts/1").ConfigureAwait(false);
            Assert.IsTrue(response.IsSuccessStatusCode, "GET posts/1 (no leading slash) should succeed");
            await ReportResultAPI.ReportResultPass(StafContext, "API/RelativeEndpoint",
                $"GET posts/1 - Status: {(int)response.StatusCode}").ConfigureAwait(false);
        }

        [TestMethod]
        public async Task Api_PostPutPatchDelete_RoundTrip_ReturnsExpectedStatus()
        {
            var createBody = new { title = "staf", body = "playwright", userId = 1 };
            var post = await ApiClient.PostJsonAsync("/posts", createBody).ConfigureAwait(false);
            Assert.IsTrue(post.IsSuccessStatusCode, "POST /posts should succeed");

            var updateBody = new { id = 1, title = "staf-updated", body = "playwright", userId = 1 };
            var put = await ApiClient.PutJsonAsync("/posts/1", updateBody).ConfigureAwait(false);
            Assert.IsTrue(put.IsSuccessStatusCode, "PUT /posts/1 should succeed");

            var patchBody = new { title = "staf-patched" };
            var patch = await ApiClient.PatchJsonAsync("/posts/1", patchBody).ConfigureAwait(false);
            Assert.IsTrue(patch.IsSuccessStatusCode, "PATCH /posts/1 should succeed");

            var delete = await ApiClient.DeleteAsync("/posts/1").ConfigureAwait(false);
            Assert.IsTrue(delete.IsSuccessStatusCode, "DELETE /posts/1 should succeed");

            await ReportResultAPI.ReportResultPass(StafContext, "API/Mutations",
                $"POST={(int)post.StatusCode}, PUT={(int)put.StatusCode}, PATCH={(int)patch.StatusCode}, DELETE={(int)delete.StatusCode}")
                .ConfigureAwait(false);
        }

        [TestMethod]
        public async Task Api_DeserializeResponse_MapsJsonBody()
        {
            var response = await ApiClient.GetAsync("/posts/1").ConfigureAwait(false);
            Assert.IsTrue(response.IsSuccessStatusCode);

            var post = await ApiClient.DeserializeResponseAsync<JsonPlaceholderPost>(response).ConfigureAwait(false);
            Assert.IsNotNull(post);
            Assert.AreEqual(1, post.Id);
            Assert.IsFalse(string.IsNullOrWhiteSpace(post.Title));

            await ReportResultAPI.ReportResultPass(StafContext, "API/Deserialize",
                $"Deserialized post id={post.Id}, title length={post.Title?.Length ?? 0}").ConfigureAwait(false);
        }

        private sealed class JsonPlaceholderPost
        {
            public int UserId { get; set; }
            public int Id { get; set; }
            public string? Title { get; set; }
            public string? Body { get; set; }
        }
    }
}
