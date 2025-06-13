using Microsoft.EntityFrameworkCore;
using Planetary.Domain.Models;

namespace Planetary.Application.Interfaces
{
    public interface IPlanetaryDbContext
    {
        DbSet<Planet> Planets { get; }
        DbSet<Criteria> Criteria { get; }
        DbSet<PlanetCriteria> PlanetCriteria { get; }
        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}