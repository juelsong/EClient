namespace RandomDataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Text.Json;
    using System.Text.Unicode;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public class ResultGenerator
    {
        public readonly decimal AlertLimit = 50;
        public readonly decimal ActionLimit = 100;

        public string GetRandomData()
        {
            var locationDatas = new List<Location>();
            for (int i = 0; i < 3; i++)//i is location
            {
                var location = new Location()
                {
                    LocationId = i,
                    Classification = "A",
                    LocationName = $"Location_{i}",
                    Environment = "Static",
                    TestTypeDatas = new List<TestTypeData>()
                };
                var value = new Random().NextDouble();
                var dataLength = new Random().NextInt64(10);

                #region 悬浮粒子
                var testTypeData = new TestTypeData()
                {         
                    LocationId = i,
                    TestTypeId = 1,
                    TestTypeName = $"悬浮粒子{i}",
                    TestDatas = new List<TestData>()
                };
                for (int data = 0; data < dataLength; data++)
                {
                    var code = $"code_{data}";
                    var currentDate = DateTime.Now;
                    var limitLevel = value % 2 == 0 ?
                        LimitLevel.Action : value % 2 > 0 ?
                        LimitLevel.Alert : LimitLevel.None;
                    testTypeData.TestDatas.Add(
                    new TestData()
                    {
                        LocationId = i,
                        TestTypeId = 1,
                        TestId = data,
                        Barcode = code,
                        TestDateTime = currentDate,
                        LimitLevel = limitLevel,
                        LimitDescription = value.ToString(),
                        Name = "05Count",
                        Value = new Random().NextDouble()
                    });
                    testTypeData.TestDatas.Add(
                    new TestData()
                    {
                        LocationId = i,
                        TestTypeId = 1,
                        TestId = data,
                        Barcode = code,
                        TestDateTime = currentDate,
                        LimitLevel = limitLevel,
                        LimitDescription = value.ToString(),
                        Name = "50Count",
                        Value = new Random().NextDouble()
                    });
                }
                location.TestTypeDatas.Add(testTypeData);
                #endregion

                #region 浮游菌 
                value = new Random().NextDouble();
                var testTypeData2 = new TestTypeData()
                {
                    LocationId = i,
                    TestTypeId = 2,
                    TestTypeName = "浮游菌",
                    TestDatas = new List<TestData>()
                };
                for (int data = 0; data < dataLength; data++)
                {
                    testTypeData2.TestDatas.Add(new TestData()
                    {
                        LocationId = i,
                        TestTypeId = 2,
                        TestId= data,
                        Barcode = Guid.NewGuid().ToString(),
                        TestDateTime = DateTime.Now,
                        LimitLevel = value % 2 == 0 ? LimitLevel.Action : value % 2 > 0 ? LimitLevel.Alert : LimitLevel.None,
                        LimitDescription= value.ToString(),
                        Name = "浮游菌测试结果",
                        Value = new Random().NextDouble()
                        //Meassurements = new List<Meassurement>()
                        //{
                        //    new Meassurement()
                        //    {
                        //        Id = i,
                        //        Name = "浮游菌测试结果",
                        //        Value = new Random().NextDouble(),
                        //    },
                        //}
                    });
                }
                location.TestTypeDatas.Add(testTypeData2);
                #endregion
                locationDatas.Add(location);
            }
            var report = new TestReport()
            {
                ComponyName = "极光元",
                CurrentDateTime = DateTime.Now,
                Description = "结果统计报表描述",
                DateDescription ="2023年第5月",
                Name = "结果统计报表",
                UserName = "Emis_Admin",
                LocationsData = locationDatas,
            };
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            return JsonSerializer.Serialize(report, options);
        }
    }
}
