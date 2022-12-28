using Gofore.Demo.MovieCatalog.Domain.Models;
using Gofore.Demo.MovieCatalog.Persistence.Repositories;

namespace Gofore.Demo.MovieCatalog.API.GraphQL.Movies;

/// <summary>
/// Defines the root level queries provided by this area
/// </summary>
[ExtendObjectType("Query")]
internal sealed class Queries
{
    /// <summary>
    /// Search for movies based on specific criteria
    /// </summary>
    /// <param name="context">Entity Framework context to use for querying</param>
    /// <returns>A list of <see cref="Movie" /> object that match the criteria</returns>
    [UseProjection]
    [UseFiltering]
    public IQueryable<Movie> GetMovies(MovieContext context) => context.Movies;    
}
