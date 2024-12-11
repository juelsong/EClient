namespace EHost.Mqtt
{
    using log4net;
    using System.Text;
    using uPLibrary.Networking.M2Mqtt;

    public class MqttClientManagerOther
    {
        private MqttClient mqttClient;
        private string mqttServer;
        private int mqttPort;
        private string topic;
        private string username;
        private string password;
        private readonly ILog logger = LogManager.GetLogger(nameof(MqttClientManager));


        public string ClientId { get; private set; }
        public MqttClientManagerOther(string mqttServer, int mqttPort, string topic, string username, string password, string clientId)
        {
            this.mqttServer = mqttServer;
            this.mqttPort = mqttPort;
            this.topic = topic;
            this.username = username;
            this.password = password;
            this.ClientId = clientId;
            mqttClient = new MqttClient(
                mqttServer,
                mqttPort,
                false,
                null,
                null,
                MqttSslProtocols.TLSv1_2);
            try
            {
                var s = mqttClient.Connect(ClientId, username, password);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }


        public bool IsConnected()
        {
            return this.mqttClient.IsConnected;
        }
        public void Dispose()
        {
            if (mqttClient.IsConnected)
            {
                mqttClient.Disconnect();
            }
        }


        public void PublishMessage(string message)
        {
            if (mqttClient.IsConnected)
            {
                mqttClient.Publish(topic, Encoding.Default.GetBytes(message), 0, false);
            }
        }
    }
}
