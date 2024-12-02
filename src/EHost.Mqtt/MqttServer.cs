namespace EHost.Mqtt
{
    using log4net;
    using MQTTnet;
    using MQTTnet.Server;
    using System;

    /// <summary>
    /// mqtt服务端
    /// </summary>
    public class MqttServerBase
    {
        private readonly MqttServer _mqttServer;
        private readonly ILog logger = LogManager.GetLogger(nameof(MqttServerBase));

        public MqttServerBase(int port)
        {
            var optionsBuilder = new MqttServerOptionsBuilder()
                .WithDefaultEndpoint()
                .WithDefaultEndpointPort(port)
                ;


            var options = optionsBuilder.Build();
            var factory = new MqttFactory();

            _mqttServer = factory.CreateMqttServer(options);


            // 注册事件
            _mqttServer.ValidatingConnectionAsync += HandleValidatingConnectionAsync;

            _mqttServer.ClientConnectedAsync += e =>
            {
                logger.Info($"Client '{e.ClientId}' connected.");
                return Task.CompletedTask;
            };
            _mqttServer.ClientDisconnectedAsync += e =>
            {
                logger.Info($"Client '{e.ClientId}' disconnected.");
                return Task.CompletedTask;
            };

            _mqttServer.ClientSubscribedTopicAsync += e =>
            {
                logger.Info($"Client '{e.ClientId}' subscribed to topic '{e.TopicFilter.Topic}'");
                return Task.CompletedTask;
            };

            // 设置客户端取消订阅主题时的处理器
            _mqttServer.ClientUnsubscribedTopicAsync += e =>
            {
                logger.Info($"Client '{e.ClientId}' unsubscribed from topic '{e.TopicFilter}'");
                return Task.CompletedTask;
            };

        }


        public async Task StartAsync()
        {
            await _mqttServer.StartAsync();
            logger.Info("MQTT Server started.");
        }

        public async Task StopAsync()
        {
            await _mqttServer.StopAsync();
            logger.Info("MQTT Server stopped.");
        }

        private Task HandleValidatingConnectionAsync(ValidatingConnectionEventArgs context)
        {
            // 这里可以添加连接验证逻辑
            logger.Info($"Client '{context.ClientId}' is trying to connect.");
            return Task.CompletedTask;
        }
    }
}



