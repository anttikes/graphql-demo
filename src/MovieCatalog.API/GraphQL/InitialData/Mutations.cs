using System.Text.Json;
using MediatR;

namespace MovieCatalog.API.GraphQL.InitialData;

[ExtendObjectType("Mutation")]
internal sealed class Mutations
{
    /// <summary>
    /// Uploads and parses the specified file and seeds the database with initial data; only usable when the database is empty
    /// </summary>
    public async Task<Domain.Models.Movie[]> UploadInitialData(IFile file, [Service] IMediator mediator, CancellationToken cancellationToken)
    {
        await using (Stream content = file.OpenReadStream())
        {
            var opts = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            var moviesInFile = await JsonSerializer.DeserializeAsync<Movie[]>(content, opts, cancellationToken);

            if (moviesInFile is null || !moviesInFile.Any())
            {
                return new Domain.Models.Movie[0];
            }

            List<Domain.Models.Movie> addedMovies = new();

            foreach(var movie in moviesInFile)
            {
                var director = new Domain.Models.Person()
                {
                    FirstName = movie.Director.FirstName,
                    LastName = movie.Director.LastName
                };

                var actors = movie.Actors.Select(x => new Domain.Models.Person()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).ToArray();

                var cmd = new Domain.Commands.Movies.Add(
                    movie.Name,
                    movie.Year,
                    movie.Synopsis,
                    director,
                    movie.AgeLimit,
                    movie.Rating,
                    actors,
                    movie.Genres
                );

                var newMovie = await mediator.Send(cmd, cancellationToken);

                addedMovies.Add(newMovie);
            }

            return addedMovies.ToArray();
        }
    }
}
