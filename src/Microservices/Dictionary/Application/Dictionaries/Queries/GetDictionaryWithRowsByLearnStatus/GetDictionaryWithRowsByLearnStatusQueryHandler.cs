using Application.Abstractions.Messaging;
using Application.DictionaryRow.Queries.GetDictionaryWithRowsByLearnStatus;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Dictionaries.Queries.GetDictionaryWithRowsByLearnStatus
{
    internal class GetDictionaryWithRowsByLearnStatusQueryHandler : IQueryHandler<GetDictionaryWithRowsByLearnStatusQuery, Dictionary>
    {
        private readonly IDictionaryRepository dictionaryRepository;

        public GetDictionaryWithRowsByLearnStatusQueryHandler(IDictionaryRepository dictionaryRepository) {
            this.dictionaryRepository = dictionaryRepository;
        }
        public async Task<Result<Dictionary>> Handle(GetDictionaryWithRowsByLearnStatusQuery request, CancellationToken cancellationToken) {
            var dictionary = await dictionaryRepository.GetDictionaryWithRowsByLearnStatus(request.DictionaryId,request.DictionaryRowLearnStatus);

            if(dictionary == null) {
                return Result.Failure<Dictionary>(ApplicationError.Dictionary.DictionariesWasNotFound);
            }

            return Result.Success(dictionary);
        }
    }
}
