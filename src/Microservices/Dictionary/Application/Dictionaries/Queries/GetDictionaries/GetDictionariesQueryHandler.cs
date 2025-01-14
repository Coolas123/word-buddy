using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Dictionaries.Queries.GetDictionaries
{
    internal class GetDictionariesQueryHandler : IQueryHandler<GetDictionariesQuery, IEnumerable<Dictionary>>
    {
        private readonly IDictionaryRepository dictionaryRepository;

        public GetDictionariesQueryHandler(IDictionaryRepository dictionaryRepository) {
            this.dictionaryRepository = dictionaryRepository;
        }
        public async Task<Result<IEnumerable<Dictionary>>> Handle(GetDictionariesQuery request, CancellationToken cancellationToken) {
            var dictionaries = dictionaryRepository.GetDictionariesByUserId(request.UserId);
            if (!dictionaries.Any()) {
                return Result.Failure<IEnumerable<Dictionary>>(Domain.Errors.ApplicationError.Dictionary.DictionariesWasNotFound);
            }

            return Result.Success(dictionaries);
        }
    }
}
