using Microsoft.EntityFrameworkCore;
using GameShop.Catalog.API.Models;

namespace GameShop.Catalog.API.Data;

/// <summary>
/// Database context for the Game Shop Catalog
/// </summary>
public class CatalogDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the CatalogDbContext
    /// </summary>
    /// <param name="options">The options to be used by the DbContext</param>
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) 
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the games in the catalog
    /// </summary>
    public DbSet<Game> Games => Set<Game>();

    /// <summary>
    /// Gets or sets the genres in the catalog
    /// </summary>
    public DbSet<Genre> Genres => Set<Genre>();

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Game entity
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Publisher).IsRequired().HasMaxLength(100);
            
            // Configure relationship with Genre
            entity.HasOne(e => e.Genre)
                  .WithMany(g => g.Games)
                  .HasForeignKey(e => e.GenreId)
                  .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        });

        // Configure Genre entity
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);
            
            // Add a unique constraint on the Name
            entity.HasIndex(e => e.Name).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}