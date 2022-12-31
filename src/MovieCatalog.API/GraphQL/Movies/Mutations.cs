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
    /// <remarks>Actors and genres, if specified in the request, fully replace existing data in the movie</remarks>
    /// <returns>The updated movie</returns>
    public async Task<Movie> UpdateMovie(Update request, [Service] IMediator mediator, CancellationToken cancellationToken)
        => await mediator.Send(request, cancellationToken);

    /// <summary>
    /// Permanently removes a movie
    /// </summary>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<bool> RemoveMovie(Remove request, [Service] IMediator mediator, CancellationToken cancellationToken)
        => await mediator.Send(request, cancellationToken);

    /// <summary>
    /// Adds one or more actors to a movie
    /// </summary>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<Movie> AddActors(AddActors request, [Service] IMediator mediator, CancellationToken cancellationToken)
        => await mediator.Send(request, cancellationToken);

    /// <summary>
    /// Removes one or more actors from a movie
    /// </summary>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<Movie> RemoveActors(RemoveActors request, [Service] IMediator mediator, CancellationToken cancellationToken)
        => await mediator.Send(request, cancellationToken);

    /// <summary>
    /// Adds one or more genres to a movie
    /// </summary>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<Movie> AddGenres(AddGenres request, [Service] IMediator mediator, CancellationToken cancellationToken)
        => await mediator.Send(request, cancellationToken);

    /// <summary>
    /// Removes one or more genres from a movie
    /// </summary>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<Movie> RemoveGenres(RemoveGenres request, [Service] IMediator mediator, CancellationToken cancellationToken)
        => await mediator.Send(request, cancellationToken);
}
