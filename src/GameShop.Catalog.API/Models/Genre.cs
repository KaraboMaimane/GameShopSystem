namespace GameShop.Catalog.API.Models;

/// <summary>
/// Represents a game genre in the catalog
/// </summary>
public class Genre
{
    /// <summary>
    /// Unique identifier for the genre
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the genre (e.g., "Action", "RPG", "Strategy")
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional description of the genre
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Navigation property for games in this genre
    /// </summary>
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}