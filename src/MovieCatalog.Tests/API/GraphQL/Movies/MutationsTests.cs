using MovieCatalog.API.GraphQL.Movies;
using MovieCatalog.Domain.Commands.Movies;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Tests.API.GraphQL.Movies;

public class MutationsTests
{
    [Fact]
    public async Task AddMovie_ShouldSucceed()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var token = new CancellationTokenSource().Token;

        // The file is copied over by the project file
        mediatorMock.Setup(x => x.Send(It.IsAny<Add>(), token)).ReturnsAsync(new Movie());

        // Act
        var systemUnderTest = new Mutations();
        var movie = await systemUnderTest.AddMovie(It.IsAny<Add>(), mediatorMock.Object, token);

        // Assert
        Assert.NotNull(movie);
    }

    [Fact]
    public async Task UpdateMovie_ShouldSucceed()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var token = new CancellationTokenSource().Token;

        // The file is copied over by the project file
        mediatorMock.Setup(x => x.Send(It.IsAny<Update>(), token)).ReturnsAsync(new Movie());

        // Act
        var systemUnderTest = new Mutations();
        var movie = await systemUnderTest.UpdateMovie(It.IsAny<Update>(), mediatorMock.Object, token);

        // Assert
        Assert.NotNull(movie);
    }

    [Fact]
    public async Task RemoveMovie_ShouldSucceed()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var token = new CancellationTokenSource().Token;

        // The file is copied over by the project file
        mediatorMock.Setup(x => x.Send(It.IsAny<Remove>(), token)).ReturnsAsync(true);

        // Act
        var systemUnderTest = new Mutations();
        var returnValue = await systemUnderTest.RemoveMovie(It.IsAny<Remove>(), mediatorMock.Object, token);

        // Assert
        Assert.True(returnValue);
    }

    [Fact]
    public async Task AddActors_ShouldSucceed()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var token = new CancellationTokenSource().Token;

        // The file is copied over by the project file
        mediatorMock.Setup(x => x.Send(It.IsAny<AddActors>(), token)).ReturnsAsync(new Movie());

        // Act
        var systemUnderTest = new Mutations();
        var movie = await systemUnderTest.AddActors(It.IsAny<AddActors>(), mediatorMock.Object, token);

        // Assert
        Assert.NotNull(movie);
    }

    [Fact]
    public async Task RemoveActors_ShouldSucceed()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var token = new CancellationTokenSource().Token;

        // The file is copied over by the project file
        mediatorMock.Setup(x => x.Send(It.IsAny<RemoveActors>(), token)).ReturnsAsync(new Movie());

        // Act
        var systemUnderTest = new Mutations();
        var movie = await systemUnderTest.RemoveActors(It.IsAny<RemoveActors>(), mediatorMock.Object, token);

        // Assert
        Assert.NotNull(movie);
    }

    [Fact]
    public async Task AddGenres_ShouldSucceed()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var token = new CancellationTokenSource().Token;

        // The file is copied over by the project file
        mediatorMock.Setup(x => x.Send(It.IsAny<AddGenres>(), token)).ReturnsAsync(new Movie());

        // Act
        var systemUnderTest = new Mutations();
        var movie = await systemUnderTest.AddGenres(It.IsAny<AddGenres>(), mediatorMock.Object, token);

        // Assert
        Assert.NotNull(movie);
    }

    [Fact]
    public async Task RemoveGenres_ShouldSucceed()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var token = new CancellationTokenSource().Token;

        // The file is copied over by the project file
        mediatorMock.Setup(x => x.Send(It.IsAny<RemoveGenres>(), token)).ReturnsAsync(new Movie());

        // Act
        var systemUnderTest = new Mutations();
        var movie = await systemUnderTest.RemoveGenres(It.IsAny<RemoveGenres>(), mediatorMock.Object, token);

        // Assert
        Assert.NotNull(movie);
    }
}
