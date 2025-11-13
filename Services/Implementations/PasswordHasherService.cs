using AuthifyAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthifyAPI.Services.Implementations;

public class PasswordHasherService : IPasswordHasherService
{
    private readonly PasswordHasher<object> _passwordHasher = new();

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(null!, password);
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(null!, providedPassword, hashedPassword);
        return result == PasswordVerificationResult.Success;
    }
} 