namespace AuthifyAPI.Services.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(Guid userId, string email, string role);
}