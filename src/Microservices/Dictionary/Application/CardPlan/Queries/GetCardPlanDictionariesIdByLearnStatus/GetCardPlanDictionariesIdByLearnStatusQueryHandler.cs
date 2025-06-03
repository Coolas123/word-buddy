using Application.Abstractions.Messaging;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CardPlan.Queries.GetCardPlanDictionariesId
{
    internal class GetCardPlanDictionariesIdByLearnStatusQueryHandler : IQueryHandler<GetCardPlanDictionariesIdByLearnStatusQuery, Domain.Entities.CardPlan>
    {
        private readonly ICardPlanRepository cardPlanRepository;

        public GetCardPlanDictionariesIdByLearnStatusQueryHandler(ICardPlanRepository cardPlanRepository) {
            this.cardPlanRepository = cardPlanRepository;
        }
        public async Task<Result<Domain.Entities.CardPlan>> Handle(GetCardPlanDictionariesIdByLearnStatusQuery request, CancellationToken cancellationToken) {
            var dictionariesId = await cardPlanRepository.GetCardPlanDictionariesIdByLearnStatus(request.CardPlanId,request.LearnStatus);

            if (dictionariesId?.Dictionaries.Any() == false) {
                return Result.Failure<Domain.Entities.CardPlan>(ApplicationError.CardPlan.DictionariesWasNotFound);
            }

            return Result.Success(dictionariesId);
        }
    }
}
