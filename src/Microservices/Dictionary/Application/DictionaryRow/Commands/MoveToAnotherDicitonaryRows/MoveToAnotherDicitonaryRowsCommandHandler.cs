using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;

namespace Application.DictionaryRow.Commands.MoveToAnotherDicitonaryRows
{
    internal class MoveToAnotherDicitonaryRowsCommandHandler : ICommandHandler<MoveToAnotherDicitonaryRowsCommand>
    {
        private readonly IDictionaryRowRepository wordRepository;

        public MoveToAnotherDicitonaryRowsCommandHandler(IDictionaryRowRepository wordRepository) {
            this.wordRepository = wordRepository;
        }
        public async Task<Result> Handle(MoveToAnotherDicitonaryRowsCommand request, CancellationToken cancellationToken) {
            await wordRepository.UpdateRowsBelongAsync(request.DictionaryTargetId,request.WordsId);

            return Result.Success();
        }
    }
}
