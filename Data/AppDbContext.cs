using Cat_API_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Cat_API_Project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cat> Cats { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<BreedFact> BreedFacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cat>()
                .HasOne(c => c.Breed)
                .WithMany(b => b.Cats)
                .HasForeignKey(c => c.BreedId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BreedFact>()
                .HasOne(bf => bf.Breed)
                .WithMany(b => b.BreedFacts)
                .HasForeignKey(bf => bf.BreedId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Breed>()
                .HasIndex(b => b.ExternalBreedId)
                .IsUnique();

            modelBuilder.Entity<Breed>()
                .Property(b => b.BreedName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
