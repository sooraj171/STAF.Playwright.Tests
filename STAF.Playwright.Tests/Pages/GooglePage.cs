using Microsoft.Playwright;
using STAF.Playwright.Framework;
using STAF.Playwright.Framework.Core;

namespace STAF.Playwright.Pages
{
    public class GooglePage : BasePage
    {
        private readonly IStafTestContext _testContext;

        public GooglePage(IPage page, TestContext testContext) : base(page, testContext)
        {
            _testContext = new STAF.Playwright.Framework.Runners.MSTest.MsTestContextAdapter(testContext);
        }

        /// <summary>STAF 3.0 multi-runner constructor.</summary>
        public GooglePage(IPage page, IStafTestContext testContext) : base(page, testContext)
        {
            _testContext = testContext;
        }

        #region elements
        public ILocator SearchBox => Page.Locator("[title='Search']");
        private const string SearchBoxSelector = "[title='Search']";
        #endregion

        public async Task VerifyGooglePageIsDisplayed()
        {
            if (await WaitForElementVisibleAsync(SearchBox, 5000))
            {
                await ReportResult.ReportResultPass(Page, _testContext, "Google Page", "Google page is displayed.");
            }
            else
            {
                await ReportResult.ReportResultFail(Page, _testContext, "Google Page", "Google page was not displayed.");
                Assert.Fail("Google page was not displayed.");
            }
        }

        public async Task SearchFor(string search)
        {
            // Prefer STAF 3.0 selector overloads (automatic step reporting).
            await EnterTextAsync(SearchBoxSelector, search, "Search for " + search);
            await PressAsync(SearchBoxSelector, "Enter", "Submit search");
        }
    }
}
