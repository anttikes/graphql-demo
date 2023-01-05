using Microsoft.EntityFrameworkCore;
using MovieCatalog.API.GraphQL.Movies;
using MovieCatalog.Domain.Models;
using MovieCatalog.Persistence.Repositories;

namespace MovieCatalog.Tests.API.GraphQL.Movies;

public class QueriesTests
{
    [Fact]
    public void GetMovies_ShouldSucceed()
    {
        // Arrange
        var optsBuilder = new DbContextOptionsBuilder<MovieContext>();
        optsBuilder.UseInMemoryDatabase("MovieCatalog.Tests.API.GraphQL.Movies.QueriesTests");

        using var context = new MovieContext(optsBuilder.Options);

        context.Database.EnsureCreated();

        var dummyMovie = new Movie()
        {
            Name = "Test Movie",
            Synopsis = "A unit test, movie and a director walk into a bar...",
            Director = new Person()
            {
                FirstName = "Lana",
                LastName = "Wachowski"
            }
        };

        context.Movies.Add(dummyMovie);
        context.SaveChanges();

        // Act
        var systemUnderTest = new Queries();
        var movieQueryable = systemUnderTest.GetMovies(context);

        // Assert
        Assert.NotNull(movieQueryable);
        Assert.NotEmpty(movieQueryable);
    }
}
