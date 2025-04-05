using Application.Abstractions.Messaging;
using Domain.Shared;
using MediatR;

namespace Application.Dictionaries.Commands.UpdateDictionaryAndRows
{
    internal class UpdateDictionaryAndRowsCommandHandler : ICommandHandler<UpdateDictionaryAndRowsCommand>
    {
        private readonly ISender sender;

        public UpdateDictionaryAndRowsCommandHandler(ISender sender) {
            this.sender = sender;
        }

        public async Task<Result> Handle(UpdateDictionaryAndRowsCommand request, CancellationToken cancellationToken) {
            var updateDictionaryResult = await sender.Send(request.UpdateDictionaryCommand);

            if (updateDictionaryResult.IsFailure) {
                return updateDictionaryResult;
            }

            if(request.UpdateDictionaryRowsCommand?.DictionaryRows.Count > 0) {
                var updateDictionaryRowResult = await sender.Send(request.UpdateDictionaryRowsCommand);
                if (updateDictionaryRowResult.IsFailure) {
                    return updateDictionaryRowResult;
                }
            }

            if(request.CreateDictionaryRowsCommand?.DictionaryRows.Count > 0) {
                 var createWordsCommandResult = await sender.Send(request.CreateDictionaryRowsCommand);
                if (createWordsCommandResult.IsFailure) {
                    return createWordsCommandResult;
                }
            }

            return Result.Success();
        }
    }
}
