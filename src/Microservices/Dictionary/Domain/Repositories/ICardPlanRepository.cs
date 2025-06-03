
using Domain.Entities;
using Domain.Enums;

namespace Domain.Repositories
{
    public interface ICardPlanRepository: IBaseRepository<CardPlan>
    {
        Task<IEnumerable<CardPlan>> GetCardPlansByUserId(Guid userId);
        Task<IEnumerable<CardPlan>> GetCardPlansWithDictionariesAndCardBoxexByUserId(Guid userId);
        Task<IEnumerable<Guid>> GetDictionariesId(Guid cardPlanId,LearnStatus learnStatus);
        Task<CardPlan> GetCardPlanDictionariesIdByLearnStatus(Guid cardPlanId, LearnStatus learnStatus);
    }
}
