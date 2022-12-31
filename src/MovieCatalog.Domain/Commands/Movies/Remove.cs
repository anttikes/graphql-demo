using FluentValidation;
using MediatR;
using MovieCatalog.Domain.ValidationRules.Movies;

namespace MovieCatalog.Domain.Commands.Movies;

/// <summary>
/// Represents a request to completely remove a movie from the catalog
/// </summary>
public sealed class Remove : IRequest<bool>
{
    /// <inheritdoc cref="Models.Movie.Id" />
    public Guid MovieId { get; }

    /// <summary>
    /// Constructs a new instance of the <see cref="Remove" /> request
    /// </summary>
    public Remove(Guid movieId)
    {
        MovieId = movieId;
    }
}

/// <summary>
/// Defines the validation rules for the <see cref="Remove" /> request
/// </summary>
internal sealed class RemoveValidationRules : AbstractValidator<Remove>
{
    public RemoveValidationRules()
    {
        RuleFor(x => x.MovieId).MovieId();
    }
}
