using Application.Abstractions.Messaging;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.GeneratedTextHistory.Queries
{
    internal class GetGeneratedTextHistoryQueryHandler : IQueryHandler<GetGeneratedTextHistoryQuery, IEnumerable<Domain.Entities.GeneratedTextHistory>>
    {
        private readonly IGeneratedTextHistoryRepository generatedTextHistoryRepository;

        public GetGeneratedTextHistoryQueryHandler(IGeneratedTextHistoryRepository generatedTextHistoryRepository) {
            this.generatedTextHistoryRepository = generatedTextHistoryRepository;
        }

        public async Task<Result<IEnumerable<Domain.Entities.GeneratedTextHistory>>> Handle(GetGeneratedTextHistoryQuery request, CancellationToken cancellationToken) {
            var contexts = await generatedTextHistoryRepository.GetByUserIdAsync(request.UserId);

            if (contexts==null || !contexts.Any()) {
                return Result.Failure<IEnumerable<Domain.Entities.GeneratedTextHistory>>(ApplicationError.GeneratedTextHistory.GeneratedTextHistoryWasNotFound);
            }

            return Result.Success(contexts);
        }
    }
}
