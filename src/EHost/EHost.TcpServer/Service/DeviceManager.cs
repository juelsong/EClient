using EHost.Infrastructure.Entity.Environment;
using EHost.TcpServer.ParserHelper;
using log4net;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EHost.TcpServer.Service
{
    public class DeviceManager<T>
    {
        public class Command
        {
            public int Id { get; set; }
            /// <summary>
            /// 指令符
            /// </summary>
            public byte CommandSymbol { get; set; }

            /// <summary>
            /// 操作码
            /// </summary>
            public byte CommandControl { get; set; }
        }
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;
        private readonly ILog _logger = LogManager.GetLogger(nameof(DeviceManager<T>));
        private readonly BlockingCollection<T>? _queue;
        private const int MaxBufferSize = 1024 * 1024; // 1 MB

        private readonly ConcurrentDictionary<Command, TaskCompletionSource<string>> _responseTasks;
        private int _nextRequestId;
        public string ClientId { get; set; }

        public DeviceManager(TcpClient client, /*ILog logger,*/ BlockingCollection<T>? queue)
        {
            _client = client;
            _stream = client.GetStream();
            //_logger = logger;
            _queue = queue;
            _responseTasks = new ConcurrentDictionary<Command, TaskCompletionSource<string>>();
            _nextRequestId = 1;
        }


        public async Task<string> SendDataAsync(string data, string dataType)
        {
            int requestId = _nextRequestId++;
            string requestData = $"request:{requestId}:{data}";
            byte[] dataBytes = GetDataBytes(data, dataType); // 直接使用传入的 data，而不是 requestData
            var command = new Command()
            {
                Id = requestId,
                CommandSymbol = dataBytes[2],
                CommandControl = dataBytes[3],
            };

            await _stream.WriteAsync(dataBytes, 0, dataBytes.Length);

            var tcs = new TaskCompletionSource<string>();
            _responseTasks[command] = tcs;
            // 设置超时机制，避免无限等待
            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10)); // 10秒超时
            var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);

            if (completedTask == timeoutTask)
            {
                _responseTasks.TryRemove(command, out _);
                //throw new TimeoutException("等待设备响应超时。");
                _logger.Error($"SendDataAsync Command timeout {data}");
                return "Timeout";
            }
            return await tcs.Task; // 等待设备返回数据
        }

        private byte[] GetDataBytes(string data, string dataType)
        {
            switch (dataType.ToLower())
            {
                case "hex":
                    return ConvertHexStringToBytes(data);
                case "ascii":
                    return Encoding.ASCII.GetBytes(data);
                case "utf8":
                    return Encoding.UTF8.GetBytes(data);
                default:
                    throw new NotSupportedException($"Data type '{dataType}' is not supported.");
            }
        }

        private byte[] ConvertHexStringToBytes(string hex)
        {
            // 移除空格和其他非十六进制字符
            hex = hex.Replace(" ", "").Replace("-", "").Replace(":", "");

            if (hex.Length % 2 != 0)
            {
                throw new ArgumentException("Hex string must have an even length.");
            }

            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
        private static string ToHex(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                hex.AppendFormat("{0:X2}", b);
            }
            return hex.ToString();
        }
        public async Task HandleClientAsync()
        {
            byte[] buffer = new byte[1024];
            List<byte> receivedData = new List<byte>();

            while (true)
            {
                try
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {

                        break; // 客户端关闭了连接
                    }

                    // 将新数据添加到接收缓冲区
                    receivedData.AddRange(buffer.Take(bytesRead));

                    // 检查缓冲区是否超过最大大小
                    if (receivedData.Count > MaxBufferSize)
                    {
                        _logger.Warn($"接收缓冲区超过最大大小 ({MaxBufferSize} bytes)，清理无效数据。");
                        receivedData.Clear(); // 清理缓冲区
                        continue;
                    }


                    string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    _logger.Info($"Received full message: {data}");
                    //查找 Id

                    // 长时间获取的数据 
                    while (ContainsMessageWithStartAndEndBytes(receivedData.ToArray()))
                    {
                        var fullMessage = GetSingleFullData(receivedData);
                        ProcessFullMessage(fullMessage.FullData, bytesRead);
                        receivedData.RemoveRange(0, fullMessage.EndIndex + 1);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("Error handling TCP client connection", ex);
                    break;
                }
            }
        }

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
        private void ProcessFullMessage(byte[] fullMessage, int bytesRead)
        {
            // 这里处理完整的消息
            // 例如，您可以解析消息并更新队列或其他数据结构
            var currentClient = _client.Client.RemoteEndPoint as IPEndPoint;

            // Echo the received data back to the client
            switch (typeof(T).Name)
            {
                case nameof(EnvironmentalSensor):
                    _logger.Info($"Received full message: {BitConverter.ToString(fullMessage)}");
                    if (Protocol.ProtocolExtract(fullMessage, out var dataStructure))
                    {
                        switch (dataStructure.CommandType)
                        {
                            case 0xF2:
                                //TODO判断数据是否是 SendDataAsync 的返回值
                                var key = _responseTasks.Keys.FirstOrDefault(i => i.CommandSymbol == dataStructure.CommandSymbol && dataStructure.CommandControl == (byte)(i.CommandControl | 0x80));
                                if (key != null && _responseTasks.ContainsKey(key))
                                {
                                    // 找到对应的 TaskCompletionSource 并设置结果
                                    _responseTasks[key].SetResult("success");
                                    _responseTasks.TryRemove(key, out _);
                                }
                                else
                                {
                                    // 如果没有找到对应的键，可以记录日志或者进行其他错误处理
                                    _logger.Warn("No matching response task found for the received data.");
                                }

                                break;
                            case 0xF3:
                                if (dataStructure.CommandSymbol == 0x86 && dataStructure.CommandControl == 0x06)
                                {
                                    //(_queue as BlockingCollection<EnvironmentalSensor>)?.Add(dataStructure.PayloadData.EnvironmentalSensorParse());
                                    ClientId = dataStructure.PayloadData.EnvironmentalSensorParse().DeviceIdNode;
                                }
                                break;
                            default:
                                _logger.Info($"Type is {dataStructure.CommandType}, Need Develop");
                                break;
                        }
                    }

                    break;
                case nameof(FugitiveDust):
                    string data = Encoding.UTF8.GetString(fullMessage, 0, bytesRead);
                    if (Protocol.ProtocolExtract(data, out var fugitiveDust))
                    {
                        (_queue as BlockingCollection<FugitiveDust>)?.Add(fugitiveDust.DataSegment.FugitiveDustParse());
                    }

                    break;
                case nameof(MonitorData):
                    _logger.Info($"Received data from client {currentClient?.Address.ToString()}:{currentClient?.Port.ToString()} Data is [{Encoding.UTF8.GetString(fullMessage, 0, bytesRead)}]");

                    string monitorDataStr = Encoding.UTF8.GetString(fullMessage, 0, bytesRead);
                    if (Protocol.ProtocolExtract(monitorDataStr, out var monitorData))
                    {
                        (_queue as BlockingCollection<MonitorData>)?.Add(monitorData.DataSegment.MonitorParse());
                    }

                    break;
                default:
                    break;
            }

            //string data = Encoding.UTF8.GetString(fullMessage, 0, bytesRead);
            //_logger.LogInformation($"Received data from client {clientAddress}:{clientPort} Data is [{data}]");
            //if (Protocol.ProtocolExtract(data, out var dataStructure))
            //{
            //    _fugitiveDustQueue?.Add(dataStructure.DataSegment.FugitiveDustParse());
            //}

        }
    }
}
