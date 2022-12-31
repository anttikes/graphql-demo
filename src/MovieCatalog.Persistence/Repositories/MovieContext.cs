using MovieCatalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MovieCatalog.Persistence.Repositories;

public class MovieContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();

    public MovieContext(DbContextOptions<MovieContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entityBuilder =>
        {
            entityBuilder.ToTable(nameof(Movies));

            entityBuilder.HasKey(x => x.Id)
                         .HasName("PK_Movies_Id");

            entityBuilder.Property(x => x.Name);
            entityBuilder.Property(x => x.Year);
            entityBuilder.Property(x => x.AgeLimit);
            entityBuilder.Property(x => x.Rating);
            entityBuilder.Property(x => x.Synopsis);

            // The value comparer performs equality comparison, calculates a combined hash value, and is responsible for snapshots
            var genresComparer = new ValueComparer<ICollection<string>>(
                (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
            );

            // Use System.Text.Json to serialize the collection into a string type and back
            var genresConverter = new ValueConverter<ICollection<string>, string>(
                value => JsonSerializer.Serialize(value, new JsonSerializerOptions(JsonSerializerDefaults.Web)),
                value => JsonSerializer.Deserialize<ICollection<string>>(value, new JsonSerializerOptions(JsonSerializerDefaults.Web))!
            );

            entityBuilder.Property(x => x.Genres)
                         .HasConversion(genresConverter, genresComparer);

            entityBuilder.OwnsOne(x => x.Director, director =>
            {
                director.ToTable("Directors");

                director.WithOwner()
                        .HasForeignKey("MovieId")
                        .HasConstraintName("FK_Directors_MovieId_Movies_Id");

                director.Property<Guid>("Id");
                director.HasKey("Id")
                        .HasName("PK_Directors_Id");

                director.Property(x => x.FirstName);
                director.Property(x => x.LastName);
            });

            entityBuilder.OwnsMany(x => x.Actors, actor =>
            {
                actor.ToTable("Actors");

                actor.WithOwner()
                     .HasForeignKey("MovieId")
                     .HasConstraintName("FK_Actors_MovieId_Movies_Id");

                actor.Property<Guid>("Id");
                actor.HasKey("Id")
                     .HasName("PK_Actors_Id");

                actor.Property(x => x.FirstName);
                actor.Property(x => x.LastName);
            });
        });
    }
}
