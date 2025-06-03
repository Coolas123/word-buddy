using Application.Abstractions.Messaging;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CardPlan.Queries.GetCardPlanWithDictionariesAndCardBoxes
{
    internal class GetCardPlanWithDictionariesAndCardBoxesQueryHandler : IQueryHandler<GetCardPlanWithDictionariesAndCardBoxesQuery, IEnumerable<Domain.Entities.CardPlan>>
    {
        private readonly ICardPlanRepository cardPlanRepository;
        private readonly IDictionaryRowRepository rowRepository;

        public GetCardPlanWithDictionariesAndCardBoxesQueryHandler(ICardPlanRepository cardPlanRepository, IDictionaryRowRepository rowRepository) {
            this.cardPlanRepository = cardPlanRepository;
            this.rowRepository = rowRepository;
        }
        public async Task<Result<IEnumerable<Domain.Entities.CardPlan>>> Handle(GetCardPlanWithDictionariesAndCardBoxesQuery request, CancellationToken cancellationToken) {
            var cardPlans = await cardPlanRepository.GetCardPlansWithDictionariesAndCardBoxexByUserId(request.UserId);
            
            if (!cardPlans.Any()) {
                return Result.Failure<IEnumerable<Domain.Entities.CardPlan>>(ApplicationError.CardPlan.ThereIsNoCardPlans);
            }

            return Result.Success(cardPlans);
        }
    }
}
