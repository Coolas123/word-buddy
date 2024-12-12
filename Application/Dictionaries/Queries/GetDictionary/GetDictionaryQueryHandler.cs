using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Dictionaries.Queries.GetDictionary
{
    internal class GetDictionaryQueryHandler : IQueryHandler<GetDictionaryQuery, Dictionary>
    {
        private readonly IDictionaryRepository dictionaryRepository;

        public GetDictionaryQueryHandler(IDictionaryRepository dictionaryRepository) {
            this.dictionaryRepository = dictionaryRepository;
        }
        public async Task<Result<Dictionary>> Handle(GetDictionaryQuery request, CancellationToken cancellationToken) {
            var dictionary = await dictionaryRepository.GetDictionaryWithWordsAndTranslations(request.DictionaryId);

            if(dictionary != null) {
                return Result.Success(dictionary);
            }

            return Result.Failure<Dictionary>(Domain.Errors.ApplicationError.Dictionary.DictionariesWasNotFound);
        }
    }
}



