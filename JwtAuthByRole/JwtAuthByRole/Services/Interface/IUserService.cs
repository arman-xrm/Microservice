using JwtAuthByRole.Data.Entity;

namespace JwtAuthByRole.Services.Interface
{
    public interface IUserService
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<User> GetUserById(int id);
    }
}
