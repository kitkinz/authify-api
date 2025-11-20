using AuthifyAPI.Data;
using AuthifyAPI.Models;
using AuthifyAPI.DTOs;
using AuthifyAPI.Services.Interfaces;
using AuthifyAPI.Repositories;
using AuthifyAPI.Constants;

namespace AuthifyAPI.Services;

public class AuthifyService : IAuthifyService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasher;

    public AuthifyService(IPasswordHasherService passwordHasher, IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<ServiceResult<RegisteredUser>> RegisterUser(RegisterDto registerDto)
    {
        if (await _userRepository.EmailExistsAsync(registerDto.Email))
        {
            return ServiceResult<RegisteredUser>.FailureResult(ErrorMessages.EMAIL_ALREADY_EXISTS);
        }

        var user = await _userRepository.CreateUserAsync(new User
        {
            Id = Guid.NewGuid(),
            Email = registerDto.Email,
            PasswordHash = _passwordHasher.HashPassword(registerDto.Password),
            // user for now
            Role = "User"
        });

        var registeredUser = new RegisteredUser
        {
            Email = user.Email,
            Role = user.Role,
        };
        
        return ServiceResult<RegisteredUser>.SuccessResult(registeredUser);
    }
}