using System.Text.Json.Serialization;

namespace Gofore.Demo.MovieCatalog.API.GraphQL.Files;

public sealed class Movie
{
    public string Name { get; }
    public ushort Year { get; }
    public IEnumerable<string> Genres { get; }
    public byte AgeLimit { get; }
    public byte Rating { get; }
    public IEnumerable<Actor> Actors { get; }
    public Director Director { get; }
    public string Synopsis { get; }

    [JsonConstructor]
    public Movie(
        string name, 
        ushort year, 
        IEnumerable<string> genres, 
        byte ageLimit, 
        byte rating, 
        IEnumerable<Actor> actors, 
        Director director, 
        string synopsis
    )
    {
        Name = name;
        Year = year;
        Genres = genres;
        AgeLimit = ageLimit;
        Rating = rating;
        Actors = actors;
        Director = director;
        Synopsis = synopsis;
    }
}

public sealed class Actor
{
    public string FirstName { get; }
    public string LastName { get; }

    [JsonConstructor]
    public Actor(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

public sealed class Director
{
    public string FirstName { get; }
    public string LastName { get; }

    [JsonConstructor]
    public Director(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}