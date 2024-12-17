using backend_solar.Models;
using backend_solar.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace backend_solar.Services {
    public class LoginService {
        private readonly SensorContext _context;
        private readonly string jwtKey;

        public LoginService(SensorContext context, IConfiguration configuration) {
            _context = context;
            jwtKey = configuration["JwtSettings:Key"]!;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO request) {
            try {
                var user = await _context.Users.Where(u => u.Email == request.Email).FirstAsync();
                if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.Password)) {
                    throw new AuthenticationException();
                }
                var response = new LoginResponseDTO { AccessToken = GenerateAccessToken(user), UserId = user.Id };
                return response;
            }
            catch (Exception) {
                throw new AuthenticationException("Invalid email or password!");
            }
        }

        private string GenerateAccessToken(User user) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwtKey);

            var claims = new List<Claim> {
                new("isAdmin", user.IsAdmin.ToString().ToLower()),
                new(JwtRegisteredClaimNames.Email, user.Email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Issuer = "id.teste.com",
                Audience = "solar.teste.com",
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var jwt = tokenHandler.WriteToken(token);
            return jwt;
        }
    }
}
