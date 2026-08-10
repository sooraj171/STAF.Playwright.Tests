using STAF.Playwright.Framework;

namespace STAF.Playwright.Tests.Tests.BrowserState
{
    /// <summary>
    /// Exercises STAF 3.0 BrowserStateManager path helpers (no live login required).
    /// </summary>
    [TestClass]
    public class BrowserStateFeatureTests
    {
        private string _tempDir = null!;

        [TestInitialize]
        public void Initialize()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), "STAF_BSFeat_" + Guid.NewGuid().ToString("N")[..8]);
            Directory.CreateDirectory(_tempDir);
        }

        [TestCleanup]
        public void Cleanup()
        {
            try
            {
                if (Directory.Exists(_tempDir))
                    Directory.Delete(_tempDir, recursive: true);
            }
            catch { /* ignore */ }
        }

        [TestMethod]
        public void BrowserState_GetDirectory_CreatesBrowserStateSubfolder()
        {
            string dir = BrowserStateManager.GetBrowserStateDirectory(_tempDir);
            Assert.AreEqual(Path.Combine(_tempDir, "BrowserState"), dir);
            Assert.IsTrue(Directory.Exists(dir));
        }

        [TestMethod]
        public void BrowserState_GetPath_UsesSanitizedUsername()
        {
            string path = BrowserStateManager.GetBrowserStatePath("admin", _tempDir);
            Assert.AreEqual(Path.Combine(_tempDir, "BrowserState", "browserstate_admin.json"), path);

            string unsafePath = BrowserStateManager.GetBrowserStatePath("user:name", _tempDir);
            Assert.IsFalse(Path.GetFileName(unsafePath).Contains(':'));
            Assert.IsTrue(unsafePath.EndsWith(".json", StringComparison.Ordinal));
        }

        [TestMethod]
        public void BrowserState_GetPath_NullOrEmptyUsername_Throws()
        {
            Assert.ThrowsExactly<ArgumentException>(() => BrowserStateManager.GetBrowserStatePath(null!, _tempDir));
            Assert.ThrowsExactly<ArgumentException>(() => BrowserStateManager.GetBrowserStatePath("  ", _tempDir));
        }

        [TestMethod]
        public void BrowserState_DeleteAll_RemovesOnlyStateFiles()
        {
            string stateDir = BrowserStateManager.GetBrowserStateDirectory(_tempDir);
            string stateFile = Path.Combine(stateDir, "browserstate_admin.json");
            string otherFile = Path.Combine(stateDir, "notes.json");
            File.WriteAllText(stateFile, "{}");
            File.WriteAllText(otherFile, "{}");

            BrowserStateManager.DeleteAllBrowserStateFiles(_tempDir);

            Assert.IsFalse(File.Exists(stateFile));
            Assert.IsTrue(File.Exists(otherFile));
        }
    }
}
