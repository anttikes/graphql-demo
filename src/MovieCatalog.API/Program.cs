using Gofore.Demo.MovieCatalog.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gofore.Demo.MovieCatalog.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var app = CreateHostBuilder(args)
                    .Build();

        app.MapGraphQL();

        app.Run();
    }

    public static WebApplicationBuilder CreateHostBuilder(string[] args)
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
            .BindRuntimeType<ushort, UnsignedShortType>()
            .RegisterDbContext<MovieContext>(DbContextKind.Pooled)
            .AddQueryType()
                .AddTypeExtension<Gofore.Demo.MovieCatalog.API.GraphQL.Movies.Query>()
                .AddTypeExtension<Gofore.Demo.MovieCatalog.API.GraphQL.People.Query>();

        return builder;
    }
}
