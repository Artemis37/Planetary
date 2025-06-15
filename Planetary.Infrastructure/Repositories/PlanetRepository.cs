using Microsoft.EntityFrameworkCore;
using Planetary.Application.Interfaces;
using Planetary.Domain.Models;
using Planetary.Infrastructure.Context;

namespace Planetary.Infrastructure.Repositories
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly PlanetaryContext _context;

        public PlanetRepository(PlanetaryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Planet>> GetAllAsync()
        {
            var connString = _context.Database.GetConnectionString();
            return await _context.Planets
                .Include(p => p.PlanetCriteria)
                .ThenInclude(pc => pc.Criteria)
                .ToListAsync();
        }

        public async Task<Planet?> GetByIdAsync(Guid id)
        {
            return await _context.Planets
                .Include(p => p.PlanetCriteria)
                .ThenInclude(pc => pc.Criteria)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Planet>> GetPlanetsByUserIdAsync(Guid userId)
        {
            return await _context.Planets
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<Planet> AddAsync(Planet planet)
        {
            await _context.Planets.AddAsync(planet);
            await _context.SaveChangesAsync();
            return planet;
        }

        public async Task UpdateAsync(Planet planet)
        {
            _context.Planets.Update(planet);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var planet = await _context.Planets.FindAsync(id);
            if (planet != null)
            {
                _context.Planets.Remove(planet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Planets.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> AddCriteria(IEnumerable<PlanetCriteria> planetCriterias)
        {
            await _context.PlanetCriteria.AddRangeAsync(planetCriterias);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}