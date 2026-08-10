using STAF.Playwright.Framework;

namespace STAF.Playwright.Tests.Tests.Framework
{
    /// <summary>
    /// SqlDbClient surface checks that do not require a live SQL Server.
    /// </summary>
    [TestClass]
    public class SqlDbClientFeatureTests
    {
        [TestMethod]
        public void SqlDbClient_NullOrEmptyConnectionString_Throws()
        {
            Assert.ThrowsExactly<ArgumentException>(() => new SqlDbClient(null!));
            Assert.ThrowsExactly<ArgumentException>(() => new SqlDbClient("   "));
        }

        [TestMethod]
        public async Task SqlDbClient_TestConnection_WithInvalidServer_ReturnsFalse()
        {
            using var client = new SqlDbClient(
                "Server=127.0.0.1,1;Database=does_not_exist;User Id=sa;Password=x;TrustServerCertificate=True;Connect Timeout=1");
            bool ok = await client.TestConnectionAsync();
            Assert.IsFalse(ok);
        }

        [TestMethod]
        public void TestBaseAPI_ExposesProtectedDbClient()
        {
            var prop = typeof(TestBaseAPI).GetProperty(
                "DbClient",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);

            Assert.IsNotNull(prop, "TestBaseAPI should expose DbClient in STAF 3.0");
            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            Assert.AreEqual(typeof(SqlDbClient), type);
        }
    }
}
