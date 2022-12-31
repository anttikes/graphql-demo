using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MovieCatalog.Persistence.Repositories;

/// <summary>
/// Uses System.Text.Json to convert a collection of genre names from <see cref="Movie.Genres" /> into a single-string JSON representation and back
/// </summary>
internal class GenresConverter : ValueConverter<ICollection<string>, string>
{
    public GenresConverter() : base(
        value => JsonSerializer.Serialize(value, new JsonSerializerOptions(JsonSerializerDefaults.Web)),
        value => JsonSerializer.Deserialize<ICollection<string>>(value, new JsonSerializerOptions(JsonSerializerDefaults.Web))!)
    {
    }
}
