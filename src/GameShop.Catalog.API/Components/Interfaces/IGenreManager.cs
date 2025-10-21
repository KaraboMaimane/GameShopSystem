using GameShop.Catalog.API.DTOs;

namespace GameShop.Catalog.API.Components.Interfaces;

/// <summary>
/// Business logic interface for genre operations
/// </summary>
public interface IGenreManager
{
    /// <summary>
    /// Gets all genres
    /// </summary>
    /// <returns>Collection of genre DTOs</returns>
    Task<IEnumerable<GenreDto>> GetAllGenresAsync();

    /// <summary>
    /// Gets a genre by its ID
    /// </summary>
    /// <param name="id">The genre ID</param>
    /// <returns>Genre DTO if found, null otherwise</returns>
    Task<GenreDto?> GetGenreByIdAsync(int id);

    /// <summary>
    /// Creates a new genre
    /// </summary>
    /// <param name="createGenreDto">The genre creation data</param>
    /// <returns>The created genre DTO</returns>
    Task<GenreDto> CreateGenreAsync(CreateGenreDto createGenreDto);

    /// <summary>
    /// Updates an existing genre
    /// </summary>
    /// <param name="id">The ID of the genre to update</param>
    /// <param name="updateGenreDto">The genre update data</param>
    /// <returns>True if update was successful, false otherwise</returns>
    Task<bool> UpdateGenreAsync(int id, UpdateGenreDto updateGenreDto);

    /// <summary>
    /// Deletes a genre
    /// </summary>
    /// <param name="id">The ID of the genre to delete</param>
    /// <returns>True if deletion was successful, false otherwise</returns>
    Task<bool> DeleteGenreAsync(int id);
}