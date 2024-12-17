using backend_solar.Models;
using backend_solar.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_solar.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase {

        private readonly LoginService _loginService;

        public LoginController(LoginService loginService) {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Login([FromBody] LoginRequestDTO loginRequest) {
            var user = await _loginService.Login(loginRequest);
            return Ok(user);
        }
    }
}
