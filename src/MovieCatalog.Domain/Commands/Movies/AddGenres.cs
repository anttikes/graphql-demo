using FluentValidation;
using MediatR;
using MovieCatalog.Domain.Models;
using MovieCatalog.Domain.ValidationRules.Movies;
using MovieCatalog.Domain.ValidationRules.People;

namespace MovieCatalog.Domain.Commands.Movies;

/// <summary>
/// Represents a request to add one or more genres from a movie
/// </summary>
public sealed class AddGenres : IRequest<Movie>
{
    /// <inheritdoc cref="Models.Movie.Id" />
    public Guid MovieId { get; }

    /// <summary>
    /// Genres to remove from the movie
    /// </summary>
    public IEnumerable<string> Genres { get; }

    /// <summary>
    /// Constructs a new instance of the <see cref="AddGenres" /> request
    /// </summary>
    public AddGenres(
        Guid movieId,
        IEnumerable<string> genres)
    {
        MovieId = movieId;
        Genres = genres;
    }
}

/// <summary>
/// Defines the validation rules for the <see cref="AddGenres" /> request
/// </summary>
internal sealed class AddGenresValidationRules : AbstractValidator<AddGenres>
{
    public AddGenresValidationRules()
    {
        RuleFor(x => x.MovieId).MovieId();
        RuleForEach(x => x.Genres).GenreName();
    }
}
