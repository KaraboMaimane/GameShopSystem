# Game Shop Catalog Service

## Project Overview
This is part of a distributed Game Shop system, specifically the Catalog Microservice. The service manages game and genre information, following clean architecture principles and SOLID design patterns.

## Technology Stack
- .NET 9.0
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI

## Project Structure
```
GameShop.sln
├── src/
│   └── GameShop.Catalog.API/    # Main API Project
│       ├── Models/              # Domain Entities
│       ├── DTOs/                # Data Transfer Objects
│       ├── Repositories/        # Data Access Layer
│       ├── Components/          # Business Logic Layer
│       ├── Controllers/         # API Endpoints
│       ├── Configuration/       # App Configuration
│       └── Data/               # Database Context
└── tests/
    └── GameShop.Catalog.Tests/  # Test Project
```

## Implementation Steps (In Order)

### 1. Domain Models (Models/)
First, we created the core domain entities that represent our business objects:

#### `Genre.cs`
```csharp
public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<Game> Games { get; set; }
}
```

#### `Game.cs`
```csharp
public class Game
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Publisher { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int GenreId { get; set; }
    public virtual Genre? Genre { get; set; }
}
```

**Why?** Models are the foundation of our domain and represent the core business entities.

### 2. Data Transfer Objects (DTOs/)
We created DTOs to:
- Separate internal domain models from external API contracts
- Control what data gets exposed to clients
- Handle different create/update scenarios

Three types of DTOs for each entity:
1. **Read DTO** (GameDto/GenreDto) - For returning data
2. **Create DTO** - For creating new entities
3. **Update DTO** - For updating existing entities

**Why?** DTOs provide a clean separation between our internal domain models and what we expose to clients.

### 3. Database Context (Data/)
Set up Entity Framework Core with:

#### `CatalogDbContext.cs`
- Inherits from `DbContext`
- Configures entity relationships
- Sets up database constraints
- Handles data validations

#### `SeedData.cs`
- Provides initial data for development
- Creates sample genres and games
- Maintains referential integrity

**Why?** The database context provides a clean abstraction over our database operations and ensures data integrity.

### 4. Repository Layer (Repositories/)
Implemented the repository pattern with:

#### Interfaces (`Repositories/Interfaces/`)
- `IGameRepository.cs`
- `IGenreRepository.cs`

Define contracts for:
- CRUD operations
- Async methods
- Specific query methods

#### Implementations (`Repositories/Implementations/`)
- `GameRepository.cs`
- `GenreRepository.cs`

Implement:
- Entity Framework Core operations
- Proper relationship loading
- Error handling
- Concurrency management

**Why?** Repositories provide a clean abstraction over data access and make our code more testable and maintainable.

### 5. Configuration (Configuration/)
Set up dependency injection and configuration in:

#### `DependencyInjection.cs`
```csharp
public static class DependencyInjection
{
    public static IServiceCollection AddDatabaseServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Database configuration
        // Repository registration
    }
}
```

#### `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=GameShopCatalog;..."
  }
}
```

**Why?** Proper configuration management ensures our application is configurable and follows dependency injection principles.

### 6. Business Logic Layer (Components/)
Implemented business logic managers to:
- Handle data transformation between DTOs and domain models
- Implement business rules and validation
- Coordinate between different repositories
- Provide a clean API for controllers

#### Interfaces (`Components/Interfaces/`)
- `IGameManager.cs`
- `IGenreManager.cs`

Define business operations:
- CRUD operations using DTOs
- Additional business rules
- Input validation
- Coordination between entities

#### Implementations (`Components/Implementations/`)
- `GameManager.cs`
- `GenreManager.cs`

Implement:
- Business logic and rules
- DTO mappings
- Cross-entity operations
- Error handling and validation

**Why?** The business logic layer:
- Separates business rules from data access
- Handles DTO transformations
- Provides a clean API for controllers
- Ensures business rule consistency

### 7. API Controllers (Controllers/)
Implemented REST API endpoints with:

#### `GenresController.cs`
- GET /api/genres - Get all genres
- GET /api/genres/{id} - Get genre by ID
- POST /api/genres - Create new genre
- PUT /api/genres/{id} - Update existing genre
- DELETE /api/genres/{id} - Delete genre

#### `GamesController.cs`
- GET /api/games - Get all games
- GET /api/games/{id} - Get game by ID
- GET /api/games/bygenre/{genreId} - Get games by genre
- POST /api/games - Create new game
- PUT /api/games/{id} - Update existing game
- DELETE /api/games/{id} - Delete game

Features implemented in controllers:
- Full OpenAPI/Swagger documentation
- Proper HTTP status codes
- Error handling and logging
- Input validation
- Action result types
- Response type documentation

**Why?** Controllers:
- Provide REST API endpoints
- Handle HTTP requests/responses
- Manage status codes
- Document API behavior
- Handle errors appropriately

## What's Next?

### 2. API Controllers
- Implement REST endpoints
- Handle HTTP requests
- Manage response codes
- Document API with Swagger

### 3. Automated Testing
- Unit tests for business logic
- Integration tests for repositories
- API endpoint tests

### 4. Advanced Features
- API Versioning
- Caching
- Logging
- Error handling middleware
- Health checks

### 8. Infrastructure Features

#### API Versioning
API versioning is crucial for maintaining backward compatibility while allowing the API to evolve. We've implemented a flexible versioning strategy that supports:

```csharp
// Version 1 API endpoint
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/games")]
public class GamesController : BaseApiController
```

Versioning can be specified in three ways:
1. URL Path: `/api/v1/games`
2. Query String: `/api/games?api-version=1.0`
3. Header: `api-version: 1.0`

**Why Version APIs?**
- Allows making breaking changes without affecting existing clients
- Enables gradual migration of clients to newer versions
- Provides time to deprecate old functionality

**Educational Note:**
Think of API versioning like phone models (iPhone 13, 14, 15). Each new version can add features or change how things work, but older versions still need to work for existing users.

#### Global Error Handlingthe (Middleware/ErrorHandlingMiddleware.cs)
- Centralizes error handling
- Provides consistent error responses
- Includes detailed errors in development
- Maps exceptions to appropriate HTTP status codes

**Why?** Ensures consistent error handling and prevents sensitive error details from reaching clients in production.

#### Request/Response Logging (Middleware/RequestResponseLoggingMiddleware.cs)
- Logs incoming HTTP requests
- Records response times
- Tracks response status codes
- Helps with monitoring and debugging

**Why?** Provides visibility into API usage and performance.

#### Health Checks (HealthChecks/DatabaseHealthCheck.cs)
- Monitors database connectivity
- Provides health status endpoint
- Returns detailed health information
- Supports monitoring tools

**Why?** Enables monitoring tools to track application health and availability.

## Quick Start Guide

### Prerequisites
1. .NET 9.0 SDK
2. SQL Server
3. Visual Studio/VS Code

### Useful .NET CLI Commands

#### Project Creation
```bash
# Create solution
dotnet new sln -n GameShop

