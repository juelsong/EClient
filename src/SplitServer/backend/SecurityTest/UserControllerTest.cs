using ESys.Security.Models;
using ESys.UnitTest;
using ESys.Utilty.Defs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SecurityTest
{
    [TestClass]
    public class UserControllerTest
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            UnitTestContext.Instance.Initialize<ESys.App.Startup>("securitytestsettings.json");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            UnitTestContext.Instance.Cleanup();
        }

        [TestMethod]
        public async Task TestChangeSelfPass()
        {
            using var client = UnitTestContext.Instance.GetAdminClient();
            Task<HttpResponseMessage> GetChangeSelfPassResponse(string oriPass, string newPass)
            {
                var changePassSelf = new ChangePassSelf()
                {
                    OriPassword = oriPass,
                    Password = newPass,
                };
                var str = JsonSerializer.Serialize(changePassSelf, UnitTestContext.Instance.DefaultJsonSerializerOptions);
                var content = new StringContent(str, Encoding.UTF8, "application/json");

                return client.PatchAsync("/api/user/self-password", content);
            }
            async Task AssertSucess(HttpResponseMessage rsp)
            {
                Assert.IsNotNull(rsp);
                Assert.AreEqual(rsp.StatusCode, System.Net.HttpStatusCode.OK);
                var str = await rsp.Content.ReadAsStringAsync();
                var ret = JsonSerializer.Deserialize<Result>(str);
                Assert.IsTrue(ret.Success);
            }
            var rsp = await GetChangeSelfPassResponse("ESys_Admin", "admin");
            await AssertSucess(rsp);
            rsp = await GetChangeSelfPassResponse("admin", "ESys_Admin");
            await AssertSucess(rsp);
        }

        [TestMethod]
        public async Task TestChangeSelfPassError()
        {
            using var client = UnitTestContext.Instance.GetAdminClient();
            var changePassSelf = new ChangePassSelf()
            {
                OriPassword = "FFFF",
                Password = "admin",
            };
            var str = JsonSerializer.Serialize(changePassSelf, UnitTestContext.Instance.DefaultJsonSerializerOptions);
            var content = new StringContent(str, Encoding.UTF8, "application/json");

            var rsp = await client.PatchAsync("/api/user/self-password", content);
            Assert.IsNotNull(rsp);
            Assert.AreEqual(rsp.StatusCode, System.Net.HttpStatusCode.OK);
            str = await rsp.Content.ReadAsStringAsync();
            var ret = JsonSerializer.Deserialize<Result>(str, UnitTestContext.Instance.DefaultJsonSerializerOptions);
            Assert.IsFalse(ret.Success);
            Assert.AreEqual(ret.Code, ErrorCode.User.WrongPassword);
        }
    }
}
