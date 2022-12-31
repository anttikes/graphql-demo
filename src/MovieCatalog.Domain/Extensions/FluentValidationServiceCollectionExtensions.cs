using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace MovieCatalog.Domain.Extensions;

public static class FluentValidationServiceCollectionExtensions
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);

        return services;
    }
}
