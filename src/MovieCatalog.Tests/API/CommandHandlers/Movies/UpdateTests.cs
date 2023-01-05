using Microsoft.EntityFrameworkCore;
using MovieCatalog.API.CommandHandlers.Movies;
using MovieCatalog.Domain.Commands.Movies;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Tests.API.CommandHAndlers.Movies;

public class UpdateTests : IClassFixture<DBFixture>
{
    private readonly DBFixture _fixture;

    public UpdateTests(DBFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void UpdateMovie_ShouldSucceed()
    {
        // Arrange
        using var initialContext = _fixture.CreateDbContext();

        var originalMovie = initialContext.Movies.AsNoTracking().FirstOrDefault();

        Assert.NotNull(originalMovie);

        var command = new Update(
            originalMovie.Id,
            "New Alien vs. Predator v2",
            null,
            null,
            null,
            null,
            null,
            null,
            null
        );

        // Act
        await using var systemUnderTest = new UpdateCommandHandler(_fixture, new UpdateValidationRules());
        var updatedMovie = await systemUnderTest.Handle(command, default);

        // Assert
        Assert.NotNull(updatedMovie);

        using var testContext = _fixture.CreateDbContext();
        var movieInContext = testContext.Movies.Find(updatedMovie.Id);

        Assert.NotNull(movieInContext);
        Assert.Equal(updatedMovie.Name, movieInContext.Name);
        Assert.NotEqual(originalMovie.Name, movieInContext.Name);
    }
}
