using System.Text.Json;
using Gofore.Demo.MovieCatalog.Persistence.Repositories;

namespace Gofore.Demo.MovieCatalog.API.GraphQL.Files;

[ExtendObjectType("Mutation")]
internal sealed class Mutations
{
    /// <summary>
    /// Uploads and parses the specified file and seeds the database with initial data; only usable when the database is considered empty
    /// </summary>
    /// <param name="file">File containing the initial data</param>
    /// <param name="context">Entity Framework context to use for saving data</param>
    /// <param name="cancellationToken">Token used to cancel the operation</param>
    /// <returns></returns>
    public async Task<bool> UploadInitialData(IFile file, MovieContext context, CancellationToken cancellationToken)
    {
        if (context.Movies.Any() || context.People.Any())
        {
            return false;
        }

        await using (Stream content = file.OpenReadStream())
        {
            var opts = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            var movies = await JsonSerializer.DeserializeAsync<Movie[]>(content, opts, cancellationToken);

            if (movies is null)
            {
                return false;
            }

            foreach(var movie in movies)
            {
                Domain.Models.Movie newMovie = new Domain.Models.Movie(movie.Name, movie.Year);

                newMovie.SetAgeLimit(movie.AgeLimit);
                newMovie.SetRating(movie.Rating);
                newMovie.SetSynopsis(movie.Synopsis);

                newMovie.SetDirector(new(movie.Director.FirstName, movie.Director.LastName));

                foreach(var actor in movie.Actors)                
                {
                    newMovie.AddActor(new(actor.FirstName, actor.LastName));
                }

                context.Movies.Add(newMovie);
            }

            context.SaveChanges();

            return true;
        }
    }
}
