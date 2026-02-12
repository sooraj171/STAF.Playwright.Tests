
using Microsoft.Playwright;
using STAF.Playwright.Framework;

namespace STAF.Playwright.Pages
{
    public class GooglePage : BasePage
    {
        public IPage _google;
        TestContext TestContext { get; set; }
        string TestName { get; set; }
        public GooglePage(IPage page, TestContext testContext) : base(page, testContext)
        {
            _google = page;
            TestContext = testContext;
            TestName = testContext.TestName??"NoName";
        }

        #region elements
        public ILocator SearchBox => _google.Locator("[title='Search']"); //FindAppElementAsync("input[name='q']");

        #endregion

        public async Task VerifyGooglePageIsDisplayed()
        {
            if (await WaitForElementVisibleAsync(SearchBox, 5000))
            {
                await ReportResult.ReportResultPass(Page, TestContext, "Google Page", "Google page is displayed.");
            }
            else
            {
                await ReportResult.ReportResultFail(Page, TestContext, "Google Page", "Google page was not displayed.");
                Assert.Fail("Google page was not displayed.");
            }
        }

        public async Task SearchFor(string search)
        {
            var searchBox = SearchBox;
            await EnterTextAsync(searchBox, search, TestName, "Search for " + search);
            await PressAsync(searchBox, "Enter", TestName, "Submit search");
        }
    }
}
