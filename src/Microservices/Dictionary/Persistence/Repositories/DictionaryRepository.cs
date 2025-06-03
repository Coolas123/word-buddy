using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class DictionaryRepository : BaseRepository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public async Task<IEnumerable<Dictionary>> GetDictionariesByUserIdAsync(Guid userId) {
            return await dbSet.Where(x=>x.UserId == userId).ToListAsync();
        }

        public async Task<Dictionary> GetDictionaryWithRowsAsync(Guid dictionaryId) {
            return await dbSet
                .Include(x => x.DictionaryRows)
                .FirstOrDefaultAsync(x => x.Id == dictionaryId);
        }

        public async Task<List<Dictionary>> GetDictionariesWithRowsAsync(IEnumerable<Guid> dictionariesId) {
            return await dbSet
                .Where(x=> dictionariesId.Contains(x.Id))
                .Include(x => x.DictionaryRows).ToListAsync();
        }

        public async Task InsertWordContextAsync(Guid dictionaryId, Guid dictionaryRowId, string context) {
            var dictionary = await dbSet.FindAsync(dictionaryId);
            dictionary?.AddWordContextToWord(dictionaryRowId, context);
        }

        public async Task<IEnumerable<Dictionary>> GetFreeDicitonariesForCardPlanAsync(Guid userId) {
            return await dbSet.Where(x=>x.UserId == userId && x.CardPlanId == null).ToArrayAsync();
        }

        public async Task UpdateCaprdPlanIds(IEnumerable<Guid> dictionariesId, Guid? cardPlanId) {
            await dbSet.Where(x => dictionariesId.Contains(x.Id)).ForEachAsync(x => x.ChangeCardPlanId(cardPlanId));
        }

        public Task<Dictionary> GetDictionaryWithRowsByLearnStatus(Guid id, LearnStatus learnStatus) {
            return dbSet.Where(x => x.Id == id).Include(x => x.DictionaryRows.Where(x => x.LearnStatus == learnStatus)).FirstOrDefaultAsync();
        }

        public Task<Dictionary> GetDictionaryViewModelWithRows(Guid id) {
            return dbSet.Include(x=>x.DictionaryRows).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
