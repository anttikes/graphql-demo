using MovieCatalog.Domain.Models;
using MovieCatalog.Persistence.Repositories;

namespace MovieCatalog.API.GraphQL.Movies;

[ExtendObjectType("Query")]
internal sealed class Queries
{
    /// <summary>
    /// Search for movies based on specific criteria
    /// </summary>
    /// <param name="context">Entity Framework context to use for querying</param>
    /// <returns>A list of <see cref="Movie" /> object that match the criteria</returns>
    [UseFiltering]
    public IQueryable<Movie> GetMovies(MovieContext context) => context.Movies;
}
