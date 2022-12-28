using Gofore.Demo.MovieCatalog.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gofore.Demo.MovieCatalog.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var app = CreateHostBuilder(args)
                    .Build();

        app.MapGraphQL("/");

        app.Run();
    }

    private static WebApplicationBuilder CreateHostBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddPooledDbContextFactory<MovieContext>(optionsBuilder =>
        {
            var migrationAssemblyName = typeof(MovieContext).Assembly.GetName().Name;

            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING"), opt => opt.MigrationsAssembly(migrationAssemblyName));

            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        });

        builder.Services
            .AddGraphQLServer()
            .AddType<UploadType>()
            .AddType<UnsignedShortType>()
            .RegisterDbContext<MovieContext>(DbContextKind.Pooled)
            .AddQueryType(descriptor => descriptor.Description("Contains the available query operations"))
                .AddTypeExtension<GraphQL.Movies.Queries>()
                .AddTypeExtension<GraphQL.People.Queries>()
            .AddMutationType(descriptor => descriptor.Description("Contains the available mutations"))
                .AddTypeExtension<GraphQL.Files.Mutations>()
            .AddTypeExtension<GraphQL.Movies.MovieTypeExtension>()
            .AddTypeExtension<GraphQL.People.PersonTypeExtension>()
            .AddProjections()
            .AddFiltering();

        // Returning at this stage allows EF Core migrations to work against the API project
        return builder;
    }
}
