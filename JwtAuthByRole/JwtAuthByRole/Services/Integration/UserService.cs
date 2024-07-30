using JwtAuthByRole.Data.Entity;
using JwtAuthByRole.Data;
using JwtAuthByRole.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthByRole.Services.Integration
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<User> Register(User user, string password)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }
            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
    }
}
