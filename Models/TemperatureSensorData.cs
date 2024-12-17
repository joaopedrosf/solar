namespace backend_solar.Models {
    public class TemperatureSensorData : SensorBaseData {
        public double Temperature { get; set; }

        public TemperatureSensorData() {
            Type = SensorType.Temperature;
        }
    }
}
