using GameShop.Catalog.API.Components.Interfaces;
using GameShop.Catalog.API.DTOs;
using GameShop.Catalog.API.Models;
using GameShop.Catalog.API.Repositories.Interfaces;

namespace GameShop.Catalog.API.Components.Implementations;

/// <summary>
/// Implementation of genre business logic
/// </summary>
public class GenreManager : IGenreManager
{
    private readonly IGenreRepository _genreRepository;

    /// <summary>
    /// Initializes a new instance of the GenreManager
    /// </summary>
    /// <param name="genreRepository">The genre repository</param>
    public GenreManager(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
    {
        var genres = await _genreRepository.GetAllAsync();
        return genres.Select(genre => MapToDto(genre));
    }

    /// <inheritdoc/>
    public async Task<GenreDto?> GetGenreByIdAsync(int id)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        return genre != null ? MapToDto(genre) : null;
    }

    /// <inheritdoc/>
    public async Task<GenreDto> CreateGenreAsync(CreateGenreDto createGenreDto)
    {
        var genre = new Genre
        {
            Name = createGenreDto.Name,
            Description = createGenreDto.Description
        };

        var createdGenre = await _genreRepository.CreateAsync(genre);
        return MapToDto(createdGenre);
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateGenreAsync(int id, UpdateGenreDto updateGenreDto)
    {
        var existingGenre = await _genreRepository.GetByIdAsync(id);
        if (existingGenre == null)
            return false;

        existingGenre.Name = updateGenreDto.Name;
        existingGenre.Description = updateGenreDto.Description;

        return await _genreRepository.UpdateAsync(existingGenre);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteGenreAsync(int id)
    {
        return await _genreRepository.DeleteAsync(id);
    }

    private static GenreDto MapToDto(Genre genre)
    {
        return new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description
        };
    }
}