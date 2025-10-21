using GameShop.Catalog.API.Models;

namespace GameShop.Catalog.API.Repositories.Interfaces;

/// <summary>
/// Repository interface for Game entity operations
/// </summary>
public interface IGameRepository
{
    /// <summary>
    /// Gets all games asynchronously
    /// </summary>
    /// <returns>A collection of all games</returns>
    Task<IEnumerable<Game>> GetAllAsync();

    /// <summary>
    /// Gets a game by its ID asynchronously
    /// </summary>
    /// <param name="id">The ID of the game to retrieve</param>
    /// <returns>The game if found, null otherwise</returns>
    Task<Game?> GetByIdAsync(int id);

    /// <summary>
    /// Gets games by genre ID asynchronously
    /// </summary>
    /// <param name="genreId">The ID of the genre to filter by</param>
    /// <returns>A collection of games in the specified genre</returns>
    Task<IEnumerable<Game>> GetByGenreAsync(int genreId);

    /// <summary>
    /// Creates a new game asynchronously
    /// </summary>
    /// <param name="game">The game entity to create</param>
    /// <returns>The created game with its assigned ID</returns>
    Task<Game> CreateAsync(Game game);

    /// <summary>
    /// Updates an existing game asynchronously
    /// </summary>
    /// <param name="game">The game entity with updated values</param>
    /// <returns>True if the update was successful, false otherwise</returns>
    Task<bool> UpdateAsync(Game game);

    /// <summary>
    /// Deletes a game by its ID asynchronously
    /// </summary>
    /// <param name="id">The ID of the game to delete</param>
    /// <returns>True if the deletion was successful, false otherwise</returns>
    Task<bool> DeleteAsync(int id);
}