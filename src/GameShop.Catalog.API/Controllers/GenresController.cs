using GameShop.Catalog.API.Components.Interfaces;
using GameShop.Catalog.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Catalog.API.Controllers;

/// <summary>
/// Controller for managing game genres
/// </summary>
[Produces("application/json")]
public class GenresController : BaseApiController
{
    private readonly IGenreManager _genreManager;
    private readonly ILogger<GenresController> _logger;

    /// <summary>
    /// Initializes a new instance of the GenresController
    /// </summary>
    /// <param name="genreManager">The genre manager service</param>
    /// <param name="logger">The logger service</param>
    public GenresController(IGenreManager genreManager, ILogger<GenresController> logger)
    {
        _genreManager = genreManager;
        _logger = logger;
    }

    /// <summary>
    /// Gets all genres
    /// </summary>
    /// <returns>A list of all genres</returns>
    /// <response code="200">Returns the list of genres</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GenreDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GenreDto>>> GetAllGenres()
    {
        try
        {
            var genres = await _genreManager.GetAllGenresAsync();
            return Ok(genres);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all genres");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving genres");
        }
    }

    /// <summary>
    /// Gets a specific genre by id
    /// </summary>
    /// <param name="id">The ID of the genre to retrieve</param>
    /// <returns>The requested genre</returns>
    /// <response code="200">Returns the requested genre</response>
    /// <response code="404">If the genre is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenreDto>> GetGenre(int id)
    {
        try
        {
            var genre = await _genreManager.GetGenreByIdAsync(id);
            if (genre == null)
                return NotFound();

            return Ok(genre);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving genre with ID: {GenreId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving genre");
        }
    }

    /// <summary>
    /// Creates a new genre
    /// </summary>
    /// <param name="createGenreDto">The genre information</param>
    /// <returns>The created genre</returns>
    /// <response code="201">Returns the newly created genre</response>
    /// <response code="400">If the genre information is invalid</response>
    [HttpPost]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GenreDto>> CreateGenre(CreateGenreDto createGenreDto)
    {
        try
        {
            var genre = await _genreManager.CreateGenreAsync(createGenreDto);
            return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating genre");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating genre");
        }
    }

    /// <summary>
    /// Updates a specific genre
    /// </summary>
    /// <param name="id">The ID of the genre to update</param>
    /// <param name="updateGenreDto">The updated genre information</param>
    /// <returns>No content</returns>
    /// <response code="204">If the genre was successfully updated</response>
    /// <response code="404">If the genre was not found</response>
    /// <response code="400">If the genre information is invalid</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateGenre(int id, UpdateGenreDto updateGenreDto)
    {
        try
        {
            var success = await _genreManager.UpdateGenreAsync(id, updateGenreDto);
            if (!success)
                return NotFound();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating genre with ID: {GenreId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error updating genre");
        }
    }

    /// <summary>
    /// Deletes a specific genre
    /// </summary>
    /// <param name="id">The ID of the genre to delete</param>
    /// <returns>No content</returns>
    /// <response code="204">If the genre was successfully deleted</response>
    /// <response code="404">If the genre was not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        try
        {
            var success = await _genreManager.DeleteGenreAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting genre with ID: {GenreId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting genre");
        }
    }
}