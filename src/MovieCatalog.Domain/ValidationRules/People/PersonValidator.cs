using FluentValidation;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Domain.ValidationRules.People;

/// <summary>
/// Defines the validation rules for a <see cref="Person" />
/// </summary>
internal sealed class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.FirstName).FirstName();
        RuleFor(x => x.LastName).LastName();
    }
}
