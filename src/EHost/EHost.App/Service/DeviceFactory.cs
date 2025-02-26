namespace EService.Service
{
    using EHost.App.Db;
    using EHost.App.Models;
    using EHost.App.Service;
    using EHost.Contract;
    using EHost.Infrastructure.Entity;
    using EHost.Infrastructure.Entity.Environment;
    using EHost.TcpServer.Service;
    using log4net;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Concurrent;
    using System.Text.Json;
    using System.Threading;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

    /// <summary>
    /// 用于下发设备指令
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeviceFactory<T> : IDeviceService, IDisposable
    {
        private readonly ILog _logger = LogManager.GetLogger(nameof(DeviceFactory<T>));
        /// <summary>
        /// 多设备服务
        /// </summary>
        private readonly TcpMuiltiServerPlus<T> _tcpMultiServerPlus;
        /// <summary>
        /// 指令服务
        /// </summary>
        private readonly CommandTcpServer _commandTcpServer;

        /// <summary>
        /// 指令队列
        /// </summary>
        private readonly BlockingCollection<(int DeviceId, string Command)> _commandQueue;
        private readonly BlockingCollection<T> _queue;

        //CancellationTokenSource 当调用 Stop 方法时，它会取消监控任务，从而允许任务在完成当前操作后正常停止，而不是突然中断。
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly CancellationTokenSource _commandCancellationTokenSource = new CancellationTokenSource();

        private Dictionary<int, List<MonitorDataMinute>> deviceDatas = new();
        public DeviceFactory(IConfiguration configuration)
        {
            _queue = [];

            _commandQueue = [];
            _tcpMultiServerPlus = new TcpMuiltiServerPlus<T>(configuration.GetValue<int>("TcpServer:InternalPort"), _queue);
            _commandTcpServer = new CommandTcpServer(configuration.GetValue<int>("TcpServer:CommandPort"), EnqueueCommand);
            // Start the JSON TCP server with a message processing function
        }
        public void Start()
        {

            _logger.Info("Starting EnvironmentFactory");
            // Start the TcpMultiServer
            _ = _tcpMultiServerPlus.StartAsync();
            _ = _commandTcpServer.StartAsync();

            // Start a task to monitor the sensor queue and insert data into the database
            var commandQueueTask = Task.Run(async () =>
            {
                while (!_commandCancellationTokenSource.Token.IsCancellationRequested)
                {
                    if (_commandQueue.TryTake(out var c))
                    {
                        if (c.Command.Contains("query"))
                        {
                            var request = JsonSerializer.Deserialize<DeviceQueryRequest>(c.Command);
                            var response = await QueryDevices(request);
                            // 将结果发送回 JsonTcpServer
                            _commandTcpServer.SendResponseToClient(c.DeviceId, JsonSerializer.Serialize(response));
                        }
                        else if (c.Command.Contains("data_send")) //command
                        {
                            var request = JsonSerializer.Deserialize<DeviceCommandRequest>(c.Command);
                            var response = await SendCommand(request);
                            // 将结果发送回 JsonTcpServer
                            _commandTcpServer.SendResponseToClient(c.DeviceId, JsonSerializer.Serialize(response));
                        }

                    }
                    //foreach (var (deviceId, command) in _commandQueue.GetConsumingEnumerable())
                    //{
                    //}
                }
            }, _commandCancellationTokenSource.Token);


        }
        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _tcpMultiServerPlus.Stop();
            _commandTcpServer.Stop();

            _logger.Info("EnvironmentFactory stopped");
        }

        public void EnqueueCommand(int deviceId, string command)
        {
            _commandQueue.Add((deviceId, command));
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<DeviceQueryResponse> QueryDevices(DeviceQueryRequest request)
        {
            // 发送指令到 _tcpMultiServerPlus
            //_logger.Info($"Processed command: {command}, Result: {result}");

            return await _tcpMultiServerPlus.QueryDeviceAsync(request);

        }

        public async Task<DeviceCommandResponse> SendCommand(DeviceCommandRequest request)
        {
            // 发送指令到 _tcpMultiServerPlus
            //_logger.Info($"Processed command: {command}, Result: {result}");

            return await _tcpMultiServerPlus.SendCommandAsync(request);

        }
    }
}


