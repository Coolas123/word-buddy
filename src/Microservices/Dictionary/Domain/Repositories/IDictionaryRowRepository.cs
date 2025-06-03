using Domain.Entities;
using Domain.Enums;

namespace Domain.Repositories
{
    public interface IDictionaryRowRepository : IBaseRepository<DictionaryRow>
    {
        Task CreateRangeAsync(IEnumerable<DictionaryRow> words);
        Task UpdateRowsBelongAsync(Guid dicitonaryTargetId, IEnumerable<Guid> wordsId);
        Task<Dictionary<LearnStatus, int>> CountTotalWords(IEnumerable<Guid> dicitonariesId);
        Task UpdateLearnStatus(IEnumerable<(Guid DictionaryRowId, LearnStatus LearnStatus, DateTime LastChangedAt, bool IsCardPlan)> dictionaryRows);
    }
}
