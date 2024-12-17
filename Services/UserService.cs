using backend_solar.Models;
using backend_solar.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend_solar.Services {
    public class UserService {
        private readonly SensorContext _context;

        public UserService(SensorContext context) {
            _context = context;
        }

        public async Task<User> CreateUser(User user) {
            user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> FindUser(Guid userId) {
            var user = await _context.Users.AsNoTracking().Where(u => u.Id == userId).Include(u => u.UserPlaces).AsNoTracking().FirstAsync();
            return user;
        }
    }
}
