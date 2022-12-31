namespace MovieCatalog.Domain.Models;

/// <summary>
/// Represents a person; may also be an actor in, or a director of one or more movies
/// </summary>
public sealed class Person : IEquatable<Person>
{
    /// <summary>
    /// First name of the person
    /// </summary>
    public string FirstName { get; set; } = null!;   // Switching to C# 11 would allow use of the 'required' keyword

    /// <summary>
    /// Last name of the person
    /// </summary>
    public string LastName { get; set; } = null!;   // Switching to C# 11 would allow use of the 'required' keyword

    #region IEquatable implementation

    public override bool Equals(object? obj) => Equals(obj as Person);

    public static bool operator ==(Person lhs, Person rhs) => object.Equals(lhs, rhs);

    public static bool operator !=(Person lhs, Person rhs) => ! (lhs == rhs);

    public override int GetHashCode() => FirstName.GetHashCode() ^ LastName.GetHashCode();

    public bool Equals(Person? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return FirstName.Equals(other.FirstName) && LastName.Equals(other.LastName);
    }

    #endregion
}
