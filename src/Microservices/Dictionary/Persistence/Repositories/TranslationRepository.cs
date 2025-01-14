using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class TranslationRepository : BaseRepository<Translation>, ITranslationRepository
    {
        public TranslationRepository(ApplicationDbContext dbContext) : base(dbContext) {

        }

        public bool CreateRange(IEnumerable<Translation> translations) {
            try {
                dbSet.AddRange(translations);
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
