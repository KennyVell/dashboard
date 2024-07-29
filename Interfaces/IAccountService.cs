using dashboard.DTOs;
using dashboard.Models;

namespace dashboard.Interfaces
{
    public interface IAccountService
    {
        Task<User> Register(UserDTO user);
        Task<User> Login(LoginDTO user);
        Task<User> GoogleLoginAsync(string email, string? name);
    }
}
