using AuthifyAPI.Models;

namespace AuthifyAPI.Repositories;

public interface IUserRepository
{
    Task<bool> EmailExistsAsync(string email);
    Task<User> CreateUserAsync(User user);
    Task<User?> GetUserByEmailAsync(string email);
}