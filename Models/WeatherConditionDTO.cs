namespace backend_solar.Models {
    public class WeatherConditionDTO {
        public string Text { get; set; }
        public string Icon { get; set; }
        public WeatherConditionEnum Code { get; set; }
    }
}
