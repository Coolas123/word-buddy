using Application.Abstractions.Messaging;
using Domain.Enums;
using Domain.Repositories;
using Domain.Shared;

namespace Application.DictionaryRow.Commands.CreateWord
{
    internal sealed class CreateDictionaryRowsCommandHandler : ICommandHandler<CreateDictionaryRowsCommand, List<Guid>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDictionaryRowRepository wordRepository;

        public CreateDictionaryRowsCommandHandler(IUnitOfWork unitOfWork, IDictionaryRowRepository wordRepository) {
            this.unitOfWork = unitOfWork;
            this.wordRepository = wordRepository;
        }
        public async Task<Result<List<Guid>>> Handle(CreateDictionaryRowsCommand request, CancellationToken cancellationToken) {
            var wordsForSave = new List<Domain.Entities.DictionaryRow>(request.DictionaryRows.Count);

            var GuidList = new List<Guid>(request.DictionaryRows.Count);   

            for(int i=0; i<request.DictionaryRows.Count; i++) {
                var newGuid = Guid.NewGuid();

                GuidList.Add(newGuid);

                wordsForSave.Add(Domain.Entities.DictionaryRow.Create(
                newGuid,
                (Guid)request.DictionaryId!,
                request.DictionaryRows[i].WordText,
                request.DictionaryRows[i].LearnStatus,
                DateTime.Now.ToUniversalTime(),
                request.DictionaryRows[i].WordTranslation,
                request.DictionaryRows[i].WordContexts,
                null,
                null));
            }

            await wordRepository.CreateRangeAsync(wordsForSave);

            await unitOfWork.SaveChangesAsync();
            
            return Result.Success(GuidList);
        }
    }
}
