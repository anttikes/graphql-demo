using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Domain.Extensions;
using MovieCatalog.Persistence.Repositories;

namespace MovieCatalog.API;

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

        builder.Services.AddFluentValidation();
        builder.Services.AddMediatR(typeof(Program));

        builder.Services.AddPooledDbContextFactory<MovieContext>(optionsBuilder =>
        {
            var migrationAssemblyName = typeof(MovieContext).Assembly.GetName().Name;

#if !DEBUG
            optionsBuilder.UseModel(MovieCatalog.Persistence.CompiledModels.MovieContextModel.Instance);
#endif
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING"), opt => opt.MigrationsAssembly(migrationAssemblyName));

            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        });

        builder.Services
            .AddGraphQLServer()
            .AddType<UploadType>()
            .AddType<UnsignedShortType>()
            .RegisterDbContext<MovieContext>(DbContextKind.Pooled)
            .RegisterService<IMediator>(ServiceKind.Pooled)
            .AddQueryType(descriptor => descriptor.Description("Contains the available queries"))
                .AddTypeExtension<GraphQL.Movies.Queries>()
            .AddMutationType(descriptor => descriptor.Description("Contains the available mutations"))
                .AddTypeExtension<GraphQL.InitialData.Mutations>()
                .AddTypeExtension<GraphQL.Movies.Mutations>()
            .AddFiltering()
            .AddSorting();

        // Returning at this stage allows EF Core migrations to work against the API project
        return builder;
    }
}
