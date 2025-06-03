using Domain.Entities;
using Domain.Enums;
using System.Collections;

namespace Domain.Repositories
{
    public interface IDictionaryRepository : IBaseRepository<Dictionary>
    {
        Task<IEnumerable<Dictionary>> GetDictionariesByUserIdAsync(Guid userId);
        Task<Dictionary> GetDictionaryWithRowsAsync(Guid dictionaryId);
        Task<List<Dictionary>> GetDictionariesWithRowsAsync(IEnumerable<Guid> dictionariesId);
        Task InsertWordContextAsync(Guid dictionaryId, Guid dictionaryRowId, string context);
        Task<IEnumerable<Dictionary>> GetFreeDicitonariesForCardPlanAsync(Guid userId);
        Task UpdateCaprdPlanIds(IEnumerable<Guid> dictionariesId, Guid? cardPlanId);
        Task<Dictionary> GetDictionaryWithRowsByLearnStatus(Guid id, LearnStatus learnStatus);
        Task<Dictionary> GetDictionaryViewModelWithRows(Guid id);
    }
}
