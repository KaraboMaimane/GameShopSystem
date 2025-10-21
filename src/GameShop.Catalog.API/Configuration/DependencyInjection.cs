using GameShop.Catalog.API.Components.Implementations;
using GameShop.Catalog.API.Components.Interfaces;
using GameShop.Catalog.API.Data;
using GameShop.Catalog.API.Repositories.Implementations;
using GameShop.Catalog.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Catalog.API.Configuration;

/// <summary>
/// Contains extension methods for configuring services in the application
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Configures database and related services
    /// </summary>
    /// <param name="services">The service collection to add services to</param>
    /// <param name="configuration">The configuration containing connection strings</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddDatabaseServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<CatalogDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                }
            ));

        // Register repositories
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();

        // Register business logic components
        services.AddScoped<IGameManager, GameManager>();
        services.AddScoped<IGenreManager, GenreManager>();

        return services;
    }
}