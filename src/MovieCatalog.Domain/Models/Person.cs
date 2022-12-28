using System.Diagnostics.CodeAnalysis;

namespace MovieCatalog.Domain.Models;

/// <summary>
/// Represents a person; may also be an actor in, or a director of one or more movies
/// </summary>
public sealed class Person
{
    /// <summary>
    /// Unique identifier of the person
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// First name of the person
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Last name of the person
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// List of movies that this person has directed
    /// </summary>
    public ICollection<Movie> Directions { get; } = new HashSet<Movie>();

    /// <summary>
    /// List of movies that this person has acted or appeared in
    /// </summary>
    public ICollection<Movie> Appearances { get; } = new HashSet<Movie>();

    /// <summary>
    /// Constructs a new instance of <see cref="Person" />
    /// </summary>
    /// <param name="firstName">First name of the person</param>
    /// <param name="lastName">Last name of the person</param>
    public Person(string firstName, string lastName)
    {
        Id = Guid.NewGuid();

        UpdateFirstName(firstName);
        UpdateLastName(lastName);
    }

    /// <summary>
    /// Updates the first name of the person
    /// </summary>
    /// <param name="newFirstName">New first name of the person</param>
    /// <exception cref="ArgumentException">Thrown if the specified new name is invalid</exception>
    [MemberNotNull(nameof(FirstName))]
    public void UpdateFirstName(string newFirstName)
    {
        if (string.IsNullOrWhiteSpace(newFirstName))
        {
            throw new ArgumentException("First name cannot be null, an empty string, or consist only of whitespace");
        }

        FirstName = newFirstName;
    }

    /// <summary>
    /// Updates the last name of the person
    /// </summary>
    /// <param name="newLastName">New last name of the person</param>
    /// <exception cref="ArgumentException">Thrown if the specified new name is invalid</exception>
    [MemberNotNull(nameof(LastName))]
    public void UpdateLastName(string newLastName)
    {
        if (string.IsNullOrWhiteSpace(newLastName))
        {
            throw new ArgumentException("Last name cannot be null, an empty string, or consist only of whitespace");
        }

        LastName = newLastName;
    }
}
