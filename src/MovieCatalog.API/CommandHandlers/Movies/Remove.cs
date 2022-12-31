using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Domain.Commands.Movies;
using MovieCatalog.Domain.Models;
using MovieCatalog.Persistence.Repositories;

namespace MovieCatalog.API.CommandHandlers.Movies;

/// <summary>
/// Provides processing logic for the <see cref="Remove" /> command
/// </summary>
internal sealed class RemoveCommandHandler : IRequestHandler<Remove, bool>, IAsyncDisposable
{
    private readonly MovieContext _context;
    private readonly IValidator<Remove> _validator;

    public RemoveCommandHandler(IDbContextFactory<MovieContext> dbContextFactory, IValidator<Remove> validator)
    {
        _context = dbContextFactory.CreateDbContext();
        _validator = validator;
    }

    public async Task<bool> Handle(Remove request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var movie = new Movie
        {
            Id = request.MovieId
        };

        _context.Movies.Attach(movie);
        _context.Movies.Remove(movie);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }
}
