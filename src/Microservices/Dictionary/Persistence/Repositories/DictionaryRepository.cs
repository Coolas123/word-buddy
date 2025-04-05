using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class DictionaryRepository : BaseRepository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public IEnumerable<Dictionary> GetDictionariesByUserId(Guid userId) {
            return dbSet.Where(x=>x.UserId == userId).ToList();
        }

        public async Task<Dictionary> GetDictionaryWithRows(Guid dictionaryId) {
            return await dbSet
                .Include(x => x.DictionaryRows)
                .FirstOrDefaultAsync(x => x.Id == dictionaryId);
        }
    }
}
