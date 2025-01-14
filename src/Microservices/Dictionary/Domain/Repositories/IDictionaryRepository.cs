using Domain.Entities;
using System.Collections;

namespace Domain.Repositories
{
    public interface IDictionaryRepository : IBaseRepository<Dictionary>
    {
        IEnumerable<Dictionary> GetDictionariesByUserId(Guid userId);
        Task<Dictionary> GetDictionaryWithWordsAndTranslations(Guid dictionaryId);
    }
}
