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

    /// <summary>
    /// 接受对外信息 并存储DB
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnvironmentFactory<T> :  IDisposable
    {
        private readonly ILog _logger = LogManager.GetLogger(nameof(EnvironmentFactory<T>));
        /// <summary>
        /// 多设备服务
        /// </summary>
        private readonly TcpMultiServer<T> _tcpMultiServer;

        /// <summary>
        /// 数据库队列
        /// </summary>
        private readonly BlockingCollection<T> _queue;

        private readonly EServerDbContext _dbContext;
        //CancellationTokenSource 当调用 Stop 方法时，它会取消监控任务，从而允许任务在完成当前操作后正常停止，而不是突然中断。
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly CancellationTokenSource _commandCancellationTokenSource = new CancellationTokenSource();

        private Dictionary<int, List<MonitorDataMinute>> deviceDatas = new();
        public EnvironmentFactory(EServerDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _queue = [];
            _tcpMultiServer = new TcpMultiServer<T>(configuration.GetValue<int>("TcpServer:GetEnvironmentPort"), _queue);
            // Start the JSON TCP server with a message processing function
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
                        if (_queue.TryTake(out var sensor))
                        {
                            // Insert the sensor data into the database

                            // 确保sensor不是null并且是FugitiveDust类型
                            if (sensor is MonitorData monitorData)
                            {
                                MonitorDataMinute monitorDataMinute = new MonitorDataMinute();

                                monitorDataMinute.CopyPropertiesFrom(monitorData);
                                // 插入传感器数据到数据库
                                if (deviceDatas.ContainsKey(monitorData.EquipmentId))
                                {
                                    // 如果存在，只需添加到现有列表中
                                    deviceDatas[monitorData.EquipmentId].Add(monitorDataMinute);
                                }
                                else
                                {
                                    // 如果不存在，创建新列表并添加
                                    deviceDatas.Add(monitorData.EquipmentId, new List<MonitorDataMinute> { monitorDataMinute });
                                }
                                _dbContext.EnvironmentalSensorMinute.Add(monitorDataMinute);



                                // 如果需要获取列表中的数据数量
                                if (deviceDatas.TryGetValue(monitorData.EquipmentId, out List<MonitorDataMinute>? dataList) && dataList != null)
                                {
                                    if (dataList.Count % 15 == 0)
                                    {
                                        MonitorDataQuarter quarter = new();

                                        quarter.CopyPropertiesFrom(GetCalculatorData(dataList.TakeLast(15)));
                                        _dbContext.EnvironmentalSensorQuarter.Add(quarter);
                                    }
                                    if (dataList.Count % 60 == 0)
                                    {
                                        MonitorDataHour hour = new();

                                        hour.CopyPropertiesFrom(GetCalculatorData(dataList.TakeLast(60)));

                                        _dbContext.EnvironmentalSensorHour.Add(hour);
                                    }
                                    if (dataList.Count == 60 * 24)
                                    {
                                        MonitorDataDaily daily = new();

                                        daily.CopyPropertiesFrom(GetCalculatorData(dataList.TakeLast(60 * 24)));

                                        _dbContext.EnvironmentalSensorDaily.Add(daily);
                                        deviceDatas[monitorData.EquipmentId] = [];
                                    }
                                }
                                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                                Console.WriteLine($"Insert Db {JsonSerializer.Serialize(monitorData)}");

                            }
                            else
                            {
                                // 如果sensor不是FugitiveDust类型，可以在这里处理错误或记录日志
                                Console.WriteLine("The sensor object is not of type MonitorData.");
                            }
                        }
                        else
                        {
                            _logger.Info("The sensor queue 60s is empty");
                            await Task.Delay(1000 * 60); // 使用Task.Delay代替Thread.Sleep
                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        _logger.Error("Error inserting sensor data into the database", ex);
                    }
                }
            }, _cancellationTokenSource.Token);
        }
        public void Stop()
        {
            _cancellationTokenSource.Cancel();

            _dbContext.Dispose();
            _logger.Info("EnvironmentFactory stopped");
        }
        private MonitorData GetCalculatorData(IEnumerable<MonitorData> monitorDatas)
        {
            if (monitorDatas == null || !monitorDatas.Any())
                throw new ArgumentException("数据列表不能为空");

            return new MonitorData()
            {
                EquipmentId = monitorDatas.First().EquipmentId,
                Date = monitorDatas.Last().Date,

                // PM2.5
                pm2_5_average = (int)monitorDatas.Average(x => x.pm2_5_average),
                pm2_5_max = monitorDatas.Max(x => x.pm2_5_max),
                pm2_5_min = monitorDatas.Min(x => x.pm2_5_min),

                // PM10
                pm10_average = (int)monitorDatas.Average(x => x.pm10_average),
                pm10_max = monitorDatas.Max(x => x.pm10_max),
                pm10_min = monitorDatas.Min(x => x.pm10_min),

                // TSP
                ParticulateMatterAvg = (int)monitorDatas.Average(x => x.ParticulateMatterAvg),
                ParticulateMatterMax = monitorDatas.Max(x => x.ParticulateMatterMax),
                ParticulateMatterMin = monitorDatas.Min(x => x.ParticulateMatterMin),

                // 温度
                TemperatureAvg = (int)monitorDatas.Average(x => x.TemperatureAvg),
                TemperatureMax = monitorDatas.Max(x => x.TemperatureMax),
                TemperatureMin = monitorDatas.Min(x => x.TemperatureMin),

                // 湿度
                HumidityAvg = (int)monitorDatas.Average(x => x.HumidityAvg),
                HumidityMax = monitorDatas.Max(x => x.HumidityMax),
                HumidityMin = monitorDatas.Min(x => x.HumidityMin),

                // 气压
                AirPressureAvg = (int)monitorDatas.Average(x => x.AirPressureAvg),
                AirPressureMax = monitorDatas.Max(x => x.AirPressureMax),
                AirPressureMin = monitorDatas.Min(x => x.AirPressureMin),

                // 风速
                WindSpeedAvg = (int)monitorDatas.Average(x => x.WindSpeedAvg),
                WindSpeedMax = monitorDatas.Max(x => x.WindSpeedMax),
                WindSpeedMin = monitorDatas.Min(x => x.WindSpeedMin),

                // 风向
                WindDirectionAvg = (int)monitorDatas.Average(x => x.WindDirectionAvg),
                WindDirectionMax = monitorDatas.Max(x => x.WindDirectionMax),
                WindDirectionMin = monitorDatas.Min(x => x.WindDirectionMin),

                // 噪声
                NoiseAvg = (int)monitorDatas.Average(x => x.NoiseAvg),
                NoiseMax = monitorDatas.Max(x => x.NoiseMax),
                NoiseMin = monitorDatas.Min(x => x.NoiseMin),

                // 其他字段
                CPMAvg = (int)monitorDatas.Average(x => x.CPMAvg),
                // 使用最新一条数据的状态值
                is_power_on = monitorDatas.Last().is_power_on,
                is_valid_data = monitorDatas.Last().is_valid_data,
                equipment_data_valid_flag = monitorDatas.Last().equipment_data_valid_flag,
                source_of_data = monitorDatas.Last().source_of_data,
                dust_calibration_step = monitorDatas.Last().dust_calibration_step
            };
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}


