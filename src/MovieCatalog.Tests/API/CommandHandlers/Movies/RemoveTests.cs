using MovieCatalog.API.CommandHandlers.Movies;
using MovieCatalog.Domain.Commands.Movies;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Tests.API.CommandHAndlers.Movies;

public class RemoveTests : IClassFixture<DBFixture>
{
    private readonly DBFixture _fixture;

    public RemoveTests(DBFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void RemoveMovie_ShouldSucceed()
    {
        // Arrange
        var dummyMovie = new Movie()
        {
            Name = "This movie is going to go away",
            Year = 2022,
            AgeLimit = 16,
            Rating = 5,
            Synopsis = "It's not going to stay",
            Director = new Person() { FirstName = "Paul", LastName = "Verhoeven" }
        };

        dummyMovie.Actors.Add(new() { FirstName = "Keanu", LastName = "Reeves" });

        dummyMovie.Genres.Add("Fantasy");

        using var initialContext = _fixture.CreateDbContext();

        initialContext.Movies.Add(dummyMovie);
        var changesCount = initialContext.SaveChanges();

        Assert.NotEqual(0, changesCount);

        var command = new Remove(
            dummyMovie.Id
        );

        // Act
        await using var systemUnderTest = new RemoveCommandHandler(_fixture, new RemoveValidationRules());
        var wasRemoved = await systemUnderTest.Handle(command, default);

        // Assert
        Assert.True(wasRemoved);

        using var testContext = _fixture.CreateDbContext();
        var movieInContext = testContext.Movies.Find(dummyMovie.Id);

        Assert.Null(movieInContext);
    }
}
