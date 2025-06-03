using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using static Domain.Errors.ApplicationError;

namespace Persistence.Repositories
{
    public sealed class DictionaryRowRepository : BaseRepository<DictionaryRow>, IDictionaryRowRepository
    {
        public DictionaryRowRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<Dictionary<LearnStatus,int>> CountTotalWords(IEnumerable<Guid> dicitonariesId) {
            return dbSet
                .Where(x => dicitonariesId.Contains(x.DictionaryId))
                .GroupBy(x => x.LearnStatus)
                .Select(x =>new { x.Key, Count=x.Count() })
                .ToDictionaryAsync(x=>x.Key,x=>x.Count);
        }

        public async Task CreateRangeAsync(IEnumerable<DictionaryRow> words) {
            await dbSet.AddRangeAsync(words);
        }

        public async Task UpdateLearnStatus(IEnumerable<(Guid DictionaryRowId, LearnStatus LearnStatus, DateTime LastChangedAt, bool IsCardPlan)> dictionaryRows) {
            foreach (var item in dictionaryRows) {
                var row = DictionaryRow.CreateForAttachingToDb(item.DictionaryRowId);
                dbSet.Attach(row);
                row.ChangeLearnStatus(item.LearnStatus);

                if (item.IsCardPlan) {
                    row.UpdateCardBoxLearnStatusChangedAt(item.LastChangedAt);
                }
                else {
                    row.UpdateLearnStatusChangedAt(item.LastChangedAt);
                }
            }
        }

        public async Task UpdateRowsBelongAsync(Guid dicitonaryTargetId, IEnumerable<Guid> wordsId) {
            await dbSet.Where(x => wordsId.Contains(x.Id)).ExecuteUpdateAsync(x=>x.SetProperty(p=>p.DictionaryId, dicitonaryTargetId));
        }
    }
}
