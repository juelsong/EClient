namespace UniteTest
{
    using EHost.App.Db;
    using EHost.Contract.Entity;
    using EHost.Mqtt;
    using EHost.TcpServer.ParserHelper;
    using EHost.TcpServer.Service;
    using EMqttRelay.Service;
    using EService.Service;
    using log4net.Config;
    using Microsoft.AspNetCore.Hosting.Server;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Testing.Platform.Logging;
    using System.Data;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    [TestClass]
    public sealed class TestMqtt
    {
        private readonly int port;
        private readonly string host;
        private readonly TcpMultiServer server;
        private readonly MqttServerBase mqttServer;
        private readonly int mqttServerPort = 18883;
        private readonly MqttClientManager mqttClient;


        private MqttClientManagerOther mqttClient2;
        IConfigurationRoot configuration;
        public TestMqtt()
        {
            // 初始化log4net配置
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            // 创建LoggerFactory
            this.port = 40003;
            this.host = GetLocalIPAddress();

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();

            server = new TcpMultiServer(configuration);
            mqttServer = new MqttServerBase(mqttServerPort);

            mqttClient = new MqttClientManager(
                configuration["Mqtt:Server"] ?? "localhost",
                configuration.GetValue<int>("Mqtt:Port"),
                configuration["Mqtt:Topic"] ?? "defaultTopic",
                configuration["Mqtt:Username"] ?? "defaultUsername",
                configuration["Mqtt:Password"] ?? "defaultPassword",
                "HJZB0XH0150109"
                );

        }
        [TestInitialize]
        public void TestInit()
        {

        }
        [TestCleanup]
        public void TestClean()
        {
            server.Stop();
        }
        [TestMethod]
        public async Task TestTcpClient()
        {
            _= server.StartAsync();

            int numberOfClients = 1;
            var tasks = new Task[numberOfClients];

            //await mqttClient2.Connect().ConfigureAwait(false);
            for (int i = 0; i < numberOfClients; i++)
            {
                int clientId = i;
                tasks[i] = Task.Run(async () =>
                    await ConnectAndSendData(clientId, this.host, this.port)
                );
            }

            await Task.WhenAll(tasks);
            Assert.AreEqual(numberOfClients, tasks.Length);

        }
        [TestMethod]
        public async Task TestMqttClient()
        {
            //_ = mqttServer.StartAsync();
            await mqttClient.Connect().ConfigureAwait(false);
            //await mqttClient2.Connect().ConfigureAwait(false);
            mqttClient2 = new MqttClientManagerOther(
                configuration["Mqtt:Server"] ?? "localhost",
                configuration.GetValue<int>("Mqtt:Port"),
                configuration["Mqtt:Topic"] ?? "defaultTopic",
                configuration["Mqtt:Username"] ?? "defaultUsername",
                configuration["Mqtt:Password"] ?? "defaultPassword",
                "HJZB0XH0150109"
                );
            while (true)
            {
                var str = "##0563QN=20241205100227000;ST=22;CN=2051;PW=123456;MN=HJZB0XH0150109;CP=&&DataTime=20241205100227;a34001-Avg=0.160,a34001-Max=0.160,a34001-Min=0.160,a34001-Flag=N;a01007-Avg=0.0,a01007-Max=0.0,a01007-Min=0.0,a01007-Flag=N;a01008-Avg=0,a01008-Max=0,a01008-Min=0,a01008-Flag=N;a01001-Avg=15.1,a01001-Max=15.2,a01001-Min=15.1,a01001-Flag=N;a01002-Avg=54.4,a01002-Max=54.5,a01002-Min=54.4,a01002-Flag=N;a01006-Avg=0.00,a01006-Max=0.00,a01006-Min=0.00,a01006-Flag=F;a50001-Avg=41.7,a50001-Max=43.1,a50001-Min=40.9,a50001-Flag=N;cpm-Avg=57,cpm-Max=57,cpm-Min=57,cpm-Flag=N;&&8701";

                //Protocol.ProtocolExtract(str ,out var dataStructure);
                mqttClient2.PublishMessage(str);
                //await mqttClient.PublishMessage(str);

                ////await mqttClient.PublishMessage("##0252ST=22;CN=2011;PW=987243;MN=HJZB0XH0150109;CP=&&DataTime=20241202165000;a34001-Avg=0.080,a34001-Flag=N;a50001-Avg=50.4,a50001-Flag=N;a01001-Avg=120.0,a01001-Flag=N;a01002-Avg=99.9,a01002-Flag=N;a01007-Avg=0.0,a01007-Flag=N;a01008-Avg=0.0,a01008-Flag=N&&DBC1").ConfigureAwait(false);

                await Task.Delay(TimeSpan.FromSeconds(10));

            }
        }
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        private static async Task ConnectAndSendData(int clientId, string remoteAddress, int remotePort)
        {
            TcpClient client = new TcpClient();
            try
            {
                // 假设服务器IP地址为127.0.0.1，端口为13000
                await client.ConnectAsync(remoteAddress, remotePort);

                Debug.WriteLine($"Client {clientId} connected.");

                NetworkStream stream = client.GetStream();
                while (true)
                {
                    // 构造要发送的数据
                    byte[] message = GetBasicBytes(clientId);


                    // 发送数据
                   await stream.WriteAsync(message, 0, message.Length);

                    // 等待1分钟
                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Client {clientId} error: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }
        private static byte[] GetBasicBytes(int clientId)
        {
            var data = "##0563QN=20241205100227000;ST=22;CN=2051;PW=123456;MN=HJZB0XH0150109;CP=&&DataTime=20241205100227;a34001-Avg=0.160,a34001-Max=0.160,a34001-Min=0.160,a34001-Flag=N;a01007-Avg=0.0,a01007-Max=0.0,a01007-Min=0.0,a01007-Flag=N;a01008-Avg=0,a01008-Max=0,a01008-Min=0,a01008-Flag=N;a01001-Avg=15.1,a01001-Max=15.2,a01001-Min=15.1,a01001-Flag=N;a01002-Avg=54.4,a01002-Max=54.5,a01002-Min=54.4,a01002-Flag=N;a01006-Avg=0.00,a01006-Max=0.00,a01006-Min=0.00,a01006-Flag=F;a50001-Avg=41.7,a50001-Max=43.1,a50001-Min=40.9,a50001-Flag=N;cpm-Avg=57,cpm-Max=57,cpm-Min=57,cpm-Flag=N;&&8701";
            Debug.WriteLine($"Client {clientId} send data: {data}");
            return Encoding.ASCII.GetBytes(data);

        }
        private static byte[] GetBytes(int clientId)
        {
            //            byte[] data = new byte[] {
            //                0xAC, 0xF3, 0x86, 0x06, 0x00, 0x00, 0x00, 0x2A, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFC, 0x8F,
            //                0x18, 0x0B, 0x1A, 0x0A, 0x39, 0x2F, 0x00, 0x00, 0x78, 0x61, 0x67, 0x31, 0x30, 0x32, 0x35, 0x39,
            //                0x39, 0x36, 0x34, 0x36, 0x35, 0x35, 0x00, 0x00, 0x0A, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20,
            //                0x00, 0x00, 0x95, 0x2A, 0xB1
            //};
            //            Debug.WriteLine("AC F3 86 06 00 00 00 2A 00 00 00 00 00 00 FC 8F 18 0B 1A 0A 39 2F 00 00 78 61 67 31 30 32 35 39 39 36 34 36 35 35 00 00 0A 08 00 00 00 00 00 20 00 00 95 2A B1 ");

            byte[] data1 = HexStringToByteArray("AC F3 86 06 00 00 00 2A 00 00 00 00 00 00 FC 8F 18 0B 1A 0A 39 2F 00 00 78 61 67 31 30 32 35 39 39 36 34 36 35 35 00 00 0A 08 00 00 00 00 00 20 00 00 95 2A B1");
            Debug.WriteLine(string.Join(", ", data1.Select(b => "0x" + b.ToString("X2"))));

            return data1;
        }

        public static byte[] HexStringToByteArray(string hexString)
        {
            // 移除空格
            hexString = hexString.Replace(" ", "");

            // 检查字符串长度是否为偶数
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException("The hexadecimal string must have an even number of characters.");
            }

            // 将十六进制字符串转换为字节数组
            byte[] byteArray = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
            {
                byteArray[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }

            return byteArray;
        }
    }


}
