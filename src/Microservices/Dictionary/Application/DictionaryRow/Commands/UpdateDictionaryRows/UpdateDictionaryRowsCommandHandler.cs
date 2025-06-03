using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.AspNetCore.Hosting;

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
            var imPathBase = @"../Application/img/";
            foreach (var row in request.DictionaryRows) {
                if (!string.IsNullOrEmpty(row.ImgBase64)) {
                    var r = row.ImgBase64.Remove(0, row.ImgBase64.IndexOf(',') + 1);
                    byte[] bytes = Convert.FromBase64String(row.ImgBase64);
                    File.WriteAllBytes(imPathBase + row.Id, bytes);
                }
                updateWords.Add(Domain.Entities.DictionaryRow.Create(
                    row.Id,
                    request.DictionaryId,
                    row.WordText,
                    row.LearnStatus,
                    row.LearnStatusChangedAt,
                    row.WordTranslation,
                    row.WordContexts,
                    string.IsNullOrEmpty(row.ImgBase64) ? null: imPathBase + row.Id,
                    row.NoteText));
            }

            wordRepository.UpdateRange(updateWords);

            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
