using Domain.Entities;

namespace Domain.Repositories
{
    public interface IWordRepository : IBaseRepository<Word>
    {
        bool CreateRange(IEnumerable<Word> words);
    }
}
