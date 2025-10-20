namespace GameShop.Catalog.API.DTOs;

/// <summary>
/// Data Transfer Object for Game information
/// </summary>
public class GameDto
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
    /// Genre information for the game
    /// </summary>
    public GenreDto? Genre { get; set; }
}

/// <summary>
/// DTO for creating a new game
/// </summary>
public class CreateGameDto
{
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
    /// ID of the genre this game belongs to
    /// </summary>
    public int GenreId { get; set; }
}

/// <summary>
/// DTO for updating an existing game
/// </summary>
public class UpdateGameDto
{
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
    /// ID of the genre this game belongs to
    /// </summary>
    public int GenreId { get; set; }
}