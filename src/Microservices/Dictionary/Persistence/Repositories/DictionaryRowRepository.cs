using Domain.Entities;
using Domain.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace Persistence.Repositories
{
    public sealed class DictionaryRowRepository : BaseRepository<DictionaryRow>, IDictionaryRowRepository
    {
        public DictionaryRowRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public void CreateRange(IEnumerable<DictionaryRow> words) {
            dbSet.AddRange(words);
        }
    }
}
