namespace backend_solar.Models {
    public class SolarPanelData : SensorBaseData {
        public double CurrentPowerGeneration { get; set; }
        public SolarPanelData() {
            Type = SensorType.SolarPanel;
        }
    }
}
