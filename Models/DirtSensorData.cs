namespace backend_solar.Models {
    public class DirtSensorData : SensorBaseData {
        public DirtLevel DirtLevel { get; set; }

        public DirtSensorData() {
            Type = SensorType.Dirt;
        }
    }
}
