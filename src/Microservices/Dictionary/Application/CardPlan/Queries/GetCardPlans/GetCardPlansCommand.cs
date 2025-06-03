using Application.Abstractions.Messaging;

namespace Application.CardPlan.Queries.GetCardPlans
{
    public sealed class GetCardPlansCommand  :ICommand<IEnumerable<Domain.Entities.CardPlan>>
    {
        public GetCardPlansCommand(Guid userId) {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
