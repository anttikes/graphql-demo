using FluentValidation;
using MediatR;
using MovieCatalog.Domain.Models;
using MovieCatalog.Domain.ValidationRules.Movies;
using MovieCatalog.Domain.ValidationRules.People;

namespace MovieCatalog.Domain.Commands.Movies;

/// <summary>
/// Represents a request to add one or more actors to a movie
/// </summary>
public sealed class AddActors : IRequest<Movie>
{
    /// <inheritdoc cref="Models.Movie.Id" />
    public Guid MovieId { get; }

    /// <summary>
    /// Actors to add to the movie
    /// </summary>
    public IEnumerable<Person> Actors { get; }

    /// <summary>
    /// Constructs a new instance of the <see cref="Add" /> request
    /// </summary>
    public AddActors(
        Guid movieId,
        IEnumerable<Person> actors)
    {
        MovieId = movieId;
        Actors = actors;
    }
}

/// <summary>
/// Defines the validation rules for the <see cref="AddActors" /> request
/// </summary>
internal sealed class AddActorsValidationRules : AbstractValidator<AddActors>
{
    public AddActorsValidationRules()
    {
        RuleFor(x => x.MovieId).MovieId();
        RuleForEach(x => x.Actors).SetValidator(new PersonValidator());
    }
}
