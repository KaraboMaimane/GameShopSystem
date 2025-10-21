namespace GameShop.Catalog.API.Middleware;

/// <summary>
/// Represents an API error response
/// </summary>
public class ApiError
{
    /// <summary>
    /// Gets or sets the error message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the detailed error information (only included in development)
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// Gets or sets the error code
    /// </summary>
    public string Code { get; set; } = string.Empty;
}