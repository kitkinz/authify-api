namespace AuthifyAPI.DTOs;

public class ErrorResponse
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public int StatusCode { get; set; }
    public string? Details { get; set; }
}