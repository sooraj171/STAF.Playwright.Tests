using STAF.Playwright.Framework;
using STAF.Playwright.Pages;

namespace STAF.Playwright
{
    [TestClass]
    public sealed class Test1 : BaseTest
    {
        //TestContext TestContext { get; set; }
        [TestMethod]
        public async Task TestMethod1()
        {
            // BaseTest navigates to BaseUrl from testsetting.runsettings in TestInitialize; optional override:
            await Page.GotoAsync(ConfigManager.GetParameter("BaseUrl") ?? "https://www.google.com");

            GooglePage googlePage = new (Page, TestContext);
            await googlePage.VerifyGooglePageIsDisplayed();
            await googlePage.SearchFor("Playwright");
        }
    }
    
}
