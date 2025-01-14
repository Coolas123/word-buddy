using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Words.Commands.CreateWord
{
    internal sealed class CreateWordsCommandHandler : ICommandHandler<CreateWordsCommand, List<Guid>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWordRepository wordRepository;

        public CreateWordsCommandHandler(IUnitOfWork unitOfWork, IWordRepository wordRepository) {
            this.unitOfWork = unitOfWork;
            this.wordRepository = wordRepository;
        }
        public async Task<Result<List<Guid>>> Handle(CreateWordsCommand request, CancellationToken cancellationToken) {
            var wordsForSave = new List<Word>(request.Words.Count);

            var GuidList = new List<Guid>(request.Words.Count);   

            for(int i=0; i<request.Words.Count; i++) {
                var newGuid = Guid.NewGuid();

                GuidList.Add(newGuid);

                wordsForSave.Add(new Word(
                newGuid,
                (Guid)request.DictionaryId,
                request.Words[i].Text,
                LearnStatus.NotStudied,
                DateTime.Now.ToUniversalTime(),
                DateTime.Now.ToUniversalTime(),
                new Translation(newGuid, request.Words[i].Translation.Text)));
            }

            wordRepository.CreateRange(wordsForSave);

            await unitOfWork.SaveChangesAsync();
            
            return Result.Success(GuidList);
        }
    }
}
