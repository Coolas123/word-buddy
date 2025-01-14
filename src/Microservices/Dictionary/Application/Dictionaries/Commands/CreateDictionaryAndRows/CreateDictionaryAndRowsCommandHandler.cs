using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.Dictionaries.Commands.CreateDictionaryRow
{
    internal sealed class CreateDictionaryAndRowsCommandHandler : ICommandHandler<CreateDictionaryAndRowsCommand>
    {
        private readonly ISender sender;

        public CreateDictionaryAndRowsCommandHandler(ISender sender) {
            this.sender = sender;
        }
        public async Task<Result> Handle(CreateDictionaryAndRowsCommand request, CancellationToken cancellationToken) {
            var createDictionaryResult = await sender.Send(request.CreateDictionaryCommand);

            if (createDictionaryResult.IsFailure){
                return createDictionaryResult;
            }

            request.CreateWordsCommand.DictionaryId = createDictionaryResult.Value();

            var createWordResult =  await sender.Send(request.CreateWordsCommand);

            if (createWordResult.IsFailure) {
                return createWordResult;
            }

            //request.CreateTranslationCommand.WordGuids = CreateWordResult.Value();

            //var CreateTranslationResult = await sender.Send(request.CreateTranslationCommand);

            //if (CreateTranslationResult.IsFailure) {
            //    return CreateTranslationResult;
            //}

            return Result.Success();
        }
    }
}
