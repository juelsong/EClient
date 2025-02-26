using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace EHost.TcpServer.Service
{
    /// <summary>
    /// 用于接受指令信息
    /// </summary>
    public class CommandTcpServer
    {
        protected readonly int _port;

        private readonly TcpListener _tcpListener;
        private readonly ILog _logger = LogManager.GetLogger(nameof(CommandTcpServer));
        private readonly ConcurrentDictionary<int, TcpClient> _connectedDevices;
        private readonly Action<int, string> _enqueueCommandAction;

        private bool _isRunning;
        private int _nextDeviceId;

        public CommandTcpServer(int port, Action<int, string> enqueueCommandAction)
        {
            _port = port;
            _tcpListener = new TcpListener(IPAddress.Any, port);
            _connectedDevices = new ConcurrentDictionary<int, TcpClient>();
            _nextDeviceId = 1;
            _enqueueCommandAction = enqueueCommandAction;
        }

        public async Task StartAsync()
        {
            _tcpListener.Start();
            _isRunning = true;
            _logger.Info($"Command TCP server started  on _port {_port}");

            while (_isRunning)
            {
                try
                {
                    var client = await _tcpListener.AcceptTcpClientAsync();
                    var deviceId = _nextDeviceId++;
                    _connectedDevices.TryAdd(deviceId, client);
                    Console.WriteLine($"Device connected with ID: {deviceId}");

                    _ = Task.Run(() => HandleClientAsync(deviceId, client));
                }
                catch (Exception ex)
                {
                    _logger.Error("Error accepting client connection", ex);
                }
            }
        }

        public void Stop()
        {
            _isRunning = false;
            _tcpListener.Stop();
            _logger.Info("JSON TCP server stopped.");
        }

        private async Task HandleClientAsync(int deviceId, TcpClient client)
        {
            var buffer = new byte[1024];
            var stream = client.GetStream();

            try
            {
                while (true)
                {
                    var byteCount = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (byteCount <= 0)
                    {
                        Console.WriteLine($"device {deviceId}: disconnected");

                        break; // Client disconnected
                    }

                    var message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                    Console.WriteLine($"Received message from device {deviceId}: {message}");

                    // 使用委托将命令和设备ID传入队列
                    _enqueueCommandAction(deviceId, message);

                    // 立即回复客户端，告知命令已接收
                    var response = "Command received and processing.";
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    Console.WriteLine($"Sent response to device {deviceId}: {response}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling device {deviceId}: {ex.Message}");
            }
            finally
            {
                if (_connectedDevices.TryRemove(deviceId, out _))
                {
                    Console.WriteLine($"Device {deviceId} removed from management.");
                }
                client.Close();
                Console.WriteLine($"Device {deviceId} disconnected.");
            }
        }

        public void SendResponseToClient(int deviceId, string result)
        {
            if (_connectedDevices.TryGetValue(deviceId, out var client))
            {
                var stream = client.GetStream();
                var responseBytes = Encoding.UTF8.GetBytes(result);
                stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                Console.WriteLine($"Sent final result to device {deviceId}: {result}");
            }
            else
            {
                Console.WriteLine($"Device {deviceId} not found");
            }
        }
    }
}

