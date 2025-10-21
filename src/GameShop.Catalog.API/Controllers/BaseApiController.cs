using Microsoft.AspNetCore.Mvc;

namespace GameShop.Catalog.API.Controllers;

/// <summary>
/// Base API controller with versioning support
/// 
/// Understanding API Controller Attributes:
/// -------------------------------------
/// [ApiController]: Enables API-specific behaviors:
///   - Automatic model validation
///   - Automatic HTTP 400 responses for invalid models
///   - Binding source parameter inference
/// 
/// [Route]: Defines the URL pattern for the controller
///   - "api/v{version:apiVersion}/[controller]" breaks down to:
///     * api/ - Standard API prefix
///     * v{version:apiVersion} - Version number (e.g., v1)
///     * [controller] - Controller name without "Controller" suffix
/// 
/// [ApiVersion("1.0")]: Specifies the API version this controller supports
///   - Multiple versions can be supported using multiple attributes
///   - Can mark versions as deprecated: [ApiVersion("1.0", Deprecated = true)]
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public abstract class BaseApiController : ControllerBase
{
    // Common controller functionality can be added here
    
    /// <summary>
    /// Creates an error response with consistent format
    /// </summary>
    /// <param name="message">The error message</param>
    /// <param name="code">The error code</param>
    /// <returns>Object result with error details</returns>
    protected IActionResult Error(string message, string code = "error")
    {
        return BadRequest(new
        {
            Error = new { Message = message, Code = code }
        });
    }
}