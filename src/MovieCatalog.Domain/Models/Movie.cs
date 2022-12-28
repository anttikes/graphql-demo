using System.Diagnostics.CodeAnalysis;

namespace Gofore.Demo.MovieCatalog.Domain.Models;

/// <summary>
/// Represents a movie
/// </summary>
public sealed class Movie
{
    /// <summary>
    /// Maximum age limit that can be specified
    /// </summary>
    public const byte MAX_AGE_LIMIT_IN_YEARS = 21;
    
    /// <summary>
    /// Maximum rating for a movie
    /// </summary>
    public const byte MAX_RATING_STARS = 5;
    
    /// <summary>
    /// Limits the lowest possible year for movie publication
    /// </summary>
    public const ushort YEAR_WHEN_FIRST_MOVIE_WAS_SHOT = 1878;

    /// <summary>
    /// Unique identifier of the movie
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Name of the movie
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Year of publication for the movie
    /// </summary>
    public ushort Year { get; private set; }
    
    /// <summary>
    /// Age limit of the movie; if zero then movie has no age limit
    /// </summary>
    public byte AgeLimit { get; private set; }

    /// <summary>
    /// Rating of the movie; if zero then movie has not been rated yet
    /// </summary>
    public byte Rating { get; private set; }

    /// <summary>
    /// <c>True</c> if the movie has a synopsis; <c>false</c> otherwise
    /// </summary>
    public bool HasSynopsis => !string.IsNullOrWhiteSpace(Synopsis);

    /// <summary>
    /// Synopsis, or short summary, of the movie's plot
    /// </summary>
    public string? Synopsis { get; private set; }

    private HashSet<Person> _actors = new HashSet<Person>();

    /// <summary>
    /// Actors of the movie; can be empty if the movie has no actors
    /// </summary>
    public IReadOnlyCollection<Person> Actors => _actors;

    /// <summary>
    /// Director of the movie; if <c>null</c> then the movie has no director
    /// </summary>
    public Person? Director { get; private set; }

    /// <summary>
    /// Constructs a new instance of <see cref="Movie" />
    /// </summary>
    /// <param name="name">Name of the movie</param>
    /// <param name="year">Year of publication for the movie</param>
    public Movie(string name, ushort year)
    {
        SetName(name);
        SetYear(year);
    }

    /// <summary>
    /// Sets the name of the movie
    /// </summary>
    /// <param name="newName">New name of the movie</param>
    /// <exception cref="ArgumentException">Thrown if the specified new name is invalid</exception>
    [MemberNotNull(nameof(Name))]
    public void SetName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("Movie name cannot be null, an empty string, or consist only of whitespace");
        }

        Name = newName;
    }

    /// <summary>
    /// Sets the year of publication of the movie
    /// </summary>
    /// <param name="newYear">New year of publication for the movie</param>
    /// <exception cref="ArgumentException">Thrown if the specified new year is invalid</exception>
    public void SetYear(ushort newYear)
    {
        if (newYear < YEAR_WHEN_FIRST_MOVIE_WAS_SHOT)
        {
            throw new ArgumentException("Specified year is before the first published movie.");
        }

        Year = newYear;
    }
     
    /// <summary>
    /// Sets the age limit of the movie
    /// </summary>
    /// <param name="newLimit">New age limit</param>
    /// <exception cref="ArgumentException">Thrown if the specified age limit is invalid</exception>
    public void SetAgeLimit(byte newLimit)
    {
        if (newLimit > MAX_AGE_LIMIT_IN_YEARS)
        {
            throw new ArgumentException("Maximum age limit exceeded");
        }

        AgeLimit = newLimit;
    }

    /// <summary>
    /// Removes the age limit of the movie
    /// </summary>
    public void RemoveAgeLimit()
    {
        AgeLimit = 0;
    }

    /// <summary>
    /// Sets the rating of the movie
    /// </summary>
    /// <param name="newRating">New rating of the movie</param>
    /// <exception cref="ArgumentException">Thrown if the specified rating is invalid</exception>
    public void SetRating(byte newRating)
    {
        if (newRating > MAX_RATING_STARS)
        {
            throw new ArgumentException("Maximum rating exceeded");
        }

        Rating = newRating;
    }

    /// <summary>
    /// Removes the rating of the movie
    /// </summary>
    public void RemoveRating()
    {
        Rating = 0;
    }

    /// <summary>
    /// Sets the director of the movie
    /// </summary>
    /// <param name="newDirector">The new director of the movie</param>
    /// <exception cref="ArgumentNullException">The provided argument was null</exception>
    [MemberNotNull(nameof(Director))]
    public void SetDirector(Person newDirector)
    {
        if (newDirector is null)
        {
            throw new ArgumentNullException(nameof(newDirector));
        }

        Director = newDirector;
    }

    /// <summary>
    /// Removes the director of the movie
    /// </summary>
    public void RemoveDirector()
    {
        Director = null;
    }

    /// <summary>
    /// Adds actors to the cast of the movie
    /// </summary>
    /// <param name="newActors">List of actors to add</param>
    /// <exception cref="ArgumentNullException">The provided argument was null</exception>
    public void AddActors(IEnumerable<Person> newActors)
    {
        if (newActors is null)
        {
            throw new ArgumentNullException(nameof(newActors));
        }

        foreach(var actor in newActors)
        {
            AddActor(actor);
        }
    }

    /// <summary>
    /// Adds the specified actor to the cast of the movie
    /// </summary>
    /// <param name="newActor">The actor to add</param>
    /// <exception cref="ArgumentNullException">The provided argument was null</exception>
    public void AddActor(Person newActor)
    {
        if (newActor is null)
        {
            throw new ArgumentNullException(nameof(newActor));
        }

        _actors.Add(newActor);
    }

    /// <summary>
    /// Removes the specified actor from the cast of the movie
    /// </summary>
    /// <param name="existingActor">The actor to remove</param>
    public void RemoveActor(Person existingActor)
    {
        _actors.Remove(existingActor);
    }

    /// <summary>
    /// Sets the synopsis of the movie
    /// </summary>
    /// <param name="newSynopsis">The new synopsis</param>
    /// <exception cref="ArgumentException">The provided argument was invalid</exception>
    [MemberNotNull(nameof(Synopsis))]
    public void SetSynopsis(string newSynopsis)
    {
        if (string.IsNullOrWhiteSpace(newSynopsis))
        {
            throw new ArgumentException("Movie synopsis cannot be null, an empty string, or consist only of whitespace");
        }

        Synopsis = newSynopsis;
    }
}
