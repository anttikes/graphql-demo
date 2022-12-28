using Gofore.Demo.MovieCatalog.Domain.Models;
using Gofore.Demo.MovieCatalog.Persistence.Repositories;

namespace Gofore.Demo.MovieCatalog.API.GraphQL.People;

[ExtendObjectType("Query")]
internal sealed class Queries
{
    /// <summary>
    /// Search for people based on specific criteria
    /// </summary>
    /// <param name="context">Entity Framework context to use for querying</param>
    /// <returns>A list of <see cref="Person" /> object that match the criteria</returns>
    [UseFiltering]
    public IQueryable<Person> GetPeople(MovieContext context) => context.People;
}
