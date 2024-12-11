using ESys.Schedule.Controllers;
using ESys.UnitTest;
using ESys.Utilty.Defs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScheduleControllerTest
{
    [TestClass]
    public class ResultEntryControllerTest
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            UnitTestContext.Instance.Initialize<ESys.App.Startup>("schedulecontrollersettings.json");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            UnitTestContext.Instance.Cleanup();
        }

        [TestMethod]
        public async Task TestResultEntry()
        {
            using var client = UnitTestContext.Instance.GetAdminClient();
            var currentTasks = new List<CurrentTask>();
            for (int i = 0; i < 1; i++)
            {
                currentTasks.Add(new CurrentTask()
                {
                    CurrentId = 256,
                    Medias = new List<MediaData>() { new MediaData() { Id = 1 } },
                    Devices = new List<DeviceData>() { new DeviceData() { Id = 1 } },
                    ProductNote = "testnote1",
                    Note = "测试数据，我是假的",
                    ResultDataSet = new List<ResultData>()
                    {
                        new ResultData()
                        {
                        MeasurementId = 1,
                        SignId = 5,
                        UnitOfMeasureId = 8,
                        Value =new Random().Next(100)
                        },
                        new ResultData()
                        {
                        MeasurementId = 8,
                        SignId = 5,
                        UnitOfMeasureId = 8,
                        Value =new Random().Next(100)
                        },
                        new ResultData()
                        {
                        MeasurementId = 9,
                        SignId = 5,
                        UnitOfMeasureId = 8,
                        Value =new Random().Next(100)
                        }
                    }
                });
            }
            var generateData = new ImportData()
            {
                CurrentTasks = currentTasks

            };
            var str = JsonSerializer.Serialize(generateData, UnitTestContext.Instance.DefaultJsonSerializerOptions);
            var content = new StringContent(str, Encoding.UTF8, "application/json");
            var rsp = await client.PostAsync("/ResultEntry/ESignTasks", content);
            Assert.IsNotNull(rsp);
            Assert.AreEqual(rsp.StatusCode, System.Net.HttpStatusCode.OK);
            str = await rsp.Content.ReadAsStringAsync();
            var ret = JsonSerializer.Deserialize<Result>(str, UnitTestContext.Instance.DefaultJsonSerializerOptions);
            Assert.IsFalse(ret.Success);
            Assert.AreEqual(ret.Code, ErrorCode.User.WrongPassword);
        }

    }

}
