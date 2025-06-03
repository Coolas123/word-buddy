using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Dictionaries.Queries.GetDictionaryViewModelWithRows
{
    internal class GetDictionaryViewModelWithRowsQueryHandler : IQueryHandler<GetDictionaryViewModelWithRowsQuery, Dictionary>
    {
        private readonly IDictionaryRepository dictionaryRepository;

        public GetDictionaryViewModelWithRowsQueryHandler(IDictionaryRepository dictionaryRepository) {
            this.dictionaryRepository = dictionaryRepository;
        }

        public async Task<Result<Dictionary>> Handle(GetDictionaryViewModelWithRowsQuery request, CancellationToken cancellationToken) {
            var dictionary = await dictionaryRepository.GetDictionaryViewModelWithRows(request.DictionaryId);

            if (dictionary == null) {
                return Result.Failure<Dictionary>(ApplicationError.Dictionary.DictionariesWasNotFound);
            }

            return Result.Success(dictionary);
        }
    }
}
