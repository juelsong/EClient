namespace EHost.Mqtt
{
    using log4net;
    using Microsoft.Extensions.Options;
    using MQTTnet;
    using MQTTnet.Adapter;
    using MQTTnet.Client;
    using MQTTnet.Exceptions;
    using MQTTnet.Protocol;
    using MQTTnet.Server;
    using System.Net.Security;
    using System.Runtime.CompilerServices;
    using System.Security.Authentication;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    public class MqttClientManager
    {
        private IMqttClient mqttClient;
        private string mqttServer;
        private int mqttPort;
        private string topic;
        private string username;
        private string password;
        private readonly ILog logger = LogManager.GetLogger(nameof(MqttClientManager));
        private readonly MqttClientOptions options;


        public string ClientId { get; private set; }
        public MqttClientManager(string mqttServer, int mqttPort, string topic, string username, string password, string clientId)
        {
            this.mqttServer = mqttServer;
            this.mqttPort = mqttPort;
            this.topic = topic;
            this.username = username;
            this.password = password;
            this.ClientId = clientId;
            InitializeMqttClient();
            options = new MqttClientOptionsBuilder()
                .WithTcpServer(mqttServer, mqttPort)
                .WithCredentials(username, password)
                .WithClientId(ClientId)
                .WithCleanSession(false)
                .WithTlsOptions(new MqttClientTlsOptions
                {
                    UseTls = true,
                    AllowUntrustedCertificates = true, // 允许不受信任的证书
                    IgnoreCertificateChainErrors = true, // 忽略证书链错误
                    IgnoreCertificateRevocationErrors = true, // 忽略证书吊销错误
                    SslProtocol = SslProtocols.Tls12 | SslProtocols.Tls13, // 选择适当的 SSL/TLS 协议版本
                    CertificateValidationHandler = CertificateValidationHandler
                })
                .Build();

        }

        public void InitializeMqttClient()
        {
            var factory = new MqttFactory();
            mqttClient = factory.CreateMqttClient();
            mqttClient.ConnectedAsync += MqttClient_ConnectedAsync; ;
            mqttClient.DisconnectedAsync += MqttClient_DisconnectedAsync;
            mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;
        }
        
        private static bool CertificateValidationHandler(MqttClientCertificateValidationEventArgs args)
        {
            // 这里实现自定义的证书验证逻辑
            // 例如，你可以根据证书的属性来决定是否接受证书

            //// 检查证书是否有效
            //bool isCertificateValid = args.Certificate.IsValid();

            // 你也可以检查证书链是否有效
            //bool isCertificateChainValid = args.Chain.ChainPolicy.RevocationMode == X509RevocationMode.NoCheck;

            //// 如果证书无效，你可以选择是否忽略SSL错误
            //if (!isCertificateValid || !isCertificateChainValid)
            //{
            //    // 检查SSL错误
            //    if (args.SslPolicyErrors == SslPolicyErrors.None)
            //    {
            //        // 没有错误，接受证书
            //        return true;
            //    }
            //    else
            //    {
            //        // 有错误，可以根据情况决定是否接受证书
            //        // 例如，如果你知道证书是自签名的，你可以选择忽略某些错误
            //        Console.WriteLine("SSL证书验证错误: " + args.SslPolicyErrors);
            //        // 返回true以忽略错误，或者返回false以拒绝连接
            //        return true; // 注意：在生产环境中，不建议无条件接受所有证书
            //    }
            //}

            // 如果证书有效，接受证书
            return true;
        }
        public bool IsConnected()
        {
            return this.mqttClient.IsConnected;
        }
        public void Dispose()
        {
            if (mqttClient.IsConnected)
            {
                mqttClient.Dispose();
            }
        }

        private Task MqttClient_ConnectedAsync(MqttClientConnectedEventArgs e)
        {
            logger.Info("Connected to MQTT Broker");
            Task subscribeTask = mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
            Task.Run(() =>
            {
                subscribeTask.Wait();
                logger.Info("Subscribed to topic: " + topic);
            });
            return Task.CompletedTask;
        }

        private Task MqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs e)
        {
            logger.Info("client Disconnected from MQTT Broker");
            return Task.CompletedTask;
        }

        private Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            logger.Info("Received message from topic: " + e.ApplicationMessage.Topic + " Message: " + Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment));
            return Task.CompletedTask;
        }

        public async Task PublishMessage(string message)
        {


            //var jsonFormatter = new MqttJsonFormatter();
            var publishOptions = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
                .WithRetainFlag(false)
                .Build();
            if (!mqttClient.IsConnected)
            {
                try
                {
                    await mqttClient.ConnectAsync(options).ConfigureAwait(false);
                }
                catch (MqttProtocolViolationException ex)
                {
                    logger.Error(ex);
                    throw;
                }
            }
            await mqttClient.PublishAsync(publishOptions).ConfigureAwait(false);
            logger.Info("Message published to topic: " + topic);
        }
    }
}
