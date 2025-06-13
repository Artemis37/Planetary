using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

            // ValueConverter
            var converter = new ValueConverter<IReadOnlyCollection<string>, string>(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList().AsReadOnly()
            );

            // ValueComparer
            var comparer = new ValueComparer<IReadOnlyCollection<string>>(
                (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList().AsReadOnly()
            );

            builder.Property(u => u.FavoritePlanets)
                .HasConversion(converter)
                .Metadata.SetValueComparer(comparer);
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