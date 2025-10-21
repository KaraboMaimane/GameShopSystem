using System.Diagnostics;

namespace GameShop.Catalog.API.Middleware;

/// <summary>
/// Middleware to log HTTP requests and responses
/// </summary>
public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the RequestResponseLoggingMiddleware
    /// </summary>
    public RequestResponseLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the middleware
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        // Start timing the request
        var stopwatch = Stopwatch.StartNew();

        // Log the request
        LogRequest(context.Request);

        // Continue down the middleware pipeline
        await _next(context);

        // Stop timing
        stopwatch.Stop();

        // Log the response
        LogResponse(context.Response, stopwatch.ElapsedMilliseconds);
    }

    private void LogRequest(HttpRequest request)
    {
        _logger.LogInformation(
            "HTTP {RequestMethod} {RequestPath} received from {RemoteIpAddress}",
            request.Method,
            request.Path,
            request.HttpContext.Connection.RemoteIpAddress);
    }

    private void LogResponse(HttpResponse response, long elapsedMs)
    {
        _logger.LogInformation(
            "HTTP {StatusCode} returned in {ElapsedMilliseconds}ms",
            response.StatusCode,
            elapsedMs);
    }
}