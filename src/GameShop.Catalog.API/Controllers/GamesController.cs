using GameShop.Catalog.API.Components.Interfaces;
using GameShop.Catalog.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Catalog.API.Controllers;

/// <summary>
/// Controller for managing games
/// </summary>
[Produces("application/json")]
public class GamesController : BaseApiController
{
    private readonly IGameManager _gameManager;
    private readonly ILogger<GamesController> _logger;

    /// <summary>
    /// Initializes a new instance of the GamesController
    /// </summary>
    /// <param name="gameManager">The game manager service</param>
    /// <param name="logger">The logger service</param>
    public GamesController(IGameManager gameManager, ILogger<GamesController> logger)
    {
        _gameManager = gameManager;
        _logger = logger;
    }

    /// <summary>
    /// Gets all games
    /// </summary>
    /// <returns>A list of all games</returns>
    /// <response code="200">Returns the list of games</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GameDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetAllGames()
    {
        try
        {
            var games = await _gameManager.GetAllGamesAsync();
            return Ok(games);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all games");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving games");
        }
    }

    /// <summary>
    /// Gets a specific game by id
    /// </summary>
    /// <param name="id">The ID of the game to retrieve</param>
    /// <returns>The requested game</returns>
    /// <response code="200">Returns the requested game</response>
    /// <response code="404">If the game is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GameDto>> GetGame(int id)
    {
        try
        {
            var game = await _gameManager.GetGameByIdAsync(id);
            if (game == null)
                return NotFound();

            return Ok(game);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving game with ID: {GameId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving game");
        }
    }

    /// <summary>
    /// Gets all games in a specific genre
    /// </summary>
    /// <param name="genreId">The ID of the genre to filter by</param>
    /// <returns>A list of games in the specified genre</returns>
    /// <response code="200">Returns the list of games in the genre</response>
    [HttpGet("bygenre/{genreId}")]
    [ProducesResponseType(typeof(IEnumerable<GameDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesByGenre(int genreId)
    {
        try
        {
            var games = await _gameManager.GetGamesByGenreAsync(genreId);
            return Ok(games);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving games for genre ID: {GenreId}", genreId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving games");
        }
    }

    /// <summary>
    /// Creates a new game
    /// </summary>
    /// <param name="createGameDto">The game information</param>
    /// <returns>The created game</returns>
    /// <response code="201">Returns the newly created game</response>
    /// <response code="400">If the game information is invalid</response>
    [HttpPost]
    [ProducesResponseType(typeof(GameDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GameDto>> CreateGame(CreateGameDto createGameDto)
    {
        try
        {
            var game = await _gameManager.CreateGameAsync(createGameDto);
            return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Invalid operation while creating game");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating game");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating game");
        }
    }

    /// <summary>
    /// Updates a specific game
    /// </summary>
    /// <param name="id">The ID of the game to update</param>
    /// <param name="updateGameDto">The updated game information</param>
    /// <returns>No content</returns>
    /// <response code="204">If the game was successfully updated</response>
    /// <response code="404">If the game was not found</response>
    /// <response code="400">If the game information is invalid</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateGame(int id, UpdateGameDto updateGameDto)
    {
        try
        {
            var success = await _gameManager.UpdateGameAsync(id, updateGameDto);
            if (!success)
                return NotFound();

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Invalid operation while updating game with ID: {GameId}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating game with ID: {GameId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error updating game");
        }
    }

    /// <summary>
    /// Deletes a specific game
    /// </summary>
    /// <param name="id">The ID of the game to delete</param>
    /// <returns>No content</returns>
    /// <response code="204">If the game was successfully deleted</response>
    /// <response code="404">If the game was not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGame(int id)
    {
        try
        {
            var success = await _gameManager.DeleteGameAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting game with ID: {GameId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting game");
        }
    }
}