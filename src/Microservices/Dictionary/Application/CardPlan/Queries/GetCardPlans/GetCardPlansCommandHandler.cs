using Application.Abstractions.Messaging;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CardPlan.Queries.GetCardPlans
{
    internal class GetCardPlansCommandHandler : ICommandHandler<GetCardPlansCommand, IEnumerable<Domain.Entities.CardPlan>>
    {
        private readonly ICardPlanRepository cardPlanRepository;
        
        public GetCardPlansCommandHandler(ICardPlanRepository cardPlanRepository) {
            this.cardPlanRepository = cardPlanRepository;
        }

        public async Task<Result<IEnumerable<Domain.Entities.CardPlan>>> Handle(GetCardPlansCommand request, CancellationToken cancellationToken) {
            var cardPlans = await cardPlanRepository.GetCardPlansByUserId(request.UserId);

            if (!cardPlans.Any()) {
                return Result.Failure<IEnumerable<Domain.Entities.CardPlan>>(ApplicationError.CardPlan.ThereIsNoCardPlans);
            }

            return Result.Success(cardPlans);
        }
    }
}
