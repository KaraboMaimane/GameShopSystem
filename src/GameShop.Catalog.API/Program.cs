using GameShop.Catalog.API.Configuration;
using GameShop.Catalog.API.Data;
using GameShop.Catalog.API.HealthChecks;
using GameShop.Catalog.API.Middleware;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Use the default configuration from launchSettings.json
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenLocalhost(5001); // HTTP on localhost only for development
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add API versioning
builder.Services.AddApiVersioningSetup();

// Configure Swagger
builder.Services.AddSwaggerGen(options =>
{
    // Document both v1 and v2 APIs
    options.SwaggerDoc("v1", new() { 
        Title = "GameShop Catalog API V1", 
        Version = "v1",
        Description = @"First version of the Game Shop Catalog API.
                       
Key Features:
- Basic CRUD operations for Games and Genres
- Health monitoring endpoints
- Comprehensive error handling
- Request/Response logging"
    });
    
    // Include XML comments in Swagger documentation
    var xmlFile = "GameShop.Catalog.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    // Add security definitions and requirements here if needed
    // options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {...});
});

// Add health checks
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("database_health_check");

// Add database services
builder.Services.AddDatabaseServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add global error handling
app.UseMiddleware<ErrorHandlingMiddleware>();

// Add request/response logging
app.UseMiddleware<RequestResponseLoggingMiddleware>();

// Only redirect to HTTPS in production
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

// Configure health check endpoint with detailed response
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            Status = report.Status.ToString(),
            Duration = report.TotalDuration,
            Checks = report.Entries.Select(e => new
            {
                Name = e.Key,
                Status = e.Value.Status.ToString(),
                Duration = e.Value.Duration,
                Description = e.Value.Description
            })
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
});

// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CatalogDbContext>();
        await SeedData.InitializeAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();
