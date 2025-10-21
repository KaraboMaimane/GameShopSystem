using GameShop.Catalog.API.DTOs;

namespace GameShop.Catalog.API.Components.Interfaces;

/// <summary>
/// Business logic interface for game operations
/// </summary>
public interface IGameManager
{
    /// <summary>
    /// Gets all games
    /// </summary>
    /// <returns>Collection of game DTOs</returns>
    Task<IEnumerable<GameDto>> GetAllGamesAsync();

    /// <summary>
    /// Gets a game by its ID
    /// </summary>
    /// <param name="id">The game ID</param>
    /// <returns>Game DTO if found, null otherwise</returns>
    Task<GameDto?> GetGameByIdAsync(int id);

    /// <summary>
    /// Gets all games in a specific genre
    /// </summary>
    /// <param name="genreId">The genre ID</param>
    /// <returns>Collection of game DTOs in the specified genre</returns>
    Task<IEnumerable<GameDto>> GetGamesByGenreAsync(int genreId);

    /// <summary>
    /// Creates a new game
    /// </summary>
    /// <param name="createGameDto">The game creation data</param>
    /// <returns>The created game DTO</returns>
    Task<GameDto> CreateGameAsync(CreateGameDto createGameDto);

    /// <summary>
    /// Updates an existing game
    /// </summary>
    /// <param name="id">The ID of the game to update</param>
    /// <param name="updateGameDto">The game update data</param>
    /// <returns>True if update was successful, false otherwise</returns>
    Task<bool> UpdateGameAsync(int id, UpdateGameDto updateGameDto);

    /// <summary>
    /// Deletes a game
    /// </summary>
    /// <param name="id">The ID of the game to delete</param>
    /// <returns>True if deletion was successful, false otherwise</returns>
    Task<bool> DeleteGameAsync(int id);
}