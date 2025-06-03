using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Dictionaries.Queries.GetFreeDicitonariesForCardPlan
{
    internal class GetFreeDicitonariesForCardPlanQueryHandler : IQueryHandler<GetFreeDicitonariesForCardPlanQuery, IEnumerable<Dictionary>>
    {
        private readonly IDictionaryRepository dictionaryRepository;

        public GetFreeDicitonariesForCardPlanQueryHandler(IDictionaryRepository dictionaryRepository) {
            this.dictionaryRepository = dictionaryRepository;
        }

        public async Task<Result<IEnumerable<Dictionary>>> Handle(GetFreeDicitonariesForCardPlanQuery request, CancellationToken cancellationToken) {
            var dictionaries = await dictionaryRepository.GetFreeDicitonariesForCardPlanAsync(request.UserId);

            if (!dictionaries.Any()) {
                return Result.Failure<IEnumerable<Dictionary>>(Domain.Errors.ApplicationError.Dictionary.DictionariesWasNotFound);
            }

            return Result.Success(dictionaries);
        }
    }
}
