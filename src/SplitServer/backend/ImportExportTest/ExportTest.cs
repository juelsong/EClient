namespace ImportExportTest
{
    using ESys.Infrastructure.Entity;
    using ESys.UnitTest;
    using ESys.Utilty.Defs;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    [TestClass]
    public class ExportTest
    {

        [ClassInitialize]
        public static void ClassInitialize(TestContext ctx)
        {
            UnitTestContext.Instance.Initialize<ESys.App.Startup>("importexportsettings.json");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            UnitTestContext.Instance.Cleanup();
        }
        [TestMethod]
        public async Task TestExport()
        {
            using var client = UnitTestContext.Instance.GetAdminClient();

            Task<HttpResponseMessage> GetExcel(string type)
            {
                var str = JsonSerializer.Serialize(new { }, UnitTestContext.Instance.DefaultJsonSerializerOptions);
                var content = new StringContent(str, Encoding.UTF8, "application/json");

                return client.GetAsync($"/ImportAndExport/exportExcel/{type}"/*, content*/);
            }
            async Task AssertSucess(HttpResponseMessage rsp)
            {
                Assert.IsNotNull(rsp);
                Assert.AreEqual(rsp.StatusCode, System.Net.HttpStatusCode.OK);
                var str = await rsp.Content.ReadAsStringAsync();
                var ret = JsonSerializer.Deserialize<Result>(str);
                Assert.IsTrue(ret.Success);
            }
            var rsp = await GetExcel(nameof(Location));
            await AssertSucess(rsp);
            var path = Path.GetDirectoryName("测试文件.xlsx");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var stream = rsp.Content.ReadAsStreamAsync().Result;
            using var fs = File.Create(path);
            stream.CopyTo(fs);
            Assert.IsTrue(stream.Length > 0);

        }
        //[TestMethod]
        //public void Testsrt()
        //{
        //    Assert.IsTrue(1>0);

        //}
    }
}