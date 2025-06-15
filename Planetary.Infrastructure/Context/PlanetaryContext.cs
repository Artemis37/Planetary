using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Planetary.Domain.Models;

namespace Planetary.Infrastructure.Context
{
    public class PlanetaryContext(DbContextOptions<PlanetaryContext> options) : DbContext(options)
    {
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Criteria> Criteria { get; set; }
        public DbSet<PlanetCriteria> PlanetCriteria { get; set; }
        public DbSet<User> Users { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Planet>(ConfigurePlanet);
            modelBuilder.Entity<Criteria>(ConfigureCriteria);
            modelBuilder.Entity<PlanetCriteria>(ConfigurePlanetCriteria);
            modelBuilder.Entity<User>(ConfigureUser);
        }

        private void ConfigurePlanet(EntityTypeBuilder<Planet> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Navigation(p => p.PlanetCriteria)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasField("_planetCriteria");
                
            builder.HasMany(p => p.PlanetCriteria)
                .WithOne(pc => pc.Planet)
                .HasForeignKey(pc => pc.PlanetId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasOne(p => p.User)
                .WithMany(u => u.Planets)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureCriteria(EntityTypeBuilder<Criteria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired();
                
            builder.Navigation(c => c.PlanetCriteria)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasField("_planetCriteria");

            builder.HasMany(c => c.PlanetCriteria)
                .WithOne(pc => pc.Criteria)
                .HasForeignKey(pc => pc.CriteriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigurePlanetCriteria(EntityTypeBuilder<PlanetCriteria> builder)
        {
            builder.HasKey(pc => pc.Id);

            builder.Property(pc => pc.Id)
                .ValueGeneratedOnAdd();
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(u => u.Username)
                .IsUnique();

            builder.HasIndex(u => u.Email)
                .IsUnique();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Here you could add automatic audit properties like CreatedDate, ModifiedDate
            // Example:
            // foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            // {
            //     switch (entry.State)
            //     {
            //         case EntityState.Added:
            //             entry.Entity.CreatedDate = DateTime.UtcNow;
            //             break;
            //         case EntityState.Modified:
            //             entry.Entity.ModifiedDate = DateTime.UtcNow;
            //             break;
            //     }
            // }
            
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}