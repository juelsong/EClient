namespace EHost.Mqtt
{
    using log4net;
    using MQTTnet;
    using MQTTnet.Client;
    using MQTTnet.Exceptions;
    using MQTTnet.Protocol;
    using MQTTnet.Server;
    using System.Diagnostics;
    using System.Security.Authentication;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class MqttClientManager
    {
        const string mosquitto_org = @"
-----BEGIN CERTIFICATE-----
MIIDjjCCAnagAwIBAgIQAzrx5qcRqaC7KGSxHQn65TANBgkqhkiG9w0BAQsFADBh
MQswCQYDVQQGEwJVUzEVMBMGA1UEChMMRGlnaUNlcnQgSW5jMRkwFwYDVQQLExB3
d3cuZGlnaWNlcnQuY29tMSAwHgYDVQQDExdEaWdpQ2VydCBHbG9iYWwgUm9vdCBH
MjAeFw0xMzA4MDExMjAwMDBaFw0zODAxMTUxMjAwMDBaMGExCzAJBgNVBAYTAlVT
MRUwEwYDVQQKEwxEaWdpQ2VydCBJbmMxGTAXBgNVBAsTEHd3dy5kaWdpY2VydC5j
b20xIDAeBgNVBAMTF0RpZ2lDZXJ0IEdsb2JhbCBSb290IEcyMIIBIjANBgkqhkiG
9w0BAQEFAAOCAQ8AMIIBCgKCAQEAuzfNNNx7a8myaJCtSnX/RrohCgiN9RlUyfuI
2/Ou8jqJkTx65qsGGmvPrC3oXgkkRLpimn7Wo6h+4FR1IAWsULecYxpsMNzaHxmx
1x7e/dfgy5SDN67sH0NO3Xss0r0upS/kqbitOtSZpLYl6ZtrAGCSYP9PIUkY92eQ
q2EGnI/yuum06ZIya7XzV+hdG82MHauVBJVJ8zUtluNJbd134/tJS7SsVQepj5Wz
tCO7TG1F8PapspUwtP1MVYwnSlcUfIKdzXOS0xZKBgyMUNGPHgm+F6HmIcr9g+UQ
vIOlCsRnKPZzFBQ9RnbDhxSJITRNrw9FDKZJobq7nMWxM4MphQIDAQABo0IwQDAP
BgNVHRMBAf8EBTADAQH/MA4GA1UdDwEB/wQEAwIBhjAdBgNVHQ4EFgQUTiJUIBiV
5uNu5g/6+rkS7QYXjzkwDQYJKoZIhvcNAQELBQADggEBAGBnKJRvDkhj6zHd6mcY
1Yl9PMWLSn/pvtsrF9+wX3N3KjITOYFnQoQj8kVnNeyIv/iPsGEMNKSuIEyExtv4
NeF22d+mQrvHRAiGfzZ0JFrabA0UWTW98kndth/Jsw1HKj2ZL7tcu7XUIOGZX1NG
Fdtom/DzMNU+MeKNhJ7jitralj41E6Vf8PlwUHBHQRFXGU7Aj64GxJUTFy8bJZ91
8rGOmaFvE7FBcf6IKshPECBV1/MUReXgRPTqh5Uykw7+U0b6LJ3/iyK5S9kJRaTe
pLiaWN0bfVKfjllDiIGknibVb63dDcY3fe0Dkhvld1927jyNxF1WW6LZZm6zNTfl
MrY=
-----END CERTIFICATE-----
";
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

            var factory = new MqttFactory();
            mqttClient = factory.CreateMqttClient();

            //X509Certificate2Collection caChain = new X509Certificate2Collection();
            //caChain.ImportFromPem(mosquitto_org); // from https://test.mosquitto.org/ssl/mosquitto.org.crt
            //var certificate = new X509Certificate("broker.emqx.io-ca.crt", "");

            options = new MqttClientOptionsBuilder()
                .WithTcpServer(mqttServer, mqttPort)
                .WithCredentials(username, password)
                .WithClientId(ClientId)
                .WithCleanSession(false)
                .WithKeepAlivePeriod(TimeSpan.FromMinutes(2))
                .WithTlsOptions(new MqttClientTlsOptions
                {
                    UseTls = true,
                    //AllowUntrustedCertificates  = true,
                    //SslProtocol = SslProtocols.Tls12,
                    CertificateValidationHandler = (e) => true,
                    IgnoreCertificateRevocationErrors = true,
                    IgnoreCertificateChainErrors = true,
                    AllowUntrustedCertificates = true,
                    //TargetHost = mqttServer+":"+mqttPort,                    
                })
                .Build();
            InitializeMqttClient();

        }

        public void InitializeMqttClient()
        {
            mqttClient.ConnectedAsync += MqttClient_ConnectedAsync; ;
            mqttClient.DisconnectedAsync += MqttClient_DisconnectedAsync;
            mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;
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
            logger.Info($"{ClientId} Connected to MQTT Broker");
            //var subscribeTask = mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
            //    .WithTopic(topic)
            //    .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
            //    .WithAtMostOnceQoS()
            //    .Build());

            //await subscribeTask.ConfigureAwait(false);
            //logger.Info($"{ClientId} Subscribed to topic: " + topic);
            return Task.CompletedTask;
        }

        private Task MqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs e)
        {
            logger.Info($"{ClientId} Disconnected from MQTT Broker");
            return Task.CompletedTask;
        }

        private Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            logger.Info($"{ClientId} Received message from topic: " + e.ApplicationMessage.Topic + " Message: " + Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment));
            return Task.CompletedTask;
        }
        public async Task Connect()
        {
            MqttClientConnectResult connectResult; // 声明变量

            try
            {
                connectResult = await mqttClient.ConnectAsync(options).ConfigureAwait(false);
                logger.Info($"Connecting {connectResult.UserProperties}");
            }
            catch (MqttClientNotConnectedException ex)
            {
                logger.Error(ex);

                throw;
            }
            catch (MqttProtocolViolationException ex)
            {
                logger.Error(ex);
                throw;
            }
            catch (MqttCommunicationException ex)
            {
                logger.Error(ex);
                throw;
            }
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
                await Connect().ConfigureAwait(false);
            }
            try
            {
                var result = await mqttClient.PublishAsync(publishOptions, CancellationToken.None);
                logger.Info("PublishResult" + result.IsSuccess + result.ReasonCode + result.PacketIdentifier);
                logger.Info($"{ClientId}  published to {this.mqttServer}:{this.mqttPort} at topic: [{topic}]:{message} ");
            }
            catch (MqttClientNotConnectedException ex)
            {
                logger.Error(ex);

                throw;
            }
            catch (MqttClientDisconnectedException ex)
            {
                logger.Error(ex);

                throw;
            }
            catch (MqttProtocolViolationException ex)
            {
                logger.Error(ex);
                throw;
            }
            catch (MqttCommunicationException ex)
            {
                logger.Error(ex);
                throw;
            }

        }
    }
}
