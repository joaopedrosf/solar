namespace backend_solar.Models {
    public abstract class SensorBaseData {
        public Guid Id { get; set; }
        public SensorType Type { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsNetworkWorking { get; set; }
        public bool IsStatusOk { get; set; }
    }
}
