using backend_solar.Connections;
using backend_solar.Models;
using backend_solar.Repositories;
using Microsoft.EntityFrameworkCore;
using MQTTnet;

namespace backend_solar.Services {
    public class SensorService {

        private readonly MQTTConnectionFactory _mqttConnectionFactory;
        private readonly SensorContext _context;
        private readonly WeatherService _weatherService;
        private readonly List<WeatherConditionEnum> rainyConditions = new List<WeatherConditionEnum> { WeatherConditionEnum.PatchyRainPossible, WeatherConditionEnum.PatchyLightRain, WeatherConditionEnum.LightRain, WeatherConditionEnum.ModerateRainAtTimes, WeatherConditionEnum.ModerateRain, WeatherConditionEnum.HeavyRainAtTimes, WeatherConditionEnum.HeavyRain, WeatherConditionEnum.LightFreezingRain, WeatherConditionEnum.ModerateOrHeavyFreezingRain, WeatherConditionEnum.LightRainShower, WeatherConditionEnum.ModerateOrHeavyRainShower, WeatherConditionEnum.TorrentialRainShower, WeatherConditionEnum.PatchyLightRainWithThunder, WeatherConditionEnum.ModerateOrHeavyRainWithThunder };

        public SensorService(MQTTConnectionFactory mqttConnectionFactory, SensorContext context, WeatherService weatherService) {
            _mqttConnectionFactory = mqttConnectionFactory;
            _context = context;
            _weatherService = weatherService;
        }

        public async Task<Sensor> CreateSensor(Sensor sensor) {
            await _context.AddAsync(sensor);
            await _context.SaveChangesAsync();
            return sensor;
        }

        public async Task ReceiveSolarPanelSensorData(SolarPanelData data) {
            data.Type = SensorType.SolarPanel;
            await VerifySensorStatus(data);

            if (data.CurrentPowerGeneration < 0.5) {
                var weather = _weatherService.GetCurrentWeather("Goiania");
                if (weather.Current.Condition.Code != WeatherConditionEnum.Sunny) {
                    await SendNotification("Solar panel generating low power because it's not sunny outside");
                }
                else {
                    await SendNotification("Solar panel is not generating enough power");
                }
            }
            
            await _context.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task ReceiveTemperatureSensorData(TemperatureSensorData data) {
            data.Type = SensorType.Temperature;
            await VerifySensorStatus(data);

            if (data.Temperature > 45 && data.Temperature < 60) {
                await SendNotification("System overheating detected");
            }

            else if (data.Temperature >= 60) {
                var mqttClient = await _mqttConnectionFactory.Create();
                await mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
                    .WithTopic("shutdownSystem")
                    .WithPayload("System passed max levels of temperature, shutting down for safety...")
                    .Build());
            }

            await _context.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task ReceiveDirtSensorData(DirtSensorData data) {
            data.Type = SensorType.Dirt;
            await VerifySensorStatus(data);

            if (data.DirtLevel == DirtLevel.High) {
                await SendNotification($"Dirt sensor {data.Id} detected high dirt level");
            }

            else if (data.DirtLevel == DirtLevel.Extreme) {
                var weather = _weatherService.GetCurrentWeather("Goiania");
                if (rainyConditions.Contains(weather.Current.Condition.Code)) {
                    await SendNotification("Extreme dirt level detected, but it's raining outside, so the cleaning system will not be activated");
                }
                else {
                    var mqttClient = await _mqttConnectionFactory.Create();
                    await mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
                        .WithTopic("activateCleaningSystem")
                        .WithPayload("Extreme dirt level detected")
                        .Build());
                }
            }

            await _context.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task ReceiveLuminositySensorData(LuminositySensorData data) {
            data.Type = SensorType.Luminosity;
            await VerifySensorStatus(data);

            var weather = _weatherService.GetCurrentWeather("Goiania");
            if (data.Luminosity < 3000 && weather.Current.Condition.Code == WeatherConditionEnum.Sunny) {
                await SendNotification("Low luminosity detected, but it's sunny outside");
            }

            await _context.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TemperatureSensorData>> FindTemperatureGraph(Guid sensorId, DateTime initialDate, DateTime finalDate) {
            return await _context.TemperatureSensorData
                    .Where(d => d.Id == sensorId && d.Timestamp >= initialDate && d.Timestamp <= finalDate)
                    .OrderBy(d => d.Timestamp)
                    .ToListAsync();
        }

        public async Task<List<LuminositySensorData>> FindLuminosityGraph(Guid sensorId, DateTime initialDate, DateTime finalDate) {
               return await _context.LuminositySensorData
                    .Where(d => d.Id == sensorId && d.Timestamp >= initialDate && d.Timestamp <= finalDate)
                    .OrderBy(d => d.Timestamp)
                    .ToListAsync();
        }

        public async Task<List<SolarPanelData>> FindPowerGenerationGraph(Guid sensorId, DateTime initialDate, DateTime finalDate) {
            return await _context.SolarPanelData
                    .Where(d => d.Id == sensorId && d.Timestamp >= initialDate && d.Timestamp <= finalDate)
                    .OrderBy(d => d.Timestamp)
                    .ToListAsync();
        }

        public async Task<List<DirtSensorData>> FindDirtGraph(Guid sensorId, DateTime initialDate, DateTime finalDate) {
            return await _context.DirtSensorData
                    .Where(d => d.Id == sensorId && d.Timestamp >= initialDate && d.Timestamp <= finalDate)
                    .OrderBy(d => d.Timestamp)
                    .ToListAsync();
        }

        private async Task SendNotification(string payload) {
            var mqttClient = await _mqttConnectionFactory.Create();
            await mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
                .WithTopic("notificationTopic")
                .WithPayload(payload)
                .Build());
        }

        private async Task VerifySensorStatus(SensorBaseData data) {
            if (!data.IsStatusOk) {
                await SendNotification($"Sensor {data.Id} of type {data.Type} is not working properly");
            }

            if (!data.IsNetworkWorking) {
                await SendNotification($"Sensor {data.Id} of type {data.Type} had network problems");
            }
        }
    }
}
