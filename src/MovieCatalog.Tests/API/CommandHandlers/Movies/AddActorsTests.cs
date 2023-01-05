using Microsoft.EntityFrameworkCore;
using MovieCatalog.API.CommandHandlers.Movies;
using MovieCatalog.Domain.Commands.Movies;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Tests.API.CommandHAndlers.Movies;

public class AddActorsTests : IClassFixture<DBFixture>
{
    private readonly DBFixture _fixture;

    public AddActorsTests(DBFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void AddActors_ShouldSucceed()
    {
        // Arrange
        using var initialContext = _fixture.CreateDbContext();

        var originalMovie = initialContext.Movies.AsNoTracking().FirstOrDefault();

        Assert.NotNull(originalMovie);

        var newActors = new Person[]
        {
            new() { FirstName = "Sleepy", LastName = "Sleepers" }
        };

        var command = new AddActors(
            originalMovie.Id,
            newActors
        );

        // Act
        await using var systemUnderTest = new AddActorsCommandHandler(_fixture, new AddActorsValidationRules());
        var newMovie = await systemUnderTest.Handle(command, default);

        // Assert
        Assert.NotNull(newMovie);

        using var testContext = _fixture.CreateDbContext();
        var movieInContext = testContext.Movies.Find(newMovie.Id);

        Assert.NotNull(movieInContext);
        Assert.NotEqual(originalMovie.Actors, movieInContext.Actors);
        Assert.Equal(newMovie.Actors, movieInContext.Actors);
    }
}
