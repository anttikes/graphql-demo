namespace MovieCatalog.Domain.Models;

/// <summary>
/// Represents a movie genre
/// </summary>
public sealed class Genre
{
    /// <summary>
    /// Unique identifier of the genre
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Name of the genre
    /// </summary>
    public string Name { get; set; } = null!;   // Switching to C# 11 would allow use of the 'required' keyword

    /// <summary>
    /// Movies that exhibit themes from this genre
    /// </summary>
    public ICollection<Movie> Movies { get; } = new List<Movie>();
}
