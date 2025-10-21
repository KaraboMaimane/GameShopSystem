using Microsoft.EntityFrameworkCore;
using GameShop.Catalog.API.Data;
using GameShop.Catalog.API.Models;
using GameShop.Catalog.API.Repositories.Interfaces;

namespace GameShop.Catalog.API.Repositories.Implementations;

/// <summary>
/// Implementation of the game repository using Entity Framework Core
/// </summary>
public class GameRepository : IGameRepository
{
    private readonly CatalogDbContext _context;

    /// <summary>
    /// Initializes a new instance of the GameRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public GameRepository(CatalogDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await _context.Games
            .Include(g => g.Genre)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<Game?> GetByIdAsync(int id)
    {
        return await _context.Games
            .Include(g => g.Genre)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Game>> GetByGenreAsync(int genreId)
    {
        return await _context.Games
            .Include(g => g.Genre)
            .Where(g => g.GenreId == genreId)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<Game> CreateAsync(Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        return game;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(Game game)
    {
        try
        {
            _context.Entry(game).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(int id)
    {
        var game = await _context.Games.FindAsync(id);
        if (game == null)
            return false;

        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
        return true;
    }
}