# Create Web API project
dotnet new webapi -n GameShop.Catalog.API

# Create xUnit test project
dotnet new xunit -n GameShop.Catalog.Tests

# Add projects to solution
dotnet sln add src/GameShop.Catalog.API/GameShop.Catalog.API.csproj
dotnet sln add tests/GameShop.Catalog.Tests/GameShop.Catalog.Tests.csproj
```

#### Package Management
```bash
# Add Entity Framework Core packages
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design

# Add Swagger documentation
dotnet add package Swashbuckle.AspNetCore

# Tool installation (if needed)
dotnet tool install --global dotnet-ef
```

#### Database Operations
```bash
# Create initial migration
dotnet ef migrations add InitialCreate

# Apply migrations
dotnet ef database update

# Remove last migration
dotnet ef migrations remove

# Generate SQL script
dotnet ef migrations script
```

#### Development
```bash
# Run the application
dotnet run

# Watch for changes
dotnet watch run

# Build solution
dotnet build

# Run tests
dotnet test
```

### Setup Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/YourUsername/GameShop.git
   cd GameShop
   ```

2. Update connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=GameShopCatalog;..."
     }
   }
   ```

3. Apply database migrations:
   ```bash
   cd src/GameShop.Catalog.API
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Access the API:
   - Swagger UI: https://localhost:5001/swagger
   - Health Check: https://localhost:5001/health

### Understanding API Design Concepts

### API Versioning Explained
When building APIs, changes are inevitable. API versioning helps manage these changes without breaking existing applications. Here's how:

1. **Types of Changes:**
   - **Non-Breaking Changes:** Adding new optional fields or endpoints
   - **Breaking Changes:** Removing/renaming fields, changing data types

2. **Version Numbers Explained:**
   ```
   v1.2.3
   │ │ │
   │ │ └─ Patch (Bug fixes)
   │ └─── Minor (New features, backward compatible)
   └───── Major (Breaking changes)
   ```

3. **Best Practices:**
   - Start with v1
   - Use whole numbers for major versions
   - Keep old versions running until clients migrate
   - Document changes between versions

### Request/Response Logging Explained
Logging helps track what's happening in your API:

```plaintext
Request Log Example:
[INFO] HTTP GET /api/v1/games received from 192.168.1.1
        │    │   └─ Path    └─ Client IP
        │    └─ Method
        └─ Log Level

Response Log Example:
[INFO] HTTP 200 returned in 125ms
        │   │   └─ Response Time
        │   └─ Status Code
        └─ Log Level
```

### Health Checks Explained
Health checks help monitor your API's status:

```json
{
  "status": "Healthy",
  "duration": "125ms",
  "checks": [
    {
      "name": "database_health_check",
      "status": "Healthy",
      "duration": "100ms"
    }
  ]
}
```

## Key Endpoints
- Games API: `/api/games`
- Genres API: `/api/genres`
- Health Check: `/health`

## Development Principles Applied
1. **SOLID Principles**
   - Single Responsibility (Each class has one job)
   - Open/Closed (Extensible through interfaces)
   - Liskov Substitution (Implementations are interchangeable)
   - Interface Segregation (Focused interfaces)
   - Dependency Inversion (Depend on abstractions)

2. **Clean Architecture**
   - Separation of concerns
   - Independence of frameworks
   - Testability
   - Maintainability

3. **Best Practices**
   - Async/await throughout
   - Proper error handling
   - XML documentation
   - Consistent naming conventions

## Contributing
[To be added]

## License
[To be added]