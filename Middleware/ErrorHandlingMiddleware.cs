using AuthifyAPI.DTOs;

namespace AuthifyAPI.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger,
        IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _env = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Success = false,
                ErrorMessage = "An error occurred while processing your request",
                StatusCode = 500,
                Details = _env.IsDevelopment() ? ex.ToString() : null
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}