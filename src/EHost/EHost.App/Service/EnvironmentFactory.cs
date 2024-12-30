namespace EService.Service
{
    using EHost.App.Db;
    using EHost.Contract;
    using EHost.Contract.Entity;
    using EHost.Infrastructure.Entity;
    using EHost.Infrastructure.Entity.Environment;
    using EHost.TcpServer.Service;
    using log4net;
    using System.Collections.Concurrent;
    using System.Reflection;
    using System.Runtime.Loader;
    using System.Text.Json;
    using System.Threading;

    public class EnvironmentFactory<T> : IDisposable
    {
        private readonly ILog _logger = LogManager.GetLogger(nameof(EnvironmentFactory<T>));
        private readonly TcpMultiServer<T> _tcpMultiServer;
        private readonly BlockingCollection<T> queue;

        private readonly EServerDbContext _dbContext;
        //CancellationTokenSource 当调用 Stop 方法时，它会取消监控任务，从而允许任务在完成当前操作后正常停止，而不是突然中断。
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private Dictionary<string, int> DeviceInsertCount = new Dictionary<string, int>();
        public EnvironmentFactory(EServerDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            queue = [];
            _tcpMultiServer = new TcpMultiServer<T>(configuration, queue);

        }
        public void Start()
        {
            _logger.Info("Starting EnvironmentFactory");
            // Start the TcpMultiServer
            _ = _tcpMultiServer.StartAsync();

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
                            if (sensor is EnvironmentalSensor environmentalSensor)
                            {
                                EnvironmentalSensorMinute environmentalSensorMinute = new EnvironmentalSensorMinute();

                                environmentalSensor.CopyPropertiesTo(environmentalSensorMinute);
                                // 插入传感器数据到数据库
                                _dbContext.EnvironmentalSensorMinutes.Add(environmentalSensorMinute);
                                if (this.DeviceInsertCount.ContainsKey(environmentalSensor.DeviceIdNet))
                                {
                                    // 如果存在，更新值
                                    DeviceInsertCount[environmentalSensor.DeviceIdNet]++;
                                }
                                else
                                {
                                    DeviceInsertCount.Add(environmentalSensor.DeviceIdNet, 1);
                                }
                                DeviceInsertCount.TryGetValue(environmentalSensor.DeviceIdNet, out int count);
                                if ( count % 15  == 0)
                                {
                                    EnvironmentalSensorQuarter quarter = new EnvironmentalSensorQuarter();

                                    environmentalSensor.CopyPropertiesTo(quarter);

                                    _dbContext.EnvironmentalSensorQuarters.Add(quarter);
                                }
                                if (count % 60 == 0)
                                {
                                    EnvironmentalSensorHour hour = new EnvironmentalSensorHour();

                                    environmentalSensor.CopyPropertiesTo(hour);

                                    _dbContext.EnvironmentalSensorHours.Add(hour);
                                }
                                if (count == 60 * 24)
                                {
                                    EnvironmentalSensorDaily daily = new EnvironmentalSensorDaily();

                                    environmentalSensor.CopyPropertiesTo(daily);

                                    _dbContext.EnvironmentalSensorDailies.Add(daily);
                                    DeviceInsertCount[environmentalSensor.DeviceIdNet] = 0;
                                }
                                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                                Console.WriteLine($"Insert Db {JsonSerializer.Serialize(environmentalSensor)}");

                            }
                            else
                            {
                                // 如果sensor不是FugitiveDust类型，可以在这里处理错误或记录日志
                                Console.WriteLine("The sensor object is not of type EnvironmentalSensor.");
                            }
                        }
                        else
                        {
                            _logger.Info("The sensor queue is empty");
                            await Task.Delay(1000 * 60); // 使用Task.Delay代替Thread.Sleep
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
            _dbContext.Dispose();
            _logger.Info("EnvironmentFactory stopped");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}


