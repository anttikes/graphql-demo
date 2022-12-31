using FluentValidation;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Domain.ValidationRules.People;

/// <summary>
/// Contains the validation rules related to a <see cref="Person" />
/// </summary>
public static class PersonValidationRules
{
    /// <summary>
    /// Defines the validation rules for <see cref="Person.FirstName" />
    /// </summary>
    public static IRuleBuilderOptions<T, string> FirstName<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty().WithMessage("First name cannot be null, an empty string, or consist only of whitespace");
    }

    /// <summary>
    /// Defines the validation rules for <see cref="Person.LastName" />
    /// </summary>
    public static IRuleBuilderOptions<T, string> LastName<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty().WithMessage("Last name cannot be null, an empty string, or consist only of whitespace");
    }
}
