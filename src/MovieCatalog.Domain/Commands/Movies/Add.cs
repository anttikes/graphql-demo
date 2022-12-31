using FluentValidation;
using MediatR;
using MovieCatalog.Domain.Models;
using MovieCatalog.Domain.ValidationRules.Movies;
using MovieCatalog.Domain.ValidationRules.People;

namespace MovieCatalog.Domain.Commands.Movies;

/// <summary>
/// Represents a request to add a new movie to the catalog
/// </summary>
public sealed class Add : IRequest<Movie>
{
    /// <inheritdoc cref="Models.Movie.Name" />
    public string Name { get; }

    /// <inheritdoc cref="Models.Movie.Year" />
    public ushort Year { get; }

    /// <inheritdoc cref="Models.Movie.Synopsis" />
    public string Synopsis { get; }

    /// <inheritdoc cref="Models.Movie.Director" />
    public Person Director { get; }

    /// <inheritdoc cref="Models.Movie.AgeLimit" />
    public byte? AgeLimit { get; }

    /// <inheritdoc cref="Models.Movie.Rating" />
    public byte? Rating { get; }

    /// <inheritdoc cref="Models.Movie.Actors" />
    public IEnumerable<Person>? Actors { get; }

    /// <inheritdoc cref="Models.Movie.Genres" />
    public IEnumerable<string>? Genres { get; }

    /// <summary>
    /// Constructs a new instance of the <see cref="Add" /> request
    /// </summary>
    public Add(
        string name,
        ushort year,
        string synopsis,
        Person director,
        byte? ageLimit,
        byte? rating,
        IEnumerable<Person>? actors,
        IEnumerable<string>? genres)
    {
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
/// Defines the validation rules for the <see cref="Add" /> request
/// </summary>
internal sealed class AddValidationRules : AbstractValidator<Add>
{
    public AddValidationRules()
    {
        RuleFor(x => x.Name).Name();
        RuleFor(x => x.Year).Year();
        RuleFor(x => x.Synopsis).Synopsis();
        RuleFor(x => x.Director).SetValidator(new PersonValidator());

        When(x => x.AgeLimit is not null, () => RuleFor(x => x.AgeLimit!.Value).AgeLimit());
        When(x => x.Rating is not null, () => RuleFor(x => x.Rating!.Value).Rating());
        When(x => x.Actors is not null, () => RuleForEach(x => x.Actors!).SetValidator(new PersonValidator()));
        When(x => x.Genres is not null, () => RuleForEach(x => x.Genres!).GenreName());
    }
}
