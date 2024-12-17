namespace backend_solar.Models {
    public class PlaceDashboardDTO {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<SolarPanelData> SolarPanelSensors { get; set; } = new();
        public List<TemperatureSensorData> TemperatureSensors { get; set; } = new();
        public List<DirtSensorData> DirtSensors { get; set; } = new();
        public List<LuminositySensorData> LuminositySensors { get; set; } = new();
    }
}
