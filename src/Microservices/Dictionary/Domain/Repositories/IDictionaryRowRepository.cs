using Domain.Entities;

namespace Domain.Repositories
{
    public interface IDictionaryRowRepository : IBaseRepository<DictionaryRow>
    {
        void CreateRange(IEnumerable<DictionaryRow> words);
    }
}
