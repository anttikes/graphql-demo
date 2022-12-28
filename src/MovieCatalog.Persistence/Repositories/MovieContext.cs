using MovieCatalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieCatalog.Persistence.Repositories;

public class MovieContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Person> People => Set<Person>();

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

            entityBuilder.HasOne(x => x.Director)
                         .WithMany(x => x.Directions);

            entityBuilder.HasMany(x => x.Actors)
                         .WithMany(x => x.Appearances)
                         .UsingEntity(joinEntity =>
                         {
                             joinEntity.ToTable("Actors");

                             joinEntity.Property<Guid>("PersonId");
                             joinEntity.Property<Guid>("MovieId");

                             joinEntity.HasKey("PersonId", "MovieId")
                                       .HasName("PK_Actors_PersonId_MovieId");
                         });
        });

        modelBuilder.Entity<Person>(entityBuilder =>
        {
            entityBuilder.ToTable(nameof(People));

            entityBuilder.HasKey(x => x.Id)
                         .HasName("PK_People_Id");

            entityBuilder.Property(x => x.FirstName);
            entityBuilder.Property(x => x.LastName);
        });
    }
}
