using HotChocolate.Types;
using MovieCatalog.API.GraphQL.InitialData;
using MovieCatalog.Domain.Commands.Movies;

namespace MovieCatalog.Tests.API.GraphQL.InitialData;

public class MutationsTests
{
    [Fact]
    public async Task UploadInitialData_ShouldSucceed()
    {
        // Arrange
        var fileStub = new Mock<IFile>();
        var mediatorMock = new Mock<IMediator>();
        var token = new CancellationTokenSource().Token;

        // The file is copied over by the project file
        fileStub.Setup(x => x.OpenReadStream()).Returns(File.Open("movies-compact.json", FileMode.Open));
        mediatorMock.Setup(x => x.Send(It.IsAny<Add>(), token)).ReturnsAsync(new MovieCatalog.Domain.Models.Movie());

        // Act
        var systemUnderTest = new Mutations();
        var movies = await systemUnderTest.UploadInitialData(fileStub.Object, mediatorMock.Object, token);

        // Assert
        Assert.NotNull(movies);
        Assert.NotEmpty(movies);
    }
}
