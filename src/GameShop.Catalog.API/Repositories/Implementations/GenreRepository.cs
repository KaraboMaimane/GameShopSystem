using Microsoft.EntityFrameworkCore;
using GameShop.Catalog.API.Data;
using GameShop.Catalog.API.Models;
using GameShop.Catalog.API.Repositories.Interfaces;

namespace GameShop.Catalog.API.Repositories.Implementations;

/// <summary>
/// Implementation of the genre repository using Entity Framework Core
/// </summary>
public class GenreRepository : IGenreRepository
{
    private readonly CatalogDbContext _context;

    /// <summary>
    /// Initializes a new instance of the GenreRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public GenreRepository(CatalogDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
        return await _context.Genres
            .Include(g => g.Games)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<Genre?> GetByIdAsync(int id)
    {
        return await _context.Genres
            .Include(g => g.Games)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    /// <inheritdoc/>
    public async Task<Genre> CreateAsync(Genre genre)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
        return genre;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(Genre genre)
    {
        try
        {
            _context.Entry(genre).State = EntityState.Modified;
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
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
            return false;

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
        return true;
    }
}