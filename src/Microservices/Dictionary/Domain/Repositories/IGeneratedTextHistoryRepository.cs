using Domain.Entities;

namespace Domain.Repositories
{
    public interface IGeneratedTextHistoryRepository : IBaseRepository<GeneratedTextHistory>
    {
        Task<IEnumerable<GeneratedTextHistory>> GetByUserIdAsync(Guid userId);
    }
}
