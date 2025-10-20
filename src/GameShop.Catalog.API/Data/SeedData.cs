using GameShop.Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Catalog.API.Data;

/// <summary>
/// Provides seed data for the catalog database
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data if it's empty
    /// </summary>
    /// <param name="context">The database context to seed</param>
    public static async Task InitializeAsync(CatalogDbContext context)
    {
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Only seed if there are no genres
        if (!await context.Genres.AnyAsync())
        {
            // Add genres
            var genres = new List<Genre>
            {
                new Genre { Name = "Action", Description = "Fast-paced games focusing on combat and movement" },
                new Genre { Name = "RPG", Description = "Games where players assume the roles of characters in a fictional setting" },
                new Genre { Name = "Strategy", Description = "Games that emphasize tactical thinking and planning" },
                new Genre { Name = "Adventure", Description = "Story-driven games with exploration and puzzle-solving" },
                new Genre { Name = "Sports", Description = "Games that simulate traditional physical sports" }
            };

            await context.Genres.AddRangeAsync(genres);
            await context.SaveChangesAsync();

            // Add sample games
            var games = new List<Game>
            {
                new Game
                {
                    Title = "Epic Quest",
                    Description = "An exciting journey through mystical lands",
                    Price = 59.99m,
                    Publisher = "GameCo",
                    ReleaseDate = DateTime.Now.AddMonths(-2),
                    GenreId = genres[1].Id // RPG
                },
                new Game
                {
                    Title = "Strategy Master",
                    Description = "Build your empire and conquer the world",
                    Price = 49.99m,
                    Publisher = "StrategyGames Inc",
                    ReleaseDate = DateTime.Now.AddMonths(-1),
                    GenreId = genres[2].Id // Strategy
                }
            };

            await context.Games.AddRangeAsync(games);
            await context.SaveChangesAsync();
        }
    }
}