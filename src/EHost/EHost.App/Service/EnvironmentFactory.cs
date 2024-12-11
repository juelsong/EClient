namespace EService.Service
{
    using EHost.App.Db;
    using EHost.Contract.Entity;
    using EHost.Infrastructure.Entity.Environment;
    using EHost.TcpServer.Service;
    using log4net;
    using System.Collections.Concurrent;
    using System.Threading;

    public class EnvironmentFactory<T> : IDisposable
    {
        private readonly ILog _logger = LogManager.GetLogger(nameof(EnvironmentFactory<T>));
        private readonly TcpMultiServer<T> _tcpMultiServer;
        private readonly BlockingCollection<T> queue;

        private readonly EServerDbContext _dbContext;
        //CancellationTokenSource 当调用 Stop 方法时，它会取消监控任务，从而允许任务在完成当前操作后正常停止，而不是突然中断。
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public EnvironmentFactory(EServerDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            queue = [];

            _tcpMultiServer = new TcpMultiServer<T>(configuration, queue);
        }
        public async Task StartAsync()
        {
            _logger.Info("Starting EnvironmentFactory");
            // Start the TcpMultiServer
            await _tcpMultiServer.StartAsync();

            // Start a task to monitor the sensor queue and insert data into the database
            var monitorQueueTask = Task.Run(async () =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        // Try to dequeue an item from the sensor queue
                        if (queue.TryTake(out var sensor))
                        {
                            // Insert the sensor data into the database

                            // 确保sensor不是null并且是FugitiveDust类型
                            if (sensor is FugitiveDust fugitiveDust)
                            {
                                // 插入传感器数据到数据库
                                _dbContext.FugitiveDust.Add(fugitiveDust);
                                await _dbContext.SaveChangesAsync();
                            }
                            else
                            {
                                // 如果sensor不是FugitiveDust类型，可以在这里处理错误或记录日志
                                Console.WriteLine("The sensor object is not of type FugitiveDust.");
                            }
                        }
                        else
                        {
                            _logger.Info("The sensor queue is empty");
                            Thread.Sleep(1000 * 60);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error inserting sensor data into the database", ex);
                    }
                }
            }, _cancellationTokenSource.Token);
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _tcpMultiServer.Stop();
            _logger.Info("EnvironmentFactory stopped");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}


