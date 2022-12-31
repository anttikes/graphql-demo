using FluentValidation;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Domain.ValidationRules.Movies;

/// <summary>
/// Contains the validation rules related to a <see cref="Movie" />
/// </summary>
public static class MovieValidationRules
{
    /// <summary>
    /// Limits the lowest possible year for movie publication
    /// </summary>
    private const ushort YEAR_WHEN_FIRST_MOVIE_WAS_SHOT = 1878;

    /// <summary>
    /// Maximum age limit that can be specified; summarized from various movie rating governing bodies listed in https://en.wikipedia.org/wiki/Motion_picture_content_rating_system
    /// </summary>
    public const byte MAX_AGE_LIMIT_IN_YEARS = 21;

    /// <summary>
    /// Maximum rating for a movie
    /// </summary>
    public const byte MAX_RATING_IN_STARS = 5;

    /// <summary>
    /// Defines the validation rules for <see cref="Movie.Id" />
    /// </summary>
    public static IRuleBuilderOptions<T, Guid> MovieId<T>(this IRuleBuilder<T, Guid> rule)
    {
        return rule
            .NotEmpty().WithMessage("Identifier of the movie must be provided");
    }

    /// <summary>
    /// Defines the validation rules for <see cref="Movie.Name" />
    /// </summary>
    public static IRuleBuilderOptions<T, string> Name<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty().WithMessage("Movie name cannot be null, an empty string, or consist fully of whitespace");
    }

    /// <summary>
    /// Defines the validation rules for <see cref="Movie.Year" />
    /// </summary>
    public static IRuleBuilderOptions<T, ushort> Year<T>(this IRuleBuilder<T, ushort> rule)
    {
        return rule
            .NotNull().WithMessage("Movie publication year cannot be null")
            .GreaterThanOrEqualTo(YEAR_WHEN_FIRST_MOVIE_WAS_SHOT).WithMessage("Specified year is before the first published movie");
    }

    /// <summary>
    /// Defines the validation rules for <see cref="Movie.Synopsis" />
    /// </summary>
    public static IRuleBuilderOptions<T, string> Synopsis<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty().WithMessage("Movie synopsis cannot be null, an empty string, or consist only of whitespace");
    }

    /// <summary>
    /// Defines the validation rules for <see cref="Movie.AgeLimit" />
    /// </summary>
    public static IRuleBuilderOptions<T, byte> AgeLimit<T>(this IRuleBuilder<T, byte> rule)
    {
        return rule
            .NotNull().WithMessage("Movie age limit must be provided")
            .LessThanOrEqualTo(MAX_AGE_LIMIT_IN_YEARS).WithMessage("Maximum settable age limit was exceeded");
    }

    /// <summary>
    /// Defines the validation rules for <see cref="Movie.AgeLimit" />
    /// </summary>
    public static IRuleBuilderOptions<T, byte> Rating<T>(this IRuleBuilder<T, byte> rule)
    {
        return rule
            .NotNull().WithMessage("Movie rating must be provided")
            .LessThanOrEqualTo(MAX_RATING_IN_STARS).WithMessage("Maximum rating for a movie was exceeded");
    }

    /// <summary>
    /// Defines the validation rules for <see cref="Person" /> when the person is representing an actor in a movie
    /// </summary>
    public static IRuleBuilderOptions<T, Person> Actor<T>(this IRuleBuilder<T, Person> rule)
    {
        return rule
            .NotEmpty().WithMessage("Person identifier must be provided when specifying an actor");
    }

    /// <summary>
    /// Defines the validation rules for a genre name
    /// </summary>
    public static IRuleBuilderOptions<T, string> GenreName<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty().WithMessage("Genre name cannot be null, an empty string, or consist only of whitespace");
    }
}
