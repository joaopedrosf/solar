using System.Text.Json.Serialization;

namespace backend_solar.Models {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DirtLevel {
        Low,
        Moderate,
        High,
        Extreme
    }
}
