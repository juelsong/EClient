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
                                    //var mqttClient = new MqttClientManager(this.mqttServer, this.mqttPort, this.topic, this.username, this.password, fugitiveDust.EquipmentCode);
                                    currentMN = fugitiveDust.EquipmentCode;
                                    var mqttClient = _mqttClientManagers.Where(m => m.ClientId == currentMN).FirstOrDefault();
                                    if (mqttClient == null)
                                    {
                                        mqttClient = new MqttClientManager(this.mqttServer, this.mqttPort, this.topic, this.username, this.password, currentMN);
                                        _mqttClientManagers.Add(mqttClient);

                                    }
                                    //TODO 数据加密
                                    await mqttClient.PublishMessage(this.GetEncryptionData(data)).ConfigureAwait(false);
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
        private string GetEncryptionData(string data)
        {
            return data;
        }
    }

}

