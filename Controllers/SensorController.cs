using backend_solar.Models;
using backend_solar.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_solar.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class SensorController : ControllerBase {

        private SensorService _sensorService;

        public SensorController(SensorService sensorService) {
            _sensorService = sensorService;
        }

        [HttpPost]
        public ActionResult CreateSensor([FromBody] Sensor sensor) {
            var sensorResult = _sensorService.CreateSensor(sensor);
            return Ok(sensor);
        }

        [HttpPost("data/temperature")]
        public async Task<ActionResult> InsertTemperatureSensorData([FromBody] TemperatureSensorData data) {
            await _sensorService.ReceiveTemperatureSensorData(data);
            return Ok();
        }

        [HttpGet("data/temperature/{sensorId}")]
        public async Task<ActionResult<List<TemperatureSensorData>>> FindTemperatureGraph(Guid sensorId, [FromBody] SearchTimespanDTO searchTimespan) {
            var temperatureData = await _sensorService.FindTemperatureGraph(sensorId, searchTimespan.InitialDate, searchTimespan.FinalDate);
            return Ok(temperatureData);
        }

        [HttpPost("data/solarpanel")]
        public async Task<ActionResult> InsertSolarPanelSensorData([FromBody] SolarPanelData data) {
            await _sensorService.ReceiveSolarPanelSensorData(data);
            return Ok();
        }

        [HttpGet("data/solarpanel/{sensorId}")]
        public async Task<ActionResult<List<SolarPanelData>>> FindSolarPanelGraph(Guid sensorId, [FromBody] SearchTimespanDTO searchTimespan) {
            var solarPanelData = await _sensorService.FindPowerGenerationGraph(sensorId, searchTimespan.InitialDate, searchTimespan.FinalDate);
            return Ok(solarPanelData);
        }

        [HttpPost("data/dirt")]
        public async Task<ActionResult> InsertDirtSensorData([FromBody] DirtSensorData data) {
            await _sensorService.ReceiveDirtSensorData(data);
            return Ok();
        }

        [HttpGet("data/dirt/{sensorId}")]
        public async Task<ActionResult<List<DirtSensorData>>> FindDirtGraph(Guid sensorId, [FromBody] SearchTimespanDTO searchTimespan) {
            var dirtData = await _sensorService.FindDirtGraph(sensorId, searchTimespan.InitialDate, searchTimespan.FinalDate);
            return Ok(dirtData);
        }

        [HttpPost("data/luminosity")]
        public async Task<ActionResult> InsertLuminosityData([FromBody] LuminositySensorData data) {
            await _sensorService.ReceiveLuminositySensorData(data);
            return Ok();
        }

        [HttpGet("data/luminosity/{sensorId}")]
        public async Task<ActionResult<List<LuminositySensorData>>> FindLuminosityGraph(Guid sensorId, [FromBody] SearchTimespanDTO searchTimespan) {
            var luminosityData = await _sensorService.FindLuminosityGraph(sensorId, searchTimespan.InitialDate, searchTimespan.FinalDate);
            return Ok(luminosityData);
        }
    }
}
