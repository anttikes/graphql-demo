using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Domain.Models;
using MovieCatalog.Persistence.Repositories;

public class DBFixture : IDisposable, IDbContextFactory<MovieContext>
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<MovieContext> _contextOptions;

    public MovieContext CreateDbContext() => new MovieContext(_contextOptions);

    public DBFixture()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var migrationAssemblyName = typeof(MovieContext).Assembly.GetName().Name;

        _contextOptions = new DbContextOptionsBuilder<MovieContext>()
            .UseSqlite(_connection, opt => opt.MigrationsAssembly(migrationAssemblyName))
            .Options;

        using var context = new MovieContext(_contextOptions);

        context.Database.EnsureCreated();

        var movieAVP = new Movie()
        {
            Name = "Alien vs. Predator",
            Year = 2022,
            AgeLimit = 18,
            Rating = 3,
            Synopsis = "An alien meets a predator, and then, well, you know what happens?",
            Director = new Person() { FirstName = "Paul", LastName = "Anderson" }
        };

        movieAVP.Actors.Add(new() { FirstName = "Sylvester", LastName = "Stallone" });
        movieAVP.Actors.Add(new() { FirstName = "Arnold", LastName = "Schwarzenegger" });

        movieAVP.Genres.Add("Action");
        movieAVP.Genres.Add("Sci-fi");

        context.Movies.Add(movieAVP);
        context.SaveChanges();
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}
