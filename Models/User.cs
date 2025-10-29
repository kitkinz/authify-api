using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthifyAPI.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [JsonIgnore]
    public string PasswordHash { get; set; }
    
    public string Role { get; set; } = "User";
}