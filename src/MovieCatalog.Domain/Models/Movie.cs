namespace MovieCatalog.Domain.Models;

/// <summary>
/// Represents a movie
/// </summary>
public sealed class Movie
{
    /// <summary>
    /// Unique identifier of the movie
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Name of the movie
    /// </summary>
    public string Name { get; set; } = null!;   // Switching to C# 11 would allow use of the 'required' keyword

    /// <summary>
    /// Year of publication for the movie
    /// </summary>
    public ushort Year { get; set; }

    /// <summary>
    /// Age limit of the movie; if zero then movie has no age limit
    /// </summary>
    public byte AgeLimit { get; set; }

    /// <summary>
    /// Rating of the movie; if zero then movie has not been rated yet
    /// </summary>
    public byte Rating { get; set; }

    /// <summary>
    /// Synopsis, or short summary, of the movie's plot
    /// </summary>
    public string Synopsis { get; set; } = null!;   // Switching to C# 11 would allow use of the 'required' keyword

    /// <summary>
    /// Actors of the movie; can be empty if the movie has no actors
    /// </summary>
    public ICollection<Person> Actors { get; } = new List<Person>();

    /// <summary>
    /// Genres of the movie; can be empty if the movie has no genres defined
    /// </summary>
    public ICollection<Genre> Genres { get; } = new List<Genre>();

    /// <summary>
    /// Unique identifier of the person who is the director; if <c>null</c> then the movie has no director
    /// </summary>
    public Guid? DirectorId { get; set; }

    /// <summary>
    /// Director of the movie; if <c>null</c> then the movie has no director
    /// </summary>
    public Person? Director { get; set; }
}
