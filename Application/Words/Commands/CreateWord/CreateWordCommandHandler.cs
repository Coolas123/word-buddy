using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Words.Commands.CreateWord
{
    internal sealed class CreateWordCommandHandler : ICommandHandler<CreateWordCommand, List<Guid>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWordRepository wordRepository;

        public CreateWordCommandHandler(IUnitOfWork unitOfWork, IWordRepository wordRepository) {
            this.unitOfWork = unitOfWork;
            this.wordRepository = wordRepository;
        }
        public async Task<Result<List<Guid>>> Handle(CreateWordCommand request, CancellationToken cancellationToken) {
            var wordsForSave = new List<Word>(request.CreateWords.Count);

            var GuidList = new List<Guid>(request.CreateWords.Count);   

            for(int i=0; i<request.CreateWords.Count; i++) {
                var newGuid = Guid.NewGuid();

                GuidList.Add(newGuid);

                wordsForSave.Add(new Word(
                newGuid,
                (Guid)request.DictionaryId,
                request.CreateWords[i].Text,
                LearnStatus.NotStudied,
                DateTime.Now.ToUniversalTime()));
            }

            wordRepository.CreateRange(wordsForSave);

            await unitOfWork.SaveChangesAsync();
            
            return Result.Success(GuidList);
        }
    }
}
