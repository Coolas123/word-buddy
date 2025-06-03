
using Application.Abstractions.Messaging;

namespace Application.CardPlan.Queries.GetCardPlanWithDictionariesAndCardBoxes
{
    public sealed class GetCardPlanWithDictionariesAndCardBoxesQuery : IQuery<IEnumerable<Domain.Entities.CardPlan>>
    {
        public GetCardPlanWithDictionariesAndCardBoxesQuery(Guid userId) {
            UserId = userId;
        }
        public Guid UserId { get; set; }
    }
}
