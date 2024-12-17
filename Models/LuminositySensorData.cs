namespace backend_solar.Models {
    public class LuminositySensorData : SensorBaseData {
        public double Luminosity { get; set; }

        public LuminositySensorData() {
            Type = SensorType.Luminosity;
        }
    }
}
