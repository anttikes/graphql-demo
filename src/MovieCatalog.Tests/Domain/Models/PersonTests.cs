using MovieCatalog.Domain.Models;

namespace MovieCatalog.Tests.Domain.Models;

public class PersonTests
{
    [Fact]
    public void PersonEquality_ShouldSucceed()
    {
        // Arrange
        var personA = new Person() { FirstName = "John", LastName = "Smith" };
        var personB = new Person() { FirstName = "John", LastName = "Smith" };

        Assert.Equal(personA, personB);
        Assert.True(personA == personB);
        Assert.True(object.Equals(personA, personB));
    }

    [Fact]
    public void PersonInequality_ShouldSucceed()
    {
        // Arrange
        var personA = new Person() { FirstName = "John", LastName = "Smith" };
        var personB = new Person() { FirstName = "Jane", LastName = "Smith" };
        Person? personC = null;

        Assert.NotEqual(personA, personB);
        Assert.False(personA == personB);
        Assert.True(personA != personB);

        Assert.NotEqual(personA, personC);
        Assert.False(object.Equals(personA, personC));
    }
}
