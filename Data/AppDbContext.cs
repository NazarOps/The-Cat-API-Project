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
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Account -> Cats (One to many)
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Cats)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.SetNull);

            // Breed -> Cats (One to many)
            modelBuilder.Entity<Cat>()
                .HasOne(c => c.Breed)
                .WithMany(b => b.Cats)
                .HasForeignKey(c => c.BreedId)
                .OnDelete(DeleteBehavior.Cascade);

            //Breed -> BreedFacts (One to many)
            modelBuilder.Entity<BreedFact>()
                .HasOne(bf => bf.Breed)
                .WithMany(b => b.BreedFacts)
                .HasForeignKey(bf => bf.BreedId)
                .OnDelete(DeleteBehavior.Cascade);

            //External BreedId
            modelBuilder.Entity<Breed>()
                .HasIndex(b => b.ExternalBreedId)
                .IsUnique();

            //BreedName rules
            modelBuilder.Entity<Breed>()
                .Property(b => b.BreedName)
                .IsRequired()
                .HasMaxLength(100);

            //Account rules
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Email)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .Property(a => a.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<Account>()
                .Property(a => a.PasswordSalt)
                .IsRequired();

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Username)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Account>()
                .Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Account>()
                .Property(a => a.Username)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Account>()
                .Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(150);

            // Cat rules
            modelBuilder.Entity<Cat>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Cat>()
                .Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
