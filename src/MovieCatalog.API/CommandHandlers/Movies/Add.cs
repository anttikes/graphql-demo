using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Domain.Commands.Movies;
using MovieCatalog.Domain.Models;
using MovieCatalog.Persistence.Repositories;

namespace MovieCatalog.API.CommandHandlers.Movies;

/// <summary>
/// Provides processing logic for the <see cref="Add" /> request
/// </summary>
internal sealed class AddCommandHandler : IRequestHandler<Add, Movie>, IAsyncDisposable
{
    private readonly MovieContext _context;
    private readonly IValidator<Add> _validator;

    public AddCommandHandler(IDbContextFactory<MovieContext> dbContextFactory, IValidator<Add> validator)
    {
        _context = dbContextFactory.CreateDbContext();
        _validator = validator;
    }

    public async Task<Movie> Handle(Add request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var movie = new Movie
        {
            Name = request.Name,
            Year = request.Year,
            Synopsis = request.Synopsis,
            AgeLimit = request.AgeLimit,
            Rating = request.Rating,
            Director = request.Director
        };

        if (request.Actors is not null)
        {
            foreach(var actor in request.Actors)
            {
                movie.Actors.Add(actor);
            }
        }

        if (request.Genres is not null)
        {
            foreach(var genre in request.Genres)
            {
                movie.Genres.Add(genre);
            }
        }

        _context.Movies.Add(movie);

        await _context.SaveChangesAsync(cancellationToken);

        return movie;
    }

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }
}
