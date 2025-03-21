﻿
namespace EHost.TcpServer.Service
{
    using EHost.Infrastructure.Entity;
    using EHost.Infrastructure.Entity.Environment;
    using EHost.TcpServer.ParserHelper;
    using log4net;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Concurrent;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    /// <summary>
    /// 单链接
    /// </summary>
    public class TcpSingleServer
    {
        private readonly int _port;
        private readonly TcpListener tcpListener;
        private readonly Thread listenThread;
        private readonly ILog logger = LogManager.GetLogger(nameof(TcpSingleServer));

        public TcpSingleServer(int _port)
        {
            this._port = _port;
            tcpListener = new TcpListener(IPAddress.Any, _port);

            listenThread = new Thread(new ThreadStart(Listen));
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        public void Start()
        {
            // Start the listening thread
            Thread listenThread = new Thread(Listen);
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        private void Listen()
        {
            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                HandleClient(client);
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            while (true)
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    break;
                }
                string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                logger.Info($"Received data from client: {data}");

                // Echo the received data back to the client
                byte[] echoData = Encoding.UTF8.GetBytes(data);
                stream.Write(echoData, 0, echoData.Length);
            }
            client.Close();
        }
    }

    /// <summary>
    /// 可允许多连接
    /// </summary>
    public class TcpMultiServer<T>
    {
        protected readonly int _port;
        protected readonly ILog logger = LogManager.GetLogger(nameof(TcpMultiServer<T>));
        protected readonly BlockingCollection<T>? _queue;
        protected readonly ConcurrentDictionary<string, TcpClient> connectedDevices = new ConcurrentDictionary<string, TcpClient>();

        protected TcpListener tcpListener;
        protected bool isRunning;


        public TcpMultiServer(int port, BlockingCollection<T>? queue = null)
        {
            _port = port;
            _queue = queue;
        }

        public async virtual Task StartAsync()
        {
            logger.Info($"Starting GetEnvironmentPort TCP server on _port {_port}");
            tcpListener = new TcpListener(IPAddress.Any, _port);
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
                var currentClient = client.Client.RemoteEndPoint as IPEndPoint;
                if (null != currentClient)
                {
                    logger.Info("Accepted new TCP client connection");
                    using NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    List<byte> receivedData = new List<byte>();

                    while (isRunning)
                    {
                        try
                        {
                            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                            if (bytesRead == 0)
                            {
                                break; // 客户端关闭了连接
                            }

                            // 将接收到的数据添加到列表中
                            // 将接收到的数据添加到列表中
                            receivedData.AddRange(buffer.Take(bytesRead));

                            while (ContainsMessageWithStartAndEndBytes(receivedData.ToArray()))
                            {
                                var fullMessage = GetSingleFullData(receivedData);
                                // 处理完整消息
                                ProcessFullMessage(fullMessage.FullData, bytesRead, currentClient.Address.ToString(), currentClient.Port.ToString());

                                // 移除已处理的消息
                                receivedData.RemoveRange(0, fullMessage.EndIndex + 1);
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Error handling TCP client connection", ex);
                            break;
                        }
                    }
                }
                logger.Info("Closed TCP client connection");
            }

        }
        ///
        private (byte[] FullData, int EndIndex) GetSingleFullData(List<byte> receivedData)
        {
            if (typeof(T).Name is nameof(EnvironmentalSensor))
            {
                // 检查是否收到了完整的消息（以 0xAC 开始，以 0xB1 结束）
                // 提取完整消息
                int startIndex = receivedData.IndexOf(0xAC);
                int endIndex = receivedData.IndexOf(0xB1);
                return (receivedData.GetRange(startIndex, endIndex - startIndex + 1).ToArray(), endIndex);
            }
            return (receivedData.ToArray(), receivedData.Count - 1);
        }
        /// <summary>
        /// 判断包含帧头和尾
        /// 对外接口 固定为0xAC 0xB1
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool ContainsMessageWithStartAndEndBytes(byte[] data)
        {
            // 检查是否收到了完整的消息（以 0xAC 开始，以 0xB1 结束）

            // 检查数据是否至少包含开始和结束字节
            if (data.Length < 2)
            {
                return false;
            }
            if (typeof(T).Name is nameof(EnvironmentalSensor))
            {
                // 检查数组是否包含第一个字节
                if (Array.IndexOf(data, (byte)0xAC) == -1)
                {
                    return false; // 如果不包含第一个字节，返回 false
                }

                // 检查数组是否包含第二个字节
                if (Array.IndexOf(data, (byte)0xB1) == -1)
                {
                    return false; // 如果不包含第二个字节，返回 false
                }
            }
            return true; // 如果两个字节都存在，返回 true
        }
        private void ProcessFullMessage(byte[] fullMessage, int bytesRead, string clientAddress, string client_port)
        {
            // 这里处理完整的消息
            // 例如，您可以解析消息并更新队列或其他数据结构
            logger.Info($"Received full message: {BitConverter.ToString(fullMessage)}");


            // Echo the received data back to the client
            switch (typeof(T).Name)
            {
                case nameof(EnvironmentalSensor):
                    if (Protocol.ProtocolExtract(fullMessage, out var dataStructure))
                    {
                        switch (dataStructure.CommandType)
                        {
                            case 0xF3:
                                if (dataStructure.CommandSymbol == 0x86 && dataStructure.CommandControl == 0x06)
                                {
                                    (_queue as BlockingCollection<EnvironmentalSensor>)?.Add(dataStructure.PayloadData.EnvironmentalSensorParse());
                                }
                                break;
                            default:
                                logger.Info($"Type is {dataStructure.CommandType}, Need Develop");
                                break;
                        }
                    }

                    break;
                case nameof(FugitiveDust):
                    string data = Encoding.UTF8.GetString(fullMessage, 0, bytesRead);
                    logger.Info($"Received data from client {clientAddress}:{client_port} Data is [{data}]");
                    if (Protocol.ProtocolExtract(data, out var fugitiveDust))
                    {
                        (_queue as BlockingCollection<FugitiveDust>)?.Add(fugitiveDust.DataSegment.FugitiveDustParse());
                    }

                    break;
                case nameof(MonitorData):
                    string monitorDataStr = Encoding.UTF8.GetString(fullMessage, 0, bytesRead);
                    logger.Info($"Received data from client {clientAddress}:{client_port} Data is [{monitorDataStr}]");
                    if (Protocol.ProtocolExtract(monitorDataStr, out var monitorData))
                    {
                        (_queue as BlockingCollection<MonitorData>)?.Add(monitorData.DataSegment.MonitorParse());
                    }

                    break;
                default:
                    break;
            }

            //string data = Encoding.UTF8.GetString(fullMessage, 0, bytesRead);
            //logger.LogInformation($"Received data from client {clientAddress}:{client_port} Data is [{data}]");
            //if (Protocol.ProtocolExtract(data, out var dataStructure))
            //{
            //    _fugitiveDustQueue?.Add(dataStructure.DataSegment.FugitiveDustParse());
            //}

        }
    }

}

