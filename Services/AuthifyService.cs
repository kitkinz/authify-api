using AuthifyAPI.Data;
using AuthifyAPI.Models;
using AuthifyAPI.Services.Interfaces;

namespace AuthifyAPI.Services;

public class AuthifyService
{
    private readonly AppDbContext _dbContext;
    private readonly IPasswordHasherService _passwordHasher;

    public AuthifyService(AppDbContext dbContext, IPasswordHasherService passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterRequest> RegisterUser(RegisterRequest registerRequest)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = registerRequest.Email,
            PasswordHash = _passwordHasher.HashPassword(registerRequest.Password),
            Role = "User"
            
        };
        
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return registerRequest;
    } 
}