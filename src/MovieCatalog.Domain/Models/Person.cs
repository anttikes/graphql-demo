using System.Diagnostics.CodeAnalysis;

namespace Gofore.Demo.MovieCatalog.Domain.Models;

public sealed class Person
{
    public Guid Id { get; private set; }
    
    public string FirstName { get; private set; }
    
    public string LastName { get; private set; }

    public ICollection<Movie> Directions { get; } = new HashSet<Movie>();

    public ICollection<Movie> Appearances { get; } = new HashSet<Movie>();

    public Person(string firstName, string lastName)
    {
        Id = Guid.NewGuid();

        UpdateFirstName(firstName);
        UpdateLastName(lastName);
    }

    [MemberNotNull(nameof(FirstName))]
    public void UpdateFirstName(string newFirstName)
    {
        if (string.IsNullOrWhiteSpace(newFirstName))
        {
            throw new ArgumentException("First name cannot be null, an empty string, or consist only of whitespace");
        }

        FirstName = newFirstName;
    }

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