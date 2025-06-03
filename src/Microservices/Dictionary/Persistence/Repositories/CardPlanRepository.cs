using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class CardPlanRepository : BaseRepository<CardPlan>, ICardPlanRepository
    {
        public CardPlanRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public async Task<CardPlan> GetCardPlanDictionariesIdByLearnStatus(Guid cardPlanId, LearnStatus learnStatus) {
            return await dbSet
                .Where(x => x.Id == cardPlanId)
                .Include(x => x.Dictionaries.Where(x => x.DictionaryRows.Any(x => x.LearnStatus == learnStatus)))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CardPlan>> GetCardPlansByUserId(Guid userId) {
            return await dbSet.Where(x=>x.UserId == userId).ToArrayAsync();
        }

        public async Task<IEnumerable<CardPlan>> GetCardPlansWithDictionariesAndCardBoxexByUserId(Guid userId) {
            return await dbSet
                .Where(x => x.UserId == userId)
                .Include(x=>x.Dictionaries)
                .Include(x=> x.CardBoxes)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Guid>> GetDictionariesId(Guid cardPlanId, LearnStatus learnStatus) {
            return await dbSet
                .SelectMany(x => x.Dictionaries.Where(x=>x.DictionaryRows.Any(x=>x.LearnStatus == learnStatus)))
                .Select(x=>x.Id)
                .ToArrayAsync();
        }
    }
}
