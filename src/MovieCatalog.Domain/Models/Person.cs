namespace MovieCatalog.Domain.Models;

/// <summary>
/// Represents a person; may also be an actor in, or a director of one or more movies
/// </summary>
public sealed class Person
{
    /// <summary>
    /// Unique identifier of the person
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// First name of the person
    /// </summary>
    public string FirstName { get; set; } = null!;   // Switching to C# 11 would allow use of the 'required' keyword

    /// <summary>
    /// Last name of the person
    /// </summary>
    public string LastName { get; set; } = null!;   // Switching to C# 11 would allow use of the 'required' keyword

    /// <summary>
    /// List of movies that this person has directed
    /// </summary>
    public ICollection<Movie> Directions { get; } = new List<Movie>();

    /// <summary>
    /// List of movies that this person has acted or appeared in
    /// </summary>
    public ICollection<Movie> Appearances { get; } = new List<Movie>();
}
