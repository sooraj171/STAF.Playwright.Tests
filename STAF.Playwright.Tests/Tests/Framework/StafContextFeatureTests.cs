using STAF.Playwright.Framework;
using STAF.Playwright.Framework.Core;

namespace STAF.Playwright.Tests.Tests.Framework
{
    /// <summary>
    /// Exercises STAF 3.0 runner-agnostic IStafTestContext and StafContext on base classes.
    /// </summary>
    [TestClass]
    public class StafContextFeatureTests
    {
        [TestMethod]
        public void SimpleStafTestContext_RequiresTestName()
        {
            Assert.ThrowsExactly<ArgumentException>(() => new SimpleStafTestContext(null!));
            Assert.ThrowsExactly<ArgumentException>(() => new SimpleStafTestContext("  "));

            var ctx = new SimpleStafTestContext("MyTest");
            Assert.AreEqual("MyTest", ctx.TestName);
            ctx.AddResultFile("unused.html"); // no-op for NUnit/xUnit adapter
        }

        [TestMethod]
        public async Task ReportResultAPI_AcceptsIStafTestContext()
        {
            var ctx = new SimpleStafTestContext(nameof(ReportResultAPI_AcceptsIStafTestContext));
            await ReportResultAPI.ReportResultPass(ctx, "Framework/StafContext", "IStafTestContext overload OK");
            await ReportResultAPI.ReportResultFail(ctx, "Framework/StafContext", "Fail overload reachable (manual report)");
        }
    }

    [TestClass]
    public class StafContextUiSmokeTests : BaseTest
    {
        [TestMethod]
        public async Task BaseTest_StafContext_IsAvailableForReporting()
        {
            Assert.IsNotNull(StafContext);
            Assert.IsFalse(string.IsNullOrWhiteSpace(StafContext.TestName));

            string? url = ConfigManager.GetParameter("BaseUrl");
            if (!string.IsNullOrWhiteSpace(url))
                await Page.GotoAsync(url);

            await ReportResult.ReportResultPass(Page, StafContext, "Framework/StafContext", "UI reporting via IStafTestContext");
        }
    }

    [TestClass]
    public class StafContextApiSmokeTests : TestBaseAPI
    {
        [TestMethod]
        public async Task TestBaseAPI_StafContext_IsAvailableForReporting()
        {
            Assert.IsNotNull(StafContext);
            Assert.IsFalse(string.IsNullOrWhiteSpace(StafContext.TestName));

            var response = await ApiClient.GetAsync("posts/1");
            Assert.IsTrue(response.IsSuccessStatusCode);

            await ReportResultAPI.ReportResultPass(StafContext, "Framework/StafContext",
                $"GET posts/1 via relative endpoint - {(int)response.StatusCode}");
        }
    }
}
