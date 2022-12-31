using MovieCatalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieCatalog.Persistence.Repositories;

/// <summary>
/// Enables persisting of movie data into a SQL Server database
/// </summary>
public class MovieContext : DbContext
{
    /// <summary>
    /// Represents a collection of movies
    /// </summary>
    public DbSet<Movie> Movies => Set<Movie>();

    public MovieContext(DbContextOptions<MovieContext> options) : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure movies
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

            // Configure genres
            entityBuilder.Property(x => x.Genres)
                         .HasConversion<GenresConverter, GenresComparer>();

            // Configure director
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

            // Configure actors
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
