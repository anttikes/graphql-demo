using Microsoft.EntityFrameworkCore;
using MovieCatalog.API.CommandHandlers.Movies;
using MovieCatalog.Domain.Commands.Movies;

namespace MovieCatalog.Tests.API.CommandHAndlers.Movies;

public class RemoveActorsTests : IClassFixture<DBFixture>
{
    private readonly DBFixture _fixture;

    public RemoveActorsTests(DBFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void RemoveActors_ShouldSucceed()
    {
        // Arrange
        using var initialContext = _fixture.CreateDbContext();

        var originalMovie = initialContext.Movies.AsNoTracking().FirstOrDefault();

        Assert.NotNull(originalMovie);

        var command = new RemoveActors(
            originalMovie.Id,
            new[] { originalMovie.Actors.First() }
        );

        // Act
        await using var systemUnderTest = new RemoveActorsCommandHandler(_fixture, new RemoveActorsValidationRules());
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
