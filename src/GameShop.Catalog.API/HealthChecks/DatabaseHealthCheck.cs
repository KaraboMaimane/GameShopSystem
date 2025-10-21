using Microsoft.Extensions.Diagnostics.HealthChecks;
using GameShop.Catalog.API.Data;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Catalog.API.HealthChecks;

/// <summary>
/// Health check for the database
/// </summary>
public class DatabaseHealthCheck : IHealthCheck
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<DatabaseHealthCheck> _logger;

    /// <summary>
    /// Initializes a new instance of the DatabaseHealthCheck
    /// </summary>
    public DatabaseHealthCheck(CatalogDbContext context, ILogger<DatabaseHealthCheck> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Checks database health by attempting to connect
    /// </summary>
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            // Attempt to connect to the database
            await _context.Database.CanConnectAsync(cancellationToken);
            
            return HealthCheckResult.Healthy("Database is healthy");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database health check failed");
            return HealthCheckResult.Unhealthy("Database is unhealthy", ex);
        }
    }
}