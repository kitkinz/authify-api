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
    private readonly IJwtService _jwtService;

    public AuthifyService(IPasswordHasherService passwordHasher, IUserRepository userRepository, IJwtService jwtService)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtService = jwtService;
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

    public async Task<ServiceResult<LoginResponseDto>> LoginUser(LoginDto loginDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

        if (user == null)
        {
            return ServiceResult<LoginResponseDto>.FailureResult(ErrorMessages.INVALID_CREDENTIALS);
        }

        if (!_passwordHasher.VerifyPassword(user.PasswordHash, loginDto.Password))
        {
            return ServiceResult<LoginResponseDto>.FailureResult(ErrorMessages.INVALID_CREDENTIALS);
        }

        var loggedInUser = new LoginResponseDto
        {
            Token = _jwtService.GenerateJwtToken(user.Id, user.Email, user.Role),
            User = new RegisteredUser()
            {
                Email = user.Email,
                Role = user.Role,
            }
        };

        return ServiceResult<LoginResponseDto>.SuccessResult(loggedInUser);
    }
}