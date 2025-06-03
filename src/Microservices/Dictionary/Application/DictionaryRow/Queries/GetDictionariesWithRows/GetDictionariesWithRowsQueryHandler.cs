using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;

namespace Application.DictionaryRow.Queries.GetDictionariesWithRows
{
    internal class GetDictionariesWithRowsQueryHandler : IQueryHandler<GetDictionariesWithRowsQuery, IEnumerable<Dictionary>>
    {
        private readonly IDictionaryRepository dictionaryRepository;

        public GetDictionariesWithRowsQueryHandler(IDictionaryRepository dictionaryRepository) {
            this.dictionaryRepository = dictionaryRepository;
        }
        public async Task<Result<IEnumerable<Dictionary>>> Handle(GetDictionariesWithRowsQuery request, CancellationToken cancellationToken) {
            var dictionaries = await dictionaryRepository.GetDictionariesWithRowsAsync(request.DictioanariesId);

            if (!dictionaries.Any()) {
                return Result.Failure<IEnumerable<Dictionary>>(Domain.Errors.ApplicationError.Dictionary.DictionariesWasNotFound);
            }

            return Result.Success<IEnumerable<Dictionary>>(dictionaries);
        }
    }
}
