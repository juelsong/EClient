namespace UniteTest
{
    using EHost.Mqtt;
    using EHost.TcpServer.ParserHelper;
    using EMqttRelay.Service;
    using log4net.Config;
    using Microsoft.Extensions.Configuration;
    using System.Text;

    [TestClass]
    public sealed class TestCrc
    {
        public TestCrc()
        {
            // 初始化log4net配置
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

        }
        [TestInitialize]
        public void TestInit()
        {

        }
        [TestCleanup]
        public void TestClean()
        {
        }
        [TestMethod]
        public void Crc16()
        {
            var str = "QN=20160801085857223;ST=32;CN=1062;PW=100000;MN=010000A8900016F000169DC0;Flag=5;CP=&&RtdInterval=30&&";
            var target = 0x1C80;
            byte[] bytes = Encoding.UTF8.GetBytes(str);

            ushort crc = CrcHelper.CRC16_Checkout(bytes, bytes.Length);

            Assert.AreEqual(crc, target);
        }
    }
}
