using Planetary.Domain.Models;

namespace Planetary.Application.Interfaces
{
    public interface IPlanetRepository
    {
        Task<IEnumerable<Planet>> GetAllAsync();
        Task<Planet?> GetByIdAsync(Guid id);
        Task<Planet> AddAsync(Planet planet);
        Task UpdateAsync(Planet planet);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}