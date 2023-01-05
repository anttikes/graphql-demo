using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MovieCatalog.Persistence.Repositories;

/// <summary>
/// Provides equality comparison, combined hash value calculation and snapshot generation mechanics for the <see cref="Movie.Genres" /> property
/// </summary>
internal class GenresComparer : ValueComparer<HashSet<string>>
{
    public GenresComparer() : base(
        (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
        c => c.ToHashSet())
    {
    }
}
