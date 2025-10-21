using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace GameShop.Catalog.API.Configuration;

/// <summary>
/// Contains extension methods for configuring API versioning
/// 
/// What is API Versioning?
/// ----------------------
/// API versioning allows us to make changes to our API without breaking existing clients.
/// When we make breaking changes, we can create a new version while maintaining the old one.
/// 
/// Key Concepts:
/// 1. Version Number: Identifies different API versions (e.g., v1, v2)
/// 2. Version Strategy: How we specify the version (URL, header, or query string)
/// 3. Version Reader: Component that reads version information from the request
/// 
/// Example Usage:
/// - URL: /api/v1/games
/// - Header: api-version: 1.0
/// - Query: /api/games?api-version=1.0
/// </summary>
public static class ApiVersioningSetup
{
    /// <summary>
    /// Configures API versioning for the application
    /// </summary>
    /// <param name="services">The service collection to configure</param>
    /// <returns>The configured service collection</returns>
    public static IServiceCollection AddApiVersioningSetup(this IServiceCollection services)
    {
        // Add API versioning services to the container
        services.AddApiVersioning(options =>
        {
            // Specify the default API version
            // This version will be used when client doesn't specify a version
            options.DefaultApiVersion = new ApiVersion(1, 0);
            
            // Assume the default version when client doesn't specify one
            // If false, client must always specify a version
            options.AssumeDefaultVersionWhenUnspecified = true;
            
            // Include API version in response headers
            // Helps clients understand API version status
            options.ReportApiVersions = true;

            // Configure version reader
            // This setup allows reading version from:
            // 1. URL path segment (/api/v1/games)
            // 2. Query string (?api-version=1.0)
            // 3. Header (api-version: 1.0)
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("api-version")
            );
        });

        // Add API explorer for versioning
        // This enables Swagger to show different API versions
        services.AddVersionedApiExplorer(options =>
        {
            // Format version as 'v1.0'
            options.GroupNameFormat = "'v'VVV";
            
            // Substitute version into URL path
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}