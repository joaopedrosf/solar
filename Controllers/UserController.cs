using backend_solar.Models;
using backend_solar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_solar.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase {
        private readonly UserService _userService;

        public UserController(UserService userService) {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user) {
            var userResult = await _userService.CreateUser(user);
            return Ok(userResult);
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<ActionResult<User>> FindUser(Guid userId) {
            var user = await _userService.FindUser(userId);
            return Ok(user);
        }
    }
}
