using Gofore.Demo.MovieCatalog.Domain.Models;

namespace Gofore.Demo.MovieCatalog.API.GraphQL.People;

/// <summary>
/// Extends the <see cref="Person" /> type, allowing modifications of the type in the GraphQL schema
/// </summary>
internal sealed class PersonTypeExtension : ObjectTypeExtension<Person>
{
    protected override void Configure(IObjectTypeDescriptor<Person> descriptor)
    {
        // Using selections with parameterized constructors is not supported at the moment, so we disable traversals; see https://github.com/ChilliCream/hotchocolate/issues/4387
        descriptor.Field(x => x.Directions).Ignore();
        descriptor.Field(x => x.Appearances).Ignore();
    }
}
