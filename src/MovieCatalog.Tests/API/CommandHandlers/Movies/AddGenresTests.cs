using Microsoft.EntityFrameworkCore;
using MovieCatalog.API.CommandHandlers.Movies;
using MovieCatalog.Domain.Commands.Movies;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Tests.API.CommandHAndlers.Movies;

public class AddGenresTests : IClassFixture<DBFixture>
{
    private readonly DBFixture _fixture;

    public AddGenresTests(DBFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void AddGenres_ShouldSucceed()
    {
        // Arrange
        using var initialContext = _fixture.CreateDbContext();

        var originalMovie = initialContext.Movies.AsNoTracking().FirstOrDefault();

        Assert.NotNull(originalMovie);

        var newGenres = new[]
        {
            "Monsters"
        };

        var command = new AddGenres(
            originalMovie.Id,
            newGenres
        );

        // Act
        await using var systemUnderTest = new AddGenresCommandHandler(_fixture, new AddGenresValidationRules());
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
