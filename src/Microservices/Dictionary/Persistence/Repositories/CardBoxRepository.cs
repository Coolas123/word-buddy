using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public class CardBoxRepository : BaseRepository<CardBox>, ICardBoxRepository
    {
        public CardBoxRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public async Task CreateRangeAsync(IEnumerable<CardBox> cardBoxes) {
            await dbSet.AddRangeAsync(cardBoxes);
        }
    }
}
