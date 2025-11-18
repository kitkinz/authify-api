using System.ComponentModel.DataAnnotations;
using AuthifyAPI.Validations;

namespace AuthifyAPI.DTOs;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StrongPassword]
    public string Password { get; set; }
}