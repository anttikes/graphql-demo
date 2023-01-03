using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Domain.Commands.Movies;
using MovieCatalog.Domain.Models;
using MovieCatalog.Persistence.Repositories;

namespace MovieCatalog.API.CommandHandlers.Movies;

/// <summary>
/// Provides processing logic for the <see cref="Update" /> command
/// </summary>
internal sealed class UpdateCommandHandler : IRequestHandler<Update, Movie>, IAsyncDisposable
{
    private readonly MovieContext _context;
    private readonly IValidator<Update> _validator;

    public UpdateCommandHandler(IDbContextFactory<MovieContext> dbContextFactory, IValidator<Update> validator)
    {
        _context = dbContextFactory.CreateDbContext();
        _validator = validator;
    }

    public async Task<Movie> Handle(Update request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var movie = await _context.Movies.FindAsync(request.MovieId);

        if (movie is null)
        {
            throw new InvalidOperationException("Movie with a specified ID could not be found");
        }

        if (request.Name is not null)
        {
            movie.Name = request.Name;
        }

        if (request.Year is not null)
        {
            movie.Year = request.Year.Value;
        }

        if (request.Synopsis is not null)
        {
            movie.Synopsis = request.Synopsis;
        }

        if (request.AgeLimit is not null)
        {
            movie.AgeLimit = request.AgeLimit.Value;
        }

        if (request.Rating is not null)
        {
            movie.Rating = request.Rating.Value;
        }

        if (request.Actors is not null)
        {
            movie.Actors.Clear();

            foreach(var actor in request.Actors)
            {
                var newActor = new Person()
                {
                    FirstName = actor.FirstName,
                    LastName = actor.LastName
                };

                movie.Actors.Add(newActor);
            }
        }

        if (request.Genres is not null)
        {
            movie.Genres.Clear();

            foreach(var genre in request.Genres)
            {
                movie.Genres.Add(genre);
            }
        }

        if (request.Director is not null)
        {
            movie.Director.FirstName = request.Director.FirstName;
            movie.Director.LastName = request.Director.LastName;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return movie;
    }

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }
}
