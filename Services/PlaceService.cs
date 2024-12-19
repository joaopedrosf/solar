using backend_solar.Models;
using backend_solar.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend_solar.Services {
    public class PlaceService {
        private readonly SensorContext _context;

        public PlaceService(SensorContext context) {
            _context = context;
        }

        public async Task<Place> CreatePlace(CreatePlaceDTO placeDto) {
            var place = new Place() { Name = placeDto.Name };
            place.AllowedUsers = await _context.Users.Where(u => u.Id == placeDto.UserId).ToListAsync();
            await _context.Places.AddAsync(place);
            await _context.SaveChangesAsync();
            return place;
        }

        public async Task<PlaceDashboardDTO> FindPlace(Guid placeId) {
            var place = await _context.Places.AsNoTracking().Where(p => p.Id == placeId).Include(p => p.Sensors).AsNoTracking().FirstAsync();
            var placeDto = new PlaceDashboardDTO() { Id = place.Id, Name = place.Name };
            foreach (var sensor in place.Sensors) {
                switch (sensor.Type) {
                    case SensorType.Dirt:
                        var dirtSensor = await _context.DirtSensorData.AsNoTracking().Where(d => d.Id == sensor.Id).OrderByDescending(d => d.Timestamp).FirstOrDefaultAsync();
                        if (dirtSensor is null) {
                            dirtSensor = new DirtSensorData() { Id = sensor.Id, Type = SensorType.Dirt };
                        }
                        placeDto.DirtSensors.Add(dirtSensor);
                        break;
                    case SensorType.Luminosity:
                        var luminositySensor = await _context.LuminositySensorData.AsNoTracking().Where(d => d.Id == sensor.Id).OrderByDescending(d => d.Timestamp).FirstOrDefaultAsync();
                        if (luminositySensor is null) {
                            luminositySensor = new LuminositySensorData() { Id = sensor.Id, Type = SensorType.Luminosity };
                        }
                        placeDto.LuminositySensors.Add(luminositySensor);
                        break;
                    case SensorType.SolarPanel:
                        var solarPanelSensor = await _context.SolarPanelData.AsNoTracking().Where(d => d.Id == sensor.Id).OrderByDescending(d => d.Timestamp).FirstOrDefaultAsync();
                        if (solarPanelSensor is null) {
                            solarPanelSensor = new SolarPanelData() { Id = sensor.Id, Type = SensorType.SolarPanel };
                        }
                        placeDto.SolarPanelSensors.Add(solarPanelSensor);
                        break;
                    case SensorType.Temperature:
                        var temperatureSensor = await _context.TemperatureSensorData.AsNoTracking().Where(d => d.Id == sensor.Id).OrderByDescending(d => d.Timestamp).FirstOrDefaultAsync();
                        if (temperatureSensor is null) {
                            temperatureSensor = new TemperatureSensorData() { Id = sensor.Id, Type = SensorType.Temperature };
                        }
                        placeDto.TemperatureSensors.Add(temperatureSensor);
                        break;
                }
            }

            return placeDto;
        }
    }
}
