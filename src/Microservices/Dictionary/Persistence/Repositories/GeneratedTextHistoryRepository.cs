using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class GeneratedTextHistoryRepository : BaseRepository<GeneratedTextHistory>, IGeneratedTextHistoryRepository
    {
        public GeneratedTextHistoryRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public async Task<IEnumerable<GeneratedTextHistory>> GetByUserIdAsync(Guid userId) {
            return await dbSet.Where(x=>x.UserId == userId).ToArrayAsync();
        }
    }
}
