using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public class DictionaryRepository : BaseRepository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }
    }
}
