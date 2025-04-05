using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;

namespace Application.DictionaryRow.Commands.UpdateWord.UpdateWords
{
    internal class UpdateDictionaryRowsCommandHandler : ICommandHandler<UpdateDictionaryRowsCommand>
    {
        private readonly IDictionaryRowRepository wordRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateDictionaryRowsCommandHandler(IDictionaryRowRepository wordRepository, IUnitOfWork unitOfWork)
        {
            this.wordRepository = wordRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateDictionaryRowsCommand request, CancellationToken cancellationToken)
        {
            var updateWords = new List<Domain.Entities.DictionaryRow>(request.DictionaryRows.Count());
            foreach(var row in request.DictionaryRows) {
                updateWords.Add(Domain.Entities.DictionaryRow.Create(
                    row.Id,
                    request.DictionaryId,
                    row.WordText,
                    row.LearnStatus,
                    row.LearnStatusChangedAt,
                    row.CreatedAt,
                    row.WordTranslation,
                    row.WordContexts));
            }

            wordRepository.UpdateRange(updateWords);

            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
