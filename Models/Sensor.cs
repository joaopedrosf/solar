namespace backend_solar.Models {
    public class Sensor {
        public Guid Id { get; set; }
        public Guid PlaceId { get; set; }
        public SensorType Type { get; set; }

    }
}
