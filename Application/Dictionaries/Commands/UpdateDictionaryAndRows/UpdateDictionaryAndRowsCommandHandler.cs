using Application.Abstractions.Messaging;
using Application.Words.Commands.CreateWord;
using Application.Words.Commands.UpdateWord.UpdateWords;
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

            if(request.UpdateWordsCommand?.Words?.Count > 0) {
                var updateDictionaryRowResult = await sender.Send(request.UpdateWordsCommand);
                if (updateDictionaryRowResult.IsFailure) {
                    return updateDictionaryRowResult;
                }
            }

            if(request.CreateWordsCommand?.Words?.Count > 0) {
                 var createWordsCommandResult = await sender.Send(request.CreateWordsCommand);
                if (createWordsCommandResult.IsFailure) {
                    return createWordsCommandResult;
                }
            }

            return Result.Success();
        }
    }
}
