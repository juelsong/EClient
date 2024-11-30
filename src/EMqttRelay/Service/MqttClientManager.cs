using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System.Text;

namespace EMqttRelay.Service
{
    public class MqttClientManager
    {
        private IMqttClient mqttClient;
        private string mqttServer;
        private int mqttPort;
        private string topic;
        private string username;
        private string password;

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
        }

        private void InitializeMqttClient()
        {
            var factory = new MqttFactory();
            mqttClient = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(mqttServer, mqttPort)
                .WithCredentials(username, password)
                .WithClientId(ClientId)
                .WithCleanSession()
                //.WithKeepAlivePeriod(TimeSpan.FromSeconds(30))
                //.WithTls(new MqttClientTlsOptionsBuilder()
                //    .WithCertificateValidationHandler(ValidateServerCertificate)
                //    .Build())
                .WithTlsOptions(new MqttClientTlsOptions()
                {


                })
                .Build();

            mqttClient.ConnectedAsync += MqttClient_ConnectedAsync; ;
            mqttClient.DisconnectedAsync += MqttClient_DisconnectedAsync;
            mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;
            mqttClient.ConnectAsync(options).Wait();

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
            Console.WriteLine("Connected to MQTT Broker");
            Task subscribeTask = mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
            Task.Run(() =>
            {
                subscribeTask.Wait();
                Console.WriteLine("Subscribed to topic: " + topic);
            });
            return Task.CompletedTask;
        }

        private Task MqttClient_DisconnectedAsync(EventArgs e)
        {
            Console.WriteLine("Disconnected from MQTT Broker");
            return Task.CompletedTask;
        }

        private Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine("Received message from topic: " + e.ApplicationMessage.Topic + " Message: " + Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment));
            return Task.CompletedTask;
        }

        public void PublishMessage(string message)
        {
            if (mqttClient.IsConnected)
            {
                //var jsonFormatter = new MqttJsonFormatter();
                var publishOptions = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(message)
                    .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
                    .WithRetainFlag(false)
                    .Build();

                mqttClient.PublishAsync(publishOptions).Wait();
                Console.WriteLine("Message published to topic: " + topic);
            }
            else
            {
                Console.WriteLine("Client is not connected. Cannot publish message.");
            }
        }
    }
}
