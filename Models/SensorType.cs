using System.Text.Json.Serialization;

namespace backend_solar.Models {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SensorType {
        SolarPanel,
        Dirt,
        Luminosity,
        Temperature
    }
}
