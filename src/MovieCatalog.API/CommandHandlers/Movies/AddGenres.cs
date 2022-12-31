using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Domain.Commands.Movies;
using MovieCatalog.Domain.Models;
using MovieCatalog.Persistence.Repositories;

namespace MovieCatalog.API.CommandHandlers.Movies;

/// <summary>
/// Provides processing logic for the <see cref="AddGenres" /> request
/// </summary>
internal sealed class AddGenresCommandHandler : IRequestHandler<AddGenres, Movie>, IAsyncDisposable
{
    private readonly MovieContext _context;
    private readonly IValidator<AddGenres> _validator;

    public AddGenresCommandHandler(IDbContextFactory<MovieContext> dbContextFactory, IValidator<AddGenres> validator)
    {
        _context = dbContextFactory.CreateDbContext();
        _validator = validator;
    }

    public async Task<Movie> Handle(AddGenres request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var movie = _context.Movies.Find(request.MovieId);

        if (movie is null)
        {
            throw new InvalidOperationException("Movie with a specified ID could not be found");
        }

        foreach(var genre in request.Genres)
        {
            movie.Genres.Add(genre);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return movie;
    }

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }
}
