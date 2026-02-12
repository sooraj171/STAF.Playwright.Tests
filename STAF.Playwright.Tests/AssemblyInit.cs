using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace STAF.Playwright.Tests
{
    /// <summary>
    /// Assembly init/cleanup must live in the test assembly so MSTest discovers them.
    /// Delegates to the STAF.Playwright framework to create ResultTemplate.html at start
    /// and ResultTemplateFinal.html (final HTML report) at cleanup.
    /// </summary>
    [TestClass]
    public class AssemblyInit
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext tc)
        {
            STAF.Playwright.Framework.AssemblyInit.AssemblyInitialize(tc);
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            STAF.Playwright.Framework.AssemblyInit.AssemblyCleanUp();
        }
    }
}
