using FluentValidation;
using MediatR;
using MovieCatalog.Domain.Models;
using MovieCatalog.Domain.ValidationRules.Movies;
using MovieCatalog.Domain.ValidationRules.People;

namespace MovieCatalog.Domain.Commands.Movies;

/// <summary>
/// Represents a request to update a movie's details
/// </summary>
public sealed class Update : IRequest<Movie>
{
    /// <inheritdoc cref="Models.Movie.Id" />
    public Guid MovieId { get; }

    /// <inheritdoc cref="Models.Movie.Name" />
    public string? Name { get; }

    /// <inheritdoc cref="Models.Movie.Year" />
    public ushort? Year { get; }

    /// <inheritdoc cref="Models.Movie.Synopsis" />
    public string? Synopsis { get; }

    /// <inheritdoc cref="Models.Movie.Director" />
    public Person? Director { get; }

    /// <inheritdoc cref="Models.Movie.AgeLimit" />
    public byte? AgeLimit { get; }

    /// <inheritdoc cref="Models.Movie.Rating" />
    public byte? Rating { get; }

    /// <inheritdoc cref="Models.Movie.Actors" />
    public IEnumerable<Person>? Actors { get; }

    /// <inheritdoc cref="Models.Movie.Genres" />
    public IEnumerable<string>? Genres { get; }

    /// <summary>
    /// Constructs a new instance of the <see cref="Update" /> request
    /// </summary>
    public Update(
        Guid movieId,
        string? name,
        ushort? year,
        string? synopsis,
        Person? director,
        byte? ageLimit,
        byte? rating,
        IEnumerable<Person>? actors,
        IEnumerable<string>? genres
    )
    {
        MovieId = movieId;
        Name = name;
        Year = year;
        Synopsis = synopsis;
        Director = director;
        AgeLimit = ageLimit;
        Rating = rating;
        Actors = actors;
        Genres = genres;
    }
}

/// <summary>
/// Defines the validation rules for the <see cref="Update" /> request
/// </summary>
internal sealed class UpdateValidationRules : AbstractValidator<Update>
{
    public UpdateValidationRules()
    {
        When(x => x.Name is not null, () => RuleFor(x => x.Name!).Name());
        When(x => x.Year is not null, () => RuleFor(x => x.Year!.Value).Year());
        When(x => x.Synopsis is not null, () => RuleFor(x => x.Synopsis!).Synopsis());
        When(x => x.AgeLimit is not null, () => RuleFor(x => x.AgeLimit!.Value).AgeLimit());
        When(x => x.Rating is not null, () => RuleFor(x => x.Rating!.Value).Rating());
        When(x => x.Director is not null, () => RuleFor(x => x.Director!).SetValidator(new PersonValidator()));

        When(x => x.Actors is not null, () => RuleForEach(x => x.Actors!).SetValidator(new PersonValidator()));
        When(x => x.Genres is not null, () => RuleForEach(x => x.Genres!).GenreName());
    }
}
