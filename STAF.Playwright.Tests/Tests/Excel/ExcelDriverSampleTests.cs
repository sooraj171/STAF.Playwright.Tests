using ClosedXML.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STAF.Playwright.Framework.Excel;

namespace STAF.Playwright.Tests.Tests.Excel
{
    /// <summary>
    /// Sample tests for the ExcelDriver from STAF.Playwright (read, write, compare Excel files).
    /// Uses a temp directory; no external files required.
    /// </summary>
    [TestClass]
    public class ExcelDriverSampleTests
    {
        private ExcelDriver _driver = null!;
        private string _tempDir = null!;

        [TestInitialize]
        public void Initialize()
        {
            _driver = new ExcelDriver();
            _tempDir = Path.Combine(Path.GetTempPath(), "STAF_Excel_" + Guid.NewGuid().ToString("N"));
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

        private string TempPath(string fileName) => Path.Combine(_tempDir, fileName);

        [TestMethod]
        public void Excel_CreateWorkbook_AddSheet_SetAndGetCell_SaveAndOpen()
        {
            using (var workbook = _driver.CreateWorkbook())
            {
                _driver.AddSheet(workbook, "Data");
                _driver.SetCellValue(workbook, 1, 1, 1, "Hello");
                _driver.SetCellValue(workbook, 1, 1, 2, "STAF");
                string path = TempPath("sample.xlsx");
                _driver.Save(workbook, path);
                Assert.IsTrue(File.Exists(path));
            }

            using (var opened = _driver.Open(TempPath("sample.xlsx")))
            {
                Assert.AreEqual("Hello", _driver.GetCellValue(opened, 1, 1, 1));
                Assert.AreEqual("STAF", _driver.GetCellValue(opened, 1, 1, 2));
            }
        }

        [TestMethod]
        public void Excel_CompareFiles_WhenIdentical_ReturnsMatching()
        {
            string path = CreateSampleFile("a.xlsx", "A1", "Same", "B1", "Value");
            var status = _driver.CompareFiles(path, path);
            Assert.IsTrue(status.IsMatching);
            Assert.IsEmpty(status.CellDifferences);
        }

        [TestMethod]
        public void Excel_CompareFiles_WhenDifferent_ReturnsCellDifferences()
        {
            string path1 = CreateSampleFile("p1.xlsx", "A1", "Same", "B1", "First");
            string path2 = CreateSampleFile("p2.xlsx", "A1", "Same", "B1", "Second");
            var status = _driver.CompareFiles(path1, path2);
            Assert.IsFalse(status.IsMatching);
            Assert.IsGreaterThanOrEqualTo(status.CellDifferences.Count, 1);
        }

        private string CreateSampleFile(string fileName, params string[] cellValuePairs)
        {
            string path = TempPath(fileName);
            using var workbook = new XLWorkbook();
            var sheet = workbook.Worksheets.Add("Sheet1");
            for (int i = 0; i < cellValuePairs.Length - 1; i += 2)
                sheet.Cell(cellValuePairs[i]).Value = cellValuePairs[i + 1];
            workbook.SaveAs(path);
            return path;
        }
    }
}
