using Microsoft.EntityFrameworkCore;
using MovieCatalog.API.CommandHandlers.Movies;
using MovieCatalog.Domain.Commands.Movies;

namespace MovieCatalog.Tests.API.CommandHAndlers.Movies;

public class RemoveGenresTests : IClassFixture<DBFixture>
{
    private readonly DBFixture _fixture;

    public RemoveGenresTests(DBFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void RemoveGenres_ShouldSucceed()
    {
        // Arrange
        using var initialContext = _fixture.CreateDbContext();

        var originalMovie = initialContext.Movies.AsNoTracking().FirstOrDefault();

        Assert.NotNull(originalMovie);

        var command = new RemoveGenres(
            originalMovie.Id,
            new[] { originalMovie.Genres.First() }
        );

        // Act
        await using var systemUnderTest = new RemoveGenresCommandHandler(_fixture, new RemoveGenresValidationRules());
        var newMovie = await systemUnderTest.Handle(command, default);

        // Assert
        Assert.NotNull(newMovie);

        using var testContext = _fixture.CreateDbContext();
        var movieInContext = testContext.Movies.Find(newMovie.Id);

        Assert.NotNull(movieInContext);
        Assert.NotEqual(originalMovie.Genres, movieInContext.Genres);
        Assert.Equal(newMovie.Genres, movieInContext.Genres);
    }
}
