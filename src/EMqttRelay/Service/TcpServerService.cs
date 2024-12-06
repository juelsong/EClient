namespace EMqttRelay.Service
{
    using EHost.Mqtt;
    using EHost.TcpServer.ParserHelper;
    using EHost.TcpServer.Service;
    using log4net;
    using Microsoft.Extensions.Configuration;
    using MQTTnet.Exceptions;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 可允许多连接
    /// </summary>
    public class TcpMultiServer
    {
        private readonly int port;
        private readonly ILog logger = LogManager.GetLogger(nameof(TcpMultiServer));
        private readonly List<MqttClientManager> _mqttClientManagers = new List<MqttClientManager>();
        private TcpListener tcpListener;
        private bool isRunning;

        private readonly string mqttServer;
        private readonly int mqttPort;
        private readonly string topic;
        private readonly string username;
        private readonly string password;
        private readonly string clientId;


        public TcpMultiServer(IConfiguration configuration)
        {
            this.port = configuration.GetValue<int>("TcpServer:Port");

            // 从配置文件中读取设置
            this.mqttServer = configuration["Mqtt:Server"] ?? "localhost";
            this.mqttPort = configuration.GetValue<int>("Mqtt:Port");
            this.topic = configuration["Mqtt:Topic"] ?? "defaultTopic";
            this.username = configuration["Mqtt:Username"] ?? "defaultUsername";
            this.password = configuration["Mqtt:Password"] ?? "defaultPassword";
            this.clientId = configuration["Mqtt:ClientId"] ?? "defaultClientId";
        }

        public async Task StartAsync()
        {
            logger.Info($"Starting TCP server on port {port}");
            tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();
            isRunning = true;

            while (isRunning)
            {
                try
                {
                    TcpClient client = await tcpListener.AcceptTcpClientAsync();
                    _ = HandleClientAsync(client); // Fire and forget
                }
                catch (Exception ex) when (isRunning)
                {
                    logger.Error("Error accepting TCP client connection", ex);
                }
            }
        }

        public void Stop()
        {
            isRunning = false;
            tcpListener?.Stop();
            logger.Info("TCP server stopped");
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                var currentClient = (client.Client.RemoteEndPoint as IPEndPoint);
                var currentMN = "";
                if (null != currentClient)
                {
                    logger.Info("Accepted new TCP client connection");
                    using NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    //List<byte> receivedData = new List<byte>();

                    while (isRunning)
                    {
                        try
                        {
                            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                            if (bytesRead == 0)
                            {
                                var mqttClientRemove = _mqttClientManagers.Where(m => m.ClientId == currentMN).FirstOrDefault();
                                if (mqttClientRemove != null)
                                {
                                    mqttClientRemove.Dispose();
                                    _mqttClientManagers.Remove(mqttClientRemove);
                                }
                                logger.Info("Closed TCP client connection");

                                break; // 客户端关闭了连接
                            }

                            // 将接收到的数据添加到列表中
                            // 将接收到的数据添加到列表中
                            //receivedData.AddRange(buffer.Take(bytesRead));

                            // 检查是否收到了完整的消息
                            //while (ContainsMessageWithStartAndEndBytes(buffer.ToArray(), "##"))
                            {
                                // 处理完整消息
                                string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                                logger.Info($"Received data from client {currentClient.Address.ToString()}:{currentClient.Port.ToString()} Data is [{data}]");

                                if (Protocol.ProtocolExtract(data, out var dataStructure))
                                {
                                    var fugitiveDust = dataStructure.DataSegment.FugitiveDustParse();
                                    //var jsonData = dataStructure.DataSegment.JsonParse();
                                    //var mqttClient = new MqttClientManager(this.mqttServer, this.mqttPort, this.topic, this.username, this.password, fugitiveDust.EquipmentCode);
                                    currentMN = fugitiveDust.EquipmentCode;
                                    var mqttClient = _mqttClientManagers.Where(m => m.ClientId == currentMN).FirstOrDefault();
                                    if (mqttClient == null)
                                    {
                                        mqttClient = new MqttClientManager(this.mqttServer, this.mqttPort, this.topic, this.username, this.password, currentMN);
                                        _mqttClientManagers.Add(mqttClient);

                                    }
                                    //TODO 数据加密
                                    await mqttClient.PublishMessage(this.GetEncryptionData(dataStructure.DataSegment, fugitiveDust.Date)).ConfigureAwait(false);
                                }
                            }

                        }

                        catch (MqttProtocolViolationException ex)
                        {
                            logger.Error("Error handling TCP client connection", ex);
                            break;
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Error handling TCP client connection", ex);
                            break;
                        }
                    }
                }
            }

        }
        /// <summary>
        /// 判断包含帧头和尾
        /// 固定为0xAC 0xB1
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool ContainsMessageWithStartAndEndBytes(byte[] data, string start = "", string end = "")
        {
            // 检查数据是否至少包含开始和结束字节
            if (data.Length < (start.Length + end.Length))
            {
                return false;
            }
            string dataString = Encoding.UTF8.GetString(data, 0, data.Length);
            if (dataString.Contains(start) && dataString.Contains(end))
            {
                return true;
            }
            return false;
            //// 检查数组是否包含第一个字节
            //if (Array.IndexOf(data, (byte)0xAC) == -1)
            //{
            //    return false; // 如果不包含第一个字节，返回 false
            //}

            //// 检查数组是否包含第二个字节
            //if (Array.IndexOf(data, (byte)0xB1) == -1)
            //{
            //    return false; // 如果不包含第二个字节，返回 false
            //}
            //return true; // 如果两个字节都存在，返回 true
        }
        private string GetEncryptionData(string data,DateTime date)
        {
            date = DateTime.Now;
#if DEBUG
            data = "ST=22;CN=2051;PW=123456;MN=HJZB0XH0150109;CP=&&DataTime=20241205100227;a34001-Avg=0.160,a34001-Max=0.160,a34001-Min=0.160,a34001-Flag=N;a01007-Avg=0.0,a01007-Max=0.0,a01007-Min=0.0,a01007-Flag=N;a01008-Avg=0,a01008-Max=0,a01008-Min=0,a01008-Flag=N;a01001-Avg=15.1,a01001-Max=15.2,a01001-Min=15.1,a01001-Flag=N;a01002-Avg=54.4,a01002-Max=54.5,a01002-Min=54.4,a01002-Flag=N;a01006-Avg=0.00,a01006-Max=0.00,a01006-Min=0.00,a01006-Flag=F;a50001-Avg=41.7,a50001-Max=43.1,a50001-Min=40.9,a50001-Flag=N;cpm-Avg=57,cpm-Max=57,cpm-Min=57,cpm-Flag=N;&&";
            string currentTime = date.ToString("yyyyMMddHHmmss");
            string pattern = @"DataTime=[0-9]{14}";
            string replacement = "DataTime=" + currentTime;
            string modifiedString = Regex.Replace(data, pattern, replacement);
#else
            StringBuilder sb = new StringBuilder(data);
            int lastAndIndex = data.LastIndexOf("&&");

            if (lastAndIndex != -1)
            {
                sb.Insert(lastAndIndex, ";");
            }
            string modifiedString = sb.ToString();
#endif
            modifiedString = $"QN={date.ToString("yyyyMMddHHmmssfff")};" + modifiedString;
            //data = $"QN=20241205090532000;" + data;

            byte[] bytes = Encoding.UTF8.GetBytes(modifiedString);

            var crc = CrcHelper.CRC16_Checkout(bytes, bytes.Length).ToString("X4");

            modifiedString = $"##{modifiedString.Length.ToString("0000")}{modifiedString}{crc}\r\n";
            return modifiedString;
        }
    }

}

