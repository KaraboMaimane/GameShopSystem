namespace GameShop.Catalog.API.DTOs;

/// <summary>
/// Data Transfer Object for Genre information
/// </summary>
public class GenreDto
{
    /// <summary>
    /// Unique identifier for the genre
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the genre
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of the genre
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// DTO for creating a new genre
/// </summary>
public class CreateGenreDto
{
    /// <summary>
    /// Name of the genre
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of the genre
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// DTO for updating an existing genre
/// </summary>
public class UpdateGenreDto
{
    /// <summary>
    /// Name of the genre
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of the genre
    /// </summary>
    public string? Description { get; set; }
}