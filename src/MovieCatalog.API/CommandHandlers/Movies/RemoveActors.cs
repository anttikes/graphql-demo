using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Domain.Commands.Movies;
using MovieCatalog.Domain.Models;
using MovieCatalog.Persistence.Repositories;

namespace MovieCatalog.API.CommandHandlers.Movies;

/// <summary>
/// Provides processing logic for the <see cref="RemoveActors" /> request
/// </summary>
internal sealed class RemoveActorsCommandHandler : IRequestHandler<RemoveActors, Movie>, IAsyncDisposable
{
    private readonly MovieContext _context;
    private readonly IValidator<RemoveActors> _validator;

    public RemoveActorsCommandHandler(IDbContextFactory<MovieContext> dbContextFactory, IValidator<RemoveActors> validator)
    {
        _context = dbContextFactory.CreateDbContext();
        _validator = validator;
    }

    public async Task<Movie> Handle(RemoveActors request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var movie = _context.Movies.Find(request.MovieId);

        if (movie is null)
        {
            throw new InvalidOperationException("Movie with a specified ID could not be found");
        }

        foreach(var actor in request.Actors)
        {
            var existingActor = movie.Actors.SingleOrDefault(x => x.FirstName == actor.FirstName && x.LastName == actor.LastName);

            if (existingActor is not null)
            {
                movie.Actors.Remove(existingActor);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return movie;
    }

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }
}
