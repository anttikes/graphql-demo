using Gofore.Demo.MovieCatalog.Domain.Models;

namespace Gofore.Demo.MovieCatalog.API.GraphQL.Movies;

/// <summary>
/// Extends the <see cref="Movie" /> type, allowing modifications of the type in the GraphQL schema
/// </summary>
internal sealed class MovieTypeExtension : ObjectTypeExtension<Movie>
{
    protected override void Configure(IObjectTypeDescriptor<Movie> descriptor)
    {
        // Using selections with parameterized constructors is not supported at the moment, so we disable traversals; see https://github.com/ChilliCream/hotchocolate/issues/4387
        descriptor.Field(x => x.Actors).Ignore();
        descriptor.Field(x => x.Director).Ignore();
    }
}
