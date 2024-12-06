namespace EHost.Mqtt
{
    using log4net;
    using MQTTnet;
    using MQTTnet.Exceptions;
    using MQTTnet.Protocol;
    using MQTTnet.Server;
    using System;
    using System.Net;
    using System.Security.Authentication;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// mqtt服务端
    /// </summary>
    public class MqttServerBase
    {
        private readonly MqttServer _mqttServer;
        private readonly ILog logger = LogManager.GetLogger(nameof(MqttServerBase));
        private readonly int _port;

        public MqttServerBase(int port)
        {
            this._port = port;
            var certificate = CreateSelfSignedCertificate("1.3.6.1.5.5.7.3.1");

            var optionsBuilder = new MqttServerOptionsBuilder()
                .WithDefaultEndpointPort(port) // 设置默认的端口号，应与客户端使用的端口号相同
                .WithEncryptedEndpoint() // 启用加密端点，因为客户端使用TLS
                .WithEncryptionCertificate(certificate)
                ;
            var options = optionsBuilder.Build();
            var factory = new MqttFactory();

            _mqttServer = factory.CreateMqttServer(options);
            this.SetEvent();
        }

        static X509Certificate2 CreateSelfSignedCertificate(string oid)
        {
            var sanBuilder = new SubjectAlternativeNameBuilder();
            sanBuilder.AddIpAddress(IPAddress.Loopback);
            sanBuilder.AddIpAddress(IPAddress.IPv6Loopback);
            sanBuilder.AddDnsName("localhost");

            using (var rsa = RSA.Create())
            {
                var certRequest = new CertificateRequest("CN=localhost", rsa, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);

                certRequest.CertificateExtensions.Add(
                    new X509KeyUsageExtension(X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature, false));

                certRequest.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(new OidCollection { new(oid) }, false));

                certRequest.CertificateExtensions.Add(sanBuilder.Build());

                using (var certificate = certRequest.CreateSelfSigned(DateTimeOffset.Now.AddMinutes(-10), DateTimeOffset.Now.AddMinutes(10)))
                {
                    var pfxCertificate = new X509Certificate2(
                        certificate.Export(X509ContentType.Pfx),
                        (string)null!,
                        X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

                    return pfxCertificate;
                }
            }
        }
        public async Task StartAsync()
        {
            try
            {
                await _mqttServer.StartAsync();
                logger.Info($"MQTT Server started.port {this._port}");

            }
            catch (MqttProtocolViolationException ex)
            {
                logger.Error(ex);

                throw;
            }
            catch (Exception ex)
            {
                logger.Error(ex);

                throw;
            }
            
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
        public  void SetEvent()
        {
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
    }
}



