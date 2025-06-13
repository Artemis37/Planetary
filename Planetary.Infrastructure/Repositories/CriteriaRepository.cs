using Microsoft.EntityFrameworkCore;
using Planetary.Application.Interfaces;
using Planetary.Domain.Models;
using Planetary.Infrastructure.Context;
using System.Diagnostics;

namespace Planetary.Infrastructure.Repositories
{
    public class CriteriaRepository : ICriteriaRepository
    {
        private readonly PlanetaryContext _context;

        public CriteriaRepository(PlanetaryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Criteria>> GetAllAsync()
        {
            // Get and log the connection string
            var connectionString = _context.Database.GetConnectionString();
            return await _context.Criteria.ToListAsync();
        }

        public async Task<Criteria?> GetByIdAsync(Guid id)
        {
            return await _context.Criteria.FindAsync(id);
        }

        public async Task<Criteria> AddAsync(Criteria criteria)
        {
            await _context.Criteria.AddAsync(criteria);
            await _context.SaveChangesAsync();
            return criteria;
        }

        public async Task UpdateAsync(Criteria criteria)
        {
            _context.Criteria.Update(criteria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var criteria = await _context.Criteria.FindAsync(id);
            if (criteria != null)
            {
                _context.Criteria.Remove(criteria);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Criteria.AnyAsync(c => c.Id == id);
        }
    }
}