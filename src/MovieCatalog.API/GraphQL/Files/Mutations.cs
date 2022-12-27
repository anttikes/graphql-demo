using System.Text.Json;
using Gofore.Demo.MovieCatalog.Domain.Models;
using Gofore.Demo.MovieCatalog.Persistence.Repositories;

namespace Gofore.Demo.MovieCatalog.API.GraphQL.Files;

[ExtendObjectType("Mutation")]
public sealed class Mutations
{
    public async Task<int> UploadInitialData(IFile file, MovieContext context, CancellationToken cancellationToken)
    {
        if (context.Movies.Any() || context.People.Any())
        {
            return 0;
        }

        await using (Stream content = file.OpenReadStream())
        {
            var opts = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            var movies = await JsonSerializer.DeserializeAsync<Movie[]>(content, opts, cancellationToken);

            if (movies is null)
            {
                return 0;
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

            return movies.Length;
        }
    }
}