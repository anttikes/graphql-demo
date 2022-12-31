using FluentValidation;
using MediatR;
using MovieCatalog.Domain.Models;
using MovieCatalog.Domain.ValidationRules.Movies;
using MovieCatalog.Domain.ValidationRules.People;

namespace MovieCatalog.Domain.Commands.Movies;

/// <summary>
/// Represents a request to remove one or more actors from a movie
/// </summary>
public sealed class RemoveActors : IRequest<Movie>
{
    /// <inheritdoc cref="Models.Movie.Id" />
    public Guid MovieId { get; }

    /// <summary>
    /// Actors to remove from the movie
    /// </summary>
    public IEnumerable<Person> Actors { get; }

    /// <summary>
    /// Constructs a new instance of the <see cref="Add" /> request
    /// </summary>
    public RemoveActors(
        Guid movieId,
        IEnumerable<Person> actors)
    {
        MovieId = movieId;
        Actors = actors;
    }
}

/// <summary>
/// Defines the validation rules for the <see cref="RemoveActors" /> request
/// </summary>
internal sealed class RemoveActorsValidationRules : AbstractValidator<RemoveActors>
{
    public RemoveActorsValidationRules()
    {
        RuleFor(x => x.MovieId).MovieId();
        RuleForEach(x => x.Actors).SetValidator(new PersonValidator());
    }
}
