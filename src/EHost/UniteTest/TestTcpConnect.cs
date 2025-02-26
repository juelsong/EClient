namespace UniteTest
{
    using EHost.App.Db;
    using EHost.Infrastructure.Entity.Environment;
    using EService.Service;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Testing.Platform.Logging;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    [TestClass]
    public sealed class TestTcpConnect
    {
        private readonly EnvironmentFactory<MonitorData> environmentFactory;
        private readonly int port;
        private readonly string host;

        public TestTcpConnect()
        {
            // 创建LoggerFactory

            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information); // 设置最小日志级别
                builder.AddDebug();
                builder.AddConsole();
            });
            // 使用LoggerFactory创建ILogger实例
            var _logger = loggerFactory.CreateLogger<EnvironmentFactory<MonitorData>>();
            _logger.LogInformation("Example log message");


            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.UnitTest.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            this.port = configuration.GetSection("TcpServer").GetValue<int>("Port");
            var dbString = configuration.GetSection("ConnectionStrings").GetValue<string>("connectionstr")
                ?? throw new ArgumentNullException("未找到数据库连接字符串");

            var db = new EServerDbContext(dbString);

            this.host = "127.0.0.1";// GetLocalIPAddress();

            environmentFactory = new EnvironmentFactory<MonitorData>(db, configuration);
        }
        [TestInitialize]
        public void TestInit()
        {
             environmentFactory.Start();

        }
        [TestCleanup]
        public void TestClean()
        {
            environmentFactory.Stop();
        }
        [TestMethod]
        public async Task TestTcpClient()
        {
            int numberOfClients = 10;
            var tasks = new Task[numberOfClients];

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
                    byte[] message = GetBytes(clientId);//GetBasicBytes(clientId);

                    // 发送数据
                    await stream.WriteAsync(message, 0, message.Length);

                    // 等待1分钟
                    await Task.Delay(TimeSpan.FromSeconds(1)/*FromMinutes(1).*/);
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
            string data = $"##0461QN=20241124211851306;ST=22;CN=2011;PW=123456;MN=xag10259964655;Flag=5;CP=&&DataTime=20241124211851;a34001-Min=0.000,a34001-Max=0.000,a34001-Avg=0.000,a34001-Flag=F;a01001-Min=0.0,a01001-Max=0.0,a01001-Avg=0.0,a01001-Flag=F;a01002-Min=0.0,a01002-Max=0.0,a01002-Avg=0.0,a01002-Flag=F;a01007-Min=0.0,a01007-Max=0.0,a01007-Avg=0.0,a01007-Flag=F;a01008-Min=0.0,a01008-Max=0.0,a01008-Avg=0.0,a01008-Flag=F;a50001-Min=0.0,a50001-Max=0.0,a50001-Avg=0.0,a50001-Flag=F;&&3D40";
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

            //byte[] data1 = HexStringToByteArray("AC F3 86 06 00 00 00 2A 00 00 00 00 00 00 FC 8F 18 0B 1A 0A 39 2F 00 00 78 61 67 31 30 32 35 39 39 36 34 36 35 35 00 00 0A 08 00 00 00 00 00 20 00 00 95 2A B1");
            //byte[] data1 = HexStringToByteArray("AC F3 86 06 00 00 00 70 00 00 00 00 00 00 FC D1 18 0C 15 11 36 05 00 00 FF FF FF FF FF FF FF FF FF FF FF FF FF FF 00 00 03 08 00 46 00 46 00 46 00 46 04 08 01 F5 01 EA 01 DE 01 DF 05 08 01 3B 00 BB 00 2D 00 87 06 08 00 0C 00 08 00 04 00 04 07 08 00 35 00 35 00 35 00 35 08 08 01 2D 01 2D 01 2D 01 2D 11 08 28 37 28 37 28 37 28 37 0A 08 00 00 00 00 00 0B 00 01 DE 67 B1");
            byte[] data1 = HexStringToByteArray("AC F3 86 06 00 00 00 34 00 00 00 00 00 00 FC 2F 00 01 01 00 03 3B 00 00 31 32 33 34 35 36 37 38 39 30 31 32 33 34 00 00 03 08 00 2F 00 2F 00 2F 00 2F 0A 08 00 00 00 00 FF FF 01 01 0D 73 B1 ");

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
