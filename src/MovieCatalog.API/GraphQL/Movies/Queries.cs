using Gofore.Demo.MovieCatalog.Domain.Models;
using Gofore.Demo.MovieCatalog.Persistence.Repositories;

namespace Gofore.Demo.MovieCatalog.API.GraphQL.Movies;

[ExtendObjectType("Query")]
public sealed class Queries
{
    public IQueryable<Movie> GetMovies(MovieContext context) => context.Movies;
}