using GameShop.Catalog.API.Models;

namespace GameShop.Catalog.API.Repositories.Interfaces;

/// <summary>
/// Repository interface for Genre entity operations
/// </summary>
public interface IGenreRepository
{
    /// <summary>
    /// Gets all genres asynchronously
    /// </summary>
    /// <returns>A collection of all genres</returns>
    Task<IEnumerable<Genre>> GetAllAsync();

    /// <summary>
    /// Gets a genre by its ID asynchronously
    /// </summary>
    /// <param name="id">The ID of the genre to retrieve</param>
    /// <returns>The genre if found, null otherwise</returns>
    Task<Genre?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new genre asynchronously
    /// </summary>
    /// <param name="genre">The genre entity to create</param>
    /// <returns>The created genre with its assigned ID</returns>
    Task<Genre> CreateAsync(Genre genre);

    /// <summary>
    /// Updates an existing genre asynchronously
    /// </summary>
    /// <param name="genre">The genre entity with updated values</param>
    /// <returns>True if the update was successful, false otherwise</returns>
    Task<bool> UpdateAsync(Genre genre);

    /// <summary>
    /// Deletes a genre by its ID asynchronously
    /// </summary>
    /// <param name="id">The ID of the genre to delete</param>
    /// <returns>True if the deletion was successful, false otherwise</returns>
    Task<bool> DeleteAsync(int id);
}