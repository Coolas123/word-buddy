using Domain.Entities;

namespace Domain.Repositories
{
    public interface ITranslationRepository : IBaseRepository<Translation>
    {
        bool CreateRange(IEnumerable<Translation> words);
    }
}
