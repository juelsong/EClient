using EHost.App.Models;
using EHost.TcpServer.Service;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace EHost.App.Service
{
    /// <summary>
    /// 用于多设备下发指令
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TcpMuiltiServerPlus<T> : TcpMultiServer<T>
    {
        private readonly ConcurrentDictionary<string, DeviceManager<T>> deviceManagers = new ConcurrentDictionary<string, DeviceManager<T>>();

        public TcpMuiltiServerPlus(int port, BlockingCollection<T>? queue = null)
            : base(port, queue)
        {
        }

        public override async Task StartAsync()
        {
            logger.Info($"Starting TCP server on _port {_port}");
            tcpListener = new TcpListener(IPAddress.Any, _port);
            tcpListener.Start();
            isRunning = true;
            try
            {
                while (isRunning)
                {
                    TcpClient client = await tcpListener.AcceptTcpClientAsync();
                    var clientEndpoint = client.Client.RemoteEndPoint?.ToString();
                    var deviceManager = new DeviceManager<T>(client, _queue);
                    if (!string.IsNullOrEmpty(clientEndpoint))
                    {
                        deviceManagers.TryAdd(clientEndpoint, deviceManager);
                        logger.Info($"Device connected: {clientEndpoint}");

                        // 使用异步任务处理客户端
                        _ = Task.Run(async () =>
                        {
                            try
                            {
                                await deviceManager.HandleClientAsync();
                            }
                            catch (Exception ex)
                            {
                                logger.Error($"Error handling client {clientEndpoint}", ex);
                            }
                            finally
                            {
                                // 确保在客户端断开连接时释放资源
                                if (deviceManagers.TryRemove(clientEndpoint, out _))
                                {
                                    logger.Info($"Device disconnected: {clientEndpoint}");
                                }
                                client.Close();
                            }
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in TCP server loop", ex);
            }
            finally
            {
                tcpListener.Stop();
                logger.Info("TCP server stopped.");
            }
        }
        // 方法来获取所有 DeviceManager<T> 的 ID
        public IEnumerable<DeviceManager<T>> GetAllDeviceManagers()
        {
            return deviceManagers.Values;
        }
        // 方法来获取所有 DeviceManager<T> 的 ID
        public DeviceManager<T> GetDeviceManager(string clientId)
        {
            return GetAllDeviceManagers().FirstOrDefault(dm => dm.ClientId == clientId);
        }
        public async Task<DeviceCommandResponse> SendCommandAsync(DeviceCommandRequest request)
        {
            var deviceCommandResponse = new DeviceCommandResponse();
            var commandResponses = new List<CommandResult>();
            //TODO 发送指令给设备
            //验证用户名和密码
            var deviceCommand = "";
            try
            {
                foreach (var id in request.Request.Ids)
                {
                    var d = GetDeviceManager(id);
                    if (d != null)
                    {
                        var response = await d.SendDataAsync(request.Request.Data, request.Request.DataType);
                        commandResponses.Add(new CommandResult
                        {
                            Id = id,
                            Response = response,
                            ResponseType = request.Request.ResponseType,
                            Status = "success"
                        });
                    }
                    else
                    {
                        commandResponses.Add(new CommandResult
                        {
                            Id = id,
                            Status = "error",
                            Message = "设备未连接",
                            Code = "3001"
                        });
                    }
                }
                deviceCommandResponse.Status = "success";
                deviceCommandResponse.Message = "发送成功";
                deviceCommandResponse.Data = commandResponses;
            }
            catch (Exception ex)
            {
                logger.Error("Error sending command", ex);
                deviceCommandResponse.Status = "error";
                deviceCommandResponse.Message = "服务器内部错误";
            }
            return deviceCommandResponse;
        }

        public async Task<DeviceQueryResponse> QueryDeviceAsync(DeviceQueryRequest request)
        {
            var responses = new DeviceQueryResponse();
            //TODO: 获取设备信息
            var deviceQueryCommand = "";

            try
            {
                if (string.IsNullOrEmpty(request.Id))
                {
                    foreach (var device in deviceManagers)
                    {
                        var response = await device.Value.SendDataAsync(deviceQueryCommand, "utf8");
                        var deviceInfo = JsonSerializer.Deserialize<DeviceInfo>(response);
                        responses.Data.Add(deviceInfo);
                    }
                }
                else
                {
                    if (deviceManagers.TryGetValue(request.Id, out var deviceManager))
                    {
                        var response = await deviceManager.SendDataAsync(deviceQueryCommand, "utf8");
                        var deviceInfo = JsonSerializer.Deserialize<DeviceInfo>(response);
                        responses.Data.Add(deviceInfo);
                    }
                }
                responses.Status = "success";
                responses.Message = "查询成功";
            }
            catch (Exception ex)
            {
                logger.Error("Error querying device", ex);
                responses.Status = "error";
                responses.Message = "查询失败";
            }
            return responses;
        }

    }
}