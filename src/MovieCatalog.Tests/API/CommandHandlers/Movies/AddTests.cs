using MovieCatalog.API.CommandHandlers.Movies;
using MovieCatalog.Domain.Commands.Movies;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Tests.API.CommandHAndlers.Movies;

public class AddTests : IClassFixture<DBFixture>
{
    private readonly DBFixture _fixture;

    public AddTests(DBFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void AddMovie_ShouldSucceed()
    {
        // Arrange
        var actors = new Person[]
        {
            new() { FirstName = "Kid", LastName = "Karma" }
        };

        var genres = new[]
        {
            "Children"
        };

        var command = new Add(
            "Sandman",
            2021,
            "The Sandman comes, and throws sleepy sand, and then you sleep",
            new() { FirstName = "Sleepy", LastName = "Sleeper" },
            7,
            2,
            actors,
            genres
        );

        // Act
        await using var systemUnderTest = new AddCommandHandler(_fixture, new AddValidationRules());
        var newMovie = await systemUnderTest.Handle(command, default);

        // Assert
        Assert.NotNull(newMovie);

        using var testContext = _fixture.CreateDbContext();
        var movieInContext = testContext.Movies.Find(newMovie.Id);

        Assert.NotNull(movieInContext);
        Assert.Equal(newMovie.Name, movieInContext.Name);
        Assert.Equal(newMovie.Year, movieInContext.Year);
        Assert.Equal(newMovie.Synopsis, movieInContext.Synopsis);
        Assert.Equal(newMovie.Director, movieInContext.Director);
        Assert.Equal(newMovie.AgeLimit, movieInContext.AgeLimit);
        Assert.Equal(newMovie.Rating, movieInContext.Rating);
        Assert.Equal(newMovie.Actors, movieInContext.Actors);
        Assert.Equal(newMovie.Genres, movieInContext.Genres);
    }
}
