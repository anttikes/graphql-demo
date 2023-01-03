using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Domain.Commands.Movies;
using MovieCatalog.Domain.Models;
using MovieCatalog.Persistence.Repositories;

namespace MovieCatalog.API.CommandHandlers.Movies;

/// <summary>
/// Provides processing logic for the <see cref="RemoveGenres" /> request
/// </summary>
internal sealed class RemoveGenresCommandHandler : IRequestHandler<RemoveGenres, Movie>, IAsyncDisposable
{
    private readonly MovieContext _context;
    private readonly IValidator<RemoveGenres> _validator;

    public RemoveGenresCommandHandler(IDbContextFactory<MovieContext> dbContextFactory, IValidator<RemoveGenres> validator)
    {
        _context = dbContextFactory.CreateDbContext();
        _validator = validator;
    }

    public async Task<Movie> Handle(RemoveGenres request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var movie = await _context.Movies.FindAsync(request.MovieId);

        if (movie is null)
        {
            throw new InvalidOperationException("Movie with a specified ID could not be found");
        }

        foreach(var genre in request.Genres)
        {
            movie.Genres.Remove(genre);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return movie;
    }

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }
}
