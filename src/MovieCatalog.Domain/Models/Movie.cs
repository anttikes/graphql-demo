using System.Diagnostics.CodeAnalysis;

namespace Gofore.Demo.MovieCatalog.Domain.Models;

public sealed class Movie
{
    public const byte MAX_AGE_LIMIT_IN_YEARS = 21;
    public const byte MAX_RATING_STARS = 5;
    public const ushort YEAR_WHEN_FIRST_MOVIE_WAS_SHOT = 1878;

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public ushort Year { get; private set; }

    public byte AgeLimit { get; private set; }

    public byte Rating { get; private set; }

    public bool HasSynopsis => !string.IsNullOrWhiteSpace(Synopsis);

    public string? Synopsis { get; private set; }

    private HashSet<Person> _actors = new HashSet<Person>();
    public IReadOnlyCollection<Person> Actors => _actors;

    public Person? Director { get; private set; }

    public Movie(string name, ushort year)
    {
        SetName(name);
        SetYear(year);
    }

    [MemberNotNull(nameof(Name))]
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Movie name cannot be null, an empty string, or consist only of whitespace");
        }

        Name = name;
    }

    public void SetYear(ushort newYear)
    {
        if (newYear < YEAR_WHEN_FIRST_MOVIE_WAS_SHOT)
        {
            throw new ArgumentException("Specified year is before the first published movie.");
        }

        Year = newYear;
    }
    
    public void SetAgeLimit(byte newLimit)
    {
        if (newLimit > MAX_AGE_LIMIT_IN_YEARS)
        {
            throw new ArgumentException("Maximum age limit exceeded");
        }

        AgeLimit = newLimit;
    }

    public void RemoveAgeLimit()
    {
        AgeLimit = 0;
    }

    public void SetRating(byte newRating)
    {
        if (newRating > MAX_RATING_STARS)
        {
            throw new ArgumentException("Maximum rating exceeded");
        }

        Rating = newRating;
    }

    [MemberNotNull(nameof(Director))]
    public void SetDirector(Person newDirector)
    {
        if (newDirector is null)
        {
            throw new ArgumentNullException(nameof(newDirector));
        }

        Director = newDirector;
    }

    public void RemoveDirector()
    {
        Director = null;
    }

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

    public void AddActor(Person newActor)
    {
        if (newActor is null)
        {
            throw new ArgumentNullException(nameof(newActor));
        }

        _actors.Add(newActor);
    }

    public void RemoveActor(Person existingActor)
    {
        _actors.Remove(existingActor);
    }

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
