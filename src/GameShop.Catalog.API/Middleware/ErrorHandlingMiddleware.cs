using System.Net;
using System.Text.Json;

namespace GameShop.Catalog.API.Middleware;

/// <summary>
/// Middleware to handle exceptions globally across the application
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    /// <summary>
    /// Initializes a new instance of the ErrorHandlingMiddleware
    /// </summary>
    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger,
        IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    /// <summary>
    /// Invokes the middleware
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unhandled exception has occurred");

        var statusCode = GetStatusCode(exception);
        var response = new ApiError
        {
            Message = GetUserMessage(exception),
            Code = statusCode.ToString(),
            // Only include exception details in development
            Details = _env.IsDevelopment() ? exception.ToString() : null
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }

    private static HttpStatusCode GetStatusCode(Exception exception)
    {
        // Map common exceptions to appropriate HTTP status codes
        return exception switch
        {
            InvalidOperationException => HttpStatusCode.BadRequest,
            KeyNotFoundException => HttpStatusCode.NotFound,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            // Add more exception mappings as needed
            _ => HttpStatusCode.InternalServerError
        };
    }

    private static string GetUserMessage(Exception exception)
    {
        // Provide user-friendly messages based on exception type
        return exception switch
        {
            InvalidOperationException => exception.Message, // Business logic errors are safe to show
            KeyNotFoundException => "The requested resource was not found",
            UnauthorizedAccessException => "You are not authorized to perform this action",
            _ => "An unexpected error occurred. Please try again later."
        };
    }
}