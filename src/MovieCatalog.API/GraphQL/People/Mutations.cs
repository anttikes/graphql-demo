using Gofore.Demo.MovieCatalog.Domain.Models;
using Gofore.Demo.MovieCatalog.Persistence.Repositories;

namespace Gofore.Demo.MovieCatalog.API.GraphQL.People;

[ExtendObjectType("Mutation")]
internal sealed class Mutations
{
    /// <summary>
    /// Creates a new person
    /// </summary>
    /// <param name="firstName">The first name of the person</param>
    /// <param name="lastName">The last name of the person</param>
    /// <param name="context">Entity Framework context to use</param>
    /// <returns>The newly created person</returns>
    public async Task<Person> CreatePerson(string firstName, string lastName, MovieContext context)
    {
        var newPerson = new Domain.Models.Person(firstName, lastName);

        context.Add(newPerson);

        await context.SaveChangesAsync();

        return newPerson;
    }

    /// <summary>
    /// Permanently removes a person
    /// </summary>
    /// <param name="personId">Unique identifier of the person</param>
    /// <param name="context">Entity Framework context to use</param>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<bool> RemovePerson(Guid personId, MovieContext context)
    {
        var person = context.People.Find(personId);

        if (person is null)
        {
            return false;
        }

        context.People.Remove(person);

        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Sets the first name of the person
    /// </summary>
    /// <param name="personId">Unique identifier of the person</param>
    /// <param name="newName">New first name of the person</param>
    /// <param name="context">Entity Framework context to use</param>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<bool> SetFirstName(Guid personId, string newName, MovieContext context)
    {
        var movie = await context.Movies.FindAsync(personId);

        if (movie is null)
        {
            return false;
        }

        movie.SetName(newName);

        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Sets the last name of the person
    /// </summary>
    /// <param name="personId">Unique identifier of the person</param>
    /// <param name="newName">New last name of the person</param>
    /// <param name="context">Entity Framework context to use</param>
    /// <returns><c>True</c> if the operation succeeded; <c>false</c> otherwise</returns>
    public async Task<bool> SetLastName(Guid personId, string newName, MovieContext context)
    {
        var movie = await context.Movies.FindAsync(personId);

        if (movie is null)
        {
            return false;
        }

        movie.SetName(newName);

        await context.SaveChangesAsync();

        return true;
    }
}
