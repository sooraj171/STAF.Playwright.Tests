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
            await Page.GotoAsync("https://www.google.com");

            GooglePage googlePage = new (Page, TestContext);
            await googlePage.VerifyGooglePageIsDisplayed();
            await googlePage.SearchFor("Playwright");
        }
    }
    
}
