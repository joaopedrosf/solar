using MQTTnet;
using MQTTnet.Client;

namespace backend_solar.Connections {
    public class MQTTConnectionFactory {

        private readonly string _ipAdress;
        private readonly int _port;

        public MQTTConnectionFactory(IConfiguration configuration) {
            _ipAdress = configuration.GetValue<string>("mqttIp");
            _port = configuration.GetValue<int>("mqttPort");
        }

        public async Task<IMqttClient> Create() {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(_ipAdress, _port)
                .WithCleanSession()
                .Build();

            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();

            await mqttClient.ConnectAsync(options);

            return mqttClient;
        }
    }
}
