using STAF.Playwright.Framework;
using STAF.Playwright.Framework.Configuration;

namespace STAF.Playwright.Tests.Tests.Configuration
{
    /// <summary>
    /// Exercises STAF 3.0 configuration: runsettings, STAF_ env overrides, GetOptions, and testdata.
    /// </summary>
    [TestClass]
    [DoNotParallelize] // Mutates STAF_* process environment variables
    public class ConfigFeatureTests
    {
        private string _tempDir = null!;
        private string? _prevStafEnv;
        private string? _prevStafHeadless;

        [TestInitialize]
        public void Initialize()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), "STAF_ConfigFeat_" + Guid.NewGuid().ToString("N")[..8]);
            Directory.CreateDirectory(_tempDir);
            _prevStafEnv = Environment.GetEnvironmentVariable("STAF_Environment");
            _prevStafHeadless = Environment.GetEnvironmentVariable("STAF_Headless");
        }

        [TestCleanup]
        public void Cleanup()
        {
            SetEnv("STAF_Environment", _prevStafEnv);
            SetEnv("STAF_Headless", _prevStafHeadless);
            try
            {
                if (Directory.Exists(_tempDir))
                    Directory.Delete(_tempDir, recursive: true);
            }
            catch { /* ignore */ }
        }

        [TestMethod]
        public void Config_GetOptions_LoadsBrowserStateAndLoginDefaults()
        {
            string runSettingsPath = WriteRunSettings("""
                <?xml version="1.0" encoding="utf-8"?>
                <RunSettings>
                  <TestRunParameters>
                    <Parameter name="BaseUrl" value="https://example.com" />
                    <Parameter name="ApiBaseUrl" value="https://api.example.com/v2" />
                    <Parameter name="Browser" value="Chrome" />
                    <Parameter name="Headless" value="true" />
                    <Parameter name="Environment" value="QA" />
                    <Parameter name="BrowserStateEnabled" value="true" />
                    <Parameter name="SmartSessionRefresh" value="false" />
                    <Parameter name="LoginUrl" value="https://example.com/login" />
                  </TestRunParameters>
                </RunSettings>
                """);

            var config = new ConfigManager(runSettingsPath, testDataPath: null);
            TestRunOptions options = config.GetOptions();

            Assert.AreEqual("https://example.com", options.BaseUrl);
            Assert.AreEqual("https://api.example.com/v2", options.ApiBaseUrl);
            Assert.IsTrue(options.BrowserStateEnabled);
            Assert.IsFalse(options.SmartSessionRefresh);
            Assert.AreEqual("https://example.com/login", options.LoginUrl);
            Assert.AreEqual("#username", options.LoginUsernameSelector);
            Assert.AreEqual("#password", options.LoginPasswordSelector);
            Assert.AreEqual("button[type=submit]", options.LoginSubmitSelector);
        }

        [TestMethod]
        public void Config_StafEnvironmentOverride_WinsOverRunSettings()
        {
            string runSettingsPath = WriteRunSettings("""
                <?xml version="1.0" encoding="utf-8"?>
                <RunSettings>
                  <TestRunParameters>
                    <Parameter name="Environment" value="QA" />
                    <Parameter name="Headless" value="false" />
                  </TestRunParameters>
                </RunSettings>
                """);

            Environment.SetEnvironmentVariable("STAF_Environment", "Prod");
            Environment.SetEnvironmentVariable("STAF_Headless", "true");

            var config = new ConfigManager(runSettingsPath, testDataPath: null);

            Assert.AreEqual("Prod", config.GetParameter("Environment"));
            Assert.AreEqual("Prod", config.GetCurrentEnvironment());
            Assert.IsTrue(config.GetHeadlessMode());
            Assert.AreEqual("Prod", config.GetOptions().Environment);
        }

        [TestMethod]
        public void Config_GetTestData_FromTestdataJson()
        {
            string testDataPath = Path.Combine(_tempDir, "testdata.json");
            File.WriteAllText(testDataPath, """
                {
                  "QA": {
                    "Parent1": {
                      "Child1": "Child1Value",
                      "Child2": "Child2Value"
                    }
                  }
                }
                """);

            var config = new ConfigManager(runSettingsPath: null, testDataPath);
            Assert.IsTrue(config.IsTestDataLoaded);
            Assert.AreEqual("Child1Value", config.GetTestData("QA", "Parent1", "Child1"));
            var section = config.GetTestDataSection("QA", "Parent1");
            Assert.AreEqual(2, section.Count);
            Assert.AreEqual("Child2Value", section["Child2"]);
        }

        [TestMethod]
        public void Config_NullPaths_DoesNotThrow()
        {
            var config = new ConfigManager((string?)null, (string?)null);
            Assert.IsNotNull(config);
            Assert.IsNotNull(config.GetOptions());
        }

        private string WriteRunSettings(string xml)
        {
            string path = Path.Combine(_tempDir, "testsetting.runsettings");
            File.WriteAllText(path, xml);
            return path;
        }

        private static void SetEnv(string name, string? value)
        {
            if (value is null)
                Environment.SetEnvironmentVariable(name, null);
            else
                Environment.SetEnvironmentVariable(name, value);
        }
    }
}
