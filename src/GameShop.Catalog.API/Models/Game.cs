namespace GameShop.Catalog.API.Models;

/// <summary>
/// Represents a game in the catalog
/// </summary>
public class Game
{
    /// <summary>
    /// Unique identifier for the game
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Title of the game
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Description of the game
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Price of the game
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Publisher of the game
    /// </summary>
    public string Publisher { get; set; } = string.Empty;

    /// <summary>
    /// Release date of the game
    /// </summary>
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    /// Foreign key for Genre
    /// </summary>
    public int GenreId { get; set; }

    /// <summary>
    /// Navigation property for the game's genre
    /// </summary>
    public virtual Genre? Genre { get; set; }
}