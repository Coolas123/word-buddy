using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Words.Commands.UpdateWord.UpdateWords
{
    internal class UpdateWordsCommandHandler : ICommandHandler<UpdateWordsCommand>
    {
        private readonly IWordRepository wordRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateWordsCommandHandler(IWordRepository wordRepository, IUnitOfWork unitOfWork)
        {
            this.wordRepository = wordRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateWordsCommand request, CancellationToken cancellationToken)
        {
            var updateWords = new List<Word>(request.Words.Count());
            foreach(var word in request.Words) {
                updateWords.Add(new Word(
                    word.Id,
                    request.DictionaryId,
                    word.Text,
                    word.LearnStatus,
                    word.LearnStatusChangedAt,
                    word.CreatedAt,
                    new Translation(word.Id,word.Translation.Text)));
            }

            wordRepository.UpdateRange(updateWords);

            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
