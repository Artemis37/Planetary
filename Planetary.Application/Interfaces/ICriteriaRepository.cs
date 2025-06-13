using Planetary.Domain.Models;

namespace Planetary.Application.Interfaces
{
    public interface ICriteriaRepository
    {
        Task<IEnumerable<Criteria>> GetAllAsync();
        Task<Criteria?> GetByIdAsync(Guid id);
        Task<Criteria> AddAsync(Criteria criteria);
        Task UpdateAsync(Criteria criteria);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}