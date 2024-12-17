using backend_solar.Models;
using backend_solar.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_solar.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PlaceController : ControllerBase {

        private readonly PlaceService _placeService;

        public PlaceController(PlaceService placeService) {
            _placeService = placeService;
        }

        [HttpPost]
        public async Task<ActionResult<Place>> CreatePlace([FromBody] CreatePlaceDTO placeDto) {
            var placeResult = await _placeService.CreatePlace(placeDto);
            return Ok(placeResult);
        }

        [HttpGet("{placeId}")]
        public async Task<ActionResult<PlaceDashboardDTO>> FindPlace(Guid placeId) {
            var place = await _placeService.FindPlace(placeId);
            return Ok(place);
        }
    }
}
