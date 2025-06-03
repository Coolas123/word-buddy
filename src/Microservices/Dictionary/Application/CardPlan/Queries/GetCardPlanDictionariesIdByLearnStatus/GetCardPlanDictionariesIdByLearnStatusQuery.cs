using Application.Abstractions.Messaging;
using Domain.Enums;

namespace Application.CardPlan.Queries.GetCardPlanDictionariesId
{
    public sealed class GetCardPlanDictionariesIdByLearnStatusQuery : IQuery<Domain.Entities.CardPlan>
    {
        public GetCardPlanDictionariesIdByLearnStatusQuery(Guid cardPlanId, LearnStatus learnStatus) {
            CardPlanId = cardPlanId;
            LearnStatus = learnStatus;
        }

        public Guid CardPlanId { get; set; }
        public LearnStatus LearnStatus { get; set; }
    }
}
