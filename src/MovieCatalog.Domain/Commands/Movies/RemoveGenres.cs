using FluentValidation;
using MediatR;
using MovieCatalog.Domain.Models;
using MovieCatalog.Domain.ValidationRules.Movies;
using MovieCatalog.Domain.ValidationRules.People;

namespace MovieCatalog.Domain.Commands.Movies;

/// <summary>
/// Represents a request to remove one or more genres from a movie
/// </summary>
public sealed class RemoveGenres : IRequest<Movie>
{
    /// <inheritdoc cref="Models.Movie.Id" />
    public Guid MovieId { get; }

    /// <summary>
    /// Genres to remove from the movie
    /// </summary>
    public IEnumerable<string> Genres { get; }

    /// <summary>
    /// Constructs a new instance of the <see cref="RemoveGenres" /> request
    /// </summary>
    public RemoveGenres(
        Guid movieId,
        IEnumerable<string> genres)
    {
        MovieId = movieId;
        Genres = genres;
    }
}

/// <summary>
/// Defines the validation rules for the <see cref="RemoveGenres" /> request
/// </summary>
internal sealed class RemoveGenresValidationRules : AbstractValidator<RemoveGenres>
{
    public RemoveGenresValidationRules()
    {
        RuleFor(x => x.MovieId).MovieId();
        RuleForEach(x => x.Genres).GenreName();
    }
}
