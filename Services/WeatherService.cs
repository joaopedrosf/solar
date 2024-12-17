using backend_solar.Models;
using System.Text.Json;

namespace backend_solar.Services {
    public class WeatherService {

        private readonly string _apiKey;

        public WeatherService(IConfiguration configuration) {
            _apiKey = configuration["weatherApiKey"]!;
        }

        public WeatherApiDTO GetCurrentWeather(string query) {
            using (HttpClient httpClient = new HttpClient()) {
                string url = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={query}";
                var response = httpClient.GetAsync(url).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var currentWeather = JsonSerializer.Deserialize<WeatherApiDTO>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return currentWeather;
            }
        }
    }
}
