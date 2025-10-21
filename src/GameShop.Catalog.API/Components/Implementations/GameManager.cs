using GameShop.Catalog.API.Components.Interfaces;
using GameShop.Catalog.API.DTOs;
using GameShop.Catalog.API.Models;
using GameShop.Catalog.API.Repositories.Interfaces;

namespace GameShop.Catalog.API.Components.Implementations;

/// <summary>
/// Implementation of game business logic
/// </summary>
public class GameManager : IGameManager
{
    private readonly IGameRepository _gameRepository;
    private readonly IGenreRepository _genreRepository;

    /// <summary>
    /// Initializes a new instance of the GameManager
    /// </summary>
    /// <param name="gameRepository">The game repository</param>
    /// <param name="genreRepository">The genre repository</param>
    public GameManager(IGameRepository gameRepository, IGenreRepository genreRepository)
    {
        _gameRepository = gameRepository;
        _genreRepository = genreRepository;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<GameDto>> GetAllGamesAsync()
    {
        var games = await _gameRepository.GetAllAsync();
        return games.Select(game => MapToDto(game));
    }

    /// <inheritdoc/>
    public async Task<GameDto?> GetGameByIdAsync(int id)
    {
        var game = await _gameRepository.GetByIdAsync(id);
        return game != null ? MapToDto(game) : null;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<GameDto>> GetGamesByGenreAsync(int genreId)
    {
        var games = await _gameRepository.GetByGenreAsync(genreId);
        return games.Select(game => MapToDto(game));
    }

    /// <inheritdoc/>
    public async Task<GameDto> CreateGameAsync(CreateGameDto createGameDto)
    {
        // Validate genre exists
        var genre = await _genreRepository.GetByIdAsync(createGameDto.GenreId);
        if (genre == null)
            throw new InvalidOperationException($"Genre with ID {createGameDto.GenreId} not found");

        var game = new Game
        {
            Title = createGameDto.Title,
            Description = createGameDto.Description,
            Price = createGameDto.Price,
            Publisher = createGameDto.Publisher,
            ReleaseDate = createGameDto.ReleaseDate,
            GenreId = createGameDto.GenreId
        };

        var createdGame = await _gameRepository.CreateAsync(game);
        return MapToDto(createdGame);
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateGameAsync(int id, UpdateGameDto updateGameDto)
    {
        // Validate game exists
        var existingGame = await _gameRepository.GetByIdAsync(id);
        if (existingGame == null)
            return false;

        // Validate genre exists
        var genre = await _genreRepository.GetByIdAsync(updateGameDto.GenreId);
        if (genre == null)
            throw new InvalidOperationException($"Genre with ID {updateGameDto.GenreId} not found");

        // Update properties
        existingGame.Title = updateGameDto.Title;
        existingGame.Description = updateGameDto.Description;
        existingGame.Price = updateGameDto.Price;
        existingGame.Publisher = updateGameDto.Publisher;
        existingGame.ReleaseDate = updateGameDto.ReleaseDate;
        existingGame.GenreId = updateGameDto.GenreId;

        return await _gameRepository.UpdateAsync(existingGame);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteGameAsync(int id)
    {
        return await _gameRepository.DeleteAsync(id);
    }

    private static GameDto MapToDto(Game game)
    {
        return new GameDto
        {
            Id = game.Id,
            Title = game.Title,
            Description = game.Description,
            Price = game.Price,
            Publisher = game.Publisher,
            ReleaseDate = game.ReleaseDate,
            Genre = game.Genre != null ? new GenreDto
            {
                Id = game.Genre.Id,
                Name = game.Genre.Name,
                Description = game.Genre.Description
            } : null
        };
    }
}