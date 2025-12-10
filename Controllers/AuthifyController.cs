using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using AuthifyAPI.DTOs;
using AuthifyAPI.Constants;
using AuthifyAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AuthifyAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthifyController : ControllerBase
{
    private readonly IAuthifyService _authifyService;

    public AuthifyController(IAuthifyService authifyService)
    {
        _authifyService = authifyService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterDto registerRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authifyService.RegisterUser(registerRequest);

        if (result.Success)
        {
            return Ok(result.Data);
        }

        if (result.ErrorMessage == ErrorMessages.EMAIL_ALREADY_EXISTS)
        {
            return Conflict(result.ErrorMessage);
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginDto loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authifyService.LoginUser(loginRequest);

        if (!result.Success)
        {
            return Unauthorized(result.ErrorMessage);
        }

        return Ok(result.Data);
    }

    [Authorize]
    [HttpGet("protected")]
    public IActionResult GetProtectedData()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;

        return Ok(new
        {
            Message = "You have access to protected data!",
            UserId = userId,
            Email = email,
            Role = role
        });
    }
}