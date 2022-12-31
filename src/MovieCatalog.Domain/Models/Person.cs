namespace MovieCatalog.Domain.Models;

/// <summary>
/// Represents a person; may also be an actor in, or a director of one or more movies
/// </summary>
public sealed class Person
{
    /// <summary>
    /// First name of the person
    /// </summary>
    public string FirstName { get; set; } = null!;   // Switching to C# 11 would allow use of the 'required' keyword

    /// <summary>
    /// Last name of the person
    /// </summary>
    public string LastName { get; set; } = null!;   // Switching to C# 11 would allow use of the 'required' keyword
}
