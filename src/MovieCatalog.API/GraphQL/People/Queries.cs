using Gofore.Demo.MovieCatalog.Domain.Models;
using Gofore.Demo.MovieCatalog.Persistence.Repositories;

namespace Gofore.Demo.MovieCatalog.API.GraphQL.People;

[ExtendObjectType("Query")]
public sealed class Query
{
    public IQueryable<Person> GetPeople(MovieContext context) => context.People;
}