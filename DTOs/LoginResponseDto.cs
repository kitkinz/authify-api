using System.ComponentModel.DataAnnotations;

namespace AuthifyAPI.DTOs;

public class LoginResponseDto
{
    public string Token { get; set; }
    public RegisteredUser User { get; set; }
}