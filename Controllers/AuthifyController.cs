using Microsoft.AspNetCore.Mvc;
using AuthifyAPI.Models;
using AuthifyAPI.Services;

namespace AuthifyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthifyController : ControllerBase
{
    private readonly AuthifyService _authifyService;

    public AuthifyController(AuthifyService authifyService)
    {
        _authifyService = authifyService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterRequest registerRequest)
    {
        var result = await _authifyService.RegisterUser(registerRequest);
        return Ok(result);
    }
}