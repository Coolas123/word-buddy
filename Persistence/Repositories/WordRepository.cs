using Domain.Entities;
using Domain.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace Persistence.Repositories
{
    public sealed class WordRepository : BaseRepository<Word>, IWordRepository
    {
        public WordRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public bool CreateRange(IEnumerable<Word> words) {
            try {
                dbSet.AddRange(words);
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
