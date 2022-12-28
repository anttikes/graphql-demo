using Gofore.Demo.MovieCatalog.Domain.Models;
using Gofore.Demo.MovieCatalog.Persistence.Repositories;

namespace Gofore.Demo.MovieCatalog.API.GraphQL.Movies;

[ExtendObjectType("Mutation")]
internal sealed class Mutations
{
    /// <summary>
    /// Create a new movie
    /// </summary>
    /// <param name="name">Name of the movie</param>
    /// <param name="year">Year when the movie was (or will be) published</param>
    /// <param name="context">Entity Framework context to use</param>
    /// <returns>The newly created movie</returns>
    public async Task<Movie> CreateMovie(string name, ushort year, MovieContext context)
    {
        var movie = new Movie(name, year);

        context.Movies.Add(movie);

        await context.SaveChangesAsync();

        return movie;
    }

    /// <summary>
    /// Permanently removes a movie
    /// </summary>
    /// <param name="movieId">Unique identifier of the movie</param>
    /// <param name="context">Entity Framework context to use</param>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<bool> RemovePerson(Guid movieId, MovieContext context)
    {
        var movie = context.Movies.Find(movieId);

        if (movie is null)
        {
            return false;
        }

        context.Movies.Remove(movie);

        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Sets the name of the movie
    /// </summary>
    /// <param name="movieId">Unique identifier of the movie</param>
    /// <param name="newName">New name of the movie</param>
    /// <param name="context">Entity Framework context to use</param>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<bool> SetName(Guid movieId, string newName, MovieContext context)
    {
        var movie = await context.Movies.FindAsync(movieId);

        if (movie is null)
        {
            return false;
        }

        movie.SetName(newName);

        await context.SaveChangesAsync();

        return true;
    }
    
    /// <summary>
    /// Sets the year of publication of the movie
    /// </summary>
    /// <param name="movieId">Unique identifier of the movie</param>
    /// <param name="newYear">New year of publication for the movie</param>
    /// <param name="context">Entity Framework context to use</param>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<bool> SetYearName(Guid movieId, ushort newYear, MovieContext context)
    {
        var movie = await context.Movies.FindAsync(movieId);

        if (movie is null)
        {
            return false;
        }

        movie.SetYear(newYear);

        await context.SaveChangesAsync();

        return true;
    }
    
    /// <summary>
    /// Sets the age limit of the movie
    /// </summary>
    /// <param name="movieId">Unique identifier of the movie</param>
    /// <param name="newAgeLimit">New age limit</param>
    /// <param name="context">Entity Framework context to use</param>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<bool> SetAgeLimit(Guid movieId, byte newAgeLimit, MovieContext context)
    {
        var movie = await context.Movies.FindAsync(movieId);

        if (movie is null)
        {
            return false;
        }

        movie.SetAgeLimit(newAgeLimit);

        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Removes the age limit of the movie
    /// </summary>
    /// <param name="movieId">Unique identifier of the movie</param>
    /// <param name="context">Entity Framework context to use</param>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<bool> RemoveAgeLimit(Guid movieId, MovieContext context)
    {
        var movie = await context.Movies.FindAsync(movieId);

        if (movie is null)
        {
            return false;
        }

        movie.RemoveAgeLimit();

        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Sets the rating of the movie
    /// </summary>
    /// <param name="movieId">Unique identifier of the movie</param>
    /// <param name="newRating">>New rating of the movie</param>
    /// <param name="context">Entity Framework context to use</param>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<bool> SetRating(Guid movieId, byte newRating, MovieContext context)
    {
        var movie = await context.Movies.FindAsync(movieId);

        if (movie is null)
        {
            return false;
        }

        movie.SetRating(newRating);

        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Removes the rating of the movie
    /// </summary>
    /// <param name="movieId">Unique identifier of the movie</param>
    /// <param name="context">Entity Framework context to use</param>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<bool> RemoveRating(Guid movieId, MovieContext context)
    {
        var movie = await context.Movies.FindAsync(movieId);

        if (movie is null)
        {
            return false;
        }

        movie.RemoveRating();

        await context.SaveChangesAsync();

        return true;
    }
    
    /// <summary>
    /// Sets the synopsis of the movie
    /// </summary>
    /// <param name="movieId">Unique identifier of the movie</param>
    /// <param name="newSynopsis">The new synopsis of the movie</param>
    /// <param name="context">Entity Framework context to use</param>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<bool> SetSynopsis(Guid movieId, string newSynopsis, MovieContext context)
    {
        var movie = await context.Movies.FindAsync(movieId);

        if (movie is null)
        {
            return false;
        }

        movie.SetSynopsis(newSynopsis);

        await context.SaveChangesAsync();

        return true;
    }
}
