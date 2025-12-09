using AuthifyAPI.DTOs;

namespace AuthifyAPI.Services.Interfaces;

public interface IAuthifyService
{
    Task<ServiceResult<RegisteredUser>> RegisterUser(RegisterDto registerDto);
    Task<ServiceResult<LoginResponseDto>> LoginUser(LoginDto loginDto);
}