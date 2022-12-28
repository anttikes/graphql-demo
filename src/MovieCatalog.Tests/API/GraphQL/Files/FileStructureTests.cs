using System.Text.Json;
using Gofore.Demo.MovieCatalog.API.GraphQL.Files;

namespace Gofore.Demo.MovieCatalog.Tests.API.GraphQL.Files;

public class FileStructureTests
{
    [Fact]
    public void DeserializationShouldSucceed()
    {
        // The file is copied over by the project file
        var file = File.Open("movies-compact.json", FileMode.Open);

        var content = JsonSerializer.Deserialize<IEnumerable<Movie>>(file, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        file.Close();

        Assert.NotNull(content);
        Assert.Equal(20, content.Count());
    }
}