using MediatR;
using MovieCatalog.Domain.Commands.Movies;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.API.GraphQL.Movies;

[ExtendObjectType("Mutation")]
internal sealed class Mutations
{
    /// <summary>
    /// Adds a new movie to the catalog
    /// </summary>
    /// <returns>The newly created movie</returns>
    public async Task<Movie> AddMovie(Add request, [Service] IMediator mediator, CancellationToken cancellationToken)
        => await mediator.Send(request, cancellationToken);

    /// <summary>
    /// Updates the details of a movie
    /// </summary>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<Movie> UpdateMovie(Update request, [Service] IMediator mediator, CancellationToken cancellationToken)
        => await mediator.Send(request, cancellationToken);

    /// <summary>
    /// Permanently removes a movie
    /// </summary>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<bool> RemoveMovie(Remove request, [Service] IMediator mediator, CancellationToken cancellationToken)
        => await mediator.Send(request, cancellationToken);
}
